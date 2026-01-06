using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000315 RID: 789
	internal sealed class BeginEndAwaitableAdapter : RendezvousAwaitable<IAsyncResult>
	{
		// Token: 0x060021BF RID: 8639 RVA: 0x0007911C File Offset: 0x0007731C
		public BeginEndAwaitableAdapter()
		{
			base.RunContinuationsAsynchronously = false;
		}

		// Token: 0x04001BDF RID: 7135
		public static readonly AsyncCallback Callback = delegate(IAsyncResult asyncResult)
		{
			((BeginEndAwaitableAdapter)asyncResult.AsyncState).SetResult(asyncResult);
		};
	}
}
