using System;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.VisualScripting
{
	// Token: 0x020000A2 RID: 162
	[AddComponentMenu("")]
	public sealed class UnityOnScrollRectValueChangedMessageListener : MessageListener
	{
		// Token: 0x06000412 RID: 1042 RVA: 0x00009918 File Offset: 0x00007B18
		private void Start()
		{
			ScrollRect component = base.GetComponent<ScrollRect>();
			if (component == null)
			{
				return;
			}
			ScrollRect.ScrollRectEvent onValueChanged = component.onValueChanged;
			if (onValueChanged == null)
			{
				return;
			}
			onValueChanged.AddListener(delegate(Vector2 value)
			{
				EventBus.Trigger<Vector2>("OnScrollRectValueChanged", base.gameObject, value);
			});
		}
	}
}
