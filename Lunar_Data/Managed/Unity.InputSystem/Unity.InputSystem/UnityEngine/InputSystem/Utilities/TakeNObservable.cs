using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200013A RID: 314
	internal class TakeNObservable<TValue> : IObservable<TValue>
	{
		// Token: 0x06001120 RID: 4384 RVA: 0x000516FC File Offset: 0x0004F8FC
		public TakeNObservable(IObservable<TValue> source, int count)
		{
			this.m_Source = source;
			this.m_Count = count;
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00051712 File Offset: 0x0004F912
		public IDisposable Subscribe(IObserver<TValue> observer)
		{
			return this.m_Source.Subscribe(new TakeNObservable<TValue>.Take(this, observer));
		}

		// Token: 0x040006CD RID: 1741
		private IObservable<TValue> m_Source;

		// Token: 0x040006CE RID: 1742
		private int m_Count;

		// Token: 0x02000242 RID: 578
		private class Take : IObserver<TValue>
		{
			// Token: 0x060015C2 RID: 5570 RVA: 0x00063A1C File Offset: 0x00061C1C
			public Take(TakeNObservable<TValue> observable, IObserver<TValue> observer)
			{
				this.m_Observer = observer;
				this.m_Remaining = observable.m_Count;
			}

			// Token: 0x060015C3 RID: 5571 RVA: 0x00063A37 File Offset: 0x00061C37
			public void OnCompleted()
			{
			}

			// Token: 0x060015C4 RID: 5572 RVA: 0x00063A39 File Offset: 0x00061C39
			public void OnError(Exception error)
			{
				Debug.LogException(error);
			}

			// Token: 0x060015C5 RID: 5573 RVA: 0x00063A41 File Offset: 0x00061C41
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

			// Token: 0x04000C1F RID: 3103
			private IObserver<TValue> m_Observer;

			// Token: 0x04000C20 RID: 3104
			private int m_Remaining;
		}
	}
}
