using System;
using UnityEngine;

// Token: 0x02000040 RID: 64
public class FadeMaster : MonoBehaviour
{
	// Token: 0x060001D3 RID: 467 RVA: 0x00009F91 File Offset: 0x00008191
	private void OnEnable()
	{
		FadeMaster.instance = this;
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x00009F99 File Offset: 0x00008199
	private void OnDisable()
	{
		if (FadeMaster.instance == this)
		{
			FadeMaster.instance = null;
		}
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x00009FAE File Offset: 0x000081AE
	private void Start()
	{
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x00009FB0 File Offset: 0x000081B0
	private void Update()
	{
		if (this.fading)
		{
			this.canvasGroup.alpha = ((this.canvasGroup.alpha < 0.99f) ? Mathf.Lerp(this.canvasGroup.alpha, 1f, Time.deltaTime * 5f) : 1f);
			this.canvasGroup.interactable = true;
			this.canvasGroup.blocksRaycasts = true;
			return;
		}
		this.canvasGroup.alpha = ((this.canvasGroup.alpha > 0.01f) ? Mathf.Lerp(this.canvasGroup.alpha, 0f, Time.deltaTime * 5f) : 0f);
		this.canvasGroup.interactable = false;
		this.canvasGroup.blocksRaycasts = false;
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x0000A07E File Offset: 0x0000827E
	public void SetFadeInstant(bool fade)
	{
		this.fading = fade;
		this.canvasGroup.alpha = (float)(fade ? 1 : 0);
		this.canvasGroup.interactable = fade;
		this.canvasGroup.blocksRaycasts = fade;
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x0000A0B2 File Offset: 0x000082B2
	public void SetFade(bool fade)
	{
		this.fading = fade;
		this.canvasGroup.interactable = fade;
		this.canvasGroup.blocksRaycasts = fade;
	}

	// Token: 0x0400016B RID: 363
	public static FadeMaster instance;

	// Token: 0x0400016C RID: 364
	[SerializeField]
	private CanvasGroup canvasGroup;

	// Token: 0x0400016D RID: 365
	[SerializeField]
	public bool fading;
}
