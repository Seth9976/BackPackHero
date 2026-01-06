using System;
using UnityEngine;

// Token: 0x02000036 RID: 54
public class EntranceManager : MonoBehaviour
{
	// Token: 0x060001AC RID: 428 RVA: 0x00009795 File Offset: 0x00007995
	private void Start()
	{
	}

	// Token: 0x060001AD RID: 429 RVA: 0x00009798 File Offset: 0x00007998
	private void Update()
	{
		if (!this.createdStartingRoom && !RoomManager.instance.currentRoom)
		{
			CardManager.instance.isAllowedToDraw = false;
			this.startingRoom = Object.Instantiate<GameObject>(this.startingRoomPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
			RoomManager.instance.SetRoom(this.startingRoom);
			this.createdStartingRoom = true;
			return;
		}
		if (this.loadedLevel)
		{
			if (!GameCamera.instance.isMovingToANewRoom && this.startingRoom)
			{
				Object.Destroy(this.startingRoom);
			}
			return;
		}
		if (!RoomManager.instance.currentRoom)
		{
			return;
		}
		if (Player.instance && !RoomManager.instance.currentRoom.bounds.Contains(Player.instance.transform.position))
		{
			CardManager.instance.isAllowedToDraw = true;
			GameObject gameObject = Object.Instantiate<GameObject>(this.levels[Random.Range(0, this.levels.Length)], new Vector3(0f, 9f, 0f), Quaternion.identity);
			RoomManager.instance.currentRoom = gameObject.GetComponent<Room>();
			this.loadedLevel = true;
			GameCamera.instance.isMovingToANewRoom = true;
			RoomManager.instance.ForceScan();
		}
	}

	// Token: 0x0400014E RID: 334
	[SerializeField]
	public GameObject startingRoomPrefab;

	// Token: 0x0400014F RID: 335
	[SerializeField]
	public GameObject startingRoom;

	// Token: 0x04000150 RID: 336
	[SerializeField]
	private GameObject[] levels;

	// Token: 0x04000151 RID: 337
	private bool loadedLevel;

	// Token: 0x04000152 RID: 338
	[SerializeField]
	private bool createdStartingRoom;
}
