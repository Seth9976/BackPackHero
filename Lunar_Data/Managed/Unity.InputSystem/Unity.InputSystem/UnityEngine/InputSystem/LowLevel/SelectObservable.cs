using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000F7 RID: 247
	internal class SelectObservable<TSource, TResult> : IObservable<TResult>
	{
		// Token: 0x06000E89 RID: 3721 RVA: 0x000473B7 File Offset: 0x000455B7
		public SelectObservable(IObservable<TSource> source, Func<TSource, TResult> filter)
		{
			this.m_Source = source;
			this.m_Filter = filter;
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x000473CD File Offset: 0x000455CD
		public IDisposable Subscribe(IObserver<TResult> observer)
		{
			return this.m_Source.Subscribe(new SelectObservable<TSource, TResult>.Select(this, observer));
		}

		// Token: 0x040005F9 RID: 1529
		private readonly IObservable<TSource> m_Source;

		// Token: 0x040005FA RID: 1530
		private readonly Func<TSource, TResult> m_Filter;

		// Token: 0x0200021D RID: 541
		private class Select : IObserver<TSource>
		{
			// Token: 0x060014EE RID: 5358 RVA: 0x00060815 File Offset: 0x0005EA15
			public Select(SelectObservable<TSource, TResult> observable, IObserver<TResult> observer)
			{
				this.m_Observable = observable;
				this.m_Observer = observer;
			}

			// Token: 0x060014EF RID: 5359 RVA: 0x0006082B File Offset: 0x0005EA2B
			public void OnCompleted()
			{
			}

			// Token: 0x060014F0 RID: 5360 RVA: 0x0006082D File Offset: 0x0005EA2D
			public void OnError(Exception error)
			{
				Debug.LogException(error);
			}

			// Token: 0x060014F1 RID: 5361 RVA: 0x00060838 File Offset: 0x0005EA38
			public void OnNext(TSource evt)
			{
				TResult tresult = this.m_Observable.m_Filter(evt);
				this.m_Observer.OnNext(tresult);
			}

			// Token: 0x04000B5F RID: 2911
			private SelectObservable<TSource, TResult> m_Observable;

			// Token: 0x04000B60 RID: 2912
			private readonly IObserver<TResult> m_Observer;
		}
	}
}
