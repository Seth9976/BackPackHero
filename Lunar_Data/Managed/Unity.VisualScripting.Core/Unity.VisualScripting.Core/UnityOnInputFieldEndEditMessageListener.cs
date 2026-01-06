using System;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.VisualScripting
{
	// Token: 0x0200009F RID: 159
	[AddComponentMenu("")]
	public sealed class UnityOnInputFieldEndEditMessageListener : MessageListener
	{
		// Token: 0x06000409 RID: 1033 RVA: 0x0000984F File Offset: 0x00007A4F
		private void Start()
		{
			InputField component = base.GetComponent<InputField>();
			if (component == null)
			{
				return;
			}
			InputField.EndEditEvent onEndEdit = component.onEndEdit;
			if (onEndEdit == null)
			{
				return;
			}
			onEndEdit.AddListener(delegate(string value)
			{
				EventBus.Trigger<string>("OnInputFieldEndEdit", base.gameObject, value);
			});
		}
	}
}
