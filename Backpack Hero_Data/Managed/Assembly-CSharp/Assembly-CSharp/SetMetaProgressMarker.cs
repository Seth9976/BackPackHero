using System;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Actions;
using UnityEngine;

// Token: 0x02000045 RID: 69
[CreateMenu("SetMetaProgressMarker", 0)]
public class SetMetaProgressMarker : ActionDataBase
{
	// Token: 0x06000135 RID: 309 RVA: 0x000083E0 File Offset: 0x000065E0
	public override void OnStart()
	{
		MetaProgressSaveManager main = MetaProgressSaveManager.main;
		if (!main)
		{
			return;
		}
		if (this.type == SetMetaProgressMarker.Type.Boolean)
		{
			main.AddMetaProgressMarker(this.marker);
			return;
		}
		if (this.type == SetMetaProgressMarker.Type.Integer)
		{
			main.AddMetaProgressMarker(this.marker, this.numberToAdd);
		}
	}

	// Token: 0x040000C7 RID: 199
	[SerializeField]
	private MetaProgressSaveManager.MetaProgressMarker marker;

	// Token: 0x040000C8 RID: 200
	[SerializeField]
	private SetMetaProgressMarker.Type type;

	// Token: 0x040000C9 RID: 201
	[SerializeField]
	private int numberToAdd = 1;

	// Token: 0x02000264 RID: 612
	public enum Type
	{
		// Token: 0x04000F04 RID: 3844
		Boolean,
		// Token: 0x04000F05 RID: 3845
		Integer
	}
}
