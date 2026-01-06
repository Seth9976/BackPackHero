using System;
using UnityEngine;

// Token: 0x02000091 RID: 145
public class PrefabSpawner : MonoBehaviour
{
	// Token: 0x06000322 RID: 802 RVA: 0x0001240C File Offset: 0x0001060C
	public void Spawn()
	{
		Vector3 vector = Vector3.zero;
		switch (this.spawnPosition)
		{
		case PrefabSpawner.SpawnPosition.zero:
			vector = Vector3.zero;
			break;
		case PrefabSpawner.SpawnPosition.thisPosition:
			vector = base.transform.position;
			break;
		case PrefabSpawner.SpawnPosition.parentPosition:
			if (this.parent)
			{
				vector = this.parent.position;
			}
			else
			{
				vector = base.transform.position;
			}
			break;
		case PrefabSpawner.SpawnPosition.prefabPosition:
			vector = this.prefabToSpawn.transform.position;
			break;
		case PrefabSpawner.SpawnPosition.thisXprefabY:
			vector = new Vector3(base.transform.position.x, this.prefabToSpawn.transform.position.y, 0f);
			break;
		}
		if (this.parent)
		{
			Object.Instantiate<GameObject>(this.prefabToSpawn, vector, Quaternion.identity, this.parent);
			return;
		}
		Object.Instantiate<GameObject>(this.prefabToSpawn, vector, Quaternion.identity, base.transform.parent);
	}

	// Token: 0x04000227 RID: 551
	[SerializeField]
	private PrefabSpawner.SpawnPosition spawnPosition;

	// Token: 0x04000228 RID: 552
	[SerializeField]
	private GameObject prefabToSpawn;

	// Token: 0x04000229 RID: 553
	[SerializeField]
	private Transform parent;

	// Token: 0x02000299 RID: 665
	private enum SpawnPosition
	{
		// Token: 0x04000FCD RID: 4045
		zero,
		// Token: 0x04000FCE RID: 4046
		thisPosition,
		// Token: 0x04000FCF RID: 4047
		parentPosition,
		// Token: 0x04000FD0 RID: 4048
		prefabPosition,
		// Token: 0x04000FD1 RID: 4049
		thisXprefabY
	}
}
