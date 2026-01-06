using System;

namespace System.ComponentModel
{
	/// <summary>Notifies clients that a property value has changed.</summary>
	// Token: 0x02000716 RID: 1814
	public interface INotifyPropertyChanged
	{
		/// <summary>Occurs when a property value changes.</summary>
		// Token: 0x14000047 RID: 71
		// (add) Token: 0x060039C6 RID: 14790
		// (remove) Token: 0x060039C7 RID: 14791
		event PropertyChangedEventHandler PropertyChanged;
	}
}
