using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000A5 RID: 165
public class Card : MonoBehaviour
{
	// Token: 0x060003AA RID: 938 RVA: 0x00015F86 File Offset: 0x00014186
	private void Awake()
	{
		if (Card.allCards.Contains(this))
		{
			return;
		}
		Card.allCards.Add(this);
	}

	// Token: 0x060003AB RID: 939 RVA: 0x00015FA1 File Offset: 0x000141A1
	private void OnDestroy()
	{
		Card.allCards.Remove(this);
	}

	// Token: 0x060003AC RID: 940 RVA: 0x00015FB0 File Offset: 0x000141B0
	public static void RemoveAllCursorCards()
	{
		foreach (Card card in Card.allCards)
		{
			if (!card.stuck)
			{
				Object.Destroy(card.gameObject);
			}
		}
	}

	// Token: 0x060003AD RID: 941 RVA: 0x00016010 File Offset: 0x00014210
	public void SetCharacter(Sprite sprite)
	{
		if (!this.characterIcon)
		{
			return;
		}
		if (!MetaProgressSaveManager.main.HasAnyMetaProgressMarker(new List<MetaProgressSaveManager.MetaProgressMarker>
		{
			MetaProgressSaveManager.MetaProgressMarker.unlockedSatchel,
			MetaProgressSaveManager.MetaProgressMarker.unlockedCR8,
			MetaProgressSaveManager.MetaProgressMarker.unlockedTote,
			MetaProgressSaveManager.MetaProgressMarker.unlockedPochette
		}))
		{
			this.characterIcon.enabled = false;
		}
		this.characterIcon.sprite = sprite;
	}

	// Token: 0x060003AE RID: 942 RVA: 0x00016074 File Offset: 0x00014274
	private void Start()
	{
		if (this.canvasGroup)
		{
			this.canvasGroup.blocksRaycasts = false;
		}
		GameObject gameObject = GameObject.FindGameObjectWithTag("SecondaryCanvas");
		if (gameObject)
		{
			this.canvas = gameObject.GetComponent<Canvas>();
			this.canvasRect = this.canvas.GetComponent<RectTransform>();
		}
		else
		{
			this.canvas = base.GetComponentInParent<Canvas>();
			this.canvasRect = this.canvas.GetComponent<RectTransform>();
		}
		if (this.fill)
		{
			this.fillParent = this.fill.transform.parent.GetComponent<RectTransform>();
		}
		if (!this.ignoreLayOut)
		{
			if (!this.stuck)
			{
				this.canvasGroup.alpha = 0f;
			}
			base.StartCoroutine(this.FindHeight());
		}
		bool flag = this.canBeMovedUp;
		base.transform.SetAsLastSibling();
	}

	// Token: 0x060003AF RID: 943 RVA: 0x00016150 File Offset: 0x00014350
	public void IgnoreShop()
	{
		this.priceTag.SetActive(false);
	}

	// Token: 0x060003B0 RID: 944 RVA: 0x00016160 File Offset: 0x00014360
	public void MakeStuck()
	{
		this.canvasGroup.blocksRaycasts = true;
		this.stuck = true;
		BoxCollider2D[] componentsInChildren = base.GetComponentsInChildren<BoxCollider2D>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enabled = true;
		}
		SimpleHoverText[] componentsInChildren2 = base.GetComponentsInChildren<SimpleHoverText>();
		for (int i = 0; i < componentsInChildren2.Length; i++)
		{
			componentsInChildren2[i].enabled = true;
		}
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x000161BC File Offset: 0x000143BC
	public void GetDescriptionMission(Missions mission, GameObject p)
	{
		this.parent = p;
		GameObject gameObject = Object.Instantiate<GameObject>(this.textPrefab, base.transform);
		gameObject.GetComponent<TextMeshProUGUI>().text = Missions.MissionTranslationName(mission);
		gameObject.GetComponent<TextMeshProUGUI>().enableAutoSizing = true;
		this.SetCharacter(mission.validForCharacter.mapCharacterSprite[0]);
		foreach (RunType.RunProperty runProperty in mission.runProperties)
		{
			RunType.RunProperty.Type type = runProperty.type;
			if (type == RunType.RunProperty.Type.mustKeep || type == RunType.RunProperty.Type.startWith)
			{
				GameObject gameObject2 = Object.Instantiate<GameObject>(this.textPrefab, base.transform);
				string text = RunTypeSelector.GetProperties(new List<RunType.RunProperty> { runProperty }).Split(':', StringSplitOptions.None)[0];
				gameObject2.GetComponent<TextMeshProUGUI>().text = text;
				GameObject gameObject3 = Object.Instantiate<GameObject>(this.itemsPrefab, base.transform);
				gameObject3.GetComponent<RectTransform>().sizeDelta = this.rectTransform.sizeDelta;
				using (List<GameObject>.Enumerator enumerator2 = runProperty.assignedPrefabs.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						GameObject gameObject4 = enumerator2.Current;
						Overworld_InventoryItemButton componentInChildren = Object.Instantiate<GameObject>(this.overworldItemButtonPrefab, gameObject3.transform).GetComponentInChildren<Overworld_InventoryItemButton>();
						componentInChildren.draggable = false;
						componentInChildren.Setup(gameObject4, -1);
					}
					continue;
				}
			}
			string text2 = RunTypeSelector.GetProperties(new List<RunType.RunProperty> { runProperty });
			text2 = text2.Trim();
			if (text2.EndsWith("<br>"))
			{
				text2 = text2.Remove(text2.LastIndexOf("<br>"));
			}
			if (text2.Length > 4)
			{
				Object.Instantiate<GameObject>(this.textPrefab, base.transform).GetComponent<TextMeshProUGUI>().text = text2;
			}
		}
		Object.Instantiate<GameObject>(this.textPrefab, base.transform).GetComponent<TextMeshProUGUI>().text = "<br>" + LangaugeManager.main.GetTextByKey("mreward");
		GameObject gameObject5 = Object.Instantiate<GameObject>(this.itemsPrefab, base.transform);
		gameObject5.GetComponent<RectTransform>().sizeDelta = this.rectTransform.sizeDelta;
		foreach (GameObject gameObject6 in mission.rewards)
		{
			Overworld_InventoryItemButton componentInChildren2 = Object.Instantiate<GameObject>(this.overworldItemButtonPrefab, gameObject5.transform).GetComponentInChildren<Overworld_InventoryItemButton>();
			componentInChildren2.draggable = false;
			Item2 component = gameObject6.GetComponent<Item2>();
			if (component && !component.itemType.Contains(Item2.ItemType.Loot))
			{
				componentInChildren2.ShowAsNewUnlock();
			}
			componentInChildren2.Setup(gameObject6, -1);
		}
		foreach (Missions missions in mission.rewardsMissions)
		{
			Overworld_InventoryItemButton componentInChildren3 = Object.Instantiate<GameObject>(this.overworldItemButtonPrefab, gameObject5.transform).GetComponentInChildren<Overworld_InventoryItemButton>();
			componentInChildren3.draggable = false;
			componentInChildren3.Setup(missions);
		}
	}

	// Token: 0x060003B2 RID: 946 RVA: 0x00016518 File Offset: 0x00014718
	public void GetDesciptionsSimpleImage(Sprite sprite, string title, List<string> descriptors, GameObject p)
	{
		this.iconSprite.sprite = sprite;
		this.iconSprite.SetNativeSize();
		if (this.iconSprite.rectTransform.sizeDelta.x > this.iconSprite.rectTransform.sizeDelta.y)
		{
			this.iconSprite.rectTransform.sizeDelta = new Vector2(400f, 400f * this.iconSprite.rectTransform.sizeDelta.y / this.iconSprite.rectTransform.sizeDelta.x);
		}
		else
		{
			this.iconSprite.rectTransform.sizeDelta = new Vector2(400f * this.iconSprite.rectTransform.sizeDelta.x / this.iconSprite.rectTransform.sizeDelta.y, 400f);
		}
		if (title == "")
		{
			this.displayName.transform.parent.gameObject.SetActive(false);
		}
		else
		{
			this.displayName.text = title;
		}
		for (int i = 0; i < descriptors.Count; i++)
		{
			if (LangaugeManager.main.KeyExists(descriptors[i]))
			{
				descriptors[i] = LangaugeManager.main.GetTextByKey(descriptors[i]);
			}
		}
		this.GetDescriptionsSimple(descriptors, p);
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x0001667C File Offset: 0x0001487C
	public void GetDescriptionsSimple(List<string> descriptors, GameObject p)
	{
		this.parent = p;
		List<Card.DescriptorTotal> list = new List<Card.DescriptorTotal>();
		foreach (string text in descriptors)
		{
			list.Add(new Card.DescriptorTotal
			{
				trigger = null,
				texts = new List<string> { text },
				flavor = true
			});
		}
		this.SetPropertyTexts(list, TextAlignmentOptions.MidlineLeft);
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x0001670C File Offset: 0x0001490C
	public void GetDescriptionsLore(string loreName, GameObject p)
	{
		string textByKey = LangaugeManager.main.GetTextByKey(loreName);
		this.displayName.text = textByKey;
		this.parent = p;
		List<Card.DescriptorTotal> list = new List<Card.DescriptorTotal>();
		Lore component = base.GetComponent<Lore>();
		if (!component)
		{
			return;
		}
		Lore.LoreType loreType = component.GetLoreType(loreName);
		this.iconSprite.sprite = loreType.sprite;
		foreach (string text in loreType.descriptionKeys)
		{
			list.Add(new Card.DescriptorTotal
			{
				trigger = null,
				texts = new List<string> { LangaugeManager.main.GetTextByKey(text) },
				flavor = false
			});
		}
		this.SetPropertyTexts(list, TextAlignmentOptions.MidlineLeft);
		HighlightText[] componentsInChildren = base.GetComponentsInChildren<HighlightText>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enabled = false;
		}
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x00016818 File Offset: 0x00014A18
	public void SetParent(GameObject o)
	{
		this.parent = o;
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x00016821 File Offset: 0x00014A21
	public GameObject GetParent()
	{
		return this.parent;
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x0001682C File Offset: 0x00014A2C
	public void GetDescription(Character character)
	{
		this.iconSprite.sprite = character.mapCharacterSprite[0];
		this.iconSprite.SetNativeSize();
		string textByKey = LangaugeManager.main.GetTextByKey(character.characterNameKey);
		this.displayName.text = textByKey;
		List<Card.DescriptorTotal> list = new List<Card.DescriptorTotal>();
		string characterDescriptionKey = character.characterDescriptionKey;
		string text = LangaugeManager.main.GetTextByKey(characterDescriptionKey) ?? "";
		list.Add(new Card.DescriptorTotal
		{
			trigger = null,
			texts = new List<string> { text },
			flavor = false
		});
		this.SetPropertyTexts(list, TextAlignmentOptions.MidlineLeft);
		base.StartCoroutine(this.FindHeight());
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x000168E8 File Offset: 0x00014AE8
	public void GetDescription(SellingTile sellingTile)
	{
		this.parent = sellingTile.gameObject;
		SpriteRenderer componentInChildren = sellingTile.GetComponentInChildren<SpriteRenderer>();
		if (componentInChildren)
		{
			this.iconSprite.sprite = componentInChildren.sprite;
			this.iconSprite.SetNativeSize();
			this.iconSprite.rectTransform.sizeDelta = this.iconSprite.rectTransform.sizeDelta.normalized * 170f;
		}
		this.itemType.text = LangaugeManager.main.GetTextByKey("Ground");
		this.itemRarity.text = "";
		string textByKey = LangaugeManager.main.GetTextByKey(Item2.GetDisplayName(sellingTile.name));
		this.displayName.text = textByKey;
		List<Card.DescriptorTotal> list = new List<Card.DescriptorTotal>();
		this.SetPropertyTexts(list, TextAlignmentOptions.MidlineLeft);
		base.StartCoroutine(this.FindHeight());
	}

	// Token: 0x060003B9 RID: 953 RVA: 0x000169CC File Offset: 0x00014BCC
	public void GetDescription(Overworld_NPC npc)
	{
		this.parent = npc.gameObject;
		this.iconSprite.sprite = npc.defaultSprite;
		this.iconSprite.SetNativeSize();
		this.iconSprite.rectTransform.sizeDelta = this.iconSprite.rectTransform.sizeDelta.normalized * 170f;
		string text = Item2.GetDisplayName(npc.name);
		text = text.ToUpper().Replace("OVERWORLD", "").Trim();
		this.itemType.text = "";
		this.itemRarity.text = "";
		this.displayName.text = LangaugeManager.main.GetTextByKey(text);
		List<Card.DescriptorTotal> list = new List<Card.DescriptorTotal>();
		string text2 = text + "1";
		if (LangaugeManager.main.KeyExists(text2))
		{
			string text3 = LangaugeManager.main.GetTextByKey(text2) ?? "";
			list.Add(new Card.DescriptorTotal
			{
				trigger = null,
				texts = new List<string> { text3 },
				flavor = false
			});
		}
		this.SetPropertyTexts(list, TextAlignmentOptions.MidlineLeft);
		base.StartCoroutine(this.FindHeight());
	}

	// Token: 0x060003BA RID: 954 RVA: 0x00016B14 File Offset: 0x00014D14
	public void GetResourceCosts(List<Overworld_ResourceManager.Resource> resources)
	{
		Overworld_ResourceDisplayPanel componentInChildren = base.GetComponentInChildren<Overworld_ResourceDisplayPanel>();
		if (componentInChildren)
		{
			componentInChildren.SetupResources(resources);
		}
	}

	// Token: 0x060003BB RID: 955 RVA: 0x00016B38 File Offset: 0x00014D38
	public void GetDescription(Overworld_Structure building)
	{
		this.parent = building.gameObject;
		SpriteRenderer componentInChildren = building.GetComponentInChildren<SpriteRenderer>();
		if (componentInChildren)
		{
			this.iconSprite.sprite = componentInChildren.sprite;
			this.iconSprite.SetNativeSize();
			this.iconSprite.rectTransform.sizeDelta = this.iconSprite.rectTransform.sizeDelta.normalized * 170f;
		}
		string text = LangaugeManager.main.GetTextByKey(Item2.GetDisplayName(building.name));
		if (building.name.ToLower().Trim().Contains("rubble"))
		{
			text = LangaugeManager.main.GetTextByKey("rubble");
		}
		this.displayName.text = text;
		List<Card.DescriptorTotal> list = new List<Card.DescriptorTotal>();
		int num = 0;
		this.itemType.text = "";
		foreach (Overworld_Structure.StructureType structureType in building.structureTypes)
		{
			string text2 = structureType.ToString();
			if (LangaugeManager.main.KeyExists(text2))
			{
				if (num > 0)
				{
					TextMeshProUGUI textMeshProUGUI = this.itemType;
					textMeshProUGUI.text += "<br>";
				}
				TextMeshProUGUI textMeshProUGUI2 = this.itemType;
				textMeshProUGUI2.text += LangaugeManager.main.GetTextByKey(text2);
				num++;
			}
		}
		this.itemRarity.text = "";
		Overworld_Structure.AllStructuresApplyAllModifiers();
		building.GetEffectTotals();
		if (building.appliedModifiers.Count > 0)
		{
			string text3 = LangaugeManager.main.GetTextByKey("buet") + ": " + building.currentEfficiencyBonus.ToString() + "%";
			List<string> list2 = new List<string>();
			List<int> list3 = new List<int>();
			List<float> list4 = new List<float>();
			foreach (Overworld_Structure.Modifier modifier in building.appliedModifiers)
			{
				if (!list2.Contains(modifier.originName))
				{
					list2.Add(modifier.originName);
					list3.Add(1);
					list4.Add(modifier.efficiencyBonus);
				}
				else
				{
					int num2 = list2.IndexOf(modifier.originName);
					List<int> list5 = list3;
					int num3 = num2;
					int num4 = list5[num3];
					list5[num3] = num4 + 1;
					List<float> list6 = list4;
					num4 = num2;
					list6[num4] += modifier.efficiencyBonus;
				}
			}
			int num5 = 0;
			while (num5 < list2.Count && num5 < list2.Count && num5 < list3.Count && num5 < list4.Count)
			{
				string text4 = "";
				if (list4[num5] > 0f)
				{
					text4 = "+";
				}
				if (list3[num5] == 1)
				{
					text3 = string.Concat(new string[]
					{
						text3,
						"<size=90%><br>  ",
						LangaugeManager.main.GetTextByKey(list2[num5]),
						": ",
						text4,
						list4[num5].ToString(),
						"%</size>"
					});
				}
				else
				{
					text3 = string.Concat(new string[]
					{
						text3,
						"<size=90%><br>  ",
						list3[num5].ToString(),
						"x ",
						LangaugeManager.main.GetTextByKey(list2[num5]),
						": ",
						text4,
						list4[num5].ToString(),
						"%</size>"
					});
				}
				num5++;
			}
			list.Add(new Card.DescriptorTotal
			{
				trigger = null,
				texts = new List<string> { text3 },
				flavor = false
			});
		}
		string text5 = text + "1";
		if (LangaugeManager.main.KeyExists(text5))
		{
			string text6 = LangaugeManager.main.GetTextByKey(text5) ?? "";
			list.Add(new Card.DescriptorTotal
			{
				trigger = null,
				texts = new List<string> { text6 },
				flavor = false
			});
		}
		if (building.placementConditions.Contains(Overworld_Structure.PlacementCondition.MustBeNearEdge))
		{
			string text7 = LangaugeManager.main.GetTextByKey("gm36d") ?? "";
			list.Add(new Card.DescriptorTotal
			{
				trigger = null,
				texts = new List<string> { text7 },
				flavor = false
			});
		}
		foreach (Overworld_ResourceManager.Resource resource in building.resourcesToAddEachRun)
		{
			if (resource.type != Overworld_ResourceManager.Resource.Type.Population)
			{
				string text8 = "";
				Card.DescriptorTotal descriptorTotal = new Card.DescriptorTotal();
				switch (resource.type)
				{
				case Overworld_ResourceManager.Resource.Type.Food:
					text8 = LangaugeManager.main.GetTextByKey("bfood").Replace("/x", resource.amount.ToString() ?? "") ?? "";
					break;
				case Overworld_ResourceManager.Resource.Type.BuildingMaterial:
					text8 = LangaugeManager.main.GetTextByKey("bbuildingMaterials").Replace("/x", resource.amount.ToString() ?? "") ?? "";
					break;
				case Overworld_ResourceManager.Resource.Type.Treasure:
					text8 = LangaugeManager.main.GetTextByKey("btreasure").Replace("/x", resource.amount.ToString() ?? "") ?? "";
					break;
				}
				descriptorTotal.trigger = null;
				descriptorTotal.texts = new List<string> { text8 };
				descriptorTotal.flavor = false;
				list.Add(descriptorTotal);
			}
		}
		foreach (Overworld_Structure.Modifier modifier2 in building.modifiers)
		{
			Card.DescriptorTotal descriptorTotal2 = new Card.DescriptorTotal();
			descriptorTotal2.trigger = null;
			descriptorTotal2.triggerOverrideKey = this.GetStructureModifierDescription(modifier2);
			string text9 = LangaugeManager.main.GetTextByKey("bumoef1");
			if (modifier2.efficiencyBonus > 0f)
			{
				text9 = text9.Replace("/x", "+" + modifier2.efficiencyBonus.ToString() + "%");
			}
			else
			{
				text9 = text9.Replace("/x", modifier2.efficiencyBonus.ToString() + "%");
			}
			descriptorTotal2.texts = new List<string> { text9 };
			descriptorTotal2.stackable = false;
			list.Add(descriptorTotal2);
		}
		if (building.spacesForExpansion > 0)
		{
			Card.DescriptorTotal descriptorTotal3 = new Card.DescriptorTotal();
			descriptorTotal3.trigger = null;
			string text10 = "";
			if (building.tagsForExpansion.Contains(GridObject.Tag.farm))
			{
				text10 = LangaugeManager.main.GetTextByKey("bumTile1");
			}
			else if (building.tagsForExpansion.Contains(GridObject.Tag.path))
			{
				text10 = LangaugeManager.main.GetTextByKey("bumTile2");
			}
			else if (building.tagsForExpansion.Contains(GridObject.Tag.brick))
			{
				text10 = LangaugeManager.main.GetTextByKey("bumTile3");
			}
			else if (building.tagsForExpansion.Contains(GridObject.Tag.stone))
			{
				text10 = LangaugeManager.main.GetTextByKey("bumTile4");
			}
			else if (building.tagsForExpansion.Contains(GridObject.Tag.any))
			{
				text10 = LangaugeManager.main.GetTextByKey("bumTileA");
			}
			text10 = text10.Replace("/x", building.spacesForExpansion.ToString() ?? "");
			descriptorTotal3.texts = new List<string> { text10 };
			descriptorTotal3.stackable = false;
			list.Add(descriptorTotal3);
		}
		text5 = text + "2";
		if (LangaugeManager.main.KeyExists(text5))
		{
			string text11 = "/" + LangaugeManager.main.GetTextByKey(text5);
			list.Add(new Card.DescriptorTotal
			{
				trigger = null,
				texts = new List<string> { text11 },
				flavor = true
			});
		}
		this.SetPropertyTexts(list, TextAlignmentOptions.MidlineLeft);
		base.StartCoroutine(this.FindHeight());
	}

	// Token: 0x060003BC RID: 956 RVA: 0x0001742C File Offset: 0x0001562C
	private string GetStructureModifierDescription(Overworld_Structure.Modifier modifier)
	{
		string text = "";
		if (modifier.applyToSelf && Card.CheckStructureTypes(modifier.structureTypes, Overworld_Structure.StructureType.Decoration, Overworld_Structure.StructureType.Undefined, Overworld_Structure.StructureType.Undefined))
		{
			text = "bumo1";
		}
		else if (modifier.applyToSelf && Card.CheckStructureTypes(modifier.structureTypes, Overworld_Structure.StructureType.Commercial, Overworld_Structure.StructureType.Undefined, Overworld_Structure.StructureType.Undefined))
		{
			text = "bumo2";
		}
		else if (modifier.applyToSelf && Card.CheckStructureTypes(modifier.structureTypes, Overworld_Structure.StructureType.BridgeAndWater, Overworld_Structure.StructureType.Undefined, Overworld_Structure.StructureType.Undefined))
		{
			text = "bumo3";
		}
		else if (modifier.applyToSelf && Card.CheckStructureTypes(modifier.structureTypes, Overworld_Structure.StructureType.Military, Overworld_Structure.StructureType.Undefined, Overworld_Structure.StructureType.Undefined))
		{
			text = "bumo4";
		}
		else if (modifier.applyToSelf && Card.CheckStructureTypes(modifier.structureTypes, Overworld_Structure.StructureType.Workshop, Overworld_Structure.StructureType.Undefined, Overworld_Structure.StructureType.Undefined))
		{
			text = "bumo5";
		}
		else if (modifier.applyToSelf && Card.CheckStructureTypes(modifier.structureTypes, Overworld_Structure.StructureType.Natural, Overworld_Structure.StructureType.Undefined, Overworld_Structure.StructureType.Undefined))
		{
			text = "bumo6";
		}
		else if (modifier.applyToSelf && Card.CheckStructureTypes(modifier.structureTypes, Overworld_Structure.StructureType.Agrarian, Overworld_Structure.StructureType.Undefined, Overworld_Structure.StructureType.Undefined))
		{
			text = "bumo6b";
		}
		else if (modifier.applyToSelf && Card.CheckStructureTypes(modifier.structureTypes, Overworld_Structure.StructureType.Religious, Overworld_Structure.StructureType.Undefined, Overworld_Structure.StructureType.Undefined))
		{
			text = "bumo7";
		}
		else if (modifier.applyToSelf && Card.CheckStructureTypes(modifier.structureTypes, Overworld_Structure.StructureType.Decoration, Overworld_Structure.StructureType.Undefined, Overworld_Structure.StructureType.Undefined))
		{
			text = "bumo8";
		}
		else if (modifier.connectionType == Overworld_Structure.Modifier.ConnectionType.connectedViaPath && Card.CheckStructureTypes(modifier.structureTypes, Overworld_Structure.StructureType.Researcher, Overworld_Structure.StructureType.Undefined, Overworld_Structure.StructureType.Undefined) && !modifier.applyToSelf)
		{
			text = "bumo9";
		}
		else if (modifier.connectionType == Overworld_Structure.Modifier.ConnectionType.Nearby && modifier.tagTypes.Contains(GridObject.Tag.farm) && modifier.applyToSelf)
		{
			text = "bumo10";
		}
		else if (modifier.connectionType == Overworld_Structure.Modifier.ConnectionType.Nearby && modifier.tagTypes.Contains(GridObject.Tag.stone) && modifier.applyToSelf)
		{
			text = "bumo11";
		}
		else if (modifier.connectionType == Overworld_Structure.Modifier.ConnectionType.Nearby && modifier.tagTypes.Contains(GridObject.Tag.water) && modifier.applyToSelf)
		{
			text = "bumo3";
		}
		else if (modifier.connectionType == Overworld_Structure.Modifier.ConnectionType.connectedViaPath && Card.CheckStructureTypes(modifier.structureTypes, Overworld_Structure.StructureType.Residential, Overworld_Structure.StructureType.Undefined, Overworld_Structure.StructureType.Undefined) && modifier.applyToSelf)
		{
			text = "bumo12";
		}
		return text;
	}

	// Token: 0x060003BD RID: 957 RVA: 0x00017644 File Offset: 0x00015844
	public void GetDescriptions(EnemyActionPreview enemyActionPreview, GameObject p)
	{
		this.parent = p;
		string text = EnemyActionPreview.GetAttackDescription(enemyActionPreview.myAttackReference, enemyActionPreview.enemy.stats);
		text = text.Replace("/x", enemyActionPreview.myAttackReference.currentPower.ToString() ?? "");
		List<string> list = new List<string>();
		list.Add(text);
		List<Card.DescriptorTotal> list2 = new List<Card.DescriptorTotal>();
		foreach (string text2 in list)
		{
			list2.Add(new Card.DescriptorTotal
			{
				trigger = null,
				texts = new List<string> { text2 },
				flavor = true
			});
		}
		this.SetPropertyTexts(list2, TextAlignmentOptions.MidlineLeft);
	}

	// Token: 0x060003BE RID: 958 RVA: 0x00017720 File Offset: 0x00015920
	public void GetDescriptions(Enemy enemy, GameObject p)
	{
		this.parent = p;
		string text = LangaugeManager.main.GetTextByKey(enemy.displayName);
		if (enemy.alternateName != "" && enemy.isPerformingAlternateAttacks)
		{
			text = LangaugeManager.main.GetTextByKey(enemy.alternateName);
		}
		this.displayName.text = text;
		if (this.displayName.text.Length <= 12)
		{
			this.displayName.fontSize = 48f;
		}
		else
		{
			this.displayName.characterSpacing = -0.3f * (float)this.displayName.text.Length;
			this.displayName.fontSize = (float)(620 / this.displayName.text.Length);
		}
		List<Card.DescriptorTotal> list = new List<Card.DescriptorTotal>();
		foreach (Item2.CombattEffect combattEffect in enemy.combattEffects)
		{
			string text2 = "";
			Item2.EffectTotal effectTotalFromCombatEffect = Item2.GetEffectTotalFromCombatEffect(combattEffect);
			text2 = this.GetEffectTotalDescription(effectTotalFromCombatEffect);
			string text3 = "\\bto\\s+enemy\\b";
			text2 = Regex.Replace(text2, text3, "");
			bool flag = false;
			foreach (Card.DescriptorTotal descriptorTotal in list)
			{
				if (descriptorTotal.trigger != null && Item2.CheckIfSameTrigger(descriptorTotal.trigger, combattEffect.trigger))
				{
					flag = true;
					descriptorTotal.texts.Add(text2.Trim());
					break;
				}
			}
			if (!flag)
			{
				list.Add(new Card.DescriptorTotal
				{
					trigger = combattEffect.trigger,
					texts = new List<string> { text2.Trim() }
				});
			}
		}
		list.Add(new Card.DescriptorTotal
		{
			trigger = null,
			texts = new List<string> { LangaugeManager.main.GetTextByKey("enemyXP").Replace("/x", enemy.xp.ToString() ?? "") }
		});
		foreach (StatusEffect statusEffect in enemy.GetAllPowers())
		{
			if (statusEffect.type != StatusEffect.Type.reactive && statusEffect.type != StatusEffect.Type.Wary)
			{
				string statusEffectDescription = statusEffect.GetStatusEffectDescription();
				list.Add(new Card.DescriptorTotal
				{
					trigger = null,
					texts = new List<string> { statusEffectDescription },
					flavor = false
				});
			}
		}
		list.Add(new Card.DescriptorTotal
		{
			trigger = null,
			texts = new List<string> { LangaugeManager.main.GetTextByKey("enemyHP").Replace("/x", enemy.stats.health.ToString() ?? "").Replace("/y", enemy.stats.maxHealth.ToString() ?? "") }
		});
		if (enemy.isSummon)
		{
			list.Add(new Card.DescriptorTotal
			{
				trigger = null,
				texts = new List<string> { LangaugeManager.main.GetTextByKey("enemyS") },
				flavor = false
			});
		}
		string text4 = Item2.GetDisplayName(enemy.name);
		if (LangaugeManager.main.KeyExists(text4 + "1"))
		{
			list.Add(new Card.DescriptorTotal
			{
				trigger = null,
				texts = new List<string> { "/" + LangaugeManager.main.GetTextByKey(text4 + "1") },
				flavor = true
			});
		}
		this.SetPropertyTexts(list, TextAlignmentOptions.MidlineLeft);
	}

	// Token: 0x060003BF RID: 959 RVA: 0x00017B34 File Offset: 0x00015D34
	public void GetDescriptions(PetItem petItem, GameObject p)
	{
		this.parent = p;
		PetMaster petMaster = petItem.petMaster;
		if (!petMaster)
		{
			return;
		}
		SpriteRenderer componentInChildren = petMaster.combatPetPrefab.GetComponentInChildren<SpriteRenderer>();
		this.iconSprite.sprite = componentInChildren.sprite;
		this.iconSprite.SetNativeSize();
		if (!componentInChildren.flipX)
		{
			this.iconSprite.transform.localScale = new Vector3(-1f, 1f, 1f);
		}
		List<Card.DescriptorTotal> list = new List<Card.DescriptorTotal>();
		list.Add(new Card.DescriptorTotal
		{
			trigger = null,
			texts = new List<string> { LangaugeManager.main.GetTextByKey("enemyHP").Replace("/x", petMaster.health.ToString() ?? "").Replace("/y", petMaster.maxHealth.ToString() ?? "") }
		});
		if (petMaster.APonSummon != 0)
		{
			list.Add(new Card.DescriptorTotal
			{
				trigger = null,
				texts = new List<string> { LangaugeManager.main.GetTextByKey("petEnergySummon").Replace("/x", petMaster.APonSummon.ToString() ?? "") }
			});
		}
		if (petMaster.APperTurn != 0)
		{
			list.Add(new Card.DescriptorTotal
			{
				trigger = null,
				texts = new List<string> { LangaugeManager.main.GetTextByKey("petEnergy").Replace("/x", petMaster.APperTurn.ToString() ?? "") }
			});
		}
		foreach (Item2.CombattEffect combattEffect in petMaster.petEffects)
		{
			string text = "";
			Item2.EffectTotal effectTotalFromCombatEffect = Item2.GetEffectTotalFromCombatEffect(combattEffect);
			text = this.GetEffectTotalDescription(effectTotalFromCombatEffect);
			bool flag = false;
			foreach (Card.DescriptorTotal descriptorTotal in list)
			{
				if (descriptorTotal.trigger != null && Item2.CheckIfSameTrigger(descriptorTotal.trigger, combattEffect.trigger))
				{
					flag = true;
					descriptorTotal.texts.Add(text.Trim());
					break;
				}
			}
			if (!flag)
			{
				list.Add(new Card.DescriptorTotal
				{
					trigger = combattEffect.trigger,
					texts = new List<string> { text.Trim() }
				});
			}
		}
		this.SetPropertyTexts(list, TextAlignmentOptions.MidlineLeft);
	}

	// Token: 0x060003C0 RID: 960 RVA: 0x00017DF0 File Offset: 0x00015FF0
	private void ShowCosts(List<Item2.Cost> costs, GameObject apCostText, GameObject manaText, GameObject goldCostText)
	{
		if (Item2.GetCurrentCost(Item2.Cost.Type.energy, costs) == -999)
		{
			apCostText.SetActive(false);
		}
		else
		{
			apCostText.SetActive(true);
			apCostText.GetComponentInChildren<TextMeshProUGUI>().text = Mathf.Max(0, Item2.GetCurrentCost(Item2.Cost.Type.energy, costs)).ToString() ?? "";
		}
		if (Item2.GetCurrentCost(Item2.Cost.Type.mana, costs) == -999)
		{
			manaText.SetActive(false);
		}
		else
		{
			manaText.SetActive(true);
			manaText.GetComponentInChildren<TextMeshProUGUI>().text = Mathf.Max(0, Item2.GetCurrentCost(Item2.Cost.Type.mana, costs)).ToString() ?? "";
		}
		if (Item2.GetCurrentCost(Item2.Cost.Type.gold, costs) == -999)
		{
			goldCostText.SetActive(false);
			return;
		}
		goldCostText.SetActive(true);
		goldCostText.GetComponentInChildren<TextMeshProUGUI>().text = Mathf.Max(0, Item2.GetCurrentCost(Item2.Cost.Type.gold, costs)).ToString() ?? "";
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x00017ED4 File Offset: 0x000160D4
	public void GetDescriptions(PetItem2 petItem, GameObject p)
	{
		if (!petItem)
		{
			return;
		}
		this.parent = p;
		SpriteRenderer componentInChildren = petItem.combatPetPrefab.GetComponentInChildren<SpriteRenderer>();
		this.iconSprite.sprite = componentInChildren.sprite;
		this.iconSprite.SetNativeSize();
		if (!componentInChildren.flipX)
		{
			this.iconSprite.transform.localScale = new Vector3(-1f, 1f, 1f);
		}
		List<Card.DescriptorTotal> list = new List<Card.DescriptorTotal>();
		list.Add(new Card.DescriptorTotal
		{
			trigger = null,
			texts = new List<string> { LangaugeManager.main.GetTextByKey("enemyHP").Replace("/x", petItem.health.ToString() ?? "").Replace("/y", petItem.maxHealth.ToString() ?? "") }
		});
		CombatPet combatPet = null;
		if (petItem.combatPet)
		{
			combatPet = petItem.combatPet.GetComponent<CombatPet>();
		}
		if (!combatPet)
		{
			combatPet = petItem.combatPetPrefab.GetComponent<CombatPet>();
		}
		this.moveableSymbol.SetActive(false);
		this.SetPropertyTexts(list, TextAlignmentOptions.MidlineLeft);
	}

	// Token: 0x060003C2 RID: 962 RVA: 0x00018004 File Offset: 0x00016204
	private void SetPropertyTexts(List<Card.DescriptorTotal> descriptorTotals, TextAlignmentOptions textAlignment = TextAlignmentOptions.MidlineLeft)
	{
		foreach (object obj in this.cardPropertiesParent)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
		List<Card.DescriptorTotal> list = new List<Card.DescriptorTotal>();
		foreach (Card.DescriptorTotal descriptorTotal in descriptorTotals)
		{
			if (descriptorTotal.trigger != null && (descriptorTotal.trigger.trigger == Item2.Trigger.ActionTrigger.constant || descriptorTotal.trigger.trigger == Item2.Trigger.ActionTrigger.constantEarly))
			{
				list.Insert(0, descriptorTotal);
			}
			else
			{
				list.Add(descriptorTotal);
			}
		}
		foreach (Card.DescriptorTotal descriptorTotal2 in list)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.cardPropertyPrefab, base.transform.position, Quaternion.identity, this.cardPropertiesParent);
			CardEffectProperty component = gameObject.GetComponent<CardEffectProperty>();
			gameObject.GetComponent<HighlightText>().disableEmoji = descriptorTotal2.disableEmoji;
			string text = "";
			if (descriptorTotal2.triggerOverrideKey.Length > 1)
			{
				text = LangaugeManager.main.GetTextByKey(descriptorTotal2.triggerOverrideKey) + ",";
				if (descriptorTotal2.triggerOverrideValue != -999f)
				{
					text = text.Replace("/x", descriptorTotal2.triggerOverrideValue.ToString() ?? "");
				}
			}
			else if (descriptorTotal2.trigger != null && descriptorTotal2.trigger.trigger == Item2.Trigger.ActionTrigger.whenScripted)
			{
				ScriptedTrigger componentInChildren = this.parent.GetComponentInChildren<ScriptedTrigger>();
				if (componentInChildren)
				{
					if (componentInChildren.triggerKey.Length > 1 && componentInChildren.secondTriggerKey.Length > 1)
					{
						text = LangaugeManager.main.GetTextByKey(componentInChildren.triggerKey) + ", " + LangaugeManager.main.GetTextByKey(componentInChildren.secondTriggerKey) + ",";
					}
					else if (componentInChildren.triggerKey.Length > 1)
					{
						text = LangaugeManager.main.GetTextByKey(componentInChildren.triggerKey) + ",";
					}
					text = text.Replace("/x", componentInChildren.triggerValue.ToString() ?? "");
					if (componentInChildren.showProgress)
					{
						int value = componentInChildren.GetValue();
						if (value > 0)
						{
							text = string.Concat(new string[]
							{
								"(",
								value.ToString(),
								"/",
								componentInChildren.triggerValue.ToString(),
								") ",
								text
							});
						}
					}
				}
			}
			else if (descriptorTotal2.trigger != null)
			{
				text = this.GetTriggerDescription(descriptorTotal2.trigger, descriptorTotal2.stackable);
			}
			component.triggerText.text = text;
			if (descriptorTotal2.effectOverrideKey.Length > 1)
			{
				descriptorTotal2.texts = new List<string> { descriptorTotal2.effectOverrideKey };
			}
			if (this.parent.GetComponent<PetItem2>())
			{
				for (int i = 0; i < descriptorTotal2.texts.Count; i++)
				{
					if (!descriptorTotal2.texts[i].ToLower().Contains(LangaugeManager.main.GetTextByKey("damage").ToLower()) && !descriptorTotal2.texts[i].ToLower().Contains("<size"))
					{
						for (int j = i + 1; j < descriptorTotal2.texts.Count; j++)
						{
							if (descriptorTotal2.texts[i] == descriptorTotal2.texts[j])
							{
								descriptorTotal2.texts.RemoveAt(j);
								j--;
							}
						}
					}
				}
			}
			foreach (string text2 in descriptorTotal2.texts)
			{
				GameObject gameObject2 = Object.Instantiate<GameObject>(component.cardPropertyPrefab, Vector3.zero, Quaternion.identity, component.transform);
				gameObject2.GetComponent<HighlightText>().disableEmoji = descriptorTotal2.disableEmoji;
				TextMeshProUGUI componentInChildren2 = gameObject2.GetComponentInChildren<TextMeshProUGUI>();
				string text3 = text2;
				if (text3.Length > 0 && text3.Substring(0, 1) == "/")
				{
					text3 = text2.Substring(1);
					text3 = "<size=80%>" + text3 + "</size>";
					componentInChildren2.fontStyle = FontStyles.Italic;
					componentInChildren2.characterSpacing = -2f;
					componentInChildren2.lineSpacing = -20f;
				}
				componentInChildren2.text = text3;
				componentInChildren2.alignment = textAlignment;
			}
			if (descriptorTotal2.flavor)
			{
				component.transform.SetAsLastSibling();
				if (this.cardSecondaryPropertiesParent)
				{
					component.transform.SetParent(this.cardSecondaryPropertiesParent);
				}
				Image[] array = component.GetComponentsInChildren<Image>();
				for (int k = 0; k < array.Length; k++)
				{
					array[k].enabled = false;
				}
				foreach (VerticalLayoutGroup verticalLayoutGroup in component.GetComponentsInChildren<VerticalLayoutGroup>())
				{
					verticalLayoutGroup.padding.left = 0;
					verticalLayoutGroup.padding.bottom = 6;
				}
			}
			else if (text.Length == 0)
			{
				component.transform.SetAsFirstSibling();
				Image[] array = component.GetComponentsInChildren<Image>();
				for (int k = 0; k < array.Length; k++)
				{
					array[k].sprite = this.cardTextBorderConstantSprite;
				}
			}
		}
		base.StartCoroutine(this.FindHeight());
	}

	// Token: 0x060003C3 RID: 963 RVA: 0x00018620 File Offset: 0x00016820
	private bool CheckAreas(List<Item2.Area> areas, Item2.Area area, Item2.Area area2 = Item2.Area.undefined, Item2.Area area3 = Item2.Area.undefined)
	{
		return (areas.Count <= 1 || area2 != Item2.Area.undefined) && (areas.Count <= 2 || area3 != Item2.Area.undefined) && (areas.Contains(area) && (area2 == Item2.Area.undefined || areas.Contains(area2)) && (area3 == Item2.Area.undefined || areas.Contains(area3)));
	}

	// Token: 0x060003C4 RID: 964 RVA: 0x00018678 File Offset: 0x00016878
	public static bool CheckTypes(List<Item2.ItemType> types, Item2.ItemType type, Item2.ItemType type2 = Item2.ItemType.undefined, Item2.ItemType type3 = Item2.ItemType.undefined)
	{
		return (types.Count <= 1 || type2 != Item2.ItemType.undefined) && (types.Count <= 2 || type3 != Item2.ItemType.undefined) && (types.Contains(type) && (type2 == Item2.ItemType.undefined || types.Contains(type2)) && (type3 == Item2.ItemType.undefined || types.Contains(type3)));
	}

	// Token: 0x060003C5 RID: 965 RVA: 0x000186CC File Offset: 0x000168CC
	public static bool CheckStructureTypes(List<Overworld_Structure.StructureType> types, Overworld_Structure.StructureType type1, Overworld_Structure.StructureType type2 = Overworld_Structure.StructureType.Undefined, Overworld_Structure.StructureType type3 = Overworld_Structure.StructureType.Undefined)
	{
		return (types.Count <= 1 || type2 != Overworld_Structure.StructureType.Undefined) && (types.Count <= 2 || type3 != Overworld_Structure.StructureType.Undefined) && (types.Contains(type1) && (type2 == Overworld_Structure.StructureType.Undefined || types.Contains(type2)) && (type3 == Overworld_Structure.StructureType.Undefined || types.Contains(type3)));
	}

	// Token: 0x060003C6 RID: 966 RVA: 0x0001871C File Offset: 0x0001691C
	public string GetTriggerDescription(Item2.Trigger trigger, bool plural = false)
	{
		string text = "";
		if (trigger.areas.Contains(Item2.Area.self) && trigger.areas.Count <= 1)
		{
			if (trigger.trigger == Item2.Trigger.ActionTrigger.onUse || trigger.trigger == Item2.Trigger.ActionTrigger.useEarly || trigger.trigger == Item2.Trigger.ActionTrigger.useLate)
			{
				text += LangaugeManager.main.GetTextByKey("t1");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onUseUntilOverheat || trigger.trigger == Item2.Trigger.ActionTrigger.onUseUntilOverheatLate)
			{
				text += LangaugeManager.main.GetTextByKey("toverheat");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onOverheat)
			{
				text += LangaugeManager.main.GetTextByKey("toverheat1");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onHeatReset)
			{
				text += LangaugeManager.main.GetTextByKey("tHeatReset");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onAlternateUse || trigger.trigger == Item2.Trigger.ActionTrigger.onComboUse)
			{
				text += LangaugeManager.main.GetTextByKey("t1alt");
				if (this.alternateUseSymbol)
				{
					this.alternateUseSymbol.SetActive(true);
				}
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onFirstUse)
			{
				Item2 component = this.parent.GetComponent<Item2>();
				if (component && GameFlowManager.main && GameFlowManager.main.GetCombatStatSpecificItem(component, GameFlowManager.CombatStat.Length.turn) >= 1)
				{
					text = text + "<color=#ff5555>" + LangaugeManager.main.GetTextByKey("t1firstOff") + "</color>";
				}
				else
				{
					text += LangaugeManager.main.GetTextByKey("t1first");
				}
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onDestroy)
			{
				text += LangaugeManager.main.GetTextByKey("t2");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onAdd)
			{
				text += LangaugeManager.main.GetTextByKey("t3");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onRemove)
			{
				text += LangaugeManager.main.GetTextByKey("t4");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onTurnStart)
			{
				text += LangaugeManager.main.GetTextByKey("t5");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onTurnEnd)
			{
				text += LangaugeManager.main.GetTextByKey("t6");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onCombatStart)
			{
				text += LangaugeManager.main.GetTextByKey("t7");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onCombatEnd)
			{
				text += LangaugeManager.main.GetTextByKey("t8");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onKillEnemy)
			{
				text += LangaugeManager.main.GetTextByKey("t9");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.whenAnEnemyIsDefeated)
			{
				text += LangaugeManager.main.GetTextByKey("t9board");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onKillNonSummonEnemy)
			{
				text += LangaugeManager.main.GetTextByKey("t10");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onRotate)
			{
				text += LangaugeManager.main.GetTextByKey("t11");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onOutOfUses)
			{
				text += LangaugeManager.main.GetTextByKey("t12");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onComboSelect)
			{
				text += LangaugeManager.main.GetTextByKey("t14");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onTakeDamage)
			{
				text += LangaugeManager.main.GetTextByKey("t15");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.whenAttacked)
			{
				text += LangaugeManager.main.GetTextByKey("t15b");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onMovePast)
			{
				text += LangaugeManager.main.GetTextByKey("t16");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onMoveCollide)
			{
				text += LangaugeManager.main.GetTextByKey("t16b");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onMoveFinish)
			{
				text += LangaugeManager.main.GetTextByKey("t17");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onMoveCombat)
			{
				text += LangaugeManager.main.GetTextByKey("t18");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onScratch)
			{
				text += LangaugeManager.main.GetTextByKey("t19");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onSummonCarving || trigger.trigger == Item2.Trigger.ActionTrigger.onSummonCarvingLate || trigger.trigger == Item2.Trigger.ActionTrigger.onSummonCarvingEarly || trigger.trigger == Item2.Trigger.ActionTrigger.onSummonPet)
			{
				text += LangaugeManager.main.GetTextByKey("t1c");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onSummonAnyPet)
			{
				text += LangaugeManager.main.GetTextByKey("t1cAnyPet");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onDiscard)
			{
				text += LangaugeManager.main.GetTextByKey("t1d");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onDie)
			{
				text += LangaugeManager.main.GetTextByKey("t89");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onClearCarvings)
			{
				text += LangaugeManager.main.GetTextByKey("t125");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onPetDies)
			{
				text += LangaugeManager.main.GetTextByKey("t130");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onEnemyAsleep)
			{
				text += LangaugeManager.main.GetTextByKey("t131");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.whenZombied)
			{
				text += LangaugeManager.main.GetTextByKey("t132");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.whenManaFlowsThrough)
			{
				text += LangaugeManager.main.GetTextByKey("t133");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.whenLeftBehind)
			{
				text += LangaugeManager.main.GetTextByKey("t135");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.whenNotPlayed)
			{
				text += LangaugeManager.main.GetTextByKey("t139");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onReorganize)
			{
				text += LangaugeManager.main.GetTextByKey("t149");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.whenThisDestroys)
			{
				text += LangaugeManager.main.GetTextByKey("t140");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && trigger.types.Contains(Item2.ItemType.Sunshine))
			{
				text += LangaugeManager.main.GetTextByKey("t159");
			}
		}
		else
		{
			List<Item2.Area> list = new List<Item2.Area>();
			List<Item2.ItemType> types = trigger.types;
			bool flag = false;
			foreach (Item2.Area area in trigger.areas)
			{
				Item2.Area area3;
				Item2.Area area2 = (area3 = area);
				if (this.parent && this.parent.transform)
				{
					area3 = Item2.TranslateArea(area3, this.parent.transform);
				}
				if (area2 != area3)
				{
					flag = true;
				}
				list.Add(area3);
			}
			if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.right, Item2.Area.undefined, Item2.Area.undefined) && flag && Card.CheckTypes(types, Item2.ItemType.GridEmpty, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t20");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.bottom, Item2.Area.undefined, Item2.Area.undefined) && flag && Card.CheckTypes(types, Item2.ItemType.GridEmpty, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t21");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.left, Item2.Area.undefined, Item2.Area.undefined) && flag && Card.CheckTypes(types, Item2.ItemType.GridEmpty, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t22");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.top, Item2.Area.undefined, Item2.Area.undefined) && flag && Card.CheckTypes(types, Item2.ItemType.GridEmpty, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t23");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.right, Item2.Area.undefined, Item2.Area.undefined) && flag && Card.CheckTypes(types, Item2.ItemType.Structure, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t20s");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.bottom, Item2.Area.undefined, Item2.Area.undefined) && flag && Card.CheckTypes(types, Item2.ItemType.Structure, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t21s");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.left, Item2.Area.undefined, Item2.Area.undefined) && flag && Card.CheckTypes(types, Item2.ItemType.Structure, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t22s");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.top, Item2.Area.undefined, Item2.Area.undefined) && flag && Card.CheckTypes(types, Item2.ItemType.Structure, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t23s");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Structure, Item2.ItemType.undefined, Item2.ItemType.undefined) && plural)
			{
				text += LangaugeManager.main.GetTextByKey("t24");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.diagonal, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Structure, Item2.ItemType.undefined, Item2.ItemType.undefined) && plural)
			{
				text += LangaugeManager.main.GetTextByKey("t25");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Armor, Item2.ItemType.Shield, Item2.ItemType.undefined) && plural)
			{
				text += LangaugeManager.main.GetTextByKey("t26b");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Armor, Item2.ItemType.Shield, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t26");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.bottom, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Grid, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t27");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onDestroy && this.CheckAreas(list, Item2.Area.column, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t28");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onDestroy && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t29");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onDestroy && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.diagonal, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t29b");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.top, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Grid, Item2.ItemType.undefined, Item2.ItemType.undefined) && plural)
			{
				text += LangaugeManager.main.GetTextByKey("t30");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.top, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Grid, Item2.ItemType.undefined, Item2.ItemType.undefined) && !plural)
			{
				text += LangaugeManager.main.GetTextByKey("t30b");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onUse && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t31");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.oneSpaceOver, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t32");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Curse, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t33b");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.diagonal, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Curse, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t33");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onKillEnemy && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Wand, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t34");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t35");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.connected, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.ManaStone, Item2.ItemType.undefined, Item2.ItemType.undefined) && plural)
			{
				text += LangaugeManager.main.GetTextByKey("t36");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.connected, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.ManaStone, Item2.ItemType.undefined, Item2.ItemType.undefined) && !plural)
			{
				text += LangaugeManager.main.GetTextByKey("t36b");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Curse, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t37");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.diagonal, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Fish, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t38");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onKillNonSummonEnemy && this.CheckAreas(list, Item2.Area.right, Item2.Area.undefined, Item2.Area.undefined) && flag && Card.CheckTypes(types, Item2.ItemType.Arrow, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t39");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onKillNonSummonEnemy && this.CheckAreas(list, Item2.Area.bottom, Item2.Area.undefined, Item2.Area.undefined) && flag && Card.CheckTypes(types, Item2.ItemType.Arrow, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t40");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onKillNonSummonEnemy && this.CheckAreas(list, Item2.Area.left, Item2.Area.undefined, Item2.Area.undefined) && flag && Card.CheckTypes(types, Item2.ItemType.Arrow, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t41");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onKillNonSummonEnemy && this.CheckAreas(list, Item2.Area.top, Item2.Area.undefined, Item2.Area.undefined) && flag && Card.CheckTypes(types, Item2.ItemType.Arrow, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t42");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onKillEnemy && this.CheckAreas(list, Item2.Area.right, Item2.Area.undefined, Item2.Area.undefined) && flag && Card.CheckTypes(types, Item2.ItemType.Arrow, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t43");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onKillEnemy && this.CheckAreas(list, Item2.Area.bottom, Item2.Area.undefined, Item2.Area.undefined) && flag && Card.CheckTypes(types, Item2.ItemType.Arrow, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t44");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onKillEnemy && this.CheckAreas(list, Item2.Area.left, Item2.Area.undefined, Item2.Area.undefined) && flag && Card.CheckTypes(types, Item2.ItemType.Arrow, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t45");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onKillEnemy && this.CheckAreas(list, Item2.Area.top, Item2.Area.undefined, Item2.Area.undefined) && flag && Card.CheckTypes(types, Item2.ItemType.Arrow, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t46");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.top, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Grid, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t47");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.diagonal, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Key, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t48");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.top, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Grid, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t49");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.top, Item2.Area.undefined, Item2.Area.undefined) && plural && Card.CheckTypes(types, Item2.ItemType.Grid, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t50");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.right, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Grid, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t51");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.left, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Grid, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t52");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && !plural)
			{
				text += LangaugeManager.main.GetTextByKey("t53s");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t53");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.GridEmpty, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t54");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t55");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onUse && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t56");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.diagonal, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Consumable, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t57");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onTurnStart && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Gem, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t58");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onKillEnemy && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t59");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onKillEnemy && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t59b");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onUse && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t60");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.diagonal, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t61");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Accessory, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t62");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Gem, Item2.ItemType.undefined, Item2.ItemType.undefined) && plural)
			{
				text += LangaugeManager.main.GetTextByKey("t63");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && !plural)
			{
				text += LangaugeManager.main.GetTextByKey("t64");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.diagonal, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && !plural)
			{
				text += LangaugeManager.main.GetTextByKey("t65");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.diagonal, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Armor, Item2.ItemType.Shield, Item2.ItemType.undefined) && !plural)
			{
				text += LangaugeManager.main.GetTextByKey("t66");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onUse && this.CheckAreas(list, Item2.Area.diagonal, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Shield, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t67");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.diagonal, Item2.Area.adjacent, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Ring, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t68");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.diagonal, Item2.Area.adjacent, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Curse, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t69");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Armor, Item2.ItemType.undefined, Item2.ItemType.undefined) && plural)
			{
				text += LangaugeManager.main.GetTextByKey("t70b");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Armor, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t70");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onDestroy && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.diagonal, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.ManaStone, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t71");
			}
			else if ((trigger.trigger == Item2.Trigger.ActionTrigger.onSummonCarving || trigger.trigger == Item2.Trigger.ActionTrigger.onSummonCarvingLate || trigger.trigger == Item2.Trigger.ActionTrigger.onSummonCarvingEarly) && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("tc2");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.row, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Consumable, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t72v");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.row, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.ManaStone, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t73");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Consumable, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t74");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Fish, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t75");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.right, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.GridEmpty, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t76");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.column, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.GridEmpty, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t77");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.top, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.ManaStone, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t78");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onUse && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Carving, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t80");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.diagonalLine, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && plural)
			{
				text += LangaugeManager.main.GetTextByKey("t81a");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.diagonalLine, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t81");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onUse && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Carving, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t82");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onSummonCarving && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Carving, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t82b");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onSummonCarving && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.diagonal, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Carving, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t82c");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onUse && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Shield, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t83");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onKillNonSummonEnemy && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t85");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Carving, Item2.ItemType.undefined, Item2.ItemType.undefined) && plural)
			{
				text += LangaugeManager.main.GetTextByKey("t86");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Carving, Item2.ItemType.undefined, Item2.ItemType.undefined) && !plural)
			{
				text += LangaugeManager.main.GetTextByKey("t86b");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onSummonCarving && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Carving, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t87");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onUse && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Consumable, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t88");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onUse && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t90");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onUse && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Shield, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t91");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.inAnotherPocket, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Instrument, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t92");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.oneSpaceOver, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Hammer, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t94");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.row, Item2.Area.column, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Shuriken, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t96");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.inThisPocket, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && plural)
			{
				text += LangaugeManager.main.GetTextByKey("t101");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.inThisPocket, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Armor, Item2.ItemType.undefined, Item2.ItemType.undefined) && plural)
			{
				text += LangaugeManager.main.GetTextByKey("t100");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.inAnotherPocket, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.ManaStone, Item2.ItemType.undefined, Item2.ItemType.undefined) && plural)
			{
				text += LangaugeManager.main.GetTextByKey("t98");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.inThisPocket, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && !plural)
			{
				text += LangaugeManager.main.GetTextByKey("t97");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.inThisPocket, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && !plural)
			{
				text += LangaugeManager.main.GetTextByKey("t99");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.inThisPocket, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && plural)
			{
				text += LangaugeManager.main.GetTextByKey("t99b");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.inAnotherPocket, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Shuriken, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t103");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.inAnotherPocket, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Armor, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t108");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onDestroy && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Scary, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t109");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Scary, Item2.ItemType.undefined, Item2.ItemType.undefined) && plural)
			{
				text += LangaugeManager.main.GetTextByKey("t110");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.bottom, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Scary, Item2.ItemType.undefined, Item2.ItemType.undefined) && plural)
			{
				text += LangaugeManager.main.GetTextByKey("t112");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onUse && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t113");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onTurnStart && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Footwear, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t114");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onTurnStart && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Helmet, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t115");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onDestroy && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Festive, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t117");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.bottom, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Festive, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t118");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Festive, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t119");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onKillNonSummonEnemy && this.CheckAreas(list, Item2.Area.board, Item2.Area.self, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Cleaver, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t120");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onKillNonSummonEnemy && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Cleaver, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t120");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.column, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Totem, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t122");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onUse && this.CheckAreas(list, Item2.Area.column, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t123");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.bottom, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.GridEmpty, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t124");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onSummonCarving && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Plant, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t126");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.connected, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Structure, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t134");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onUse && this.CheckAreas(list, Item2.Area.myPlaySpace, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t137");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onUse && this.CheckAreas(list, Item2.Area.row, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t138");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.bottom, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t141");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.bottom, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Hazard, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t142");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.diagonal, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Hazard, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t143");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.onDestroy && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t145");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.row, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Hazard, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t150");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.diagonal, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.GridEmpty, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t154");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.diagonal, Item2.Area.adjacent, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.ManaStone, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t155");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.column, Item2.Area.row, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Consumable, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t156");
			}
			else if (trigger.trigger == Item2.Trigger.ActionTrigger.constant && this.CheckAreas(list, Item2.Area.row, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(types, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined))
			{
				text += LangaugeManager.main.GetTextByKey("t157");
			}
			if (flag)
			{
				text = text.Replace("[rotation]", ModTextGen.GetModifierRotation(list).Trim());
				text = text.Replace("[", "<color=#87ffe3>");
				text = text.Replace("]", "</color>");
			}
			else
			{
				text = text.Replace("[", "");
				text = text.Replace("]", "");
			}
		}
		text = text.Trim();
		Item2 component2 = this.parent.GetComponent<Item2>();
		if (trigger.requiresActivation && component2 && !component2.CheckForStatusEffect(Item2.ItemStatusEffect.Type.isActivated))
		{
			text = text + " <color=#FF4A60>" + LangaugeManager.main.GetTextByKey("twa") + "</color>";
		}
		if (text.Length > 0)
		{
			text += ",";
		}
		return text;
	}

	// Token: 0x060003C7 RID: 967 RVA: 0x0001AF10 File Offset: 0x00019110
	public string GetAddModifierDescription(Item2.AddModifier addModifier)
	{
		string text = "";
		bool flag = false;
		List<Item2.Area> list = new List<Item2.Area>();
		foreach (Item2.Area area in addModifier.areasToModify)
		{
			Item2.Area area3;
			Item2.Area area2 = (area3 = area);
			if (this.parent && this.parent.transform)
			{
				area3 = Item2.TranslateArea(area3, this.parent.transform);
			}
			if (area2 != area3)
			{
				flag = true;
			}
			list.Add(area3);
		}
		text += "<color=#AAFFFF>";
		if (addModifier.descriptionKey.Length > 2)
		{
			text += LangaugeManager.main.GetTextByKey(addModifier.descriptionKey);
			if (flag)
			{
				text = text.Replace("[rotation]", ModTextGen.GetModifierRotation(list).Trim());
			}
		}
		else if (addModifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(addModifier.typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined))
		{
			text += LangaugeManager.main.GetTextByKey("am1");
		}
		else if (addModifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(addModifier.typesToModify, Item2.ItemType.Shield, Item2.ItemType.undefined, Item2.ItemType.undefined))
		{
			text += LangaugeManager.main.GetTextByKey("am2");
		}
		else if (addModifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(addModifier.typesToModify, Item2.ItemType.Wand, Item2.ItemType.undefined, Item2.ItemType.undefined))
		{
			text += LangaugeManager.main.GetTextByKey("am3");
		}
		else if (addModifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(addModifier.typesToModify, Item2.ItemType.Bow, Item2.ItemType.undefined, Item2.ItemType.undefined))
		{
			text += LangaugeManager.main.GetTextByKey("am4");
		}
		else if (addModifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(addModifier.typesToModify, Item2.ItemType.Arrow, Item2.ItemType.undefined, Item2.ItemType.undefined))
		{
			text += LangaugeManager.main.GetTextByKey("am5");
		}
		else if (addModifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.column, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(addModifier.typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined))
		{
			text += LangaugeManager.main.GetTextByKey("am7");
		}
		else if (addModifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(addModifier.typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined))
		{
			text += LangaugeManager.main.GetTextByKey("am8");
		}
		else if (addModifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(addModifier.typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined))
		{
			text += LangaugeManager.main.GetTextByKey("am11");
		}
		text += ":</color><br><size=25%><br></size>";
		Item2.Modifier modifier = addModifier.modifier;
		string text2 = this.GetTriggerDescription(modifier.trigger, modifier.stackable).Trim();
		if (text2.Length > 0)
		{
			text += text2;
			text += " ";
		}
		text += this.GetModifierDescription(addModifier.modifier, false).Trim();
		text = text.Trim();
		return text;
	}

	// Token: 0x060003C8 RID: 968 RVA: 0x0001B280 File Offset: 0x00019480
	public string GetModifierDescription(Item2.Modifier modifier, bool showOrigin = true)
	{
		string text = "";
		Item2.Effect effect = modifier.effects[0];
		bool flag = false;
		List<Item2.ItemType> typesToModify = modifier.typesToModify;
		List<Item2.Area> list = new List<Item2.Area>();
		foreach (Item2.Area area in modifier.areasToModify)
		{
			Item2.Area area3;
			Item2.Area area2 = (area3 = area);
			if (this.parent && this.parent.transform)
			{
				area3 = Item2.TranslateArea(area3, this.parent.transform);
			}
			if (area2 != area3)
			{
				flag = true;
			}
			list.Add(area3);
		}
		Item2.Modifier.Length length = modifier.length;
		if (length == Item2.Modifier.Length.twoTurns)
		{
			length = Item2.Modifier.Length.forTurn;
		}
		if (modifier.descriptionKey.Length > 0)
		{
			text += LangaugeManager.main.GetTextByKey(modifier.descriptionKey);
			if (modifier.descriptionDisplayValue != -999f)
			{
				text = text.Replace("/x", modifier.descriptionDisplayValue.ToString() ?? "");
			}
			else
			{
				ValueChanger[] array = this.parent.GetComponentsInChildren<ValueChanger>();
				int i = 0;
				while (i < array.Length)
				{
					ValueChanger valueChanger = array[i];
					if (valueChanger.storedEffect == modifier.effects[0])
					{
						if (text.Contains("%"))
						{
							text = text.Replace("/x", (valueChanger.multiplier * 100f).ToString() ?? "");
							break;
						}
						if (valueChanger.multiplier > 0f)
						{
							text = text.Replace("/x", "+" + valueChanger.multiplier.ToString());
							break;
						}
						text = text.Replace("/x", valueChanger.multiplier.ToString() ?? "");
						break;
					}
					else
					{
						i++;
					}
				}
				if (flag)
				{
					text = text.Replace("[rotation]", ModTextGen.GetModifierRotation(list).Trim());
				}
			}
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && length == Item2.Modifier.Length.permanent && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("m1");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Destroy)
		{
			text += LangaugeManager.main.GetTextByKey("m2");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Curse, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Destroy)
		{
			text += LangaugeManager.main.GetTextByKey("m3");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.target == Item2.Effect.Target.enemy && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m4");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.target == Item2.Effect.Target.player && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m5");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.target == Item2.Effect.Target.unspecified && length == Item2.Modifier.Length.untilUse && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("m6u");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.target == Item2.Effect.Target.unspecified && length == Item2.Modifier.Length.untilDiscard && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("m6d");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.target == Item2.Effect.Target.unspecified && (length == Item2.Modifier.Length.whileActive || length == Item2.Modifier.Length.untilUnzombied) && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("m6");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.target == Item2.Effect.Target.unspecified && length == Item2.Modifier.Length.whileActive && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative)
		{
			text += LangaugeManager.main.GetTextByKey("m7");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Block && effect.target == Item2.Effect.Target.unspecified && (length == Item2.Modifier.Length.whileActive || length == Item2.Modifier.Length.untilUse))
		{
			text += LangaugeManager.main.GetTextByKey("m8");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Block && effect.target == Item2.Effect.Target.unspecified && length == Item2.Modifier.Length.forTurn)
		{
			text += LangaugeManager.main.GetTextByKey("m8b");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Block && effect.target == Item2.Effect.Target.unspecified && length == Item2.Modifier.Length.forCombat)
		{
			text += LangaugeManager.main.GetTextByKey("m9");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.AP && effect.target == Item2.Effect.Target.unspecified && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m10");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Armor, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Block && effect.target == Item2.Effect.Target.unspecified && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m11");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.target == Item2.Effect.Target.unspecified && length == Item2.Modifier.Length.whileActive && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("m12");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Melee, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.target == Item2.Effect.Target.unspecified && length == Item2.Modifier.Length.whileActive && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("m12mel");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.row, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.target == Item2.Effect.Target.unspecified && length == Item2.Modifier.Length.whileActive && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("m13");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.right, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.target == Item2.Effect.Target.unspecified && length == Item2.Modifier.Length.whileActive && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("m13b");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.bottom, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Cleaver, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.target == Item2.Effect.Target.unspecified && length == Item2.Modifier.Length.whileActive && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative)
		{
			text += LangaugeManager.main.GetTextByKey("m14");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.closest && this.CheckAreas(list, Item2.Area.right, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Arrow, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate && effect.target == Item2.Effect.Target.unspecified && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m15");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.closest && this.CheckAreas(list, Item2.Area.bottom, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Arrow, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate && effect.target == Item2.Effect.Target.unspecified && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m16");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.closest && this.CheckAreas(list, Item2.Area.left, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Arrow, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate && effect.target == Item2.Effect.Target.unspecified && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m17");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.closest && this.CheckAreas(list, Item2.Area.top, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Arrow, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate && effect.target == Item2.Effect.Target.unspecified && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m18");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.right, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Arrow, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate && effect.target == Item2.Effect.Target.unspecified && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m19");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.bottom, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Arrow, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate && effect.target == Item2.Effect.Target.unspecified && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m20");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.left, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Arrow, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate && effect.target == Item2.Effect.Target.unspecified && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m21");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.top, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Arrow, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate && effect.target == Item2.Effect.Target.unspecified && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m22");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.diagonal, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Structure, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.conductive && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m23");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.resetUsesPerCombat && effect.target == Item2.Effect.Target.unspecified && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m24");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.row, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.resetUsesPerTurn && effect.target == Item2.Effect.Target.unspecified && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m25");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Instrument, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.resetUsesPerTurn && effect.target == Item2.Effect.Target.unspecified && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m25i");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative)
		{
			text += LangaugeManager.main.GetTextByKey("m27");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.closest && this.CheckAreas(list, Item2.Area.right, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Arrow, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative)
		{
			text += LangaugeManager.main.GetTextByKey("m28");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.closest && this.CheckAreas(list, Item2.Area.bottom, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Arrow, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative)
		{
			text += LangaugeManager.main.GetTextByKey("m29");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.closest && this.CheckAreas(list, Item2.Area.left, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Arrow, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative)
		{
			text += LangaugeManager.main.GetTextByKey("m30");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.closest && this.CheckAreas(list, Item2.Area.top, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Arrow, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative)
		{
			text += LangaugeManager.main.GetTextByKey("m31");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.disabled && effect.itemStatusEffect[0].length == Item2.ItemStatusEffect.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m32");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Consumable, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.HP && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative)
		{
			text += LangaugeManager.main.GetTextByKey("m33");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("m34");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate)
		{
			text += LangaugeManager.main.GetTextByKey("m35");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Cleaver, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate)
		{
			text += LangaugeManager.main.GetTextByKey("m36");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.diagonal, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Cleaver, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate)
		{
			text += LangaugeManager.main.GetTextByKey("m36b");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.adjacent && this.CheckAreas(list, Item2.Area.top, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Cleaver, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate)
		{
			text += LangaugeManager.main.GetTextByKey("m37");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.adjacent && this.CheckAreas(list, Item2.Area.right, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Cleaver, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate)
		{
			text += LangaugeManager.main.GetTextByKey("m38");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.adjacent && this.CheckAreas(list, Item2.Area.bottom, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Cleaver, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate)
		{
			text += LangaugeManager.main.GetTextByKey("m39");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.adjacent && this.CheckAreas(list, Item2.Area.left, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Cleaver, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate)
		{
			text += LangaugeManager.main.GetTextByKey("m40");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.connected, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative)
		{
			text += LangaugeManager.main.GetTextByKey("m41");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Consumable, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Rage && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("m42");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.summative && length == Item2.Modifier.Length.forCombat)
		{
			text += LangaugeManager.main.GetTextByKey("m43");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.summative && length == Item2.Modifier.Length.forTurn)
		{
			text += LangaugeManager.main.GetTextByKey("m44");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.diagonal, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.summative && length == Item2.Modifier.Length.forTurn)
		{
			text += LangaugeManager.main.GetTextByKey("m45");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.summative && length == Item2.Modifier.Length.forTurn && effect.target == Item2.Effect.Target.player)
		{
			text += LangaugeManager.main.GetTextByKey("m46self");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.summative && length == Item2.Modifier.Length.forTurn)
		{
			text += LangaugeManager.main.GetTextByKey("m46");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.summative && length == Item2.Modifier.Length.forCombat)
		{
			text += LangaugeManager.main.GetTextByKey("m47");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.disabled && effect.itemStatusEffect[0].length == Item2.ItemStatusEffect.Length.turns)
		{
			text += LangaugeManager.main.GetTextByKey("m48");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.locked && effect.itemStatusEffect[0].length == Item2.ItemStatusEffect.Length.combats)
		{
			text += LangaugeManager.main.GetTextByKey("m49");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.diagonal, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Armor, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Block)
		{
			text += LangaugeManager.main.GetTextByKey("m50");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.diagonal, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Armor, Item2.ItemType.Shield, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Block)
		{
			text += LangaugeManager.main.GetTextByKey("m50b");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.disabled && effect.itemStatusEffect[0].length == Item2.ItemStatusEffect.Length.combats && effect.itemStatusEffect[0].num == 1)
		{
			text += LangaugeManager.main.GetTextByKey("m51");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.disabled && effect.itemStatusEffect[0].length == Item2.ItemStatusEffect.Length.combats)
		{
			text += LangaugeManager.main.GetTextByKey("m52");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Helmet, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Block)
		{
			text += LangaugeManager.main.GetTextByKey("m53");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Footwear, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Block)
		{
			text += LangaugeManager.main.GetTextByKey("m54");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate)
		{
			text += LangaugeManager.main.GetTextByKey("m55");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.right, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Arrow, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && flag && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative)
		{
			text += LangaugeManager.main.GetTextByKey("m56");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.left, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Arrow, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && flag && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative)
		{
			text += LangaugeManager.main.GetTextByKey("m57");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.bottom, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Arrow, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && flag && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative)
		{
			text += LangaugeManager.main.GetTextByKey("m58");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.top, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Arrow, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && flag && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative)
		{
			text += LangaugeManager.main.GetTextByKey("m59");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.connected, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage)
		{
			text += LangaugeManager.main.GetTextByKey("m60");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.diagonal, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Shield, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Block && length == Item2.Modifier.Length.forCombat)
		{
			text += LangaugeManager.main.GetTextByKey("m61b");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.diagonal, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Shield, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Block)
		{
			text += LangaugeManager.main.GetTextByKey("m61");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.closest && this.CheckAreas(list, Item2.Area.right, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Arrow, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && flag && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("m62");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.closest && this.CheckAreas(list, Item2.Area.left, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Arrow, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && flag && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("m63");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.closest && this.CheckAreas(list, Item2.Area.bottom, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Arrow, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && flag && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("m64");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.closest && this.CheckAreas(list, Item2.Area.top, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Arrow, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && flag && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("m65");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.left, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Armor, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Block && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("m66");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.column, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("m67");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.diagonal, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Poison && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("m68");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.row, Item2.Area.column, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.summative && length == Item2.Modifier.Length.forCombat)
		{
			text += LangaugeManager.main.GetTextByKey("m69c");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.row, Item2.Area.column, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("m69");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.diagonal, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.summative && length == Item2.Modifier.Length.forCombat)
		{
			text += LangaugeManager.main.GetTextByKey("m70");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Spikes && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.summative && length == Item2.Modifier.Length.forTurn)
		{
			text += LangaugeManager.main.GetTextByKey("m71");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Shield, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Spikes && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.summative && length == Item2.Modifier.Length.forTurn)
		{
			text += LangaugeManager.main.GetTextByKey("m72");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.diagonal, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Cleaver, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate)
		{
			text += LangaugeManager.main.GetTextByKey("m73");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.diagonal, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Cleaver, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage)
		{
			text += LangaugeManager.main.GetTextByKey("m74");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.closest && this.CheckAreas(list, Item2.Area.top, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Cleaver, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate && flag)
		{
			text += LangaugeManager.main.GetTextByKey("m75");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.closest && this.CheckAreas(list, Item2.Area.right, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Cleaver, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate && flag)
		{
			text += LangaugeManager.main.GetTextByKey("m76");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.closest && this.CheckAreas(list, Item2.Area.bottom, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Cleaver, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate && flag)
		{
			text += LangaugeManager.main.GetTextByKey("m77");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.closest && this.CheckAreas(list, Item2.Area.left, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Cleaver, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate && flag)
		{
			text += LangaugeManager.main.GetTextByKey("m78");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Shield, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate)
		{
			text += LangaugeManager.main.GetTextByKey("m79");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.HP)
		{
			text += LangaugeManager.main.GetTextByKey("m80");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.column, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m81");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.row, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Weak && effect.mathematicalType == Item2.Effect.MathematicalType.summative && length == Item2.Modifier.Length.forCombat)
		{
			text += LangaugeManager.main.GetTextByKey("m82");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && length == Item2.Modifier.Length.permanent && modifier.trigger.trigger == Item2.Trigger.ActionTrigger.onComboUse)
		{
			text += LangaugeManager.main.GetTextByKey("m83");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.diagonal, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("m84");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && length == Item2.Modifier.Length.forCombat)
		{
			text += LangaugeManager.main.GetTextByKey("m85c");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative)
		{
			text += LangaugeManager.main.GetTextByKey("m85");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.adjacent && this.CheckAreas(list, Item2.Area.bottom, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && flag)
		{
			text += LangaugeManager.main.GetTextByKey("m86");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.adjacent && this.CheckAreas(list, Item2.Area.left, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && flag)
		{
			text += LangaugeManager.main.GetTextByKey("m87");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.adjacent && this.CheckAreas(list, Item2.Area.top, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && flag)
		{
			text += LangaugeManager.main.GetTextByKey("m88");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.adjacent && this.CheckAreas(list, Item2.Area.right, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && flag)
		{
			text += LangaugeManager.main.GetTextByKey("m89");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.row, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Poison && effect.mathematicalType == Item2.Effect.MathematicalType.summative && length == Item2.Modifier.Length.forCombat)
		{
			text += LangaugeManager.main.GetTextByKey("m90");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.diagonal, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Consumable, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Destroy)
		{
			text += LangaugeManager.main.GetTextByKey("m91");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Poison && effect.mathematicalType == Item2.Effect.MathematicalType.summative && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m92");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.oneSpaceOver, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.summative && length == Item2.Modifier.Length.forTurn)
		{
			text += LangaugeManager.main.GetTextByKey("m93b");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.oneSpaceOver, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.summative && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m93");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.AllStatusEffects && effect.mathematicalType == Item2.Effect.MathematicalType.summative && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m94");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.AddDamageToScratch && effect.mathematicalType == Item2.Effect.MathematicalType.summative && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m95");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.right, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Helmet, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.disabled && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m96");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Shield, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Spikes && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m97");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.right, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Footwear, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.disabled && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m98");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.bottom, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Structure, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Block && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m99");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Poison && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m100");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Dodge && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m100dod");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Rage && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m100rag");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Shield, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Spikes && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m101");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.diagonal, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Consumable, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.HP && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m102");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.column, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && length == Item2.Modifier.Length.forTurn)
		{
			text += LangaugeManager.main.GetTextByKey("m103");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.column, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && length == Item2.Modifier.Length.forCombat)
		{
			text += LangaugeManager.main.GetTextByKey("m103b");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.column, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Wand, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.summative && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m104");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.AddToManaCost && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.itemStatusEffect[0].length == Item2.ItemStatusEffect.Length.permanent)
		{
			text += LangaugeManager.main.GetTextByKey("m105");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.AddToEnergyCost && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.itemStatusEffect[0].length == Item2.ItemStatusEffect.Length.combats)
		{
			text += LangaugeManager.main.GetTextByKey("m106");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.myPlaySpace, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && length == Item2.Modifier.Length.permanent)
		{
			text += LangaugeManager.main.GetTextByKey("m110p");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.myPlaySpace, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative)
		{
			text += LangaugeManager.main.GetTextByKey("m110");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.myPlaySpace, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate)
		{
			text += LangaugeManager.main.GetTextByKey("m111");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.myPlaySpace, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Destroy)
		{
			text += LangaugeManager.main.GetTextByKey("m112");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.myPlaySpace, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Destroy)
		{
			text += LangaugeManager.main.GetTextByKey("m115");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.zTop && this.CheckAreas(list, Item2.Area.myPlaySpace, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Destroy)
		{
			text += LangaugeManager.main.GetTextByKey("m115");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.myPlaySpace, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.resetUsesPerTurn)
		{
			text += LangaugeManager.main.GetTextByKey("m115b");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Shield, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Block && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("m117");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Mana && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("m118");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Consumable, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Destroy && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("m119");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Fish, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Destroy && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("m119");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.closest && this.CheckAreas(list, Item2.Area.left, Item2.Area.undefined, Item2.Area.undefined) && flag && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Destroy)
		{
			text += LangaugeManager.main.GetTextByKey("m132");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.closest && this.CheckAreas(list, Item2.Area.top, Item2.Area.undefined, Item2.Area.undefined) && flag && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Destroy)
		{
			text += LangaugeManager.main.GetTextByKey("m133");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.closest && this.CheckAreas(list, Item2.Area.right, Item2.Area.undefined, Item2.Area.undefined) && flag && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Destroy)
		{
			text += LangaugeManager.main.GetTextByKey("m134");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.closest && this.CheckAreas(list, Item2.Area.bottom, Item2.Area.undefined, Item2.Area.undefined) && flag && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Destroy)
		{
			text += LangaugeManager.main.GetTextByKey("m135");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.adjacent && this.CheckAreas(list, Item2.Area.left, Item2.Area.undefined, Item2.Area.undefined) && flag && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Destroy)
		{
			text += LangaugeManager.main.GetTextByKey("m132b");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.adjacent && this.CheckAreas(list, Item2.Area.top, Item2.Area.undefined, Item2.Area.undefined) && flag && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Destroy)
		{
			text += LangaugeManager.main.GetTextByKey("m133b");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.adjacent && this.CheckAreas(list, Item2.Area.right, Item2.Area.undefined, Item2.Area.undefined) && flag && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Destroy)
		{
			text += LangaugeManager.main.GetTextByKey("m134b");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.adjacent && this.CheckAreas(list, Item2.Area.bottom, Item2.Area.undefined, Item2.Area.undefined) && flag && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Destroy)
		{
			text += LangaugeManager.main.GetTextByKey("m135b");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.AddToEnergyCost && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("m137");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.resetUsesPerTurn && effect.target == Item2.Effect.Target.unspecified && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m138");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Burn && length == Item2.Modifier.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m140");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Weak && length == Item2.Modifier.Length.forCombat)
		{
			text += LangaugeManager.main.GetTextByKey("m142");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.DiscardCarving)
		{
			text += LangaugeManager.main.GetTextByKey("m158");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.BanishCarving)
		{
			text += LangaugeManager.main.GetTextByKey("m113");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.AddToEnergyCost && effect.itemStatusEffect[0].length == Item2.ItemStatusEffect.Length.turns)
		{
			text += LangaugeManager.main.GetTextByKey("m159");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.AddToEnergyCost && effect.itemStatusEffect[0].length == Item2.ItemStatusEffect.Length.permanent)
		{
			text += LangaugeManager.main.GetTextByKey("m159p");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Carving, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.DiscardCarving)
		{
			text += LangaugeManager.main.GetTextByKey("m161");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.myPlaySpace, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Carving, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.DiscardCarving)
		{
			text += LangaugeManager.main.GetTextByKey("m162");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.myPlaySpace, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Carving, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.BanishCarving)
		{
			text += LangaugeManager.main.GetTextByKey("m162b");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Wand, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage)
		{
			text += LangaugeManager.main.GetTextByKey("m167");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.diagonal, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Wand, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.AddToManaCost && effect.itemStatusEffect[0].length == Item2.ItemStatusEffect.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m168");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.AddToManaCost && effect.itemStatusEffect[0].length == Item2.ItemStatusEffect.Length.turns)
		{
			text += LangaugeManager.main.GetTextByKey("m169");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.AddToManaCost && effect.itemStatusEffect[0].length == Item2.ItemStatusEffect.Length.combats)
		{
			text += LangaugeManager.main.GetTextByKey("m169b");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.AddToManaCost && effect.itemStatusEffect[0].length == Item2.ItemStatusEffect.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m169c");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.Bow, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.AddToEnergyCost && effect.itemStatusEffect[0].length == Item2.ItemStatusEffect.Length.whileActive)
		{
			text += LangaugeManager.main.GetTextByKey("m170");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Burn && effect.target == Item2.Effect.Target.player && length == Item2.Modifier.Length.forCombat)
		{
			text += LangaugeManager.main.GetTextByKey("m171b");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Burn)
		{
			text += LangaugeManager.main.GetTextByKey("m171");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Carving, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.BanishCarving)
		{
			text += LangaugeManager.main.GetTextByKey("m172");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.isActivated && effect.itemStatusEffect[0].length == Item2.ItemStatusEffect.Length.combats)
		{
			text += LangaugeManager.main.GetTextByKey("m173");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Drum, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.isActivated && effect.itemStatusEffect[0].length == Item2.ItemStatusEffect.Length.combats)
		{
			text += LangaugeManager.main.GetTextByKey("m174");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Drum, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Charm)
		{
			text += LangaugeManager.main.GetTextByKey("m175");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.inAnotherPocket, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Instrument, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Charm)
		{
			text += LangaugeManager.main.GetTextByKey("m176");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Charm)
		{
			text += LangaugeManager.main.GetTextByKey("m177");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.column, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Instrument, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Charm)
		{
			text += LangaugeManager.main.GetTextByKey("m178");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.oneSpaceOver, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Hammer, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("m179");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.oneSpaceOver, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Hammer, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative)
		{
			text += LangaugeManager.main.GetTextByKey("m180");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.oneSpaceOver, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Hammer, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Burn && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("m181");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.GetGold && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("m182");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.oneSpaceOver, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Hammer, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.resetUsesPerTurn)
		{
			text += LangaugeManager.main.GetTextByKey("m183");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.diagonal, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Hammer, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.resetUsesPerTurn)
		{
			text += LangaugeManager.main.GetTextByKey("m184");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.inAnotherPocket, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Hammer, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.resetUsesPerTurn)
		{
			text += LangaugeManager.main.GetTextByKey("m185");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.inAnotherPocket, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Arrow, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate)
		{
			text += LangaugeManager.main.GetTextByKey("m186");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.diagonal, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Drum, Item2.ItemType.Instrument, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Charm)
		{
			text += LangaugeManager.main.GetTextByKey("m187");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Drum, Item2.ItemType.Instrument, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Charm)
		{
			text += LangaugeManager.main.GetTextByKey("m187b");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.oneSpaceOver, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Melee, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && length == Item2.Modifier.Length.forTurn)
		{
			text += LangaugeManager.main.GetTextByKey("m188b");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.oneSpaceOver, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Melee, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage)
		{
			text += LangaugeManager.main.GetTextByKey("m188");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.locked && effect.itemStatusEffect[0].length == Item2.ItemStatusEffect.Length.combats)
		{
			text += LangaugeManager.main.GetTextByKey("m189");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Rage && effect.target == Item2.Effect.Target.allEnemies && length == Item2.Modifier.Length.forCombat)
		{
			text += LangaugeManager.main.GetTextByKey("m190");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.AddToGoldCost && effect.itemStatusEffect[0].length == Item2.ItemStatusEffect.Length.combats)
		{
			text += LangaugeManager.main.GetTextByKey("m191");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Poison && effect.target == Item2.Effect.Target.player && length == Item2.Modifier.Length.forCombat)
		{
			text += LangaugeManager.main.GetTextByKey("m192");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Burn && effect.target == Item2.Effect.Target.player && length == Item2.Modifier.Length.forCombat)
		{
			text += LangaugeManager.main.GetTextByKey("m193");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Freeze && effect.target == Item2.Effect.Target.player && length == Item2.Modifier.Length.forCombat)
		{
			text += LangaugeManager.main.GetTextByKey("m194");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Armor, Item2.ItemType.Shield, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Block)
		{
			text += LangaugeManager.main.GetTextByKey("m195");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.disabled && effect.itemStatusEffect[0].length == Item2.ItemStatusEffect.Length.combats)
		{
			text += LangaugeManager.main.GetTextByKey("m196com");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Destroy)
		{
			text += LangaugeManager.main.GetTextByKey("m196d");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.disabled)
		{
			text += LangaugeManager.main.GetTextByKey("m196");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.disabled && effect.itemStatusEffect[0].length == Item2.ItemStatusEffect.Length.turns)
		{
			text += LangaugeManager.main.GetTextByKey("m196w2");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.disabled)
		{
			text += LangaugeManager.main.GetTextByKey("m196w");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.adjacent && this.CheckAreas(list, Item2.Area.bottom, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.disabled)
		{
			text += LangaugeManager.main.GetTextByKey("m196b");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.adjacent && this.CheckAreas(list, Item2.Area.top, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.disabled)
		{
			text += LangaugeManager.main.GetTextByKey("m196c");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.myPlaySpace, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Pet, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.RevivePets && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("m197");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.myPlaySpace, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Pet, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.RevivePets && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative)
		{
			text += LangaugeManager.main.GetTextByKey("m198");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.myPlaySpace, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Block && effect.mathematicalType == Item2.Effect.MathematicalType.summative && modifier.length == Item2.Modifier.Length.forCombat)
		{
			text += LangaugeManager.main.GetTextByKey("m202");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Spikes && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("m203");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.myPlaySpace, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Damage && effect.mathematicalType == Item2.Effect.MathematicalType.summative && modifier.length == Item2.Modifier.Length.forTurn)
		{
			text += LangaugeManager.main.GetTextByKey("m205");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.myPlaySpace, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Poison && effect.mathematicalType == Item2.Effect.MathematicalType.summative && modifier.length == Item2.Modifier.Length.forTurn)
		{
			text += LangaugeManager.main.GetTextByKey("m206");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.myPlaySpace, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Poison && effect.mathematicalType == Item2.Effect.MathematicalType.summative && modifier.length == Item2.Modifier.Length.forCombat)
		{
			text += LangaugeManager.main.GetTextByKey("m206b");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.column, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Totem, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate)
		{
			text += LangaugeManager.main.GetTextByKey("m207");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.adjacent && this.CheckAreas(list, Item2.Area.top, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate && flag)
		{
			text += LangaugeManager.main.GetTextByKey("m209");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.adjacent && this.CheckAreas(list, Item2.Area.left, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate && flag)
		{
			text += LangaugeManager.main.GetTextByKey("m209l");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.adjacent && this.CheckAreas(list, Item2.Area.right, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate && flag)
		{
			text += LangaugeManager.main.GetTextByKey("m209r");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.adjacent && this.CheckAreas(list, Item2.Area.bottom, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate && flag)
		{
			text += LangaugeManager.main.GetTextByKey("m209d");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Kin, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate)
		{
			text += LangaugeManager.main.GetTextByKey("m210");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Carving, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.DiscardCarving)
		{
			text += LangaugeManager.main.GetTextByKey("m211");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.row, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Carving, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.DiscardCarving)
		{
			text += LangaugeManager.main.GetTextByKey("m214");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.adjacent && this.CheckAreas(list, Item2.Area.top, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Pet, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.SummonPet)
		{
			text += LangaugeManager.main.GetTextByKey("m215");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.adjacent && this.CheckAreas(list, Item2.Area.left, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Pet, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.SummonPet)
		{
			text += LangaugeManager.main.GetTextByKey("m215l");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.adjacent && this.CheckAreas(list, Item2.Area.right, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Pet, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.SummonPet)
		{
			text += LangaugeManager.main.GetTextByKey("m215r");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.adjacent && this.CheckAreas(list, Item2.Area.bottom, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Pet, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.SummonPet)
		{
			text += LangaugeManager.main.GetTextByKey("m215b");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.board, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Pet, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate)
		{
			text += LangaugeManager.main.GetTextByKey("m216");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Pet, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate)
		{
			text += LangaugeManager.main.GetTextByKey("m217");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.diagonal, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Instrument, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate)
		{
			text += LangaugeManager.main.GetTextByKey("m218");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.connected, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.ManaStone, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.disabled)
		{
			text += LangaugeManager.main.GetTextByKey("m223");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Hazard, Item2.ItemType.undefined, Item2.ItemType.undefined))
		{
			text += LangaugeManager.main.GetTextByKey("m224");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.diagonal, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Hazard, Item2.ItemType.undefined, Item2.ItemType.undefined))
		{
			text += LangaugeManager.main.GetTextByKey("m225");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.myPlaySpace, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.locked)
		{
			text += LangaugeManager.main.GetTextByKey("m226");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.bottom, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.disabled && effect.itemStatusEffect[0].length == Item2.ItemStatusEffect.Length.turns)
		{
			text += LangaugeManager.main.GetTextByKey("m227");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate)
		{
			text += LangaugeManager.main.GetTextByKey("m228");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.diagonal, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Hazard, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Destroy)
		{
			text += LangaugeManager.main.GetTextByKey("m230");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Book, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Activate)
		{
			text += LangaugeManager.main.GetTextByKey("m231");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.RemoveFromGrid)
		{
			text += LangaugeManager.main.GetTextByKey("m234");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.adjacent && this.CheckAreas(list, Item2.Area.left, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.ghostly && effect.itemStatusEffect[0].length == Item2.ItemStatusEffect.Length.turns)
		{
			text += LangaugeManager.main.GetTextByKey("m235a");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.adjacent && this.CheckAreas(list, Item2.Area.right, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.ghostly && effect.itemStatusEffect[0].length == Item2.ItemStatusEffect.Length.turns)
		{
			text += LangaugeManager.main.GetTextByKey("m235b");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.adjacent && this.CheckAreas(list, Item2.Area.top, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.ghostly && effect.itemStatusEffect[0].length == Item2.ItemStatusEffect.Length.turns)
		{
			text += LangaugeManager.main.GetTextByKey("m235c");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.adjacent && this.CheckAreas(list, Item2.Area.bottom, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ItemStatusEffect && effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.ghostly && effect.itemStatusEffect[0].length == Item2.ItemStatusEffect.Length.turns)
		{
			text += LangaugeManager.main.GetTextByKey("m235d");
		}
		else if (this.CheckAreas(list, Item2.Area.self, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.TransformItemSwitch)
		{
			text += LangaugeManager.main.GetTextByKey("m236");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Component, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.resetComponentHeat)
		{
			text += LangaugeManager.main.GetTextByKey("m238");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.diagonal, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Component, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.resetComponentHeat)
		{
			text += LangaugeManager.main.GetTextByKey("m237");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.row, Item2.Area.column, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Hazard, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Destroy)
		{
			text += LangaugeManager.main.GetTextByKey("m241");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.inAnotherPocket, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Destroy)
		{
			text += LangaugeManager.main.GetTextByKey("m246");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Weapon, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.Slow)
		{
			text += LangaugeManager.main.GetTextByKey("m249");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.bottom, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ProvideSunshine)
		{
			text += LangaugeManager.main.GetTextByKey("m253");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.row, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ProvideSunshine)
		{
			text += LangaugeManager.main.GetTextByKey("m254");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.adjacent, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ProvideSunshine)
		{
			text += LangaugeManager.main.GetTextByKey("m255");
		}
		else if (modifier.areaDistance == Item2.AreaDistance.all && this.CheckAreas(list, Item2.Area.inAnotherPocket, Item2.Area.undefined, Item2.Area.undefined) && Card.CheckTypes(typesToModify, Item2.ItemType.Any, Item2.ItemType.undefined, Item2.ItemType.undefined) && effect.type == Item2.Effect.Type.ProvideSunshine)
		{
			text += LangaugeManager.main.GetTextByKey("m256");
		}
		float num = effect.value;
		foreach (ValueChanger valueChanger2 in this.parent.GetComponentsInChildren<ValueChanger>())
		{
			if (valueChanger2.valueToReplace == effect.value)
			{
				num = valueChanger2.multiplier;
			}
		}
		if (effect.itemStatusEffect.Count > 0)
		{
			text = text.Replace("/y", Mathf.RoundToInt((float)effect.itemStatusEffect[0].num).ToString() ?? "");
		}
		if (effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			if (num >= 0f)
			{
				text = text.Replace("/x", "+" + num.ToString());
			}
			else
			{
				text = text.Replace("/x", num.ToString() ?? "");
			}
		}
		else if (num >= 0f)
		{
			text = text.Replace("/x", "+" + Mathf.RoundToInt(num * 100f).ToString());
		}
		else
		{
			text = text.Replace("/x", Mathf.RoundToInt(num * 100f).ToString() ?? "");
		}
		if (modifier.origin != "self" && modifier.origin != "" && modifier.origin.Length > 1 && showOrigin)
		{
			string text2 = LangaugeManager.main.GetTextByKey("amApplied").Replace("/x", LangaugeManager.main.GetTextByKey(modifier.origin));
			text = "<color=#FFAAFF>" + text2 + ":</color><br><size=25%><br></size>" + text.Trim();
		}
		text = text.Replace("[", "<color=#87ffe3>");
		text = text.Replace("]", "</color>");
		return text;
	}

	// Token: 0x060003C9 RID: 969 RVA: 0x0002065C File Offset: 0x0001E85C
	public string GetTextDescriptions(Item2 item, GameObject p)
	{
		this.GetDescriptions(item, p, false);
		string text = "";
		foreach (TextMeshProUGUI textMeshProUGUI in p.GetComponentsInChildren<TextMeshProUGUI>())
		{
			text += textMeshProUGUI.text;
		}
		return text;
	}

	// Token: 0x060003CA RID: 970 RVA: 0x000206A0 File Offset: 0x0001E8A0
	public List<Card.DescriptorTotal> GetDescriptions(Item2 item, GameObject p, bool headless = false)
	{
		this.parent = p;
		GameManager main = GameManager.main;
		Curse component = this.parent.GetComponent<Curse>();
		if (item && item.offsetOfItemShape != 0f)
		{
			this.itemShapeParent.localPosition = new Vector3(this.itemShapeParent.transform.localPosition.x, item.offsetOfItemShape, 0f);
		}
		ValueChanger[] componentsInChildren = p.GetComponentsInChildren<ValueChanger>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].FindEffectTemp();
		}
		ValueChangerEx[] componentsInChildren2 = p.GetComponentsInChildren<ValueChangerEx>();
		for (int i = 0; i < componentsInChildren2.Length; i++)
		{
			componentsInChildren2[i].FindEffectTemp();
		}
		if (!headless)
		{
			BoxCollider2D[] componentsInChildren3 = p.GetComponentsInChildren<BoxCollider2D>();
			new List<Vector2>();
			foreach (BoxCollider2D boxCollider2D in componentsInChildren3)
			{
				for (int j = 0; j < Mathf.RoundToInt(boxCollider2D.size.x); j++)
				{
					for (int k = 0; k < Mathf.RoundToInt(boxCollider2D.size.y); k++)
					{
						GameObject gameObject = Object.Instantiate<GameObject>(this.itemShapePrefab, Vector3.zero, Quaternion.identity, this.itemShapeParent);
						float num = boxCollider2D.size.x / -2f + (float)j + 0.5f + boxCollider2D.offset.x;
						float num2 = boxCollider2D.size.y / -2f + (float)k + 0.5f + boxCollider2D.offset.y;
						gameObject.transform.localPosition = new Vector3(num * 26f, num2 * 26f, 0f);
					}
				}
			}
			float num3 = 0f;
			float num4 = 0f;
			List<float> list = new List<float>();
			List<float> list2 = new List<float>();
			foreach (object obj in this.itemShapeParent)
			{
				Transform transform = (Transform)obj;
				float x = transform.position.x;
				float y = transform.position.y;
				if (!list.Contains(x))
				{
					num3 += x;
					list.Add(x);
				}
				if (!list2.Contains(y))
				{
					num4 += y;
					list2.Add(y);
				}
			}
			float num5 = num3 / (float)list.Count;
			float num6 = num4 / (float)list2.Count;
			float num7 = num5 - this.itemShapeParent.position.x;
			float num8 = num6 - this.itemShapeParent.position.y;
			foreach (object obj2 in this.itemShapeParent)
			{
				((Transform)obj2).localPosition += Vector3.right * num7 + Vector3.up * num8 + Vector3.right * (float)(list.Count - 1) * 13f + Vector3.down * (float)(list2.Count - 1) * 13f;
			}
		}
		string text = "";
		if (LangaugeManager.main.KeyExists(Item2.GetDisplayName(item.name)))
		{
			text = LangaugeManager.main.GetTextByKey(Item2.GetDisplayName(item.name));
		}
		else if (item.transform.parent != null && LangaugeManager.main.KeyExists(Item2.GetDisplayName(item.transform.parent.name)))
		{
			text = LangaugeManager.main.GetTextByKey(Item2.GetDisplayName(item.transform.parent.name));
		}
		this.displayName.text = text;
		if (this.displayName.text.Length <= 12)
		{
			this.displayName.fontSize = 48f;
		}
		else
		{
			this.displayName.characterSpacing = -0.3f * (float)this.displayName.text.Length;
			this.displayName.fontSize = (float)(620 / this.displayName.text.Length);
		}
		if (Item2.GetCurrentCost(Item2.Cost.Type.energy, item.costs) == -999 || (Item2.GetCurrentCost(Item2.Cost.Type.energy, item.costs) == 0 && item.itemType.Contains(Item2.ItemType.Component) && !item.CheckForStatusEffect(Item2.ItemStatusEffect.Type.canBeUsedByCR8Directly)))
		{
			this.apCostText.SetActive(false);
		}
		else
		{
			this.apCostText.SetActive(true);
			this.apCostText.GetComponentInChildren<TextMeshProUGUI>().text = Item2.GetCurrentCostText(Item2.Cost.Type.energy, item.costs);
		}
		if (Item2.GetCurrentCost(Item2.Cost.Type.mana, item.costs) == -999)
		{
			this.manaText.SetActive(false);
		}
		else
		{
			this.manaText.SetActive(true);
			this.manaText.GetComponentInChildren<TextMeshProUGUI>().text = Item2.GetCurrentCostText(Item2.Cost.Type.mana, item.costs);
		}
		if (Item2.GetCurrentCost(Item2.Cost.Type.gold, item.costs) == -999)
		{
			this.goldCostText.SetActive(false);
		}
		else
		{
			this.goldCostText.SetActive(true);
			this.goldCostText.GetComponentInChildren<TextMeshProUGUI>().text = Item2.GetCurrentCostText(Item2.Cost.Type.gold, item.costs);
		}
		if (this.sunshineChargesText)
		{
			if (Item2.GetCurrentCost(Item2.Cost.Type.sunShine, item.costs) == -999)
			{
				this.sunshineChargesText.SetActive(false);
			}
			else
			{
				this.sunshineChargesText.SetActive(true);
				this.sunshineChargesText.GetComponentInChildren<TextMeshProUGUI>().text = Item2.GetCurrentCostText(Item2.Cost.Type.sunShine, item.costs);
			}
		}
		if (item.GetStatusEffectValue(Item2.ItemStatusEffect.Type.canBeMovedInCombat) != -1 || item.GetStatusEffectValue(Item2.ItemStatusEffect.Type.canBeMovedInCombatButReturnsToOriginalPosition) != -1)
		{
			this.moveableSymbol.SetActive(true);
		}
		else
		{
			this.moveableSymbol.SetActive(false);
		}
		if (this.transformableSymbol)
		{
			if (p.GetComponent<ItemSwitcher>())
			{
				this.transformableSymbol.SetActive(true);
				if (!item.CanBeManuallyTransformed())
				{
					this.transformableSymbol.GetComponentInChildren<Image>().sprite = this.automatedTransformSprite;
					this.transformableSymbol.GetComponentInChildren<SimpleHoverText>().SetText("TransformAutomatedHover");
				}
			}
			else
			{
				this.transformableSymbol.SetActive(false);
			}
		}
		if (this.alternateUseSymbol)
		{
			this.alternateUseSymbol.SetActive(false);
		}
		EnergyEmitter energyEmitter = item.GetComponent<EnergyEmitter>();
		if (!energyEmitter)
		{
			energyEmitter = item.GetComponentInParent<EnergyEmitter>();
		}
		if (energyEmitter)
		{
			if (energyEmitter.maxHeat > 0)
			{
				this.apSummonCostText.SetActive(true);
				TMP_Text componentInChildren = this.apSummonCostText.GetComponentInChildren<TextMeshProUGUI>();
				int i = Mathf.Max(energyEmitter.maxHeat, 0);
				componentInChildren.text = i.ToString() ?? "";
			}
			else
			{
				this.apSummonCostText.SetActive(false);
			}
		}
		Carving component2 = item.GetComponent<Carving>();
		if (component2)
		{
			if (item.itemMovement && item.itemMovement.inGrid)
			{
				this.summonCostsParent.transform.localScale = Vector3.one * 0.75f;
				Image[] array2 = this.summonCostsParent.GetComponentsInChildren<Image>();
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i].color = new Color(0.4f, 0.4f, 0.4f);
				}
				this.standardCostsParent.transform.localScale = Vector3.one * 1f;
				array2 = this.standardCostsParent.GetComponentsInChildren<Image>();
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i].color = Color.white;
				}
			}
			else
			{
				this.summonCostsParent.transform.localScale = Vector3.one * 1f;
				Image[] array2 = this.summonCostsParent.GetComponentsInChildren<Image>();
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i].color = Color.white;
				}
				this.standardCostsParent.transform.localScale = Vector3.one * 0.75f;
				array2 = this.standardCostsParent.GetComponentsInChildren<Image>();
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i].color = new Color(0.4f, 0.4f, 0.4f);
				}
			}
			int currentCost = Item2.GetCurrentCost(Item2.Cost.Type.energy, component2.summoningCosts);
			if (currentCost == -999)
			{
				this.apSummonCostText.SetActive(false);
			}
			else
			{
				this.apSummonCostText.SetActive(true);
				TMP_Text componentInChildren2 = this.apSummonCostText.GetComponentInChildren<TextMeshProUGUI>();
				int i = Mathf.Max(currentCost, 0);
				componentInChildren2.text = i.ToString() ?? "";
			}
			int currentCost2 = Item2.GetCurrentCost(Item2.Cost.Type.mana, component2.summoningCosts);
			if (currentCost2 == -999)
			{
				this.manaSummonText.SetActive(false);
			}
			else
			{
				this.manaSummonText.SetActive(true);
				TMP_Text componentInChildren3 = this.manaSummonText.GetComponentInChildren<TextMeshProUGUI>();
				int i = Mathf.Max(currentCost2, 0);
				componentInChildren3.text = i.ToString() ?? "";
			}
			int currentCost3 = Item2.GetCurrentCost(Item2.Cost.Type.gold, component2.summoningCosts);
			if (currentCost3 == -999)
			{
				this.goldSummonCostText.SetActive(false);
			}
			else
			{
				this.goldSummonCostText.SetActive(true);
				TMP_Text componentInChildren4 = this.goldSummonCostText.GetComponentInChildren<TextMeshProUGUI>();
				int i = Mathf.Max(currentCost3, 0);
				componentInChildren4.text = i.ToString() ?? "";
			}
		}
		bool flag = true;
		int num9 = 0;
		this.itemType.text = "";
		foreach (Item2.ItemType itemType in item.itemType)
		{
			if (itemType == Item2.ItemType.Relic || itemType == Item2.ItemType.Curse || itemType == Item2.ItemType.Blessing || itemType == Item2.ItemType.Hazard || itemType == Item2.ItemType.Fragment)
			{
				flag = false;
			}
			if (num9 > 0)
			{
				TextMeshProUGUI textMeshProUGUI = this.itemType;
				textMeshProUGUI.text += "<br>";
			}
			TextMeshProUGUI textMeshProUGUI2 = this.itemType;
			textMeshProUGUI2.text += LangaugeManager.main.GetTextByKey(itemType.ToString());
			num9++;
		}
		if (flag && !item.hideRarity)
		{
			this.itemRarity.text = LangaugeManager.main.GetTextByKey(item.rarity.ToString());
			if (item.rarity == Item2.Rarity.Common)
			{
				this.itemRarity.color = Color.white;
			}
			else if (item.rarity == Item2.Rarity.Uncommon)
			{
				this.itemRarity.color = Color.green;
			}
			else if (item.rarity == Item2.Rarity.Rare)
			{
				this.itemRarity.color = Color.yellow;
			}
			else if (item.rarity == Item2.Rarity.Legendary)
			{
				this.itemRarity.color = Color.magenta;
			}
		}
		else
		{
			this.itemRarity.text = "";
		}
		new List<string>();
		if (!item.CheckForStatusEffect(Item2.ItemStatusEffect.Type.cantBeSold) || item.isForSale)
		{
			Store store = Object.FindObjectOfType<Store>();
			if (store && item.gameObject && item.gameObject.activeInHierarchy && ((!store.carvingStore && !item.itemType.Contains(Item2.ItemType.Carving)) || item.isForSale))
			{
				this.priceTag.SetActive(true);
				if (item.isOwned)
				{
					this.priceTagText.text = LangaugeManager.main.GetTextByKey("owned") + ":" + item.cost.ToString();
				}
				else if (!item.isDiscounted)
				{
					this.priceTagText.text = LangaugeManager.main.GetTextByKey("cost") + ":" + item.cost.ToString();
				}
				else
				{
					this.priceTagText.text = LangaugeManager.main.GetTextByKey("onSale") + ":" + item.cost.ToString();
				}
			}
		}
		List<Card.DescriptorTotal> list3 = new List<Card.DescriptorTotal>();
		List<Card.ItemStatusEffectCounter> list4 = new List<Card.ItemStatusEffectCounter>();
		using (List<ContextMenuButton.ContextMenuButtonAndCost>.Enumerator enumerator3 = item.contextMenuOptions.GetEnumerator())
		{
			while (enumerator3.MoveNext())
			{
				if (enumerator3.Current.type == ContextMenuButton.Type.selectForComboUse)
				{
					List<Item2.ItemType> comboTypes = item.GetComboTypes();
					if (comboTypes.Contains(Item2.ItemType.Any))
					{
						Card.DescriptorTotal descriptorTotal = new Card.DescriptorTotal();
						descriptorTotal.trigger = null;
						string textByKey = LangaugeManager.main.GetTextByKey("cm20explAny");
						descriptorTotal.texts = new List<string> { textByKey };
						list3.Add(descriptorTotal);
					}
					else
					{
						string text2 = "";
						foreach (Item2.ItemType itemType2 in comboTypes)
						{
							text2 += "<br>  ";
							text2 += LangaugeManager.main.GetTextByKey(itemType2.ToString());
						}
						Card.DescriptorTotal descriptorTotal2 = new Card.DescriptorTotal();
						descriptorTotal2.trigger = null;
						string text3 = LangaugeManager.main.GetTextByKey("cm20expl");
						text3 = text3.Replace("/x", text2);
						descriptorTotal2.texts = new List<string> { text3 };
						list3.Add(descriptorTotal2);
					}
				}
			}
		}
		foreach (Item2.ItemStatusEffect itemStatusEffect in item.activeItemStatusEffects)
		{
			if (itemStatusEffect.showOnCard && itemStatusEffect.type != Item2.ItemStatusEffect.Type.runsAutomaticallyOnCoreUse && itemStatusEffect.type != Item2.ItemStatusEffect.Type.strengthBasedOnDistance && itemStatusEffect.type != Item2.ItemStatusEffect.Type.cannotBeFoundInStores && itemStatusEffect.type != Item2.ItemStatusEffect.Type.cantBeSold && itemStatusEffect.type != Item2.ItemStatusEffect.Type.canBeForged && ((itemStatusEffect.type != Item2.ItemStatusEffect.Type.canBeMovedInCombat && itemStatusEffect.type != Item2.ItemStatusEffect.Type.canBeMovedInCombatButReturnsToOriginalPosition) || item.mustBePlacedOnItemTypeInCombat == Item2.ItemType.Grid))
			{
				bool flag2 = false;
				foreach (Card.ItemStatusEffectCounter itemStatusEffectCounter in list4)
				{
					if (itemStatusEffectCounter.type == itemStatusEffect.type)
					{
						flag2 = true;
						itemStatusEffectCounter.effects.Add(itemStatusEffect);
						break;
					}
				}
				if (!flag2)
				{
					list4.Add(new Card.ItemStatusEffectCounter
					{
						type = itemStatusEffect.type,
						effects = new List<Item2.ItemStatusEffect> { itemStatusEffect }
					});
				}
			}
		}
		foreach (SpecificConditionToUse specificConditionToUse in this.parent.GetComponentsInChildren<SpecificConditionToUse>())
		{
			if (specificConditionToUse.cardKey.Length > 1)
			{
				Card.DescriptorTotal descriptorTotal3 = new Card.DescriptorTotal();
				descriptorTotal3.trigger = null;
				string text4 = LangaugeManager.main.GetTextByKey(specificConditionToUse.cardKey);
				text4 = text4.Replace("/x", specificConditionToUse.value.ToString());
				descriptorTotal3.texts = new List<string> { text4 };
				list3.Add(descriptorTotal3);
			}
		}
		if (item.showTextForMovement && (item.mustBePlacedOnItemType != Item2.ItemType.Grid || item.mustBePlacedOnItemTypeInCombat != Item2.ItemType.Grid))
		{
			Card.DescriptorTotal descriptorTotal4 = new Card.DescriptorTotal();
			descriptorTotal4.trigger = null;
			string text5;
			if (item.mustBePlacedOnItemType != Item2.ItemType.Any && item.mustBePlacedOnItemType != Item2.ItemType.Grid)
			{
				text5 = LangaugeManager.main.GetTextByKey("isCurseMBPO");
				text5 = text5 + " " + LangaugeManager.main.GetTextByKey(item.mustBePlacedOnItemType.ToString());
			}
			else if (item.mustBePlacedOnItemTypeInCombat != Item2.ItemType.Any && item.mustBePlacedOnItemTypeInCombat != Item2.ItemType.Grid)
			{
				text5 = LangaugeManager.main.GetTextByKey("isCurseMBPO");
				text5 = text5 + " " + LangaugeManager.main.GetTextByKey(item.mustBePlacedOnItemTypeInCombat.ToString());
			}
			else
			{
				text5 = LangaugeManager.main.GetTextByKey("isCurseMBPO2");
			}
			descriptorTotal4.texts = new List<string> { text5 };
			list3.Add(descriptorTotal4);
		}
		RunType.RunProperty runProperty = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.unlimitedForges);
		int statusEffectValue = item.GetStatusEffectValue(Item2.ItemStatusEffect.Type.canBeForged);
		if (statusEffectValue > 0 && runProperty == null)
		{
			this.forgeCount.gameObject.SetActive(true);
			this.forgeCount.text = statusEffectValue.ToString() ?? "";
		}
		else
		{
			this.forgeCount.gameObject.SetActive(false);
		}
		if (Singleton.Instance.storyMode)
		{
			if (this.metaProgressImage3)
			{
				this.metaProgressImage3.SetActive(false);
			}
			if (!this.metaProgressImage1 || !this.metaProgressImage2)
			{
				goto IL_11B4;
			}
			List<Overworld_BuildingInterface.Research> list5;
			List<Overworld_BuildingInterface.Research> list6;
			DebugItemManager.main.FindResearch(item, out list5, out list6);
			List<Overworld_BuildingInterface.Research> list7 = new List<Overworld_BuildingInterface.Research>();
			list7.AddRange(list5);
			list7.AddRange(list6);
			if (list6.Count > 0)
			{
				this.metaProgressImage1.SetActive(true);
			}
			else
			{
				this.metaProgressImage1.SetActive(false);
			}
			if (list5.Count > 0)
			{
				this.metaProgressImage2.SetActive(true);
			}
			else
			{
				this.metaProgressImage2.SetActive(false);
			}
			using (List<Overworld_BuildingInterface.Research>.Enumerator enumerator6 = list7.GetEnumerator())
			{
				while (enumerator6.MoveNext())
				{
					if (enumerator6.Current.isFavorite)
					{
						this.metaProgressImage3.SetActive(true);
						break;
					}
				}
				goto IL_11B4;
			}
		}
		if (this.metaProgressImage1)
		{
			GameObject gameObject2 = this.metaProgressImage1;
			if (gameObject2 != null)
			{
				gameObject2.SetActive(false);
			}
		}
		if (this.metaProgressImage2)
		{
			GameObject gameObject3 = this.metaProgressImage2;
			if (gameObject3 != null)
			{
				gameObject3.SetActive(false);
			}
		}
		if (this.metaProgressImage3)
		{
			GameObject gameObject4 = this.metaProgressImage3;
			if (gameObject4 != null)
			{
				gameObject4.SetActive(false);
			}
		}
		IL_11B4:
		if (!component)
		{
			foreach (Card.ItemStatusEffectCounter itemStatusEffectCounter2 in list4)
			{
				string itemStatusEffects = this.GetItemStatusEffects(itemStatusEffectCounter2);
				if (itemStatusEffects.Length > 1)
				{
					Card.DescriptorTotal descriptorTotal5 = new Card.DescriptorTotal();
					if (itemStatusEffectCounter2.type == Item2.ItemStatusEffect.Type.takingDamageDoesStartOfTurnEffects)
					{
						descriptorTotal5.trigger = new Item2.Trigger
						{
							trigger = Item2.Trigger.ActionTrigger.onTakeDamage,
							areas = new List<Item2.Area> { Item2.Area.self },
							types = new List<Item2.ItemType> { Item2.ItemType.Any }
						};
					}
					else
					{
						descriptorTotal5.trigger = null;
					}
					descriptorTotal5.texts = new List<string> { itemStatusEffects };
					list3.Add(descriptorTotal5);
				}
			}
		}
		foreach (Item2.LimitedUses limitedUses in item.usesLimits)
		{
			list3.Add(new Card.DescriptorTotal
			{
				trigger = null,
				texts = new List<string> { this.GetUseLimits(limitedUses).Trim() }
			});
		}
		foreach (Item2.MovementEffect movementEffect in item.movementEffects)
		{
			if (movementEffect.descriptionKey == null || !(movementEffect.descriptionKey == "hidden"))
			{
				bool flag3 = false;
				foreach (Card.DescriptorTotal descriptorTotal6 in list3)
				{
					if (descriptorTotal6.trigger != null && Item2.CheckIfSameTrigger(descriptorTotal6.trigger, movementEffect.trigger))
					{
						flag3 = true;
						descriptorTotal6.texts.Add(this.GetMovementEffects(movementEffect).Trim());
						break;
					}
				}
				if (!flag3)
				{
					list3.Add(new Card.DescriptorTotal
					{
						trigger = movementEffect.trigger,
						texts = new List<string> { this.GetMovementEffects(movementEffect).Trim() }
					});
				}
			}
		}
		foreach (Item2.CreateEffect createEffect in item.createEffects)
		{
			if (!(createEffect.descriptor == "") && createEffect.descriptor != null && !createEffect.descriptor.Contains("hidden") && !createEffect.descriptor.Contains("hide"))
			{
				bool flag4 = false;
				foreach (Card.DescriptorTotal descriptorTotal7 in list3)
				{
					if (descriptorTotal7.trigger != null && Item2.CheckIfSameTrigger(descriptorTotal7.trigger, createEffect.trigger))
					{
						flag4 = true;
						descriptorTotal7.texts.Add(this.GetCreateEffects(createEffect).Trim());
						break;
					}
				}
				if (!flag4)
				{
					Card.DescriptorTotal descriptorTotal8 = new Card.DescriptorTotal();
					foreach (GameObject gameObject5 in createEffect.itemsToCreate)
					{
						if (gameObject5 && gameObject5.GetComponent<ModdedItem>() != null)
						{
							descriptorTotal8.disableEmoji = true;
							break;
						}
					}
					descriptorTotal8.trigger = createEffect.trigger;
					descriptorTotal8.triggerOverrideKey = createEffect.trigger.triggerOverrideKey;
					if (createEffect.trigger.triggerOverrideKey.Length > 0)
					{
						descriptorTotal8.triggerOverrideKey = createEffect.trigger.triggerOverrideKey;
					}
					descriptorTotal8.texts = new List<string> { this.GetCreateEffects(createEffect).Trim() };
					list3.Add(descriptorTotal8);
				}
			}
		}
		item.GetEffectTotals();
		foreach (Item2.EffectTotal effectTotal in item.effectTotals)
		{
			if (effectTotal.effect.type != Item2.Effect.Type.ModifierMultiplier && effectTotal.effect.type != Item2.Effect.Type.ProvideSunshine && (effectTotal.effectPieces.Count != 2 || !(effectTotal.effectPieces[1].name == "hidden")) && !(effectTotal.effect.effectOverrideKey == "hidden"))
			{
				bool flag5 = false;
				foreach (Card.DescriptorTotal descriptorTotal9 in list3)
				{
					if (descriptorTotal9.trigger != null && Item2.CheckIfSameTrigger(descriptorTotal9.trigger, effectTotal.trigger))
					{
						flag5 = true;
						string text6 = this.GetEffectTotalDescription(effectTotal).Trim();
						descriptorTotal9.texts.Add(text6);
						break;
					}
				}
				if (!flag5)
				{
					list3.Add(new Card.DescriptorTotal
					{
						trigger = effectTotal.trigger,
						triggerOverrideKey = effectTotal.trigger.triggerOverrideKey,
						texts = new List<string> { this.GetEffectTotalDescription(effectTotal).Trim() }
					});
				}
			}
		}
		foreach (Item2.Modifier modifier in item.modifiers)
		{
			if (!(modifier.descriptionKey == "hidden") && (!modifier.IsSelf() || !item.itemType.Contains(Item2.ItemType.Pet)))
			{
				bool flag6 = false;
				string text7 = modifier.triggerKey;
				if (modifier.triggerKey.Length > 1)
				{
					text7 = modifier.triggerKey;
				}
				if (modifier.triggerKey.Length <= 0)
				{
					foreach (Card.DescriptorTotal descriptorTotal10 in list3)
					{
						if ((descriptorTotal10.trigger != null && Item2.CheckIfSameTrigger(descriptorTotal10.trigger, modifier.trigger) && descriptorTotal10.triggerOverrideKey.Length < 1 && text7.Length < 1) || (descriptorTotal10.triggerOverrideKey.Length >= 1 && text7.Length >= 1 && descriptorTotal10.triggerOverrideKey == text7))
						{
							flag6 = true;
							descriptorTotal10.texts.Add(this.GetModifierDescription(modifier, true).Trim());
							break;
						}
					}
				}
				if (!flag6)
				{
					Card.DescriptorTotal descriptorTotal11 = new Card.DescriptorTotal();
					descriptorTotal11.trigger = modifier.trigger;
					descriptorTotal11.triggerOverrideKey = modifier.trigger.triggerOverrideKey;
					if (text7.Length > 0)
					{
						descriptorTotal11.triggerOverrideKey = text7;
					}
					if (modifier.triggerDisplayValue != -999f)
					{
						descriptorTotal11.triggerOverrideValue = modifier.triggerDisplayValue;
					}
					descriptorTotal11.texts = new List<string> { this.GetModifierDescription(modifier, true).Trim() };
					descriptorTotal11.stackable = modifier.stackable;
					list3.Add(descriptorTotal11);
				}
			}
		}
		foreach (Item2.AddModifier addModifier in item.addModifiers)
		{
			if (addModifier.descriptionKey == null || !(addModifier.descriptionKey == "hidden"))
			{
				bool flag7 = false;
				foreach (Card.DescriptorTotal descriptorTotal12 in list3)
				{
					if (descriptorTotal12.trigger != null && Item2.CheckIfSameTrigger(descriptorTotal12.trigger, addModifier.trigger))
					{
						flag7 = true;
						descriptorTotal12.texts.Add(this.GetAddModifierDescription(addModifier).Trim());
						break;
					}
				}
				if (!flag7)
				{
					list3.Add(new Card.DescriptorTotal
					{
						trigger = addModifier.trigger,
						triggerOverrideKey = addModifier.trigger.triggerOverrideKey,
						texts = new List<string> { this.GetAddModifierDescription(addModifier).Trim() },
						stackable = addModifier.modifier.stackable
					});
				}
			}
		}
		foreach (Item2.EnergyEffect energyEffect in item.energyEffects)
		{
			if (energyEffect.descriptionKey != null && energyEffect.descriptionKey.Length != 0 && !(energyEffect.descriptionKey == "hidden"))
			{
				string text8 = LangaugeManager.main.GetTextByKey(energyEffect.descriptionKey).Trim();
				text8 = text8.Replace("/x", energyEffect.value.ToString() ?? "");
				bool flag8 = false;
				foreach (Card.DescriptorTotal descriptorTotal13 in list3)
				{
					if (descriptorTotal13.trigger != null && Item2.CheckIfSameTrigger(descriptorTotal13.trigger, energyEffect.trigger))
					{
						flag8 = true;
						descriptorTotal13.texts.Add(text8);
						break;
					}
				}
				if (!flag8)
				{
					list3.Add(new Card.DescriptorTotal
					{
						trigger = energyEffect.trigger,
						triggerOverrideKey = energyEffect.trigger.triggerOverrideKey,
						texts = new List<string> { text8 }
					});
				}
			}
		}
		foreach (SpecialItem specialItem in item.GetComponentsInChildren<SpecialItem>())
		{
			if (specialItem.description.Length > 0)
			{
				string text9 = LangaugeManager.main.GetTextByKey(specialItem.GetDescription()).Replace("/x", specialItem.value.ToString() ?? "").Trim();
				bool flag9 = false;
				foreach (Card.DescriptorTotal descriptorTotal14 in list3)
				{
					if (descriptorTotal14.trigger != null && Item2.CheckIfSameTrigger(descriptorTotal14.trigger, specialItem.trigger))
					{
						flag9 = true;
						descriptorTotal14.texts.Add(text9);
						break;
					}
				}
				if (!flag9)
				{
					list3.Add(new Card.DescriptorTotal
					{
						trigger = specialItem.trigger,
						triggerOverrideKey = specialItem.trigger.triggerOverrideKey,
						texts = new List<string> { text9 }
					});
				}
			}
		}
		CustomDescriptions customDescriptions = item.GetComponent<CustomDescriptions>();
		if (!customDescriptions)
		{
			customDescriptions = item.GetComponentInParent<CustomDescriptions>();
		}
		if (customDescriptions)
		{
			foreach (CustomDescriptions.Description description in customDescriptions.descriptions)
			{
				if ((!description.requireCertainRotation || item.transform.eulerAngles.z == (float)description.zRotation) && (!Player.main || description.validForCharacters == null || description.validForCharacters.Count <= 0 || description.validForCharacters.Contains(Player.main.characterName)))
				{
					bool flag10 = false;
					string textByKey2 = LangaugeManager.main.GetTextByKey(description.description);
					string text10 = "/x";
					int i = description.GetValue();
					string text11 = textByKey2.Replace(text10, i.ToString() ?? "").Trim();
					text11 = text11.Replace("[", "<color=#87ffe3>");
					text11 = text11.Replace("]", "</color>");
					foreach (Card.DescriptorTotal descriptorTotal15 in list3)
					{
						if (descriptorTotal15.trigger != null && Item2.CheckIfSameTrigger(descriptorTotal15.trigger, description.trigger))
						{
							flag10 = true;
							descriptorTotal15.texts.Add(text11);
							break;
						}
					}
					if (!flag10)
					{
						Card.DescriptorTotal descriptorTotal16 = new Card.DescriptorTotal();
						descriptorTotal16.trigger = description.trigger;
						if (description.triggerName.Length > 1)
						{
							descriptorTotal16.triggerOverrideKey = description.triggerName;
						}
						descriptorTotal16.texts = new List<string> { text11 };
						list3.Add(descriptorTotal16);
					}
				}
			}
		}
		foreach (string text12 in item.descriptions)
		{
			if (LangaugeManager.main.KeyExists(text12))
			{
				string text13 = LangaugeManager.main.GetTextByKey(text12) ?? "";
				list3.Add(new Card.DescriptorTotal
				{
					trigger = null,
					texts = new List<string> { text13 },
					flavor = true
				});
			}
		}
		foreach (Overworld_ResourceManager.Resource resource in item.resourcesToGet)
		{
			string text14 = "";
			switch (resource.type)
			{
			case Overworld_ResourceManager.Resource.Type.Food:
				text14 += LangaugeManager.main.GetTextByKey("extraResFood");
				break;
			case Overworld_ResourceManager.Resource.Type.BuildingMaterial:
				text14 += LangaugeManager.main.GetTextByKey("extraResBM");
				break;
			case Overworld_ResourceManager.Resource.Type.Treasure:
				text14 += LangaugeManager.main.GetTextByKey("extraResTreasure");
				break;
			}
			text14 = text14.Replace("/x", resource.amount.ToString() ?? "");
			list3.Insert(0, new Card.DescriptorTotal
			{
				trigger = null,
				texts = new List<string> { text14 },
				flavor = false
			});
		}
		string text15 = item.displayName + "1";
		if (LangaugeManager.main.KeyExists(text15))
		{
			string text16 = LangaugeManager.main.GetTextByKey(text15) ?? "";
			list3.Add(new Card.DescriptorTotal
			{
				trigger = null,
				texts = new List<string> { text16 },
				flavor = false
			});
		}
		text15 = item.displayName + "2";
		if (LangaugeManager.main.KeyExists(text15))
		{
			string text17 = "/" + LangaugeManager.main.GetTextByKey(text15);
			list3.Add(new Card.DescriptorTotal
			{
				trigger = null,
				texts = new List<string> { text17 },
				flavor = true
			});
		}
		CustomDescriptions componentInParent = this.parent.GetComponentInParent<CustomDescriptions>();
		if (componentInParent != null && componentInParent.flavorTextKey != "")
		{
			list3.Add(new Card.DescriptorTotal
			{
				trigger = null,
				texts = new List<string> { "/" + LangaugeManager.main.GetTextByKey(componentInParent.flavorTextKey) },
				flavor = true
			});
		}
		ModdedItem component3 = item.gameObject.GetComponent<ModdedItem>();
		if (component3 != null)
		{
			list3.Add(new Card.DescriptorTotal
			{
				trigger = null,
				texts = new List<string> { "<size=80%>Modpack: <br><color=#ff80ffff>" + LangaugeManager.main.GetTextByKey(component3.fromModpack.displayName) + "</color></size>" },
				flavor = false
			});
		}
		PetItem2 component4 = p.GetComponent<PetItem2>();
		if (component4)
		{
			list3.Add(new Card.DescriptorTotal
			{
				trigger = null,
				texts = new List<string> { LangaugeManager.main.GetTextByKey("enemyHP").Replace("/x", component4.health.ToString() ?? "").Replace("/y", component4.maxHealth.ToString() ?? "") }
			});
			if (component4.APperTurn != 0)
			{
				list3.Add(new Card.DescriptorTotal
				{
					trigger = null,
					texts = new List<string> { LangaugeManager.main.GetTextByKey("petEnergy").Replace("/x", component4.APperTurn.ToString() ?? "") }
				});
			}
			this.ShowCosts(component4.summoningCosts, this.apSummonCostText, this.manaSummonText, this.goldSummonCostText);
			if (component4.combatPet)
			{
				this.summonCostsParent.transform.localScale = Vector3.one * 0.75f;
				Image[] array2 = this.summonCostsParent.GetComponentsInChildren<Image>();
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i].color = new Color(0.4f, 0.4f, 0.4f);
				}
				this.standardCostsParent.transform.localScale = Vector3.one;
				array2 = this.standardCostsParent.GetComponentsInChildren<Image>();
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i].color = Color.white;
				}
			}
			else
			{
				this.summonCostsParent.transform.localScale = Vector3.one * 1f;
				Image[] array2 = this.summonCostsParent.GetComponentsInChildren<Image>();
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i].color = Color.white;
				}
				this.standardCostsParent.transform.localScale = Vector3.one * 0.75f;
				array2 = this.standardCostsParent.GetComponentsInChildren<Image>();
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i].color = new Color(0.4f, 0.4f, 0.4f);
				}
			}
		}
		this.SetPropertyTexts(list3, TextAlignmentOptions.MidlineLeft);
		return list3;
	}

	// Token: 0x060003CB RID: 971 RVA: 0x00022D80 File Offset: 0x00020F80
	private string GetItemStatusEffects(Card.ItemStatusEffectCounter itemStatusEffectCounter)
	{
		Item2 component = this.parent.GetComponent<Item2>();
		string text = "";
		if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.disabled && component && component.itemMovement && component.itemMovement.inPouch)
		{
			return "";
		}
		if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.disabled)
		{
			text = LangaugeManager.main.GetTextByKey("is1");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.projectile)
		{
			text = LangaugeManager.main.GetTextByKey("is2");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.locked)
		{
			text = LangaugeManager.main.GetTextByKey("is3");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.enflamed)
		{
			text = LangaugeManager.main.GetTextByKey("is4");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.conductive)
		{
			text = LangaugeManager.main.GetTextByKey("is5");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.heavy)
		{
			text = LangaugeManager.main.GetTextByKey("is6");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.buoyant)
		{
			text = LangaugeManager.main.GetTextByKey("is7");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.canBePlayedOverOtherItems && component && !component.itemType.Contains(Item2.ItemType.Hazard) && !component.itemType.Contains(Item2.ItemType.Blessing))
		{
			text = LangaugeManager.main.GetTextByKey("is8");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.ductTape)
		{
			text = LangaugeManager.main.GetTextByKey("is9");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.canBeUsedByCR8Directly && Player.main && Player.main.characterName == Character.CharacterName.CR8)
		{
			text = LangaugeManager.main.GetTextByKey("is10");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.piercing)
		{
			text = LangaugeManager.main.GetTextByKey("is11");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.allowsItemsInIllusorySpaces)
		{
			text = LangaugeManager.main.GetTextByKey("is12");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.CR8ChargesReverseWhenOffGrid)
		{
			text = LangaugeManager.main.GetTextByKey("is13");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.isActivated)
		{
			text = "<color=#2FFF3E>" + LangaugeManager.main.GetTextByKey("is14") + "</color>";
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.canBeMovedInCombat || itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.canBeMovedInCombatButReturnsToOriginalPosition)
		{
			Item2.Area area = Item2.Area.board;
			if (component)
			{
				area = component.moveArea;
			}
			if (area != Item2.Area.adjacent)
			{
				if (area == Item2.Area.diagonal)
				{
					text = LangaugeManager.main.GetTextByKey("is15b");
				}
				else
				{
					text = LangaugeManager.main.GetTextByKey("is15") ?? "";
				}
			}
			else
			{
				text = LangaugeManager.main.GetTextByKey("is15c");
			}
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.allowsFreeMove)
		{
			text = LangaugeManager.main.GetTextByKey("is16");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.canOnlyBeHeldByPochette)
		{
			text = LangaugeManager.main.GetTextByKey("is17");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.petsAreSummonedBehindPochette)
		{
			text = LangaugeManager.main.GetTextByKey("is18");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.effigy)
		{
			text = LangaugeManager.main.GetTextByKey("is19");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.Natural)
		{
			text = LangaugeManager.main.GetTextByKey("is21");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.Cleansed)
		{
			text = LangaugeManager.main.GetTextByKey("is22");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.countAsEmpty)
		{
			text = LangaugeManager.main.GetTextByKey("is23");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.ghostly)
		{
			text = LangaugeManager.main.GetTextByKey("is24");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.invincible)
		{
			text = LangaugeManager.main.GetTextByKey("is25");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.cannotBeRotated)
		{
			text = LangaugeManager.main.GetTextByKey("is26");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.energyCarriesBetweenTurns)
		{
			text = LangaugeManager.main.GetTextByKey("is27");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.cannotPlaceItemsOfSameTypeAdjacent)
		{
			text = LangaugeManager.main.GetTextByKey("is28");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.takingDamageDoesStartOfTurnEffects)
		{
			text = LangaugeManager.main.GetTextByKey("is29");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.cannotGainGold)
		{
			text = LangaugeManager.main.GetTextByKey("is30");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.pauperRun)
		{
			text = LangaugeManager.main.GetTextByKey("is31");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.blockIsNotRemoved)
		{
			text = LangaugeManager.main.GetTextByKey("is32");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.scalingEnergy)
		{
			text = LangaugeManager.main.GetTextByKey("rtd24").Replace("/x", "1");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.cannotBePlacedAtEdge)
		{
			text = LangaugeManager.main.GetTextByKey("is34");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.mustBePlacedAtEdge)
		{
			text = LangaugeManager.main.GetTextByKey("is33");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.unique)
		{
			text = LangaugeManager.main.GetTextByKey("is35");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.vampiric)
		{
			text = LangaugeManager.main.GetTextByKey("is36");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.effectsGravity)
		{
			text = LangaugeManager.main.GetTextByKey("is37");
		}
		else if (itemStatusEffectCounter.type == Item2.ItemStatusEffect.Type.temporary)
		{
			text = LangaugeManager.main.GetTextByKey("is38");
		}
		if (text == "")
		{
			return "";
		}
		foreach (Item2.ItemStatusEffect itemStatusEffect in itemStatusEffectCounter.effects)
		{
			if (itemStatusEffect.length == Item2.ItemStatusEffect.Length.turns || itemStatusEffect.length == Item2.ItemStatusEffect.Length.combats)
			{
				string textByKey = LangaugeManager.main.GetTextByKey(itemStatusEffect.source);
				string textByKey2 = LangaugeManager.main.GetTextByKey(itemStatusEffect.length.ToString());
				if (textByKey != null && textByKey.Length > 1 && textByKey2 != null)
				{
					text = string.Concat(new string[]
					{
						text,
						"<br><size=90%>  ",
						textByKey,
						": ",
						itemStatusEffect.num.ToString(),
						" ",
						textByKey2,
						"</size>"
					});
				}
				else if (textByKey2 != null)
				{
					text = string.Concat(new string[]
					{
						text,
						itemStatusEffect.num.ToString(),
						" ",
						textByKey2,
						"</size>"
					});
				}
			}
			else if (itemStatusEffect.source != null && itemStatusEffect.source.Length > 1)
			{
				text = text + "<br><size=90%>  " + LangaugeManager.main.GetTextByKey(itemStatusEffect.source) + "</size>";
			}
		}
		return text;
	}

	// Token: 0x060003CC RID: 972 RVA: 0x00023484 File Offset: 0x00021684
	private string GetUseLimits(Item2.LimitedUses useLimit)
	{
		string text = "";
		if (useLimit.type == Item2.LimitedUses.Type.total)
		{
			if (useLimit.currentValue == 1f || (useLimit.currentValue == -999f && useLimit.value == 1f))
			{
				text += LangaugeManager.main.GetTextByKey("ul1");
			}
			else
			{
				text += LangaugeManager.main.GetTextByKey("ul2");
			}
		}
		if (useLimit.type == Item2.LimitedUses.Type.perTurn)
		{
			if (useLimit.currentValue == useLimit.value || useLimit.currentValue == -999f)
			{
				if (useLimit.currentValue == 1f || (useLimit.currentValue == -999f && useLimit.value == 1f))
				{
					text += LangaugeManager.main.GetTextByKey("ul3");
				}
				else
				{
					text += LangaugeManager.main.GetTextByKey("ul4");
				}
			}
			else if (useLimit.currentValue == 1f || (useLimit.currentValue == -999f && useLimit.value == 1f))
			{
				text += LangaugeManager.main.GetTextByKey("ul5");
			}
			else
			{
				text += LangaugeManager.main.GetTextByKey("ul6");
			}
		}
		if (useLimit.type == Item2.LimitedUses.Type.perCombat)
		{
			if (useLimit.currentValue == useLimit.value || useLimit.currentValue == -999f)
			{
				if (useLimit.currentValue == 1f || (useLimit.currentValue == -999f && useLimit.value == 1f))
				{
					text += LangaugeManager.main.GetTextByKey("ul7");
				}
				else
				{
					text += LangaugeManager.main.GetTextByKey("ul8");
				}
			}
			else if (useLimit.currentValue == 1f || (useLimit.currentValue == -999f && useLimit.value == 1f))
			{
				text += LangaugeManager.main.GetTextByKey("ul9");
			}
			else
			{
				text += LangaugeManager.main.GetTextByKey("ul10");
			}
		}
		if (useLimit.currentValue != -999f)
		{
			text = text.Replace("/x", useLimit.currentValue.ToString() ?? "");
		}
		else
		{
			text = text.Replace("/x", useLimit.value.ToString() ?? "");
		}
		if (useLimit.currentValue != -999f && useLimit.currentValue != useLimit.value)
		{
			text += "<size=90%>";
			if (useLimit.value == 1f)
			{
				text = string.Concat(new string[]
				{
					text,
					"<br>  ",
					LangaugeManager.main.GetTextByKey("base"),
					": ",
					useLimit.value.ToString(),
					" ",
					LangaugeManager.main.GetTextByKey("ul")
				});
			}
			else
			{
				text = string.Concat(new string[]
				{
					text,
					"<br>  ",
					LangaugeManager.main.GetTextByKey("base"),
					": ",
					useLimit.value.ToString(),
					" ",
					LangaugeManager.main.GetTextByKey("ulp")
				});
			}
			text += "</size>";
		}
		return text;
	}

	// Token: 0x060003CD RID: 973 RVA: 0x000237E8 File Offset: 0x000219E8
	public string GetMovementEffects(Item2.MovementEffect movementEffect)
	{
		string text = "";
		float num = movementEffect.rotationAmount - Mathf.Floor(movementEffect.rotationAmount / 360f) * 360f;
		if (movementEffect.descriptionKey.Length > 1)
		{
			text = LangaugeManager.main.GetTextByKey(movementEffect.descriptionKey);
		}
		else if (movementEffect.movementVariety == Item2.MovementEffect.MovementVariety.toRandomSpace)
		{
			Vector2 vector = movementEffect.movementAmount;
			if (movementEffect.movementType == Item2.MovementEffect.MovementType.local)
			{
				vector = this.parent.transform.rotation * movementEffect.movementAmount;
			}
			if (vector == Vector2.zero)
			{
				text = LangaugeManager.main.GetTextByKey("meRan");
			}
			else if (vector == new Vector2(1f, 0f))
			{
				text = LangaugeManager.main.GetTextByKey("meRanR");
			}
			else if (vector == new Vector2(-1f, 0f))
			{
				text = LangaugeManager.main.GetTextByKey("meRanL");
			}
			else if (vector == new Vector2(0f, 1f))
			{
				text = LangaugeManager.main.GetTextByKey("meRanU");
			}
			else if (vector == new Vector2(0f, -1f))
			{
				text = LangaugeManager.main.GetTextByKey("meRanD");
			}
		}
		else if (num == 270f || num == -90f)
		{
			text = LangaugeManager.main.GetTextByKey("me1");
		}
		else if (num == 180f || num == -180f)
		{
			text = LangaugeManager.main.GetTextByKey("me2");
		}
		else if (num == 90f || num == -270f)
		{
			text = LangaugeManager.main.GetTextByKey("me3");
		}
		else if (movementEffect.movementAmount == new Vector2(0f, 1f) && movementEffect.movementType == Item2.MovementEffect.MovementType.local && movementEffect.movementLength == Item2.MovementEffect.MovementLength.untilHit)
		{
			text = LangaugeManager.main.GetTextByKey("me4");
		}
		else if (movementEffect.movementAmount == new Vector2(0f, 1f) && movementEffect.movementType == Item2.MovementEffect.MovementType.local)
		{
			text = LangaugeManager.main.GetTextByKey("me5");
		}
		else if (movementEffect.movementAmount == new Vector2(0f, 1f) && movementEffect.movementType == Item2.MovementEffect.MovementType.global)
		{
			text = LangaugeManager.main.GetTextByKey("me6");
		}
		if (movementEffect.movementType == Item2.MovementEffect.MovementType.local)
		{
			text = text.Replace("[", "<color=#87ffe3>");
			text = text.Replace("]", "</color>");
		}
		return text;
	}

	// Token: 0x060003CE RID: 974 RVA: 0x00023A9C File Offset: 0x00021C9C
	private string GetCreateEffects(Item2.CreateEffect createEffect)
	{
		string text = LangaugeManager.main.GetTextByKey(createEffect.descriptor);
		bool flag = false;
		List<Item2.Area> list = new List<Item2.Area>();
		foreach (Item2.Area area in createEffect.areasToCreateTheItem)
		{
			Item2.Area area3;
			Item2.Area area2 = (area3 = area);
			if (this.parent && this.parent.transform)
			{
				area3 = Item2.TranslateArea(area3, this.parent.transform);
			}
			if (area2 != area3)
			{
				flag = true;
			}
			list.Add(area3);
		}
		if (flag)
		{
			text = text.Replace("[rotation]", ModTextGen.GetModifierRotation(list).Trim());
			text = text.Replace("[", "<color=#87ffe3>");
			text = text.Replace("]", "</color>");
		}
		if (createEffect.itemsToCreate.Count > 1)
		{
			text = text.Replace("/x", createEffect.itemsToCreate.Count.ToString() ?? "");
		}
		else
		{
			text = text.Replace("/x", createEffect.numberToCreate.ToString() ?? "");
		}
		return text;
	}

	// Token: 0x060003CF RID: 975 RVA: 0x00023BDC File Offset: 0x00021DDC
	public string GetEffectTotalDescription(Item2.EffectTotal effectTotal)
	{
		string text = "";
		Item2.Effect effect = effectTotal.effect;
		bool flag = true;
		bool flag2 = false;
		if (effect.effectOverrideKey.Length > 0)
		{
			text = LangaugeManager.main.GetTextByKey(effect.effectOverrideKey);
		}
		else if (effect.target == Item2.Effect.Target.enemy && effect.type == Item2.Effect.Type.Damage)
		{
			text += LangaugeManager.main.GetTextByKey("e1");
		}
		else if (effect.target == Item2.Effect.Target.player && effect.type == Item2.Effect.Type.Damage)
		{
			text += LangaugeManager.main.GetTextByKey("e2");
		}
		else if (effect.target == Item2.Effect.Target.allEnemies && effect.type == Item2.Effect.Type.Damage)
		{
			text += LangaugeManager.main.GetTextByKey("e3");
		}
		else if (effect.target == Item2.Effect.Target.everyone && effect.type == Item2.Effect.Type.Damage)
		{
			text += LangaugeManager.main.GetTextByKey("e3b");
		}
		else if (effect.target == Item2.Effect.Target.everyoneButPochette && effect.type == Item2.Effect.Type.Damage)
		{
			text += LangaugeManager.main.GetTextByKey("e3every");
		}
		else if (effect.target == Item2.Effect.Target.enemy && effect.type == Item2.Effect.Type.Vampire)
		{
			text += LangaugeManager.main.GetTextByKey("e4");
		}
		else if (effect.target == Item2.Effect.Target.allEnemies && effect.type == Item2.Effect.Type.Vampire)
		{
			text += LangaugeManager.main.GetTextByKey("e5");
		}
		else if (effect.target == Item2.Effect.Target.player && effect.type == Item2.Effect.Type.Block && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e6");
		}
		else if (effect.target == Item2.Effect.Target.player && effect.type == Item2.Effect.Type.Block && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e7");
		}
		else if (effect.target == Item2.Effect.Target.player && effect.type == Item2.Effect.Type.Block && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e8");
		}
		else if (effect.target == Item2.Effect.Target.truePlayer && effect.type == Item2.Effect.Type.Block && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e8Po");
		}
		else if (effect.target == Item2.Effect.Target.enemy && effect.type == Item2.Effect.Type.Block && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e9");
		}
		else if (effect.target == Item2.Effect.Target.allEnemies && effect.type == Item2.Effect.Type.Block && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e10");
		}
		else if (effect.target == Item2.Effect.Target.player && effect.type == Item2.Effect.Type.Block && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e11");
		}
		else if (effect.target == Item2.Effect.Target.enemy && effect.type == Item2.Effect.Type.Block && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e12");
		}
		else if (effect.target == Item2.Effect.Target.allEnemies && effect.type == Item2.Effect.Type.Block && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e13");
		}
		else if (effect.target == Item2.Effect.Target.statusFromItem && effect.type == Item2.Effect.Type.HP && effect.value > 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e14pet");
		}
		else if (effect.target == Item2.Effect.Target.player && effect.type == Item2.Effect.Type.HP && effect.value > 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e14");
		}
		else if (effect.target == Item2.Effect.Target.enemy && effect.type == Item2.Effect.Type.HP && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e15");
		}
		else if (effect.target == Item2.Effect.Target.allEnemies && effect.type == Item2.Effect.Type.HP && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e16");
		}
		else if (effect.target == Item2.Effect.Target.player && effect.type == Item2.Effect.Type.HP && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e17");
		}
		else if (effect.target == Item2.Effect.Target.enemy && effect.type == Item2.Effect.Type.HP && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e18");
		}
		else if (effect.target == Item2.Effect.Target.allEnemies && effect.type == Item2.Effect.Type.HP && effect.value > 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e19");
		}
		else if (effect.type == Item2.Effect.Type.AP && effect.value >= 0f && effect.target == Item2.Effect.Target.adjacentFriendlies)
		{
			text += LangaugeManager.main.GetTextByKey("e132");
		}
		else if (effect.type == Item2.Effect.Type.AP && effect.target == Item2.Effect.Target.truePlayer)
		{
			text += LangaugeManager.main.GetTextByKey("e20po");
			flag2 = true;
		}
		else if (effect.type == Item2.Effect.Type.AP && effect.target == Item2.Effect.Target.statusFromItem)
		{
			text += LangaugeManager.main.GetTextByKey("e20pet");
			flag2 = true;
		}
		else if ((effect.type == Item2.Effect.Type.AP || effect.type == Item2.Effect.Type.NextTurnAP) && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e20");
		}
		else if (effect.type == Item2.Effect.Type.AP && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e20d");
		}
		else if (effect.type == Item2.Effect.Type.MaxHP)
		{
			if (effect.value >= 0f)
			{
				text += LangaugeManager.main.GetTextByKey("e21");
			}
			else
			{
				text += LangaugeManager.main.GetTextByKey("e21b");
			}
		}
		else if (effect.type == Item2.Effect.Type.Mana)
		{
			if (effect.value >= 0f)
			{
				text += LangaugeManager.main.GetTextByKey("e22");
			}
			else if (effect.value <= -999f)
			{
				text += LangaugeManager.main.GetTextByKey("e22ball");
			}
			else
			{
				text += LangaugeManager.main.GetTextByKey("e22b");
			}
		}
		else if (effect.type == Item2.Effect.Type.ManaToSelfOnly)
		{
			if (effect.value >= 99f)
			{
				text += LangaugeManager.main.GetTextByKey("m219");
			}
			else if (effect.value <= -99f)
			{
				text += LangaugeManager.main.GetTextByKey("m220");
			}
		}
		else if (effect.type == Item2.Effect.Type.Luck && effect.value <= 3f)
		{
			text += LangaugeManager.main.GetTextByKey("e23");
		}
		else if (effect.type == Item2.Effect.Type.Luck && effect.value > 3f)
		{
			text += LangaugeManager.main.GetTextByKey("e24");
		}
		else if (effect.type == Item2.Effect.Type.AllStatusEffects && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e25");
		}
		else if (effect.type == Item2.Effect.Type.AllStatusEffects && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e26");
		}
		else if (effect.type == Item2.Effect.Type.AllStatusEffects && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e27");
		}
		else if (effect.type == Item2.Effect.Type.AllStatusEffects && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e28");
		}
		else if (effect.type == Item2.Effect.Type.AllStatusEffects && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e29");
		}
		else if (effect.type == Item2.Effect.Type.AllStatusEffects && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e29b");
		}
		else if (effect.type == Item2.Effect.Type.AllStatusEffects && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e29c");
		}
		else if (effect.type == Item2.Effect.Type.AllStatusEffects && effect.target == Item2.Effect.Target.everyone && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e29e");
		}
		else if (effect.type == Item2.Effect.Type.AllStatusEffects && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e30");
		}
		else if (effect.type == Item2.Effect.Type.AllStatusEffects && effect.target == Item2.Effect.Target.statusFromItem && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e30pet");
		}
		else if (effect.type == Item2.Effect.Type.Poison && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e31");
		}
		else if (effect.type == Item2.Effect.Type.Zombie && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e38d");
		}
		else if (effect.type == Item2.Effect.Type.Regen && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e32");
		}
		else if (effect.type == Item2.Effect.Type.Spikes && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e33");
		}
		else if (effect.type == Item2.Effect.Type.Haste && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e34");
		}
		else if (effect.type == Item2.Effect.Type.Haste && effect.target == Item2.Effect.Target.truePlayer && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e34po");
		}
		else if (effect.type == Item2.Effect.Type.Slow && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e35");
		}
		else if (effect.type == Item2.Effect.Type.Rage && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e36");
		}
		else if (effect.type == Item2.Effect.Type.Weak && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e37");
		}
		else if (effect.type == Item2.Effect.Type.Dodge && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e38");
		}
		else if (effect.type == Item2.Effect.Type.Burn && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e38b");
		}
		else if (effect.type == Item2.Effect.Type.Freeze && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e38c");
		}
		else if (effect.type == Item2.Effect.Type.Poison && (effect.target == Item2.Effect.Target.enemy || effect.target == Item2.Effect.Target.reactiveEnemy) && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e39");
		}
		else if (effect.type == Item2.Effect.Type.Regen && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e40");
		}
		else if (effect.type == Item2.Effect.Type.Spikes && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e41");
		}
		else if (effect.type == Item2.Effect.Type.Haste && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e42");
		}
		else if (effect.type == Item2.Effect.Type.Slow && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e43");
		}
		else if (effect.type == Item2.Effect.Type.Rage && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e44");
		}
		else if (effect.type == Item2.Effect.Type.Weak && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e45");
		}
		else if (effect.type == Item2.Effect.Type.Dodge && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e46");
		}
		else if (effect.type == Item2.Effect.Type.Burn && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e46b");
		}
		else if (effect.type == Item2.Effect.Type.Poison && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e47");
		}
		else if (effect.type == Item2.Effect.Type.Regen && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e48");
		}
		else if (effect.type == Item2.Effect.Type.Spikes && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e49");
		}
		else if (effect.type == Item2.Effect.Type.Haste && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e50");
		}
		else if (effect.type == Item2.Effect.Type.Slow && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e51");
		}
		else if (effect.type == Item2.Effect.Type.Rage && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e52");
		}
		else if (effect.type == Item2.Effect.Type.Weak && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e53");
		}
		else if (effect.type == Item2.Effect.Type.Dodge && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e54");
		}
		else if (effect.type == Item2.Effect.Type.Burn && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e54b");
		}
		else if (effect.type == Item2.Effect.Type.Poison && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e55");
		}
		else if (effect.type == Item2.Effect.Type.Charm && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("e54ch1");
		}
		else if (effect.type == Item2.Effect.Type.Charm && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("e54ch2");
		}
		else if (effect.type == Item2.Effect.Type.Sleep && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("e54sl1");
		}
		else if (effect.type == Item2.Effect.Type.Sleep && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("e54sl2");
		}
		else if (effect.type == Item2.Effect.Type.Freeze && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("e54fr1");
		}
		else if (effect.type == Item2.Effect.Type.Freeze && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("e54fr2");
		}
		else if (effect.type == Item2.Effect.Type.Regen && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e56");
		}
		else if (effect.type == Item2.Effect.Type.Spikes && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e57");
		}
		else if (effect.type == Item2.Effect.Type.Haste && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e58");
		}
		else if (effect.type == Item2.Effect.Type.Slow && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e59");
		}
		else if (effect.type == Item2.Effect.Type.Rage && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e60");
		}
		else if (effect.type == Item2.Effect.Type.Weak && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e61");
		}
		else if (effect.type == Item2.Effect.Type.Dodge && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e62");
		}
		else if (effect.type == Item2.Effect.Type.Poison && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e63");
		}
		else if (effect.type == Item2.Effect.Type.Regen && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e64");
		}
		else if (effect.type == Item2.Effect.Type.Spikes && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e65");
		}
		else if (effect.type == Item2.Effect.Type.Haste && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e66");
		}
		else if (effect.type == Item2.Effect.Type.Slow && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e67");
		}
		else if (effect.type == Item2.Effect.Type.Rage && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e68");
		}
		else if (effect.type == Item2.Effect.Type.Weak && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e69");
		}
		else if (effect.type == Item2.Effect.Type.Dodge && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e70");
		}
		else if (effect.type == Item2.Effect.Type.Poison && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e71");
		}
		else if (effect.type == Item2.Effect.Type.Regen && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e72");
		}
		else if (effect.type == Item2.Effect.Type.Spikes && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e73");
		}
		else if (effect.type == Item2.Effect.Type.Haste && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e74");
		}
		else if (effect.type == Item2.Effect.Type.Slow && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e75");
		}
		else if (effect.type == Item2.Effect.Type.Rage && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e76");
		}
		else if (effect.type == Item2.Effect.Type.Weak && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e77");
		}
		else if (effect.type == Item2.Effect.Type.Dodge && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e78");
		}
		else if (effect.type == Item2.Effect.Type.Poison && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e79");
		}
		else if (effect.type == Item2.Effect.Type.Regen && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e80");
		}
		else if (effect.type == Item2.Effect.Type.Spikes && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e81");
		}
		else if (effect.type == Item2.Effect.Type.Haste && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e82");
		}
		else if (effect.type == Item2.Effect.Type.Slow && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e83");
		}
		else if (effect.type == Item2.Effect.Type.Rage && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e84");
		}
		else if (effect.type == Item2.Effect.Type.Weak && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e85");
		}
		else if (effect.type == Item2.Effect.Type.Dodge && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e86");
		}
		else if (effect.type == Item2.Effect.Type.Poison && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e87");
		}
		else if (effect.type == Item2.Effect.Type.Regen && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e88");
		}
		else if (effect.type == Item2.Effect.Type.Spikes && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e89");
		}
		else if (effect.type == Item2.Effect.Type.Haste && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e90");
		}
		else if (effect.type == Item2.Effect.Type.Slow && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e91");
		}
		else if (effect.type == Item2.Effect.Type.Rage && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e92");
		}
		else if (effect.type == Item2.Effect.Type.Weak && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e93");
		}
		else if (effect.type == Item2.Effect.Type.Dodge && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e94");
		}
		else if (effect.type == Item2.Effect.Type.Poison && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e95");
		}
		else if (effect.type == Item2.Effect.Type.Regen && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e96");
		}
		else if (effect.type == Item2.Effect.Type.Spikes && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e97");
		}
		else if (effect.type == Item2.Effect.Type.Haste && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e98");
		}
		else if (effect.type == Item2.Effect.Type.Slow && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e99");
		}
		else if (effect.type == Item2.Effect.Type.Rage && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e100");
		}
		else if (effect.type == Item2.Effect.Type.Weak && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e101");
		}
		else if (effect.type == Item2.Effect.Type.Dodge && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e102");
		}
		else if (effect.type == Item2.Effect.Type.Poison && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e103");
		}
		else if (effect.type == Item2.Effect.Type.Regen && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e104");
		}
		else if (effect.type == Item2.Effect.Type.Spikes && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e105");
		}
		else if (effect.type == Item2.Effect.Type.Haste && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e106");
		}
		else if (effect.type == Item2.Effect.Type.Slow && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e107");
		}
		else if (effect.type == Item2.Effect.Type.Rage && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e108");
		}
		else if (effect.type == Item2.Effect.Type.Weak && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e109");
		}
		else if (effect.type == Item2.Effect.Type.Dodge && effect.target == Item2.Effect.Target.player && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e110");
		}
		else if (effect.type == Item2.Effect.Type.Poison && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e111");
		}
		else if (effect.type == Item2.Effect.Type.Regen && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e112");
		}
		else if (effect.type == Item2.Effect.Type.Spikes && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e113");
		}
		else if (effect.type == Item2.Effect.Type.Haste && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e114");
		}
		else if (effect.type == Item2.Effect.Type.Slow && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e115");
		}
		else if (effect.type == Item2.Effect.Type.Rage && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e116");
		}
		else if (effect.type == Item2.Effect.Type.Weak && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e117");
		}
		else if (effect.type == Item2.Effect.Type.Dodge && effect.target == Item2.Effect.Target.enemy && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e118");
		}
		else if (effect.type == Item2.Effect.Type.Poison && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e119");
		}
		else if (effect.type == Item2.Effect.Type.Regen && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e120");
		}
		else if (effect.type == Item2.Effect.Type.Spikes && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e121");
		}
		else if (effect.type == Item2.Effect.Type.Haste && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e122");
		}
		else if (effect.type == Item2.Effect.Type.Slow && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e123");
		}
		else if (effect.type == Item2.Effect.Type.Rage && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e124");
		}
		else if (effect.type == Item2.Effect.Type.Weak && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e125");
		}
		else if (effect.type == Item2.Effect.Type.Dodge && effect.target == Item2.Effect.Target.allEnemies && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative && effect.value < 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e126");
		}
		else if (effect.type == Item2.Effect.Type.Burn && effect.target == Item2.Effect.Target.everyoneButPochette && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("e126bebs");
		}
		else if (effect.type == Item2.Effect.Type.Poison && effect.target == Item2.Effect.Target.everyoneButPochette && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("e126pebs");
		}
		else if (effect.type == Item2.Effect.Type.AddDamageToScratch && effect.mathematicalType == Item2.Effect.MathematicalType.summative && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e127");
		}
		else if (effect.type == Item2.Effect.Type.DrawToteCarvings && effect.value >= 0f)
		{
			text += LangaugeManager.main.GetTextByKey("e128");
		}
		else if (effect.type == Item2.Effect.Type.ToughHide && effect.value >= 0f && effect.target == Item2.Effect.Target.enemy)
		{
			text += LangaugeManager.main.GetTextByKey("e129");
		}
		else if (effect.type == Item2.Effect.Type.ChangeCostOfReorganize)
		{
			text += LangaugeManager.main.GetTextByKey("e130").Replace("/x", Mathf.CeilToInt(effectTotal.effect.value).ToString() ?? "");
		}
		else if (effect.type == Item2.Effect.Type.ChangeCostOfClear)
		{
			text += LangaugeManager.main.GetTextByKey("e130b").Replace("/x", Mathf.CeilToInt(effectTotal.effect.value).ToString() ?? "");
		}
		else if (effect.type == Item2.Effect.Type.GetGold)
		{
			if (Mathf.CeilToInt(effectTotal.effect.value) < 0)
			{
				text += LangaugeManager.main.GetTextByKey("ceg").Replace("/x", Mathf.CeilToInt(effectTotal.effect.value).ToString() ?? "");
			}
			else
			{
				text += LangaugeManager.main.GetTextByKey("ceg").Replace("/x", "+" + Mathf.CeilToInt(effectTotal.effect.value).ToString());
			}
		}
		else if (effect.type == Item2.Effect.Type.SummonPet)
		{
			text += LangaugeManager.main.GetTextByKey("e131");
		}
		else if (effect.type == Item2.Effect.Type.Block && effect.target == Item2.Effect.Target.friendliesInFront)
		{
			text += LangaugeManager.main.GetTextByKey("e133");
		}
		else if (effect.type == Item2.Effect.Type.HP && effect.target == Item2.Effect.Target.adjacentFriendlies)
		{
			text += LangaugeManager.main.GetTextByKey("e134");
		}
		else if (effect.type == Item2.Effect.Type.RevivePets && effect.target == Item2.Effect.Target.adjacentFriendlies)
		{
			text += LangaugeManager.main.GetTextByKey("e135");
		}
		else if (effect.type == Item2.Effect.Type.Rage && effect.target == Item2.Effect.Target.friendliesBehind)
		{
			text += LangaugeManager.main.GetTextByKey("e139");
		}
		else if (effect.type == Item2.Effect.Type.Block && effect.target == Item2.Effect.Target.allFriendlies)
		{
			text += LangaugeManager.main.GetTextByKey("e139block");
		}
		else if (effect.type == Item2.Effect.Type.Poison && effect.target == Item2.Effect.Target.statusFromItem)
		{
			text += LangaugeManager.main.GetTextByKey("e141");
		}
		else if (effect.type == Item2.Effect.Type.RevivePets && effect.target == Item2.Effect.Target.statusFromItem && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("m197");
		}
		else if (effect.type == Item2.Effect.Type.RevivePets && effect.target == Item2.Effect.Target.statusFromItem && effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative)
		{
			text += LangaugeManager.main.GetTextByKey("m198");
		}
		else if (effect.type == Item2.Effect.Type.HP && effect.target == Item2.Effect.Target.frontmostFriendly && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("e142");
		}
		else if (effect.type == Item2.Effect.Type.AddFood && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("e144");
		}
		else if (effect.type == Item2.Effect.Type.AddMaterial && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("e145");
		}
		else if (effect.type == Item2.Effect.Type.AddTreasure && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("e146");
		}
		else if (effect.type == Item2.Effect.Type.AddPopulation && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("e147");
		}
		else if (effect.type == Item2.Effect.Type.AddGiftItem && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("e148");
		}
		else if (effect.type == Item2.Effect.Type.Spikes && effect.target == Item2.Effect.Target.adjacentFriendlies && effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text += LangaugeManager.main.GetTextByKey("e149");
		}
		else if (effect.type == Item2.Effect.Type.Zombie && effect.target == Item2.Effect.Target.statusFromItem)
		{
			text += LangaugeManager.main.GetTextByKey("e153");
		}
		else if (effect.type == Item2.Effect.Type.Exhaust)
		{
			text += LangaugeManager.main.GetTextByKey("e154");
		}
		else if (effect.type == Item2.Effect.Type.Curse)
		{
			if (effect.value >= 0f)
			{
				text += LangaugeManager.main.GetTextByKey("e155");
			}
			else
			{
				text += LangaugeManager.main.GetTextByKey("e156");
			}
		}
		if (effect.value >= 0f && flag2)
		{
			text = text.Replace("/x", "+/x");
		}
		if (effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			text = text.Replace("/x", Mathf.Abs(Mathf.CeilToInt(effectTotal.effect.value)).ToString() ?? "");
		}
		else
		{
			text = text.Replace("/x", Mathf.Abs(Mathf.RoundToInt(effectTotal.effect.value * 100f)).ToString() ?? "");
		}
		return text + this.GetEffectPieces(effectTotal, flag);
	}

	// Token: 0x060003D0 RID: 976 RVA: 0x00026910 File Offset: 0x00024B10
	private string GetEffectPieces(Item2.EffectTotal effectTotal, bool showEffectPieces)
	{
		if (effectTotal.effect.type == Item2.Effect.Type.Luck)
		{
			return "";
		}
		if (!showEffectPieces)
		{
			return "";
		}
		if (effectTotal.effectPieces.Count <= 2 && effectTotal.effect.value == effectTotal.effectPieces[0].value && (effectTotal.effectPieces[0].name.Trim().ToLower() == "base" || effectTotal.effectPieces[0].name.Trim().ToLower() == Item2.GetDisplayName(this.parent.name).Trim().ToLower()))
		{
			return "";
		}
		return Card.GetEffectPieceDescriptions(effectTotal);
	}

	// Token: 0x060003D1 RID: 977 RVA: 0x000269D8 File Offset: 0x00024BD8
	private static string GetEffectPieceDescriptions(Item2.EffectTotal effectTotal)
	{
		string text = "";
		text += "<size=90%>";
		foreach (Item2.EffectTotal.EffectPiece effectPiece in effectTotal.effectPieces)
		{
			if (effectPiece.value != 0f)
			{
				string text2 = "";
				if (effectPiece.numberOfTimes > 1)
				{
					text2 = effectPiece.numberOfTimes.ToString() + "x ";
				}
				if (effectPiece.name == "All multipliers")
				{
					if (effectTotal.multiplier != 1f)
					{
						text = string.Concat(new string[]
						{
							text,
							"<br>  <u>",
							text2,
							LangaugeManager.main.GetTextByKey("efm"),
							": ",
							(effectTotal.multiplier * 100f).ToString(),
							"%"
						});
						text += "</u>";
						text = string.Concat(new string[]
						{
							text,
							"<br>  ",
							text2,
							LangaugeManager.main.GetTextByKey("base"),
							": 100%"
						});
					}
				}
				else if (effectTotal.effect.type == Item2.Effect.Type.ModifierMultiplier)
				{
					if (effectPiece.name == "Base")
					{
						text = string.Concat(new string[]
						{
							text,
							"<br>  ",
							text2,
							LangaugeManager.main.GetTextByKey(effectPiece.name),
							": ",
							effectPiece.value.ToString()
						});
					}
					else
					{
						text = string.Concat(new string[]
						{
							text,
							"<br>  ",
							text2,
							LangaugeManager.main.GetTextByKey(effectPiece.name),
							": ",
							(effectPiece.value * 100f).ToString(),
							"%"
						});
					}
				}
				else if (effectPiece.value >= 0f && effectPiece.name != "Base")
				{
					string text3 = "";
					if (effectPiece.mathematicalType == Item2.Effect.MathematicalType.summative)
					{
						text3 = string.Concat(new string[]
						{
							text3,
							"<br>  ",
							text2,
							LangaugeManager.main.GetTextByKey(effectPiece.name),
							": +",
							effectPiece.value.ToString()
						});
					}
					else if (effectPiece.mathematicalType == Item2.Effect.MathematicalType.multiplicative)
					{
						text3 = string.Concat(new string[]
						{
							text3,
							"<br>  ",
							text2,
							LangaugeManager.main.GetTextByKey(effectPiece.name),
							": +",
							(effectPiece.value * 100f).ToString(),
							"%"
						});
					}
					if (text3.Length > 25)
					{
						text3 = "<cspace=-0.1em>" + text3 + "</cspace>";
					}
					else if (text3.Length > 22)
					{
						text3 = "<cspace=-0.05em>" + text3 + "</cspace>";
					}
					text += text3;
				}
				else if (effectPiece.mathematicalType == Item2.Effect.MathematicalType.summative)
				{
					text = string.Concat(new string[]
					{
						text,
						"<br>  ",
						text2,
						LangaugeManager.main.GetTextByKey(effectPiece.name),
						": ",
						effectPiece.value.ToString()
					});
				}
				else if (effectPiece.mathematicalType == Item2.Effect.MathematicalType.multiplicative)
				{
					text = string.Concat(new string[]
					{
						text,
						"<br>  ",
						text2,
						LangaugeManager.main.GetTextByKey(effectPiece.name),
						": ",
						(effectPiece.value * 100f).ToString(),
						"%"
					});
				}
			}
		}
		text += "</size>";
		return text;
	}

	// Token: 0x060003D2 RID: 978 RVA: 0x00026DF0 File Offset: 0x00024FF0
	private Vector3[] GetRectCorners(RectTransform rectTransform)
	{
		this.canvasRect = base.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
		Vector3[] array = new Vector3[4];
		rectTransform.GetWorldCorners(array);
		for (int i = 0; i < 4; i++)
		{
			array[i] = this.canvasRect.InverseTransformVector(array[i] - this.canvasRect.anchoredPosition3D);
		}
		return array;
	}

	// Token: 0x060003D3 RID: 979 RVA: 0x00026E54 File Offset: 0x00025054
	public static Vector2 GetLocalPoint(Vector2 point, Canvas canvas)
	{
		Vector2 vector;
		if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), Camera.main.WorldToScreenPoint(point), canvas.worldCamera, out vector);
		}
		else
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), Camera.main.WorldToScreenPoint(point), null, out vector);
		}
		return vector;
	}

	// Token: 0x060003D4 RID: 980 RVA: 0x00026EBC File Offset: 0x000250BC
	public static void SetPivots(RectTransform rectTransform, Vector2 localPoint)
	{
		if (localPoint.x > 0f)
		{
			rectTransform.pivot = new Vector2(1f, rectTransform.pivot.y);
		}
		else
		{
			rectTransform.pivot = new Vector2(0f, rectTransform.pivot.y);
		}
		if (localPoint.y < 0f)
		{
			rectTransform.pivot = new Vector2(rectTransform.pivot.x, 0f);
			return;
		}
		rectTransform.pivot = new Vector2(rectTransform.pivot.x, 1f);
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x00026F54 File Offset: 0x00025154
	public static void SpaceRectTransform(RectTransform rectTransform, Canvas canvas, Vector2 localPoint)
	{
		if (rectTransform.pivot == new Vector2(1f, 1f))
		{
			localPoint -= new Vector2(50f, 50f);
		}
		else if (rectTransform.pivot == new Vector2(0f, 1f))
		{
			localPoint -= new Vector2(-60f, 60f);
		}
		else if (rectTransform.pivot == new Vector2(1f, 0f))
		{
			localPoint -= new Vector2(20f, -20f);
		}
		if (rectTransform.pivot == new Vector2(0f, 0f))
		{
			localPoint -= new Vector2(-20f, -20f);
		}
		rectTransform.anchoredPosition = localPoint;
	}

	// Token: 0x060003D6 RID: 982 RVA: 0x00027038 File Offset: 0x00025238
	private void Update()
	{
		if (!this.canvasGroup)
		{
			return;
		}
		if (this.deleteOnDeactivate && (!this.parent || !this.parent.activeInHierarchy))
		{
			Object.Destroy(base.gameObject);
			return;
		}
		if (Overworld_Manager.main && Overworld_Manager.main.IsBuildingMode() && this.parent && !this.parent.GetComponent<RectTransform>())
		{
			this.rectTransform.pivot = new Vector2(0.5f, 0.5f);
			base.transform.position = Overworld_Manager.main.cardPosition.position;
			return;
		}
		Vector2 localPoint = Card.GetLocalPoint(DigitalCursor.main.transform.position, this.canvas);
		if (!this.stuck)
		{
			Card.SpaceRectTransform(this.rectTransform, this.canvas, localPoint);
			this.canvasGroup.interactable = false;
			this.canvasGroup.blocksRaycasts = false;
			Vector2 vector = this.rectTransform.TransformPoint(this.rectTransform.rect.min);
			this.canvasRect.TransformPoint(this.canvasRect.rect.min) - vector;
			this.KeepFullyOnScreen();
			return;
		}
		this.canvasGroup.interactable = true;
		this.canvasGroup.blocksRaycasts = true;
		this.canvasGroup.alpha = 1f;
	}

	// Token: 0x060003D7 RID: 983 RVA: 0x000271CC File Offset: 0x000253CC
	private void KeepFullyOnScreen()
	{
		Vector3 localPosition = this.rectTransform.localPosition;
		Vector3 vector = this.canvasRect.rect.min - this.rectTransform.rect.min;
		Vector3 vector2 = this.canvasRect.rect.max - this.rectTransform.rect.max;
		localPosition.x = Mathf.Clamp(this.rectTransform.localPosition.x, vector.x, vector2.x);
		localPosition.y = Mathf.Clamp(this.rectTransform.localPosition.y, vector.y, vector2.y);
		this.rectTransform.localPosition = localPosition;
	}

	// Token: 0x060003D8 RID: 984 RVA: 0x000272A3 File Offset: 0x000254A3
	public void EndHover()
	{
		if (this.stuck)
		{
			return;
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060003D9 RID: 985 RVA: 0x000272B9 File Offset: 0x000254B9
	private IEnumerator FindHeight()
	{
		if (this.isBuilding)
		{
			yield break;
		}
		this.isBuilding = true;
		if (this.canvasGroup && !this.stuck)
		{
			this.canvasGroup.alpha = 0f;
		}
		LangaugeManager.main.SetFont(base.transform);
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		Vector2 vector;
		if (this.canvas.renderMode == RenderMode.ScreenSpaceCamera)
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.canvas.GetComponent<RectTransform>(), Camera.main.WorldToScreenPoint(DigitalCursor.main.transform.position), this.canvas.worldCamera, out vector);
		}
		else
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.canvas.GetComponent<RectTransform>(), Camera.main.WorldToScreenPoint(DigitalCursor.main.transform.position), null, out vector);
		}
		if (this.stuck)
		{
			this.rectTransform.pivot = new Vector2(0.5f, 1f);
			this.rectTransform.anchoredPosition = Vector2.zero;
		}
		else
		{
			if (vector.x > 0f)
			{
				this.rectTransform.pivot = new Vector2(1f, this.rectTransform.pivot.y);
			}
			else
			{
				this.rectTransform.pivot = new Vector2(0f, this.rectTransform.pivot.y);
			}
			if (vector.y < 0f)
			{
				this.rectTransform.pivot = new Vector2(this.rectTransform.pivot.x, 0f);
			}
			else
			{
				this.rectTransform.pivot = new Vector2(this.rectTransform.pivot.x, 1f);
			}
		}
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		this.isSetup = true;
		if (!this.stuck)
		{
			float t = 0f;
			while (t < 0.1f)
			{
				t += Time.deltaTime;
				this.canvasGroup.alpha = Mathf.Lerp(0f, 1f, t / 0.1f);
				yield return null;
			}
		}
		this.canvasGroup.alpha = 1f;
		this.isBuilding = false;
		yield break;
	}

	// Token: 0x04000296 RID: 662
	public static List<Card> allCards = new List<Card>();

	// Token: 0x04000297 RID: 663
	[SerializeField]
	private Sprite automatedTransformSprite;

	// Token: 0x04000298 RID: 664
	[SerializeField]
	private Image characterIcon;

	// Token: 0x04000299 RID: 665
	[SerializeField]
	private GameObject textPrefab;

	// Token: 0x0400029A RID: 666
	[SerializeField]
	private GameObject itemsPrefab;

	// Token: 0x0400029B RID: 667
	[SerializeField]
	private GameObject overworldItemButtonPrefab;

	// Token: 0x0400029C RID: 668
	public bool deleteOnDeactivate = true;

	// Token: 0x0400029D RID: 669
	public bool canBeMovedUp;

	// Token: 0x0400029E RID: 670
	public bool canBeRemoved;

	// Token: 0x0400029F RID: 671
	[SerializeField]
	private Transform itemShapeParent;

	// Token: 0x040002A0 RID: 672
	[SerializeField]
	public TextMeshProUGUI forgeCount;

	// Token: 0x040002A1 RID: 673
	[SerializeField]
	private GameObject metaProgressImage1;

	// Token: 0x040002A2 RID: 674
	[SerializeField]
	private GameObject metaProgressImage2;

	// Token: 0x040002A3 RID: 675
	[SerializeField]
	private GameObject metaProgressImage3;

	// Token: 0x040002A4 RID: 676
	[SerializeField]
	private GameObject itemShapePrefab;

	// Token: 0x040002A5 RID: 677
	[SerializeField]
	private Sprite cardTextBorderConstantSprite;

	// Token: 0x040002A6 RID: 678
	[SerializeField]
	private Transform cardPropertiesParent;

	// Token: 0x040002A7 RID: 679
	[SerializeField]
	private Transform cardSecondaryPropertiesParent;

	// Token: 0x040002A8 RID: 680
	[SerializeField]
	private Image cardBackground;

	// Token: 0x040002A9 RID: 681
	[SerializeField]
	private float border = 10f;

	// Token: 0x040002AA RID: 682
	[SerializeField]
	private RectTransform fill;

	// Token: 0x040002AB RID: 683
	[SerializeField]
	private GameObject priceTag;

	// Token: 0x040002AC RID: 684
	[SerializeField]
	private TextMeshProUGUI priceTagText;

	// Token: 0x040002AD RID: 685
	[SerializeField]
	private RectTransform rectTransform;

	// Token: 0x040002AE RID: 686
	private Canvas canvas;

	// Token: 0x040002AF RID: 687
	private RectTransform canvasRect;

	// Token: 0x040002B0 RID: 688
	private Vector2 fullSize;

	// Token: 0x040002B1 RID: 689
	private float time;

	// Token: 0x040002B2 RID: 690
	private float expandTime = 0.2f;

	// Token: 0x040002B3 RID: 691
	private float timeSetup;

	// Token: 0x040002B4 RID: 692
	public bool stuck;

	// Token: 0x040002B5 RID: 693
	private bool isSetup;

	// Token: 0x040002B6 RID: 694
	private RectTransform fillParent;

	// Token: 0x040002B7 RID: 695
	[SerializeField]
	private CanvasGroup canvasGroup;

	// Token: 0x040002B8 RID: 696
	public Image iconSprite;

	// Token: 0x040002B9 RID: 697
	public TextMeshProUGUI displayName;

	// Token: 0x040002BA RID: 698
	[SerializeField]
	private GameObject cardPropertyPrefab;

	// Token: 0x040002BB RID: 699
	[SerializeField]
	private GameObject standardCostsParent;

	// Token: 0x040002BC RID: 700
	[SerializeField]
	private GameObject apCostText;

	// Token: 0x040002BD RID: 701
	[SerializeField]
	private GameObject manaText;

	// Token: 0x040002BE RID: 702
	[SerializeField]
	private GameObject goldCostText;

	// Token: 0x040002BF RID: 703
	[SerializeField]
	private GameObject sunshineChargesText;

	// Token: 0x040002C0 RID: 704
	[SerializeField]
	private GameObject moveableSymbol;

	// Token: 0x040002C1 RID: 705
	[SerializeField]
	private GameObject transformableSymbol;

	// Token: 0x040002C2 RID: 706
	[SerializeField]
	private GameObject alternateUseSymbol;

	// Token: 0x040002C3 RID: 707
	[SerializeField]
	private GameObject summonCostsParent;

	// Token: 0x040002C4 RID: 708
	[SerializeField]
	private GameObject apSummonCostText;

	// Token: 0x040002C5 RID: 709
	[SerializeField]
	private GameObject manaSummonText;

	// Token: 0x040002C6 RID: 710
	[SerializeField]
	private GameObject goldSummonCostText;

	// Token: 0x040002C7 RID: 711
	[SerializeField]
	private TextMeshProUGUI itemType;

	// Token: 0x040002C8 RID: 712
	[SerializeField]
	private TextMeshProUGUI itemRarity;

	// Token: 0x040002C9 RID: 713
	private GameObject parent;

	// Token: 0x040002CA RID: 714
	private float timeAlive;

	// Token: 0x040002CB RID: 715
	[SerializeField]
	private bool ignoreLayOut;

	// Token: 0x040002CC RID: 716
	[Header("Pet Properties")]
	private Image petSprite;

	// Token: 0x040002CD RID: 717
	private bool isBuilding;

	// Token: 0x020002B6 RID: 694
	private class ItemStatusEffectCounter
	{
		// Token: 0x0400105C RID: 4188
		public Item2.ItemStatusEffect.Type type;

		// Token: 0x0400105D RID: 4189
		public List<Item2.ItemStatusEffect> effects;
	}

	// Token: 0x020002B7 RID: 695
	public class DescriptorTotal
	{
		// Token: 0x0400105E RID: 4190
		public Item2.Trigger trigger;

		// Token: 0x0400105F RID: 4191
		public string triggerOverrideKey = "";

		// Token: 0x04001060 RID: 4192
		public string effectOverrideKey = "";

		// Token: 0x04001061 RID: 4193
		public float triggerOverrideValue = -999f;

		// Token: 0x04001062 RID: 4194
		public bool stackable;

		// Token: 0x04001063 RID: 4195
		public List<string> texts;

		// Token: 0x04001064 RID: 4196
		public bool flavor;

		// Token: 0x04001065 RID: 4197
		public bool disableEmoji;
	}
}
