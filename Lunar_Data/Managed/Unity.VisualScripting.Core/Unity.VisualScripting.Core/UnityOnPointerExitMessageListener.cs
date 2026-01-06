using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Unity.VisualScripting
{
	// Token: 0x020000AF RID: 175
	[AddComponentMenu("")]
	public sealed class UnityOnPointerExitMessageListener : MessageListener, IPointerExitHandler, IEventSystemHandler
	{
		// Token: 0x0600042F RID: 1071 RVA: 0x00009AEF File Offset: 0x00007CEF
		public void OnPointerExit(PointerEventData eventData)
		{
			EventBus.Trigger<PointerEventData>("OnPointerExit", base.gameObject, eventData);
		}
	}
}
