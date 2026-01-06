using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Token: 0x020000CC RID: 204
public class Match2CardMaster : MonoBehaviour
{
	// Token: 0x060005E6 RID: 1510 RVA: 0x0003A150 File Offset: 0x00038350
	private void Start()
	{
		this.text.text = LangaugeManager.main.GetTextByKey("mini7").Replace("/x", this.guesses.ToString() ?? "");
		this.randomEventMaster = base.GetComponent<RandomEventMaster>();
		this.gameManager = GameManager.main;
		base.StartCoroutine(this.Setup());
	}

	// Token: 0x060005E7 RID: 1511 RVA: 0x0003A1B9 File Offset: 0x000383B9
	private IEnumerator Setup()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		List<int> list = new List<int>();
		for (int i = 0; i < 12; i++)
		{
			list.Add(i);
		}
		while (list.Count > 4)
		{
			int num = Random.Range(0, list.Count);
			int num2 = list[num];
			list.RemoveAt(num);
			num = Random.Range(0, list.Count);
			int num3 = list[num];
			list.RemoveAt(num);
			Match2Card component = this.cardParent.GetChild(num2).GetComponent<Match2Card>();
			Match2Card component2 = this.cardParent.GetChild(num3).GetComponent<Match2Card>();
			GameObject gameObject = this.gameManager.SpawnItemNoStacks(new List<Item2.ItemType>(), new List<Item2.Rarity>
			{
				Item2.Rarity.Uncommon,
				Item2.Rarity.Rare,
				Item2.Rarity.Legendary
			});
			GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, base.transform.position, Quaternion.identity);
			gameObject.transform.SetParent(this.cardParent.GetChild(num2));
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = Vector3.one * 100f;
			gameObject.GetComponent<ItemMovement>().moveToItemTransform = false;
			gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
			component.item = gameObject;
			component.HideItem();
			gameObject2.transform.SetParent(this.cardParent.GetChild(num3));
			gameObject2.transform.localPosition = Vector3.zero;
			gameObject2.transform.localScale = Vector3.one * 100f;
			gameObject2.GetComponent<ItemMovement>().moveToItemTransform = false;
			gameObject2.GetComponent<SpriteRenderer>().sortingOrder = 2;
			component2.item = gameObject2;
			component2.HideItem();
		}
		while (list.Count > 0)
		{
			int num4 = Random.Range(0, list.Count);
			int num5 = list[num4];
			list.RemoveAt(num4);
			num4 = Random.Range(0, list.Count);
			int num6 = list[num4];
			list.RemoveAt(num4);
			Match2Card component3 = this.cardParent.GetChild(num5).GetComponent<Match2Card>();
			Match2Card component4 = this.cardParent.GetChild(num6).GetComponent<Match2Card>();
			GameObject gameObject3 = this.gameManager.SpawnCurse();
			GameObject gameObject4 = Object.Instantiate<GameObject>(gameObject3, base.transform.position, Quaternion.identity);
			gameObject3.transform.SetParent(this.cardParent.GetChild(num5));
			gameObject3.transform.localPosition = Vector3.zero;
			gameObject3.transform.localScale = Vector3.one * 100f;
			gameObject3.GetComponent<ItemMovement>().moveToItemTransform = false;
			gameObject3.GetComponent<SpriteRenderer>().sortingOrder = 2;
			component3.item = gameObject3;
			component3.HideItem();
			gameObject4.transform.SetParent(this.cardParent.GetChild(num6));
			gameObject4.transform.localPosition = Vector3.zero;
			gameObject4.transform.localScale = Vector3.one * 100f;
			gameObject4.GetComponent<ItemMovement>().moveToItemTransform = false;
			gameObject4.GetComponent<SpriteRenderer>().sortingOrder = 2;
			component4.item = gameObject4;
			component4.HideItem();
		}
		yield break;
	}

	// Token: 0x060005E8 RID: 1512 RVA: 0x0003A1C8 File Offset: 0x000383C8
	private void Update()
	{
	}

	// Token: 0x060005E9 RID: 1513 RVA: 0x0003A1CC File Offset: 0x000383CC
	public void Flip(Match2Card match2Card)
	{
		if (this.guesses <= 0)
		{
			this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("mini5"));
			return;
		}
		if (!this.match2Card1)
		{
			SoundManager.main.PlaySFX("menuBlip");
			this.match2Card1 = match2Card;
			match2Card.FlipMe();
			return;
		}
		if (!this.match2Card2 && match2Card == this.match2Card1)
		{
			return;
		}
		if (!this.match2Card2)
		{
			SoundManager.main.PlaySFX("menuBlip");
			this.match2Card2 = match2Card;
			match2Card.FlipMe();
			return;
		}
		this.guesses--;
		this.text.text = LangaugeManager.main.GetTextByKey("mini7").Replace("/x", this.guesses.ToString() ?? "");
		if (this.match2Card1.item.GetComponent<Item2>().displayName == this.match2Card2.item.GetComponent<Item2>().displayName && this.match2Card1.item.GetComponent<SpriteRenderer>().sprite == this.match2Card2.item.GetComponent<SpriteRenderer>().sprite)
		{
			this.match2Card1.GetComponent<CanvasGroup>().alpha = 0f;
			this.match2Card2.GetComponent<CanvasGroup>().alpha = 0f;
			GameObject gameObject = Object.Instantiate<GameObject>(this.match2Card1.item, Vector3.zero, Quaternion.identity);
			ItemMovement component = gameObject.GetComponent<ItemMovement>();
			component.moveToItemTransform = true;
			component.transform.localScale = Vector3.one;
			Item2 component2 = gameObject.GetComponent<Item2>();
			if (Item2.ShareItemTypes(component2.itemType, new List<Item2.ItemType> { Item2.ItemType.Curse }))
			{
				SoundManager.main.PlaySFX("miniGameBad");
			}
			else
			{
				SoundManager.main.PlaySFX("miniGameGood");
			}
			component.StartCoroutine(component.Move(new Vector2(0f, 2.5f), 0));
			if (!Item2.ShareItemTypes(component2.itemType, new List<Item2.ItemType> { Item2.ItemType.Curse }))
			{
				gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
			}
			this.match2Card1.item.GetComponent<ItemMovement>().DelayDestroy();
			this.match2Card2.item.GetComponent<ItemMovement>().DelayDestroy();
		}
		else
		{
			this.match2Card1.FlipMe();
			this.match2Card2.FlipMe();
		}
		this.match2Card1 = null;
		this.match2Card2 = null;
	}

	// Token: 0x060005EA RID: 1514 RVA: 0x0003A450 File Offset: 0x00038650
	public void EndEvent()
	{
		SoundManager.main.PlaySFX("menuBlip");
		if (this.match2Card1)
		{
			this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("mini4"));
			return;
		}
		this.randomEventMaster.finished = true;
		this.randomEventMaster.dungeonEvent.AddEventProperty(DungeonEvent.EventProperty.Type.finished, 0);
		this.gameManager.viewingEvent = false;
		this.eventBoxAnimator.Play("Out");
		if (!this.gameManager)
		{
			this.gameManager = GameManager.main;
		}
		this.gameManager.SetAllItemColliders(true);
	}

	// Token: 0x040004B1 RID: 1201
	[SerializeField]
	private Match2Card match2Card1;

	// Token: 0x040004B2 RID: 1202
	[SerializeField]
	private Match2Card match2Card2;

	// Token: 0x040004B3 RID: 1203
	[SerializeField]
	private Transform cardParent;

	// Token: 0x040004B4 RID: 1204
	[SerializeField]
	private TextMeshProUGUI text;

	// Token: 0x040004B5 RID: 1205
	[SerializeField]
	private Animator eventBoxAnimator;

	// Token: 0x040004B6 RID: 1206
	private GameManager gameManager;

	// Token: 0x040004B7 RID: 1207
	private RandomEventMaster randomEventMaster;

	// Token: 0x040004B8 RID: 1208
	private int guesses = 6;
}
