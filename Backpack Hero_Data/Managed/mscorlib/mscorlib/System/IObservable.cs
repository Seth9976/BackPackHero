using System;

namespace System
{
	/// <summary>Defines a provider for push-based notification.</summary>
	/// <typeparam name="T">The object that provides notification information.This type parameter is covariant. That is, you can use either the type you specified or any type that is more derived. For more information about covariance and contravariance, see Covariance and Contravariance in Generics.</typeparam>
	// Token: 0x02000141 RID: 321
	public interface IObservable<out T>
	{
		/// <summary>Notifies the provider that an observer is to receive notifications.</summary>
		/// <returns>A reference to an interface that allows observers to stop receiving notifications before the provider has finished sending them.</returns>
		/// <param name="observer">The object that is to receive notifications.</param>
		// Token: 0x06000C01 RID: 3073
		IDisposable Subscribe(IObserver<T> observer);
	}
}
