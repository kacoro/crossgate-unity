// GENERATED AUTOMATICALLY FROM 'Assets/InputSystem/PlayerInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""GamePlay"",
            ""id"": ""727d86b4-49b1-4396-b32f-ff7754ceded2"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2d52e438-4ad4-453f-ae72-faf75ecac8ab"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ok"",
                    ""type"": ""Button"",
                    ""id"": ""d771f297-705f-43e5-b25a-c8be5dd8632a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""1b506d06-9c4c-4f7b-8266-5a295a438097"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Start"",
                    ""type"": ""Button"",
                    ""id"": ""566a3cbc-42c8-4993-994f-08a12123475e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseDown"",
                    ""type"": ""Button"",
                    ""id"": ""9e6adf8b-33df-4bdf-9132-b9183c8c1892"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseUp"",
                    ""type"": ""Button"",
                    ""id"": ""9369a429-336c-4bcf-a45d-89966f0cf58a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseDrag"",
                    ""type"": ""PassThrough"",
                    ""id"": ""6cd838d7-1b2e-43ba-a1eb-09d81b1f3652"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""677bcead-18cc-4c1a-82e8-771ecd3ec8cb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""67cd35fa-4541-41e3-8e6c-9e49766efd07"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""ae5221c1-5688-436d-8b01-5af4dbaa7b30"",
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
                    ""id"": ""f51b7d4d-99ff-4943-986b-73a8e01dc182"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f127a548-01ca-43f5-97b7-0f0b54177af6"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c1b70c96-e5f7-4881-a482-45dc3ba3db6f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ff5ee55f-dd52-43bd-8983-2db684c8a60c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arroow"",
                    ""id"": ""6da5d1df-2ff0-4289-a76d-c1ea7db0f558"",
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
                    ""id"": ""5fe72e78-c631-4d4e-a99f-78eb3b4156d4"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""90eb4b12-12fb-4fbe-a749-f2ce3bee8098"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f82b895b-06bd-41f6-a631-be7de511b334"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f778272a-ce02-4e6d-b138-77fab2c43f13"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""1233ba0b-5b09-4662-8352-847c8e927663"",
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
                    ""id"": ""ea6bc1ed-709a-4c32-8dc1-81783054de5b"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxGamePad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e9d03bf3-3a41-4e56-b7ce-cccbbec4fc0b"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxGamePad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4d70a539-7e10-4322-bc2f-a6ab1ec8d15e"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxGamePad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4725e045-97f8-4c52-897c-9b6830e3209a"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxGamePad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""fe8c7c10-e63b-40c8-80ad-8a4903539abc"",
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
                    ""id"": ""6a81f22d-6158-4223-b25c-6b0e6b44ad03"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxGamePad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e5d6fd9e-1859-4771-8fc0-f474c93f04ef"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxGamePad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8156653b-1a7f-41c2-bf8c-98c0b0abfb11"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxGamePad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3a7e46f5-2d63-46b5-b700-248435f56718"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxGamePad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""1f4b9425-3d07-4e7d-8115-6ca5bde31645"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Ok"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b0f0ffc6-a862-43bc-a2a5-de220bc4f13d"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxGamePad"",
                    ""action"": ""Ok"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""24c818ea-9d32-4b06-94f1-9b81b4f206ab"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Ok"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1189a5ea-6e94-4862-bf77-170d270a053e"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""095068e3-02da-4665-8e82-75abe3abf1e4"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxGamePad"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1816960f-a1ce-42df-aeb8-beb7556740b0"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""486207ae-92ee-4464-8d24-152095f03b0a"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fd75399b-f30d-46ea-9ce3-b2338015272c"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxGamePad"",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fec0daf9-4e08-458c-b825-f58102ed6f95"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MouseDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1bbd36e8-d12d-42bb-a839-a31623f8eedf"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MouseUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""67600fe4-7c61-4103-bf50-5279b27c270c"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MouseDrag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2b59e76e-357a-47e2-b9de-0c90deb2e0f4"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""8b6bee7a-2e8c-41f0-a22c-170c19ab7748"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""ef70a0bb-5a1b-4976-98ef-7a951077cc9d"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxGamePad"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f0880d27-65bb-44cc-8504-6ca44b7d30d1"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxGamePad"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""76b809d1-6866-4778-86e2-3adcb64f34a4"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxGamePad"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e337797d-77f3-474a-a1fa-9309725a511d"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxGamePad"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": []
        },
        {
            ""name"": ""XboxGamePad"",
            ""bindingGroup"": ""XboxGamePad"",
            ""devices"": []
        }
    ]
}");
        // GamePlay
        m_GamePlay = asset.FindActionMap("GamePlay", throwIfNotFound: true);
        m_GamePlay_Move = m_GamePlay.FindAction("Move", throwIfNotFound: true);
        m_GamePlay_Ok = m_GamePlay.FindAction("Ok", throwIfNotFound: true);
        m_GamePlay_Cancel = m_GamePlay.FindAction("Cancel", throwIfNotFound: true);
        m_GamePlay_Start = m_GamePlay.FindAction("Start", throwIfNotFound: true);
        m_GamePlay_MouseDown = m_GamePlay.FindAction("MouseDown", throwIfNotFound: true);
        m_GamePlay_MouseUp = m_GamePlay.FindAction("MouseUp", throwIfNotFound: true);
        m_GamePlay_MouseDrag = m_GamePlay.FindAction("MouseDrag", throwIfNotFound: true);
        m_GamePlay_Submit = m_GamePlay.FindAction("Submit", throwIfNotFound: true);
        m_GamePlay_Select = m_GamePlay.FindAction("Select", throwIfNotFound: true);
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

    // GamePlay
    private readonly InputActionMap m_GamePlay;
    private IGamePlayActions m_GamePlayActionsCallbackInterface;
    private readonly InputAction m_GamePlay_Move;
    private readonly InputAction m_GamePlay_Ok;
    private readonly InputAction m_GamePlay_Cancel;
    private readonly InputAction m_GamePlay_Start;
    private readonly InputAction m_GamePlay_MouseDown;
    private readonly InputAction m_GamePlay_MouseUp;
    private readonly InputAction m_GamePlay_MouseDrag;
    private readonly InputAction m_GamePlay_Submit;
    private readonly InputAction m_GamePlay_Select;
    public struct GamePlayActions
    {
        private @PlayerInputActions m_Wrapper;
        public GamePlayActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_GamePlay_Move;
        public InputAction @Ok => m_Wrapper.m_GamePlay_Ok;
        public InputAction @Cancel => m_Wrapper.m_GamePlay_Cancel;
        public InputAction @Start => m_Wrapper.m_GamePlay_Start;
        public InputAction @MouseDown => m_Wrapper.m_GamePlay_MouseDown;
        public InputAction @MouseUp => m_Wrapper.m_GamePlay_MouseUp;
        public InputAction @MouseDrag => m_Wrapper.m_GamePlay_MouseDrag;
        public InputAction @Submit => m_Wrapper.m_GamePlay_Submit;
        public InputAction @Select => m_Wrapper.m_GamePlay_Select;
        public InputActionMap Get() { return m_Wrapper.m_GamePlay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GamePlayActions set) { return set.Get(); }
        public void SetCallbacks(IGamePlayActions instance)
        {
            if (m_Wrapper.m_GamePlayActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMove;
                @Ok.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnOk;
                @Ok.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnOk;
                @Ok.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnOk;
                @Cancel.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnCancel;
                @Start.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnStart;
                @Start.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnStart;
                @Start.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnStart;
                @MouseDown.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMouseDown;
                @MouseDown.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMouseDown;
                @MouseDown.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMouseDown;
                @MouseUp.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMouseUp;
                @MouseUp.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMouseUp;
                @MouseUp.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMouseUp;
                @MouseDrag.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMouseDrag;
                @MouseDrag.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMouseDrag;
                @MouseDrag.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMouseDrag;
                @Submit.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnSubmit;
                @Submit.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnSubmit;
                @Submit.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnSubmit;
                @Select.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnSelect;
            }
            m_Wrapper.m_GamePlayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Ok.started += instance.OnOk;
                @Ok.performed += instance.OnOk;
                @Ok.canceled += instance.OnOk;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @Start.started += instance.OnStart;
                @Start.performed += instance.OnStart;
                @Start.canceled += instance.OnStart;
                @MouseDown.started += instance.OnMouseDown;
                @MouseDown.performed += instance.OnMouseDown;
                @MouseDown.canceled += instance.OnMouseDown;
                @MouseUp.started += instance.OnMouseUp;
                @MouseUp.performed += instance.OnMouseUp;
                @MouseUp.canceled += instance.OnMouseUp;
                @MouseDrag.started += instance.OnMouseDrag;
                @MouseDrag.performed += instance.OnMouseDrag;
                @MouseDrag.canceled += instance.OnMouseDrag;
                @Submit.started += instance.OnSubmit;
                @Submit.performed += instance.OnSubmit;
                @Submit.canceled += instance.OnSubmit;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
            }
        }
    }
    public GamePlayActions @GamePlay => new GamePlayActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_XboxGamePadSchemeIndex = -1;
    public InputControlScheme XboxGamePadScheme
    {
        get
        {
            if (m_XboxGamePadSchemeIndex == -1) m_XboxGamePadSchemeIndex = asset.FindControlSchemeIndex("XboxGamePad");
            return asset.controlSchemes[m_XboxGamePadSchemeIndex];
        }
    }
    public interface IGamePlayActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnOk(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnStart(InputAction.CallbackContext context);
        void OnMouseDown(InputAction.CallbackContext context);
        void OnMouseUp(InputAction.CallbackContext context);
        void OnMouseDrag(InputAction.CallbackContext context);
        void OnSubmit(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
    }
}
