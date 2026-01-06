using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200008A RID: 138
	[AddComponentMenu("")]
	public sealed class UnityOnControllerColliderHitMessageListener : MessageListener
	{
		// Token: 0x060003DD RID: 989 RVA: 0x000095D2 File Offset: 0x000077D2
		private void OnControllerColliderHit(ControllerColliderHit hit)
		{
			EventBus.Trigger<ControllerColliderHit>("OnControllerColliderHit", base.gameObject, hit);
		}
	}
}
