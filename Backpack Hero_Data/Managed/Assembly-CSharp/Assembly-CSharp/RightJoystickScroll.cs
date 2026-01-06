using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000099 RID: 153
public class RightJoystickScroll : MonoBehaviour
{
	// Token: 0x0600035E RID: 862 RVA: 0x00013A70 File Offset: 0x00011C70
	private void Start()
	{
		this.scrollRect = base.GetComponent<ScrollRect>();
		this.scrollRectTransform = base.GetComponent<RectTransform>();
		this.contentSlider = this.scrollRect.content;
	}

	// Token: 0x0600035F RID: 863 RVA: 0x00013A9C File Offset: 0x00011C9C
	private void Update()
	{
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.controller)
		{
			return;
		}
		float y = DigitalCursor.main.moveFreeVector.y;
		if (Mathf.Abs(y) > 0.1f)
		{
			if (this.scrollRect.vertical)
			{
				this.contentSlider.anchoredPosition = new Vector2(0f, Mathf.Clamp(this.contentSlider.anchoredPosition.y + y * -50f, 0f, this.contentSlider.rect.height * this.contentSlider.transform.localScale.y - this.scrollRectTransform.rect.height));
				return;
			}
			this.contentSlider.anchoredPosition = new Vector2(Mathf.Clamp(this.contentSlider.anchoredPosition.x + y * -50f, 0f, this.contentSlider.rect.width * this.contentSlider.transform.localScale.x - this.scrollRectTransform.rect.width), 0f);
		}
	}

	// Token: 0x04000271 RID: 625
	private ScrollRect scrollRect;

	// Token: 0x04000272 RID: 626
	private RectTransform scrollRectTransform;

	// Token: 0x04000273 RID: 627
	private RectTransform contentSlider;
}
