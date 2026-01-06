using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000089 RID: 137
	[AddComponentMenu("")]
	public sealed class UnityOnCollisionStayMessageListener : MessageListener
	{
		// Token: 0x060003DB RID: 987 RVA: 0x000095B7 File Offset: 0x000077B7
		private void OnCollisionStay(Collision collision)
		{
			EventBus.Trigger<Collision>("OnCollisionStay", base.gameObject, collision);
		}
	}
}
