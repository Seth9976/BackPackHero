using System;
using UnityEngine;

// Token: 0x0200010E RID: 270
public class MapManager : MonoBehaviour
{
	// Token: 0x06000936 RID: 2358 RVA: 0x0005F3F3 File Offset: 0x0005D5F3
	private void Start()
	{
	}

	// Token: 0x06000937 RID: 2359 RVA: 0x0005F3F8 File Offset: 0x0005D5F8
	private void Update()
	{
		if (this.mapMode == MapManager.MapMode.createRoom)
		{
			if (!this.mapSelector)
			{
				this.mapSelector = Object.Instantiate<GameObject>(this.mapSelectorPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity, this.map);
				return;
			}
			Vector3 position = this.mapSelector.transform.position;
			this.mapSelector.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3 vector = new Vector3(Mathf.Round(this.mapSelector.transform.localPosition.x), Mathf.Round(this.mapSelector.transform.localPosition.y), -3f);
			this.mapSelector.transform.localPosition = new Vector3(Mathf.Clamp(this.mapSelector.transform.localPosition.x, -6f, 6f), Mathf.Clamp(this.mapSelector.transform.localPosition.y, 0f, 4f), -3f);
			this.mapSelector.transform.localPosition = new Vector3(Mathf.Round(this.mapSelector.transform.localPosition.x), Mathf.Round(this.mapSelector.transform.localPosition.y), -3f);
			if (position != vector)
			{
				Object room = DungeonRoom.GetRoom(this.mapSelector.transform.position);
				DungeonRoom room2 = DungeonRoom.GetRoom(this.mapSelector.transform.position + Vector3.left);
				DungeonRoom room3 = DungeonRoom.GetRoom(this.mapSelector.transform.position + Vector3.right);
				DungeonRoom room4 = DungeonRoom.GetRoom(this.mapSelector.transform.position + Vector3.up);
				DungeonRoom room5 = DungeonRoom.GetRoom(this.mapSelector.transform.position + Vector3.down);
				if (!room && (room2 || room3 || room4 || room5))
				{
					this.mapSelector.SetActive(true);
				}
				else
				{
					this.mapSelector.SetActive(false);
				}
			}
			if (this.mapSelector.transform.localPosition == vector && Input.GetMouseButtonDown(0))
			{
				if (DungeonRoom.GetRoom(this.mapSelector.transform.position))
				{
					return;
				}
				GameObject gameObject = Object.Instantiate<GameObject>(this.roomPrefab, this.mapSelector.transform.position, Quaternion.identity, this.map);
				gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 0f);
				DungeonRoom room6 = DungeonRoom.GetRoom(this.mapSelector.transform.position + Vector3.left);
				if (room6)
				{
					room6.rightBlocked = false;
					room6.ChooseSprite();
				}
				DungeonRoom room7 = DungeonRoom.GetRoom(this.mapSelector.transform.position + Vector3.right);
				if (room7)
				{
					room7.leftBlocked = false;
					room7.ChooseSprite();
				}
				DungeonRoom room8 = DungeonRoom.GetRoom(this.mapSelector.transform.position + Vector3.up);
				if (room8)
				{
					room8.downBlocked = false;
					room8.ChooseSprite();
				}
				DungeonRoom room9 = DungeonRoom.GetRoom(this.mapSelector.transform.position + Vector3.down);
				if (room9)
				{
					room9.upBlocked = false;
					room9.ChooseSprite();
				}
				gameObject.GetComponent<DungeonRoom>().ChooseSprite();
				Object.Destroy(this.mapSelector);
				this.mapMode = MapManager.MapMode.move;
			}
		}
	}

	// Token: 0x04000749 RID: 1865
	public MapManager.MapMode mapMode;

	// Token: 0x0400074A RID: 1866
	private GameObject mapSelector;

	// Token: 0x0400074B RID: 1867
	[SerializeField]
	private GameObject roomPrefab;

	// Token: 0x0400074C RID: 1868
	[SerializeField]
	private GameObject mapSelectorPrefab;

	// Token: 0x0400074D RID: 1869
	[SerializeField]
	private Transform map;

	// Token: 0x02000384 RID: 900
	public enum MapMode
	{
		// Token: 0x0400153D RID: 5437
		move,
		// Token: 0x0400153E RID: 5438
		createRoom
	}
}
