using System;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Actions;
using UnityEngine;

// Token: 0x02000047 RID: 71
[CreateMenu("ShowSimpleUnlock", 0)]
public class ShowSimpleUnlock : ActionDataBase
{
	// Token: 0x06000139 RID: 313 RVA: 0x00008461 File Offset: 0x00006661
	public override void OnStart()
	{
		this.window = Overworld_Manager.main.OpenNewSimpleImageWindow(this.sprite, this.unlockName, this.cardName, this.cardText);
	}

	// Token: 0x0600013A RID: 314 RVA: 0x0000848B File Offset: 0x0000668B
	public override ActionStatus OnUpdate()
	{
		if (this.window && this.window.activeInHierarchy)
		{
			return ActionStatus.Continue;
		}
		return ActionStatus.Success;
	}

	// Token: 0x040000CB RID: 203
	public Sprite sprite;

	// Token: 0x040000CC RID: 204
	public string unlockName = "gm72d";

	// Token: 0x040000CD RID: 205
	public string cardName;

	// Token: 0x040000CE RID: 206
	public string cardText;

	// Token: 0x040000CF RID: 207
	private GameObject window;
}
