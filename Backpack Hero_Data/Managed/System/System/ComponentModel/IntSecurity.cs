using System;
using System.IO;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200072C RID: 1836
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal static class IntSecurity
	{
		// Token: 0x06003A4E RID: 14926 RVA: 0x000CA711 File Offset: 0x000C8911
		public static string UnsafeGetFullPath(string fileName)
		{
			return Path.GetFullPath(fileName);
		}
	}
}
