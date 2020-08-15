// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/InputMaster.inputactions'

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
            ""name"": ""MandelbrotExplorer"",
            ""id"": ""bffea7db-04ec-4c17-8f2d-fe11777d6404"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""12c571ae-ef94-4bd0-858f-ed3a31c92a95"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotation"",
                    ""type"": ""Value"",
                    ""id"": ""045dacfe-b2df-4d49-b87f-8aa566f4cd7e"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""55822c44-72d2-4f0f-9a37-b5ef9bba7c4c"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""278574e6-f792-4419-baa4-bdf09dd916aa"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""31ab690e-d69b-4f7d-aaf4-ea64436d6287"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8449e5ea-4efe-4e49-b475-e3ef7b3a9714"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""af12a3af-4109-4545-8e26-f08478f51b90"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b8b96fe7-ca1f-4173-bcdb-bb05f15ded6c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Direction"",
                    ""id"": ""d02758cb-de69-4ce1-b01c-a2418eccdda4"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""75f67404-96a0-4948-bf05-ec1fb55bb287"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""8a62136a-8835-4f2b-9943-505c01cdd07d"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""InOrOut"",
                    ""id"": ""d597fc5f-8cf4-42c0-b603-4fbd50b40dec"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""8396d435-f8ee-4d1a-b578-60547d26f9a5"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""2f16d007-3d9d-482e-b97e-e4f9163eb5b5"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // MandelbrotExplorer
        m_MandelbrotExplorer = asset.FindActionMap("MandelbrotExplorer", throwIfNotFound: true);
        m_MandelbrotExplorer_Movement = m_MandelbrotExplorer.FindAction("Movement", throwIfNotFound: true);
        m_MandelbrotExplorer_Rotation = m_MandelbrotExplorer.FindAction("Rotation", throwIfNotFound: true);
        m_MandelbrotExplorer_Zoom = m_MandelbrotExplorer.FindAction("Zoom", throwIfNotFound: true);
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

    // MandelbrotExplorer
    private readonly InputActionMap m_MandelbrotExplorer;
    private IMandelbrotExplorerActions m_MandelbrotExplorerActionsCallbackInterface;
    private readonly InputAction m_MandelbrotExplorer_Movement;
    private readonly InputAction m_MandelbrotExplorer_Rotation;
    private readonly InputAction m_MandelbrotExplorer_Zoom;
    public struct MandelbrotExplorerActions
    {
        private @InputMaster m_Wrapper;
        public MandelbrotExplorerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_MandelbrotExplorer_Movement;
        public InputAction @Rotation => m_Wrapper.m_MandelbrotExplorer_Rotation;
        public InputAction @Zoom => m_Wrapper.m_MandelbrotExplorer_Zoom;
        public InputActionMap Get() { return m_Wrapper.m_MandelbrotExplorer; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MandelbrotExplorerActions set) { return set.Get(); }
        public void SetCallbacks(IMandelbrotExplorerActions instance)
        {
            if (m_Wrapper.m_MandelbrotExplorerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_MandelbrotExplorerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_MandelbrotExplorerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_MandelbrotExplorerActionsCallbackInterface.OnMovement;
                @Rotation.started -= m_Wrapper.m_MandelbrotExplorerActionsCallbackInterface.OnRotation;
                @Rotation.performed -= m_Wrapper.m_MandelbrotExplorerActionsCallbackInterface.OnRotation;
                @Rotation.canceled -= m_Wrapper.m_MandelbrotExplorerActionsCallbackInterface.OnRotation;
                @Zoom.started -= m_Wrapper.m_MandelbrotExplorerActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_MandelbrotExplorerActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_MandelbrotExplorerActionsCallbackInterface.OnZoom;
            }
            m_Wrapper.m_MandelbrotExplorerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Rotation.started += instance.OnRotation;
                @Rotation.performed += instance.OnRotation;
                @Rotation.canceled += instance.OnRotation;
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
            }
        }
    }
    public MandelbrotExplorerActions @MandelbrotExplorer => new MandelbrotExplorerActions(this);
    public interface IMandelbrotExplorerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnRotation(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
    }
}
