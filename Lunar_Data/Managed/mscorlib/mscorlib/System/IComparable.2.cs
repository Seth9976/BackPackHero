using System;

namespace System
{
	/// <summary>Defines a generalized comparison method that a value type or class implements to create a type-specific comparison method for ordering instances.</summary>
	/// <typeparam name="T">The type of objects to compare.This type parameter is contravariant. That is, you can use either the type you specified or any type that is less derived. For more information about covariance and contravariance, see Covariance and Contravariance in Generics.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200013A RID: 314
	public interface IComparable<in T>
	{
		/// <summary>Compares the current object with another object of the same type.</summary>
		/// <returns>A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />. </returns>
		/// <param name="other">An object to compare with this object.</param>
		// Token: 0x06000BEA RID: 3050
		int CompareTo(T other);
	}
}
