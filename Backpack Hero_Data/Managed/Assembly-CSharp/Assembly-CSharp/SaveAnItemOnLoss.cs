using System;
using UnityEngine;

// Token: 0x0200009C RID: 156
public class SaveAnItemOnLoss : MonoBehaviour
{
	// Token: 0x0600036B RID: 875 RVA: 0x00013DED File Offset: 0x00011FED
	private void Start()
	{
	}

	// Token: 0x0600036C RID: 876 RVA: 0x00013DEF File Offset: 0x00011FEF
	private void Update()
	{
	}

	// Token: 0x0600036D RID: 877 RVA: 0x00013DF4 File Offset: 0x00011FF4
	public void GetItem(Item2 item)
	{
		if (item.itemType.Contains(Item2.ItemType.Hazard))
		{
			return;
		}
		foreach (object obj in this.itemParent)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
		if (this.savedItem == item)
		{
			SoundManager.main.PlaySFX("negative");
			this.text.SetActive(true);
			this.savedItem = null;
			return;
		}
		SoundManager.main.PlaySFX("moveHere");
		this.savedItem = item;
		this.text.SetActive(false);
		UICarvingIndicator component = Object.Instantiate<GameObject>(this.itemUIStandIn, this.itemParent).GetComponent<UICarvingIndicator>();
		component.GetComponent<RectTransform>().sizeDelta = new Vector2(265f, 265f);
		component.Setup(item);
	}

	// Token: 0x0600036E RID: 878 RVA: 0x00013EE8 File Offset: 0x000120E8
	public void SaveItem()
	{
		if (!this.savedItem)
		{
			return;
		}
		Debug.Log("Saving item: " + this.savedItem.name);
		MetaProgressSaveManager.main.AddItem(this.savedItem);
		ItemPouch component = this.savedItem.GetComponent<ItemPouch>();
		if (component)
		{
			foreach (GameObject gameObject in component.itemsInside)
			{
				if (gameObject)
				{
					Item2 component2 = gameObject.GetComponent<Item2>();
					if (component2)
					{
						MetaProgressSaveManager.main.AddItem(component2);
					}
				}
			}
		}
	}

	// Token: 0x04000277 RID: 631
	[SerializeField]
	private GameObject itemUIStandIn;

	// Token: 0x04000278 RID: 632
	private Item2 savedItem;

	// Token: 0x04000279 RID: 633
	[SerializeField]
	private Transform itemParent;

	// Token: 0x0400027A RID: 634
	[SerializeField]
	private GameObject text;
}
