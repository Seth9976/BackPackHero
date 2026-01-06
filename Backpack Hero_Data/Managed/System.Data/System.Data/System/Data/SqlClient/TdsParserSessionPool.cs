using System;
using System.Collections.Generic;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x0200021E RID: 542
	internal class TdsParserSessionPool
	{
		// Token: 0x06001905 RID: 6405 RVA: 0x0007E014 File Offset: 0x0007C214
		internal TdsParserSessionPool(TdsParser parser)
		{
			this._parser = parser;
			this._cache = new List<TdsParserStateObject>();
			this._freeStateObjects = new TdsParserStateObject[10];
			this._freeStateObjectCount = 0;
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001906 RID: 6406 RVA: 0x0007E042 File Offset: 0x0007C242
		private bool IsDisposed
		{
			get
			{
				return this._freeStateObjects == null;
			}
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x0007E050 File Offset: 0x0007C250
		internal void Deactivate()
		{
			List<TdsParserStateObject> cache = this._cache;
			lock (cache)
			{
				for (int i = this._cache.Count - 1; i >= 0; i--)
				{
					TdsParserStateObject tdsParserStateObject = this._cache[i];
					if (tdsParserStateObject != null && tdsParserStateObject.IsOrphaned)
					{
						this.PutSession(tdsParserStateObject);
					}
				}
			}
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x0007E0C4 File Offset: 0x0007C2C4
		internal void Dispose()
		{
			List<TdsParserStateObject> cache = this._cache;
			lock (cache)
			{
				for (int i = 0; i < this._freeStateObjectCount; i++)
				{
					if (this._freeStateObjects[i] != null)
					{
						this._freeStateObjects[i].Dispose();
					}
				}
				this._freeStateObjects = null;
				this._freeStateObjectCount = 0;
				for (int j = 0; j < this._cache.Count; j++)
				{
					if (this._cache[j] != null)
					{
						if (this._cache[j].IsOrphaned)
						{
							this._cache[j].Dispose();
						}
						else
						{
							this._cache[j].DecrementPendingCallbacks(false);
						}
					}
				}
				this._cache.Clear();
				this._cachedCount = 0;
			}
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x0007E1A4 File Offset: 0x0007C3A4
		internal TdsParserStateObject GetSession(object owner)
		{
			List<TdsParserStateObject> cache = this._cache;
			TdsParserStateObject tdsParserStateObject;
			lock (cache)
			{
				if (this.IsDisposed)
				{
					throw ADP.ClosedConnectionError();
				}
				if (this._freeStateObjectCount > 0)
				{
					this._freeStateObjectCount--;
					tdsParserStateObject = this._freeStateObjects[this._freeStateObjectCount];
					this._freeStateObjects[this._freeStateObjectCount] = null;
				}
				else
				{
					tdsParserStateObject = this._parser.CreateSession();
					this._cache.Add(tdsParserStateObject);
					this._cachedCount = this._cache.Count;
				}
				tdsParserStateObject.Activate(owner);
			}
			return tdsParserStateObject;
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x0007E254 File Offset: 0x0007C454
		internal void PutSession(TdsParserStateObject session)
		{
			bool flag = session.Deactivate();
			List<TdsParserStateObject> cache = this._cache;
			lock (cache)
			{
				if (this.IsDisposed)
				{
					session.Dispose();
				}
				else if (flag && this._freeStateObjectCount < 10)
				{
					this._freeStateObjects[this._freeStateObjectCount] = session;
					this._freeStateObjectCount++;
				}
				else
				{
					this._cache.Remove(session);
					this._cachedCount = this._cache.Count;
					session.Dispose();
				}
				session.RemoveOwner();
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x0600190B RID: 6411 RVA: 0x0007E2FC File Offset: 0x0007C4FC
		internal int ActiveSessionsCount
		{
			get
			{
				return this._cachedCount - this._freeStateObjectCount;
			}
		}

		// Token: 0x04001218 RID: 4632
		private const int MaxInactiveCount = 10;

		// Token: 0x04001219 RID: 4633
		private readonly TdsParser _parser;

		// Token: 0x0400121A RID: 4634
		private readonly List<TdsParserStateObject> _cache;

		// Token: 0x0400121B RID: 4635
		private int _cachedCount;

		// Token: 0x0400121C RID: 4636
		private TdsParserStateObject[] _freeStateObjects;

		// Token: 0x0400121D RID: 4637
		private int _freeStateObjectCount;
	}
}
