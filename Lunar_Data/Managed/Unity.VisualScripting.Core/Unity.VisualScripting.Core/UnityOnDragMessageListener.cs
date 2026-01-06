using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Unity.VisualScripting
{
	// Token: 0x020000A8 RID: 168
	[AddComponentMenu("")]
	public sealed class UnityOnDragMessageListener : MessageListener, IDragHandler, IEventSystemHandler
	{
		// Token: 0x06000421 RID: 1057 RVA: 0x00009A32 File Offset: 0x00007C32
		public void OnDrag(PointerEventData eventData)
		{
			EventBus.Trigger<PointerEventData>("OnDrag", base.gameObject, eventData);
		}
	}
}
