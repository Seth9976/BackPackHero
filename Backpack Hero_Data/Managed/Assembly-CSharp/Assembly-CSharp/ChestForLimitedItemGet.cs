using System;
using UnityEngine;

// Token: 0x020000C4 RID: 196
public class ChestForLimitedItemGet : MonoBehaviour
{
	// Token: 0x06000594 RID: 1428 RVA: 0x00036E91 File Offset: 0x00035091
	private void Start()
	{
		this.offsetY = this.chestTop.GetComponent<SpriteRenderer>().size.y + 0.5f;
	}

	// Token: 0x06000595 RID: 1429 RVA: 0x00036EB4 File Offset: 0x000350B4
	private void Update()
	{
		if (this.chestState == ChestForLimitedItemGet.ChestState.open)
		{
			this.chestTop.localPosition = Vector3.MoveTowards(this.chestTop.localPosition, new Vector3(this.chestTop.localPosition.x, this.offsetY * -1f, this.chestTop.localPosition.z), this.speedOfOpening * Time.deltaTime);
			return;
		}
		this.chestTop.localPosition = Vector3.MoveTowards(this.chestTop.localPosition, new Vector3(this.chestTop.localPosition.x, 0f, this.chestTop.localPosition.z), this.speedOfOpening * Time.deltaTime);
	}

	// Token: 0x0400044A RID: 1098
	[SerializeField]
	private Transform chest;

	// Token: 0x0400044B RID: 1099
	[SerializeField]
	private Transform chestTop;

	// Token: 0x0400044C RID: 1100
	public ChestForLimitedItemGet.ChestState chestState;

	// Token: 0x0400044D RID: 1101
	[SerializeField]
	private float speedOfOpening;

	// Token: 0x0400044E RID: 1102
	private float offsetY;

	// Token: 0x020002FB RID: 763
	public enum ChestState
	{
		// Token: 0x040011D2 RID: 4562
		open,
		// Token: 0x040011D3 RID: 4563
		close
	}
}
