using System;
using UnityEngine;

// Token: 0x02000041 RID: 65
public class FadeOutWithPlayer : MonoBehaviour
{
	// Token: 0x060001DA RID: 474 RVA: 0x0000A0DB File Offset: 0x000082DB
	private void Start()
	{
	}

	// Token: 0x060001DB RID: 475 RVA: 0x0000A0E0 File Offset: 0x000082E0
	private void Update()
	{
		if (!Player.instance)
		{
			return;
		}
		Vector2 vector = Player.instance.transform.position;
		Vector2 vector2 = Camera.main.WorldToScreenPoint(vector);
		Vector3[] array = new Vector3[4];
		this.rectTransform.GetWorldCorners(array);
		Rect rect = new Rect(array[0], array[2] - array[0]);
		float num = 1f;
		if (rect.Contains(vector2))
		{
			num = 0.15f;
		}
		this.canvasGroup.alpha = Mathf.MoveTowards(this.canvasGroup.alpha, num, Time.deltaTime * 4f);
	}

	// Token: 0x0400016E RID: 366
	[SerializeField]
	private CanvasGroup canvasGroup;

	// Token: 0x0400016F RID: 367
	[SerializeField]
	private RectTransform rectTransform;
}
