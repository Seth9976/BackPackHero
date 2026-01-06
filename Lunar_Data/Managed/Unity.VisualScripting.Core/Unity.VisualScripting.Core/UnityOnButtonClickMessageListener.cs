using System;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.VisualScripting
{
	// Token: 0x0200009D RID: 157
	[AddComponentMenu("")]
	public sealed class UnityOnButtonClickMessageListener : MessageListener
	{
		// Token: 0x06000403 RID: 1027 RVA: 0x000097CA File Offset: 0x000079CA
		private void Start()
		{
			Button component = base.GetComponent<Button>();
			if (component == null)
			{
				return;
			}
			Button.ButtonClickedEvent onClick = component.onClick;
			if (onClick == null)
			{
				return;
			}
			onClick.AddListener(delegate
			{
				EventBus.Trigger("OnButtonClick", base.gameObject);
			});
		}
	}
}
