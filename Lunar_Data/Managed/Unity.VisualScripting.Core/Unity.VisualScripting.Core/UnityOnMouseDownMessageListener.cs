using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200008D RID: 141
	[AddComponentMenu("")]
	public sealed class UnityOnMouseDownMessageListener : MessageListener
	{
		// Token: 0x060003E3 RID: 995 RVA: 0x00009623 File Offset: 0x00007823
		private void OnMouseDown()
		{
			EventBus.Trigger("OnMouseDown", base.gameObject);
		}
	}
}
