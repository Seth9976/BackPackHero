using System;
using System.Globalization;
using System.Resources;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface for designers to access resource readers and writers for specific <see cref="T:System.Globalization.CultureInfo" /> resource types.</summary>
	// Token: 0x0200077C RID: 1916
	public interface IResourceService
	{
		/// <summary>Locates the resource reader for the specified culture and returns it.</summary>
		/// <returns>An <see cref="T:System.Resources.IResourceReader" /> interface that contains the resources for the culture, or null if no resources for the culture exist.</returns>
		/// <param name="info">The <see cref="T:System.Globalization.CultureInfo" /> of the resource for which to retrieve a resource reader. </param>
		// Token: 0x06003CE7 RID: 15591
		IResourceReader GetResourceReader(CultureInfo info);

		/// <summary>Locates the resource writer for the specified culture and returns it.</summary>
		/// <returns>An <see cref="T:System.Resources.IResourceWriter" /> interface for the specified culture.</returns>
		/// <param name="info">The <see cref="T:System.Globalization.CultureInfo" /> of the resource for which to create a resource writer. </param>
		// Token: 0x06003CE8 RID: 15592
		IResourceWriter GetResourceWriter(CultureInfo info);
	}
}
