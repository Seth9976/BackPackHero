using System;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Collects information relevant to a warning or error returned by the data source.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000113 RID: 275
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbError
	{
		// Token: 0x06000F59 RID: 3929 RVA: 0x00003D55 File Offset: 0x00001F55
		internal OleDbError()
		{
		}

		/// <summary>Gets a short description of the error.</summary>
		/// <returns>A short description of the error.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public string Message
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the database-specific error information.</summary>
		/// <returns>The database-specific error information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000F5B RID: 3931 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public int NativeError
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the name of the provider that generated the error.</summary>
		/// <returns>The name of the provider that generated the error.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public string Source
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the five-character error code following the ANSI SQL standard for the database.</summary>
		/// <returns>The five-character error code, which identifies the source of the error, if the error can be issued from more than one place.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000F5D RID: 3933 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public string SQLState
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the complete text of the error message.</summary>
		/// <returns>The complete text of the error.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000F5E RID: 3934 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public override string ToString()
		{
			throw ADP.OleDb();
		}
	}
}
