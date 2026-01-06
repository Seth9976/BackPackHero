using System;
using System.Runtime.InteropServices;

namespace Mono.Btls
{
	// Token: 0x020000CF RID: 207
	internal class MonoBtlsBioMemory : MonoBtlsBio
	{
		// Token: 0x06000417 RID: 1047
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_bio_mem_new();

		// Token: 0x06000418 RID: 1048
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_bio_mem_get_data(IntPtr handle, out IntPtr data);

		// Token: 0x06000419 RID: 1049 RVA: 0x0000CCD9 File Offset: 0x0000AED9
		public MonoBtlsBioMemory()
			: base(new MonoBtlsBio.BoringBioHandle(MonoBtlsBioMemory.mono_btls_bio_mem_new()))
		{
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000CCEC File Offset: 0x0000AEEC
		public byte[] GetData()
		{
			bool flag = false;
			byte[] array2;
			try
			{
				base.Handle.DangerousAddRef(ref flag);
				IntPtr intPtr;
				int num = MonoBtlsBioMemory.mono_btls_bio_mem_get_data(base.Handle.DangerousGetHandle(), out intPtr);
				base.CheckError(num > 0, "GetData");
				byte[] array = new byte[num];
				Marshal.Copy(intPtr, array, 0, num);
				array2 = array;
			}
			finally
			{
				if (flag)
				{
					base.Handle.DangerousRelease();
				}
			}
			return array2;
		}
	}
}
