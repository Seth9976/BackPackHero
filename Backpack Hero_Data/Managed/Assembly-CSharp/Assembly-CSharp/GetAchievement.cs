using System;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Actions;
using UnityEngine;

// Token: 0x0200003A RID: 58
[CreateMenu("GetAchievement", 0)]
public class GetAchievement : ActionDataBase
{
	// Token: 0x0600011B RID: 283 RVA: 0x00007E6B File Offset: 0x0000606B
	public override void OnStart()
	{
		AchievementAbstractor.instance.ConsiderAchievement(this.achievementID);
	}

	// Token: 0x040000B5 RID: 181
	[SerializeField]
	private string achievementID;
}
