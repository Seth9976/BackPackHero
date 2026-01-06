using System;

namespace System.Collections
{
	/// <summary>Supplies a hash code for an object, using a custom hash function.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000A4F RID: 2639
	[Obsolete("Please use IEqualityComparer instead.")]
	public interface IHashCodeProvider
	{
		/// <summary>Returns a hash code for the specified object.</summary>
		/// <returns>A hash code for the specified object.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned. </param>
		/// <exception cref="T:System.ArgumentNullException">The type of <paramref name="obj" /> is a reference type and <paramref name="obj" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005EAB RID: 24235
		int GetHashCode(object obj);
	}
}
