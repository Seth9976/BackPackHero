using System;

namespace System.Data
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Data.DataTable.ColumnChanging" /> event.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:System.Data.DataColumnChangeEventArgs" /> that contains the event data. </param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200004D RID: 77
	// (Invoke) Token: 0x06000376 RID: 886
	public delegate void DataColumnChangeEventHandler(object sender, DataColumnChangeEventArgs e);
}
