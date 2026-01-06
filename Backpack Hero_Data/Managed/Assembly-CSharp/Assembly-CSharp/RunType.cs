using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000161 RID: 353
[CreateAssetMenu(fileName = "RunType", menuName = "ScriptableObjects/RunType")]
[ES3Serializable]
public class RunType : ScriptableObject
{
	// Token: 0x04000B7C RID: 2940
	[SerializeField]
	public bool hardMode;

	// Token: 0x04000B7D RID: 2941
	[SerializeField]
	public string runTypeLanguageKey;

	// Token: 0x04000B7E RID: 2942
	[SerializeField]
	public List<RunType> requiredToUnlock;

	// Token: 0x04000B7F RID: 2943
	[SerializeField]
	public List<Character.CharacterName> validForCharacters;

	// Token: 0x04000B80 RID: 2944
	public List<RunType.RunProperty> runProperties;

	// Token: 0x02000424 RID: 1060
	[Serializable]
	public class RunProperty
	{
		// Token: 0x060019A9 RID: 6569 RVA: 0x000D0A16 File Offset: 0x000CEC16
		public static implicit operator bool(RunType.RunProperty runProperty)
		{
			return runProperty != null;
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x000D0A1E File Offset: 0x000CEC1E
		public void Clone(RunType.RunProperty clone)
		{
			this.type = clone.type;
			this.value = clone.value;
			this.itemTypes = clone.itemTypes;
			this.assignedPrefabs = clone.assignedPrefabs;
		}

		// Token: 0x04001826 RID: 6182
		public RunType.RunProperty.Type type;

		// Token: 0x04001827 RID: 6183
		public int value;

		// Token: 0x04001828 RID: 6184
		public bool showOnCard = true;

		// Token: 0x04001829 RID: 6185
		public List<Item2.ItemType> itemTypes = new List<Item2.ItemType>();

		// Token: 0x0400182A RID: 6186
		public List<GameObject> assignedPrefabs = new List<GameObject>();

		// Token: 0x0400182B RID: 6187
		public List<GameObject> assignedGameObject = new List<GameObject>();

		// Token: 0x020004BE RID: 1214
		public enum Type
		{
			// Token: 0x04001C68 RID: 7272
			noXP,
			// Token: 0x04001C69 RID: 7273
			mustKeep,
			// Token: 0x04001C6A RID: 7274
			getExtraItem,
			// Token: 0x04001C6B RID: 7275
			increaseHP,
			// Token: 0x04001C6C RID: 7276
			increaseXP,
			// Token: 0x04001C6D RID: 7277
			increaseGold,
			// Token: 0x04001C6E RID: 7278
			increaseEnemyHealth,
			// Token: 0x04001C6F RID: 7279
			limitBackpackWidth,
			// Token: 0x04001C70 RID: 7280
			allItemsCommonOrUncommon,
			// Token: 0x04001C71 RID: 7281
			startWith,
			// Token: 0x04001C72 RID: 7282
			startFromMatt,
			// Token: 0x04001C73 RID: 7283
			cannotFind,
			// Token: 0x04001C74 RID: 7284
			chestsContainExtra,
			// Token: 0x04001C75 RID: 7285
			enemyAttacksMultipler,
			// Token: 0x04001C76 RID: 7286
			cannotFindItemOfType,
			// Token: 0x04001C77 RID: 7287
			bonusDamage,
			// Token: 0x04001C78 RID: 7288
			increaseLuck,
			// Token: 0x04001C79 RID: 7289
			changeStatusEffectsToDamage,
			// Token: 0x04001C7A RID: 7290
			setHP,
			// Token: 0x04001C7B RID: 7291
			replaceItemGetWithChest,
			// Token: 0x04001C7C RID: 7292
			replaceItemGetWithStore,
			// Token: 0x04001C7D RID: 7293
			itemSizeExact,
			// Token: 0x04001C7E RID: 7294
			itemSizeLarger,
			// Token: 0x04001C7F RID: 7295
			scalingEnergy,
			// Token: 0x04001C80 RID: 7296
			setEnergy,
			// Token: 0x04001C81 RID: 7297
			noRelics,
			// Token: 0x04001C82 RID: 7298
			oneFloorBeforeBoss,
			// Token: 0x04001C83 RID: 7299
			enemiesWontPoison,
			// Token: 0x04001C84 RID: 7300
			puzzleMode,
			// Token: 0x04001C85 RID: 7301
			dontStartWithStandard,
			// Token: 0x04001C86 RID: 7302
			enemiesWontCurse,
			// Token: 0x04001C87 RID: 7303
			startingBackpackSize,
			// Token: 0x04001C88 RID: 7304
			bossRush,
			// Token: 0x04001C89 RID: 7305
			replaceAllItems,
			// Token: 0x04001C8A RID: 7306
			startFromAssignedPrefab,
			// Token: 0x04001C8B RID: 7307
			unlimitedForges,
			// Token: 0x04001C8C RID: 7308
			youCanStillFindItemsOfExludedTypeIfTheyAlsoHaveThisType,
			// Token: 0x04001C8D RID: 7309
			cannotFindEvent,
			// Token: 0x04001C8E RID: 7310
			youCanSeeTheWholeMap,
			// Token: 0x04001C8F RID: 7311
			additionalEncounter,
			// Token: 0x04001C90 RID: 7312
			replaceEncounter,
			// Token: 0x04001C91 RID: 7313
			increaseChanceOfFindingItemType,
			// Token: 0x04001C92 RID: 7314
			doTutorial,
			// Token: 0x04001C93 RID: 7315
			startOnLevel,
			// Token: 0x04001C94 RID: 7316
			resetTutorials,
			// Token: 0x04001C95 RID: 7317
			doStoryIntro,
			// Token: 0x04001C96 RID: 7318
			metaProgressRun,
			// Token: 0x04001C97 RID: 7319
			runEndsAfterZone1,
			// Token: 0x04001C98 RID: 7320
			runEndsAfterZone2,
			// Token: 0x04001C99 RID: 7321
			runEndsAfterZone3,
			// Token: 0x04001C9A RID: 7322
			cryptDisabled,
			// Token: 0x04001C9B RID: 7323
			brambleDisabled,
			// Token: 0x04001C9C RID: 7324
			deepCavesDisabled,
			// Token: 0x04001C9D RID: 7325
			enchantedSwampDisabled,
			// Token: 0x04001C9E RID: 7326
			magmaCoreDisabled,
			// Token: 0x04001C9F RID: 7327
			frozenHeartDisabled,
			// Token: 0x04001CA0 RID: 7328
			storyModeProgressDisabled,
			// Token: 0x04001CA1 RID: 7329
			classicMatthew,
			// Token: 0x04001CA2 RID: 7330
			startWithItemFromTown,
			// Token: 0x04001CA3 RID: 7331
			finalRun,
			// Token: 0x04001CA4 RID: 7332
			stillSpawnPets
		}
	}
}
