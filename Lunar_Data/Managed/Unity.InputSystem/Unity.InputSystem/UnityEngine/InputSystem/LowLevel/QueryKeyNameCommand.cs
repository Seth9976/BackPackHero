using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000B7 RID: 183
	[StructLayout(LayoutKind.Explicit, Size = 268)]
	public struct QueryKeyNameCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x000418D0 File Offset: 0x0003FAD0
		public static FourCC Type
		{
			get
			{
				return new FourCC('K', 'Y', 'C', 'F');
			}
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x000418E0 File Offset: 0x0003FAE0
		public unsafe string ReadKeyName()
		{
			fixed (QueryKeyNameCommand* ptr = &this)
			{
				return StringHelpers.ReadStringFromBuffer(new IntPtr((void*)(&ptr->nameBuffer.FixedElementField)), 256);
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x0004190B File Offset: 0x0003FB0B
		public FourCC typeStatic
		{
			get
			{
				return QueryKeyNameCommand.Type;
			}
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00041914 File Offset: 0x0003FB14
		public static QueryKeyNameCommand Create(Key key)
		{
			return new QueryKeyNameCommand
			{
				baseCommand = new InputDeviceCommand(QueryKeyNameCommand.Type, 268),
				scanOrKeyCode = (int)key
			};
		}

		// Token: 0x040004C3 RID: 1219
		internal const int kMaxNameLength = 256;

		// Token: 0x040004C4 RID: 1220
		internal const int kSize = 268;

		// Token: 0x040004C5 RID: 1221
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x040004C6 RID: 1222
		[FieldOffset(8)]
		public int scanOrKeyCode;

		// Token: 0x040004C7 RID: 1223
		[FixedBuffer(typeof(byte), 256)]
		[FieldOffset(12)]
		public QueryKeyNameCommand.<nameBuffer>e__FixedBuffer nameBuffer;

		// Token: 0x020001FC RID: 508
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
