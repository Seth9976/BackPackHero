using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000057 RID: 87
public class GetTimeLeft : MonoBehaviour
{
	// Token: 0x0600018E RID: 398 RVA: 0x0000A0E9 File Offset: 0x000082E9
	private void Start()
	{
	}

	// Token: 0x0600018F RID: 399 RVA: 0x0000A0EC File Offset: 0x000082EC
	private void Update()
	{
		DateTime now = DateTime.Now;
		DateTime dateTime = new DateTime(2023, 6, 23, 14, 0, 0);
		DateTime dateTime2 = new DateTime(2023, 7, 15, 9, 0, 0);
		TimeSpan timeSpan = dateTime2.Subtract(now);
		this.text.text = string.Concat(new string[]
		{
			"Time Left: ",
			timeSpan.Days.ToString(),
			"d : ",
			timeSpan.Hours.ToString(),
			"h : ",
			timeSpan.Minutes.ToString(),
			"m : ",
			timeSpan.Seconds.ToString(),
			"s"
		});
		this.slider.value = (float)timeSpan.TotalSeconds / (float)dateTime2.Subtract(dateTime).TotalSeconds;
		if (timeSpan.TotalSeconds <= 0.0)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x04000102 RID: 258
	[SerializeField]
	private TextMeshProUGUI text;

	// Token: 0x04000103 RID: 259
	[SerializeField]
	private Slider slider;
}
