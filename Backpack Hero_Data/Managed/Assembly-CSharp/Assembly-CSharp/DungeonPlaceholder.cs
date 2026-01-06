using System;
using UnityEngine;

// Token: 0x020000C1 RID: 193
[ExecuteInEditMode]
public class DungeonPlaceholder : MonoBehaviour
{
	// Token: 0x06000587 RID: 1415 RVA: 0x000366BF File Offset: 0x000348BF
	private void Update()
	{
	}

	// Token: 0x04000433 RID: 1075
	public DungeonSpawner.DungeonEventSpawn.Type type;

	// Token: 0x04000434 RID: 1076
	public int priority;

	// Token: 0x04000435 RID: 1077
	public bool used;

	// Token: 0x04000436 RID: 1078
	public bool forceFill;

	// Token: 0x04000437 RID: 1079
	public bool canBeDeletedIfEmpty;
}
