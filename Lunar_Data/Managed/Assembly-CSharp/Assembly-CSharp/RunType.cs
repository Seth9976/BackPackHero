using System;
using System.Collections.Generic;
using SaveSystem.States;
using UnityEngine;

// Token: 0x02000086 RID: 134
[CreateAssetMenu(fileName = "RunType", menuName = "ScriptableObjects/RunType", order = 3)]
public class RunType : ScriptableObject
{
	// Token: 0x040002BE RID: 702
	[Header("---------------------------Validity---------------------------")]
	[SerializeField]
	public PlayableCharacter.CharacterName characterName;

	// Token: 0x040002BF RID: 703
	[SerializeField]
	public ProgressState.Unlock necessaryUnlock;

	// Token: 0x040002C0 RID: 704
	[Header("---------------------------Run Properties---------------------------")]
	[SerializeField]
	public bool forceTutorial;

	// Token: 0x040002C1 RID: 705
	[SerializeField]
	public string runName;

	// Token: 0x040002C2 RID: 706
	[SerializeField]
	public string runDescription;

	// Token: 0x040002C3 RID: 707
	[SerializeField]
	public List<RunType.RunProperty> runProperties;

	// Token: 0x02000109 RID: 265
	[Serializable]
	public class RunProperty
	{
		// Token: 0x040004D5 RID: 1237
		public RunType.RunProperty.RunPropertyType runPropertyType;

		// Token: 0x040004D6 RID: 1238
		public float value;

		// Token: 0x040004D7 RID: 1239
		public List<GameObject> objs;

		// Token: 0x040004D8 RID: 1240
		[Header("---------------------------Descriptors---------------------------")]
		public string descriptorKey;

		// Token: 0x040004D9 RID: 1241
		public bool isPercentage;

		// Token: 0x040004DA RID: 1242
		public bool showObjectsWithImages;

		// Token: 0x0200012D RID: 301
		public enum RunPropertyType
		{
			// Token: 0x04000563 RID: 1379
			StartingDeck,
			// Token: 0x04000564 RID: 1380
			ExtraTimeToSurvive,
			// Token: 0x04000565 RID: 1381
			GainMoreExperience,
			// Token: 0x04000566 RID: 1382
			FasterWaveManagerTimer,
			// Token: 0x04000567 RID: 1383
			FasterEnemyMovement,
			// Token: 0x04000568 RID: 1384
			ExtraHealthEachLevel
		}
	}
}
