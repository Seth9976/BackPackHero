using System;
using System.Collections.Generic;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Actions;
using UnityEngine;

// Token: 0x0200003B RID: 59
[CreateMenu("Town Actions/Give Item", 0)]
public class GiveItem : ActionDataBase
{
	// Token: 0x0600011D RID: 285 RVA: 0x00007E85 File Offset: 0x00006085
	public override void OnStart()
	{
		MetaProgressSaveManager.main.AddItems(this.itemsToGive.ConvertAll<Item2>((GameObject item) => item.GetComponent<Item2>()));
	}

	// Token: 0x040000B6 RID: 182
	[SerializeField]
	private List<GameObject> itemsToGive;
}
