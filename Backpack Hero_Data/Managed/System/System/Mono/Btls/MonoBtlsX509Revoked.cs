using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Mono.Btls
{
	// Token: 0x02000106 RID: 262
	internal class MonoBtlsX509Revoked : MonoBtlsObject
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x00010F46 File Offset: 0x0000F146
		internal new MonoBtlsX509Revoked.BoringX509RevokedHandle Handle
		{
			get
			{
				return (MonoBtlsX509Revoked.BoringX509RevokedHandle)base.Handle;
			}
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0000C9EE File Offset: 0x0000ABEE
		internal MonoBtlsX509Revoked(MonoBtlsX509Revoked.BoringX509RevokedHandle handle)
			: base(handle)
		{
		}

		// Token: 0x06000603 RID: 1539
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_revoked_get_serial_number(IntPtr handle, IntPtr data, int size);

		// Token: 0x06000604 RID: 1540
		[DllImport("libmono-btls-shared")]
		private static extern long mono_btls_x509_revoked_get_revocation_date(IntPtr handle);

		// Token: 0x06000605 RID: 1541
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_revoked_get_reason(IntPtr handle);

		// Token: 0x06000606 RID: 1542
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_revoked_get_sequence(IntPtr handle);

		// Token: 0x06000607 RID: 1543
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_x509_revoked_free(IntPtr handle);

		// Token: 0x06000608 RID: 1544 RVA: 0x00010F54 File Offset: 0x0000F154
		public byte[] GetSerialNumber()
		{
			int num = 256;
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			byte[] array2;
			try
			{
				int num2 = MonoBtlsX509Revoked.mono_btls_x509_revoked_get_serial_number(this.Handle.DangerousGetHandle(), intPtr, num);
				base.CheckError(num2 > 0, "GetSerialNumber");
				byte[] array = new byte[num2];
				Marshal.Copy(intPtr, array, 0, num2);
				array2 = array;
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return array2;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00010FCC File Offset: 0x0000F1CC
		public DateTime GetRevocationDate()
		{
			long num = MonoBtlsX509Revoked.mono_btls_x509_revoked_get_revocation_date(this.Handle.DangerousGetHandle());
			return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds((double)num);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00011004 File Offset: 0x0000F204
		public int GetReason()
		{
			return MonoBtlsX509Revoked.mono_btls_x509_revoked_get_reason(this.Handle.DangerousGetHandle());
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00011016 File Offset: 0x0000F216
		public int GetSequence()
		{
			return MonoBtlsX509Revoked.mono_btls_x509_revoked_get_sequence(this.Handle.DangerousGetHandle());
		}

		// Token: 0x02000107 RID: 263
		internal class BoringX509RevokedHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x0600060C RID: 1548 RVA: 0x0000CCA4 File Offset: 0x0000AEA4
			public BoringX509RevokedHandle(IntPtr handle)
				: base(handle, true)
			{
			}

			// Token: 0x0600060D RID: 1549 RVA: 0x00011028 File Offset: 0x0000F228
			protected override bool ReleaseHandle()
			{
				if (this.handle != IntPtr.Zero)
				{
					MonoBtlsX509Revoked.mono_btls_x509_revoked_free(this.handle);
				}
				return true;
			}

			// Token: 0x0600060E RID: 1550 RVA: 0x000101A1 File Offset: 0x0000E3A1
			public IntPtr StealHandle()
			{
				return Interlocked.Exchange(ref this.handle, IntPtr.Zero);
			}
		}
	}
}
