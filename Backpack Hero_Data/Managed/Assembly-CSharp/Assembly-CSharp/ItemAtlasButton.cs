using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000106 RID: 262
public class ItemAtlasButton : MonoBehaviour
{
	// Token: 0x060008FA RID: 2298 RVA: 0x0005DA0C File Offset: 0x0005BC0C
	public static void AllButtonsConsiderOwned()
	{
		ItemAtlasButton[] array = Object.FindObjectsOfType<ItemAtlasButton>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].ConsiderOwned();
		}
	}

	// Token: 0x060008FB RID: 2299 RVA: 0x0005DA38 File Offset: 0x0005BC38
	public void ConsiderOwned()
	{
		string text = Item2.GetDisplayName(base.GetComponentInChildren<TextMeshProUGUI>().text);
		if (this.mission)
		{
			text = Missions.Stringify(this.mission);
		}
		if (ItemAtlasButton.IsOwned(text))
		{
			this.alreadyOwnedCheckMark.SetActive(true);
			return;
		}
		this.alreadyOwnedCheckMark.SetActive(false);
	}

	// Token: 0x060008FC RID: 2300 RVA: 0x0005DA90 File Offset: 0x0005BC90
	public static bool IsOwned(string name)
	{
		return MetaProgressSaveManager.main.ItemIsUnlocked(name) || MetaProgressSaveManager.main.availableBuildings.Contains(name) || MetaProgressSaveManager.main.availableTiles.Contains(name) || MetaProgressSaveManager.main.missionsUnlocked.Contains(name);
	}

	// Token: 0x060008FD RID: 2301 RVA: 0x0005DAE4 File Offset: 0x0005BCE4
	public void DisplayMyItem()
	{
		if (this.type == ItemAtlasButton.Type.item || this.type == ItemAtlasButton.Type.structure)
		{
			string text = base.GetComponentInChildren<TextMeshProUGUI>().text;
			ItemAtlas itemAtlas = Object.FindObjectOfType<ItemAtlas>();
			if (itemAtlas)
			{
				itemAtlas.LoadCardPublic(this.key);
				return;
			}
			StoreAtlas storeAtlas = Object.FindObjectOfType<StoreAtlas>();
			if (storeAtlas)
			{
				storeAtlas.LoadCardPublic(text);
				storeAtlas.SetCosts(this.costs);
				return;
			}
		}
		else
		{
			if (this.type == ItemAtlasButton.Type.selector)
			{
				Object.FindObjectOfType<ItemAtlas>().FindItemsOfType(this.itemType);
				return;
			}
			if (this.type == ItemAtlasButton.Type.help)
			{
				TutorialPopUpManager.main.DisplayTutorial(this.key);
				return;
			}
			if (this.type == ItemAtlasButton.Type.tile)
			{
				StoreAtlas storeAtlas2 = Object.FindObjectOfType<StoreAtlas>();
				if (storeAtlas2)
				{
					storeAtlas2.LoadCardPublic(this.sellingTile.name);
					storeAtlas2.SetCosts(this.costs);
					return;
				}
			}
			else if (this.type == ItemAtlasButton.Type.term)
			{
				if (!Object.FindObjectOfType<CardManager>())
				{
					return;
				}
				string text2 = this.key + "d";
				ItemAtlas itemAtlas2 = Object.FindObjectOfType<ItemAtlas>();
				if (itemAtlas2)
				{
					itemAtlas2.lastSelectedItem = base.transform;
					itemAtlas2.LoadCardPublic(text2);
					return;
				}
			}
			else
			{
				if (this.type == ItemAtlasButton.Type.mission)
				{
					StoreAtlas storeAtlas3 = Object.FindObjectOfType<StoreAtlas>();
					if (storeAtlas3)
					{
						storeAtlas3.SetMission(this.mission);
						storeAtlas3.SetCosts(this.costs);
					}
					this.OpenCardMission(this.mission);
					return;
				}
				if (this.type == ItemAtlasButton.Type.lore)
				{
					string text3 = this.key;
					ItemAtlas itemAtlas3 = Object.FindObjectOfType<ItemAtlas>();
					if (itemAtlas3)
					{
						itemAtlas3.lastSelectedItem = base.transform;
						itemAtlas3.LoadCardPublic(text3);
						return;
					}
				}
				else if (this.type == ItemAtlasButton.Type.research)
				{
					string text4 = this.key;
					ItemAtlas itemAtlas4 = Object.FindObjectOfType<ItemAtlas>();
					if (itemAtlas4)
					{
						itemAtlas4.LoadResearch(text4);
					}
				}
			}
		}
	}

	// Token: 0x060008FE RID: 2302 RVA: 0x0005DCB0 File Offset: 0x0005BEB0
	public void OpenCardMission(Missions missions)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.cardPrefabSimple, Vector3.zero, Quaternion.identity);
		StoreAtlas storeAtlas = Object.FindObjectOfType<StoreAtlas>();
		if (storeAtlas)
		{
			gameObject.transform.SetParent(storeAtlas.cardButtonParent);
		}
		gameObject.transform.localScale = Vector3.one;
		Card component = gameObject.GetComponent<Card>();
		component.GetDescriptionMission(missions, base.gameObject);
		component.stuck = true;
		component.deleteOnDeactivate = false;
		ItemAtlasButton.AllButtonsConsiderOwned();
	}

	// Token: 0x060008FF RID: 2303 RVA: 0x0005DD28 File Offset: 0x0005BF28
	public void SetSprite(SpriteRenderer spriteRenderer, string key)
	{
		this.key = key;
		this.itemImage.sprite = spriteRenderer.sprite;
		this.itemImage.color = spriteRenderer.color;
		if (spriteRenderer.flipX)
		{
			this.itemImage.transform.localScale = new Vector3(-1f, 1f, 1f);
		}
		this.itemImage.SetNativeSize();
		this.itemImage.rectTransform.sizeDelta = this.itemImage.rectTransform.sizeDelta.normalized * 170f;
		if (this.itemImage.rectTransform.sizeDelta.x > this.itemImage.rectTransform.sizeDelta.y && this.itemImage.rectTransform.sizeDelta.x > 200f)
		{
			this.itemImage.rectTransform.rotation = Quaternion.Euler(0f, 0f, -90f);
			this.itemImage.rectTransform.sizeDelta *= 1.2f;
		}
	}

	// Token: 0x04000718 RID: 1816
	public ItemAtlasButton.Type type;

	// Token: 0x04000719 RID: 1817
	public Item2.ItemType itemType;

	// Token: 0x0400071A RID: 1818
	public Image itemImage;

	// Token: 0x0400071B RID: 1819
	public string key;

	// Token: 0x0400071C RID: 1820
	public List<Overworld_ResourceManager.Resource> costs = new List<Overworld_ResourceManager.Resource>();

	// Token: 0x0400071D RID: 1821
	[SerializeField]
	private GameObject alreadyOwnedCheckMark;

	// Token: 0x0400071E RID: 1822
	public SellingTile sellingTile;

	// Token: 0x0400071F RID: 1823
	public Missions mission;

	// Token: 0x04000720 RID: 1824
	[SerializeField]
	private GameObject cardPrefabSimple;

	// Token: 0x0200037F RID: 895
	public enum Type
	{
		// Token: 0x0400152A RID: 5418
		item,
		// Token: 0x0400152B RID: 5419
		selector,
		// Token: 0x0400152C RID: 5420
		help,
		// Token: 0x0400152D RID: 5421
		term,
		// Token: 0x0400152E RID: 5422
		structure,
		// Token: 0x0400152F RID: 5423
		tile,
		// Token: 0x04001530 RID: 5424
		mission,
		// Token: 0x04001531 RID: 5425
		lore,
		// Token: 0x04001532 RID: 5426
		research
	}
}
