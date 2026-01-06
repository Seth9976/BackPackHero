using System;

namespace System.Net
{
	// Token: 0x02000448 RID: 1096
	internal class StaticProxy : ProxyChain
	{
		// Token: 0x060022AA RID: 8874 RVA: 0x0007EF5E File Offset: 0x0007D15E
		internal StaticProxy(Uri destination, Uri proxy)
			: base(destination)
		{
			if (proxy == null)
			{
				throw new ArgumentNullException("proxy");
			}
			this.m_Proxy = proxy;
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x0007EF82 File Offset: 0x0007D182
		protected override bool GetNextProxy(out Uri proxy)
		{
			proxy = this.m_Proxy;
			if (proxy == null)
			{
				return false;
			}
			this.m_Proxy = null;
			return true;
		}

		// Token: 0x04001429 RID: 5161
		private Uri m_Proxy;
	}
}
