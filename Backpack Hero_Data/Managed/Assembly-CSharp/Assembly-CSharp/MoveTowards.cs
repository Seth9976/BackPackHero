using System;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Actions;
using UnityEngine;

// Token: 0x0200003F RID: 63
[CreateMenu("MoveTowards", 0)]
public class MoveTowards : ActionDataBase
{
	// Token: 0x06000125 RID: 293 RVA: 0x00007FB8 File Offset: 0x000061B8
	public override void OnStart()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag(this.tagName);
		if (gameObject == null)
		{
			return;
		}
		OVerworld_NPCDestination componentInChildren = gameObject.GetComponentInChildren<OVerworld_NPCDestination>();
		if (componentInChildren)
		{
			gameObject = componentInChildren.gameObject;
		}
		Overworld_NPC currentNPCSpeaker = Overworld_ConversationManager.main.currentNPCSpeaker;
		Overworld_ConversationManager.main.ReleaseSpeaker();
		currentNPCSpeaker.FindPathToTransform(gameObject.transform);
	}

	// Token: 0x040000BD RID: 189
	public string tagName;
}
