using System;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.VisualScripting
{
	// Token: 0x020000A1 RID: 161
	[AddComponentMenu("")]
	public sealed class UnityOnScrollbarValueChangedMessageListener : MessageListener
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x000098D5 File Offset: 0x00007AD5
		private void Start()
		{
			Scrollbar component = base.GetComponent<Scrollbar>();
			if (component == null)
			{
				return;
			}
			Scrollbar.ScrollEvent onValueChanged = component.onValueChanged;
			if (onValueChanged == null)
			{
				return;
			}
			onValueChanged.AddListener(delegate(float value)
			{
				EventBus.Trigger<float>("OnScrollbarValueChanged", base.gameObject, value);
			});
		}
	}
}
