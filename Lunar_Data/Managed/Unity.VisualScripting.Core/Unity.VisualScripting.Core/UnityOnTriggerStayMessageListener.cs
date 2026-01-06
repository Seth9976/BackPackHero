using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200009C RID: 156
	[AddComponentMenu("")]
	public sealed class UnityOnTriggerStayMessageListener : MessageListener
	{
		// Token: 0x06000401 RID: 1025 RVA: 0x000097AF File Offset: 0x000079AF
		private void OnTriggerStay(Collider other)
		{
			EventBus.Trigger<Collider>("OnTriggerStay", base.gameObject, other);
		}
	}
}
