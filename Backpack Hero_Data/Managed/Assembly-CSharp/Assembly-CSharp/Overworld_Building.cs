using System;
using Pathfinding;
using UnityEngine;

// Token: 0x02000139 RID: 313
public class Overworld_Building : MonoBehaviour
{
	// Token: 0x06000BBD RID: 3005 RVA: 0x0007B28E File Offset: 0x0007948E
	private void Start()
	{
	}

	// Token: 0x06000BBE RID: 3006 RVA: 0x0007B290 File Offset: 0x00079490
	private void Update()
	{
		if (this.timeToSpawn <= 0f)
		{
			this.timeToSpawn = (float)Random.Range(5, 7);
			Object.Instantiate<GameObject>(this.npc, base.transform.position + Vector3.down * 3f, Quaternion.identity);
			return;
		}
		this.timeToSpawn -= Time.deltaTime;
	}

	// Token: 0x04000990 RID: 2448
	[SerializeField]
	private GameObject npc;

	// Token: 0x04000991 RID: 2449
	private Seeker seeker;

	// Token: 0x04000992 RID: 2450
	private float timeToSpawn;
}
