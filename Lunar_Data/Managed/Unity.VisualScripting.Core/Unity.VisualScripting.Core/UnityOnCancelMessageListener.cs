using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Unity.VisualScripting
{
	// Token: 0x020000A6 RID: 166
	[AddComponentMenu("")]
	public sealed class UnityOnCancelMessageListener : MessageListener, ICancelHandler, IEventSystemHandler
	{
		// Token: 0x0600041D RID: 1053 RVA: 0x000099FC File Offset: 0x00007BFC
		public void OnCancel(BaseEventData eventData)
		{
			EventBus.Trigger<BaseEventData>("OnCancel", base.gameObject, eventData);
		}
	}
}
