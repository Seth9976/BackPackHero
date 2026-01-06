using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000099 RID: 153
	[AddComponentMenu("")]
	public sealed class UnityOnTriggerExit2DMessageListener : MessageListener
	{
		// Token: 0x060003FB RID: 1019 RVA: 0x0000975E File Offset: 0x0000795E
		private void OnTriggerExit2D(Collider2D other)
		{
			EventBus.Trigger<Collider2D>("OnTriggerExit2D", base.gameObject, other);
		}
	}
}
