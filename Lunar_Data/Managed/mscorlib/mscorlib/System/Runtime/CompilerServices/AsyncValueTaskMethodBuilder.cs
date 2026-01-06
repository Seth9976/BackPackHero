using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007DD RID: 2013
	[StructLayout(LayoutKind.Auto)]
	public struct AsyncValueTaskMethodBuilder
	{
		// Token: 0x060045C4 RID: 17860 RVA: 0x000E51C4 File Offset: 0x000E33C4
		public static AsyncValueTaskMethodBuilder Create()
		{
			return default(AsyncValueTaskMethodBuilder);
		}

		// Token: 0x060045C5 RID: 17861 RVA: 0x000E51DA File Offset: 0x000E33DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			this._methodBuilder.Start<TStateMachine>(ref stateMachine);
		}

		// Token: 0x060045C6 RID: 17862 RVA: 0x000E51E8 File Offset: 0x000E33E8
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this._methodBuilder.SetStateMachine(stateMachine);
		}

		// Token: 0x060045C7 RID: 17863 RVA: 0x000E51F6 File Offset: 0x000E33F6
		public void SetResult()
		{
			if (this._useBuilder)
			{
				this._methodBuilder.SetResult();
				return;
			}
			this._haveResult = true;
		}

		// Token: 0x060045C8 RID: 17864 RVA: 0x000E5213 File Offset: 0x000E3413
		public void SetException(Exception exception)
		{
			this._methodBuilder.SetException(exception);
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x060045C9 RID: 17865 RVA: 0x000E5224 File Offset: 0x000E3424
		public ValueTask Task
		{
			get
			{
				if (this._haveResult)
				{
					return default(ValueTask);
				}
				this._useBuilder = true;
				return new ValueTask(this._methodBuilder.Task);
			}
		}

		// Token: 0x060045CA RID: 17866 RVA: 0x000E525A File Offset: 0x000E345A
		public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this._useBuilder = true;
			this._methodBuilder.AwaitOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x060045CB RID: 17867 RVA: 0x000E5270 File Offset: 0x000E3470
		[SecuritySafeCritical]
		public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this._useBuilder = true;
			this._methodBuilder.AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x04002D24 RID: 11556
		private AsyncTaskMethodBuilder _methodBuilder;

		// Token: 0x04002D25 RID: 11557
		private bool _haveResult;

		// Token: 0x04002D26 RID: 11558
		private bool _useBuilder;
	}
}
