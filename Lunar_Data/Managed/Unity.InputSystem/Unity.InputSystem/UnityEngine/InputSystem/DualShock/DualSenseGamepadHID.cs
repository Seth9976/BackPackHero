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
	// Token: 0x0200009C RID: 156
	[InputControlLayout(stateType = typeof(DualSenseHIDInputReport), displayName = "DualSense HID")]
	public class DualSenseGamepadHID : DualShockGamepad, IEventMerger, IEventPreProcessor, IInputStateCallbackReceiver
	{
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x000404C6 File Offset: 0x0003E6C6
		// (set) Token: 0x06000C2A RID: 3114 RVA: 0x000404CE File Offset: 0x0003E6CE
		public ButtonControl leftTriggerButton { get; protected set; }

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x000404D7 File Offset: 0x0003E6D7
		// (set) Token: 0x06000C2C RID: 3116 RVA: 0x000404DF File Offset: 0x0003E6DF
		public ButtonControl rightTriggerButton { get; protected set; }

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x000404E8 File Offset: 0x0003E6E8
		// (set) Token: 0x06000C2E RID: 3118 RVA: 0x000404F0 File Offset: 0x0003E6F0
		public ButtonControl playStationButton { get; protected set; }

		// Token: 0x06000C2F RID: 3119 RVA: 0x000404F9 File Offset: 0x0003E6F9
		protected override void FinishSetup()
		{
			this.leftTriggerButton = base.GetChildControl<ButtonControl>("leftTriggerButton");
			this.rightTriggerButton = base.GetChildControl<ButtonControl>("rightTriggerButton");
			this.playStationButton = base.GetChildControl<ButtonControl>("systemButton");
			base.FinishSetup();
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x00040534 File Offset: 0x0003E734
		public override void PauseHaptics()
		{
			if (this.m_LowFrequencyMotorSpeed == null && this.m_HighFrequenceyMotorSpeed == null)
			{
				return;
			}
			this.SetMotorSpeedsAndLightBarColor(new float?(0f), new float?(0f), this.m_LightBarColor);
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x00040574 File Offset: 0x0003E774
		public override void ResetHaptics()
		{
			if (this.m_LowFrequencyMotorSpeed == null && this.m_HighFrequenceyMotorSpeed == null)
			{
				return;
			}
			this.m_HighFrequenceyMotorSpeed = null;
			this.m_LowFrequencyMotorSpeed = null;
			this.SetMotorSpeedsAndLightBarColor(this.m_LowFrequencyMotorSpeed, this.m_HighFrequenceyMotorSpeed, this.m_LightBarColor);
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x000405CD File Offset: 0x0003E7CD
		public override void ResumeHaptics()
		{
			if (this.m_LowFrequencyMotorSpeed == null && this.m_HighFrequenceyMotorSpeed == null)
			{
				return;
			}
			this.SetMotorSpeedsAndLightBarColor(this.m_LowFrequencyMotorSpeed, this.m_HighFrequenceyMotorSpeed, this.m_LightBarColor);
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00040603 File Offset: 0x0003E803
		public override void SetLightBarColor(Color color)
		{
			this.m_LightBarColor = new Color?(color);
			this.SetMotorSpeedsAndLightBarColor(this.m_LowFrequencyMotorSpeed, this.m_HighFrequenceyMotorSpeed, this.m_LightBarColor);
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x0004062A File Offset: 0x0003E82A
		public override void SetMotorSpeeds(float lowFrequency, float highFrequency)
		{
			this.m_LowFrequencyMotorSpeed = new float?(lowFrequency);
			this.m_HighFrequenceyMotorSpeed = new float?(highFrequency);
			this.SetMotorSpeedsAndLightBarColor(this.m_LowFrequencyMotorSpeed, this.m_HighFrequenceyMotorSpeed, this.m_LightBarColor);
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x00040660 File Offset: 0x0003E860
		public bool SetMotorSpeedsAndLightBarColor(float? lowFrequency, float? highFrequency, Color? color)
		{
			float num = ((lowFrequency != null) ? lowFrequency.Value : 0f);
			float num2 = ((highFrequency != null) ? highFrequency.Value : 0f);
			Color color2 = ((color != null) ? color.Value : Color.black);
			DualSenseHIDUSBOutputReport dualSenseHIDUSBOutputReport = DualSenseHIDUSBOutputReport.Create(new DualSenseHIDOutputReportPayload
			{
				enableFlags1 = 3,
				enableFlags2 = 4,
				lowFrequencyMotorSpeed = (byte)NumberHelpers.NormalizedFloatToUInt(num, 0U, 255U),
				highFrequencyMotorSpeed = (byte)NumberHelpers.NormalizedFloatToUInt(num2, 0U, 255U),
				redColor = (byte)NumberHelpers.NormalizedFloatToUInt(color2.r, 0U, 255U),
				greenColor = (byte)NumberHelpers.NormalizedFloatToUInt(color2.g, 0U, 255U),
				blueColor = (byte)NumberHelpers.NormalizedFloatToUInt(color2.b, 0U, 255U)
			});
			return base.ExecuteCommand<DualSenseHIDUSBOutputReport>(ref dualSenseHIDUSBOutputReport) >= 0L;
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x00040757 File Offset: 0x0003E957
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static bool MergeForward(DualSenseGamepadHID.DualSenseHIDUSBInputReport* currentState, DualSenseGamepadHID.DualSenseHIDUSBInputReport* nextState)
		{
			return currentState->buttons0 == nextState->buttons0 && currentState->buttons1 == nextState->buttons1 && currentState->buttons2 == nextState->buttons2;
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x00040785 File Offset: 0x0003E985
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static bool MergeForward(DualSenseGamepadHID.DualSenseHIDBluetoothInputReport* currentState, DualSenseGamepadHID.DualSenseHIDBluetoothInputReport* nextState)
		{
			return currentState->buttons0 == nextState->buttons0 && currentState->buttons1 == nextState->buttons1 && currentState->buttons2 == nextState->buttons2;
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x000407B3 File Offset: 0x0003E9B3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static bool MergeForward(DualSenseGamepadHID.DualSenseHIDMinimalInputReport* currentState, DualSenseGamepadHID.DualSenseHIDMinimalInputReport* nextState)
		{
			return currentState->buttons0 == nextState->buttons0 && currentState->buttons1 == nextState->buttons1 && currentState->buttons2 == nextState->buttons2;
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x000407E4 File Offset: 0x0003E9E4
		unsafe bool IEventMerger.MergeForward(InputEventPtr currentEventPtr, InputEventPtr nextEventPtr)
		{
			if (currentEventPtr.type != 1398030676 || nextEventPtr.type != 1398030676)
			{
				return false;
			}
			StateEvent* ptr = StateEvent.FromUnchecked(currentEventPtr);
			StateEvent* ptr2 = StateEvent.FromUnchecked(nextEventPtr);
			if (ptr->stateFormat != DualSenseGamepadHID.DualSenseHIDGenericInputReport.Format || ptr2->stateFormat != DualSenseGamepadHID.DualSenseHIDGenericInputReport.Format)
			{
				return false;
			}
			if (ptr->stateSizeInBytes != ptr2->stateSizeInBytes)
			{
				return false;
			}
			DualSenseGamepadHID.DualSenseHIDGenericInputReport* state = (DualSenseGamepadHID.DualSenseHIDGenericInputReport*)ptr->state;
			DualSenseGamepadHID.DualSenseHIDGenericInputReport* state2 = (DualSenseGamepadHID.DualSenseHIDGenericInputReport*)ptr2->state;
			if (state->reportId != state2->reportId)
			{
				return false;
			}
			if (state->reportId == 1)
			{
				if ((ulong)ptr->stateSizeInBytes == (ulong)((long)DualSenseGamepadHID.DualSenseHIDMinimalInputReport.ExpectedSize1) || (ulong)ptr->stateSizeInBytes == (ulong)((long)DualSenseGamepadHID.DualSenseHIDMinimalInputReport.ExpectedSize2))
				{
					DualSenseGamepadHID.DualSenseHIDMinimalInputReport* state3 = (DualSenseGamepadHID.DualSenseHIDMinimalInputReport*)ptr->state;
					DualSenseGamepadHID.DualSenseHIDMinimalInputReport* state4 = (DualSenseGamepadHID.DualSenseHIDMinimalInputReport*)ptr2->state;
					return DualSenseGamepadHID.MergeForward(state3, state4);
				}
				DualSenseGamepadHID.DualSenseHIDUSBInputReport* state5 = (DualSenseGamepadHID.DualSenseHIDUSBInputReport*)ptr->state;
				DualSenseGamepadHID.DualSenseHIDUSBInputReport* state6 = (DualSenseGamepadHID.DualSenseHIDUSBInputReport*)ptr2->state;
				return DualSenseGamepadHID.MergeForward(state5, state6);
			}
			else
			{
				if (state->reportId == 49)
				{
					DualSenseGamepadHID.DualSenseHIDBluetoothInputReport* state7 = (DualSenseGamepadHID.DualSenseHIDBluetoothInputReport*)ptr->state;
					DualSenseGamepadHID.DualSenseHIDBluetoothInputReport* state8 = (DualSenseGamepadHID.DualSenseHIDBluetoothInputReport*)ptr2->state;
					return DualSenseGamepadHID.MergeForward(state7, state8);
				}
				return false;
			}
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x00040908 File Offset: 0x0003EB08
		unsafe bool IEventPreProcessor.PreProcessEvent(InputEventPtr eventPtr)
		{
			if (eventPtr.type != 1398030676)
			{
				return eventPtr.type != 1145852993;
			}
			StateEvent* ptr = StateEvent.FromUnchecked(eventPtr);
			if (ptr->stateFormat == DualSenseHIDInputReport.Format)
			{
				return true;
			}
			uint stateSizeInBytes = ptr->stateSizeInBytes;
			if (ptr->stateFormat != DualSenseGamepadHID.DualSenseHIDGenericInputReport.Format || (ulong)stateSizeInBytes < (ulong)((long)sizeof(DualSenseHIDInputReport)))
			{
				return false;
			}
			DualSenseGamepadHID.DualSenseHIDGenericInputReport* state = (DualSenseGamepadHID.DualSenseHIDGenericInputReport*)ptr->state;
			if (state->reportId == 1)
			{
				if ((ulong)ptr->stateSizeInBytes == (ulong)((long)DualSenseGamepadHID.DualSenseHIDMinimalInputReport.ExpectedSize1) || (ulong)ptr->stateSizeInBytes == (ulong)((long)DualSenseGamepadHID.DualSenseHIDMinimalInputReport.ExpectedSize2))
				{
					DualSenseHIDInputReport dualSenseHIDInputReport = ((DualSenseGamepadHID.DualSenseHIDMinimalInputReport*)ptr->state)->ToHIDInputReport();
					*(DualSenseHIDInputReport*)ptr->state = dualSenseHIDInputReport;
				}
				else
				{
					DualSenseHIDInputReport dualSenseHIDInputReport2 = ((DualSenseGamepadHID.DualSenseHIDUSBInputReport*)ptr->state)->ToHIDInputReport();
					*(DualSenseHIDInputReport*)ptr->state = dualSenseHIDInputReport2;
				}
				ptr->stateFormat = DualSenseHIDInputReport.Format;
				return true;
			}
			if (state->reportId == 49)
			{
				DualSenseHIDInputReport dualSenseHIDInputReport3 = ((DualSenseGamepadHID.DualSenseHIDBluetoothInputReport*)ptr->state)->ToHIDInputReport();
				*(DualSenseHIDInputReport*)ptr->state = dualSenseHIDInputReport3;
				ptr->stateFormat = DualSenseHIDInputReport.Format;
				return true;
			}
			return false;
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x00040A26 File Offset: 0x0003EC26
		public void OnNextUpdate()
		{
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x00040A28 File Offset: 0x0003EC28
		public unsafe void OnStateEvent(InputEventPtr eventPtr)
		{
			if (eventPtr.type == 1398030676 && eventPtr.stateFormat == DualSenseHIDInputReport.Format)
			{
				DualSenseHIDInputReport* ptr = (DualSenseHIDInputReport*)((byte*)base.currentStatePtr + this.m_StateBlock.byteOffset);
				DualSenseHIDInputReport* state = (DualSenseHIDInputReport*)StateEvent.FromUnchecked(eventPtr)->state;
				if (state->leftStickX >= 120 && state->leftStickX <= 135 && state->leftStickY >= 120 && state->leftStickY <= 135 && state->rightStickX >= 120 && state->rightStickX <= 135 && state->rightStickY >= 120 && state->rightStickY <= 135 && state->leftTrigger == ptr->leftTrigger && state->rightTrigger == ptr->rightTrigger && state->buttons0 == ptr->buttons0 && state->buttons1 == ptr->buttons1 && state->buttons2 == ptr->buttons2)
				{
					InputSystem.s_Manager.DontMakeCurrentlyUpdatingDeviceCurrent();
				}
			}
			InputState.Change(this, eventPtr, InputUpdateType.None);
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00040B4B File Offset: 0x0003ED4B
		public bool GetStateOffsetForEvent(InputControl control, InputEventPtr eventPtr, ref uint offset)
		{
			return false;
		}

		// Token: 0x04000456 RID: 1110
		private float? m_LowFrequencyMotorSpeed;

		// Token: 0x04000457 RID: 1111
		private float? m_HighFrequenceyMotorSpeed;

		// Token: 0x04000458 RID: 1112
		private Color? m_LightBarColor;

		// Token: 0x04000459 RID: 1113
		private byte outputSequenceId;

		// Token: 0x0400045A RID: 1114
		internal const byte JitterMaskLow = 120;

		// Token: 0x0400045B RID: 1115
		internal const byte JitterMaskHigh = 135;

		// Token: 0x020001F0 RID: 496
		[StructLayout(LayoutKind.Explicit)]
		internal struct DualSenseHIDGenericInputReport
		{
			// Token: 0x1700057C RID: 1404
			// (get) Token: 0x06001462 RID: 5218 RVA: 0x0005EE98 File Offset: 0x0005D098
			public static FourCC Format
			{
				get
				{
					return new FourCC('H', 'I', 'D', ' ');
				}
			}

			// Token: 0x04000AD4 RID: 2772
			[FieldOffset(0)]
			public byte reportId;
		}

		// Token: 0x020001F1 RID: 497
		[StructLayout(LayoutKind.Explicit)]
		internal struct DualSenseHIDUSBInputReport
		{
			// Token: 0x06001463 RID: 5219 RVA: 0x0005EEA8 File Offset: 0x0005D0A8
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public DualSenseHIDInputReport ToHIDInputReport()
			{
				return new DualSenseHIDInputReport
				{
					leftStickX = this.leftStickX,
					leftStickY = this.leftStickY,
					rightStickX = this.rightStickX,
					rightStickY = this.rightStickY,
					leftTrigger = this.leftTrigger,
					rightTrigger = this.rightTrigger,
					buttons0 = this.buttons0,
					buttons1 = this.buttons1,
					buttons2 = (this.buttons2 & 7)
				};
			}

			// Token: 0x04000AD5 RID: 2773
			public const int ExpectedReportId = 1;

			// Token: 0x04000AD6 RID: 2774
			[FieldOffset(0)]
			public byte reportId;

			// Token: 0x04000AD7 RID: 2775
			[FieldOffset(1)]
			public byte leftStickX;

			// Token: 0x04000AD8 RID: 2776
			[FieldOffset(2)]
			public byte leftStickY;

			// Token: 0x04000AD9 RID: 2777
			[FieldOffset(3)]
			public byte rightStickX;

			// Token: 0x04000ADA RID: 2778
			[FieldOffset(4)]
			public byte rightStickY;

			// Token: 0x04000ADB RID: 2779
			[FieldOffset(5)]
			public byte leftTrigger;

			// Token: 0x04000ADC RID: 2780
			[FieldOffset(6)]
			public byte rightTrigger;

			// Token: 0x04000ADD RID: 2781
			[FieldOffset(8)]
			public byte buttons0;

			// Token: 0x04000ADE RID: 2782
			[FieldOffset(9)]
			public byte buttons1;

			// Token: 0x04000ADF RID: 2783
			[FieldOffset(10)]
			public byte buttons2;
		}

		// Token: 0x020001F2 RID: 498
		[StructLayout(LayoutKind.Explicit)]
		internal struct DualSenseHIDBluetoothInputReport
		{
			// Token: 0x06001464 RID: 5220 RVA: 0x0005EF38 File Offset: 0x0005D138
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public DualSenseHIDInputReport ToHIDInputReport()
			{
				return new DualSenseHIDInputReport
				{
					leftStickX = this.leftStickX,
					leftStickY = this.leftStickY,
					rightStickX = this.rightStickX,
					rightStickY = this.rightStickY,
					leftTrigger = this.leftTrigger,
					rightTrigger = this.rightTrigger,
					buttons0 = this.buttons0,
					buttons1 = this.buttons1,
					buttons2 = (this.buttons2 & 7)
				};
			}

			// Token: 0x04000AE0 RID: 2784
			public const int ExpectedReportId = 49;

			// Token: 0x04000AE1 RID: 2785
			[FieldOffset(0)]
			public byte reportId;

			// Token: 0x04000AE2 RID: 2786
			[FieldOffset(2)]
			public byte leftStickX;

			// Token: 0x04000AE3 RID: 2787
			[FieldOffset(3)]
			public byte leftStickY;

			// Token: 0x04000AE4 RID: 2788
			[FieldOffset(4)]
			public byte rightStickX;

			// Token: 0x04000AE5 RID: 2789
			[FieldOffset(5)]
			public byte rightStickY;

			// Token: 0x04000AE6 RID: 2790
			[FieldOffset(6)]
			public byte leftTrigger;

			// Token: 0x04000AE7 RID: 2791
			[FieldOffset(7)]
			public byte rightTrigger;

			// Token: 0x04000AE8 RID: 2792
			[FieldOffset(9)]
			public byte buttons0;

			// Token: 0x04000AE9 RID: 2793
			[FieldOffset(10)]
			public byte buttons1;

			// Token: 0x04000AEA RID: 2794
			[FieldOffset(11)]
			public byte buttons2;
		}

		// Token: 0x020001F3 RID: 499
		[StructLayout(LayoutKind.Explicit)]
		internal struct DualSenseHIDMinimalInputReport
		{
			// Token: 0x06001465 RID: 5221 RVA: 0x0005EFC8 File Offset: 0x0005D1C8
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public DualSenseHIDInputReport ToHIDInputReport()
			{
				return new DualSenseHIDInputReport
				{
					leftStickX = this.leftStickX,
					leftStickY = this.leftStickY,
					rightStickX = this.rightStickX,
					rightStickY = this.rightStickY,
					leftTrigger = this.leftTrigger,
					rightTrigger = this.rightTrigger,
					buttons0 = this.buttons0,
					buttons1 = this.buttons1,
					buttons2 = (this.buttons2 & 3)
				};
			}

			// Token: 0x04000AEB RID: 2795
			public static int ExpectedSize1 = 10;

			// Token: 0x04000AEC RID: 2796
			public static int ExpectedSize2 = 78;

			// Token: 0x04000AED RID: 2797
			[FieldOffset(0)]
			public byte reportId;

			// Token: 0x04000AEE RID: 2798
			[FieldOffset(1)]
			public byte leftStickX;

			// Token: 0x04000AEF RID: 2799
			[FieldOffset(2)]
			public byte leftStickY;

			// Token: 0x04000AF0 RID: 2800
			[FieldOffset(3)]
			public byte rightStickX;

			// Token: 0x04000AF1 RID: 2801
			[FieldOffset(4)]
			public byte rightStickY;

			// Token: 0x04000AF2 RID: 2802
			[FieldOffset(5)]
			public byte buttons0;

			// Token: 0x04000AF3 RID: 2803
			[FieldOffset(6)]
			public byte buttons1;

			// Token: 0x04000AF4 RID: 2804
			[FieldOffset(7)]
			public byte buttons2;

			// Token: 0x04000AF5 RID: 2805
			[FieldOffset(8)]
			public byte leftTrigger;

			// Token: 0x04000AF6 RID: 2806
			[FieldOffset(9)]
			public byte rightTrigger;
		}
	}
}
