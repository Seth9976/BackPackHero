using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.DualShock.LowLevel
{
	// Token: 0x020000A7 RID: 167
	[StructLayout(LayoutKind.Explicit, Size = 40)]
	internal struct DualShockHIDOutputReport : IInputDeviceCommandInfo
	{
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x000413AF File Offset: 0x0003F5AF
		public static FourCC Type
		{
			get
			{
				return new FourCC('H', 'I', 'D', 'O');
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x000413BE File Offset: 0x0003F5BE
		public FourCC typeStatic
		{
			get
			{
				return DualShockHIDOutputReport.Type;
			}
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x000413C8 File Offset: 0x0003F5C8
		public void SetMotorSpeeds(float lowFreq, float highFreq)
		{
			this.flags |= 1;
			this.lowFrequencyMotorSpeed = (byte)Mathf.Clamp(lowFreq * 255f, 0f, 255f);
			this.highFrequencyMotorSpeed = (byte)Mathf.Clamp(highFreq * 255f, 0f, 255f);
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x00041420 File Offset: 0x0003F620
		public void SetColor(Color color)
		{
			this.flags |= 2;
			this.redColor = (byte)Mathf.Clamp(color.r * 255f, 0f, 255f);
			this.greenColor = (byte)Mathf.Clamp(color.g * 255f, 0f, 255f);
			this.blueColor = (byte)Mathf.Clamp(color.b * 255f, 0f, 255f);
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x000414A4 File Offset: 0x0003F6A4
		public static DualShockHIDOutputReport Create()
		{
			return new DualShockHIDOutputReport
			{
				baseCommand = new InputDeviceCommand(DualShockHIDOutputReport.Type, 40),
				reportId = 5
			};
		}

		// Token: 0x0400049A RID: 1178
		internal const int kSize = 40;

		// Token: 0x0400049B RID: 1179
		internal const int kReportId = 5;

		// Token: 0x0400049C RID: 1180
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x0400049D RID: 1181
		[FieldOffset(8)]
		public byte reportId;

		// Token: 0x0400049E RID: 1182
		[FieldOffset(9)]
		public byte flags;

		// Token: 0x0400049F RID: 1183
		[FixedBuffer(typeof(byte), 2)]
		[FieldOffset(10)]
		public DualShockHIDOutputReport.<unknown1>e__FixedBuffer unknown1;

		// Token: 0x040004A0 RID: 1184
		[FieldOffset(12)]
		public byte highFrequencyMotorSpeed;

		// Token: 0x040004A1 RID: 1185
		[FieldOffset(13)]
		public byte lowFrequencyMotorSpeed;

		// Token: 0x040004A2 RID: 1186
		[FieldOffset(14)]
		public byte redColor;

		// Token: 0x040004A3 RID: 1187
		[FieldOffset(15)]
		public byte greenColor;

		// Token: 0x040004A4 RID: 1188
		[FieldOffset(16)]
		public byte blueColor;

		// Token: 0x040004A5 RID: 1189
		[FixedBuffer(typeof(byte), 23)]
		[FieldOffset(17)]
		public DualShockHIDOutputReport.<unknown2>e__FixedBuffer unknown2;

		// Token: 0x020001F7 RID: 503
		[Flags]
		public enum Flags
		{
			// Token: 0x04000B04 RID: 2820
			Rumble = 1,
			// Token: 0x04000B05 RID: 2821
			Color = 2
		}

		// Token: 0x020001F8 RID: 504
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 2)]
		public struct <unknown1>e__FixedBuffer
		{
			// Token: 0x04000B06 RID: 2822
			public byte FixedElementField;
		}

		// Token: 0x020001F9 RID: 505
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 23)]
		public struct <unknown2>e__FixedBuffer
		{
			// Token: 0x04000B07 RID: 2823
			public byte FixedElementField;
		}
	}
}
