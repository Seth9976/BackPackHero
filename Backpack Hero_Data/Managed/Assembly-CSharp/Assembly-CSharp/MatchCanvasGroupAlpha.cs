using System;
using UnityEngine;

// Token: 0x02000070 RID: 112
public class MatchCanvasGroupAlpha : MonoBehaviour
{
	// Token: 0x06000230 RID: 560 RVA: 0x0000DB57 File Offset: 0x0000BD57
	private void Start()
	{
		this.myCanvasGroup = base.GetComponent<CanvasGroup>();
	}

	// Token: 0x06000231 RID: 561 RVA: 0x0000DB65 File Offset: 0x0000BD65
	private void Update()
	{
		if (!this.myCanvasGroup || !this.canvasGroup)
		{
			return;
		}
		this.myCanvasGroup.alpha = this.canvasGroup.alpha;
	}

	// Token: 0x04000174 RID: 372
	[SerializeField]
	private CanvasGroup canvasGroup;

	// Token: 0x04000175 RID: 373
	private CanvasGroup myCanvasGroup;
}
