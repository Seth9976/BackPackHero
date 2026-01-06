using System;

namespace System.ComponentModel
{
	/// <summary>Represents the method that will handle the <see cref="E:System.ComponentModel.INotifyPropertyChanging.PropertyChanging" /> event of an <see cref="T:System.ComponentModel.INotifyPropertyChanging" /> interface. </summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:System.ComponentModel.PropertyChangingEventArgs" /> that contains the event data.</param>
	// Token: 0x0200071B RID: 1819
	// (Invoke) Token: 0x060039D3 RID: 14803
	public delegate void PropertyChangingEventHandler(object sender, PropertyChangingEventArgs e);
}
