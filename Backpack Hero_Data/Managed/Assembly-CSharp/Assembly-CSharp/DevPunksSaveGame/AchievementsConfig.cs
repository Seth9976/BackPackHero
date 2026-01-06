using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DevPunksSaveGame
{
	// Token: 0x0200022F RID: 559
	public static class AchievementsConfig
	{
		// Token: 0x06001272 RID: 4722 RVA: 0x000AD5A8 File Offset: 0x000AB7A8
		internal static AchievementIds GetAchievementIds(string achievement)
		{
			AchievementIds achievementIds = AchievementsConfig._achievements.FirstOrDefault((AchievementIds a) => a.SteamId == achievement);
			if (achievementIds.SteamId == achievement)
			{
				return achievementIds;
			}
			Debug.LogWarning("GetAchievementIds achievement " + achievement + " not found");
			return achievementIds;
		}

		// Token: 0x04000E77 RID: 3703
		internal static readonly List<AchievementIds> _achievements = new List<AchievementIds>
		{
			new AchievementIds
			{
				SteamId = "GotPradasLocket",
				XboxId = "1",
				PS4Id = 1,
				PS5Id = 1,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "UnlockedDeepCave",
				XboxId = "2",
				PS4Id = 2,
				PS5Id = 2,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "ItemDiscoverer",
				XboxId = "3",
				PS4Id = 3,
				PS5Id = 3,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "UnlockedBramble",
				XboxId = "4",
				PS4Id = 4,
				PS5Id = 4,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "Popular",
				XboxId = "5",
				PS4Id = 5,
				PS5Id = 5,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "BuiltTownHall",
				XboxId = "6",
				PS4Id = 6,
				PS5Id = 6,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "Multipurpose",
				XboxId = "7",
				PS4Id = 7,
				PS5Id = 7,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "ShivMaster",
				XboxId = "8",
				PS4Id = 8,
				PS5Id = 8,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "UnlockedSatchel",
				XboxId = "9",
				PS4Id = 9,
				PS5Id = 9,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "UnlockedEnchantedSwamp",
				XboxId = "10",
				PS4Id = 10,
				PS5Id = 10,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "Glutton",
				XboxId = "11",
				PS4Id = 11,
				PS5Id = 11,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "FullyArmored",
				XboxId = "12",
				PS4Id = 12,
				PS5Id = 12,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "ItemExpert",
				XboxId = "13",
				PS4Id = 13,
				PS5Id = 13,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "StatusMaster",
				XboxId = "14",
				PS4Id = 14,
				PS5Id = 14,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "Mage",
				XboxId = "15",
				PS4Id = 15,
				PS5Id = 15,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "UnlockedTote",
				XboxId = "16",
				PS4Id = 16,
				PS5Id = 16,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "Builder",
				XboxId = "17",
				PS4Id = 17,
				PS5Id = 17,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "UnlockedMagmaCore",
				XboxId = "18",
				PS4Id = 18,
				PS5Id = 18,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "PoisonStack",
				XboxId = "19",
				PS4Id = 19,
				PS5Id = 19,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "Haggler",
				XboxId = "20",
				PS4Id = 20,
				PS5Id = 20,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "UnlockedFrozenHeart",
				XboxId = "21",
				PS4Id = 21,
				PS5Id = 21,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "UnlockedPochette",
				XboxId = "22",
				PS4Id = 22,
				PS5Id = 22,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "UnlockedCR8",
				XboxId = "23",
				PS4Id = 23,
				PS5Id = 23,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "WonAQuickRun",
				XboxId = "24",
				PS4Id = 24,
				PS5Id = 24,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "WonAQuickRunAsPurse",
				XboxId = "25",
				PS4Id = 25,
				PS5Id = 25,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "Archer",
				XboxId = "26",
				PS4Id = 26,
				PS5Id = 26,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "ItemHero",
				XboxId = "27",
				PS4Id = 27,
				PS5Id = 27,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "WonAQuickRunAsSatchel",
				XboxId = "28",
				PS4Id = 28,
				PS5Id = 28,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "BeatAHardMode",
				XboxId = "29",
				PS4Id = 29,
				PS5Id = 29,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "CursedRun",
				XboxId = "30",
				PS4Id = 30,
				PS5Id = 30,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "WonAQuickRunAsCR8",
				XboxId = "31",
				PS4Id = 31,
				PS5Id = 31,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "WonAQuickRunAsPochette",
				XboxId = "32",
				PS4Id = 32,
				PS5Id = 32,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "WonAQuickRunAsTote",
				XboxId = "33",
				PS4Id = 33,
				PS5Id = 33,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "SurvivedTheRaid",
				XboxId = "34",
				PS4Id = 34,
				PS5Id = 34,
				UnlockCount = 0U
			},
			new AchievementIds
			{
				SteamId = "DefeatedChaos",
				XboxId = "35",
				PS4Id = 35,
				PS5Id = 35,
				UnlockCount = 0U
			}
		};
	}
}
