using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.GAME
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CharacterMovement characterMovement;
        public Transform target;
        [SerializeField] private float targetHeight = 1.9f;
        [SerializeField] private Vector2 xRotationRange = new Vector2(-70, 70);
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        public bool inBatlte;


        private Vector2 targetLook;
        [Range(0.1f, 10)]
        [SerializeField] float sensitivity = 1f;
        public Quaternion lookRotation => target.rotation;

        /*private void Start()
        {
            characterMovement.onEnterInBattle += Player_OnEnterInBattle;
        }*/

        private void LateUpdate()
        {
            if(!inBatlte)
            {
                target.transform.position = characterMovement.transform.position + Vector3.up * targetHeight;
            }
            target.transform.rotation = Quaternion.Euler(targetLook.x, targetLook.y, 0);
            
        }

        void Player_OnEnterInBattle(CharacterCollisionEventArgs e)
        {
            //virtualCamera.m_LookAt = target;
            //Create a new camera for battle or change properties in editor
            
        }

        public void IncrementLookRotation(Vector2 lookDelta)
        {
            targetLook += lookDelta * sensitivity;
            targetLook.x = Mathf.Clamp(targetLook.x, xRotationRange.x, xRotationRange.y);
        }
    }
}
