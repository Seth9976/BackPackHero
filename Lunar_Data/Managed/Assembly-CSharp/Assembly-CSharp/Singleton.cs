using System;
using System.Collections.Generic;
using SaveSystem;
using SaveSystem.States;
using UnityEngine;

// Token: 0x02000091 RID: 145
public class Singleton : MonoBehaviour
{
	// Token: 0x060003BF RID: 959 RVA: 0x00012953 File Offset: 0x00010B53
	private void OnEnable()
	{
		if (Singleton.instance == null)
		{
			Singleton.instance = this;
			Object.DontDestroyOnLoad(base.gameObject);
			return;
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060003C0 RID: 960 RVA: 0x0001297F File Offset: 0x00010B7F
	private void OnDisable()
	{
		if (Singleton.instance == this)
		{
			Singleton.instance = null;
		}
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x00012994 File Offset: 0x00010B94
	private void Start()
	{
		SaveManager.LoadOptions();
		SaveManager.LoadProgress();
		this.UpdateStartingRunAccomplishments();
	}

	// Token: 0x060003C2 RID: 962 RVA: 0x000129A6 File Offset: 0x00010BA6
	private void Update()
	{
	}

	// Token: 0x060003C3 RID: 963 RVA: 0x000129A8 File Offset: 0x00010BA8
	public void DeleteAllProgress()
	{
		this.accomplishments.Clear();
		this.completedRuns.Clear();
		this.startingRunAccomplishments.Clear();
		this.startingCompletedRuns.Clear();
	}

	// Token: 0x060003C4 RID: 964 RVA: 0x000129D8 File Offset: 0x00010BD8
	public void CompleteRun()
	{
		if (!this.selectedRun)
		{
			this.selectedRun = this.defaultRunType;
		}
		if (this.CheckCompletedRun(this.selectedRun))
		{
			return;
		}
		this.completedRuns.Add(Utils.GetPrefabName(this.selectedRun.name));
	}

	// Token: 0x060003C5 RID: 965 RVA: 0x00012A28 File Offset: 0x00010C28
	public bool CheckStartingCompletedRun(RunType runType)
	{
		return this.CheckStartingCompletedRun(runType.name);
	}

	// Token: 0x060003C6 RID: 966 RVA: 0x00012A36 File Offset: 0x00010C36
	public bool CheckStartingCompletedRun(string runName)
	{
		runName = Utils.GetPrefabName(runName);
		return this.startingCompletedRuns.Contains(runName);
	}

	// Token: 0x060003C7 RID: 967 RVA: 0x00012A4C File Offset: 0x00010C4C
	public bool CheckCompletedRun(RunType runType)
	{
		return this.CheckCompletedRun(runType.name);
	}

	// Token: 0x060003C8 RID: 968 RVA: 0x00012A5A File Offset: 0x00010C5A
	public bool CheckCompletedRun(string runName)
	{
		runName = Utils.GetPrefabName(runName);
		return this.completedRuns.Contains(runName);
	}

	// Token: 0x060003C9 RID: 969 RVA: 0x00012A70 File Offset: 0x00010C70
	public void UpdateStartingRunAccomplishments()
	{
		this.startingRunAccomplishments.Clear();
		foreach (Singleton.Accomplishment accomplishment in this.accomplishments)
		{
			this.startingRunAccomplishments.Add(new Singleton.Accomplishment
			{
				accomplishment = accomplishment.accomplishment,
				character = accomplishment.character,
				amount = accomplishment.amount
			});
		}
		this.startingCompletedRuns.Clear();
		foreach (string text in this.completedRuns)
		{
			this.startingCompletedRuns.Add(text);
		}
	}

	// Token: 0x060003CA RID: 970 RVA: 0x00012B50 File Offset: 0x00010D50
	public int CheckAccomplishmentStartingValue(ProgressState.Accomplishment accomplishment)
	{
		foreach (Singleton.Accomplishment accomplishment2 in this.startingRunAccomplishments)
		{
			if (accomplishment2.accomplishment == accomplishment && accomplishment2.character == this.selectedCharacter.characterName)
			{
				return accomplishment2.amount;
			}
		}
		return 0;
	}

	// Token: 0x060003CB RID: 971 RVA: 0x00012BC4 File Offset: 0x00010DC4
	public int CheckAccomplishmentValue(ProgressState.Accomplishment accomplishment)
	{
		foreach (Singleton.Accomplishment accomplishment2 in this.accomplishments)
		{
			if (accomplishment2.accomplishment == accomplishment && accomplishment2.character == this.selectedCharacter.characterName)
			{
				return accomplishment2.amount;
			}
		}
		return 0;
	}

	// Token: 0x060003CC RID: 972 RVA: 0x00012C38 File Offset: 0x00010E38
	public bool CheckAccomplishment(ProgressState.Accomplishment accomplishment)
	{
		foreach (Singleton.Accomplishment accomplishment2 in this.accomplishments)
		{
			if (accomplishment2.accomplishment == accomplishment && accomplishment2.character == this.selectedCharacter.characterName)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060003CD RID: 973 RVA: 0x00012CA8 File Offset: 0x00010EA8
	public void AddAccomplishment(ProgressState.Accomplishment accomplishment)
	{
		foreach (Singleton.Accomplishment accomplishment2 in this.accomplishments)
		{
			if (accomplishment2.accomplishment == accomplishment)
			{
				accomplishment2.amount++;
				return;
			}
		}
		this.accomplishments.Add(new Singleton.Accomplishment
		{
			accomplishment = accomplishment,
			character = this.selectedCharacter.characterName,
			amount = 1
		});
	}

	// Token: 0x060003CE RID: 974 RVA: 0x00012D3C File Offset: 0x00010F3C
	public void AddAccomplishment(ProgressState.Accomplishment accomplishment, int amount = 1)
	{
		foreach (Singleton.Accomplishment accomplishment2 in this.accomplishments)
		{
			if (accomplishment2.accomplishment == accomplishment && accomplishment2.character == this.selectedCharacter.characterName)
			{
				accomplishment2.amount += amount;
				return;
			}
		}
		this.accomplishments.Add(new Singleton.Accomplishment
		{
			accomplishment = accomplishment,
			character = this.selectedCharacter.characterName,
			amount = amount
		});
	}

	// Token: 0x040002E7 RID: 743
	public static Singleton instance;

	// Token: 0x040002E8 RID: 744
	[Header("--------------------------Options-------------------------")]
	public bool stretchToFill;

	// Token: 0x040002E9 RID: 745
	[Header("--------------------------Controller--------------------------")]
	public Singleton.ControlType controlType;

	// Token: 0x040002EA RID: 746
	[Header("--------------------------Defaults--------------------------")]
	[SerializeField]
	private RunType defaultRunType;

	// Token: 0x040002EB RID: 747
	[Header("--------------------------Meta Game Data--------------------------")]
	public List<Singleton.Accomplishment> startingRunAccomplishments = new List<Singleton.Accomplishment>();

	// Token: 0x040002EC RID: 748
	public List<Singleton.Accomplishment> accomplishments = new List<Singleton.Accomplishment>();

	// Token: 0x040002ED RID: 749
	public List<string> startingCompletedRuns = new List<string>();

	// Token: 0x040002EE RID: 750
	public List<string> completedRuns = new List<string>();

	// Token: 0x040002EF RID: 751
	[Header("--------------------------Run Information--------------------------")]
	[SerializeField]
	public PlayableCharacter selectedCharacter;

	// Token: 0x040002F0 RID: 752
	[SerializeField]
	public RunType selectedRun;

	// Token: 0x0200010E RID: 270
	public enum ControlType
	{
		// Token: 0x040004E5 RID: 1253
		Xbox,
		// Token: 0x040004E6 RID: 1254
		Switch
	}

	// Token: 0x0200010F RID: 271
	[Serializable]
	public class Accomplishment
	{
		// Token: 0x040004E7 RID: 1255
		public ProgressState.Accomplishment accomplishment;

		// Token: 0x040004E8 RID: 1256
		public PlayableCharacter.CharacterName character;

		// Token: 0x040004E9 RID: 1257
		public int amount;
	}
}
