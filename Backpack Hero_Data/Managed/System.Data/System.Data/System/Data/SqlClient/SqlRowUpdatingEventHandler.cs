using System;

namespace System.Data.SqlClient
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Data.SqlClient.SqlDataAdapter.RowUpdating" /> event of a <see cref="T:System.Data.SqlClient.SqlDataAdapter" />.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">The <see cref="T:System.Data.SqlClient.SqlRowUpdatingEventArgs" /> that contains the event data. </param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001D3 RID: 467
	// (Invoke) Token: 0x06001686 RID: 5766
	public delegate void SqlRowUpdatingEventHandler(object sender, SqlRowUpdatingEventArgs e);
}
