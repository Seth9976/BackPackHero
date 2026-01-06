using System;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.VisualScripting
{
	// Token: 0x020000A3 RID: 163
	[AddComponentMenu("")]
	public sealed class UnityOnSliderValueChangedMessageListener : MessageListener
	{
		// Token: 0x06000415 RID: 1045 RVA: 0x0000995B File Offset: 0x00007B5B
		private void Start()
		{
			Slider component = base.GetComponent<Slider>();
			if (component == null)
			{
				return;
			}
			Slider.SliderEvent onValueChanged = component.onValueChanged;
			if (onValueChanged == null)
			{
				return;
			}
			onValueChanged.AddListener(delegate(float value)
			{
				EventBus.Trigger<float>("OnSliderValueChanged", base.gameObject, value);
			});
		}
	}
}
