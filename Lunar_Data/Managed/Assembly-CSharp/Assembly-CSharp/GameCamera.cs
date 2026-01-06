using System;
using UnityEngine;

// Token: 0x02000044 RID: 68
public class GameCamera : MonoBehaviour
{
	// Token: 0x060001E4 RID: 484 RVA: 0x0000A3CA File Offset: 0x000085CA
	private void OnEnable()
	{
		if (GameCamera.instance == null)
		{
			GameCamera.instance = this;
		}
	}

	// Token: 0x060001E5 RID: 485 RVA: 0x0000A3DF File Offset: 0x000085DF
	private void OnDisable()
	{
		if (GameCamera.instance == this)
		{
			GameCamera.instance = null;
		}
	}

	// Token: 0x060001E6 RID: 486 RVA: 0x0000A3F4 File Offset: 0x000085F4
	private void Start()
	{
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x0000A3F8 File Offset: 0x000085F8
	private void Update()
	{
		if (!this.target)
		{
			if (Player.instance)
			{
				this.target = Player.instance.transform;
			}
			return;
		}
		if (this.isMovingToANewRoom)
		{
			this.MoveToNewRoom();
			return;
		}
		this.FollowPlayer();
	}

	// Token: 0x060001E8 RID: 488 RVA: 0x0000A444 File Offset: 0x00008644
	private void MoveToNewRoom()
	{
		Vector2 cameraPositionInBounds = this.GetCameraPositionInBounds(base.transform.position, RoomManager.instance.currentRoom);
		base.transform.position = Vector2.MoveTowards(base.transform.position, cameraPositionInBounds, 45f * Time.deltaTime);
		if (Vector2.Distance(base.transform.position, cameraPositionInBounds) < 0.1f)
		{
			this.isMovingToANewRoom = false;
		}
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, -10f);
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x0000A4FC File Offset: 0x000086FC
	public void JumpToTarget()
	{
		this.smoothing = Vector2.zero;
		base.transform.position = this.target.position;
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, -10f);
	}

	// Token: 0x060001EA RID: 490 RVA: 0x0000A560 File Offset: 0x00008760
	private void FollowPlayer()
	{
		Bounds bounds = new Bounds(base.transform.position + new Vector3(this.flexibleSize.center.x, this.flexibleSize.center.y, 0f), new Vector3(this.flexibleSize.size.x, this.flexibleSize.size.y, 99f));
		if (!bounds.Contains(this.target.transform.position))
		{
			Vector3 vector = bounds.ClosestPoint(this.target.transform.position);
			float num = vector.x - this.target.transform.position.x;
			float num2 = vector.y - this.target.transform.position.y;
			Vector2 vector2 = base.transform.position - new Vector2(num, num2);
			base.transform.position = Vector2.SmoothDamp(base.transform.position, vector2, ref this.smoothing, this.followSpeed);
		}
		base.transform.position = this.GetCameraPositionInBounds(base.transform.position, RoomManager.instance.currentRoom);
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, -10f);
	}

	// Token: 0x060001EB RID: 491 RVA: 0x0000A6F8 File Offset: 0x000088F8
	private Vector2 GetCameraPositionInBounds(Vector2 pos, Room currentRoom)
	{
		if (!currentRoom)
		{
			return pos;
		}
		float num = pos.x + Camera.main.orthographicSize * Camera.main.aspect;
		float num2 = pos.x - Camera.main.orthographicSize * Camera.main.aspect;
		float num3 = pos.y + Camera.main.orthographicSize;
		float num4 = pos.y - Camera.main.orthographicSize + this.bottomCrop;
		Bounds bounds = currentRoom.bounds;
		if (bounds.size.x < Camera.main.orthographicSize * Camera.main.aspect * 2f)
		{
			pos.x = currentRoom.transform.position.x + bounds.center.x;
		}
		else if (num > bounds.max.x)
		{
			pos.x = currentRoom.transform.position.x + bounds.max.x - Camera.main.orthographicSize * Camera.main.aspect;
		}
		else if (num2 < bounds.min.x)
		{
			pos.x = currentRoom.transform.position.x + bounds.min.x + Camera.main.orthographicSize * Camera.main.aspect;
		}
		if (bounds.size.y < Camera.main.orthographicSize * 2f + this.bottomCrop)
		{
			pos.y = currentRoom.transform.position.y + bounds.center.y - this.bottomCrop / 2f;
		}
		else if (num3 > bounds.max.y)
		{
			pos.y = currentRoom.transform.position.y + bounds.max.y - Camera.main.orthographicSize;
		}
		else if (num4 < bounds.min.y)
		{
			pos.y = currentRoom.transform.position.y + bounds.min.y + Camera.main.orthographicSize - this.bottomCrop;
		}
		return pos;
	}

	// Token: 0x060001EC RID: 492 RVA: 0x0000A943 File Offset: 0x00008B43
	public void SetTarget(Transform t)
	{
		this.target = t;
	}

	// Token: 0x060001ED RID: 493 RVA: 0x0000A94C File Offset: 0x00008B4C
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(base.transform.position + new Vector3(this.flexibleSize.center.x, this.flexibleSize.center.y, 0f), new Vector3(this.flexibleSize.size.x, this.flexibleSize.size.y, 99f));
	}

	// Token: 0x04000177 RID: 375
	public static GameCamera instance;

	// Token: 0x04000178 RID: 376
	[SerializeField]
	private Transform target;

	// Token: 0x04000179 RID: 377
	[SerializeField]
	private Bounds flexibleSize;

	// Token: 0x0400017A RID: 378
	private Vector2 smoothing;

	// Token: 0x0400017B RID: 379
	[SerializeField]
	private float followSpeed = 10f;

	// Token: 0x0400017C RID: 380
	[SerializeField]
	private float bottomCrop = 0.25f;

	// Token: 0x0400017D RID: 381
	[NonSerialized]
	public bool isMovingToANewRoom;
}
