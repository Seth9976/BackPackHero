using System;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Actions;

// Token: 0x02000037 RID: 55
[CreateMenu("ItemInteractions/AcceptOneOfAKindItem", 0)]
public class AcceptOneOfAKindItem : ActionDataBase
{
	// Token: 0x06000114 RID: 276 RVA: 0x00007CB8 File Offset: 0x00005EB8
	public override void OnStart()
	{
		string displayName = Item2.GetDisplayName(Overworld_InventoryManager.main.chosenItem.name);
		Overworld_InventoryManager.main.RemoveItem();
		if (!MetaProgressSaveManager.main.oneOfAKindItems.Contains(displayName))
		{
			MetaProgressSaveManager.main.oneOfAKindItems.Add(displayName);
		}
	}
}
