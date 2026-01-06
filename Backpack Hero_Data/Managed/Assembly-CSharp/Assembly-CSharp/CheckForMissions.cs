using System;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Nodes;
using UnityEngine;

// Token: 0x0200002E RID: 46
[CreateMenu("CheckForMissions", 0)]
public class CheckForMissions : ConditionDataBase
{
	// Token: 0x06000100 RID: 256 RVA: 0x00007618 File Offset: 0x00005818
	public override bool OnGetIsValid(INode parent)
	{
		string text = Missions.Stringify(this.mission);
		return (this.isTrue && this.type == CheckForMissions.Type.unlocked && MetaProgressSaveManager.main.missionsUnlocked.Contains(text)) || (!this.isTrue && this.type == CheckForMissions.Type.unlocked && !MetaProgressSaveManager.main.missionsUnlocked.Contains(text)) || (this.isTrue && this.type == CheckForMissions.Type.completed && MetaProgressSaveManager.main.missionsComplete.Contains(text)) || (!this.isTrue && this.type == CheckForMissions.Type.completed && !MetaProgressSaveManager.main.missionsComplete.Contains(text));
	}

	// Token: 0x0400009A RID: 154
	[SerializeField]
	public Missions mission;

	// Token: 0x0400009B RID: 155
	[SerializeField]
	public CheckForMissions.Type type;

	// Token: 0x0400009C RID: 156
	[SerializeField]
	private bool isTrue;

	// Token: 0x0200025D RID: 605
	public enum Type
	{
		// Token: 0x04000EEC RID: 3820
		unlocked = 1,
		// Token: 0x04000EED RID: 3821
		completed
	}
}
