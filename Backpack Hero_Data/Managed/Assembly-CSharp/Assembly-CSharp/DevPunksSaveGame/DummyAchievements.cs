using System;
using DevPunksSaveGame.Interfaces;
using UnityEngine;

namespace DevPunksSaveGame
{
	// Token: 0x0200022D RID: 557
	public class DummyAchievements : AchievementInterface
	{
		// Token: 0x06001254 RID: 4692 RVA: 0x000AD474 File Offset: 0x000AB674
		public void UnlockAchievement(AchievementIds ids, int value = 0)
		{
			if (value != 0)
			{
				Debug.Log(string.Format("Unlock achievement: {0}. Unlocked {1} of {2}, percentage: {3}%", new object[]
				{
					ids.SteamId,
					value,
					ids.UnlockCount,
					(long)(100 * value) / (long)((ulong)ids.UnlockCount)
				}));
				return;
			}
			Debug.Log("Unlock achievement: " + ids.SteamId);
		}
	}
}
