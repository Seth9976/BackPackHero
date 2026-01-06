using System;
using UnityEngine;

// Token: 0x02000064 RID: 100
public class ItemSlot : MonoBehaviour
{
	// Token: 0x060001E1 RID: 481 RVA: 0x0000BB33 File Offset: 0x00009D33
	private void Start()
	{
		this.overworld_BuildingInterface = base.GetComponentInParent<Overworld_BuildingInterface>();
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x0000BB44 File Offset: 0x00009D44
	public Overworld_InventoryItemButton GetItemButton()
	{
		foreach (object obj in base.transform)
		{
			Overworld_InventoryItemButton component = ((Transform)obj).GetComponent<Overworld_InventoryItemButton>();
			if (component)
			{
				return component;
			}
		}
		return null;
	}

	// Token: 0x060001E3 RID: 483 RVA: 0x0000BBAC File Offset: 0x00009DAC
	public void AddItemButton(Overworld_InventoryItemButton itemButton)
	{
	}

	// Token: 0x060001E4 RID: 484 RVA: 0x0000BBAE File Offset: 0x00009DAE
	public void RemoveItemButton(Overworld_InventoryItemButton itemButton)
	{
	}

	// Token: 0x0400014A RID: 330
	private Transform current;

	// Token: 0x0400014B RID: 331
	private Overworld_BuildingInterface overworld_BuildingInterface;

	// Token: 0x0400014C RID: 332
	[SerializeField]
	private GameObject inventoryItemPrefab;
}
