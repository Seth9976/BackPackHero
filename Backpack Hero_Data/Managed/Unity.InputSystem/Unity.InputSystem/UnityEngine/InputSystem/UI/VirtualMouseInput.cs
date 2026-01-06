using System;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;

namespace UnityEngine.InputSystem.UI
{
	// Token: 0x0200008C RID: 140
	[AddComponentMenu("Input/Virtual Mouse")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.6/manual/UISupport.html#virtual-mouse-cursor-control")]
	public class VirtualMouseInput : MonoBehaviour
	{
		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x0003C0B1 File Offset: 0x0003A2B1
		// (set) Token: 0x06000B49 RID: 2889 RVA: 0x0003C0B9 File Offset: 0x0003A2B9
		public RectTransform cursorTransform
		{
			get
			{
				return this.m_CursorTransform;
			}
			set
			{
				this.m_CursorTransform = value;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x0003C0C2 File Offset: 0x0003A2C2
		// (set) Token: 0x06000B4B RID: 2891 RVA: 0x0003C0CA File Offset: 0x0003A2CA
		public float cursorSpeed
		{
			get
			{
				return this.m_CursorSpeed;
			}
			set
			{
				this.m_CursorSpeed = value;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x0003C0D3 File Offset: 0x0003A2D3
		// (set) Token: 0x06000B4D RID: 2893 RVA: 0x0003C0DC File Offset: 0x0003A2DC
		public VirtualMouseInput.CursorMode cursorMode
		{
			get
			{
				return this.m_CursorMode;
			}
			set
			{
				if (this.m_CursorMode == value)
				{
					return;
				}
				if (this.m_CursorMode == VirtualMouseInput.CursorMode.HardwareCursorIfAvailable && this.m_SystemMouse != null)
				{
					InputSystem.EnableDevice(this.m_SystemMouse);
					this.m_SystemMouse = null;
				}
				this.m_CursorMode = value;
				if (this.m_CursorMode == VirtualMouseInput.CursorMode.HardwareCursorIfAvailable)
				{
					this.TryEnableHardwareCursor();
					return;
				}
				if (this.m_CursorGraphic != null)
				{
					this.m_CursorGraphic.enabled = true;
				}
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x0003C147 File Offset: 0x0003A347
		// (set) Token: 0x06000B4F RID: 2895 RVA: 0x0003C14F File Offset: 0x0003A34F
		public Graphic cursorGraphic
		{
			get
			{
				return this.m_CursorGraphic;
			}
			set
			{
				this.m_CursorGraphic = value;
				this.TryFindCanvas();
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x0003C15E File Offset: 0x0003A35E
		// (set) Token: 0x06000B51 RID: 2897 RVA: 0x0003C166 File Offset: 0x0003A366
		public float scrollSpeed
		{
			get
			{
				return this.m_ScrollSpeed;
			}
			set
			{
				this.m_ScrollSpeed = value;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x0003C16F File Offset: 0x0003A36F
		public Mouse virtualMouse
		{
			get
			{
				return this.m_VirtualMouse;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x0003C177 File Offset: 0x0003A377
		// (set) Token: 0x06000B54 RID: 2900 RVA: 0x0003C17F File Offset: 0x0003A37F
		public InputActionProperty stickAction
		{
			get
			{
				return this.m_StickAction;
			}
			set
			{
				VirtualMouseInput.SetAction(ref this.m_StickAction, value);
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x0003C18D File Offset: 0x0003A38D
		// (set) Token: 0x06000B56 RID: 2902 RVA: 0x0003C198 File Offset: 0x0003A398
		public InputActionProperty leftButtonAction
		{
			get
			{
				return this.m_LeftButtonAction;
			}
			set
			{
				if (this.m_ButtonActionTriggeredDelegate != null)
				{
					VirtualMouseInput.SetActionCallback(this.m_LeftButtonAction, this.m_ButtonActionTriggeredDelegate, false);
				}
				VirtualMouseInput.SetAction(ref this.m_LeftButtonAction, value);
				if (this.m_ButtonActionTriggeredDelegate != null)
				{
					VirtualMouseInput.SetActionCallback(this.m_LeftButtonAction, this.m_ButtonActionTriggeredDelegate, true);
				}
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x0003C1E5 File Offset: 0x0003A3E5
		// (set) Token: 0x06000B58 RID: 2904 RVA: 0x0003C1F0 File Offset: 0x0003A3F0
		public InputActionProperty rightButtonAction
		{
			get
			{
				return this.m_RightButtonAction;
			}
			set
			{
				if (this.m_ButtonActionTriggeredDelegate != null)
				{
					VirtualMouseInput.SetActionCallback(this.m_RightButtonAction, this.m_ButtonActionTriggeredDelegate, false);
				}
				VirtualMouseInput.SetAction(ref this.m_RightButtonAction, value);
				if (this.m_ButtonActionTriggeredDelegate != null)
				{
					VirtualMouseInput.SetActionCallback(this.m_RightButtonAction, this.m_ButtonActionTriggeredDelegate, true);
				}
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x0003C23D File Offset: 0x0003A43D
		// (set) Token: 0x06000B5A RID: 2906 RVA: 0x0003C248 File Offset: 0x0003A448
		public InputActionProperty middleButtonAction
		{
			get
			{
				return this.m_MiddleButtonAction;
			}
			set
			{
				if (this.m_ButtonActionTriggeredDelegate != null)
				{
					VirtualMouseInput.SetActionCallback(this.m_MiddleButtonAction, this.m_ButtonActionTriggeredDelegate, false);
				}
				VirtualMouseInput.SetAction(ref this.m_MiddleButtonAction, value);
				if (this.m_ButtonActionTriggeredDelegate != null)
				{
					VirtualMouseInput.SetActionCallback(this.m_MiddleButtonAction, this.m_ButtonActionTriggeredDelegate, true);
				}
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x0003C295 File Offset: 0x0003A495
		// (set) Token: 0x06000B5C RID: 2908 RVA: 0x0003C2A0 File Offset: 0x0003A4A0
		public InputActionProperty forwardButtonAction
		{
			get
			{
				return this.m_ForwardButtonAction;
			}
			set
			{
				if (this.m_ButtonActionTriggeredDelegate != null)
				{
					VirtualMouseInput.SetActionCallback(this.m_ForwardButtonAction, this.m_ButtonActionTriggeredDelegate, false);
				}
				VirtualMouseInput.SetAction(ref this.m_ForwardButtonAction, value);
				if (this.m_ButtonActionTriggeredDelegate != null)
				{
					VirtualMouseInput.SetActionCallback(this.m_ForwardButtonAction, this.m_ButtonActionTriggeredDelegate, true);
				}
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x0003C2ED File Offset: 0x0003A4ED
		// (set) Token: 0x06000B5E RID: 2910 RVA: 0x0003C2F8 File Offset: 0x0003A4F8
		public InputActionProperty backButtonAction
		{
			get
			{
				return this.m_BackButtonAction;
			}
			set
			{
				if (this.m_ButtonActionTriggeredDelegate != null)
				{
					VirtualMouseInput.SetActionCallback(this.m_BackButtonAction, this.m_ButtonActionTriggeredDelegate, false);
				}
				VirtualMouseInput.SetAction(ref this.m_BackButtonAction, value);
				if (this.m_ButtonActionTriggeredDelegate != null)
				{
					VirtualMouseInput.SetActionCallback(this.m_BackButtonAction, this.m_ButtonActionTriggeredDelegate, true);
				}
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x0003C345 File Offset: 0x0003A545
		// (set) Token: 0x06000B60 RID: 2912 RVA: 0x0003C34D File Offset: 0x0003A54D
		public InputActionProperty scrollWheelAction
		{
			get
			{
				return this.m_ScrollWheelAction;
			}
			set
			{
				VirtualMouseInput.SetAction(ref this.m_ScrollWheelAction, value);
			}
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0003C35C File Offset: 0x0003A55C
		protected void OnEnable()
		{
			if (this.m_CursorMode == VirtualMouseInput.CursorMode.HardwareCursorIfAvailable)
			{
				this.TryEnableHardwareCursor();
			}
			if (this.m_VirtualMouse == null)
			{
				this.m_VirtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse", null, null);
			}
			else if (!this.m_VirtualMouse.added)
			{
				InputSystem.AddDevice(this.m_VirtualMouse);
			}
			if (this.m_CursorTransform != null)
			{
				Vector2 anchoredPosition = this.m_CursorTransform.anchoredPosition;
				InputState.Change<Vector2>(this.m_VirtualMouse.position, anchoredPosition, InputUpdateType.None, default(InputEventPtr));
				Mouse systemMouse = this.m_SystemMouse;
				if (systemMouse != null)
				{
					systemMouse.WarpCursorPosition(anchoredPosition);
				}
			}
			if (this.m_AfterInputUpdateDelegate == null)
			{
				this.m_AfterInputUpdateDelegate = new Action(this.OnAfterInputUpdate);
			}
			InputSystem.onAfterUpdate += this.m_AfterInputUpdateDelegate;
			if (this.m_ButtonActionTriggeredDelegate == null)
			{
				this.m_ButtonActionTriggeredDelegate = new Action<InputAction.CallbackContext>(this.OnButtonActionTriggered);
			}
			VirtualMouseInput.SetActionCallback(this.m_LeftButtonAction, this.m_ButtonActionTriggeredDelegate, true);
			VirtualMouseInput.SetActionCallback(this.m_RightButtonAction, this.m_ButtonActionTriggeredDelegate, true);
			VirtualMouseInput.SetActionCallback(this.m_MiddleButtonAction, this.m_ButtonActionTriggeredDelegate, true);
			VirtualMouseInput.SetActionCallback(this.m_ForwardButtonAction, this.m_ButtonActionTriggeredDelegate, true);
			VirtualMouseInput.SetActionCallback(this.m_BackButtonAction, this.m_ButtonActionTriggeredDelegate, true);
			InputAction action = this.m_StickAction.action;
			if (action != null)
			{
				action.Enable();
			}
			InputAction action2 = this.m_LeftButtonAction.action;
			if (action2 != null)
			{
				action2.Enable();
			}
			InputAction action3 = this.m_RightButtonAction.action;
			if (action3 != null)
			{
				action3.Enable();
			}
			InputAction action4 = this.m_MiddleButtonAction.action;
			if (action4 != null)
			{
				action4.Enable();
			}
			InputAction action5 = this.m_ForwardButtonAction.action;
			if (action5 != null)
			{
				action5.Enable();
			}
			InputAction action6 = this.m_BackButtonAction.action;
			if (action6 != null)
			{
				action6.Enable();
			}
			InputAction action7 = this.m_ScrollWheelAction.action;
			if (action7 == null)
			{
				return;
			}
			action7.Enable();
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0003C52C File Offset: 0x0003A72C
		protected void OnDisable()
		{
			if (this.m_VirtualMouse != null && this.m_VirtualMouse.added)
			{
				InputSystem.RemoveDevice(this.m_VirtualMouse);
			}
			if (this.m_SystemMouse != null)
			{
				InputSystem.EnableDevice(this.m_SystemMouse);
				this.m_SystemMouse = null;
			}
			if (this.m_AfterInputUpdateDelegate != null)
			{
				InputSystem.onAfterUpdate -= this.m_AfterInputUpdateDelegate;
			}
			InputAction action = this.m_StickAction.action;
			if (action != null)
			{
				action.Disable();
			}
			InputAction action2 = this.m_LeftButtonAction.action;
			if (action2 != null)
			{
				action2.Disable();
			}
			InputAction action3 = this.m_RightButtonAction.action;
			if (action3 != null)
			{
				action3.Disable();
			}
			InputAction action4 = this.m_MiddleButtonAction.action;
			if (action4 != null)
			{
				action4.Disable();
			}
			InputAction action5 = this.m_ForwardButtonAction.action;
			if (action5 != null)
			{
				action5.Disable();
			}
			InputAction action6 = this.m_BackButtonAction.action;
			if (action6 != null)
			{
				action6.Disable();
			}
			InputAction action7 = this.m_ScrollWheelAction.action;
			if (action7 != null)
			{
				action7.Disable();
			}
			if (this.m_ButtonActionTriggeredDelegate != null)
			{
				VirtualMouseInput.SetActionCallback(this.m_LeftButtonAction, this.m_ButtonActionTriggeredDelegate, false);
				VirtualMouseInput.SetActionCallback(this.m_RightButtonAction, this.m_ButtonActionTriggeredDelegate, false);
				VirtualMouseInput.SetActionCallback(this.m_MiddleButtonAction, this.m_ButtonActionTriggeredDelegate, false);
				VirtualMouseInput.SetActionCallback(this.m_ForwardButtonAction, this.m_ButtonActionTriggeredDelegate, false);
				VirtualMouseInput.SetActionCallback(this.m_BackButtonAction, this.m_ButtonActionTriggeredDelegate, false);
			}
			this.m_LastTime = 0.0;
			this.m_LastStickValue = default(Vector2);
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0003C69D File Offset: 0x0003A89D
		private void TryFindCanvas()
		{
			Graphic cursorGraphic = this.m_CursorGraphic;
			this.m_Canvas = ((cursorGraphic != null) ? cursorGraphic.GetComponentInParent<Canvas>() : null);
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0003C6B8 File Offset: 0x0003A8B8
		private unsafe void TryEnableHardwareCursor()
		{
			ReadOnlyArray<InputDevice> devices = InputSystem.devices;
			for (int i = 0; i < devices.Count; i++)
			{
				InputDevice inputDevice = devices[i];
				if (inputDevice.native)
				{
					Mouse mouse = inputDevice as Mouse;
					if (mouse != null)
					{
						this.m_SystemMouse = mouse;
						break;
					}
				}
			}
			if (this.m_SystemMouse == null)
			{
				if (this.m_CursorGraphic != null)
				{
					this.m_CursorGraphic.enabled = true;
				}
				return;
			}
			InputSystem.DisableDevice(this.m_SystemMouse, false);
			if (this.m_VirtualMouse != null)
			{
				this.m_SystemMouse.WarpCursorPosition(*this.m_VirtualMouse.position.value);
			}
			if (this.m_CursorGraphic != null)
			{
				this.m_CursorGraphic.enabled = false;
			}
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0003C774 File Offset: 0x0003A974
		private unsafe void UpdateMotion()
		{
			if (this.m_VirtualMouse == null)
			{
				return;
			}
			InputAction action = this.m_StickAction.action;
			if (action == null)
			{
				return;
			}
			Vector2 vector = action.ReadValue<Vector2>();
			if (Mathf.Approximately(0f, vector.x) && Mathf.Approximately(0f, vector.y))
			{
				this.m_LastTime = 0.0;
				this.m_LastStickValue = default(Vector2);
			}
			else
			{
				double currentTime = InputState.currentTime;
				if (Mathf.Approximately(0f, this.m_LastStickValue.x) && Mathf.Approximately(0f, this.m_LastStickValue.y))
				{
					this.m_LastTime = currentTime;
				}
				float num = (float)(currentTime - this.m_LastTime);
				Vector2 vector2 = new Vector2(this.m_CursorSpeed * vector.x * num, this.m_CursorSpeed * vector.y * num);
				Vector2 vector3 = *this.m_VirtualMouse.position.value + vector2;
				if (this.m_Canvas != null)
				{
					Rect pixelRect = this.m_Canvas.pixelRect;
					vector3.x = Mathf.Clamp(vector3.x, pixelRect.xMin, pixelRect.xMax);
					vector3.y = Mathf.Clamp(vector3.y, pixelRect.yMin, pixelRect.yMax);
				}
				InputState.Change<Vector2>(this.m_VirtualMouse.position, vector3, InputUpdateType.None, default(InputEventPtr));
				InputState.Change<Vector2>(this.m_VirtualMouse.delta, vector2, InputUpdateType.None, default(InputEventPtr));
				if (this.m_CursorTransform != null && (this.m_CursorMode == VirtualMouseInput.CursorMode.SoftwareCursor || (this.m_CursorMode == VirtualMouseInput.CursorMode.HardwareCursorIfAvailable && this.m_SystemMouse == null)))
				{
					this.m_CursorTransform.anchoredPosition = vector3;
				}
				this.m_LastStickValue = vector;
				this.m_LastTime = currentTime;
				Mouse systemMouse = this.m_SystemMouse;
				if (systemMouse != null)
				{
					systemMouse.WarpCursorPosition(vector3);
				}
			}
			InputAction action2 = this.m_ScrollWheelAction.action;
			if (action2 != null)
			{
				Vector2 vector4 = action2.ReadValue<Vector2>();
				vector4.x *= this.m_ScrollSpeed;
				vector4.y *= this.m_ScrollSpeed;
				InputState.Change<Vector2>(this.m_VirtualMouse.scroll, vector4, InputUpdateType.None, default(InputEventPtr));
			}
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0003C9B4 File Offset: 0x0003ABB4
		private void OnButtonActionTriggered(InputAction.CallbackContext context)
		{
			if (this.m_VirtualMouse == null)
			{
				return;
			}
			InputAction action = context.action;
			MouseButton? mouseButton = null;
			if (action == this.m_LeftButtonAction.action)
			{
				mouseButton = new MouseButton?(MouseButton.Left);
			}
			else if (action == this.m_RightButtonAction.action)
			{
				mouseButton = new MouseButton?(MouseButton.Right);
			}
			else if (action == this.m_MiddleButtonAction.action)
			{
				mouseButton = new MouseButton?(MouseButton.Middle);
			}
			else if (action == this.m_ForwardButtonAction.action)
			{
				mouseButton = new MouseButton?(MouseButton.Forward);
			}
			else if (action == this.m_BackButtonAction.action)
			{
				mouseButton = new MouseButton?(MouseButton.Back);
			}
			if (mouseButton != null)
			{
				bool flag = context.control.IsPressed(0f);
				MouseState mouseState;
				this.m_VirtualMouse.CopyState(out mouseState);
				mouseState.WithButton(mouseButton.Value, flag);
				InputState.Change<MouseState>(this.m_VirtualMouse, mouseState, InputUpdateType.None, default(InputEventPtr));
			}
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0003CAA0 File Offset: 0x0003ACA0
		private static void SetActionCallback(InputActionProperty field, Action<InputAction.CallbackContext> callback, bool install = true)
		{
			InputAction action = field.action;
			if (action == null)
			{
				return;
			}
			if (install)
			{
				action.started += callback;
				action.canceled += callback;
				return;
			}
			action.started -= callback;
			action.canceled -= callback;
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0003CADC File Offset: 0x0003ACDC
		private static void SetAction(ref InputActionProperty field, InputActionProperty value)
		{
			InputActionProperty inputActionProperty = field;
			field = value;
			if (inputActionProperty.reference == null)
			{
				InputAction action = inputActionProperty.action;
				if (action != null && action.enabled)
				{
					action.Disable();
					if (value.reference == null)
					{
						InputAction action2 = value.action;
						if (action2 == null)
						{
							return;
						}
						action2.Enable();
					}
				}
			}
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0003CB3F File Offset: 0x0003AD3F
		private void OnAfterInputUpdate()
		{
			this.UpdateMotion();
		}

		// Token: 0x040003FA RID: 1018
		[Header("Cursor")]
		[Tooltip("Whether the component should set the cursor position of the hardware mouse cursor, if one is available. If so, the software cursor pointed (to by 'Cursor Graphic') will be hidden.")]
		[SerializeField]
		private VirtualMouseInput.CursorMode m_CursorMode;

		// Token: 0x040003FB RID: 1019
		[Tooltip("The graphic that represents the software cursor. This is hidden if a hardware cursor (see 'Cursor Mode') is used.")]
		[SerializeField]
		private Graphic m_CursorGraphic;

		// Token: 0x040003FC RID: 1020
		[Tooltip("The transform for the software cursor. Will only be set if a software cursor is used (see 'Cursor Mode'). Moving the cursor updates the anchored position of the transform.")]
		[SerializeField]
		private RectTransform m_CursorTransform;

		// Token: 0x040003FD RID: 1021
		[Header("Motion")]
		[Tooltip("Speed in pixels per second with which to move the cursor. Scaled by the input from 'Stick Action'.")]
		[SerializeField]
		private float m_CursorSpeed = 400f;

		// Token: 0x040003FE RID: 1022
		[Tooltip("Scale factor to apply to 'Scroll Wheel Action' when setting the mouse 'scrollWheel' control.")]
		[SerializeField]
		private float m_ScrollSpeed = 45f;

		// Token: 0x040003FF RID: 1023
		[Space(10f)]
		[Tooltip("Vector2 action that moves the cursor left/right (X) and up/down (Y) on screen.")]
		[SerializeField]
		private InputActionProperty m_StickAction;

		// Token: 0x04000400 RID: 1024
		[Tooltip("Button action that triggers a left-click on the mouse.")]
		[SerializeField]
		private InputActionProperty m_LeftButtonAction;

		// Token: 0x04000401 RID: 1025
		[Tooltip("Button action that triggers a middle-click on the mouse.")]
		[SerializeField]
		private InputActionProperty m_MiddleButtonAction;

		// Token: 0x04000402 RID: 1026
		[Tooltip("Button action that triggers a right-click on the mouse.")]
		[SerializeField]
		private InputActionProperty m_RightButtonAction;

		// Token: 0x04000403 RID: 1027
		[Tooltip("Button action that triggers a forward button (button #4) click on the mouse.")]
		[SerializeField]
		private InputActionProperty m_ForwardButtonAction;

		// Token: 0x04000404 RID: 1028
		[Tooltip("Button action that triggers a back button (button #5) click on the mouse.")]
		[SerializeField]
		private InputActionProperty m_BackButtonAction;

		// Token: 0x04000405 RID: 1029
		[Tooltip("Vector2 action that feeds into the mouse 'scrollWheel' action (scaled by 'Scroll Speed').")]
		[SerializeField]
		private InputActionProperty m_ScrollWheelAction;

		// Token: 0x04000406 RID: 1030
		private Canvas m_Canvas;

		// Token: 0x04000407 RID: 1031
		private Mouse m_VirtualMouse;

		// Token: 0x04000408 RID: 1032
		private Mouse m_SystemMouse;

		// Token: 0x04000409 RID: 1033
		private Action m_AfterInputUpdateDelegate;

		// Token: 0x0400040A RID: 1034
		private Action<InputAction.CallbackContext> m_ButtonActionTriggeredDelegate;

		// Token: 0x0400040B RID: 1035
		private double m_LastTime;

		// Token: 0x0400040C RID: 1036
		private Vector2 m_LastStickValue;

		// Token: 0x020001CE RID: 462
		public enum CursorMode
		{
			// Token: 0x0400095D RID: 2397
			SoftwareCursor,
			// Token: 0x0400095E RID: 2398
			HardwareCursorIfAvailable
		}
	}
}
