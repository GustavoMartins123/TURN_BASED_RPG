using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace RPG.GAME
{
    public class Player : MonoBehaviour
    {
        [SerializeField] List<CharacterBase> partners = new();
        [SerializeField] private CharacterMovement characterMovement;
        [SerializeField] private CameraController cameraController;
        public CharacterBase character;

        private PlayerController inputActions;

        private void Awake()
        {
            inputActions = new PlayerController();
            inputActions.Enable();
        }

        private void Start()
        {
            character.Team = partners;
        }

        private void Update()
        {
            Vector2 moveInput = inputActions.PlayerActions_Move.Move.ReadValue<Vector2>();

            var wantsToJump = inputActions.PlayerActions_Move.Jump.WasPressedThisFrame();

            characterMovement.SetInput(new CharacterMovementInput()
            {
                moveInput = moveInput,
                lookRotation = cameraController.lookRotation,
                wantsToJump = wantsToJump
            });

            var look = inputActions.PlayerActions_Move.Look.ReadValue<Vector2>();

            cameraController.IncrementLookRotation(new Vector2(look.y, look.x));
        }

        void GetSelectedCharacter()
        {
            //Battle only
            if (!GameManager.Instance.GetCurrentTurn())
            {
                return;
            }
            Vector2 mousePosition = inputActions.PlayerActions_Battle.MousePosition.ReadValue<Vector2>();

            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, 1<<8))
            {
                GameManager.Instance.currentTarget = hit.collider.GetComponent<Enemy>().character;
                Debug.Log(hit.collider.name);
            }
            else
            {
                Debug.Log(hit.transform.name);
                return;
            }
        }

        public void BattleInit()
        {
            inputActions.PlayerActions_Battle.Enable();
            inputActions.PlayerActions_Battle.MouseSelect.performed += ctx => GetSelectedCharacter();
            inputActions.PlayerActions_Move.Disable();
        }

        public void BattleOver()
        {
            inputActions.PlayerActions_Battle.Disable();
            inputActions.PlayerActions_Battle.MouseSelect.performed -= ctx => GetSelectedCharacter();
            inputActions.PlayerActions_Move.Enable();
        }

    }

    
}
