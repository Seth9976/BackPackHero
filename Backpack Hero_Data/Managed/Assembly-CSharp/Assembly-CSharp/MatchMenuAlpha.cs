using System;
using UnityEngine;

// Token: 0x0200010F RID: 271
public class MatchMenuAlpha : MonoBehaviour
{
	// Token: 0x06000939 RID: 2361 RVA: 0x0005F82B File Offset: 0x0005DA2B
	private void Start()
	{
		if (!this.canvasGroup)
		{
			this.canvasGroup = base.GetComponentInParent<CanvasGroup>();
		}
	}

	// Token: 0x0600093A RID: 2362 RVA: 0x0005F848 File Offset: 0x0005DA48
	private void LateUpdate()
	{
		base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, 0f);
		if (this.canvasGroup.alpha < 1f)
		{
			this.spriteRenderer.color = new Color(this.spriteRenderer.color.r, this.spriteRenderer.color.g, this.spriteRenderer.color.b, this.canvasGroup.alpha);
		}
	}

	// Token: 0x0400074E RID: 1870
	[SerializeField]
	private SpriteRenderer spriteRenderer;

	// Token: 0x0400074F RID: 1871
	[SerializeField]
	private CanvasGroup canvasGroup;
}
