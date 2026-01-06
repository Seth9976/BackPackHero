using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000086 RID: 134
	[AddComponentMenu("")]
	public sealed class UnityOnCollisionExit2DMessageListener : MessageListener
	{
		// Token: 0x060003D5 RID: 981 RVA: 0x00009566 File Offset: 0x00007766
		private void OnCollisionExit2D(Collision2D collision)
		{
			EventBus.Trigger<Collision2D>("OnCollisionExit2D", base.gameObject, collision);
		}
	}
}
