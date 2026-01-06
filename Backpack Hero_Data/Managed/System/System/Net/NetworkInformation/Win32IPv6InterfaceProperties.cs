using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000542 RID: 1346
	internal class Win32IPv6InterfaceProperties : IPv6InterfaceProperties
	{
		// Token: 0x06002B37 RID: 11063 RVA: 0x0009C95F File Offset: 0x0009AB5F
		public Win32IPv6InterfaceProperties(Win32_MIB_IFROW mib)
		{
			this.mib = mib;
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06002B38 RID: 11064 RVA: 0x0009C96E File Offset: 0x0009AB6E
		public override int Index
		{
			get
			{
				return this.mib.Index;
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06002B39 RID: 11065 RVA: 0x0009C97B File Offset: 0x0009AB7B
		public override int Mtu
		{
			get
			{
				return this.mib.Mtu;
			}
		}

		// Token: 0x0400194D RID: 6477
		private Win32_MIB_IFROW mib;
	}
}
