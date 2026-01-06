using System;
using UnityEngine;

// Token: 0x02000114 RID: 276
public class PopUp : MonoBehaviour
{
	// Token: 0x0600096D RID: 2413 RVA: 0x00060E00 File Offset: 0x0005F000
	private void Start()
	{
		this.canvas = base.GetComponentInParent<Canvas>();
		this.rect = base.transform.GetComponent<RectTransform>();
		this.parentRect = base.transform.parent.GetComponent<RectTransform>();
		this.canvasRect = this.canvas.GetComponent<RectTransform>();
	}

	// Token: 0x0600096E RID: 2414 RVA: 0x00060E51 File Offset: 0x0005F051
	private void Update()
	{
		this.KeepFullyOnScreen();
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x00060E5C File Offset: 0x0005F05C
	private void KeepFullyOnScreen()
	{
		float num = (this.canvasRect.sizeDelta.x - this.rect.sizeDelta.x) * -0.5f;
		float num2 = (this.canvasRect.sizeDelta.x - this.rect.sizeDelta.x) * 0.5f;
		float num3 = (this.canvasRect.sizeDelta.y - this.rect.sizeDelta.y) * -0.5f;
		float num4 = (this.canvasRect.sizeDelta.y - this.rect.sizeDelta.y) * 0.5f;
		Vector3 vector = this.parentRect.anchoredPosition;
		vector.x = Mathf.Clamp(vector.x, num, num2);
		vector.y = Mathf.Clamp(vector.y, num3, num4);
		this.parentRect.anchoredPosition = vector;
	}

	// Token: 0x0400077D RID: 1917
	private Canvas canvas;

	// Token: 0x0400077E RID: 1918
	private RectTransform canvasRect;

	// Token: 0x0400077F RID: 1919
	private RectTransform rect;

	// Token: 0x04000780 RID: 1920
	private RectTransform parentRect;
}
