using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Unity.VisualScripting
{
	// Token: 0x020000AA RID: 170
	[AddComponentMenu("")]
	public sealed class UnityOnEndDragMessageListener : MessageListener, IEndDragHandler, IEventSystemHandler
	{
		// Token: 0x06000425 RID: 1061 RVA: 0x00009A68 File Offset: 0x00007C68
		public void OnEndDrag(PointerEventData eventData)
		{
			EventBus.Trigger<PointerEventData>("OnEndDrag", base.gameObject, eventData);
		}
	}
}
