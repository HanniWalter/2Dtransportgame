// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerInputController.inputactions'

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
        }
    ],
    ""controlSchemes"": []
}");
        // Keyboard
        m_Keyboard = asset.FindActionMap("Keyboard", throwIfNotFound: true);
        m_Keyboard_wasd = m_Keyboard.FindAction("wasd", throwIfNotFound: true);
        m_Keyboard_numplusminus = m_Keyboard.FindAction("numplusminus", throwIfNotFound: true);
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
    public interface IKeyboardActions
    {
        void OnWasd(InputAction.CallbackContext context);
        void OnNumplusminus(InputAction.CallbackContext context);
    }
}
