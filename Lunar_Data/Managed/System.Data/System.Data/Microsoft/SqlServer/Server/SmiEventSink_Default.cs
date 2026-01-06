using System;
using System.Data.SqlClient;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000393 RID: 915
	internal class SmiEventSink_Default : SmiEventSink
	{
		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06002C13 RID: 11283 RVA: 0x000C123F File Offset: 0x000BF43F
		internal bool HasMessages
		{
			get
			{
				return this._errors != null || this._warnings != null;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06002C14 RID: 11284 RVA: 0x00003DF6 File Offset: 0x00001FF6
		internal virtual string ServerVersion
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002C15 RID: 11285 RVA: 0x000C1254 File Offset: 0x000BF454
		protected virtual void DispatchMessages()
		{
			SqlException ex = this.ProcessMessages(true);
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06002C16 RID: 11286 RVA: 0x000C1270 File Offset: 0x000BF470
		protected SqlException ProcessMessages(bool ignoreWarnings)
		{
			SqlException ex = null;
			SqlErrorCollection sqlErrorCollection = null;
			if (this._errors != null)
			{
				if (this._warnings != null)
				{
					foreach (object obj in this._warnings)
					{
						SqlError sqlError = (SqlError)obj;
						this._errors.Add(sqlError);
					}
				}
				sqlErrorCollection = this._errors;
				this._errors = null;
				this._warnings = null;
			}
			else
			{
				if (!ignoreWarnings)
				{
					sqlErrorCollection = this._warnings;
				}
				this._warnings = null;
			}
			if (sqlErrorCollection != null)
			{
				ex = SqlException.CreateException(sqlErrorCollection, this.ServerVersion);
			}
			return ex;
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x000C1320 File Offset: 0x000BF520
		internal void ProcessMessagesAndThrow()
		{
			if (this.HasMessages)
			{
				this.DispatchMessages();
			}
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x000C1330 File Offset: 0x000BF530
		internal SmiEventSink_Default()
		{
		}

		// Token: 0x04001AC7 RID: 6855
		private SqlErrorCollection _errors;

		// Token: 0x04001AC8 RID: 6856
		private SqlErrorCollection _warnings;
	}
}
