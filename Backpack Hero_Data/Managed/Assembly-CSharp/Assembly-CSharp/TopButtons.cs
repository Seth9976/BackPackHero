using System;
using UnityEngine;

// Token: 0x02000172 RID: 370
public class TopButtons : MonoBehaviour
{
	// Token: 0x06000EFD RID: 3837 RVA: 0x00094378 File Offset: 0x00092578
	private void Start()
	{
		this.first = true;
		this.lastOne = DigitalCursor.main.controlStyle;
		this.lastFakeController = DigitalCursor.main.fakeController;
	}

	// Token: 0x06000EFE RID: 3838 RVA: 0x000943A4 File Offset: 0x000925A4
	private void Update()
	{
		if ((DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.cursor || DigitalCursor.main.fakeController) && (this.first || this.lastOne != DigitalCursor.main.controlStyle || this.lastFakeController != DigitalCursor.main.fakeController))
		{
			foreach (object obj in base.transform)
			{
				((Transform)obj).gameObject.SetActive(true);
			}
			this.lastOne = DigitalCursor.main.controlStyle;
			this.lastFakeController = DigitalCursor.main.fakeController;
		}
		else if (DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.controller && (this.first || this.lastOne != DigitalCursor.main.controlStyle || this.lastFakeController != DigitalCursor.main.fakeController))
		{
			foreach (object obj2 in base.transform)
			{
				((Transform)obj2).gameObject.SetActive(false);
			}
			this.lastOne = DigitalCursor.main.controlStyle;
			this.lastFakeController = DigitalCursor.main.fakeController;
		}
		this.first = false;
	}

	// Token: 0x04000C27 RID: 3111
	private DigitalCursor.ControlStyle lastOne;

	// Token: 0x04000C28 RID: 3112
	private bool first;

	// Token: 0x04000C29 RID: 3113
	private bool lastFakeController;
}
