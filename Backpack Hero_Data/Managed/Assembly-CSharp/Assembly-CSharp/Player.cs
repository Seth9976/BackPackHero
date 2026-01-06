using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Token: 0x02000111 RID: 273
public class Player : MonoBehaviour
{
	// Token: 0x06000941 RID: 2369 RVA: 0x0005FA64 File Offset: 0x0005DC64
	private void Awake()
	{
		Player.main = this;
	}

	// Token: 0x06000942 RID: 2370 RVA: 0x0005FA6C File Offset: 0x0005DC6C
	private void OnDestroy()
	{
		Player.main = null;
	}

	// Token: 0x06000943 RID: 2371 RVA: 0x0005FA74 File Offset: 0x0005DC74
	private void Start()
	{
		this.ChooseCharacterSimpleStart();
		this.gameManager = GameManager.main;
		this.animator = base.GetComponentInChildren<Animator>();
		this.draggableCombat = base.GetComponentInChildren<DraggableCombat>();
		this.difference = new Vector3(-0.6f, 1.2f, 15f);
		this.APAnimator.gameObject.SetActive(false);
		this.mySpacerLocation = Object.Instantiate<GameObject>(this.spacerPrefab, base.transform.position, Quaternion.identity).transform;
		this.mySpacerLocation.SetParent(GameObject.FindGameObjectWithTag("PlayerSpacerParent").transform);
		this.mySpacerLocation.SetAsFirstSibling();
	}

	// Token: 0x06000944 RID: 2372 RVA: 0x0005FB20 File Offset: 0x0005DD20
	public void SetBagSize()
	{
		Transform transform = GameObject.FindGameObjectWithTag("GridParent").transform;
		foreach (object obj in transform)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
		if (this.characterName == Character.CharacterName.Satchel)
		{
			int num = 9;
			RunType.RunProperty runProperty = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.startingBackpackSize);
			if (runProperty)
			{
				num = 9 + Mathf.FloorToInt((float)((runProperty.value - 3) * 3));
			}
			List<PathFinding.Location> list;
			PathFinding.FindRandomConnectedSpaces(new Vector2(0f, 2.65f), 10, new Func<PathFinding.Location, bool>(PathFinding.OpenGridSpace), out list, num);
			using (List<PathFinding.Location>.Enumerator enumerator2 = list.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					PathFinding.Location location = enumerator2.Current;
					GameObject gameObject = Object.Instantiate<GameObject>(this.gridPrefab, Vector3.zero, Quaternion.identity, transform);
					gameObject.transform.position = new Vector3(location.position.x, location.position.y, 0f);
					gameObject.transform.localPosition = Vector3Int.RoundToInt(gameObject.transform.localPosition);
				}
				goto IL_0265;
			}
		}
		int num2 = Mathf.FloorToInt(this.chosenCharacter.defaultBagSize.x / 2f);
		RunType.RunProperty runProperty2 = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.startingBackpackSize);
		if (runProperty2)
		{
			num2 = Mathf.FloorToInt((float)((runProperty2.value - 1) / 2));
		}
		int num3 = Mathf.FloorToInt(this.chosenCharacter.defaultBagSize.y / 2f);
		for (int i = num2 * -1; i <= num2; i++)
		{
			for (int j = num3 * -1; j <= num3; j++)
			{
				bool flag = false;
				foreach (RaycastHit2D raycastHit2D in Physics2D.RaycastAll(transform.TransformPoint(new Vector2((float)i, (float)j)), Vector2.zero))
				{
					if (raycastHit2D.collider.GetComponent<GridSquare>())
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					Object.Instantiate<GameObject>(this.gridPrefab, Vector3.zero, Quaternion.identity, transform).transform.localPosition = new Vector3((float)i, (float)j, 0f);
				}
			}
		}
		IL_0265:
		ModularBackpack.SetAllBackpackSprites();
		Object.FindObjectOfType<LevelUpManager>().ResizeAllBackpacks();
	}

	// Token: 0x06000945 RID: 2373 RVA: 0x0005FDC0 File Offset: 0x0005DFC0
	public void NewRun()
	{
		this.stats.SetMaxHP(this.chosenCharacter.startingHealth);
		this.stats.SetHealth(this.stats.maxHealth);
	}

	// Token: 0x06000946 RID: 2374 RVA: 0x0005FDF0 File Offset: 0x0005DFF0
	public void ChooseCharacterSimpleStart()
	{
		this.characterName = Singleton.Instance.character;
		foreach (Character character in this.characterProperties)
		{
			if (character.characterName == this.characterName)
			{
				this.chosenCharacter = character;
			}
		}
	}

	// Token: 0x06000947 RID: 2375 RVA: 0x0005FE64 File Offset: 0x0005E064
	public void SetCostumeToBackpack()
	{
		this.animator.runtimeAnimatorController = this.standardPurseController;
	}

	// Token: 0x06000948 RID: 2376 RVA: 0x0005FE78 File Offset: 0x0005E078
	public void ChoseCharacter()
	{
		if (!this.gameManager)
		{
			this.gameManager = GameManager.main;
		}
		if (!this.animator)
		{
			this.animator = base.GetComponentInChildren<Animator>();
		}
		this.characterName = Singleton.Instance.character;
		foreach (Character character in this.characterProperties)
		{
			if (character.characterName == this.characterName)
			{
				this.chosenCharacter = character;
				this.animator.runtimeAnimatorController = character.animatorControllers[Mathf.Min(Singleton.Instance.costumeNumber, character.animatorControllers.Count - 1)];
				Object.FindObjectOfType<DungeonPlayer>().GetComponent<SpriteRenderer>().sprite = character.mapCharacterSprite[Mathf.Min(Singleton.Instance.costumeNumber, character.mapCharacterSprite.Count - 1)];
				GameObject.FindGameObjectWithTag("mapBackground").GetComponent<SpriteRenderer>().sprite = character.mapSprite;
			}
		}
		if (RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.doStoryIntro) && !MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.backpackCollected) && Singleton.Instance.storyMode)
		{
			this.animator.runtimeAnimatorController = this.purseSansBackpackAnimatorController;
		}
		this.APperTurn = this.chosenCharacter.defaultEnergyPerTurn;
		ActionButtonManager actionButtonManager = Object.FindObjectOfType<ActionButtonManager>();
		if (actionButtonManager)
		{
			actionButtonManager.StopAllCoroutines();
			actionButtonManager.StartCoroutine(actionButtonManager.CreateButtons(this.chosenCharacter.buttonTypes));
			actionButtonManager.EnableOutOfBattleButtons();
		}
		this.AddExperience(0, base.transform.position);
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x00060034 File Offset: 0x0005E234
	public void SpawnObjects()
	{
		if (!RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.dontStartWithStandard))
		{
			if (this.chosenCharacter.startingObjects.Count > 0)
			{
				foreach (GameObject gameObject in this.chosenCharacter.startingObjects)
				{
					Object.Instantiate<GameObject>(gameObject, Vector3.zero, Quaternion.identity);
				}
				this.gameManager.MoveAllItems();
			}
			this.SpawnStuffSuchAsPets();
			return;
		}
		if (RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.stillSpawnPets))
		{
			this.SpawnStuffSuchAsPets();
		}
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x000600DC File Offset: 0x0005E2DC
	private void SpawnStuffSuchAsPets()
	{
		int num = 0;
		if (this.chosenCharacter.startingObjectsForLimitedItemGet.Count > 0)
		{
			foreach (GameObject gameObject in this.chosenCharacter.startingObjectsForLimitedItemGet)
			{
				GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, Vector3.zero, Quaternion.identity);
				ItemMovement component = gameObject2.GetComponent<ItemMovement>();
				gameObject2.transform.position = new Vector3((float)(num * 2 - 2), -4.9f, 0f);
				component.outOfInventoryPosition = gameObject2.transform.localPosition;
				component.outOfInventoryRotation = gameObject2.transform.rotation;
				component.returnsToOutOfInventoryPosition = true;
				num++;
			}
			this.gameManager.StartSimpleLimitedItemGetPeriod(1);
			if (this.chosenCharacter.characterName == Character.CharacterName.Pochette)
			{
				Object.FindObjectOfType<TutorialManager>().ConsiderTutorial("pets");
			}
		}
	}

	// Token: 0x0600094B RID: 2379 RVA: 0x000601D0 File Offset: 0x0005E3D0
	private void Update()
	{
		if (this.mySpacerLocation && !this.draggableCombat.isDragging)
		{
			Vector3 vector = new Vector3(this.mySpacerLocation.transform.position.x, this.playerSpritePositionTransform.transform.position.y, this.playerSpritePositionTransform.transform.position.z);
			this.playerSpritePositionTransform.position = Vector3.MoveTowards(this.playerSpritePositionTransform.position, vector, 10f * Time.deltaTime);
		}
		else
		{
			this.playerSpritePositionTransform.localPosition = Vector3.MoveTowards(this.playerSpritePositionTransform.localPosition, Vector3.zero, 5f * Time.deltaTime);
		}
		this.apText.transform.parent.parent.position = this.playerSpritePositionTransform.position + this.difference;
	}

	// Token: 0x0600094C RID: 2380 RVA: 0x000602C4 File Offset: 0x0005E4C4
	public void ShowAP()
	{
		if (GameFlowManager.main.combatEndedAbruptly)
		{
			this.HideAP();
			return;
		}
		if (this.APAnimator && !this.APAnimator.gameObject.activeInHierarchy)
		{
			this.APAnimator.gameObject.SetActive(true);
			this.apText.text = this.AP.ToString() ?? "";
			this.APAnimator.Play("ApIntro", 0, 0f);
		}
	}

	// Token: 0x0600094D RID: 2381 RVA: 0x0006034C File Offset: 0x0005E54C
	public void HideAP()
	{
		if (this.APAnimator && this.APAnimator.gameObject.activeInHierarchy)
		{
			this.APAnimator.gameObject.SetActive(true);
			this.APAnimator.Play("ApExit", 0, 0f);
		}
		if (this.characterName == Character.CharacterName.Pochette)
		{
			CombatPet.HideAllAp();
		}
	}

	// Token: 0x0600094E RID: 2382 RVA: 0x000603B0 File Offset: 0x0005E5B0
	public void AddExperience(int value, Vector3 pos)
	{
		if (RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.noXP) != null)
		{
			return;
		}
		if (this.chosenCharacter == null)
		{
			return;
		}
		if (PlayerStatTracking.main)
		{
			PlayerStatTracking.main.AddStat("Experience gained", value);
		}
		this.experience += value;
		if (this.level < this.chosenCharacter.levelUps.Count - 1)
		{
			this.experienceText.text = string.Concat(new string[]
			{
				LangaugeManager.main.GetTextByKey("h2"),
				": ",
				this.experience.ToString(),
				" / ",
				this.chosenCharacter.levelUps[this.level + 1].xpRequired.ToString()
			});
		}
		else
		{
			this.experienceText.text = LangaugeManager.main.GetTextByKey("h3");
		}
		if (value == 0)
		{
			return;
		}
		GameObject gameObject = Object.Instantiate<GameObject>(this.experienceCounterPrefab, pos, Quaternion.identity, GameObject.FindGameObjectWithTag("PrimaryCanvas").GetComponent<Canvas>().transform);
		TextMeshProUGUI component = gameObject.GetComponent<TextMeshProUGUI>();
		component.text = "xp + " + Mathf.Abs(value).ToString();
		component.color = Color.yellow;
		Rigidbody2D component2 = gameObject.GetComponent<Rigidbody2D>();
		component2.velocity = new Vector2(0f, Random.Range(0.5f, 0.8f));
		component2.gravityScale = 0f;
		gameObject.GetComponent<Animator>();
	}

	// Token: 0x0600094F RID: 2383 RVA: 0x0006052C File Offset: 0x0005E72C
	public void IncrementLevel()
	{
		if (this.level >= this.chosenCharacter.levelUps.Count - 1)
		{
			return;
		}
		if (this.experience >= this.chosenCharacter.levelUps[this.level + 1].xpRequired)
		{
			this.level++;
			this.experience -= this.chosenCharacter.levelUps[this.level].xpRequired;
			this.AddExperience(0, base.transform.position);
			Character.LevelUp.Reward reward = this.chosenCharacter.levelUps[this.level].rewards[0];
			Object.FindObjectOfType<LevelUpManager>().spacesSaved += reward.rewardValue;
		}
	}

	// Token: 0x06000950 RID: 2384 RVA: 0x000605FD File Offset: 0x0005E7FD
	public void LevelUp()
	{
		this.IncrementLevel();
		LevelUpManager levelUpManager = Object.FindObjectOfType<LevelUpManager>();
		levelUpManager.StartCoroutine(levelUpManager.LevelUp(0));
	}

	// Token: 0x06000951 RID: 2385 RVA: 0x00060618 File Offset: 0x0005E818
	public void ChangeAP(int amount)
	{
		if (CR8Manager.instance.isRunning)
		{
			EnergyBall currentEnergyBall = EnergyBall.GetCurrentEnergyBall();
			if (currentEnergyBall)
			{
				currentEnergyBall.ChangeEnergy(amount);
			}
			return;
		}
		Animator component = this.gameManager.endTurnButton.GetComponent<Animator>();
		if (amount < 0)
		{
			this.APAnimator.Play("AP_PulseDown", 0, 0f);
			PlayerStatTracking.main.AddStat("Energy used", amount);
		}
		else if (amount > 0)
		{
			this.APAnimator.Play("AP_PulseUp", 0, 0f);
			PlayerStatTracking.main.AddStat("Energy gained", amount);
			SoundManager.main.PlaySFX("energy");
		}
		this.AP += amount;
		if (this.AP <= 0 && component)
		{
			component.Play("Pulse");
		}
		this.apText.text = this.AP.ToString() ?? "";
		Item2.SetAllItemColors();
	}

	// Token: 0x06000952 RID: 2386 RVA: 0x0006070C File Offset: 0x0005E90C
	public void SetAP(int amount)
	{
		PlayerStatTracking.main.AddStat("Energy gained", amount);
		Animator component = this.gameManager.endTurnButton.GetComponent<Animator>();
		if (amount < this.AP)
		{
			this.APAnimator.Play("AP_PulseDown", 0, 0f);
		}
		else if (amount > this.AP)
		{
			this.APAnimator.Play("AP_PulseUp", 0, 0f);
		}
		this.AP = amount;
		if (this.AP <= 0 && component)
		{
			component.Play("Pulse");
		}
		this.apText.text = this.AP.ToString() ?? "";
	}

	// Token: 0x06000953 RID: 2387 RVA: 0x000607BC File Offset: 0x0005E9BC
	public void CombatStart()
	{
		this.APEndedTurnWith = 0;
		this.APToAddNextTurn = 0;
	}

	// Token: 0x06000954 RID: 2388 RVA: 0x000607CC File Offset: 0x0005E9CC
	public void CombatEnd()
	{
	}

	// Token: 0x06000955 RID: 2389 RVA: 0x000607D0 File Offset: 0x0005E9D0
	public void SetAPForNewTurn()
	{
		RunType.RunProperty runProperty = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.setEnergy);
		RunType.RunProperty runProperty2 = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.scalingEnergy);
		Item2 itemWithStatusEffect = Item2.GetItemWithStatusEffect(Item2.ItemStatusEffect.Type.scalingEnergy, null, false);
		int ap = this.AP;
		if (runProperty)
		{
			this.SetAP(runProperty.value);
		}
		else if (runProperty2)
		{
			this.SetAP(Mathf.Min(runProperty2.value - 1 + GameFlowManager.main.turnNumber, 5));
		}
		else if (itemWithStatusEffect)
		{
			this.SetAP(Mathf.Min(GameFlowManager.main.turnNumber, 99));
		}
		else
		{
			this.SetAP(this.AP + this.APperTurn);
		}
		if (Item2.GetItemWithStatusEffect(Item2.ItemStatusEffect.Type.energyCarriesBetweenTurns, null, false))
		{
			this.ChangeAP(this.APEndedTurnWith);
		}
		this.ChangeAP(this.APToAddNextTurn);
		this.APEndedTurnWith = 0;
		this.APToAddNextTurn = 0;
	}

	// Token: 0x06000956 RID: 2390 RVA: 0x000608A7 File Offset: 0x0005EAA7
	public void NextTurn()
	{
		PetItem2.SetAllPetAP();
		this.SetAPForNewTurn();
		this.ShowAP();
	}

	// Token: 0x06000957 RID: 2391 RVA: 0x000608BA File Offset: 0x0005EABA
	public void Hurt()
	{
		this.animator.Play("Player_Hurt", 0, 0f);
	}

	// Token: 0x04000756 RID: 1878
	public static Player main;

	// Token: 0x04000757 RID: 1879
	private DraggableCombat draggableCombat;

	// Token: 0x04000758 RID: 1880
	[SerializeField]
	private GameObject spacerPrefab;

	// Token: 0x04000759 RID: 1881
	[SerializeField]
	public Transform mySpacerLocation;

	// Token: 0x0400075A RID: 1882
	[SerializeField]
	private GameObject experienceCounterPrefab;

	// Token: 0x0400075B RID: 1883
	[SerializeField]
	public Status stats;

	// Token: 0x0400075C RID: 1884
	[HideInInspector]
	public int experience;

	// Token: 0x0400075D RID: 1885
	public Character chosenCharacter;

	// Token: 0x0400075E RID: 1886
	public Character.CharacterName characterName;

	// Token: 0x0400075F RID: 1887
	[SerializeField]
	private List<Character> characterProperties;

	// Token: 0x04000760 RID: 1888
	public int level = -1;

	// Token: 0x04000761 RID: 1889
	public int APToAddNextTurn;

	// Token: 0x04000762 RID: 1890
	public int APEndedTurnWith;

	// Token: 0x04000763 RID: 1891
	public int AP = 3;

	// Token: 0x04000764 RID: 1892
	public int APperTurn = 3;

	// Token: 0x04000765 RID: 1893
	public float uncommonLuck;

	// Token: 0x04000766 RID: 1894
	public float rareLuck;

	// Token: 0x04000767 RID: 1895
	public float legendaryLuck;

	// Token: 0x04000768 RID: 1896
	public float uncommonLuckFromItems;

	// Token: 0x04000769 RID: 1897
	public float rareLuckFromItems;

	// Token: 0x0400076A RID: 1898
	public float legendaryLuckFromItems;

	// Token: 0x0400076B RID: 1899
	[SerializeField]
	private TextMeshProUGUI apText;

	// Token: 0x0400076C RID: 1900
	[SerializeField]
	private TextMeshProUGUI experienceText;

	// Token: 0x0400076D RID: 1901
	[SerializeField]
	private Animator APAnimator;

	// Token: 0x0400076E RID: 1902
	private GameManager gameManager;

	// Token: 0x0400076F RID: 1903
	public Animator animator;

	// Token: 0x04000770 RID: 1904
	private Vector3 difference;

	// Token: 0x04000771 RID: 1905
	public SpriteRenderer itemToInteractWith;

	// Token: 0x04000772 RID: 1906
	[SerializeField]
	public Transform playerSpritePositionTransform;

	// Token: 0x04000773 RID: 1907
	[SerializeField]
	private RuntimeAnimatorController purseSansBackpackAnimatorController;

	// Token: 0x04000774 RID: 1908
	[SerializeField]
	private RuntimeAnimatorController standardPurseController;

	// Token: 0x04000775 RID: 1909
	[SerializeField]
	private GameObject gridPrefab;
}
