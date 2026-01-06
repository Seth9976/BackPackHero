using System;

namespace System.ComponentModel
{
	/// <summary>Indicates whether a class converts property change events to <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> events.</summary>
	// Token: 0x020006CF RID: 1743
	public interface IRaiseItemChangedEvents
	{
		/// <summary>Gets a value indicating whether the <see cref="T:System.ComponentModel.IRaiseItemChangedEvents" /> object raises <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> events.</summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.IRaiseItemChangedEvents" /> object raises <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> events when one of its property values changes; otherwise, false.</returns>
		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x0600379A RID: 14234
		bool RaisesItemChangedEvents { get; }
	}
}
