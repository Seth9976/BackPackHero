using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000138 RID: 312
	internal class Observer<TValue> : IObserver<TValue>
	{
		// Token: 0x0600111A RID: 4378 RVA: 0x0005168F File Offset: 0x0004F88F
		public Observer(Action<TValue> onNext, Action onCompleted = null)
		{
			this.m_OnNext = onNext;
			this.m_OnCompleted = onCompleted;
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x000516A5 File Offset: 0x0004F8A5
		public void OnCompleted()
		{
			Action onCompleted = this.m_OnCompleted;
			if (onCompleted == null)
			{
				return;
			}
			onCompleted();
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x000516B7 File Offset: 0x0004F8B7
		public void OnError(Exception error)
		{
			Debug.LogException(error);
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x000516BF File Offset: 0x0004F8BF
		public void OnNext(TValue evt)
		{
			Action<TValue> onNext = this.m_OnNext;
			if (onNext == null)
			{
				return;
			}
			onNext(evt);
		}

		// Token: 0x040006C9 RID: 1737
		private Action<TValue> m_OnNext;

		// Token: 0x040006CA RID: 1738
		private Action m_OnCompleted;
	}
}
