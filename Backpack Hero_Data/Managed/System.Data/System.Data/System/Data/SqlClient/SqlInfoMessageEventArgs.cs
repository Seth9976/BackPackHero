using System;
using Unity;

namespace System.Data.SqlClient
{
	/// <summary>Provides data for the <see cref="E:System.Data.SqlClient.SqlConnection.InfoMessage" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001B2 RID: 434
	public sealed class SqlInfoMessageEventArgs : EventArgs
	{
		// Token: 0x06001502 RID: 5378 RVA: 0x00068A50 File Offset: 0x00066C50
		internal SqlInfoMessageEventArgs(SqlException exception)
		{
			this._exception = exception;
		}

		/// <summary>Gets the collection of warnings sent from the server.</summary>
		/// <returns>The collection of warnings sent from the server.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001503 RID: 5379 RVA: 0x00068A5F File Offset: 0x00066C5F
		public SqlErrorCollection Errors
		{
			get
			{
				return this._exception.Errors;
			}
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x00068A6C File Offset: 0x00066C6C
		private bool ShouldSerializeErrors()
		{
			return this._exception != null && 0 < this._exception.Errors.Count;
		}

		/// <summary>Gets the full text of the error sent from the database.</summary>
		/// <returns>The full text of the error.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06001505 RID: 5381 RVA: 0x00068A8B File Offset: 0x00066C8B
		public string Message
		{
			get
			{
				return this._exception.Message;
			}
		}

		/// <summary>Gets the name of the object that generated the error.</summary>
		/// <returns>The name of the object that generated the error.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06001506 RID: 5382 RVA: 0x00068A98 File Offset: 0x00066C98
		public string Source
		{
			get
			{
				return this._exception.Source;
			}
		}

		/// <summary>Retrieves a string representation of the <see cref="E:System.Data.SqlClient.SqlConnection.InfoMessage" /> event.</summary>
		/// <returns>A string representing the <see cref="E:System.Data.SqlClient.SqlConnection.InfoMessage" /> event.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001507 RID: 5383 RVA: 0x00068AA5 File Offset: 0x00066CA5
		public override string ToString()
		{
			return this.Message;
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x0000E24C File Offset: 0x0000C44C
		internal SqlInfoMessageEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000E2D RID: 3629
		private SqlException _exception;
	}
}
