using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000369 RID: 873
	[StructLayout(LayoutKind.Sequential)]
	internal class SecPkgContext_StreamSizes
	{
		// Token: 0x06001CF1 RID: 7409 RVA: 0x00068C24 File Offset: 0x00066E24
		internal unsafe SecPkgContext_StreamSizes(byte[] memory)
		{
			fixed (byte[] array = memory)
			{
				void* ptr;
				if (memory == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = (void*)(&array[0]);
				}
				IntPtr intPtr = new IntPtr(ptr);
				checked
				{
					try
					{
						this.cbHeader = (int)((uint)Marshal.ReadInt32(intPtr));
						this.cbTrailer = (int)((uint)Marshal.ReadInt32(intPtr, 4));
						this.cbMaximumMessage = (int)((uint)Marshal.ReadInt32(intPtr, 8));
						this.cBuffers = (int)((uint)Marshal.ReadInt32(intPtr, 12));
						this.cbBlockSize = (int)((uint)Marshal.ReadInt32(intPtr, 16));
					}
					catch (OverflowException)
					{
						NetEventSource.Fail(this, "Negative size.", ".ctor");
						throw;
					}
				}
			}
		}

		// Token: 0x04000EA7 RID: 3751
		public int cbHeader;

		// Token: 0x04000EA8 RID: 3752
		public int cbTrailer;

		// Token: 0x04000EA9 RID: 3753
		public int cbMaximumMessage;

		// Token: 0x04000EAA RID: 3754
		public int cBuffers;

		// Token: 0x04000EAB RID: 3755
		public int cbBlockSize;

		// Token: 0x04000EAC RID: 3756
		public static readonly int SizeOf = Marshal.SizeOf<SecPkgContext_StreamSizes>();
	}
}
