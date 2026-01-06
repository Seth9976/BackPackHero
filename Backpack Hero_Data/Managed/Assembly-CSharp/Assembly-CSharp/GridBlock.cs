using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000D6 RID: 214
public class GridBlock : CustomInputHandler
{
	// Token: 0x06000632 RID: 1586 RVA: 0x0003CC68 File Offset: 0x0003AE68
	private void Awake()
	{
		GridBlock.allGridBlocks.Add(this);
	}

	// Token: 0x06000633 RID: 1587 RVA: 0x0003CC75 File Offset: 0x0003AE75
	private void OnDestroy()
	{
		GridBlock.allGridBlocks.Remove(this);
	}

	// Token: 0x06000634 RID: 1588 RVA: 0x0003CC83 File Offset: 0x0003AE83
	private void Start()
	{
		this.gameManager = GameManager.main;
		this.storedPosition = base.transform.localPosition;
	}

	// Token: 0x06000635 RID: 1589 RVA: 0x0003CCA4 File Offset: 0x0003AEA4
	private void Update()
	{
		if (this.isDragging)
		{
			bool flag = false;
			this.MoveToMouse(out flag);
			if ((!Input.GetMouseButton(0) && DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.cursor) || (DigitalCursor.main.GetInputDown("confirm") && this.dragTime > 0.3f))
			{
				this.StopDrag();
			}
			this.dragTime += Time.deltaTime;
			if (DigitalCursor.main.GetInputDown("cancel"))
			{
				if (this.gameManager.draggingItem == base.gameObject)
				{
					this.gameManager.draggingItem = null;
				}
				this.isMoving = true;
				this.isAnchored = false;
				this.isDragging = false;
				this.dragTime = 0f;
				return;
			}
		}
		else if (this.isMoving)
		{
			this.dragTime = 0f;
			if (Vector2.Distance(base.transform.localPosition, this.storedPosition) > 0.01f)
			{
				base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, this.storedPosition, 50f * Time.deltaTime);
				return;
			}
			this.isMoving = false;
			return;
		}
		else if (!this.isAnchored)
		{
			this.dragTime = 0f;
			this.bounceTime += Time.deltaTime / 3f;
			if (this.bounceTime > 1f)
			{
				this.bounceTime = 0f;
			}
			base.transform.localPosition = this.storedPosition + Vector3.up * Mathf.Abs(0.5f - this.bounceTime) * 0.25f;
		}
	}

	// Token: 0x06000636 RID: 1590 RVA: 0x0003CE58 File Offset: 0x0003B058
	public void MoveToMouse(out bool wasMoved)
	{
		this.difference = Vector2.ClampMagnitude(this.difference, 1.5f);
		wasMoved = false;
		Vector2 vector = DigitalCursor.main.transform.position + this.RoundPosition(this.difference);
		vector = this.LimitPosition(vector);
		vector = base.transform.parent.InverseTransformPoint(vector);
		if (DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.controller)
		{
			vector = this.RoundPosition(vector);
		}
		base.transform.localPosition = new Vector3(vector.x, vector.y, -5.5f);
	}

	// Token: 0x06000637 RID: 1591 RVA: 0x0003CF00 File Offset: 0x0003B100
	public Vector2 RoundPosition(Vector2 newPos)
	{
		base.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Round(base.transform.rotation.eulerAngles.z / 90f) * 90f);
		float num;
		float num2;
		if (base.transform.rotation.eulerAngles.z != 90f && base.transform.rotation.eulerAngles.z != -90f && base.transform.rotation.eulerAngles.z != 270f)
		{
			if (Mathf.Round(this.size.y) % 2f == 0f)
			{
				num = Mathf.Round(newPos.y - 0.5f) + 0.5f;
			}
			else
			{
				num = Mathf.Round(newPos.y);
			}
			if (Mathf.Round(this.size.x) % 2f == 0f)
			{
				num2 = Mathf.Round(newPos.x - 0.5f) + 0.5f;
			}
			else
			{
				num2 = Mathf.Round(newPos.x);
			}
		}
		else
		{
			if (Mathf.Round(this.size.x) % 2f == 0f)
			{
				num = Mathf.Round(newPos.y - 0.5f) + 0.5f;
			}
			else
			{
				num = Mathf.Round(newPos.y);
			}
			if (Mathf.Round(this.size.y) % 2f == 0f)
			{
				num2 = Mathf.Round(newPos.x - 0.5f) + 0.5f;
			}
			else
			{
				num2 = Mathf.Round(newPos.x);
			}
		}
		return new Vector2(num2, num);
	}

	// Token: 0x06000638 RID: 1592 RVA: 0x0003D0E0 File Offset: 0x0003B2E0
	private Vector2 LimitPosition(Vector2 pos)
	{
		Vector2 vector = this.gameManager.upperLeft.transform.position;
		Vector2 vector2 = this.size * 0.9f;
		if (base.transform.rotation.eulerAngles.z == 90f || base.transform.rotation.eulerAngles.z == -90f || base.transform.rotation.eulerAngles.z == 270f)
		{
			vector2 = new Vector2(vector2.y, vector2.x);
		}
		float num = Mathf.Clamp(pos.x, vector.x + vector2.x / 2f + base.transform.parent.position.x, vector.x * -1f - vector2.x / 2f + base.transform.parent.position.x);
		float num2 = Mathf.Clamp(pos.y, -0.5f + vector2.y / 2f + base.transform.parent.position.y - 7.65f, vector.y - vector2.y / 2f + base.transform.parent.position.y);
		return new Vector2(num, num2);
	}

	// Token: 0x06000639 RID: 1593 RVA: 0x0003D255 File Offset: 0x0003B455
	private void OnMouseDown()
	{
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		this.StartDrag();
	}

	// Token: 0x0600063A RID: 1594 RVA: 0x0003D26A File Offset: 0x0003B46A
	public override void OnPressStart(string keyName, bool overrideKeyName)
	{
		if (keyName == "confirm" || overrideKeyName)
		{
			this.StartDrag();
		}
	}

	// Token: 0x0600063B RID: 1595 RVA: 0x0003D284 File Offset: 0x0003B484
	public void StartDrag()
	{
		this.RebindTransforms();
		foreach (GridBlock gridBlock in Object.FindObjectsOfType<GridBlock>())
		{
			if (gridBlock.isAnchored)
			{
				gridBlock.isMoving = true;
				gridBlock.isAnchored = false;
				gridBlock.RebindTransforms();
			}
		}
		SoundManager.main.PlaySFX("pickup");
		this.gameManager.draggingItem = base.gameObject;
		Vector3 eulerAngles = base.transform.eulerAngles;
		eulerAngles.x = 0f;
		eulerAngles.y = 0f;
		eulerAngles.z = Mathf.Round(eulerAngles.z / 90f) * 90f;
		base.transform.eulerAngles = eulerAngles;
		this.startRotation = base.transform.rotation;
		this.startPosition = base.transform.position;
		if (DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.controller)
		{
			this.difference = Vector2.zero;
			Vector2 vector = this.RoundPosition(base.transform.localPosition);
			base.transform.localPosition = new Vector3(vector.x, vector.y, -5f);
			DigitalCursor.main.ClearFollow();
		}
		else
		{
			this.difference = base.transform.position - DigitalCursor.main.transform.position;
		}
		this.isDragging = true;
		this.isMoving = false;
		this.isAnchored = false;
		Object.FindObjectOfType<LevelUpManager>().ResizeAllBackpacks();
	}

	// Token: 0x0600063C RID: 1596 RVA: 0x0003D404 File Offset: 0x0003B604
	private void StopDrag()
	{
		if (this.gameManager.draggingItem == base.gameObject)
		{
			this.gameManager.draggingItem = null;
		}
		LevelUpManager levelUpManager = Object.FindObjectOfType<LevelUpManager>();
		this.isDragging = false;
		this.dragTime = 0f;
		Vector2 vector = this.RoundPosition(base.transform.localPosition);
		base.transform.localPosition = new Vector3(vector.x, vector.y, -5f);
		bool flag = false;
		foreach (Vector2 vector2 in new List<Vector2>
		{
			Vector2.up,
			Vector2.down,
			Vector2.left,
			Vector2.right
		})
		{
			BoxCollider2D[] componentsInChildren = base.GetComponentsInChildren<BoxCollider2D>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				foreach (RaycastHit2D raycastHit2D in Physics2D.RaycastAll(componentsInChildren[i].transform.position + vector2, Vector2.zero))
				{
					if (!(raycastHit2D.collider.transform.parent == base.transform) && !(raycastHit2D.collider.transform == base.transform) && raycastHit2D.collider.GetComponent<GridSquare>())
					{
						flag = true;
						break;
					}
				}
			}
		}
		if (Player.main.characterName == Character.CharacterName.Satchel)
		{
			flag = true;
		}
		if ((double)base.transform.localPosition.x < -7.9 || (double)base.transform.localPosition.x > 7.9 || base.transform.localPosition.y <= -3.9f || base.transform.localPosition.y >= 3.9f)
		{
			flag = false;
		}
		if (!flag)
		{
			this.isMoving = true;
			this.isAnchored = false;
			return;
		}
		Object.FindObjectOfType<LevelUpManager>().ChangeSpaces(-1);
		this.isAnchored = true;
		while (base.transform.childCount > 0)
		{
			Transform child = base.transform.GetChild(0);
			foreach (RaycastHit2D raycastHit2D2 in Physics2D.RaycastAll(child.position, Vector2.zero))
			{
				if (!(raycastHit2D2.collider.transform == child) && raycastHit2D2.collider.gameObject.CompareTag("GridSquare"))
				{
					child.GetComponent<SpriteRenderer>().color = Color.red;
				}
			}
			this.transforms.Add(child);
			child.SetParent(base.transform.parent);
			child.localPosition = Vector3Int.RoundToInt(child.localPosition);
			child.localPosition = new Vector3(child.localPosition.x, child.localPosition.y, -4f);
			if (child.localPosition.x < (float)((levelUpManager.maxSizeX + 1) * -1) || child.localPosition.x > (float)(levelUpManager.maxSizeX + 1) || child.localPosition.y < -2f || child.localPosition.y > 2f)
			{
				child.GetComponent<SpriteRenderer>().color = Color.red;
			}
		}
		Object.FindObjectOfType<LevelUpManager>().ResizeAllBackpacks();
	}

	// Token: 0x0600063D RID: 1597 RVA: 0x0003D7B8 File Offset: 0x0003B9B8
	public void Rotate(float angle)
	{
		if (this.gameManager && this.gameManager.draggingItem && this.gameManager.draggingItem != base.gameObject)
		{
			return;
		}
		Vector2 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		base.transform.RotateAround(vector, Vector3.forward, angle);
	}

	// Token: 0x0600063E RID: 1598 RVA: 0x0003D82C File Offset: 0x0003BA2C
	private void RebindTransforms()
	{
		if (this.transforms.Count > 0)
		{
			Object.FindObjectOfType<LevelUpManager>().ChangeSpaces(1);
		}
		foreach (Transform transform in this.transforms)
		{
			transform.SetParent(base.transform);
			transform.GetComponent<SpriteRenderer>().color = Color.white;
		}
		this.transforms = new List<Transform>();
	}

	// Token: 0x040004FF RID: 1279
	public static List<GridBlock> allGridBlocks = new List<GridBlock>();

	// Token: 0x04000500 RID: 1280
	private Vector3 startPosition;

	// Token: 0x04000501 RID: 1281
	private Quaternion startRotation;

	// Token: 0x04000502 RID: 1282
	private Vector2 difference;

	// Token: 0x04000503 RID: 1283
	private bool isDragging;

	// Token: 0x04000504 RID: 1284
	public bool isAnchored;

	// Token: 0x04000505 RID: 1285
	private bool isMoving;

	// Token: 0x04000506 RID: 1286
	private GameManager gameManager;

	// Token: 0x04000507 RID: 1287
	private float bounceTime;

	// Token: 0x04000508 RID: 1288
	private Vector3 storedPosition;

	// Token: 0x04000509 RID: 1289
	[SerializeField]
	private Vector2 size;

	// Token: 0x0400050A RID: 1290
	private float dragTime;

	// Token: 0x0400050B RID: 1291
	private List<Transform> transforms = new List<Transform>();
}
