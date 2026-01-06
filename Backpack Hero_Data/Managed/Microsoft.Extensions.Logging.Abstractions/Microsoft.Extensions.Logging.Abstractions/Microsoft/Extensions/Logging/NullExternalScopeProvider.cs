using System;

namespace Microsoft.Extensions.Logging
{
	/// <summary>
	/// Scope provider that does nothing.
	/// </summary>
	// Token: 0x02000019 RID: 25
	internal class NullExternalScopeProvider : IExternalScopeProvider
	{
		// Token: 0x0600007C RID: 124 RVA: 0x0000322B File Offset: 0x0000142B
		private NullExternalScopeProvider()
		{
		}

		/// <summary>
		/// Returns a cached instance of <see cref="T:Microsoft.Extensions.Logging.NullExternalScopeProvider" />.
		/// </summary>
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003233 File Offset: 0x00001433
		public static IExternalScopeProvider Instance { get; } = new NullExternalScopeProvider();

		/// <inheritdoc />
		// Token: 0x0600007E RID: 126 RVA: 0x0000323A File Offset: 0x0000143A
		void IExternalScopeProvider.ForEachScope<TState>(Action<object, TState> callback, TState state)
		{
		}

		/// <inheritdoc />
		// Token: 0x0600007F RID: 127 RVA: 0x0000323C File Offset: 0x0000143C
		IDisposable IExternalScopeProvider.Push(object state)
		{
			return NullScope.Instance;
		}
	}
}
