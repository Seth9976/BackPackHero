using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000084 RID: 132
public class PaxOptions : MonoBehaviour
{
	// Token: 0x060002E9 RID: 745 RVA: 0x0001117C File Offset: 0x0000F37C
	public void Start()
	{
		this.UpdateOptions();
	}

	// Token: 0x060002EA RID: 746 RVA: 0x00011184 File Offset: 0x0000F384
	public void UpdateOptions()
	{
		Singleton.Instance.isDemo = this.isDemo.isOn;
		Singleton.Instance.allowOptions = this.allowOptions.isOn;
		Singleton.Instance.enableTimeout = this.enableTimeout.isOn;
		Singleton.Instance.allowOtherCharacters = this.allowOtherCharacters.isOn;
		Singleton.Instance.allowOtherGameModes = this.allowOtherGameModes.isOn;
		Singleton.Instance.endGameAfterBoss = this.endGAmeAfterBoss.isOn;
		Singleton.Instance.showQRCode = this.showQRCode.isOn;
		int num = Mathf.RoundToInt(Mathf.Lerp(3f, 60f, this.timerSlider.value));
		Singleton.Instance.demoTime = (float)(num * 60);
		this.timerText.text = "Minutes allowed: " + num.ToString();
	}

	// Token: 0x060002EB RID: 747 RVA: 0x0001126F File Offset: 0x0000F46F
	public void StartGame()
	{
		SceneManager.LoadScene("MainMenu");
		Object.FindAnyObjectByType<DigitalCursor>().controlStyle = DigitalCursor.ControlStyle.controller;
	}

	// Token: 0x040001E3 RID: 483
	[SerializeField]
	private TextMeshProUGUI timerText;

	// Token: 0x040001E4 RID: 484
	[SerializeField]
	private Slider timerSlider;

	// Token: 0x040001E5 RID: 485
	[SerializeField]
	private Toggle isDemo;

	// Token: 0x040001E6 RID: 486
	[SerializeField]
	private Toggle allowOptions;

	// Token: 0x040001E7 RID: 487
	[SerializeField]
	private Toggle enableTimeout;

	// Token: 0x040001E8 RID: 488
	[SerializeField]
	private Toggle endGAmeAfterBoss;

	// Token: 0x040001E9 RID: 489
	[SerializeField]
	private Toggle allowOtherGameModes;

	// Token: 0x040001EA RID: 490
	[SerializeField]
	private Toggle allowOtherCharacters;

	// Token: 0x040001EB RID: 491
	[SerializeField]
	private Toggle showQRCode;
}
