using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.DualShock.LowLevel;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.DualShock
{
	// Token: 0x0200009D RID: 157
	[InputControlLayout(stateType = typeof(DualShock4HIDInputReport), hideInUI = true, isNoisy = true)]
	public class DualShock4GamepadHID : DualShockGamepad, IEventPreProcessor, IInputStateCallbackReceiver
	{
		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x00040B9E File Offset: 0x0003ED9E
		// (set) Token: 0x06000C43 RID: 3139 RVA: 0x00040BA6 File Offset: 0x0003EDA6
		public ButtonControl leftTriggerButton { get; protected set; }

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x00040BAF File Offset: 0x0003EDAF
		// (set) Token: 0x06000C45 RID: 3141 RVA: 0x00040BB7 File Offset: 0x0003EDB7
		public ButtonControl rightTriggerButton { get; protected set; }

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x00040BC0 File Offset: 0x0003EDC0
		// (set) Token: 0x06000C47 RID: 3143 RVA: 0x00040BC8 File Offset: 0x0003EDC8
		public ButtonControl playStationButton { get; protected set; }

		// Token: 0x06000C48 RID: 3144 RVA: 0x00040BD1 File Offset: 0x0003EDD1
		protected override void FinishSetup()
		{
			this.leftTriggerButton = base.GetChildControl<ButtonControl>("leftTriggerButton");
			this.rightTriggerButton = base.GetChildControl<ButtonControl>("rightTriggerButton");
			this.playStationButton = base.GetChildControl<ButtonControl>("systemButton");
			base.FinishSetup();
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x00040C0C File Offset: 0x0003EE0C
		public override void PauseHaptics()
		{
			if (this.m_LowFrequencyMotorSpeed == null && this.m_HighFrequenceyMotorSpeed == null && this.m_LightBarColor == null)
			{
				return;
			}
			DualShockHIDOutputReport dualShockHIDOutputReport = DualShockHIDOutputReport.Create();
			dualShockHIDOutputReport.SetMotorSpeeds(0f, 0f);
			if (this.m_LightBarColor != null)
			{
				dualShockHIDOutputReport.SetColor(Color.black);
			}
			base.ExecuteCommand<DualShockHIDOutputReport>(ref dualShockHIDOutputReport);
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x00040C7C File Offset: 0x0003EE7C
		public override void ResetHaptics()
		{
			if (this.m_LowFrequencyMotorSpeed == null && this.m_HighFrequenceyMotorSpeed == null && this.m_LightBarColor == null)
			{
				return;
			}
			DualShockHIDOutputReport dualShockHIDOutputReport = DualShockHIDOutputReport.Create();
			dualShockHIDOutputReport.SetMotorSpeeds(0f, 0f);
			if (this.m_LightBarColor != null)
			{
				dualShockHIDOutputReport.SetColor(Color.black);
			}
			base.ExecuteCommand<DualShockHIDOutputReport>(ref dualShockHIDOutputReport);
			this.m_HighFrequenceyMotorSpeed = null;
			this.m_LowFrequencyMotorSpeed = null;
			this.m_LightBarColor = null;
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x00040D10 File Offset: 0x0003EF10
		public override void ResumeHaptics()
		{
			if (this.m_LowFrequencyMotorSpeed == null && this.m_HighFrequenceyMotorSpeed == null && this.m_LightBarColor == null)
			{
				return;
			}
			DualShockHIDOutputReport dualShockHIDOutputReport = DualShockHIDOutputReport.Create();
			if (this.m_LowFrequencyMotorSpeed != null || this.m_HighFrequenceyMotorSpeed != null)
			{
				dualShockHIDOutputReport.SetMotorSpeeds(this.m_LowFrequencyMotorSpeed.Value, this.m_HighFrequenceyMotorSpeed.Value);
			}
			if (this.m_LightBarColor != null)
			{
				dualShockHIDOutputReport.SetColor(this.m_LightBarColor.Value);
			}
			base.ExecuteCommand<DualShockHIDOutputReport>(ref dualShockHIDOutputReport);
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00040DAC File Offset: 0x0003EFAC
		public override void SetLightBarColor(Color color)
		{
			DualShockHIDOutputReport dualShockHIDOutputReport = DualShockHIDOutputReport.Create();
			dualShockHIDOutputReport.SetColor(color);
			base.ExecuteCommand<DualShockHIDOutputReport>(ref dualShockHIDOutputReport);
			this.m_LightBarColor = new Color?(color);
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x00040DDC File Offset: 0x0003EFDC
		public override void SetMotorSpeeds(float lowFrequency, float highFrequency)
		{
			DualShockHIDOutputReport dualShockHIDOutputReport = DualShockHIDOutputReport.Create();
			dualShockHIDOutputReport.SetMotorSpeeds(lowFrequency, highFrequency);
			base.ExecuteCommand<DualShockHIDOutputReport>(ref dualShockHIDOutputReport);
			this.m_LowFrequencyMotorSpeed = new float?(lowFrequency);
			this.m_HighFrequenceyMotorSpeed = new float?(highFrequency);
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00040E1C File Offset: 0x0003F01C
		public bool SetMotorSpeedsAndLightBarColor(float lowFrequency, float highFrequency, Color color)
		{
			DualShockHIDOutputReport dualShockHIDOutputReport = DualShockHIDOutputReport.Create();
			dualShockHIDOutputReport.SetMotorSpeeds(lowFrequency, highFrequency);
			dualShockHIDOutputReport.SetColor(color);
			long num = base.ExecuteCommand<DualShockHIDOutputReport>(ref dualShockHIDOutputReport);
			this.m_LowFrequencyMotorSpeed = new float?(lowFrequency);
			this.m_HighFrequenceyMotorSpeed = new float?(highFrequency);
			this.m_LightBarColor = new Color?(color);
			return num >= 0L;
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00040E74 File Offset: 0x0003F074
		unsafe bool IEventPreProcessor.PreProcessEvent(InputEventPtr eventPtr)
		{
			if (eventPtr.type != 1398030676)
			{
				return eventPtr.type != 1145852993;
			}
			StateEvent* ptr = StateEvent.FromUnchecked(eventPtr);
			if (ptr->stateFormat == DualShock4HIDInputReport.Format)
			{
				return true;
			}
			uint stateSizeInBytes = ptr->stateSizeInBytes;
			if (ptr->stateFormat != DualShock4GamepadHID.DualShock4HIDGenericInputReport.Format || (ulong)stateSizeInBytes < (ulong)((long)sizeof(DualShock4GamepadHID.DualShock4HIDGenericInputReport)))
			{
				return false;
			}
			byte* state = (byte*)ptr->state;
			byte b = *state;
			if (b != 1)
			{
				if (b - 17 > 8)
				{
					return false;
				}
				if ((state[1] & 128) == 0)
				{
					return false;
				}
				if ((ulong)stateSizeInBytes < (ulong)((long)(sizeof(DualShock4GamepadHID.DualShock4HIDGenericInputReport) + 3)))
				{
					return false;
				}
				DualShock4HIDInputReport dualShock4HIDInputReport = ((DualShock4GamepadHID.DualShock4HIDGenericInputReport*)(state + 3))->ToHIDInputReport();
				*(DualShock4HIDInputReport*)ptr->state = dualShock4HIDInputReport;
				ptr->stateFormat = DualShock4HIDInputReport.Format;
				return true;
			}
			else
			{
				if ((ulong)stateSizeInBytes < (ulong)((long)(sizeof(DualShock4GamepadHID.DualShock4HIDGenericInputReport) + 1)))
				{
					return false;
				}
				DualShock4HIDInputReport dualShock4HIDInputReport2 = ((DualShock4GamepadHID.DualShock4HIDGenericInputReport*)(state + 1))->ToHIDInputReport();
				*(DualShock4HIDInputReport*)ptr->state = dualShock4HIDInputReport2;
				ptr->stateFormat = DualShock4HIDInputReport.Format;
				return true;
			}
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00040F7D File Offset: 0x0003F17D
		public void OnNextUpdate()
		{
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x00040F80 File Offset: 0x0003F180
		public unsafe void OnStateEvent(InputEventPtr eventPtr)
		{
			if (eventPtr.type == 1398030676 && eventPtr.stateFormat == DualShock4HIDInputReport.Format)
			{
				DualShock4HIDInputReport* ptr = (DualShock4HIDInputReport*)((byte*)base.currentStatePtr + this.m_StateBlock.byteOffset);
				DualShock4HIDInputReport* state = (DualShock4HIDInputReport*)StateEvent.FromUnchecked(eventPtr)->state;
				if (state->leftStickX >= 120 && state->leftStickX <= 135 && state->leftStickY >= 120 && state->leftStickY <= 135 && state->rightStickX >= 120 && state->rightStickX <= 135 && state->rightStickY >= 120 && state->rightStickY <= 135 && state->leftTrigger == ptr->leftTrigger && state->rightTrigger == ptr->rightTrigger && state->buttons1 == ptr->buttons1 && state->buttons2 == ptr->buttons2 && state->buttons3 == ptr->buttons3)
				{
					InputSystem.s_Manager.DontMakeCurrentlyUpdatingDeviceCurrent();
				}
			}
			InputState.Change(this, eventPtr, InputUpdateType.None);
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x000410A3 File Offset: 0x0003F2A3
		public bool GetStateOffsetForEvent(InputControl control, InputEventPtr eventPtr, ref uint offset)
		{
			return false;
		}

		// Token: 0x0400045F RID: 1119
		private float? m_LowFrequencyMotorSpeed;

		// Token: 0x04000460 RID: 1120
		private float? m_HighFrequenceyMotorSpeed;

		// Token: 0x04000461 RID: 1121
		private Color? m_LightBarColor;

		// Token: 0x04000462 RID: 1122
		internal const byte JitterMaskLow = 120;

		// Token: 0x04000463 RID: 1123
		internal const byte JitterMaskHigh = 135;

		// Token: 0x020001F4 RID: 500
		[StructLayout(LayoutKind.Explicit)]
		internal struct DualShock4HIDGenericInputReport
		{
			// Token: 0x1700057F RID: 1407
			// (get) Token: 0x0600146E RID: 5230 RVA: 0x0005F27A File Offset: 0x0005D47A
			public static FourCC Format
			{
				get
				{
					return new FourCC('H', 'I', 'D', ' ');
				}
			}

			// Token: 0x0600146F RID: 5231 RVA: 0x0005F28C File Offset: 0x0005D48C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public DualShock4HIDInputReport ToHIDInputReport()
			{
				return new DualShock4HIDInputReport
				{
					leftStickX = this.leftStickX,
					leftStickY = this.leftStickY,
					rightStickX = this.rightStickX,
					rightStickY = this.rightStickY,
					leftTrigger = this.leftTrigger,
					rightTrigger = this.rightTrigger,
					buttons1 = this.buttons0,
					buttons2 = this.buttons1,
					buttons3 = this.buttons2
				};
			}

			// Token: 0x04000AF8 RID: 2808
			[FieldOffset(0)]
			public byte leftStickX;

			// Token: 0x04000AF9 RID: 2809
			[FieldOffset(1)]
			public byte leftStickY;

			// Token: 0x04000AFA RID: 2810
			[FieldOffset(2)]
			public byte rightStickX;

			// Token: 0x04000AFB RID: 2811
			[FieldOffset(3)]
			public byte rightStickY;

			// Token: 0x04000AFC RID: 2812
			[FieldOffset(4)]
			public byte buttons0;

			// Token: 0x04000AFD RID: 2813
			[FieldOffset(5)]
			public byte buttons1;

			// Token: 0x04000AFE RID: 2814
			[FieldOffset(6)]
			public byte buttons2;

			// Token: 0x04000AFF RID: 2815
			[FieldOffset(7)]
			public byte leftTrigger;

			// Token: 0x04000B00 RID: 2816
			[FieldOffset(8)]
			public byte rightTrigger;
		}
	}
}
