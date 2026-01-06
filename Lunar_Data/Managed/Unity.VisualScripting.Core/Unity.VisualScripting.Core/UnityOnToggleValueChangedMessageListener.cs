using System;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.VisualScripting
{
	// Token: 0x020000A4 RID: 164
	[AddComponentMenu("")]
	public sealed class UnityOnToggleValueChangedMessageListener : MessageListener
	{
		// Token: 0x06000418 RID: 1048 RVA: 0x0000999E File Offset: 0x00007B9E
		private void Start()
		{
			Toggle component = base.GetComponent<Toggle>();
			if (component == null)
			{
				return;
			}
			Toggle.ToggleEvent onValueChanged = component.onValueChanged;
			if (onValueChanged == null)
			{
				return;
			}
			onValueChanged.AddListener(delegate(bool value)
			{
				EventBus.Trigger<bool>("OnToggleValueChanged", base.gameObject, value);
			});
		}
	}
}
