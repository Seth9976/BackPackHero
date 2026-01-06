using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SaveSystem.States
{
	// Token: 0x020000B3 RID: 179
	public class ProgressState : State
	{
		// Token: 0x060004B8 RID: 1208 RVA: 0x000170B9 File Offset: 0x000152B9
		public new object Migrate()
		{
			return null;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x000170BC File Offset: 0x000152BC
		public static ProgressState Capture()
		{
			return new ProgressState
			{
				accomplishments = Singleton.instance.accomplishments,
				completedRuns = Singleton.instance.completedRuns
			};
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x000170E3 File Offset: 0x000152E3
		public new void Restore()
		{
			Singleton.instance.accomplishments = this.accomplishments;
			Singleton.instance.completedRuns = this.completedRuns;
		}

		// Token: 0x040003A6 RID: 934
		public static readonly Dictionary<int, Type> versionClasses = new Dictionary<int, Type> { 
		{
			1,
			typeof(ProgressState)
		} };

		// Token: 0x040003A7 RID: 935
		public new StateType type = StateType.Progress;

		// Token: 0x040003A8 RID: 936
		public new int version = 1;

		// Token: 0x040003A9 RID: 937
		public List<Singleton.Accomplishment> accomplishments = new List<Singleton.Accomplishment>();

		// Token: 0x040003AA RID: 938
		public List<string> completedRuns = new List<string>();

		// Token: 0x02000128 RID: 296
		[JsonConverter(typeof(JSONStateSerializer.DefaultUnknownEnumConverter))]
		public enum Unlock
		{
			// Token: 0x04000532 RID: 1330
			Unknown,
			// Token: 0x04000533 RID: 1331
			PriestCharacter,
			// Token: 0x04000534 RID: 1332
			garlic,
			// Token: 0x04000535 RID: 1333
			stakes
		}

		// Token: 0x02000129 RID: 297
		[JsonConverter(typeof(JSONStateSerializer.DefaultUnknownEnumConverter))]
		public enum Accomplishment
		{
			// Token: 0x04000537 RID: 1335
			Unknown,
			// Token: 0x04000538 RID: 1336
			TutorialCompleted,
			// Token: 0x04000539 RID: 1337
			EnemiesKilled,
			// Token: 0x0400053A RID: 1338
			RoomsSurvived,
			// Token: 0x0400053B RID: 1339
			RunsCompleted,
			// Token: 0x0400053C RID: 1340
			CardsPlayed,
			// Token: 0x0400053D RID: 1341
			CardsDrawn,
			// Token: 0x0400053E RID: 1342
			TimesDied,
			// Token: 0x0400053F RID: 1343
			UtilitiesUsed,
			// Token: 0x04000540 RID: 1344
			AttacksUsed,
			// Token: 0x04000541 RID: 1345
			MovementsUsed,
			// Token: 0x04000542 RID: 1346
			GhoulsKilled = 101,
			// Token: 0x04000543 RID: 1347
			StakesFired,
			// Token: 0x04000544 RID: 1348
			GarlicsUsed = 201,
			// Token: 0x04000545 RID: 1349
			FrozenCardsUsed,
			// Token: 0x04000546 RID: 1350
			FireCardsUsed,
			// Token: 0x04000547 RID: 1351
			StakesCardsUsed,
			// Token: 0x04000548 RID: 1352
			StandardBattleNunRun = 301
		}
	}
}
