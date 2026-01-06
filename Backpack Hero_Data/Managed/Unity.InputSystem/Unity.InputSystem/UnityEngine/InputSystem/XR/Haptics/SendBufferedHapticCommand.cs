using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.XR.Haptics
{
	// Token: 0x02000076 RID: 118
	[StructLayout(LayoutKind.Explicit, Size = 1040)]
	public struct SendBufferedHapticCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x000368D9 File Offset: 0x00034AD9
		private static FourCC Type
		{
			get
			{
				return new FourCC('X', 'H', 'U', '0');
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x000368E8 File Offset: 0x00034AE8
		public FourCC typeStatic
		{
			get
			{
				return SendBufferedHapticCommand.Type;
			}
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x000368F0 File Offset: 0x00034AF0
		public unsafe static SendBufferedHapticCommand Create(byte[] rumbleBuffer)
		{
			if (rumbleBuffer == null)
			{
				throw new ArgumentNullException("rumbleBuffer");
			}
			int num = Mathf.Min(1024, rumbleBuffer.Length);
			SendBufferedHapticCommand sendBufferedHapticCommand = new SendBufferedHapticCommand
			{
				baseCommand = new InputDeviceCommand(SendBufferedHapticCommand.Type, 1040),
				bufferSize = num
			};
			SendBufferedHapticCommand* ptr = &sendBufferedHapticCommand;
			fixed (byte[] array = rumbleBuffer)
			{
				byte* ptr2;
				if (rumbleBuffer == null || array.Length == 0)
				{
					ptr2 = null;
				}
				else
				{
					ptr2 = &array[0];
				}
				for (int i = 0; i < num; i++)
				{
					*((ref ptr->buffer.FixedElementField) + i) = ptr2[i];
				}
			}
			return sendBufferedHapticCommand;
		}

		// Token: 0x04000368 RID: 872
		private const int kMaxHapticBufferSize = 1024;

		// Token: 0x04000369 RID: 873
		private const int kSize = 1040;

		// Token: 0x0400036A RID: 874
		[FieldOffset(0)]
		private InputDeviceCommand baseCommand;

		// Token: 0x0400036B RID: 875
		[FieldOffset(8)]
		private int channel;

		// Token: 0x0400036C RID: 876
		[FieldOffset(12)]
		private int bufferSize;

		// Token: 0x0400036D RID: 877
		[FixedBuffer(typeof(byte), 1024)]
		[FieldOffset(16)]
		private SendBufferedHapticCommand.<buffer>e__FixedBuffer buffer;

		// Token: 0x020001BC RID: 444
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 1024)]
		public struct <buffer>e__FixedBuffer
		{
			// Token: 0x040008F3 RID: 2291
			public byte FixedElementField;
		}
	}
}
