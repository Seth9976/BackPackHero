using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

// Token: 0x0200019C RID: 412
public class StoreAtlas : MonoBehaviour
{
	// Token: 0x0600107F RID: 4223 RVA: 0x0009D456 File Offset: 0x0009B656
	private void Start()
	{
		DigitalCursor.main.Show();
		this.eventBoxAnimator = base.GetComponentInChildren<Animator>();
	}

	// Token: 0x06001080 RID: 4224 RVA: 0x0009D470 File Offset: 0x0009B670
	private void Update()
	{
		string text = (this.chosenItem ? Item2.GetDisplayName(this.chosenItem.name) : (this.chosenStructure ? Item2.GetDisplayName(this.chosenStructure.name) : (this.chosenSellingTile ? Item2.GetDisplayName(this.chosenSellingTile.name) : (this.chosenMission ? this.chosenMission.name : ""))));
		if (text == "" || ItemAtlasButton.IsOwned(text))
		{
			this.buyButton.gameObject.SetActive(false);
			this.resourceDisplayPanel.gameObject.SetActive(false);
		}
		else
		{
			this.buyButton.gameObject.SetActive(true);
		}
		if (!this.eventBoxAnimator.gameObject.activeInHierarchy)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001081 RID: 4225 RVA: 0x0009D564 File Offset: 0x0009B764
	public void SetupStoreContents(List<Overworld_ShopKeeper.ShopItem> _items)
	{
		foreach (Overworld_ShopKeeper.ShopItem shopItem in _items)
		{
			if (shopItem.unlockMarker == MetaProgressSaveManager.MetaProgressMarker.none || MetaProgressSaveManager.main.HasMetaProgressMarker(shopItem.unlockMarker))
			{
				this.CreateButton(shopItem);
			}
		}
	}

	// Token: 0x06001082 RID: 4226 RVA: 0x0009D5CC File Offset: 0x0009B7CC
	private void CreateButton(Overworld_ShopKeeper.ShopItem shopItem)
	{
		Missions mission = shopItem.mission;
		GameObject item = shopItem.item;
		if (item && !mission)
		{
			string displayName = Item2.GetDisplayName(item.name);
			Object.FindObjectOfType<MetaProgressSaveManager>();
			GameObject gameObject = Object.Instantiate<GameObject>(this.itemButton, Vector3.zero, Quaternion.identity, this.itemButtonParent);
			gameObject.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey(Item2.GetDisplayName(item.name));
			SpriteRenderer componentInChildren = item.GetComponentInChildren<SpriteRenderer>();
			ItemAtlasButton component = gameObject.GetComponent<ItemAtlasButton>();
			component.SetSprite(componentInChildren, displayName);
			component.costs = shopItem.cost;
			SellingTile component2 = item.GetComponent<SellingTile>();
			if (component2)
			{
				component.type = ItemAtlasButton.Type.tile;
				component.sellingTile = component2;
			}
			component.ConsiderOwned();
			return;
		}
		if (mission)
		{
			GameObject gameObject2 = Object.Instantiate<GameObject>(this.itemButton, Vector3.zero, Quaternion.identity, this.itemButtonParent);
			gameObject2.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey(mission.name);
			ItemAtlasButton component3 = gameObject2.GetComponent<ItemAtlasButton>();
			component3.itemImage.sprite = this.missionSymbol;
			gameObject2.GetComponentInChildren<TextMeshProUGUI>().text = Item2.GetDisplayName(Missions.MissionTranslationName(mission));
			component3.type = ItemAtlasButton.Type.mission;
			component3.mission = mission;
			component3.costs = shopItem.cost;
			component3.ConsiderOwned();
		}
	}

	// Token: 0x06001083 RID: 4227 RVA: 0x0009D72C File Offset: 0x0009B92C
	public void LoadCardPublic(string text)
	{
		ContextMenuManager contextMenuManager = Object.FindObjectOfType<ContextMenuManager>();
		if (contextMenuManager && contextMenuManager.currentState == ContextMenuManager.CurrentState.viewingCard)
		{
			return;
		}
		base.StartCoroutine(this.LoadCard(text));
	}

	// Token: 0x06001084 RID: 4228 RVA: 0x0009D760 File Offset: 0x0009B960
	public void SetMission(Missions mission)
	{
		foreach (object obj in this.cardButtonParent.transform)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
		this.chosenItem = null;
		this.chosenStructure = null;
		this.chosenSellingTile = null;
		this.chosenMission = mission;
	}

	// Token: 0x06001085 RID: 4229 RVA: 0x0009D7DC File Offset: 0x0009B9DC
	public void SetCosts(List<Overworld_ResourceManager.Resource> costs)
	{
		this.resourceDisplayPanel.gameObject.SetActive(true);
		this.resourceDisplayPanel.SetupResources(costs);
		this.costs = costs;
	}

	// Token: 0x06001086 RID: 4230 RVA: 0x0009D802 File Offset: 0x0009BA02
	private IEnumerator LoadCard(string text)
	{
		foreach (object obj in this.cardButtonParent.transform)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
		if (this.finished)
		{
			yield break;
		}
		this.chosenItem = null;
		this.chosenStructure = null;
		this.chosenSellingTile = null;
		this.chosenMission = null;
		DebugItemManager debugItemManager = Object.FindObjectOfType<DebugItemManager>();
		if (debugItemManager)
		{
			GameObject gameObject = null;
			foreach (GameObject gameObject2 in debugItemManager.items)
			{
				if (LangaugeManager.main.GetTextByKey(Item2.GetDisplayName(gameObject2.name)).ToLower().Trim() == text.ToLower().Trim())
				{
					this.spawnedObjects.Add(gameObject);
					Item2 item2 = gameObject2.GetComponent<Item2>();
					this.chosenItem = item2;
					yield return new WaitForEndOfFrame();
					yield return new WaitForFixedUpdate();
					GameObject gameObject3;
					if (item2.itemType.Contains(Item2.ItemType.Carving))
					{
						gameObject3 = Object.Instantiate<GameObject>(this.cardCarvingPrefab, Vector3.zero, Quaternion.identity, this.cardButtonParent);
						item2.GetComponent<ItemMovement>().ApplyCardToItem(gameObject3, null, null, false);
					}
					else if (item2.itemType.Contains(Item2.ItemType.Pet))
					{
						gameObject3 = Object.Instantiate<GameObject>(this.petCardPrefab, Vector3.zero, Quaternion.identity, this.cardButtonParent);
						item2.GetComponent<ItemMovement>().ApplyCardToItem(gameObject3, null, null, false);
					}
					else
					{
						gameObject3 = Object.Instantiate<GameObject>(this.cardPrefab, Vector3.zero, Quaternion.identity, this.cardButtonParent);
						item2.GetComponent<ItemMovement>().ApplyCardToItem(gameObject3, null, null, false);
					}
					Card component = gameObject3.GetComponent<Card>();
					component.stuck = true;
					component.deleteOnDeactivate = false;
					List<string> list = new List<string>();
					foreach (Character.CharacterName characterName in item2.validForCharacters)
					{
						if (characterName == Character.CharacterName.Any)
						{
							list = new List<string>();
							break;
						}
						string text2 = characterName.ToString().ToLower();
						if (text2 == "cr8")
						{
							text2 = "CR-8";
						}
						list.Add(LangaugeManager.main.GetTextByKey(text2));
					}
					if (list.Count > 0)
					{
						Card componentInChildren = Object.Instantiate<GameObject>(this.additionalInfoPrefab, Vector3.zero, Quaternion.identity, this.cardButtonParent).GetComponentInChildren<Card>();
						list.Insert(0, LangaugeManager.main.GetTextByKey("ia1"));
						componentInChildren.GetDescriptionsSimple(list, base.gameObject);
					}
					List<string> list2 = new List<string>();
					foreach (DungeonLevel.Zone zone in item2.validForZones)
					{
						string text3 = zone.ToString();
						text3 = Regex.Replace(text3, "[A-Z]", " $0");
						list2.Add(LangaugeManager.main.GetTextByKey(text3));
					}
					if (list2.Count > 0)
					{
						Card componentInChildren2 = Object.Instantiate<GameObject>(this.additionalInfoPrefab, Vector3.zero, Quaternion.identity, this.cardButtonParent).GetComponentInChildren<Card>();
						list2.Insert(0, LangaugeManager.main.GetTextByKey("ia2"));
						componentInChildren2.GetDescriptionsSimple(list2, base.gameObject);
						break;
					}
					break;
				}
			}
			List<GameObject>.Enumerator enumerator2 = default(List<GameObject>.Enumerator);
			Overworld_BuildingManager overworld_BuildingManager = Object.FindObjectOfType<Overworld_BuildingManager>();
			if (overworld_BuildingManager)
			{
				GameObject building = overworld_BuildingManager.GetBuilding(Item2.GetDisplayName(text));
				if (building)
				{
					Overworld_Structure component2 = building.GetComponent<Overworld_Structure>();
					GameObject gameObject4 = Overworld_CardManager.main.DisplayCard(component2, null);
					gameObject4.transform.SetParent(this.cardButtonParent);
					gameObject4.transform.localScale = Vector3.one;
					Card component3 = gameObject4.GetComponent<Card>();
					component3.stuck = true;
					component3.deleteOnDeactivate = false;
					this.chosenStructure = component2;
				}
				GameObject tileBase = overworld_BuildingManager.GetTileBase(Item2.GetDisplayName(text));
				if (tileBase)
				{
					SellingTile component4 = tileBase.GetComponent<SellingTile>();
					GameObject gameObject5 = Overworld_CardManager.main.DisplayCard(component4);
					gameObject5.transform.SetParent(this.cardButtonParent);
					gameObject5.transform.localScale = Vector3.one;
					Card component5 = gameObject5.GetComponent<Card>();
					component5.stuck = true;
					component5.deleteOnDeactivate = false;
					this.chosenSellingTile = component4;
				}
			}
			base.transform.SetAsFirstSibling();
		}
		yield break;
		yield break;
	}

	// Token: 0x06001087 RID: 4231 RVA: 0x0009D818 File Offset: 0x0009BA18
	public void Buy()
	{
		if (!Overworld_ResourceManager.main.HasEnoughResources(this.costs, -1))
		{
			MessageManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm35"));
			return;
		}
		Overworld_ResourceManager.ChangeResourceAmountBy(MetaProgressSaveManager.main.resources, this.costs, -1);
		if (this.chosenItem)
		{
			MetaProgressSaveManager.main.UnlockItem(this.chosenItem);
			Overworld_Manager.main.OpenNewItemWindow(this.chosenItem);
		}
		if (this.chosenStructure)
		{
			Overworld_BuildingManager.main.AddBuilding(Item2.GetDisplayName(this.chosenStructure.name));
			Overworld_Manager.main.OpenNewConstructionWindow(this.chosenStructure);
		}
		if (this.chosenSellingTile)
		{
			Overworld_BuildingManager.main.AddTile(Item2.GetDisplayName(this.chosenSellingTile.name));
			Overworld_Manager.main.OpenNewConstructionWindow(this.chosenSellingTile);
		}
		if (this.chosenMission)
		{
			MetaProgressSaveManager.main.AddMission(this.chosenMission);
			Overworld_Manager.main.OpenNewMissionWindow(this.chosenMission);
		}
		ItemAtlasButton.AllButtonsConsiderOwned();
	}

	// Token: 0x06001088 RID: 4232 RVA: 0x0009D938 File Offset: 0x0009BB38
	public void EndEvent()
	{
		DigitalCursor.main.Hide();
		SingleUI component = base.GetComponent<SingleUI>();
		if (component)
		{
			component.CloseAndDestroy();
		}
		if (this.finished)
		{
			return;
		}
		this.finished = true;
		this.eventBoxAnimator.Play("Out");
		GameManager main = GameManager.main;
		if (main)
		{
			main.viewingEvent = false;
			main.SetAllItemColliders(true);
		}
		if (this.fromConversation)
		{
			return;
		}
		Overworld_ConversationManager main2 = Overworld_ConversationManager.main;
		if (main2)
		{
			main2.ReleaseSpeaker();
		}
		Overworld_Manager main3 = Overworld_Manager.main;
		if (main3)
		{
			main3.SetState(Overworld_Manager.State.MOVING);
		}
	}

	// Token: 0x04000D66 RID: 3430
	[SerializeField]
	private Sprite missionSymbol;

	// Token: 0x04000D67 RID: 3431
	[SerializeField]
	private GameObject itemButton;

	// Token: 0x04000D68 RID: 3432
	[SerializeField]
	private Transform itemButtonParent;

	// Token: 0x04000D69 RID: 3433
	[SerializeField]
	private List<GameObject> items;

	// Token: 0x04000D6A RID: 3434
	[SerializeField]
	private Overworld_ResourceDisplayPanel resourceDisplayPanel;

	// Token: 0x04000D6B RID: 3435
	[SerializeField]
	private GameObject cardPrefab;

	// Token: 0x04000D6C RID: 3436
	[SerializeField]
	private GameObject cardCarvingPrefab;

	// Token: 0x04000D6D RID: 3437
	[SerializeField]
	private GameObject petCardPrefab;

	// Token: 0x04000D6E RID: 3438
	[SerializeField]
	public Transform cardButtonParent;

	// Token: 0x04000D6F RID: 3439
	[SerializeField]
	private GameObject additionalInfoPrefab;

	// Token: 0x04000D70 RID: 3440
	private List<GameObject> spawnedObjects = new List<GameObject>();

	// Token: 0x04000D71 RID: 3441
	private Animator eventBoxAnimator;

	// Token: 0x04000D72 RID: 3442
	public bool finished;

	// Token: 0x04000D73 RID: 3443
	public bool fromConversation;

	// Token: 0x04000D74 RID: 3444
	private Item2 chosenItem;

	// Token: 0x04000D75 RID: 3445
	private Overworld_Structure chosenStructure;

	// Token: 0x04000D76 RID: 3446
	private SellingTile chosenSellingTile;

	// Token: 0x04000D77 RID: 3447
	private Missions chosenMission;

	// Token: 0x04000D78 RID: 3448
	[SerializeField]
	private GameObject buyButton;

	// Token: 0x04000D79 RID: 3449
	private List<Overworld_ResourceManager.Resource> costs = new List<Overworld_ResourceManager.Resource>();
}
