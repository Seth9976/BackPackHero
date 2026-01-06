using System;
using UnityEngine.Events;

namespace CleverCrow.Fluid.Utilities.UnityEvents
{
	// Token: 0x02000009 RID: 9
	public class UnityEventPlus<T1, T2, T3> : UnityEvent<T1, T2, T3>, IUnityEvent<T1, T2, T3>
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000020D9 File Offset: 0x000002D9
		void IUnityEvent<T1, T2, T3>.AddListener(UnityAction<T1, T2, T3> call)
		{
			base.AddListener(call);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000020E2 File Offset: 0x000002E2
		void IUnityEvent<T1, T2, T3>.RemoveListener(UnityAction<T1, T2, T3> call)
		{
			base.RemoveListener(call);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000020EB File Offset: 0x000002EB
		void IUnityEvent<T1, T2, T3>.Invoke(T1 arg1, T2 arg2, T3 arg3)
		{
			base.Invoke(arg1, arg2, arg3);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000020F6 File Offset: 0x000002F6
		void IUnityEvent<T1, T2, T3>.RemoveAllListeners()
		{
			base.RemoveAllListeners();
		}
	}
}
