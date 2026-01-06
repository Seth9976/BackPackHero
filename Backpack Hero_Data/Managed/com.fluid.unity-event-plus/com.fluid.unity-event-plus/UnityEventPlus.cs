using System;
using UnityEngine.Events;

namespace CleverCrow.Fluid.Utilities.UnityEvents
{
	// Token: 0x02000006 RID: 6
	[Serializable]
	public class UnityEventPlus : UnityEvent, IUnityEvent
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002058 File Offset: 0x00000258
		void IUnityEvent.AddListener(UnityAction call)
		{
			base.AddListener(call);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002061 File Offset: 0x00000261
		void IUnityEvent.RemoveListener(UnityAction call)
		{
			base.RemoveListener(call);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000206A File Offset: 0x0000026A
		void IUnityEvent.Invoke()
		{
			base.Invoke();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002072 File Offset: 0x00000272
		void IUnityEvent.RemoveAllListeners()
		{
			base.RemoveAllListeners();
		}
	}
}
