using System;
using UnityEngine.Events;

namespace CleverCrow.Fluid.Utilities.UnityEvents
{
	// Token: 0x02000008 RID: 8
	public class UnityEventPlus<T1, T2> : UnityEvent<T1, T2>, IUnityEvent<T1, T2>
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000020AD File Offset: 0x000002AD
		void IUnityEvent<T1, T2>.AddListener(UnityAction<T1, T2> call)
		{
			base.AddListener(call);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000020B6 File Offset: 0x000002B6
		void IUnityEvent<T1, T2>.RemoveListener(UnityAction<T1, T2> call)
		{
			base.RemoveListener(call);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000020BF File Offset: 0x000002BF
		void IUnityEvent<T1, T2>.Invoke(T1 arg1, T2 arg2)
		{
			base.Invoke(arg1, arg2);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000020C9 File Offset: 0x000002C9
		void IUnityEvent<T1, T2>.RemoveAllListeners()
		{
			base.RemoveAllListeners();
		}
	}
}
