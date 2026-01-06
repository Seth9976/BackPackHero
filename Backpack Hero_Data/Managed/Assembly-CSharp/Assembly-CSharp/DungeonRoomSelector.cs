using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000BE RID: 190
[ExecuteInEditMode]
public class DungeonRoomSelector : MonoBehaviour
{
	// Token: 0x06000554 RID: 1364 RVA: 0x0003445D File Offset: 0x0003265D
	private void Start()
	{
	}

	// Token: 0x06000555 RID: 1365 RVA: 0x00034460 File Offset: 0x00032660
	private void Update()
	{
		if (this.getRoomShape)
		{
			this.getRoomShape = false;
			DungeonRoomSelector[] array;
			if (Application.isPlaying)
			{
				array = Object.FindObjectsOfType<DungeonRoomSelector>();
				for (int i = 0; i < array.Length; i++)
				{
					array[i].ChooseSprite();
				}
				return;
			}
			array = Object.FindObjectsOfType<DungeonRoomSelector>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].ChooseSprite();
			}
		}
	}

	// Token: 0x06000556 RID: 1366 RVA: 0x000344BC File Offset: 0x000326BC
	public bool RoomHere(Vector2 pos, Vector2 direction)
	{
		if (!this.dungeonRoom)
		{
			this.dungeonRoom = base.GetComponent<DungeonRoom>();
			if (!this.dungeonRoom)
			{
				return false;
			}
		}
		List<GameObject> list = new List<GameObject>();
		List<DungeonRoom> list2 = new List<DungeonRoom>(DungeonRoom.allRooms);
		for (int i = 0; i < list2.Count; i++)
		{
			DungeonRoom dungeonRoom = list2[i];
			if (dungeonRoom.gameObject.scene.name != this.dungeonRoom.gameObject.scene.name)
			{
				Debug.Log("removing room because Scene A is " + dungeonRoom.gameObject.scene.name + " and Scene B is " + this.dungeonRoom.gameObject.scene.name);
				list2.RemoveAt(i);
				i--;
			}
		}
		foreach (DungeonRoom dungeonRoom2 in list2)
		{
			if (Vector2.Distance(dungeonRoom2.transform.position, pos + direction) < 0.5f)
			{
				list.Add(dungeonRoom2.gameObject);
			}
		}
		foreach (GameObject gameObject in list)
		{
			DungeonRoom component = gameObject.GetComponent<DungeonRoom>();
			if (component)
			{
				if ((direction == Vector2.right && component.leftBlocked) || (direction == Vector2.left && component.rightBlocked) || (direction == Vector2.up && component.downBlocked) || (direction == Vector2.down && component.upBlocked))
				{
					return false;
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x000346C0 File Offset: 0x000328C0
	public void ChooseSprite()
	{
		this.dungeonRoom.left = false;
		if (!this.dungeonRoom.leftBlocked)
		{
			this.dungeonRoom.left = this.RoomHere(base.transform.position, Vector2.left);
		}
		this.dungeonRoom.right = false;
		if (!this.dungeonRoom.rightBlocked)
		{
			this.dungeonRoom.right = this.RoomHere(base.transform.position, Vector2.right);
		}
		this.dungeonRoom.up = false;
		if (!this.dungeonRoom.upBlocked)
		{
			this.dungeonRoom.up = this.RoomHere(base.transform.position, Vector2.up);
		}
		this.dungeonRoom.down = false;
		if (!this.dungeonRoom.downBlocked)
		{
			this.dungeonRoom.down = this.RoomHere(base.transform.position, Vector2.down);
		}
		if (!Application.isPlaying)
		{
			DungeonPlaceholder componentInChildren = base.GetComponentInChildren<DungeonPlaceholder>();
			if (componentInChildren && componentInChildren.type == DungeonSpawner.DungeonEventSpawn.Type.connectionRoom)
			{
				if (base.transform.localPosition.x >= 2f && !this.dungeonRoom.rightBlocked)
				{
					this.dungeonRoom.right = true;
				}
				if (base.transform.localPosition.x <= 0f && !this.dungeonRoom.leftBlocked)
				{
					this.dungeonRoom.left = true;
				}
			}
		}
		bool up = this.dungeonRoom.up;
		bool down = this.dungeonRoom.down;
		bool left = this.dungeonRoom.left;
		bool right = this.dungeonRoom.right;
		int num = 0;
		if (up && down && left && right)
		{
			num = 14;
		}
		else if (up && left && right)
		{
			num = 13;
		}
		else if (up && down && left)
		{
			num = 12;
		}
		else if (down && left && right)
		{
			num = 11;
		}
		else if (up && down && right)
		{
			num = 10;
		}
		else if (left && up)
		{
			num = 9;
		}
		else if (left && down)
		{
			num = 8;
		}
		else if (right && down)
		{
			num = 7;
		}
		else if (right && up)
		{
			num = 6;
		}
		else if (right && left)
		{
			num = 5;
		}
		else if (up && down)
		{
			num = 4;
		}
		else if (left)
		{
			num = 3;
		}
		else if (right)
		{
			num = 2;
		}
		else if (down)
		{
			num = 1;
		}
		else if (up)
		{
			num = 0;
		}
		DungeonSpawner main = DungeonSpawner.main;
		if (main)
		{
			int num2 = Random.Range(0, 3);
			if (num2 == 0)
			{
				this.spriteRenderer.sprite = main.roomSprites[num];
			}
			if (num2 == 1)
			{
				this.spriteRenderer.sprite = main.roomSprites2[num];
			}
			if (num2 == 2)
			{
				this.spriteRenderer.sprite = main.roomSprites3[num];
			}
		}
	}

	// Token: 0x0400040D RID: 1037
	[SerializeField]
	private bool getRoomShape;

	// Token: 0x0400040E RID: 1038
	[SerializeField]
	private DungeonRoom dungeonRoom;

	// Token: 0x0400040F RID: 1039
	[SerializeField]
	private SpriteRenderer spriteRenderer;
}
