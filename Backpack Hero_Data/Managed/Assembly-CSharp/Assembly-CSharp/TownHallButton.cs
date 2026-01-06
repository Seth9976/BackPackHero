using System;
using UnityEngine;

// Token: 0x020001A5 RID: 421
public class TownHallButton : MonoBehaviour
{
	// Token: 0x060010B1 RID: 4273 RVA: 0x0009E763 File Offset: 0x0009C963
	private void Start()
	{
		this.parentBuildingInterface = base.GetComponentInParent<Overworld_BuildingInterface>();
		this.townHallStructure = this.parentBuildingInterface.overworld_BuildingInterfaceLauncher.GetComponent<Overworld_Structure>();
		this.SetSelector(false);
	}

	// Token: 0x060010B2 RID: 4274 RVA: 0x0009E790 File Offset: 0x0009C990
	public bool IsVisible(Overworld_BuildingInterfaceLauncher[] allLaunchers)
	{
		if (MetaProgressSaveManager.main.researchBuildingsDiscovered.Contains(Item2.GetDisplayName(this.myBuildingInterface.name)))
		{
			return true;
		}
		for (int i = 0; i < allLaunchers.Length; i++)
		{
			if (allLaunchers[i].buildingInterfacePrefab == this.myBuildingInterface.gameObject)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060010B3 RID: 4275 RVA: 0x0009E7ED File Offset: 0x0009C9ED
	public void SetSelector(bool visible)
	{
		this.selector.SetActive(visible);
	}

	// Token: 0x060010B4 RID: 4276 RVA: 0x0009E7FC File Offset: 0x0009C9FC
	public void TownHallButtonPressed()
	{
		TownHallButton[] componentsInChildren = base.transform.parent.GetComponentsInChildren<TownHallButton>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].SetSelector(false);
		}
		this.SetSelector(true);
		this.parentBuildingInterface.SetBuildingText(this.myBuildingInterface.name);
		Debug.Log("Searching for: " + this.myBuildingInterface.name);
		foreach (Overworld_Structure overworld_Structure in this.townHallStructure.connectedStructuresViaPath)
		{
			Overworld_BuildingInterfaceLauncher component = overworld_Structure.GetComponent<Overworld_BuildingInterfaceLauncher>();
			if (component)
			{
				Debug.Log("Found: " + component.buildingInterfacePrefab.name);
				if (component.buildingInterfacePrefab == this.myBuildingInterface.gameObject)
				{
					Debug.Log("Matched: " + component.buildingInterfacePrefab.name);
					this.myStructure = overworld_Structure;
					this.SetupResearches();
					return;
				}
			}
		}
		Overworld_BuildingInterfaceLauncher[] array = Object.FindObjectsOfType<Overworld_BuildingInterfaceLauncher>();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].buildingInterfacePrefab == this.myBuildingInterface.gameObject)
			{
				this.parentBuildingInterface.SetupWarning(LangaugeManager.main.GetTextByKey("townHallError2"));
				return;
			}
		}
		this.parentBuildingInterface.SetupWarning(LangaugeManager.main.GetTextByKey("townHallError1"));
	}

	// Token: 0x060010B5 RID: 4277 RVA: 0x0009E988 File Offset: 0x0009CB88
	private void SetupResearches()
	{
		this.parentBuildingInterface.currentEfficiencyBonus = this.myStructure.currentEfficiencyBonus;
		this.parentBuildingInterface.SetEfficiencyText(this.parentBuildingInterface.currentEfficiencyBonus);
		this.parentBuildingInterface.researches = this.myBuildingInterface.researches;
		this.parentBuildingInterface.SetupResearches();
	}

	// Token: 0x04000D90 RID: 3472
	private Overworld_BuildingInterface parentBuildingInterface;

	// Token: 0x04000D91 RID: 3473
	private Overworld_Structure townHallStructure;

	// Token: 0x04000D92 RID: 3474
	[SerializeField]
	private GameObject selector;

	// Token: 0x04000D93 RID: 3475
	[SerializeField]
	private Overworld_BuildingInterface myBuildingInterface;

	// Token: 0x04000D94 RID: 3476
	[SerializeField]
	private Overworld_Structure myStructure;
}
