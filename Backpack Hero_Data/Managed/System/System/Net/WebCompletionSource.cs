using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020004C9 RID: 1225
	internal class WebCompletionSource<T>
	{
		// Token: 0x060027CE RID: 10190 RVA: 0x000939F8 File Offset: 0x00091BF8
		public WebCompletionSource(bool runAsync = true)
		{
			this.completion = new TaskCompletionSource<WebCompletionSource<T>.Result>(runAsync ? TaskCreationOptions.RunContinuationsAsynchronously : TaskCreationOptions.None);
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x060027CF RID: 10191 RVA: 0x00093A13 File Offset: 0x00091C13
		internal WebCompletionSource<T>.Result CurrentResult
		{
			get
			{
				return this.currentResult;
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x060027D0 RID: 10192 RVA: 0x00093A1B File Offset: 0x00091C1B
		internal WebCompletionSource<T>.Status CurrentStatus
		{
			get
			{
				WebCompletionSource<T>.Result result = this.currentResult;
				if (result == null)
				{
					return WebCompletionSource<T>.Status.Running;
				}
				return result.Status;
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x060027D1 RID: 10193 RVA: 0x00093A2E File Offset: 0x00091C2E
		internal Task Task
		{
			get
			{
				return this.completion.Task;
			}
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x00093A3C File Offset: 0x00091C3C
		public bool TrySetCompleted(T argument)
		{
			WebCompletionSource<T>.Result result = new WebCompletionSource<T>.Result(argument);
			return Interlocked.CompareExchange<WebCompletionSource<T>.Result>(ref this.currentResult, result, null) == null && this.completion.TrySetResult(result);
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x00093A70 File Offset: 0x00091C70
		public bool TrySetCompleted()
		{
			WebCompletionSource<T>.Result result = new WebCompletionSource<T>.Result(WebCompletionSource<T>.Status.Completed, null);
			return Interlocked.CompareExchange<WebCompletionSource<T>.Result>(ref this.currentResult, result, null) == null && this.completion.TrySetResult(result);
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x00093AA2 File Offset: 0x00091CA2
		public bool TrySetCanceled()
		{
			return this.TrySetCanceled(new OperationCanceledException());
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x00093AB0 File Offset: 0x00091CB0
		public bool TrySetCanceled(OperationCanceledException error)
		{
			WebCompletionSource<T>.Result result = new WebCompletionSource<T>.Result(WebCompletionSource<T>.Status.Canceled, ExceptionDispatchInfo.Capture(error));
			return Interlocked.CompareExchange<WebCompletionSource<T>.Result>(ref this.currentResult, result, null) == null && this.completion.TrySetResult(result);
		}

		// Token: 0x060027D6 RID: 10198 RVA: 0x00093AE8 File Offset: 0x00091CE8
		public bool TrySetException(Exception error)
		{
			WebCompletionSource<T>.Result result = new WebCompletionSource<T>.Result(WebCompletionSource<T>.Status.Faulted, ExceptionDispatchInfo.Capture(error));
			return Interlocked.CompareExchange<WebCompletionSource<T>.Result>(ref this.currentResult, result, null) == null && this.completion.TrySetResult(result);
		}

		// Token: 0x060027D7 RID: 10199 RVA: 0x00093B1F File Offset: 0x00091D1F
		public void ThrowOnError()
		{
			if (!this.completion.Task.IsCompleted)
			{
				return;
			}
			ExceptionDispatchInfo error = this.completion.Task.Result.Error;
			if (error == null)
			{
				return;
			}
			error.Throw();
		}

		// Token: 0x060027D8 RID: 10200 RVA: 0x00093B54 File Offset: 0x00091D54
		public async Task<T> WaitForCompletion()
		{
			WebCompletionSource<T>.Result result = await this.completion.Task.ConfigureAwait(false);
			if (result.Status == WebCompletionSource<T>.Status.Completed)
			{
				return result.Argument;
			}
			result.Error.Throw();
			throw new InvalidOperationException("Should never happen.");
		}

		// Token: 0x0400170C RID: 5900
		private TaskCompletionSource<WebCompletionSource<T>.Result> completion;

		// Token: 0x0400170D RID: 5901
		private WebCompletionSource<T>.Result currentResult;

		// Token: 0x020004CA RID: 1226
		internal enum Status
		{
			// Token: 0x0400170F RID: 5903
			Running,
			// Token: 0x04001710 RID: 5904
			Completed,
			// Token: 0x04001711 RID: 5905
			Canceled,
			// Token: 0x04001712 RID: 5906
			Faulted
		}

		// Token: 0x020004CB RID: 1227
		internal class Result
		{
			// Token: 0x17000858 RID: 2136
			// (get) Token: 0x060027D9 RID: 10201 RVA: 0x00093B97 File Offset: 0x00091D97
			public WebCompletionSource<T>.Status Status { get; }

			// Token: 0x17000859 RID: 2137
			// (get) Token: 0x060027DA RID: 10202 RVA: 0x00093B9F File Offset: 0x00091D9F
			public bool Success
			{
				get
				{
					return this.Status == WebCompletionSource<T>.Status.Completed;
				}
			}

			// Token: 0x1700085A RID: 2138
			// (get) Token: 0x060027DB RID: 10203 RVA: 0x00093BAA File Offset: 0x00091DAA
			public ExceptionDispatchInfo Error { get; }

			// Token: 0x1700085B RID: 2139
			// (get) Token: 0x060027DC RID: 10204 RVA: 0x00093BB2 File Offset: 0x00091DB2
			public T Argument { get; }

			// Token: 0x060027DD RID: 10205 RVA: 0x00093BBA File Offset: 0x00091DBA
			public Result(T argument)
			{
				this.Status = WebCompletionSource<T>.Status.Completed;
				this.Argument = argument;
			}

			// Token: 0x060027DE RID: 10206 RVA: 0x00093BD0 File Offset: 0x00091DD0
			public Result(WebCompletionSource<T>.Status state, ExceptionDispatchInfo error)
			{
				this.Status = state;
				this.Error = error;
			}
		}
	}
}
