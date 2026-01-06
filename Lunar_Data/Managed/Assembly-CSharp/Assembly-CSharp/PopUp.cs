using System;
using TMPro;
using UnityEngine;

// Token: 0x02000074 RID: 116
public class PopUp : MonoBehaviour
{
	// Token: 0x06000345 RID: 837 RVA: 0x00010AC0 File Offset: 0x0000ECC0
	private void Update()
	{
		this.timer += Time.deltaTime;
		if (this.timer > this.delay)
		{
			this.popUpBox.transform.position += Vector3.up * 10f * Time.deltaTime;
			this.canvasGroup.alpha = Mathf.Lerp(1f, 0f, (this.timer - this.delay) / this.fadeOutTime);
			if (this.canvasGroup.alpha == 0f)
			{
				Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x06000346 RID: 838 RVA: 0x00010B6C File Offset: 0x0000ED6C
	public void ShowText(string x)
	{
		this.text.text = x;
		base.gameObject.SetActive(true);
	}

	// Token: 0x0400027E RID: 638
	[SerializeField]
	private TextMeshProUGUI text;

	// Token: 0x0400027F RID: 639
	[SerializeField]
	private CanvasGroup canvasGroup;

	// Token: 0x04000280 RID: 640
	[SerializeField]
	private Transform popUpBox;

	// Token: 0x04000281 RID: 641
	private float delay = 2f;

	// Token: 0x04000282 RID: 642
	private float fadeOutTime = 1f;

	// Token: 0x04000283 RID: 643
	private float timer;
}
