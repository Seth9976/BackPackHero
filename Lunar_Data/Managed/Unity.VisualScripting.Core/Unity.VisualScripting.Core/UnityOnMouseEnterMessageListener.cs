using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200008F RID: 143
	[AddComponentMenu("")]
	public sealed class UnityOnMouseEnterMessageListener : MessageListener
	{
		// Token: 0x060003E7 RID: 999 RVA: 0x00009657 File Offset: 0x00007857
		private void OnMouseEnter()
		{
			EventBus.Trigger("OnMouseEnter", base.gameObject);
		}
	}
}
