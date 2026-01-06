using System;

namespace System.Data.SqlClient
{
	/// <summary>Handles the <see cref="E:System.Data.SqlClient.SqlDependency.OnChange" /> event that is fired when a notification is received for any of the commands associated with a <see cref="T:System.Data.SqlClient.SqlDependency" /> object.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.Data.SqlClient.SqlNotificationEventArgs" /> object that contains the event data.</param>
	// Token: 0x02000136 RID: 310
	// (Invoke) Token: 0x0600101F RID: 4127
	public delegate void OnChangeEventHandler(object sender, SqlNotificationEventArgs e);
}
