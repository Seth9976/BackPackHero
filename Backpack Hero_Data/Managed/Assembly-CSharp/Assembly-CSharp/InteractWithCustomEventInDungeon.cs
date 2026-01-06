using System;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Actions;
using UnityEngine;

// Token: 0x0200003D RID: 61
[CreateMenu("Dungeon Actions/InteractWithCustomEventInDungeon", 0)]
public class InteractWithCustomEventInDungeon : ActionDataBase
{
	// Token: 0x06000121 RID: 289 RVA: 0x00007EE8 File Offset: 0x000060E8
	public override void OnStart()
	{
		CustomEvent customEvent = Object.FindObjectOfType<CustomEvent>();
		if (customEvent != null)
		{
			customEvent.InteractFromDialogue(this.num);
			return;
		}
		Debug.Log("No custom event found");
	}

	// Token: 0x040000B9 RID: 185
	[SerializeField]
	private int num;
}
