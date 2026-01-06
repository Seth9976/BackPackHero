using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000BC RID: 188
public class DungeonPlayer : CustomInputHandler
{
	// Token: 0x06000522 RID: 1314 RVA: 0x0003274A File Offset: 0x0003094A
	private void Awake()
	{
		DungeonPlayer.main = this;
	}

	// Token: 0x06000523 RID: 1315 RVA: 0x00032752 File Offset: 0x00030952
	private void OnDestory()
	{
		if (DungeonPlayer.main == this)
		{
			DungeonPlayer.main = null;
		}
	}

	// Token: 0x06000524 RID: 1316 RVA: 0x00032768 File Offset: 0x00030968
	private void Start()
	{
		this.player = Player.main.GetComponentInChildren<Animator>().gameObject;
		this.playerTransform = this.player.transform;
		this.gameManager = GameManager.main;
		this.gameFlowManager = GameFlowManager.main;
		this.animator = base.GetComponent<Animator>();
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x06000525 RID: 1317 RVA: 0x000327CC File Offset: 0x000309CC
	public void PathFromPlayerToRoom(DungeonRoom dungeonRoom)
	{
		TutorialManager tutorialManager = TutorialManager.main;
		List<Vector2> list = new List<Vector2>();
		bool flag = PathFinding.FindPath(base.transform.position, dungeonRoom.transform.position, new Func<PathFinding.Location, bool, bool>(this.AcceptableSpaceToSeeThrough), out list, null);
		if (!flag)
		{
			DungeonEvent componentInChildren = dungeonRoom.GetComponentInChildren<DungeonEvent>();
			if (componentInChildren && componentInChildren.turnsToExpire != -1)
			{
				flag = PathFinding.FindPath(base.transform.position, dungeonRoom.transform.position, new Func<PathFinding.Location, bool, bool>(DungeonPlayer.AcceptableSpaceAnyRoom), out list, null);
			}
		}
		RunType.RunProperty runProperty = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.youCanSeeTheWholeMap);
		if (flag || tutorialManager.tutorialSequence != TutorialManager.TutorialSequence.trulyDone || runProperty)
		{
			SpriteRenderer spriteRenderer = dungeonRoom.spriteRenderer;
			if (spriteRenderer)
			{
				spriteRenderer.enabled = true;
			}
			foreach (object obj in dungeonRoom.transform)
			{
				((Transform)obj).transform.localPosition = new Vector3(0f, 0f, -1f);
			}
			dungeonRoom.canBeSeenOnMap = true;
			dungeonRoom.EnableInterface();
			return;
		}
		SpriteRenderer spriteRenderer2 = dungeonRoom.spriteRenderer;
		if (spriteRenderer2)
		{
			spriteRenderer2.color = new Color(1f, 1f, 1f, 0f);
			spriteRenderer2.enabled = false;
		}
		foreach (object obj2 in dungeonRoom.transform)
		{
			((Transform)obj2).transform.localPosition = new Vector3(0f, 0f, 1000f);
		}
		dungeonRoom.canBeSeenOnMap = false;
		dungeonRoom.DisableInterface();
	}

	// Token: 0x06000526 RID: 1318 RVA: 0x000329C8 File Offset: 0x00030BC8
	public void FindReachableEvents()
	{
		for (int i = 0; i < this.roomFades.Count; i++)
		{
			GameObject gameObject = this.roomFades[i];
			if (!gameObject)
			{
				this.roomFades.RemoveAt(i);
				i--;
			}
			else
			{
				this.roomFades.RemoveAt(i);
				i--;
				Object.Destroy(gameObject);
			}
		}
		DungeonSpawner dungeonSpawner = DungeonSpawner.main;
		Transform roomsParent = DungeonSpawner.main.roomsParent;
		List<DungeonRoom> allRooms = DungeonRoom.GetAllRooms();
		foreach (DungeonRoom dungeonRoom in allRooms)
		{
			this.PathFromPlayerToRoom(dungeonRoom);
		}
		foreach (DungeonRoom dungeonRoom2 in allRooms)
		{
			if (dungeonRoom2.spriteRenderer.enabled)
			{
				foreach (DungeonRoom dungeonRoom3 in allRooms)
				{
					if (Vector2.Distance(dungeonRoom2.transform.position, dungeonRoom3.transform.position) <= 1f)
					{
						SpriteRenderer spriteRenderer = dungeonRoom3.spriteRenderer;
						if (spriteRenderer && !spriteRenderer.enabled)
						{
							if (dungeonRoom2.right && dungeonRoom3.transform.position.x > dungeonRoom2.transform.position.x + 0.5f)
							{
								GameObject gameObject2 = Object.Instantiate<GameObject>(dungeonSpawner.roomFade, dungeonRoom3.transform.position, Quaternion.identity, roomsParent);
								gameObject2.GetComponent<SpriteRenderer>().sprite = dungeonSpawner.roomFadeSprites[0];
								this.roomFades.Add(gameObject2);
							}
							else if (dungeonRoom2.left && dungeonRoom3.transform.position.x < dungeonRoom2.transform.position.x - 0.5f)
							{
								GameObject gameObject3 = Object.Instantiate<GameObject>(dungeonSpawner.roomFade, dungeonRoom3.transform.position, Quaternion.identity, roomsParent);
								gameObject3.GetComponent<SpriteRenderer>().sprite = dungeonSpawner.roomFadeSprites[1];
								this.roomFades.Add(gameObject3);
							}
							else if (dungeonRoom2.up && dungeonRoom3.transform.position.y > dungeonRoom2.transform.position.y + 0.5f)
							{
								GameObject gameObject4 = Object.Instantiate<GameObject>(dungeonSpawner.roomFade, dungeonRoom3.transform.position, Quaternion.identity, roomsParent);
								gameObject4.GetComponent<SpriteRenderer>().sprite = dungeonSpawner.roomFadeSprites[2];
								this.roomFades.Add(gameObject4);
							}
							else if (dungeonRoom2.down && dungeonRoom3.transform.position.y < dungeonRoom2.transform.position.y - 0.5f)
							{
								GameObject gameObject5 = Object.Instantiate<GameObject>(dungeonSpawner.roomFade, dungeonRoom3.transform.position, Quaternion.identity, roomsParent);
								gameObject5.GetComponent<SpriteRenderer>().sprite = dungeonSpawner.roomFadeSprites[3];
								this.roomFades.Add(gameObject5);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000527 RID: 1319 RVA: 0x00032D74 File Offset: 0x00030F74
	private void Update()
	{
	}

	// Token: 0x06000528 RID: 1320 RVA: 0x00032D78 File Offset: 0x00030F78
	public void ConsiderRoom(Vector3 pos)
	{
		foreach (RaycastHit2D raycastHit2D in Physics2D.RaycastAll(pos, Vector2.zero))
		{
			DungeonRoom component = raycastHit2D.collider.GetComponent<DungeonRoom>();
			if (component)
			{
				component.MoveToRoom();
				return;
			}
		}
	}

	// Token: 0x06000529 RID: 1321 RVA: 0x00032DC8 File Offset: 0x00030FC8
	public DungeonRoom FindClosestRoom()
	{
		List<DungeonRoom> allRooms = DungeonRoom.GetAllRooms();
		float num = 999f;
		DungeonRoom dungeonRoom = null;
		foreach (DungeonRoom dungeonRoom2 in allRooms)
		{
			float num2 = Vector2.Distance(dungeonRoom2.transform.position, base.transform.position);
			if (num2 < num)
			{
				dungeonRoom = dungeonRoom2;
				num = num2;
			}
		}
		return dungeonRoom;
	}

	// Token: 0x0600052A RID: 1322 RVA: 0x00032E50 File Offset: 0x00031050
	public void MoveToClosestRoom()
	{
		if (!this.isMoving)
		{
			DungeonRoom dungeonRoom = this.FindClosestRoom();
			if (dungeonRoom)
			{
				base.transform.position = new Vector3(dungeonRoom.transform.position.x, dungeonRoom.transform.position.y, base.transform.position.z);
			}
		}
	}

	// Token: 0x0600052B RID: 1323 RVA: 0x00032EB4 File Offset: 0x000310B4
	private void Arrive()
	{
		DungeonEvent[] array = Object.FindObjectsOfType<DungeonEvent>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].RemoveEventProperty(DungeonEvent.EventProperty.Type.open);
		}
		if (this.room)
		{
			DungeonEvent relevantEvent = DungeonPlayer.GetRelevantEvent(base.transform.position);
			if (relevantEvent)
			{
				bool flag = false;
				using (List<GameObject>.Enumerator enumerator = relevantEvent.itemsToSpawn.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current == null)
						{
							this.FindReachableEvents();
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					this.StartEvent();
				}
			}
		}
		else
		{
			this.FindReachableEvents();
		}
		this.gameManager.SetAllSpritesToLayer0();
		if (this.room)
		{
			this.room.GetComponentInChildren<DungeonEvent>();
		}
	}

	// Token: 0x0600052C RID: 1324 RVA: 0x00032F94 File Offset: 0x00031194
	private void ArriveAnimation()
	{
		if (this.dungeonEvent && (!this.dungeonEvent.IsFinished() || this.dungeonEvent.dungeonEventType == DungeonEvent.DungeonEventType.Enemy || this.dungeonEvent.dungeonEventType == DungeonEvent.DungeonEventType.Shambler))
		{
			this.player.GetComponent<Animator>().Play("Player_Idle");
			return;
		}
		this.player.GetComponent<Animator>().Play("Player_ReadMap");
		Object.FindObjectOfType<BackgroundController>().running = false;
	}

	// Token: 0x0600052D RID: 1325 RVA: 0x00033010 File Offset: 0x00031210
	public static DungeonEvent GetRelevantEvent(Vector2 pos)
	{
		DungeonEvent dungeonEvent = null;
		DungeonRoom dungeonRoom = DungeonRoom.GetDungeonRoom(PathFinding.GetObjectsAtVector(pos));
		if (dungeonRoom)
		{
			foreach (DungeonEvent dungeonEvent2 in dungeonRoom.GetComponentsInChildren<DungeonEvent>())
			{
				if (!dungeonEvent2.IsFinished())
				{
					dungeonEvent = dungeonEvent2;
					if (dungeonEvent2.dungeonEventType == DungeonEvent.DungeonEventType.Shambler)
					{
						break;
					}
				}
			}
		}
		return dungeonEvent;
	}

	// Token: 0x0600052E RID: 1326 RVA: 0x00033068 File Offset: 0x00031268
	public void StartEvent()
	{
		DungeonEvent relevantEvent = DungeonPlayer.GetRelevantEvent(base.transform.position);
		if (relevantEvent)
		{
			if (Singleton.Instance.autoCloseMap)
			{
				this.gameManager.ShowInventory();
			}
			this.dungeonEvent = relevantEvent;
			relevantEvent.Play();
			base.StartCoroutine(this.WalkIntoEvent());
		}
	}

	// Token: 0x0600052F RID: 1327 RVA: 0x000330C4 File Offset: 0x000312C4
	public IEnumerator WalkIntoEvent()
	{
		BackgroundController backgroundController = Object.FindObjectOfType<BackgroundController>();
		yield return new WaitForSeconds(0.1f);
		backgroundController.running = false;
		bool flag = false;
		GameObject[] array = GameObject.FindGameObjectsWithTag("InteractiveVisual");
		for (int i = 0; i < array.Length; i++)
		{
			global::AnimationEvent component = array[i].GetComponent<global::AnimationEvent>();
			if (!component || !component.dontFindMe)
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			while (this.gameManager.player.transform.position.x > -3.5f)
			{
				backgroundController.ScrollBackground(8f);
				yield return null;
			}
		}
		yield break;
	}

	// Token: 0x06000530 RID: 1328 RVA: 0x000330D4 File Offset: 0x000312D4
	public bool InABlockedroom()
	{
		DungeonRoom dungeonRoom = this.FindClosestRoom();
		if (dungeonRoom)
		{
			DungeonEvent componentInChildren = dungeonRoom.GetComponentInChildren<DungeonEvent>();
			if (componentInChildren)
			{
				if (componentInChildren.IsFinished())
				{
					return false;
				}
				if (!componentInChildren.passable || componentInChildren.cannotWalkAwayFrom)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000531 RID: 1329 RVA: 0x0003311C File Offset: 0x0003131C
	public void Shake()
	{
		SoundManager.main.PlaySFX("cantMoveHere");
		this.animator.Play("playerDungeonShake");
	}

	// Token: 0x06000532 RID: 1330 RVA: 0x00033140 File Offset: 0x00031340
	public void MoveTo(Vector2 pos, DungeonRoom dest)
	{
		TutorialManager tutorialManager = Object.FindObjectOfType<TutorialManager>();
		if (tutorialManager && tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.organizeChest)
		{
			return;
		}
		if (!this.animator)
		{
			this.animator = base.GetComponent<Animator>();
		}
		if (Item2.GetItemOfType(Item2.ItemType.Core, Item2.GetAllItemsOutsideGrid()).Count >= 1)
		{
			this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gmCR81"));
			this.Shake();
			return;
		}
		foreach (Item2 item in Item2.GetAllItems())
		{
			if (!item.itemMovement.inGrid && !item.destroyed)
			{
				bool flag = RunTypeManager.CheckIfAssignedItemIsInProperty(RunType.RunProperty.Type.mustKeep, item.gameObject);
				if (!flag)
				{
					flag = item.CheckForStatusEffect(Item2.ItemStatusEffect.Type.cannotBeLeft);
				}
				if (flag)
				{
					this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm44") + " " + LangaugeManager.main.GetTextByKey(item.displayName));
					SoundManager.main.PlaySFX("cantMoveHere");
					this.animator.Play("playerDungeonShake");
					return;
				}
			}
		}
		if (this.locked)
		{
			return;
		}
		bool flag2 = this.InABlockedroom();
		RunType.RunProperty runProperty = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.puzzleMode);
		if (runProperty != null && !Object.FindObjectOfType<ItemMovementManager>().CheckAllForValidPlacementBool(runProperty))
		{
			this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm45"));
			SoundManager.main.PlaySFX("cantMoveHere");
			this.animator.Play("playerDungeonShake");
			return;
		}
		RandomEventMaster randomEventMaster = Object.FindObjectOfType<RandomEventMaster>();
		if (this.gameFlowManager.battlePhase != GameFlowManager.BattlePhase.outOfBattle)
		{
			this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm37"));
			SoundManager.main.PlaySFX("cantMoveHere");
			this.animator.Play("playerDungeonShake");
			return;
		}
		if (randomEventMaster && !randomEventMaster.finished)
		{
			this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm38"));
			SoundManager.main.PlaySFX("cantMoveHere");
			this.animator.Play("playerDungeonShake");
			return;
		}
		if (flag2 && this.isMoving)
		{
			return;
		}
		List<Vector2> list;
		if (!PathFinding.FindPath(this.FindClosestRoom().transform.position, pos, new Func<PathFinding.Location, bool, bool>(this.AcceptableSpace), out list, null))
		{
			if (!this.isMoving)
			{
				this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm39"));
				this.MoveToClosestRoom();
				SoundManager.main.PlaySFX("cantMoveHere");
				this.animator.Play("playerDungeonShake");
			}
			return;
		}
		if (this.room == dest)
		{
			return;
		}
		if (GameManager.main.IsConsideringCurseReplacement())
		{
			return;
		}
		this.room = dest;
		if (Singleton.Instance.ironMan)
		{
			SaveManager.DeleteSave(Singleton.Instance.saveNumber);
		}
		base.StopAllCoroutines();
		this.gameManager.EndCurseReplacement();
		base.StartCoroutine(this.MoveToRoom(pos, false));
	}

	// Token: 0x06000533 RID: 1331 RVA: 0x00033468 File Offset: 0x00031668
	private bool AcceptableSpaceToSeeThrough(PathFinding.Location location, bool isDest)
	{
		foreach (DungeonRoom dungeonRoom in DungeonRoom.allRooms)
		{
			if (dungeonRoom && Vector2.Distance(location.position, dungeonRoom.transform.position) <= 0.5f)
			{
				if (location.path.Count > 0 && ((location.position.x > location.path[location.path.Count - 1].x && !dungeonRoom.left) || (location.position.x < location.path[location.path.Count - 1].x && !dungeonRoom.right) || (location.position.y > location.path[location.path.Count - 1].y && !dungeonRoom.down) || (location.position.y < location.path[location.path.Count - 1].y && !dungeonRoom.up)))
				{
					return false;
				}
				if (location.strikeNumber >= 2 && location.path.Count >= 2 && location.position - location.path[location.path.Count - 1] != location.path[location.path.Count - 1] - location.path[location.path.Count - 2])
				{
					return false;
				}
				if (location.strikeNumber >= 1 && location.path.Count >= 2)
				{
					location.strikeNumber++;
				}
				if (dungeonRoom.transform.childCount > 0)
				{
					DungeonEvent component = dungeonRoom.transform.GetChild(0).GetComponent<DungeonEvent>();
					if (!component || component.passable || isDest)
					{
						return true;
					}
					location.strikeNumber++;
					if (location.strikeNumber >= 2)
					{
						return false;
					}
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000534 RID: 1332 RVA: 0x000336D8 File Offset: 0x000318D8
	private bool AcceptableSpace(PathFinding.Location location, bool isDest)
	{
		foreach (DungeonRoom dungeonRoom in DungeonRoom.allRooms)
		{
			if (dungeonRoom && Vector2.Distance(location.position, dungeonRoom.transform.position) <= 0.5f)
			{
				if (location.path.Count > 0 && ((location.position.x - 0.25f > location.path[location.path.Count - 1].x && !dungeonRoom.left) || (location.position.x + 0.25f < location.path[location.path.Count - 1].x && !dungeonRoom.right) || (location.position.y - 0.25f > location.path[location.path.Count - 1].y && !dungeonRoom.down) || ((double)location.position.y + 0.25 < (double)location.path[location.path.Count - 1].y && !dungeonRoom.up)))
				{
					return false;
				}
				if (dungeonRoom.transform.childCount <= 0)
				{
					return true;
				}
				DungeonEvent component = dungeonRoom.transform.GetChild(0).GetComponent<DungeonEvent>();
				if (!component || component.passable || isDest)
				{
					return true;
				}
				return false;
			}
		}
		return false;
	}

	// Token: 0x06000535 RID: 1333 RVA: 0x000338A8 File Offset: 0x00031AA8
	public static bool AcceptableSpaceAnyRoom(PathFinding.Location location, bool isDest)
	{
		GameObject[] objectsAtVector = PathFinding.GetObjectsAtVector(location.position);
		for (int i = 0; i < objectsAtVector.Length; i++)
		{
			DungeonRoom component = objectsAtVector[i].GetComponent<DungeonRoom>();
			if (component)
			{
				return location.path.Count <= 0 || ((location.position.x <= location.path[location.path.Count - 1].x || component.left) && (location.position.x >= location.path[location.path.Count - 1].x || component.right) && (location.position.y <= location.path[location.path.Count - 1].y || component.down) && (location.position.y >= location.path[location.path.Count - 1].y || component.up));
			}
		}
		return false;
	}

	// Token: 0x06000536 RID: 1334 RVA: 0x000339CC File Offset: 0x00031BCC
	public void DeleteEventOnMap()
	{
		this.dungeonEvent = null;
		DungeonRoom dungeonRoom = DungeonRoom.GetDungeonRoom(PathFinding.GetObjectsAtVector(base.transform.position));
		if (dungeonRoom)
		{
			foreach (DungeonEvent dungeonEvent in dungeonRoom.GetComponentsInChildren<DungeonEvent>())
			{
				if (dungeonEvent.IsFinished())
				{
					Object.Destroy(dungeonEvent.gameObject);
				}
			}
		}
	}

	// Token: 0x06000537 RID: 1335 RVA: 0x00033A2F File Offset: 0x00031C2F
	public IEnumerator MoveToRoom(Vector2 pos, bool simple = false)
	{
		if (!this.playerScript)
		{
			this.playerScript = Player.main;
		}
		List<Vector2> vecs;
		bool flag = PathFinding.FindPath(this.FindClosestRoom().transform.position, pos, new Func<PathFinding.Location, bool, bool>(this.AcceptableSpace), out vecs, null);
		if (simple)
		{
			flag = true;
			vecs = new List<Vector2>
			{
				this.FindClosestRoom().transform.position,
				pos
			};
		}
		if (flag)
		{
			this.gameManager.travelling = true;
			Store store = Object.FindObjectOfType<Store>();
			if (store)
			{
				store.GoodBye();
			}
			EventNPC eventNPC = Object.FindObjectOfType<EventNPC>();
			if (eventNPC)
			{
				eventNPC.GoodBye();
			}
			if (Parcel.main)
			{
				Parcel.main.Goodbye();
			}
			this.gameManager.RemoveItemsOutsideGrid();
			ItemPouch.AllPouchesMarkAllItemsAsOwned();
			Item2.MarkAllAsOwned();
			this.gameFlowManager.DoAllSavedDestroys();
			ItemPouch.CloseAllPouches();
			GameObject[] array = GameObject.FindGameObjectsWithTag("InteractiveVisualClickMe");
			for (int i = 0; i < array.Length; i++)
			{
				Object.Destroy(array[i]);
			}
			Chest[] array2 = Object.FindObjectsOfType<Chest>();
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].isOpen = true;
			}
			Store[] array3 = Object.FindObjectsOfType<Store>();
			for (int i = 0; i < array3.Length; i++)
			{
				array3[i].isOpen = true;
			}
			Door[] array4 = Object.FindObjectsOfType<Door>();
			for (int i = 0; i < array4.Length; i++)
			{
				array4[i].isOpen = true;
			}
			SimpleClickItem[] array5 = Object.FindObjectsOfType<SimpleClickItem>();
			for (int i = 0; i < array5.Length; i++)
			{
				array5[i].StoreItems();
			}
			if (this.playerScript.characterName == Character.CharacterName.Tote)
			{
				foreach (GameObject gameObject in Object.FindObjectOfType<Tote>().GetAllCards())
				{
					if (gameObject.activeInHierarchy)
					{
						gameObject.GetComponent<Item2>().isOwned = true;
					}
					else
					{
						gameObject.SetActive(true);
						gameObject.GetComponent<Item2>().isOwned = true;
						gameObject.SetActive(false);
					}
				}
			}
			this.DeleteEventOnMap();
			Player player = Player.main;
			player.stats.ClampHealth();
			yield return new WaitForEndOfFrame();
			yield return new WaitForFixedUpdate();
			yield return new WaitForEndOfFrame();
			yield return new WaitForFixedUpdate();
			Object.FindObjectOfType<BackgroundController>().running = true;
			player.GetComponentInChildren<Animator>().Play("Player_Run");
			SoundManager.main.PlaySFX("moveHere");
			this.isMoving = true;
			if (this.gameManager.limitedItemReorganize)
			{
				this.gameManager.FinishLimitedItemSelection();
			}
			this.gameManager.standardSpawnAfter = false;
			vecs.Add(pos);
			for (int j = 0; j < vecs.Count; j++)
			{
				vecs[j] = base.transform.parent.InverseTransformPoint(vecs[j]);
			}
			float timeToStep = 0.09f;
			float timeToStepSound = 0.18f;
			DungeonShambler[] shamblers = Object.FindObjectsOfType<DungeonShambler>();
			foreach (DungeonShambler dungeonShambler in shamblers)
			{
				if (dungeonShambler.transform.localPosition.z != 1000f)
				{
					dungeonShambler.transform.localPosition = new Vector3(0f, 0f, -1.1f);
				}
			}
			bool firstStep = true;
			float timeOut = 0f;
			while (vecs.Count > 0)
			{
				if (vecs.Count == 0)
				{
					yield return null;
				}
				else
				{
					if (vecs.Count >= 1)
					{
						this.lastSpace = base.transform.localPosition;
					}
					if (vecs.Count == 1)
					{
						this.gameManager.DespawnAllDungeonVisuals();
					}
					Vector3 pos2 = new Vector3(vecs[0].x, vecs[0].y, base.transform.localPosition.z);
					while (Vector2.Distance(base.transform.localPosition, pos2) > 0.01f)
					{
						float num = this.speedOfMovement;
						base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, pos2, num * Time.deltaTime);
						timeToStep -= Time.deltaTime * (num / this.speedOfMovement);
						timeToStepSound -= Time.deltaTime;
						if (timeToStepSound <= 0f)
						{
							SoundManager.main.PlaySFXPitched("footstep", Random.Range(0.85f, 1.15f), false);
							timeToStepSound = 0.22f;
						}
						if (timeToStep <= 0f)
						{
							timeToStep = 0.07f;
							Quaternion quaternion = Quaternion.identity;
							Vector3 vector = Vector3.zero;
							if (pos2.y < base.transform.localPosition.y - 0.01f)
							{
								quaternion = Quaternion.Euler(0f, 0f, (float)Random.Range(170, 191));
								vector = Vector3.right * Random.Range(-0.1f, 0.1f);
							}
							else if (pos2.x < base.transform.localPosition.x - 0.01f)
							{
								quaternion = Quaternion.Euler(0f, 0f, (float)Random.Range(80, 101));
								vector = Vector3.up * Random.Range(-0.1f, 0.1f);
							}
							else if (pos2.x > base.transform.localPosition.x + 0.01f)
							{
								quaternion = Quaternion.Euler(0f, 0f, (float)Random.Range(-100, -79));
								vector = Vector3.up * Random.Range(-0.1f, 0.1f);
							}
							else
							{
								vector = Vector3.right * Random.Range(-0.1f, 0.12f);
							}
							GameObject gameObject2 = Object.Instantiate<GameObject>(this.footStepPrefab, base.transform.position + Vector3.forward + Vector3.forward * 1.15f + vector, quaternion, base.transform.parent);
							if (!this.playerScript)
							{
								this.playerScript = Player.main;
							}
							else
							{
								gameObject2.GetComponent<SpriteRenderer>().sprite = this.playerScript.chosenCharacter.footstepSprite;
							}
						}
						yield return null;
					}
					base.transform.localPosition = pos2;
					GameObject[] objectsAtVector = PathFinding.GetObjectsAtVector(base.transform.position);
					DungeonRoom dungeonRoom = DungeonRoom.GetDungeonRoom(objectsAtVector);
					if (dungeonRoom)
					{
						this.dungeonEvent = DungeonPlayer.GetRelevantEvent(base.transform.position);
						if (this.dungeonEvent && vecs.Count > 1)
						{
							this.dungeonEvent.Pass(vecs[1] - pos2);
						}
					}
					timeOut = 0f;
					if (!firstStep)
					{
						foreach (DungeonShambler shambler in shamblers)
						{
							if (shambler.asleep)
							{
								shambler.ConsiderWaking();
								if (!shambler.asleep)
								{
									timeOut = 0.5f;
								}
							}
							else
							{
								yield return new WaitForFixedUpdate();
								shambler.SetPath();
								if (shambler.outPath.Count == 1)
								{
									shambler.transform.localPosition = new Vector3(0f, 0f, -1.1f);
									shambler.StopAllCoroutines();
									yield return shambler.Move();
									this.gameManager.DespawnAllDungeonVisuals();
									vecs = new List<Vector2>();
									this.room = this.FindClosestRoom();
								}
								else if (shambler.outPath.Count >= 2)
								{
									shambler.transform.localPosition = new Vector3(0f, 0f, -1.1f);
									shambler.StopAllCoroutines();
									shambler.StartCoroutine(shambler.Move());
									timeOut = 0.25f;
								}
								shambler = null;
							}
						}
						DungeonShambler[] array7 = null;
						foreach (DungeonEvent dungeonEvent in Object.FindObjectsOfType<DungeonEvent>())
						{
							if (dungeonEvent.turnsToExpire != -1)
							{
								dungeonEvent.turnsToExpire--;
								dungeonEvent.UpdateText();
								if (dungeonEvent.turnsToExpire == 0)
								{
									dungeonEvent.DestroyWithParticles();
								}
							}
						}
					}
					if (dungeonRoom)
					{
						this.dungeonEvent = DungeonPlayer.GetRelevantEvent(base.transform.position);
						if (this.dungeonEvent && (!this.dungeonEvent.passable || this.dungeonEvent.dungeonEventType == DungeonEvent.DungeonEventType.Shambler))
						{
							this.gameManager.DespawnAllDungeonVisuals();
							vecs = new List<Vector2>();
							this.room = this.FindClosestRoom();
						}
					}
					firstStep = false;
					if (vecs.Count > 0)
					{
						vecs.RemoveAt(0);
					}
					pos2 = default(Vector3);
					dungeonRoom = null;
				}
			}
			this.isMoving = false;
			this.gameManager.travelling = false;
			this.MoveToClosestRoom();
			this.ArriveAnimation();
			while (timeOut > 0f)
			{
				Object.FindObjectOfType<BackgroundController>().running = false;
				yield return null;
				timeOut -= Time.deltaTime;
			}
			this.Arrive();
			player = null;
			shamblers = null;
		}
		else
		{
			this.MoveToClosestRoom();
			SoundManager.main.PlaySFX("cantMoveHere");
			this.animator.Play("playerDungeonShake");
			if (this.isMoving)
			{
				yield return this.MoveToRoom(this.FindClosestRoom().transform.position, false);
			}
		}
		yield break;
	}

	// Token: 0x06000538 RID: 1336 RVA: 0x00033A4C File Offset: 0x00031C4C
	private void OnMouseOver()
	{
		if (Input.GetMouseButton(0))
		{
			this.RemoveCard();
			return;
		}
		this.ShowCard();
	}

	// Token: 0x06000539 RID: 1337 RVA: 0x00033A63 File Offset: 0x00031C63
	public override void OnCursorHold()
	{
		this.ShowCard();
	}

	// Token: 0x0600053A RID: 1338 RVA: 0x00033A6C File Offset: 0x00031C6C
	private void ShowCard()
	{
		this.timeToDisplayCard += Time.deltaTime;
		if (this.timeToDisplayCard > 0.35f && !this.previewCard)
		{
			this.previewCard = Object.Instantiate<GameObject>(this.cardPrefab, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("PrimaryCanvas").GetComponent<Canvas>().transform);
			Card component = this.previewCard.GetComponent<Card>();
			string text = "";
			Player player = Player.main;
			if (player)
			{
				text = LangaugeManager.main.GetTextByKey("map" + player.characterName.ToString());
			}
			component.GetDescriptionsSimple(new List<string> { text }, base.gameObject);
		}
	}

	// Token: 0x0600053B RID: 1339 RVA: 0x00033B36 File Offset: 0x00031D36
	public override void OnCursorEnd()
	{
		this.RemoveCard();
	}

	// Token: 0x0600053C RID: 1340 RVA: 0x00033B3E File Offset: 0x00031D3E
	private void OnMouseExit()
	{
		this.RemoveCard();
	}

	// Token: 0x0600053D RID: 1341 RVA: 0x00033B46 File Offset: 0x00031D46
	private void RemoveCard()
	{
		if (this.previewCard)
		{
			Object.Destroy(this.previewCard);
		}
		this.timeToDisplayCard = 0f;
	}

	// Token: 0x0600053E RID: 1342 RVA: 0x00033B6B File Offset: 0x00031D6B
	public IEnumerator FindWay()
	{
		this.isMovingFromLockedDoor = true;
		this.gameManager.travelling = true;
		yield return new WaitForSeconds(1f);
		this.gameManager.travelling = false;
		this.gameManager.ShowMap(false);
		this.gameManager.travelling = true;
		this.isMoving = true;
		yield return new WaitForSeconds(0.6f);
		yield return new WaitForFixedUpdate();
		this.room = null;
		this.StartCoroutine(this.MoveToRoom(base.transform.parent.TransformPoint(this.lastSpace), true));
		yield return new WaitForSeconds(0.6f);
		this.gameManager.travelling = false;
		this.isMovingFromLockedDoor = false;
		yield break;
	}

	// Token: 0x040003E7 RID: 999
	public static DungeonPlayer main;

	// Token: 0x040003E8 RID: 1000
	[SerializeField]
	public float speedOfMovement = 4.8f;

	// Token: 0x040003E9 RID: 1001
	private GameObject player;

	// Token: 0x040003EA RID: 1002
	private Transform playerTransform;

	// Token: 0x040003EB RID: 1003
	public DungeonRoom room;

	// Token: 0x040003EC RID: 1004
	[HideInInspector]
	private GameManager gameManager;

	// Token: 0x040003ED RID: 1005
	[HideInInspector]
	private GameFlowManager gameFlowManager;

	// Token: 0x040003EE RID: 1006
	[SerializeField]
	public bool isMoving;

	// Token: 0x040003EF RID: 1007
	private Animator animator;

	// Token: 0x040003F0 RID: 1008
	[SerializeField]
	private GameObject cardPrefab;

	// Token: 0x040003F1 RID: 1009
	private GameObject previewCard;

	// Token: 0x040003F2 RID: 1010
	private float timeToDisplayCard;

	// Token: 0x040003F3 RID: 1011
	public bool locked;

	// Token: 0x040003F4 RID: 1012
	[SerializeField]
	public GameObject footStepPrefab;

	// Token: 0x040003F5 RID: 1013
	public DungeonEvent dungeonEvent;

	// Token: 0x040003F6 RID: 1014
	private Player playerScript;

	// Token: 0x040003F7 RID: 1015
	private SpriteRenderer spriteRenderer;

	// Token: 0x040003F8 RID: 1016
	private List<GameObject> roomFades = new List<GameObject>();

	// Token: 0x040003F9 RID: 1017
	[HideInInspector]
	public Vector2 lastSpace;

	// Token: 0x040003FA RID: 1018
	public bool isMovingFromLockedDoor;
}
