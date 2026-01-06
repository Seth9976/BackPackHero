using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface for adding and removing extender providers at design time.</summary>
	// Token: 0x02000777 RID: 1911
	public interface IExtenderProviderService
	{
		/// <summary>Adds the specified extender provider.</summary>
		/// <param name="provider">The extender provider to add. </param>
		// Token: 0x06003CCF RID: 15567
		void AddExtenderProvider(IExtenderProvider provider);

		/// <summary>Removes the specified extender provider.</summary>
		/// <param name="provider">The extender provider to remove. </param>
		// Token: 0x06003CD0 RID: 15568
		void RemoveExtenderProvider(IExtenderProvider provider);
	}
}
