using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x02000069 RID: 105
public class ItemSwitcher : MonoBehaviour
{
	// Token: 0x0600020D RID: 525 RVA: 0x0000CCB0 File Offset: 0x0000AEB0
	public List<ItemSwitcher.Item2Change> GetAllItemChanges()
	{
		List<ItemSwitcher.Item2Change> list = new List<ItemSwitcher.Item2Change>();
		foreach (ItemSwitcher.Item2Change item2Change in this.item2Changes)
		{
			if (item2Change.alternateItem == this.currentReference)
			{
				list.Insert(0, item2Change);
			}
			else
			{
				list.Add(item2Change);
			}
		}
		return list;
	}

	// Token: 0x0600020E RID: 526 RVA: 0x0000CD28 File Offset: 0x0000AF28
	public List<Item2> GetItem2s()
	{
		List<Item2> list = new List<Item2>();
		foreach (ItemSwitcher.Item2Change item2Change in this.item2Changes)
		{
			list.Add(item2Change.alternateItem);
		}
		return list;
	}

	// Token: 0x0600020F RID: 527 RVA: 0x0000CD88 File Offset: 0x0000AF88
	private void Start()
	{
		this.currentItem2Comp = base.GetComponent<Item2>();
		if (!this.currentReference)
		{
			this.ForceChoose();
		}
	}

	// Token: 0x06000210 RID: 528 RVA: 0x0000CDA9 File Offset: 0x0000AFA9
	private void Update()
	{
		if (this.changeCondition == ItemSwitcher.ChangeCondition.Rotation)
		{
			this.ChooseItem();
		}
	}

	// Token: 0x06000211 RID: 529 RVA: 0x0000CDBC File Offset: 0x0000AFBC
	private void ChooseItem()
	{
		base.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Round(base.transform.rotation.eulerAngles.z));
		foreach (ItemSwitcher.Item2Change item2Change in this.item2Changes)
		{
			if (!(item2Change.alternateItem == this.currentReference) && ((base.transform.rotation.eulerAngles.z == 0f && item2Change.changeNumber == 0f) || base.transform.rotation.eulerAngles.z % 360f == item2Change.changeNumber % 360f))
			{
				this.ChangeItem2(item2Change);
			}
		}
	}

	// Token: 0x06000212 RID: 530 RVA: 0x0000CEB4 File Offset: 0x0000B0B4
	public void ForceChoose()
	{
		int num = -1;
		if (this.currentReference)
		{
			for (int i = 0; i < this.item2Changes.Count; i++)
			{
				if (this.item2Changes[i].alternateItem == this.currentReference)
				{
					num = i;
					break;
				}
			}
		}
		num++;
		if (num >= this.item2Changes.Count)
		{
			num = 0;
		}
		if (this.changeCondition == ItemSwitcher.ChangeCondition.Rotation)
		{
			base.transform.rotation *= Quaternion.Euler(0f, 0f, 180f);
		}
		this.ChangeItem2(this.item2Changes[num]);
	}

	// Token: 0x06000213 RID: 531 RVA: 0x0000CF60 File Offset: 0x0000B160
	private void ChangeItem2(ItemSwitcher.Item2Change changingTo)
	{
		Item2 alternateItem = changingTo.alternateItem;
		if (alternateItem == this.currentReference)
		{
			return;
		}
		if (this.currentReference != null)
		{
			SoundManager.main.PlaySFX("transform");
		}
		ValueChanger component = base.GetComponent<ValueChanger>();
		if (component)
		{
			component.ResetValueForSaving();
		}
		SpriteRenderer component2 = base.GetComponent<SpriteRenderer>();
		if (component2 && changingTo.sprite)
		{
			component2.sprite = changingTo.sprite;
		}
		this.currentReference = alternateItem;
		Item2 item = (Item2)base.gameObject.AddComponent(alternateItem.GetType());
		foreach (FieldInfo fieldInfo in alternateItem.GetType().GetFields())
		{
			fieldInfo.SetValue(item, fieldInfo.GetValue(alternateItem));
		}
		item.isOwned = this.currentItem2Comp.isOwned;
		item.isForSale = this.currentItem2Comp.isForSale;
		item.cost = this.currentItem2Comp.cost;
		item.currentCharges = this.currentItem2Comp.currentCharges;
		item.parentInventoryGrid = this.currentItem2Comp.parentInventoryGrid;
		item.lastParentInventoryGrid = this.currentItem2Comp.lastParentInventoryGrid;
		Object.Destroy(this.currentItem2Comp);
		this.currentItem2Comp = item;
		ItemMovement component3 = base.GetComponent<ItemMovement>();
		component3.myItem = this.currentItem2Comp;
		component3.SpawnParticles();
		ConnectionManager.main.FindManaNetworks();
		GameFlowManager.main.CheckConstants();
		if (component)
		{
			component.FindEffectInAFrameRoutine();
		}
	}

	// Token: 0x0400015D RID: 349
	public ItemSwitcher.ChangeCondition changeCondition;

	// Token: 0x0400015E RID: 350
	[SerializeField]
	private List<ItemSwitcher.Item2Change> item2Changes = new List<ItemSwitcher.Item2Change>();

	// Token: 0x0400015F RID: 351
	private Item2 currentItem2Comp;

	// Token: 0x04000160 RID: 352
	private Item2 currentReference;

	// Token: 0x0200027D RID: 637
	public enum ChangeCondition
	{
		// Token: 0x04000F61 RID: 3937
		Rotation,
		// Token: 0x04000F62 RID: 3938
		Toggle
	}

	// Token: 0x0200027E RID: 638
	[Serializable]
	public class Item2Change
	{
		// Token: 0x04000F63 RID: 3939
		public Sprite sprite;

		// Token: 0x04000F64 RID: 3940
		public float changeNumber;

		// Token: 0x04000F65 RID: 3941
		public Item2 alternateItem;
	}
}
