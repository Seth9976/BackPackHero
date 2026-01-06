using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000192 RID: 402
public class ScrollRectController : MonoBehaviour
{
	// Token: 0x06001029 RID: 4137 RVA: 0x0009BC68 File Offset: 0x00099E68
	private void Start()
	{
		this.scrollRect = base.GetComponent<ScrollRect>();
		if (!this.scrollRect)
		{
			Object.Destroy(this);
			return;
		}
		this.scrollRectTransform = base.GetComponent<RectTransform>();
		this.selectableItems = base.GetComponentsInChildren<Selectable>();
		this.contentSlider = this.scrollRect.content;
	}

	// Token: 0x0600102A RID: 4138 RVA: 0x0009BCC0 File Offset: 0x00099EC0
	private void Update()
	{
		this.ConsiderMouseWheel();
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.controller)
		{
			return;
		}
		if (!EventSystem.current || !EventSystem.current.currentSelectedGameObject)
		{
			return;
		}
		if (EventSystem.current.currentSelectedGameObject.transform.IsChildOf(base.transform))
		{
			foreach (object obj in this.contentSlider)
			{
				Transform transform = (Transform)obj;
				if (EventSystem.current.currentSelectedGameObject.transform.IsChildOf(transform) || EventSystem.current.currentSelectedGameObject == transform.gameObject)
				{
					this.CenterOnElement(transform.GetComponent<RectTransform>());
				}
			}
		}
	}

	// Token: 0x0600102B RID: 4139 RVA: 0x0009BD9C File Offset: 0x00099F9C
	private bool IfOverElement(Vector2 position, RectTransform rectTransform)
	{
		return rectTransform.rect.Contains(rectTransform.InverseTransformPoint(position));
	}

	// Token: 0x0600102C RID: 4140 RVA: 0x0009BDC8 File Offset: 0x00099FC8
	private void ConsiderMouseWheel()
	{
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		if (!this.IfOverElement(DigitalCursor.main.transform.position, this.scrollRectTransform))
		{
			return;
		}
		if (Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")) > 0f)
		{
			if (this.scrollRect.vertical && this.contentSlider.rect.height * this.contentSlider.transform.localScale.y < this.scrollRectTransform.rect.height)
			{
				return;
			}
			if (this.scrollRect.vertical)
			{
				this.contentSlider.anchoredPosition = new Vector2(0f, Mathf.Clamp(this.contentSlider.anchoredPosition.y + Input.GetAxis("Mouse ScrollWheel") * -1000f, 0f, this.contentSlider.rect.height * this.contentSlider.transform.localScale.y - this.scrollRectTransform.rect.height));
				return;
			}
			this.contentSlider.anchoredPosition = new Vector2(Mathf.Clamp(this.contentSlider.anchoredPosition.x + Input.GetAxis("Mouse ScrollWheel") * -1000f, 0f, this.contentSlider.rect.width * this.contentSlider.transform.localScale.x - this.scrollRectTransform.rect.width), 0f);
		}
	}

	// Token: 0x0600102D RID: 4141 RVA: 0x0009BF74 File Offset: 0x0009A174
	private void CenterOnElement(RectTransform rectTransform)
	{
		if (this.scrollRect.vertical && this.contentSlider.rect.height * this.contentSlider.transform.localScale.y < this.scrollRectTransform.rect.height)
		{
			return;
		}
		if (this.scrollRect.vertical)
		{
			this.contentSlider.anchoredPosition = new Vector2(0f, Mathf.Clamp(rectTransform.anchoredPosition.y * -1f - this.scrollRectTransform.rect.height / 2f, 0f, this.contentSlider.rect.height - this.scrollRectTransform.rect.height));
		}
	}

	// Token: 0x04000D44 RID: 3396
	private Selectable[] selectableItems;

	// Token: 0x04000D45 RID: 3397
	private ScrollRect scrollRect;

	// Token: 0x04000D46 RID: 3398
	private RectTransform scrollRectTransform;

	// Token: 0x04000D47 RID: 3399
	private RectTransform contentSlider;
}
