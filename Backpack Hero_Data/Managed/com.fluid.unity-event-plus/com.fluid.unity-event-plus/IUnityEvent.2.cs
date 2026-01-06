using System;
using UnityEngine.Events;

namespace CleverCrow.Fluid.Utilities.UnityEvents
{
	// Token: 0x02000003 RID: 3
	public interface IUnityEvent<T1>
	{
		// Token: 0x06000005 RID: 5
		void AddListener(UnityAction<T1> call);

		// Token: 0x06000006 RID: 6
		void RemoveListener(UnityAction<T1> call);

		// Token: 0x06000007 RID: 7
		void Invoke(T1 arg1);

		// Token: 0x06000008 RID: 8
		void RemoveAllListeners();
	}
}
