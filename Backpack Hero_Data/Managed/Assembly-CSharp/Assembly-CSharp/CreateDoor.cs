using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200001D RID: 29
public class CreateDoor : CustomEvent
{
	// Token: 0x060000C8 RID: 200 RVA: 0x00006AEC File Offset: 0x00004CEC
	public override void InteractFromDialogue(int x)
	{
		DungeonEvent dungeonEvent = null;
		EventNPC component = base.GetComponent<EventNPC>();
		if (component)
		{
			dungeonEvent = component.dungeonEvent;
		}
		dungeonEvent.cannotWalkAwayFrom = false;
		dungeonEvent.itemsToSpawn = new List<GameObject> { this.doorPrefab };
		Vector3 vector = new Vector3(base.transform.position.x + 2.2f, this.doorPrefab.transform.position.y, this.doorPrefab.transform.position.y);
		Door componentInChildren = Object.Instantiate<GameObject>(this.doorPrefab, vector, Quaternion.identity, base.transform.parent).GetComponentInChildren<Door>();
		if (componentInChildren)
		{
			componentInChildren.SetLevel(DungeonLevel.Zone.Chaos);
		}
	}

	// Token: 0x0400006D RID: 109
	[SerializeField]
	private GameObject doorPrefab;
}
