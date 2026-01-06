using System;
using System.Threading;

namespace System.Net.Mime
{
	// Token: 0x02000619 RID: 1561
	internal sealed class MultiAsyncResult : LazyAsyncResult
	{
		// Token: 0x0600320C RID: 12812 RVA: 0x000B38CC File Offset: 0x000B1ACC
		internal MultiAsyncResult(object context, AsyncCallback callback, object state)
			: base(context, state, callback)
		{
			this._context = context;
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x0600320D RID: 12813 RVA: 0x000B38DE File Offset: 0x000B1ADE
		internal object Context
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x0600320E RID: 12814 RVA: 0x000B38E6 File Offset: 0x000B1AE6
		internal void Enter()
		{
			this.Increment();
		}

		// Token: 0x0600320F RID: 12815 RVA: 0x000B38EE File Offset: 0x000B1AEE
		internal void Leave()
		{
			this.Decrement();
		}

		// Token: 0x06003210 RID: 12816 RVA: 0x000B38F6 File Offset: 0x000B1AF6
		internal void Leave(object result)
		{
			base.Result = result;
			this.Decrement();
		}

		// Token: 0x06003211 RID: 12817 RVA: 0x000B3905 File Offset: 0x000B1B05
		private void Decrement()
		{
			if (Interlocked.Decrement(ref this._outstanding) == -1)
			{
				base.InvokeCallback(base.Result);
			}
		}

		// Token: 0x06003212 RID: 12818 RVA: 0x000B3921 File Offset: 0x000B1B21
		private void Increment()
		{
			Interlocked.Increment(ref this._outstanding);
		}

		// Token: 0x06003213 RID: 12819 RVA: 0x000B38EE File Offset: 0x000B1AEE
		internal void CompleteSequence()
		{
			this.Decrement();
		}

		// Token: 0x06003214 RID: 12820 RVA: 0x000B392F File Offset: 0x000B1B2F
		internal static object End(IAsyncResult result)
		{
			MultiAsyncResult multiAsyncResult = (MultiAsyncResult)result;
			multiAsyncResult.InternalWaitForCompletion();
			return multiAsyncResult.Result;
		}

		// Token: 0x04001E93 RID: 7827
		private readonly object _context;

		// Token: 0x04001E94 RID: 7828
		private int _outstanding;
	}
}
