using System;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.VisualScripting
{
	// Token: 0x0200009E RID: 158
	[AddComponentMenu("")]
	public sealed class UnityOnDropdownValueChangedMessageListener : MessageListener
	{
		// Token: 0x06000406 RID: 1030 RVA: 0x0000980C File Offset: 0x00007A0C
		private void Start()
		{
			Dropdown component = base.GetComponent<Dropdown>();
			if (component == null)
			{
				return;
			}
			Dropdown.DropdownEvent onValueChanged = component.onValueChanged;
			if (onValueChanged == null)
			{
				return;
			}
			onValueChanged.AddListener(delegate(int value)
			{
				EventBus.Trigger<int>("OnDropdownValueChanged", base.gameObject, value);
			});
		}
	}
}
