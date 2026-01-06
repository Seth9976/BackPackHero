using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200013B RID: 315
	internal class WhereObservable<TValue> : IObservable<TValue>
	{
		// Token: 0x06001122 RID: 4386 RVA: 0x00051726 File Offset: 0x0004F926
		public WhereObservable(IObservable<TValue> source, Func<TValue, bool> predicate)
		{
			this.m_Source = source;
			this.m_Predicate = predicate;
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x0005173C File Offset: 0x0004F93C
		public IDisposable Subscribe(IObserver<TValue> observer)
		{
			return this.m_Source.Subscribe(new WhereObservable<TValue>.Where(this, observer));
		}

		// Token: 0x040006CF RID: 1743
		private readonly IObservable<TValue> m_Source;

		// Token: 0x040006D0 RID: 1744
		private readonly Func<TValue, bool> m_Predicate;

		// Token: 0x02000243 RID: 579
		private class Where : IObserver<TValue>
		{
			// Token: 0x060015C6 RID: 5574 RVA: 0x00063A81 File Offset: 0x00061C81
			public Where(WhereObservable<TValue> observable, IObserver<TValue> observer)
			{
				this.m_Observable = observable;
				this.m_Observer = observer;
			}

			// Token: 0x060015C7 RID: 5575 RVA: 0x00063A97 File Offset: 0x00061C97
			public void OnCompleted()
			{
			}

			// Token: 0x060015C8 RID: 5576 RVA: 0x00063A99 File Offset: 0x00061C99
			public void OnError(Exception error)
			{
				Debug.LogException(error);
			}

			// Token: 0x060015C9 RID: 5577 RVA: 0x00063AA1 File Offset: 0x00061CA1
			public void OnNext(TValue evt)
			{
				if (this.m_Observable.m_Predicate(evt))
				{
					this.m_Observer.OnNext(evt);
				}
			}

			// Token: 0x04000C21 RID: 3105
			private WhereObservable<TValue> m_Observable;

			// Token: 0x04000C22 RID: 3106
			private readonly IObserver<TValue> m_Observer;
		}
	}
}
