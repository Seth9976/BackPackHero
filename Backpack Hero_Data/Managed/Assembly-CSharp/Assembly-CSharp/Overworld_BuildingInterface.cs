using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200007E RID: 126
public class Overworld_BuildingInterface : MonoBehaviour
{
	// Token: 0x060002A2 RID: 674 RVA: 0x0000FA54 File Offset: 0x0000DC54
	public void SetupWarning(string text)
	{
		if (!this.townHallWarning)
		{
			return;
		}
		foreach (object obj in this.unlockBarParent)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
		TextMeshProUGUI componentInChildren = Object.Instantiate<GameObject>(this.townHallWarning, this.unlockBarParent).GetComponentInChildren<TextMeshProUGUI>();
		if (componentInChildren)
		{
			componentInChildren.text = text;
		}
	}

	// Token: 0x060002A3 RID: 675 RVA: 0x0000FAE4 File Offset: 0x0000DCE4
	public void SetupResearches()
	{
		foreach (object obj in this.unlockBarParent)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
		foreach (GameObject gameObject in this.GetResearchBars())
		{
			gameObject.transform.SetParent(this.unlockBarParent);
			gameObject.transform.localScale = Vector3.one;
		}
	}

	// Token: 0x060002A4 RID: 676 RVA: 0x0000FB9C File Offset: 0x0000DD9C
	public bool NewResearchIsAvailable()
	{
		foreach (Overworld_BuildingInterface.Research research in this.researches)
		{
			if (research.Available() && !research.HasBeenSeen())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060002A5 RID: 677 RVA: 0x0000FC00 File Offset: 0x0000DE00
	public List<GameObject> GetResearchBars()
	{
		List<GameObject> list = new List<GameObject>();
		foreach (Overworld_BuildingInterface.Research research in this.researches)
		{
			research.Load(this);
		}
		this.researches.Sort((Overworld_BuildingInterface.Research x, Overworld_BuildingInterface.Research y) => x.GetName().CompareTo(y.GetName()));
		this.researches.Sort(delegate(Overworld_BuildingInterface.Research x, Overworld_BuildingInterface.Research y)
		{
			if (x.type <= y.type)
			{
				return 1;
			}
			return -1;
		});
		this.researches.Sort((Overworld_BuildingInterface.Research x, Overworld_BuildingInterface.Research y) => y.isFavorite.CompareTo(x.isFavorite));
		this.researches.Sort((Overworld_BuildingInterface.Research x, Overworld_BuildingInterface.Research y) => x.IsComplete().CompareTo(y.IsComplete()));
		foreach (Overworld_BuildingInterface.Research research2 in this.researches)
		{
			if (research2.Available())
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.unlockBarPrefab);
				ResearchUnlockBar component = gameObject.GetComponent<ResearchUnlockBar>();
				component.Setup(research2, this);
				component.SetLauncherSprite(true);
				list.Add(gameObject);
			}
		}
		return list;
	}

	// Token: 0x060002A6 RID: 678 RVA: 0x0000FD6C File Offset: 0x0000DF6C
	private void OnEnable()
	{
		Debug.Log("OnEnable" + base.name);
		bool flag = this.setupAgain;
	}

	// Token: 0x060002A7 RID: 679 RVA: 0x0000FD8A File Offset: 0x0000DF8A
	private void OnDisable()
	{
		Debug.Log("OnDisable" + base.name);
	}

	// Token: 0x060002A8 RID: 680 RVA: 0x0000FDA4 File Offset: 0x0000DFA4
	private void Start()
	{
		if (this.type == Overworld_BuildingInterface.Type.NewResearcher)
		{
			this.SetupResearches();
		}
		if (this.selectedBuildingText)
		{
			this.selectedBuildingText.gameObject.SetActive(false);
		}
		if (this.titleText && this.descriptorText)
		{
			this.titleText.text = LangaugeManager.main.GetTextByKey(Item2.GetDisplayName(base.name));
			this.descriptorText.gameObject.SetActive(false);
		}
		if (!MetaProgressSaveManager.main.researchBuildingsDiscovered.Contains(Item2.GetDisplayName(base.name)))
		{
			MetaProgressSaveManager.main.researchBuildingsDiscovered.Add(Item2.GetDisplayName(base.name));
		}
		if (this.researchedAlreadyText)
		{
			this.researchedAlreadyText.gameObject.SetActive(false);
		}
		Overworld_Structure component = this.overworld_BuildingInterfaceLauncher.GetComponent<Overworld_Structure>();
		if (component)
		{
			this.currentEfficiencyBonus = component.currentEfficiencyBonus;
			this.SetEfficiencyText(this.currentEfficiencyBonus);
		}
		else
		{
			this.currentEfficiencyBonus = 100f;
			if (this.efficiencyText)
			{
				this.efficiencyText.gameObject.SetActive(false);
			}
		}
		if (this.type == Overworld_BuildingInterface.Type.NewResearcher)
		{
			this.setupAgain = true;
		}
		if (this.type == Overworld_BuildingInterface.Type.TownHall)
		{
			Overworld_BuildingInterfaceLauncher[] array = Object.FindObjectsOfType<Overworld_BuildingInterfaceLauncher>();
			foreach (TownHallButton townHallButton in base.GetComponentsInChildren<TownHallButton>())
			{
				if (townHallButton.IsVisible(array))
				{
					townHallButton.gameObject.SetActive(true);
				}
				else
				{
					townHallButton.gameObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x060002A9 RID: 681 RVA: 0x0000FF34 File Offset: 0x0000E134
	public void SetBuildingText(string name)
	{
		if (!this.selectedBuildingText)
		{
			return;
		}
		string textByKey = LangaugeManager.main.GetTextByKey(Item2.GetDisplayName(name));
		this.selectedBuildingText.gameObject.SetActive(true);
		this.selectedBuildingText.text = textByKey;
	}

	// Token: 0x060002AA RID: 682 RVA: 0x0000FF80 File Offset: 0x0000E180
	public void SetEfficiencyText(float num)
	{
		if (!this.efficiencyText)
		{
			return;
		}
		this.efficiencyText.text = LangaugeManager.main.GetTextByKey("bumoef1").Replace("/x", num.ToString() + "%");
		LangaugeManager.main.SetFont(this.efficiencyText.transform);
	}

	// Token: 0x060002AB RID: 683 RVA: 0x0000FFE8 File Offset: 0x0000E1E8
	private void Update()
	{
		if (this.type == Overworld_BuildingInterface.Type.Researcher)
		{
			string firstItemString = this.overworldInventory.GetFirstItemString();
			if (firstItemString == this.lastItem)
			{
				return;
			}
			this.lastItem = firstItemString;
			this.ConsiderResearch();
		}
	}

	// Token: 0x060002AC RID: 684 RVA: 0x00010026 File Offset: 0x0000E226
	public void DeactivateButton()
	{
		this.button.SetActive(false);
	}

	// Token: 0x060002AD RID: 685 RVA: 0x00010034 File Offset: 0x0000E234
	public void DoResearch()
	{
		if (this.researchRoutine != null)
		{
			return;
		}
		string firstItemString = this.overworldInventory.GetFirstItemString();
		List<Overworld_ResourceManager.Resource> resources = MetaProgressSaveManager.main.GetResearch(firstItemString).resources;
		List<Overworld_ResourceManager.Resource> resourcesWithEfficiency = this.GetResourcesWithEfficiency(resources);
		if (!Overworld_ResourceManager.main.HasEnoughResources(resourcesWithEfficiency, -1))
		{
			PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm35"));
			return;
		}
		this.researchRoutine = base.StartCoroutine(this.DoResearchRoutine());
	}

	// Token: 0x060002AE RID: 686 RVA: 0x000100A9 File Offset: 0x0000E2A9
	private IEnumerator DoResearchRoutine()
	{
		if (!this.slider)
		{
			this.CompleteResearch();
			base.StopCoroutine(this.researchRoutine);
			this.researchRoutine = null;
			yield break;
		}
		this.slider.value = 0f;
		float time = 0f;
		float totalTime = 0.5f;
		SoundManager.main.PlaySFX("research");
		while (time < totalTime)
		{
			time += Time.deltaTime;
			this.slider.value = Mathf.Lerp(0f, 1f, time / totalTime);
			yield return null;
		}
		this.CompleteResearch();
		base.StopCoroutine(this.researchRoutine);
		this.researchRoutine = null;
		yield break;
	}

	// Token: 0x060002AF RID: 687 RVA: 0x000100B8 File Offset: 0x0000E2B8
	private void CompleteResearch()
	{
		string firstItemString = this.overworldInventory.GetFirstItemString();
		MetaProgressSaveManager.Research research = MetaProgressSaveManager.main.GetResearch(firstItemString);
		if (research == null || MetaProgressSaveManager.main.ResearchComplete(research))
		{
			return;
		}
		List<Overworld_ResourceManager.Resource> resources = research.resources;
		List<Overworld_ResourceManager.Resource> resourcesWithEfficiency = this.GetResourcesWithEfficiency(resources);
		if (!Overworld_ResourceManager.main.HasEnoughResources(resourcesWithEfficiency, 1))
		{
			SoundManager.main.PlaySFX("negative");
			return;
		}
		if (DebugItemManager.main.GetItem2ByName(firstItemString).itemType.Contains(Item2.ItemType.Magic))
		{
			MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.completedMagicResearch, 1);
		}
		Overworld_ResourceManager.ChangeResourceAmountBy(MetaProgressSaveManager.main.resources, resourcesWithEfficiency, -1);
		MetaProgressSaveManager.main.CompleteResearch(research);
		this.overworldInventory.RemoveAll();
		this.ConsiderResearch();
		OverworldInventory.UpdateAllPages();
	}

	// Token: 0x060002B0 RID: 688 RVA: 0x00010174 File Offset: 0x0000E374
	public void ConsiderResearch()
	{
		if (this.slider)
		{
			this.slider.value = 0f;
		}
		this.resourceDisplayPanelBeforeBonus.gameObject.SetActive(false);
		this.efficiencyText.gameObject.SetActive(false);
		this.button.SetActive(false);
		if (this.researchedAlreadyText)
		{
			this.researchedAlreadyText.gameObject.SetActive(false);
		}
		string firstItemString = this.overworldInventory.GetFirstItemString();
		if (firstItemString == null || firstItemString == "")
		{
			return;
		}
		MetaProgressSaveManager.Research research = MetaProgressSaveManager.main.GetResearch(firstItemString);
		if (research == null)
		{
			if (this.researchedAlreadyText)
			{
				this.researchedAlreadyText.gameObject.SetActive(true);
				this.researchedAlreadyText.text = LangaugeManager.main.GetTextByKey("gm64b");
				return;
			}
		}
		else if (MetaProgressSaveManager.main.ResearchComplete(research))
		{
			if (this.researchedAlreadyText)
			{
				this.researchedAlreadyText.gameObject.SetActive(true);
				this.researchedAlreadyText.text = LangaugeManager.main.GetTextByKey("gm64");
			}
			if (this.slider)
			{
				this.slider.value = 1f;
				return;
			}
		}
		else
		{
			if (this.researchedAlreadyText)
			{
				this.researchedAlreadyText.gameObject.SetActive(false);
			}
			this.resourceDisplayPanelBeforeBonus.gameObject.SetActive(true);
			this.efficiencyText.gameObject.SetActive(true);
			List<Overworld_ResourceManager.Resource> resources = research.resources;
			this.resourceDisplayPanelBeforeBonus.SetupResources(resources);
			List<Overworld_ResourceManager.Resource> resourcesWithEfficiency = this.GetResourcesWithEfficiency(resources);
			this.resourceDisplayPanel.SetupResources(resourcesWithEfficiency);
			this.button.SetActive(true);
		}
	}

	// Token: 0x060002B1 RID: 689 RVA: 0x0001032C File Offset: 0x0000E52C
	private void DestroyItemButtonInSlotCurrently()
	{
		this.overworld_BuildingInterfaceLauncher.storedItems.Clear();
		ItemSlot componentInChildren = base.GetComponentInChildren<ItemSlot>();
		Overworld_InventoryItemButton itemButton = componentInChildren.GetItemButton();
		if (itemButton)
		{
			componentInChildren.RemoveItemButton(itemButton);
			Object.Destroy(itemButton.gameObject);
		}
	}

	// Token: 0x060002B2 RID: 690 RVA: 0x00010374 File Offset: 0x0000E574
	public void DoGrinding()
	{
		int num = 0;
		List<Overworld_ResourceManager.Resource> list = new List<Overworld_ResourceManager.Resource>();
		foreach (string text in this.overworld_BuildingInterfaceLauncher.storedItems)
		{
			Item2 item2ByName = DebugItemManager.main.GetItem2ByName(text);
			list = Overworld_ResourceManager.AddResources(list, item2ByName.resourcesToGet);
			num += this.ConsiderGridValue(item2ByName);
		}
		list = Overworld_ResourceManager.AddResources(this.GetResourcesToGet(num), list);
		List<Overworld_ResourceManager.Resource> resourcesWithEfficiency = this.GetResourcesWithEfficiency(list);
		Overworld_ResourceManager.ChangeResourceAmountBy(MetaProgressSaveManager.main.resources, resourcesWithEfficiency, 1);
		this.overworldInventory.RemoveAll();
		MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.soldSomethingInTown);
		this.UpdateGrindingValue();
	}

	// Token: 0x060002B3 RID: 691 RVA: 0x0001043C File Offset: 0x0000E63C
	public void UpdateGrindingValue()
	{
		int num = 0;
		List<Overworld_ResourceManager.Resource> list = new List<Overworld_ResourceManager.Resource>();
		foreach (string text in this.overworld_BuildingInterfaceLauncher.storedItems)
		{
			Item2 item2ByName = DebugItemManager.main.GetItem2ByName(text);
			list = Overworld_ResourceManager.AddResources(list, item2ByName.resourcesToGet);
			num += this.ConsiderGridValue(item2ByName);
		}
		list = Overworld_ResourceManager.AddResources(this.GetResourcesToGet(num), list);
		this.resourceDisplayPanelBeforeBonus.gameObject.SetActive(true);
		this.resourceDisplayPanel.gameObject.SetActive(true);
		if (num == 0)
		{
			this.efficiencyText.gameObject.SetActive(false);
			this.button.SetActive(false);
		}
		else
		{
			this.efficiencyText.gameObject.SetActive(true);
			this.button.SetActive(true);
		}
		this.resourceDisplayPanelBeforeBonus.SetupResources(list);
		List<Overworld_ResourceManager.Resource> resourcesWithEfficiency = this.GetResourcesWithEfficiency(list);
		this.resourceDisplayPanel.SetupResources(resourcesWithEfficiency);
	}

	// Token: 0x060002B4 RID: 692 RVA: 0x0001054C File Offset: 0x0000E74C
	private List<Overworld_ResourceManager.Resource> GetResourcesWithEfficiency(List<Overworld_ResourceManager.Resource> resources)
	{
		List<Overworld_ResourceManager.Resource> list = new List<Overworld_ResourceManager.Resource>();
		foreach (Overworld_ResourceManager.Resource resource in resources)
		{
			Overworld_ResourceManager.Resource resource2 = new Overworld_ResourceManager.Resource();
			resource2.type = resource.type;
			if (this.type == Overworld_BuildingInterface.Type.Grinder)
			{
				resource2.amount = Mathf.RoundToInt((float)resource.amount * (this.currentEfficiencyBonus / 100f));
			}
			else if (this.type == Overworld_BuildingInterface.Type.Researcher)
			{
				resource2.amount = Mathf.RoundToInt((float)resource.amount / (this.currentEfficiencyBonus / 100f));
			}
			list.Add(resource2);
		}
		return list;
	}

	// Token: 0x060002B5 RID: 693 RVA: 0x00010604 File Offset: 0x0000E804
	public List<Overworld_ResourceManager.Resource> GetResourcesToGet(int value)
	{
		List<Overworld_ResourceManager.Resource> list = new List<Overworld_ResourceManager.Resource>();
		foreach (Overworld_BuildingInterface.ResourceMultiplier resourceMultiplier in this.resourceMultipliers)
		{
			int num = Mathf.RoundToInt(resourceMultiplier.multiplier * (float)value);
			list.Add(new Overworld_ResourceManager.Resource
			{
				type = resourceMultiplier.resourceType,
				amount = num
			});
		}
		return list;
	}

	// Token: 0x060002B6 RID: 694 RVA: 0x0001068C File Offset: 0x0000E88C
	public int ConsiderGridValue(Item2 item2)
	{
		if (item2 == null)
		{
			return -1;
		}
		float num = (float)item2.GetItemMovement().GetSpacesNeeded();
		switch (item2.rarity)
		{
		case Item2.Rarity.Common:
			num *= 1f;
			break;
		case Item2.Rarity.Uncommon:
			num *= 1.5f;
			break;
		case Item2.Rarity.Rare:
			num *= 2f;
			break;
		case Item2.Rarity.Legendary:
			num *= 3f;
			break;
		}
		return Mathf.RoundToInt(num);
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x000106FC File Offset: 0x0000E8FC
	public void Close()
	{
		Overworld_Structure.SetupAllExclamationPoints();
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060002B8 RID: 696 RVA: 0x00010710 File Offset: 0x0000E910
	public void OnDestroy()
	{
		foreach (Overworld_BuildingInterface.Research research in this.researches)
		{
			string name = research.GetName();
			if (!(MetaProgressSaveManager.main.GetNewResearch(name) == ""))
			{
				string text = research.Stringify();
				MetaProgressSaveManager.main.AddNewResearch(name, text);
			}
		}
		if (this.overworld_BuildingInterfaceLauncher)
		{
			this.overworld_BuildingInterfaceLauncher.CloseInterface();
		}
	}

	// Token: 0x040001B6 RID: 438
	[SerializeField]
	private MetaProgressSaveManager.MetaProgressMarker metaProgressMarker;

	// Token: 0x040001B7 RID: 439
	[SerializeField]
	public Sprite launcherSpriteDefault;

	// Token: 0x040001B8 RID: 440
	[SerializeField]
	public GameObject townHallWarning;

	// Token: 0x040001B9 RID: 441
	[SerializeField]
	public List<Overworld_BuildingInterface.Research> researches;

	// Token: 0x040001BA RID: 442
	[SerializeField]
	private GameObject unlockBarPrefab;

	// Token: 0x040001BB RID: 443
	[SerializeField]
	private Transform unlockBarParent;

	// Token: 0x040001BC RID: 444
	[SerializeField]
	private TextMeshProUGUI titleText;

	// Token: 0x040001BD RID: 445
	[SerializeField]
	private TextMeshProUGUI descriptorText;

	// Token: 0x040001BE RID: 446
	[SerializeField]
	public Overworld_BuildingInterface.Type type;

	// Token: 0x040001BF RID: 447
	[SerializeField]
	public Overworld_BuildingInterfaceLauncher overworld_BuildingInterfaceLauncher;

	// Token: 0x040001C0 RID: 448
	[SerializeField]
	private GameObject button;

	// Token: 0x040001C1 RID: 449
	[SerializeField]
	private List<Item2.ItemType> itemsToInteractWith;

	// Token: 0x040001C2 RID: 450
	[SerializeField]
	private TextMeshProUGUI efficiencyText;

	// Token: 0x040001C3 RID: 451
	[SerializeField]
	private TextMeshProUGUI selectedBuildingText;

	// Token: 0x040001C4 RID: 452
	[SerializeField]
	private OverworldInventory overworldInventory;

	// Token: 0x040001C5 RID: 453
	[SerializeField]
	private Overworld_ResourceDisplayPanel resourceDisplayPanelBeforeBonus;

	// Token: 0x040001C6 RID: 454
	[SerializeField]
	private Overworld_ResourceDisplayPanel resourceDisplayPanel;

	// Token: 0x040001C7 RID: 455
	[SerializeField]
	private TextMeshProUGUI researchedAlreadyText;

	// Token: 0x040001C8 RID: 456
	public float currentEfficiencyBonus = 100f;

	// Token: 0x040001C9 RID: 457
	[SerializeField]
	private Slider slider;

	// Token: 0x040001CA RID: 458
	[SerializeField]
	private List<Overworld_BuildingInterface.ResourceMultiplier> resourceMultipliers;

	// Token: 0x040001CB RID: 459
	private bool setupAgain;

	// Token: 0x040001CC RID: 460
	private string lastItem = "";

	// Token: 0x040001CD RID: 461
	private Coroutine researchRoutine;

	// Token: 0x0200028B RID: 651
	[Serializable]
	public class Research
	{
		// Token: 0x06001390 RID: 5008 RVA: 0x000B0648 File Offset: 0x000AE848
		public string GetName()
		{
			if (this.item)
			{
				return this.item.name;
			}
			if (this.mission)
			{
				return this.mission.name;
			}
			if (this.name != "")
			{
				return this.name;
			}
			return "";
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x000B06A5 File Offset: 0x000AE8A5
		private void SetFavorite(bool x)
		{
			this.isFavorite = x;
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x000B06B0 File Offset: 0x000AE8B0
		public bool Available()
		{
			foreach (GameObject gameObject in this.itemsRequired)
			{
				Item2 component = gameObject.GetComponent<Item2>();
				if (component)
				{
					if (!MetaProgressSaveManager.ConditionsMet(component.conditions))
					{
						return false;
					}
					if (component.storyModeAvailabilityType == Item2.AvailabilityType.UnlockDependent && !MetaProgressSaveManager.main.itemsUnlocked.Contains(Item2.GetDisplayName(component.name)))
					{
						return false;
					}
				}
			}
			if (this.item)
			{
				Item2 component2 = this.item.GetComponent<Item2>();
				if (component2)
				{
					if (component2 && !MetaProgressSaveManager.ConditionsMet(component2.conditions))
					{
						return false;
					}
					if (component2.validForCharacters.Count > 0 && !component2.validForCharacters.Contains(Character.CharacterName.Purse))
					{
						bool flag = false;
						if (component2.validForCharacters.Contains(Character.CharacterName.Satchel) && MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedSatchel))
						{
							flag = true;
						}
						if (component2.validForCharacters.Contains(Character.CharacterName.CR8) && MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedCR8))
						{
							flag = true;
						}
						if (component2.validForCharacters.Contains(Character.CharacterName.Pochette) && MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedPochette))
						{
							flag = true;
						}
						if (component2.validForCharacters.Contains(Character.CharacterName.Tote) && MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedTote))
						{
							flag = true;
						}
						if (!flag)
						{
							return false;
						}
					}
				}
			}
			if (this.mission)
			{
				if (this.mission.validForCharacter.characterName == Character.CharacterName.Satchel && !MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedSatchel))
				{
					return false;
				}
				if (this.mission.validForCharacter.characterName == Character.CharacterName.CR8 && !MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedCR8))
				{
					return false;
				}
				if (this.mission.validForCharacter.characterName == Character.CharacterName.Pochette && !MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedPochette))
				{
					return false;
				}
				if (this.mission.validForCharacter.characterName == Character.CharacterName.Tote && !MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedTote))
				{
					return false;
				}
			}
			return MetaProgressSaveManager.ConditionsMet(this.conditions);
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x000B08D8 File Offset: 0x000AEAD8
		public bool HasBeenSeen()
		{
			return !(MetaProgressSaveManager.main.GetNewResearch(this.GetName()) == "");
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x000B08FC File Offset: 0x000AEAFC
		public void SetupList()
		{
			if (this.mission)
			{
				this.type = Overworld_BuildingInterface.Research.Type.Mission;
			}
			else if (this.item)
			{
				this.type = Overworld_BuildingInterface.Research.Type.Item;
			}
			else if (this.name != "" && this.type != Overworld_BuildingInterface.Research.Type.MetaProgressMarker)
			{
				this.type = Overworld_BuildingInterface.Research.Type.lore;
			}
			else
			{
				this.type = Overworld_BuildingInterface.Research.Type.MetaProgressMarker;
			}
			this.submittedAlready = new List<bool>();
			for (int i = 0; i < this.resourcesRequired.Count; i++)
			{
				this.submittedAlready.Add(false);
			}
			for (int j = 0; j < this.itemTypesRequired.Count; j++)
			{
				this.submittedAlready.Add(false);
			}
			for (int k = 0; k < this.itemsRequired.Count; k++)
			{
				this.submittedAlready.Add(false);
			}
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x000B09D4 File Offset: 0x000AEBD4
		public void SubmitCost(int num)
		{
			if (this.submittedAlready == null || this.submittedAlready.Count == 0)
			{
				this.SetupList();
			}
			this.submittedAlready[num] = true;
			string text = this.Stringify();
			MetaProgressSaveManager.main.AddNewResearch(this.GetName(), text);
			for (int i = 0; i < this.submittedAlready.Count; i++)
			{
				if (!this.submittedAlready[i])
				{
					return;
				}
			}
			if (this.overworld_BuildingInterface && this.overworld_BuildingInterface.metaProgressMarker != MetaProgressSaveManager.MetaProgressMarker.none)
			{
				MetaProgressSaveManager.main.AddMetaProgressMarker(this.overworld_BuildingInterface.metaProgressMarker, 1);
			}
			if (this.type == Overworld_BuildingInterface.Research.Type.Item && this.item)
			{
				Item2 component = this.item.GetComponent<Item2>();
				if (component)
				{
					MetaProgressSaveManager.main.UnlockItem(component);
					Overworld_Manager.main.OpenNewItemWindow(component);
				}
				Overworld_Structure component2 = this.item.GetComponent<Overworld_Structure>();
				if (component2)
				{
					Overworld_BuildingManager.main.AddBuilding(component2.name);
					Overworld_Manager.main.OpenNewConstructionWindow(component2);
				}
				SellingTile component3 = this.item.GetComponent<SellingTile>();
				if (component3)
				{
					Overworld_BuildingManager.main.AddTile(component3.name);
					Overworld_Manager.main.OpenNewConstructionWindow(component3);
				}
			}
			if (this.type == Overworld_BuildingInterface.Research.Type.Mission && this.mission)
			{
				if (!MetaProgressSaveManager.main.missionsUnlocked.Contains(this.mission.name))
				{
					MetaProgressSaveManager.main.missionsUnlocked.Add(this.mission.name);
				}
				Overworld_Manager.main.OpenNewMissionWindow(this.mission);
			}
			if (this.type == Overworld_BuildingInterface.Research.Type.lore)
			{
				Overworld_InventoryManager.main.AddLore(this.name);
			}
			if (this.type == Overworld_BuildingInterface.Research.Type.MetaProgressMarker)
			{
				MetaProgressSaveManager.main.AddMetaProgressMarker((MetaProgressSaveManager.MetaProgressMarker)Enum.Parse(typeof(MetaProgressSaveManager.MetaProgressMarker), this.name), 1);
			}
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x000B0BC4 File Offset: 0x000AEDC4
		public void Load(Overworld_BuildingInterface overworld_BuildingInterface = null)
		{
			if (overworld_BuildingInterface != null)
			{
				this.overworld_BuildingInterface = overworld_BuildingInterface;
			}
			this.isFavorite = false;
			this.SetupList();
			string text = "";
			if (this.item)
			{
				text = MetaProgressSaveManager.main.GetNewResearch(this.item.name);
			}
			else if (this.mission)
			{
				text = MetaProgressSaveManager.main.GetNewResearch(this.mission.name);
			}
			else if (this.name != "")
			{
				text = MetaProgressSaveManager.main.GetNewResearch(this.name);
			}
			if (text != "")
			{
				if (this.item)
				{
					text = text.Substring(this.item.name.Length);
				}
				else if (this.mission)
				{
					text = text.Substring(this.mission.name.Length);
				}
				else if (this.name != "")
				{
					text = text.Substring(this.name.Length);
				}
				for (int i = 0; i < text.Length; i++)
				{
					if (text[i] == '!')
					{
						this.isFavorite = true;
					}
					if (this.submittedAlready.Count <= i)
					{
						break;
					}
					if (text[i] == '1')
					{
						this.submittedAlready[i] = true;
					}
				}
			}
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x000B0D2C File Offset: 0x000AEF2C
		public bool IsComplete()
		{
			this.Load(null);
			using (List<bool>.Enumerator enumerator = this.submittedAlready.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x000B0D88 File Offset: 0x000AEF88
		public bool IsComplete(int num)
		{
			return this.submittedAlready.Count > num && this.submittedAlready[num];
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x000B0DA8 File Offset: 0x000AEFA8
		public string Stringify()
		{
			string text = "";
			if (this.item)
			{
				text = this.item.name;
			}
			else if (this.mission)
			{
				text = this.mission.name;
			}
			else if (this.name != "")
			{
				text = this.name;
			}
			using (List<bool>.Enumerator enumerator = this.submittedAlready.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current)
					{
						text += "1";
					}
					else
					{
						text += "0";
					}
				}
			}
			if (this.isFavorite)
			{
				text += "!";
			}
			return text;
		}

		// Token: 0x04000F91 RID: 3985
		[Header("Item")]
		[SerializeField]
		public Overworld_BuildingInterface.Research.Type type;

		// Token: 0x04000F92 RID: 3986
		[HideInInspector]
		public Sprite launcherSprite;

		// Token: 0x04000F93 RID: 3987
		[HideInInspector]
		public string launcherName;

		// Token: 0x04000F94 RID: 3988
		public Missions mission;

		// Token: 0x04000F95 RID: 3989
		public GameObject item;

		// Token: 0x04000F96 RID: 3990
		public string name;

		// Token: 0x04000F97 RID: 3991
		public string hoverText;

		// Token: 0x04000F98 RID: 3992
		public Sprite sprite;

		// Token: 0x04000F99 RID: 3993
		[HideInInspector]
		public bool isFavorite;

		// Token: 0x04000F9A RID: 3994
		[Header("Resources Required")]
		[SerializeField]
		private List<MetaProgressSaveManager.MetaProgressCondition> conditions;

		// Token: 0x04000F9B RID: 3995
		public List<Overworld_ResourceManager.Resource> resourcesRequired;

		// Token: 0x04000F9C RID: 3996
		public List<Item2.ItemType> itemTypesRequired;

		// Token: 0x04000F9D RID: 3997
		public List<Item2.Rarity> itemRaritiesRequired;

		// Token: 0x04000F9E RID: 3998
		public List<GameObject> itemsRequired;

		// Token: 0x04000F9F RID: 3999
		[HideInInspector]
		public List<bool> submittedAlready;

		// Token: 0x04000FA0 RID: 4000
		[SerializeField]
		private Overworld_BuildingInterface overworld_BuildingInterface;

		// Token: 0x0200048C RID: 1164
		public enum Type
		{
			// Token: 0x04001A8B RID: 6795
			Item,
			// Token: 0x04001A8C RID: 6796
			Mission,
			// Token: 0x04001A8D RID: 6797
			lore,
			// Token: 0x04001A8E RID: 6798
			MetaProgressMarker
		}
	}

	// Token: 0x0200028C RID: 652
	public enum Type
	{
		// Token: 0x04000FA2 RID: 4002
		Grinder,
		// Token: 0x04000FA3 RID: 4003
		Researcher,
		// Token: 0x04000FA4 RID: 4004
		NewResearcher,
		// Token: 0x04000FA5 RID: 4005
		TownHall
	}

	// Token: 0x0200028D RID: 653
	[Serializable]
	private class ResourceMultiplier
	{
		// Token: 0x04000FA6 RID: 4006
		public Overworld_ResourceManager.Resource.Type resourceType;

		// Token: 0x04000FA7 RID: 4007
		public float multiplier;
	}
}
