using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C0 RID: 192
public class DungeonSpawner : MonoBehaviour
{
	// Token: 0x06000563 RID: 1379 RVA: 0x00034F22 File Offset: 0x00033122
	private void Awake()
	{
		DungeonSpawner.main = this;
	}

	// Token: 0x06000564 RID: 1380 RVA: 0x00034F2A File Offset: 0x0003312A
	private void OnDestory()
	{
		DungeonSpawner.main = null;
	}

	// Token: 0x06000565 RID: 1381 RVA: 0x00034F34 File Offset: 0x00033134
	public void CreateDungeonEvent(GameObject prefab, DungeonSpawner.DungeonEventSpawn.Type type)
	{
		DungeonSpawner.DungeonEventSpawn dungeonEventSpawn = new DungeonSpawner.DungeonEventSpawn();
		dungeonEventSpawn.prefabList = new List<GameObject> { prefab };
		dungeonEventSpawn.type = type;
		this.itemsThatNeedAHome.Add(dungeonEventSpawn);
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x00034F6C File Offset: 0x0003316C
	public void ClearPropertiesByLength(DungeonSpawner.DungeonProperty.Length length)
	{
		for (int i = 0; i < this.dungeonProperties.Count; i++)
		{
			if (this.dungeonProperties[i].length == length)
			{
				this.dungeonProperties.RemoveAt(i);
				i--;
			}
		}
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x00034FB4 File Offset: 0x000331B4
	public void ClearPropertiesByType(DungeonSpawner.DungeonProperty.Type type)
	{
		for (int i = 0; i < this.dungeonProperties.Count; i++)
		{
			if (this.dungeonProperties[i].type == type)
			{
				this.dungeonProperties.RemoveAt(i);
				i--;
			}
		}
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x00034FFB File Offset: 0x000331FB
	public void AddDungeonProperty(DungeonSpawner.DungeonProperty dungeonProperty)
	{
		this.dungeonProperties.Add(dungeonProperty);
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x0003500C File Offset: 0x0003320C
	public void AddDungeonPropertyEvent(DungeonSpawner.DungeonEventSpawn dungeonEventSpawn, int floorsToSpawn)
	{
		DungeonSpawner.DungeonProperty dungeonProperty = new DungeonSpawner.DungeonProperty();
		dungeonProperty.type = DungeonSpawner.DungeonProperty.Type.eventInFloors;
		dungeonProperty.dungeonEventSpawn1 = dungeonEventSpawn;
		dungeonProperty.length = DungeonSpawner.DungeonProperty.Length.Perm;
		dungeonProperty.value = floorsToSpawn;
		this.dungeonProperties.Add(dungeonProperty);
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x00035048 File Offset: 0x00033248
	public void AddDungeonProperty(DungeonSpawner.DungeonProperty.Type type, DungeonSpawner.DungeonProperty.Length length, int num = 0, GameObject prefab = null, string text = "")
	{
		DungeonSpawner.DungeonProperty dungeonProperty = new DungeonSpawner.DungeonProperty();
		dungeonProperty.type = type;
		dungeonProperty.length = length;
		dungeonProperty.value = num;
		dungeonProperty.prefabToSpawn = prefab;
		dungeonProperty.text = text;
		this.dungeonProperties.Add(dungeonProperty);
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x0003508C File Offset: 0x0003328C
	public List<DungeonSpawner.DungeonProperty> GetAllDungeonPropertiesOfType(DungeonSpawner.DungeonProperty.Type type)
	{
		List<DungeonSpawner.DungeonProperty> list = new List<DungeonSpawner.DungeonProperty>();
		foreach (DungeonSpawner.DungeonProperty dungeonProperty in this.dungeonProperties)
		{
			if (dungeonProperty.type == type)
			{
				list.Add(dungeonProperty);
			}
		}
		return list;
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x000350F0 File Offset: 0x000332F0
	public void SetDungeonPropertyValue(DungeonSpawner.DungeonProperty.Type type, int num)
	{
		foreach (DungeonSpawner.DungeonProperty dungeonProperty in this.dungeonProperties)
		{
			if (type == dungeonProperty.type)
			{
				dungeonProperty.value = num;
				return;
			}
		}
		DungeonSpawner.DungeonProperty dungeonProperty2 = new DungeonSpawner.DungeonProperty();
		dungeonProperty2.type = type;
		dungeonProperty2.value = num;
		this.dungeonProperties.Add(dungeonProperty2);
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x00035170 File Offset: 0x00033370
	public void AddDungeonPropertyValue(DungeonSpawner.DungeonProperty.Type type, int num)
	{
		foreach (DungeonSpawner.DungeonProperty dungeonProperty in this.dungeonProperties)
		{
			if (type == dungeonProperty.type)
			{
				dungeonProperty.value += num;
				return;
			}
		}
		DungeonSpawner.DungeonProperty dungeonProperty2 = new DungeonSpawner.DungeonProperty();
		dungeonProperty2.value = num;
		this.dungeonProperties.Add(dungeonProperty2);
	}

	// Token: 0x0600056E RID: 1390 RVA: 0x000351F0 File Offset: 0x000333F0
	public int GetDungeonPropertyValue(DungeonSpawner.DungeonProperty.Type type)
	{
		foreach (DungeonSpawner.DungeonProperty dungeonProperty in this.dungeonProperties)
		{
			if (type == dungeonProperty.type)
			{
				return dungeonProperty.value;
			}
		}
		return -1;
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x00035254 File Offset: 0x00033454
	public void SetDungeonPropertyString(DungeonSpawner.DungeonProperty.Type type, string text)
	{
		foreach (DungeonSpawner.DungeonProperty dungeonProperty in this.dungeonProperties)
		{
			if (type == dungeonProperty.type)
			{
				dungeonProperty.text = text;
				return;
			}
		}
		DungeonSpawner.DungeonProperty dungeonProperty2 = new DungeonSpawner.DungeonProperty();
		dungeonProperty2.type = type;
		dungeonProperty2.text = text;
		this.dungeonProperties.Add(dungeonProperty2);
	}

	// Token: 0x06000570 RID: 1392 RVA: 0x000352D4 File Offset: 0x000334D4
	public string GetDungeonPropertyString(DungeonSpawner.DungeonProperty.Type type)
	{
		foreach (DungeonSpawner.DungeonProperty dungeonProperty in this.dungeonProperties)
		{
			if (type == dungeonProperty.type)
			{
				return dungeonProperty.text;
			}
		}
		return "";
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x0003533C File Offset: 0x0003353C
	private void Start()
	{
		this.gameManager = GameManager.main;
		if (Singleton.Instance.storyMode && !MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.broughtBackPradasNecklace))
		{
			this.SetBoss(this.kingsGuardFirstEncounter);
		}
	}

	// Token: 0x06000572 RID: 1394 RVA: 0x00035370 File Offset: 0x00033570
	public DungeonLevel GetDungeonLevel(DungeonLevel.Zone zone)
	{
		foreach (DungeonLevel dungeonLevel in this.dungeonLevels)
		{
			if (dungeonLevel.zone == zone)
			{
				return dungeonLevel;
			}
		}
		return null;
	}

	// Token: 0x06000573 RID: 1395 RVA: 0x000353CC File Offset: 0x000335CC
	public DungeonRoom ChooseRandomRoom()
	{
		DungeonPlaceholder[] array = Object.FindObjectsOfType<DungeonPlaceholder>();
		List<DungeonRoom> list = new List<DungeonRoom>();
		foreach (DungeonPlaceholder dungeonPlaceholder in array)
		{
			if (!dungeonPlaceholder.used)
			{
				list.Add(dungeonPlaceholder.GetComponent<DungeonRoom>());
			}
		}
		if (list.Count == 0)
		{
			return null;
		}
		int num = Random.Range(0, list.Count);
		list[num].GetComponent<DungeonPlaceholder>().used = true;
		return list[num];
	}

	// Token: 0x06000574 RID: 1396 RVA: 0x00035440 File Offset: 0x00033640
	private bool EmptySpace(PathFinding.Location location, bool isDest)
	{
		GameObject[] objectsAtVector = PathFinding.GetObjectsAtVector(location.position);
		for (int i = 0; i < objectsAtVector.Length; i++)
		{
			if (objectsAtVector[i].GetComponent<DungeonRoom>())
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000575 RID: 1397 RVA: 0x00035479 File Offset: 0x00033679
	public IEnumerator SpawnChambers()
	{
		yield return new WaitForFixedUpdate();
		this.roomsParent = GameObject.FindGameObjectWithTag("RoomsParent").transform;
		this.roomsParent.localPosition = new Vector3(0f, -2f, 2f);
		foreach (object obj in this.roomsParent)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
		yield return new WaitForFixedUpdate();
		if (this.ConsiderSpecialRooms())
		{
			yield return new WaitForFixedUpdate();
			yield return new WaitForFixedUpdate();
			yield return new WaitForFixedUpdate();
			yield return new WaitForFixedUpdate();
			this.MarkAllFilledRoomsAsUsed();
		}
		else if (this.gameManager.dungeonLevel.currentFloor == DungeonLevel.Floor.boss)
		{
			GameObject gameObject;
			if (this.gameManager.zoneNumber == 2)
			{
				int num = Random.Range(0, this.dungeonFinalBossRoomsPrefabs.Length);
				gameObject = Object.Instantiate<GameObject>(this.dungeonFinalBossRoomsPrefabs[num], Vector3.zero, Quaternion.identity, this.roomsParent);
			}
			else
			{
				int num2 = Random.Range(0, this.dungeonBossRoomsPrefabs.Length);
				gameObject = Object.Instantiate<GameObject>(this.dungeonBossRoomsPrefabs[num2], Vector3.zero, Quaternion.identity, this.roomsParent);
			}
			gameObject.transform.localPosition = new Vector3(-1f, 0f);
			yield return new WaitForFixedUpdate();
			this.MarkAllFilledRoomsAsUsed();
		}
		else if (this.gameManager.dungeonLevel.currentFloor == DungeonLevel.Floor.intro)
		{
			Object.Instantiate<GameObject>(this.brambleOrCryptRooms, Vector3.zero, Quaternion.identity, this.roomsParent).transform.localPosition = new Vector3(-1f, 0f);
			yield return new WaitForFixedUpdate();
		}
		else if (this.gameManager.dungeonLevel.currentFloor == DungeonLevel.Floor.first)
		{
			List<GameObject> list = new List<GameObject>(this.dungeonRoomBlockPrefabs);
			DoomRoomBlock room = null;
			DoomRoomBlock room2 = null;
			for (int i = -2; i <= 2; i += 4)
			{
				if (list.Count == 0)
				{
					list = new List<GameObject>(this.dungeonRoomBlockPrefabs);
				}
				int num3 = Random.Range(0, list.Count);
				GameObject gameObject2 = Object.Instantiate<GameObject>(list[num3], Vector3.zero, Quaternion.identity, this.roomsParent);
				list.RemoveAt(num3);
				gameObject2.transform.localPosition = new Vector3(Mathf.Round((float)i) - 1f, 0f);
				if (i == -2)
				{
					room = gameObject2.GetComponent<DoomRoomBlock>();
				}
				else if (i == 2)
				{
					room2 = gameObject2.GetComponent<DoomRoomBlock>();
				}
			}
			yield return new WaitForFixedUpdate();
			this.MarkAllFilledRoomsAsUsed();
			room.ConnectRooms(room.GetRightBlock(DungeonSpawner.DungeonEventSpawn.Type.connectionRoom).localPosition, room2.GetLeftBlock(DungeonSpawner.DungeonEventSpawn.Type.connectionRoom).localPosition);
			yield return new WaitForFixedUpdate();
			DungeonPlaceholder dungeonPlaceholder = this.FindDungeonPlaceholder(DungeonSpawner.DungeonEventSpawn.Type.connectionRoom);
			while (dungeonPlaceholder)
			{
				dungeonPlaceholder.type = DungeonSpawner.DungeonEventSpawn.Type.exitEntrance;
				dungeonPlaceholder.used = false;
				dungeonPlaceholder.priority = 0;
				dungeonPlaceholder = this.FindDungeonPlaceholder(DungeonSpawner.DungeonEventSpawn.Type.connectionRoom);
			}
			yield return new WaitForFixedUpdate();
			room = null;
			room2 = null;
		}
		else
		{
			List<GameObject> list2 = new List<GameObject>(this.dungeonRoomBlockPrefabs);
			DoomRoomBlock room2 = null;
			DoomRoomBlock room = null;
			DoomRoomBlock room3 = null;
			for (int j = -4; j <= 4; j += 4)
			{
				if (list2.Count == 0)
				{
					list2 = new List<GameObject>(this.dungeonRoomBlockPrefabs);
				}
				int num4 = Random.Range(0, list2.Count);
				GameObject gameObject3 = Object.Instantiate<GameObject>(list2[num4], Vector3.zero, Quaternion.identity, this.roomsParent);
				list2.RemoveAt(num4);
				gameObject3.transform.localPosition = new Vector3(Mathf.Round((float)j) - 1f, 0f);
				if (j == -4)
				{
					room2 = gameObject3.GetComponent<DoomRoomBlock>();
				}
				else if (j == 0)
				{
					room = gameObject3.GetComponent<DoomRoomBlock>();
				}
				else if (j == 4)
				{
					room3 = gameObject3.GetComponent<DoomRoomBlock>();
				}
			}
			yield return new WaitForFixedUpdate();
			this.MarkAllFilledRoomsAsUsed();
			room2.ConnectRooms(room2.GetRightBlock(DungeonSpawner.DungeonEventSpawn.Type.connectionRoom).localPosition, room.GetLeftBlock(DungeonSpawner.DungeonEventSpawn.Type.connectionRoom).localPosition);
			room.ConnectRooms(room.GetRightBlock(DungeonSpawner.DungeonEventSpawn.Type.connectionRoom).localPosition, room3.GetLeftBlock(DungeonSpawner.DungeonEventSpawn.Type.connectionRoom).localPosition);
			yield return new WaitForFixedUpdate();
			DungeonPlaceholder dungeonPlaceholder2 = this.FindDungeonPlaceholder(DungeonSpawner.DungeonEventSpawn.Type.connectionRoom);
			while (dungeonPlaceholder2)
			{
				dungeonPlaceholder2.type = DungeonSpawner.DungeonEventSpawn.Type.exitEntrance;
				dungeonPlaceholder2.used = false;
				dungeonPlaceholder2.priority = 0;
				dungeonPlaceholder2 = this.FindDungeonPlaceholder(DungeonSpawner.DungeonEventSpawn.Type.connectionRoom);
			}
			yield return new WaitForFixedUpdate();
			room2 = null;
			room = null;
			room3 = null;
		}
		if (TwitchManager.isRunningPolls())
		{
			TwitchManager.Instance.pollManager.onFloorStart();
		}
		this.SetBoss();
		this.SpawnPlayer();
		if (GameManager.main.dungeonLevel.currentFloor != DungeonLevel.Floor.boss)
		{
			DungeonPlaceholder dungeonPlaceholder3 = this.FindDungeonPlaceholder(DungeonSpawner.DungeonEventSpawn.Type.exitEntrance);
			DungeonPlaceholder dungeonPlaceholder4 = this.FindDungeonPlaceholder(DungeonSpawner.DungeonEventSpawn.Type.exitEntrance);
			if (dungeonPlaceholder4)
			{
				if (Vector2.Distance(dungeonPlaceholder4.transform.position, this.player.transform.position) > Vector2.Distance(dungeonPlaceholder3.transform.position, this.player.transform.position))
				{
					dungeonPlaceholder3.type = DungeonSpawner.DungeonEventSpawn.Type.optional;
					dungeonPlaceholder3.used = false;
					dungeonPlaceholder3 = dungeonPlaceholder4;
				}
				else
				{
					dungeonPlaceholder4.type = DungeonSpawner.DungeonEventSpawn.Type.optional;
					dungeonPlaceholder4.used = false;
				}
			}
			if (dungeonPlaceholder3)
			{
				Transform transform = dungeonPlaceholder3.transform;
				Object.Instantiate<GameObject>(this.exitPrefab, transform.position + Vector3.back, Quaternion.identity, transform);
				dungeonPlaceholder3.used = true;
			}
		}
		this.ItemsSpawnEvents();
		yield return new WaitForFixedUpdate();
		if (Singleton.Instance.IsStoryModeLevels() && this.gameManager.dungeonLevel.currentFloor == DungeonLevel.Floor.boss)
		{
			List<DungeonEvent> list3 = DungeonEvent.FindDungeonEventsOfType(DungeonEvent.DungeonEventType.Exit);
			bool flag = true;
			foreach (DungeonEvent dungeonEvent in list3)
			{
				if (flag)
				{
					flag = false;
					Object.Instantiate<GameObject>(this.victoryChestPrefab, dungeonEvent.transform.position, Quaternion.identity, dungeonEvent.transform.parent);
					Object.Destroy(dungeonEvent.gameObject);
				}
				else
				{
					Object.Destroy(dungeonEvent.transform.parent.gameObject);
				}
			}
		}
		yield return new WaitForFixedUpdate();
		yield return this.SetAllRoomSpritesAndEncounters();
		Object.FindObjectOfType<DungeonPlayer>().FindReachableEvents();
		if (this.gameManager.dungeonLevel.currentFloor == DungeonLevel.Floor.intro)
		{
			this.gameManager.dungeonLevel.currentFloor = DungeonLevel.Floor.boss;
		}
		foreach (DungeonRoom dungeonRoom in Object.FindObjectsOfType<DungeonRoom>())
		{
			dungeonRoom.transform.localPosition = Vector3Int.RoundToInt(dungeonRoom.transform.localPosition);
		}
		yield break;
	}

	// Token: 0x06000576 RID: 1398 RVA: 0x00035488 File Offset: 0x00033688
	private void MarkAllFilledRoomsAsUsed()
	{
		foreach (DungeonPlaceholder dungeonPlaceholder in Object.FindObjectsOfType<DungeonPlaceholder>())
		{
			if (dungeonPlaceholder.transform.childCount > 0)
			{
				dungeonPlaceholder.used = true;
			}
			else
			{
				dungeonPlaceholder.used = false;
			}
			if (dungeonPlaceholder.forceFill)
			{
				dungeonPlaceholder.priority -= 5;
			}
		}
	}

	// Token: 0x06000577 RID: 1399 RVA: 0x000354E4 File Offset: 0x000336E4
	private void ItemsSpawnEvents()
	{
		List<Item2> itemsWithStatusEffect = Item2.GetItemsWithStatusEffect(Item2.ItemStatusEffect.Type.spawnsEvent, null, false);
		while (itemsWithStatusEffect.Count > 0)
		{
			DungeonRoom dungeonRoom = this.ChooseRandomRoom();
			if (dungeonRoom)
			{
				foreach (GameObject gameObject in itemsWithStatusEffect[0].GetStatusEffectPrefabs(Item2.ItemStatusEffect.Type.spawnsEvent))
				{
					Object.Instantiate<GameObject>(gameObject, dungeonRoom.transform.position + Vector3.back, Quaternion.identity, dungeonRoom.transform);
				}
			}
			itemsWithStatusEffect.RemoveAt(0);
		}
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x00035588 File Offset: 0x00033788
	private void SetBoss()
	{
		if (this.GetDungeonPropertyString(DungeonSpawner.DungeonProperty.Type.chosenBoss) == "")
		{
			List<DungeonLevel.EnemyEncounter2> possibleBossEncounters = this.GetPossibleBossEncounters();
			int num = Random.Range(0, possibleBossEncounters.Count);
			this.SetBoss(possibleBossEncounters[num]);
		}
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x000355CC File Offset: 0x000337CC
	public void SetBoss(DungeonLevel.EnemyEncounter2 enemyEncounter)
	{
		string name = enemyEncounter.enemiesInGroup[0].name;
		this.SetDungeonPropertyString(DungeonSpawner.DungeonProperty.Type.chosenBoss, name);
	}

	// Token: 0x0600057A RID: 1402 RVA: 0x000355F4 File Offset: 0x000337F4
	public List<DungeonLevel.EnemyEncounter2> GetPossibleBossEncounters()
	{
		List<DungeonLevel.EnemyEncounter2> list = new List<DungeonLevel.EnemyEncounter2>();
		foreach (DungeonLevel.EnemyEncounter2 enemyEncounter in this.gameManager.dungeonLevel.enemyEncounters)
		{
			if (enemyEncounter.floor == DungeonLevel.Floor.boss && enemyEncounter.IsValid())
			{
				list.Add(enemyEncounter);
			}
		}
		return list;
	}

	// Token: 0x0600057B RID: 1403 RVA: 0x0003566C File Offset: 0x0003386C
	public void SpawnPlayer()
	{
		DungeonPlaceholder dungeonPlaceholder = this.FindDungeonPlaceholder(DungeonSpawner.DungeonEventSpawn.Type.entranceOnly);
		if (!dungeonPlaceholder)
		{
			dungeonPlaceholder = this.FindDungeonPlaceholder(DungeonSpawner.DungeonEventSpawn.Type.exitEntrance);
		}
		Vector2 vector = dungeonPlaceholder.transform.position;
		this.player.transform.position = vector + Vector3.back;
	}

	// Token: 0x0600057C RID: 1404 RVA: 0x000356C4 File Offset: 0x000338C4
	public bool ConsiderSpecialRooms()
	{
		RunType.RunProperty runProperty = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.startOnLevel);
		if (runProperty)
		{
			int floor = GameManager.main.floor;
			if (floor > runProperty.assignedPrefabs.Count)
			{
				return false;
			}
			GameObject gameObject = Object.Instantiate<GameObject>(runProperty.assignedPrefabs[floor - 1], Vector3.zero, Quaternion.identity);
			gameObject.transform.SetParent(this.roomsParent);
			gameObject.transform.localPosition = new Vector3(-1f, 0f);
			return true;
		}
		else
		{
			int num = GameManager.main.dungeonLevel.currentFloor - DungeonLevel.Floor.first;
			if (num >= 0 && GameManager.main && GameManager.main.dungeonLevel && GameManager.main.dungeonLevel.levelPrefabs != null && GameManager.main.dungeonLevel.levelPrefabs.Count > num)
			{
				GameObject gameObject2 = Object.Instantiate<GameObject>(GameManager.main.dungeonLevel.levelPrefabs[num], Vector3.zero, Quaternion.identity);
				gameObject2.transform.SetParent(this.roomsParent);
				gameObject2.transform.localPosition = new Vector3(-1f, 0f);
				return true;
			}
			return false;
		}
	}

	// Token: 0x0600057D RID: 1405 RVA: 0x000357F7 File Offset: 0x000339F7
	public IEnumerator SetAllRoomSpritesAndEncounters()
	{
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		foreach (DungeonRoom dungeonRoom in DungeonRoom.GetAllRooms())
		{
			dungeonRoom.ChooseSprite();
		}
		yield return new WaitForFixedUpdate();
		RunType.RunProperty runProperty = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.startOnLevel);
		if (runProperty && GameManager.main.floor <= runProperty.assignedPrefabs.Count)
		{
			yield break;
		}
		DungeonEvent dungeonEvent = null;
		foreach (DungeonEvent dungeonEvent2 in Object.FindObjectsOfType<DungeonEvent>())
		{
			if (dungeonEvent2.dungeonEventType == DungeonEvent.DungeonEventType.Exit)
			{
				dungeonEvent = dungeonEvent2;
				break;
			}
		}
		if (dungeonEvent && this.gameManager.dungeonLevel.currentFloor != DungeonLevel.Floor.boss)
		{
			List<Vector2> list;
			PathFinding.FindPath(this.player.transform.position, dungeonEvent.transform.position, new Func<PathFinding.Location, bool, bool>(DungeonPlayer.AcceptableSpaceAnyRoom), out list, null);
			foreach (Vector2 vector in list)
			{
				foreach (RaycastHit2D raycastHit2D in Physics2D.RaycastAll(vector, Vector2.zero))
				{
					DungeonPlaceholder componentInChildren = raycastHit2D.collider.GetComponentInChildren<DungeonPlaceholder>();
					if (componentInChildren && (componentInChildren.type == DungeonSpawner.DungeonEventSpawn.Type.mandatoryFight || componentInChildren.type == DungeonSpawner.DungeonEventSpawn.Type.mainPath))
					{
						componentInChildren.type = DungeonSpawner.DungeonEventSpawn.Type.mandatoryFight;
						componentInChildren.priority = -1;
					}
				}
			}
		}
		DungeonEvent.simpleClicks = new List<int>();
		this.SetAllEncounters();
		foreach (DungeonPlaceholder dungeonPlaceholder in Object.FindObjectsOfType<DungeonPlaceholder>())
		{
			if (dungeonPlaceholder.canBeDeletedIfEmpty && !dungeonPlaceholder.used && dungeonPlaceholder.transform.childCount == 0)
			{
				Object.Destroy(dungeonPlaceholder.gameObject);
			}
		}
		yield return new WaitForFixedUpdate();
		using (List<DungeonRoom>.Enumerator enumerator = DungeonRoom.GetAllRooms().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				DungeonRoom dungeonRoom2 = enumerator.Current;
				dungeonRoom2.ChooseSprite();
			}
			yield break;
		}
		yield break;
	}

	// Token: 0x0600057E RID: 1406 RVA: 0x00035808 File Offset: 0x00033A08
	private DungeonPlaceholder FindDungeonPlaceholder(DungeonSpawner.DungeonEventSpawn.Type type)
	{
		List<DungeonPlaceholder> list = new List<DungeonPlaceholder>();
		DungeonPlaceholder[] array = Object.FindObjectsOfType<DungeonPlaceholder>();
		int num = 999;
		foreach (DungeonPlaceholder dungeonPlaceholder in array)
		{
			if (!dungeonPlaceholder.used && dungeonPlaceholder.type == type && dungeonPlaceholder.priority <= num)
			{
				num = dungeonPlaceholder.priority;
				list.Add(dungeonPlaceholder);
			}
		}
		for (int j = 0; j < list.Count; j++)
		{
			if (list[j].priority > num)
			{
				list.RemoveAt(j);
				j--;
			}
		}
		if (list.Count == 0)
		{
			return null;
		}
		int num2 = Random.Range(0, list.Count);
		list[num2].used = true;
		foreach (DungeonPlaceholder dungeonPlaceholder2 in array)
		{
			if (!(list[num2] == dungeonPlaceholder2))
			{
				float num3 = Vector2.Distance(dungeonPlaceholder2.transform.position, list[num2].transform.position);
				if (num3 <= 2f && dungeonPlaceholder2.type == list[num2].type)
				{
					dungeonPlaceholder2.priority++;
				}
				if (list[num2].type == DungeonSpawner.DungeonEventSpawn.Type.optionalDanger && dungeonPlaceholder2.type == DungeonSpawner.DungeonEventSpawn.Type.optional)
				{
					dungeonPlaceholder2.priority -= Mathf.RoundToInt(4f - num3);
				}
			}
		}
		return list[num2];
	}

	// Token: 0x0600057F RID: 1407 RVA: 0x00035994 File Offset: 0x00033B94
	public void SetAllEncounters()
	{
		this.itemsThatNeedAHome = new List<DungeonSpawner.DungeonEventSpawn>();
		foreach (DungeonLevel.DungeonEventsToSpawn dungeonEventsToSpawn in this.gameManager.dungeonLevel.itemsToSpawnOnMap)
		{
			if (dungeonEventsToSpawn.floor == this.gameManager.dungeonLevel.currentFloor)
			{
				using (List<DungeonSpawner.DungeonEventSpawn>.Enumerator enumerator2 = dungeonEventsToSpawn.itemsToSpawnOnMap.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						DungeonSpawner.DungeonEventSpawn dungeonEventSpawn = enumerator2.Current;
						if ((!dungeonEventSpawn.repeatLoopsOnly || this.gameManager.floor > 9) && (!Singleton.Instance.IsStoryModeLevels() || !dungeonEventSpawn.ignoreOnLastFloor) && (!dungeonEventSpawn.storyModeOnly || Singleton.Instance.storyMode) && (!Singleton.Instance.storyMode || MetaProgressSaveManager.ConditionsMet(dungeonEventSpawn.storyModeConditions)) && (dungeonEventSpawn.requiredRunProperties.Count <= 0 || RunTypeManager.CheckIfAllRunPropertiesExist(dungeonEventSpawn.requiredRunProperties)) && (dungeonEventSpawn.disablingRunProperties.Count <= 0 || !RunTypeManager.CheckIfRunPropertiesExist(dungeonEventSpawn.disablingRunProperties)))
						{
							this.itemsThatNeedAHome.Add(dungeonEventSpawn.Clone());
						}
					}
					break;
				}
			}
		}
		if (this.gameManager.dungeonLevel.currentFloor == DungeonLevel.Floor.first || this.gameManager.dungeonLevel.currentFloor == DungeonLevel.Floor.second)
		{
			foreach (DungeonSpawner.DungeonProperty dungeonProperty in this.GetAllDungeonPropertiesOfType(DungeonSpawner.DungeonProperty.Type.eventInFloors))
			{
				dungeonProperty.value--;
				if (dungeonProperty.value <= 0)
				{
					this.itemsThatNeedAHome.Add(dungeonProperty.dungeonEventSpawn1.Clone());
					this.dungeonProperties.Remove(dungeonProperty);
				}
			}
		}
		RunType.RunProperty runProperty = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.additionalEncounter);
		if (runProperty)
		{
			foreach (GameObject gameObject in runProperty.assignedPrefabs)
			{
				bool flag = false;
				foreach (DungeonSpawner.DungeonEventSpawn dungeonEventSpawn2 in this.itemsThatNeedAHome)
				{
					if (dungeonEventSpawn2.prefabList.Contains(gameObject))
					{
						dungeonEventSpawn2.num = new Vector2(dungeonEventSpawn2.num.x + 1f, dungeonEventSpawn2.num.y + 1f);
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					DungeonSpawner.DungeonEventSpawn dungeonEventSpawn3 = new DungeonSpawner.DungeonEventSpawn();
					dungeonEventSpawn3.prefabList = new List<GameObject> { gameObject };
					dungeonEventSpawn3.num = Vector2.one;
					dungeonEventSpawn3.type = DungeonSpawner.DungeonEventSpawn.Type.mainPath;
				}
			}
		}
		runProperty = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.replaceEncounter);
		if (runProperty)
		{
			foreach (DungeonSpawner.DungeonEventSpawn dungeonEventSpawn4 in this.itemsThatNeedAHome)
			{
				if (dungeonEventSpawn4.prefabList.Contains(runProperty.assignedPrefabs[0]))
				{
					dungeonEventSpawn4.prefabList.Remove(runProperty.assignedPrefabs[0]);
					dungeonEventSpawn4.prefabList.Add(runProperty.assignedPrefabs[1]);
					break;
				}
			}
		}
		foreach (DungeonSpawner.DungeonEventSpawn dungeonEventSpawn5 in this.itemsThatNeedAHome)
		{
			if ((dungeonEventSpawn5.validForCharacters == null || dungeonEventSpawn5.validForCharacters.Count == 0 || dungeonEventSpawn5.validForCharacters.Contains(Character.CharacterName.Any) || dungeonEventSpawn5.validForCharacters.Contains(Player.main.characterName)) && (dungeonEventSpawn5.eventType == EventManager.EventType.None || EventManager.instance.eventType == dungeonEventSpawn5.eventType))
			{
				int num = Mathf.RoundToInt(Random.Range(dungeonEventSpawn5.num.x, dungeonEventSpawn5.num.y));
				for (int i = 0; i < num; i++)
				{
					DungeonPlaceholder dungeonPlaceholder = this.FindDungeonPlaceholder(dungeonEventSpawn5.type);
					if (dungeonPlaceholder == null)
					{
						DungeonRoom dungeonRoom = this.ChooseRandomRoom();
						if (dungeonRoom)
						{
							int num2 = Random.Range(0, dungeonEventSpawn5.prefabList.Count);
							Object.Instantiate<GameObject>(dungeonEventSpawn5.prefabList[num2], dungeonRoom.transform.position + Vector3.back, Quaternion.identity, dungeonRoom.transform);
						}
					}
					else
					{
						int num3 = Random.Range(0, dungeonEventSpawn5.prefabList.Count);
						Object.Instantiate<GameObject>(dungeonEventSpawn5.prefabList[num3], dungeonPlaceholder.transform.position + Vector3.back, Quaternion.identity, dungeonPlaceholder.transform);
					}
				}
			}
		}
		foreach (DungeonPlaceholder dungeonPlaceholder2 in Object.FindObjectsOfType<DungeonPlaceholder>())
		{
			if (!dungeonPlaceholder2.used && dungeonPlaceholder2.forceFill)
			{
				List<GameObject> list = new List<GameObject>();
				foreach (DungeonSpawner.DungeonEventSpawn dungeonEventSpawn6 in this.itemsThatNeedAHome)
				{
					if (dungeonEventSpawn6.type == dungeonPlaceholder2.type)
					{
						list.AddRange(dungeonEventSpawn6.prefabList);
					}
				}
				if (list.Count > 0)
				{
					int num4 = Random.Range(0, list.Count);
					Object.Instantiate<GameObject>(list[num4], dungeonPlaceholder2.transform.position + Vector3.back, Quaternion.identity, dungeonPlaceholder2.transform);
				}
			}
		}
		this.usedEnemyEncounters = new List<DungeonLevel.EnemyEncounter2>();
		this.usedEventEncounters = new List<DungeonLevel.EventEncounter2>();
		foreach (DungeonEvent dungeonEvent in Object.FindObjectsOfType<DungeonEvent>())
		{
			if (dungeonEvent.dungeonEventType == DungeonEvent.DungeonEventType.Enemy)
			{
				DungeonLevel.EnemyEncounter2 enemyEncounter = this.GetEnemyEncounter(false);
				this.usedEnemyEncounters.Add(enemyEncounter);
				dungeonEvent.itemsToSpawn = enemyEncounter.enemiesInGroup;
			}
			else if (dungeonEvent.dungeonEventType == DungeonEvent.DungeonEventType.Shambler)
			{
				DungeonLevel.EnemyEncounter2 enemyEncounter2 = this.GetEnemyEncounter(true);
				this.usedEnemyEncounters.Add(enemyEncounter2);
				dungeonEvent.itemsToSpawn = enemyEncounter2.enemiesInGroup;
			}
			else if (dungeonEvent.dungeonEventType == DungeonEvent.DungeonEventType.Chance && dungeonEvent.itemsToSpawn.Count == 0)
			{
				DungeonLevel.EventEncounter2 eventEncounter = this.GetEventEncounter();
				this.usedEventEncounters.Add(eventEncounter);
				dungeonEvent.itemsToSpawn = eventEncounter.eventType;
			}
		}
	}

	// Token: 0x06000580 RID: 1408 RVA: 0x00036108 File Offset: 0x00034308
	public DungeonLevel.EventEncounter2 GetEventEncounter()
	{
		TutorialManager tutorialManager = TutorialManager.main;
		if (tutorialManager && tutorialManager.playType == TutorialManager.PlayType.testing && this.gameManager.floor == 0)
		{
			return tutorialManager.eventEncounter;
		}
		List<DungeonLevel.EventEncounter2> list = this.GetAllPossibleEventEncounters(this.usedEventEncounters);
		if (list.Count == 0)
		{
			list = this.GetAllPossibleEventEncounters(new List<DungeonLevel.EventEncounter2>());
		}
		if (list.Count == 0)
		{
			list = this.gameManager.dungeonLevel.eventEncounters;
		}
		float num = 0f;
		foreach (DungeonLevel.EventEncounter2 eventEncounter in list)
		{
			num += eventEncounter.weight;
		}
		float num2 = Random.Range(0f, num);
		float num3 = 0f;
		foreach (DungeonLevel.EventEncounter2 eventEncounter2 in list)
		{
			num3 += eventEncounter2.weight;
			if (num3 >= num2)
			{
				return eventEncounter2;
			}
		}
		return list[0];
	}

	// Token: 0x06000581 RID: 1409 RVA: 0x00036234 File Offset: 0x00034434
	private bool ShareObjs(List<GameObject> aList, List<GameObject> bList)
	{
		foreach (GameObject gameObject in aList)
		{
			if (bList.Contains(gameObject))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000582 RID: 1410 RVA: 0x0003628C File Offset: 0x0003448C
	private List<DungeonLevel.EventEncounter2> GetAllPossibleEventEncounters(List<DungeonLevel.EventEncounter2> exclusionList)
	{
		Player player = Player.main;
		RunType.RunProperty runProperty = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.cannotFindEvent);
		List<DungeonLevel.EventEncounter2> list = new List<DungeonLevel.EventEncounter2>();
		foreach (DungeonLevel.EventEncounter2 eventEncounter in this.gameManager.dungeonLevel.eventEncounters)
		{
			EventNPC componentInChildren = eventEncounter.eventType[0].GetComponentInChildren<EventNPC>();
			if (!exclusionList.Contains(eventEncounter) && (!Singleton.Instance.storyMode || MetaProgressSaveManager.ConditionsMet(eventEncounter.storyModeConditions)) && (eventEncounter.disablingRunProperties.Count <= 0 || !RunTypeManager.CheckIfRunPropertiesExist(eventEncounter.disablingRunProperties)) && RunTypeManager.CheckIfAllRunPropertiesExist(eventEncounter.requiredRunProperties) && (!eventEncounter.storyModeOnly || Singleton.Instance.storyMode) && (componentInChildren.eventType == EventManager.EventType.None || EventManager.instance.eventType == componentInChildren.eventType) && (componentInChildren.validForCharacters == null || componentInChildren.validForCharacters.Count == 0 || componentInChildren.validForCharacters.Contains(Character.CharacterName.Any) || componentInChildren.validForCharacters.Contains(player.characterName)) && (eventEncounter.floor.Contains(this.gameManager.dungeonLevel.currentFloor) || (eventEncounter.floor.Contains(DungeonLevel.Floor.all) && this.gameManager.dungeonLevel.currentFloor != DungeonLevel.Floor.boss)) && (!runProperty || !this.ShareObjs(runProperty.assignedPrefabs, eventEncounter.eventType)))
			{
				list.Add(eventEncounter);
			}
		}
		return list;
	}

	// Token: 0x06000583 RID: 1411 RVA: 0x00036450 File Offset: 0x00034650
	public DungeonLevel.EnemyEncounter2 GetEnemyEncounter(bool findElite = false)
	{
		TutorialManager tutorialManager = Object.FindObjectOfType<TutorialManager>();
		if (tutorialManager && tutorialManager.playType == TutorialManager.PlayType.testing && this.gameManager.floor == 0)
		{
			return tutorialManager.enemyEncounter;
		}
		List<DungeonLevel.EnemyEncounter2> list = this.GetAllPossibleEnemyEncounters(this.usedEnemyEncounters, findElite);
		if (list.Count == 0)
		{
			list = this.GetAllPossibleEnemyEncounters(new List<DungeonLevel.EnemyEncounter2>(), findElite);
		}
		if (list.Count == 0)
		{
			list = this.gameManager.dungeonLevel.enemyEncounters;
		}
		if (this.gameManager.dungeonLevel.currentFloor == DungeonLevel.Floor.boss)
		{
			foreach (DungeonLevel.EnemyEncounter2 enemyEncounter in list)
			{
				if (Item2.GetDisplayName(enemyEncounter.enemiesInGroup[0].name) == Item2.GetDisplayName(this.GetDungeonPropertyString(DungeonSpawner.DungeonProperty.Type.chosenBoss)))
				{
					this.ClearPropertiesByType(DungeonSpawner.DungeonProperty.Type.chosenBoss);
					return enemyEncounter;
				}
			}
		}
		int num = Random.Range(0, list.Count);
		return list[num];
	}

	// Token: 0x06000584 RID: 1412 RVA: 0x00036560 File Offset: 0x00034760
	private List<DungeonLevel.EnemyEncounter2> GetAllPossibleEnemyEncounters(List<DungeonLevel.EnemyEncounter2> exclusionList, bool findElite = false)
	{
		List<DungeonLevel.EnemyEncounter2> list = new List<DungeonLevel.EnemyEncounter2>();
		foreach (DungeonLevel.EnemyEncounter2 enemyEncounter in this.gameManager.dungeonLevel.enemyEncounters)
		{
			Player player = Player.main;
			if (enemyEncounter.IsValid() && enemyEncounter.isElite == findElite)
			{
				bool flag = true;
				foreach (GameObject gameObject in enemyEncounter.enemiesInGroup)
				{
					Enemy component = gameObject.GetComponent<Enemy>();
					if (component)
					{
						List<Character.CharacterName> validForCharacters = component.validForCharacters;
						if (validForCharacters.Count != 0 && !validForCharacters.Contains(Character.CharacterName.Any) && !validForCharacters.Contains(player.characterName))
						{
							flag = false;
							break;
						}
					}
				}
				if (flag && !exclusionList.Contains(enemyEncounter) && (enemyEncounter.floor == this.gameManager.dungeonLevel.currentFloor || enemyEncounter.floor == DungeonLevel.Floor.all))
				{
					list.Add(enemyEncounter);
				}
			}
		}
		return list;
	}

	// Token: 0x06000585 RID: 1413 RVA: 0x00036694 File Offset: 0x00034894
	private void Update()
	{
	}

	// Token: 0x04000419 RID: 1049
	public static DungeonSpawner main;

	// Token: 0x0400041A RID: 1050
	[SerializeField]
	private DungeonLevel.EnemyEncounter2 kingsGuardFirstEncounter;

	// Token: 0x0400041B RID: 1051
	[SerializeField]
	private GameObject brambleOrCryptRooms;

	// Token: 0x0400041C RID: 1052
	[SerializeField]
	public List<Sprite> roomFadeSprites;

	// Token: 0x0400041D RID: 1053
	[SerializeField]
	public GameObject roomFade;

	// Token: 0x0400041E RID: 1054
	[SerializeField]
	public Sprite[] roomSprites;

	// Token: 0x0400041F RID: 1055
	[SerializeField]
	public Sprite[] roomSprites2;

	// Token: 0x04000420 RID: 1056
	[SerializeField]
	public Sprite[] roomSprites3;

	// Token: 0x04000421 RID: 1057
	[Header("Prefabs to Spawn Dungeon Events")]
	[SerializeField]
	public GameObject exitPrefab;

	// Token: 0x04000422 RID: 1058
	[Header("Things to Spawn on this floor")]
	[SerializeField]
	private List<DungeonSpawner.DungeonEventSpawn> itemsThatNeedAHome;

	// Token: 0x04000423 RID: 1059
	[SerializeField]
	private GameObject victoryChestPrefab;

	// Token: 0x04000424 RID: 1060
	[SerializeField]
	private GameObject parcelPrefab;

	// Token: 0x04000425 RID: 1061
	[SerializeField]
	private GameObject locketPrefab;

	// Token: 0x04000426 RID: 1062
	public List<DungeonSpawner.DungeonProperty> dungeonProperties = new List<DungeonSpawner.DungeonProperty>();

	// Token: 0x04000427 RID: 1063
	[SerializeField]
	public Transform roomsParent;

	// Token: 0x04000428 RID: 1064
	[SerializeField]
	public GameObject player;

	// Token: 0x04000429 RID: 1065
	[SerializeField]
	private List<GameObject> dungeonRoomBlockPrefabs;

	// Token: 0x0400042A RID: 1066
	[SerializeField]
	public GameObject[] dungeonRoomAttachmentPrefabs;

	// Token: 0x0400042B RID: 1067
	[SerializeField]
	private GameObject[] dungeonBossRoomsPrefabs;

	// Token: 0x0400042C RID: 1068
	[SerializeField]
	private GameObject[] dungeonFinalBossRoomsPrefabs;

	// Token: 0x0400042D RID: 1069
	[SerializeField]
	private GameObject[] finalRunRooms;

	// Token: 0x0400042E RID: 1070
	[SerializeField]
	public GameObject connectingRoom;

	// Token: 0x0400042F RID: 1071
	[SerializeField]
	private List<DungeonLevel> dungeonLevels;

	// Token: 0x04000430 RID: 1072
	public List<DungeonLevel.EnemyEncounter2> usedEnemyEncounters = new List<DungeonLevel.EnemyEncounter2>();

	// Token: 0x04000431 RID: 1073
	public List<DungeonLevel.EventEncounter2> usedEventEncounters = new List<DungeonLevel.EventEncounter2>();

	// Token: 0x04000432 RID: 1074
	private GameManager gameManager;

	// Token: 0x020002F3 RID: 755
	[Serializable]
	public class DungeonEventSpawn
	{
		// Token: 0x06001526 RID: 5414 RVA: 0x000B8F28 File Offset: 0x000B7128
		public DungeonSpawner.DungeonEventSpawn Clone()
		{
			return (DungeonSpawner.DungeonEventSpawn)base.MemberwiseClone();
		}

		// Token: 0x040011A2 RID: 4514
		public List<Character.CharacterName> validForCharacters;

		// Token: 0x040011A3 RID: 4515
		[SerializeField]
		public EventManager.EventType eventType;

		// Token: 0x040011A4 RID: 4516
		public List<GameObject> prefabList = new List<GameObject>();

		// Token: 0x040011A5 RID: 4517
		public DungeonSpawner.DungeonEventSpawn.Type type;

		// Token: 0x040011A6 RID: 4518
		public Vector2 num;

		// Token: 0x040011A7 RID: 4519
		public bool repeatLoopsOnly;

		// Token: 0x040011A8 RID: 4520
		public bool ignoreOnLastFloor;

		// Token: 0x040011A9 RID: 4521
		public bool storyModeOnly;

		// Token: 0x040011AA RID: 4522
		public List<RunType.RunProperty.Type> requiredRunProperties = new List<RunType.RunProperty.Type>();

		// Token: 0x040011AB RID: 4523
		public List<RunType.RunProperty.Type> disablingRunProperties = new List<RunType.RunProperty.Type>();

		// Token: 0x040011AC RID: 4524
		public List<MetaProgressSaveManager.MetaProgressCondition> storyModeConditions = new List<MetaProgressSaveManager.MetaProgressCondition>();

		// Token: 0x02000497 RID: 1175
		public enum Type
		{
			// Token: 0x04001AC8 RID: 6856
			mandatoryFight,
			// Token: 0x04001AC9 RID: 6857
			mainPath,
			// Token: 0x04001ACA RID: 6858
			optional,
			// Token: 0x04001ACB RID: 6859
			exitEntrance,
			// Token: 0x04001ACC RID: 6860
			connectionRoom,
			// Token: 0x04001ACD RID: 6861
			optionalDanger,
			// Token: 0x04001ACE RID: 6862
			emptyRoom,
			// Token: 0x04001ACF RID: 6863
			entranceOnly
		}
	}

	// Token: 0x020002F4 RID: 756
	[Serializable]
	public class DungeonProperty
	{
		// Token: 0x040011AD RID: 4525
		public DungeonSpawner.DungeonProperty.Type type;

		// Token: 0x040011AE RID: 4526
		public DungeonSpawner.DungeonProperty.Length length = DungeonSpawner.DungeonProperty.Length.Perm;

		// Token: 0x040011AF RID: 4527
		public int value;

		// Token: 0x040011B0 RID: 4528
		public GameObject prefabToSpawn;

		// Token: 0x040011B1 RID: 4529
		public DungeonSpawner.DungeonEventSpawn dungeonEventSpawn1;

		// Token: 0x040011B2 RID: 4530
		public Item2.ItemType itemType;

		// Token: 0x040011B3 RID: 4531
		public string text;

		// Token: 0x02000498 RID: 1176
		public enum Type
		{
			// Token: 0x04001AD1 RID: 6865
			chosenBoss,
			// Token: 0x04001AD2 RID: 6866
			eventInFloors,
			// Token: 0x04001AD3 RID: 6867
			scavenger
		}

		// Token: 0x02000499 RID: 1177
		public enum Length
		{
			// Token: 0x04001AD5 RID: 6869
			forZone,
			// Token: 0x04001AD6 RID: 6870
			forFloor,
			// Token: 0x04001AD7 RID: 6871
			Perm
		}
	}
}
