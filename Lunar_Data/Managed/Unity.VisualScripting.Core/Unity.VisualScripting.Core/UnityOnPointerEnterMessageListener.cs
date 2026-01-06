using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Unity.VisualScripting
{
	// Token: 0x020000AE RID: 174
	[AddComponentMenu("")]
	public sealed class UnityOnPointerEnterMessageListener : MessageListener, IPointerEnterHandler, IEventSystemHandler
	{
		// Token: 0x0600042D RID: 1069 RVA: 0x00009AD4 File Offset: 0x00007CD4
		public void OnPointerEnter(PointerEventData eventData)
		{
			EventBus.Trigger<PointerEventData>("OnPointerEnter", base.gameObject, eventData);
		}
	}
}
