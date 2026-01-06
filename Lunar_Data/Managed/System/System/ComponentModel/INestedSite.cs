using System;

namespace System.ComponentModel
{
	/// <summary>Provides the ability to retrieve the full nested name of a component.</summary>
	// Token: 0x020006CE RID: 1742
	public interface INestedSite : ISite, IServiceProvider
	{
		/// <summary>Gets the full name of the component in this site.</summary>
		/// <returns>The full name of the component in this site.</returns>
		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x06003799 RID: 14233
		string FullName { get; }
	}
}
