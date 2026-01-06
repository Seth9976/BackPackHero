using System;
using UnityEngine;

// Token: 0x020000AE RID: 174
public class DeckButton : CustomInputHandler
{
	// Token: 0x06000435 RID: 1077 RVA: 0x00029A99 File Offset: 0x00027C99
	private void Start()
	{
	}

	// Token: 0x06000436 RID: 1078 RVA: 0x00029A9B File Offset: 0x00027C9B
	private void Update()
	{
	}

	// Token: 0x06000437 RID: 1079 RVA: 0x00029A9D File Offset: 0x00027C9D
	private void OnMouseUpAsButton()
	{
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		this.ShowCards();
	}

	// Token: 0x06000438 RID: 1080 RVA: 0x00029AB2 File Offset: 0x00027CB2
	public override void OnPressStart(string keyName, bool overrideKeyName)
	{
		if (keyName == "confirm" || overrideKeyName)
		{
			this.ShowCards();
		}
	}

	// Token: 0x06000439 RID: 1081 RVA: 0x00029AC9 File Offset: 0x00027CC9
	public void ShowCards()
	{
		if (SingleUI.IsViewingPopUp())
		{
			return;
		}
		Object.FindObjectOfType<Tote>().ButtonForShowItems(this.type);
	}

	// Token: 0x04000334 RID: 820
	[SerializeField]
	private int type;
}
