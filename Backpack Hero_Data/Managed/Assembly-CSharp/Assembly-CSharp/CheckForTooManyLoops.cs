using System;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Nodes;
using UnityEngine;

// Token: 0x02000033 RID: 51
[CreateMenu("CheckForTooManyLoops", 0)]
public class CheckForTooManyLoops : ConditionDataBase
{
	// Token: 0x0600010A RID: 266 RVA: 0x00007886 File Offset: 0x00005A86
	public override bool OnGetIsValid(INode parent)
	{
		if (CheckForTooManyLoops.currentLoops < this.maxLoops)
		{
			CheckForTooManyLoops.currentLoops++;
			return true;
		}
		return false;
	}

	// Token: 0x040000A4 RID: 164
	[SerializeField]
	private int maxLoops = 3;

	// Token: 0x040000A5 RID: 165
	public static int currentLoops;
}
