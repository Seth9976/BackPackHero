using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000084 RID: 132
	[AddComponentMenu("")]
	public sealed class UnityOnCollisionEnter2DMessageListener : MessageListener
	{
		// Token: 0x060003D1 RID: 977 RVA: 0x00009530 File Offset: 0x00007730
		private void OnCollisionEnter2D(Collision2D collision)
		{
			EventBus.Trigger<Collision2D>("OnCollisionEnter2D", base.gameObject, collision);
		}
	}
}
