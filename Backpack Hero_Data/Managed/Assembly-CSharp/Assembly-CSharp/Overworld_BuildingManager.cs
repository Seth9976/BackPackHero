using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

// Token: 0x0200013B RID: 315
public class Overworld_BuildingManager : MonoBehaviour
{
	// Token: 0x06000BC1 RID: 3009 RVA: 0x0007B316 File Offset: 0x00079516
	private void Awake()
	{
		Overworld_BuildingManager.main = this;
	}

	// Token: 0x06000BC2 RID: 3010 RVA: 0x0007B31E File Offset: 0x0007951E
	private void OnDestroy()
	{
		if (Overworld_BuildingManager.main == this)
		{
			Overworld_BuildingManager.main = null;
		}
	}

	// Token: 0x06000BC3 RID: 3011 RVA: 0x0007B333 File Offset: 0x00079533
	public void AddBuilding(RuleTile tile)
	{
		MetaProgressSaveManager.main.availableTiles.Add(Item2.GetDisplayName(tile.name));
		this.ResetBuildingButtons();
	}

	// Token: 0x06000BC4 RID: 3012 RVA: 0x0007B358 File Offset: 0x00079558
	public GameObject GetBuilding(string buildingName)
	{
		foreach (GameObject gameObject in this.allStructures)
		{
			if (Item2.GetDisplayName(buildingName) == Item2.GetDisplayName(gameObject.name))
			{
				return gameObject;
			}
		}
		return null;
	}

	// Token: 0x06000BC5 RID: 3013 RVA: 0x0007B3C4 File Offset: 0x000795C4
	public GameObject GetTileBase(string buildingName)
	{
		foreach (GameObject gameObject in this.allTiles)
		{
			if (Item2.GetDisplayName(buildingName) == Item2.GetDisplayName(gameObject.name))
			{
				return gameObject;
			}
		}
		return null;
	}

	// Token: 0x06000BC6 RID: 3014 RVA: 0x0007B430 File Offset: 0x00079630
	public List<GameObject> GetBuildings(List<string> buildingNames)
	{
		List<GameObject> list = new List<GameObject>();
		foreach (string text in buildingNames)
		{
			GameObject building = this.GetBuilding(text);
			if (building != null)
			{
				list.Add(building);
			}
		}
		return list;
	}

	// Token: 0x06000BC7 RID: 3015 RVA: 0x0007B498 File Offset: 0x00079698
	public bool BuildingUnlocked(GameObject building)
	{
		using (List<string>.Enumerator enumerator = MetaProgressSaveManager.main.availableBuildings.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (Item2.GetDisplayName(enumerator.Current) == Item2.GetDisplayName(building.name))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000BC8 RID: 3016 RVA: 0x0007B508 File Offset: 0x00079708
	public void AddBuilding(string building)
	{
		if (!MetaProgressSaveManager.ContainsString(MetaProgressSaveManager.main.availableBuildings, Item2.GetDisplayName(building)))
		{
			MetaProgressSaveManager.main.availableBuildings.Add(Item2.GetDisplayName(building));
		}
		this.ResetBuildingButtons();
	}

	// Token: 0x06000BC9 RID: 3017 RVA: 0x0007B53C File Offset: 0x0007973C
	public void AddTile(string tile)
	{
		if (!MetaProgressSaveManager.ContainsString(MetaProgressSaveManager.main.availableTiles, Item2.GetDisplayName(tile)))
		{
			MetaProgressSaveManager.main.availableTiles.Add(Item2.GetDisplayName(tile));
		}
		this.ResetBuildingButtons();
	}

	// Token: 0x06000BCA RID: 3018 RVA: 0x0007B570 File Offset: 0x00079770
	public void AddBuilding(GameObject building)
	{
		if (!MetaProgressSaveManager.ContainsString(MetaProgressSaveManager.main.availableBuildings, Item2.GetDisplayName(building.name)))
		{
			MetaProgressSaveManager.main.availableBuildings.Add(Item2.GetDisplayName(building.name));
		}
		this.ResetBuildingButtons();
	}

	// Token: 0x06000BCB RID: 3019 RVA: 0x0007B5B0 File Offset: 0x000797B0
	public void RemoveBuilding(GameObject building)
	{
		for (int i = 0; i < MetaProgressSaveManager.main.availableBuildings.Count; i++)
		{
			if (Item2.GetDisplayName(MetaProgressSaveManager.main.availableBuildings[i]) == Item2.GetDisplayName(building.name))
			{
				MetaProgressSaveManager.main.availableBuildings.RemoveAt(i);
				break;
			}
		}
		this.ResetBuildingButtons();
	}

	// Token: 0x06000BCC RID: 3020 RVA: 0x0007B616 File Offset: 0x00079816
	private void Start()
	{
		this.CheckForPrefabAndTile();
		this.ResetBuildingButtons();
		if (!MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.firstBuildingBuilt))
		{
			this.buildButtons.SetActive(false);
		}
	}

	// Token: 0x06000BCD RID: 3021 RVA: 0x0007B640 File Offset: 0x00079840
	private void CheckForPrefabAndTile()
	{
		foreach (Overworld_BuildingManager.Building building in this.buildingList.buildings)
		{
			if (building.prefab != null && building.tile != null)
			{
				Debug.Log("Prefab and Tile are set for " + building.prefab.name);
			}
		}
	}

	// Token: 0x06000BCE RID: 3022 RVA: 0x0007B6C8 File Offset: 0x000798C8
	public void RefundCosts(GameObject buildingObject)
	{
		Overworld_Structure component = buildingObject.GetComponent<Overworld_Structure>();
		if (component)
		{
			int num = Overworld_Structure.StructuresOfType(component).Count;
			if (component.name.ToLower().Contains("rubble") || !component.increasesCost)
			{
				num = 1;
			}
			foreach (Overworld_ResourceManager.Resource resource in Overworld_ResourceManager.main.MultiplyResourceCosts(component.costs, num))
			{
				Overworld_ResourceManager.ChangeResourceAmountBy(MetaProgressSaveManager.main.resources, resource.type, resource.amount);
			}
			return;
		}
		foreach (Overworld_BuildingManager.Building building in this.buildingList.buildings)
		{
			if (building.prefab && this.CheckIfSameName(building.prefab.name, buildingObject.name))
			{
				using (List<Overworld_ResourceManager.Resource>.Enumerator enumerator = building.costs.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						Overworld_ResourceManager.Resource resource2 = enumerator.Current;
						Overworld_ResourceManager.ChangeResourceAmountBy(MetaProgressSaveManager.main.resources, resource2.type, resource2.amount);
						break;
					}
				}
			}
		}
	}

	// Token: 0x06000BCF RID: 3023 RVA: 0x0007B848 File Offset: 0x00079A48
	public void RefundCosts(TileBase tile)
	{
		if (!tile)
		{
			return;
		}
		foreach (Overworld_BuildingManager.Building building in this.buildingList.buildings)
		{
			if (building.tile && this.CheckIfSameName(building.tile.name, tile.name))
			{
				using (List<Overworld_ResourceManager.Resource>.Enumerator enumerator2 = building.costs.GetEnumerator())
				{
					if (enumerator2.MoveNext())
					{
						Overworld_ResourceManager.Resource resource = enumerator2.Current;
						Overworld_ResourceManager.ChangeResourceAmountBy(MetaProgressSaveManager.main.resources, resource.type, resource.amount);
						break;
					}
				}
			}
		}
	}

	// Token: 0x06000BD0 RID: 3024 RVA: 0x0007B924 File Offset: 0x00079B24
	private bool CheckIfSameName(string a, string b)
	{
		if (a.Contains("("))
		{
			a = a.Substring(0, a.IndexOf("("));
		}
		if (b.Contains("("))
		{
			b = b.Substring(0, b.IndexOf("("));
		}
		a = a.ToLower().Trim();
		b = b.ToLower().Trim();
		return a == b;
	}

	// Token: 0x06000BD1 RID: 3025 RVA: 0x0007B999 File Offset: 0x00079B99
	public void PulseDestroy()
	{
		this.buildButtons.gameObject.SetActive(true);
		ArrowTutorialManager.instance.PointArrow(this.hammerIcon.GetComponent<RectTransform>());
	}

	// Token: 0x06000BD2 RID: 3026 RVA: 0x0007B9C1 File Offset: 0x00079BC1
	public void PulseBuildings()
	{
	}

	// Token: 0x06000BD3 RID: 3027 RVA: 0x0007B9C4 File Offset: 0x00079BC4
	public void ClearPulse(GameObject g)
	{
		PulseImage component = g.GetComponent<PulseImage>();
		if (component)
		{
			Object.Destroy(component);
		}
	}

	// Token: 0x06000BD4 RID: 3028 RVA: 0x0007B9E8 File Offset: 0x00079BE8
	public bool AnythingToBuild()
	{
		return this.buildingParent.childCount != 0 || this.tilemapParent.childCount != 0 || this.decorationParent.childCount != 0 || this.decoration2Parent.childCount != 0 || this.decoration3Parent.childCount != 0 || this.tilemapParent.childCount != 0;
	}

	// Token: 0x06000BD5 RID: 3029 RVA: 0x0007BA46 File Offset: 0x00079C46
	public void EnableBuildingBox()
	{
		this.buildingParent.transform.parent.gameObject.SetActive(true);
	}

	// Token: 0x06000BD6 RID: 3030 RVA: 0x0007BA64 File Offset: 0x00079C64
	public void ResetBuildingButtons()
	{
		this.buildingParent.transform.parent.gameObject.SetActive(true);
		this.buildingParent.gameObject.SetActive(true);
		this.tilemapParent.gameObject.SetActive(true);
		this.decorationParent.gameObject.SetActive(true);
		this.decoration2Parent.gameObject.SetActive(true);
		this.decoration3Parent.gameObject.SetActive(true);
		this.ClearPulse(this.buildingParent.gameObject);
		this.ClearPulse(this.tilemapParent.gameObject);
		this.ClearPulse(this.decorationParent.gameObject);
		this.ClearPulse(this.decoration2Parent.gameObject);
		this.ClearPulse(this.decoration3Parent.gameObject);
		while (this.buildingParent.childCount > 0)
		{
			GameObject gameObject = this.buildingParent.GetChild(0).gameObject;
			gameObject.transform.SetParent(null);
			Object.Destroy(gameObject);
		}
		while (this.tilemapParent.childCount > 0)
		{
			GameObject gameObject2 = this.tilemapParent.GetChild(0).gameObject;
			gameObject2.transform.SetParent(null);
			Object.Destroy(gameObject2);
		}
		while (this.decorationParent.childCount > 0)
		{
			GameObject gameObject3 = this.decorationParent.GetChild(0).gameObject;
			gameObject3.transform.SetParent(null);
			Object.Destroy(gameObject3);
		}
		while (this.decoration2Parent.childCount > 0)
		{
			GameObject gameObject4 = this.decoration2Parent.GetChild(0).gameObject;
			gameObject4.transform.SetParent(null);
			Object.Destroy(gameObject4);
		}
		while (this.decoration3Parent.childCount > 0)
		{
			GameObject gameObject5 = this.decoration3Parent.GetChild(0).gameObject;
			gameObject5.transform.SetParent(null);
			Object.Destroy(gameObject5);
		}
		if (!this.AnythingToBuild())
		{
			this.buildingParent.transform.parent.gameObject.SetActive(false);
		}
		foreach (GameObject gameObject6 in this.GetBuildings(MetaProgressSaveManager.main.availableBuildings))
		{
			Overworld_Structure component = gameObject6.GetComponent<Overworld_Structure>();
			if (component && (!component.oneOfAKind || Overworld_Structure.StructuresOfType(component).Count <= 0))
			{
				this.AddBuildingButton(new Overworld_BuildingManager.Building
				{
					category = component.category,
					prefab = gameObject6,
					costs = component.costs
				});
			}
		}
		using (List<GameObject>.Enumerator enumerator = this.allTiles.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				GameObject tile = enumerator.Current;
				if (MetaProgressSaveManager.main.availableTiles.Exists((string x) => Item2.GetDisplayName(x) == Item2.GetDisplayName(tile.name)))
				{
					Overworld_BuildingManager.Building building = new Overworld_BuildingManager.Building();
					building.category = Overworld_BuildingManager.BuildingCategory.Tile;
					SellingTile component2 = tile.GetComponent<SellingTile>();
					building.tile = component2.tileToSpawn;
					building.costs = new List<Overworld_ResourceManager.Resource>
					{
						new Overworld_ResourceManager.Resource
						{
							type = Overworld_ResourceManager.Resource.Type.BuildingMaterial,
							amount = 3
						}
					};
					this.AddBuildingButton(building);
				}
			}
		}
		this.buildingParent.GetComponent<Overworld_ButtonGroup>().Setup();
		this.tilemapParent.GetComponent<Overworld_ButtonGroup>().Setup();
		this.decorationParent.GetComponent<Overworld_ButtonGroup>().Setup();
		this.decoration2Parent.GetComponent<Overworld_ButtonGroup>().Setup();
		this.decoration3Parent.GetComponent<Overworld_ButtonGroup>().Setup();
		this.RebuildNavigation();
		if (this.buildingParent.childCount == 0)
		{
			this.buildingParent.gameObject.SetActive(false);
		}
		if (this.tilemapParent.childCount == 0)
		{
			this.tilemapParent.gameObject.SetActive(false);
		}
		if (this.decorationParent.childCount == 0)
		{
			this.decorationParent.gameObject.SetActive(false);
		}
		if (this.decoration2Parent.childCount == 0)
		{
			this.decoration2Parent.gameObject.SetActive(false);
		}
		if (this.decoration3Parent.childCount == 0)
		{
			this.decoration3Parent.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000BD7 RID: 3031 RVA: 0x0007BEA0 File Offset: 0x0007A0A0
	public void ConsiderAddingBuildButton()
	{
	}

	// Token: 0x06000BD8 RID: 3032 RVA: 0x0007BEA4 File Offset: 0x0007A0A4
	public void RebuildNavigation()
	{
		this.BuildNavigation(this.buildingParent, null, this.GetFirstSelectable(new List<Transform> { this.decorationParent, this.decoration2Parent, this.decoration3Parent, this.tilemapParent }));
		this.BuildNavigation(this.decorationParent, this.GetFirstSelectable(new List<Transform> { this.buildingParent }), this.GetFirstSelectable(new List<Transform> { this.decoration2Parent, this.decoration3Parent, this.tilemapParent }));
		this.BuildNavigation(this.decoration2Parent, this.GetFirstSelectable(new List<Transform> { this.decorationParent, this.buildingParent }), this.GetFirstSelectable(new List<Transform> { this.decoration3Parent, this.tilemapParent }));
		this.BuildNavigation(this.decoration3Parent, this.GetFirstSelectable(new List<Transform> { this.decoration2Parent, this.decorationParent, this.buildingParent }), this.GetFirstSelectable(new List<Transform> { this.tilemapParent }));
		this.BuildNavigation(this.tilemapParent, this.GetFirstSelectable(new List<Transform> { this.decoration3Parent, this.decoration2Parent, this.decorationParent, this.buildingParent }), null);
	}

	// Token: 0x06000BD9 RID: 3033 RVA: 0x0007C038 File Offset: 0x0007A238
	private Selectable GetFirstSelectable(List<Transform> transforms)
	{
		for (int i = 0; i < transforms.Count; i++)
		{
			Selectable componentInChildren = transforms[i].GetComponentInChildren<Selectable>();
			if (componentInChildren)
			{
				return componentInChildren;
			}
		}
		return null;
	}

	// Token: 0x06000BDA RID: 3034 RVA: 0x0007C070 File Offset: 0x0007A270
	private void BuildNavigation(Transform t, Selectable up, Selectable down)
	{
		Selectable selectable = null;
		for (int i = 0; i < t.childCount; i++)
		{
			Component child = t.GetChild(i);
			Selectable selectable2;
			if (i < t.childCount - 1)
			{
				selectable2 = t.GetChild(i + 1).GetComponent<Selectable>();
			}
			else
			{
				selectable2 = null;
			}
			Selectable component = child.GetComponent<Selectable>();
			Navigation navigation = new Navigation
			{
				mode = Navigation.Mode.Explicit
			};
			navigation.selectOnDown = down;
			navigation.selectOnUp = up;
			navigation.selectOnLeft = selectable;
			navigation.selectOnRight = selectable2;
			component.navigation = navigation;
			selectable = component;
		}
	}

	// Token: 0x06000BDB RID: 3035 RVA: 0x0007C0F8 File Offset: 0x0007A2F8
	public GameObject CreateNewBuildingButton(GameObject building)
	{
		Overworld_Structure component = building.GetComponent<Overworld_Structure>();
		if (!component)
		{
			return null;
		}
		return this.AddBuildingButton(new Overworld_BuildingManager.Building
		{
			category = component.category,
			prefab = building,
			costs = component.costs
		});
	}

	// Token: 0x06000BDC RID: 3036 RVA: 0x0007C144 File Offset: 0x0007A344
	public void RemoveBuildingButtonIfItExists(GameObject building)
	{
		this.RemoveBuildingButtonFromTransform(this.buildingParent, building);
		this.RemoveBuildingButtonFromTransform(this.tilemapParent, building);
		this.RemoveBuildingButtonFromTransform(this.decorationParent, building);
		this.RemoveBuildingButtonFromTransform(this.decoration2Parent, building);
		this.RemoveBuildingButtonFromTransform(this.decoration3Parent, building);
	}

	// Token: 0x06000BDD RID: 3037 RVA: 0x0007C194 File Offset: 0x0007A394
	public void RemoveBuildingButtonFromTransform(Transform parent, GameObject building)
	{
		foreach (object obj in parent)
		{
			Transform transform = (Transform)obj;
			Overworld_Button component = transform.GetComponent<Overworld_Button>();
			if (component && component.objectToSpawn == building)
			{
				transform.SetParent(null);
				Object.Destroy(transform.gameObject);
			}
		}
	}

	// Token: 0x06000BDE RID: 3038 RVA: 0x0007C210 File Offset: 0x0007A410
	public GameObject AddBuildingButton(Overworld_BuildingManager.Building building)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.buildingButtonPrefab);
		Overworld_Button buttonScript = gameObject.GetComponent<Overworld_Button>();
		buttonScript.SetBuilding(building);
		if (building.category == Overworld_BuildingManager.BuildingCategory.Building)
		{
			this.buildingParent.gameObject.SetActive(true);
			gameObject.transform.SetParent(this.buildingParent);
		}
		else if (building.category == Overworld_BuildingManager.BuildingCategory.Tile)
		{
			this.tilemapParent.gameObject.SetActive(true);
			gameObject.transform.SetParent(this.tilemapParent);
			EventTrigger component = gameObject.GetComponent<EventTrigger>();
			if (component)
			{
				Object.Destroy(component);
			}
			gameObject.GetComponent<Button>().onClick.AddListener(delegate
			{
				buttonScript.SpawnBuilding();
			});
		}
		else if (building.category == Overworld_BuildingManager.BuildingCategory.Decoration)
		{
			this.decorationParent.gameObject.SetActive(true);
			gameObject.transform.SetParent(this.decorationParent);
		}
		else if (building.category == Overworld_BuildingManager.BuildingCategory.Decoration2)
		{
			this.decoration2Parent.gameObject.SetActive(true);
			gameObject.transform.SetParent(this.decoration2Parent);
		}
		else if (building.category == Overworld_BuildingManager.BuildingCategory.Decoration3)
		{
			this.decoration3Parent.gameObject.SetActive(true);
			gameObject.transform.SetParent(this.decoration3Parent);
		}
		gameObject.transform.localScale = Vector3.one;
		return gameObject;
	}

	// Token: 0x06000BDF RID: 3039 RVA: 0x0007C36D File Offset: 0x0007A56D
	public void SelectBuildingMenu()
	{
		if (!Overworld_Manager.IsFreeToMove())
		{
			return;
		}
		DigitalCursor.main.SelectFirstSelectableInElementInstantly(this.hammerIcon.transform);
		DigitalCursor.main.Show();
		Overworld_Manager.main.SetState(Overworld_Manager.State.INMENU);
	}

	// Token: 0x06000BE0 RID: 3040 RVA: 0x0007C3A1 File Offset: 0x0007A5A1
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.B))
		{
			this.AddBuildingButton(this.buildingList.buildings[0]);
		}
	}

	// Token: 0x04000994 RID: 2452
	[SerializeField]
	private GameObject buildButtons;

	// Token: 0x04000995 RID: 2453
	[SerializeField]
	public GameObject destroyButton;

	// Token: 0x04000996 RID: 2454
	public static Overworld_BuildingManager main;

	// Token: 0x04000997 RID: 2455
	[SerializeField]
	private Overworld_BuildingList buildingList;

	// Token: 0x04000998 RID: 2456
	[SerializeField]
	private Transform buildingParent;

	// Token: 0x04000999 RID: 2457
	[SerializeField]
	private Transform hammerIcon;

	// Token: 0x0400099A RID: 2458
	[SerializeField]
	private Transform tilemapParent;

	// Token: 0x0400099B RID: 2459
	[SerializeField]
	private Transform decorationParent;

	// Token: 0x0400099C RID: 2460
	[SerializeField]
	private Transform decoration2Parent;

	// Token: 0x0400099D RID: 2461
	[SerializeField]
	private Transform decoration3Parent;

	// Token: 0x0400099E RID: 2462
	[SerializeField]
	private GameObject buildingButtonPrefab;

	// Token: 0x0400099F RID: 2463
	[SerializeField]
	private List<GameObject> allStructures = new List<GameObject>();

	// Token: 0x040009A0 RID: 2464
	[SerializeField]
	private List<GameObject> allTiles = new List<GameObject>();

	// Token: 0x020003E9 RID: 1001
	public enum BuildingCategory
	{
		// Token: 0x04001733 RID: 5939
		Building,
		// Token: 0x04001734 RID: 5940
		Tile,
		// Token: 0x04001735 RID: 5941
		Decoration,
		// Token: 0x04001736 RID: 5942
		Decoration2,
		// Token: 0x04001737 RID: 5943
		Decoration3
	}

	// Token: 0x020003EA RID: 1002
	[Serializable]
	public class Building
	{
		// Token: 0x04001738 RID: 5944
		public Overworld_BuildingManager.BuildingCategory category;

		// Token: 0x04001739 RID: 5945
		public GameObject prefab;

		// Token: 0x0400173A RID: 5946
		public RuleTile tile;

		// Token: 0x0400173B RID: 5947
		public List<Overworld_ResourceManager.Resource> costs = new List<Overworld_ResourceManager.Resource>();

		// Token: 0x0400173C RID: 5948
		public bool unlocked = true;
	}
}
