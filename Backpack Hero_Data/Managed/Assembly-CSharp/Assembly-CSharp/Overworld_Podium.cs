using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000082 RID: 130
public class Overworld_Podium : Overworld_InteractiveObject
{
	// Token: 0x060002D6 RID: 726 RVA: 0x00010D39 File Offset: 0x0000EF39
	private void Start()
	{
		this.SetupSpriteFromLoad();
	}

	// Token: 0x060002D7 RID: 727 RVA: 0x00010D41 File Offset: 0x0000EF41
	private void Update()
	{
	}

	// Token: 0x060002D8 RID: 728 RVA: 0x00010D44 File Offset: 0x0000EF44
	private void SetupSpriteFromLoad()
	{
		ItemStorage component = base.GetComponent<ItemStorage>();
		if (!component || component.storedItems.Count == 0)
		{
			return;
		}
		string text = component.storedItems[0];
		GameObject itemByName = DebugItemManager.main.GetItemByName(text);
		if (!itemByName)
		{
			return;
		}
		SpriteRenderer component2 = itemByName.GetComponent<SpriteRenderer>();
		if (!component2)
		{
			return;
		}
		this.itemSprite.sprite = component2.sprite;
		this.itemSprite.enabled = true;
		this.itemSprite.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
	}

	// Token: 0x060002D9 RID: 729 RVA: 0x00010DE4 File Offset: 0x0000EFE4
	private void AddItemBackToInv()
	{
		ItemStorage component = base.GetComponent<ItemStorage>();
		if (!component || component.storedItems.Count == 0)
		{
			return;
		}
		string text = component.storedItems[0];
		if (text != "")
		{
			MetaProgressSaveManager.main.storedItems.Add(text);
			component.storedItems = new List<string>();
			this.itemSprite.sprite = null;
			this.itemSprite.enabled = false;
		}
	}

	// Token: 0x060002DA RID: 730 RVA: 0x00010E61 File Offset: 0x0000F061
	public void Remove()
	{
		this.AddItemBackToInv();
	}

	// Token: 0x060002DB RID: 731 RVA: 0x00010E69 File Offset: 0x0000F069
	public override void Interact()
	{
		this.AddItemBackToInv();
		Overworld_Manager.main.StartInteraction(this);
		Overworld_InventoryManager.main.ToggleInventory(Overworld_InventoryManager.ClickAction.SELECTITEM);
	}

	// Token: 0x060002DC RID: 732 RVA: 0x00010E87 File Offset: 0x0000F087
	public override void EndInteraction()
	{
		this.GetItem(Overworld_InventoryManager.main.chosenItem);
		Overworld_InventoryManager.main.RemoveItem();
	}

	// Token: 0x060002DD RID: 733 RVA: 0x00010EA4 File Offset: 0x0000F0A4
	public void GetItem(Item2 item)
	{
		if (item == null)
		{
			return;
		}
		SpriteRenderer component = item.GetComponent<SpriteRenderer>();
		if (!component)
		{
			return;
		}
		ItemStorage component2 = base.GetComponent<ItemStorage>();
		if (component2)
		{
			component2.storedItems.Add(item.name);
		}
		this.itemSprite.sprite = component.sprite;
		this.itemSprite.enabled = true;
		this.itemSprite.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
	}

	// Token: 0x040001DA RID: 474
	[SerializeField]
	private SpriteRenderer itemSprite;
}
