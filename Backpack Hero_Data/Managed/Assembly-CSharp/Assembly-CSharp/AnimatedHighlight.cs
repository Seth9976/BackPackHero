using System;
using UnityEngine;

// Token: 0x020000F8 RID: 248
public class AnimatedHighlight : MonoBehaviour
{
	// Token: 0x0600089D RID: 2205 RVA: 0x0005B52C File Offset: 0x0005972C
	private void Start()
	{
		this.itemNum = this.highlightNumber;
		this.time = 0f;
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		this.spriteRenderer.sprite = this.itemHighlightSprites[this.itemNum];
	}

	// Token: 0x0600089E RID: 2206 RVA: 0x0005B56C File Offset: 0x0005976C
	private void Update()
	{
		this.time += Time.deltaTime;
		if (this.time > 0.3f)
		{
			this.time = 0f;
			this.itemNum += 16;
			if (this.itemNum >= this.itemHighlightSprites.Length)
			{
				this.itemNum = this.highlightNumber;
			}
			this.spriteRenderer.sprite = this.itemHighlightSprites[this.itemNum];
		}
	}

	// Token: 0x040006CA RID: 1738
	[SerializeField]
	private Sprite[] itemHighlightSprites;

	// Token: 0x040006CB RID: 1739
	private SpriteRenderer spriteRenderer;

	// Token: 0x040006CC RID: 1740
	public int highlightNumber;

	// Token: 0x040006CD RID: 1741
	private int itemNum;

	// Token: 0x040006CE RID: 1742
	private float time;
}
