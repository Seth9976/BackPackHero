using System;

namespace System.Data
{
	/// <summary>The delegate type for the event handlers of the <see cref="E:System.Data.SqlClient.SqlCommand.StatementCompleted" /> event.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The data for the event.</param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000E3 RID: 227
	// (Invoke) Token: 0x06000CB0 RID: 3248
	public delegate void StatementCompletedEventHandler(object sender, StatementCompletedEventArgs e);
}
