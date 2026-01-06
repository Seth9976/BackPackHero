using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Unity.VisualScripting
{
	// Token: 0x020000AC RID: 172
	[AddComponentMenu("")]
	public sealed class UnityOnPointerClickMessageListener : MessageListener, IPointerClickHandler, IEventSystemHandler
	{
		// Token: 0x06000429 RID: 1065 RVA: 0x00009A9E File Offset: 0x00007C9E
		public void OnPointerClick(PointerEventData eventData)
		{
			EventBus.Trigger<PointerEventData>("OnPointerClick", base.gameObject, eventData);
		}
	}
}
