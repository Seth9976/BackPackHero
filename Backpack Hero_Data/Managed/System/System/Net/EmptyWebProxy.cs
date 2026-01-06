using System;

namespace System.Net
{
	// Token: 0x02000458 RID: 1112
	[Serializable]
	internal sealed class EmptyWebProxy : IAutoWebProxy, IWebProxy
	{
		// Token: 0x060022F6 RID: 8950 RVA: 0x00003914 File Offset: 0x00001B14
		public Uri GetProxy(Uri uri)
		{
			return uri;
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x0000390E File Offset: 0x00001B0E
		public bool IsBypassed(Uri uri)
		{
			return true;
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x060022F8 RID: 8952 RVA: 0x000802D4 File Offset: 0x0007E4D4
		// (set) Token: 0x060022F9 RID: 8953 RVA: 0x000802DC File Offset: 0x0007E4DC
		public ICredentials Credentials
		{
			get
			{
				return this.m_credentials;
			}
			set
			{
				this.m_credentials = value;
			}
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x000802E5 File Offset: 0x0007E4E5
		ProxyChain IAutoWebProxy.GetProxies(Uri destination)
		{
			return new DirectProxy(destination);
		}

		// Token: 0x04001459 RID: 5209
		[NonSerialized]
		private ICredentials m_credentials;
	}
}
