using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.EnhancedTouch
{
	// Token: 0x0200009A RID: 154
	[AddComponentMenu("Input/Debug/Touch Simulation")]
	[ExecuteInEditMode]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.5/manual/Touch.html#touch-simulation")]
	public class TouchSimulation : MonoBehaviour, IInputStateChangeMonitor
	{
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x0003FB77 File Offset: 0x0003DD77
		// (set) Token: 0x06000BFE RID: 3070 RVA: 0x0003FB7F File Offset: 0x0003DD7F
		public Touchscreen simulatedTouchscreen { get; private set; }

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x0003FB88 File Offset: 0x0003DD88
		public static TouchSimulation instance
		{
			get
			{
				return TouchSimulation.s_Instance;
			}
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0003FB90 File Offset: 0x0003DD90
		public static void Enable()
		{
			if (TouchSimulation.instance == null)
			{
				GameObject gameObject = new GameObject();
				gameObject.SetActive(false);
				gameObject.hideFlags = HideFlags.HideAndDontSave;
				TouchSimulation.s_Instance = gameObject.AddComponent<TouchSimulation>();
				TouchSimulation.instance.gameObject.SetActive(true);
			}
			TouchSimulation.instance.enabled = true;
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x0003FBE3 File Offset: 0x0003DDE3
		public static void Disable()
		{
			if (TouchSimulation.instance != null)
			{
				TouchSimulation.instance.enabled = false;
			}
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0003FBFD File Offset: 0x0003DDFD
		public static void Destroy()
		{
			TouchSimulation.Disable();
			if (TouchSimulation.s_Instance != null)
			{
				Object.Destroy(TouchSimulation.s_Instance.gameObject);
				TouchSimulation.s_Instance = null;
			}
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0003FC28 File Offset: 0x0003DE28
		protected void AddPointer(Pointer pointer)
		{
			if (pointer == null)
			{
				throw new ArgumentNullException("pointer");
			}
			if (this.m_Pointers.ContainsReference(this.m_NumPointers, pointer))
			{
				return;
			}
			ArrayHelpers.AppendWithCapacity<Pointer>(ref this.m_Pointers, ref this.m_NumPointers, pointer, 10);
			ArrayHelpers.Append<Vector2>(ref this.m_CurrentPositions, default(Vector2));
			ArrayHelpers.Append<int>(ref this.m_CurrentDisplayIndices, 0);
			InputSystem.DisableDevice(pointer, true);
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0003FC98 File Offset: 0x0003DE98
		protected void RemovePointer(Pointer pointer)
		{
			if (pointer == null)
			{
				throw new ArgumentNullException("pointer");
			}
			int num = this.m_Pointers.IndexOfReference(pointer, this.m_NumPointers);
			if (num == -1)
			{
				return;
			}
			for (int i = 0; i < this.m_Touches.Length; i++)
			{
				ButtonControl buttonControl = this.m_Touches[i];
				if (buttonControl == null || buttonControl.device == pointer)
				{
					this.UpdateTouch(i, num, TouchPhase.Canceled, default(InputEventPtr));
				}
			}
			this.m_Pointers.EraseAtWithCapacity(ref this.m_NumPointers, num);
			ArrayHelpers.EraseAt<Vector2>(ref this.m_CurrentPositions, num);
			ArrayHelpers.EraseAt<int>(ref this.m_CurrentDisplayIndices, num);
			if (pointer.added)
			{
				InputSystem.EnableDevice(pointer);
			}
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0003FD40 File Offset: 0x0003DF40
		private unsafe void OnEvent(InputEventPtr eventPtr, InputDevice device)
		{
			int num = this.m_Pointers.IndexOfReference(device, this.m_NumPointers);
			if (num < 0)
			{
				return;
			}
			FourCC type = eventPtr.type;
			if (type != 1398030676 && type != 1145852993)
			{
				return;
			}
			Pointer pointer = this.m_Pointers[num];
			Vector2Control position = pointer.position;
			void* statePtrFromStateEventUnchecked = position.GetStatePtrFromStateEventUnchecked(eventPtr, type);
			if (statePtrFromStateEventUnchecked != null)
			{
				this.m_CurrentPositions[num] = position.ReadValueFromState(statePtrFromStateEventUnchecked);
			}
			IntegerControl displayIndex = pointer.displayIndex;
			void* statePtrFromStateEventUnchecked2 = displayIndex.GetStatePtrFromStateEventUnchecked(eventPtr, type);
			if (statePtrFromStateEventUnchecked2 != null)
			{
				this.m_CurrentDisplayIndices[num] = displayIndex.ReadValueFromState(statePtrFromStateEventUnchecked2);
			}
			for (int i = 0; i < this.m_Touches.Length; i++)
			{
				ButtonControl buttonControl = this.m_Touches[i];
				if (buttonControl != null && buttonControl.device == device)
				{
					void* statePtrFromStateEventUnchecked3 = buttonControl.GetStatePtrFromStateEventUnchecked(eventPtr, type);
					if (statePtrFromStateEventUnchecked3 == null)
					{
						if (statePtrFromStateEventUnchecked != null)
						{
							this.UpdateTouch(i, num, TouchPhase.Moved, eventPtr);
						}
					}
					else if (buttonControl.ReadValueFromState(statePtrFromStateEventUnchecked3) < ButtonControl.s_GlobalDefaultButtonPressPoint * ButtonControl.s_GlobalDefaultButtonReleaseThreshold)
					{
						this.UpdateTouch(i, num, TouchPhase.Ended, eventPtr);
					}
				}
			}
			foreach (InputControl inputControl in eventPtr.EnumerateControls(InputControlExtensions.Enumerate.IgnoreControlsInDefaultState, device, 0f))
			{
				if (inputControl.isButton)
				{
					void* statePtrFromStateEventUnchecked4 = inputControl.GetStatePtrFromStateEventUnchecked(eventPtr, type);
					float num2 = 0f;
					inputControl.ReadValueFromStateIntoBuffer(statePtrFromStateEventUnchecked4, UnsafeUtility.AddressOf<float>(ref num2), 4);
					if (num2 > ButtonControl.s_GlobalDefaultButtonPressPoint)
					{
						int num3 = this.m_Touches.IndexOfReference(inputControl, -1);
						if (num3 < 0)
						{
							num3 = this.m_Touches.IndexOfReference(null, -1);
							if (num3 >= 0)
							{
								this.m_Touches[num3] = (ButtonControl)inputControl;
								this.UpdateTouch(num3, num, TouchPhase.Began, eventPtr);
							}
						}
						else
						{
							this.UpdateTouch(num3, num, TouchPhase.Moved, eventPtr);
						}
					}
				}
			}
			eventPtr.handled = true;
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0003FF40 File Offset: 0x0003E140
		private void OnDeviceChange(InputDevice device, InputDeviceChange change)
		{
			if (device == this.simulatedTouchscreen && change == InputDeviceChange.Removed)
			{
				TouchSimulation.Disable();
				return;
			}
			if (change != InputDeviceChange.Added)
			{
				if (change != InputDeviceChange.Removed)
				{
					return;
				}
				Pointer pointer = device as Pointer;
				if (pointer != null)
				{
					this.RemovePointer(pointer);
				}
			}
			else
			{
				Pointer pointer2 = device as Pointer;
				if (pointer2 != null)
				{
					if (device is Touchscreen)
					{
						return;
					}
					this.AddPointer(pointer2);
					return;
				}
			}
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0003FF94 File Offset: 0x0003E194
		protected void OnEnable()
		{
			if (this.simulatedTouchscreen != null)
			{
				if (!this.simulatedTouchscreen.added)
				{
					InputSystem.AddDevice(this.simulatedTouchscreen);
				}
			}
			else
			{
				this.simulatedTouchscreen = InputSystem.GetDevice("Simulated Touchscreen") as Touchscreen;
				if (this.simulatedTouchscreen == null)
				{
					this.simulatedTouchscreen = InputSystem.AddDevice<Touchscreen>("Simulated Touchscreen");
				}
			}
			if (this.m_Touches == null)
			{
				this.m_Touches = new ButtonControl[this.simulatedTouchscreen.touches.Count];
			}
			foreach (InputDevice inputDevice in InputSystem.devices)
			{
				this.OnDeviceChange(inputDevice, InputDeviceChange.Added);
			}
			if (this.m_OnDeviceChange == null)
			{
				this.m_OnDeviceChange = new Action<InputDevice, InputDeviceChange>(this.OnDeviceChange);
			}
			if (this.m_OnEvent == null)
			{
				this.m_OnEvent = new Action<InputEventPtr, InputDevice>(this.OnEvent);
			}
			InputSystem.onDeviceChange += this.m_OnDeviceChange;
			InputSystem.onEvent += this.m_OnEvent;
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x000400B4 File Offset: 0x0003E2B4
		protected void OnDisable()
		{
			if (this.simulatedTouchscreen != null && this.simulatedTouchscreen.added)
			{
				InputSystem.RemoveDevice(this.simulatedTouchscreen);
			}
			for (int i = 0; i < this.m_NumPointers; i++)
			{
				InputSystem.EnableDevice(this.m_Pointers[i]);
			}
			this.m_Pointers.Clear(this.m_NumPointers);
			this.m_Touches.Clear<ButtonControl>();
			this.m_NumPointers = 0;
			this.m_LastTouchId = 0;
			this.m_PrimaryTouchIndex = -1;
			InputSystem.onDeviceChange -= this.m_OnDeviceChange;
			InputSystem.onEvent -= this.m_OnEvent;
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x00040150 File Offset: 0x0003E350
		private unsafe void UpdateTouch(int touchIndex, int pointerIndex, TouchPhase phase, InputEventPtr eventPtr = default(InputEventPtr))
		{
			Vector2 vector = this.m_CurrentPositions[pointerIndex];
			byte b = (byte)this.m_CurrentDisplayIndices[pointerIndex];
			TouchState touchState = new TouchState
			{
				phase = phase,
				position = vector,
				displayIndex = b
			};
			double num = (eventPtr.valid ? eventPtr.time : InputState.currentTime);
			TouchState* ptr = (TouchState*)((byte*)this.simulatedTouchscreen.currentStatePtr + this.simulatedTouchscreen.touches[touchIndex].stateBlock.byteOffset);
			if (phase == TouchPhase.Began)
			{
				touchState.isPrimaryTouch = this.m_PrimaryTouchIndex < 0;
				touchState.startTime = num;
				touchState.startPosition = vector;
				int num2 = this.m_LastTouchId + 1;
				this.m_LastTouchId = num2;
				touchState.touchId = num2;
				touchState.tapCount = ptr->tapCount;
				if (touchState.isPrimaryTouch)
				{
					this.m_PrimaryTouchIndex = touchIndex;
				}
			}
			else
			{
				touchState.touchId = ptr->touchId;
				touchState.isPrimaryTouch = this.m_PrimaryTouchIndex == touchIndex;
				touchState.delta = vector - ptr->position;
				touchState.startPosition = ptr->startPosition;
				touchState.startTime = ptr->startTime;
				touchState.tapCount = ptr->tapCount;
				if (phase == TouchPhase.Ended)
				{
					touchState.isTap = num - ptr->startTime <= (double)Touchscreen.s_TapTime && (vector - ptr->startPosition).sqrMagnitude <= Touchscreen.s_TapRadiusSquared;
					if (touchState.isTap)
					{
						touchState.tapCount += 1;
					}
				}
			}
			if (touchState.isPrimaryTouch)
			{
				InputState.Change<TouchState>(this.simulatedTouchscreen.primaryTouch, touchState, InputUpdateType.None, eventPtr);
			}
			InputState.Change<TouchState>(this.simulatedTouchscreen.touches[touchIndex], touchState, InputUpdateType.None, eventPtr);
			if (phase.IsEndedOrCanceled())
			{
				this.m_Touches[touchIndex] = null;
				if (this.m_PrimaryTouchIndex == touchIndex)
				{
					this.m_PrimaryTouchIndex = -1;
				}
			}
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00040353 File Offset: 0x0003E553
		void IInputStateChangeMonitor.NotifyControlStateChanged(InputControl control, double time, InputEventPtr eventPtr, long monitorIndex)
		{
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00040355 File Offset: 0x0003E555
		void IInputStateChangeMonitor.NotifyTimerExpired(InputControl control, double time, long monitorIndex, int timerIndex)
		{
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x00040357 File Offset: 0x0003E557
		protected void InstallStateChangeMonitors(int startIndex = 0)
		{
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00040359 File Offset: 0x0003E559
		protected void OnSourceControlChangedValue(InputControl control, double time, InputEventPtr eventPtr, long sourceDeviceAndButtonIndex)
		{
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0004035B File Offset: 0x0003E55B
		protected void UninstallStateChangeMonitors(int startIndex = 0)
		{
		}

		// Token: 0x0400043F RID: 1087
		[NonSerialized]
		private int m_NumPointers;

		// Token: 0x04000440 RID: 1088
		[NonSerialized]
		private Pointer[] m_Pointers;

		// Token: 0x04000441 RID: 1089
		[NonSerialized]
		private Vector2[] m_CurrentPositions;

		// Token: 0x04000442 RID: 1090
		[NonSerialized]
		private int[] m_CurrentDisplayIndices;

		// Token: 0x04000443 RID: 1091
		[NonSerialized]
		private ButtonControl[] m_Touches;

		// Token: 0x04000444 RID: 1092
		[NonSerialized]
		private int m_LastTouchId;

		// Token: 0x04000445 RID: 1093
		[NonSerialized]
		private int m_PrimaryTouchIndex = -1;

		// Token: 0x04000446 RID: 1094
		[NonSerialized]
		private Action<InputDevice, InputDeviceChange> m_OnDeviceChange;

		// Token: 0x04000447 RID: 1095
		[NonSerialized]
		private Action<InputEventPtr, InputDevice> m_OnEvent;

		// Token: 0x04000448 RID: 1096
		internal static TouchSimulation s_Instance;
	}
}
