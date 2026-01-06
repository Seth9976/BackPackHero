using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Unity.VisualScripting
{
	// Token: 0x020000B1 RID: 177
	[AddComponentMenu("")]
	public sealed class UnityOnScrollMessageListener : MessageListener, IScrollHandler, IEventSystemHandler
	{
		// Token: 0x06000433 RID: 1075 RVA: 0x00009B25 File Offset: 0x00007D25
		public void OnScroll(PointerEventData eventData)
		{
			EventBus.Trigger<PointerEventData>("OnScroll", base.gameObject, eventData);
		}
	}
}
