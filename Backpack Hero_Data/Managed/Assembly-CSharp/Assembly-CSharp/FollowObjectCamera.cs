using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Tilemaps;

// Token: 0x0200000F RID: 15
[ExecuteInEditMode]
public class FollowObjectCamera : MonoBehaviour
{
	// Token: 0x06000045 RID: 69 RVA: 0x000032E8 File Offset: 0x000014E8
	private void Start()
	{
	}

	// Token: 0x06000046 RID: 70 RVA: 0x000032EA File Offset: 0x000014EA
	private void Update()
	{
		if (Application.IsPlaying(base.gameObject) || this.followInEditMode)
		{
			if (!this.followObject)
			{
				this.followObject = GameObject.FindGameObjectWithTag(this.tagName);
				return;
			}
			this.RunCamera();
		}
	}

	// Token: 0x06000047 RID: 71 RVA: 0x00003326 File Offset: 0x00001526
	public void FollowObject(Transform t)
	{
		if (!t || !t.gameObject)
		{
			return;
		}
		this.followObject = t.gameObject;
	}

	// Token: 0x06000048 RID: 72 RVA: 0x0000334A File Offset: 0x0000154A
	public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
	{
		return Quaternion.Euler(angles) * (point - pivot) + pivot;
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00003364 File Offset: 0x00001564
	private void OnEnable()
	{
		if (this.coroutine != null)
		{
			base.StopCoroutine(this.coroutine);
		}
		this.coroutine = base.StartCoroutine(this.ScaleCamera());
	}

	// Token: 0x0600004A RID: 74 RVA: 0x0000338C File Offset: 0x0000158C
	private IEnumerator ScaleCamera()
	{
		if (!this.pixelPerfectCamera)
		{
			yield break;
		}
		float startTime = 0f;
		while (startTime < 1f)
		{
			startTime += Time.deltaTime;
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600004B RID: 75 RVA: 0x0000339C File Offset: 0x0000159C
	private void RunCamera()
	{
		if (this.cameraState == FollowObjectCamera.CameraState.Follow)
		{
			Vector2 cameraPositionOnObject = this.GetCameraPositionOnObject(this.followObject.transform);
			this.SetPosition(cameraPositionOnObject);
		}
	}

	// Token: 0x0600004C RID: 76 RVA: 0x000033CA File Offset: 0x000015CA
	private void SetPosition(Vector2 pos)
	{
		this.cameraTransform.position = new Vector3(pos.x, pos.y, base.transform.position.z);
	}

	// Token: 0x0600004D RID: 77 RVA: 0x000033F8 File Offset: 0x000015F8
	private Vector2 GetCameraPositionOnObject(Transform objectToFollow)
	{
		Vector2 vector = base.transform.position;
		Bounds bounds = new Bounds(base.transform.position, new Vector3(this.flexibleSize.x, this.flexibleSize.y, this.flexibleSize.z));
		if (!bounds.Contains(objectToFollow.transform.position) && (!this.ignoreOnUI || (DigitalCursor.main && !DigitalCursor.main.OverUI())))
		{
			Vector3 vector2 = bounds.ClosestPoint(objectToFollow.transform.position);
			float num = vector2.x - objectToFollow.transform.position.x;
			float num2 = vector2.y - objectToFollow.transform.position.y;
			Vector3 position = objectToFollow.transform.position;
			Vector2 vector3 = base.transform.position - new Vector2(num, num2);
			vector = Vector2.SmoothDamp(vector, vector3, ref this.smoothing, this.followSpeed);
		}
		float num3 = vector.x + Camera.main.orthographicSize * Camera.main.aspect;
		float num4 = vector.x - Camera.main.orthographicSize * Camera.main.aspect;
		float num5 = vector.y + Camera.main.orthographicSize;
		float num6 = vector.y - Camera.main.orthographicSize;
		Vector2 vector4 = new Vector2(this.myMap.cellBounds.center.x, this.myMap.cellBounds.center.y);
		this.myMap.CellToWorld(new Vector3Int((int)vector4.x, (int)vector4.y, 0));
		new Vector2((float)this.myMap.cellBounds.size.x * this.myMap.cellSize.x, (float)this.myMap.cellBounds.size.y * this.myMap.cellSize.y);
		if (this.anchorPoint)
		{
			if (this.anchorPoint.position.x > num3 - 2f)
			{
				vector = new Vector2(this.anchorPoint.position.x + 2f - Camera.main.orthographicSize * Camera.main.aspect, vector.y);
			}
			if (this.anchorPoint.position.x < num4 + 2f)
			{
				vector = new Vector2(this.anchorPoint.position.x - 2f + Camera.main.orthographicSize * Camera.main.aspect, vector.y);
			}
			if (this.anchorPoint.position.y > num5 - 2f)
			{
				vector = new Vector2(vector.x, this.anchorPoint.position.y + 2f - Camera.main.orthographicSize);
			}
			if (this.anchorPoint.position.y < num6 + 2f)
			{
				vector = new Vector2(vector.x, this.anchorPoint.position.y - 2f + Camera.main.orthographicSize);
			}
		}
		Vector4 limitsOfTileMap = this.GetLimitsOfTileMap();
		float num7 = Camera.main.orthographicSize * Camera.main.aspect;
		float orthographicSize = Camera.main.orthographicSize;
		if (vector.x - num7 < limitsOfTileMap.x)
		{
			vector = new Vector2(limitsOfTileMap.x + num7, vector.y);
		}
		if (vector.x + num7 > limitsOfTileMap.y)
		{
			vector = new Vector2(limitsOfTileMap.y - num7, vector.y);
		}
		if (vector.y - orthographicSize < limitsOfTileMap.z)
		{
			vector = new Vector2(vector.x, limitsOfTileMap.z + orthographicSize);
		}
		if (vector.y + orthographicSize > limitsOfTileMap.w)
		{
			vector = new Vector2(vector.x, limitsOfTileMap.w - orthographicSize);
		}
		return vector;
	}

	// Token: 0x0600004E RID: 78 RVA: 0x0000384C File Offset: 0x00001A4C
	public Vector4 GetLimitsOfTileMap()
	{
		Vector2 vector = new Vector2(this.myMap.cellBounds.center.x, this.myMap.cellBounds.center.y);
		Vector2 vector2 = this.myMap.CellToWorld(new Vector3Int((int)vector.x, (int)vector.y, 0));
		vector2 += this.additionalOffsetCenter;
		Vector2 vector3 = new Vector2((float)this.myMap.cellBounds.size.x * this.myMap.cellSize.x, (float)this.myMap.cellBounds.size.y * this.myMap.cellSize.y);
		vector3 += this.additionalOffset;
		return new Vector4(vector2.x - vector3.x / 2f - 0.5f, vector2.x + vector3.x / 2f + 0.5f, vector2.y - vector3.y / 2f - 0.5f, vector2.y + vector3.y / 2f + 0.5f);
	}

	// Token: 0x0600004F RID: 79 RVA: 0x0000399C File Offset: 0x00001B9C
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube(this.cameraTransform.position, new Vector3(this.flexibleSize.x, this.flexibleSize.y, this.flexibleSize.z));
		if (this.myMap)
		{
			Vector4 limitsOfTileMap = this.GetLimitsOfTileMap();
			Gizmos.color = Color.red;
			Gizmos.DrawWireCube(new Vector3(limitsOfTileMap.x + (limitsOfTileMap.y - limitsOfTileMap.x) / 2f, limitsOfTileMap.z + (limitsOfTileMap.w - limitsOfTileMap.z) / 2f, 0f), new Vector3(limitsOfTileMap.y - limitsOfTileMap.x, limitsOfTileMap.w - limitsOfTileMap.z, 0f));
		}
	}

	// Token: 0x0400001E RID: 30
	[SerializeField]
	private bool ignoreOnUI;

	// Token: 0x0400001F RID: 31
	[SerializeField]
	private Transform rightBlock;

	// Token: 0x04000020 RID: 32
	[SerializeField]
	private Transform cameraTransform;

	// Token: 0x04000021 RID: 33
	[SerializeField]
	private float followSpeed = 5f;

	// Token: 0x04000022 RID: 34
	public FollowObjectCamera.CameraState cameraState;

	// Token: 0x04000023 RID: 35
	[SerializeField]
	private Vector3 flexibleSize;

	// Token: 0x04000024 RID: 36
	public Transform anchorPoint;

	// Token: 0x04000025 RID: 37
	public GameObject followObject;

	// Token: 0x04000026 RID: 38
	[SerializeField]
	private Tilemap myMap;

	// Token: 0x04000027 RID: 39
	[SerializeField]
	private bool followInEditMode;

	// Token: 0x04000028 RID: 40
	[SerializeField]
	private bool getBoxPosition;

	// Token: 0x04000029 RID: 41
	[SerializeField]
	private PixelPerfectCamera pixelPerfectCamera;

	// Token: 0x0400002A RID: 42
	private Vector2 smoothing;

	// Token: 0x0400002B RID: 43
	[SerializeField]
	private string tagName;

	// Token: 0x0400002C RID: 44
	[SerializeField]
	private Vector2 additionalOffset;

	// Token: 0x0400002D RID: 45
	[SerializeField]
	private Vector2 additionalOffsetCenter;

	// Token: 0x0400002E RID: 46
	private Coroutine coroutine;

	// Token: 0x0200023D RID: 573
	public enum CameraState
	{
		// Token: 0x04000E9C RID: 3740
		Follow
	}
}
