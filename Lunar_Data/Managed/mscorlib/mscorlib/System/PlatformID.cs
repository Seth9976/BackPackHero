using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Identifies the operating system, or platform, supported by an assembly.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000248 RID: 584
	[ComVisible(true)]
	[Serializable]
	public enum PlatformID
	{
		/// <summary>The operating system is Win32s. Win32s is a layer that runs on 16-bit versions of Windows to provide access to 32-bit applications.</summary>
		// Token: 0x04001770 RID: 6000
		Win32S,
		/// <summary>The operating system is Windows 95 or Windows 98.</summary>
		// Token: 0x04001771 RID: 6001
		Win32Windows,
		/// <summary>The operating system is Windows NT or later.</summary>
		// Token: 0x04001772 RID: 6002
		Win32NT,
		/// <summary>The operating system is Windows CE.</summary>
		// Token: 0x04001773 RID: 6003
		WinCE,
		/// <summary>The operating system is Unix.</summary>
		// Token: 0x04001774 RID: 6004
		Unix,
		/// <summary>The development platform is Xbox 360.</summary>
		// Token: 0x04001775 RID: 6005
		Xbox,
		/// <summary>The operating system is Macintosh.</summary>
		// Token: 0x04001776 RID: 6006
		MacOSX
	}
}
