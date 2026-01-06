using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000160 RID: 352
[CreateAssetMenu(fileName = "Mission", menuName = "ScriptableObjects/Mission")]
[ES3Serializable]
public class Missions : ScriptableObject
{
	// Token: 0x06000E13 RID: 3603 RVA: 0x0008BA8A File Offset: 0x00089C8A
	public static string Stringify(Missions m)
	{
		return m.name;
	}

	// Token: 0x06000E14 RID: 3604 RVA: 0x0008BA92 File Offset: 0x00089C92
	public static string MissionTranslationName(Missions m)
	{
		if (m.runTypeNumber == 0)
		{
			return LangaugeManager.main.GetTextByKey(m.runTypeLanguageKey);
		}
		return LangaugeManager.main.GetTextByKey(m.runTypeLanguageKey) + " " + m.runTypeNumber.ToString();
	}

	// Token: 0x06000E15 RID: 3605 RVA: 0x0008BAD4 File Offset: 0x00089CD4
	public static bool IsNextLevel()
	{
		if (GameManager.main.floor == 9 && RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.finalRun))
		{
			return true;
		}
		if (GameManager.main.floor == 0 && TutorialManager.main.playType == TutorialManager.PlayType.cr8Tutorial)
		{
			return false;
		}
		if (GameManager.main.dungeonLevel.currentFloor != DungeonLevel.Floor.boss)
		{
			return true;
		}
		if ((RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.runEndsAfterZone1) && GameManager.main.zoneNumber == 0) || (RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.runEndsAfterZone2) && GameManager.main.zoneNumber == 1) || (RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.runEndsAfterZone3) && GameManager.main.zoneNumber == 2))
		{
			return false;
		}
		if (!Singleton.Instance.mission || !Singleton.Instance.storyMode)
		{
			return true;
		}
		if (Singleton.Instance.IsStoryModeLevels())
		{
			Singleton.Instance.mission.GetNextDungeonLevel(GameManager.main.zoneNumber + 1);
			return true;
		}
		return true;
	}

	// Token: 0x06000E16 RID: 3606 RVA: 0x0008BBCF File Offset: 0x00089DCF
	public DungeonLevel GetNextDungeonLevel(int zoneNumber)
	{
		if (zoneNumber >= this.dungeonLevels.Count)
		{
			return null;
		}
		return this.dungeonLevels[zoneNumber];
	}

	// Token: 0x06000E17 RID: 3607 RVA: 0x0008BBF0 File Offset: 0x00089DF0
	public List<RunType.RunProperty> GetAllRunProperties()
	{
		List<RunType.RunProperty> list = new List<RunType.RunProperty>();
		list.AddRange(this.runProperties);
		foreach (Missions.ConditionalRunProperty conditionalRunProperty in this.conditionalRunProperties)
		{
			using (List<MetaProgressSaveManager.MetaProgressCondition>.Enumerator enumerator2 = conditionalRunProperty.conditions.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (MetaProgressSaveManager.ConditionMet(enumerator2.Current))
					{
						list.Add(conditionalRunProperty.runProperty);
					}
				}
			}
		}
		if (!this.canBeRepeated && MetaProgressSaveManager.main.missionsComplete.Contains(Missions.Stringify(this)) && MetaProgressSaveManager.main.GetMetaProgressMarkerValue(MetaProgressSaveManager.MetaProgressMarker.runsPlayed) >= 2)
		{
			list.Add(new RunType.RunProperty
			{
				type = RunType.RunProperty.Type.storyModeProgressDisabled
			});
		}
		return list;
	}

	// Token: 0x04000B6F RID: 2927
	[SerializeField]
	public bool hardMode;

	// Token: 0x04000B70 RID: 2928
	[TextArea(15, 8)]
	[SerializeField]
	public string missionGottenFromNote = "";

	// Token: 0x04000B71 RID: 2929
	[SerializeField]
	public bool canBeRepeated;

	// Token: 0x04000B72 RID: 2930
	[SerializeField]
	public List<MetaProgressSaveManager.MetaProgressMarker> temporaryMetaProgressMarker;

	// Token: 0x04000B73 RID: 2931
	[SerializeField]
	public List<MetaProgressSaveManager.MetaProgressMarker> metaProgressMarkersToAddOnWin;

	// Token: 0x04000B74 RID: 2932
	[SerializeField]
	public List<DungeonLevel> dungeonLevels;

	// Token: 0x04000B75 RID: 2933
	[SerializeField]
	public string runTypeLanguageKey;

	// Token: 0x04000B76 RID: 2934
	[SerializeField]
	public int runTypeNumber;

	// Token: 0x04000B77 RID: 2935
	public Character validForCharacter;

	// Token: 0x04000B78 RID: 2936
	[SerializeField]
	public List<GameObject> rewards;

	// Token: 0x04000B79 RID: 2937
	[SerializeField]
	public List<Missions> rewardsMissions;

	// Token: 0x04000B7A RID: 2938
	public List<Missions.ConditionalRunProperty> conditionalRunProperties = new List<Missions.ConditionalRunProperty>();

	// Token: 0x04000B7B RID: 2939
	public List<RunType.RunProperty> runProperties;

	// Token: 0x02000423 RID: 1059
	[Serializable]
	public class ConditionalRunProperty
	{
		// Token: 0x04001824 RID: 6180
		public List<MetaProgressSaveManager.MetaProgressCondition> conditions = new List<MetaProgressSaveManager.MetaProgressCondition>();

		// Token: 0x04001825 RID: 6181
		public RunType.RunProperty runProperty;
	}
}
