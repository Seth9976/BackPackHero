using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200009A RID: 154
	[AddComponentMenu("")]
	public sealed class UnityOnTriggerExitMessageListener : MessageListener
	{
		// Token: 0x060003FD RID: 1021 RVA: 0x00009779 File Offset: 0x00007979
		private void OnTriggerExit(Collider other)
		{
			EventBus.Trigger<Collider>("OnTriggerExit", base.gameObject, other);
		}
	}
}
