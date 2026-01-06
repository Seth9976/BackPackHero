using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000097 RID: 151
	[AddComponentMenu("")]
	public sealed class UnityOnTriggerEnter2DMessageListener : MessageListener
	{
		// Token: 0x060003F7 RID: 1015 RVA: 0x00009728 File Offset: 0x00007928
		private void OnTriggerEnter2D(Collider2D other)
		{
			EventBus.Trigger<Collider2D>("OnTriggerEnter2D", base.gameObject, other);
		}
	}
}
