using System;

namespace DevPunksSaveGame
{
	// Token: 0x0200022A RID: 554
	public static class AchievementService
	{
		// Token: 0x0600121C RID: 4636 RVA: 0x000AC738 File Offset: 0x000AA938
		public static void Unlock(string achievementId)
		{
			ConsoleWrapper.Instance.UnlockAchievement(achievementId, 0);
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x000AC746 File Offset: 0x000AA946
		public static void Aggregate(string achievementId, int value)
		{
			ConsoleWrapper.Instance.UnlockAchievement(achievementId, value);
		}
	}
}
