using System;

namespace Steamworks
{
	// Token: 0x0200017C RID: 380
	[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
	internal class CallbackIdentityAttribute : Attribute
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x0000CC6F File Offset: 0x0000AE6F
		// (set) Token: 0x060008AF RID: 2223 RVA: 0x0000CC77 File Offset: 0x0000AE77
		public int Identity { get; set; }

		// Token: 0x060008B0 RID: 2224 RVA: 0x0000CC80 File Offset: 0x0000AE80
		public CallbackIdentityAttribute(int callbackNum)
		{
			this.Identity = callbackNum;
		}
	}
}
