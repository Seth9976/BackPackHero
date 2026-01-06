using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000022 RID: 34
public class DeselectText : MonoBehaviour
{
	// Token: 0x060000DD RID: 221 RVA: 0x00007013 File Offset: 0x00005213
	private void Start()
	{
	}

	// Token: 0x060000DE RID: 222 RVA: 0x00007015 File Offset: 0x00005215
	private void Update()
	{
		if (EventSystem.current && EventSystem.current.currentSelectedGameObject == base.gameObject && DigitalCursor.main.GetInputDown("cancel"))
		{
			DigitalCursor.main.SelectPreviousUIElement();
		}
	}
}
