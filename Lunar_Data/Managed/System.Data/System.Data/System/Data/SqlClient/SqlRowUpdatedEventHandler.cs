using System;

namespace System.Data.SqlClient
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Data.SqlClient.SqlDataAdapter.RowUpdated" /> event of a <see cref="T:System.Data.SqlClient.SqlDataAdapter" />.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">The <see cref="T:System.Data.SqlClient.SqlRowUpdatedEventArgs" /> that contains the event data. </param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001D1 RID: 465
	// (Invoke) Token: 0x0600167D RID: 5757
	public delegate void SqlRowUpdatedEventHandler(object sender, SqlRowUpdatedEventArgs e);
}
