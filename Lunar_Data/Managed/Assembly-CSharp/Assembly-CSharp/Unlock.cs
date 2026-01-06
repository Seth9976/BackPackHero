using System;
using SaveSystem.States;
using UnityEngine;

// Token: 0x020000A2 RID: 162
[CreateAssetMenu(fileName = "Unlock", menuName = "ScriptableObjects/Unlock")]
public class Unlock : ScriptableObject
{
	// Token: 0x0600045C RID: 1116 RVA: 0x00015698 File Offset: 0x00013898
	public bool AlreadyUnlocked()
	{
		if (this.necessaryCompletedRun != null)
		{
			return Singleton.instance.CheckStartingCompletedRun(this.necessaryCompletedRun);
		}
		return (float)Singleton.instance.CheckAccomplishmentStartingValue(this.accomplishment) >= this.amount;
	}

	// Token: 0x0600045D RID: 1117 RVA: 0x000156E8 File Offset: 0x000138E8
	public bool Unlocked()
	{
		if (this.necessaryCompletedRun != null)
		{
			return Singleton.instance.CheckCompletedRun(this.necessaryCompletedRun);
		}
		return (float)Singleton.instance.CheckAccomplishmentValue(this.accomplishment) >= this.amount;
	}

	// Token: 0x0600045E RID: 1118 RVA: 0x00015738 File Offset: 0x00013938
	public bool UnlockedThisRun()
	{
		if (this.necessaryCompletedRun != null)
		{
			return Singleton.instance.CheckCompletedRun(this.necessaryCompletedRun) && !Singleton.instance.CheckStartingCompletedRun(this.necessaryCompletedRun);
		}
		float num = (float)Singleton.instance.CheckAccomplishmentStartingValue(this.accomplishment);
		return (float)Singleton.instance.CheckAccomplishmentValue(this.accomplishment) >= this.amount && num < this.amount;
	}

	// Token: 0x0600045F RID: 1119 RVA: 0x000157B4 File Offset: 0x000139B4
	public float ProgressMade()
	{
		if (this.necessaryCompletedRun != null)
		{
			float num = (float)(Singleton.instance.CheckStartingCompletedRun(this.necessaryCompletedRun) ? 1 : 0);
			return (float)(Singleton.instance.CheckCompletedRun(this.necessaryCompletedRun) ? 1 : 0) - num;
		}
		float num2 = (float)Singleton.instance.CheckAccomplishmentStartingValue(this.accomplishment);
		return ((float)Singleton.instance.CheckAccomplishmentValue(this.accomplishment) - num2) / this.amount;
	}

	// Token: 0x04000350 RID: 848
	public bool isVisible = true;

	// Token: 0x04000351 RID: 849
	[Header("----------------------Unlock Conditions----------------------")]
	[SerializeField]
	public ProgressState.Accomplishment accomplishment;

	// Token: 0x04000352 RID: 850
	[SerializeField]
	public RunType necessaryCompletedRun;

	// Token: 0x04000353 RID: 851
	[SerializeField]
	public float amount = 100f;

	// Token: 0x04000354 RID: 852
	[Header("----------------------Unlock Effects----------------------")]
	[Header("----------------------Unlock Effects----------------------")]
	[SerializeField]
	public GameObject unlockedItem;

	// Token: 0x04000355 RID: 853
	[SerializeField]
	public RunType runType;

	// Token: 0x04000356 RID: 854
	[SerializeField]
	public PlayableCharacter character;
}
