using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200000D RID: 13
[ExecuteInEditMode]
public class CircleRenderer : MonoBehaviour
{
	// Token: 0x06000036 RID: 54 RVA: 0x00002DE7 File Offset: 0x00000FE7
	private void Start()
	{
		if (!this.col)
		{
			this.col = base.GetComponent<Collider2D>();
		}
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00002E02 File Offset: 0x00001002
	public void ShowArea()
	{
		this.DrawCircle();
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00002E0C File Offset: 0x0000100C
	public void HideCircle()
	{
		foreach (object obj in base.transform)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
		this.showingSquares = false;
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00002E70 File Offset: 0x00001070
	public void ShowCircleAgain()
	{
		if (this.showCircleCoroutine != null)
		{
			base.StopCoroutine(this.showCircleCoroutine);
		}
		if (!base.gameObject || !base.gameObject.activeInHierarchy)
		{
			return;
		}
		this.showCircleCoroutine = base.StartCoroutine(this.ShowCircleAgainCoroutine());
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00002EBE File Offset: 0x000010BE
	private IEnumerator ShowCircleAgainCoroutine()
	{
		this.HideCircle();
		yield return new WaitForSeconds(0.1f);
		yield return new WaitForFixedUpdate();
		this.DrawCircle();
		this.showCircleCoroutine = null;
		yield break;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00002ED0 File Offset: 0x000010D0
	private void Update()
	{
		base.transform.localScale = Vector3.one;
		if (base.transform.lossyScale.x < 0f)
		{
			base.transform.localScale = new Vector3(-1f, 1f, 1f);
		}
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00002F24 File Offset: 0x00001124
	private List<Vector2> SpacesWithinCollider()
	{
		if (!this.col)
		{
			return null;
		}
		List<Vector2> list = new List<Vector2>();
		Vector2 vector = Overworld_Manager.AlignToGridHalf(base.transform.position);
		float num = vector.x - 10f;
		float num2 = vector.x + 10f;
		float num3 = vector.y - 10f;
		float num4 = vector.y + 10f;
		this.col.enabled = true;
		for (float num5 = num; num5 < num2; num5 += 1f)
		{
			for (float num6 = num3; num6 < num4; num6 += 1f)
			{
				Vector2 vector2 = new Vector2(num5, num6);
				vector2 = Overworld_Manager.AlignToGridHalf(vector2);
				if (this.col.OverlapPoint(vector2) && !list.Contains(vector2))
				{
					list.Add(vector2);
				}
			}
		}
		this.col.enabled = false;
		return list;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00003010 File Offset: 0x00001210
	private void DrawCircle()
	{
		if (!Overworld_Manager.main.IsState(Overworld_Manager.State.NewBuildMode))
		{
			return;
		}
		if (this.showingSquares)
		{
			return;
		}
		if (!this.col)
		{
			return;
		}
		foreach (Vector2 vector in this.SpacesWithinCollider())
		{
			this.SetupBlock(vector);
		}
		Tiler[] componentsInChildren = base.GetComponentsInChildren<Tiler>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].SetTile();
		}
		this.showingSquares = true;
	}

	// Token: 0x0600003E RID: 62 RVA: 0x000030AC File Offset: 0x000012AC
	public int GetWaterInArea()
	{
		int num = 0;
		foreach (Vector2 vector in this.SpacesWithinCollider())
		{
			Vector3Int vector3Int = Overworld_Manager.main.tilemap.WorldToCell(Vector3Int.FloorToInt(vector));
			if (Overworld_Manager.main.tilemap.GetTile(vector3Int) == this.waterTile)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x0600003F RID: 63 RVA: 0x0000313C File Offset: 0x0000133C
	public List<Overworld_Structure> GetStructuresInArea()
	{
		this.col.enabled = true;
		List<Overworld_Structure> list = new List<Overworld_Structure>();
		BoxCollider2D component = base.GetComponent<BoxCollider2D>();
		if (component)
		{
			foreach (RaycastHit2D raycastHit2D in Physics2D.BoxCastAll(base.transform.position + component.offset, component.size, 0f, Vector2.zero))
			{
				Overworld_Structure component2 = raycastHit2D.collider.GetComponent<Overworld_Structure>();
				if (component2)
				{
					list.Add(component2);
				}
			}
			this.col.enabled = false;
			return list;
		}
		CircleCollider2D component3 = base.GetComponent<CircleCollider2D>();
		if (component3)
		{
			foreach (RaycastHit2D raycastHit2D2 in Physics2D.CircleCastAll(base.transform.position + component3.offset, component3.radius, Vector2.zero))
			{
				Overworld_Structure component4 = raycastHit2D2.collider.GetComponent<Overworld_Structure>();
				if (component4)
				{
					list.Add(component4);
				}
			}
			this.col.enabled = false;
			return list;
		}
		this.col.enabled = false;
		return list;
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00003280 File Offset: 0x00001480
	private void SetupBlock(Vector2 position)
	{
		Object.Instantiate<GameObject>(this.squarePrefab, position, Quaternion.identity).transform.SetParent(base.transform);
	}

	// Token: 0x04000017 RID: 23
	[SerializeField]
	private float radius = 1f;

	// Token: 0x04000018 RID: 24
	[SerializeField]
	private GameObject squarePrefab;

	// Token: 0x04000019 RID: 25
	[SerializeField]
	private Collider2D col;

	// Token: 0x0400001A RID: 26
	[SerializeField]
	private RuleTile waterTile;

	// Token: 0x0400001B RID: 27
	private bool showingSquares;

	// Token: 0x0400001C RID: 28
	private Coroutine showCircleCoroutine;
}
