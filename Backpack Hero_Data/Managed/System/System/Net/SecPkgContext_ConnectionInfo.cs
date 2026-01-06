using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x0200035E RID: 862
	[StructLayout(LayoutKind.Sequential)]
	internal class SecPkgContext_ConnectionInfo
	{
		// Token: 0x06001C93 RID: 7315 RVA: 0x00067674 File Offset: 0x00065874
		internal unsafe SecPkgContext_ConnectionInfo(byte[] nativeBuffer)
		{
			fixed (byte[] array = nativeBuffer)
			{
				void* ptr;
				if (nativeBuffer == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = (void*)(&array[0]);
				}
				try
				{
					IntPtr intPtr = new IntPtr(ptr);
					this.Protocol = Marshal.ReadInt32(intPtr);
					this.DataCipherAlg = Marshal.ReadInt32(intPtr, 4);
					this.DataKeySize = Marshal.ReadInt32(intPtr, 8);
					this.DataHashAlg = Marshal.ReadInt32(intPtr, 12);
					this.DataHashKeySize = Marshal.ReadInt32(intPtr, 16);
					this.KeyExchangeAlg = Marshal.ReadInt32(intPtr, 20);
					this.KeyExchKeySize = Marshal.ReadInt32(intPtr, 24);
				}
				catch (OverflowException)
				{
					NetEventSource.Fail(this, "Negative size", ".ctor");
					throw;
				}
			}
		}

		// Token: 0x04000E89 RID: 3721
		public readonly int Protocol;

		// Token: 0x04000E8A RID: 3722
		public readonly int DataCipherAlg;

		// Token: 0x04000E8B RID: 3723
		public readonly int DataKeySize;

		// Token: 0x04000E8C RID: 3724
		public readonly int DataHashAlg;

		// Token: 0x04000E8D RID: 3725
		public readonly int DataHashKeySize;

		// Token: 0x04000E8E RID: 3726
		public readonly int KeyExchangeAlg;

		// Token: 0x04000E8F RID: 3727
		public readonly int KeyExchKeySize;
	}
}
