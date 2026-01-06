using System;

namespace System.ComponentModel
{
	/// <summary>Provides a base class for the container filter service.</summary>
	// Token: 0x020006A4 RID: 1700
	public abstract class ContainerFilterService
	{
		/// <summary>Filters the component collection.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.ComponentCollection" /> that represents a modified collection.</returns>
		/// <param name="components">The component collection to filter.</param>
		// Token: 0x06003670 RID: 13936 RVA: 0x00003914 File Offset: 0x00001B14
		public virtual ComponentCollection FilterComponents(ComponentCollection components)
		{
			return components;
		}
	}
}
