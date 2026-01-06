using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000087 RID: 135
	[AddComponentMenu("")]
	public sealed class UnityOnCollisionExitMessageListener : MessageListener
	{
		// Token: 0x060003D7 RID: 983 RVA: 0x00009581 File Offset: 0x00007781
		private void OnCollisionExit(Collision collision)
		{
			EventBus.Trigger<Collision>("OnCollisionExit", base.gameObject, collision);
		}
	}
}
