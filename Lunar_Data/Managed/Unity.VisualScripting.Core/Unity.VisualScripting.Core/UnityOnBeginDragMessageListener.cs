using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Unity.VisualScripting
{
	// Token: 0x020000A5 RID: 165
	[AddComponentMenu("")]
	public sealed class UnityOnBeginDragMessageListener : MessageListener, IBeginDragHandler, IEventSystemHandler
	{
		// Token: 0x0600041B RID: 1051 RVA: 0x000099E1 File Offset: 0x00007BE1
		public void OnBeginDrag(PointerEventData eventData)
		{
			EventBus.Trigger<PointerEventData>("OnBeginDrag", base.gameObject, eventData);
		}
	}
}
