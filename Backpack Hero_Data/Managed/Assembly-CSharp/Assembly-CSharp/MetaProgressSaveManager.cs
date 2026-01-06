using System;
using System.Collections.Generic;
using System.Linq;
using DevPunksSaveGame;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000165 RID: 357
public class MetaProgressSaveManager : MonoBehaviour
{
	// Token: 0x06000E4D RID: 3661 RVA: 0x0008F1EC File Offset: 0x0008D3EC
	private void OnDestroy()
	{
		if (MetaProgressSaveManager.main == this)
		{
			MetaProgressSaveManager.main = null;
		}
	}

	// Token: 0x06000E4E RID: 3662 RVA: 0x0008F201 File Offset: 0x0008D401
	private void Awake()
	{
		if (MetaProgressSaveManager.main == null)
		{
			MetaProgressSaveManager.main = this;
			return;
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06000E4F RID: 3663 RVA: 0x0008F222 File Offset: 0x0008D422
	private void Start()
	{
	}

	// Token: 0x06000E50 RID: 3664 RVA: 0x0008F224 File Offset: 0x0008D424
	private void Update()
	{
		if (this.loadMetaProgress)
		{
			this.loadMetaProgress = false;
			this.Load();
		}
		if (this.loadResearchNew)
		{
			this.loadResearchNew = false;
			this.researchNamesComplete = new List<string>();
		}
	}

	// Token: 0x06000E51 RID: 3665 RVA: 0x0008F258 File Offset: 0x0008D458
	public int GetAllResearches()
	{
		if (this.itemAtlas == null)
		{
			Debug.LogError("itemAtlas is null");
		}
		int num = 0;
		foreach (GameObject gameObject in this.itemAtlas.overworldResearchUI)
		{
			if (gameObject == null)
			{
				Debug.LogError("Encountered null in itemAtlas.overworldResearchUI");
			}
			else
			{
				Overworld_BuildingInterface component = gameObject.GetComponent<Overworld_BuildingInterface>();
				if (component)
				{
					num += component.researches.Count;
				}
			}
		}
		return num;
	}

	// Token: 0x06000E52 RID: 3666 RVA: 0x0008F2F8 File Offset: 0x0008D4F8
	public static MetaProgressSaveManager.MetaProgressMarker TranslateStringToMarker(string s)
	{
		foreach (object obj in Enum.GetValues(typeof(MetaProgressSaveManager.MetaProgressMarker)))
		{
			MetaProgressSaveManager.MetaProgressMarker metaProgressMarker = (MetaProgressSaveManager.MetaProgressMarker)obj;
			if (metaProgressMarker.ToString().ToLower().Trim() == s.ToLower().Trim())
			{
				return metaProgressMarker;
			}
		}
		return MetaProgressSaveManager.MetaProgressMarker.none;
	}

	// Token: 0x06000E53 RID: 3667 RVA: 0x0008F384 File Offset: 0x0008D584
	public static bool ContainsString(List<string> list, string s)
	{
		using (List<string>.Enumerator enumerator = list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.ToLower().Trim() == s.ToLower().Trim())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000E54 RID: 3668 RVA: 0x0008F3F0 File Offset: 0x0008D5F0
	public static void AddBoolCondition(List<MetaProgressSaveManager.MetaProgressCondition> conditions, MetaProgressSaveManager.MetaProgressMarker marker)
	{
		conditions.Add(new MetaProgressSaveManager.MetaProgressCondition
		{
			marker = marker,
			isTrue = true,
			type = MetaProgressSaveManager.MetaProgressCondition.Type.Boolean
		});
	}

	// Token: 0x06000E55 RID: 3669 RVA: 0x0008F420 File Offset: 0x0008D620
	public static bool ConditionsMet(List<MetaProgressSaveManager.MetaProgressCondition> conditions)
	{
		using (List<MetaProgressSaveManager.MetaProgressCondition>.Enumerator enumerator = conditions.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!MetaProgressSaveManager.ConditionMet(enumerator.Current))
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x06000E56 RID: 3670 RVA: 0x0008F474 File Offset: 0x0008D674
	public static bool ConditionMet(MetaProgressSaveManager.MetaProgressCondition m)
	{
		if (m.storyModeOnly && !Singleton.Instance.storyMode)
		{
			return false;
		}
		if (m.type == MetaProgressSaveManager.MetaProgressCondition.Type.Boolean)
		{
			if (MetaProgressSaveManager.main.HasMetaProgressMarker(m.marker) == m.isTrue)
			{
				return true;
			}
		}
		else if (m.type == MetaProgressSaveManager.MetaProgressCondition.Type.Integer)
		{
			if (MetaProgressSaveManager.main.GetMetaProgressMarkerValue(m.marker) >= m.greaterThanOrEqualToValue && m.isTrue)
			{
				return true;
			}
			if (MetaProgressSaveManager.main.GetMetaProgressMarkerValue(m.marker) < m.greaterThanOrEqualToValue && !m.isTrue)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000E57 RID: 3671 RVA: 0x0008F508 File Offset: 0x0008D708
	public void AddRunEvent(MetaProgressSaveManager.LastRun.RunEvents runEvent)
	{
		if (runEvent == MetaProgressSaveManager.LastRun.RunEvents.none)
		{
			return;
		}
		if (this.lastRun == null)
		{
			this.lastRun = new MetaProgressSaveManager.LastRun();
		}
		if (this.lastRun.events == null)
		{
			this.lastRun.events = new List<MetaProgressSaveManager.LastRun.RunEvents>();
		}
		if (!this.lastRun.events.Contains(runEvent))
		{
			this.lastRun.events.Add(runEvent);
		}
	}

	// Token: 0x06000E58 RID: 3672 RVA: 0x0008F570 File Offset: 0x0008D770
	public void AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker m)
	{
		if (m == MetaProgressSaveManager.MetaProgressMarker.none)
		{
			return;
		}
		MetaProgressSaveManager.MetaProgressTracker metaProgressMarker = this.GetMetaProgressMarker(m);
		if (metaProgressMarker != null)
		{
			metaProgressMarker.isTemporary = false;
			return;
		}
		MetaProgressSaveManager.MetaProgressTracker metaProgressTracker = new MetaProgressSaveManager.MetaProgressTracker();
		metaProgressTracker.metaProgressMarker = m;
		metaProgressTracker.number = 0;
		this.metaProgressTrackers.Add(metaProgressTracker);
	}

	// Token: 0x06000E59 RID: 3673 RVA: 0x0008F5B4 File Offset: 0x0008D7B4
	public void RemoveTemporaryMarkers()
	{
		this.metaProgressTrackers.RemoveAll((MetaProgressSaveManager.MetaProgressTracker x) => x.isTemporary);
	}

	// Token: 0x06000E5A RID: 3674 RVA: 0x0008F5E4 File Offset: 0x0008D7E4
	public bool HasOneOfAKindItem(Item2 item)
	{
		return this.storedItems.Where((string x) => Item2.GetDisplayName(x) == Item2.GetDisplayName(item.name)).Count<string>() > 1 || this.oneOfAKindItems.Where((string x) => Item2.GetDisplayName(x) == Item2.GetDisplayName(item.name)).Count<string>() > 0;
	}

	// Token: 0x06000E5B RID: 3675 RVA: 0x0008F644 File Offset: 0x0008D844
	public void AddNewResearch(string name, string nameAndValues)
	{
		for (int i = 0; i < this.researchNamesComplete.Count; i++)
		{
			if (this.researchNamesComplete[i].Contains(name))
			{
				this.researchNamesComplete[i] = nameAndValues;
				return;
			}
		}
		this.researchNamesComplete.Add(nameAndValues);
	}

	// Token: 0x06000E5C RID: 3676 RVA: 0x0008F698 File Offset: 0x0008D898
	public string GetNewResearch(string name)
	{
		for (int i = 0; i < this.researchNamesComplete.Count; i++)
		{
			string text = this.researchNamesComplete[i];
			if (text.Contains(name) && text.Replace(name, "").Replace("1", "").Replace("0", "")
				.Replace("!", "")
				.Length <= 0)
			{
				return text;
			}
		}
		return "";
	}

	// Token: 0x06000E5D RID: 3677 RVA: 0x0008F71C File Offset: 0x0008D91C
	public void UnlockCostume(RuntimeAnimatorController controller)
	{
		string name = controller.name;
		if (!this.availableCostumes.Contains(name))
		{
			this.availableCostumes.Add(name);
		}
	}

	// Token: 0x06000E5E RID: 3678 RVA: 0x0008F74C File Offset: 0x0008D94C
	public bool CostumeUnlocked(RuntimeAnimatorController controller)
	{
		string name = controller.name;
		using (List<string>.Enumerator enumerator = this.availableCostumes.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current == name)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000E5F RID: 3679 RVA: 0x0008F7B0 File Offset: 0x0008D9B0
	public void ConsiderAdditionalMarkers()
	{
		foreach (Overworld_ResourceManager.Resource resource in this.resources)
		{
			if (resource.type == Overworld_ResourceManager.Resource.Type.Population && resource.amount >= 12)
			{
				this.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.has12population);
			}
			if (resource.type == Overworld_ResourceManager.Resource.Type.Population && resource.amount >= 25)
			{
				this.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.has25population);
			}
			if (resource.type == Overworld_ResourceManager.Resource.Type.Population && resource.amount >= 50)
			{
				this.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.hasEnoughPopulationForClementine);
			}
		}
		if (this.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.runsWon))
		{
			this.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedHazards);
		}
		else if (this.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.beatDeepCave))
		{
			this.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedCurses);
		}
		int researchesComplete = this.GetResearchesComplete();
		int allResearches = this.GetAllResearches();
		if (researchesComplete >= 1)
		{
			this.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.readyForChest);
			return;
		}
		if (researchesComplete >= allResearches / 2)
		{
			this.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.readyForRarePrimrose);
			return;
		}
		if (researchesComplete >= allResearches)
		{
			this.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.readyForGoldPile);
		}
	}

	// Token: 0x06000E60 RID: 3680 RVA: 0x0008F8B4 File Offset: 0x0008DAB4
	public void CalculatePopulation()
	{
		int num = 0;
		foreach (Overworld_Structure overworld_Structure in Overworld_Structure.structures)
		{
			if (!overworld_Structure.isDragging || !overworld_Structure.isBuildingFromSolidClick)
			{
				num += overworld_Structure.populationAdd;
			}
		}
		using (List<Overworld_NPC>.Enumerator enumerator2 = Overworld_NPC.npcs.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				if (enumerator2.Current.portraitSprite)
				{
					num++;
				}
			}
		}
		if (num >= 40)
		{
			AchievementAbstractor.instance.ConsiderAchievement("Popular");
		}
		Overworld_ResourceManager.Resource resource = this.resources.Find((Overworld_ResourceManager.Resource x) => x.type == Overworld_ResourceManager.Resource.Type.Population);
		if (resource != null)
		{
			resource.amount = num;
		}
		else
		{
			Overworld_ResourceManager.Resource resource2 = new Overworld_ResourceManager.Resource
			{
				type = Overworld_ResourceManager.Resource.Type.Population,
				amount = num
			};
			this.resources.Add(resource2);
		}
		if (Overworld_ResourceManager.main)
		{
			Overworld_ResourceManager.main.UpdateCounters();
		}
	}

	// Token: 0x06000E61 RID: 3681 RVA: 0x0008F9E8 File Offset: 0x0008DBE8
	private int GetResearchesComplete()
	{
		int num = 0;
		using (List<string>.Enumerator enumerator = this.researchNamesComplete.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.Contains("0"))
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x06000E62 RID: 3682 RVA: 0x0008FA48 File Offset: 0x0008DC48
	public int GetResourceAmount(Overworld_ResourceManager.Resource.Type resourceType)
	{
		foreach (Overworld_ResourceManager.Resource resource in this.resources)
		{
			if (resource.type == resourceType)
			{
				return resource.amount;
			}
		}
		return 0;
	}

	// Token: 0x06000E63 RID: 3683 RVA: 0x0008FAAC File Offset: 0x0008DCAC
	public static void AddResourcesToResourceList(List<MetaProgressSaveManager.ResourceToAdd> resourceToAdds, List<Overworld_ResourceManager.Resource> resources)
	{
		foreach (MetaProgressSaveManager.ResourceToAdd resourceToAdd in resourceToAdds)
		{
			bool flag = false;
			foreach (Overworld_ResourceManager.Resource resource in resources)
			{
				if (resource.type == resourceToAdd.type)
				{
					resource.amount += resourceToAdd.amount;
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				resources.Add(new Overworld_ResourceManager.Resource
				{
					type = resourceToAdd.type,
					amount = resourceToAdd.amount
				});
			}
		}
	}

	// Token: 0x06000E64 RID: 3684 RVA: 0x0008FB88 File Offset: 0x0008DD88
	public void RemoveRunSave()
	{
		ES3.DeleteFile(Application.persistentDataPath + "/" + "bphStoryModeRun" + Singleton.Instance.storyModeSlot.ToString() + ".sav");
	}

	// Token: 0x06000E65 RID: 3685 RVA: 0x0008FBBC File Offset: 0x0008DDBC
	private void AddResource(Overworld_ResourceManager.Resource.Type type)
	{
		MetaProgressSaveManager.ResourceToAdd resourceToAdd = new MetaProgressSaveManager.ResourceToAdd();
		resourceToAdd.type = type;
		resourceToAdd.amount = 0;
		resourceToAdd.currentEfficiencyBonus = 0f;
		this.resourcesToAdd.Add(resourceToAdd);
	}

	// Token: 0x06000E66 RID: 3686 RVA: 0x0008FBF4 File Offset: 0x0008DDF4
	public void CalculatePassiveGenerationRate()
	{
		this.resourcesToAdd.Clear();
		this.AddResource(Overworld_ResourceManager.Resource.Type.Treasure);
		this.AddResource(Overworld_ResourceManager.Resource.Type.Food);
		this.AddResource(Overworld_ResourceManager.Resource.Type.BuildingMaterial);
		Overworld_Structure.AllStructuresApplyAllModifiers();
		foreach (Overworld_Structure overworld_Structure in Overworld_Structure.structures)
		{
			if (!overworld_Structure.isDragging)
			{
				overworld_Structure.GetEffectTotals();
				foreach (Overworld_ResourceManager.Resource resource in overworld_Structure.resourcesToAddEachRun)
				{
					foreach (MetaProgressSaveManager.ResourceToAdd resourceToAdd in this.resourcesToAdd)
					{
						if (resourceToAdd.type == resource.type)
						{
							int num = Mathf.RoundToInt((float)resource.amount);
							int num2 = Mathf.RoundToInt((float)resource.amount * overworld_Structure.currentEfficiencyBonus / 100f) - num;
							resourceToAdd.amount += num;
							resourceToAdd.currentEfficiencyBonus += (float)num2;
						}
					}
				}
			}
		}
		Overworld_ResourceManager.main.UpdateResourcesToGain();
	}

	// Token: 0x06000E67 RID: 3687 RVA: 0x0008FD60 File Offset: 0x0008DF60
	public void CompleteResearch(MetaProgressSaveManager.Research r)
	{
		this.researchNamesComplete.Add(r.item.name);
		this.AddMetaProgressMarker(r.metaProgressMarker);
		if (r.objsToUnlock.Count > 0)
		{
			MetaProgressSaveManager.main.UnlockItems(r.objsToUnlock.ConvertAll<Item2>((GameObject x) => x.GetComponent<Item2>()));
			Overworld_Manager.main.OpenNewItemsWindow(r.objsToUnlock.ConvertAll<Item2>((GameObject x) => x.GetComponent<Item2>()));
		}
		foreach (ScriptableObject scriptableObject in r.assetsToUnlock)
		{
			if (scriptableObject is Missions)
			{
				Missions missions = (Missions)scriptableObject;
				if (!this.missionsUnlocked.Contains(missions.name))
				{
					this.missionsUnlocked.Add(missions.name);
					Overworld_Manager.main.OpenNewMissionWindow(missions);
				}
			}
		}
	}

	// Token: 0x06000E68 RID: 3688 RVA: 0x0008FE84 File Offset: 0x0008E084
	public List<Overworld_ResourceManager.Resource> GetResoucesToGain()
	{
		List<Overworld_ResourceManager.Resource> list = new List<Overworld_ResourceManager.Resource>();
		foreach (MetaProgressSaveManager.ResourceToAdd resourceToAdd in this.resourcesToAdd)
		{
			list.Add(new Overworld_ResourceManager.Resource
			{
				type = resourceToAdd.type,
				amount = Mathf.RoundToInt((float)resourceToAdd.amount + resourceToAdd.currentEfficiencyBonus)
			});
		}
		return list;
	}

	// Token: 0x06000E69 RID: 3689 RVA: 0x0008FF0C File Offset: 0x0008E10C
	public bool ResearchComplete(MetaProgressSaveManager.Research r)
	{
		return this.researchNamesComplete.Contains(r.item.name);
	}

	// Token: 0x06000E6A RID: 3690 RVA: 0x0008FF24 File Offset: 0x0008E124
	public MetaProgressSaveManager.Research GetResearch(string itemName)
	{
		foreach (MetaProgressSaveManager.Research research in this.researchList.researchList)
		{
			if (research.item && research.item.name != null && !(research.item.name == "") && Item2.GetDisplayName(research.item.name) == Item2.GetDisplayName(itemName))
			{
				foreach (Overworld_ResourceManager.Resource resource in research.resources)
				{
					resource.amount = Mathf.Abs(resource.amount);
				}
				return research;
			}
		}
		return null;
	}

	// Token: 0x06000E6B RID: 3691 RVA: 0x00090020 File Offset: 0x0008E220
	public void AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker m, int value)
	{
		this.AddMetaProgressMarker(m);
		this.SetMetaProgressMarkerValue(m, value);
	}

	// Token: 0x06000E6C RID: 3692 RVA: 0x00090034 File Offset: 0x0008E234
	public void DisableIntroMetaProgressMarkers()
	{
		for (int i = 0; i < this.metaProgressTrackers.Count; i++)
		{
			if (this.metaProgressTrackers[i].metaProgressMarker == MetaProgressSaveManager.MetaProgressMarker.firstConversationWithZaar || this.metaProgressTrackers[i].metaProgressMarker == MetaProgressSaveManager.MetaProgressMarker.firstBuildingBuilt || this.metaProgressTrackers[i].metaProgressMarker == MetaProgressSaveManager.MetaProgressMarker.firstRunStartedFromOverworld || this.metaProgressTrackers[i].metaProgressMarker == MetaProgressSaveManager.MetaProgressMarker.destroyedOldShop || this.metaProgressTrackers[i].metaProgressMarker == MetaProgressSaveManager.MetaProgressMarker.talkedToZaarAfterDestruction)
			{
				this.metaProgressTrackers.RemoveAt(i);
				i--;
			}
		}
	}

	// Token: 0x06000E6D RID: 3693 RVA: 0x000900D0 File Offset: 0x0008E2D0
	public void AddMetaProgressMarker(List<MetaProgressSaveManager.MetaProgressMarker> ms)
	{
		foreach (MetaProgressSaveManager.MetaProgressMarker metaProgressMarker in ms)
		{
			this.AddMetaProgressMarker(metaProgressMarker);
		}
	}

	// Token: 0x06000E6E RID: 3694 RVA: 0x00090120 File Offset: 0x0008E320
	public bool HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker m)
	{
		return this.GetMetaProgressMarker(m) != null;
	}

	// Token: 0x06000E6F RID: 3695 RVA: 0x00090130 File Offset: 0x0008E330
	public void AddMetaProgressMarkerTemporary(MetaProgressSaveManager.MetaProgressMarker m)
	{
		if (m == MetaProgressSaveManager.MetaProgressMarker.none)
		{
			return;
		}
		if (this.HasMetaProgressMarker(m))
		{
			return;
		}
		MetaProgressSaveManager.MetaProgressTracker metaProgressTracker = new MetaProgressSaveManager.MetaProgressTracker();
		metaProgressTracker.metaProgressMarker = m;
		metaProgressTracker.number = 0;
		metaProgressTracker.isTemporary = true;
		this.metaProgressTrackers.Add(metaProgressTracker);
	}

	// Token: 0x06000E70 RID: 3696 RVA: 0x00090174 File Offset: 0x0008E374
	private void SetMetaProgressMarkerValue(MetaProgressSaveManager.MetaProgressMarker m, int value)
	{
		MetaProgressSaveManager.MetaProgressTracker metaProgressMarker = this.GetMetaProgressMarker(m);
		if (metaProgressMarker == null)
		{
			return;
		}
		metaProgressMarker.number += value;
	}

	// Token: 0x06000E71 RID: 3697 RVA: 0x0009019C File Offset: 0x0008E39C
	public bool HasAllMetaProgressMarker(List<MetaProgressSaveManager.MetaProgressMarker> markers)
	{
		foreach (MetaProgressSaveManager.MetaProgressMarker metaProgressMarker in markers)
		{
			if (!this.HasMetaProgressMarker(metaProgressMarker))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000E72 RID: 3698 RVA: 0x000901F4 File Offset: 0x0008E3F4
	public bool HasAnyMetaProgressMarker(List<MetaProgressSaveManager.MetaProgressMarker> markers)
	{
		foreach (MetaProgressSaveManager.MetaProgressMarker metaProgressMarker in markers)
		{
			if (this.HasMetaProgressMarker(metaProgressMarker))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000E73 RID: 3699 RVA: 0x0009024C File Offset: 0x0008E44C
	private MetaProgressSaveManager.MetaProgressTracker GetMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker m)
	{
		foreach (MetaProgressSaveManager.MetaProgressTracker metaProgressTracker in this.metaProgressTrackers)
		{
			if (metaProgressTracker.metaProgressMarker == m)
			{
				return metaProgressTracker;
			}
		}
		return null;
	}

	// Token: 0x06000E74 RID: 3700 RVA: 0x000902A8 File Offset: 0x0008E4A8
	public int GetMetaProgressMarkerValue(MetaProgressSaveManager.MetaProgressMarker m)
	{
		MetaProgressSaveManager.MetaProgressTracker metaProgressMarker = this.GetMetaProgressMarker(m);
		if (metaProgressMarker == null)
		{
			return -1;
		}
		return metaProgressMarker.number;
	}

	// Token: 0x06000E75 RID: 3701 RVA: 0x000902C8 File Offset: 0x0008E4C8
	public void ResetLastRun()
	{
		this.lastRun = new MetaProgressSaveManager.LastRun();
	}

	// Token: 0x06000E76 RID: 3702 RVA: 0x000902D8 File Offset: 0x0008E4D8
	public void SaveLastRun(MetaProgressSaveManager.LastRun.Result result)
	{
		if (!Singleton.Instance.storyMode)
		{
			return;
		}
		if (!Singleton.Instance.mission)
		{
			return;
		}
		if (this.lastRun == null)
		{
			this.lastRun = new MetaProgressSaveManager.LastRun();
		}
		this.lastRun.missionName = Item2.GetDisplayName(Singleton.Instance.mission.name);
		this.lastRun.character = Singleton.Instance.character;
		this.lastRun.result = result;
		List<Item2> list;
		if (result == MetaProgressSaveManager.LastRun.Result.success)
		{
			list = Item2.GetAllItems();
		}
		else
		{
			list = Item2.GetAllItemsInGrid();
		}
		list = list.FindAll((Item2 x) => !x.itemType.Contains(Item2.ItemType.Hazard));
		this.lastRun.itemsDiscovered.Clear();
		foreach (Item2 item in list)
		{
			this.lastRun.itemsDiscovered.Add(Item2.GetDisplayName(item.name));
		}
	}

	// Token: 0x06000E77 RID: 3703 RVA: 0x000903F8 File Offset: 0x0008E5F8
	public void StoreAllItemsTotal()
	{
		if (RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.storyModeProgressDisabled) != null)
		{
			return;
		}
		Debug.Log("Storing all items total");
		List<Item2> list = Item2.GetAllItems();
		Tote tote = Object.FindObjectOfType<Tote>();
		if (tote)
		{
			list.AddRange(tote.GetAllCarvings());
		}
		list.AddRange(ItemPouch.GetAllItem2sFromPouches());
		list = list.FindAll((Item2 x) => !x.itemType.Contains(Item2.ItemType.Hazard));
		foreach (Item2 item2 in list)
		{
			this.AddItem(item2);
		}
		using (List<Item2>.Enumerator enumerator = GameManager.main.victoryItems.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Item2 item = enumerator.Current;
				if (list.Where((Item2 x) => Item2.GetDisplayName(x.name) == Item2.GetDisplayName(item.name)).Count<Item2>() <= 0)
				{
					this.AddItem(item);
				}
			}
		}
	}

	// Token: 0x06000E78 RID: 3704 RVA: 0x00090520 File Offset: 0x0008E720
	public void StoreAllItemsFromGameBackpack()
	{
		foreach (Item2 item in Item2.GetAllItemsInGrid().FindAll((Item2 x) => !x.itemType.Contains(Item2.ItemType.Hazard)))
		{
			this.AddItem(item);
		}
	}

	// Token: 0x06000E79 RID: 3705 RVA: 0x00090598 File Offset: 0x0008E798
	public void StoreItems(List<Item2> items)
	{
		foreach (Item2 item in items)
		{
			this.AddItem(item);
		}
	}

	// Token: 0x06000E7A RID: 3706 RVA: 0x000905E8 File Offset: 0x0008E7E8
	public bool HasMission(Missions m)
	{
		return this.missionsUnlocked.Contains(Missions.Stringify(m));
	}

	// Token: 0x06000E7B RID: 3707 RVA: 0x000905FB File Offset: 0x0008E7FB
	public void AddMission(Missions m)
	{
		if (this.missionsUnlocked.Contains(Missions.Stringify(m)))
		{
			return;
		}
		this.missionsUnlocked.Add(Missions.Stringify(m));
	}

	// Token: 0x06000E7C RID: 3708 RVA: 0x00090624 File Offset: 0x0008E824
	public void CompleteMission(Missions m)
	{
		if (!this.missionsComplete.Contains(Missions.Stringify(m)))
		{
			this.missionsComplete.Add(Missions.Stringify(m));
		}
		this.SaveLastRun(MetaProgressSaveManager.LastRun.Result.success);
		this.StoreAllItemsTotal();
		if (m.hardMode)
		{
			AchievementAbstractor.instance.ConsiderAchievement("BeatAHardMode");
		}
		MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.runsWon, 1);
		foreach (MetaProgressSaveManager.MetaProgressMarker metaProgressMarker in Singleton.Instance.mission.metaProgressMarkersToAddOnWin)
		{
			MetaProgressSaveManager.main.AddMetaProgressMarker(metaProgressMarker);
		}
		switch (Player.main.characterName)
		{
		case Character.CharacterName.Purse:
			MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.winsWithPurse, 1);
			break;
		case Character.CharacterName.Satchel:
			MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.winsWithSatchel, 1);
			break;
		case Character.CharacterName.Tote:
			MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.winsWithTote, 1);
			break;
		case Character.CharacterName.CR8:
			MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.winsWithCR8, 1);
			break;
		case Character.CharacterName.Pochette:
			MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.winsWithPochette, 1);
			break;
		}
		switch (GameManager.main.dungeonLevel.zone)
		{
		case DungeonLevel.Zone.dungeon:
			this.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.beatCrypt, 1);
			return;
		case DungeonLevel.Zone.deepCave:
			this.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.beatDeepCave, 1);
			return;
		case DungeonLevel.Zone.magmaCore:
			this.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.beatMagmaCore, 1);
			return;
		case DungeonLevel.Zone.EnchantedSwamp:
			this.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.beatEnchantedSwamp, 1);
			return;
		case DungeonLevel.Zone.theBramble:
			this.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.beatBramble, 1);
			return;
		case DungeonLevel.Zone.ice:
			this.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.beatFrozenHeart, 1);
			return;
		default:
			return;
		}
	}

	// Token: 0x06000E7D RID: 3709 RVA: 0x000907B8 File Offset: 0x0008E9B8
	public void AddItems(List<Item2> items)
	{
		foreach (Item2 item in items)
		{
			this.AddItem(item);
		}
	}

	// Token: 0x06000E7E RID: 3710 RVA: 0x00090808 File Offset: 0x0008EA08
	public void AddItem(Item2 item)
	{
		if (!item)
		{
			return;
		}
		string displayName = Item2.GetDisplayName(item.name);
		this.storedItems.Add(displayName);
		if (item.markerIfBroughtHome != MetaProgressSaveManager.MetaProgressMarker.none)
		{
			this.AddMetaProgressMarker(item.markerIfBroughtHome);
		}
		if (item.oneOfAKindType == Item2.OneOfAKindType.OneTotal && !this.oneOfAKindItems.Contains(displayName))
		{
			this.oneOfAKindItems.Add(displayName);
		}
	}

	// Token: 0x06000E7F RID: 3711 RVA: 0x00090870 File Offset: 0x0008EA70
	public void DeleteOneOfAKindItem(Item2 item)
	{
		if (!item)
		{
			return;
		}
		string displayName = Item2.GetDisplayName(item.name);
		this.DeleteOneOfAKindItem(displayName);
	}

	// Token: 0x06000E80 RID: 3712 RVA: 0x00090899 File Offset: 0x0008EA99
	public void DeleteOneOfAKindItem(string itemName)
	{
		itemName = Item2.GetDisplayName(itemName);
		if (!this.oneOfAKindItems.Contains(itemName))
		{
			return;
		}
		this.oneOfAKindItems.Remove(itemName);
	}

	// Token: 0x06000E81 RID: 3713 RVA: 0x000908C0 File Offset: 0x0008EAC0
	public bool AnyMetaData(int saveSlot = -1)
	{
		this.SetupSettings(saveSlot, false);
		bool flag = false;
		try
		{
			flag = ES3.FileExists(this.settings.path);
		}
		catch
		{
			return true;
		}
		return flag;
	}

	// Token: 0x06000E82 RID: 3714 RVA: 0x00090904 File Offset: 0x0008EB04
	public void SetupMetaData(int saveSlot = -1)
	{
		new List<MetaProgressSaveManager.RunCompleted>();
		this.SetupSettings(saveSlot, true);
		this.Load();
	}

	// Token: 0x06000E83 RID: 3715 RVA: 0x0009091C File Offset: 0x0008EB1C
	public void SetupSettings(int saveSlot, bool forLoading = false)
	{
		if (saveSlot == -1 || !Singleton.Instance.storyMode)
		{
			string text = Application.persistentDataPath + "/";
			this.settings = new ES3Settings(new Enum[] { ES3.Location.Cache });
			if (forLoading)
			{
				this.settings.location = ES3.Location.File;
			}
			this.settings.path = text + "bphMeta.sav";
			return;
		}
		ES3Settings es3Settings = SaveIncrementer.GetSettings("bphStoryModeMetaData" + saveSlot.ToString(), ".sav");
		this.settings = new ES3Settings(new Enum[] { ES3.Location.Cache });
		this.settings.location = es3Settings.location;
		this.settings.path = es3Settings.path;
	}

	// Token: 0x06000E84 RID: 3716 RVA: 0x000909E4 File Offset: 0x0008EBE4
	public DateTime GetDateTimeFromSlot(int saveSlot)
	{
		this.SetupSettings(saveSlot, false);
		ValueTuple<bool, byte[]> valueTuple = ConsoleWrapper.Instance.LoadFile(this.settings.path, "metaData", false);
		bool item = valueTuple.Item1;
		byte[] item2 = valueTuple.Item2;
		if (item)
		{
			ES3.SaveRaw(item2, this.settings);
			return ES3.Load<DateTime>("dateTime", this.dateTime, this.settings);
		}
		return DateTime.UnixEpoch;
	}

	// Token: 0x06000E85 RID: 3717 RVA: 0x00090A4C File Offset: 0x0008EC4C
	public void AddNew()
	{
		MetaProgressSaveManager.RunCompleted runCompleted = new MetaProgressSaveManager.RunCompleted();
		runCompleted.character = Singleton.Instance.character;
		runCompleted.runType = Singleton.Instance.runType.name;
		foreach (MetaProgressSaveManager.RunCompleted runCompleted2 in this.runsCompleted)
		{
			if (runCompleted2.character == runCompleted.character && runCompleted2.runType == runCompleted.runType && runCompleted2.ironMan == runCompleted.ironMan)
			{
				return;
			}
		}
		if (Singleton.Instance.runType.hardMode)
		{
			AchievementAbstractor.instance.ConsiderAchievement("BeatAHardMode");
		}
		this.runsCompleted.Add(runCompleted);
		this.SaveAll();
	}

	// Token: 0x06000E86 RID: 3718 RVA: 0x00090B28 File Offset: 0x0008ED28
	public bool RunTypeCompleted(RunType runType, Character.CharacterName character, bool mustBeIronMan = false)
	{
		foreach (MetaProgressSaveManager.RunCompleted runCompleted in this.runsCompleted)
		{
			if (runCompleted.character == character && runCompleted.runType == runType.name && (runCompleted.ironMan || !mustBeIronMan))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000E87 RID: 3719 RVA: 0x00090BA4 File Offset: 0x0008EDA4
	public void FindNewItem(GameObject obj)
	{
		string displayName = Item2.GetDisplayName(obj.name);
		if (!this.itemsDiscovered.Contains(displayName))
		{
			this.itemsDiscovered.Add(displayName);
		}
		if (this.itemsDiscovered.Count >= 700)
		{
			AchievementAbstractor.instance.ConsiderAchievement("ItemHero");
		}
		if (this.itemsDiscovered.Count >= 400)
		{
			AchievementAbstractor.instance.ConsiderAchievement("ItemExpert");
		}
		if (this.itemsDiscovered.Count >= 200)
		{
			AchievementAbstractor.instance.ConsiderAchievement("ItemDiscoverer");
		}
	}

	// Token: 0x06000E88 RID: 3720 RVA: 0x00090C3C File Offset: 0x0008EE3C
	public void UnlockItems(List<Item2> items)
	{
		foreach (Item2 item in items)
		{
			this.UnlockItem(item);
		}
	}

	// Token: 0x06000E89 RID: 3721 RVA: 0x00090C8C File Offset: 0x0008EE8C
	public void UnlockItem(Item2 item)
	{
		string displayName = Item2.GetDisplayName(item.name);
		if (!this.itemsUnlocked.Contains(displayName))
		{
			this.itemsUnlocked.Add(displayName);
		}
	}

	// Token: 0x06000E8A RID: 3722 RVA: 0x00090CBF File Offset: 0x0008EEBF
	public bool ItemIsUnlocked(string name)
	{
		name = Item2.GetDisplayName(name);
		return this.itemsUnlocked.Contains(name);
	}

	// Token: 0x06000E8B RID: 3723 RVA: 0x00090CDC File Offset: 0x0008EEDC
	public bool ItemIsUnlocked(Item2 item)
	{
		string displayName = Item2.GetDisplayName(item.name);
		return this.itemsUnlocked.Contains(displayName);
	}

	// Token: 0x06000E8C RID: 3724 RVA: 0x00090D06 File Offset: 0x0008EF06
	public bool FoundItem(string name)
	{
		name = Item2.GetDisplayName(name).ToUpper().Trim();
		return this.itemsDiscovered.Contains(name);
	}

	// Token: 0x06000E8D RID: 3725 RVA: 0x00090D2B File Offset: 0x0008EF2B
	public void SaveCompletedTutorials(List<string> completedTutorials)
	{
		this.completedTutorials = completedTutorials;
	}

	// Token: 0x06000E8E RID: 3726 RVA: 0x00090D34 File Offset: 0x0008EF34
	public List<string> LoadCompletedTutorials()
	{
		if (Singleton.Instance.storyMode)
		{
			this.settings = SaveIncrementer.GetSettings("bphStoryModeMetaData" + Singleton.Instance.storyModeSlot.ToString(), ".sav");
		}
		else
		{
			this.SetupSettings(-1, true);
		}
		Console.Out.WriteLine("Loading completedTutorials");
		if (!ES3.KeyExists("completedTutorials", this.settings))
		{
			Console.Out.WriteLine("No key completedTutorials, returning empty");
			this.completedTutorials = new List<string>();
			return this.completedTutorials;
		}
		Console.Out.WriteLine("Successfully loaded completedTutorials [" + string.Join(", ", this.completedTutorials) + "]");
		this.completedTutorials = ES3.Load<List<string>>("completedTutorials", new List<string>(), this.settings);
		return this.completedTutorials;
	}

	// Token: 0x06000E8F RID: 3727 RVA: 0x00090E0C File Offset: 0x0008F00C
	public void SaveAll()
	{
		if (Singleton.Instance.storyMode)
		{
			this.settings = SaveIncrementer.IncrementSaveFile("bphStoryModeMetaData" + Singleton.Instance.storyModeSlot.ToString(), ".sav");
		}
		else
		{
			this.SetupSettings(-1, false);
		}
		Debug.Log("Saving Meta! " + this.settings.path);
		try
		{
			ES3.Save<List<Overworld_ResourceManager.Resource>>("resources", this.resources, this.settings);
			ES3.Save<List<MetaProgressSaveManager.RunCompleted>>("metaProgress", this.runsCompleted, this.settings);
			ES3.Save<List<string>>("itemsDiscovered", this.itemsDiscovered, this.settings);
			ES3.Save<List<string>>("itemsUnlocked", this.itemsUnlocked, this.settings);
			ES3.Save<List<string>>("storedItems", this.storedItems, this.settings);
			ES3.Save<List<string>>("oneOfAKindItems", this.oneOfAKindItems, this.settings);
			ES3.Save<List<MetaProgressSaveManager.MetaProgressTracker>>("metaProgressMarkers", this.metaProgressTrackers, this.settings);
			ES3.Save<List<string>>("missionsUnlocked", this.missionsUnlocked, this.settings);
			ES3.Save<List<string>>("missionsComplete", this.missionsComplete, this.settings);
			ES3.Save<List<MetaProgressSaveManager.NPCsUnlocked>>("npcsUnlocked", this.nPCsUnlocked, this.settings);
			ES3.Save<List<string>>("loresUnlocked", this.loresUnlocked, this.settings);
			ES3.Save<List<string>>("researches", this.researchNamesComplete, this.settings);
			ES3.Save<MetaProgressSaveManager.LastRun>("lastRun", this.lastRun, this.settings);
			ES3.Save<List<MetaProgressSaveManager.ResourceToAdd>>("resourcesToAdd", this.resourcesToAdd, this.settings);
			ES3.Save<List<string>>("availableBuildings", this.availableBuildings, this.settings);
			ES3.Save<List<string>>("availableTiles", this.availableTiles, this.settings);
			ES3.Save<List<string>>("dialoguesPerformed", this.dialoguesPerformed, this.settings);
			ES3.Save<List<string>>("availableCostumes", this.availableCostumes, this.settings);
			ES3.Save<List<string>>("completedNewResearch", this.completedNewResearch, this.settings);
			ES3.Save<List<string>>("researchBuildingsDiscovered", this.researchBuildingsDiscovered, this.settings);
			ES3.Save<List<string>>("completedTutorials", this.completedTutorials, this.settings);
			SaveIncrementer.FlushToDisk(this.settings, true);
			Debug.Log("Saved Meta! " + this.settings.path);
			string text = "";
			foreach (MetaProgressSaveManager.MetaProgressTracker metaProgressTracker in this.metaProgressTrackers)
			{
				if (text != "")
				{
					text += ", ";
				}
				text = text + metaProgressTracker.metaProgressMarker.ToString() + ":" + metaProgressTracker.number.ToString();
			}
			Debug.Log("Saved meta progress markers: " + text);
		}
		catch
		{
			Debug.Log("Save was corrupted - couldn't save");
		}
	}

	// Token: 0x06000E90 RID: 3728 RVA: 0x00091130 File Offset: 0x0008F330
	public void Load()
	{
		this.currentSaveFile = null;
		if (Singleton.Instance.storyMode)
		{
			Debug.Log("---Loading Meta incrementing---");
			try
			{
				List<string> saveFilesForSlot = SaveIncrementer.GetSaveFilesForSlot("bphStoryModeMetaData" + Singleton.Instance.storyModeSlot.ToString(), ".sav", false);
				Debug.Log("SaveList");
				foreach (string text in saveFilesForSlot)
				{
					Debug.Log(text);
				}
				if (saveFilesForSlot.Count == 0)
				{
					string text2 = Application.persistentDataPath + "/";
					this.settings.path = text2 + SaveIncrementer.GetFilenameForSlot(saveFilesForSlot, "bphStoryModeMetaData" + Singleton.Instance.storyModeSlot.ToString(), ".sav", true);
					try
					{
						Debug.Log("Attempting to load new meta save at " + this.settings.path + "should reset data to default values");
						this.LoadMetaInner(this.settings);
					}
					catch (Exception ex)
					{
						string text3 = "Could not load new meta save ";
						string path = this.settings.path;
						string text4 = " --- ";
						Exception ex2 = ex;
						Debug.LogError(text3 + path + text4 + ((ex2 != null) ? ex2.ToString() : null));
					}
					return;
				}
				foreach (string text5 in saveFilesForSlot)
				{
					try
					{
						string text6 = Application.persistentDataPath + "/";
						this.settings.path = text6 + text5;
						Debug.Log("Attempting to load meta save " + text5);
						this.currentSaveFile = this.settings.path;
						this.LoadMetaInner(this.settings);
						return;
					}
					catch (Exception ex3)
					{
						string text7 = "Could not load meta save ";
						string text8 = text5;
						string text9 = " --- ";
						Exception ex4 = ex3;
						Debug.LogError(text7 + text8 + text9 + ((ex4 != null) ? ex4.ToString() : null));
						SaveIncrementer.blacklist.Add(text5);
						this.currentSaveFile = null;
					}
				}
			}
			catch (Exception ex5)
			{
				string text10 = "Error while loading the metadata ";
				Exception ex6 = ex5;
				Debug.LogError(text10 + ((ex6 != null) ? ex6.ToString() : null));
				this.currentSaveFile = null;
			}
			this.currentSaveFile = null;
			Singleton.Instance.errorMessage = "Could not load your metadata save file. Please restart the game and try again. <br> If the issue persists, please contact us.";
			SceneManager.LoadScene("MainMenu");
			return;
		}
		Debug.Log("Loading Meta for a non-story mode game");
		this.SetupSettings(-1, true);
		try
		{
			Debug.Log("Loading Meta! from path: " + this.settings.path);
			this.LoadMetaInner(this.settings);
		}
		catch (Exception ex7)
		{
			string text11 = "Save was corrupted! Will be replaced.";
			Exception ex8 = ex7;
			Debug.Log(text11 + ((ex8 != null) ? ex8.ToString() : null));
		}
	}

	// Token: 0x06000E91 RID: 3729 RVA: 0x0009146C File Offset: 0x0008F66C
	public void LoadMetaInner(ES3Settings settings)
	{
		this.dateTime = ES3.GetTimestamp(settings);
		this.resources = ES3.Load<List<Overworld_ResourceManager.Resource>>("resources", new List<Overworld_ResourceManager.Resource>(), settings);
		this.runsCompleted = ES3.Load<List<MetaProgressSaveManager.RunCompleted>>("metaProgress", new List<MetaProgressSaveManager.RunCompleted>(), settings);
		this.itemsDiscovered = ES3.Load<List<string>>("itemsDiscovered", new List<string>(), settings);
		this.itemsUnlocked = ES3.Load<List<string>>("itemsUnlocked", new List<string>(), settings);
		this.storedItems = ES3.Load<List<string>>("storedItems", new List<string>(), settings);
		this.oneOfAKindItems = ES3.Load<List<string>>("oneOfAKindItems", new List<string>(), settings);
		this.metaProgressTrackers = ES3.Load<List<MetaProgressSaveManager.MetaProgressTracker>>("metaProgressMarkers", new List<MetaProgressSaveManager.MetaProgressTracker>(), settings);
		this.missionsUnlocked = ES3.Load<List<string>>("missionsUnlocked", new List<string>(), settings);
		this.missionsComplete = ES3.Load<List<string>>("missionsComplete", new List<string>(), settings);
		this.nPCsUnlocked = ES3.Load<List<MetaProgressSaveManager.NPCsUnlocked>>("npcsUnlocked", new List<MetaProgressSaveManager.NPCsUnlocked>(), settings);
		this.loresUnlocked = ES3.Load<List<string>>("loresUnlocked", new List<string>(), settings);
		this.researchNamesComplete = ES3.Load<List<string>>("researches", new List<string>(), settings);
		this.lastRun = ES3.Load<MetaProgressSaveManager.LastRun>("lastRun", new MetaProgressSaveManager.LastRun(), settings);
		this.resourcesToAdd = ES3.Load<List<MetaProgressSaveManager.ResourceToAdd>>("resourcesToAdd", new List<MetaProgressSaveManager.ResourceToAdd>(), settings);
		this.availableBuildings = ES3.Load<List<string>>("availableBuildings", new List<string>(), settings);
		this.availableTiles = ES3.Load<List<string>>("availableTiles", new List<string>(), settings);
		this.dialoguesPerformed = ES3.Load<List<string>>("dialoguesPerformed", new List<string>(), settings);
		this.availableCostumes = ES3.Load<List<string>>("availableCostumes", new List<string>(), settings);
		this.completedNewResearch = ES3.Load<List<string>>("completedNewResearch", new List<string>(), settings);
		this.researchBuildingsDiscovered = ES3.Load<List<string>>("researchBuildingsDiscovered", new List<string>(), settings);
		this.ResetNPCTickets();
		this.LoadCompletedTutorials();
		Debug.Log("Loaded Meta! from path: " + settings.path);
		Debug.Log("Loaded Meta! Runs completed: " + this.runsCompleted.Count.ToString());
		Debug.Log("Loaded! Items Discovered: " + this.itemsDiscovered.Count.ToString());
		string text = "";
		foreach (MetaProgressSaveManager.MetaProgressTracker metaProgressTracker in this.metaProgressTrackers)
		{
			if (text != "")
			{
				text += ", ";
			}
			text = text + metaProgressTracker.metaProgressMarker.ToString() + ":" + metaProgressTracker.number.ToString();
		}
		Debug.Log("Loaded meta progress markers: " + text);
	}

	// Token: 0x06000E92 RID: 3730 RVA: 0x0009173C File Offset: 0x0008F93C
	public void ResetNPCTickets()
	{
		foreach (MetaProgressSaveManager.NPCsUnlocked npcsUnlocked in this.nPCsUnlocked)
		{
			npcsUnlocked.numberSpawned = 0;
		}
	}

	// Token: 0x06000E93 RID: 3731 RVA: 0x00091790 File Offset: 0x0008F990
	public void ChangePopTicket(GameObject populationPrefab, int populationAdd)
	{
	}

	// Token: 0x04000BA6 RID: 2982
	[Header("---------------------References---------------------")]
	[SerializeField]
	private ItemAtlas itemAtlas;

	// Token: 0x04000BA7 RID: 2983
	[SerializeField]
	private bool loadMetaProgress;

	// Token: 0x04000BA8 RID: 2984
	[SerializeField]
	private bool loadResearchNew;

	// Token: 0x04000BA9 RID: 2985
	[HideInInspector]
	private string currentSaveFile;

	// Token: 0x04000BAA RID: 2986
	public static MetaProgressSaveManager main;

	// Token: 0x04000BAB RID: 2987
	private ES3Settings settings;

	// Token: 0x04000BAC RID: 2988
	public List<MetaProgressSaveManager.RunCompleted> runsCompleted = new List<MetaProgressSaveManager.RunCompleted>();

	// Token: 0x04000BAD RID: 2989
	private ConsoleWrapper _consoleWrapper;

	// Token: 0x04000BAE RID: 2990
	private bool setup;

	// Token: 0x04000BAF RID: 2991
	[Header("---------------------Meta Progress---------------------")]
	public List<string> missionsComplete = new List<string>();

	// Token: 0x04000BB0 RID: 2992
	public List<string> storedItems = new List<string>();

	// Token: 0x04000BB1 RID: 2993
	public List<string> oneOfAKindItems = new List<string>();

	// Token: 0x04000BB2 RID: 2994
	public List<string> itemsDiscovered = new List<string>();

	// Token: 0x04000BB3 RID: 2995
	public List<string> itemsUnlocked = new List<string>();

	// Token: 0x04000BB4 RID: 2996
	public List<string> dialoguesPerformed = new List<string>();

	// Token: 0x04000BB5 RID: 2997
	[SerializeField]
	private List<MetaProgressSaveManager.MetaProgressTracker> metaProgressTrackers = new List<MetaProgressSaveManager.MetaProgressTracker>();

	// Token: 0x04000BB6 RID: 2998
	[SerializeField]
	private ResearchList researchList;

	// Token: 0x04000BB7 RID: 2999
	[SerializeField]
	private List<string> researchNamesComplete = new List<string>();

	// Token: 0x04000BB8 RID: 3000
	[SerializeField]
	public List<string> completedTutorials = new List<string>();

	// Token: 0x04000BB9 RID: 3001
	[SerializeField]
	public List<string> loresUnlocked = new List<string>();

	// Token: 0x04000BBA RID: 3002
	[SerializeField]
	public List<MetaProgressSaveManager.NPCsUnlocked> nPCsUnlocked = new List<MetaProgressSaveManager.NPCsUnlocked>();

	// Token: 0x04000BBB RID: 3003
	[SerializeField]
	public List<string> availableBuildings = new List<string>();

	// Token: 0x04000BBC RID: 3004
	[SerializeField]
	public List<string> availableTiles = new List<string>();

	// Token: 0x04000BBD RID: 3005
	[SerializeField]
	public List<string> availableCostumes = new List<string>();

	// Token: 0x04000BBE RID: 3006
	[SerializeField]
	public List<string> completedNewResearch = new List<string>();

	// Token: 0x04000BBF RID: 3007
	[SerializeField]
	public List<string> researchBuildingsDiscovered = new List<string>();

	// Token: 0x04000BC0 RID: 3008
	[SerializeField]
	public MetaProgressSaveManager.LastRun lastRun = new MetaProgressSaveManager.LastRun();

	// Token: 0x04000BC1 RID: 3009
	[SerializeField]
	public List<MetaProgressSaveManager.ResourceToAdd> resourcesToAdd = new List<MetaProgressSaveManager.ResourceToAdd>();

	// Token: 0x04000BC2 RID: 3010
	public DateTime dateTime;

	// Token: 0x04000BC3 RID: 3011
	public List<Overworld_ResourceManager.Resource> resources = new List<Overworld_ResourceManager.Resource>();

	// Token: 0x04000BC4 RID: 3012
	public List<string> missionsUnlocked = new List<string>();

	// Token: 0x0200042C RID: 1068
	[ES3Serializable]
	[Serializable]
	public class Research
	{
		// Token: 0x060019CF RID: 6607 RVA: 0x000D0E2E File Offset: 0x000CF02E
		public static implicit operator bool(MetaProgressSaveManager.Research r)
		{
			return r != null;
		}

		// Token: 0x04001844 RID: 6212
		public GameObject item;

		// Token: 0x04001845 RID: 6213
		public List<Overworld_ResourceManager.Resource> resources = new List<Overworld_ResourceManager.Resource>();

		// Token: 0x04001846 RID: 6214
		[Header("Meta Progress")]
		public MetaProgressSaveManager.MetaProgressMarker metaProgressMarker;

		// Token: 0x04001847 RID: 6215
		public List<GameObject> objsToUnlock;

		// Token: 0x04001848 RID: 6216
		public List<ScriptableObject> assetsToUnlock;
	}

	// Token: 0x0200042D RID: 1069
	[Serializable]
	public class NPCsUnlocked
	{
		// Token: 0x04001849 RID: 6217
		public string npcName = "";

		// Token: 0x0400184A RID: 6218
		public int numberUnlocked;

		// Token: 0x0400184B RID: 6219
		public int numberSpawned;
	}

	// Token: 0x0200042E RID: 1070
	[Serializable]
	public class LastRun
	{
		// Token: 0x0400184C RID: 6220
		public string missionName = "";

		// Token: 0x0400184D RID: 6221
		public List<MetaProgressSaveManager.LastRun.RunEvents> events = new List<MetaProgressSaveManager.LastRun.RunEvents>();

		// Token: 0x0400184E RID: 6222
		public Character.CharacterName character;

		// Token: 0x0400184F RID: 6223
		public List<string> itemsDiscovered = new List<string>();

		// Token: 0x04001850 RID: 6224
		public MetaProgressSaveManager.LastRun.Result result;

		// Token: 0x020004BF RID: 1215
		public enum RunEvents
		{
			// Token: 0x04001CA6 RID: 7334
			none,
			// Token: 0x04001CA7 RID: 7335
			metParcel,
			// Token: 0x04001CA8 RID: 7336
			wentShopping,
			// Token: 0x04001CA9 RID: 7337
			forgedAnItem,
			// Token: 0x04001CAA RID: 7338
			gotARelic,
			// Token: 0x04001CAB RID: 7339
			metShadyTrader,
			// Token: 0x04001CAC RID: 7340
			playedAMinigame,
			// Token: 0x04001CAD RID: 7341
			gotACurse,
			// Token: 0x04001CAE RID: 7342
			wentToCrypt,
			// Token: 0x04001CAF RID: 7343
			wentToBramble,
			// Token: 0x04001CB0 RID: 7344
			wentToDeepCave,
			// Token: 0x04001CB1 RID: 7345
			wentToEnchantedSwamp,
			// Token: 0x04001CB2 RID: 7346
			wentToMagmaCore,
			// Token: 0x04001CB3 RID: 7347
			wentToFrozenHeart,
			// Token: 0x04001CB4 RID: 7348
			gotABlessing
		}

		// Token: 0x020004C0 RID: 1216
		public enum Result
		{
			// Token: 0x04001CB6 RID: 7350
			none,
			// Token: 0x04001CB7 RID: 7351
			success,
			// Token: 0x04001CB8 RID: 7352
			died,
			// Token: 0x04001CB9 RID: 7353
			quit
		}
	}

	// Token: 0x0200042F RID: 1071
	[Serializable]
	public class MetaProgressCondition
	{
		// Token: 0x04001851 RID: 6225
		[SerializeField]
		public MetaProgressSaveManager.MetaProgressCondition.Type type;

		// Token: 0x04001852 RID: 6226
		public bool storyModeOnly = true;

		// Token: 0x04001853 RID: 6227
		public MetaProgressSaveManager.MetaProgressMarker marker;

		// Token: 0x04001854 RID: 6228
		public bool isTrue = true;

		// Token: 0x04001855 RID: 6229
		[SerializeField]
		public int greaterThanOrEqualToValue;

		// Token: 0x020004C1 RID: 1217
		public enum Type
		{
			// Token: 0x04001CBB RID: 7355
			Boolean,
			// Token: 0x04001CBC RID: 7356
			Integer
		}
	}

	// Token: 0x02000430 RID: 1072
	[ES3Serializable]
	[Serializable]
	private class MetaProgressTracker
	{
		// Token: 0x04001856 RID: 6230
		public MetaProgressSaveManager.MetaProgressMarker metaProgressMarker;

		// Token: 0x04001857 RID: 6231
		public int number;

		// Token: 0x04001858 RID: 6232
		public bool isTemporary;
	}

	// Token: 0x02000431 RID: 1073
	[ES3Serializable]
	[Serializable]
	public class RunCompleted
	{
		// Token: 0x04001859 RID: 6233
		public string runType;

		// Token: 0x0400185A RID: 6234
		public Character.CharacterName character;

		// Token: 0x0400185B RID: 6235
		public bool ironMan;
	}

	// Token: 0x02000432 RID: 1074
	[Serializable]
	public class ResourceToAdd
	{
		// Token: 0x0400185C RID: 6236
		public Overworld_ResourceManager.Resource.Type type;

		// Token: 0x0400185D RID: 6237
		public int amount;

		// Token: 0x0400185E RID: 6238
		public float currentEfficiencyBonus;
	}

	// Token: 0x02000433 RID: 1075
	public enum MetaProgressMarker
	{
		// Token: 0x04001860 RID: 6240
		none,
		// Token: 0x04001861 RID: 6241
		firstConversationInTown,
		// Token: 0x04001862 RID: 6242
		firstConversationWithZaar,
		// Token: 0x04001863 RID: 6243
		firstBuildingBuilt,
		// Token: 0x04001864 RID: 6244
		firstRunStartedFromOverworld,
		// Token: 0x04001865 RID: 6245
		backpackCollected,
		// Token: 0x04001866 RID: 6246
		unlockedTote,
		// Token: 0x04001867 RID: 6247
		unlockedCR8,
		// Token: 0x04001868 RID: 6248
		unlockedSatchel,
		// Token: 0x04001869 RID: 6249
		unlockedPochette,
		// Token: 0x0400186A RID: 6250
		runsWon,
		// Token: 0x0400186B RID: 6251
		runsPlayed,
		// Token: 0x0400186C RID: 6252
		runsDied,
		// Token: 0x0400186D RID: 6253
		beatCrypt,
		// Token: 0x0400186E RID: 6254
		beatBramble,
		// Token: 0x0400186F RID: 6255
		beatDeepCave,
		// Token: 0x04001870 RID: 6256
		beatEnchantedSwamp,
		// Token: 0x04001871 RID: 6257
		beatMagmaCore,
		// Token: 0x04001872 RID: 6258
		beatFrozenHeart,
		// Token: 0x04001873 RID: 6259
		hasBeenCursed,
		// Token: 0x04001874 RID: 6260
		unlockedPurseHouse,
		// Token: 0x04001875 RID: 6261
		unlockedMatthew,
		// Token: 0x04001876 RID: 6262
		unlockedMagic,
		// Token: 0x04001877 RID: 6263
		unlockedCurses,
		// Token: 0x04001878 RID: 6264
		metConstance,
		// Token: 0x04001879 RID: 6265
		unlockedArchery,
		// Token: 0x0400187A RID: 6266
		unlockedHazards,
		// Token: 0x0400187B RID: 6267
		bossesEncountered,
		// Token: 0x0400187C RID: 6268
		unlockedVisionOfDanger,
		// Token: 0x0400187D RID: 6269
		winsWithPurse,
		// Token: 0x0400187E RID: 6270
		winsWithCR8,
		// Token: 0x0400187F RID: 6271
		winsWithSatchel,
		// Token: 0x04001880 RID: 6272
		winsWithPochette,
		// Token: 0x04001881 RID: 6273
		winsWithTote,
		// Token: 0x04001882 RID: 6274
		metPasha,
		// Token: 0x04001883 RID: 6275
		metEdith,
		// Token: 0x04001884 RID: 6276
		metNora,
		// Token: 0x04001885 RID: 6277
		metFelix,
		// Token: 0x04001886 RID: 6278
		metJoseline,
		// Token: 0x04001887 RID: 6279
		metCR8,
		// Token: 0x04001888 RID: 6280
		metSatchel,
		// Token: 0x04001889 RID: 6281
		metPochette,
		// Token: 0x0400188A RID: 6282
		metTote,
		// Token: 0x0400188B RID: 6283
		metClementine,
		// Token: 0x0400188C RID: 6284
		metMayor,
		// Token: 0x0400188D RID: 6285
		metSam,
		// Token: 0x0400188E RID: 6286
		metStumpy,
		// Token: 0x0400188F RID: 6287
		metVivienne,
		// Token: 0x04001890 RID: 6288
		metParcel,
		// Token: 0x04001891 RID: 6289
		gaveCrownToMayor,
		// Token: 0x04001892 RID: 6290
		unlockedBramble,
		// Token: 0x04001893 RID: 6291
		unlockedDeepCave,
		// Token: 0x04001894 RID: 6292
		unlockedEnchantedSwamp,
		// Token: 0x04001895 RID: 6293
		unlockedMagmaCore,
		// Token: 0x04001896 RID: 6294
		unlockedFrozenHeart,
		// Token: 0x04001897 RID: 6295
		unlockedFinalArea,
		// Token: 0x04001898 RID: 6296
		gaveWeaponToMayor,
		// Token: 0x04001899 RID: 6297
		gaveConsumableToMayor,
		// Token: 0x0400189A RID: 6298
		gaveShieldToMayor,
		// Token: 0x0400189B RID: 6299
		gaveAllItemsToMayor,
		// Token: 0x0400189C RID: 6300
		talkedToZaarAndUnlockedMissionMenu,
		// Token: 0x0400189D RID: 6301
		firstConversationWithCarpenter,
		// Token: 0x0400189E RID: 6302
		carpenterBuilt,
		// Token: 0x0400189F RID: 6303
		builtConstanceHouse,
		// Token: 0x040018A0 RID: 6304
		gotCR8quest,
		// Token: 0x040018A1 RID: 6305
		gaveCR8piece,
		// Token: 0x040018A2 RID: 6306
		gotAllCR8pieces,
		// Token: 0x040018A3 RID: 6307
		gotJobFlyer,
		// Token: 0x040018A4 RID: 6308
		invitedPatitaToTown,
		// Token: 0x040018A5 RID: 6309
		metPatitaInTown,
		// Token: 0x040018A6 RID: 6310
		cleansedCurse,
		// Token: 0x040018A7 RID: 6311
		completedMagicResearch,
		// Token: 0x040018A8 RID: 6312
		heardAboutTote,
		// Token: 0x040018A9 RID: 6313
		gaveProtoManastone,
		// Token: 0x040018AA RID: 6314
		builtBlackSmith,
		// Token: 0x040018AB RID: 6315
		metFisherman,
		// Token: 0x040018AC RID: 6316
		gaveFishToFisherman,
		// Token: 0x040018AD RID: 6317
		metBankerPurrteller,
		// Token: 0x040018AE RID: 6318
		metMasterArcherInDungeon,
		// Token: 0x040018AF RID: 6319
		metMasterArcher,
		// Token: 0x040018B0 RID: 6320
		has12population,
		// Token: 0x040018B1 RID: 6321
		has25population,
		// Token: 0x040018B2 RID: 6322
		liedToDad,
		// Token: 0x040018B3 RID: 6323
		toldDadYouStopAdventuring,
		// Token: 0x040018B4 RID: 6324
		toldDadYouAreWorkingToRebuildOrderia,
		// Token: 0x040018B5 RID: 6325
		zaarGaveFirstMission,
		// Token: 0x040018B6 RID: 6326
		unlockedElites,
		// Token: 0x040018B7 RID: 6327
		unlockedMiniGames,
		// Token: 0x040018B8 RID: 6328
		townRaided,
		// Token: 0x040018B9 RID: 6329
		returnedToDungeonAfterRaid,
		// Token: 0x040018BA RID: 6330
		talkedToZaarAfterRaid,
		// Token: 0x040018BB RID: 6331
		talkedToStumpyAfterRaid,
		// Token: 0x040018BC RID: 6332
		talkedToMayorAfterRaid,
		// Token: 0x040018BD RID: 6333
		gavePlushToMayor,
		// Token: 0x040018BE RID: 6334
		builtPurseHouse,
		// Token: 0x040018BF RID: 6335
		readyForRaid,
		// Token: 0x040018C0 RID: 6336
		talkedToLouisAfterSecondRun,
		// Token: 0x040018C1 RID: 6337
		foundProtoManaStone,
		// Token: 0x040018C2 RID: 6338
		talkedToZaarAboutShops,
		// Token: 0x040018C3 RID: 6339
		builtFletcher,
		// Token: 0x040018C4 RID: 6340
		unlockedForges,
		// Token: 0x040018C5 RID: 6341
		invitedSatchel,
		// Token: 0x040018C6 RID: 6342
		searchingForSatchel,
		// Token: 0x040018C7 RID: 6343
		unlockedBridges,
		// Token: 0x040018C8 RID: 6344
		builtHouseForMayor,
		// Token: 0x040018C9 RID: 6345
		builtFirstHouse,
		// Token: 0x040018CA RID: 6346
		hasEnoughPopulationForClementine,
		// Token: 0x040018CB RID: 6347
		metGhost,
		// Token: 0x040018CC RID: 6348
		destroyedOldShop,
		// Token: 0x040018CD RID: 6349
		talkedToZaarAfterDestruction,
		// Token: 0x040018CE RID: 6350
		talkedToConstanceAfterGettingCR8Built,
		// Token: 0x040018CF RID: 6351
		foughtKingsGuardFirstTime,
		// Token: 0x040018D0 RID: 6352
		broughtBackPradasNecklace,
		// Token: 0x040018D1 RID: 6353
		gaveDadPradasNecklace,
		// Token: 0x040018D2 RID: 6354
		returnedToDungeonAfterGaveDadNecklace,
		// Token: 0x040018D3 RID: 6355
		openedDoorToDeepCave,
		// Token: 0x040018D4 RID: 6356
		openedDoorToBramble,
		// Token: 0x040018D5 RID: 6357
		openedDoorToEnchantedSwamp,
		// Token: 0x040018D6 RID: 6358
		openedDoorToMagmaCore,
		// Token: 0x040018D7 RID: 6359
		openedDoorToFrozenHeart,
		// Token: 0x040018D8 RID: 6360
		talkedToMatthewABoutDeepCave,
		// Token: 0x040018D9 RID: 6361
		talkedToMatthewABoutBramble,
		// Token: 0x040018DA RID: 6362
		talkedToMatthewABoutEnchantedSwamp,
		// Token: 0x040018DB RID: 6363
		talkedToMatthewABoutMagmaCore,
		// Token: 0x040018DC RID: 6364
		talkedToMatthewABoutFrozenHeart,
		// Token: 0x040018DD RID: 6365
		soldSomethingInTown,
		// Token: 0x040018DE RID: 6366
		heardAboutResearchFromZaar,
		// Token: 0x040018DF RID: 6367
		metMissBurrough,
		// Token: 0x040018E0 RID: 6368
		builtSchoolHouse,
		// Token: 0x040018E1 RID: 6369
		metLily,
		// Token: 0x040018E2 RID: 6370
		metRaven,
		// Token: 0x040018E3 RID: 6371
		metKingsGuardInTown,
		// Token: 0x040018E4 RID: 6372
		defeatedKingsGuardDuringRaid1,
		// Token: 0x040018E5 RID: 6373
		defeatedKingsGuardDuringRaid2,
		// Token: 0x040018E6 RID: 6374
		defeatedKingsGuardDuringRaid3,
		// Token: 0x040018E7 RID: 6375
		defeatedKingsGuardDuringRaid4,
		// Token: 0x040018E8 RID: 6376
		defeatedKingsGuardDuringRaid5,
		// Token: 0x040018E9 RID: 6377
		unlockedStandardRunForSatchel,
		// Token: 0x040018EA RID: 6378
		unlockedStandardRunForTote,
		// Token: 0x040018EB RID: 6379
		unlockedStandardRunForPochette,
		// Token: 0x040018EC RID: 6380
		unlockedStandardRunForCR8,
		// Token: 0x040018ED RID: 6381
		talkedToSamAboutSatchel,
		// Token: 0x040018EE RID: 6382
		visitedChaoticDarkness,
		// Token: 0x040018EF RID: 6383
		cr8ReadyForFinale,
		// Token: 0x040018F0 RID: 6384
		satchelReadyForFinale,
		// Token: 0x040018F1 RID: 6385
		toteReadyForFinale,
		// Token: 0x040018F2 RID: 6386
		pochetteReadyForFinale,
		// Token: 0x040018F3 RID: 6387
		broughtBackMysteriousLetter,
		// Token: 0x040018F4 RID: 6388
		wonTheWholeDangGame,
		// Token: 0x040018F5 RID: 6389
		gotFletcherRecipeFromMasterArcher,
		// Token: 0x040018F6 RID: 6390
		foundAnyLoreItem,
		// Token: 0x040018F7 RID: 6391
		builtMagicalMycelium,
		// Token: 0x040018F8 RID: 6392
		invitedToteToTown,
		// Token: 0x040018F9 RID: 6393
		beatPochetteFirstTime,
		// Token: 0x040018FA RID: 6394
		beatPochetteSecondTime,
		// Token: 0x040018FB RID: 6395
		beatPochetteThirdTime,
		// Token: 0x040018FC RID: 6396
		rescuedLouis,
		// Token: 0x040018FD RID: 6397
		kingDefeated,
		// Token: 0x040018FE RID: 6398
		seenCinematic2,
		// Token: 0x040018FF RID: 6399
		metJeweler,
		// Token: 0x04001900 RID: 6400
		talkedToMatthewAboutFinalLevel,
		// Token: 0x04001901 RID: 6401
		builtTownHall,
		// Token: 0x04001902 RID: 6402
		talkedToQuillSwishAboutTownHall,
		// Token: 0x04001903 RID: 6403
		readyToRescueLouis,
		// Token: 0x04001904 RID: 6404
		unlockedHomingBeacon,
		// Token: 0x04001905 RID: 6405
		HomingBeaconBuilt,
		// Token: 0x04001906 RID: 6406
		talkedToConstanceAboutFinalQuest,
		// Token: 0x04001907 RID: 6407
		metPrada,
		// Token: 0x04001908 RID: 6408
		talkedToDadAboutPrada,
		// Token: 0x04001909 RID: 6409
		talkedToMattAboutPrada,
		// Token: 0x0400190A RID: 6410
		talkedToConstanceAboutPrada,
		// Token: 0x0400190B RID: 6411
		talkedToZaarAboutPrada,
		// Token: 0x0400190C RID: 6412
		talkedToMayorAboutPrada,
		// Token: 0x0400190D RID: 6413
		talkedToSatchelAboutPrada,
		// Token: 0x0400190E RID: 6414
		talkedToToteAboutPrada,
		// Token: 0x0400190F RID: 6415
		talkedToCR8aboutPrada,
		// Token: 0x04001910 RID: 6416
		talkedToPochetteAboutPrada,
		// Token: 0x04001911 RID: 6417
		broughtQuillswishPlushToTown,
		// Token: 0x04001912 RID: 6418
		gotCampaignTrailQuest,
		// Token: 0x04001913 RID: 6419
		completedCampaignTrailQuest,
		// Token: 0x04001914 RID: 6420
		fishingShackBuilt,
		// Token: 0x04001915 RID: 6421
		unlockedChest,
		// Token: 0x04001916 RID: 6422
		unlockedRarePrimrose,
		// Token: 0x04001917 RID: 6423
		unlockedGoldPile,
		// Token: 0x04001918 RID: 6424
		readyForChest,
		// Token: 0x04001919 RID: 6425
		readyForRarePrimrose,
		// Token: 0x0400191A RID: 6426
		readyForGoldPile,
		// Token: 0x0400191B RID: 6427
		unlockedTownHall,
		// Token: 0x0400191C RID: 6428
		storedAnItemWithParcel,
		// Token: 0x0400191D RID: 6429
		beatCR8HardMode5,
		// Token: 0x0400191E RID: 6430
		beatSatchelHardMode5,
		// Token: 0x0400191F RID: 6431
		beatToteHardMode5,
		// Token: 0x04001920 RID: 6432
		beatPochetteHardMode5,
		// Token: 0x04001921 RID: 6433
		beatPurseHardMode5,
		// Token: 0x04001922 RID: 6434
		unlockedGoldenQuillswish,
		// Token: 0x04001923 RID: 6435
		hasBeenBlessed,
		// Token: 0x04001924 RID: 6436
		builtTheTemple
	}
}
