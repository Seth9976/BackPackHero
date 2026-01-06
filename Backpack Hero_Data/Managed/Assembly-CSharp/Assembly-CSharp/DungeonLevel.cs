using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000BB RID: 187
[CreateAssetMenu(fileName = "DungeonLevel", menuName = "ScriptableObjects/Dungeon Level")]
public class DungeonLevel : ScriptableObject
{
	// Token: 0x06000520 RID: 1312 RVA: 0x000326B8 File Offset: 0x000308B8
	public DungeonLevel.StageSprites GetStageSprites()
	{
		if (!EventManager.instance)
		{
			return this.stageSprites;
		}
		if (this.eventSprites == null || this.eventSprites.Length == 0)
		{
			return this.stageSprites;
		}
		for (int i = 0; i < this.eventSprites.Length; i++)
		{
			if (EventManager.instance.eventType == this.eventSprites[i].eventType)
			{
				return this.eventSprites[i].stageSprites;
			}
		}
		return this.stageSprites;
	}

	// Token: 0x040003D9 RID: 985
	[SerializeField]
	private DungeonLevel.EventSprites[] eventSprites;

	// Token: 0x040003DA RID: 986
	[SerializeField]
	public MetaProgressSaveManager.LastRun.RunEvents runEvent;

	// Token: 0x040003DB RID: 987
	public DungeonLevel.Zone zone;

	// Token: 0x040003DC RID: 988
	public string areaName;

	// Token: 0x040003DD RID: 989
	public List<DungeonLevel.Floor> floors;

	// Token: 0x040003DE RID: 990
	[HideInInspector]
	public DungeonLevel.Floor currentFloor = DungeonLevel.Floor.first;

	// Token: 0x040003DF RID: 991
	public AudioClip levelSong;

	// Token: 0x040003E0 RID: 992
	public Sprite doorSprite;

	// Token: 0x040003E1 RID: 993
	public Color backgroundColor;

	// Token: 0x040003E2 RID: 994
	public DungeonLevel.StageSprites stageSprites;

	// Token: 0x040003E3 RID: 995
	public List<DungeonLevel.DungeonEventsToSpawn> itemsToSpawnOnMap;

	// Token: 0x040003E4 RID: 996
	public List<DungeonLevel.EnemyEncounter2> enemyEncounters;

	// Token: 0x040003E5 RID: 997
	public List<DungeonLevel.EventEncounter2> eventEncounters;

	// Token: 0x040003E6 RID: 998
	public List<GameObject> levelPrefabs = new List<GameObject>();

	// Token: 0x020002E6 RID: 742
	[Serializable]
	private class EventSprites
	{
		// Token: 0x0400115A RID: 4442
		public EventManager.EventType eventType;

		// Token: 0x0400115B RID: 4443
		public DungeonLevel.StageSprites stageSprites;
	}

	// Token: 0x020002E7 RID: 743
	[Serializable]
	public class DungeonEventsToSpawn
	{
		// Token: 0x0400115C RID: 4444
		public DungeonLevel.Floor floor;

		// Token: 0x0400115D RID: 4445
		public List<DungeonSpawner.DungeonEventSpawn> itemsToSpawnOnMap;
	}

	// Token: 0x020002E8 RID: 744
	[Serializable]
	public class SpriteList
	{
		// Token: 0x0400115E RID: 4446
		public List<Sprite> spritesThatMustSpawnInOrder;

		// Token: 0x0400115F RID: 4447
		public List<RuntimeAnimatorController> animators;

		// Token: 0x04001160 RID: 4448
		public List<GameObject> sceneryPrefabs;
	}

	// Token: 0x020002E9 RID: 745
	[Serializable]
	public class StageSprites
	{
		// Token: 0x04001161 RID: 4449
		public List<DungeonLevel.SpriteList> ceilings;

		// Token: 0x04001162 RID: 4450
		public List<DungeonLevel.SpriteList> walls;

		// Token: 0x04001163 RID: 4451
		public List<DungeonLevel.SpriteList> floors;

		// Token: 0x04001164 RID: 4452
		public List<DungeonLevel.SpriteList> wholePieces;
	}

	// Token: 0x020002EA RID: 746
	[Serializable]
	public class EnemyEncounter2
	{
		// Token: 0x0600150A RID: 5386 RVA: 0x000B7DA0 File Offset: 0x000B5FA0
		public bool IsValid()
		{
			return (this.eventType == EventManager.EventType.None || EventManager.instance.eventType == this.eventType) && (this.requiredRunProperties.Count <= 0 || RunTypeManager.CheckIfRunPropertiesExist(this.requiredRunProperties)) && (this.disablingRunProperties.Count <= 0 || !RunTypeManager.CheckIfRunPropertiesExist(this.disablingRunProperties)) && (!Singleton.Instance.storyMode || MetaProgressSaveManager.ConditionsMet(this.storyModeConditions)) && (!Singleton.Instance.mission || !this.excludedMission.Contains(Singleton.Instance.mission)) && (!Singleton.Instance.mission || this.requiredMission.Count <= 0 || this.requiredMission.Contains(Singleton.Instance.mission)) && (Singleton.Instance.mission || this.requiredMission.Count <= 0) && (!this.storyModeOnly || Singleton.Instance.storyMode);
		}

		// Token: 0x04001165 RID: 4453
		[SerializeField]
		private EventManager.EventType eventType;

		// Token: 0x04001166 RID: 4454
		[SerializeField]
		private List<MetaProgressSaveManager.MetaProgressCondition> storyModeConditions = new List<MetaProgressSaveManager.MetaProgressCondition>();

		// Token: 0x04001167 RID: 4455
		[SerializeField]
		private List<RunType.RunProperty.Type> requiredRunProperties = new List<RunType.RunProperty.Type>();

		// Token: 0x04001168 RID: 4456
		[SerializeField]
		private List<RunType.RunProperty.Type> disablingRunProperties = new List<RunType.RunProperty.Type>();

		// Token: 0x04001169 RID: 4457
		[SerializeField]
		private List<Missions> excludedMission;

		// Token: 0x0400116A RID: 4458
		[SerializeField]
		private List<Missions> requiredMission;

		// Token: 0x0400116B RID: 4459
		[SerializeField]
		private bool storyModeOnly;

		// Token: 0x0400116C RID: 4460
		public DungeonLevel.Floor floor;

		// Token: 0x0400116D RID: 4461
		public bool isElite;

		// Token: 0x0400116E RID: 4462
		public List<GameObject> enemiesInGroup;
	}

	// Token: 0x020002EB RID: 747
	[Serializable]
	public class EventEncounter2
	{
		// Token: 0x0400116F RID: 4463
		[Header("----Event Encounter----")]
		public List<RunType.RunProperty.Type> requiredRunProperties = new List<RunType.RunProperty.Type>();

		// Token: 0x04001170 RID: 4464
		public List<RunType.RunProperty.Type> disablingRunProperties = new List<RunType.RunProperty.Type>();

		// Token: 0x04001171 RID: 4465
		public List<MetaProgressSaveManager.MetaProgressCondition> storyModeConditions = new List<MetaProgressSaveManager.MetaProgressCondition>();

		// Token: 0x04001172 RID: 4466
		public List<GameObject> eventType;

		// Token: 0x04001173 RID: 4467
		public List<DungeonLevel.Floor> floor;

		// Token: 0x04001174 RID: 4468
		public float weight = 1f;

		// Token: 0x04001175 RID: 4469
		public bool storyModeOnly;
	}

	// Token: 0x020002EC RID: 748
	[Serializable]
	public class ItemList
	{
		// Token: 0x04001176 RID: 4470
		public Character.CharacterName character;

		// Token: 0x04001177 RID: 4471
		public List<GameObject> items;
	}

	// Token: 0x020002ED RID: 749
	public enum Floor
	{
		// Token: 0x04001179 RID: 4473
		all,
		// Token: 0x0400117A RID: 4474
		intro,
		// Token: 0x0400117B RID: 4475
		first,
		// Token: 0x0400117C RID: 4476
		second,
		// Token: 0x0400117D RID: 4477
		third,
		// Token: 0x0400117E RID: 4478
		boss
	}

	// Token: 0x020002EE RID: 750
	public enum Zone
	{
		// Token: 0x04001180 RID: 4480
		dungeon,
		// Token: 0x04001181 RID: 4481
		deepCave,
		// Token: 0x04001182 RID: 4482
		magmaCore,
		// Token: 0x04001183 RID: 4483
		EnchantedSwamp,
		// Token: 0x04001184 RID: 4484
		theBramble,
		// Token: 0x04001185 RID: 4485
		ice,
		// Token: 0x04001186 RID: 4486
		Chaos
	}
}
