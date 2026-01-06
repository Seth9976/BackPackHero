using System;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Actions;
using UnityEngine;

// Token: 0x0200003C RID: 60
[CreateMenu("Town Actions/Give Resource", 0)]
public class GiveResourceAction : ActionDataBase
{
	// Token: 0x0600011F RID: 287 RVA: 0x00007EC3 File Offset: 0x000060C3
	public override void OnStart()
	{
		Overworld_ResourceManager.ChangeResourceAmountBy(MetaProgressSaveManager.main.resources, this.type, this.amount);
	}

	// Token: 0x040000B7 RID: 183
	[SerializeField]
	private Overworld_ResourceManager.Resource.Type type;

	// Token: 0x040000B8 RID: 184
	[SerializeField]
	private int amount;
}
