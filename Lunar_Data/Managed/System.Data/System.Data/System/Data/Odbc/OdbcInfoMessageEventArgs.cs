using System;
using System.Text;
using Unity;

namespace System.Data.Odbc
{
	/// <summary>Provides data for the <see cref="E:System.Data.Odbc.OdbcConnection.InfoMessage" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200029D RID: 669
	public sealed class OdbcInfoMessageEventArgs : EventArgs
	{
		// Token: 0x06001D2F RID: 7471 RVA: 0x0008EEC9 File Offset: 0x0008D0C9
		internal OdbcInfoMessageEventArgs(OdbcErrorCollection errors)
		{
			this._errors = errors;
		}

		/// <summary>Gets the collection of warnings sent from the data source.</summary>
		/// <returns>The collection of warnings sent from the data source.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001D30 RID: 7472 RVA: 0x0008EED8 File Offset: 0x0008D0D8
		public OdbcErrorCollection Errors
		{
			get
			{
				return this._errors;
			}
		}

		/// <summary>Gets the full text of the error sent from the database.</summary>
		/// <returns>The full text of the error.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001D31 RID: 7473 RVA: 0x0008EEE0 File Offset: 0x0008D0E0
		public string Message
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (object obj in this.Errors)
				{
					OdbcError odbcError = (OdbcError)obj;
					if (0 < stringBuilder.Length)
					{
						stringBuilder.Append(Environment.NewLine);
					}
					stringBuilder.Append(odbcError.Message);
				}
				return stringBuilder.ToString();
			}
		}

		/// <summary>Retrieves a string representation of the <see cref="E:System.Data.Odbc.OdbcConnection.InfoMessage" /> event.</summary>
		/// <returns>A string representing the <see cref="E:System.Data.Odbc.OdbcConnection.InfoMessage" /> event.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001D32 RID: 7474 RVA: 0x0008EF60 File Offset: 0x0008D160
		public override string ToString()
		{
			return this.Message;
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x0000E24C File Offset: 0x0000C44C
		internal OdbcInfoMessageEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040015AE RID: 5550
		private OdbcErrorCollection _errors;
	}
}
