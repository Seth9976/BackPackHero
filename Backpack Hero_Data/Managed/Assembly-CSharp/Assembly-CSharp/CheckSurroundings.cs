using System;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Nodes;
using UnityEngine;

// Token: 0x02000035 RID: 53
[CreateMenu("CheckSurroundings", 0)]
public class CheckSurroundings : ConditionDataBase
{
	// Token: 0x0600010E RID: 270 RVA: 0x000079B0 File Offset: 0x00005BB0
	public override bool OnGetIsValid(INode parent)
	{
		if (!Overworld_ConversationManager.main || !Overworld_ConversationManager.main.currentNPCSpeaker)
		{
			return false;
		}
		DateTime utcNow = DateTime.UtcNow;
		if ((utcNow - CheckSurroundings.lastCheckTime).TotalSeconds > 1.0 || (utcNow - CheckSurroundings.lastCheckTime).TotalSeconds < -1.0 || !CheckSurroundings.closestStructure)
		{
			CheckSurroundings.lastCheckTime = utcNow;
			CheckSurroundings.closestStructure = null;
			Vector2 vector = Overworld_ConversationManager.main.currentNPCSpeaker.transform.position;
			float num = 6f;
			foreach (Overworld_Structure overworld_Structure in Overworld_Structure.structures)
			{
				float num2 = Vector2.Distance(vector, overworld_Structure.transform.position);
				if (num2 < num)
				{
					CheckSurroundings.closestStructure = overworld_Structure;
					num = num2;
				}
			}
		}
		if (!CheckSurroundings.closestStructure)
		{
			return false;
		}
		if (Item2.GetDisplayName(CheckSurroundings.closestStructure.name) == Item2.GetDisplayName(this.structure.name))
		{
			Overworld_ConversationManager.main.currentNPCSpeaker.FaceTowards(CheckSurroundings.closestStructure.transform.position, 0.5f);
			return true;
		}
		return false;
	}

	// Token: 0x040000A8 RID: 168
	[SerializeField]
	private Overworld_Structure structure;

	// Token: 0x040000A9 RID: 169
	[SerializeField]
	private bool isUnlocked;

	// Token: 0x040000AA RID: 170
	private static Overworld_Structure closestStructure;

	// Token: 0x040000AB RID: 171
	private static DateTime lastCheckTime = DateTime.UtcNow;
}
