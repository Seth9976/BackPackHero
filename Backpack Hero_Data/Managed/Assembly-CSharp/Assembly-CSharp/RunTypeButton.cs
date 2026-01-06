using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000162 RID: 354
public class RunTypeButton : MonoBehaviour
{
	// Token: 0x06000E1A RID: 3610 RVA: 0x0008BD0C File Offset: 0x00089F0C
	private void Start()
	{
		if (this.runType)
		{
			base.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey(this.runType.runTypeLanguageKey);
		}
		if (this.mission)
		{
			this.hasWonImage.SetActive(false);
			this.hasWonIronManImage.SetActive(false);
			this.isLockedImage.SetActive(false);
			base.GetComponentInChildren<TextMeshProUGUI>().text = Missions.MissionTranslationName(this.mission);
			if (!this.UnlockedRunType())
			{
				this.isLockedImage.SetActive(true);
			}
			return;
		}
		if (MetaProgressSaveManager.main.RunTypeCompleted(this.runType, Singleton.Instance.character, false))
		{
			this.hasWonImage.SetActive(true);
		}
		else
		{
			this.hasWonImage.SetActive(false);
		}
		if (MetaProgressSaveManager.main.RunTypeCompleted(this.runType, Singleton.Instance.character, true))
		{
			this.hasWonIronManImage.SetActive(true);
		}
		else
		{
			this.hasWonIronManImage.SetActive(false);
		}
		if (this.UnlockedRunType())
		{
			this.isLockedImage.SetActive(false);
			return;
		}
		this.isLockedImage.SetActive(true);
	}

	// Token: 0x06000E1B RID: 3611 RVA: 0x0008BE34 File Offset: 0x0008A034
	public bool UnlockedRunType()
	{
		if (!Singleton.Instance.allowOtherGameModes && base.transform.GetSiblingIndex() > 0)
		{
			return false;
		}
		if (this.mission)
		{
			if (!this.mission.runProperties.Find((RunType.RunProperty x) => x.type == RunType.RunProperty.Type.runEndsAfterZone2) || MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedDeepCave))
			{
				if (!this.mission.runProperties.Find((RunType.RunProperty x) => x.type == RunType.RunProperty.Type.runEndsAfterZone3) || MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedMagmaCore))
				{
					if (!this.mission.dungeonLevels.Find((DungeonLevel x) => x.zone == DungeonLevel.Zone.deepCave) || MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedDeepCave))
					{
						if (!this.mission.dungeonLevels.Find((DungeonLevel x) => x.zone == DungeonLevel.Zone.EnchantedSwamp) || MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedEnchantedSwamp))
						{
							if (!this.mission.dungeonLevels.Find((DungeonLevel x) => x.zone == DungeonLevel.Zone.ice) || MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedFrozenHeart))
							{
								if (!this.mission.dungeonLevels.Find((DungeonLevel x) => x.zone == DungeonLevel.Zone.magmaCore) || MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedMagmaCore))
								{
									if (!this.mission.dungeonLevels.Find((DungeonLevel x) => x.zone == DungeonLevel.Zone.theBramble) || MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedBramble))
									{
										goto IL_0219;
									}
								}
							}
						}
					}
				}
			}
			return false;
		}
		IL_0219:
		if (!this.runType && this.mission)
		{
			return true;
		}
		MetaProgressSaveManager main = MetaProgressSaveManager.main;
		foreach (RunType runType in this.runType.requiredToUnlock)
		{
			if (!main.RunTypeCompleted(runType, Singleton.Instance.character, false))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000E1C RID: 3612 RVA: 0x0008C0DC File Offset: 0x0008A2DC
	public void SetRunType()
	{
		RunTypeSelector runTypeSelector = Object.FindObjectOfType<RunTypeSelector>();
		if (runTypeSelector && runTypeSelector.selectedRunTypeButton)
		{
			runTypeSelector.selectedRunTypeButton.GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 1f);
		}
		SoundManager.main.PlaySFX("menuBlip");
		if (this.runType)
		{
			Singleton.Instance.runType = this.runType;
			Singleton.Instance.mission = null;
			string text = RunTypeSelector.SetText(this.runType, this.hasWonImage.activeInHierarchy, this.isLockedImage.activeInHierarchy, this.runType.requiredToUnlock);
			RunTypeSelector.SetTextToBox(GameObject.FindGameObjectWithTag("RunTypeText").GetComponentInChildren<TextMeshProUGUI>(), text);
			if (runTypeSelector)
			{
				runTypeSelector.selectedRunTypeButton = this;
			}
		}
		if (this.mission)
		{
			base.GetComponentInChildren<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
			Singleton.Instance.runType = null;
			Singleton.Instance.mission = this.mission;
			Singleton.Instance.completedTutorial = true;
			Transform transform = GameObject.FindGameObjectWithTag("RunTypeTextParent").transform;
			while (transform.childCount > 0)
			{
				Transform child = transform.GetChild(0);
				child.transform.SetParent(null);
				Object.Destroy(child.gameObject);
			}
			string text2 = "<br><b><size=140%>" + Missions.MissionTranslationName(this.mission) + "<br></size></b>";
			Object.Instantiate<GameObject>(this.textPrefab, transform).GetComponent<TextMeshProUGUI>().text = text2;
			if (MetaProgressSaveManager.main.missionsComplete.Contains(this.mission.name) && !this.mission.canBeRepeated)
			{
				string text3 = "<br><color=\"red\">" + LangaugeManager.main.GetTextByKey("mcomplete") + "</color>";
				Object.Instantiate<GameObject>(this.textPrefab, transform).GetComponent<TextMeshProUGUI>().text = text3;
			}
			if (LangaugeManager.main.KeyExists(this.mission.runTypeLanguageKey + "d"))
			{
				string text4 = "<br>";
				text4 += LangaugeManager.main.GetTextByKey(this.mission.runTypeLanguageKey + "d");
				text4 += "<br>";
				Object.Instantiate<GameObject>(this.textPrefab, transform).GetComponent<TextMeshProUGUI>().text = text4;
			}
			foreach (RunType.RunProperty runProperty in this.mission.runProperties)
			{
				RunType.RunProperty.Type type = runProperty.type;
				if (type != RunType.RunProperty.Type.mustKeep && type != RunType.RunProperty.Type.startWith)
				{
					if (type != RunType.RunProperty.Type.startWithItemFromTown)
					{
						string text5 = RunTypeSelector.GetProperties(new List<RunType.RunProperty> { runProperty });
						text5 = text5.Trim();
						if (text5.EndsWith("<br>"))
						{
							text5 = text5.Remove(text5.LastIndexOf("<br>"));
						}
						if (text5.Length > 0)
						{
							Object.Instantiate<GameObject>(this.textPrefab, transform).GetComponent<TextMeshProUGUI>().text = text5;
							continue;
						}
						continue;
					}
				}
				else
				{
					GameObject gameObject = Object.Instantiate<GameObject>(this.textPrefab, transform);
					string text6 = RunTypeSelector.GetProperties(new List<RunType.RunProperty> { runProperty }).Split(':', StringSplitOptions.None)[0];
					gameObject.GetComponent<TextMeshProUGUI>().text = text6;
					GameObject gameObject2 = Object.Instantiate<GameObject>(this.itemsPrefab, transform);
					using (List<GameObject>.Enumerator enumerator2 = runProperty.assignedPrefabs.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							GameObject gameObject3 = enumerator2.Current;
							Overworld_InventoryItemButton componentInChildren = Object.Instantiate<GameObject>(this.overworldItemButtonPrefab, gameObject2.transform).GetComponentInChildren<Overworld_InventoryItemButton>();
							componentInChildren.draggable = false;
							componentInChildren.Setup(gameObject3, -1);
						}
						continue;
					}
				}
				GameObject gameObject4 = Object.Instantiate<GameObject>(this.textPrefab, transform);
				string text7 = RunTypeSelector.GetProperties(new List<RunType.RunProperty> { runProperty }).Split(':', StringSplitOptions.None)[0];
				gameObject4.GetComponent<TextMeshProUGUI>().text = text7;
				GameObject gameObject5 = Object.Instantiate<GameObject>(this.itemsPrefab, transform);
				for (int i = 0; i < runProperty.value; i++)
				{
					Overworld_InventoryItemButton componentInChildren2 = Object.Instantiate<GameObject>(this.overworldItemButtonPrefab, gameObject5.transform).GetComponentInChildren<Overworld_InventoryItemButton>();
					componentInChildren2.draggable = false;
					componentInChildren2.SetupAsOptionItem();
				}
			}
			LangaugeManager.main.SetFont(transform);
			if (runTypeSelector)
			{
				runTypeSelector.selectedRunTypeButton = this;
				runTypeSelector.AddRewards(this.mission.rewards, this.mission.rewardsMissions);
			}
		}
	}

	// Token: 0x06000E1D RID: 3613 RVA: 0x0008C598 File Offset: 0x0008A798
	public static RunTypeButton FindRunTypeButton(RunType runType)
	{
		foreach (RunTypeButton runTypeButton in Object.FindObjectsOfType<RunTypeButton>())
		{
			if (runTypeButton.runType == runType)
			{
				return runTypeButton;
			}
		}
		return null;
	}

	// Token: 0x04000B81 RID: 2945
	public RunType runType;

	// Token: 0x04000B82 RID: 2946
	public Missions mission;

	// Token: 0x04000B83 RID: 2947
	[SerializeField]
	private GameObject hasWonImage;

	// Token: 0x04000B84 RID: 2948
	[SerializeField]
	private GameObject hasWonIronManImage;

	// Token: 0x04000B85 RID: 2949
	[SerializeField]
	private GameObject isLockedImage;

	// Token: 0x04000B86 RID: 2950
	[SerializeField]
	private GameObject textPrefab;

	// Token: 0x04000B87 RID: 2951
	[SerializeField]
	private GameObject itemsPrefab;

	// Token: 0x04000B88 RID: 2952
	[SerializeField]
	private GameObject overworldItemButtonPrefab;
}
