using System;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Nodes;
using UnityEngine;

// Token: 0x02000036 RID: 54
[CreateMenu("CheckSurroundingsNPCs", 0)]
public class CheckSurroundingsNPCs : ConditionDataBase
{
	// Token: 0x06000111 RID: 273 RVA: 0x00007B30 File Offset: 0x00005D30
	public override bool OnGetIsValid(INode parent)
	{
		if (!Overworld_ConversationManager.main || !Overworld_ConversationManager.main.currentNPCSpeaker)
		{
			return false;
		}
		DateTime utcNow = DateTime.UtcNow;
		if ((utcNow - CheckSurroundingsNPCs.lastCheckTime).TotalSeconds > 1.0 || (utcNow - CheckSurroundingsNPCs.lastCheckTime).TotalSeconds < -1.0 || !CheckSurroundingsNPCs.closestNPC)
		{
			CheckSurroundingsNPCs.lastCheckTime = utcNow;
			CheckSurroundingsNPCs.closestNPC = null;
			Vector2 vector = Overworld_ConversationManager.main.currentNPCSpeaker.transform.position;
			float num = 6f;
			foreach (Overworld_NPC overworld_NPC in Overworld_NPC.npcs)
			{
				if (overworld_NPC.canViewCard)
				{
					float num2 = Vector2.Distance(vector, overworld_NPC.transform.position);
					if (num2 < num)
					{
						CheckSurroundingsNPCs.closestNPC = overworld_NPC;
						num = num2;
					}
				}
			}
		}
		if (!CheckSurroundingsNPCs.closestNPC)
		{
			return false;
		}
		if (Item2.GetDisplayName(CheckSurroundingsNPCs.closestNPC.name) == Item2.GetDisplayName(this.NPCtoCheck.name))
		{
			Overworld_ConversationManager.main.currentNPCSpeaker.FaceTowards(CheckSurroundingsNPCs.closestNPC.transform.position, 0.5f);
			return true;
		}
		return false;
	}

	// Token: 0x040000AC RID: 172
	[SerializeField]
	private Overworld_NPC NPCtoCheck;

	// Token: 0x040000AD RID: 173
	[SerializeField]
	private bool isUnlocked;

	// Token: 0x040000AE RID: 174
	private static Overworld_NPC closestNPC;

	// Token: 0x040000AF RID: 175
	private static DateTime lastCheckTime = DateTime.UtcNow;
}
