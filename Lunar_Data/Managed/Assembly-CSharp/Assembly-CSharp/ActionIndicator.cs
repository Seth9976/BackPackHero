using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000003 RID: 3
public class ActionIndicator : MonoBehaviour
{
	// Token: 0x06000003 RID: 3 RVA: 0x00002077 File Offset: 0x00000277
	private void Start()
	{
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00002079 File Offset: 0x00000279
	private void Update()
	{
	}

	// Token: 0x06000005 RID: 5 RVA: 0x0000207C File Offset: 0x0000027C
	public void Show(bool show)
	{
		if (show)
		{
			if (this.spriteRenderer)
			{
				this.spriteRenderer.enabled = true;
			}
			if (this.lineRenderer)
			{
				this.lineRenderer.enabled = true;
				return;
			}
		}
		else
		{
			if (this.spriteRenderer)
			{
				this.spriteRenderer.enabled = false;
			}
			if (this.lineRenderer)
			{
				this.lineRenderer.enabled = false;
			}
		}
	}

	// Token: 0x06000006 RID: 6 RVA: 0x000020F1 File Offset: 0x000002F1
	public void SetSortingOrder(int num)
	{
		if (this.spriteRenderer)
		{
			this.spriteRenderer.sortingOrder = num;
		}
		if (this.lineRenderer)
		{
			this.lineRenderer.sortingOrder = num;
		}
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00002128 File Offset: 0x00000328
	private Vector3 LRPLZ(Vector2 vec, LineRenderer line)
	{
		float num;
		if (!RoomManager.instance.currentRoom)
		{
			num = line.transform.position.z;
		}
		else
		{
			Bounds bounds = RoomManager.instance.currentRoom.bounds;
			num = Mathf.Lerp(this.zPositionValues.x, this.zPositionValues.y, (vec.y - bounds.min.y) / bounds.size.y);
		}
		return new Vector3(vec.x, vec.y, num);
	}

	// Token: 0x06000008 RID: 8 RVA: 0x000021C0 File Offset: 0x000003C0
	public Vector3[] GetPositions()
	{
		Vector3[] array = new Vector3[this.lineRenderer.positionCount];
		this.lineRenderer.GetPositions(array);
		return array;
	}

	// Token: 0x06000009 RID: 9 RVA: 0x000021EC File Offset: 0x000003EC
	public void ClearLine()
	{
		this.lineRenderer.positionCount = 0;
	}

	// Token: 0x0600000A RID: 10 RVA: 0x000021FA File Offset: 0x000003FA
	public void ShowAtPosition(Vector2 pos)
	{
		base.transform.position = pos;
		this.Show(true);
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00002214 File Offset: 0x00000414
	public void DrawLine(List<Vector2> vecs)
	{
		this.lineRenderer.positionCount = vecs.Count;
		for (int i = 0; i < vecs.Count; i++)
		{
			this.lineRenderer.SetPosition(i, this.LRPLZ(vecs[i], this.lineRenderer));
		}
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00002262 File Offset: 0x00000462
	public void DrawLine(Vector2 start, Vector2 end)
	{
		this.lineRenderer.positionCount = 2;
		this.lineRenderer.SetPosition(0, this.LRPLZ(start, this.lineRenderer));
		this.lineRenderer.SetPosition(1, this.LRPLZ(end, this.lineRenderer));
	}

	// Token: 0x0600000D RID: 13 RVA: 0x000022A4 File Offset: 0x000004A4
	public void DrawArc(Vector2 posOfAim, float arcLength)
	{
		Vector2 vector = Player.instance.transform.position;
		Vector2 vector2 = posOfAim - vector;
		float num = Mathf.Atan2(vector2.y, vector2.x) * 57.29578f;
		float magnitude = vector2.magnitude;
		float num2 = num - arcLength / 2f;
		float num3 = arcLength / 2f;
		float num4 = 10f;
		float num5 = arcLength / num4;
		this.lineRenderer.positionCount = (int)num4 + 1;
		int num6 = 0;
		while ((float)num6 < num4 + 1f)
		{
			float num7 = num2 + num5 * (float)num6;
			Vector2 vector3 = new Vector2(Mathf.Cos(num7 * 0.017453292f), Mathf.Sin(num7 * 0.017453292f));
			Vector2 vector4 = vector + vector3 * magnitude;
			this.lineRenderer.SetPosition(num6, vector4);
			num6++;
		}
	}

	// Token: 0x0600000E RID: 14 RVA: 0x00002384 File Offset: 0x00000584
	public void DrawCircle(Vector2 center, float radius)
	{
		int num = 100;
		this.lineRenderer.positionCount = num + 1;
		for (int i = 0; i <= num; i++)
		{
			float num2 = (float)i / (float)num;
			float num3 = Mathf.Lerp(0f, 360f, num2);
			Vector2 vector = center + new Vector2(Mathf.Cos(num3 * 0.017453292f), Mathf.Sin(num3 * 0.017453292f)) * radius;
			this.lineRenderer.SetPosition(i, this.LRPLZ(vector, this.lineRenderer));
		}
	}

	// Token: 0x04000003 RID: 3
	[SerializeField]
	private SpriteRenderer spriteRenderer;

	// Token: 0x04000004 RID: 4
	[SerializeField]
	private LineRenderer lineRenderer;

	// Token: 0x04000005 RID: 5
	[SerializeField]
	private Vector2 zPositionValues = new Vector2(0f, 0f);
}
