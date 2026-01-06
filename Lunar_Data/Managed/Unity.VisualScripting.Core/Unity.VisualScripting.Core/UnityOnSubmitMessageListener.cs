using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Unity.VisualScripting
{
	// Token: 0x020000B3 RID: 179
	[AddComponentMenu("")]
	public sealed class UnityOnSubmitMessageListener : MessageListener, ISubmitHandler, IEventSystemHandler
	{
		// Token: 0x06000437 RID: 1079 RVA: 0x00009B5B File Offset: 0x00007D5B
		public void OnSubmit(BaseEventData eventData)
		{
			EventBus.Trigger<BaseEventData>("OnSubmit", base.gameObject, eventData);
		}
	}
}
