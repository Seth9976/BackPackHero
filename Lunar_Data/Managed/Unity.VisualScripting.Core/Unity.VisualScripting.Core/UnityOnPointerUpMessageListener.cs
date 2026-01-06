using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Unity.VisualScripting
{
	// Token: 0x020000B0 RID: 176
	[AddComponentMenu("")]
	public sealed class UnityOnPointerUpMessageListener : MessageListener, IPointerUpHandler, IEventSystemHandler
	{
		// Token: 0x06000431 RID: 1073 RVA: 0x00009B0A File Offset: 0x00007D0A
		public void OnPointerUp(PointerEventData eventData)
		{
			EventBus.Trigger<PointerEventData>("OnPointerUp", base.gameObject, eventData);
		}
	}
}
