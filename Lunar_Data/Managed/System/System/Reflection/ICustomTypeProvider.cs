using System;

namespace System.Reflection
{
	/// <summary>Represents an object that provides a custom type.</summary>
	// Token: 0x02000877 RID: 2167
	public interface ICustomTypeProvider
	{
		/// <summary>Gets the custom type provided by this object.</summary>
		/// <returns>The custom type. </returns>
		// Token: 0x060044BC RID: 17596
		Type GetCustomType();
	}
}
