using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200018F RID: 399
public class Tutorial : MonoBehaviour
{
	// Token: 0x06001018 RID: 4120 RVA: 0x0009B743 File Offset: 0x00099943
	public void SetTutorial(string _titleText, string _descriptiveText, Sprite image)
	{
		this.titleText.text = _titleText;
		this.descriptiveText.text = _descriptiveText;
		this.tutorialImage.sprite = image;
		LangaugeManager.main.SetFont(base.transform);
	}

	// Token: 0x04000D33 RID: 3379
	[SerializeField]
	private TextMeshProUGUI titleText;

	// Token: 0x04000D34 RID: 3380
	[SerializeField]
	private TextMeshProUGUI descriptiveText;

	// Token: 0x04000D35 RID: 3381
	[SerializeField]
	private Image tutorialImage;
}
