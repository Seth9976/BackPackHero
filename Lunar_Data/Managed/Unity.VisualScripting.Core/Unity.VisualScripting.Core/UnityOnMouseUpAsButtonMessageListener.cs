using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000092 RID: 146
	[AddComponentMenu("")]
	public sealed class UnityOnMouseUpAsButtonMessageListener : MessageListener
	{
		// Token: 0x060003ED RID: 1005 RVA: 0x000096A5 File Offset: 0x000078A5
		private void OnMouseUpAsButton()
		{
			EventBus.Trigger("OnMouseUpAsButton", base.gameObject);
		}
	}
}
