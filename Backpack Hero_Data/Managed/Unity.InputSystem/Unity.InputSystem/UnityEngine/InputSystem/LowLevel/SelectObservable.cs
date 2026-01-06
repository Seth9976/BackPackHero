using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000F7 RID: 247
	internal class SelectObservable<TSource, TResult> : IObservable<TResult>
	{
		// Token: 0x06000E8E RID: 3726 RVA: 0x00047403 File Offset: 0x00045603
		public SelectObservable(IObservable<TSource> source, Func<TSource, TResult> filter)
		{
			this.m_Source = source;
			this.m_Filter = filter;
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x00047419 File Offset: 0x00045619
		public IDisposable Subscribe(IObserver<TResult> observer)
		{
			return this.m_Source.Subscribe(new SelectObservable<TSource, TResult>.Select(this, observer));
		}

		// Token: 0x040005FA RID: 1530
		private readonly IObservable<TSource> m_Source;

		// Token: 0x040005FB RID: 1531
		private readonly Func<TSource, TResult> m_Filter;

		// Token: 0x0200021D RID: 541
		private class Select : IObserver<TSource>
		{
			// Token: 0x060014F5 RID: 5365 RVA: 0x00060A29 File Offset: 0x0005EC29
			public Select(SelectObservable<TSource, TResult> observable, IObserver<TResult> observer)
			{
				this.m_Observable = observable;
				this.m_Observer = observer;
			}

			// Token: 0x060014F6 RID: 5366 RVA: 0x00060A3F File Offset: 0x0005EC3F
			public void OnCompleted()
			{
			}

			// Token: 0x060014F7 RID: 5367 RVA: 0x00060A41 File Offset: 0x0005EC41
			public void OnError(Exception error)
			{
				Debug.LogException(error);
			}

			// Token: 0x060014F8 RID: 5368 RVA: 0x00060A4C File Offset: 0x0005EC4C
			public void OnNext(TSource evt)
			{
				TResult tresult = this.m_Observable.m_Filter(evt);
				this.m_Observer.OnNext(tresult);
			}

			// Token: 0x04000B60 RID: 2912
			private SelectObservable<TSource, TResult> m_Observable;

			// Token: 0x04000B61 RID: 2913
			private readonly IObserver<TResult> m_Observer;
		}
	}
}
