using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Net
{
	// Token: 0x02000444 RID: 1092
	internal abstract class ProxyChain : IEnumerable<Uri>, IEnumerable, IDisposable
	{
		// Token: 0x06002295 RID: 8853 RVA: 0x0007EC74 File Offset: 0x0007CE74
		protected ProxyChain(Uri destination)
		{
			this.m_Destination = destination;
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x0007EC90 File Offset: 0x0007CE90
		public IEnumerator<Uri> GetEnumerator()
		{
			ProxyChain.ProxyEnumerator proxyEnumerator = new ProxyChain.ProxyEnumerator(this);
			if (this.m_MainEnumerator == null)
			{
				this.m_MainEnumerator = proxyEnumerator;
			}
			return proxyEnumerator;
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x0007ECB4 File Offset: 0x0007CEB4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x00003917 File Offset: 0x00001B17
		public virtual void Dispose()
		{
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06002299 RID: 8857 RVA: 0x0007ECBC File Offset: 0x0007CEBC
		internal IEnumerator<Uri> Enumerator
		{
			get
			{
				if (this.m_MainEnumerator != null)
				{
					return this.m_MainEnumerator;
				}
				return this.GetEnumerator();
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x0600229A RID: 8858 RVA: 0x0007ECE0 File Offset: 0x0007CEE0
		internal Uri Destination
		{
			get
			{
				return this.m_Destination;
			}
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void Abort()
		{
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x0007ECE8 File Offset: 0x0007CEE8
		internal bool HttpAbort(HttpWebRequest request, WebException webException)
		{
			this.Abort();
			return true;
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x0600229D RID: 8861 RVA: 0x0007ECF1 File Offset: 0x0007CEF1
		internal HttpAbortDelegate HttpAbortDelegate
		{
			get
			{
				if (this.m_HttpAbortDelegate == null)
				{
					this.m_HttpAbortDelegate = new HttpAbortDelegate(this.HttpAbort);
				}
				return this.m_HttpAbortDelegate;
			}
		}

		// Token: 0x0600229E RID: 8862
		protected abstract bool GetNextProxy(out Uri proxy);

		// Token: 0x0400141B RID: 5147
		private List<Uri> m_Cache = new List<Uri>();

		// Token: 0x0400141C RID: 5148
		private bool m_CacheComplete;

		// Token: 0x0400141D RID: 5149
		private ProxyChain.ProxyEnumerator m_MainEnumerator;

		// Token: 0x0400141E RID: 5150
		private Uri m_Destination;

		// Token: 0x0400141F RID: 5151
		private HttpAbortDelegate m_HttpAbortDelegate;

		// Token: 0x02000445 RID: 1093
		private class ProxyEnumerator : IEnumerator<Uri>, IDisposable, IEnumerator
		{
			// Token: 0x0600229F RID: 8863 RVA: 0x0007ED13 File Offset: 0x0007CF13
			internal ProxyEnumerator(ProxyChain chain)
			{
				this.m_Chain = chain;
			}

			// Token: 0x170006DB RID: 1755
			// (get) Token: 0x060022A0 RID: 8864 RVA: 0x0007ED29 File Offset: 0x0007CF29
			public Uri Current
			{
				get
				{
					if (this.m_Finished || this.m_CurrentIndex < 0)
					{
						throw new InvalidOperationException(SR.GetString("Enumeration has either not started or has already finished."));
					}
					return this.m_Chain.m_Cache[this.m_CurrentIndex];
				}
			}

			// Token: 0x170006DC RID: 1756
			// (get) Token: 0x060022A1 RID: 8865 RVA: 0x0007ED62 File Offset: 0x0007CF62
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060022A2 RID: 8866 RVA: 0x0007ED6C File Offset: 0x0007CF6C
			public bool MoveNext()
			{
				if (this.m_Finished)
				{
					return false;
				}
				checked
				{
					this.m_CurrentIndex++;
					if (this.m_Chain.m_Cache.Count > this.m_CurrentIndex)
					{
						return true;
					}
					if (this.m_Chain.m_CacheComplete)
					{
						this.m_Finished = true;
						return false;
					}
					List<Uri> cache = this.m_Chain.m_Cache;
					bool flag2;
					lock (cache)
					{
						if (this.m_Chain.m_Cache.Count > this.m_CurrentIndex)
						{
							flag2 = true;
						}
						else if (this.m_Chain.m_CacheComplete)
						{
							this.m_Finished = true;
							flag2 = false;
						}
						else
						{
							Uri uri;
							while (this.m_Chain.GetNextProxy(out uri))
							{
								if (uri == null)
								{
									if (this.m_TriedDirect)
									{
										continue;
									}
									this.m_TriedDirect = true;
								}
								this.m_Chain.m_Cache.Add(uri);
								return true;
							}
							this.m_Finished = true;
							this.m_Chain.m_CacheComplete = true;
							flag2 = false;
						}
					}
					return flag2;
				}
			}

			// Token: 0x060022A3 RID: 8867 RVA: 0x0007EE7C File Offset: 0x0007D07C
			public void Reset()
			{
				this.m_Finished = false;
				this.m_CurrentIndex = -1;
			}

			// Token: 0x060022A4 RID: 8868 RVA: 0x00003917 File Offset: 0x00001B17
			public void Dispose()
			{
			}

			// Token: 0x04001420 RID: 5152
			private ProxyChain m_Chain;

			// Token: 0x04001421 RID: 5153
			private bool m_Finished;

			// Token: 0x04001422 RID: 5154
			private int m_CurrentIndex = -1;

			// Token: 0x04001423 RID: 5155
			private bool m_TriedDirect;
		}
	}
}
