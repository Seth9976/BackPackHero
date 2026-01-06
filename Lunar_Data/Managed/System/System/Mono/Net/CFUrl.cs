using System;
using System.Runtime.InteropServices;

namespace Mono.Net
{
	// Token: 0x0200007D RID: 125
	internal class CFUrl : CFObject
	{
		// Token: 0x060001D4 RID: 468 RVA: 0x00002474 File Offset: 0x00000674
		public CFUrl(IntPtr handle, bool own)
			: base(handle, own)
		{
		}

		// Token: 0x060001D5 RID: 469
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFURLCreateWithString(IntPtr allocator, IntPtr str, IntPtr baseURL);

		// Token: 0x060001D6 RID: 470 RVA: 0x00005574 File Offset: 0x00003774
		public static CFUrl Create(string absolute)
		{
			if (string.IsNullOrEmpty(absolute))
			{
				return null;
			}
			CFString cfstring = CFString.Create(absolute);
			IntPtr intPtr = CFUrl.CFURLCreateWithString(IntPtr.Zero, cfstring.Handle, IntPtr.Zero);
			cfstring.Dispose();
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new CFUrl(intPtr, true);
		}
	}
}
