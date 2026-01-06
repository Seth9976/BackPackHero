using System;
using UnityEngine;

// Token: 0x02000150 RID: 336
public class Overworld_NPC_Spawner : MonoBehaviour
{
	// Token: 0x06000D47 RID: 3399 RVA: 0x00085998 File Offset: 0x00083B98
	private void Start()
	{
		this.structure = base.GetComponentInParent<Overworld_Structure>();
		this.currentTimeToSpawn = Random.Range(0f, this.timeToSpawn * 2f);
	}

	// Token: 0x06000D48 RID: 3400 RVA: 0x000859C4 File Offset: 0x00083BC4
	private void Update()
	{
		if (MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.townRaided) && !MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.returnedToDungeonAfterRaid))
		{
			return;
		}
		if (!this.spawnGenericNPCs)
		{
			return;
		}
		this.currentTimeToSpawn -= Time.deltaTime;
		if (this.currentTimeToSpawn <= 0f)
		{
			this.currentTimeToSpawn = this.timeToSpawn;
			GameObject gameObject;
			if (this.structure)
			{
				gameObject = OVerworld_NPCManager.main.SpawnAnyNPC(base.transform.position, this.structure.structureTypes);
			}
			else
			{
				gameObject = OVerworld_NPCManager.main.SpawnAnyNPC(base.transform.position, null);
			}
			if (!gameObject)
			{
				return;
			}
			this.SpawnNPC(gameObject, false);
		}
	}

	// Token: 0x06000D49 RID: 3401 RVA: 0x00085A86 File Offset: 0x00083C86
	public void Delay()
	{
		this.currentTimeToSpawn = 30f;
	}

	// Token: 0x06000D4A RID: 3402 RVA: 0x00085A94 File Offset: 0x00083C94
	private void SpawnNPC(GameObject npc, bool setHome = false)
	{
		OVerworld_NPCDestination componentInChildren = base.transform.parent.GetComponentInChildren<OVerworld_NPCDestination>();
		Overworld_NPC component = npc.GetComponent<Overworld_NPC>();
		if (setHome)
		{
			component.SetHome(base.transform.GetComponentInParent<Overworld_Structure>().transform);
		}
		if (!componentInChildren || !component)
		{
			return;
		}
		component.SetCurrentDestination(componentInChildren);
	}

	// Token: 0x04000AC0 RID: 2752
	[SerializeField]
	private bool spawnGenericNPCs = true;

	// Token: 0x04000AC1 RID: 2753
	private Overworld_Structure structure;

	// Token: 0x04000AC2 RID: 2754
	[SerializeField]
	private float timeToSpawn = 3f;

	// Token: 0x04000AC3 RID: 2755
	private float currentTimeToSpawn = 3f;

	// Token: 0x04000AC4 RID: 2756
	public bool spawnAgain;
}
