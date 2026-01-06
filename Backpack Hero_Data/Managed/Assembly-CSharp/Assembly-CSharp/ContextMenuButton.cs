using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Token: 0x020000B7 RID: 183
public class ContextMenuButton : MonoBehaviour
{
	// Token: 0x060004F8 RID: 1272 RVA: 0x0003065C File Offset: 0x0002E85C
	private void Start()
	{
		this.gameManager = GameManager.main;
		foreach (Item2.Cost cost in this.chosenContextMenuButton.costs)
		{
			cost.GetCurrentValue();
			if (cost.type == Item2.Cost.Type.gold)
			{
				this.goldCost.SetActive(true);
				this.goldCost.GetComponentInChildren<TextMeshProUGUI>().text = Item2.CalculateGoldCost(cost.currentValue).ToString() ?? "";
			}
			else if (cost.type == Item2.Cost.Type.energy && GameFlowManager.main.battlePhase != GameFlowManager.BattlePhase.outOfBattle)
			{
				this.energyCost.SetActive(true);
				this.energyCost.GetComponentInChildren<TextMeshProUGUI>().text = cost.currentValue.ToString() ?? "";
			}
		}
		ContextMenuButton.Type type = this.chosenContextMenuButton.type;
		TextMeshProUGUI componentInChildren = base.GetComponentInChildren<TextMeshProUGUI>();
		if (type == ContextMenuButton.Type.useItem)
		{
			componentInChildren.text = LangaugeManager.main.GetTextByKey("cm2");
			return;
		}
		if (type == ContextMenuButton.Type.viewCard)
		{
			componentInChildren.text = LangaugeManager.main.GetTextByKey("cm1");
			return;
		}
		if (type == ContextMenuButton.Type.useItemMax)
		{
			componentInChildren.text = LangaugeManager.main.GetTextByKey("cm3");
			return;
		}
		if (type == ContextMenuButton.Type.rerollItems)
		{
			componentInChildren.text = LangaugeManager.main.GetTextByKey("cm4");
			return;
		}
		if (type == ContextMenuButton.Type.createRoom)
		{
			componentInChildren.text = LangaugeManager.main.GetTextByKey("cm5");
			return;
		}
		if (type == ContextMenuButton.Type.startBattle)
		{
			componentInChildren.text = LangaugeManager.main.GetTextByKey("cm6");
			return;
		}
		if (type == ContextMenuButton.Type.addCombatReward)
		{
			componentInChildren.text = LangaugeManager.main.GetTextByKey("cm7");
			return;
		}
		if (type == ContextMenuButton.Type.duplicatePotion)
		{
			componentInChildren.text = LangaugeManager.main.GetTextByKey("cm8");
			return;
		}
		if (type == ContextMenuButton.Type.combineConsumables)
		{
			componentInChildren.text = LangaugeManager.main.GetTextByKey("cm9");
			return;
		}
		if (type == ContextMenuButton.Type.flipX)
		{
			componentInChildren.text = LangaugeManager.main.GetTextByKey("cm10");
			return;
		}
		if (type == ContextMenuButton.Type.test)
		{
			componentInChildren.text = LangaugeManager.main.GetTextByKey("b10");
			return;
		}
		if (type == ContextMenuButton.Type.returnCarving)
		{
			componentInChildren.text = LangaugeManager.main.GetTextByKey("cm11");
			return;
		}
		if (type == ContextMenuButton.Type.sellCarving)
		{
			componentInChildren.text = LangaugeManager.main.GetTextByKey("cm12");
			return;
		}
		if (type == ContextMenuButton.Type.openPouch)
		{
			componentInChildren.text = LangaugeManager.main.GetTextByKey("cm13");
			return;
		}
		if (type == ContextMenuButton.Type.closePouch)
		{
			componentInChildren.text = LangaugeManager.main.GetTextByKey("cm14");
			return;
		}
		if (type == ContextMenuButton.Type.viewInventory)
		{
			componentInChildren.text = LangaugeManager.main.GetTextByKey("cm15");
			return;
		}
		if (type == ContextMenuButton.Type.recallPet)
		{
			componentInChildren.text = LangaugeManager.main.GetTextByKey("cm16");
			return;
		}
		if (type == ContextMenuButton.Type.sellItem)
		{
			componentInChildren.text = LangaugeManager.main.GetTextByKey("cm17");
			return;
		}
		if (type == ContextMenuButton.Type.unlock)
		{
			componentInChildren.text = LangaugeManager.main.GetTextByKey("cm18");
			return;
		}
		if (type == ContextMenuButton.Type.transform)
		{
			componentInChildren.text = LangaugeManager.main.GetTextByKey("cm19");
			return;
		}
		if (type != ContextMenuButton.Type.selectForComboUse && type != ContextMenuButton.Type.alternateUse)
		{
			if (type == ContextMenuButton.Type.viewResearch)
			{
				componentInChildren.text = LangaugeManager.main.GetTextByKey("cm22");
			}
			return;
		}
		if (!GameFlowManager.main.selectedItem || GameFlowManager.main.selectedItem != ContextMenuManager.main.selectedItem)
		{
			componentInChildren.text = LangaugeManager.main.GetTextByKey("cm20");
			return;
		}
		componentInChildren.text = LangaugeManager.main.GetTextByKey("cm21");
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x000309F0 File Offset: 0x0002EBF0
	private void Update()
	{
	}

	// Token: 0x060004FA RID: 1274 RVA: 0x000309F4 File Offset: 0x0002EBF4
	public void Press()
	{
		Player main = Player.main;
		foreach (Item2.Cost cost in this.chosenContextMenuButton.costs)
		{
			if (cost.type == Item2.Cost.Type.gold && this.gameManager.GetCurrentGold() < Item2.CalculateGoldCost(Item2.GetCurrentCost(Item2.Cost.Type.gold, this.chosenContextMenuButton.costs)))
			{
				GameManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm27"));
				return;
			}
			if (cost.type == Item2.Cost.Type.energy && GameFlowManager.main.battlePhase != GameFlowManager.BattlePhase.outOfBattle && main.AP < cost.currentValue)
			{
				GameManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm15"));
				return;
			}
		}
		if (this.chosenContextMenuButton.useTime == ContextMenuButton.ContextMenuButtonAndCost.UseTime.outOfBattle && GameFlowManager.main.battlePhase != GameFlowManager.BattlePhase.outOfBattle)
		{
			GameManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm60"));
			return;
		}
		if (this.chosenContextMenuButton.useTime == ContextMenuButton.ContextMenuButtonAndCost.UseTime.inBattle && GameFlowManager.main.battlePhase != GameFlowManager.BattlePhase.playerTurn)
		{
			GameManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm59"));
			return;
		}
		if (this.chosenContextMenuButton.useTime == ContextMenuButton.ContextMenuButtonAndCost.UseTime.duringCombatReward && !GameManager.main.limitedItemReorganize)
		{
			GameManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm61"));
			return;
		}
		if (this.chosenContextMenuButton.useTime == ContextMenuButton.ContextMenuButtonAndCost.UseTime.inAnEmptyRoom && !GameManager.main.InEmptyRoom(true))
		{
			GameManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm2"));
			return;
		}
		if (ContextMenuManager.main.selectedItem.isForSale && !ContextMenuManager.main.selectedItem.itemMovement.inGrid && this.chosenContextMenuButton.type != ContextMenuButton.Type.viewCard && this.chosenContextMenuButton.type != ContextMenuButton.Type.viewResearch)
		{
			GameManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm62"));
			return;
		}
		if (this.chosenContextMenuButton.timeToRemoveCost == ContextMenuButton.ContextMenuButtonAndCost.TimeToRemoveCost.onClick && this.chosenContextMenuButton.costs.Count > 0)
		{
			Item2.DetractCosts(ContextMenuManager.main.selectedItem, this.chosenContextMenuButton.costs, null);
		}
		ContextMenuManager.main.Command(this.chosenContextMenuButton.type, this.chosenContextMenuButton.costs, this.chosenContextMenuButton.playerAnimation);
	}

	// Token: 0x060004FB RID: 1275 RVA: 0x00030C74 File Offset: 0x0002EE74
	private void DetractCosts()
	{
	}

	// Token: 0x040003BD RID: 957
	public GameObject goldCost;

	// Token: 0x040003BE RID: 958
	public GameObject energyCost;

	// Token: 0x040003BF RID: 959
	public ContextMenuButton.ContextMenuButtonAndCost chosenContextMenuButton;

	// Token: 0x040003C0 RID: 960
	private GameManager gameManager;

	// Token: 0x020002E0 RID: 736
	[Serializable]
	public class ContextMenuButtonAndCost
	{
		// Token: 0x0400112A RID: 4394
		public ContextMenuButton.Type type;

		// Token: 0x0400112B RID: 4395
		public List<Item2.Cost> costs = new List<Item2.Cost>();

		// Token: 0x0400112C RID: 4396
		public ContextMenuButton.ContextMenuButtonAndCost.TimeToRemoveCost timeToRemoveCost;

		// Token: 0x0400112D RID: 4397
		public ContextMenuButton.ContextMenuButtonAndCost.UseTime useTime;

		// Token: 0x0400112E RID: 4398
		public Item2.PlayerAnimation playerAnimation = Item2.PlayerAnimation.UseDefault;

		// Token: 0x02000495 RID: 1173
		public enum TimeToRemoveCost
		{
			// Token: 0x04001ABF RID: 6847
			onClick,
			// Token: 0x04001AC0 RID: 6848
			onCombine
		}

		// Token: 0x02000496 RID: 1174
		public enum UseTime
		{
			// Token: 0x04001AC2 RID: 6850
			inOrOutOfBattle,
			// Token: 0x04001AC3 RID: 6851
			inBattle,
			// Token: 0x04001AC4 RID: 6852
			outOfBattle,
			// Token: 0x04001AC5 RID: 6853
			duringCombatReward,
			// Token: 0x04001AC6 RID: 6854
			inAnEmptyRoom
		}
	}

	// Token: 0x020002E1 RID: 737
	public enum Type
	{
		// Token: 0x04001130 RID: 4400
		useItem,
		// Token: 0x04001131 RID: 4401
		viewCard,
		// Token: 0x04001132 RID: 4402
		useItemMax,
		// Token: 0x04001133 RID: 4403
		rerollItems,
		// Token: 0x04001134 RID: 4404
		createRoom,
		// Token: 0x04001135 RID: 4405
		startBattle,
		// Token: 0x04001136 RID: 4406
		addCombatReward,
		// Token: 0x04001137 RID: 4407
		duplicatePotion,
		// Token: 0x04001138 RID: 4408
		combineConsumables,
		// Token: 0x04001139 RID: 4409
		flipX,
		// Token: 0x0400113A RID: 4410
		test,
		// Token: 0x0400113B RID: 4411
		returnCarving,
		// Token: 0x0400113C RID: 4412
		sellCarving,
		// Token: 0x0400113D RID: 4413
		openPouch,
		// Token: 0x0400113E RID: 4414
		closePouch,
		// Token: 0x0400113F RID: 4415
		viewInventory,
		// Token: 0x04001140 RID: 4416
		recallPet,
		// Token: 0x04001141 RID: 4417
		sellItem,
		// Token: 0x04001142 RID: 4418
		unlock,
		// Token: 0x04001143 RID: 4419
		transform,
		// Token: 0x04001144 RID: 4420
		selectForComboUse,
		// Token: 0x04001145 RID: 4421
		alternateUse,
		// Token: 0x04001146 RID: 4422
		viewResearch
	}
}
