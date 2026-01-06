using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000095 RID: 149
	[AddComponentMenu("")]
	public sealed class UnityOnTransformChildrenChangedMessageListener : MessageListener
	{
		// Token: 0x060003F3 RID: 1011 RVA: 0x000096F4 File Offset: 0x000078F4
		private void OnTransformChildrenChanged()
		{
			EventBus.Trigger("OnTransformChildrenChanged", base.gameObject);
		}
	}
}
