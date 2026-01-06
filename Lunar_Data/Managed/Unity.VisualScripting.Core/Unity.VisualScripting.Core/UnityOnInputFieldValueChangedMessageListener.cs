using System;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.VisualScripting
{
	// Token: 0x020000A0 RID: 160
	[AddComponentMenu("")]
	public sealed class UnityOnInputFieldValueChangedMessageListener : MessageListener
	{
		// Token: 0x0600040C RID: 1036 RVA: 0x00009892 File Offset: 0x00007A92
		private void Start()
		{
			InputField component = base.GetComponent<InputField>();
			if (component == null)
			{
				return;
			}
			InputField.OnChangeEvent onValueChanged = component.onValueChanged;
			if (onValueChanged == null)
			{
				return;
			}
			onValueChanged.AddListener(delegate(string value)
			{
				EventBus.Trigger<string>("OnInputFieldValueChanged", base.gameObject, value);
			});
		}
	}
}
