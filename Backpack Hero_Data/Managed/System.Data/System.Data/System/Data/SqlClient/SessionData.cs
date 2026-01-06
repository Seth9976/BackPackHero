using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Data.SqlClient
{
	// Token: 0x020001B7 RID: 439
	internal class SessionData
	{
		// Token: 0x0600153E RID: 5438 RVA: 0x00068FFC File Offset: 0x000671FC
		public SessionData(SessionData recoveryData)
		{
			this._initialDatabase = recoveryData._initialDatabase;
			this._initialCollation = recoveryData._initialCollation;
			this._initialLanguage = recoveryData._initialLanguage;
			this._resolvedAliases = recoveryData._resolvedAliases;
			for (int i = 0; i < 256; i++)
			{
				if (recoveryData._initialState[i] != null)
				{
					this._initialState[i] = (byte[])recoveryData._initialState[i].Clone();
				}
			}
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x00069093 File Offset: 0x00067293
		public SessionData()
		{
			this._resolvedAliases = new Dictionary<string, Tuple<string, string>>(2);
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x000690C7 File Offset: 0x000672C7
		public void Reset()
		{
			this._database = null;
			this._collation = null;
			this._language = null;
			if (this._deltaDirty)
			{
				this._delta = new SessionStateRecord[256];
				this._deltaDirty = false;
			}
			this._unrecoverableStatesCount = 0;
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x00069104 File Offset: 0x00067304
		[Conditional("DEBUG")]
		public void AssertUnrecoverableStateCountIsCorrect()
		{
			byte b = 0;
			foreach (SessionStateRecord sessionStateRecord in this._delta)
			{
				if (sessionStateRecord != null && !sessionStateRecord._recoverable)
				{
					b += 1;
				}
			}
		}

		// Token: 0x04000E43 RID: 3651
		internal const int _maxNumberOfSessionStates = 256;

		// Token: 0x04000E44 RID: 3652
		internal uint _tdsVersion;

		// Token: 0x04000E45 RID: 3653
		internal bool _encrypted;

		// Token: 0x04000E46 RID: 3654
		internal string _database;

		// Token: 0x04000E47 RID: 3655
		internal SqlCollation _collation;

		// Token: 0x04000E48 RID: 3656
		internal string _language;

		// Token: 0x04000E49 RID: 3657
		internal string _initialDatabase;

		// Token: 0x04000E4A RID: 3658
		internal SqlCollation _initialCollation;

		// Token: 0x04000E4B RID: 3659
		internal string _initialLanguage;

		// Token: 0x04000E4C RID: 3660
		internal byte _unrecoverableStatesCount;

		// Token: 0x04000E4D RID: 3661
		internal Dictionary<string, Tuple<string, string>> _resolvedAliases;

		// Token: 0x04000E4E RID: 3662
		internal SessionStateRecord[] _delta = new SessionStateRecord[256];

		// Token: 0x04000E4F RID: 3663
		internal bool _deltaDirty;

		// Token: 0x04000E50 RID: 3664
		internal byte[][] _initialState = new byte[256][];
	}
}
