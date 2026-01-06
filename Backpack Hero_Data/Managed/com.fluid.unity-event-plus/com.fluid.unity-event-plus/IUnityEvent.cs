using System;
using UnityEngine.Events;

namespace CleverCrow.Fluid.Utilities.UnityEvents
{
	// Token: 0x02000002 RID: 2
	public interface IUnityEvent
	{
		// Token: 0x06000001 RID: 1
		void AddListener(UnityAction call);

		// Token: 0x06000002 RID: 2
		void RemoveListener(UnityAction call);

		// Token: 0x06000003 RID: 3
		void Invoke();

		// Token: 0x06000004 RID: 4
		void RemoveAllListeners();
	}
}
