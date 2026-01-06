using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000091 RID: 145
	[AddComponentMenu("")]
	public sealed class UnityOnMouseOverMessageListener : MessageListener
	{
		// Token: 0x060003EB RID: 1003 RVA: 0x0000968B File Offset: 0x0000788B
		private void OnMouseOver()
		{
			EventBus.Trigger("OnMouseOver", base.gameObject);
		}
	}
}
