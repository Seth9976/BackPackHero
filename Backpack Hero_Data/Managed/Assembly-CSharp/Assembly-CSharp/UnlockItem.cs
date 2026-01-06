using System;
using System.Collections.Generic;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Actions;
using UnityEngine;

// Token: 0x02000048 RID: 72
[CreateMenu("Town Actions/Unlock Item", 0)]
public class UnlockItem : ActionDataBase
{
	// Token: 0x0600013C RID: 316 RVA: 0x000084C0 File Offset: 0x000066C0
	public override void OnStart()
	{
		MetaProgressSaveManager.main.UnlockItems(this.itemsToGive.ConvertAll<Item2>((GameObject item) => item.GetComponent<Item2>()));
		this.window = Overworld_Manager.main.OpenNewItemsWindow(this.itemsToGive.ConvertAll<Item2>((GameObject item) => item.GetComponent<Item2>()));
	}

	// Token: 0x0600013D RID: 317 RVA: 0x0000853B File Offset: 0x0000673B
	public override ActionStatus OnUpdate()
	{
		if (this.window != null)
		{
			return ActionStatus.Continue;
		}
		return ActionStatus.Success;
	}

	// Token: 0x040000D0 RID: 208
	[SerializeField]
	private List<GameObject> itemsToGive;

	// Token: 0x040000D1 RID: 209
	private GameObject window;
}
