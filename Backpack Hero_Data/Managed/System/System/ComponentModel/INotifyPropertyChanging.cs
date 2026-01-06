using System;

namespace System.ComponentModel
{
	/// <summary>Notifies clients that a property value is changing.</summary>
	// Token: 0x02000717 RID: 1815
	public interface INotifyPropertyChanging
	{
		/// <summary>Occurs when a property value is changing.</summary>
		// Token: 0x14000048 RID: 72
		// (add) Token: 0x060039C8 RID: 14792
		// (remove) Token: 0x060039C9 RID: 14793
		event PropertyChangingEventHandler PropertyChanging;
	}
}
