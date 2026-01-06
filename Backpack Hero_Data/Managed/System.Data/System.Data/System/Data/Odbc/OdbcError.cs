using System;
using Unity;

namespace System.Data.Odbc
{
	/// <summary>Collects information relevant to a warning or error returned by the data source.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000296 RID: 662
	[Serializable]
	public sealed class OdbcError
	{
		// Token: 0x06001CFE RID: 7422 RVA: 0x0008E8E8 File Offset: 0x0008CAE8
		internal OdbcError(string source, string message, string state, int nativeerror)
		{
			this._source = source;
			this._message = message;
			this._state = state;
			this._nativeerror = nativeerror;
		}

		/// <summary>Gets a short description of the error.</summary>
		/// <returns>A description of the error.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001CFF RID: 7423 RVA: 0x0008E90D File Offset: 0x0008CB0D
		public string Message
		{
			get
			{
				if (this._message == null)
				{
					return string.Empty;
				}
				return this._message;
			}
		}

		/// <summary>Gets the five-character error code that follows the ANSI SQL standard for the database.</summary>
		/// <returns>The five-character error code, which identifies the source of the error if the error can be issued from more than one place.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001D00 RID: 7424 RVA: 0x0008E923 File Offset: 0x0008CB23
		public string SQLState
		{
			get
			{
				return this._state;
			}
		}

		/// <summary>Gets the data source-specific error information.</summary>
		/// <returns>The data source-specific error information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001D01 RID: 7425 RVA: 0x0008E92B File Offset: 0x0008CB2B
		public int NativeError
		{
			get
			{
				return this._nativeerror;
			}
		}

		/// <summary>Gets the name of the driver that generated the error.</summary>
		/// <returns>The name of the driver that generated the error.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001D02 RID: 7426 RVA: 0x0008E933 File Offset: 0x0008CB33
		public string Source
		{
			get
			{
				if (this._source == null)
				{
					return string.Empty;
				}
				return this._source;
			}
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x0008E949 File Offset: 0x0008CB49
		internal void SetSource(string Source)
		{
			this._source = Source;
		}

		/// <summary>Gets the complete text of the error message.</summary>
		/// <returns>The complete text of the error.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001D04 RID: 7428 RVA: 0x0008E952 File Offset: 0x0008CB52
		public override string ToString()
		{
			return this.Message;
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x0000E24C File Offset: 0x0000C44C
		internal OdbcError()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040015A5 RID: 5541
		internal string _message;

		// Token: 0x040015A6 RID: 5542
		internal string _state;

		// Token: 0x040015A7 RID: 5543
		internal int _nativeerror;

		// Token: 0x040015A8 RID: 5544
		internal string _source;
	}
}
