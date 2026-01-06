using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a type description provider for a specified type. </summary>
	// Token: 0x0200078F RID: 1935
	public abstract class TypeDescriptionProviderService
	{
		/// <summary>Gets a type description provider for the specified object.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> that corresponds with <paramref name="instance" />.</returns>
		/// <param name="instance">The object to get a type description provider for.</param>
		// Token: 0x06003D38 RID: 15672
		public abstract TypeDescriptionProvider GetProvider(object instance);

		/// <summary>Gets a type description provider for the specified type.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> that corresponds with <paramref name="type" />.</returns>
		/// <param name="type">The type to get a type description provider for.</param>
		// Token: 0x06003D39 RID: 15673
		public abstract TypeDescriptionProvider GetProvider(Type type);
	}
}
