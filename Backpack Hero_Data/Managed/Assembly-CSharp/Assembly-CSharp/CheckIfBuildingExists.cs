using System;
using System.Collections.Generic;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Nodes;
using UnityEngine;

// Token: 0x02000034 RID: 52
[CreateMenu("CheckIfBuildingExists", 0)]
public class CheckIfBuildingExists : ConditionDataBase
{
	// Token: 0x0600010C RID: 268 RVA: 0x000078B4 File Offset: 0x00005AB4
	public override bool OnGetIsValid(INode parent)
	{
		int num = 0;
		foreach (GameObject gameObject in this.itemsToCheckFor)
		{
			Overworld_Structure component = gameObject.GetComponent<Overworld_Structure>();
			if (component == null)
			{
				Debug.LogError("Item to check for is not a structure");
				return true;
			}
			using (List<Overworld_Structure>.Enumerator enumerator2 = Overworld_Structure.structures.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (Item2.GetDisplayName(enumerator2.Current.name) == Item2.GetDisplayName(component.name))
					{
						num++;
					}
				}
			}
		}
		if (num >= this.itemsToCheckFor.Count)
		{
			return this.isTrue;
		}
		return !this.isTrue;
	}

	// Token: 0x040000A6 RID: 166
	[SerializeField]
	private List<GameObject> itemsToCheckFor = new List<GameObject>();

	// Token: 0x040000A7 RID: 167
	[SerializeField]
	private bool isTrue;
}
