using System;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Nodes;
using UnityEngine;

// Token: 0x0200002D RID: 45
[CreateMenu("CheckForMetaProgressMarker", 0)]
public class CheckForMetaProgressMarker : ConditionDataBase
{
	// Token: 0x060000FE RID: 254 RVA: 0x0000758C File Offset: 0x0000578C
	public override bool OnGetIsValid(INode parent)
	{
		MetaProgressSaveManager main = MetaProgressSaveManager.main;
		if (!main)
		{
			return false;
		}
		if (this.type == CheckForMetaProgressMarker.Type.Boolean)
		{
			if (main.HasMetaProgressMarker(this.marker) == this.isTrue)
			{
				return true;
			}
		}
		else if (this.type == CheckForMetaProgressMarker.Type.Integer)
		{
			if (main.GetMetaProgressMarkerValue(this.marker) >= this.greaterThanOrEqualToValue && this.isTrue)
			{
				return true;
			}
			if (main.GetMetaProgressMarkerValue(this.marker) < this.greaterThanOrEqualToValue && !this.isTrue)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x04000096 RID: 150
	[SerializeField]
	private CheckForMetaProgressMarker.Type type;

	// Token: 0x04000097 RID: 151
	[SerializeField]
	private MetaProgressSaveManager.MetaProgressMarker marker;

	// Token: 0x04000098 RID: 152
	[SerializeField]
	private bool isTrue;

	// Token: 0x04000099 RID: 153
	[SerializeField]
	private int greaterThanOrEqualToValue;

	// Token: 0x0200025C RID: 604
	public enum Type
	{
		// Token: 0x04000EE9 RID: 3817
		Boolean,
		// Token: 0x04000EEA RID: 3818
		Integer
	}
}
