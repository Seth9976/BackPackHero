using System;

namespace System.Data
{
	/// <summary>Provides additional information for the <see cref="E:System.Data.SqlClient.SqlCommand.StatementCompleted" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000E2 RID: 226
	public sealed class StatementCompletedEventArgs : EventArgs
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Data.StatementCompletedEventArgs" /> class.</summary>
		/// <param name="recordCount">Indicates the number of rows affected by the statement that caused the <see cref="E:System.Data.SqlClient.SqlCommand.StatementCompleted" />  event to occur.</param>
		// Token: 0x06000CAD RID: 3245 RVA: 0x00039FEC File Offset: 0x000381EC
		public StatementCompletedEventArgs(int recordCount)
		{
			this.RecordCount = recordCount;
		}

		/// <summary>Indicates the number of rows affected by the statement that caused the <see cref="E:System.Data.SqlClient.SqlCommand.StatementCompleted" /> event to occur.</summary>
		/// <returns>The number of rows affected.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x00039FFB File Offset: 0x000381FB
		public int RecordCount { get; }
	}
}
