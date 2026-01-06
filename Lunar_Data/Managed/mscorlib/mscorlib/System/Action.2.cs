using System;

namespace System
{
	/// <summary>Encapsulates a method that has a single parameter and does not return a value.</summary>
	/// <param name="obj">The parameter of the method that this delegate encapsulates.</param>
	/// <typeparam name="T">The type of the parameter of the method that this delegate encapsulates.This type parameter is contravariant. That is, you can use either the type you specified or any type that is less derived. For more information about covariance and contravariance, see Covariance and Contravariance in Generics.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000DD RID: 221
	// (Invoke) Token: 0x06000697 RID: 1687
	public delegate void Action<in T>(T obj);
}
