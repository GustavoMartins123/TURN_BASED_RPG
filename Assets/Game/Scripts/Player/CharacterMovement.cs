using KinematicCharacterController;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

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
        public EventHandler<CharacterCollisionEventArgs> onEnterInBattle;
        public KinematicCharacterMotor motor;
        [SerializeField] private Player player;
        [SerializeField] private PlayerBattle playerBattle;
        [SerializeField] private LayerMask enemyLayer;

        [Header("Ground Movement")]
        public float maxSpeed, acceleration, rotationSpeed;
        public float gravity = 30f;
        public float jumpHeight = 1.5f;
        [Range(0.01f, 0.3f)]
        public float jumpRequestDuration = 0.1f;

        [Header("Air Movement")]
        public float airMaxSpeed = 3f;
        public float airAcceleration = 20f;
        [Min(0)]
        public float drag = 0.5f;


        private Vector3 moveInput;
        private float jumpRequestExpireTime;

        public float JumpSpeed => Mathf.Sqrt(2 * gravity * jumpHeight);


        private void Awake()
        {
            motor.CharacterController = this;
        }

        public void SetInput(in CharacterMovementInput input)
        {
            moveInput = Vector3.zero;
            if(input.moveInput != Vector2.zero)
            {
                moveInput = new Vector3(input.moveInput.x, 0, input.moveInput.y);
                moveInput = input.lookRotation * moveInput;
                moveInput.y = 0;
                moveInput.Normalize();
            }

            if(input.wantsToJump)
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

        public float ApplyDrag(float velocity, float drag, float deltaTime)
        {
            return velocity * (1f / (1f + drag * deltaTime));
        }

        public void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                if (hitCollider.TryGetComponent<Enemy>(out var enemyCharacter))
                {
                    onEnterInBattle?.Invoke(this, new CharacterCollisionEventArgs(playerBattle, enemyCharacter));
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
