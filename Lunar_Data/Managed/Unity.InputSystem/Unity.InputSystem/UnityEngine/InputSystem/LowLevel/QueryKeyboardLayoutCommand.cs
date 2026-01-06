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
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x0004182D File Offset: 0x0003FA2D
		public static FourCC Type
		{
			get
			{
				return new FourCC('K', 'B', 'L', 'T');
			}
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x0004183C File Offset: 0x0003FA3C
		public unsafe string ReadLayoutName()
		{
			fixed (QueryKeyboardLayoutCommand* ptr = &this)
			{
				return StringHelpers.ReadStringFromBuffer(new IntPtr((void*)(&ptr->nameBuffer.FixedElementField)), 256);
			}
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00041868 File Offset: 0x0003FA68
		public unsafe void WriteLayoutName(string name)
		{
			fixed (QueryKeyboardLayoutCommand* ptr = &this)
			{
				QueryKeyboardLayoutCommand* ptr2 = ptr;
				StringHelpers.WriteStringToBuffer(name, new IntPtr((void*)(&ptr2->nameBuffer.FixedElementField)), 256);
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x0004189A File Offset: 0x0003FA9A
		public FourCC typeStatic
		{
			get
			{
				return QueryKeyboardLayoutCommand.Type;
			}
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x000418A4 File Offset: 0x0003FAA4
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
			// Token: 0x04000B0B RID: 2827
			public byte FixedElementField;
		}
	}
}
