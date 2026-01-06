using System;

namespace System.Net
{
	// Token: 0x02000447 RID: 1095
	internal class DirectProxy : ProxyChain
	{
		// Token: 0x060022A8 RID: 8872 RVA: 0x0007EF3E File Offset: 0x0007D13E
		internal DirectProxy(Uri destination)
			: base(destination)
		{
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x0007EF47 File Offset: 0x0007D147
		protected override bool GetNextProxy(out Uri proxy)
		{
			proxy = null;
			if (this.m_ProxyRetrieved)
			{
				return false;
			}
			this.m_ProxyRetrieved = true;
			return true;
		}

		// Token: 0x04001428 RID: 5160
		private bool m_ProxyRetrieved;
	}
}
