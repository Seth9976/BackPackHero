using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020000AA RID: 170
public class CR8Manager : MonoBehaviour
{
	// Token: 0x060003EF RID: 1007 RVA: 0x000277B0 File Offset: 0x000259B0
	private void Awake()
	{
		if (CR8Manager.instance == null)
		{
			CR8Manager.instance = this;
		}
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x000277C5 File Offset: 0x000259C5
	private void OnDestroy()
	{
		if (CR8Manager.instance == this)
		{
			CR8Manager.instance = null;
		}
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x000277DA File Offset: 0x000259DA
	private void Start()
	{
		this.gameManager = GameManager.main;
		this.gameFlowManager = GameFlowManager.main;
		if (Player.main.characterName == Character.CharacterName.CR8)
		{
			GameObject.FindGameObjectWithTag("PlayerActionPoints").SetActive(false);
		}
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x0002780F File Offset: 0x00025A0F
	private void Update()
	{
		if (this.gameFlowManager.battlePhase == GameFlowManager.BattlePhase.outOfBattle && this.isRunning && !this.isTesting)
		{
			base.StopAllCoroutines();
			this.RemoveAllEnergies();
		}
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x0002783C File Offset: 0x00025A3C
	public void RemoveAllEnergies()
	{
		this.isRunning = false;
		EnergyBall[] array = Object.FindObjectsOfType<EnergyBall>();
		for (int i = 0; i < array.Length; i++)
		{
			Object.Destroy(array[i].gameObject);
		}
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x00027871 File Offset: 0x00025A71
	public void Test(GameObject selectedItem = null)
	{
		if (this.gameFlowManager.battlePhase != GameFlowManager.BattlePhase.outOfBattle || Object.FindObjectOfType<DungeonPlayer>().isMoving)
		{
			return;
		}
		if (!this.isRunning && !this.isTesting)
		{
			base.StartCoroutine(this.StartTurn(true, selectedItem));
		}
	}

	// Token: 0x060003F5 RID: 1013 RVA: 0x000278AC File Offset: 0x00025AAC
	public void StartCombat()
	{
		this.RemoveAllEnergies();
		EnergyEmitter[] array = Object.FindObjectsOfType<EnergyEmitter>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].StartCombat();
		}
	}

	// Token: 0x060003F6 RID: 1014 RVA: 0x000278DB File Offset: 0x00025ADB
	public void SetupTurn()
	{
		this.turnSpent = false;
	}

	// Token: 0x060003F7 RID: 1015 RVA: 0x000278E4 File Offset: 0x00025AE4
	public void EndTurn()
	{
		this.turnSpent = true;
		this.RemoveAllEnergies();
	}

	// Token: 0x060003F8 RID: 1016 RVA: 0x000278F4 File Offset: 0x00025AF4
	public static bool ValidEntrance(Item2 item, Item2.Trigger trigger)
	{
		if (Player.main.characterName != Character.CharacterName.CR8)
		{
			return true;
		}
		if (trigger.validEntrances.Count == 0)
		{
			return true;
		}
		if (trigger.trigger == Item2.Trigger.ActionTrigger.onOverheat)
		{
			return true;
		}
		EnergyBall currentEnergyBall = EnergyBall.GetCurrentEnergyBall();
		if (!currentEnergyBall)
		{
			return true;
		}
		if (!EnergyBall.startedByEnergyBall)
		{
			return true;
		}
		Debug.Log(string.Concat(new string[]
		{
			"Current energy ball is ",
			currentEnergyBall.name,
			" num: ",
			currentEnergyBall.energyBallNum.ToString(),
			" direction is ",
			EnergyBall.publicCurrentDirection.ToString(),
			" trigger is ",
			trigger.trigger.ToString()
		}));
		return EnergyEmitter.CorrectDirection(EnergyBall.publicCurrentDirection, item.transform, trigger.validEntrances);
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x000279D0 File Offset: 0x00025BD0
	public IEnumerator StartTurn(bool test = false, GameObject selectedItem = null)
	{
		if (this.gameFlowManager.battlePhase != GameFlowManager.BattlePhase.playerTurn && !test)
		{
			yield break;
		}
		if (this.isRunning)
		{
			yield break;
		}
		this.turnSpent = true;
		this.exit = false;
		this.isTesting = test;
		this.gameManager.ShowStopButton();
		if (test)
		{
			this.gameManager.inventoryPhase = GameManager.InventoryPhase.locked;
		}
		yield return null;
		yield return null;
		this.isRunning = true;
		if (selectedItem == null)
		{
			List<Item2> list = Item2.GetAllItemsInGrid();
			list = Item2.FilterByTypes(new List<Item2.ItemType> { Item2.ItemType.Component }, list);
			list = Item2.SortItemsByPosition(list);
			list.Insert(0, Item2.FindSingleObjectOfType(Item2.ItemType.Core));
			foreach (Item2 item in list)
			{
				if (item && item.itemMovement.inGrid)
				{
					EnergyEmitter component = item.GetComponent<EnergyEmitter>();
					if (component)
					{
						component.AutoCreateEnergy();
						yield return this.RunCharges(test);
						this.isRunning = true;
						if (this.exit)
						{
							break;
						}
					}
				}
			}
			List<Item2>.Enumerator enumerator = default(List<Item2>.Enumerator);
		}
		else
		{
			Item2 component2 = selectedItem.GetComponent<Item2>();
			if (selectedItem.GetComponent<EnergyEmitter>() && component2)
			{
				yield return GameFlowManager.main.UseItem(component2, true, false, Item2.PlayerAnimation.UseDefault, true, true);
				yield return this.RunCharges(true);
				this.isRunning = true;
			}
		}
		yield return new WaitForSeconds(0.1f);
		while (this.gameFlowManager.isCheckingEffects)
		{
			yield return new WaitForSeconds(0.1f);
		}
		if (this.isTesting)
		{
			this.gameFlowManager.battlePhase = GameFlowManager.BattlePhase.outOfBattle;
			Item2.RemoveAllModifiers(new List<Item2.Modifier.Length>
			{
				Item2.Modifier.Length.forTurn,
				Item2.Modifier.Length.forCombat
			}, -1);
			this.gameManager.inventoryPhase = GameManager.InventoryPhase.open;
			ManaStone.ResetAllStones();
		}
		else
		{
			this.gameFlowManager.battlePhase = GameFlowManager.BattlePhase.playerTurn;
		}
		EnergyBall.ResetAllHeat();
		this.RemoveAllEnergies();
		Item2.SetAllItemColors();
		this.isTesting = false;
		this.isRunning = false;
		EnergyBall.startedByEnergyBall = false;
		this.gameManager.StopRepeatActionButton();
		yield break;
		yield break;
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x000279ED File Offset: 0x00025BED
	public IEnumerator RunCharges(bool test)
	{
		while (this.gameFlowManager.isCheckingEffects)
		{
			yield return new WaitForSeconds(0.1f);
		}
		this.isRunning = true;
		bool anythingMoving = false;
		do
		{
			anythingMoving = false;
			List<EnergyBall> list = new List<EnergyBall>(EnergyBall.energyBalls);
			list.OrderBy((EnergyBall o) => o.energyBallNum);
			list.Reverse();
			foreach (EnergyBall energyBall in list)
			{
				if (!energyBall.isDestroying)
				{
					anythingMoving = true;
					yield return energyBall.MoveEnergyBall(test);
					break;
				}
			}
			List<EnergyBall>.Enumerator enumerator = default(List<EnergyBall>.Enumerator);
		}
		while (!this.exit && anythingMoving);
		this.isRunning = false;
		yield break;
		yield break;
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x00027A04 File Offset: 0x00025C04
	public List<GameObject> SpawnComponenets(int num = 5)
	{
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < num; i++)
		{
			GameObject gameObject = this.SpawnRandomComponenet();
			gameObject.transform.position = new Vector3(-3f + 1.5f * (float)i, -5f, 0f);
			ItemMovement component = gameObject.GetComponent<ItemMovement>();
			component.outOfInventoryPosition = gameObject.transform.localPosition;
			component.outOfInventoryRotation = Quaternion.identity;
			component.returnsToOutOfInventoryPosition = true;
			list.Add(gameObject);
		}
		return list;
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x00027A84 File Offset: 0x00025C84
	public GameObject SpawnRandomComponenet()
	{
		bool flag;
		GameObject gameObject = Item2.ChooseRandomFromList(Item2.GetItemOfRarity(this.gameManager.ChooseRarity(out flag, 0f, true), this.componenets), true);
		GameManager main = GameManager.main;
		if (main != null)
		{
			main.ShowGotLucky(gameObject.transform, flag);
		}
		return Object.Instantiate<GameObject>(gameObject, Vector3.zero, Quaternion.identity);
	}

	// Token: 0x040002ED RID: 749
	public static CR8Manager instance;

	// Token: 0x040002EE RID: 750
	[SerializeField]
	public GameObject cr8energyFlowPrefabBackup;

	// Token: 0x040002EF RID: 751
	[SerializeField]
	private List<GameObject> componenets;

	// Token: 0x040002F0 RID: 752
	public bool isRunning;

	// Token: 0x040002F1 RID: 753
	public bool isTesting;

	// Token: 0x040002F2 RID: 754
	private GameFlowManager gameFlowManager;

	// Token: 0x040002F3 RID: 755
	private GameManager gameManager;

	// Token: 0x040002F4 RID: 756
	public bool setToRunForever;

	// Token: 0x040002F5 RID: 757
	public bool skippedTurnToReorg;

	// Token: 0x040002F6 RID: 758
	public int energyBallNum;

	// Token: 0x040002F7 RID: 759
	public bool exit;

	// Token: 0x040002F8 RID: 760
	public bool turnSpent;
}
