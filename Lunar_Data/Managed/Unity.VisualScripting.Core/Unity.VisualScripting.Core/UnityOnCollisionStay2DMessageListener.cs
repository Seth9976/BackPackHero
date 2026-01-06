using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000088 RID: 136
	[AddComponentMenu("")]
	public sealed class UnityOnCollisionStay2DMessageListener : MessageListener
	{
		// Token: 0x060003D9 RID: 985 RVA: 0x0000959C File Offset: 0x0000779C
		private void OnCollisionStay2D(Collision2D collision)
		{
			EventBus.Trigger<Collision2D>("OnCollisionStay2D", base.gameObject, collision);
		}
	}
}
