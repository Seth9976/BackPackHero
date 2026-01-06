using System;
using System.Collections.Generic;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000139 RID: 313
	internal class SelectManyObservable<TSource, TResult> : IObservable<TResult>
	{
		// Token: 0x06001125 RID: 4389 RVA: 0x000518E6 File Offset: 0x0004FAE6
		public SelectManyObservable(IObservable<TSource> source, Func<TSource, IEnumerable<TResult>> filter)
		{
			this.m_Source = source;
			this.m_Filter = filter;
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x000518FC File Offset: 0x0004FAFC
		public IDisposable Subscribe(IObserver<TResult> observer)
		{
			return this.m_Source.Subscribe(new SelectManyObservable<TSource, TResult>.Select(this, observer));
		}

		// Token: 0x040006CC RID: 1740
		private readonly IObservable<TSource> m_Source;

		// Token: 0x040006CD RID: 1741
		private readonly Func<TSource, IEnumerable<TResult>> m_Filter;

		// Token: 0x02000241 RID: 577
		private class Select : IObserver<TSource>
		{
			// Token: 0x060015C5 RID: 5573 RVA: 0x00063BAE File Offset: 0x00061DAE
			public Select(SelectManyObservable<TSource, TResult> observable, IObserver<TResult> observer)
			{
				this.m_Observable = observable;
				this.m_Observer = observer;
			}

			// Token: 0x060015C6 RID: 5574 RVA: 0x00063BC4 File Offset: 0x00061DC4
			public void OnCompleted()
			{
			}

			// Token: 0x060015C7 RID: 5575 RVA: 0x00063BC6 File Offset: 0x00061DC6
			public void OnError(Exception error)
			{
				Debug.LogException(error);
			}

			// Token: 0x060015C8 RID: 5576 RVA: 0x00063BD0 File Offset: 0x00061DD0
			public void OnNext(TSource evt)
			{
				foreach (TResult tresult in this.m_Observable.m_Filter(evt))
				{
					this.m_Observer.OnNext(tresult);
				}
			}

			// Token: 0x04000C1E RID: 3102
			private SelectManyObservable<TSource, TResult> m_Observable;

			// Token: 0x04000C1F RID: 3103
			private readonly IObserver<TResult> m_Observer;
		}
	}
}
