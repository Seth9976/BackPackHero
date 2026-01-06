using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200008E RID: 142
	[AddComponentMenu("")]
	public sealed class UnityOnMouseDragMessageListener : MessageListener
	{
		// Token: 0x060003E5 RID: 997 RVA: 0x0000963D File Offset: 0x0000783D
		private void OnMouseDrag()
		{
			EventBus.Trigger("OnMouseDrag", base.gameObject);
		}
	}
}
