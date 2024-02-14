using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.GAME
{
    public class InputManager : MonoBehaviour
    {
        private PlayerController controller;
        private PlayerController.PlayerActionsActions actions;

        // Start is called before the first frame update
        void Awake()
        {
            controller = new PlayerController();
            actions = controller.PlayerActions;
        }

        private void OnEnable()
        {
            actions.Enable();
        }

        private void OnDisable()
        {
            actions.Disable();
        }

        private void Start()
        {
            actions.MouseSelect.performed += ctx => GetSelectedCharacter();    
        }
        void GetSelectedCharacter()
        {
            //Battle only
            Vector2 mousePosition = actions.MousePosition.ReadValue<Vector2>();

            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, 1<<8))
            {
                GameManager.Instance.currentTarget = hit.collider.GetComponent<Enemy>().character;
            }
            else
            {
                return;
            }
        }
    }
}
