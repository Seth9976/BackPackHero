using System;
using UnityEngine.Events;

namespace CleverCrow.Fluid.Utilities.UnityEvents
{
	// Token: 0x02000007 RID: 7
	public class UnityEventPlus<T1> : UnityEvent<T1>, IUnityEvent<T1>
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002082 File Offset: 0x00000282
		void IUnityEvent<T1>.AddListener(UnityAction<T1> call)
		{
			base.AddListener(call);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000208B File Offset: 0x0000028B
		void IUnityEvent<T1>.RemoveListener(UnityAction<T1> call)
		{
			base.RemoveListener(call);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002094 File Offset: 0x00000294
		void IUnityEvent<T1>.Invoke(T1 arg1)
		{
			base.Invoke(arg1);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000209D File Offset: 0x0000029D
		void IUnityEvent<T1>.RemoveAllListeners()
		{
			base.RemoveAllListeners();
		}
	}
}
