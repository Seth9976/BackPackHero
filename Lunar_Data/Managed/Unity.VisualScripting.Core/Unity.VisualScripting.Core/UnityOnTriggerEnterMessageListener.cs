using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000098 RID: 152
	[AddComponentMenu("")]
	public sealed class UnityOnTriggerEnterMessageListener : MessageListener
	{
		// Token: 0x060003F9 RID: 1017 RVA: 0x00009743 File Offset: 0x00007943
		private void OnTriggerEnter(Collider other)
		{
			EventBus.Trigger<Collider>("OnTriggerEnter", base.gameObject, other);
		}
	}
}
