using System;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Nodes;
using UnityEngine;

// Token: 0x0200002B RID: 43
[CreateMenu("CheckForLastRunResult", 0)]
public class CheckForLastRunResult : ConditionDataBase
{
	// Token: 0x060000FA RID: 250 RVA: 0x00007494 File Offset: 0x00005694
	public override bool OnGetIsValid(INode parent)
	{
		return (this.characterName == Character.CharacterName.Any || this.characterName == MetaProgressSaveManager.main.lastRun.character) && (this.result == MetaProgressSaveManager.LastRun.Result.none || this.result == MetaProgressSaveManager.main.lastRun.result) && (this.runEvent == MetaProgressSaveManager.LastRun.RunEvents.none || (MetaProgressSaveManager.main.lastRun != null && MetaProgressSaveManager.main.lastRun.events.Contains(this.runEvent))) && (!this.mission || !(Item2.GetDisplayName(this.mission.name) != MetaProgressSaveManager.main.lastRun.missionName));
	}

	// Token: 0x04000092 RID: 146
	[SerializeField]
	public Character.CharacterName characterName;

	// Token: 0x04000093 RID: 147
	[SerializeField]
	private MetaProgressSaveManager.LastRun.Result result;

	// Token: 0x04000094 RID: 148
	[SerializeField]
	public Missions mission;

	// Token: 0x04000095 RID: 149
	[SerializeField]
	public MetaProgressSaveManager.LastRun.RunEvents runEvent;
}
