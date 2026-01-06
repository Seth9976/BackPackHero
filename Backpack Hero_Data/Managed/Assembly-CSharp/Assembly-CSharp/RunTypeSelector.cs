using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000164 RID: 356
public class RunTypeSelector : MonoBehaviour
{
	// Token: 0x06000E34 RID: 3636 RVA: 0x0008D2B0 File Offset: 0x0008B4B0
	public Missions GetMissionFromName(string name)
	{
		foreach (Missions missions in this.allMissions)
		{
			if (missions.name == name)
			{
				return missions;
			}
		}
		return null;
	}

	// Token: 0x06000E35 RID: 3637 RVA: 0x0008D314 File Offset: 0x0008B514
	public Missions GetMissionFromLanguageKeyAndNum(string name, int num)
	{
		foreach (Missions missions in this.allMissions)
		{
			if (missions.runTypeLanguageKey == name && missions.runTypeNumber == num)
			{
				return missions;
			}
		}
		return null;
	}

	// Token: 0x06000E36 RID: 3638 RVA: 0x0008D380 File Offset: 0x0008B580
	public Missions GetMissionFromListOfProperties(List<RunType.RunProperty> properties)
	{
		foreach (Missions missions in this.allMissions)
		{
			bool flag = true;
			foreach (RunType.RunProperty runProperty in missions.runProperties)
			{
				bool flag2 = false;
				foreach (RunType.RunProperty runProperty2 in properties)
				{
					if (runProperty.type == runProperty2.type && runProperty.assignedPrefabs == runProperty2.assignedPrefabs && runProperty.value == runProperty2.value)
					{
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				return missions;
			}
		}
		return null;
	}

	// Token: 0x06000E37 RID: 3639 RVA: 0x0008D494 File Offset: 0x0008B694
	private void Start()
	{
		if (Singleton.Instance.storyMode && MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedCR8) && !MetaProgressSaveManager.main.missionsUnlocked.Contains(Missions.Stringify(this.CR8TutorialRun)))
		{
			MetaProgressSaveManager.main.missionsUnlocked.Add(Missions.Stringify(this.CR8TutorialRun));
		}
		this.availableMissions.Clear();
		this.completedMissions.Clear();
		using (List<string>.Enumerator enumerator = MetaProgressSaveManager.main.missionsUnlocked.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				string x2 = enumerator.Current;
				Missions missions = this.allMissions.Find((Missions y) => Missions.Stringify(y) == x2);
				if (missions != null)
				{
					this.availableMissions.Add(missions);
				}
			}
		}
		using (List<string>.Enumerator enumerator = MetaProgressSaveManager.main.missionsComplete.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				string x = enumerator.Current;
				Missions missions2 = this.allMissions.Find((Missions y) => Missions.Stringify(y) == x);
				if (missions2 != null)
				{
					this.completedMissions.Add(missions2);
				}
			}
		}
		for (int i = 0; i < this.availableMissions.Count; i++)
		{
			if (this.completedMissions.Contains(this.availableMissions[i]))
			{
				this.availableMissions.RemoveAt(i);
				i--;
			}
		}
		this.LoadRunTypes();
	}

	// Token: 0x06000E38 RID: 3640 RVA: 0x0008D648 File Offset: 0x0008B848
	private void Update()
	{
		if (Singleton.Instance.developmentMode && Input.GetKeyDown(KeyCode.F1))
		{
			this.availableMissions = new List<Missions>(this.allMissions);
			this.LoadRunTypes();
		}
	}

	// Token: 0x06000E39 RID: 3641 RVA: 0x0008D67C File Offset: 0x0008B87C
	public void LoadRunTypes()
	{
		if (this.ironManToggle)
		{
			this.ironManToggle.isOn = Singleton.Instance.ironMan;
		}
		foreach (object obj in this.runTypeButtonParentTransform)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
		if (!this.isMissionSelect)
		{
			foreach (RunType runType in this.runTypes)
			{
				if (runType.validForCharacters.Count == 0 || runType.validForCharacters.Contains(Character.CharacterName.Any) || runType.validForCharacters.Contains(Singleton.Instance.character))
				{
					Object.Instantiate<GameObject>(this.runTypePrefab, Vector3.zero, Quaternion.identity, this.runTypeButtonParentTransform).GetComponentInChildren<RunTypeButton>().runType = runType;
				}
			}
			base.StartCoroutine(this.SelectRunType(Singleton.Instance.runType));
		}
		else
		{
			Singleton.Instance.doTutorial = false;
			Singleton.Instance.completedTutorial = true;
			Singleton.Instance.storyMode = true;
			this.showingAvailable = true;
			this.completedButton.GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f);
			this.availableButton.GetComponentInChildren<Image>().color = new Color(0.5f, 0.5f, 0.5f);
			this.ShowMissions();
		}
		LangaugeManager.main.SetFont(base.transform);
	}

	// Token: 0x06000E3A RID: 3642 RVA: 0x0008D838 File Offset: 0x0008BA38
	public void ShowAvailableMissions()
	{
		this.completedButton.GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f);
		this.availableButton.GetComponentInChildren<Image>().color = new Color(0.5f, 0.5f, 0.5f);
		this.showingAvailable = true;
		this.ShowMissions();
	}

	// Token: 0x06000E3B RID: 3643 RVA: 0x0008D89C File Offset: 0x0008BA9C
	public void ShowCompletedMissions()
	{
		this.completedButton.GetComponentInChildren<Image>().color = new Color(0.5f, 0.5f, 0.5f);
		this.availableButton.GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f);
		this.showingAvailable = false;
		this.ShowMissions();
	}

	// Token: 0x06000E3C RID: 3644 RVA: 0x0008D900 File Offset: 0x0008BB00
	private void ShowMissions()
	{
		while (this.runTypeButtonParentTransform.childCount > 0)
		{
			GameObject gameObject = this.runTypeButtonParentTransform.GetChild(0).gameObject;
			gameObject.transform.SetParent(null);
			gameObject.transform.position = new Vector3(10000f, 10000f, 10000f);
			Object.Destroy(gameObject);
		}
		if (this.showingAvailable)
		{
			if (Singleton.Instance.character == Character.CharacterName.Purse || Singleton.Instance.character == Character.CharacterName.Satchel || (Singleton.Instance.character == Character.CharacterName.Tote && MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedStandardRunForTote)) || Singleton.Instance.character == Character.CharacterName.Pochette || (Singleton.Instance.character == Character.CharacterName.CR8 && MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedStandardRunForCR8)))
			{
				RunTypeButton componentInChildren = Object.Instantiate<GameObject>(this.runTypePrefab, Vector3.zero, Quaternion.identity, this.runTypeButtonParentTransform).GetComponentInChildren<RunTypeButton>();
				RectTransform component = componentInChildren.GetComponent<RectTransform>();
				component.sizeDelta = new Vector2(component.sizeDelta.x, component.sizeDelta.y + 100f);
				switch (Singleton.Instance.character)
				{
				case Character.CharacterName.Purse:
					componentInChildren.mission = this.standardRun;
					break;
				case Character.CharacterName.Satchel:
					componentInChildren.mission = this.standardRunSatchel;
					break;
				case Character.CharacterName.Tote:
					componentInChildren.mission = this.standardRunTote;
					break;
				case Character.CharacterName.CR8:
					componentInChildren.mission = this.standardRunCR8;
					break;
				case Character.CharacterName.Pochette:
					componentInChildren.mission = this.standardRunPochette;
					break;
				}
			}
			this.availableMissions = this.availableMissions.OrderBy((Missions x) => x.ToString()).ToList<Missions>();
			using (List<Missions>.Enumerator enumerator = this.availableMissions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Missions missions = enumerator.Current;
					if (!(missions == this.standardRun) && !(missions == this.standardRunSatchel) && !(missions == this.standardRunTote) && !(missions == this.standardRunPochette) && !(missions == this.standardRunCR8) && missions.validForCharacter.characterName == Singleton.Instance.character)
					{
						Object.Instantiate<GameObject>(this.runTypePrefab, Vector3.zero, Quaternion.identity, this.runTypeButtonParentTransform).GetComponentInChildren<RunTypeButton>().mission = missions;
					}
				}
				goto IL_036E;
			}
		}
		this.completedMissions = this.completedMissions.OrderBy((Missions x) => x.ToString()).ToList<Missions>();
		foreach (Missions missions2 in this.completedMissions)
		{
			if (!(missions2 == this.standardRun) && !(missions2 == this.standardRunSatchel) && !(missions2 == this.standardRunTote) && !(missions2 == this.standardRunPochette) && !(missions2 == this.standardRunCR8) && missions2.validForCharacter.characterName == Singleton.Instance.character)
			{
				Object.Instantiate<GameObject>(this.runTypePrefab, Vector3.zero, Quaternion.identity, this.runTypeButtonParentTransform).GetComponentInChildren<RunTypeButton>().mission = missions2;
			}
		}
		IL_036E:
		if (this.runTypeButtonParentTransform.childCount > 0)
		{
			DigitalCursor.main.SelectUIElement(this.runTypeButtonParentTransform.GetChild(0).gameObject);
			Button componentInChildren2 = this.runTypeButtonParentTransform.GetChild(0).GetComponentInChildren<Button>();
			if (componentInChildren2)
			{
				componentInChildren2.onClick.Invoke();
			}
		}
		LangaugeManager.main.SetFont(this.runTypeButtonParentTransform);
	}

	// Token: 0x06000E3D RID: 3645 RVA: 0x0008DCF8 File Offset: 0x0008BEF8
	private void ClearRewards()
	{
		foreach (object obj in this.rewardsTransform)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
	}

	// Token: 0x06000E3E RID: 3646 RVA: 0x0008DD54 File Offset: 0x0008BF54
	public void AddRewards(List<GameObject> objs, List<Missions> missions)
	{
		this.ClearRewards();
		foreach (GameObject gameObject in objs)
		{
			Overworld_InventoryItemButton componentInChildren = Object.Instantiate<GameObject>(this.rewardsPrefab, Vector3.zero, Quaternion.identity, this.rewardsTransform).GetComponentInChildren<Overworld_InventoryItemButton>();
			componentInChildren.draggable = false;
			componentInChildren.Setup(gameObject, -1);
			Item2 component = gameObject.GetComponent<Item2>();
			if (component && !component.itemType.Contains(Item2.ItemType.Loot))
			{
				componentInChildren.ShowAsNewUnlock();
			}
		}
		foreach (Missions missions2 in missions)
		{
			Overworld_InventoryItemButton componentInChildren2 = Object.Instantiate<GameObject>(this.rewardsPrefab, Vector3.zero, Quaternion.identity, this.rewardsTransform).GetComponentInChildren<Overworld_InventoryItemButton>();
			componentInChildren2.draggable = false;
			componentInChildren2.Setup(missions2);
		}
	}

	// Token: 0x06000E3F RID: 3647 RVA: 0x0008DE5C File Offset: 0x0008C05C
	private IEnumerator SelectRunType(RunType runType)
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		foreach (RunTypeButton runTypeButton in base.GetComponentsInChildren<RunTypeButton>())
		{
			if (runTypeButton.runType == runType)
			{
				DigitalCursor.main.SelectUIElement(runTypeButton.gameObject);
				runTypeButton.GetComponent<Button>().OnSubmit(new BaseEventData(EventSystem.current));
				yield break;
			}
		}
		GameObject gameObject = this.runTypeButtonParentTransform.transform.GetChild(0).gameObject;
		DigitalCursor.main.SelectUIElement(gameObject);
		gameObject.GetComponent<Button>().OnSubmit(new BaseEventData(EventSystem.current));
		yield break;
	}

	// Token: 0x06000E40 RID: 3648 RVA: 0x0008DE72 File Offset: 0x0008C072
	public static void SetTextToBox(TextMeshProUGUI textBox, string x)
	{
		textBox.text = x;
		LangaugeManager.main.SetFont(textBox.transform);
	}

	// Token: 0x06000E41 RID: 3649 RVA: 0x0008DE8C File Offset: 0x0008C08C
	public static string GetTitleText(Missions mission)
	{
		string text = "<br><b><size=140%>";
		text = text + LangaugeManager.main.GetTextByKey(mission.runTypeLanguageKey) + "<br></size></b>";
		if (LangaugeManager.main.KeyExists(mission.runTypeLanguageKey + "d"))
		{
			text += "<br>";
			text += LangaugeManager.main.GetTextByKey(mission.runTypeLanguageKey + "d");
			text += "<br>";
		}
		return text;
	}

	// Token: 0x06000E42 RID: 3650 RVA: 0x0008DF11 File Offset: 0x0008C111
	public static string SetText(Missions mission)
	{
		return RunTypeSelector.GetTitleText(mission) + RunTypeSelector.GetProperties(mission.runProperties) + "<br>";
	}

	// Token: 0x06000E43 RID: 3651 RVA: 0x0008DF34 File Offset: 0x0008C134
	public static string SetText(RunType runType, bool hasWon, bool isLocked, List<RunType> lockedBehindRuns)
	{
		string text = "<br><b><size=140%>";
		text = text + LangaugeManager.main.GetTextByKey(runType.runTypeLanguageKey) + "<br></size></b>";
		if (LangaugeManager.main.KeyExists(runType.runTypeLanguageKey + "d"))
		{
			text += "<br>";
			text += LangaugeManager.main.GetTextByKey(runType.runTypeLanguageKey + "d");
			text += "<br>";
		}
		text += RunTypeSelector.GetProperties(runType.runProperties);
		if (hasWon)
		{
			text += "<br>";
			text = text + "<color=#DD9D40>" + LangaugeManager.main.GetTextByKey("rte1") + "</color>";
		}
		if (lockedBehindRuns.Count > 0)
		{
			MetaProgressSaveManager metaProgressSaveManager = Object.FindObjectOfType<MetaProgressSaveManager>();
			text += "<br><br>";
			if (isLocked)
			{
				text = text + "<color=#DD2222>" + LangaugeManager.main.GetTextByKey("rte2") + "</color>";
			}
			else
			{
				text += LangaugeManager.main.GetTextByKey("rte2b");
			}
			foreach (RunType runType2 in lockedBehindRuns)
			{
				bool flag = metaProgressSaveManager.RunTypeCompleted(runType2, Singleton.Instance.character, false);
				if (!flag)
				{
					text += "<color=#DD2222>";
				}
				text = text + "<br><size=80%> •" + LangaugeManager.main.GetTextByKey(runType2.runTypeLanguageKey) + "</size>";
				if (!flag)
				{
					text += "</color>";
				}
			}
		}
		text += "<br>";
		return text;
	}

	// Token: 0x06000E44 RID: 3652 RVA: 0x0008E0EC File Offset: 0x0008C2EC
	public static string GetProperties(List<RunType.RunProperty> runProperties)
	{
		string text = "";
		foreach (RunType.RunProperty runProperty in runProperties)
		{
			if (runProperty.showOnCard)
			{
				text += "<br>";
				if (runProperty.type == RunType.RunProperty.Type.noXP)
				{
					text += LangaugeManager.main.GetTextByKey("rtd1");
				}
				else if (runProperty.type == RunType.RunProperty.Type.mustKeep)
				{
					text += LangaugeManager.main.GetTextByKey("rtd2");
					text += RunTypeSelector.GetItemNames(runProperty.assignedPrefabs);
				}
				else if (runProperty.type == RunType.RunProperty.Type.getExtraItem)
				{
					if (runProperty.value > 0)
					{
						text += LangaugeManager.main.GetTextByKey("rtd3").Replace("/x", "+" + runProperty.value.ToString());
					}
					else
					{
						text += LangaugeManager.main.GetTextByKey("rtd3").Replace("/x", runProperty.value.ToString() ?? "");
					}
				}
				else if (runProperty.type == RunType.RunProperty.Type.increaseHP)
				{
					if (runProperty.value > 0)
					{
						text += LangaugeManager.main.GetTextByKey("rtd4").Replace("/x", "+" + runProperty.value.ToString());
					}
					else
					{
						text += LangaugeManager.main.GetTextByKey("rtd4").Replace("/x", runProperty.value.ToString() ?? "");
					}
				}
				else if (runProperty.type == RunType.RunProperty.Type.increaseXP)
				{
					if (runProperty.value > 0)
					{
						text += LangaugeManager.main.GetTextByKey("rtd5").Replace("/x", "+" + runProperty.value.ToString());
					}
					else
					{
						text += LangaugeManager.main.GetTextByKey("rtd5").Replace("/x", runProperty.value.ToString() ?? "");
					}
				}
				else if (runProperty.type == RunType.RunProperty.Type.increaseGold)
				{
					if (runProperty.value > 0)
					{
						text += LangaugeManager.main.GetTextByKey("rtd6").Replace("/x", "+" + runProperty.value.ToString());
					}
					else
					{
						text += LangaugeManager.main.GetTextByKey("rtd6").Replace("/x", runProperty.value.ToString() ?? "");
					}
				}
				else if (runProperty.type == RunType.RunProperty.Type.increaseEnemyHealth)
				{
					if (runProperty.value > 0)
					{
						text += LangaugeManager.main.GetTextByKey("rtd7").Replace("/x", "+" + runProperty.value.ToString());
					}
					else
					{
						text += LangaugeManager.main.GetTextByKey("rtd7").Replace("/x", runProperty.value.ToString() ?? "");
					}
				}
				else if (runProperty.type == RunType.RunProperty.Type.limitBackpackWidth)
				{
					text += LangaugeManager.main.GetTextByKey("rtd8").Replace("/x", runProperty.value.ToString() ?? "");
				}
				else if (runProperty.type == RunType.RunProperty.Type.allItemsCommonOrUncommon)
				{
					if (runProperty.value > 0)
					{
						text += LangaugeManager.main.GetTextByKey("rtd9").Replace("/x", "+" + runProperty.value.ToString());
					}
					else
					{
						text += LangaugeManager.main.GetTextByKey("rtd9").Replace("/x", runProperty.value.ToString() ?? "");
					}
				}
				else if (runProperty.type == RunType.RunProperty.Type.startWith)
				{
					text += LangaugeManager.main.GetTextByKey("rtd10");
					text += RunTypeSelector.GetItemNames(runProperty.assignedPrefabs);
				}
				else if (runProperty.type == RunType.RunProperty.Type.cannotFind)
				{
					text += LangaugeManager.main.GetTextByKey("rtd11");
					text += RunTypeSelector.GetItemNames(runProperty.assignedPrefabs);
				}
				else if (runProperty.type == RunType.RunProperty.Type.chestsContainExtra)
				{
					if (runProperty.value > 0)
					{
						text += LangaugeManager.main.GetTextByKey("rtd12").Replace("/x", "+" + runProperty.value.ToString());
					}
					else
					{
						text += LangaugeManager.main.GetTextByKey("rtd12").Replace("/x", runProperty.value.ToString() ?? "");
					}
				}
				else if (runProperty.type == RunType.RunProperty.Type.enemyAttacksMultipler)
				{
					if (runProperty.value > 0)
					{
						text += LangaugeManager.main.GetTextByKey("rtd13").Replace("/x", "+" + runProperty.value.ToString());
					}
					else
					{
						text += LangaugeManager.main.GetTextByKey("rtd13").Replace("/x", runProperty.value.ToString() ?? "");
					}
				}
				else if (runProperty.type == RunType.RunProperty.Type.cannotFindItemOfType)
				{
					text += LangaugeManager.main.GetTextByKey("rtd14");
					text += RunTypeSelector.GetItemTypes(runProperty.itemTypes);
				}
				else if (runProperty.type == RunType.RunProperty.Type.bonusDamage)
				{
					if (runProperty.value > 0)
					{
						text += LangaugeManager.main.GetTextByKey("rtd15").Replace("/x", "+" + runProperty.value.ToString());
					}
					else
					{
						text += LangaugeManager.main.GetTextByKey("rtd15").Replace("/x", runProperty.value.ToString() ?? "");
					}
				}
				else if (runProperty.type == RunType.RunProperty.Type.increaseLuck)
				{
					if (runProperty.value > 0)
					{
						text += LangaugeManager.main.GetTextByKey("rtd16").Replace("/x", "+" + runProperty.value.ToString());
					}
					else
					{
						text += LangaugeManager.main.GetTextByKey("rtd16").Replace("/x", runProperty.value.ToString() ?? "");
					}
				}
				else if (runProperty.type == RunType.RunProperty.Type.changeStatusEffectsToDamage)
				{
					text += LangaugeManager.main.GetTextByKey("rtd17");
				}
				else if (runProperty.type == RunType.RunProperty.Type.startFromMatt)
				{
					text += LangaugeManager.main.GetTextByKey("rtd18");
				}
				else if (runProperty.type == RunType.RunProperty.Type.setHP)
				{
					text += LangaugeManager.main.GetTextByKey("rtd19").Replace("/x", runProperty.value.ToString() ?? "");
				}
				else if (runProperty.type == RunType.RunProperty.Type.replaceItemGetWithChest)
				{
					text += LangaugeManager.main.GetTextByKey("rtd20");
				}
				else if (runProperty.type == RunType.RunProperty.Type.replaceItemGetWithStore)
				{
					text += LangaugeManager.main.GetTextByKey("rtd21");
				}
				else if (runProperty.type == RunType.RunProperty.Type.itemSizeExact)
				{
					text += LangaugeManager.main.GetTextByKey("rtd22").Replace("/x", runProperty.value.ToString() ?? "");
				}
				else if (runProperty.type == RunType.RunProperty.Type.itemSizeLarger)
				{
					text += LangaugeManager.main.GetTextByKey("rtd23").Replace("/x", runProperty.value.ToString() ?? "");
				}
				else if (runProperty.type == RunType.RunProperty.Type.scalingEnergy)
				{
					text += LangaugeManager.main.GetTextByKey("rtd24").Replace("/x", runProperty.value.ToString() ?? "");
				}
				else if (runProperty.type == RunType.RunProperty.Type.setEnergy)
				{
					text += LangaugeManager.main.GetTextByKey("rtd25").Replace("/x", runProperty.value.ToString() ?? "");
				}
				else if (runProperty.type == RunType.RunProperty.Type.noRelics)
				{
					text += LangaugeManager.main.GetTextByKey("rtd26");
				}
				else if (runProperty.type == RunType.RunProperty.Type.oneFloorBeforeBoss)
				{
					text += LangaugeManager.main.GetTextByKey("rtd27");
				}
				else if (runProperty.type == RunType.RunProperty.Type.enemiesWontPoison)
				{
					text += LangaugeManager.main.GetTextByKey("rtd28");
				}
				else if (runProperty.type == RunType.RunProperty.Type.dontStartWithStandard)
				{
					text += LangaugeManager.main.GetTextByKey("rtd29");
				}
				else if (runProperty.type == RunType.RunProperty.Type.puzzleMode)
				{
					text += LangaugeManager.main.GetTextByKey("rtd30");
					text += RunTypeSelector.GetItemNames(runProperty.assignedPrefabs);
				}
				else if (runProperty.type == RunType.RunProperty.Type.enemiesWontCurse)
				{
					text += LangaugeManager.main.GetTextByKey("rtd31");
				}
				else if (runProperty.type == RunType.RunProperty.Type.startingBackpackSize)
				{
					text += LangaugeManager.main.GetTextByKey("rtd32").Replace("/x", runProperty.value.ToString() ?? "");
				}
				else if (runProperty.type == RunType.RunProperty.Type.bossRush)
				{
					text += LangaugeManager.main.GetTextByKey("rtd33");
				}
				else if (runProperty.type == RunType.RunProperty.Type.startFromAssignedPrefab)
				{
					text += LangaugeManager.main.GetTextByKey("rtd34");
					text += RunTypeSelector.GetItemNames(runProperty.assignedPrefabs);
				}
				else if (runProperty.type == RunType.RunProperty.Type.replaceAllItems)
				{
					text += LangaugeManager.main.GetTextByKey("rtd35");
				}
				else if (runProperty.type == RunType.RunProperty.Type.unlimitedForges)
				{
					text += LangaugeManager.main.GetTextByKey("rtd36");
				}
				else if (runProperty.type == RunType.RunProperty.Type.youCanStillFindItemsOfExludedTypeIfTheyAlsoHaveThisType)
				{
					text += LangaugeManager.main.GetTextByKey("rtd37");
					text += RunTypeSelector.GetItemTypes(runProperty.itemTypes);
				}
				else if (runProperty.type == RunType.RunProperty.Type.youCanSeeTheWholeMap)
				{
					text += LangaugeManager.main.GetTextByKey("rtd38");
				}
				else if (runProperty.type == RunType.RunProperty.Type.additionalEncounter)
				{
					text += LangaugeManager.main.GetTextByKey("rtd39");
					text += RunTypeSelector.GetItemNames(runProperty.assignedPrefabs);
				}
				else if (runProperty.type == RunType.RunProperty.Type.replaceEncounter)
				{
					text += LangaugeManager.main.GetTextByKey("rtd40");
					text += RunTypeSelector.GetItemNames(new List<GameObject> { runProperty.assignedPrefabs[0] });
					text += "<br><br>";
					text += LangaugeManager.main.GetTextByKey("rtd40b");
					text += RunTypeSelector.GetItemNames(new List<GameObject> { runProperty.assignedPrefabs[1] });
				}
				else if (runProperty.type == RunType.RunProperty.Type.increaseChanceOfFindingItemType)
				{
					text += LangaugeManager.main.GetTextByKey("rtd41");
					text += RunTypeSelector.GetItemTypes(runProperty.itemTypes);
				}
				else if (runProperty.type == RunType.RunProperty.Type.increaseChanceOfFindingItemType)
				{
					text += LangaugeManager.main.GetTextByKey("rtd41");
					text += RunTypeSelector.GetItemTypes(runProperty.itemTypes);
				}
				else if (runProperty.type == RunType.RunProperty.Type.runEndsAfterZone1)
				{
					text += LangaugeManager.main.GetTextByKey("rtd42").Replace("/x", "1");
				}
				else if (runProperty.type == RunType.RunProperty.Type.runEndsAfterZone2)
				{
					text += LangaugeManager.main.GetTextByKey("rtd42").Replace("/x", "2");
				}
				else if (runProperty.type == RunType.RunProperty.Type.runEndsAfterZone3)
				{
					text += LangaugeManager.main.GetTextByKey("rtd42").Replace("/x", "3");
				}
				text += "<br>";
			}
		}
		return text;
	}

	// Token: 0x06000E45 RID: 3653 RVA: 0x0008EDC4 File Offset: 0x0008CFC4
	private static string GetItemTypes(List<Item2.ItemType> itemTypes)
	{
		string text = "";
		foreach (Item2.ItemType itemType in itemTypes)
		{
			text += "<br><size=80%>  •";
			string textByKey = LangaugeManager.main.GetTextByKey(itemType.ToString());
			text = text + textByKey + "</size>";
		}
		return text;
	}

	// Token: 0x06000E46 RID: 3654 RVA: 0x0008EE44 File Offset: 0x0008D044
	private static string GetItemNames(List<GameObject> gameObjects)
	{
		List<string> list = new List<string>();
		List<int> list2 = new List<int>();
		foreach (GameObject gameObject in gameObjects)
		{
			string textByKey = LangaugeManager.main.GetTextByKey(Item2.GetDisplayName(gameObject.name));
			if (!list.Contains(textByKey))
			{
				list.Add(textByKey);
				list2.Add(1);
			}
			else
			{
				int num = list.IndexOf(textByKey);
				List<int> list3 = list2;
				int num2 = num;
				int num3 = list3[num2];
				list3[num2] = num3 + 1;
			}
		}
		string text = "";
		for (int i = 0; i < list2.Count; i++)
		{
			text += "<br><size=80%>  •";
			if (list2[i] == 1)
			{
				text += list[i];
			}
			else
			{
				text = text + list2[i].ToString() + "x " + list[i];
			}
			text += "</size>";
		}
		return text;
	}

	// Token: 0x06000E47 RID: 3655 RVA: 0x0008EF64 File Offset: 0x0008D164
	public void IronManToggle()
	{
		Singleton.Instance.ironMan = this.ironManToggle.isOn;
	}

	// Token: 0x06000E48 RID: 3656 RVA: 0x0008EF7C File Offset: 0x0008D17C
	public void SaveOverworldAndGoAdventuring()
	{
		Singleton.Instance.doTutorial = false;
		Singleton.Instance.loadSave = false;
		if (!this.selectedRunTypeButton.UnlockedRunType())
		{
			PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm78"));
			return;
		}
		MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.runsPlayed, 1);
		MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.firstRunStartedFromOverworld);
		if (MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.townRaided))
		{
			MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.returnedToDungeonAfterRaid);
		}
		if (MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.gaveDadPradasNecklace))
		{
			MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.returnedToDungeonAfterGaveDadNecklace);
		}
		base.StartCoroutine(this.WaitForSave());
	}

	// Token: 0x06000E49 RID: 3657 RVA: 0x0008F01F File Offset: 0x0008D21F
	private IEnumerator WaitForSave()
	{
		Overworld_SaveManager saveManager = Object.FindObjectOfType<Overworld_SaveManager>();
		if (saveManager)
		{
			saveManager.SaveCommand(false);
		}
		yield return new WaitForSeconds(0.25f);
		while (saveManager.isSavingOrLoading)
		{
			yield return null;
		}
		this.GoAdventuring();
		yield break;
	}

	// Token: 0x06000E4A RID: 3658 RVA: 0x0008F030 File Offset: 0x0008D230
	public void GoAdventuring()
	{
		MenuManager menuManager = Object.FindObjectOfType<MenuManager>();
		if (!this.selectedRunTypeButton.UnlockedRunType())
		{
			string textByKey = LangaugeManager.main.GetTextByKey("rte3");
			if (menuManager)
			{
				menuManager.CreatePopUp(textByKey, Object.FindObjectOfType<Canvas>().transform.InverseTransformPoint(DigitalCursor.main.transform.position), 0.5f);
			}
			return;
		}
		if (menuManager)
		{
			menuManager.GoAdventuring();
			return;
		}
		this.GoAdventuring2();
	}

	// Token: 0x06000E4B RID: 3659 RVA: 0x0008F0B0 File Offset: 0x0008D2B0
	private void GoAdventuring2()
	{
		Singleton.Instance.otherPrefabObjectsToSpawn = new List<GameObject>();
		foreach (Overworld_InventoryItemButton overworld_InventoryItemButton in Object.FindObjectsOfType<Overworld_InventoryItemButton>())
		{
			if (!(overworld_InventoryItemButton.item == null) && overworld_InventoryItemButton.optionItem)
			{
				if (overworld_InventoryItemButton.item.oneOfAKindType == Item2.OneOfAKindType.OneTotal)
				{
					if (PopUpManager.main)
					{
						PopUpManager.main.CreatePopUp("This item cannot be taken", overworld_InventoryItemButton.transform.position);
					}
					return;
				}
				MetaProgressSaveManager.main.storedItems.Remove(Item2.GetDisplayName(overworld_InventoryItemButton.item.name));
				Singleton.Instance.otherPrefabObjectsToSpawn.Add(overworld_InventoryItemButton.item.gameObject);
			}
		}
		RunType runType = Singleton.Instance.runType;
		if (RunTypeManager.CheckForRunPropertyInRunType(RunType.RunProperty.Type.doTutorial, runType) != null)
		{
			Singleton.Instance.doTutorial = true;
			Singleton.Instance.completedTutorial = false;
		}
		SceneLoader.main.LoadScene("Game", LoadSceneMode.Single, null, null);
	}

	// Token: 0x04000B91 RID: 2961
	public bool isMissionSelect;

	// Token: 0x04000B92 RID: 2962
	private bool showingAvailable = true;

	// Token: 0x04000B93 RID: 2963
	[SerializeField]
	private GameObject availableButton;

	// Token: 0x04000B94 RID: 2964
	[SerializeField]
	private GameObject completedButton;

	// Token: 0x04000B95 RID: 2965
	[SerializeField]
	private List<RunType> runTypes = new List<RunType>();

	// Token: 0x04000B96 RID: 2966
	[SerializeField]
	private Missions standardRun;

	// Token: 0x04000B97 RID: 2967
	[SerializeField]
	private Missions standardRunSatchel;

	// Token: 0x04000B98 RID: 2968
	[SerializeField]
	private Missions standardRunTote;

	// Token: 0x04000B99 RID: 2969
	[SerializeField]
	private Missions standardRunPochette;

	// Token: 0x04000B9A RID: 2970
	[SerializeField]
	private Missions standardRunCR8;

	// Token: 0x04000B9B RID: 2971
	[SerializeField]
	private Missions CR8TutorialRun;

	// Token: 0x04000B9C RID: 2972
	[SerializeField]
	private List<Missions> availableMissions = new List<Missions>();

	// Token: 0x04000B9D RID: 2973
	[SerializeField]
	private List<Missions> completedMissions = new List<Missions>();

	// Token: 0x04000B9E RID: 2974
	[SerializeField]
	private GameObject runTypePrefab;

	// Token: 0x04000B9F RID: 2975
	[SerializeField]
	private Transform runTypeButtonParentTransform;

	// Token: 0x04000BA0 RID: 2976
	[SerializeField]
	private TextMeshProUGUI runTypeDescriptorText;

	// Token: 0x04000BA1 RID: 2977
	[SerializeField]
	private Toggle ironManToggle;

	// Token: 0x04000BA2 RID: 2978
	[SerializeField]
	private GameObject rewardsPrefab;

	// Token: 0x04000BA3 RID: 2979
	[SerializeField]
	private Transform rewardsTransform;

	// Token: 0x04000BA4 RID: 2980
	public RunTypeButton selectedRunTypeButton;

	// Token: 0x04000BA5 RID: 2981
	[SerializeField]
	public List<Missions> allMissions = new List<Missions>();
}
