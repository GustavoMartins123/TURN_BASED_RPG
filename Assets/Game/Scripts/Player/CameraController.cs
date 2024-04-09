using Cinemachine;
using System;
using UnityEngine;

namespace RPG.GAME
{
    public class CameraController : MonoBehaviour
    {
        private Transform target;
        private Vector2 targetLook;
        private bool inBattle;
        [SerializeField] private CharacterMovement characterMovement;
        [SerializeField] private float targetHeight = 1.9f;
        [SerializeField] private Vector2 xRotationRange = new Vector2(-70, 70);
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [Range(0.1f, 10)]
        [SerializeField] float sensitivity = 1f;
        public Quaternion lookRotation => target.rotation;

        private void LateUpdate()
        {
            if(!inBattle)
            {
                target.transform.position = characterMovement.transform.position + Vector3.up * targetHeight;
            }
            target.transform.rotation = Quaternion.Euler(targetLook.x, targetLook.y, 0);
            
        }

        public void IncrementLookRotation(Vector2 lookDelta)
        {
            targetLook += lookDelta * sensitivity;
            targetLook.x = Mathf.Clamp(targetLook.x, xRotationRange.x, xRotationRange.y);
        }
        public void SetTarget(Vector3 target)
        {
            this.target.position = target;
        }
        public void SetInBattle(bool inBattle)
        {
            this.inBattle = inBattle;
        }
    }
}
