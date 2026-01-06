using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000BA RID: 186
	[StructLayout(LayoutKind.Explicit, Size = 520)]
	internal struct QueryUserIdCommand : IInputDeviceCommandInfo
	{
		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x00041AFD File Offset: 0x0003FCFD
		public static FourCC Type
		{
			get
			{
				return new FourCC('U', 'S', 'E', 'R');
			}
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00041B0C File Offset: 0x0003FD0C
		public unsafe string ReadId()
		{
			fixed (QueryUserIdCommand* ptr = &this)
			{
				return StringHelpers.ReadStringFromBuffer(new IntPtr((void*)(&ptr->idBuffer.FixedElementField)), 256);
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x00041B37 File Offset: 0x0003FD37
		public FourCC typeStatic
		{
			get
			{
				return QueryUserIdCommand.Type;
			}
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00041B40 File Offset: 0x0003FD40
		public static QueryUserIdCommand Create()
		{
			return new QueryUserIdCommand
			{
				baseCommand = new InputDeviceCommand(QueryUserIdCommand.Type, 520)
			};
		}

		// Token: 0x040004D2 RID: 1234
		public const int kMaxIdLength = 256;

		// Token: 0x040004D3 RID: 1235
		internal const int kSize = 520;

		// Token: 0x040004D4 RID: 1236
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x040004D5 RID: 1237
		[FixedBuffer(typeof(byte), 512)]
		[FieldOffset(8)]
		public QueryUserIdCommand.<idBuffer>e__FixedBuffer idBuffer;

		// Token: 0x02000200 RID: 512
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 512)]
		public struct <idBuffer>e__FixedBuffer
		{
			// Token: 0x04000B14 RID: 2836
			public byte FixedElementField;
		}
	}
}
