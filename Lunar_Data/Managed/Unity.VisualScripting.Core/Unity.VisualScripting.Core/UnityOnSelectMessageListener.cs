using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Unity.VisualScripting
{
	// Token: 0x020000B2 RID: 178
	[AddComponentMenu("")]
	public sealed class UnityOnSelectMessageListener : MessageListener, ISelectHandler, IEventSystemHandler
	{
		// Token: 0x06000435 RID: 1077 RVA: 0x00009B40 File Offset: 0x00007D40
		public void OnSelect(BaseEventData eventData)
		{
			EventBus.Trigger<BaseEventData>("OnSelect", base.gameObject, eventData);
		}
	}
}
