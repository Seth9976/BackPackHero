using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200015D RID: 349
public class PetManager : MonoBehaviour
{
	// Token: 0x06000DF4 RID: 3572 RVA: 0x0008AE15 File Offset: 0x00089015
	private void Start()
	{
	}

	// Token: 0x06000DF5 RID: 3573 RVA: 0x0008AE17 File Offset: 0x00089017
	private void Update()
	{
		if (DigitalCursor.main.GetInputDown("CharacterAction"))
		{
			this.TogglePet();
		}
	}

	// Token: 0x06000DF6 RID: 3574 RVA: 0x0008AE30 File Offset: 0x00089030
	public void TogglePet()
	{
		for (int i = 0; i < PetMaster.petMasters.Count; i++)
		{
			if (PetMaster.petMasters[i].showingInventory)
			{
				i++;
				if (i >= PetMaster.petMasters.Count)
				{
					i = 0;
				}
				PetMaster.petMasters[i].OpenInventory();
				return;
			}
		}
		if (PetMaster.petMasters.Count > 0)
		{
			PetMaster.petMasters[0].OpenInventory();
		}
	}

	// Token: 0x06000DF7 RID: 3575 RVA: 0x0008AEA5 File Offset: 0x000890A5
	public IEnumerator StartSelection()
	{
		List<string> list = new List<string>();
		List<Item2> list2 = Object.FindObjectsOfType<Item2>().ToList<Item2>();
		List<Item2> allItem2sFromPouches = ItemPouch.GetAllItem2sFromPouches();
		list2.AddRange(allItem2sFromPouches);
		foreach (Item2 item in list2)
		{
			if (item.itemType.Contains(Item2.ItemType.Pet) && !item.destroyed)
			{
				list.Add(Item2.GetDisplayName(item.name));
			}
		}
		List<GameObject> list3 = new List<GameObject>();
		foreach (GameObject gameObject in this.petMasters)
		{
			string displayName = Item2.GetDisplayName(gameObject.GetComponent<PetMaster>().petItemPrefab.name);
			if (!list.Contains(displayName))
			{
				list3.Add(gameObject);
			}
		}
		GameObject gameObject2 = GameObject.FindGameObjectWithTag("PetMasters");
		List<GameObject> petsSpawned = new List<GameObject>();
		for (int i = 0; i < 3; i++)
		{
			if (list3.Count > 0)
			{
				int num = Random.Range(0, list3.Count);
				GameObject gameObject3 = Object.Instantiate<GameObject>(list3[num], Vector3.zero, Quaternion.identity, gameObject2.transform);
				list3.RemoveAt(num);
				petsSpawned.Add(gameObject3);
			}
		}
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		int num2 = 0;
		using (List<GameObject>.Enumerator enumerator2 = petsSpawned.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				GameObject gameObject4 = enumerator2.Current;
				PetItem petItem = gameObject4.GetComponent<PetMaster>().petItem;
				ItemMovement component = petItem.GetComponent<ItemMovement>();
				petItem.transform.position = new Vector3((float)(num2 * 2 - 2), -2.6f, 0f);
				component.outOfInventoryPosition = petItem.transform.localPosition;
				component.outOfInventoryRotation = petItem.transform.rotation;
				component.returnsToOutOfInventoryPosition = true;
				num2++;
			}
			yield break;
		}
		yield break;
	}

	// Token: 0x04000B4E RID: 2894
	[SerializeField]
	private List<GameObject> petMasters;

	// Token: 0x04000B4F RID: 2895
	[SerializeField]
	private List<GameObject> combatPets;

	// Token: 0x04000B50 RID: 2896
	[SerializeField]
	private List<GameObject> petInventories;
}
