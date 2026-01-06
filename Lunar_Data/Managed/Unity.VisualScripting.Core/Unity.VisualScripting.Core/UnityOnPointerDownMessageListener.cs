using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Unity.VisualScripting
{
	// Token: 0x020000AD RID: 173
	[AddComponentMenu("")]
	public sealed class UnityOnPointerDownMessageListener : MessageListener, IPointerDownHandler, IEventSystemHandler
	{
		// Token: 0x0600042B RID: 1067 RVA: 0x00009AB9 File Offset: 0x00007CB9
		public void OnPointerDown(PointerEventData eventData)
		{
			EventBus.Trigger<PointerEventData>("OnPointerDown", base.gameObject, eventData);
		}
	}
}
