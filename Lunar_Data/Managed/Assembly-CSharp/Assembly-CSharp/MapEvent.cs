using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000055 RID: 85
public class MapEvent : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	// Token: 0x06000273 RID: 627 RVA: 0x0000CE80 File Offset: 0x0000B080
	private void OnEnable()
	{
		MapEvent.mapEvents.Add(this);
	}

	// Token: 0x06000274 RID: 628 RVA: 0x0000CE8D File Offset: 0x0000B08D
	private void OnDisable()
	{
		MapEvent.mapEvents.Remove(this);
	}

	// Token: 0x06000275 RID: 629 RVA: 0x0000CE9B File Offset: 0x0000B09B
	private void Start()
	{
	}

	// Token: 0x06000276 RID: 630 RVA: 0x0000CE9D File Offset: 0x0000B09D
	private void Update()
	{
	}

	// Token: 0x06000277 RID: 631 RVA: 0x0000CEA0 File Offset: 0x0000B0A0
	public static void SetupEvents()
	{
		foreach (MapEvent mapEvent in MapEvent.mapEvents)
		{
			mapEvent.SetupEvent();
		}
	}

	// Token: 0x06000278 RID: 632 RVA: 0x0000CEF0 File Offset: 0x0000B0F0
	private void SetEventRandomAndSprite()
	{
		MapEventContentSpawner componentInChildren = base.GetComponentInChildren<MapEventContentSpawner>();
		if (componentInChildren)
		{
			componentInChildren.SpawnEvent();
		}
		MapDescription component = this.eventPrefab.GetComponent<MapDescription>();
		if (component)
		{
			this.eventIconImage.sprite = component.mapIcon;
			return;
		}
		if (this.eventPrefab.GetComponent<Room>())
		{
			this.eventIconImage.sprite = this.combatIcon;
			return;
		}
		this.eventIconImage.sprite = this.eventIcon;
	}

	// Token: 0x06000279 RID: 633 RVA: 0x0000CF70 File Offset: 0x0000B170
	private void SetupEvent()
	{
		this.SetEventRandomAndSprite();
		List<MapEvent> eventsWithinRange = this.GetEventsWithinRange(75f * CanvasManager.instance.masterContentScaler.localScale.x);
		List<MapEvent> eventsToTheRight = this.GetEventsToTheRight(eventsWithinRange);
		this.connectedEvents = eventsToTheRight;
		foreach (MapEvent mapEvent in eventsToTheRight)
		{
			this.DrawNodes(15f, mapEvent);
		}
	}

	// Token: 0x0600027A RID: 634 RVA: 0x0000CFFC File Offset: 0x0000B1FC
	public void CreateEvent()
	{
		if (!this.eventPrefab)
		{
			return;
		}
		if (this.eventPrefab.GetComponent<Room>())
		{
			RoomManager.instance.chosenRoomPrefab = this.eventPrefab;
			return;
		}
		Object.Instantiate<GameObject>(this.eventPrefab, CanvasManager.instance.masterContentScaler);
	}

	// Token: 0x0600027B RID: 635 RVA: 0x0000D050 File Offset: 0x0000B250
	public int GetEventColumn()
	{
		return base.transform.parent.GetSiblingIndex();
	}

	// Token: 0x0600027C RID: 636 RVA: 0x0000D064 File Offset: 0x0000B264
	public List<MapEvent> GetEventsWithinRange(float range)
	{
		List<MapEvent> list = new List<MapEvent>();
		foreach (MapEvent mapEvent in MapEvent.mapEvents)
		{
			if (!(mapEvent == this) && Vector2.Distance(base.transform.position, mapEvent.transform.position) <= range)
			{
				list.Add(mapEvent);
			}
		}
		return list;
	}

	// Token: 0x0600027D RID: 637 RVA: 0x0000D0F0 File Offset: 0x0000B2F0
	public List<MapEvent> GetEventsToTheRight(List<MapEvent> events)
	{
		List<MapEvent> list = new List<MapEvent>();
		foreach (MapEvent mapEvent in events)
		{
			if (mapEvent.transform.position.x > base.transform.position.x + 25f)
			{
				list.Add(mapEvent);
			}
		}
		return list;
	}

	// Token: 0x0600027E RID: 638 RVA: 0x0000D170 File Offset: 0x0000B370
	public void DrawNodes(float numNodes, MapEvent otherEvent)
	{
		if (!this.eventConnectionNode)
		{
			return;
		}
		Vector3 normalized = (otherEvent.transform.position - base.transform.position).normalized;
		float num = Vector3.Distance(base.transform.position, otherEvent.transform.position) / numNodes;
		int num2 = 3;
		while ((float)num2 < numNodes - 3f)
		{
			Vector3 vector = base.transform.position + normalized * (num * (float)num2);
			Object.Instantiate<GameObject>(this.eventConnectionNode, vector, Quaternion.identity, this.eventConnectionNodesParent);
			num2++;
		}
	}

	// Token: 0x0600027F RID: 639 RVA: 0x0000D218 File Offset: 0x0000B418
	public static MapEvent GetNextEventInDirection(MapEvent startingEvent, Vector2 direction)
	{
		MapEvent mapEvent = null;
		float num = float.MaxValue;
		foreach (MapEvent mapEvent2 in MapEvent.mapEvents)
		{
			if (!(mapEvent2 == startingEvent) && ((Vector2.Dot(Vector2.up, direction) <= 0.5f && Vector2.Dot(Vector2.down, direction) <= 0.5f) || Mathf.Abs(mapEvent2.transform.position.x - startingEvent.transform.position.x) <= 25f) && Vector2.Dot((mapEvent2.transform.position - startingEvent.transform.position).normalized, direction) > 0.25f)
			{
				float num2 = Vector2.Distance(startingEvent.transform.position, mapEvent2.transform.position);
				if (num2 < num)
				{
					num = num2;
					mapEvent = mapEvent2;
				}
			}
		}
		return mapEvent;
	}

	// Token: 0x06000280 RID: 640 RVA: 0x0000D338 File Offset: 0x0000B538
	public void OnPointerEnter(PointerEventData eventData)
	{
		MapSelector.instance.SetSelectedObject(base.gameObject);
	}

	// Token: 0x06000281 RID: 641 RVA: 0x0000D34A File Offset: 0x0000B54A
	public void OnPointerExit(PointerEventData eventData)
	{
	}

	// Token: 0x06000282 RID: 642 RVA: 0x0000D34C File Offset: 0x0000B54C
	public void OnPointerClick(PointerEventData eventData)
	{
		Map.instance.AcceptRoom();
	}

	// Token: 0x06000283 RID: 643 RVA: 0x0000D358 File Offset: 0x0000B558
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		List<MapEvent> list = base.transform.parent.GetComponentsInChildren<MapEvent>().ToList<MapEvent>();
		list = this.GetEventsToTheRight(list);
		foreach (MapEvent mapEvent in list)
		{
			if (!(mapEvent == this) && Vector2.Distance(base.transform.position, mapEvent.transform.position) <= 75f)
			{
				Gizmos.DrawLine(base.transform.position, mapEvent.transform.position);
			}
		}
	}

	// Token: 0x040001D6 RID: 470
	public static List<MapEvent> mapEvents = new List<MapEvent>();

	// Token: 0x040001D7 RID: 471
	[SerializeField]
	private Transform eventConnectionNodesParent;

	// Token: 0x040001D8 RID: 472
	[SerializeField]
	private GameObject eventConnectionNode;

	// Token: 0x040001D9 RID: 473
	[SerializeField]
	public List<MapEvent> connectedEvents;

	// Token: 0x040001DA RID: 474
	[SerializeField]
	private Image eventIconImage;

	// Token: 0x040001DB RID: 475
	[Header("---Event Icons---")]
	[SerializeField]
	public Sprite combatIcon;

	// Token: 0x040001DC RID: 476
	[SerializeField]
	public Sprite eventIcon;

	// Token: 0x040001DD RID: 477
	[Header("---Event Type---")]
	[SerializeField]
	public GameObject eventPrefab;
}
