using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000AF RID: 175
public class Tote : MonoBehaviour
{
	// Token: 0x0600043B RID: 1083 RVA: 0x00029AEB File Offset: 0x00027CEB
	private void Awake()
	{
		Tote.main = this;
	}

	// Token: 0x0600043C RID: 1084 RVA: 0x00029AF3 File Offset: 0x00027CF3
	private void OnDestroy()
	{
		if (Tote.main == this)
		{
			Tote.main = null;
		}
	}

	// Token: 0x0600043D RID: 1085 RVA: 0x00029B08 File Offset: 0x00027D08
	private void Update()
	{
	}

	// Token: 0x0600043E RID: 1086 RVA: 0x00029B0C File Offset: 0x00027D0C
	public int CalculateClearTotalDamage()
	{
		int count = this.GetActiveCarvings().Count;
		int num = 0;
		if (Item2.GetItemStatusEffectBool(Item2.ItemStatusEffect.Type.crackedBottle))
		{
			for (int i = 0; i < count; i++)
			{
				num += i + 1;
			}
		}
		else
		{
			num = count;
		}
		if (Item2.GetItemStatusEffectBool(Item2.ItemStatusEffect.Type.onFullInventory) && GridSquare.BackpackIsFull())
		{
			num *= 2;
		}
		return num;
	}

	// Token: 0x0600043F RID: 1087 RVA: 0x00029B5C File Offset: 0x00027D5C
	public void ShowDamageValue()
	{
		TextMeshProUGUI component = Object.Instantiate<GameObject>(this.damagePrefab, new Vector3(-9.3f, 4.1f, 0f), Quaternion.identity, GameObject.FindGameObjectWithTag("SecondaryCanvas").transform).GetComponent<TextMeshProUGUI>();
		if (component)
		{
			int num = 1;
			if (Item2.GetItemStatusEffectBool(Item2.ItemStatusEffect.Type.crackedBottle))
			{
				num = this.GetActiveCarvings().Count;
			}
			if (Item2.GetItemStatusEffectBool(Item2.ItemStatusEffect.Type.onFullInventory) && GridSquare.BackpackIsFull())
			{
				num += this.CalculateClearTotalDamage() / 2;
			}
			component.text = component.text.Replace("/x", num.ToString() ?? "");
		}
	}

	// Token: 0x06000440 RID: 1088 RVA: 0x00029C02 File Offset: 0x00027E02
	public void StartCombat()
	{
		this.DiscardHand(false);
		this.RecycleCarvings();
		this.decks.gameObject.SetActive(true);
	}

	// Token: 0x06000441 RID: 1089 RVA: 0x00029C22 File Offset: 0x00027E22
	public void DrawNewHand()
	{
		this.DiscardHand(false);
		this.DrawCarvingsFromUndrawn(5);
	}

	// Token: 0x06000442 RID: 1090 RVA: 0x00029C34 File Offset: 0x00027E34
	public void StartTurn()
	{
		int num = 5;
		if (this.gameFlowManager.turnNumber == 1)
		{
			this.DrawNaturalCarvings(ref num);
			GameObject gameObject = this.DrawCarvingOfType(Item2.ItemType.Weapon);
			if (gameObject && num > 0)
			{
				this.DrawCarving(gameObject);
				num--;
			}
			GameObject gameObject2 = this.DrawCarvingOfType(Item2.ItemType.Shield);
			if (gameObject2 && num > 0)
			{
				this.DrawCarving(gameObject2);
				num--;
			}
		}
		this.DrawCarvingsFromUndrawn(num);
	}

	// Token: 0x06000443 RID: 1091 RVA: 0x00029CA0 File Offset: 0x00027EA0
	private void CreateSpacer(float height)
	{
		Transform transform = GameObject.FindGameObjectWithTag("CarvingSpacerSliderContent").transform;
		UICarvingSpacer component = Object.Instantiate<GameObject>(this.spacerPrefab, Vector3.zero, Quaternion.identity, transform).GetComponent<UICarvingSpacer>();
		component.SetWidth(height);
		component.Remove();
	}

	// Token: 0x06000444 RID: 1092 RVA: 0x00029CE4 File Offset: 0x00027EE4
	public List<GameObject> GetActiveCarvings()
	{
		List<GameObject> list = new List<GameObject>();
		foreach (GameObject gameObject in this.drawnCarvings)
		{
			ItemMovement component = gameObject.GetComponent<ItemMovement>();
			if (gameObject.activeSelf && component && component.inGrid)
			{
				list.Add(gameObject);
			}
		}
		return list;
	}

	// Token: 0x06000445 RID: 1093 RVA: 0x00029D60 File Offset: 0x00027F60
	public List<Item2> GetAllCarvings()
	{
		List<Item2> list = new List<Item2>();
		list.AddRange(this.undrawnCarvings.ConvertAll<Item2>((GameObject x) => x.GetComponent<Item2>()));
		list.AddRange(this.drawnCarvings.ConvertAll<Item2>((GameObject x) => x.GetComponent<Item2>()));
		list.AddRange(this.discardedCarvings.ConvertAll<Item2>((GameObject x) => x.GetComponent<Item2>()));
		return list;
	}

	// Token: 0x06000446 RID: 1094 RVA: 0x00029E02 File Offset: 0x00028002
	public void EndTurn()
	{
		this.DiscardHand(false);
	}

	// Token: 0x06000447 RID: 1095 RVA: 0x00029E0B File Offset: 0x0002800B
	public void EndCombat()
	{
		this.MoveAllToUndrawn();
		this.RemoveAllModifiers();
		this.decks.gameObject.SetActive(false);
	}

	// Token: 0x06000448 RID: 1096 RVA: 0x00029E2C File Offset: 0x0002802C
	public void RemoveAllModifiers()
	{
		this.CheckDeck(this.undrawnCarvings);
		foreach (GameObject gameObject in this.undrawnCarvings)
		{
			Item2 component = gameObject.GetComponent<Item2>();
			if (component)
			{
				component.RemoveModifiers(new List<Item2.Modifier.Length>
				{
					Item2.Modifier.Length.whileActive,
					Item2.Modifier.Length.forTurn,
					Item2.Modifier.Length.forCombat
				}, -999);
			}
		}
	}

	// Token: 0x06000449 RID: 1097 RVA: 0x00029EB8 File Offset: 0x000280B8
	public bool CardIsOwned(GameObject card)
	{
		return this.undrawnCarvings.Contains(card) || this.drawnCarvings.Contains(card) || this.banishedCarvings.Contains(card) || this.discardedCarvings.Contains(card);
	}

	// Token: 0x0600044A RID: 1098 RVA: 0x00029EF8 File Offset: 0x000280F8
	public GameObject DrawCarvingOfType(Item2.ItemType type)
	{
		List<GameObject> list = new List<GameObject>();
		this.CheckDeck(this.undrawnCarvings);
		foreach (GameObject gameObject in this.undrawnCarvings)
		{
			Item2 component = gameObject.GetComponent<Item2>();
			if (component && component.itemType.Contains(type))
			{
				list.Add(gameObject);
			}
		}
		if (list.Count > 0)
		{
			return Item2.ChooseRandomFromList(list, false);
		}
		return null;
	}

	// Token: 0x0600044B RID: 1099 RVA: 0x00029F8C File Offset: 0x0002818C
	public void DrawNaturalCarvings(ref int num)
	{
		List<GameObject> list = new List<GameObject>();
		using (List<GameObject>.Enumerator enumerator = this.undrawnCarvings.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				GameObject gameObject = enumerator.Current;
				Item2 component = gameObject.GetComponent<Item2>();
				if (component && component.CheckForStatusEffect(Item2.ItemStatusEffect.Type.Natural))
				{
					list.Add(gameObject);
				}
			}
			goto IL_0080;
		}
		IL_0055:
		this.CheckDeck(this.undrawnCarvings);
		GameObject gameObject2 = Item2.ChooseRandomAndRemoveFromList(list);
		if (gameObject2)
		{
			this.DrawCarving(gameObject2);
			num--;
		}
		IL_0080:
		if (list.Count <= 0)
		{
			return;
		}
		goto IL_0055;
	}

	// Token: 0x0600044C RID: 1100 RVA: 0x0002A034 File Offset: 0x00028234
	public void DrawCarvingsFromUndrawn(int num)
	{
		for (int i = 0; i < num; i++)
		{
			this.CheckDeck(this.undrawnCarvings);
			if (this.undrawnCarvings.Count <= 0)
			{
				this.RecycleCarvings();
				this.CheckDeck(this.undrawnCarvings);
				if (this.undrawnCarvings.Count <= 0)
				{
					break;
				}
			}
			this.CheckDeck(this.undrawnCarvings);
			GameObject gameObject = Item2.ChooseRandomAndRemoveFromList(this.undrawnCarvings);
			this.DrawCarving(gameObject);
		}
	}

	// Token: 0x0600044D RID: 1101 RVA: 0x0002A0A8 File Offset: 0x000282A8
	public void AddNewCarvingToUndrawn(GameObject carving)
	{
		if (GameManager.main.IsConsideringCurseReplacement())
		{
			return;
		}
		SoundManager.main.PlaySFX("wheelSFX");
		if (this.undrawnCarvingsIcon)
		{
			this.CreateProxy(carving, carving.transform.position, this.undrawnCarvingsIcon.transform).ChangeSettings(Vector3.one, Vector3.zero, carving.transform.rotation, 0.6f);
		}
		Item2 component = carving.GetComponent<Item2>();
		if (component)
		{
			component.isForSale = false;
		}
		carving.SetActive(false);
		this.undrawnCarvings.Add(carving);
		if (this.undrawnCarvings.Count >= 6 && this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.toteChestOpen)
		{
			this.gameManager.FinishReorganizeButton();
			this.gameManager.StartCoroutine(this.gameManager.HidePromptText(this.gameManager.tutorialText, -240f));
			this.tutorialManager.tutorialSequence = TutorialManager.TutorialSequence.trulyDone;
		}
		this.UpdateShowingList();
		GameManager.main.SetItemsAllowToTake();
	}

	// Token: 0x0600044E RID: 1102 RVA: 0x0002A1B0 File Offset: 0x000283B0
	public void DrawCarving(GameObject drawnCarving)
	{
		drawnCarving.SetActive(true);
		Item2 component = drawnCarving.GetComponent<Item2>();
		ItemMovement component2 = drawnCarving.GetComponent<ItemMovement>();
		if (!component || component.destroyed || !component2)
		{
			drawnCarving.SetActive(false);
			return;
		}
		component2.RemoveGridObjectPositions();
		if (component && component.gridObject)
		{
			component.gridObject.ClearGridPositions();
		}
		if (this.undrawnCarvings.Contains(drawnCarving))
		{
			this.undrawnCarvings.Remove(drawnCarving);
		}
		this.CheckDeck(this.drawnCarvings);
		if (!this.drawnCarvings.Contains(drawnCarving))
		{
			this.drawnCarvings.Add(drawnCarving);
		}
		drawnCarving.transform.position = new Vector3(-999f, -999f, 0f);
		component.RemoveModifiers(new List<Item2.Modifier.Length>
		{
			Item2.Modifier.Length.whileActive,
			Item2.Modifier.Length.forTurn
		}, -999);
		if (component.itemMovement)
		{
			component.itemMovement.inGrid = false;
			component.itemMovement.StopAllCoroutines();
			component.itemMovement.RemoveFromGrid();
			component.itemMovement.returnsToOutOfInventoryPosition = false;
		}
		SpriteRenderer component3 = component.GetComponent<SpriteRenderer>();
		if (component3)
		{
			component3.enabled = true;
		}
		component.SetColor();
		Transform transform = GameObject.FindGameObjectWithTag("CarvingSpacerSliderContent").transform;
		Carving component4 = drawnCarving.GetComponent<Carving>();
		component4.ResetFirstFrames();
		component4.myUIlocation = Object.Instantiate<GameObject>(this.spacerPrefab, Vector3.zero, Quaternion.identity, transform).transform;
		component4.myUIlocation.GetComponent<UICarvingSpacer>().SetWidth(300f);
		this.gameFlowManager.CheckConstants();
		component4.EndMoveToCardViewer();
		base.StartCoroutine(this.DrawCarvingOverTime(drawnCarving, component4.myUIlocation.transform));
		this.UpdateShowingList();
	}

	// Token: 0x0600044F RID: 1103 RVA: 0x0002A376 File Offset: 0x00028576
	private IEnumerator DrawCarvingOverTime(GameObject drawnCarving, Transform tt)
	{
		drawnCarving.transform.localScale = Vector3.zero;
		yield return new WaitForEndOfFrame();
		this.CreateProxy(drawnCarving, this.undrawnCarvingsIcon.transform.position, tt).ChangeSettings(Vector3.one * 0.5f, Vector3.one, Quaternion.Euler(0f, 0f, 180f), 0.4f);
		yield return new WaitForSeconds(0.38f);
		this.gameManager.AddParticles(drawnCarving.transform.position, drawnCarving.GetComponent<SpriteRenderer>(), this.gameManager.carvingSummonParticles);
		drawnCarving.GetComponent<Carving>().ResetFirstFrames();
		yield break;
	}

	// Token: 0x06000450 RID: 1104 RVA: 0x0002A393 File Offset: 0x00028593
	public void MoveAllToUndrawn()
	{
		this.RecycleCarvings();
		this.ReturnBanishedToDeck();
		this.RemoveAllDrawnCarvingsToHand();
	}

	// Token: 0x06000451 RID: 1105 RVA: 0x0002A3A8 File Offset: 0x000285A8
	public void DrawAllCarvings()
	{
		this.DiscardHand(true);
		this.RecycleCarvings();
		this.RemoveAllDrawnCarvingsToHand();
		this.ReturnBanishedToDeck();
		this.DrawCarvingsFromUndrawn(this.undrawnCarvings.Count);
		Store store = Object.FindObjectOfType<Store>();
		if (store)
		{
			store.SetPrices();
		}
	}

	// Token: 0x06000452 RID: 1106 RVA: 0x0002A3F4 File Offset: 0x000285F4
	public List<GameObject> GetAllCards()
	{
		List<GameObject> list = new List<GameObject>();
		foreach (GameObject gameObject in this.undrawnCarvings)
		{
			list.Add(gameObject);
		}
		foreach (GameObject gameObject2 in this.drawnCarvings)
		{
			list.Add(gameObject2);
		}
		foreach (GameObject gameObject3 in this.discardedCarvings)
		{
			list.Add(gameObject3);
		}
		foreach (GameObject gameObject4 in this.banishedCarvings)
		{
			list.Add(gameObject4);
		}
		return list;
	}

	// Token: 0x06000453 RID: 1107 RVA: 0x0002A518 File Offset: 0x00028718
	public int GetCarvingsInDiscard()
	{
		return this.discardedCarvings.Count;
	}

	// Token: 0x06000454 RID: 1108 RVA: 0x0002A525 File Offset: 0x00028725
	public int GetCarvingsUndrawn()
	{
		return this.undrawnCarvings.Count;
	}

	// Token: 0x06000455 RID: 1109 RVA: 0x0002A532 File Offset: 0x00028732
	public int GetCarvingsInHand()
	{
		return this.drawnCarvings.Count;
	}

	// Token: 0x06000456 RID: 1110 RVA: 0x0002A540 File Offset: 0x00028740
	public int GetCarvingsInHandNotPlayed()
	{
		List<GameObject> list = new List<GameObject>(this.drawnCarvings);
		int num = 0;
		foreach (GameObject gameObject in list)
		{
			Item2 component = gameObject.GetComponent<Item2>();
			if (component && component.itemMovement && !component.itemMovement.inGrid)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06000457 RID: 1111 RVA: 0x0002A5C0 File Offset: 0x000287C0
	public void DiscardHand(bool discardInGrid = false)
	{
		for (int i = 0; i < this.drawnCarvings.Count; i++)
		{
			int count = this.drawnCarvings.Count;
			GameObject gameObject = this.drawnCarvings[i];
			if (gameObject)
			{
				Item2 component = gameObject.GetComponent<Item2>();
				if (component && component.itemMovement && (discardInGrid || !component || !component.itemMovement || !component.itemMovement.inGrid))
				{
					component.itemMovement.RemoveGridObjectPositions();
					if (component)
					{
						GameFlowManager.main.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.whenNotPlayed, component, null, true, false);
					}
					this.DiscardCarving(this.drawnCarvings[i]);
					if (count != this.drawnCarvings.Count)
					{
						i--;
					}
				}
			}
		}
	}

	// Token: 0x06000458 RID: 1112 RVA: 0x0002A698 File Offset: 0x00028898
	public void RemoveCarvingFromLists(GameObject card)
	{
		if (this.drawnCarvings.Contains(card))
		{
			this.drawnCarvings.Remove(card);
		}
		if (this.undrawnCarvings.Contains(card))
		{
			this.undrawnCarvings.Remove(card);
		}
		if (this.banishedCarvings.Contains(card))
		{
			this.banishedCarvings.Remove(card);
		}
		this.UpdateShowingList();
	}

	// Token: 0x06000459 RID: 1113 RVA: 0x0002A6FC File Offset: 0x000288FC
	public void DeleteCarving(GameObject card)
	{
		this.RemoveCarvingFromLists(card);
		card.GetComponent<ItemMovement>().DelayDestroy();
		Object.Destroy(card);
	}

	// Token: 0x0600045A RID: 1114 RVA: 0x0002A718 File Offset: 0x00028918
	public void DiscardCarving(GameObject card)
	{
		Item2 component = card.GetComponent<Item2>();
		ItemMovement component2 = card.GetComponent<ItemMovement>();
		Transform myUIlocation = card.GetComponent<Carving>().myUIlocation;
		if (!component || !component2)
		{
			return;
		}
		component2.RemoveGridObjectPositions();
		if (component2 && component2.inGrid)
		{
			component2.RemoveFromGrid();
			if (component)
			{
				this.gameFlowManager.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onDiscard, component, null, true, false);
			}
		}
		if (myUIlocation)
		{
			myUIlocation.GetComponent<UICarvingSpacer>().Remove();
		}
		if (component)
		{
			component.RemoveModifiers(new List<Item2.Modifier.Length>
			{
				Item2.Modifier.Length.whileActive,
				Item2.Modifier.Length.untilDiscard
			}, -1);
		}
		if (this.banishedCarvings.Contains(card))
		{
			return;
		}
		this.CheckDeck(this.drawnCarvings);
		this.CheckDeck(this.discardedCarvings);
		if (this.drawnCarvings.Contains(card))
		{
			this.drawnCarvings.Remove(card);
		}
		if (!this.discardedCarvings.Contains(card))
		{
			this.discardedCarvings.Add(card);
		}
		if (this.discardedCarvingsText && this.discardedCarvingsText.gameObject.activeInHierarchy && this.discardedCarvingsText)
		{
			this.CreateProxy(card, card.transform.position, this.discardedCarvingsText.transform);
		}
		this.UpdateShowingList();
	}

	// Token: 0x0600045B RID: 1115 RVA: 0x0002A868 File Offset: 0x00028A68
	public ItemAnimationProxy CreateProxy(GameObject card, Vector3 start, Transform destination)
	{
		if (!card.GetComponent<SpriteRenderer>())
		{
			return null;
		}
		ItemAnimationProxy component = Object.Instantiate<GameObject>(this.itemAnimationProxyPrefab, card.transform.position, card.transform.rotation).GetComponent<ItemAnimationProxy>();
		component.CopySprite(card.GetComponent<SpriteRenderer>());
		component.FlyTo(start, destination);
		return component;
	}

	// Token: 0x0600045C RID: 1116 RVA: 0x0002A8C4 File Offset: 0x00028AC4
	public void ButtonForShowItems(int num)
	{
		if (num == 0)
		{
			this.ShowAllFromList(this.undrawnCarvings, "gmTote2");
			return;
		}
		if (num == 1)
		{
			this.ShowAllFromList(this.discardedCarvings, "gmTote3");
			return;
		}
		if (num == 2)
		{
			this.ShowAllFromList(this.banishedCarvings, "gmTote4");
			return;
		}
		if (num == 3)
		{
			foreach (GameObject gameObject in this.drawnCarvings)
			{
				ItemMovement component = gameObject.GetComponent<ItemMovement>();
				Item2 component2 = gameObject.GetComponent<Item2>();
				if (component && !component.inGrid && component2 && !component2.destroyed)
				{
					if (gameObject.transform.localScale.x < 0.9f)
					{
						gameObject.transform.localScale = Vector3.one;
					}
					else
					{
						gameObject.transform.localScale = Vector3.zero;
					}
				}
			}
		}
	}

	// Token: 0x0600045D RID: 1117 RVA: 0x0002A9C0 File Offset: 0x00028BC0
	public void SelectCarvingForFromEvent(EventButton eventButton, bool willDestroy)
	{
		this.gameManager.BeginSelectPeriod(eventButton, willDestroy);
		this.gameManager.viewingEvent = false;
		this.ShowAllFromList(this.undrawnCarvings, "gmTote2");
	}

	// Token: 0x0600045E RID: 1118 RVA: 0x0002A9EC File Offset: 0x00028BEC
	private void ReplaceInList(GameObject oldObj, GameObject newObj, ref List<GameObject> objs)
	{
		int num = objs.FindIndex((GameObject x) => x == oldObj);
		if (num != -1)
		{
			objs[num] = newObj;
		}
	}

	// Token: 0x0600045F RID: 1119 RVA: 0x0002AA27 File Offset: 0x00028C27
	public void ReplaceInLists(GameObject oldObj, GameObject newObj)
	{
		this.ReplaceInList(oldObj, newObj, ref this.discardedCarvings);
		this.ReplaceInList(oldObj, newObj, ref this.undrawnCarvings);
		this.ReplaceInList(oldObj, newObj, ref this.drawnCarvings);
		this.ReplaceInList(oldObj, newObj, ref this.banishedCarvings);
	}

	// Token: 0x06000460 RID: 1120 RVA: 0x0002AA64 File Offset: 0x00028C64
	private void UpdateShowingList()
	{
		if (this.showingList.Count == 0)
		{
			return;
		}
		if (!this.currentListParent)
		{
			return;
		}
		if (this.showingList == this.discardedCarvings)
		{
			this.UpdateList(this.discardedCarvings);
			return;
		}
		if (this.showingList == this.undrawnCarvings)
		{
			this.UpdateList(this.undrawnCarvings);
			return;
		}
		if (this.showingList == this.banishedCarvings)
		{
			this.UpdateList(this.banishedCarvings);
		}
	}

	// Token: 0x06000461 RID: 1121 RVA: 0x0002AAE0 File Offset: 0x00028CE0
	private void UpdateList(List<GameObject> objs)
	{
		if (!this.currentListParent)
		{
			return;
		}
		foreach (object obj in this.currentListParent)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
		foreach (GameObject gameObject in objs)
		{
			gameObject.SetActive(true);
			Carving component = gameObject.GetComponent<Carving>();
			component.myUIlocation = Object.Instantiate<GameObject>(this.spacerPrefabUIRedo, Vector3.zero, Quaternion.identity, this.currentListParent).transform;
			component.MoveToCardViewer();
			UICarvingIndicator component2 = component.myUIlocation.GetComponent<UICarvingIndicator>();
			Item2 component3 = gameObject.GetComponent<Item2>();
			component2.Setup(component3);
		}
	}

	// Token: 0x06000462 RID: 1122 RVA: 0x0002ABD4 File Offset: 0x00028DD4
	public void ShowAllFromList(List<GameObject> objs, string text)
	{
		if (this.gameManager.viewingEvent)
		{
			return;
		}
		this.showingList = objs;
		GameObject gameObject = GameObject.FindGameObjectWithTag("PopUpSpriteMask");
		if (gameObject)
		{
			gameObject.GetComponent<SpriteMask>().enabled = true;
		}
		Transform transform = GameObject.FindGameObjectWithTag("FrontCanvas").transform;
		GameObject gameObject2 = Object.Instantiate<GameObject>(this.cardOverlayPrefab, Vector3.zero, Quaternion.identity, transform);
		gameObject2.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey(text);
		Transform transform2 = gameObject2.GetComponentInChildren<GridLayoutGroup>().transform;
		this.currentListParent = transform2;
		this.UpdateList(objs);
	}

	// Token: 0x06000463 RID: 1123 RVA: 0x0002AC6C File Offset: 0x00028E6C
	public void BanishCarving(GameObject card)
	{
		Item2 component = card.GetComponent<Item2>();
		ItemMovement component2 = card.GetComponent<ItemMovement>();
		if (component2 && component2.inGrid)
		{
			component2.RemoveFromGrid();
		}
		if (component2)
		{
			component2.RemoveGridObjectPositions();
		}
		Transform myUIlocation = card.GetComponent<Carving>().myUIlocation;
		if (myUIlocation)
		{
			myUIlocation.GetComponent<UICarvingSpacer>().Remove();
		}
		this.CheckDeck(this.drawnCarvings);
		this.CheckDeck(this.discardedCarvings);
		this.CheckDeck(this.banishedCarvings);
		if (this.drawnCarvings.Contains(card))
		{
			this.drawnCarvings.Remove(card);
		}
		if (this.discardedCarvings.Contains(card))
		{
			this.discardedCarvings.Remove(card);
		}
		if (!this.banishedCarvings.Contains(card))
		{
			this.banishedCarvings.Add(card);
		}
		if (component)
		{
			component.StopAllCoroutines();
			component.RemoveModifiers(new List<Item2.Modifier.Length> { Item2.Modifier.Length.whileActive }, -1);
		}
		this.CreateProxy(card, card.transform.position, this.banishedCarvingsText.transform);
		if (this.tutorialManager)
		{
			this.tutorialManager.ConsiderTutorial("toteBanish");
		}
		this.UpdateShowingList();
	}

	// Token: 0x06000464 RID: 1124 RVA: 0x0002ADA0 File Offset: 0x00028FA0
	private void CheckDeck(List<GameObject> list)
	{
		for (int i = 0; i < list.Count; i++)
		{
			if (!list[i])
			{
				list.RemoveAt(i);
				i--;
			}
		}
		for (int j = 0; j < list.Count; j++)
		{
			for (int k = j + 1; k < list.Count; k++)
			{
				if (list[j] == list[k])
				{
					list.RemoveAt(k);
					k--;
				}
			}
		}
	}

	// Token: 0x06000465 RID: 1125 RVA: 0x0002AE1C File Offset: 0x0002901C
	public void RecycleCarvings()
	{
		this.CheckDeck(this.discardedCarvings);
		while (this.discardedCarvings.Count > 0)
		{
			GameObject gameObject = Item2.ChooseRandomAndRemoveFromList(this.discardedCarvings);
			if (gameObject)
			{
				Item2 component = gameObject.GetComponent<Item2>();
				if (component && !component.destroyed)
				{
					Carving component2 = gameObject.GetComponent<Carving>();
					if (component2)
					{
						component2.EndMoveToCardViewer();
					}
					if (!this.undrawnCarvings.Contains(gameObject))
					{
						this.undrawnCarvings.Add(gameObject);
					}
					if (this.undrawnCarvingsText && this.undrawnCarvingsText.gameObject.activeInHierarchy)
					{
						this.CreateProxy(gameObject, this.discardedCarvingsText.transform.position, this.undrawnCarvingsText.transform);
					}
				}
			}
		}
	}

	// Token: 0x06000466 RID: 1126 RVA: 0x0002AEEC File Offset: 0x000290EC
	public void ReturnBanishedToDeck()
	{
		this.CheckDeck(this.banishedCarvings);
		while (this.banishedCarvings.Count > 0)
		{
			GameObject gameObject = Item2.ChooseRandomAndRemoveFromList(this.banishedCarvings);
			this.undrawnCarvings.Add(gameObject);
			Carving component = gameObject.GetComponent<Carving>();
			if (component)
			{
				component.EndMoveToCardViewer();
			}
			if (this.banishedCarvingsText)
			{
				this.CreateProxy(gameObject, this.banishedCarvingsText.transform.position, this.undrawnCarvingsText.transform);
			}
		}
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x0002AF72 File Offset: 0x00029172
	public void ResetScrollRect()
	{
		this.scrollRect.GetComponentInChildren<VerticalLayoutGroup>().GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 271f);
	}

	// Token: 0x06000468 RID: 1128 RVA: 0x0002AF98 File Offset: 0x00029198
	public void AddCarvingToDeck(GameObject newCarving)
	{
		this.undrawnCarvings.Add(Object.Instantiate<GameObject>(newCarving, new Vector3(-999f, -999f, 0f), Quaternion.identity));
	}

	// Token: 0x06000469 RID: 1129 RVA: 0x0002AFC4 File Offset: 0x000291C4
	public void AddCarving(GameObject carving)
	{
		this.drawnCarvings.Add(carving);
	}

	// Token: 0x0600046A RID: 1130 RVA: 0x0002AFD2 File Offset: 0x000291D2
	public bool IsDrawn(GameObject x)
	{
		return !this.discardedCarvings.Contains(x) && !this.undrawnCarvings.Contains(x) && !this.banishedCarvings.Contains(x);
	}

	// Token: 0x0600046B RID: 1131 RVA: 0x0002B004 File Offset: 0x00029204
	public bool ThereIsAPlayedCarving()
	{
		List<GameObject> list = new List<GameObject>(this.drawnCarvings);
		while (list.Count > 0)
		{
			GameObject gameObject = list[0];
			list.RemoveAt(0);
			ItemMovement component = gameObject.GetComponent<ItemMovement>();
			if (component && component.inGrid)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600046C RID: 1132 RVA: 0x0002B054 File Offset: 0x00029254
	public void RemoveAllPlayedCarvings()
	{
		List<GameObject> list = new List<GameObject>(this.drawnCarvings);
		while (list.Count > 0)
		{
			GameObject gameObject = list[0];
			list.RemoveAt(0);
			ItemMovement component = gameObject.GetComponent<ItemMovement>();
			if (component && component.inGrid)
			{
				this.DiscardCarving(gameObject);
			}
		}
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x0002B0A8 File Offset: 0x000292A8
	public void RemoveAllDrawnCarvingsToHand()
	{
		while (this.drawnCarvings.Count > 0)
		{
			GameObject gameObject = this.drawnCarvings[0];
			this.drawnCarvings.RemoveAt(0);
			ItemMovement component = gameObject.GetComponent<ItemMovement>();
			Transform myUIlocation = gameObject.GetComponent<Carving>().myUIlocation;
			if (myUIlocation)
			{
				myUIlocation.GetComponent<UICarvingSpacer>().Remove();
			}
			if (component && component.inGrid)
			{
				component.RemoveFromGrid();
			}
			this.undrawnCarvings.Add(gameObject);
			this.CreateProxy(gameObject, gameObject.transform.position, this.undrawnCarvingsIcon.transform);
		}
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x0002B148 File Offset: 0x00029348
	public List<GameObject> SpawnCarvings()
	{
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < 5; i++)
		{
			GameObject gameObject = this.SpawnRandomCarving();
			this.AlignCarving(gameObject, i);
			list.Add(gameObject);
		}
		return list;
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x0002B180 File Offset: 0x00029380
	public void AlignCarving(GameObject carving, int i)
	{
		carving.transform.localPosition = new Vector3(-3f + 1.5f * (float)i, -5f, 0f);
		ItemMovement component = carving.GetComponent<ItemMovement>();
		component.outOfInventoryPosition = carving.transform.localPosition;
		component.outOfInventoryRotation = Quaternion.identity;
		component.returnsToOutOfInventoryPosition = true;
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x0002B1E0 File Offset: 0x000293E0
	public GameObject SpawnRandomCarving()
	{
		bool flag;
		Item2.Rarity rarity = this.gameManager.ChooseRarity(out flag, 0f, true);
		List<Item2> itemOfType = Item2.GetItemOfType(Item2.ItemType.Carving, this.gameManager.itemsToSpawn);
		Item2 item = Item2.ChooseRandomFromList(Item2.GetItemsOfRarities(new List<Item2.Rarity> { rarity }, itemOfType), true);
		GameManager gameManager = GameManager.main;
		if (gameManager != null)
		{
			gameManager.ShowGotLucky(item.transform, flag);
		}
		return this.CreateInWorldCarving(item.gameObject);
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x0002B250 File Offset: 0x00029450
	public GameObject CreateInWorldCarving(GameObject prefab)
	{
		return Object.Instantiate<GameObject>(prefab, Vector3.zero, Quaternion.identity);
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x0002B264 File Offset: 0x00029464
	public void SpawnTote()
	{
		if (Singleton.Instance.character != Character.CharacterName.Tote)
		{
			return;
		}
		if (!Singleton.Instance.loadSave)
		{
			this.NewToteRun();
		}
		this.carvingSpacerCanvas.SetActive(true);
		this.SetupToteUI();
		Transform transform = GameObject.FindGameObjectWithTag("CarvingSpacerSliderContent").transform;
		this.scrollRect = transform.GetComponentInParent<ScrollRect>();
		this.MoveAllToUndrawn();
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x0002B2C8 File Offset: 0x000294C8
	public void RemoveAllCarvings()
	{
		foreach (GameObject gameObject in this.undrawnCarvings)
		{
			Object.Destroy(gameObject);
		}
		foreach (GameObject gameObject2 in this.drawnCarvings)
		{
			Object.Destroy(gameObject2);
		}
		foreach (GameObject gameObject3 in this.discardedCarvings)
		{
			Object.Destroy(gameObject3);
		}
		foreach (GameObject gameObject4 in this.banishedCarvings)
		{
			Object.Destroy(gameObject4);
		}
		this.undrawnCarvings.Clear();
		this.drawnCarvings.Clear();
		this.discardedCarvings.Clear();
		this.banishedCarvings.Clear();
	}

	// Token: 0x06000474 RID: 1140 RVA: 0x0002B404 File Offset: 0x00029604
	private void NewToteRun()
	{
		foreach (Carving carving in Object.FindObjectsOfType<Carving>())
		{
			this.startingItems.Add(carving.gameObject);
		}
		if (!RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.dontStartWithStandard))
		{
			for (int j = 0; j < this.starterCarvings.Count; j++)
			{
				GameObject gameObject = this.starterCarvings[j];
				this.AddCarvingToDeck(gameObject);
			}
		}
		this.MarkAllAsOwned();
	}

	// Token: 0x06000475 RID: 1141 RVA: 0x0002B47C File Offset: 0x0002967C
	public void MarkAllAsOwned()
	{
		foreach (GameObject gameObject in this.undrawnCarvings)
		{
			Item2 component = gameObject.GetComponent<Item2>();
			if (component)
			{
				component.isOwned = true;
			}
		}
	}

	// Token: 0x06000476 RID: 1142 RVA: 0x0002B4DC File Offset: 0x000296DC
	public void RemoveToteUI()
	{
		if (this.wheel)
		{
			Object.Destroy(this.wheel.parent.gameObject);
		}
	}

	// Token: 0x06000477 RID: 1143 RVA: 0x0002B500 File Offset: 0x00029700
	public void SetupToteUI()
	{
		if (this.wheel)
		{
			return;
		}
		Transform transform = Object.Instantiate<GameObject>(this.toteAdditionalUIPrefab, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("Inventory").transform).transform;
		transform.localPosition = Vector3.zero;
		this.wheel = transform.GetChild(0);
		this.decks = transform.GetChild(2);
		this.undrawnCarvingsText = GameObject.Find("undrawnCarvings").GetComponentInChildren<TextMeshPro>();
		this.undrawnCarvingsIcon = this.undrawnCarvingsText.transform.parent;
		this.discardedCarvingsText = this.decks.GetChild(0).GetComponentInChildren<TextMeshPro>();
		this.banishedCarvingsText = this.decks.GetChild(1).GetComponentInChildren<TextMeshPro>();
		this.decks.gameObject.SetActive(false);
	}

	// Token: 0x06000478 RID: 1144 RVA: 0x0002B5D4 File Offset: 0x000297D4
	private void Start()
	{
		this.tutorialManager = Object.FindObjectOfType<TutorialManager>();
		this.gameFlowManager = GameFlowManager.main;
		this.gameManager = GameManager.main;
		Object.Destroy(this.savingCarving);
		GameObject gameObject = GameObject.FindGameObjectWithTag("PopUpSpriteMask");
		if (gameObject)
		{
			gameObject.GetComponent<SpriteMask>().enabled = false;
		}
	}

	// Token: 0x06000479 RID: 1145 RVA: 0x0002B62C File Offset: 0x0002982C
	public void DestroyAllCarvings()
	{
		Object.FindObjectsOfType<Carving>();
		foreach (object obj in GameObject.FindGameObjectWithTag("ItemParent").transform)
		{
			Transform transform = (Transform)obj;
			if (transform.gameObject.activeInHierarchy)
			{
				if (transform.GetComponent<Carving>())
				{
					this.DeleteCarving(transform.gameObject);
				}
			}
			else
			{
				transform.transform.position = Vector3.one * -999f;
				transform.gameObject.SetActive(true);
				if (transform.GetComponent<Carving>())
				{
					this.DeleteCarving(transform.gameObject);
				}
			}
		}
		ItemAnimationProxy[] array = Object.FindObjectsOfType<ItemAnimationProxy>();
		for (int i = 0; i < array.Length; i++)
		{
			Object.Destroy(array[i].gameObject);
		}
	}

	// Token: 0x0600047A RID: 1146 RVA: 0x0002B71C File Offset: 0x0002991C
	public void AssignAllCardsAfterLoad()
	{
		this.undrawnCarvings = new List<GameObject>();
		this.drawnCarvings = new List<GameObject>();
		this.discardedCarvings = new List<GameObject>();
		this.banishedCarvings = new List<GameObject>();
		foreach (object obj in GameObject.FindGameObjectWithTag("ItemParent").transform)
		{
			Transform transform = (Transform)obj;
			if (transform.gameObject.activeInHierarchy)
			{
				if (transform.GetComponent<Carving>())
				{
					transform.gameObject.transform.position = new Vector3(-999f, -999f, 0f);
					this.undrawnCarvings.Add(transform.gameObject);
				}
			}
			else
			{
				transform.gameObject.transform.position = new Vector3(-999f, -999f, 0f);
				transform.gameObject.SetActive(true);
				if (transform.GetComponent<Carving>())
				{
					this.undrawnCarvings.Add(transform.gameObject);
				}
			}
		}
		this.RemoveAllDrawnCarvingsToHand();
	}

	// Token: 0x0600047B RID: 1147 RVA: 0x0002B854 File Offset: 0x00029A54
	private void LateUpdate()
	{
		if (this.undrawnCarvingsIcon)
		{
			if (this.gameManager.draggingItem && this.gameFlowManager.battlePhase == GameFlowManager.BattlePhase.outOfBattle)
			{
				if (this.gameManager.draggingItem.GetComponent<Carving>())
				{
					this.undrawnCarvingsIcon.localScale = Vector3.one * 1.5f;
				}
			}
			else
			{
				this.undrawnCarvingsIcon.localScale = Vector3.one;
			}
		}
		if (this.undrawnCarvingsText && this.undrawnCarvingsText.gameObject.activeInHierarchy)
		{
			this.undrawnCarvingsText.text = this.undrawnCarvings.Count.ToString() ?? "";
		}
		if (this.decks && this.decks.gameObject.activeInHierarchy)
		{
			this.banishedCarvingsText.text = this.banishedCarvings.Count.ToString() ?? "";
			this.discardedCarvingsText.text = this.discardedCarvings.Count.ToString() ?? "";
		}
	}

	// Token: 0x04000335 RID: 821
	public static Tote main;

	// Token: 0x04000336 RID: 822
	private Vector2 startPos;

	// Token: 0x04000337 RID: 823
	private GameManager gameManager;

	// Token: 0x04000338 RID: 824
	private GameFlowManager gameFlowManager;

	// Token: 0x04000339 RID: 825
	private TutorialManager tutorialManager;

	// Token: 0x0400033A RID: 826
	[SerializeField]
	private GameObject spacerPrefabUIRedo;

	// Token: 0x0400033B RID: 827
	[SerializeField]
	private GameObject savingCarving;

	// Token: 0x0400033C RID: 828
	[SerializeField]
	private GameObject spacerPrefab;

	// Token: 0x0400033D RID: 829
	[SerializeField]
	private GameObject toteAdditionalUIPrefab;

	// Token: 0x0400033E RID: 830
	[SerializeField]
	private GameObject itemAnimationProxyPrefab;

	// Token: 0x0400033F RID: 831
	[SerializeField]
	public Transform undrawnCarvingsIcon;

	// Token: 0x04000340 RID: 832
	[SerializeField]
	private TextMeshPro undrawnCarvingsText;

	// Token: 0x04000341 RID: 833
	[SerializeField]
	private TextMeshPro discardedCarvingsText;

	// Token: 0x04000342 RID: 834
	[SerializeField]
	private TextMeshPro banishedCarvingsText;

	// Token: 0x04000343 RID: 835
	[SerializeField]
	private float speed;

	// Token: 0x04000344 RID: 836
	[SerializeField]
	private Transform wheel;

	// Token: 0x04000345 RID: 837
	private Transform decks;

	// Token: 0x04000346 RID: 838
	[SerializeField]
	private GameObject cardOverlayPrefab;

	// Token: 0x04000347 RID: 839
	[SerializeField]
	private ScrollRect scrollRect;

	// Token: 0x04000348 RID: 840
	[SerializeField]
	private List<GameObject> starterCarvings = new List<GameObject>();

	// Token: 0x04000349 RID: 841
	[Header("--------------------------------")]
	[SerializeField]
	private List<GameObject> undrawnCarvings = new List<GameObject>();

	// Token: 0x0400034A RID: 842
	[SerializeField]
	private List<GameObject> drawnCarvings = new List<GameObject>();

	// Token: 0x0400034B RID: 843
	[SerializeField]
	private List<GameObject> discardedCarvings = new List<GameObject>();

	// Token: 0x0400034C RID: 844
	[SerializeField]
	private List<GameObject> banishedCarvings = new List<GameObject>();

	// Token: 0x0400034D RID: 845
	[NonSerialized]
	private List<GameObject> startingItems = new List<GameObject>();

	// Token: 0x0400034E RID: 846
	[SerializeField]
	private GameObject carvingSpacerCanvas;

	// Token: 0x0400034F RID: 847
	[SerializeField]
	private GameObject damagePrefab;

	// Token: 0x04000350 RID: 848
	private List<GameObject> showingList = new List<GameObject>();

	// Token: 0x04000351 RID: 849
	private Transform currentListParent;
}
