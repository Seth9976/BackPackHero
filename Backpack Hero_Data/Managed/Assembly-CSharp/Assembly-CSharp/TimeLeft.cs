using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001A2 RID: 418
public class TimeLeft : MonoBehaviour
{
	// Token: 0x060010A6 RID: 4262 RVA: 0x0009E485 File Offset: 0x0009C685
	private void Start()
	{
	}

	// Token: 0x060010A7 RID: 4263 RVA: 0x0009E488 File Offset: 0x0009C688
	private void Update()
	{
		DateTime utcNow = DateTime.UtcNow;
		TimeSpan timeSpan = this.endTime - utcNow;
		if (timeSpan.TotalSeconds <= 0.0)
		{
			timeSpan = this.endTime - utcNow;
		}
		if (timeSpan.TotalSeconds <= 0.0)
		{
			base.gameObject.SetActive(false);
		}
		this.timeLeftText.text = string.Concat(new string[]
		{
			timeSpan.Days.ToString(),
			"d : ",
			timeSpan.Hours.ToString(),
			"h : ",
			timeSpan.Minutes.ToString(),
			"m : ",
			timeSpan.Seconds.ToString(),
			"s"
		});
		this.slider.value = (float)timeSpan.TotalSeconds / (float)(this.endTime - this.startTime).TotalSeconds;
	}

	// Token: 0x04000D89 RID: 3465
	[SerializeField]
	private TextMeshProUGUI timeLeftText;

	// Token: 0x04000D8A RID: 3466
	[SerializeField]
	private Slider slider;

	// Token: 0x04000D8B RID: 3467
	private DateTime startTime = new DateTime(2023, 6, 20, 12, 0, 0);

	// Token: 0x04000D8C RID: 3468
	private DateTime endTime = new DateTime(2023, 7, 26, 3, 0, 0);

	// Token: 0x04000D8D RID: 3469
	[SerializeField]
	private float timeAdjustment;
}
