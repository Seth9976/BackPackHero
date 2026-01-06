using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000B6 RID: 182
	[StructLayout(LayoutKind.Explicit, Size = 264)]
	public struct QueryKeyboardLayoutCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x00041875 File Offset: 0x0003FA75
		public static FourCC Type
		{
			get
			{
				return new FourCC('K', 'B', 'L', 'T');
			}
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00041884 File Offset: 0x0003FA84
		public unsafe string ReadLayoutName()
		{
			fixed (QueryKeyboardLayoutCommand* ptr = &this)
			{
				return StringHelpers.ReadStringFromBuffer(new IntPtr((void*)(&ptr->nameBuffer.FixedElementField)), 256);
			}
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x000418B0 File Offset: 0x0003FAB0
		public unsafe void WriteLayoutName(string name)
		{
			fixed (QueryKeyboardLayoutCommand* ptr = &this)
			{
				QueryKeyboardLayoutCommand* ptr2 = ptr;
				StringHelpers.WriteStringToBuffer(name, new IntPtr((void*)(&ptr2->nameBuffer.FixedElementField)), 256);
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x000418E2 File Offset: 0x0003FAE2
		public FourCC typeStatic
		{
			get
			{
				return QueryKeyboardLayoutCommand.Type;
			}
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x000418EC File Offset: 0x0003FAEC
		public static QueryKeyboardLayoutCommand Create()
		{
			return new QueryKeyboardLayoutCommand
			{
				baseCommand = new InputDeviceCommand(QueryKeyboardLayoutCommand.Type, 264)
			};
		}

		// Token: 0x040004C0 RID: 1216
		internal const int kMaxNameLength = 256;

		// Token: 0x040004C1 RID: 1217
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x040004C2 RID: 1218
		[FixedBuffer(typeof(byte), 256)]
		[FieldOffset(8)]
		public QueryKeyboardLayoutCommand.<nameBuffer>e__FixedBuffer nameBuffer;

		// Token: 0x020001FB RID: 507
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 256)]
		public struct <nameBuffer>e__FixedBuffer
		{
			// Token: 0x04000B0C RID: 2828
			public byte FixedElementField;
		}
	}
}
