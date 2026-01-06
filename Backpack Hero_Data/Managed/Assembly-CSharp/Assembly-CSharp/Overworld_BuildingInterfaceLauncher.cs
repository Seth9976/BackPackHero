using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200007F RID: 127
public class Overworld_BuildingInterfaceLauncher : Overworld_InteractiveObject
{
	// Token: 0x060002BA RID: 698 RVA: 0x000107C8 File Offset: 0x0000E9C8
	private void Start()
	{
		ItemStorage component = base.GetComponent<ItemStorage>();
		if (component)
		{
			this.storedItems = component.storedItems;
		}
	}

	// Token: 0x060002BB RID: 699 RVA: 0x000107F0 File Offset: 0x0000E9F0
	private void Update()
	{
	}

	// Token: 0x060002BC RID: 700 RVA: 0x000107F2 File Offset: 0x0000E9F2
	public void ChangeInterface()
	{
		this.buildingInterfacePrefab = this.backUpBuildingInterfacePrefab;
	}

	// Token: 0x060002BD RID: 701 RVA: 0x00010800 File Offset: 0x0000EA00
	public void AddAllItemsToInventory()
	{
		foreach (string text in this.storedItems)
		{
			MetaProgressSaveManager.main.storedItems.Add(text);
		}
		this.storedItems.Clear();
	}

	// Token: 0x060002BE RID: 702 RVA: 0x00010868 File Offset: 0x0000EA68
	public bool IsEmpty()
	{
		return this.storedItems.Count == 0;
	}

	// Token: 0x060002BF RID: 703 RVA: 0x00010878 File Offset: 0x0000EA78
	public bool DetermineIfResearchIsAvailableBool()
	{
		Overworld_BuildingInterface component = this.buildingInterfacePrefab.GetComponent<Overworld_BuildingInterface>();
		return component && component.NewResearchIsAvailable();
	}

	// Token: 0x060002C0 RID: 704 RVA: 0x000108A4 File Offset: 0x0000EAA4
	public void DetermineIfResearchIsAvailable()
	{
		Overworld_BuildingInterface component = this.buildingInterfacePrefab.GetComponent<Overworld_BuildingInterface>();
		if (component && component.NewResearchIsAvailable())
		{
			if (this.exclamation)
			{
				this.exclamation.SetActive(true);
			}
			Debug.Log("New research is available");
			return;
		}
		if (this.exclamation)
		{
			this.exclamation.SetActive(false);
		}
		Debug.Log("No new research is available");
	}

	// Token: 0x060002C1 RID: 705 RVA: 0x00010914 File Offset: 0x0000EB14
	public int DetermineCompleteResearch()
	{
		int num = 0;
		Overworld_BuildingInterface component = this.buildingInterfacePrefab.GetComponent<Overworld_BuildingInterface>();
		if (component)
		{
			using (List<Overworld_BuildingInterface.Research>.Enumerator enumerator = component.researches.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsComplete())
					{
						num++;
					}
				}
			}
		}
		return num;
	}

	// Token: 0x060002C2 RID: 706 RVA: 0x00010984 File Offset: 0x0000EB84
	public override void Interact()
	{
		base.Interact();
		this.OpenInterface();
	}

	// Token: 0x060002C3 RID: 707 RVA: 0x00010994 File Offset: 0x0000EB94
	public GameObject OpenInterface()
	{
		if (this.exclamation)
		{
			this.exclamation.SetActive(false);
		}
		if (!this.buildingInterfacePrefab)
		{
			Overworld_Structure component = base.GetComponent<Overworld_Structure>();
			if (component)
			{
				GameObject building = Overworld_BuildingManager.main.GetBuilding(component.name);
				if (building)
				{
					this.buildingInterfacePrefab = building.GetComponent<Overworld_BuildingInterfaceLauncher>().buildingInterfacePrefab;
				}
			}
			if (!this.buildingInterfacePrefab)
			{
				Debug.LogError("No building interface prefab found for " + base.name);
				return null;
			}
		}
		Overworld_BuildingInterface component2 = this.buildingInterfacePrefab.GetComponent<Overworld_BuildingInterface>();
		if (component2.type == Overworld_BuildingInterface.Type.NewResearcher || component2.type == Overworld_BuildingInterface.Type.TownHall)
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag("PrimaryCanvas");
			GameObject gameObject2 = Object.Instantiate<GameObject>(this.buildingInterfacePrefab, gameObject.transform);
			gameObject2.GetComponent<Overworld_BuildingInterface>().overworld_BuildingInterfaceLauncher = this;
			return gameObject2;
		}
		if (component2.type == Overworld_BuildingInterface.Type.Grinder)
		{
			Overworld_InventoryManager.main.ToggleInventory(Overworld_InventoryManager.ClickAction.NONE);
			GameObject gameObject3 = Object.Instantiate<GameObject>(this.buildingInterfacePrefab, base.transform.position, Quaternion.identity, Overworld_InventoryManager.main.inventoryInterface.transform);
			gameObject3.GetComponent<Overworld_BuildingInterface>().overworld_BuildingInterfaceLauncher = this;
			OverworldInventory componentInChildren = gameObject3.GetComponentInChildren<OverworldInventory>();
			if (componentInChildren)
			{
				componentInChildren.Setup(this.storedItems, null, Overworld_InventoryManager.ClickAction.NONE);
			}
			return gameObject3;
		}
		return null;
	}

	// Token: 0x060002C4 RID: 708 RVA: 0x00010AD6 File Offset: 0x0000ECD6
	public void CloseInterface()
	{
		Overworld_Structure.SetupAllExclamationPoints();
	}

	// Token: 0x040001CE RID: 462
	[SerializeField]
	public GameObject buildingInterfacePrefab;

	// Token: 0x040001CF RID: 463
	[SerializeField]
	private GameObject backUpBuildingInterfacePrefab;

	// Token: 0x040001D0 RID: 464
	[SerializeField]
	public List<string> storedItems;

	// Token: 0x040001D1 RID: 465
	[SerializeField]
	public GameObject exclamation;
}
