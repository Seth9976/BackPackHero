using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020000BD RID: 189
public class DungeonRoom : CustomInputHandler
{
	// Token: 0x06000540 RID: 1344 RVA: 0x00033B98 File Offset: 0x00031D98
	public static List<DungeonRoom> GetAllVisibleRooms()
	{
		List<DungeonRoom> list = new List<DungeonRoom>();
		foreach (DungeonRoom dungeonRoom in DungeonRoom.GetAllRooms())
		{
			if (dungeonRoom.canBeSeenOnMap)
			{
				list.Add(dungeonRoom);
			}
		}
		return list;
	}

	// Token: 0x06000541 RID: 1345 RVA: 0x00033BFC File Offset: 0x00031DFC
	public static List<DungeonRoom> GetAllRooms()
	{
		List<DungeonRoom> list = new List<DungeonRoom>();
		for (int i = 0; i < DungeonRoom.allRooms.Count; i++)
		{
			DungeonRoom dungeonRoom = DungeonRoom.allRooms[i];
			if (!(dungeonRoom == null) && !(dungeonRoom.gameObject == null) && dungeonRoom.gameObject.activeInHierarchy)
			{
				list.Add(dungeonRoom);
			}
		}
		if (Application.isPlaying)
		{
			return list;
		}
		DungeonRoom.allRooms = Object.FindObjectsOfType<DungeonRoom>().ToList<DungeonRoom>();
		return DungeonRoom.allRooms;
	}

	// Token: 0x06000542 RID: 1346 RVA: 0x00033C78 File Offset: 0x00031E78
	private void OnEnable()
	{
		if (!DungeonRoom.allRooms.Contains(this))
		{
			DungeonRoom.allRooms.Add(this);
		}
	}

	// Token: 0x06000543 RID: 1347 RVA: 0x00033C92 File Offset: 0x00031E92
	private void OnDisable()
	{
		DungeonRoom.allRooms.Remove(this);
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x00033CA0 File Offset: 0x00031EA0
	private void Start()
	{
		this.gameManager = GameManager.main;
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x00033CBC File Offset: 0x00031EBC
	public static List<DungeonRoom> GetAllDungeonRooms(GameObject[] objs)
	{
		List<DungeonRoom> list = new List<DungeonRoom>();
		for (int i = 0; i < objs.Length; i++)
		{
			DungeonRoom component = objs[i].GetComponent<DungeonRoom>();
			if (component)
			{
				list.Add(component);
			}
		}
		return list;
	}

	// Token: 0x06000546 RID: 1350 RVA: 0x00033CF8 File Offset: 0x00031EF8
	public static DungeonRoom GetDungeonRoom(GameObject[] objs)
	{
		for (int i = 0; i < objs.Length; i++)
		{
			DungeonRoom component = objs[i].GetComponent<DungeonRoom>();
			if (component)
			{
				return component;
			}
		}
		return null;
	}

	// Token: 0x06000547 RID: 1351 RVA: 0x00033D2C File Offset: 0x00031F2C
	private void Update()
	{
		if (this.spriteRenderer && this.spriteRenderer.enabled && base.transform.position.x < 11f && this.spriteRenderer.color.a < 1f)
		{
			this.spriteRenderer.color = new Color(1f, 1f, 1f, Mathf.Min(this.spriteRenderer.color.a + 1.25f * Time.deltaTime, 1f));
		}
	}

	// Token: 0x06000548 RID: 1352 RVA: 0x00033DC9 File Offset: 0x00031FC9
	private void OnMouseUpAsButton()
	{
		this.MoveToRoom();
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x00033DD1 File Offset: 0x00031FD1
	public override void OnPressStart(string keyName, bool overrideKeyName)
	{
		if (keyName == "confirm" || overrideKeyName)
		{
			this.MoveToRoom();
		}
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x00033DE8 File Offset: 0x00031FE8
	public void MoveToRoom()
	{
		if (this.gameManager.viewingEvent || (!this.canBeSeenOnMap && TutorialManager.main.tutorialSequence == TutorialManager.TutorialSequence.trulyDone) || this.gameManager.dead)
		{
			return;
		}
		if (SingleUI.IsViewingPopUp())
		{
			return;
		}
		if (this.isLocked)
		{
			return;
		}
		if (EventLocksDungeonMovement.IsLocked())
		{
			DungeonPlayer.main.Shake();
			return;
		}
		if (DungeonPlayer.main.InABlockedroom())
		{
			DungeonPlayer.main.Shake();
			PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm37b"));
			return;
		}
		int num = 0;
		this.consideringItemToLeaveBehind = null;
		if (!this.isLocked && !this.gameManager.viewingEvent && Object.FindObjectOfType<MapManager>().mapMode == MapManager.MapMode.move)
		{
			for (int i = 0; i < Item2.allItems.Count; i++)
			{
				Item2 item = Item2.allItems[i];
				if (item && item.itemMovement && !item.itemMovement.inGrid && !item.destroyed && item.itemType.Contains(Item2.ItemType.Curse))
				{
					item.itemMovement.PretendDestroyCurse();
					num++;
					i--;
				}
			}
		}
		GameManager.main.ChangeCurse(num);
		DungeonPlayer.main.MoveTo(base.transform.position, this);
	}

	// Token: 0x0600054B RID: 1355 RVA: 0x00033F38 File Offset: 0x00032138
	public static DungeonRoom GetRoom(Vector2 position)
	{
		GameObject[] objectsAtVector = PathFinding.GetObjectsAtVector(position);
		for (int i = 0; i < objectsAtVector.Length; i++)
		{
			DungeonRoom component = objectsAtVector[i].GetComponent<DungeonRoom>();
			if (component)
			{
				return component;
			}
		}
		return null;
	}

	// Token: 0x0600054C RID: 1356 RVA: 0x00033F70 File Offset: 0x00032170
	public static void ChooseAllSprites()
	{
		foreach (DungeonRoom dungeonRoom in DungeonRoom.GetAllRooms())
		{
			dungeonRoom.ChooseSprite();
		}
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x00033FC0 File Offset: 0x000321C0
	public static void ChooseAllSpritesInArea(Vector2 area)
	{
		foreach (DungeonRoom dungeonRoom in DungeonRoom.GetAllRooms())
		{
			if (Vector2.Distance(dungeonRoom.transform.position, area) <= 1f)
			{
				dungeonRoom.ChooseSprite();
			}
		}
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x00034030 File Offset: 0x00032230
	public void ChooseSprite()
	{
		DungeonRoomSelector component = base.GetComponent<DungeonRoomSelector>();
		if (component)
		{
			component.ChooseSprite();
		}
		this.dungeonEvent = base.GetComponentInChildren<DungeonEvent>();
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x00034060 File Offset: 0x00032260
	public override void OnCursorHold()
	{
		if (Input.GetMouseButton(0))
		{
			this.RemoveCard();
			return;
		}
		this.timeToDisplayCard += Time.deltaTime;
		if (this.timeToDisplayCard > 0.3f && !this.previewCard)
		{
			if (SingleUI.IsViewingPopUp())
			{
				return;
			}
			DungeonEvent relevantEvent = DungeonPlayer.GetRelevantEvent(base.transform.position);
			if (!relevantEvent || this.gameManager.viewingEvent || !this.canBeSeenOnMap || relevantEvent.IsFinished())
			{
				return;
			}
			this.previewCard = Object.Instantiate<GameObject>(this.cardPrefab, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("PrimaryCanvas").GetComponent<Canvas>().transform);
			Card component = this.previewCard.GetComponent<Card>();
			if (relevantEvent.mapTextOverrideKey.Length > 1)
			{
				component.GetDescriptionsSimple(new List<string> { LangaugeManager.main.GetTextByKey(relevantEvent.mapTextOverrideKey) }, base.gameObject);
				return;
			}
			if (relevantEvent.dungeonEventType == DungeonEvent.DungeonEventType.Enemy || relevantEvent.dungeonEventType == DungeonEvent.DungeonEventType.Shambler)
			{
				string text = LangaugeManager.main.GetTextByKey("map1");
				foreach (GameObject gameObject in relevantEvent.itemsToSpawn)
				{
					text = text + "<br>  " + LangaugeManager.main.GetTextByKey(Enemy.GetRealName(gameObject.name));
				}
				component.GetDescriptionsSimple(new List<string> { text }, base.gameObject);
				return;
			}
			if (relevantEvent.dungeonEventType == DungeonEvent.DungeonEventType.Exit)
			{
				string text2 = LangaugeManager.main.GetTextByKey("map2");
				text2 = text2.Replace("/x", LangaugeManager.main.GetTextByKey(this.gameManager.GetNextLevelName(relevantEvent.doorNumber)));
				component.GetDescriptionsSimple(new List<string> { text2 }, base.gameObject);
				return;
			}
			if (relevantEvent.dungeonEventType == DungeonEvent.DungeonEventType.Chest)
			{
				component.GetDescriptionsSimple(new List<string> { LangaugeManager.main.GetTextByKey("map3") }, base.gameObject);
				return;
			}
			if (relevantEvent.dungeonEventType == DungeonEvent.DungeonEventType.Chance || relevantEvent.dungeonEventType == DungeonEvent.DungeonEventType.EventNoNPC)
			{
				component.GetDescriptionsSimple(new List<string> { LangaugeManager.main.GetTextByKey("map4") }, base.gameObject);
				return;
			}
			if (relevantEvent.dungeonEventType == DungeonEvent.DungeonEventType.Store)
			{
				component.GetDescriptionsSimple(new List<string> { LangaugeManager.main.GetTextByKey("map5") }, base.gameObject);
				return;
			}
			if (relevantEvent.dungeonEventType == DungeonEvent.DungeonEventType.Lock)
			{
				component.GetDescriptionsSimple(new List<string> { LangaugeManager.main.GetTextByKey("map6") }, base.gameObject);
				return;
			}
			if (relevantEvent.dungeonEventType == DungeonEvent.DungeonEventType.Healer)
			{
				component.GetDescriptionsSimple(new List<string> { LangaugeManager.main.GetTextByKey("map7") }, base.gameObject);
				return;
			}
			if (relevantEvent.dungeonEventType == DungeonEvent.DungeonEventType.SimpleClickItem)
			{
				component.GetDescriptionsSimple(new List<string> { LangaugeManager.main.GetTextByKey("map10") }, base.gameObject);
				return;
			}
			if (relevantEvent.dungeonEventType == DungeonEvent.DungeonEventType.CaveIn)
			{
				component.GetDescriptionsSimple(new List<string> { LangaugeManager.main.GetTextByKey("map11") }, base.gameObject);
				return;
			}
			if (relevantEvent.dungeonEventType == DungeonEvent.DungeonEventType.Burrow)
			{
				component.GetDescriptionsSimple(new List<string> { LangaugeManager.main.GetTextByKey("map12") }, base.gameObject);
				return;
			}
			if (relevantEvent.dungeonEventType == DungeonEvent.DungeonEventType.Parcel)
			{
				component.GetDescriptionsSimple(new List<string> { LangaugeManager.main.GetTextByKey("map13") }, base.gameObject);
			}
		}
	}

	// Token: 0x06000550 RID: 1360 RVA: 0x0003441C File Offset: 0x0003261C
	public override void OnCursorEnd()
	{
		this.RemoveCard();
	}

	// Token: 0x06000551 RID: 1361 RVA: 0x00034424 File Offset: 0x00032624
	private void RemoveCard()
	{
		if (this.previewCard)
		{
			Object.Destroy(this.previewCard);
		}
		this.timeToDisplayCard = 0f;
	}

	// Token: 0x040003FB RID: 1019
	public static List<DungeonRoom> allRooms = new List<DungeonRoom>();

	// Token: 0x040003FC RID: 1020
	[Header("Blocks")]
	[SerializeField]
	public bool leftBlocked;

	// Token: 0x040003FD RID: 1021
	[SerializeField]
	public bool rightBlocked;

	// Token: 0x040003FE RID: 1022
	[SerializeField]
	public bool upBlocked;

	// Token: 0x040003FF RID: 1023
	[SerializeField]
	public bool downBlocked;

	// Token: 0x04000400 RID: 1024
	[SerializeField]
	public bool left;

	// Token: 0x04000401 RID: 1025
	[SerializeField]
	public bool right;

	// Token: 0x04000402 RID: 1026
	[SerializeField]
	public bool up;

	// Token: 0x04000403 RID: 1027
	[SerializeField]
	public bool down;

	// Token: 0x04000404 RID: 1028
	[SerializeField]
	private GameObject cardPrefab;

	// Token: 0x04000405 RID: 1029
	private DungeonEvent dungeonEvent;

	// Token: 0x04000406 RID: 1030
	private GameObject previewCard;

	// Token: 0x04000407 RID: 1031
	private float timeToDisplayCard;

	// Token: 0x04000408 RID: 1032
	public bool isLocked;

	// Token: 0x04000409 RID: 1033
	public bool canBeSeenOnMap;

	// Token: 0x0400040A RID: 1034
	private GameManager gameManager;

	// Token: 0x0400040B RID: 1035
	public SpriteRenderer spriteRenderer;

	// Token: 0x0400040C RID: 1036
	private Item2 consideringItemToLeaveBehind;
}
