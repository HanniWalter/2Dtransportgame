// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerInteraction/PlayerInputController.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputController : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputController"",
    ""maps"": [
        {
            ""name"": ""Keyboard"",
            ""id"": ""e78fb78b-e440-431b-9ae5-486da0d164fe"",
            ""actions"": [
                {
                    ""name"": ""wasd"",
                    ""type"": ""Value"",
                    ""id"": ""c7108094-a13d-4520-ad93-751b00e26373"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""numplusminus"",
                    ""type"": ""PassThrough"",
                    ""id"": ""d576eae2-1518-4b9c-af25-dab36229a498"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""821786b4-7518-4b64-90d0-2fd2573323fe"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""wasd"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7f9a97fd-366c-4aa1-9522-bca5ba79081a"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""wasd"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""08a44a39-1873-4c74-a655-b38c4b914966"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""wasd"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""feabde1f-73d7-48f7-a2e5-a2f3752ec28e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""wasd"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a3fbb039-76d9-457d-a7bb-713cc9541ecd"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""wasd"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""34302e15-d32f-4361-877e-c6fbfadc959e"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""numplusminus"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""a89c8084-f847-4062-8c77-5df8b54cfafb"",
                    ""path"": ""<Keyboard>/numpadMinus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""numplusminus"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e1bedcb0-b9c4-4596-9cb1-db6fddf89170"",
                    ""path"": ""<Keyboard>/numpadPlus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""numplusminus"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Mouse"",
            ""id"": ""742b49f2-d081-48d9-a5c3-3092b2c677e4"",
            ""actions"": [
                {
                    ""name"": ""RightButton"",
                    ""type"": ""Value"",
                    ""id"": ""cb7e1ce1-1ec3-4960-9d7d-2964b9a660b1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftButton"",
                    ""type"": ""Value"",
                    ""id"": ""f65ed01e-d818-4b49-a5ab-d226d171f8a9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Position"",
                    ""type"": ""Value"",
                    ""id"": ""848326be-75ac-44f1-982f-b18aa2305f77"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Delta"",
                    ""type"": ""Value"",
                    ""id"": ""738ae0fb-ab7f-41fa-b167-2a6c2ed7cbfc"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""def159ea-5fb2-459e-bea9-4a35515bac45"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f26c48bb-c8f4-47cb-b91c-aebf075244c2"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d2778991-e6d4-42b6-a2e9-a01252b24781"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6821bfe7-7757-4f60-a735-972d17353766"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Delta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Keyboard
        m_Keyboard = asset.FindActionMap("Keyboard", throwIfNotFound: true);
        m_Keyboard_wasd = m_Keyboard.FindAction("wasd", throwIfNotFound: true);
        m_Keyboard_numplusminus = m_Keyboard.FindAction("numplusminus", throwIfNotFound: true);
        // Mouse
        m_Mouse = asset.FindActionMap("Mouse", throwIfNotFound: true);
        m_Mouse_RightButton = m_Mouse.FindAction("RightButton", throwIfNotFound: true);
        m_Mouse_LeftButton = m_Mouse.FindAction("LeftButton", throwIfNotFound: true);
        m_Mouse_Position = m_Mouse.FindAction("Position", throwIfNotFound: true);
        m_Mouse_Delta = m_Mouse.FindAction("Delta", throwIfNotFound: true);
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

    // Keyboard
    private readonly InputActionMap m_Keyboard;
    private IKeyboardActions m_KeyboardActionsCallbackInterface;
    private readonly InputAction m_Keyboard_wasd;
    private readonly InputAction m_Keyboard_numplusminus;
    public struct KeyboardActions
    {
        private @PlayerInputController m_Wrapper;
        public KeyboardActions(@PlayerInputController wrapper) { m_Wrapper = wrapper; }
        public InputAction @wasd => m_Wrapper.m_Keyboard_wasd;
        public InputAction @numplusminus => m_Wrapper.m_Keyboard_numplusminus;
        public InputActionMap Get() { return m_Wrapper.m_Keyboard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardActions set) { return set.Get(); }
        public void SetCallbacks(IKeyboardActions instance)
        {
            if (m_Wrapper.m_KeyboardActionsCallbackInterface != null)
            {
                @wasd.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnWasd;
                @wasd.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnWasd;
                @wasd.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnWasd;
                @numplusminus.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnNumplusminus;
                @numplusminus.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnNumplusminus;
                @numplusminus.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnNumplusminus;
            }
            m_Wrapper.m_KeyboardActionsCallbackInterface = instance;
            if (instance != null)
            {
                @wasd.started += instance.OnWasd;
                @wasd.performed += instance.OnWasd;
                @wasd.canceled += instance.OnWasd;
                @numplusminus.started += instance.OnNumplusminus;
                @numplusminus.performed += instance.OnNumplusminus;
                @numplusminus.canceled += instance.OnNumplusminus;
            }
        }
    }
    public KeyboardActions @Keyboard => new KeyboardActions(this);

    // Mouse
    private readonly InputActionMap m_Mouse;
    private IMouseActions m_MouseActionsCallbackInterface;
    private readonly InputAction m_Mouse_RightButton;
    private readonly InputAction m_Mouse_LeftButton;
    private readonly InputAction m_Mouse_Position;
    private readonly InputAction m_Mouse_Delta;
    public struct MouseActions
    {
        private @PlayerInputController m_Wrapper;
        public MouseActions(@PlayerInputController wrapper) { m_Wrapper = wrapper; }
        public InputAction @RightButton => m_Wrapper.m_Mouse_RightButton;
        public InputAction @LeftButton => m_Wrapper.m_Mouse_LeftButton;
        public InputAction @Position => m_Wrapper.m_Mouse_Position;
        public InputAction @Delta => m_Wrapper.m_Mouse_Delta;
        public InputActionMap Get() { return m_Wrapper.m_Mouse; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MouseActions set) { return set.Get(); }
        public void SetCallbacks(IMouseActions instance)
        {
            if (m_Wrapper.m_MouseActionsCallbackInterface != null)
            {
                @RightButton.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnRightButton;
                @RightButton.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnRightButton;
                @RightButton.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnRightButton;
                @LeftButton.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnLeftButton;
                @LeftButton.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnLeftButton;
                @LeftButton.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnLeftButton;
                @Position.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnPosition;
                @Position.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnPosition;
                @Position.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnPosition;
                @Delta.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnDelta;
                @Delta.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnDelta;
                @Delta.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnDelta;
            }
            m_Wrapper.m_MouseActionsCallbackInterface = instance;
            if (instance != null)
            {
                @RightButton.started += instance.OnRightButton;
                @RightButton.performed += instance.OnRightButton;
                @RightButton.canceled += instance.OnRightButton;
                @LeftButton.started += instance.OnLeftButton;
                @LeftButton.performed += instance.OnLeftButton;
                @LeftButton.canceled += instance.OnLeftButton;
                @Position.started += instance.OnPosition;
                @Position.performed += instance.OnPosition;
                @Position.canceled += instance.OnPosition;
                @Delta.started += instance.OnDelta;
                @Delta.performed += instance.OnDelta;
                @Delta.canceled += instance.OnDelta;
            }
        }
    }
    public MouseActions @Mouse => new MouseActions(this);
    public interface IKeyboardActions
    {
        void OnWasd(InputAction.CallbackContext context);
        void OnNumplusminus(InputAction.CallbackContext context);
    }
    public interface IMouseActions
    {
        void OnRightButton(InputAction.CallbackContext context);
        void OnLeftButton(InputAction.CallbackContext context);
        void OnPosition(InputAction.CallbackContext context);
        void OnDelta(InputAction.CallbackContext context);
    }
}
