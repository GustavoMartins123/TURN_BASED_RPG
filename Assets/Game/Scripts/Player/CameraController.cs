using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GAME
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CharacterMovement characterMovement;
        public Transform target;
        [SerializeField] private float targetHeight = 1.9f;
        [SerializeField] private Vector2 xRotationRange = new Vector2(-70, 70);


        private Vector2 targetLook;
        [Range(0.1f, 10)]
        [SerializeField] float sensitivity = 1f;
        public Quaternion lookRotation => target.rotation;

        private void LateUpdate()
        {
            target.transform.position = characterMovement.transform.position + Vector3.up * targetHeight;
            target.transform.rotation = Quaternion.Euler(targetLook.x, targetLook.y, 0);
        }

        public void IncrementLookRotation(Vector2 lookDelta)
        {
            targetLook += lookDelta * sensitivity;
            targetLook.x = Mathf.Clamp(targetLook.x, xRotationRange.x, xRotationRange.y);
        }
    }
}
