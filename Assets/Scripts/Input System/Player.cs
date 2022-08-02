// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input System/Player.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Player : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Player()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player"",
    ""maps"": [
        {
            ""name"": ""Controls"",
            ""id"": ""32f96a60-0496-4cc7-9ca3-91b15b6e175f"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""f769c312-6d0c-4ed9-a3f7-3356782846e2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""5fc4601e-90de-457f-ac49-692d814128c1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Value"",
                    ""id"": ""d46b1409-a86e-4216-b8a7-28f3b4d71455"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Throw"",
                    ""type"": ""Button"",
                    ""id"": ""9993a4e2-78e2-4a04-8757-12e7a7772da6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Drop"",
                    ""type"": ""Button"",
                    ""id"": ""e3d49dad-678c-4933-a496-d6173ace5afd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Kick"",
                    ""type"": ""Button"",
                    ""id"": ""18b9cd5b-55e0-4b57-a02b-1174d8d04256"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""JoypadAim"",
                    ""type"": ""Value"",
                    ""id"": ""b7608d42-16ba-4545-be27-d65073b86dc6"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Game Menu"",
                    ""type"": ""Button"",
                    ""id"": ""6deeefa1-fc0e-4c4d-85a4-e19a80db74c6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""FasterPointer"",
                    ""type"": ""Button"",
                    ""id"": ""41810a1e-27ce-4836-a0c2-9daf938857fc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""AD"",
                    ""id"": ""989ee6c6-db55-4cdb-88b1-c7716c75a246"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""d67357a8-0590-4f2a-9952-d466cf01380b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""ab822fd7-a61a-4958-aada-4d56da597624"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ArrowKeys"",
                    ""id"": ""4d0d79e7-9ee3-4ae3-96d4-fca36c41dcfd"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""7aa3e2ff-3ea6-4ad7-96af-8a32e9028480"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""88c6a67f-f7d5-4347-9b86-3b1137709b85"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""GamePad Stick"",
                    ""id"": ""393dff59-0ab2-456c-a17b-a09c633dcdde"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone(min=0.19)"",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""0c99c8fe-b514-4789-8d3e-d21ca9ca720d"",
                    ""path"": ""<XInputController>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""c3f607f7-56f9-45ef-8e13-f8e5bf130e8d"",
                    ""path"": ""<XInputController>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""GamePad DPad"",
                    ""id"": ""472d589e-1a44-45c9-bdbe-55a83ef8761d"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""8eeb9159-0164-4dd4-90ba-fa29d10fbf1f"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""0cfe1eaf-84f3-4bd1-ab3e-143111a5e5ba"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""da3c892d-15f4-48b4-bf11-cfcddbcb922e"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""703c54b8-965e-4571-bf46-f226fc067822"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""cb6c2681-0532-4afb-8d76-737a868df5a5"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""622e9522-8282-46b1-80b2-405865d20d83"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""026ce56b-2ad1-4e6c-873f-86b1ee23a822"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a236e877-6f4b-4164-8519-2b7bafb18de2"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b710b586-1444-4067-9dae-82bb11b450e1"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""43c81b95-60da-4c6d-93fd-9bf7c864afe8"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6ac5c483-7aab-4cea-ab23-d53b3ea4522b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""927aee24-483f-40f5-ad3e-17203e73d351"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c63f282-bb63-4ca7-b792-0cf8d3f8f539"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1784947e-4eef-4482-a8fe-27d098d09cfd"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Kick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e7eda659-c973-4261-bce7-8f291cabb477"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Kick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c4a71488-f81d-436c-8a6f-486cc4c3bbd1"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Kick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""41c75e58-00e4-42fc-a0a8-e9e5735a9c3a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""JoypadAim"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""840bf0e2-022b-403c-b134-859009cbfe89"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""JoypadAim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""78680742-c29d-41cb-bc72-8299fb1d85ea"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""JoypadAim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""58794aa8-4532-4dc2-9a40-386451d4a50d"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""JoypadAim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d7d06361-392f-4e34-ac97-bad074e5cd4a"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""JoypadAim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2dea12b0-bdec-476f-88bc-3a943f085bca"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Game Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9b1945b4-a2b3-4d67-9933-f07d43220b78"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Game Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""da9ade2c-7b6b-47f5-b610-306605392a75"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""FasterPointer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard & Mouse"",
            ""bindingGroup"": ""Keyboard & Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Controls
        m_Controls = asset.FindActionMap("Controls", throwIfNotFound: true);
        m_Controls_Movement = m_Controls.FindAction("Movement", throwIfNotFound: true);
        m_Controls_Jump = m_Controls.FindAction("Jump", throwIfNotFound: true);
        m_Controls_Aim = m_Controls.FindAction("Aim", throwIfNotFound: true);
        m_Controls_Throw = m_Controls.FindAction("Throw", throwIfNotFound: true);
        m_Controls_Drop = m_Controls.FindAction("Drop", throwIfNotFound: true);
        m_Controls_Kick = m_Controls.FindAction("Kick", throwIfNotFound: true);
        m_Controls_JoypadAim = m_Controls.FindAction("JoypadAim", throwIfNotFound: true);
        m_Controls_GameMenu = m_Controls.FindAction("Game Menu", throwIfNotFound: true);
        m_Controls_FasterPointer = m_Controls.FindAction("FasterPointer", throwIfNotFound: true);
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

    // Controls
    private readonly InputActionMap m_Controls;
    private IControlsActions m_ControlsActionsCallbackInterface;
    private readonly InputAction m_Controls_Movement;
    private readonly InputAction m_Controls_Jump;
    private readonly InputAction m_Controls_Aim;
    private readonly InputAction m_Controls_Throw;
    private readonly InputAction m_Controls_Drop;
    private readonly InputAction m_Controls_Kick;
    private readonly InputAction m_Controls_JoypadAim;
    private readonly InputAction m_Controls_GameMenu;
    private readonly InputAction m_Controls_FasterPointer;
    public struct ControlsActions
    {
        private @Player m_Wrapper;
        public ControlsActions(@Player wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Controls_Movement;
        public InputAction @Jump => m_Wrapper.m_Controls_Jump;
        public InputAction @Aim => m_Wrapper.m_Controls_Aim;
        public InputAction @Throw => m_Wrapper.m_Controls_Throw;
        public InputAction @Drop => m_Wrapper.m_Controls_Drop;
        public InputAction @Kick => m_Wrapper.m_Controls_Kick;
        public InputAction @JoypadAim => m_Wrapper.m_Controls_JoypadAim;
        public InputAction @GameMenu => m_Wrapper.m_Controls_GameMenu;
        public InputAction @FasterPointer => m_Wrapper.m_Controls_FasterPointer;
        public InputActionMap Get() { return m_Wrapper.m_Controls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ControlsActions set) { return set.Get(); }
        public void SetCallbacks(IControlsActions instance)
        {
            if (m_Wrapper.m_ControlsActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMovement;
                @Jump.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnJump;
                @Aim.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnAim;
                @Throw.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnThrow;
                @Throw.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnThrow;
                @Throw.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnThrow;
                @Drop.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnDrop;
                @Drop.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnDrop;
                @Drop.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnDrop;
                @Kick.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnKick;
                @Kick.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnKick;
                @Kick.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnKick;
                @JoypadAim.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnJoypadAim;
                @JoypadAim.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnJoypadAim;
                @JoypadAim.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnJoypadAim;
                @GameMenu.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnGameMenu;
                @GameMenu.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnGameMenu;
                @GameMenu.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnGameMenu;
                @FasterPointer.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnFasterPointer;
                @FasterPointer.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnFasterPointer;
                @FasterPointer.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnFasterPointer;
            }
            m_Wrapper.m_ControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @Throw.started += instance.OnThrow;
                @Throw.performed += instance.OnThrow;
                @Throw.canceled += instance.OnThrow;
                @Drop.started += instance.OnDrop;
                @Drop.performed += instance.OnDrop;
                @Drop.canceled += instance.OnDrop;
                @Kick.started += instance.OnKick;
                @Kick.performed += instance.OnKick;
                @Kick.canceled += instance.OnKick;
                @JoypadAim.started += instance.OnJoypadAim;
                @JoypadAim.performed += instance.OnJoypadAim;
                @JoypadAim.canceled += instance.OnJoypadAim;
                @GameMenu.started += instance.OnGameMenu;
                @GameMenu.performed += instance.OnGameMenu;
                @GameMenu.canceled += instance.OnGameMenu;
                @FasterPointer.started += instance.OnFasterPointer;
                @FasterPointer.performed += instance.OnFasterPointer;
                @FasterPointer.canceled += instance.OnFasterPointer;
            }
        }
    }
    public ControlsActions @Controls => new ControlsActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard & Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    public interface IControlsActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnThrow(InputAction.CallbackContext context);
        void OnDrop(InputAction.CallbackContext context);
        void OnKick(InputAction.CallbackContext context);
        void OnJoypadAim(InputAction.CallbackContext context);
        void OnGameMenu(InputAction.CallbackContext context);
        void OnFasterPointer(InputAction.CallbackContext context);
    }
}
