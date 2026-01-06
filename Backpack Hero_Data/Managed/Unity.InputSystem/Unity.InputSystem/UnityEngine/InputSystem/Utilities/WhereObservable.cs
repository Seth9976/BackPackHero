using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200013B RID: 315
	internal class WhereObservable<TValue> : IObservable<TValue>
	{
		// Token: 0x06001129 RID: 4393 RVA: 0x0005193A File Offset: 0x0004FB3A
		public WhereObservable(IObservable<TValue> source, Func<TValue, bool> predicate)
		{
			this.m_Source = source;
			this.m_Predicate = predicate;
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x00051950 File Offset: 0x0004FB50
		public IDisposable Subscribe(IObserver<TValue> observer)
		{
			return this.m_Source.Subscribe(new WhereObservable<TValue>.Where(this, observer));
		}

		// Token: 0x040006D0 RID: 1744
		private readonly IObservable<TValue> m_Source;

		// Token: 0x040006D1 RID: 1745
		private readonly Func<TValue, bool> m_Predicate;

		// Token: 0x02000243 RID: 579
		private class Where : IObserver<TValue>
		{
			// Token: 0x060015CD RID: 5581 RVA: 0x00063C95 File Offset: 0x00061E95
			public Where(WhereObservable<TValue> observable, IObserver<TValue> observer)
			{
				this.m_Observable = observable;
				this.m_Observer = observer;
			}

			// Token: 0x060015CE RID: 5582 RVA: 0x00063CAB File Offset: 0x00061EAB
			public void OnCompleted()
			{
			}

			// Token: 0x060015CF RID: 5583 RVA: 0x00063CAD File Offset: 0x00061EAD
			public void OnError(Exception error)
			{
				Debug.LogException(error);
			}

			// Token: 0x060015D0 RID: 5584 RVA: 0x00063CB5 File Offset: 0x00061EB5
			public void OnNext(TValue evt)
			{
				if (this.m_Observable.m_Predicate(evt))
				{
					this.m_Observer.OnNext(evt);
				}
			}

			// Token: 0x04000C22 RID: 3106
			private WhereObservable<TValue> m_Observable;

			// Token: 0x04000C23 RID: 3107
			private readonly IObserver<TValue> m_Observer;
		}
	}
}
