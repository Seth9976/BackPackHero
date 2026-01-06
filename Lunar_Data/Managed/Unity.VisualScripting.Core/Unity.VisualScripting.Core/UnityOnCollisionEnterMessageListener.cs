using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000085 RID: 133
	[AddComponentMenu("")]
	public sealed class UnityOnCollisionEnterMessageListener : MessageListener
	{
		// Token: 0x060003D3 RID: 979 RVA: 0x0000954B File Offset: 0x0000774B
		private void OnCollisionEnter(Collision collision)
		{
			EventBus.Trigger<Collision>("OnCollisionEnter", base.gameObject, collision);
		}
	}
}
