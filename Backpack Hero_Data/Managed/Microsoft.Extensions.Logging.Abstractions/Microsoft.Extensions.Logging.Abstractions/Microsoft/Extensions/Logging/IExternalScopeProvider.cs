using System;

namespace Microsoft.Extensions.Logging
{
	/// <summary>
	/// Represents a storage of common scope data.
	/// </summary>
	// Token: 0x0200000C RID: 12
	public interface IExternalScopeProvider
	{
		/// <summary>
		/// Executes callback for each currently active scope objects in order of creation.
		/// All callbacks are guaranteed to be called inline from this method.
		/// </summary>
		/// <param name="callback">The callback to be executed for every scope object.</param>
		/// <param name="state">The state object to be passed into the callback.</param>
		/// <typeparam name="TState">The type of state to accept.</typeparam>
		// Token: 0x0600002C RID: 44
		void ForEachScope<TState>(Action<object, TState> callback, TState state);

		/// <summary>
		/// Adds scope object to the list.
		/// </summary>
		/// <param name="state">The scope object.</param>
		/// <returns>The <see cref="T:System.IDisposable" /> token that removes scope on dispose.</returns>
		// Token: 0x0600002D RID: 45
		IDisposable Push(object state);
	}
}
