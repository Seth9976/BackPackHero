using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000082 RID: 130
public class RoomManager : MonoBehaviour
{
	// Token: 0x06000388 RID: 904 RVA: 0x00011BB1 File Offset: 0x0000FDB1
	private void OnEnable()
	{
		if (RoomManager.instance == null)
		{
			RoomManager.instance = this;
		}
	}

	// Token: 0x06000389 RID: 905 RVA: 0x00011BC6 File Offset: 0x0000FDC6
	private void OnDisable()
	{
		if (RoomManager.instance == this)
		{
			RoomManager.instance = null;
		}
	}

	// Token: 0x0600038A RID: 906 RVA: 0x00011BDC File Offset: 0x0000FDDC
	private void Start()
	{
		if (!this.currentRoom)
		{
			Room room = Object.FindObjectOfType<Room>();
			if (room)
			{
				this.SetRoom(room.gameObject);
			}
		}
	}

	// Token: 0x0600038B RID: 907 RVA: 0x00011C10 File Offset: 0x0000FE10
	public void CreateChosenRoomPrefab()
	{
		if (this.currentRoom)
		{
			Object.Destroy(this.currentRoom.gameObject);
		}
		GameObject gameObject = Object.Instantiate<GameObject>(this.chosenRoomPrefab);
		this.currentRoom = gameObject.GetComponent<Room>();
		this.currentRoom.transform.SetParent(base.transform);
		this.currentRoom.transform.position = Vector3.zero;
		base.StartCoroutine(this.ScanAStar());
	}

	// Token: 0x0600038C RID: 908 RVA: 0x00011C8C File Offset: 0x0000FE8C
	public void ClearRoomContents()
	{
		foreach (object obj in this.roomContents)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
	}

	// Token: 0x0600038D RID: 909 RVA: 0x00011CE8 File Offset: 0x0000FEE8
	public void SetRoom(GameObject room)
	{
		Room component = room.GetComponent<Room>();
		if (!component)
		{
			Debug.LogError("No room component found on object");
			return;
		}
		this.currentRoom = component;
		base.StartCoroutine(this.ScanAStar());
	}

	// Token: 0x0600038E RID: 910 RVA: 0x00011D23 File Offset: 0x0000FF23
	public void ForceScan()
	{
		base.StartCoroutine(this.ScanAStar());
	}

	// Token: 0x0600038F RID: 911 RVA: 0x00011D32 File Offset: 0x0000FF32
	private IEnumerator ScanAStar()
	{
		yield return new WaitForFixedUpdate();
		if (!this.currentRoom)
		{
			Debug.LogError("No current room set");
			yield break;
		}
		this.astarPath.data.gridGraph.center = this.currentRoom.transform.position + this.currentRoom.bounds.center;
		this.astarPath.data.gridGraph.SetDimensions(Mathf.RoundToInt(this.currentRoom.bounds.size.x), Mathf.RoundToInt(this.currentRoom.bounds.size.y), 1f);
		this.astarPath.Scan(null);
		yield break;
	}

	// Token: 0x040002B2 RID: 690
	public static RoomManager instance;

	// Token: 0x040002B3 RID: 691
	[SerializeField]
	public Room currentRoom;

	// Token: 0x040002B4 RID: 692
	[SerializeField]
	public Transform roomContents;

	// Token: 0x040002B5 RID: 693
	[SerializeField]
	public List<Room> rooms = new List<Room>();

	// Token: 0x040002B6 RID: 694
	[SerializeField]
	private AstarPath astarPath;

	// Token: 0x040002B7 RID: 695
	public int level = 1;

	// Token: 0x040002B8 RID: 696
	public GameObject chosenRoomPrefab;
}
