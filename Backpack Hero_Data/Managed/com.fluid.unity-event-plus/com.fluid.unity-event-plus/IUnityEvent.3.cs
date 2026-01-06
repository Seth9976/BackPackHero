using System;
using UnityEngine.Events;

namespace CleverCrow.Fluid.Utilities.UnityEvents
{
	// Token: 0x02000004 RID: 4
	public interface IUnityEvent<T1, T2>
	{
		// Token: 0x06000009 RID: 9
		void AddListener(UnityAction<T1, T2> call);

		// Token: 0x0600000A RID: 10
		void RemoveListener(UnityAction<T1, T2> call);

		// Token: 0x0600000B RID: 11
		void Invoke(T1 arg1, T2 arg2);

		// Token: 0x0600000C RID: 12
		void RemoveAllListeners();
	}
}
