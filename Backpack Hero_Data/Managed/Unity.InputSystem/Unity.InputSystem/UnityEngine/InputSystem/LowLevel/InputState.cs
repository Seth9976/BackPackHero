using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000F2 RID: 242
	public static class InputState
	{
		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x00044E61 File Offset: 0x00043061
		public static InputUpdateType currentUpdateType
		{
			get
			{
				return InputUpdate.s_LatestUpdateType;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x00044E68 File Offset: 0x00043068
		public static uint updateCount
		{
			get
			{
				return InputUpdate.s_UpdateStepCount;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x00044E6F File Offset: 0x0004306F
		public static double currentTime
		{
			get
			{
				return InputRuntime.s_Instance.currentTime - InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			}
		}

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06000E2D RID: 3629 RVA: 0x00044E81 File Offset: 0x00043081
		// (remove) Token: 0x06000E2E RID: 3630 RVA: 0x00044E8E File Offset: 0x0004308E
		public static event Action<InputDevice, InputEventPtr> onChange
		{
			add
			{
				InputSystem.s_Manager.onDeviceStateChange += value;
			}
			remove
			{
				InputSystem.s_Manager.onDeviceStateChange -= value;
			}
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x00044E9C File Offset: 0x0004309C
		public unsafe static void Change(InputDevice device, InputEventPtr eventPtr, InputUpdateType updateType = InputUpdateType.None)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (!eventPtr.valid)
			{
				throw new ArgumentNullException("eventPtr");
			}
			FourCC type = eventPtr.type;
			FourCC fourCC;
			if (type == 1398030676)
			{
				fourCC = StateEvent.FromUnchecked(eventPtr)->stateFormat;
			}
			else
			{
				if (!(type == 1145852993))
				{
					return;
				}
				fourCC = DeltaStateEvent.FromUnchecked(eventPtr)->stateFormat;
			}
			if (fourCC != device.stateBlock.format)
			{
				throw new ArgumentException(string.Format("State format {0} from event does not match state format {1} of device {2}", fourCC, device.stateBlock.format, device), "eventPtr");
			}
			InputSystem.s_Manager.UpdateState(device, eventPtr, (updateType != InputUpdateType.None) ? updateType : InputSystem.s_Manager.defaultUpdateType);
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x00044F7B File Offset: 0x0004317B
		public static void Change<TState>(InputControl control, TState state, InputUpdateType updateType = InputUpdateType.None, InputEventPtr eventPtr = default(InputEventPtr)) where TState : struct
		{
			InputState.Change<TState>(control, ref state, updateType, eventPtr);
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x00044F88 File Offset: 0x00043188
		public unsafe static void Change<TState>(InputControl control, ref TState state, InputUpdateType updateType = InputUpdateType.None, InputEventPtr eventPtr = default(InputEventPtr)) where TState : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (control.stateBlock.bitOffset != 0U || control.stateBlock.sizeInBits % 8U != 0U)
			{
				throw new ArgumentException(string.Format("Cannot change state of bitfield control '{0}' using this method", control), "control");
			}
			InputDevice device = control.device;
			long num = Math.Min((long)UnsafeUtility.SizeOf<TState>(), (long)((ulong)control.m_StateBlock.alignedSizeInBytes));
			void* ptr = UnsafeUtility.AddressOf<TState>(ref state);
			uint num2 = control.stateBlock.byteOffset - device.stateBlock.byteOffset;
			InputSystem.s_Manager.UpdateState(device, (updateType != InputUpdateType.None) ? updateType : InputSystem.s_Manager.defaultUpdateType, ptr, num2, (uint)num, eventPtr.valid ? eventPtr.internalTime : InputRuntime.s_Instance.currentTime, eventPtr);
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x00045060 File Offset: 0x00043260
		public static bool IsIntegerFormat(this FourCC format)
		{
			return format == InputStateBlock.FormatBit || format == InputStateBlock.FormatInt || format == InputStateBlock.FormatByte || format == InputStateBlock.FormatShort || format == InputStateBlock.FormatSBit || format == InputStateBlock.FormatUInt || format == InputStateBlock.FormatUShort || format == InputStateBlock.FormatLong || format == InputStateBlock.FormatULong;
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x000450E4 File Offset: 0x000432E4
		public static void AddChangeMonitor(InputControl control, IInputStateChangeMonitor monitor, long monitorIndex = -1L, uint groupIndex = 0U)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (monitor == null)
			{
				throw new ArgumentNullException("monitor");
			}
			if (!control.device.added)
			{
				throw new ArgumentException(string.Format("Device for control '{0}' has not been added to system", control));
			}
			InputSystem.s_Manager.AddStateChangeMonitor(control, monitor, monitorIndex, groupIndex);
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0004513C File Offset: 0x0004333C
		public static IInputStateChangeMonitor AddChangeMonitor(InputControl control, Action<InputControl, double, InputEventPtr, long> valueChangeCallback, int monitorIndex = -1, Action<InputControl, double, long, int> timerExpiredCallback = null)
		{
			if (valueChangeCallback == null)
			{
				throw new ArgumentNullException("valueChangeCallback");
			}
			InputState.StateChangeMonitorDelegate stateChangeMonitorDelegate = new InputState.StateChangeMonitorDelegate
			{
				valueChangeCallback = valueChangeCallback,
				timerExpiredCallback = timerExpiredCallback
			};
			InputState.AddChangeMonitor(control, stateChangeMonitorDelegate, (long)monitorIndex, 0U);
			return stateChangeMonitorDelegate;
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00045176 File Offset: 0x00043376
		public static void RemoveChangeMonitor(InputControl control, IInputStateChangeMonitor monitor, long monitorIndex = -1L)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (monitor == null)
			{
				throw new ArgumentNullException("monitor");
			}
			InputSystem.s_Manager.RemoveStateChangeMonitor(control, monitor, monitorIndex);
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x000451A1 File Offset: 0x000433A1
		public static void AddChangeMonitorTimeout(InputControl control, IInputStateChangeMonitor monitor, double time, long monitorIndex = -1L, int timerIndex = -1)
		{
			if (monitor == null)
			{
				throw new ArgumentNullException("monitor");
			}
			InputSystem.s_Manager.AddStateChangeMonitorTimeout(control, monitor, time, monitorIndex, timerIndex);
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x000451C1 File Offset: 0x000433C1
		public static void RemoveChangeMonitorTimeout(IInputStateChangeMonitor monitor, long monitorIndex = -1L, int timerIndex = -1)
		{
			if (monitor == null)
			{
				throw new ArgumentNullException("monitor");
			}
			InputSystem.s_Manager.RemoveStateChangeMonitorTimeout(monitor, monitorIndex, timerIndex);
		}

		// Token: 0x02000216 RID: 534
		private class StateChangeMonitorDelegate : IInputStateChangeMonitor
		{
			// Token: 0x060014B1 RID: 5297 RVA: 0x0005FF14 File Offset: 0x0005E114
			public void NotifyControlStateChanged(InputControl control, double time, InputEventPtr eventPtr, long monitorIndex)
			{
				this.valueChangeCallback(control, time, eventPtr, monitorIndex);
			}

			// Token: 0x060014B2 RID: 5298 RVA: 0x0005FF26 File Offset: 0x0005E126
			public void NotifyTimerExpired(InputControl control, double time, long monitorIndex, int timerIndex)
			{
				Action<InputControl, double, long, int> action = this.timerExpiredCallback;
				if (action == null)
				{
					return;
				}
				action(control, time, monitorIndex, timerIndex);
			}

			// Token: 0x04000B4C RID: 2892
			public Action<InputControl, double, InputEventPtr, long> valueChangeCallback;

			// Token: 0x04000B4D RID: 2893
			public Action<InputControl, double, long, int> timerExpiredCallback;
		}
	}
}
