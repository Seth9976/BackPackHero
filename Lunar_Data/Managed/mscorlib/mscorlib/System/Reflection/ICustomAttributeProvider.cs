using System;

namespace System.Reflection
{
	/// <summary>Provides custom attributes for reflection objects that support them.</summary>
	// Token: 0x020008A1 RID: 2209
	public interface ICustomAttributeProvider
	{
		/// <summary>Returns an array of all of the custom attributes defined on this member, excluding named attributes, or an empty array if there are no custom attributes.</summary>
		/// <returns>An array of Objects representing custom attributes, or an empty array.</returns>
		/// <param name="inherit">When true, look up the hierarchy chain for the inherited custom attribute. </param>
		/// <exception cref="T:System.TypeLoadException">The custom attribute type cannot be loaded. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">There is more than one attribute of type <paramref name="attributeType" /> defined on this member. </exception>
		// Token: 0x060048F3 RID: 18675
		object[] GetCustomAttributes(bool inherit);

		/// <summary>Returns an array of custom attributes defined on this member, identified by type, or an empty array if there are no custom attributes of that type.</summary>
		/// <returns>An array of Objects representing custom attributes, or an empty array.</returns>
		/// <param name="attributeType">The type of the custom attributes. </param>
		/// <param name="inherit">When true, look up the hierarchy chain for the inherited custom attribute. </param>
		/// <exception cref="T:System.TypeLoadException">The custom attribute type cannot be loaded. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is null.</exception>
		// Token: 0x060048F4 RID: 18676
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		/// <summary>Indicates whether one or more instance of <paramref name="attributeType" /> is defined on this member.</summary>
		/// <returns>true if the <paramref name="attributeType" /> is defined on this member; false otherwise.</returns>
		/// <param name="attributeType">The type of the custom attributes. </param>
		/// <param name="inherit">When true, look up the hierarchy chain for the inherited custom attribute. </param>
		// Token: 0x060048F5 RID: 18677
		bool IsDefined(Type attributeType, bool inherit);
	}
}
