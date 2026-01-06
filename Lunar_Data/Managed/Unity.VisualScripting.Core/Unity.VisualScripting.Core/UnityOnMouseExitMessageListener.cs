using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000090 RID: 144
	[AddComponentMenu("")]
	public sealed class UnityOnMouseExitMessageListener : MessageListener
	{
		// Token: 0x060003E9 RID: 1001 RVA: 0x00009671 File Offset: 0x00007871
		private void OnMouseExit()
		{
			EventBus.Trigger("OnMouseExit", base.gameObject);
		}
	}
}
