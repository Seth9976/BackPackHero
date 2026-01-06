using System;
using System.Collections.Generic;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Nodes;
using UnityEngine;

// Token: 0x02000031 RID: 49
[CreateMenu("CheckForResources", 0)]
public class CheckForResources : ConditionDataBase
{
	// Token: 0x06000106 RID: 262 RVA: 0x0000777B File Offset: 0x0000597B
	public override bool OnGetIsValid(INode parent)
	{
		return (this.hasEnoughResources && Overworld_ResourceManager.main.HasEnoughResources(this.resources, -1)) || (!this.hasEnoughResources && !Overworld_ResourceManager.main.HasEnoughResources(this.resources, -1));
	}

	// Token: 0x040000A0 RID: 160
	[SerializeField]
	private List<Overworld_ResourceManager.Resource> resources = new List<Overworld_ResourceManager.Resource>();

	// Token: 0x040000A1 RID: 161
	[SerializeField]
	private bool hasEnoughResources;
}
