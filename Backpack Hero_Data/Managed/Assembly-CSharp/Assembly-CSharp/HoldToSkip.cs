using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200005E RID: 94
public class HoldToSkip : MonoBehaviour
{
	// Token: 0x060001A5 RID: 421 RVA: 0x0000AA2B File Offset: 0x00008C2B
	private void Start()
	{
		this.skipText.SetActive(false);
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x0000AA3C File Offset: 0x00008C3C
	private void Update()
	{
		if (Input.GetMouseButton(0) || DigitalCursor.main.GetInputHold("cancel") || DigitalCursor.main.GetInputHold("pause") || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Return))
		{
			this.skipText.SetActive(true);
			this.holdTime += Time.deltaTime;
			this.slider.value = Mathf.Lerp(0f, 1f, this.holdTime / this.necessaryHold);
			if (this.holdTime >= this.necessaryHold)
			{
				this.OnClose.Invoke();
				return;
			}
		}
		else
		{
			this.slider.value = 0f;
			this.holdTime = 0f;
		}
	}

	// Token: 0x04000117 RID: 279
	[SerializeField]
	private UnityEvent OnClose;

	// Token: 0x04000118 RID: 280
	[SerializeField]
	private GameObject skipText;

	// Token: 0x04000119 RID: 281
	[SerializeField]
	private Slider slider;

	// Token: 0x0400011A RID: 282
	private float holdTime;

	// Token: 0x0400011B RID: 283
	private float necessaryHold = 1f;
}
