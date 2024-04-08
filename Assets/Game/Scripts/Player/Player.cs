using UnityEngine;
using UnityEngine.EventSystems;

namespace RPG.GAME
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private EventSystem eventSystem;
        [SerializeField] private CharacterMovement characterMovement;
        [SerializeField] private CameraController cameraController;
        private PlayerController inputActions;

        [SerializeField] private GameObject battleScreen, uiSelectorAction;
        [SerializeField] private GameObject[] uiActions;
        [SerializeField] int currentIndexX = 0, currentIndexY = 0;

        private void Awake()
        {
            inputActions = new PlayerController();
            inputActions.PlayerActions_Move.Enable();
            inputActions.PlayerActions_LookCamera.Enable();
        }

        private void Start()
        {
            battleScreen.SetActive(false);
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

        private void Player_MoveSelectorAction()
        {
            Vector2 move = inputActions.PlayerActions_Battle.MoveSelectAction.ReadValue<Vector2>();
            int directionX = Mathf.FloorToInt(move.x);
            int directionY = Mathf.FloorToInt(move.y);
            currentIndexX = GetCurrentIndexX();
            currentIndexY = currentIndexX;
            if (directionX != 0)
            {
                int targetIndexX = currentIndexX + directionX;
                if (targetIndexX < 0)
                {
                    targetIndexX = uiActions.Length - 1;
                }
                else if (targetIndexX >= uiActions.Length)
                {
                    targetIndexX = 0;
                }
                uiSelectorAction.transform.SetParent(uiActions[targetIndexX].transform);
            }

            if (directionY != 0)
            {
                int offset = (directionY > 0) ? -2 : 2;
                int targetIndexY = currentIndexY + offset;
                if (targetIndexY >= 0 && targetIndexY < uiActions.Length)
                {
                    uiSelectorAction.transform.SetParent(uiActions[targetIndexY].transform);
                }
                else
                {
                    if(targetIndexY < 0)
                    {
                        targetIndexY = targetIndexY == -1? uiActions.Length - 1: uiActions.Length -2;
                    }
                    else
                    {
                        targetIndexY = targetIndexY == 7? 1 : targetIndexY > uiActions.Length? 1 : 0;
                    }
                    uiSelectorAction.transform.SetParent(uiActions[targetIndexY].transform);
                }
            }
            uiSelectorAction.transform.position = uiSelectorAction.transform.parent.position;
            uiSelectorAction.transform.SetAsFirstSibling();
            
        }
        private int GetCurrentIndexX()
        {
            Transform parent = uiSelectorAction.transform.parent;
            for (int i = 0; i < uiActions.Length; i++)
            {
                if (uiActions[i].transform == parent)
                {
                    return i;
                }
            }
            return -1;
        }

        void SelectAction()
        {
            eventSystem.SetSelectedGameObject(uiSelectorAction.transform.parent.gameObject);
            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.pressPosition = uiSelectorAction.transform.position;
            ExecuteEvents.Execute(uiSelectorAction, pointerEventData, ExecuteEvents.pointerClickHandler);
        }

        

        public void BattleInit()
        {
            inputActions.PlayerActions_Battle.Enable();
            inputActions.PlayerActions_Move.Disable();
            battleScreen.SetActive(true);


            inputActions.PlayerActions_Battle.MoveSelectAction.performed += ctx => Player_MoveSelectorAction();
            inputActions.PlayerActions_Battle.SelectAction.performed += ctx => SelectAction();
            cameraController.inBatlte = true;
            //uiSelectorAction.transform.SetParent(uiActions[0].transform);
        }

        public void BattleOver()
        {
            inputActions.PlayerActions_Battle.Disable();
            inputActions.PlayerActions_Move.Enable();
            battleScreen.SetActive(false);
        }

        public void PlayerTurnOrEnemy(bool activate)
        {
            battleScreen.SetActive(activate);
            if (!activate)
            {
                inputActions.PlayerActions_Battle.Disable();
            }
            else
            {
                inputActions.PlayerActions_Battle.Enable();
            }
        }
    }

    
}
