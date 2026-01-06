using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000B8 RID: 184
	[StructLayout(LayoutKind.Explicit, Size = 1040)]
	public struct QueryPairedUserAccountCommand : IInputDeviceCommandInfo
	{
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x00041990 File Offset: 0x0003FB90
		public static FourCC Type
		{
			get
			{
				return new FourCC('P', 'A', 'C', 'C');
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x000419A0 File Offset: 0x0003FBA0
		// (set) Token: 0x06000CAA RID: 3242 RVA: 0x000419CC File Offset: 0x0003FBCC
		public unsafe string id
		{
			get
			{
				fixed (byte* ptr = &this.idBuffer.FixedElementField)
				{
					return StringHelpers.ReadStringFromBuffer(new IntPtr((void*)ptr), 256);
				}
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length > 256)
				{
					throw new ArgumentException(string.Format("ID '{0}' exceeds maximum supported length of {1} characters", value, 256), "value");
				}
				fixed (byte* ptr = &this.idBuffer.FixedElementField)
				{
					byte* ptr2 = ptr;
					StringHelpers.WriteStringToBuffer(value, new IntPtr((void*)ptr2), 256);
				}
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x00041A38 File Offset: 0x0003FC38
		// (set) Token: 0x06000CAC RID: 3244 RVA: 0x00041A64 File Offset: 0x0003FC64
		public unsafe string name
		{
			get
			{
				fixed (byte* ptr = &this.nameBuffer.FixedElementField)
				{
					return StringHelpers.ReadStringFromBuffer(new IntPtr((void*)ptr), 256);
				}
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length > 256)
				{
					throw new ArgumentException(string.Format("Name '{0}' exceeds maximum supported length of {1} characters", value, 256), "value");
				}
				fixed (byte* ptr = &this.nameBuffer.FixedElementField)
				{
					byte* ptr2 = ptr;
					StringHelpers.WriteStringToBuffer(value, new IntPtr((void*)ptr2), 256);
				}
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x00041AD0 File Offset: 0x0003FCD0
		public FourCC typeStatic
		{
			get
			{
				return QueryPairedUserAccountCommand.Type;
			}
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00041AD8 File Offset: 0x0003FCD8
		public static QueryPairedUserAccountCommand Create()
		{
			return new QueryPairedUserAccountCommand
			{
				baseCommand = new InputDeviceCommand(QueryPairedUserAccountCommand.Type, 1040)
			};
		}

		// Token: 0x040004C8 RID: 1224
		internal const int kMaxNameLength = 256;

		// Token: 0x040004C9 RID: 1225
		internal const int kMaxIdLength = 256;

		// Token: 0x040004CA RID: 1226
		internal const int kSize = 1040;

		// Token: 0x040004CB RID: 1227
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x040004CC RID: 1228
		[FieldOffset(8)]
		public ulong handle;

		// Token: 0x040004CD RID: 1229
		[FixedBuffer(typeof(byte), 512)]
		[FieldOffset(16)]
		internal QueryPairedUserAccountCommand.<nameBuffer>e__FixedBuffer nameBuffer;

		// Token: 0x040004CE RID: 1230
		[FixedBuffer(typeof(byte), 512)]
		[FieldOffset(528)]
		internal QueryPairedUserAccountCommand.<idBuffer>e__FixedBuffer idBuffer;

		// Token: 0x020001FD RID: 509
		[Flags]
		public enum Result : long
		{
			// Token: 0x04000B0F RID: 2831
			DevicePairedToUserAccount = 2L,
			// Token: 0x04000B10 RID: 2832
			UserAccountSelectionInProgress = 4L,
			// Token: 0x04000B11 RID: 2833
			UserAccountSelectionComplete = 8L,
			// Token: 0x04000B12 RID: 2834
			UserAccountSelectionCanceled = 16L
		}

		// Token: 0x020001FE RID: 510
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 512)]
		public struct <nameBuffer>e__FixedBuffer
		{
			// Token: 0x04000B13 RID: 2835
			public byte FixedElementField;
		}

		// Token: 0x020001FF RID: 511
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
