// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Input : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Input()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input"",
    ""maps"": [
        {
            ""name"": ""BattleActionMap"",
            ""id"": ""4f045895-ea05-4edb-9307-94413d41c2f3"",
            ""actions"": [
                {
                    ""name"": ""MoveUp"",
                    ""type"": ""Button"",
                    ""id"": ""b06f0e88-ea03-4bfc-95b6-8bdeb6d652ca"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveDown"",
                    ""type"": ""Button"",
                    ""id"": ""450bfaa5-99f8-4909-8811-1dd9f7138115"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveLeft"",
                    ""type"": ""Button"",
                    ""id"": ""7c8aef8b-679c-4ff2-8493-538f66f6492e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveRight"",
                    ""type"": ""Button"",
                    ""id"": ""88d8364a-56b9-4618-b690-5a28fad23f05"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""99745c16-d5e8-4611-ae00-e53ef155477e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CustomScreen"",
                    ""type"": ""Button"",
                    ""id"": ""d516abe5-d656-47b8-9305-098299dce854"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UseTopAbility"",
                    ""type"": ""Button"",
                    ""id"": ""8cdc90da-de2c-4786-a7dc-6204c0df0824"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2b1392ad-d1d6-4d7c-a044-f336a50271fd"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0c3a87d3-65de-4644-a6de-9ea101e01885"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ecab869b-32ff-41d1-8629-940f1bd5a442"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""986dc677-5417-4237-bd4c-32289360ec18"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c385ee5c-547e-4778-8d05-ca886c1d5022"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b19277e5-2446-4871-b92f-4e0465f36b5a"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""57ff7370-5cb8-4e2b-8409-e92037f369f9"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2ade3bc6-1e3b-44f2-b18e-5f41cf579e58"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""39ea8faf-7aed-4034-a8b6-08052bfa76b7"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""af20cf7b-100d-4d3c-bc88-346fb368f0d5"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a32ed062-f235-4317-86f5-0d4c71641ecd"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d8c9e239-90fd-4da5-a31c-49cb41604dbb"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f058bc81-edf8-4051-a49a-38fbd7f51447"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""96a8229e-28dd-418a-9877-30a9afab2e8b"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b4351b5e-1377-469a-b403-4295a8aebabf"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CustomScreen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4bdf2515-fff3-41f0-8381-cdecc0a054a9"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CustomScreen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d735a3d5-44dc-41f3-af8b-a36a5fd93cb0"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CustomScreen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""722f4168-b32f-4d8c-acec-632687b69fbe"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseTopAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""433dd061-3d50-4b6b-b7aa-66cbde154ccc"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseTopAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // BattleActionMap
        m_BattleActionMap = asset.FindActionMap("BattleActionMap", throwIfNotFound: true);
        m_BattleActionMap_MoveUp = m_BattleActionMap.FindAction("MoveUp", throwIfNotFound: true);
        m_BattleActionMap_MoveDown = m_BattleActionMap.FindAction("MoveDown", throwIfNotFound: true);
        m_BattleActionMap_MoveLeft = m_BattleActionMap.FindAction("MoveLeft", throwIfNotFound: true);
        m_BattleActionMap_MoveRight = m_BattleActionMap.FindAction("MoveRight", throwIfNotFound: true);
        m_BattleActionMap_Pause = m_BattleActionMap.FindAction("Pause", throwIfNotFound: true);
        m_BattleActionMap_CustomScreen = m_BattleActionMap.FindAction("CustomScreen", throwIfNotFound: true);
        m_BattleActionMap_UseTopAbility = m_BattleActionMap.FindAction("UseTopAbility", throwIfNotFound: true);
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

    // BattleActionMap
    private readonly InputActionMap m_BattleActionMap;
    private IBattleActionMapActions m_BattleActionMapActionsCallbackInterface;
    private readonly InputAction m_BattleActionMap_MoveUp;
    private readonly InputAction m_BattleActionMap_MoveDown;
    private readonly InputAction m_BattleActionMap_MoveLeft;
    private readonly InputAction m_BattleActionMap_MoveRight;
    private readonly InputAction m_BattleActionMap_Pause;
    private readonly InputAction m_BattleActionMap_CustomScreen;
    private readonly InputAction m_BattleActionMap_UseTopAbility;
    public struct BattleActionMapActions
    {
        private @Input m_Wrapper;
        public BattleActionMapActions(@Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveUp => m_Wrapper.m_BattleActionMap_MoveUp;
        public InputAction @MoveDown => m_Wrapper.m_BattleActionMap_MoveDown;
        public InputAction @MoveLeft => m_Wrapper.m_BattleActionMap_MoveLeft;
        public InputAction @MoveRight => m_Wrapper.m_BattleActionMap_MoveRight;
        public InputAction @Pause => m_Wrapper.m_BattleActionMap_Pause;
        public InputAction @CustomScreen => m_Wrapper.m_BattleActionMap_CustomScreen;
        public InputAction @UseTopAbility => m_Wrapper.m_BattleActionMap_UseTopAbility;
        public InputActionMap Get() { return m_Wrapper.m_BattleActionMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BattleActionMapActions set) { return set.Get(); }
        public void SetCallbacks(IBattleActionMapActions instance)
        {
            if (m_Wrapper.m_BattleActionMapActionsCallbackInterface != null)
            {
                @MoveUp.started -= m_Wrapper.m_BattleActionMapActionsCallbackInterface.OnMoveUp;
                @MoveUp.performed -= m_Wrapper.m_BattleActionMapActionsCallbackInterface.OnMoveUp;
                @MoveUp.canceled -= m_Wrapper.m_BattleActionMapActionsCallbackInterface.OnMoveUp;
                @MoveDown.started -= m_Wrapper.m_BattleActionMapActionsCallbackInterface.OnMoveDown;
                @MoveDown.performed -= m_Wrapper.m_BattleActionMapActionsCallbackInterface.OnMoveDown;
                @MoveDown.canceled -= m_Wrapper.m_BattleActionMapActionsCallbackInterface.OnMoveDown;
                @MoveLeft.started -= m_Wrapper.m_BattleActionMapActionsCallbackInterface.OnMoveLeft;
                @MoveLeft.performed -= m_Wrapper.m_BattleActionMapActionsCallbackInterface.OnMoveLeft;
                @MoveLeft.canceled -= m_Wrapper.m_BattleActionMapActionsCallbackInterface.OnMoveLeft;
                @MoveRight.started -= m_Wrapper.m_BattleActionMapActionsCallbackInterface.OnMoveRight;
                @MoveRight.performed -= m_Wrapper.m_BattleActionMapActionsCallbackInterface.OnMoveRight;
                @MoveRight.canceled -= m_Wrapper.m_BattleActionMapActionsCallbackInterface.OnMoveRight;
                @Pause.started -= m_Wrapper.m_BattleActionMapActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_BattleActionMapActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_BattleActionMapActionsCallbackInterface.OnPause;
                @CustomScreen.started -= m_Wrapper.m_BattleActionMapActionsCallbackInterface.OnCustomScreen;
                @CustomScreen.performed -= m_Wrapper.m_BattleActionMapActionsCallbackInterface.OnCustomScreen;
                @CustomScreen.canceled -= m_Wrapper.m_BattleActionMapActionsCallbackInterface.OnCustomScreen;
                @UseTopAbility.started -= m_Wrapper.m_BattleActionMapActionsCallbackInterface.OnUseTopAbility;
                @UseTopAbility.performed -= m_Wrapper.m_BattleActionMapActionsCallbackInterface.OnUseTopAbility;
                @UseTopAbility.canceled -= m_Wrapper.m_BattleActionMapActionsCallbackInterface.OnUseTopAbility;
            }
            m_Wrapper.m_BattleActionMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveUp.started += instance.OnMoveUp;
                @MoveUp.performed += instance.OnMoveUp;
                @MoveUp.canceled += instance.OnMoveUp;
                @MoveDown.started += instance.OnMoveDown;
                @MoveDown.performed += instance.OnMoveDown;
                @MoveDown.canceled += instance.OnMoveDown;
                @MoveLeft.started += instance.OnMoveLeft;
                @MoveLeft.performed += instance.OnMoveLeft;
                @MoveLeft.canceled += instance.OnMoveLeft;
                @MoveRight.started += instance.OnMoveRight;
                @MoveRight.performed += instance.OnMoveRight;
                @MoveRight.canceled += instance.OnMoveRight;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @CustomScreen.started += instance.OnCustomScreen;
                @CustomScreen.performed += instance.OnCustomScreen;
                @CustomScreen.canceled += instance.OnCustomScreen;
                @UseTopAbility.started += instance.OnUseTopAbility;
                @UseTopAbility.performed += instance.OnUseTopAbility;
                @UseTopAbility.canceled += instance.OnUseTopAbility;
            }
        }
    }
    public BattleActionMapActions @BattleActionMap => new BattleActionMapActions(this);
    public interface IBattleActionMapActions
    {
        void OnMoveUp(InputAction.CallbackContext context);
        void OnMoveDown(InputAction.CallbackContext context);
        void OnMoveLeft(InputAction.CallbackContext context);
        void OnMoveRight(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnCustomScreen(InputAction.CallbackContext context);
        void OnUseTopAbility(InputAction.CallbackContext context);
    }
}
