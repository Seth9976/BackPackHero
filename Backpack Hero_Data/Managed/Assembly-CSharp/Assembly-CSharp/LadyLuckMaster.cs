using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000CA RID: 202
public class LadyLuckMaster : MonoBehaviour
{
	// Token: 0x060005D8 RID: 1496 RVA: 0x00039D82 File Offset: 0x00037F82
	private void Start()
	{
		this.gameManager = GameManager.main;
		this.randomEventMaster = base.GetComponent<RandomEventMaster>();
		base.StartCoroutine(this.Setup());
	}

	// Token: 0x060005D9 RID: 1497 RVA: 0x00039DA8 File Offset: 0x00037FA8
	private void Update()
	{
	}

	// Token: 0x060005DA RID: 1498 RVA: 0x00039DAA File Offset: 0x00037FAA
	private IEnumerator Setup()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		new List<GameObject>();
		List<Item2> item2s = this.randomEventMaster.dungeonEvent.GetItem2s();
		if (item2s.Count == 0)
		{
			item2s.AddRange(ItemSpawner.GetItems(2, new List<Item2.Rarity> { Item2.Rarity.Uncommon }, new List<Item2.ItemType> { Item2.ItemType.Any }, true, false));
			item2s.AddRange(ItemSpawner.GetItems(2, new List<Item2.Rarity> { Item2.Rarity.Rare }, new List<Item2.ItemType> { Item2.ItemType.Any }, true, false));
			item2s.AddRange(ItemSpawner.GetItems(1, new List<Item2.Rarity> { Item2.Rarity.Legendary }, new List<Item2.ItemType> { Item2.ItemType.Any }, true, false));
			item2s.Reverse();
			this.randomEventMaster.dungeonEvent.StoreItem2s(item2s);
		}
		UICarvingIndicator[] componentsInChildren = base.GetComponentsInChildren<UICarvingIndicator>();
		int num = 0;
		foreach (UICarvingIndicator uicarvingIndicator in componentsInChildren)
		{
			if (num >= item2s.Count)
			{
				break;
			}
			uicarvingIndicator.Setup(item2s[num]);
			num++;
		}
		yield break;
	}

	// Token: 0x060005DB RID: 1499 RVA: 0x00039DBC File Offset: 0x00037FBC
	public void RollDice()
	{
		if (this.rolling)
		{
			return;
		}
		this.replacementText.key = "gmanb1";
		this.replacementText.ReplaceText();
		this.randomEventMaster.dungeonEvent.AddEventProperty(DungeonEvent.EventProperty.Type.finished, 0);
		int num = Random.Range(0, 6);
		base.StartCoroutine(this.SetSquares(num));
	}

	// Token: 0x060005DC RID: 1500 RVA: 0x00039E15 File Offset: 0x00038015
	public IEnumerator SetSquares(int num)
	{
		this.rolling = true;
		Vector3 end = this.dice.transform.position;
		Vector3 start = this.dice.transform.position + Vector3.down * 0.25f;
		int i;
		int num3;
		for (i = 0; i < 10; i = num3 + 1)
		{
			SoundManager.main.PlaySFX("pickup");
			this.dice.transform.position = Vector3.Lerp(start, end, (float)i / 10f);
			int num2 = Random.Range(0, 6);
			this.dice.sprite = this.diceSprites[num2];
			yield return new WaitForSeconds(0.0125f * (float)i);
			num3 = i;
		}
		this.dice.transform.position = end;
		this.dice.sprite = this.diceSprites[num];
		this.total += num + 1;
		i = 0;
		while (i < this.total && i < this.barsParent.childCount)
		{
			Transform child = this.barsParent.GetChild(i);
			Image image = child.GetComponent<Image>();
			if (image)
			{
				if (image.color == Color.white)
				{
					SoundManager.main.PlaySFX("menuBlip");
					yield return new WaitForSeconds(0.1f);
				}
				image.color = Color.red;
			}
			image = null;
			num3 = i;
			i = num3 + 1;
		}
		foreach (Image image2 in this.uiImages)
		{
			image2.color = Color.white;
		}
		if (this.total > this.barsParent.childCount)
		{
			SoundManager.main.PlaySFX("miniGameBad");
			yield return new WaitForSeconds(0.5f);
			List<Item2> items = ItemSpawner.GetItems(1, new List<Item2.ItemType> { Item2.ItemType.Curse }, true, true);
			if (items.Count > 0)
			{
				ItemSpawner.InstantiateItemFree(items[0]);
			}
			this.EndEvent();
		}
		else
		{
			Image image3 = null;
			if (this.total <= 4)
			{
				image3 = this.uiImages[4];
			}
			else if (this.total <= 7)
			{
				image3 = this.uiImages[3];
			}
			else if (this.total <= 9)
			{
				image3 = this.uiImages[2];
			}
			else if (this.total <= 11)
			{
				image3 = this.uiImages[1];
			}
			else if (this.total <= 12)
			{
				image3 = this.uiImages[0];
			}
			if (image3)
			{
				image3.color = Color.grey;
			}
		}
		this.rolling = false;
		yield break;
	}

	// Token: 0x060005DD RID: 1501 RVA: 0x00039E2C File Offset: 0x0003802C
	public void EndEvent()
	{
		if (this.closed)
		{
			return;
		}
		this.closed = true;
		SoundManager.main.PlaySFX("menuBlip");
		if (this.randomEventMaster && this.randomEventMaster.dungeonEvent.GetEventProperty(DungeonEvent.EventProperty.Type.finished) == null)
		{
			this.gameManager.SetAllSpritesToLayer0();
			this.randomEventMaster.npc.isOpen = false;
		}
		SpriteRenderer[] componentsInChildren = base.GetComponentsInChildren<SpriteRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].material = this.defaultSpriteMaterial;
		}
		if (this.total > 0 && this.total <= 12)
		{
			Item2 item = null;
			List<Item2> item2s = this.randomEventMaster.dungeonEvent.GetItem2s();
			if (this.total <= 4 && item2s.Count >= 4)
			{
				item = this.randomEventMaster.dungeonEvent.GetItem2s()[4];
			}
			else if (this.total <= 7 && item2s.Count >= 3)
			{
				item = this.randomEventMaster.dungeonEvent.GetItem2s()[3];
			}
			else if (this.total <= 9 && item2s.Count >= 2)
			{
				item = this.randomEventMaster.dungeonEvent.GetItem2s()[2];
			}
			else if (this.total <= 11 && item2s.Count >= 1)
			{
				item = this.randomEventMaster.dungeonEvent.GetItem2s()[1];
			}
			else if (this.total <= 12 && item2s.Count >= 0)
			{
				item = this.randomEventMaster.dungeonEvent.GetItem2s()[0];
			}
			SoundManager.main.PlaySFX("miniGameGood");
			ItemSpawner.InstantiateItemFree(item);
			GameManager.main.MoveAllItems();
		}
		this.gameManager.viewingEvent = false;
		this.randomEventMaster.eventBoxAnimator.Play("Out");
		if (!this.gameManager)
		{
			this.gameManager = GameManager.main;
		}
		this.gameManager.SetAllItemColliders(true);
	}

	// Token: 0x0400049E RID: 1182
	[SerializeField]
	private Image dice;

	// Token: 0x0400049F RID: 1183
	[SerializeField]
	private Transform barsParent;

	// Token: 0x040004A0 RID: 1184
	[SerializeField]
	private Transform itemsPositionParent;

	// Token: 0x040004A1 RID: 1185
	[SerializeField]
	private Sprite[] diceSprites;

	// Token: 0x040004A2 RID: 1186
	[SerializeField]
	private ReplacementText replacementText;

	// Token: 0x040004A3 RID: 1187
	[SerializeField]
	private Material defaultSpriteMaterial;

	// Token: 0x040004A4 RID: 1188
	[SerializeField]
	private Material outlineMaterial;

	// Token: 0x040004A5 RID: 1189
	[SerializeField]
	private List<Image> uiImages;

	// Token: 0x040004A6 RID: 1190
	private RandomEventMaster randomEventMaster;

	// Token: 0x040004A7 RID: 1191
	private GameManager gameManager;

	// Token: 0x040004A8 RID: 1192
	private List<Item2> preSpawnedITems;

	// Token: 0x040004A9 RID: 1193
	private int total;

	// Token: 0x040004AA RID: 1194
	private bool rolling;

	// Token: 0x040004AB RID: 1195
	public bool closed;
}
