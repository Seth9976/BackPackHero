using System;

namespace System.ComponentModel
{
	/// <summary>Provides functionality for nested containers, which logically contain zero or more other components and are owned by a parent component.</summary>
	// Token: 0x020006CD RID: 1741
	public interface INestedContainer : IContainer, IDisposable
	{
		/// <summary>Gets the owning component for the nested container.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IComponent" /> that owns the nested container.</returns>
		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06003798 RID: 14232
		IComponent Owner { get; }
	}
}
