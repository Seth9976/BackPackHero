using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000094 RID: 148
	[AddComponentMenu("")]
	public sealed class UnityOnParticleCollisionMessageListener : MessageListener
	{
		// Token: 0x060003F1 RID: 1009 RVA: 0x000096D9 File Offset: 0x000078D9
		private void OnParticleCollision(GameObject other)
		{
			EventBus.Trigger<GameObject>("OnParticleCollision", base.gameObject, other);
		}
	}
}
