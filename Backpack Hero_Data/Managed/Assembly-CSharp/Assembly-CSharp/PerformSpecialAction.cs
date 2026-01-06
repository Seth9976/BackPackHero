using System;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Actions;
using UnityEngine;

// Token: 0x02000042 RID: 66
[CreateMenu("PerformSpecialAction", 0)]
public class PerformSpecialAction : ActionDataBase
{
	// Token: 0x0600012D RID: 301 RVA: 0x000080B4 File Offset: 0x000062B4
	public override void OnStart()
	{
		switch (this.actionType)
		{
		case PerformSpecialAction.ActionType.EnableRunButton:
			Overworld_Manager.main.EnableRunButton();
			break;
		case PerformSpecialAction.ActionType.AddBuilding:
			if (!Overworld_BuildingManager.main.BuildingUnlocked(this.genericObject))
			{
				Overworld_Structure component = this.genericObject.GetComponent<Overworld_Structure>();
				if (component)
				{
					Overworld_BuildingManager.main.AddBuilding(this.genericObject);
					this.window = Overworld_Manager.main.OpenNewConstructionWindow(component);
				}
				SellingTile component2 = this.genericObject.GetComponent<SellingTile>();
				if (component2)
				{
					Overworld_BuildingManager.main.AddTile(component2.name);
					this.window = Overworld_Manager.main.OpenNewConstructionWindow(component2);
				}
			}
			break;
		case PerformSpecialAction.ActionType.OpenInterface:
		{
			Overworld_BuildingInterfaceLauncher componentInChildren = Overworld_ConversationManager.main.interactiveObject.GetComponentInChildren<Overworld_BuildingInterfaceLauncher>();
			if (componentInChildren != null)
			{
				this.window = componentInChildren.OpenInterface();
			}
			break;
		}
		case PerformSpecialAction.ActionType.PulseBuild:
			Overworld_BuildingManager.main.PulseBuildings();
			break;
		case PerformSpecialAction.ActionType.UnlockCharacter:
			this.window = Overworld_Manager.main.OpenNewCharacterWindow(this.character);
			break;
		case PerformSpecialAction.ActionType.UnlockCostume:
			MetaProgressSaveManager.main.UnlockCostume(this.costume);
			this.window = Overworld_Manager.main.OpenNewCostumeWindow(this.costume);
			break;
		case PerformSpecialAction.ActionType.PulseDestroy:
		{
			Overworld_BuildingManager.main.PulseDestroy();
			OverworldTutorials overworldTutorials = Object.FindObjectOfType<OverworldTutorials>();
			if (overworldTutorials != null)
			{
				overworldTutorials.ShowDestroyStoreTutorial(true);
			}
			break;
		}
		}
		if (this.window == null)
		{
			this.skip = true;
		}
	}

	// Token: 0x0600012E RID: 302 RVA: 0x0000822B File Offset: 0x0000642B
	public override ActionStatus OnUpdate()
	{
		if (this.skip)
		{
			return ActionStatus.Success;
		}
		if (this.window != null)
		{
			return ActionStatus.Continue;
		}
		return ActionStatus.Success;
	}

	// Token: 0x040000C0 RID: 192
	[SerializeField]
	public PerformSpecialAction.ActionType actionType;

	// Token: 0x040000C1 RID: 193
	[SerializeField]
	public GameObject genericObject;

	// Token: 0x040000C2 RID: 194
	[SerializeField]
	public Character character;

	// Token: 0x040000C3 RID: 195
	[SerializeField]
	public RuntimeAnimatorController costume;

	// Token: 0x040000C4 RID: 196
	private GameObject window;

	// Token: 0x040000C5 RID: 197
	private bool skip;

	// Token: 0x02000261 RID: 609
	public enum ActionType
	{
		// Token: 0x04000EF7 RID: 3831
		EnableRunButton,
		// Token: 0x04000EF8 RID: 3832
		AddBuilding,
		// Token: 0x04000EF9 RID: 3833
		OpenInterface,
		// Token: 0x04000EFA RID: 3834
		PulseBuild,
		// Token: 0x04000EFB RID: 3835
		UnlockCharacter,
		// Token: 0x04000EFC RID: 3836
		UnlockCostume,
		// Token: 0x04000EFD RID: 3837
		PulseDestroy
	}
}
