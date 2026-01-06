using System;
using System.Collections.Generic;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000139 RID: 313
	internal class SelectManyObservable<TSource, TResult> : IObservable<TResult>
	{
		// Token: 0x0600111E RID: 4382 RVA: 0x000516D2 File Offset: 0x0004F8D2
		public SelectManyObservable(IObservable<TSource> source, Func<TSource, IEnumerable<TResult>> filter)
		{
			this.m_Source = source;
			this.m_Filter = filter;
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x000516E8 File Offset: 0x0004F8E8
		public IDisposable Subscribe(IObserver<TResult> observer)
		{
			return this.m_Source.Subscribe(new SelectManyObservable<TSource, TResult>.Select(this, observer));
		}

		// Token: 0x040006CB RID: 1739
		private readonly IObservable<TSource> m_Source;

		// Token: 0x040006CC RID: 1740
		private readonly Func<TSource, IEnumerable<TResult>> m_Filter;

		// Token: 0x02000241 RID: 577
		private class Select : IObserver<TSource>
		{
			// Token: 0x060015BE RID: 5566 RVA: 0x0006399A File Offset: 0x00061B9A
			public Select(SelectManyObservable<TSource, TResult> observable, IObserver<TResult> observer)
			{
				this.m_Observable = observable;
				this.m_Observer = observer;
			}

			// Token: 0x060015BF RID: 5567 RVA: 0x000639B0 File Offset: 0x00061BB0
			public void OnCompleted()
			{
			}

			// Token: 0x060015C0 RID: 5568 RVA: 0x000639B2 File Offset: 0x00061BB2
			public void OnError(Exception error)
			{
				Debug.LogException(error);
			}

			// Token: 0x060015C1 RID: 5569 RVA: 0x000639BC File Offset: 0x00061BBC
			public void OnNext(TSource evt)
			{
				foreach (TResult tresult in this.m_Observable.m_Filter(evt))
				{
					this.m_Observer.OnNext(tresult);
				}
			}

			// Token: 0x04000C1D RID: 3101
			private SelectManyObservable<TSource, TResult> m_Observable;

			// Token: 0x04000C1E RID: 3102
			private readonly IObserver<TResult> m_Observer;
		}
	}
}
