using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200013A RID: 314
	internal class TakeNObservable<TValue> : IObservable<TValue>
	{
		// Token: 0x06001127 RID: 4391 RVA: 0x00051910 File Offset: 0x0004FB10
		public TakeNObservable(IObservable<TValue> source, int count)
		{
			this.m_Source = source;
			this.m_Count = count;
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x00051926 File Offset: 0x0004FB26
		public IDisposable Subscribe(IObserver<TValue> observer)
		{
			return this.m_Source.Subscribe(new TakeNObservable<TValue>.Take(this, observer));
		}

		// Token: 0x040006CE RID: 1742
		private IObservable<TValue> m_Source;

		// Token: 0x040006CF RID: 1743
		private int m_Count;

		// Token: 0x02000242 RID: 578
		private class Take : IObserver<TValue>
		{
			// Token: 0x060015C9 RID: 5577 RVA: 0x00063C30 File Offset: 0x00061E30
			public Take(TakeNObservable<TValue> observable, IObserver<TValue> observer)
			{
				this.m_Observer = observer;
				this.m_Remaining = observable.m_Count;
			}

			// Token: 0x060015CA RID: 5578 RVA: 0x00063C4B File Offset: 0x00061E4B
			public void OnCompleted()
			{
			}

			// Token: 0x060015CB RID: 5579 RVA: 0x00063C4D File Offset: 0x00061E4D
			public void OnError(Exception error)
			{
				Debug.LogException(error);
			}

			// Token: 0x060015CC RID: 5580 RVA: 0x00063C55 File Offset: 0x00061E55
			public void OnNext(TValue evt)
			{
				if (this.m_Remaining <= 0)
				{
					return;
				}
				this.m_Remaining--;
				this.m_Observer.OnNext(evt);
				if (this.m_Remaining == 0)
				{
					this.m_Observer.OnCompleted();
					this.m_Observer = null;
				}
			}

			// Token: 0x04000C20 RID: 3104
			private IObserver<TValue> m_Observer;

			// Token: 0x04000C21 RID: 3105
			private int m_Remaining;
		}
	}
}
