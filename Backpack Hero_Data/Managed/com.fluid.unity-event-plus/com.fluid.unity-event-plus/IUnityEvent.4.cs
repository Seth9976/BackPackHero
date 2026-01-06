using System;
using UnityEngine.Events;

namespace CleverCrow.Fluid.Utilities.UnityEvents
{
	// Token: 0x02000005 RID: 5
	public interface IUnityEvent<T1, T2, T3>
	{
		// Token: 0x0600000D RID: 13
		void AddListener(UnityAction<T1, T2, T3> call);

		// Token: 0x0600000E RID: 14
		void RemoveListener(UnityAction<T1, T2, T3> call);

		// Token: 0x0600000F RID: 15
		void Invoke(T1 arg1, T2 arg2, T3 arg3);

		// Token: 0x06000010 RID: 16
		void RemoveAllListeners();
	}
}
