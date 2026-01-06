using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000083 RID: 131
	[AddComponentMenu("")]
	public sealed class UnityOnBecameVisibleMessageListener : MessageListener
	{
		// Token: 0x060003CF RID: 975 RVA: 0x00009516 File Offset: 0x00007716
		private void OnBecameVisible()
		{
			EventBus.Trigger("OnBecameVisible", base.gameObject);
		}
	}
}
