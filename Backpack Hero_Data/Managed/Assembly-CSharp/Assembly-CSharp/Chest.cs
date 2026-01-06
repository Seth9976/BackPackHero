using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C3 RID: 195
public class Chest : CustomInputHandler
{
	// Token: 0x0600058A RID: 1418 RVA: 0x000366D4 File Offset: 0x000348D4
	public void Start()
	{
		this.gameManager = GameManager.main;
		if (this.padlock && this.dungeonEvent && this.dungeonEvent.GetEventPropertyValue(DungeonEvent.EventProperty.Type.mimic) > 0)
		{
			this.dungeonEvent.RemoveEventProperty(DungeonEvent.EventProperty.Type.mimic);
		}
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x00036721 File Offset: 0x00034921
	private void Update()
	{
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x00036723 File Offset: 0x00034923
	public void OnMouseUpAsButton()
	{
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		this.OnPressStart("confirm", false);
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x00036740 File Offset: 0x00034940
	public override void OnPressStart(string keyName, bool overrideKeyName)
	{
		if (keyName != "confirm" && !overrideKeyName)
		{
			return;
		}
		if (this.isOpen || this.gameManager.travelling || this.gameManager.viewingEvent)
		{
			return;
		}
		if (this.padlock)
		{
			this.gameManager.CreatePopUpWorld(LangaugeManager.main.GetTextByKey("rte3"), base.transform.position);
			SoundManager.main.PlaySFX("cantMoveHere");
			return;
		}
		DigitalInputSelectOnButton componentInParent = base.GetComponentInParent<DigitalInputSelectOnButton>();
		if (componentInParent)
		{
			componentInParent.RemoveSymbol();
		}
		base.StartCoroutine(this.OpenChestRoutine());
	}

	// Token: 0x0600058E RID: 1422 RVA: 0x000367EC File Offset: 0x000349EC
	public void UnlockAndOpen(Item2 item)
	{
		if ((this.padlock && !item) || !item.itemType.Contains(Item2.ItemType.Key))
		{
			return;
		}
		if (this.padlock && item)
		{
			Object.Destroy(this.padlock);
			item.itemMovement.DelayDestroy();
		}
		base.StartCoroutine(this.OpenChestRoutine());
	}

	// Token: 0x0600058F RID: 1423 RVA: 0x00036856 File Offset: 0x00034A56
	private IEnumerator OpenChestRoutine()
	{
		if (this.isOpen)
		{
			yield break;
		}
		InputHandler componentInParent = base.GetComponentInParent<InputHandler>();
		if (componentInParent)
		{
			Object.Destroy(componentInParent);
		}
		this.isOpen = true;
		this.gameManager.ShowInventory();
		yield return new WaitForSeconds(0.35f);
		GameObject gameObject = GameObject.FindGameObjectWithTag("MapButton");
		if (gameObject)
		{
			PulseImage component = gameObject.GetComponent<PulseImage>();
			if (component)
			{
				Object.Destroy(component);
			}
		}
		this.OpenChest();
		yield break;
	}

	// Token: 0x06000590 RID: 1424 RVA: 0x00036868 File Offset: 0x00034A68
	public void OpenChest()
	{
		this.isOpen = true;
		TutorialManager tutorialManager = Object.FindObjectOfType<TutorialManager>();
		Player main = Player.main;
		if (this.dungeonEvent)
		{
			this.dungeonEvent.FinishEvent();
			RunType.RunProperty runProperty = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.bossRush);
			if (this.dungeonEvent.GetEventPropertyValue(DungeonEvent.EventProperty.Type.mimic) > 0 && this.gameManager.dungeonLevel.currentFloor != DungeonLevel.Floor.boss && this.type == Chest.Type.standard && !runProperty)
			{
				Enemy component = Object.Instantiate<GameObject>(this.mimicPrefab, base.transform.position, Quaternion.identity, base.transform.parent).GetComponent<Enemy>();
				component.health = Mathf.Min(20 + 12 * this.gameManager.floor, 220);
				foreach (Enemy.PossibleAttack possibleAttack in component.possibleAttacks)
				{
					foreach (Enemy.Attack attack in possibleAttack.attacks)
					{
						if (attack.type != Enemy.Attack.Type.curseStatus)
						{
							float num = Mathf.Min(attack.powerRange.x * ((float)Mathf.Max(this.gameManager.floor, 1) / 7f * 5f), 25f);
							attack.powerRange = new Vector2(num, num * 1.2f);
						}
					}
				}
				GameFlowManager.main.StartCombat();
				Object.Destroy(base.gameObject);
				return;
			}
		}
		if (this.chestParticles)
		{
			Object.Instantiate<GameObject>(this.chestParticles, base.transform.position + Vector3.up * 0.1f, Quaternion.identity);
		}
		SoundManager.main.PlaySFX("openChest");
		base.GetComponent<SpriteRenderer>().sprite = this.openSprite;
		if (this.gameManager.floor != 0 || tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.trulyDone || !tutorialManager || (tutorialManager.playType != TutorialManager.PlayType.tutorial && (tutorialManager.playType != TutorialManager.PlayType.cr8Tutorial || Singleton.Instance.storyMode)))
		{
			Player.main.uncommonLuck += (float)this.uncommonLuckBonus;
			Player.main.rareLuck += (float)this.rareLuckBonus;
			Player.main.legendaryLuck += (float)this.legelndaryLuckBonus;
			if (this.type == Chest.Type.carving)
			{
				Object.FindObjectOfType<Tote>().SpawnCarvings();
				this.gameManager.StartSimpleLimitedItemGetPeriod(1);
				return;
			}
			if (this.type == Chest.Type.components)
			{
				ItemSpawner.InstantiateItems(ItemSpawner.GetItemsWithLuck(5, new List<Item2.ItemType> { Item2.ItemType.Component }, false, false, 0f));
				this.gameManager.StartSimpleLimitedItemGetPeriod(1);
				return;
			}
			if (this.type == Chest.Type.pets)
			{
				ItemSpawner.InstantiateItems(ItemSpawner.GetItems(3, new List<Item2.ItemType> { Item2.ItemType.Pet }, false, true));
				this.gameManager.StartSimpleLimitedItemGetPeriod(1);
				return;
			}
			if (this.type != Chest.Type.victory)
			{
				int num2 = this.numOfItems;
				RunType.RunProperty runProperty2 = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.chestsContainExtra);
				if (runProperty2 != null)
				{
					num2 += runProperty2.value;
				}
				Item2.GetAllItemTypesExcluding(null);
				if (main.characterName == Character.CharacterName.Tote)
				{
					num2--;
					if (this.gameManager.dungeonLevel.currentFloor == DungeonLevel.Floor.boss)
					{
						Item2.GetAllItemTypesExcluding(new List<Item2.ItemType> { Item2.ItemType.Consumable });
						num2 += 2;
					}
					else
					{
						new List<Item2.ItemType>().Add(Item2.ItemType.Consumable);
					}
				}
				List<GameObject> list = new List<GameObject>();
				List<ItemSpawner.ItemToSpawn> itemsWithLuck = ItemSpawner.GetItemsWithLuck(num2);
				if (this.type == Chest.Type.cursed)
				{
					List<Item2> items = ItemSpawner.GetItems(1, new List<Item2.Rarity> { Item2.Rarity.Common }, new List<Item2.ItemType> { Item2.ItemType.Curse }, false, true);
					itemsWithLuck.AddRange(ItemSpawner.ConvertToItemToSpawn(items));
				}
				ItemSpawner.InstantiateItemsFree(itemsWithLuck, true, base.transform.position);
				if (this.canBeCurseReplaced)
				{
					this.gameManager.ConsiderCurseReplacement(list);
				}
				this.gameManager.MoveAllItems();
				return;
			}
			if (Singleton.Instance.mission)
			{
				base.StartCoroutine(this.NewExit());
				return;
			}
		}
		else
		{
			if (tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.cr8battle7 || tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.cr8battle8 || tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.cr8battle9)
			{
				for (int i = 0; i < this.cr8StartingItems.Length; i++)
				{
					Object.Instantiate<GameObject>(this.cr8StartingItems[i], base.transform.position, Quaternion.identity);
				}
				this.gameManager.MoveAllItems();
				tutorialManager.tutorialSequence = TutorialManager.TutorialSequence.trulyDone;
				return;
			}
			if (tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.toteChest)
			{
				Tote tote = Object.FindObjectOfType<Tote>();
				for (int j = 0; j < this.toteStartingItems.Length; j++)
				{
					GameObject gameObject = this.toteStartingItems[j];
					GameObject gameObject2 = tote.CreateInWorldCarving(gameObject);
					tote.AlignCarving(gameObject2, j);
					gameObject2.GetComponent<ItemMovement>().outOfInventoryPosition = new Vector3(gameObject2.transform.position.x, -2.65f, 0f);
				}
				this.gameManager.StartSimpleLimitedItemGetPeriod(6);
				tutorialManager.CombatStart();
				this.gameManager.finishReorganizingButton.SetActive(false);
				return;
			}
			for (int k = 0; k < this.startingItems.Length; k++)
			{
				GameObject gameObject3 = this.startingItems[k];
				if (k != 0)
				{
					Object.Instantiate<GameObject>(gameObject3, base.transform.position, Quaternion.identity);
				}
				else
				{
					Object.Instantiate<GameObject>(gameObject3, base.transform.position, Quaternion.Euler(0f, 0f, -90f));
				}
			}
			this.gameManager.MoveAllItems();
			this.gameManager.StartCoroutine(this.gameManager.StartTutorialInit());
			if (Object.FindObjectOfType<TutorialManager>())
			{
				this.gameManager.StartCoroutine(this.gameManager.StartTutorial());
			}
		}
	}

	// Token: 0x06000591 RID: 1425 RVA: 0x00036E64 File Offset: 0x00035064
	public IEnumerator NewExit()
	{
		Missions mission = Singleton.Instance.mission;
		List<Item2> list = new List<Item2>();
		List<Item2> list2 = new List<Item2>();
		List<Overworld_Structure> list3 = new List<Overworld_Structure>();
		foreach (GameObject gameObject in mission.rewards)
		{
			Overworld_Structure component = gameObject.GetComponent<Overworld_Structure>();
			if (component)
			{
				list3.Add(component);
			}
			else
			{
				Item2 component2 = gameObject.GetComponent<Item2>();
				if (component2)
				{
					if (!component2.itemType.Contains(Item2.ItemType.Loot))
					{
						list2.Add(component2);
					}
					else
					{
						this.gameManager.victoryItems.Add(component2);
						list.Add(component2);
					}
				}
			}
		}
		if (list3.Count > 0)
		{
			NewUnlockManager.main.OpenNewConstructionWindow(list3[0]);
			if (!MetaProgressSaveManager.main.availableBuildings.Contains(list3[0].ToString()))
			{
				MetaProgressSaveManager.main.availableBuildings.Add(list3[0].ToString());
			}
		}
		if (list2.Count > 0)
		{
			NewUnlockManager.main.OpenNewItemsWindow(list2);
			MetaProgressSaveManager.main.UnlockItems(list2);
		}
		foreach (Missions missions in mission.rewardsMissions)
		{
			NewUnlockManager.main.OpenNewMissionWindow(missions);
			MetaProgressSaveManager.main.AddMission(missions);
		}
		List<GameObject> list4 = ItemSpawner.InstantiateItemsFree(list, true, base.transform.position);
		Animator playerAnimator = Player.main.GetComponentInChildren<Animator>();
		if (list4.Count > 0)
		{
			Player.main.itemToInteractWith.sprite = list4[0].GetComponentInChildren<SpriteRenderer>().sprite;
			playerAnimator.speed = 0.4f;
			playerAnimator.Play("Player_UseItem");
		}
		yield return new WaitForSeconds(1.1f);
		playerAnimator.speed = 1f;
		Animator componentInParent = base.GetComponentInParent<Animator>();
		if (componentInParent)
		{
			componentInParent.Play("dungeonEventDespawnAndDontDestroy", 0, 0f);
		}
		yield return new WaitForSeconds(0.5f);
		GameObject gameObject2 = Object.Instantiate<GameObject>(this.exitPrefab, Player.main.transform.parent);
		gameObject2.transform.localPosition = new Vector3(this.gameManager.spawnPosition.position.x - 1f, gameObject2.transform.position.y, -2.5f);
		yield break;
	}

	// Token: 0x06000592 RID: 1426 RVA: 0x00036E73 File Offset: 0x00035073
	public IEnumerator WalkToOffScreen()
	{
		if (this.ending)
		{
			yield break;
		}
		this.ending = true;
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return null;
		Player main = Player.main;
		GameFlowManager gameFlowManager = GameFlowManager.main;
		if (Object.FindObjectOfType<TutorialManager>().playType != TutorialManager.PlayType.testing)
		{
			Object.FindObjectOfType<MetaProgressSaveManager>().AddNew();
		}
		Transform playerTransform = main.transform;
		Animator playerAnimator = playerTransform.GetComponentInChildren<Animator>();
		this.gameManager.travelling = true;
		foreach (SpriteRenderer spriteRenderer in main.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.flipX = !spriteRenderer.flipX;
		}
		playerAnimator.Play("Player_Run");
		playerAnimator.speed = 0.9f;
		while (playerTransform.position.x < base.transform.position.x - 1.46f)
		{
			playerTransform.position = Vector3.MoveTowards(playerTransform.position, new Vector3(-12f, playerTransform.position.y, playerTransform.position.z), 4.2f * Time.deltaTime);
			yield return null;
		}
		gameFlowManager.actionFinished = false;
		while (!gameFlowManager.actionFinished)
		{
			yield return null;
		}
		playerAnimator.speed = 1f;
		yield break;
	}

	// Token: 0x04000438 RID: 1080
	public Chest.Type type;

	// Token: 0x04000439 RID: 1081
	[SerializeField]
	private GameObject exitPrefab;

	// Token: 0x0400043A RID: 1082
	[SerializeField]
	private GameObject chestParticles;

	// Token: 0x0400043B RID: 1083
	[SerializeField]
	private Sprite openSprite;

	// Token: 0x0400043C RID: 1084
	[SerializeField]
	private GameObject[] startingItems;

	// Token: 0x0400043D RID: 1085
	[SerializeField]
	private GameObject[] cr8StartingItems;

	// Token: 0x0400043E RID: 1086
	[SerializeField]
	private GameObject[] toteStartingItems;

	// Token: 0x0400043F RID: 1087
	[SerializeField]
	private GameObject mimicPrefab;

	// Token: 0x04000440 RID: 1088
	[HideInInspector]
	public bool isOpen;

	// Token: 0x04000441 RID: 1089
	[HideInInspector]
	public DungeonEvent dungeonEvent;

	// Token: 0x04000442 RID: 1090
	[SerializeField]
	public GameObject padlock;

	// Token: 0x04000443 RID: 1091
	[SerializeField]
	private int numOfItems = 4;

	// Token: 0x04000444 RID: 1092
	[SerializeField]
	private int uncommonLuckBonus;

	// Token: 0x04000445 RID: 1093
	[SerializeField]
	private int rareLuckBonus;

	// Token: 0x04000446 RID: 1094
	[SerializeField]
	private int legelndaryLuckBonus;

	// Token: 0x04000447 RID: 1095
	private GameManager gameManager;

	// Token: 0x04000448 RID: 1096
	public bool canBeCurseReplaced;

	// Token: 0x04000449 RID: 1097
	private bool ending;

	// Token: 0x020002F7 RID: 759
	public enum Type
	{
		// Token: 0x040011BE RID: 4542
		standard,
		// Token: 0x040011BF RID: 4543
		carving,
		// Token: 0x040011C0 RID: 4544
		components,
		// Token: 0x040011C1 RID: 4545
		pets,
		// Token: 0x040011C2 RID: 4546
		victory,
		// Token: 0x040011C3 RID: 4547
		cursed
	}
}
