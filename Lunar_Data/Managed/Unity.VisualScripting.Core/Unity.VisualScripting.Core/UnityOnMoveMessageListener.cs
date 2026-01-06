using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Unity.VisualScripting
{
	// Token: 0x020000AB RID: 171
	[AddComponentMenu("")]
	public sealed class UnityOnMoveMessageListener : MessageListener, IMoveHandler, IEventSystemHandler
	{
		// Token: 0x06000427 RID: 1063 RVA: 0x00009A83 File Offset: 0x00007C83
		public void OnMove(AxisEventData eventData)
		{
			EventBus.Trigger<AxisEventData>("OnMove", base.gameObject, eventData);
		}
	}
}
