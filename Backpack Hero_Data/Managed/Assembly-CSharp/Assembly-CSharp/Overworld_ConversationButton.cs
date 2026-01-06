using System;
using UnityEngine;

// Token: 0x0200013F RID: 319
public class Overworld_ConversationButton : MonoBehaviour
{
	// Token: 0x06000C18 RID: 3096 RVA: 0x0007D4DC File Offset: 0x0007B6DC
	private void Start()
	{
	}

	// Token: 0x06000C19 RID: 3097 RVA: 0x0007D4DE File Offset: 0x0007B6DE
	private void Update()
	{
	}

	// Token: 0x06000C1A RID: 3098 RVA: 0x0007D4E0 File Offset: 0x0007B6E0
	public void PressButton()
	{
		if (this.isDialogueButton)
		{
			Overworld_ConversationManager.main.GetDialogueOption(this.dialogueButtonIndex);
			return;
		}
	}

	// Token: 0x040009C5 RID: 2501
	public string text;

	// Token: 0x040009C6 RID: 2502
	public bool isDialogueButton;

	// Token: 0x040009C7 RID: 2503
	public int dialogueButtonIndex;
}
