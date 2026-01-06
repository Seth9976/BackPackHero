using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Token: 0x020000C6 RID: 198
public class DungeonEvent : MonoBehaviour
{
	// Token: 0x060005A1 RID: 1441 RVA: 0x000375AC File Offset: 0x000357AC
	public static DungeonEvent FindDungeonEventOfType(DungeonEvent.DungeonEventType dungeonEventType)
	{
		foreach (DungeonEvent dungeonEvent in Object.FindObjectsOfType<DungeonEvent>())
		{
			if (dungeonEvent.dungeonEventType == dungeonEventType)
			{
				return dungeonEvent;
			}
		}
		return null;
	}

	// Token: 0x060005A2 RID: 1442 RVA: 0x000375DD File Offset: 0x000357DD
	private void Awake()
	{
		if (!DungeonEvent.dungeonEvents.Contains(this))
		{
			DungeonEvent.dungeonEvents.Add(this);
		}
	}

	// Token: 0x060005A3 RID: 1443 RVA: 0x000375F7 File Offset: 0x000357F7
	private void OnDestroy()
	{
		DungeonEvent.dungeonEvents.Remove(this);
	}

	// Token: 0x060005A4 RID: 1444 RVA: 0x00037605 File Offset: 0x00035805
	public void FinishEvent()
	{
		this.AddEventProperty(DungeonEvent.EventProperty.Type.finished, 0);
		if (this.exitPrefab)
		{
			Object.Instantiate<GameObject>(this.exitPrefab, base.transform.position, Quaternion.identity, base.transform.parent);
		}
	}

	// Token: 0x060005A5 RID: 1445 RVA: 0x00037644 File Offset: 0x00035844
	public void RemoveEventProperty(DungeonEvent.EventProperty.Type type)
	{
		for (int i = 0; i < this.eventProperties.Count; i++)
		{
			DungeonEvent.EventProperty eventProperty = this.eventProperties[i];
			if (eventProperty.type == type)
			{
				this.eventProperties.Remove(eventProperty);
				i--;
			}
		}
	}

	// Token: 0x060005A6 RID: 1446 RVA: 0x00037690 File Offset: 0x00035890
	public void AddEventProperty(DungeonEvent.EventProperty.Type type, List<int> values)
	{
		foreach (DungeonEvent.EventProperty eventProperty in this.eventProperties)
		{
			if (eventProperty.type == type)
			{
				eventProperty.values = values;
				return;
			}
		}
		DungeonEvent.EventProperty eventProperty2 = new DungeonEvent.EventProperty();
		eventProperty2.type = type;
		eventProperty2.value = 0;
		eventProperty2.values = values;
		this.eventProperties.Add(eventProperty2);
	}

	// Token: 0x060005A7 RID: 1447 RVA: 0x00037718 File Offset: 0x00035918
	public void AddEventProperty(DungeonEvent.EventProperty.Type type, int value = 0)
	{
		foreach (DungeonEvent.EventProperty eventProperty in this.eventProperties)
		{
			if (eventProperty.type == type)
			{
				eventProperty.value += value;
				return;
			}
		}
		DungeonEvent.EventProperty eventProperty2 = new DungeonEvent.EventProperty();
		eventProperty2.type = type;
		eventProperty2.value = value;
		this.eventProperties.Add(eventProperty2);
	}

	// Token: 0x060005A8 RID: 1448 RVA: 0x000377A0 File Offset: 0x000359A0
	public DungeonEvent.EventProperty GetEventProperty(DungeonEvent.EventProperty.Type type)
	{
		foreach (DungeonEvent.EventProperty eventProperty in this.eventProperties)
		{
			if (eventProperty.type == type)
			{
				return eventProperty;
			}
		}
		return null;
	}

	// Token: 0x060005A9 RID: 1449 RVA: 0x000377FC File Offset: 0x000359FC
	public int GetEventPropertyValue(DungeonEvent.EventProperty.Type type)
	{
		foreach (DungeonEvent.EventProperty eventProperty in this.eventProperties)
		{
			if (eventProperty.type == type)
			{
				return eventProperty.value;
			}
		}
		return -1;
	}

	// Token: 0x060005AA RID: 1450 RVA: 0x00037860 File Offset: 0x00035A60
	public static List<DungeonEvent> FindDungeonEventsOfType(DungeonEvent.DungeonEventType eventType)
	{
		List<DungeonEvent> list = new List<DungeonEvent>();
		foreach (DungeonEvent dungeonEvent in Object.FindObjectsOfType<DungeonEvent>())
		{
			if (dungeonEvent.dungeonEventType == eventType)
			{
				list.Add(dungeonEvent);
			}
		}
		return list;
	}

	// Token: 0x060005AB RID: 1451 RVA: 0x0003789C File Offset: 0x00035A9C
	public static List<DungeonEvent> FindAllDungeonEvents()
	{
		List<DungeonEvent> list = new List<DungeonEvent>();
		foreach (DungeonEvent dungeonEvent in Object.FindObjectsOfType<DungeonEvent>())
		{
			list.Add(dungeonEvent);
		}
		return list;
	}

	// Token: 0x060005AC RID: 1452 RVA: 0x000378D0 File Offset: 0x00035AD0
	private void Start()
	{
		if (this.doorNumber != 0 && Singleton.Instance.IsStoryModeLevels())
		{
			Object.Destroy(base.gameObject);
			return;
		}
		if (Object.FindObjectOfType<TutorialManager>().playType == TutorialManager.PlayType.testing && Object.FindObjectOfType<TutorialManager>().moveThroughBlockedItems)
		{
			this.passable = true;
		}
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		this.parentRoomSR = base.transform.parent.GetComponent<SpriteRenderer>();
		this.gameManager = GameManager.main;
		this.gameFlowManager = GameFlowManager.main;
		if (this.GetEventProperty(DungeonEvent.EventProperty.Type.mimic) == null && this.dungeonEventType == DungeonEvent.DungeonEventType.Chest && this.gameManager.floor != 0 && this.gameManager.dungeonLevel.currentFloor != DungeonLevel.Floor.boss)
		{
			if (Random.Range(0, 10) == 1)
			{
				this.AddEventProperty(DungeonEvent.EventProperty.Type.mimic, 1);
			}
			else
			{
				this.AddEventProperty(DungeonEvent.EventProperty.Type.mimic, -999);
			}
		}
		if (this.dungeonEventType == DungeonEvent.DungeonEventType.SimpleClickItem && this.GetEventPropertyValue(DungeonEvent.EventProperty.Type.SimpleClickItem) == -1)
		{
			List<int> list = new List<int> { 0, 1, 2 };
			int num = Random.Range(0, 10);
			if (GameManager.main.floor > 1 && num > 8 && (!Singleton.Instance.storyMode || MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedCurses)))
			{
				list.Add(3);
				list.Add(4);
				list.Add(5);
			}
			for (int i = 0; i < DungeonEvent.simpleClicks.Count; i++)
			{
				list.Remove(DungeonEvent.simpleClicks[i]);
			}
			if (list.Count == 0)
			{
				list = new List<int> { 0, 1, 2 };
				if (GameManager.main.floor > 1 && num > 8 && (!Singleton.Instance.storyMode || MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedCurses)))
				{
					list.Add(3);
					list.Add(4);
					list.Add(5);
				}
				DungeonEvent.simpleClicks = new List<int>();
			}
			int num2 = Random.Range(0, list.Count);
			DungeonEvent.simpleClicks.Add(list[num2]);
			this.AddEventProperty(DungeonEvent.EventProperty.Type.SimpleClickItem, list[num2]);
			int num3 = list[num2];
			this.spriteRenderer.sprite = this.sprites[Mathf.Min(num3, this.sprites.Length - 1)];
			this.itemsToSpawn = new List<GameObject> { this.itemsToSpawn[Mathf.Min(num3, this.itemsToSpawn.Count - 1)] };
		}
		if (!this.text)
		{
			this.text = base.GetComponentInChildren<TextMeshPro>();
		}
		if (this.text)
		{
			if (this.turnsToExpire == -1)
			{
				this.text.gameObject.SetActive(false);
			}
			else
			{
				this.text.gameObject.SetActive(true);
				if (this.turnsToExpire == 999)
				{
					base.StartCoroutine(this.FindDistance());
				}
			}
		}
		this.BuildParticles();
		using (List<GameObject>.Enumerator enumerator = this.itemsToSpawn.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current == null)
				{
					Object.Destroy(base.gameObject);
				}
			}
		}
	}

	// Token: 0x060005AD RID: 1453 RVA: 0x00037C0C File Offset: 0x00035E0C
	public static void RemoveAllParticlesInDungeonEvents()
	{
		DungeonEvent[] array = Object.FindObjectsOfType<DungeonEvent>();
		for (int i = 0; i < array.Length; i++)
		{
			ParticleSystem[] componentsInChildren = array[i].GetComponentsInChildren<ParticleSystem>();
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				Object.Destroy(componentsInChildren[j].gameObject);
			}
		}
	}

	// Token: 0x060005AE RID: 1454 RVA: 0x00037C54 File Offset: 0x00035E54
	public static void BuildParticlesInDungeonEvents()
	{
		DungeonEvent[] array = Object.FindObjectsOfType<DungeonEvent>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].BuildParticles();
		}
	}

	// Token: 0x060005AF RID: 1455 RVA: 0x00037C80 File Offset: 0x00035E80
	private void BuildParticles()
	{
		if (this.dungeonEventType == DungeonEvent.DungeonEventType.CaveIn)
		{
			foreach (GameObject gameObject in this.particles)
			{
				if (gameObject)
				{
					Object.Destroy(gameObject);
				}
			}
			this.particles = new List<GameObject>();
			if (this.gameManager.caveInParticles && !this.IsFinished())
			{
				GameObject gameObject2 = Object.Instantiate<GameObject>(this.gameManager.caveInParticles, base.transform.position, Quaternion.identity, base.transform);
				this.particles.Add(gameObject2);
				gameObject2.SetActive(true);
			}
			this.destroyParticles = this.gameManager.caveInCollapseParticles;
		}
	}

	// Token: 0x060005B0 RID: 1456 RVA: 0x00037D58 File Offset: 0x00035F58
	private IEnumerator FindDistance()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		List<Vector2> list;
		PathFinding.FindPath(base.transform.parent.position, Object.FindObjectOfType<DungeonPlayer>().FindClosestRoom().transform.position, new Func<PathFinding.Location, bool, bool>(DungeonPlayer.AcceptableSpaceAnyRoom), out list, null);
		this.turnsToExpire = list.Count + list.Count / 5 + Random.Range(3, 6);
		this.UpdateText();
		yield break;
	}

	// Token: 0x060005B1 RID: 1457 RVA: 0x00037D68 File Offset: 0x00035F68
	public void UpdateText()
	{
		if (!this.text)
		{
			this.text = base.GetComponentInChildren<TextMeshPro>();
		}
		if (this.text)
		{
			this.text.text = this.turnsToExpire.ToString() ?? "";
			this.text.transform.localScale = Vector3.one * 1.5f;
		}
	}

	// Token: 0x060005B2 RID: 1458 RVA: 0x00037DDC File Offset: 0x00035FDC
	private void Update()
	{
		if (!this.parentRoomSR)
		{
			this.parentRoomSR = base.transform.parent.GetComponent<SpriteRenderer>();
			return;
		}
		this.spriteRenderer.color = this.parentRoomSR.color;
		if (this.text && this.text.gameObject && this.text.gameObject.activeInHierarchy)
		{
			this.text.gameObject.transform.localScale = Vector3.MoveTowards(this.text.gameObject.transform.localScale, Vector3.one, 3f * Time.deltaTime);
		}
	}

	// Token: 0x060005B3 RID: 1459 RVA: 0x00037E93 File Offset: 0x00036093
	private void Finished()
	{
	}

	// Token: 0x060005B4 RID: 1460 RVA: 0x00037E95 File Offset: 0x00036095
	public bool IsFinished()
	{
		return this.GetEventProperty(DungeonEvent.EventProperty.Type.finished) != null;
	}

	// Token: 0x060005B5 RID: 1461 RVA: 0x00037EA4 File Offset: 0x000360A4
	public void DestroyWithParticles()
	{
		Object.Instantiate<GameObject>(this.destroyParticles, base.transform.position + Vector3.back, Quaternion.identity);
		this.turnsToExpire = -1;
		SoundManager.main.PlaySFX("negative");
		if (this.parentRoomSR)
		{
			this.parentRoomSR.enabled = false;
		}
		DungeonRoom componentInParent = base.GetComponentInParent<DungeonRoom>();
		if (componentInParent)
		{
			Object.FindObjectOfType<DungeonPlayer>().PathFromPlayerToRoom(componentInParent);
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060005B6 RID: 1462 RVA: 0x00037F2C File Offset: 0x0003612C
	public void Pass(Vector2 dir)
	{
		if (this.IsFinished())
		{
			return;
		}
		if (this.dungeonEventType == DungeonEvent.DungeonEventType.CaveIn && (dir == this.caveIn || dir == this.caveIn * -1f))
		{
			if (this.destroyParticles)
			{
				Object.Instantiate<GameObject>(this.destroyParticles, base.transform.position + Vector3.back, Quaternion.identity);
				if (this.particles[0])
				{
					Object.Destroy(this.particles[0]);
				}
			}
			SoundManager.main.PlaySFX("hitShield");
			this.FinishEvent();
			DungeonRoom componentInParent = base.GetComponentInParent<DungeonRoom>();
			if (dir == Vector2.right)
			{
				componentInParent.leftBlocked = true;
			}
			else if (dir == Vector2.left)
			{
				componentInParent.rightBlocked = true;
			}
			else if (dir == Vector2.up)
			{
				componentInParent.downBlocked = true;
			}
			else if (dir == Vector2.down)
			{
				componentInParent.upBlocked = true;
			}
			DungeonRoom.ChooseAllSpritesInArea(base.transform.position);
			Object.FindObjectOfType<DungeonPlayer>().FindReachableEvents();
		}
	}

	// Token: 0x060005B7 RID: 1463 RVA: 0x00038060 File Offset: 0x00036260
	public void Play()
	{
		Debug.Log("Playing event " + base.gameObject.name);
		if (this.GetEventProperty(DungeonEvent.EventProperty.Type.open) != null)
		{
			return;
		}
		if (this.itemsToSpawn.Count == 0)
		{
			return;
		}
		this.AddEventProperty(DungeonEvent.EventProperty.Type.open, 0);
		Player main = Player.main;
		if (this.dungeonEventType == DungeonEvent.DungeonEventType.Enemy && this.itemsToSpawn.Count == 1 && !this.itemsToSpawn[0].GetComponent<Enemy>())
		{
			this.dungeonEventType = DungeonEvent.DungeonEventType.Chance;
		}
		if (this.dungeonEventType == DungeonEvent.DungeonEventType.Enemy || this.dungeonEventType == DungeonEvent.DungeonEventType.Shambler)
		{
			for (int i = 0; i < this.itemsToSpawn.Count; i++)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.itemsToSpawn[i], Vector3.zero, Quaternion.identity, main.transform.parent);
				gameObject.transform.localPosition = new Vector3(this.gameManager.spawnPosition.position.x - (float)(i * 2) + 1f - gameObject.GetComponent<BoxCollider2D>().size.x / 2f, -4.8f + gameObject.GetComponent<BoxCollider2D>().size.y / 2f, 1f);
			}
			this.FinishEvent();
			this.gameManager.currentEventType = this.dungeonEventType;
			this.gameFlowManager.StartCombat();
			return;
		}
		if (this.dungeonEventType == DungeonEvent.DungeonEventType.Chest || this.dungeonEventType == DungeonEvent.DungeonEventType.Store || this.dungeonEventType == DungeonEvent.DungeonEventType.Chance || this.dungeonEventType == DungeonEvent.DungeonEventType.Healer || this.dungeonEventType == DungeonEvent.DungeonEventType.SimpleClickItem || this.dungeonEventType == DungeonEvent.DungeonEventType.Altar || this.dungeonEventType == DungeonEvent.DungeonEventType.Burrow || this.dungeonEventType == DungeonEvent.DungeonEventType.Parcel)
		{
			for (int j = 0; j < this.itemsToSpawn.Count; j++)
			{
				GameObject gameObject2 = Object.Instantiate<GameObject>(this.itemsToSpawn[j], Vector3.zero, Quaternion.identity, main.transform.parent);
				gameObject2.transform.localPosition = new Vector3(this.gameManager.spawnPosition.position.x - (float)(j * 2) - 1f, this.itemsToSpawn[j].transform.position.y, -2.5f);
				EventNPC componentInChildren = gameObject2.GetComponentInChildren<EventNPC>();
				if (componentInChildren)
				{
					componentInChildren.dungeonEvent = this;
				}
				Store componentInChildren2 = gameObject2.GetComponentInChildren<Store>();
				if (componentInChildren2)
				{
					componentInChildren2.dungeonEvent = this;
				}
				Chest componentInChildren3 = gameObject2.GetComponentInChildren<Chest>();
				if (componentInChildren3)
				{
					componentInChildren3.dungeonEvent = this;
				}
				SimpleClickItem componentInChildren4 = gameObject2.GetComponentInChildren<SimpleClickItem>();
				if (componentInChildren4)
				{
					componentInChildren4.dungeonEvent = this;
				}
				Parcel componentInChildren5 = gameObject2.GetComponentInChildren<Parcel>();
				if (componentInChildren5)
				{
					componentInChildren5.dungeonEvent = this;
				}
				CustomEvent componentInChildren6 = gameObject2.GetComponentInChildren<CustomEvent>();
				if (componentInChildren6)
				{
					componentInChildren6.dungeonEvent = this;
				}
			}
			Player.main.GetComponentInChildren<Animator>().Play("Player_Idle");
			return;
		}
		if (this.dungeonEventType == DungeonEvent.DungeonEventType.Lock || this.dungeonEventType == DungeonEvent.DungeonEventType.EventNoNPC)
		{
			this.gameManager.viewingEvent = true;
			GameObject gameObject3 = Object.Instantiate<GameObject>(this.itemsToSpawn[Random.Range(0, this.itemsToSpawn.Count)], Vector3.zero, Quaternion.identity, this.gameManager.eventsParent);
			gameObject3.transform.localPosition = Vector3.zero;
			gameObject3.GetComponent<RandomEventMaster>().dungeonEvent = this;
			Player.main.GetComponentInChildren<Animator>().Play("Player_Idle");
			return;
		}
		if (this.dungeonEventType == DungeonEvent.DungeonEventType.Exit)
		{
			for (int k = 0; k < this.itemsToSpawn.Count; k++)
			{
				GameObject gameObject4 = Object.Instantiate<GameObject>(this.itemsToSpawn[k], Vector3.zero, Quaternion.identity, main.transform.parent);
				gameObject4.transform.position = new Vector3(this.gameManager.spawnPosition.position.x - (float)(k * 2) - 1f, this.itemsToSpawn[k].transform.position.y, -2.5f);
				gameObject4.GetComponentInChildren<Door>().doorNumber = this.doorNumber;
			}
			Player.main.GetComponentInChildren<Animator>().Play("Player_Idle");
		}
	}

	// Token: 0x060005B8 RID: 1464 RVA: 0x0003849D File Offset: 0x0003669D
	public void StoreItem2s(List<Item2> items)
	{
		this.storedItems = items;
	}

	// Token: 0x060005B9 RID: 1465 RVA: 0x000384A6 File Offset: 0x000366A6
	public List<Item2> GetItem2s()
	{
		return this.storedItems;
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x000384B0 File Offset: 0x000366B0
	public void StoreItems(List<GameObject> items)
	{
		foreach (GameObject gameObject in items)
		{
			if (!gameObject.GetComponent<Item2>().destroyed)
			{
				gameObject.transform.SetParent(base.transform);
				gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x060005BB RID: 1467 RVA: 0x0003851C File Offset: 0x0003671C
	public bool GetItems(out List<GameObject> items)
	{
		items = new List<GameObject>();
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			bool activeInHierarchy = transform.gameObject.activeInHierarchy;
			transform.gameObject.SetActive(true);
			if (transform.GetComponent<Item2>())
			{
				items.Add(transform.gameObject);
			}
			else
			{
				transform.gameObject.SetActive(activeInHierarchy);
			}
		}
		return items.Count > 0;
	}

	// Token: 0x04000464 RID: 1124
	public static List<DungeonEvent> dungeonEvents = new List<DungeonEvent>();

	// Token: 0x04000465 RID: 1125
	[SerializeField]
	private Vector2 caveIn;

	// Token: 0x04000466 RID: 1126
	public static List<int> simpleClicks = new List<int>();

	// Token: 0x04000467 RID: 1127
	[SerializeField]
	public string mapTextOverrideKey = "";

	// Token: 0x04000468 RID: 1128
	public bool passable;

	// Token: 0x04000469 RID: 1129
	public bool cannotWalkAwayFrom;

	// Token: 0x0400046A RID: 1130
	public DungeonEvent.DungeonEventType dungeonEventType;

	// Token: 0x0400046B RID: 1131
	[SerializeField]
	public List<GameObject> itemsToSpawn;

	// Token: 0x0400046C RID: 1132
	[SerializeField]
	public GameObject exitPrefab;

	// Token: 0x0400046D RID: 1133
	[HideInInspector]
	private GameManager gameManager;

	// Token: 0x0400046E RID: 1134
	[HideInInspector]
	private GameFlowManager gameFlowManager;

	// Token: 0x0400046F RID: 1135
	private SpriteRenderer parentRoomSR;

	// Token: 0x04000470 RID: 1136
	private SpriteRenderer spriteRenderer;

	// Token: 0x04000471 RID: 1137
	[SerializeField]
	private Sprite[] sprites;

	// Token: 0x04000472 RID: 1138
	[SerializeField]
	private List<GameObject> particles = new List<GameObject>();

	// Token: 0x04000473 RID: 1139
	[SerializeField]
	private GameObject destroyParticles;

	// Token: 0x04000474 RID: 1140
	[SerializeField]
	public int turnsToExpire = -1;

	// Token: 0x04000475 RID: 1141
	[SerializeField]
	private TextMeshPro text;

	// Token: 0x04000476 RID: 1142
	public List<DungeonEvent.EventProperty> eventProperties;

	// Token: 0x04000477 RID: 1143
	public int doorNumber;

	// Token: 0x04000478 RID: 1144
	public List<Item2> storedItems = new List<Item2>();

	// Token: 0x020002FE RID: 766
	public enum DungeonEventType
	{
		// Token: 0x040011E5 RID: 4581
		Enemy,
		// Token: 0x040011E6 RID: 4582
		Elite,
		// Token: 0x040011E7 RID: 4583
		Chest,
		// Token: 0x040011E8 RID: 4584
		Store,
		// Token: 0x040011E9 RID: 4585
		Chance,
		// Token: 0x040011EA RID: 4586
		Lock,
		// Token: 0x040011EB RID: 4587
		Exit,
		// Token: 0x040011EC RID: 4588
		Healer,
		// Token: 0x040011ED RID: 4589
		EventNoNPC,
		// Token: 0x040011EE RID: 4590
		ExitAlternate,
		// Token: 0x040011EF RID: 4591
		Shambler,
		// Token: 0x040011F0 RID: 4592
		SimpleClickItem,
		// Token: 0x040011F1 RID: 4593
		CaveIn,
		// Token: 0x040011F2 RID: 4594
		Altar,
		// Token: 0x040011F3 RID: 4595
		Burrow,
		// Token: 0x040011F4 RID: 4596
		Parcel
	}

	// Token: 0x020002FF RID: 767
	[Serializable]
	public class EventProperty
	{
		// Token: 0x040011F5 RID: 4597
		public DungeonEvent.EventProperty.Type type;

		// Token: 0x040011F6 RID: 4598
		public int value;

		// Token: 0x040011F7 RID: 4599
		public List<int> values = new List<int>();

		// Token: 0x0200049A RID: 1178
		public enum Type
		{
			// Token: 0x04001AD9 RID: 6873
			finished,
			// Token: 0x04001ADA RID: 6874
			cost,
			// Token: 0x04001ADB RID: 6875
			boughtItems,
			// Token: 0x04001ADC RID: 6876
			mimic,
			// Token: 0x04001ADD RID: 6877
			randomSeed,
			// Token: 0x04001ADE RID: 6878
			SimpleClickItem,
			// Token: 0x04001ADF RID: 6879
			open,
			// Token: 0x04001AE0 RID: 6880
			increaseCost,
			// Token: 0x04001AE1 RID: 6881
			selectedType
		}
	}
}
