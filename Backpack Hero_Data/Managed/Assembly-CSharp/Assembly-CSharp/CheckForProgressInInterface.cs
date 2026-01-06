using System;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Nodes;

// Token: 0x02000030 RID: 48
[CreateMenu("CheckForProgressInInterface", 0)]
public class CheckForProgressInInterface : ConditionDataBase
{
	// Token: 0x06000104 RID: 260 RVA: 0x00007728 File Offset: 0x00005928
	public override bool OnGetIsValid(INode parent)
	{
		if (!Overworld_ConversationManager.main.currentNPCSpeaker)
		{
			return false;
		}
		Overworld_BuildingInterfaceLauncher component = Overworld_ConversationManager.main.currentNPCSpeaker.GetComponent<Overworld_BuildingInterfaceLauncher>();
		return component && component.DetermineCompleteResearch() >= this.progressToCheckFor;
	}

	// Token: 0x0400009F RID: 159
	public int progressToCheckFor;
}
