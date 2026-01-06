using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Unity.VisualScripting
{
	// Token: 0x020000A7 RID: 167
	[AddComponentMenu("")]
	public sealed class UnityOnDeselectMessageListener : MessageListener, IDeselectHandler, IEventSystemHandler
	{
		// Token: 0x0600041F RID: 1055 RVA: 0x00009A17 File Offset: 0x00007C17
		public void OnDeselect(BaseEventData eventData)
		{
			EventBus.Trigger<BaseEventData>("OnDeselect", base.gameObject, eventData);
		}
	}
}
