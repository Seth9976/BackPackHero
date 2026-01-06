using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.Extensions.Logging
{
	/// <summary>
	/// Default implementation of <see cref="T:Microsoft.Extensions.Logging.IExternalScopeProvider" />.
	/// </summary>
	// Token: 0x02000013 RID: 19
	public class LoggerExternalScopeProvider : IExternalScopeProvider
	{
		/// <inheritdoc />
		// Token: 0x06000055 RID: 85 RVA: 0x00002A70 File Offset: 0x00000C70
		public void ForEachScope<TState>(Action<object, TState> callback, TState state)
		{
			LoggerExternalScopeProvider.<>c__DisplayClass2_0<TState> CS$<>8__locals1;
			CS$<>8__locals1.callback = callback;
			CS$<>8__locals1.state = state;
			LoggerExternalScopeProvider.<ForEachScope>g__Report|2_0<TState>(this._currentScope.Value, ref CS$<>8__locals1);
		}

		/// <inheritdoc />
		// Token: 0x06000056 RID: 86 RVA: 0x00002AA0 File Offset: 0x00000CA0
		public IDisposable Push(object state)
		{
			LoggerExternalScopeProvider.Scope value = this._currentScope.Value;
			LoggerExternalScopeProvider.Scope scope = new LoggerExternalScopeProvider.Scope(this, state, value);
			this._currentScope.Value = scope;
			return scope;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002ACF File Offset: 0x00000CCF
		[CompilerGenerated]
		internal static void <ForEachScope>g__Report|2_0<TState>(LoggerExternalScopeProvider.Scope current, ref LoggerExternalScopeProvider.<>c__DisplayClass2_0<TState> A_1)
		{
			if (current == null)
			{
				return;
			}
			LoggerExternalScopeProvider.<ForEachScope>g__Report|2_0<TState>(current.Parent, ref A_1);
			A_1.callback.Invoke(current.State, A_1.state);
		}

		// Token: 0x04000012 RID: 18
		private readonly AsyncLocal<LoggerExternalScopeProvider.Scope> _currentScope = new AsyncLocal<LoggerExternalScopeProvider.Scope>();

		// Token: 0x02000023 RID: 35
		private class Scope : IDisposable
		{
			// Token: 0x060000AF RID: 175 RVA: 0x00003460 File Offset: 0x00001660
			internal Scope(LoggerExternalScopeProvider provider, object state, LoggerExternalScopeProvider.Scope parent)
			{
				this._provider = provider;
				this.State = state;
				this.Parent = parent;
			}

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000347D File Offset: 0x0000167D
			public LoggerExternalScopeProvider.Scope Parent { get; }

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003485 File Offset: 0x00001685
			public object State { get; }

			// Token: 0x060000B2 RID: 178 RVA: 0x0000348D File Offset: 0x0000168D
			public override string ToString()
			{
				object state = this.State;
				if (state == null)
				{
					return null;
				}
				return state.ToString();
			}

			// Token: 0x060000B3 RID: 179 RVA: 0x000034A0 File Offset: 0x000016A0
			public void Dispose()
			{
				if (!this._isDisposed)
				{
					this._provider._currentScope.Value = this.Parent;
					this._isDisposed = true;
				}
			}

			// Token: 0x04000037 RID: 55
			private readonly LoggerExternalScopeProvider _provider;

			// Token: 0x04000038 RID: 56
			private bool _isDisposed;
		}
	}
}
