using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000185 RID: 389
public class ItemAtlas : MonoBehaviour
{
	// Token: 0x06000F98 RID: 3992 RVA: 0x00097AF0 File Offset: 0x00095CF0
	public void ShowAsSelected(Transform selectedTrans)
	{
		foreach (object obj in this.characterSelectMenu.transform)
		{
			Image component = ((Transform)obj).GetComponent<Image>();
			if (component)
			{
				component.color = Color.white;
			}
		}
		Image component2 = selectedTrans.GetComponent<Image>();
		if (component2)
		{
			component2.color = new Color(0.5f, 0.5f, 0.5f, 1f);
		}
	}

	// Token: 0x06000F99 RID: 3993 RVA: 0x00097B90 File Offset: 0x00095D90
	public void SelectCharacter(int x)
	{
		this.SelectCharacter((Character.CharacterName)x);
	}

	// Token: 0x06000F9A RID: 3994 RVA: 0x00097B99 File Offset: 0x00095D99
	public void SelectCharacter(Character.CharacterName characterName)
	{
		this.characterName = characterName;
		this.ResetItemList();
	}

	// Token: 0x06000F9B RID: 3995 RVA: 0x00097BA8 File Offset: 0x00095DA8
	private void Start()
	{
		if (!Singleton.Instance.storyMode)
		{
			this.loreButton.SetActive(false);
			this.researchButton.SetActive(false);
		}
		this.tutorialManager = Object.FindObjectOfType<TutorialManager>();
		this.contextMenuManager = Object.FindObjectOfType<ContextMenuManager>();
		this.eventBoxAnimator = base.GetComponentInChildren<Animator>();
		LangaugeManager.main.SetFont(this.inputField.transform);
		this.backButton.SetActive(false);
		this.SetupAtlasType(ItemAtlas.ViewType.items);
	}

	// Token: 0x06000F9C RID: 3996 RVA: 0x00097C24 File Offset: 0x00095E24
	public void SetupAtlasType(string x)
	{
		if (x == "enemies")
		{
			this.SetupAtlasType(ItemAtlas.ViewType.enemies);
			return;
		}
		if (x == "items")
		{
			this.SetupAtlasType(ItemAtlas.ViewType.items);
			return;
		}
		if (x == "help")
		{
			this.SetupAtlasType(ItemAtlas.ViewType.help);
			return;
		}
		if (x == "terms")
		{
			this.SetupAtlasType(ItemAtlas.ViewType.terms);
			return;
		}
		if (x == "lore")
		{
			this.SetupAtlasType(ItemAtlas.ViewType.lore);
			return;
		}
		if (x == "research")
		{
			this.SetupAtlasType(ItemAtlas.ViewType.research);
		}
	}

	// Token: 0x06000F9D RID: 3997 RVA: 0x00097CB0 File Offset: 0x00095EB0
	public void SetupAtlasType(ItemAtlas.ViewType viewTypeChange)
	{
		this.cardButtonVerticalLayoutGroup.spacing = 60f;
		this.cardButtonVerticalLayoutGroup.padding.left = 30;
		this.cardButtonVerticalLayoutGroup.childForceExpandHeight = true;
		this.cardButtonVerticalLayoutGroup.childAlignment = TextAnchor.MiddleCenter;
		this.characterSelectMenu.SetActive(false);
		this.characterName = Character.CharacterName.Any;
		if (this.inputField)
		{
			this.inputField.text = "";
			this.inputField.text = LangaugeManager.main.GetTextByKey("gm46");
			LangaugeManager.main.SetFont(this.inputField.transform);
		}
		this.ClearItemList();
		foreach (object obj in this.selectorButtonParent)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
		this.viewType = viewTypeChange;
		if (this.viewType == ItemAtlas.ViewType.enemies)
		{
			this.FindEnemies();
			DigitalCursor.main.SelectFirstSelectableInElement(this.itemButtonParent);
			if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.controller || !this.disableSearchOnController)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.searchField, this.selectorButtonParent);
				gameObject.transform.SetAsFirstSibling();
				this.inputField = gameObject.GetComponentInChildren<TMP_InputField>();
				gameObject.SetActive(true);
			}
		}
		else if (this.viewType == ItemAtlas.ViewType.research)
		{
			this.cardButtonVerticalLayoutGroup.spacing = -120f;
			this.cardButtonVerticalLayoutGroup.padding.top = -60;
			this.cardButtonVerticalLayoutGroup.padding.left = -20;
			this.cardButtonVerticalLayoutGroup.childForceExpandHeight = false;
			this.cardButtonVerticalLayoutGroup.childAlignment = TextAnchor.UpperLeft;
			this.searchField.SetActive(false);
			this.FindResearchInterfaces();
			DigitalCursor.main.SelectFirstSelectableInElement(this.selectorButtonParent);
		}
		else if (this.viewType == ItemAtlas.ViewType.lore)
		{
			this.searchField.SetActive(false);
			this.FindLores();
			DigitalCursor.main.SelectFirstSelectableInElement(this.selectorButtonParent);
		}
		else if (this.viewType == ItemAtlas.ViewType.items)
		{
			if ((MetaProgressSaveManager.main.HasAnyMetaProgressMarker(new List<MetaProgressSaveManager.MetaProgressMarker>
			{
				MetaProgressSaveManager.MetaProgressMarker.unlockedSatchel,
				MetaProgressSaveManager.MetaProgressMarker.unlockedCR8,
				MetaProgressSaveManager.MetaProgressMarker.unlockedTote,
				MetaProgressSaveManager.MetaProgressMarker.unlockedPochette
			}) || !Singleton.Instance.storyMode) && !this.characterSelectMenu.activeSelf)
			{
				this.characterSelectMenu.SetActive(true);
				this.ShowAsSelected(this.characterSelectMenu.transform.GetChild(0));
			}
			this.SetupItemTypes();
			DigitalCursor.main.SelectUIElement(this.selectorButtonParent.GetChild(0).gameObject);
			if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.controller || !this.disableSearchOnController)
			{
				GameObject gameObject2 = Object.Instantiate<GameObject>(this.searchField, this.selectorButtonParent);
				this.inputField = gameObject2.GetComponentInChildren<TMP_InputField>();
				gameObject2.transform.SetAsFirstSibling();
				gameObject2.SetActive(true);
			}
		}
		else if (this.viewType == ItemAtlas.ViewType.help)
		{
			this.searchField.SetActive(false);
			this.FindHelps();
			DigitalCursor.main.SelectFirstSelectableInElement(this.selectorButtonParent);
		}
		else if (this.viewType == ItemAtlas.ViewType.terms)
		{
			this.searchField.SetActive(false);
			this.FindTerms();
			DigitalCursor.main.SelectFirstSelectableInElement(this.selectorButtonParent);
		}
		LangaugeManager.main.SetFont(this.selectorButtonParent);
	}

	// Token: 0x06000F9E RID: 3998 RVA: 0x00098014 File Offset: 0x00096214
	private void ClearItemCount()
	{
		this.foundItems = 0;
		this.totalItems = 0;
		this.itemCountNumber.text = "";
	}

	// Token: 0x06000F9F RID: 3999 RVA: 0x00098034 File Offset: 0x00096234
	private void SetItemCount()
	{
		this.itemCountNumber.text = this.foundItems.ToString() + "/" + this.totalItems.ToString();
	}

	// Token: 0x06000FA0 RID: 4000 RVA: 0x00098064 File Offset: 0x00096264
	public void OnInput()
	{
		string text = this.inputField.text.ToLower();
		this.searchTerm = "";
		if (text.Length < 2)
		{
			return;
		}
		this.searchTerm = text;
		this.ClearItemList();
		DebugItemManager debugItemManager = Object.FindObjectOfType<DebugItemManager>();
		if (debugItemManager)
		{
			List<GameObject> list = new List<GameObject>();
			if (this.viewType == ItemAtlas.ViewType.items)
			{
				list = debugItemManager.items;
			}
			else if (this.viewType == ItemAtlas.ViewType.enemies)
			{
				list = debugItemManager.enemies;
			}
			foreach (GameObject gameObject in list)
			{
				string displayName = Item2.GetDisplayName(gameObject.name);
				if (LangaugeManager.main.GetTextByKey(displayName).ToLower().Contains(text))
				{
					this.CreateButton(gameObject);
				}
			}
			this.SetItemCount();
		}
		LangaugeManager.main.SetFont(this.itemButtonParent);
	}

	// Token: 0x06000FA1 RID: 4001 RVA: 0x0009815C File Offset: 0x0009635C
	public void FindResearchInterfaces()
	{
		List<Transform> list = new List<Transform>();
		foreach (GameObject gameObject in this.overworldResearchUI)
		{
			string name = gameObject.name;
			if (MetaProgressSaveManager.main.researchBuildingsDiscovered.Contains(Item2.GetDisplayName(name)))
			{
				GameObject gameObject2 = Object.Instantiate<GameObject>(this.itemTypeButton, Vector3.zero, Quaternion.identity, this.selectorButtonParent);
				gameObject2.GetComponent<RectTransform>();
				ItemAtlasButton component = gameObject2.GetComponent<ItemAtlasButton>();
				component.type = ItemAtlasButton.Type.research;
				component.key = name;
				gameObject2.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey(name);
				list.Add(gameObject2.transform);
			}
		}
		this.SetAlphabeticOrder(list);
	}

	// Token: 0x06000FA2 RID: 4002 RVA: 0x0009822C File Offset: 0x0009642C
	public void FindResearch(Item2 itemToCheck, out List<Overworld_BuildingInterface.Research> researchesSpecific, out List<Overworld_BuildingInterface.Research> researchesGeneral)
	{
		researchesGeneral = new List<Overworld_BuildingInterface.Research>();
		researchesSpecific = new List<Overworld_BuildingInterface.Research>();
		this.overworldResearch = this.overworldResearchUI.Select((GameObject x) => x.GetComponent<Overworld_BuildingInterface>()).ToList<Overworld_BuildingInterface>();
		foreach (Overworld_BuildingInterface overworld_BuildingInterface in this.overworldResearch)
		{
			if (MetaProgressSaveManager.main.researchBuildingsDiscovered.Contains(Item2.GetDisplayName(overworld_BuildingInterface.name)))
			{
				foreach (Overworld_BuildingInterface.Research research in overworld_BuildingInterface.researches)
				{
					research.Load(null);
					if (research.Available() && !research.IsComplete())
					{
						research.launcherSprite = overworld_BuildingInterface.launcherSpriteDefault;
						research.launcherName = Item2.GetDisplayName(overworld_BuildingInterface.name);
						using (List<GameObject>.Enumerator enumerator3 = research.itemsRequired.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								if (Item2.GetDisplayName(enumerator3.Current.name) == Item2.GetDisplayName(itemToCheck.name) && !researchesSpecific.Contains(research))
								{
									researchesSpecific.Add(research);
								}
							}
						}
						for (int i = 0; i < research.itemTypesRequired.Count; i++)
						{
							Item2.ItemType itemType = research.itemTypesRequired[i];
							Item2.Rarity rarity = Item2.Rarity.Common;
							if (research.itemRaritiesRequired.Count > i)
							{
								rarity = research.itemRaritiesRequired[i];
							}
							if (itemToCheck.itemType.Contains(itemType) && itemToCheck.rarity >= rarity && !researchesGeneral.Contains(research))
							{
								researchesGeneral.Add(research);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000FA3 RID: 4003 RVA: 0x00098460 File Offset: 0x00096660
	public void FindLores()
	{
		List<Transform> list = new List<Transform>();
		List<string> list2 = new List<string>();
		list2 = MetaProgressSaveManager.main.loresUnlocked;
		if (Singleton.Instance.developmentMode && Singleton.Instance.unlockAllAtlas)
		{
			foreach (Lore.LoreType loreType in this.loreCardPrefab.GetComponent<Lore>().loreTypes)
			{
				if (!list2.Contains(loreType.nameKey))
				{
					list2.Add(loreType.nameKey);
				}
			}
		}
		foreach (string text in list2)
		{
			if (LangaugeManager.main.KeyExists(text))
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.itemTypeButton, Vector3.zero, Quaternion.identity, this.selectorButtonParent);
				ItemAtlasButton component = gameObject.GetComponent<ItemAtlasButton>();
				component.type = ItemAtlasButton.Type.lore;
				component.key = text;
				gameObject.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey(text);
				HighlightText componentInChildren = gameObject.GetComponentInChildren<HighlightText>();
				if (componentInChildren)
				{
					Object.Destroy(componentInChildren);
				}
				list.Add(gameObject.transform);
			}
		}
		this.SetAlphabeticOrder(list);
	}

	// Token: 0x06000FA4 RID: 4004 RVA: 0x000985C4 File Offset: 0x000967C4
	public void FindHelps()
	{
		List<Transform> list = new List<Transform>();
		List<string> list2 = MetaProgressSaveManager.main.LoadCompletedTutorials();
		if (Singleton.Instance.developmentMode && Singleton.Instance.unlockAllAtlas)
		{
			list2 = TutorialPopUpManager.main.GetAllTutorials();
		}
		foreach (string text in list2)
		{
			string text2 = "tut-" + text;
			foreach (TutorialPopUpManager.TutorialPopUp tutorialPopUp in TutorialPopUpManager.main.tutorialPopUps)
			{
				if (tutorialPopUp.name == text && tutorialPopUp.nameOverrideInLanguageManager != null && tutorialPopUp.nameOverrideInLanguageManager.Length > 0)
				{
					text2 = "tut-" + tutorialPopUp.nameOverrideInLanguageManager;
				}
			}
			if (LangaugeManager.main.KeyExists(text2))
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.itemTypeButton, Vector3.zero, Quaternion.identity, this.selectorButtonParent);
				ItemAtlasButton component = gameObject.GetComponent<ItemAtlasButton>();
				component.type = ItemAtlasButton.Type.help;
				component.key = text;
				gameObject.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey(text2);
				HighlightText componentInChildren = gameObject.GetComponentInChildren<HighlightText>();
				if (componentInChildren)
				{
					Object.Destroy(componentInChildren);
				}
				list.Add(gameObject.transform);
			}
		}
		this.SetAlphabeticOrder(list);
	}

	// Token: 0x06000FA5 RID: 4005 RVA: 0x0009876C File Offset: 0x0009696C
	public void FindTerms()
	{
		List<Transform> list = new List<Transform>();
		CardManager cardManager = Object.FindObjectOfType<CardManager>();
		if (!cardManager)
		{
			return;
		}
		foreach (string text in cardManager.strings)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.itemTypeButton, Vector3.zero, Quaternion.identity, this.selectorButtonParent);
			ItemAtlasButton component = gameObject.GetComponent<ItemAtlasButton>();
			component.type = ItemAtlasButton.Type.term;
			component.key = text;
			gameObject.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey(text);
			list.Add(gameObject.transform);
		}
		this.SetAlphabeticOrder(list);
	}

	// Token: 0x06000FA6 RID: 4006 RVA: 0x0009882C File Offset: 0x00096A2C
	public void SetupItemTypes()
	{
		List<Transform> list = new List<Transform>();
		foreach (object obj in Enum.GetValues(typeof(Item2.ItemType)))
		{
			Item2.ItemType itemType = (Item2.ItemType)obj;
			if (itemType != Item2.ItemType.Gold && itemType != Item2.ItemType.Mana && itemType != Item2.ItemType.Sunshine && LangaugeManager.main.KeyExists(itemType.ToString()))
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.itemTypeButton, Vector3.zero, Quaternion.identity, this.selectorButtonParent);
				HighlightText componentInChildren = gameObject.GetComponentInChildren<HighlightText>();
				if (componentInChildren)
				{
					Object.Destroy(componentInChildren);
				}
				ItemAtlasButton component = gameObject.GetComponent<ItemAtlasButton>();
				component.type = ItemAtlasButton.Type.selector;
				component.itemType = itemType;
				gameObject.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey(itemType.ToString());
				list.Add(gameObject.transform);
			}
		}
		this.SetAlphabeticOrder(list);
		LangaugeManager.main.SetFont(this.selectorButtonParent);
	}

	// Token: 0x06000FA7 RID: 4007 RVA: 0x00098954 File Offset: 0x00096B54
	private void SetAlphabeticOrder(List<Transform> list)
	{
		list.Sort((Transform t1, Transform t2) => t1.GetComponentInChildren<TextMeshProUGUI>().text.CompareTo(t2.GetComponentInChildren<TextMeshProUGUI>().text));
		for (int i = 0; i < list.Count; i++)
		{
			list[i].SetSiblingIndex(i);
		}
	}

	// Token: 0x06000FA8 RID: 4008 RVA: 0x000989A4 File Offset: 0x00096BA4
	private void ClearItemList()
	{
		this.DestroyAllProxies();
		this.ClearItemCount();
		this.backButton.SetActive(false);
		this.isViewing = false;
		this.itemButtonParent.transform.parent.parent.gameObject.SetActive(true);
		this.cardButtonParent.transform.parent.parent.gameObject.SetActive(false);
		foreach (object obj in this.itemButtonParent.transform)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
	}

	// Token: 0x06000FA9 RID: 4009 RVA: 0x00098A64 File Offset: 0x00096C64
	private void ResetItemList()
	{
		this.ClearItemList();
		if (this.itemType != Item2.ItemType.undefined)
		{
			this.FindItemsOfType(this.itemType);
			return;
		}
		if (this.searchTerm != "")
		{
			this.OnInput();
		}
	}

	// Token: 0x06000FAA RID: 4010 RVA: 0x00098A9C File Offset: 0x00096C9C
	public void FindEnemies()
	{
		this.ClearItemList();
		if (this.inputField)
		{
			this.inputField.text = "";
			this.inputField.text = LangaugeManager.main.GetTextByKey("gm46");
			LangaugeManager.main.SetFont(this.inputField.transform);
		}
		DebugItemManager debugItemManager = Object.FindObjectOfType<DebugItemManager>();
		if (debugItemManager)
		{
			foreach (GameObject gameObject in debugItemManager.enemies)
			{
				if (!(Item2.GetDisplayName(gameObject.name) == Item2.GetDisplayName("Pochette")) && !(Item2.GetDisplayName(gameObject.name) == Item2.GetDisplayName("Satchel")) && !(Item2.GetDisplayName(gameObject.name) == Item2.GetDisplayName("Tote")) && !(Item2.GetDisplayName(gameObject.name) == Item2.GetDisplayName("Patita")) && !(Item2.GetDisplayName(gameObject.name) == Item2.GetDisplayName("Disorder")))
				{
					this.CreateButton(gameObject);
				}
			}
		}
		this.SetItemCount();
		LangaugeManager.main.SetFont(this.itemButtonParent);
	}

	// Token: 0x06000FAB RID: 4011 RVA: 0x00098BFC File Offset: 0x00096DFC
	public void FindItemsOfType(Item2.ItemType itemType)
	{
		this.ClearItemList();
		this.itemType = itemType;
		this.searchTerm = "";
		this.inputField.text = "";
		this.inputField.text = LangaugeManager.main.GetTextByKey("gm46");
		LangaugeManager.main.SetFont(this.inputField.transform);
		DebugItemManager debugItemManager = Object.FindObjectOfType<DebugItemManager>();
		if (debugItemManager)
		{
			Func<Item2, bool> <>9__0;
			foreach (GameObject gameObject in debugItemManager.items)
			{
				ItemSwitcher component = gameObject.GetComponent<ItemSwitcher>();
				if (!gameObject.GetComponent<Item2>().itemType.Contains(itemType))
				{
					if (!component)
					{
						continue;
					}
					IEnumerable<Item2> item2s = component.GetItem2s();
					Func<Item2, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (Item2 x) => x.itemType.Contains(itemType));
					}
					if (!item2s.Any(func))
					{
						continue;
					}
				}
				this.CreateButton(gameObject);
			}
		}
		this.SetItemCount();
		LangaugeManager.main.SetFont(this.itemButtonParent);
	}

	// Token: 0x06000FAC RID: 4012 RVA: 0x00098D38 File Offset: 0x00096F38
	public void BackButtonPressed()
	{
		this.DestroyAllProxies();
		this.itemButtonParent.transform.parent.parent.gameObject.SetActive(true);
		this.cardButtonParent.transform.parent.parent.gameObject.SetActive(false);
		this.backButton.SetActive(false);
	}

	// Token: 0x06000FAD RID: 4013 RVA: 0x00098D98 File Offset: 0x00096F98
	private void CreateButton(GameObject item)
	{
		Item2 component = item.GetComponent<Item2>();
		if (component && !component.isInAtlas)
		{
			return;
		}
		if (this.characterName != Character.CharacterName.Any && component.validForCharacters.Count > 0 && !component.validForCharacters.Contains(this.characterName))
		{
			return;
		}
		string displayName = Item2.GetDisplayName(item.name);
		if (displayName.ToLower() == "gold")
		{
			return;
		}
		if (MetaProgressSaveManager.main.FoundItem(displayName) || (Singleton.Instance.developmentMode && Singleton.Instance.unlockAllAtlas))
		{
			this.foundItems++;
			GameObject gameObject = Object.Instantiate<GameObject>(this.itemButton, Vector3.zero, Quaternion.identity, this.itemButtonParent);
			gameObject.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey(Item2.GetDisplayName(item.name));
			SpriteRenderer componentInChildren = item.GetComponentInChildren<SpriteRenderer>();
			gameObject.GetComponent<ItemAtlasButton>().SetSprite(componentInChildren, displayName);
		}
		this.totalItems++;
	}

	// Token: 0x06000FAE RID: 4014 RVA: 0x00098E98 File Offset: 0x00097098
	public void LoadResearch(string text)
	{
		foreach (GameObject gameObject in this.overworldResearchUI)
		{
			if (gameObject.name == text)
			{
				this.backButton.SetActive(true);
				this.itemButtonParent.transform.parent.parent.gameObject.SetActive(false);
				this.cardButtonParent.transform.parent.parent.gameObject.SetActive(true);
				foreach (object obj in this.cardButtonParent.transform)
				{
					Object.Destroy(((Transform)obj).gameObject);
				}
				Overworld_BuildingInterface component = gameObject.GetComponent<Overworld_BuildingInterface>();
				if (component)
				{
					foreach (GameObject gameObject2 in component.GetResearchBars())
					{
						gameObject2.transform.SetParent(this.cardButtonParent);
						gameObject2.transform.localScale = Vector3.one * 0.5f;
						gameObject2.GetComponent<RectTransform>().sizeDelta = new Vector2(200f, 240f);
					}
				}
				break;
			}
		}
	}

	// Token: 0x06000FAF RID: 4015 RVA: 0x00099050 File Offset: 0x00097250
	public void LoadCardPublic(string text)
	{
		ContextMenuManager contextMenuManager = Object.FindObjectOfType<ContextMenuManager>();
		if (contextMenuManager && contextMenuManager.currentState == ContextMenuManager.CurrentState.viewingCard)
		{
			return;
		}
		base.StartCoroutine(this.LoadCard(text));
	}

	// Token: 0x06000FB0 RID: 4016 RVA: 0x00099084 File Offset: 0x00097284
	private void AddCardToAtlasPage(GameObject item, Item2 item2, Sprite sprite)
	{
		GameObject gameObject = item.GetComponent<ItemMovement>().ShowCardDirect(item2, sprite);
		gameObject.transform.SetParent(this.cardButtonParent);
		gameObject.transform.localScale = Vector3.one;
		Card component = gameObject.GetComponent<Card>();
		component.MakeStuck();
		component.IgnoreShop();
		component.deleteOnDeactivate = false;
	}

	// Token: 0x06000FB1 RID: 4017 RVA: 0x000990D6 File Offset: 0x000972D6
	private IEnumerator LoadCard(string text)
	{
		if (this.finished)
		{
			yield break;
		}
		DebugItemManager debugItemManager = Object.FindObjectOfType<DebugItemManager>();
		if (this.tutorialManager && this.tutorialManager.playType == TutorialManager.PlayType.testing)
		{
			debugItemManager.GetItem2s();
		}
		new List<Item2>();
		List<Item2> item2s = debugItemManager.item2s;
		bool flag = false;
		if (!debugItemManager)
		{
			yield break;
		}
		Item2 item = DebugItemManager.main.GetItem2ByName(text);
		if (item)
		{
			this.backButton.SetActive(true);
			this.itemButtonParent.transform.parent.parent.gameObject.SetActive(false);
			this.cardButtonParent.transform.parent.parent.gameObject.SetActive(true);
			foreach (object obj in this.cardButtonParent.transform)
			{
				Object.Destroy(((Transform)obj).gameObject);
			}
			yield return new WaitForEndOfFrame();
			yield return new WaitForFixedUpdate();
			ItemSwitcher component = item.GetComponent<ItemSwitcher>();
			if (!component)
			{
				this.AddCardToAtlasPage(item.gameObject, item, null);
			}
			else
			{
				foreach (ItemSwitcher.Item2Change item2Change in component.GetAllItemChanges())
				{
					this.AddCardToAtlasPage(item.gameObject, item2Change.alternateItem, item2Change.sprite);
				}
			}
			List<string> list = new List<string>();
			string text2 = "";
			foreach (Character.CharacterName characterName in item.validForCharacters)
			{
				if (characterName == Character.CharacterName.Any)
				{
					list = new List<string>();
					break;
				}
				string text3 = characterName.ToString().ToLower();
				if (text3 == "cr8")
				{
					text3 = "CR-8";
				}
				text2 = text2 + "  " + LangaugeManager.main.GetTextByKey(text3) + "<br>";
			}
			if (text2.Length > 1)
			{
				Card componentInChildren = Object.Instantiate<GameObject>(this.additionalInfoPrefab, Vector3.zero, Quaternion.identity, this.cardButtonParent).GetComponentInChildren<Card>();
				list.Add("<size=90%>" + LangaugeManager.main.GetTextByKey("ia1") + "</size>");
				list.Add("<size=90%>" + text2 + "</size>");
				componentInChildren.GetDescriptionsSimple(list, base.gameObject);
			}
			List<string> list2 = new List<string>();
			text2 = "";
			foreach (DungeonLevel.Zone zone in item.validForZones)
			{
				string text4 = zone.ToString();
				if (text4.ToLower().Trim() == "ice")
				{
					text4 = "FrozenHeart";
				}
				text4 = Regex.Replace(text4, "[A-Z]", " $0");
				text2 = text2 + "  " + LangaugeManager.main.GetTextByKey(text4) + "<br>";
			}
			if (text2.Length > 1)
			{
				Card componentInChildren2 = Object.Instantiate<GameObject>(this.additionalInfoPrefab, Vector3.zero, Quaternion.identity, this.cardButtonParent).GetComponentInChildren<Card>();
				list2.Add("<size=90%>" + LangaugeManager.main.GetTextByKey("ia2") + "</size>");
				list2.Add("<size=90%>" + text2 + "</size>");
				componentInChildren2.GetDescriptionsSimple(list2, base.gameObject);
			}
			if (item.createdBy != "")
			{
				Object.Instantiate<GameObject>(this.additionalInfoPrefab, Vector3.zero, Quaternion.identity, this.cardButtonParent).GetComponentInChildren<Card>().GetDescriptionsSimple(new List<string>
				{
					LangaugeManager.main.GetTextByKey("ia3"),
					"<size=90%>  " + item.createdBy + "</size>"
				}, base.gameObject);
			}
			if (Singleton.Instance.developmentMode && this.tutorialManager && this.tutorialManager.playType == TutorialManager.PlayType.testing)
			{
				Object.Instantiate<GameObject>(item.gameObject, Vector3.zero, Quaternion.identity);
			}
			flag = true;
		}
		if (!flag)
		{
			foreach (GameObject gameObject in debugItemManager.enemies)
			{
				if (Item2.GetDisplayName(gameObject.name) == text)
				{
					this.backButton.SetActive(true);
					this.itemButtonParent.transform.parent.parent.gameObject.SetActive(false);
					this.cardButtonParent.transform.parent.parent.gameObject.SetActive(true);
					foreach (object obj2 in this.cardButtonParent.transform)
					{
						Object.Destroy(((Transform)obj2).gameObject);
					}
					Enemy component2 = gameObject.GetComponent<Enemy>();
					if (!component2)
					{
						yield break;
					}
					GameObject gameObject2 = Object.Instantiate<GameObject>(this.enemyCardPrefab, Vector3.zero, Quaternion.identity, this.cardButtonParent);
					component2.ApplyToCard(gameObject2);
					Card component3 = gameObject2.GetComponent<Card>();
					component3.stuck = true;
					component3.deleteOnDeactivate = false;
					flag = true;
					break;
				}
			}
		}
		if (!flag)
		{
			using (List<string>.Enumerator enumerator6 = MetaProgressSaveManager.main.loresUnlocked.GetEnumerator())
			{
				while (enumerator6.MoveNext())
				{
					if (enumerator6.Current == text)
					{
						this.backButton.SetActive(true);
						this.itemButtonParent.transform.parent.parent.gameObject.SetActive(false);
						this.cardButtonParent.transform.parent.parent.gameObject.SetActive(true);
						foreach (object obj3 in this.cardButtonParent.transform)
						{
							Object.Destroy(((Transform)obj3).gameObject);
						}
						Card component4 = Object.Instantiate<GameObject>(this.loreCardPrefab, Vector3.zero, Quaternion.identity, this.cardButtonParent).GetComponent<Card>();
						component4.GetDescriptionsLore(text, base.gameObject);
						component4.stuck = true;
						component4.deleteOnDeactivate = false;
						flag = true;
						break;
					}
				}
			}
		}
		if (!flag)
		{
			this.cardButtonParent.transform.parent.parent.gameObject.SetActive(true);
			foreach (object obj4 in this.cardButtonParent.transform)
			{
				Object.Destroy(((Transform)obj4).gameObject);
			}
			string text5 = LangaugeManager.main.GetTextByKey(text).Replace("/x", "?");
			text5 = Regex.Replace(text5, "\\s+", " ");
			Card component5 = Object.Instantiate<GameObject>(this.additionalInfoPrefab, Vector3.zero, Quaternion.identity, this.cardButtonParent).GetComponent<Card>();
			component5.GetDescriptionsSimple(new List<string> { text5 }, base.gameObject);
			component5.stuck = true;
			flag = true;
		}
		base.transform.SetAsFirstSibling();
		this.isViewing = true;
		while (this.isViewing)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000FB2 RID: 4018 RVA: 0x000990EC File Offset: 0x000972EC
	private void DestroyAllProxies()
	{
		if (this.tutorialManager && this.tutorialManager.playType == TutorialManager.PlayType.testing)
		{
			foreach (GameObject gameObject in this.spawnedObjects)
			{
				gameObject.transform.position = Vector3.zero;
			}
		}
	}

	// Token: 0x06000FB3 RID: 4019 RVA: 0x00099164 File Offset: 0x00097364
	public void EndEvent()
	{
		if (this.finished)
		{
			return;
		}
		this.DestroyAllProxies();
		this.finished = true;
		this.eventBoxAnimator.Play("Out");
		Singleton.Instance.showingOptions = false;
		GameManager main = GameManager.main;
		if (main)
		{
			main.viewingEvent = false;
			main.SetAllItemColliders(true);
		}
	}

	// Token: 0x06000FB4 RID: 4020 RVA: 0x000991C0 File Offset: 0x000973C0
	private void Update()
	{
		if (!this.eventBoxAnimator.gameObject.activeInHierarchy)
		{
			Object.Destroy(base.gameObject);
		}
		if (this.contextMenuManager && this.contextMenuManager.currentState == ContextMenuManager.CurrentState.viewingCard)
		{
			this.disableButton.SetActive(false);
			return;
		}
		this.disableButton.SetActive(true);
	}

	// Token: 0x04000CBA RID: 3258
	public bool disableSearchOnController = true;

	// Token: 0x04000CBB RID: 3259
	private ItemAtlas.ViewType viewType;

	// Token: 0x04000CBC RID: 3260
	[SerializeField]
	private GameObject loreButton;

	// Token: 0x04000CBD RID: 3261
	[SerializeField]
	private GameObject researchButton;

	// Token: 0x04000CBE RID: 3262
	[SerializeField]
	private GameObject itemButton;

	// Token: 0x04000CBF RID: 3263
	[SerializeField]
	private GameObject itemTypeButton;

	// Token: 0x04000CC0 RID: 3264
	[SerializeField]
	private GameObject additionalInfoPrefab;

	// Token: 0x04000CC1 RID: 3265
	[SerializeField]
	private Transform itemButtonParent;

	// Token: 0x04000CC2 RID: 3266
	[SerializeField]
	private Transform selectorButtonParent;

	// Token: 0x04000CC3 RID: 3267
	[SerializeField]
	private Transform cardButtonParent;

	// Token: 0x04000CC4 RID: 3268
	[SerializeField]
	private VerticalLayoutGroup cardButtonVerticalLayoutGroup;

	// Token: 0x04000CC5 RID: 3269
	[SerializeField]
	private GameObject disableButton;

	// Token: 0x04000CC6 RID: 3270
	[SerializeField]
	private GameObject searchField;

	// Token: 0x04000CC7 RID: 3271
	[SerializeField]
	private TMP_InputField inputField;

	// Token: 0x04000CC8 RID: 3272
	[SerializeField]
	private GameObject cardPrefab;

	// Token: 0x04000CC9 RID: 3273
	[SerializeField]
	private GameObject cardCarvingPrefab;

	// Token: 0x04000CCA RID: 3274
	[SerializeField]
	private GameObject petCardPrefab;

	// Token: 0x04000CCB RID: 3275
	[SerializeField]
	private GameObject enemyCardPrefab;

	// Token: 0x04000CCC RID: 3276
	[SerializeField]
	private GameObject loreCardPrefab;

	// Token: 0x04000CCD RID: 3277
	[SerializeField]
	private GameObject treatCardPrefab;

	// Token: 0x04000CCE RID: 3278
	[SerializeField]
	private TextMeshProUGUI itemCountNumber;

	// Token: 0x04000CCF RID: 3279
	[SerializeField]
	private GameObject backButton;

	// Token: 0x04000CD0 RID: 3280
	[NonSerialized]
	public Transform lastSelectedItem;

	// Token: 0x04000CD1 RID: 3281
	private bool finished;

	// Token: 0x04000CD2 RID: 3282
	private Animator eventBoxAnimator;

	// Token: 0x04000CD3 RID: 3283
	private ContextMenuManager contextMenuManager;

	// Token: 0x04000CD4 RID: 3284
	private TutorialManager tutorialManager;

	// Token: 0x04000CD5 RID: 3285
	private int foundItems;

	// Token: 0x04000CD6 RID: 3286
	private int totalItems;

	// Token: 0x04000CD7 RID: 3287
	private List<GameObject> spawnedObjects = new List<GameObject>();

	// Token: 0x04000CD8 RID: 3288
	[SerializeField]
	private Character.CharacterName characterName;

	// Token: 0x04000CD9 RID: 3289
	[SerializeField]
	private GameObject characterSelectMenu;

	// Token: 0x04000CDA RID: 3290
	private Item2.ItemType itemType;

	// Token: 0x04000CDB RID: 3291
	private string searchTerm = "";

	// Token: 0x04000CDC RID: 3292
	[SerializeField]
	public List<GameObject> overworldResearchUI;

	// Token: 0x04000CDD RID: 3293
	private List<Overworld_BuildingInterface> overworldResearch = new List<Overworld_BuildingInterface>();

	// Token: 0x04000CDE RID: 3294
	private bool isViewing;

	// Token: 0x0200045F RID: 1119
	public enum ViewType
	{
		// Token: 0x040019F6 RID: 6646
		items,
		// Token: 0x040019F7 RID: 6647
		enemies,
		// Token: 0x040019F8 RID: 6648
		help,
		// Token: 0x040019F9 RID: 6649
		lore,
		// Token: 0x040019FA RID: 6650
		terms,
		// Token: 0x040019FB RID: 6651
		research
	}
}
