using System;

namespace System.Data.Odbc
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Data.Odbc.OdbcDataAdapter.RowUpdating" /> event of an <see cref="T:System.Data.Odbc.OdbcDataAdapter" />.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">The <see cref="T:System.Data.Odbc.OdbcRowUpdatingEventArgs" /> that contains the event data. </param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002A5 RID: 677
	// (Invoke) Token: 0x06001DD3 RID: 7635
	public delegate void OdbcRowUpdatingEventHandler(object sender, OdbcRowUpdatingEventArgs e);
}
