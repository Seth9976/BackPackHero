using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000A9 RID: 169
[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character")]
public class Character : ScriptableObject
{
	// Token: 0x040002D6 RID: 726
	[SerializeField]
	public Sprite standardGridSprite;

	// Token: 0x040002D7 RID: 727
	[SerializeField]
	public Sprite[] itemBorderBackgroundSprites;

	// Token: 0x040002D8 RID: 728
	public Sprite portraitSprite;

	// Token: 0x040002D9 RID: 729
	public string characterNameKey;

	// Token: 0x040002DA RID: 730
	public string characterDescriptionKey;

	// Token: 0x040002DB RID: 731
	public Character.CharacterName characterName;

	// Token: 0x040002DC RID: 732
	public int startingHealth = 40;

	// Token: 0x040002DD RID: 733
	public int defaultEnergyPerTurn = 3;

	// Token: 0x040002DE RID: 734
	public List<GameObject> startingObjects;

	// Token: 0x040002DF RID: 735
	public List<GameObject> startingObjectsForLimitedItemGet;

	// Token: 0x040002E0 RID: 736
	public List<RuntimeAnimatorController> animatorControllers;

	// Token: 0x040002E1 RID: 737
	public List<float> characterSelectorSizeRatio;

	// Token: 0x040002E2 RID: 738
	public List<float> yAdjustment;

	// Token: 0x040002E3 RID: 739
	public ModularBackpack.BackpackPieces backpackPieces;

	// Token: 0x040002E4 RID: 740
	[SerializeField]
	public Vector3[] decalPositions;

	// Token: 0x040002E5 RID: 741
	public Sprite mapSprite;

	// Token: 0x040002E6 RID: 742
	public List<Sprite> mapCharacterSprite;

	// Token: 0x040002E7 RID: 743
	public Sprite footstepSprite;

	// Token: 0x040002E8 RID: 744
	public Vector2 defaultBagSize;

	// Token: 0x040002E9 RID: 745
	public Vector2 endingBagSize;

	// Token: 0x040002EA RID: 746
	public Vector2 endingBagSizeDemo;

	// Token: 0x040002EB RID: 747
	public List<Character.LevelUp> levelUps;

	// Token: 0x040002EC RID: 748
	public List<ActionButtonManager.Type> buttonTypes;

	// Token: 0x020002B9 RID: 697
	[Serializable]
	public class LevelUp
	{
		// Token: 0x0400106A RID: 4202
		public bool demoEnabled = true;

		// Token: 0x0400106B RID: 4203
		public int xpRequired;

		// Token: 0x0400106C RID: 4204
		public List<Character.LevelUp.Reward> rewards;

		// Token: 0x0200048D RID: 1165
		[Serializable]
		public class Reward
		{
			// Token: 0x04001A8F RID: 6799
			public Character.LevelUp.Reward.RewardType rewardType;

			// Token: 0x04001A90 RID: 6800
			public int rewardValue;

			// Token: 0x020004C3 RID: 1219
			public enum RewardType
			{
				// Token: 0x04001CC3 RID: 7363
				NewSpace,
				// Token: 0x04001CC4 RID: 7364
				Component,
				// Token: 0x04001CC5 RID: 7365
				SpaceBlock,
				// Token: 0x04001CC6 RID: 7366
				Pets
			}
		}
	}

	// Token: 0x020002BA RID: 698
	public enum CharacterName
	{
		// Token: 0x0400106E RID: 4206
		Any,
		// Token: 0x0400106F RID: 4207
		Purse,
		// Token: 0x04001070 RID: 4208
		Satchel,
		// Token: 0x04001071 RID: 4209
		Tote,
		// Token: 0x04001072 RID: 4210
		CR8,
		// Token: 0x04001073 RID: 4211
		Pochette
	}
}
