using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000082 RID: 130
	[AddComponentMenu("")]
	public sealed class UnityOnBecameInvisibleMessageListener : MessageListener
	{
		// Token: 0x060003CD RID: 973 RVA: 0x000094FC File Offset: 0x000076FC
		private void OnBecameInvisible()
		{
			EventBus.Trigger("OnBecameInvisible", base.gameObject);
		}
	}
}
