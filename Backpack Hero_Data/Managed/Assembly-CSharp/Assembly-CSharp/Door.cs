using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C5 RID: 197
public class Door : CustomInputHandler
{
	// Token: 0x06000597 RID: 1431 RVA: 0x00036F7B File Offset: 0x0003517B
	public void SetLevel(DungeonLevel.Zone zone)
	{
		this.isPresetZone = true;
		this.presetZone = zone;
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x00036F8C File Offset: 0x0003518C
	private void Start()
	{
		this.gameManager = GameManager.main;
		base.transform.parent.localPosition = new Vector3(base.transform.parent.localPosition.x, base.transform.parent.localPosition.y, 2f);
		SpriteRenderer component = base.GetComponent<SpriteRenderer>();
		DungeonLevel.Zone nextLevel = GameManager.main.GetNextLevel(this.doorNumber);
		if (this.isPresetZone)
		{
			nextLevel = this.presetZone;
		}
		if (nextLevel != DungeonLevel.Zone.Chaos)
		{
			Animator[] array = this.bagAnimators;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].gameObject.SetActive(false);
			}
		}
		base.GetComponent<SpriteRenderer>().sprite = this.gameManager.dungeonLevel.doorSprite;
		if (GameManager.main.dungeonLevel.currentFloor != DungeonLevel.Floor.boss && this.gameManager.floor != 0 && !this.isPresetZone && !this.forceRunComlpete)
		{
			this.isLocked = false;
			this.canBeUnlocked = false;
			switch (GameManager.main.dungeonLevel.zone)
			{
			case DungeonLevel.Zone.dungeon:
				component.sprite = this.unlockedZoneDoors[0];
				return;
			case DungeonLevel.Zone.deepCave:
				component.sprite = this.unlockedZoneDoors[3];
				return;
			case DungeonLevel.Zone.magmaCore:
				component.sprite = this.unlockedZoneDoors[5];
				return;
			case DungeonLevel.Zone.EnchantedSwamp:
				component.sprite = this.unlockedZoneDoors[4];
				return;
			case DungeonLevel.Zone.theBramble:
				component.sprite = this.unlockedZoneDoors[2];
				return;
			case DungeonLevel.Zone.ice:
				component.sprite = this.unlockedZoneDoors[6];
				return;
			default:
				return;
			}
		}
		else
		{
			if ((!Missions.IsNextLevel() && !this.isPresetZone) || this.forceRunComlpete || (GameManager.main.floor == 9 && !this.isPresetZone))
			{
				this.light.gameObject.SetActive(true);
				component.sprite = this.unlockedZoneDoors[1];
				return;
			}
			switch (nextLevel)
			{
			case DungeonLevel.Zone.dungeon:
				component.sprite = this.unlockedZoneDoors[0];
				return;
			case DungeonLevel.Zone.deepCave:
				this.zoneKeySprite = this.zoneKeys[3];
				if (!Singleton.Instance.storyMode || MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.openedDoorToDeepCave))
				{
					component.sprite = this.unlockedZoneDoors[3];
					return;
				}
				component.sprite = this.lockedZoneDoors[3];
				this.isLocked = true;
				if (MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedDeepCave))
				{
					this.canBeUnlocked = true;
					return;
				}
				break;
			case DungeonLevel.Zone.magmaCore:
				this.zoneKeySprite = this.zoneKeys[5];
				if (!Singleton.Instance.storyMode || MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.openedDoorToMagmaCore))
				{
					component.sprite = this.unlockedZoneDoors[5];
					return;
				}
				component.sprite = this.lockedZoneDoors[5];
				this.isLocked = true;
				if (MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedMagmaCore))
				{
					this.canBeUnlocked = true;
					return;
				}
				break;
			case DungeonLevel.Zone.EnchantedSwamp:
				this.zoneKeySprite = this.zoneKeys[4];
				if (!Singleton.Instance.storyMode || MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.openedDoorToEnchantedSwamp))
				{
					component.sprite = this.unlockedZoneDoors[4];
					return;
				}
				component.sprite = this.lockedZoneDoors[4];
				this.isLocked = true;
				if (MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedEnchantedSwamp))
				{
					this.canBeUnlocked = true;
					return;
				}
				break;
			case DungeonLevel.Zone.theBramble:
				this.zoneKeySprite = this.zoneKeys[2];
				if (!Singleton.Instance.storyMode || MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.openedDoorToBramble))
				{
					component.sprite = this.unlockedZoneDoors[2];
					return;
				}
				component.sprite = this.lockedZoneDoors[2];
				this.isLocked = true;
				if (MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedBramble))
				{
					this.canBeUnlocked = true;
					return;
				}
				break;
			case DungeonLevel.Zone.ice:
				this.zoneKeySprite = this.zoneKeys[6];
				if (!Singleton.Instance.storyMode || MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.openedDoorToFrozenHeart))
				{
					component.sprite = this.unlockedZoneDoors[6];
					return;
				}
				component.sprite = this.lockedZoneDoors[6];
				this.isLocked = true;
				if (MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedFrozenHeart))
				{
					this.canBeUnlocked = true;
					return;
				}
				break;
			case DungeonLevel.Zone.Chaos:
				this.zoneKeySprite = this.zoneKeys[7];
				if (Singleton.Instance.storyMode)
				{
					component.sprite = this.lockedZoneDoors[7];
					this.isLocked = true;
					this.canBeUnlocked = true;
				}
				break;
			default:
				return;
			}
			return;
		}
	}

	// Token: 0x06000599 RID: 1433 RVA: 0x000373D0 File Offset: 0x000355D0
	private void Update()
	{
	}

	// Token: 0x0600059A RID: 1434 RVA: 0x000373D2 File Offset: 0x000355D2
	private IEnumerator Wait(float time)
	{
		InputHandler inputHandler = base.GetComponentInParent<InputHandler>();
		if (inputHandler)
		{
			inputHandler.enabled = false;
		}
		DungeonLevel.Zone nextLevel = GameManager.main.GetNextLevel(this.doorNumber);
		if (this.isPresetZone)
		{
			nextLevel = this.presetZone;
		}
		switch (nextLevel)
		{
		case DungeonLevel.Zone.deepCave:
			MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.openedDoorToDeepCave);
			break;
		case DungeonLevel.Zone.magmaCore:
			MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.openedDoorToMagmaCore);
			break;
		case DungeonLevel.Zone.EnchantedSwamp:
			MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.openedDoorToEnchantedSwamp);
			break;
		case DungeonLevel.Zone.theBramble:
			MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.openedDoorToBramble);
			break;
		case DungeonLevel.Zone.ice:
			MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.openedDoorToFrozenHeart);
			break;
		}
		SpriteRenderer spriteRenderer = base.GetComponent<SpriteRenderer>();
		Player main = Player.main;
		Animator playerAnimator = main.GetComponentInChildren<Animator>();
		main.itemToInteractWith.sprite = this.zoneKeySprite;
		playerAnimator.speed = 0.25f;
		if (nextLevel == DungeonLevel.Zone.Chaos)
		{
			playerAnimator.speed = 0.15f;
			playerAnimator.Play("NoBackpack_UseItem", 0, 0f);
			SoundManager.main.PlaySFX("research");
			List<GameObject> objs = new List<GameObject>();
			int correct = 0;
			if (MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.satchelReadyForFinale))
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.satchelPrefab, Player.main.transform.position + Vector3.right * 1.5f, Quaternion.identity, Player.main.transform.parent);
				objs.Add(gameObject);
				int num = correct;
				correct = num + 1;
				yield return new WaitForSeconds(0.1f);
			}
			if (MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.cr8ReadyForFinale))
			{
				GameObject gameObject2 = Object.Instantiate<GameObject>(this.cr8Prefab, Player.main.transform.position + Vector3.right * 3f, Quaternion.identity, Player.main.transform.parent);
				objs.Add(gameObject2);
				int num = correct;
				correct = num + 1;
				yield return new WaitForSeconds(0.1f);
			}
			this.bagAnimators[0].enabled = true;
			if (MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.pochetteReadyForFinale))
			{
				GameObject gameObject3 = Object.Instantiate<GameObject>(this.pochettePrefab, Player.main.transform.position + Vector3.left * 1.5f, Quaternion.identity, Player.main.transform.parent);
				objs.Add(gameObject3);
				int num = correct;
				correct = num + 1;
				yield return new WaitForSeconds(0.1f);
			}
			if (MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.toteReadyForFinale))
			{
				GameObject gameObject4 = Object.Instantiate<GameObject>(this.totePrefab, Player.main.transform.position + Vector3.left * 3f, Quaternion.identity, Player.main.transform.parent);
				objs.Add(gameObject4);
				int num = correct;
				correct = num + 1;
				yield return new WaitForSeconds(0.1f);
			}
			yield return new WaitForSeconds(1.25f);
			if (MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.satchelReadyForFinale))
			{
				this.bagAnimators[1].enabled = true;
			}
			if (MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.cr8ReadyForFinale))
			{
				this.bagAnimators[2].enabled = true;
			}
			if (MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.pochetteReadyForFinale))
			{
				this.bagAnimators[3].enabled = true;
			}
			if (MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.toteReadyForFinale))
			{
				this.bagAnimators[4].enabled = true;
			}
			if (correct < 4)
			{
				SoundManager.main.PlaySFX("miniGameBad");
				yield return new WaitForSeconds(1.5f);
				foreach (GameObject gameObject5 in objs)
				{
					Object.Destroy(gameObject5);
				}
				playerAnimator.speed = 1f;
				playerAnimator.Play("Player_Idle");
				yield break;
			}
			yield return new WaitForSeconds(1.5f);
			objs = null;
		}
		else
		{
			playerAnimator.Play("Player_UseItem");
		}
		SoundManager.main.PlaySFX("openChest");
		yield return new WaitForSeconds(time);
		SoundManager.main.PlaySFX("destroy2");
		this.explodeParticles.SetActive(true);
		this.isLocked = false;
		yield return new WaitForSeconds(0.1f);
		playerAnimator.speed = 1f;
		spriteRenderer.sprite = this.unlockedZoneDoors[this.doorNumber];
		if (inputHandler)
		{
			inputHandler.enabled = true;
		}
		yield break;
	}

	// Token: 0x0600059B RID: 1435 RVA: 0x000373E8 File Offset: 0x000355E8
	public void OnMouseUpAsButton()
	{
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		this.ConsiderWalk();
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x000373FD File Offset: 0x000355FD
	public override void OnPressStart(string keyName, bool overrideKeyName)
	{
		if (keyName != "confirm" && !overrideKeyName)
		{
			return;
		}
		this.ConsiderWalk();
	}

	// Token: 0x0600059D RID: 1437 RVA: 0x00037418 File Offset: 0x00035618
	private void ConsiderWalk()
	{
		if (this.isLocked && this.canBeUnlocked)
		{
			if (this.waitCoroutine == null)
			{
				this.waitCoroutine = base.StartCoroutine(this.Wait(0.5f));
			}
			return;
		}
		if (this.isLocked)
		{
			SoundManager.main.PlaySFX("negative");
			PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm39"), base.transform.position);
			return;
		}
		this.StartWalk();
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x0003749C File Offset: 0x0003569C
	private void StartWalk()
	{
		if (SingleUI.IsViewingPopUp())
		{
			return;
		}
		if (this.isOpen || this.gameManager.travelling)
		{
			return;
		}
		if (Singleton.Instance.storyMode)
		{
			if (Missions.IsNextLevel() && !this.forceRunComlpete && (GameManager.main.floor != 9 || this.isPresetZone))
			{
				if (GameManager.main.floor != 0 && GameManager.main.floor <= 8)
				{
					ResourceGainedManager.main.ShowResourcePanels();
				}
			}
			else
			{
				this.gameManager.EndMissionSuccessfully();
				if (this.gameManager)
				{
					this.gameManager.EndRunAnalytics("won");
				}
			}
		}
		this.gameManager.inventoryPhase = GameManager.InventoryPhase.notInteractable;
		Card.RemoveAllCursorCards();
		DigitalInputSelectOnButton componentInParent = base.GetComponentInParent<DigitalInputSelectOnButton>();
		if (componentInParent)
		{
			componentInParent.RemoveSymbol();
		}
		this.isOpen = true;
		InputHandler componentInParent2 = base.GetComponentInParent<InputHandler>();
		if (componentInParent2)
		{
			componentInParent2.enabled = false;
		}
		base.StartCoroutine(this.WalkToDoor());
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x00037595 File Offset: 0x00035795
	public IEnumerator WalkToDoor()
	{
		DungeonLevel.Zone nextLevel = GameManager.main.GetNextLevel(this.doorNumber);
		if (this.isPresetZone)
		{
			nextLevel = this.presetZone;
		}
		if (nextLevel == DungeonLevel.Zone.Chaos)
		{
			RunTypeManager.AddProperty(RunType.RunProperty.Type.finalRun, null);
			MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.visitedChaoticDarkness);
		}
		if (GameManager.main.dungeonLevel.currentFloor == DungeonLevel.Floor.boss && (GameManager.main.dungeonLevel.zone == DungeonLevel.Zone.deepCave || GameManager.main.dungeonLevel.zone == DungeonLevel.Zone.EnchantedSwamp))
		{
			switch (Player.main.characterName)
			{
			case Character.CharacterName.Satchel:
				MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.satchelReadyForFinale);
				break;
			case Character.CharacterName.Tote:
				MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.toteReadyForFinale);
				break;
			case Character.CharacterName.CR8:
				MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.cr8ReadyForFinale);
				break;
			case Character.CharacterName.Pochette:
				MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.pochetteReadyForFinale);
				break;
			}
		}
		GameObject gameObject = GameObject.FindGameObjectWithTag("PopUpSpriteMask");
		if (gameObject)
		{
			gameObject.GetComponent<SpriteMask>().enabled = false;
		}
		Object.FindObjectOfType<DungeonPlayer>().locked = true;
		Transform playerTransform = Player.main.transform;
		Animator playerAnimator = playerTransform.GetComponentInChildren<Animator>();
		GameManager gameManager = GameManager.main;
		gameManager.travelling = true;
		playerAnimator.Play("Player_Run");
		while (playerTransform.position.x < base.transform.position.x - 0.36f)
		{
			playerTransform.position = Vector3.MoveTowards(playerTransform.position, new Vector3(base.transform.position.x - 0.35f, playerTransform.position.y, playerTransform.position.z), 5.2f * Time.deltaTime);
			yield return null;
		}
		playerAnimator.Play("Player_WalkAway 1", 0, 0f);
		float y = playerTransform.position.y;
		yield return new WaitForSeconds(0.1f);
		playerTransform.GetComponentInChildren<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
		yield return new WaitForSeconds(0.25f);
		gameManager.StartCoroutine(gameManager.NextLevel(this.doorNumber));
		yield return new WaitForSeconds(2.5f);
		Object.FindObjectOfType<DungeonPlayer>().locked = false;
		base.StopAllCoroutines();
		Object.Destroy(base.transform.parent.gameObject);
		Player.main.transform.position = new Vector3(Player.main.transform.position.x, y, Player.main.transform.position.z);
		yield break;
	}

	// Token: 0x0400044F RID: 1103
	[SerializeField]
	private GameObject explodeParticles;

	// Token: 0x04000450 RID: 1104
	[SerializeField]
	private Sprite[] lockedZoneDoors;

	// Token: 0x04000451 RID: 1105
	[SerializeField]
	private Sprite[] unlockedZoneDoors;

	// Token: 0x04000452 RID: 1106
	[SerializeField]
	private Sprite[] zoneKeys;

	// Token: 0x04000453 RID: 1107
	[SerializeField]
	private Sprite[] sprites;

	// Token: 0x04000454 RID: 1108
	[SerializeField]
	private GameObject light;

	// Token: 0x04000455 RID: 1109
	[SerializeField]
	private GameObject satchelPrefab;

	// Token: 0x04000456 RID: 1110
	[SerializeField]
	private GameObject totePrefab;

	// Token: 0x04000457 RID: 1111
	[SerializeField]
	private GameObject pochettePrefab;

	// Token: 0x04000458 RID: 1112
	[SerializeField]
	private GameObject cr8Prefab;

	// Token: 0x04000459 RID: 1113
	[SerializeField]
	private Animator[] bagAnimators;

	// Token: 0x0400045A RID: 1114
	[SerializeField]
	private bool forceRunComlpete;

	// Token: 0x0400045B RID: 1115
	private GameManager gameManager;

	// Token: 0x0400045C RID: 1116
	public bool isOpen;

	// Token: 0x0400045D RID: 1117
	public int doorNumber;

	// Token: 0x0400045E RID: 1118
	private Sprite zoneKeySprite;

	// Token: 0x0400045F RID: 1119
	private bool isLocked;

	// Token: 0x04000460 RID: 1120
	private bool canBeUnlocked;

	// Token: 0x04000461 RID: 1121
	public bool isPresetZone;

	// Token: 0x04000462 RID: 1122
	public DungeonLevel.Zone presetZone;

	// Token: 0x04000463 RID: 1123
	private Coroutine waitCoroutine;
}
