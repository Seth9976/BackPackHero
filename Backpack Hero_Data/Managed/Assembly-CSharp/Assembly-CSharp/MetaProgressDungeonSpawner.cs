using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000071 RID: 113
public class MetaProgressDungeonSpawner : MonoBehaviour
{
	// Token: 0x06000233 RID: 563 RVA: 0x0000DBA0 File Offset: 0x0000BDA0
	private void Start()
	{
		GameObject gameObject = null;
		foreach (MetaProgressDungeonSpawner.DungeonSpawn dungeonSpawn in this.dungeonSpawns)
		{
			if ((dungeonSpawn.storyMode != MetaProgressDungeonSpawner.DungeonSpawn.StoryMode.onlyDoInStoryMode || Singleton.Instance.storyMode) && (dungeonSpawn.storyMode != MetaProgressDungeonSpawner.DungeonSpawn.StoryMode.onlyDoNOTinStoryMode || !Singleton.Instance.storyMode) && MetaProgressSaveManager.ConditionsMet(dungeonSpawn.conditions) && RunTypeManager.CheckIfRunPropertiesExist(dungeonSpawn.requiredRunProperties) && (dungeonSpawn.requiredCharacters.Count == 0 || dungeonSpawn.requiredCharacters.Contains(Singleton.Instance.character)) && (!dungeonSpawn.mustBeInSpecifiedZone || GameManager.main.dungeonLevel.zone == dungeonSpawn.zone))
			{
				gameObject = dungeonSpawn.itemToSpawn;
				dungeonSpawn.itemToSpawn.SetActive(true);
				Debug.Log("Spawning " + dungeonSpawn.itemToSpawn.name);
				break;
			}
		}
		foreach (MetaProgressDungeonSpawner.DungeonSpawn dungeonSpawn2 in this.dungeonSpawns)
		{
			if (gameObject != dungeonSpawn2.itemToSpawn)
			{
				Object.Destroy(dungeonSpawn2.itemToSpawn);
				Debug.Log("Destroying " + dungeonSpawn2.itemToSpawn.name);
			}
		}
	}

	// Token: 0x04000176 RID: 374
	[SerializeField]
	private List<MetaProgressDungeonSpawner.DungeonSpawn> dungeonSpawns = new List<MetaProgressDungeonSpawner.DungeonSpawn>();

	// Token: 0x02000281 RID: 641
	[Serializable]
	private class DungeonSpawn
	{
		// Token: 0x04000F6C RID: 3948
		[SerializeField]
		public DungeonLevel.Zone zone;

		// Token: 0x04000F6D RID: 3949
		[SerializeField]
		public bool mustBeInSpecifiedZone;

		// Token: 0x04000F6E RID: 3950
		public MetaProgressDungeonSpawner.DungeonSpawn.StoryMode storyMode = MetaProgressDungeonSpawner.DungeonSpawn.StoryMode.considerAlways;

		// Token: 0x04000F6F RID: 3951
		public GameObject itemToSpawn;

		// Token: 0x04000F70 RID: 3952
		public List<MetaProgressSaveManager.MetaProgressCondition> conditions = new List<MetaProgressSaveManager.MetaProgressCondition>();

		// Token: 0x04000F71 RID: 3953
		public List<RunType.RunProperty.Type> requiredRunProperties = new List<RunType.RunProperty.Type>();

		// Token: 0x04000F72 RID: 3954
		public List<Character.CharacterName> requiredCharacters = new List<Character.CharacterName>();

		// Token: 0x0200048B RID: 1163
		public enum StoryMode
		{
			// Token: 0x04001A87 RID: 6791
			onlyDoInStoryMode,
			// Token: 0x04001A88 RID: 6792
			onlyDoNOTinStoryMode,
			// Token: 0x04001A89 RID: 6793
			considerAlways
		}
	}
}
