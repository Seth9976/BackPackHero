using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000138 RID: 312
	internal class Observer<TValue> : IObserver<TValue>
	{
		// Token: 0x06001121 RID: 4385 RVA: 0x000518A3 File Offset: 0x0004FAA3
		public Observer(Action<TValue> onNext, Action onCompleted = null)
		{
			this.m_OnNext = onNext;
			this.m_OnCompleted = onCompleted;
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x000518B9 File Offset: 0x0004FAB9
		public void OnCompleted()
		{
			Action onCompleted = this.m_OnCompleted;
			if (onCompleted == null)
			{
				return;
			}
			onCompleted();
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x000518CB File Offset: 0x0004FACB
		public void OnError(Exception error)
		{
			Debug.LogException(error);
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x000518D3 File Offset: 0x0004FAD3
		public void OnNext(TValue evt)
		{
			Action<TValue> onNext = this.m_OnNext;
			if (onNext == null)
			{
				return;
			}
			onNext(evt);
		}

		// Token: 0x040006CA RID: 1738
		private Action<TValue> m_OnNext;

		// Token: 0x040006CB RID: 1739
		private Action m_OnCompleted;
	}
}
