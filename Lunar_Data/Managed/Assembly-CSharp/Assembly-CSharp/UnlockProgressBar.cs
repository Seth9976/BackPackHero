using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000A5 RID: 165
public class UnlockProgressBar : MonoBehaviour
{
	// Token: 0x06000472 RID: 1138 RVA: 0x00015DAC File Offset: 0x00013FAC
	public void SetUnlock(Unlock unlock)
	{
		this.unlock = unlock;
		if (unlock.necessaryCompletedRun)
		{
			this.replacementLabelText.SetKey("completedRun");
			this.replacementLabelText.AddAdditionalText(unlock.necessaryCompletedRun.runName, ReplacementText.AdditionalText.position.ReplaceVariable);
			this.startingAmount = (float)(Singleton.instance.CheckStartingCompletedRun(unlock.necessaryCompletedRun) ? 1 : 0);
			this.endingAmount = (float)(Singleton.instance.CheckCompletedRun(unlock.necessaryCompletedRun) ? 1 : 0);
		}
		else
		{
			this.replacementLabelText.SetKey(unlock.accomplishment.ToString());
			this.startingAmount = (float)Singleton.instance.CheckAccomplishmentStartingValue(unlock.accomplishment);
			this.endingAmount = (float)Singleton.instance.CheckAccomplishmentValue(unlock.accomplishment);
		}
		this.totalAmount = unlock.amount;
		this.currentDisplayValue = this.startingAmount;
		this.slider.value = this.CalculateProgress();
		this.progressText.text = Mathf.Round(this.currentDisplayValue).ToString() + " / " + this.totalAmount.ToString();
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x00015ED6 File Offset: 0x000140D6
	private void Start()
	{
		this.currentDelay = 0f - (float)base.transform.GetSiblingIndex() * 0.75f;
	}

	// Token: 0x06000474 RID: 1140 RVA: 0x00015EF6 File Offset: 0x000140F6
	private void Update()
	{
		if (this.currentDelay < this.initialDelay)
		{
			this.currentDelay += Time.deltaTime;
			return;
		}
		this.UpdateProgress();
	}

	// Token: 0x06000475 RID: 1141 RVA: 0x00015F20 File Offset: 0x00014120
	private void UpdateProgress()
	{
		this.sfxSource.volume = SoundManager.instance.sfxVolume * this.sfxVolume;
		if (!this.sfxSource.isPlaying && this.currentDisplayValue < this.endingAmount)
		{
			this.sfxSource.Play();
		}
		if (this.sfxVolume <= 0f)
		{
			this.sfxSource.Stop();
		}
		if (this.currentDisplayValue >= this.endingAmount)
		{
			this.sfxVolume -= 0.01f;
		}
		if (!this.openedUnlockCompletePanel && this.currentDisplayValue >= this.totalAmount)
		{
			UnlockCompletePanel componentInParent = base.GetComponentInParent<UnlockCompletePanel>();
			if (componentInParent)
			{
				componentInParent.UnlockComplete(this.unlock);
			}
			this.openedUnlockCompletePanel = true;
		}
		this.progressText.text = Mathf.Round(this.currentDisplayValue).ToString() + " / " + this.totalAmount.ToString();
		this.slider.value = this.CalculateProgress();
		this.currentDisplayValue = Mathf.MoveTowards(this.currentDisplayValue, this.endingAmount, (this.endingAmount - this.startingAmount) / this.fillTime * Time.deltaTime);
	}

	// Token: 0x06000476 RID: 1142 RVA: 0x00016054 File Offset: 0x00014254
	private float CalculateProgress()
	{
		return this.currentDisplayValue / this.totalAmount;
	}

	// Token: 0x06000477 RID: 1143 RVA: 0x00016064 File Offset: 0x00014264
	public void Show()
	{
		AllUnlocksProgressPanel componentInParent = base.GetComponentInParent<AllUnlocksProgressPanel>();
		if (componentInParent)
		{
			componentInParent.ShowUnlockDetails(this.unlock);
		}
	}

	// Token: 0x0400036B RID: 875
	[SerializeField]
	private Slider slider;

	// Token: 0x0400036C RID: 876
	[SerializeField]
	private ReplacementText replacementLabelText;

	// Token: 0x0400036D RID: 877
	[SerializeField]
	private TextMeshProUGUI progressText;

	// Token: 0x0400036E RID: 878
	[SerializeField]
	private AudioSource sfxSource;

	// Token: 0x0400036F RID: 879
	private float sfxVolume = 1f;

	// Token: 0x04000370 RID: 880
	[SerializeField]
	private float initialDelay = 0.25f;

	// Token: 0x04000371 RID: 881
	private float currentDelay;

	// Token: 0x04000372 RID: 882
	[SerializeField]
	private float fillTime = 1f;

	// Token: 0x04000373 RID: 883
	[SerializeField]
	private float totalAmount = 1000f;

	// Token: 0x04000374 RID: 884
	[SerializeField]
	private float startingAmount = 800f;

	// Token: 0x04000375 RID: 885
	[SerializeField]
	private float endingAmount = 900f;

	// Token: 0x04000376 RID: 886
	private float currentDisplayValue;

	// Token: 0x04000377 RID: 887
	private Unlock unlock;

	// Token: 0x04000378 RID: 888
	private bool openedUnlockCompletePanel;
}
