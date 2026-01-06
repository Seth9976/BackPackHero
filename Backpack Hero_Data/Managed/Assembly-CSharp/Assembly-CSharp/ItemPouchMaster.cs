using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000061 RID: 97
public class ItemPouchMaster : MonoBehaviour
{
	// Token: 0x060001CB RID: 459 RVA: 0x0000B753 File Offset: 0x00009953
	private void OnEnable()
	{
		ItemPouchMaster.itemPouchMasters.Add(this);
	}

	// Token: 0x060001CC RID: 460 RVA: 0x0000B760 File Offset: 0x00009960
	private void OnDisable()
	{
		this.itemPouch.isHoveringOver = false;
		ItemPouchMaster.itemPouchMasters.Remove(this);
	}

	// Token: 0x060001CD RID: 461 RVA: 0x0000B77C File Offset: 0x0000997C
	private void Start()
	{
		if (this.spriteRenderer && this.itemPouch && this.itemPouch.backgroundSprite)
		{
			this.spriteRenderer.sprite = this.itemPouch.backgroundSprite;
		}
	}

	// Token: 0x060001CE RID: 462 RVA: 0x0000B7CC File Offset: 0x000099CC
	private void Update()
	{
		this.bounds = new Bounds(this.spriteRenderer.transform.position, new Vector3(this.spriteRenderer.size.x * 0.9f, this.spriteRenderer.size.y * 0.9f, 1000f));
		if (this.IsBlocked(DigitalCursor.main.transform.position))
		{
			this.itemPouch.isHoveringOver = true;
			return;
		}
		this.itemPouch.isHoveringOver = false;
	}

	// Token: 0x060001CF RID: 463 RVA: 0x0000B85F File Offset: 0x00009A5F
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(this.bounds.center, this.bounds.size);
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x0000B886 File Offset: 0x00009A86
	public void SetPouch(ItemPouch itemPouch)
	{
		this.itemPouch = itemPouch;
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x0000B890 File Offset: 0x00009A90
	public static bool IsBlocked(Vector2 position, out float zPosition)
	{
		zPosition = 999f;
		for (int i = 0; i < ItemPouchMaster.itemPouchMasters.Count; i++)
		{
			ItemPouchMaster itemPouchMaster = ItemPouchMaster.itemPouchMasters[i];
			if (!itemPouchMaster)
			{
				ItemPouchMaster.itemPouchMasters.RemoveAt(i);
				i--;
			}
			else if (itemPouchMaster.bounds.Contains(position) && itemPouchMaster.transform.position.z < zPosition)
			{
				zPosition = itemPouchMaster.transform.position.z;
			}
		}
		return zPosition < 999f;
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x0000B923 File Offset: 0x00009B23
	public bool IsBlocked(Vector2 position)
	{
		return this.bounds.Contains(position);
	}

	// Token: 0x0400013D RID: 317
	private static List<ItemPouchMaster> itemPouchMasters = new List<ItemPouchMaster>();

	// Token: 0x0400013E RID: 318
	[SerializeField]
	private SpriteRenderer spriteRenderer;

	// Token: 0x0400013F RID: 319
	[SerializeField]
	private Bounds bounds;

	// Token: 0x04000140 RID: 320
	private ItemPouch itemPouch;
}
