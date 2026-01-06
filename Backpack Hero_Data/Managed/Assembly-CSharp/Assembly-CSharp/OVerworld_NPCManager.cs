using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200014E RID: 334
public class OVerworld_NPCManager : MonoBehaviour
{
	// Token: 0x06000D34 RID: 3380 RVA: 0x00084FC2 File Offset: 0x000831C2
	private void Awake()
	{
		OVerworld_NPCManager.main = this;
		MetaProgressSaveManager.main.ResetNPCTickets();
	}

	// Token: 0x06000D35 RID: 3381 RVA: 0x00084FD4 File Offset: 0x000831D4
	private void OnDestroy()
	{
		if (OVerworld_NPCManager.main == this)
		{
			OVerworld_NPCManager.main = null;
		}
	}

	// Token: 0x06000D36 RID: 3382 RVA: 0x00084FE9 File Offset: 0x000831E9
	private void Start()
	{
	}

	// Token: 0x06000D37 RID: 3383 RVA: 0x00084FEB File Offset: 0x000831EB
	private void Update()
	{
	}

	// Token: 0x06000D38 RID: 3384 RVA: 0x00084FF0 File Offset: 0x000831F0
	public void UseNPCTicket(string npcName, int numberToUse = 1)
	{
		foreach (MetaProgressSaveManager.NPCsUnlocked npcsUnlocked in MetaProgressSaveManager.main.nPCsUnlocked)
		{
			if (Item2.GetDisplayName(npcsUnlocked.npcName) == Item2.GetDisplayName(npcName))
			{
				npcsUnlocked.numberSpawned += numberToUse;
				return;
			}
		}
		MetaProgressSaveManager.NPCsUnlocked npcsUnlocked2 = new MetaProgressSaveManager.NPCsUnlocked();
		npcsUnlocked2.npcName = Item2.GetDisplayName(npcName);
		npcsUnlocked2.numberSpawned = numberToUse;
		npcsUnlocked2.numberUnlocked = 1;
		MetaProgressSaveManager.main.nPCsUnlocked.Add(npcsUnlocked2);
	}

	// Token: 0x06000D39 RID: 3385 RVA: 0x00085098 File Offset: 0x00083298
	public GameObject GetNPCPrefab(string npcName)
	{
		foreach (GameObject gameObject in this.npcPrefabs)
		{
			if (Item2.GetDisplayName(gameObject.name) == Item2.GetDisplayName(npcName))
			{
				return gameObject;
			}
		}
		return null;
	}

	// Token: 0x06000D3A RID: 3386 RVA: 0x00085104 File Offset: 0x00083304
	public GameObject SpawnAnyNPC(Vector2 position, List<Overworld_Structure.StructureType> structureTypes = null)
	{
		List<GameObject> list = new List<GameObject>();
		List<GameObject> list2 = new List<GameObject>();
		foreach (MetaProgressSaveManager.NPCsUnlocked npcsUnlocked in MetaProgressSaveManager.main.nPCsUnlocked)
		{
			int num = Mathf.Max(1, npcsUnlocked.numberUnlocked / 2);
			if (npcsUnlocked.numberSpawned < num)
			{
				GameObject npcprefab = this.GetNPCPrefab(npcsUnlocked.npcName);
				if (npcprefab && npcsUnlocked.numberUnlocked != 1)
				{
					list.Add(npcprefab);
				}
			}
		}
		if (list.Count == 0)
		{
			return null;
		}
		if (list2.Count > 0)
		{
			int num2 = Random.Range(0, list2.Count);
			return Object.Instantiate<GameObject>(list2[num2], position, Quaternion.identity, this.npcTransform);
		}
		int num3 = Random.Range(0, list.Count);
		return Object.Instantiate<GameObject>(list[num3], position, Quaternion.identity, this.npcTransform);
	}

	// Token: 0x06000D3B RID: 3387 RVA: 0x00085210 File Offset: 0x00083410
	public void AssignOutFit(GameObject npc)
	{
		List<npcOutfit> list = new List<npcOutfit>();
		list.Add(this.GetOutfitOfType(npcOutfit.Type.Body));
		list.Add(this.GetOutfitOfType(npcOutfit.Type.Head));
		if (Random.Range(0, 2) == 0)
		{
			list.Add(this.GetOutfitOfType(npcOutfit.Type.Hat));
		}
		list.Add(this.GetOutfitOfType(npcOutfit.Type.Arms));
		npc.GetComponent<Overworld_NPC_CostumeSelector>().SetOutfit(list);
	}

	// Token: 0x06000D3C RID: 3388 RVA: 0x0008526C File Offset: 0x0008346C
	private npcOutfit GetOutfitOfType(npcOutfit.Type type)
	{
		List<npcOutfit> list = new List<npcOutfit>();
		foreach (npcOutfit npcOutfit in this.outfits)
		{
			if (npcOutfit.type == type)
			{
				list.Add(npcOutfit);
			}
		}
		if (list.Count > 0)
		{
			return list[Random.Range(0, list.Count)];
		}
		return null;
	}

	// Token: 0x04000AB0 RID: 2736
	[SerializeField]
	private Transform npcTransform;

	// Token: 0x04000AB1 RID: 2737
	private List<GameObject> npcs = new List<GameObject>();

	// Token: 0x04000AB2 RID: 2738
	public static OVerworld_NPCManager main;

	// Token: 0x04000AB3 RID: 2739
	[SerializeField]
	private List<npcOutfit> outfits = new List<npcOutfit>();

	// Token: 0x04000AB4 RID: 2740
	[SerializeField]
	private List<OVerworld_NPCManager.NPCType> npcTypes = new List<OVerworld_NPCManager.NPCType>();

	// Token: 0x04000AB5 RID: 2741
	[SerializeField]
	private List<GameObject> npcPrefabs = new List<GameObject>();

	// Token: 0x02000409 RID: 1033
	[Serializable]
	public class NPCType
	{
		// Token: 0x040017BD RID: 6077
		[SerializeField]
		public List<Overworld_Structure.StructureType> allowedStructureTypes = new List<Overworld_Structure.StructureType>();

		// Token: 0x040017BE RID: 6078
		public string npcTypeName;

		// Token: 0x040017BF RID: 6079
		public bool getsRandomOutfits;

		// Token: 0x040017C0 RID: 6080
		public GameObject npcPrefab;

		// Token: 0x040017C1 RID: 6081
		public int numberAllowed = 1;

		// Token: 0x040017C2 RID: 6082
		[HideInInspector]
		public bool isSpawned;
	}
}
