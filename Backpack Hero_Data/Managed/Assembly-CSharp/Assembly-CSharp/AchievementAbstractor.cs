using System;
using Steamworks;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class AchievementAbstractor : MonoBehaviour
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	private void Awake()
	{
		if (AchievementAbstractor.instance == null)
		{
			AchievementAbstractor.instance = this;
			return;
		}
		Object.Destroy(this);
	}

	// Token: 0x06000002 RID: 2 RVA: 0x0000206C File Offset: 0x0000026C
	private void Start()
	{
		this.method = AchievementAbstractor.AchivementMethod.Steam;
		this.SetupSettings();
		this.UpdateFile();
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002084 File Offset: 0x00000284
	private void SetupSettings()
	{
		if (!this.achievementsEnabled)
		{
			return;
		}
		string text = Application.persistentDataPath + "/";
		this.settings = new ES3Settings(null, null);
		this.settings.path = text + "bphAchievementData.sav";
	}

	// Token: 0x06000004 RID: 4 RVA: 0x000020D0 File Offset: 0x000002D0
	private void UpdateFile()
	{
		if (!this.achievementsEnabled)
		{
			return;
		}
		this.SetupSettings();
		if (this.method == AchievementAbstractor.AchivementMethod.Steam)
		{
			foreach (string text in AchievementAbstractor.Achievements)
			{
				ES3.Save<bool>(text, this.AchievementUnlocked(text), this.settings);
			}
		}
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00002120 File Offset: 0x00000320
	public void ConsiderBagAchievements()
	{
		if (!this.achievementsEnabled)
		{
			return;
		}
		int num = 0;
		int num2 = 0;
		bool flag = false;
		bool flag2 = false;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		bool flag3 = false;
		bool flag4 = false;
		int num7 = 0;
		bool flag5 = false;
		foreach (Item2 item in Item2.allItems)
		{
			if (item && item.itemMovement && item.itemMovement.inGrid && !item.destroyed)
			{
				if (Item2.GetDisplayName(item.name) == Item2.GetDisplayName(this.shiv.name))
				{
					num++;
				}
				if (item.itemType.Contains(Item2.ItemType.Structure))
				{
					num2++;
				}
				if (Item2.GetDisplayName(item.name) == Item2.GetDisplayName(this.shoeHat.name) && item.transform.rotation.eulerAngles.z == 0f)
				{
					flag = true;
				}
				if (Item2.GetDisplayName(item.name) == Item2.GetDisplayName(this.shoeHat.name) && item.transform.rotation.eulerAngles.z == 180f)
				{
					flag2 = true;
				}
				if (item.itemType.Contains(Item2.ItemType.ManaStone))
				{
					num3++;
				}
				if (item.itemType.Contains(Item2.ItemType.Arrow))
				{
					num4++;
				}
				if (item.itemType.Contains(Item2.ItemType.Consumable))
				{
					num5++;
				}
				if (item.itemType.Contains(Item2.ItemType.Curse))
				{
					num6++;
				}
				if (item.itemType.Contains(Item2.ItemType.Helmet))
				{
					flag3 = true;
				}
				if (item.itemType.Contains(Item2.ItemType.Clothing))
				{
					flag4 = true;
				}
				if (item.itemType.Contains(Item2.ItemType.Glove))
				{
					num7++;
				}
				if (item.itemType.Contains(Item2.ItemType.Footwear))
				{
					flag5 = true;
				}
			}
		}
		if (num >= 5)
		{
			this.ConsiderAchievement("ShivMaster");
		}
		if (num2 >= 5)
		{
			this.ConsiderAchievement("Builder");
		}
		if (flag && flag2)
		{
			this.ConsiderAchievement("Multipurpose");
		}
		if (num3 >= 5)
		{
			this.ConsiderAchievement("Mage");
		}
		if (num4 >= 5)
		{
			this.ConsiderAchievement("Archer");
		}
		if (num5 >= 7)
		{
			this.ConsiderAchievement("Glutton");
		}
		if (num6 >= 5)
		{
			this.ConsiderAchievement("CursedRun");
		}
		if (flag3 && flag4 && num7 >= 2 && flag5)
		{
			this.ConsiderAchievement("FullyArmored");
		}
	}

	// Token: 0x06000006 RID: 6 RVA: 0x000023E0 File Offset: 0x000005E0
	public void ConsiderAchievement(string id)
	{
		if (!this.achievementsEnabled)
		{
			return;
		}
		bool flag = false;
		string[] achievements = AchievementAbstractor.Achievements;
		for (int i = 0; i < achievements.Length; i++)
		{
			if (achievements[i] == id)
			{
				flag = true;
			}
		}
		if (!flag)
		{
			Debug.LogError("Achievement not found: " + id);
			return;
		}
		try
		{
			if (!this.AchievementUnlocked(id))
			{
				this.UnlockAchievement(id);
			}
		}
		catch (Exception ex)
		{
			string text = "Error unlocking achievement: ";
			string text2 = " ";
			Exception ex2 = ex;
			Debug.LogError(text + id + text2 + ((ex2 != null) ? ex2.ToString() : null));
		}
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00002478 File Offset: 0x00000678
	private bool AchievementUnlocked(string id)
	{
		try
		{
			if (!this.achievementsEnabled)
			{
				return false;
			}
			bool flag = false;
			if (this.method == AchievementAbstractor.AchivementMethod.Steam)
			{
				SteamUserStats.GetAchievement(id, out flag);
			}
			else if (this.method != AchievementAbstractor.AchivementMethod.Epic && this.method != AchievementAbstractor.AchivementMethod.GOG)
			{
				flag = ES3.Load<bool>(id, false, this.settings);
			}
			return flag;
		}
		catch (Exception ex)
		{
			string text = "Error checking achievement: ";
			string text2 = " ";
			Exception ex2 = ex;
			Debug.LogError(text + id + text2 + ((ex2 != null) ? ex2.ToString() : null));
		}
		return false;
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002504 File Offset: 0x00000704
	private void UnlockAchievement(string id)
	{
		if (!this.achievementsEnabled)
		{
			return;
		}
		try
		{
			switch (this.method)
			{
			case AchievementAbstractor.AchivementMethod.Steam:
				if (SteamManager.Initialized)
				{
					Debug.Log("Unlocking achievement via Steam: " + id);
					SteamUserStats.SetAchievement(id);
					SteamUserStats.StoreStats();
				}
				else
				{
					this.UpdateFile();
				}
				break;
			case AchievementAbstractor.AchivementMethod.Epic:
			case AchievementAbstractor.AchivementMethod.GOG:
				break;
			default:
				ES3.Save<bool>(id, true, this.settings);
				break;
			}
			Debug.Log("Achievement unlocked: " + id);
		}
		catch (Exception ex)
		{
			string text = "Error unlocking achievement: ";
			string text2 = " ";
			Exception ex2 = ex;
			Debug.LogError(text + id + text2 + ((ex2 != null) ? ex2.ToString() : null));
		}
	}

	// Token: 0x04000001 RID: 1
	[SerializeField]
	private bool achievementsEnabled = true;

	// Token: 0x04000002 RID: 2
	public static AchievementAbstractor instance;

	// Token: 0x04000003 RID: 3
	public AchievementAbstractor.AchivementMethod method = AchievementAbstractor.AchivementMethod.Local;

	// Token: 0x04000004 RID: 4
	public static readonly string[] Achievements = new string[]
	{
		"WonAQuickRun", "WonAQuickRunAsPurse", "WonAQuickRunAsSatchel", "WonAQuickRunAsTote", "WonAQuickRunAsPochette", "WonAQuickRunAsCR8", "GotPradasLocket", "BuiltTownHall", "SurvivedTheRaid", "UnlockedSatchel",
		"UnlockedTote", "UnlockedPochette", "UnlockedCR8", "UnlockedDeepCave", "UnlockedBramble", "UnlockedEnchantedSwamp", "UnlockedMagmaCore", "UnlockedFrozenHeart", "DefeatedChaos", "BeatAHardMode",
		"ItemDiscoverer", "ItemExpert", "ItemHero", "Popular", "ShivMaster", "Builder", "Haggler", "Multipurpose", "Mage", "Archer",
		"Glutton", "StatusMaster", "CursedRun", "PoisonStack", "FullyArmored"
	};

	// Token: 0x04000005 RID: 5
	[SerializeField]
	private GameObject shiv;

	// Token: 0x04000006 RID: 6
	[SerializeField]
	private GameObject shoeHat;

	// Token: 0x04000007 RID: 7
	private ES3Settings settings;

	// Token: 0x02000237 RID: 567
	public enum AchivementMethod
	{
		// Token: 0x04000E89 RID: 3721
		Steam,
		// Token: 0x04000E8A RID: 3722
		Epic,
		// Token: 0x04000E8B RID: 3723
		GOG,
		// Token: 0x04000E8C RID: 3724
		Local
	}
}
