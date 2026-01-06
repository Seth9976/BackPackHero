using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000080 RID: 128
public class Overworld_Chest : Overworld_InteractiveObject
{
	// Token: 0x060002C6 RID: 710 RVA: 0x00010AE5 File Offset: 0x0000ECE5
	private void Start()
	{
		this.spriteRenderer = base.GetComponentInChildren<SpriteRenderer>();
	}

	// Token: 0x060002C7 RID: 711 RVA: 0x00010AF4 File Offset: 0x0000ECF4
	public void AddAllItemsToInventory()
	{
		foreach (string text in this.storedItems)
		{
			MetaProgressSaveManager.main.storedItems.Add(text);
		}
		this.storedItems.Clear();
	}

	// Token: 0x060002C8 RID: 712 RVA: 0x00010B5C File Offset: 0x0000ED5C
	public bool IsEmpty()
	{
		return this.storedItems.Count == 0;
	}

	// Token: 0x060002C9 RID: 713 RVA: 0x00010B6C File Offset: 0x0000ED6C
	public override void Interact()
	{
		this.spriteRenderer.sprite = this.openSprite;
		if (this.animateChestRoutine != null)
		{
			base.StopCoroutine(this.animateChestRoutine);
		}
		this.animateChestRoutine = base.StartCoroutine(this.OpenChest());
	}

	// Token: 0x060002CA RID: 714 RVA: 0x00010BA5 File Offset: 0x0000EDA5
	private IEnumerator OpenChest()
	{
		ItemStorage component = base.GetComponent<ItemStorage>();
		if (component)
		{
			this.storedItems = component.storedItems;
		}
		yield return new WaitForSeconds(0.25f);
		base.Interact();
		Overworld_ConversationManager.main.SetInteractiveObject(base.transform);
		Overworld_InventoryManager.main.OpenChest(this.storedItems, this.spriteRenderer.sprite);
		yield break;
	}

	// Token: 0x060002CB RID: 715 RVA: 0x00010BB4 File Offset: 0x0000EDB4
	public override void EndInteraction()
	{
		base.EndInteraction();
		if (this.animateChestRoutine != null)
		{
			base.StopCoroutine(this.animateChestRoutine);
		}
		this.animateChestRoutine = base.StartCoroutine(this.CloseChest());
		ItemStorage component = base.GetComponent<ItemStorage>();
		if (component)
		{
			this.storedItems = component.storedItems;
		}
	}

	// Token: 0x060002CC RID: 716 RVA: 0x00010C08 File Offset: 0x0000EE08
	private IEnumerator CloseChest()
	{
		yield return new WaitForSeconds(0.25f);
		this.spriteRenderer.sprite = this.closedSprite;
		yield break;
	}

	// Token: 0x040001D2 RID: 466
	private SpriteRenderer spriteRenderer;

	// Token: 0x040001D3 RID: 467
	[SerializeField]
	private Sprite openSprite;

	// Token: 0x040001D4 RID: 468
	[SerializeField]
	private Sprite closedSprite;

	// Token: 0x040001D5 RID: 469
	private Coroutine animateChestRoutine;

	// Token: 0x040001D6 RID: 470
	[SerializeField]
	private List<string> storedItems = new List<string>();
}
