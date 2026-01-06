using System;
using UnityEngine;

// Token: 0x0200009C RID: 156
public class Tooltip : MonoBehaviour
{
	// Token: 0x06000433 RID: 1075 RVA: 0x00014F5C File Offset: 0x0001315C
	private void Start()
	{
	}

	// Token: 0x06000434 RID: 1076 RVA: 0x00014F5E File Offset: 0x0001315E
	private void Update()
	{
	}

	// Token: 0x06000435 RID: 1077 RVA: 0x00014F60 File Offset: 0x00013160
	public void SetKey(string text)
	{
		this.replacementText.SetKey(text);
	}

	// Token: 0x06000436 RID: 1078 RVA: 0x00014F70 File Offset: 0x00013170
	private void KeepOnScreen()
	{
		RectTransform rectTransform = CanvasManager.instance.rectTransform;
		RectTransform rectTransform2 = this.rectTransform;
		Vector2 vector = rectTransform2.sizeDelta * base.transform.localScale;
		Vector2 vector2 = rectTransform.sizeDelta * (rectTransform2.anchorMin - Vector2.one / 2f);
		Vector2 vector3 = vector * (rectTransform2.pivot - Vector2.one / 2f * 2f);
		Vector2 vector4 = vector * (Vector2.one / 2f * 2f - rectTransform2.pivot);
		float num = rectTransform.sizeDelta.x * -0.5f - vector2.x - vector4.x + vector.x;
		float num2 = rectTransform.sizeDelta.x * 0.5f - vector2.x + vector3.x;
		float num3 = rectTransform.sizeDelta.y * -0.5f - vector2.y - vector4.y + vector.y;
		float num4 = rectTransform.sizeDelta.y * 0.5f - vector2.y + vector3.y;
		Vector2 anchoredPosition = rectTransform2.anchoredPosition;
		anchoredPosition.x = Mathf.Clamp(anchoredPosition.x, num, num2);
		anchoredPosition.y = Mathf.Clamp(anchoredPosition.y, num3, num4);
		rectTransform2.anchoredPosition = anchoredPosition;
	}

	// Token: 0x04000338 RID: 824
	public static Tooltip instance;

	// Token: 0x04000339 RID: 825
	[SerializeField]
	private RectTransform rectTransform;

	// Token: 0x0400033A RID: 826
	[SerializeField]
	private ReplacementText replacementText;
}
