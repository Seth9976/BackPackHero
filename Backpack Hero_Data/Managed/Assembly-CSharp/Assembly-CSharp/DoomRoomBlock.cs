using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000BA RID: 186
public class DoomRoomBlock : MonoBehaviour
{
	// Token: 0x06000519 RID: 1305 RVA: 0x00031FA0 File Offset: 0x000301A0
	private void Start()
	{
		DungeonSpawner dungeonSpawner = Object.FindObjectOfType<DungeonSpawner>();
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.CompareTag("AttachmentRoom"))
			{
				GameObject gameObject = Object.Instantiate<GameObject>(dungeonSpawner.dungeonRoomAttachmentPrefabs[Random.Range(0, dungeonSpawner.dungeonRoomAttachmentPrefabs.Length)], transform.position, Quaternion.identity, base.transform);
				Object.Destroy(transform.gameObject);
				while (gameObject.transform.childCount > 0)
				{
					gameObject.transform.GetChild(0).SetParent(base.transform);
				}
				Object.Destroy(gameObject);
			}
		}
		if (Random.Range(0, 2) == 1)
		{
			foreach (object obj2 in base.transform)
			{
				Transform transform2 = (Transform)obj2;
				transform2.localPosition = new Vector3(transform2.localPosition.x, 4f - transform2.localPosition.y, transform2.localPosition.z);
				DungeonRoom component = transform2.GetComponent<DungeonRoom>();
				if (component)
				{
					bool flag = false;
					bool flag2 = false;
					if (component.upBlocked)
					{
						component.upBlocked = false;
						flag = true;
					}
					if (component.downBlocked)
					{
						component.downBlocked = false;
						flag2 = true;
					}
					if (flag2)
					{
						component.upBlocked = true;
					}
					if (flag)
					{
						component.downBlocked = true;
					}
				}
			}
		}
		if (Random.Range(0, 2) == 1)
		{
			foreach (object obj3 in base.transform)
			{
				Transform transform3 = (Transform)obj3;
				transform3.localPosition = new Vector3(2f - transform3.localPosition.x, transform3.localPosition.y, transform3.localPosition.z);
				DungeonRoom component2 = transform3.GetComponent<DungeonRoom>();
				if (component2)
				{
					bool flag3 = false;
					bool flag4 = false;
					if (component2.rightBlocked)
					{
						component2.rightBlocked = false;
						flag3 = true;
					}
					if (component2.leftBlocked)
					{
						component2.leftBlocked = false;
						flag4 = true;
					}
					if (flag4)
					{
						component2.rightBlocked = true;
					}
					if (flag3)
					{
						component2.leftBlocked = true;
					}
				}
			}
		}
	}

	// Token: 0x0600051A RID: 1306 RVA: 0x0003223C File Offset: 0x0003043C
	public Transform GetBlockOfType(DungeonSpawner.DungeonEventSpawn.Type type)
	{
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			DungeonPlaceholder componentInChildren = transform.GetComponentInChildren<DungeonPlaceholder>();
			if (componentInChildren && componentInChildren.type == type)
			{
				return transform;
			}
		}
		return null;
	}

	// Token: 0x0600051B RID: 1307 RVA: 0x000322B0 File Offset: 0x000304B0
	public Transform GetRightBlock(DungeonSpawner.DungeonEventSpawn.Type type)
	{
		float num = -999f;
		List<Transform> list = new List<Transform>();
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			DungeonPlaceholder componentInChildren = transform.GetComponentInChildren<DungeonPlaceholder>();
			if (transform.localPosition.x > num && componentInChildren && componentInChildren.type == type)
			{
				num = transform.localPosition.x;
			}
		}
		foreach (object obj2 in base.transform)
		{
			Transform transform2 = (Transform)obj2;
			DungeonPlaceholder componentInChildren2 = transform2.GetComponentInChildren<DungeonPlaceholder>();
			if (transform2.localPosition.x >= num && componentInChildren2 && componentInChildren2.type == type)
			{
				transform2.GetComponent<DungeonRoom>().rightBlocked = false;
				componentInChildren2.type = DungeonSpawner.DungeonEventSpawn.Type.mandatoryFight;
				list.Add(transform2);
			}
		}
		return list[Random.Range(0, list.Count)];
	}

	// Token: 0x0600051C RID: 1308 RVA: 0x000323E8 File Offset: 0x000305E8
	public Transform GetLeftBlock(DungeonSpawner.DungeonEventSpawn.Type type)
	{
		float num = 999f;
		List<Transform> list = new List<Transform>();
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			DungeonPlaceholder componentInChildren = transform.GetComponentInChildren<DungeonPlaceholder>();
			if (transform.localPosition.x < num && componentInChildren && componentInChildren.type == type)
			{
				num = transform.localPosition.x;
			}
		}
		foreach (object obj2 in base.transform)
		{
			Transform transform2 = (Transform)obj2;
			DungeonPlaceholder componentInChildren2 = transform2.GetComponentInChildren<DungeonPlaceholder>();
			if (transform2.localPosition.x <= num && componentInChildren2 && componentInChildren2.type == type)
			{
				transform2.GetComponent<DungeonRoom>().leftBlocked = false;
				componentInChildren2.type = DungeonSpawner.DungeonEventSpawn.Type.mandatoryFight;
				list.Add(transform2);
			}
		}
		return list[Random.Range(0, list.Count)];
	}

	// Token: 0x0600051D RID: 1309 RVA: 0x00032520 File Offset: 0x00030720
	private void Update()
	{
	}

	// Token: 0x0600051E RID: 1310 RVA: 0x00032524 File Offset: 0x00030724
	public void ConnectRooms(Vector2 start, Vector2 end)
	{
		int num = 0;
		if (start.y < end.y)
		{
			num = 1;
		}
		if (start.y > end.y)
		{
			num = -1;
		}
		GameObject gameObject = Object.Instantiate<GameObject>(this.roomPrefab, Vector3.zero, base.transform.rotation, base.transform);
		DungeonRoom dungeonRoom = gameObject.GetComponent<DungeonRoom>();
		if (dungeonRoom)
		{
			dungeonRoom.leftBlocked = false;
		}
		gameObject.transform.localPosition = new Vector3(start.x + 1f, start.y, 0f);
		int num2 = Mathf.RoundToInt(start.y) + num;
		while (num2 - num != Mathf.RoundToInt(end.y))
		{
			bool flag = true;
			using (IEnumerator enumerator = base.transform.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((Transform)enumerator.Current).localPosition == new Vector3(start.x + 1f, (float)num2, 0f))
					{
						flag = false;
					}
				}
			}
			if (flag)
			{
				gameObject = Object.Instantiate<GameObject>(this.roomPrefab, Vector3.zero, base.transform.rotation, base.transform);
				gameObject.transform.localPosition = new Vector3(start.x + 1f, (float)num2, 0f);
			}
			num2 += num;
		}
		dungeonRoom = gameObject.GetComponent<DungeonRoom>();
		if (dungeonRoom)
		{
			dungeonRoom.rightBlocked = false;
		}
	}

	// Token: 0x040003D8 RID: 984
	[SerializeField]
	public GameObject roomPrefab;
}
