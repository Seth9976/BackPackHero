using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides support for root-level designer view technologies.</summary>
	// Token: 0x0200077D RID: 1917
	public interface IRootDesigner : IDesigner, IDisposable
	{
		/// <summary>Gets the set of technologies that this designer can support for its display.</summary>
		/// <returns>An array of supported <see cref="T:System.ComponentModel.Design.ViewTechnology" /> values.</returns>
		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x06003CE9 RID: 15593
		ViewTechnology[] SupportedTechnologies { get; }

		/// <summary>Gets a view object for the specified view technology.</summary>
		/// <returns>An object that represents the view for this designer.</returns>
		/// <param name="technology">A <see cref="T:System.ComponentModel.Design.ViewTechnology" /> that indicates a particular view technology.</param>
		/// <exception cref="T:System.ArgumentException">The specified view technology is not supported or does not exist. </exception>
		// Token: 0x06003CEA RID: 15594
		object GetView(ViewTechnology technology);
	}
}
