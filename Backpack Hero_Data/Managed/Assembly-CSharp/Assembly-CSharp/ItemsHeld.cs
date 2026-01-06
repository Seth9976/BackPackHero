using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000063 RID: 99
public class ItemsHeld : MonoBehaviour
{
	// Token: 0x060001D9 RID: 473 RVA: 0x0000BA07 File Offset: 0x00009C07
	private void Awake()
	{
		if (ItemsHeld.main != null)
		{
			Object.Destroy(this);
			return;
		}
		ItemsHeld.main = this;
	}

	// Token: 0x060001DA RID: 474 RVA: 0x0000BA23 File Offset: 0x00009C23
	private void OnDestroy()
	{
		if (ItemsHeld.main == this)
		{
			ItemsHeld.main = null;
		}
	}

	// Token: 0x060001DB RID: 475 RVA: 0x0000BA38 File Offset: 0x00009C38
	private void Start()
	{
	}

	// Token: 0x060001DC RID: 476 RVA: 0x0000BA3A File Offset: 0x00009C3A
	private void Update()
	{
		if (!this.gottenItems)
		{
			this.gottenItems = true;
			this.GetAllItemsHeld();
		}
	}

	// Token: 0x060001DD RID: 477 RVA: 0x0000BA54 File Offset: 0x00009C54
	public void GetAllItemsIfNotAlready()
	{
		if (DateTime.Now.Subtract(this.lastTimeChecked).TotalSeconds > 1.0)
		{
			this.GetAllItemsHeld();
		}
	}

	// Token: 0x060001DE RID: 478 RVA: 0x0000BA8D File Offset: 0x00009C8D
	public void ForceGetAllItems()
	{
		this.GetAllItemsHeld();
	}

	// Token: 0x060001DF RID: 479 RVA: 0x0000BA98 File Offset: 0x00009C98
	private void GetAllItemsHeld()
	{
		this.storedItems.Clear();
		foreach (string text in MetaProgressSaveManager.main.storedItems)
		{
			Item2 item2ByName = DebugItemManager.main.GetItem2ByName(text);
			if (item2ByName)
			{
				this.storedItems.Add(item2ByName);
			}
		}
		this.lastTimeChecked = DateTime.Now;
	}

	// Token: 0x04000146 RID: 326
	public static ItemsHeld main;

	// Token: 0x04000147 RID: 327
	[SerializeField]
	public List<Item2> storedItems;

	// Token: 0x04000148 RID: 328
	private DateTime lastTimeChecked = DateTime.Now;

	// Token: 0x04000149 RID: 329
	public bool gottenItems;
}
