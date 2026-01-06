using System;
using UnityEngine;

// Token: 0x0200006F RID: 111
public class PlayerBlur : MonoBehaviour
{
	// Token: 0x06000331 RID: 817 RVA: 0x00010174 File Offset: 0x0000E374
	public void StartWithAlpha(float alpha)
	{
		Color color = this.spriteRenderer.color;
		color.a = alpha;
		this.spriteRenderer.color = color;
		this.spriteRenderer.sprite = Player.instance.GetSprite();
	}

	// Token: 0x06000332 RID: 818 RVA: 0x000101B8 File Offset: 0x0000E3B8
	private void Update()
	{
		float num = 1f;
		if (this.timeType == PlayerBlur.TimeType.TimeManagerScaled)
		{
			num = TimeManager.instance.currentTimeScale;
		}
		this.spriteRenderer.color = Color.Lerp(this.spriteRenderer.color, Color.clear, Time.deltaTime * num * this.timeScale);
		if (this.spriteRenderer.color.a <= 0.05f)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000268 RID: 616
	[SerializeField]
	private SpriteRenderer spriteRenderer;

	// Token: 0x04000269 RID: 617
	[SerializeField]
	private PlayerBlur.TimeType timeType;

	// Token: 0x0400026A RID: 618
	[SerializeField]
	private float timeScale = 5f;

	// Token: 0x020000F9 RID: 249
	[SerializeField]
	private enum TimeType
	{
		// Token: 0x0400049D RID: 1181
		TimeManagerScaled,
		// Token: 0x0400049E RID: 1182
		Unscaled
	}
}
