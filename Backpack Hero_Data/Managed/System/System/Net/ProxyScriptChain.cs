using System;

namespace System.Net
{
	// Token: 0x02000446 RID: 1094
	internal class ProxyScriptChain : ProxyChain
	{
		// Token: 0x060022A5 RID: 8869 RVA: 0x0007EE8C File Offset: 0x0007D08C
		internal ProxyScriptChain(WebProxy proxy, Uri destination)
			: base(destination)
		{
			this.m_Proxy = proxy;
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x0007EE9C File Offset: 0x0007D09C
		protected override bool GetNextProxy(out Uri proxy)
		{
			if (this.m_CurrentIndex < 0)
			{
				proxy = null;
				return false;
			}
			if (this.m_CurrentIndex == 0)
			{
				this.m_ScriptProxies = this.m_Proxy.GetProxiesAuto(base.Destination, ref this.m_SyncStatus);
			}
			if (this.m_ScriptProxies == null || this.m_CurrentIndex >= this.m_ScriptProxies.Length)
			{
				proxy = this.m_Proxy.GetProxyAutoFailover(base.Destination);
				this.m_CurrentIndex = -1;
				return true;
			}
			Uri[] scriptProxies = this.m_ScriptProxies;
			int currentIndex = this.m_CurrentIndex;
			this.m_CurrentIndex = currentIndex + 1;
			proxy = scriptProxies[currentIndex];
			return true;
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x0007EF2B File Offset: 0x0007D12B
		internal override void Abort()
		{
			this.m_Proxy.AbortGetProxiesAuto(ref this.m_SyncStatus);
		}

		// Token: 0x04001424 RID: 5156
		private WebProxy m_Proxy;

		// Token: 0x04001425 RID: 5157
		private Uri[] m_ScriptProxies;

		// Token: 0x04001426 RID: 5158
		private int m_CurrentIndex;

		// Token: 0x04001427 RID: 5159
		private int m_SyncStatus;
	}
}
