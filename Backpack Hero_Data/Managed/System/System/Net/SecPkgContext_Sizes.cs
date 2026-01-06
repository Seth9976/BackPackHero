using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000368 RID: 872
	[StructLayout(LayoutKind.Sequential)]
	internal class SecPkgContext_Sizes
	{
		// Token: 0x06001CEF RID: 7407 RVA: 0x00068B84 File Offset: 0x00066D84
		internal unsafe SecPkgContext_Sizes(byte[] memory)
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
						this.cbMaxToken = (int)((uint)Marshal.ReadInt32(intPtr));
						this.cbMaxSignature = (int)((uint)Marshal.ReadInt32(intPtr, 4));
						this.cbBlockSize = (int)((uint)Marshal.ReadInt32(intPtr, 8));
						this.cbSecurityTrailer = (int)((uint)Marshal.ReadInt32(intPtr, 12));
					}
					catch (OverflowException)
					{
						NetEventSource.Fail(this, "Negative size.", ".ctor");
						throw;
					}
				}
			}
		}

		// Token: 0x04000EA2 RID: 3746
		public readonly int cbMaxToken;

		// Token: 0x04000EA3 RID: 3747
		public readonly int cbMaxSignature;

		// Token: 0x04000EA4 RID: 3748
		public readonly int cbBlockSize;

		// Token: 0x04000EA5 RID: 3749
		public readonly int cbSecurityTrailer;

		// Token: 0x04000EA6 RID: 3750
		public static readonly int SizeOf = Marshal.SizeOf<SecPkgContext_Sizes>();
	}
}
