// GENERATED AUTOMATICALLY FROM 'Assets/Input/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Map1"",
            ""id"": ""a948f5ab-a104-49bc-b6c3-2f7e06231f78"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""06edc7d7-e643-426b-bfa2-945014ce35ae"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""React"",
                    ""type"": ""Button"",
                    ""id"": ""74b98fb5-dcb8-4356-af60-7ae8eacc5051"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""c14a8b79-bf5f-4208-93b3-009f11db8abb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AssetKey"",
                    ""type"": ""Button"",
                    ""id"": ""663b6af9-8ed5-4ae1-835c-8a0f825c9d6d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""90b205e9-4ff4-4abe-b114-b7ab5819c8f7"",
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
                    ""id"": ""de113006-740a-4f0b-a370-82ad0abb96af"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7484dd71-3677-4ad0-804a-30443e64ca68"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""70a45f9a-fff1-4e75-af61-386a0e768cbd"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3d5e21d3-5f2e-44d2-8778-ec7d8161bc9d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ac70d55b-a115-479e-be62-cac91161edd7"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""React"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ed7fc75e-2e25-4926-9cb0-87079faa7576"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6f669bef-9a88-4133-9709-232b0ff68f0f"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c321ad0-6b3b-4595-b7a8-302bf312cfbf"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""50647aa5-f336-4178-9d4c-a4d6e7c5f069"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""AssetKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyboardAndMouse"",
            ""bindingGroup"": ""KeyboardAndMouse"",
            ""devices"": []
        }
    ]
}");
        // Map1
        m_Map1 = asset.FindActionMap("Map1", throwIfNotFound: true);
        m_Map1_Move = m_Map1.FindAction("Move", throwIfNotFound: true);
        m_Map1_React = m_Map1.FindAction("React", throwIfNotFound: true);
        m_Map1_Cancel = m_Map1.FindAction("Cancel", throwIfNotFound: true);
        m_Map1_AssetKey = m_Map1.FindAction("AssetKey", throwIfNotFound: true);
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

    // Map1
    private readonly InputActionMap m_Map1;
    private IMap1Actions m_Map1ActionsCallbackInterface;
    private readonly InputAction m_Map1_Move;
    private readonly InputAction m_Map1_React;
    private readonly InputAction m_Map1_Cancel;
    private readonly InputAction m_Map1_AssetKey;
    public struct Map1Actions
    {
        private @InputMaster m_Wrapper;
        public Map1Actions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Map1_Move;
        public InputAction @React => m_Wrapper.m_Map1_React;
        public InputAction @Cancel => m_Wrapper.m_Map1_Cancel;
        public InputAction @AssetKey => m_Wrapper.m_Map1_AssetKey;
        public InputActionMap Get() { return m_Wrapper.m_Map1; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Map1Actions set) { return set.Get(); }
        public void SetCallbacks(IMap1Actions instance)
        {
            if (m_Wrapper.m_Map1ActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_Map1ActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_Map1ActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_Map1ActionsCallbackInterface.OnMove;
                @React.started -= m_Wrapper.m_Map1ActionsCallbackInterface.OnReact;
                @React.performed -= m_Wrapper.m_Map1ActionsCallbackInterface.OnReact;
                @React.canceled -= m_Wrapper.m_Map1ActionsCallbackInterface.OnReact;
                @Cancel.started -= m_Wrapper.m_Map1ActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_Map1ActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_Map1ActionsCallbackInterface.OnCancel;
                @AssetKey.started -= m_Wrapper.m_Map1ActionsCallbackInterface.OnAssetKey;
                @AssetKey.performed -= m_Wrapper.m_Map1ActionsCallbackInterface.OnAssetKey;
                @AssetKey.canceled -= m_Wrapper.m_Map1ActionsCallbackInterface.OnAssetKey;
            }
            m_Wrapper.m_Map1ActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @React.started += instance.OnReact;
                @React.performed += instance.OnReact;
                @React.canceled += instance.OnReact;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @AssetKey.started += instance.OnAssetKey;
                @AssetKey.performed += instance.OnAssetKey;
                @AssetKey.canceled += instance.OnAssetKey;
            }
        }
    }
    public Map1Actions @Map1 => new Map1Actions(this);
    private int m_KeyboardAndMouseSchemeIndex = -1;
    public InputControlScheme KeyboardAndMouseScheme
    {
        get
        {
            if (m_KeyboardAndMouseSchemeIndex == -1) m_KeyboardAndMouseSchemeIndex = asset.FindControlSchemeIndex("KeyboardAndMouse");
            return asset.controlSchemes[m_KeyboardAndMouseSchemeIndex];
        }
    }
    public interface IMap1Actions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnReact(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnAssetKey(InputAction.CallbackContext context);
    }
}
