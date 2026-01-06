using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000096 RID: 150
	[AddComponentMenu("")]
	public sealed class UnityOnTransformParentChangedMessageListener : MessageListener
	{
		// Token: 0x060003F5 RID: 1013 RVA: 0x0000970E File Offset: 0x0000790E
		private void OnTransformParentChanged()
		{
			EventBus.Trigger("OnTransformParentChanged", base.gameObject);
		}
	}
}
