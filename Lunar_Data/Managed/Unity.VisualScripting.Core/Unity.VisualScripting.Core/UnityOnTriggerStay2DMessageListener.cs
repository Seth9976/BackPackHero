using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200009B RID: 155
	[AddComponentMenu("")]
	public sealed class UnityOnTriggerStay2DMessageListener : MessageListener
	{
		// Token: 0x060003FF RID: 1023 RVA: 0x00009794 File Offset: 0x00007994
		private void OnTriggerStay2D(Collider2D other)
		{
			EventBus.Trigger<Collider2D>("OnTriggerStay2D", base.gameObject, other);
		}
	}
}
