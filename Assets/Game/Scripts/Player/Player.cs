using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GAME
{
    public class Player : MonoBehaviour
    {
        public EventHandler<RaycastHit> onSelectTargetInBattle;
        [SerializeField] private CharacterMovement characterMovement;
        [SerializeField] private CameraController cameraController;
        private PlayerController inputActions;

        [SerializeField] private GameObject battleScreen;

        private void Awake()
        {
            inputActions = new PlayerController();
            inputActions.Enable();
            inputActions.PlayerActions_Battle.OpenBattleScreen.performed += ctx => OpenBattleScreen();
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

            var look = inputActions.PlayerActions_LookCamera.Look.ReadValue<Vector2>();
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
                onSelectTargetInBattle?.Invoke(this, hit);
                Debug.Log(hit.collider.name);
            }
            else
            {
                return;
            }
        }

        void OpenBattleScreen()
        {
            battleScreen.SetActive(!battleScreen.activeSelf);
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
