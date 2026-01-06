using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Switch.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Switch
{
	// Token: 0x0200008D RID: 141
	[InputControlLayout(stateType = typeof(SwitchProControllerHIDInputState), displayName = "Switch Pro Controller")]
	public class SwitchProControllerHID : Gamepad, IInputStateCallbackReceiver, IEventPreProcessor
	{
		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x0003CB29 File Offset: 0x0003AD29
		// (set) Token: 0x06000B6A RID: 2922 RVA: 0x0003CB31 File Offset: 0x0003AD31
		[InputControl(name = "capture", displayName = "Capture")]
		public ButtonControl captureButton { get; protected set; }

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x0003CB3A File Offset: 0x0003AD3A
		// (set) Token: 0x06000B6C RID: 2924 RVA: 0x0003CB42 File Offset: 0x0003AD42
		[InputControl(name = "home", displayName = "Home")]
		public ButtonControl homeButton { get; protected set; }

		// Token: 0x06000B6D RID: 2925 RVA: 0x0003CB4B File Offset: 0x0003AD4B
		protected override void OnAdded()
		{
			base.OnAdded();
			this.captureButton = base.GetChildControl<ButtonControl>("capture");
			this.homeButton = base.GetChildControl<ButtonControl>("home");
			this.HandshakeRestart();
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0003CB7B File Offset: 0x0003AD7B
		private void HandshakeRestart()
		{
			this.m_HandshakeStepIndex = -1;
			this.m_HandshakeTimer = InputRuntime.s_Instance.currentTime;
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0003CB94 File Offset: 0x0003AD94
		private void HandshakeTick()
		{
			double currentTime = InputRuntime.s_Instance.currentTime;
			if (currentTime >= this.m_LastUpdateTimeInternal + 2.0 && currentTime >= this.m_HandshakeTimer + 2.0)
			{
				this.m_HandshakeStepIndex = 0;
			}
			else
			{
				if (this.m_HandshakeStepIndex + 1 >= SwitchProControllerHID.s_HandshakeSequence.Length)
				{
					return;
				}
				if (currentTime <= this.m_HandshakeTimer + 0.1)
				{
					return;
				}
				this.m_HandshakeStepIndex++;
			}
			this.m_HandshakeTimer = currentTime;
			SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType commandIdType = SwitchProControllerHID.s_HandshakeSequence[this.m_HandshakeStepIndex];
			SwitchProControllerHID.SwitchMagicOutputHIDBluetooth switchMagicOutputHIDBluetooth = SwitchProControllerHID.SwitchMagicOutputHIDBluetooth.Create(commandIdType);
			if (base.ExecuteCommand<SwitchProControllerHID.SwitchMagicOutputHIDBluetooth>(ref switchMagicOutputHIDBluetooth) > 0L)
			{
				return;
			}
			SwitchProControllerHID.SwitchMagicOutputHIDUSB switchMagicOutputHIDUSB = SwitchProControllerHID.SwitchMagicOutputHIDUSB.Create(commandIdType);
			base.ExecuteCommand<SwitchProControllerHID.SwitchMagicOutputHIDUSB>(ref switchMagicOutputHIDUSB);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0003CC49 File Offset: 0x0003AE49
		public void OnNextUpdate()
		{
			this.HandshakeTick();
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0003CC54 File Offset: 0x0003AE54
		public unsafe void OnStateEvent(InputEventPtr eventPtr)
		{
			if (eventPtr.type == 1398030676 && eventPtr.stateFormat == SwitchProControllerHIDInputState.Format)
			{
				SwitchProControllerHIDInputState* ptr = (SwitchProControllerHIDInputState*)((byte*)base.currentStatePtr + this.m_StateBlock.byteOffset);
				SwitchProControllerHIDInputState* state = (SwitchProControllerHIDInputState*)StateEvent.FromUnchecked(eventPtr)->state;
				if (state->leftStickX >= 120 && state->leftStickX <= 135 && state->leftStickY >= 120 && state->leftStickY <= 135 && state->rightStickX >= 120 && state->rightStickX <= 135 && state->rightStickY >= 120 && state->rightStickY <= 135 && state->buttons1 == ptr->buttons1 && state->buttons2 == ptr->buttons2)
				{
					InputSystem.s_Manager.DontMakeCurrentlyUpdatingDeviceCurrent();
				}
			}
			InputState.Change(this, eventPtr, InputUpdateType.None);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0003CD44 File Offset: 0x0003AF44
		public bool GetStateOffsetForEvent(InputControl control, InputEventPtr eventPtr, ref uint offset)
		{
			return false;
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0003CD48 File Offset: 0x0003AF48
		public unsafe bool PreProcessEvent(InputEventPtr eventPtr)
		{
			if (eventPtr.type == 1145852993)
			{
				return DeltaStateEvent.FromUnchecked(eventPtr)->stateFormat == SwitchProControllerHIDInputState.Format;
			}
			if (eventPtr.type != 1398030676)
			{
				return true;
			}
			StateEvent* ptr = StateEvent.FromUnchecked(eventPtr);
			uint stateSizeInBytes = ptr->stateSizeInBytes;
			if (ptr->stateFormat == SwitchProControllerHIDInputState.Format)
			{
				return true;
			}
			if (ptr->stateFormat != SwitchProControllerHID.SwitchHIDGenericInputReport.Format || (ulong)stateSizeInBytes < (ulong)((long)sizeof(SwitchProControllerHID.SwitchHIDGenericInputReport)))
			{
				return false;
			}
			SwitchProControllerHID.SwitchHIDGenericInputReport* state = (SwitchProControllerHID.SwitchHIDGenericInputReport*)ptr->state;
			if (state->reportId == 63 && stateSizeInBytes >= 12U)
			{
				SwitchProControllerHIDInputState switchProControllerHIDInputState = ((SwitchProControllerHID.SwitchSimpleInputReport*)ptr->state)->ToHIDInputReport();
				*(SwitchProControllerHIDInputState*)ptr->state = switchProControllerHIDInputState;
				ptr->stateFormat = SwitchProControllerHIDInputState.Format;
				return true;
			}
			if (state->reportId == 48 && stateSizeInBytes >= 25U)
			{
				SwitchProControllerHIDInputState switchProControllerHIDInputState2 = ((SwitchProControllerHID.SwitchFullInputReport*)ptr->state)->ToHIDInputReport();
				*(SwitchProControllerHIDInputState*)ptr->state = switchProControllerHIDInputState2;
				ptr->stateFormat = SwitchProControllerHIDInputState.Format;
				return true;
			}
			if (stateSizeInBytes == 8U || stateSizeInBytes == 9U)
			{
				int num = ((stateSizeInBytes == 9U) ? 1 : 0);
				SwitchProControllerHIDInputState switchProControllerHIDInputState3 = ((SwitchProControllerHID.SwitchInputOnlyReport*)((byte*)ptr->state + num))->ToHIDInputReport();
				*(SwitchProControllerHIDInputState*)ptr->state = switchProControllerHIDInputState3;
				ptr->stateFormat = SwitchProControllerHIDInputState.Format;
				return true;
			}
			return false;
		}

		// Token: 0x0400040F RID: 1039
		private static readonly SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType[] s_HandshakeSequence = new SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType[]
		{
			SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType.Status,
			SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType.Handshake,
			SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType.Highspeed,
			SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType.Handshake,
			SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType.ForceUSB
		};

		// Token: 0x04000410 RID: 1040
		private int m_HandshakeStepIndex;

		// Token: 0x04000411 RID: 1041
		private double m_HandshakeTimer;

		// Token: 0x04000412 RID: 1042
		internal const byte JitterMaskLow = 120;

		// Token: 0x04000413 RID: 1043
		internal const byte JitterMaskHigh = 135;

		// Token: 0x020001CF RID: 463
		[StructLayout(LayoutKind.Explicit, Size = 7)]
		private struct SwitchInputOnlyReport
		{
			// Token: 0x06001419 RID: 5145 RVA: 0x0005C8B8 File Offset: 0x0005AAB8
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public SwitchProControllerHIDInputState ToHIDInputReport()
			{
				SwitchProControllerHIDInputState switchProControllerHIDInputState = new SwitchProControllerHIDInputState
				{
					leftStickX = this.leftX,
					leftStickY = this.leftY,
					rightStickX = this.rightX,
					rightStickY = this.rightY
				};
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.West, (this.buttons0 & 1) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.South, (this.buttons0 & 2) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.East, (this.buttons0 & 4) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.North, (this.buttons0 & 8) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.L, (this.buttons0 & 16) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.R, (this.buttons0 & 32) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.ZL, (this.buttons0 & 64) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.ZR, (this.buttons0 & 128) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Minus, (this.buttons1 & 1) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Plus, (this.buttons1 & 2) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.StickL, (this.buttons1 & 4) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.StickR, (this.buttons1 & 8) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Home, (this.buttons1 & 16) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Capture, (this.buttons1 & 32) > 0);
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				switch (this.hat)
				{
				case 0:
					flag2 = true;
					break;
				case 1:
					flag2 = true;
					flag3 = true;
					break;
				case 2:
					flag3 = true;
					break;
				case 3:
					flag4 = true;
					flag3 = true;
					break;
				case 4:
					flag4 = true;
					break;
				case 5:
					flag4 = true;
					flag = true;
					break;
				case 6:
					flag = true;
					break;
				case 7:
					flag2 = true;
					flag = true;
					break;
				}
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Left, flag);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Up, flag2);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Right, flag3);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Down, flag4);
				return switchProControllerHIDInputState;
			}

			// Token: 0x0400095E RID: 2398
			public const int kSize = 7;

			// Token: 0x0400095F RID: 2399
			[FieldOffset(0)]
			public byte buttons0;

			// Token: 0x04000960 RID: 2400
			[FieldOffset(1)]
			public byte buttons1;

			// Token: 0x04000961 RID: 2401
			[FieldOffset(2)]
			public byte hat;

			// Token: 0x04000962 RID: 2402
			[FieldOffset(3)]
			public byte leftX;

			// Token: 0x04000963 RID: 2403
			[FieldOffset(4)]
			public byte leftY;

			// Token: 0x04000964 RID: 2404
			[FieldOffset(5)]
			public byte rightX;

			// Token: 0x04000965 RID: 2405
			[FieldOffset(6)]
			public byte rightY;
		}

		// Token: 0x020001D0 RID: 464
		[StructLayout(LayoutKind.Explicit, Size = 12)]
		private struct SwitchSimpleInputReport
		{
			// Token: 0x0600141A RID: 5146 RVA: 0x0005CAAC File Offset: 0x0005ACAC
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public SwitchProControllerHIDInputState ToHIDInputReport()
			{
				byte b = (byte)NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits((uint)this.leftX, 16U, 8U);
				byte b2 = (byte)NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits((uint)this.leftY, 16U, 8U);
				byte b3 = (byte)NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits((uint)this.rightX, 16U, 8U);
				byte b4 = (byte)NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits((uint)this.rightY, 16U, 8U);
				SwitchProControllerHIDInputState switchProControllerHIDInputState = new SwitchProControllerHIDInputState
				{
					leftStickX = b,
					leftStickY = b2,
					rightStickX = b3,
					rightStickY = b4
				};
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.South, (this.buttons0 & 1) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.East, (this.buttons0 & 2) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.West, (this.buttons0 & 4) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.North, (this.buttons0 & 8) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.L, (this.buttons0 & 16) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.R, (this.buttons0 & 32) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.ZL, (this.buttons0 & 64) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.ZR, (this.buttons0 & 128) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Minus, (this.buttons1 & 1) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Plus, (this.buttons1 & 2) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.StickL, (this.buttons1 & 4) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.StickR, (this.buttons1 & 8) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Home, (this.buttons1 & 16) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Capture, (this.buttons1 & 32) > 0);
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				switch (this.hat)
				{
				case 0:
					flag2 = true;
					break;
				case 1:
					flag2 = true;
					flag3 = true;
					break;
				case 2:
					flag3 = true;
					break;
				case 3:
					flag4 = true;
					flag3 = true;
					break;
				case 4:
					flag4 = true;
					break;
				case 5:
					flag4 = true;
					flag = true;
					break;
				case 6:
					flag = true;
					break;
				case 7:
					flag2 = true;
					flag = true;
					break;
				}
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Left, flag);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Up, flag2);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Right, flag3);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Down, flag4);
				return switchProControllerHIDInputState;
			}

			// Token: 0x04000966 RID: 2406
			public const int kSize = 12;

			// Token: 0x04000967 RID: 2407
			public const byte ExpectedReportId = 63;

			// Token: 0x04000968 RID: 2408
			[FieldOffset(0)]
			public byte reportId;

			// Token: 0x04000969 RID: 2409
			[FieldOffset(1)]
			public byte buttons0;

			// Token: 0x0400096A RID: 2410
			[FieldOffset(2)]
			public byte buttons1;

			// Token: 0x0400096B RID: 2411
			[FieldOffset(3)]
			public byte hat;

			// Token: 0x0400096C RID: 2412
			[FieldOffset(4)]
			public ushort leftX;

			// Token: 0x0400096D RID: 2413
			[FieldOffset(6)]
			public ushort leftY;

			// Token: 0x0400096E RID: 2414
			[FieldOffset(8)]
			public ushort rightX;

			// Token: 0x0400096F RID: 2415
			[FieldOffset(10)]
			public ushort rightY;
		}

		// Token: 0x020001D1 RID: 465
		[StructLayout(LayoutKind.Explicit, Size = 25)]
		private struct SwitchFullInputReport
		{
			// Token: 0x0600141B RID: 5147 RVA: 0x0005CCDC File Offset: 0x0005AEDC
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public SwitchProControllerHIDInputState ToHIDInputReport()
			{
				uint num = (uint)((int)this.left0 | ((int)(this.left1 & 15) << 8));
				uint num2 = (uint)(((this.left1 & 240) >> 4) | ((int)this.left2 << 4));
				uint num3 = (uint)((int)this.right0 | ((int)(this.right1 & 15) << 8));
				uint num4 = (uint)(((this.right1 & 240) >> 4) | ((int)this.right2 << 4));
				byte b = (byte)NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits(num, 12U, 8U);
				byte b2 = byte.MaxValue - (byte)NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits(num2, 12U, 8U);
				byte b3 = (byte)NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits(num3, 12U, 8U);
				byte b4 = byte.MaxValue - (byte)NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits(num4, 12U, 8U);
				SwitchProControllerHIDInputState switchProControllerHIDInputState = new SwitchProControllerHIDInputState
				{
					leftStickX = b,
					leftStickY = b2,
					rightStickX = b3,
					rightStickY = b4
				};
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.West, (this.buttons0 & 1) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.North, (this.buttons0 & 2) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.South, (this.buttons0 & 4) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.East, (this.buttons0 & 8) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.R, (this.buttons0 & 64) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.ZR, (this.buttons0 & 128) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Minus, (this.buttons1 & 1) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Plus, (this.buttons1 & 2) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.StickR, (this.buttons1 & 4) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.StickL, (this.buttons1 & 8) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Home, (this.buttons1 & 16) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Capture, (this.buttons1 & 32) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Down, (this.buttons2 & 1) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Up, (this.buttons2 & 2) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Right, (this.buttons2 & 4) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Left, (this.buttons2 & 8) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.L, (this.buttons2 & 64) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.ZL, (this.buttons2 & 128) > 0);
				return switchProControllerHIDInputState;
			}

			// Token: 0x04000970 RID: 2416
			public const int kSize = 25;

			// Token: 0x04000971 RID: 2417
			public const byte ExpectedReportId = 48;

			// Token: 0x04000972 RID: 2418
			[FieldOffset(0)]
			public byte reportId;

			// Token: 0x04000973 RID: 2419
			[FieldOffset(3)]
			public byte buttons0;

			// Token: 0x04000974 RID: 2420
			[FieldOffset(4)]
			public byte buttons1;

			// Token: 0x04000975 RID: 2421
			[FieldOffset(5)]
			public byte buttons2;

			// Token: 0x04000976 RID: 2422
			[FieldOffset(6)]
			public byte left0;

			// Token: 0x04000977 RID: 2423
			[FieldOffset(7)]
			public byte left1;

			// Token: 0x04000978 RID: 2424
			[FieldOffset(8)]
			public byte left2;

			// Token: 0x04000979 RID: 2425
			[FieldOffset(9)]
			public byte right0;

			// Token: 0x0400097A RID: 2426
			[FieldOffset(10)]
			public byte right1;

			// Token: 0x0400097B RID: 2427
			[FieldOffset(11)]
			public byte right2;
		}

		// Token: 0x020001D2 RID: 466
		[StructLayout(LayoutKind.Explicit)]
		private struct SwitchHIDGenericInputReport
		{
			// Token: 0x1700056B RID: 1387
			// (get) Token: 0x0600141C RID: 5148 RVA: 0x0005CF16 File Offset: 0x0005B116
			public static FourCC Format
			{
				get
				{
					return new FourCC('H', 'I', 'D', ' ');
				}
			}

			// Token: 0x0400097C RID: 2428
			[FieldOffset(0)]
			public byte reportId;
		}

		// Token: 0x020001D3 RID: 467
		[StructLayout(LayoutKind.Explicit, Size = 49)]
		internal struct SwitchMagicOutputReport
		{
			// Token: 0x0400097D RID: 2429
			public const int kSize = 49;

			// Token: 0x0400097E RID: 2430
			public const byte ExpectedReplyInputReportId = 129;

			// Token: 0x0400097F RID: 2431
			[FieldOffset(0)]
			public byte reportType;

			// Token: 0x04000980 RID: 2432
			[FieldOffset(1)]
			public byte commandId;

			// Token: 0x0200026A RID: 618
			internal enum ReportType
			{
				// Token: 0x04000C87 RID: 3207
				Magic = 128
			}

			// Token: 0x0200026B RID: 619
			public enum CommandIdType
			{
				// Token: 0x04000C89 RID: 3209
				Status = 1,
				// Token: 0x04000C8A RID: 3210
				Handshake,
				// Token: 0x04000C8B RID: 3211
				Highspeed,
				// Token: 0x04000C8C RID: 3212
				ForceUSB
			}
		}

		// Token: 0x020001D4 RID: 468
		[StructLayout(LayoutKind.Explicit, Size = 57)]
		internal struct SwitchMagicOutputHIDBluetooth : IInputDeviceCommandInfo
		{
			// Token: 0x1700056C RID: 1388
			// (get) Token: 0x0600141D RID: 5149 RVA: 0x0005CF25 File Offset: 0x0005B125
			public static FourCC Type
			{
				get
				{
					return new FourCC('H', 'I', 'D', 'O');
				}
			}

			// Token: 0x1700056D RID: 1389
			// (get) Token: 0x0600141E RID: 5150 RVA: 0x0005CF34 File Offset: 0x0005B134
			public FourCC typeStatic
			{
				get
				{
					return SwitchProControllerHID.SwitchMagicOutputHIDBluetooth.Type;
				}
			}

			// Token: 0x0600141F RID: 5151 RVA: 0x0005CF3C File Offset: 0x0005B13C
			public static SwitchProControllerHID.SwitchMagicOutputHIDBluetooth Create(SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType type)
			{
				return new SwitchProControllerHID.SwitchMagicOutputHIDBluetooth
				{
					baseCommand = new InputDeviceCommand(SwitchProControllerHID.SwitchMagicOutputHIDBluetooth.Type, 57),
					report = new SwitchProControllerHID.SwitchMagicOutputReport
					{
						reportType = 128,
						commandId = (byte)type
					}
				};
			}

			// Token: 0x04000981 RID: 2433
			public const int kSize = 57;

			// Token: 0x04000982 RID: 2434
			[FieldOffset(0)]
			public InputDeviceCommand baseCommand;

			// Token: 0x04000983 RID: 2435
			[FieldOffset(8)]
			public SwitchProControllerHID.SwitchMagicOutputReport report;
		}

		// Token: 0x020001D5 RID: 469
		[StructLayout(LayoutKind.Explicit, Size = 72)]
		internal struct SwitchMagicOutputHIDUSB : IInputDeviceCommandInfo
		{
			// Token: 0x1700056E RID: 1390
			// (get) Token: 0x06001420 RID: 5152 RVA: 0x0005CF8A File Offset: 0x0005B18A
			public static FourCC Type
			{
				get
				{
					return new FourCC('H', 'I', 'D', 'O');
				}
			}

			// Token: 0x1700056F RID: 1391
			// (get) Token: 0x06001421 RID: 5153 RVA: 0x0005CF99 File Offset: 0x0005B199
			public FourCC typeStatic
			{
				get
				{
					return SwitchProControllerHID.SwitchMagicOutputHIDUSB.Type;
				}
			}

			// Token: 0x06001422 RID: 5154 RVA: 0x0005CFA0 File Offset: 0x0005B1A0
			public static SwitchProControllerHID.SwitchMagicOutputHIDUSB Create(SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType type)
			{
				return new SwitchProControllerHID.SwitchMagicOutputHIDUSB
				{
					baseCommand = new InputDeviceCommand(SwitchProControllerHID.SwitchMagicOutputHIDUSB.Type, 72),
					report = new SwitchProControllerHID.SwitchMagicOutputReport
					{
						reportType = 128,
						commandId = (byte)type
					}
				};
			}

			// Token: 0x04000984 RID: 2436
			public const int kSize = 72;

			// Token: 0x04000985 RID: 2437
			[FieldOffset(0)]
			public InputDeviceCommand baseCommand;

			// Token: 0x04000986 RID: 2438
			[FieldOffset(8)]
			public SwitchProControllerHID.SwitchMagicOutputReport report;
		}
	}
}
