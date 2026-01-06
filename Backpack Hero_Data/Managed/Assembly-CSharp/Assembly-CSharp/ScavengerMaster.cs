using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Token: 0x020000D0 RID: 208
public class ScavengerMaster : DungeonEventSpecial
{
	// Token: 0x06000604 RID: 1540 RVA: 0x0003B650 File Offset: 0x00039850
	private void Start()
	{
		this.itemTypeDropDown.options = new List<TMP_Dropdown.OptionData>
		{
			new TMP_Dropdown.OptionData(LangaugeManager.main.GetTextByKey("Weapon")),
			new TMP_Dropdown.OptionData(LangaugeManager.main.GetTextByKey("Armor")),
			new TMP_Dropdown.OptionData(LangaugeManager.main.GetTextByKey("Shield")),
			new TMP_Dropdown.OptionData(LangaugeManager.main.GetTextByKey("Consumable")),
			new TMP_Dropdown.OptionData(LangaugeManager.main.GetTextByKey("Accessory"))
		};
		this.donationDropDown.options = new List<TMP_Dropdown.OptionData>
		{
			new TMP_Dropdown.OptionData("5"),
			new TMP_Dropdown.OptionData("10"),
			new TMP_Dropdown.OptionData("15"),
			new TMP_Dropdown.OptionData("30")
		};
		LangaugeManager.main.SetFont(base.transform);
	}

	// Token: 0x06000605 RID: 1541 RVA: 0x0003B750 File Offset: 0x00039950
	public void ChangePrice()
	{
		RandomEventMaster component = base.GetComponent<RandomEventMaster>();
		if (component)
		{
			component.dungeonEvent.RemoveEventProperty(DungeonEvent.EventProperty.Type.increaseCost);
			if (this.donationDropDown.value == 0)
			{
				component.dungeonEvent.AddEventProperty(DungeonEvent.EventProperty.Type.increaseCost, 0);
			}
			else if (this.donationDropDown.value == 1)
			{
				component.dungeonEvent.AddEventProperty(DungeonEvent.EventProperty.Type.increaseCost, 5);
			}
			else if (this.donationDropDown.value == 2)
			{
				component.dungeonEvent.AddEventProperty(DungeonEvent.EventProperty.Type.increaseCost, 10);
			}
			else if (this.donationDropDown.value == 3)
			{
				component.dungeonEvent.AddEventProperty(DungeonEvent.EventProperty.Type.increaseCost, 25);
			}
			component.UpdateGoldCosts();
		}
	}

	// Token: 0x06000606 RID: 1542 RVA: 0x0003B7F4 File Offset: 0x000399F4
	public void SetFont(Transform t)
	{
		LangaugeManager.main.SetFont(t);
	}

	// Token: 0x06000607 RID: 1543 RVA: 0x0003B801 File Offset: 0x00039A01
	private void Update()
	{
	}

	// Token: 0x06000608 RID: 1544 RVA: 0x0003B803 File Offset: 0x00039A03
	public override void Play()
	{
		this.dropdowns.SetActive(false);
	}

	// Token: 0x06000609 RID: 1545 RVA: 0x0003B814 File Offset: 0x00039A14
	public override void SpecialEffect()
	{
		DungeonSpawner dungeonSpawner = Object.FindObjectOfType<DungeonSpawner>();
		dungeonSpawner.SetDungeonPropertyString(DungeonSpawner.DungeonProperty.Type.scavenger, this.itemTypeDropDown.value.ToString() ?? "");
		dungeonSpawner.SetDungeonPropertyValue(DungeonSpawner.DungeonProperty.Type.scavenger, this.donationDropDown.value);
	}

	// Token: 0x040004DB RID: 1243
	[SerializeField]
	private GameObject dropdowns;

	// Token: 0x040004DC RID: 1244
	[SerializeField]
	private TMP_Dropdown itemTypeDropDown;

	// Token: 0x040004DD RID: 1245
	[SerializeField]
	private TMP_Dropdown donationDropDown;
}
