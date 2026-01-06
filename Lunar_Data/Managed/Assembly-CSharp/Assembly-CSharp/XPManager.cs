using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000AD RID: 173
public class XPManager : MonoBehaviour
{
	// Token: 0x06000498 RID: 1176 RVA: 0x00016895 File Offset: 0x00014A95
	private void OnEnable()
	{
		XPManager.instance = this;
	}

	// Token: 0x06000499 RID: 1177 RVA: 0x0001689D File Offset: 0x00014A9D
	private void OnDisable()
	{
		XPManager.instance = null;
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x000168A5 File Offset: 0x00014AA5
	private void Start()
	{
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x000168A8 File Offset: 0x00014AA8
	private void Update()
	{
		if (OperatorPanel.instance)
		{
			this.xpSlider.gameObject.SetActive(false);
			return;
		}
		this.xpSlider.gameObject.SetActive(true);
		this.storedValue = Mathf.Lerp(this.storedValue, Mathf.Min(this.xp / this.nextLevelXP, 1f), Time.deltaTime * 5f);
		if (this.nextLevelXP == -1f)
		{
			this.xpSlider.value = 1f;
			return;
		}
		this.xpSlider.value = this.storedValue;
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x00016948 File Offset: 0x00014B48
	public void AddXP(int amount)
	{
		if (this.nextLevelXP == -1f)
		{
			return;
		}
		this.xp += (float)amount * RunTypeManager.instance.GetRunTypeModifierPercentage(RunType.RunProperty.RunPropertyType.GainMoreExperience);
		if (this.xp >= this.nextLevelXP)
		{
			if (this.resetXPCoroutine != null)
			{
				return;
			}
			if (Player.instance.isDead || Player.instance.isWinning)
			{
				return;
			}
			this.resetXPCoroutine = base.StartCoroutine(this.ResetXP(this.nextLevelXP));
			GameManager.instance.StartChooseCard();
		}
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x000169D0 File Offset: 0x00014BD0
	private IEnumerator ResetXP(float amount)
	{
		yield return null;
		while (SingleUI.IsViewingPopUp())
		{
			yield return null;
		}
		this.xp -= amount;
		if (this.xpLevels.Length <= this.level)
		{
			this.nextLevelXP = -1f;
		}
		else
		{
			this.nextLevelXP = this.xpLevels[this.level];
		}
		this.level++;
		this.resetXPCoroutine = null;
		yield break;
	}

	// Token: 0x0400038E RID: 910
	public static XPManager instance;

	// Token: 0x0400038F RID: 911
	[SerializeField]
	private float xp;

	// Token: 0x04000390 RID: 912
	[SerializeField]
	private float nextLevelXP = 20f;

	// Token: 0x04000391 RID: 913
	[SerializeField]
	private float[] xpLevels;

	// Token: 0x04000392 RID: 914
	private int level;

	// Token: 0x04000393 RID: 915
	[SerializeField]
	private Slider xpSlider;

	// Token: 0x04000394 RID: 916
	private float storedValue;

	// Token: 0x04000395 RID: 917
	private Coroutine resetXPCoroutine;
}
