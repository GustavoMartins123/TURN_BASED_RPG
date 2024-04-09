using KinematicCharacterController;
using System;
using UnityEngine;

namespace RPG.GAME
{
    public struct CharacterMovementInput
    {
        public Vector2 moveInput;
        public Quaternion lookRotation; 
        public bool wantsToJump;
    }
    [RequireComponent(typeof(KinematicCharacterMotor))]
    public class CharacterMovement : MonoBehaviour, ICharacterController
    {
        public Action<CharacterCollisionEventArgs> onEnterInBattle;
        public float JumpSpeed => Mathf.Sqrt(2 * gravity * jumpHeight);
        private Vector3 moveInput;
        private float jumpRequestExpireTime;
        [SerializeField] private KinematicCharacterMotor motor;
        [SerializeField] private Player player;
        [SerializeField] private PlayerBattle playerBattle;

        [Header("Ground Movement")]
        [SerializeField] private float maxSpeed, acceleration, rotationSpeed;
        [SerializeField] private float gravity = 30f;
        [SerializeField] private float jumpHeight = 1.5f;
        [Range(0.01f, 0.3f)]
        [SerializeField] private float jumpRequestDuration = 0.1f;

        [Header("Air Movement")]
        [SerializeField] private float airMaxSpeed = 3f;
        [SerializeField] private float airAcceleration = 20f;
        [Min(0)]
        [SerializeField] private float drag = 0.5f;


        private void Awake()
        {
            motor.CharacterController = this;
        }

        public void SetInput(in CharacterMovementInput input)
        {
            moveInput = Vector3.zero;
            if (input.moveInput != Vector2.zero)
            {
                moveInput = new Vector3(input.moveInput.x, 0, input.moveInput.y);
                moveInput = input.lookRotation * moveInput;
                moveInput.y = 0;
                moveInput.Normalize();
            }

            if (input.wantsToJump)
            {
                jumpRequestExpireTime = Time.time + jumpRequestDuration;

            }
        }

        public void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
        {
            if (moveInput != Vector3.zero)
            {
                var targetRotation = Quaternion.LookRotation(moveInput);
                currentRotation = Quaternion.Slerp(currentRotation, targetRotation, rotationSpeed * deltaTime);
            }
        }

        public void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
        {
            if (motor.GroundingStatus.IsStableOnGround)
            {
                var targetVelocity = moveInput * maxSpeed;
                currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, acceleration * Time.deltaTime);

                if (Time.time < jumpRequestExpireTime)
                {
                    currentVelocity.y = JumpSpeed;
                    jumpRequestExpireTime = 0;
                    motor.ForceUnground();
                }
            }
            else
            {
                var targetVelocityXZ = new Vector2(moveInput.x, moveInput.z) * airMaxSpeed;
                var currentVelocityXZ = new Vector2(currentVelocity.x, currentVelocity.z);
                currentVelocityXZ = Vector2.MoveTowards(currentVelocityXZ, targetVelocityXZ, airAcceleration * Time.deltaTime);
                currentVelocity.x = ApplyDrag(currentVelocityXZ.x, drag, deltaTime);
                currentVelocity.z = ApplyDrag(currentVelocityXZ.y, drag, deltaTime);

                currentVelocity.y -= gravity * Time.deltaTime;
            }
        }

        private float ApplyDrag(float velocity, float drag, float deltaTime)
        {
            return velocity * (1f / (1f + drag * deltaTime));
        }

        public void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
        {
            if (hitCollider.gameObject.layer == 8)
            {
                Enemy enemy = hitCollider.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    onEnterInBattle?.Invoke(new CharacterCollisionEventArgs(playerBattle, enemy));
                }
            }
        }

        #region not implemented

        public void AfterCharacterUpdate(float deltaTime)
        {

        }

        public void BeforeCharacterUpdate(float deltaTime)
        {

        }

        public bool IsColliderValidForCollisions(Collider coll)
        {
            return true;
        }

        public void OnDiscreteCollisionDetected(Collider hitCollider)
        {

        }

        public void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
        {

        }

        public void PostGroundingUpdate(float deltaTime)
        {

        }

        public void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport)
        {

        }

        #endregion
    }

    public class CharacterCollisionEventArgs : EventArgs
    {
        public PlayerBattle playerCharacter;
        public Enemy enemyCharacter;

        public CharacterCollisionEventArgs(PlayerBattle player, Enemy enemy)
        {
            playerCharacter = player;
            enemyCharacter = enemy;
        }
    }
}
