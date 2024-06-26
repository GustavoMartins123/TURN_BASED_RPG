//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Input/PlayerController.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerController: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerController"",
    ""maps"": [
        {
            ""name"": ""PlayerActions_Move"",
            ""id"": ""d9ab4c48-abc1-4abd-8068-b99e447fc6bf"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""7bb0e5f6-d9a8-4b04-81f7-b0537b4a8267"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""45ca180c-a3da-4ce7-b1ed-3fe65c430970"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""e4e06c59-150a-40c8-8022-4a67524a0b27"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""49750377-086c-4def-bddd-537286a01235"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""6052b96e-8661-45c8-be80-bed31acdcb78"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""aa9414f1-74c2-4413-b54d-f359d70ce63c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ccc4aff1-e875-4864-86d2-66d265ccc08c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7d77fd0e-ede9-42ac-a3c6-bbb15719b747"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b33a1371-3763-49ed-b484-abac23495e41"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""95a18659-c9da-45a8-86fc-3a727f3b432d"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlayerActions_Battle"",
            ""id"": ""4f4832db-0de7-4d44-ab0f-9d525ac11b7a"",
            ""actions"": [
                {
                    ""name"": ""MouseSelect"",
                    ""type"": ""Button"",
                    ""id"": ""615045af-b221-487a-8b3a-72df686a7a0a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9ba6847c-bf91-43f0-ba07-c0cac006975d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MoveSelectAction"",
                    ""type"": ""Value"",
                    ""id"": ""1c2e2ad8-64ee-43f3-82f2-648f94b22d91"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SelectAction"",
                    ""type"": ""Button"",
                    ""id"": ""1d20c23e-4ea1-4719-ab4b-62a44a64508b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6b7572ea-4ba4-4ebd-a203-8b9130fdf520"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""87f4701b-3903-412d-be04-5e34704e5852"",
                    ""path"": ""<Pen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7d6d46ac-5f3d-4571-8901-8633cc03deb6"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseSelect"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""MoveSelector"",
                    ""id"": ""ab971fa4-7c18-4360-991b-77cc62c2a5eb"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveSelectAction"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e9e81ace-bb08-4de6-9d9b-10721f6f234e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""MoveSelectAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2ad7533e-618d-489e-987e-990b25c3f451"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""MoveSelectAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c6991c2d-20a7-4677-82b3-bda876286760"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""MoveSelectAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b70c09cf-c2d0-4f74-b8b2-87eff54da124"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""MoveSelectAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2df288ba-5056-400a-93fa-df3aa94779a6"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""SelectAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlayerActions_LookCamera"",
            ""id"": ""396d8d7a-91aa-4b72-b1a9-c102d351ae0e"",
            ""actions"": [
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""5212abc0-166b-4d5d-af73-2cf7576b01a2"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8b0c5723-b417-4a0c-80a8-70c50d24d7d7"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(y=-1)"",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c7e5e639-50f2-4b6d-a24c-e98b10e25172"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyBoard"",
            ""bindingGroup"": ""KeyBoard"",
            ""devices"": []
        },
        {
            ""name"": ""GamePad"",
            ""bindingGroup"": ""GamePad"",
            ""devices"": []
        }
    ]
}");
        // PlayerActions_Move
        m_PlayerActions_Move = asset.FindActionMap("PlayerActions_Move", throwIfNotFound: true);
        m_PlayerActions_Move_Move = m_PlayerActions_Move.FindAction("Move", throwIfNotFound: true);
        m_PlayerActions_Move_Jump = m_PlayerActions_Move.FindAction("Jump", throwIfNotFound: true);
        // PlayerActions_Battle
        m_PlayerActions_Battle = asset.FindActionMap("PlayerActions_Battle", throwIfNotFound: true);
        m_PlayerActions_Battle_MouseSelect = m_PlayerActions_Battle.FindAction("MouseSelect", throwIfNotFound: true);
        m_PlayerActions_Battle_MousePosition = m_PlayerActions_Battle.FindAction("MousePosition", throwIfNotFound: true);
        m_PlayerActions_Battle_MoveSelectAction = m_PlayerActions_Battle.FindAction("MoveSelectAction", throwIfNotFound: true);
        m_PlayerActions_Battle_SelectAction = m_PlayerActions_Battle.FindAction("SelectAction", throwIfNotFound: true);
        // PlayerActions_LookCamera
        m_PlayerActions_LookCamera = asset.FindActionMap("PlayerActions_LookCamera", throwIfNotFound: true);
        m_PlayerActions_LookCamera_Look = m_PlayerActions_LookCamera.FindAction("Look", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PlayerActions_Move
    private readonly InputActionMap m_PlayerActions_Move;
    private List<IPlayerActions_MoveActions> m_PlayerActions_MoveActionsCallbackInterfaces = new List<IPlayerActions_MoveActions>();
    private readonly InputAction m_PlayerActions_Move_Move;
    private readonly InputAction m_PlayerActions_Move_Jump;
    public struct PlayerActions_MoveActions
    {
        private @PlayerController m_Wrapper;
        public PlayerActions_MoveActions(@PlayerController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerActions_Move_Move;
        public InputAction @Jump => m_Wrapper.m_PlayerActions_Move_Jump;
        public InputActionMap Get() { return m_Wrapper.m_PlayerActions_Move; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions_MoveActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions_MoveActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActions_MoveActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActions_MoveActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
        }

        private void UnregisterCallbacks(IPlayerActions_MoveActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
        }

        public void RemoveCallbacks(IPlayerActions_MoveActions instance)
        {
            if (m_Wrapper.m_PlayerActions_MoveActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions_MoveActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActions_MoveActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActions_MoveActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions_MoveActions @PlayerActions_Move => new PlayerActions_MoveActions(this);

    // PlayerActions_Battle
    private readonly InputActionMap m_PlayerActions_Battle;
    private List<IPlayerActions_BattleActions> m_PlayerActions_BattleActionsCallbackInterfaces = new List<IPlayerActions_BattleActions>();
    private readonly InputAction m_PlayerActions_Battle_MouseSelect;
    private readonly InputAction m_PlayerActions_Battle_MousePosition;
    private readonly InputAction m_PlayerActions_Battle_MoveSelectAction;
    private readonly InputAction m_PlayerActions_Battle_SelectAction;
    public struct PlayerActions_BattleActions
    {
        private @PlayerController m_Wrapper;
        public PlayerActions_BattleActions(@PlayerController wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseSelect => m_Wrapper.m_PlayerActions_Battle_MouseSelect;
        public InputAction @MousePosition => m_Wrapper.m_PlayerActions_Battle_MousePosition;
        public InputAction @MoveSelectAction => m_Wrapper.m_PlayerActions_Battle_MoveSelectAction;
        public InputAction @SelectAction => m_Wrapper.m_PlayerActions_Battle_SelectAction;
        public InputActionMap Get() { return m_Wrapper.m_PlayerActions_Battle; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions_BattleActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions_BattleActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActions_BattleActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActions_BattleActionsCallbackInterfaces.Add(instance);
            @MouseSelect.started += instance.OnMouseSelect;
            @MouseSelect.performed += instance.OnMouseSelect;
            @MouseSelect.canceled += instance.OnMouseSelect;
            @MousePosition.started += instance.OnMousePosition;
            @MousePosition.performed += instance.OnMousePosition;
            @MousePosition.canceled += instance.OnMousePosition;
            @MoveSelectAction.started += instance.OnMoveSelectAction;
            @MoveSelectAction.performed += instance.OnMoveSelectAction;
            @MoveSelectAction.canceled += instance.OnMoveSelectAction;
            @SelectAction.started += instance.OnSelectAction;
            @SelectAction.performed += instance.OnSelectAction;
            @SelectAction.canceled += instance.OnSelectAction;
        }

        private void UnregisterCallbacks(IPlayerActions_BattleActions instance)
        {
            @MouseSelect.started -= instance.OnMouseSelect;
            @MouseSelect.performed -= instance.OnMouseSelect;
            @MouseSelect.canceled -= instance.OnMouseSelect;
            @MousePosition.started -= instance.OnMousePosition;
            @MousePosition.performed -= instance.OnMousePosition;
            @MousePosition.canceled -= instance.OnMousePosition;
            @MoveSelectAction.started -= instance.OnMoveSelectAction;
            @MoveSelectAction.performed -= instance.OnMoveSelectAction;
            @MoveSelectAction.canceled -= instance.OnMoveSelectAction;
            @SelectAction.started -= instance.OnSelectAction;
            @SelectAction.performed -= instance.OnSelectAction;
            @SelectAction.canceled -= instance.OnSelectAction;
        }

        public void RemoveCallbacks(IPlayerActions_BattleActions instance)
        {
            if (m_Wrapper.m_PlayerActions_BattleActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions_BattleActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActions_BattleActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActions_BattleActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions_BattleActions @PlayerActions_Battle => new PlayerActions_BattleActions(this);

    // PlayerActions_LookCamera
    private readonly InputActionMap m_PlayerActions_LookCamera;
    private List<IPlayerActions_LookCameraActions> m_PlayerActions_LookCameraActionsCallbackInterfaces = new List<IPlayerActions_LookCameraActions>();
    private readonly InputAction m_PlayerActions_LookCamera_Look;
    public struct PlayerActions_LookCameraActions
    {
        private @PlayerController m_Wrapper;
        public PlayerActions_LookCameraActions(@PlayerController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Look => m_Wrapper.m_PlayerActions_LookCamera_Look;
        public InputActionMap Get() { return m_Wrapper.m_PlayerActions_LookCamera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions_LookCameraActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions_LookCameraActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActions_LookCameraActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActions_LookCameraActionsCallbackInterfaces.Add(instance);
            @Look.started += instance.OnLook;
            @Look.performed += instance.OnLook;
            @Look.canceled += instance.OnLook;
        }

        private void UnregisterCallbacks(IPlayerActions_LookCameraActions instance)
        {
            @Look.started -= instance.OnLook;
            @Look.performed -= instance.OnLook;
            @Look.canceled -= instance.OnLook;
        }

        public void RemoveCallbacks(IPlayerActions_LookCameraActions instance)
        {
            if (m_Wrapper.m_PlayerActions_LookCameraActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions_LookCameraActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActions_LookCameraActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActions_LookCameraActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions_LookCameraActions @PlayerActions_LookCamera => new PlayerActions_LookCameraActions(this);
    private int m_KeyBoardSchemeIndex = -1;
    public InputControlScheme KeyBoardScheme
    {
        get
        {
            if (m_KeyBoardSchemeIndex == -1) m_KeyBoardSchemeIndex = asset.FindControlSchemeIndex("KeyBoard");
            return asset.controlSchemes[m_KeyBoardSchemeIndex];
        }
    }
    private int m_GamePadSchemeIndex = -1;
    public InputControlScheme GamePadScheme
    {
        get
        {
            if (m_GamePadSchemeIndex == -1) m_GamePadSchemeIndex = asset.FindControlSchemeIndex("GamePad");
            return asset.controlSchemes[m_GamePadSchemeIndex];
        }
    }
    public interface IPlayerActions_MoveActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
    public interface IPlayerActions_BattleActions
    {
        void OnMouseSelect(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnMoveSelectAction(InputAction.CallbackContext context);
        void OnSelectAction(InputAction.CallbackContext context);
    }
    public interface IPlayerActions_LookCameraActions
    {
        void OnLook(InputAction.CallbackContext context);
    }
}
