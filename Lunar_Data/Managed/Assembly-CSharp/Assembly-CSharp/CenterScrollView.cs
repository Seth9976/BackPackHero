using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200001A RID: 26
public class CenterScrollView : MonoBehaviour
{
	// Token: 0x060000C0 RID: 192 RVA: 0x00005766 File Offset: 0x00003966
	private void Start()
	{
		this.viewPort = this.scrollRect.viewport;
		this.content = this.scrollRect.content;
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x0000578C File Offset: 0x0000398C
	private void Update()
	{
		if (!ControllerSpriteManager.instance.isUsingController)
		{
			return;
		}
		if (!EventSystem.current.currentSelectedGameObject)
		{
			return;
		}
		if (EventSystem.current.currentSelectedGameObject.transform.parent != this.content)
		{
			return;
		}
		this.scrollRect.normalizedPosition = new Vector2(0f, 1f - -EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().anchoredPosition.y / this.content.rect.height);
		if (this.scrollRect.normalizedPosition.y < 0f)
		{
			this.scrollRect.normalizedPosition = new Vector2(0f, 0f);
		}
		if (this.scrollRect.normalizedPosition.y > 1f)
		{
			this.scrollRect.normalizedPosition = new Vector2(0f, 1f);
		}
	}

	// Token: 0x0400008F RID: 143
	[SerializeField]
	private ScrollRect scrollRect;

	// Token: 0x04000090 RID: 144
	private RectTransform viewPort;

	// Token: 0x04000091 RID: 145
	private RectTransform content;
}
