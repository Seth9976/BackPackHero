using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Unity.VisualScripting
{
	// Token: 0x020000A9 RID: 169
	[AddComponentMenu("")]
	public sealed class UnityOnDropMessageListener : MessageListener, IDropHandler, IEventSystemHandler
	{
		// Token: 0x06000423 RID: 1059 RVA: 0x00009A4D File Offset: 0x00007C4D
		public void OnDrop(PointerEventData eventData)
		{
			EventBus.Trigger<PointerEventData>("OnDrop", base.gameObject, eventData);
		}
	}
}
