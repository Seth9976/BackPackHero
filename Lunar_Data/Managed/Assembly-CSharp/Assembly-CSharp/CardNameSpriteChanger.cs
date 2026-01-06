using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000016 RID: 22
public class CardNameSpriteChanger : MonoBehaviour
{
	// Token: 0x0600009E RID: 158 RVA: 0x00004C2C File Offset: 0x00002E2C
	private void Update()
	{
		if (this.thisRect.sizeDelta.x > this.parentRect.sizeDelta.x * 0.9f)
		{
			this.thisImage.sprite = this.fullBottomSprite;
			Object.Destroy(this);
		}
	}

	// Token: 0x04000079 RID: 121
	[SerializeField]
	private Sprite fullBottomSprite;

	// Token: 0x0400007A RID: 122
	[SerializeField]
	private RectTransform thisRect;

	// Token: 0x0400007B RID: 123
	[SerializeField]
	private RectTransform parentRect;

	// Token: 0x0400007C RID: 124
	[SerializeField]
	private Image thisImage;
}
