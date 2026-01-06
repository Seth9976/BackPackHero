using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000121 RID: 289
public class ActionButtonManager : MonoBehaviour
{
	// Token: 0x060009E1 RID: 2529 RVA: 0x00064CB6 File Offset: 0x00062EB6
	private void Start()
	{
		this.gameManager = GameManager.main;
		this.gameFlowManager = GameFlowManager.main;
	}

	// Token: 0x060009E2 RID: 2530 RVA: 0x00064CCE File Offset: 0x00062ECE
	private void Update()
	{
	}

	// Token: 0x060009E3 RID: 2531 RVA: 0x00064CD0 File Offset: 0x00062ED0
	public IEnumerator CreateButtons(List<ActionButtonManager.Type> types)
	{
		this.numberOfCombatButtons = 0;
		this.numberOfOutOfCombatButtons = 0;
		foreach (object obj in this.combatButtonsParent)
		{
			Transform transform = (Transform)obj;
			if (!transform.CompareTag("Scenery"))
			{
				Object.Destroy(transform.gameObject);
			}
		}
		foreach (object obj2 in this.outOfCombatButtonsParent)
		{
			Transform transform2 = (Transform)obj2;
			if (!transform2.CompareTag("Scenery"))
			{
				Object.Destroy(transform2.gameObject);
			}
		}
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		bool flag = true;
		foreach (ActionButtonManager.Type type in types)
		{
			GameObject gameObject = this.CreateButton(type);
			if (gameObject && flag)
			{
				flag = false;
			}
			else if (gameObject)
			{
				DigitalInputSelectOnButton component = gameObject.GetComponent<DigitalInputSelectOnButton>();
				if (component)
				{
					component.RemoveSymbolAndDisable();
				}
			}
		}
		int num = 0;
		foreach (object obj3 in this.combatButtonsParent)
		{
			Transform transform3 = (Transform)obj3;
			if (!transform3.CompareTag("Scenery"))
			{
				InputHandler component2 = transform3.GetComponent<InputHandler>();
				if (transform3)
				{
					if (num == 0)
					{
						component2.SetKey(InputHandler.Key.A);
					}
					if (num == 1)
					{
						component2.SetKey(InputHandler.Key.B);
					}
					if (num == 2)
					{
						component2.SetKey(InputHandler.Key.X);
					}
					if (num == 3)
					{
						component2.SetKey(InputHandler.Key.Y);
					}
				}
				num++;
			}
		}
		if (this.combatButtonsParent.transform.childCount == 1)
		{
			this.combatButtonsParent.transform.GetChild(0).gameObject.SetActive(false);
		}
		else
		{
			this.combatButtonsParent.transform.GetChild(0).gameObject.SetActive(true);
		}
		num = 0;
		foreach (object obj4 in this.outOfCombatButtonsParent)
		{
			Transform transform4 = (Transform)obj4;
			if (!transform4.CompareTag("Scenery"))
			{
				InputHandler component3 = transform4.GetComponent<InputHandler>();
				if (transform4)
				{
					if (num == 0)
					{
						component3.SetKey(InputHandler.Key.A);
					}
					if (num == 1)
					{
						component3.SetKey(InputHandler.Key.B);
					}
					if (num == 2)
					{
						component3.SetKey(InputHandler.Key.X);
					}
					if (num == 3)
					{
						component3.SetKey(InputHandler.Key.Y);
					}
				}
				num++;
			}
		}
		if (this.outOfCombatButtonsParent.transform.childCount == 1)
		{
			this.outOfCombatButtonsParent.transform.GetChild(0).gameObject.SetActive(false);
		}
		else
		{
			this.outOfCombatButtonsParent.transform.GetChild(0).gameObject.SetActive(true);
		}
		yield break;
	}

	// Token: 0x060009E4 RID: 2532 RVA: 0x00064CE8 File Offset: 0x00062EE8
	private GameObject CreateButton(ActionButtonManager.Type type)
	{
		GameObject gameObject = null;
		if (type == ActionButtonManager.Type.Reorganize)
		{
			gameObject = Object.Instantiate<GameObject>(this.actionButtonPrefab, Vector3.zero, Quaternion.identity, this.combatButtonsParent);
			gameObject.GetComponent<ReplacementText>().key = "b5";
			gameObject.GetComponentInChildren<SimpleHoverText>().SetText("bd2");
			gameObject.GetComponent<Button>().onClick.AddListener(new UnityAction(this.Reorganize));
			this.numberOfCombatButtons++;
		}
		else if (type == ActionButtonManager.Type.Scratch)
		{
			gameObject = Object.Instantiate<GameObject>(this.actionButtonPrefab, Vector3.zero, Quaternion.identity, this.combatButtonsParent);
			gameObject.GetComponent<ReplacementText>().key = "b6";
			gameObject.GetComponentInChildren<SimpleHoverText>().SetText("bd1");
			gameObject.GetComponent<Button>().onClick.AddListener(new UnityAction(this.Scratch));
			this.numberOfCombatButtons++;
		}
		else if (type == ActionButtonManager.Type.Whistle)
		{
			gameObject = Object.Instantiate<GameObject>(this.actionButtonPrefab, Vector3.zero, Quaternion.identity, this.combatButtonsParent);
			gameObject.GetComponent<ReplacementText>().key = "b14";
			gameObject.GetComponentInChildren<SimpleHoverText>().SetText("bd14");
			gameObject.GetComponent<Button>().onClick.AddListener(new UnityAction(this.Whistle));
			this.numberOfCombatButtons++;
		}
		else if (type == ActionButtonManager.Type.Test)
		{
			gameObject = Object.Instantiate<GameObject>(this.actionButtonPrefab, Vector3.zero, Quaternion.identity, this.outOfCombatButtonsParent);
			gameObject.GetComponent<ReplacementText>().key = "b10";
			gameObject.GetComponentInChildren<SimpleHoverText>().SetText("bd3");
			gameObject.GetComponent<Button>().onClick.AddListener(new UnityAction(this.Test));
			this.numberOfOutOfCombatButtons++;
		}
		else if (type == ActionButtonManager.Type.RunOnce)
		{
			gameObject = Object.Instantiate<GameObject>(this.actionButtonPrefab, Vector3.zero, Quaternion.identity, this.combatButtonsParent);
			gameObject.GetComponent<ReplacementText>().key = "b11";
			gameObject.GetComponentInChildren<SimpleHoverText>().SetText("bd4");
			gameObject.GetComponent<Button>().onClick.AddListener(new UnityAction(this.RunOnce));
			this.numberOfCombatButtons++;
		}
		else if (type == ActionButtonManager.Type.RunForever)
		{
			gameObject = Object.Instantiate<GameObject>(this.actionButtonPrefab, Vector3.zero, Quaternion.identity, this.combatButtonsParent);
			gameObject.GetComponent<ReplacementText>().key = "b13";
			gameObject.GetComponent<Button>().onClick.AddListener(new UnityAction(this.RunForever));
			this.numberOfCombatButtons++;
			gameObject = Object.Instantiate<GameObject>(this.actionButtonPrefab, Vector3.zero, Quaternion.identity, this.outOfCombatButtonsParent);
			gameObject.GetComponent<ReplacementText>().key = "b13";
			gameObject.GetComponent<Button>().onClick.AddListener(new UnityAction(this.RunForever));
			gameObject.GetComponentInChildren<SimpleHoverText>().SetText("bd5");
			this.numberOfOutOfCombatButtons++;
		}
		else if (type == ActionButtonManager.Type.RecallPets)
		{
			gameObject = Object.Instantiate<GameObject>(this.actionButtonPrefab, Vector3.zero, Quaternion.identity, this.combatButtonsParent);
			gameObject.GetComponent<ReplacementText>().key = "b15";
			gameObject.GetComponentInChildren<SimpleHoverText>().SetText("bd15");
			gameObject.GetComponent<Button>().onClick.AddListener(new UnityAction(this.RecallPets));
			this.numberOfCombatButtons++;
		}
		else if (type == ActionButtonManager.Type.RemoveCarvings)
		{
			gameObject = Object.Instantiate<GameObject>(this.actionButtonPrefab, Vector3.zero, Quaternion.identity, this.combatButtonsParent);
			gameObject.GetComponent<ReplacementText>().key = "b16";
			gameObject.GetComponentInChildren<SimpleHoverText>().SetText("bd16");
			gameObject.GetComponent<Button>().onClick.AddListener(new UnityAction(this.RemoveCarvings));
			this.numberOfCombatButtons++;
		}
		return gameObject;
	}

	// Token: 0x060009E5 RID: 2533 RVA: 0x000650C0 File Offset: 0x000632C0
	public void RemoveCarvings()
	{
		if (this.gameFlowManager.battlePhase != GameFlowManager.BattlePhase.playerTurn)
		{
			return;
		}
		if (this.gameFlowManager.isCheckingEffects)
		{
			return;
		}
		GameManager.main.EndChooseItem();
		Player main = Player.main;
		Tote tote = Object.FindObjectOfType<Tote>();
		int num = 1 + Item2.GetEffectValues(Item2.Effect.Type.ChangeCostOfClear);
		if (!tote || !tote.ThereIsAPlayedCarving())
		{
			this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm25"));
		}
		else if (main.AP >= num)
		{
			SoundManager.main.PlaySFX("cantMoveHere");
			this.gameFlowManager.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onClearCarvings, null, null, true, false);
			foreach (GameObject gameObject in tote.GetActiveCarvings())
			{
				if (gameObject)
				{
					Item2 component = gameObject.GetComponent<Item2>();
					this.gameFlowManager.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onDiscard, component, null, true, false);
					Item2.Effect effect = new Item2.Effect
					{
						type = Item2.Effect.Type.ItemStatusEffect,
						itemStatusEffect = new List<Item2.ItemStatusEffect>
						{
							new Item2.ItemStatusEffect
							{
								applyRightAway = false,
								type = Item2.ItemStatusEffect.Type.DiscardCarving,
								length = Item2.ItemStatusEffect.Length.turns
							}
						}
					};
					this.gameFlowManager.itemStatusEffectsToApplyAtEndOfQueuedActions.Add(new GameFlowManager.ItemStatusEffectToApplyAtEndOfQueuedActions("clear", component, effect, false));
				}
			}
			this.gameFlowManager.AddCombatStat(GameFlowManager.CombatStat.Type.boardsCleared, 1, null);
			int num2 = tote.CalculateClearTotalDamage();
			this.gameFlowManager.StartCoroutine(this.gameFlowManager.Scratch(num2, num, true));
		}
		else
		{
			this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm15"));
		}
		Item2.SetAllItemColors();
	}

	// Token: 0x060009E6 RID: 2534 RVA: 0x0006527C File Offset: 0x0006347C
	public void RecallPets()
	{
		if (this.gameFlowManager.battlePhase != GameFlowManager.BattlePhase.playerTurn)
		{
			return;
		}
		GameManager.main.EndChooseItem();
		Player main = Player.main;
		if (main.AP >= 1)
		{
			main.ChangeAP(-1);
			PetItem2.RemoveAllPets();
			Item2.SetAllItemColors();
			return;
		}
		this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm15"));
	}

	// Token: 0x060009E7 RID: 2535 RVA: 0x000652DD File Offset: 0x000634DD
	public void Scratch()
	{
		GameManager.main.EndChooseItem();
		this.gameManager.ScratchButton();
	}

	// Token: 0x060009E8 RID: 2536 RVA: 0x000652F4 File Offset: 0x000634F4
	public void Whistle()
	{
		GameManager.main.EndChooseItem();
		this.gameManager.WhistleButton();
	}

	// Token: 0x060009E9 RID: 2537 RVA: 0x0006530B File Offset: 0x0006350B
	public void Reorganize()
	{
		GameManager.main.EndChooseItem();
		this.gameManager.ReorganizeButton();
		GameFlowManager main = GameFlowManager.main;
		Player main2 = Player.main;
	}

	// Token: 0x060009EA RID: 2538 RVA: 0x00065330 File Offset: 0x00063530
	public void Test()
	{
		GameManager.main.EndChooseItem();
		foreach (Item2 item in Item2.GetItemOfType(Item2.ItemType.Core, Item2.GetAllItemsInGrid()))
		{
			CR8Manager.instance.Test(item.gameObject);
		}
	}

	// Token: 0x060009EB RID: 2539 RVA: 0x0006539C File Offset: 0x0006359C
	public void RunOnce()
	{
		GameManager.main.EndChooseItem();
		CR8Manager cr8Manager = Object.FindObjectOfType<CR8Manager>();
		foreach (Item2 item in Item2.GetItemOfType(Item2.ItemType.Core, Item2.GetAllItemsInGrid()))
		{
			GameFlowManager.main.StartCoroutine(cr8Manager.StartTurn(false, item.gameObject));
		}
	}

	// Token: 0x060009EC RID: 2540 RVA: 0x00065418 File Offset: 0x00063618
	public void RunForever()
	{
		GameManager.main.EndChooseItem();
		CR8Manager cr8Manager = Object.FindObjectOfType<CR8Manager>();
		cr8Manager.setToRunForever = !cr8Manager.setToRunForever;
		if (!cr8Manager.setToRunForever && GameFlowManager.main.isRunningAutoEnd)
		{
			GameFlowManager.main.interupt = true;
		}
		this.SetButtonText();
	}

	// Token: 0x060009ED RID: 2541 RVA: 0x00065467 File Offset: 0x00063667
	public void DisableBattleButtons()
	{
		if (this.gameManager.battleActionButtons.activeInHierarchy)
		{
			this.gameManager.battleActionButtons.GetComponent<Animator>().Play("BattleActionButtonsOut");
		}
	}

	// Token: 0x060009EE RID: 2542 RVA: 0x00065498 File Offset: 0x00063698
	public void EnableBattleButtons()
	{
		if (!this.gameManager)
		{
			this.gameManager = GameManager.main;
		}
		GameManager.main.EndChooseItem();
		if (this.numberOfCombatButtons > 0)
		{
			this.gameManager.battleActionButtons.SetActive(true);
		}
		if (this.gameManager.outOfBattleButtons.activeInHierarchy)
		{
			this.gameManager.outOfBattleButtons.GetComponent<Animator>().Play("BattleActionButtonsOut");
		}
		this.SetButtonText();
	}

	// Token: 0x060009EF RID: 2543 RVA: 0x00065514 File Offset: 0x00063714
	public void EnableOutOfBattleButtons()
	{
		if (!this.gameManager)
		{
			this.gameManager = GameManager.main;
		}
		GameManager.main.EndChooseItem();
		if (this.gameManager.battleActionButtons.activeInHierarchy)
		{
			this.gameManager.battleActionButtons.GetComponent<Animator>().Play("BattleActionButtonsOut");
		}
		if (this.numberOfOutOfCombatButtons > 0)
		{
			this.gameManager.outOfBattleButtons.SetActive(true);
		}
		this.SetButtonText();
	}

	// Token: 0x060009F0 RID: 2544 RVA: 0x00065590 File Offset: 0x00063790
	public void SetButtonText()
	{
		CR8Manager cr8Manager = Object.FindObjectOfType<CR8Manager>();
		foreach (ReplacementText replacementText in Object.FindObjectsOfType<ReplacementText>())
		{
			if (replacementText.key == "b12" || replacementText.key == "b13")
			{
				if (cr8Manager.setToRunForever)
				{
					replacementText.key = "b12";
				}
				else
				{
					replacementText.key = "b13";
				}
			}
			replacementText.ReplaceText();
		}
	}

	// Token: 0x04000815 RID: 2069
	[SerializeField]
	private GameObject actionButtonPrefab;

	// Token: 0x04000816 RID: 2070
	[SerializeField]
	private Transform combatButtonsParent;

	// Token: 0x04000817 RID: 2071
	[SerializeField]
	private Transform outOfCombatButtonsParent;

	// Token: 0x04000818 RID: 2072
	[SerializeField]
	private GameObject buttonPrompt;

	// Token: 0x04000819 RID: 2073
	private GameManager gameManager;

	// Token: 0x0400081A RID: 2074
	private GameFlowManager gameFlowManager;

	// Token: 0x0400081B RID: 2075
	private int numberOfCombatButtons;

	// Token: 0x0400081C RID: 2076
	private int numberOfOutOfCombatButtons;

	// Token: 0x02000397 RID: 919
	public enum Type
	{
		// Token: 0x0400159E RID: 5534
		Reorganize,
		// Token: 0x0400159F RID: 5535
		Scratch,
		// Token: 0x040015A0 RID: 5536
		Test,
		// Token: 0x040015A1 RID: 5537
		RunOnce,
		// Token: 0x040015A2 RID: 5538
		RunForever,
		// Token: 0x040015A3 RID: 5539
		Whistle,
		// Token: 0x040015A4 RID: 5540
		RecallPets,
		// Token: 0x040015A5 RID: 5541
		RemoveCarvings
	}
}
