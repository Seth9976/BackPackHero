using System;
using UnityEngine;

// Token: 0x02000054 RID: 84
public class FadeOnDistance : MonoBehaviour
{
	// Token: 0x06000185 RID: 389 RVA: 0x00009E0F File Offset: 0x0000800F
	private void Start()
	{
		this.npc = base.GetComponentInParent<Overworld_NPC>();
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x06000186 RID: 390 RVA: 0x00009E2C File Offset: 0x0000802C
	private void Update()
	{
		if (!this.spriteRenderer)
		{
			return;
		}
		float num = Vector3.Distance(base.transform.position, Overworld_Purse.main.transform.position);
		float num2 = Mathf.Lerp(0f, 1f, (num - this.closeEnough) / (this.distanceToFadeAt - this.closeEnough));
		if (num < this.closeEnough)
		{
			num2 = 0f;
		}
		float num3 = Mathf.Lerp(1f, 0.75f, num2);
		if (!this.npc)
		{
			this.spriteRenderer.color = new Color(this.spriteRenderer.color.r, this.spriteRenderer.color.g, this.spriteRenderer.color.b, num3);
		}
		else
		{
			this.spriteRenderer.color = new Color(1f, 1f, 1f, num3) * this.npc.defaultColorForExclamation;
		}
		this.spriteRenderer.drawMode = SpriteDrawMode.Sliced;
		this.spriteRenderer.size = this.defaultSize * Vector2.Lerp(new Vector2(1f, 1f), new Vector2(0.5f, 0.5f), num2);
	}

	// Token: 0x040000FB RID: 251
	private float closeEnough = 6.5f;

	// Token: 0x040000FC RID: 252
	private float distanceToFadeAt = 12f;

	// Token: 0x040000FD RID: 253
	private SpriteRenderer spriteRenderer;

	// Token: 0x040000FE RID: 254
	private Overworld_NPC npc;

	// Token: 0x040000FF RID: 255
	private Vector2 defaultSize = new Vector2(0.4f, 0.93f);
}
