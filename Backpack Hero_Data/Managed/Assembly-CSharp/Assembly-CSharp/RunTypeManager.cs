using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Token: 0x02000163 RID: 355
public class RunTypeManager : MonoBehaviour
{
	// Token: 0x17000021 RID: 33
	// (get) Token: 0x06000E1F RID: 3615 RVA: 0x0008C5D6 File Offset: 0x0008A7D6
	public static RunTypeManager Instance
	{
		get
		{
			return RunTypeManager._instance;
		}
	}

	// Token: 0x06000E20 RID: 3616 RVA: 0x0008C5E0 File Offset: 0x0008A7E0
	public static bool CheckIfAllRunPropertiesExist(List<RunType.RunProperty.Type> types)
	{
		if (types.Count == 0)
		{
			return true;
		}
		RunTypeManager runTypeManager = RunTypeManager.Instance;
		if (!runTypeManager)
		{
			RunTypeManager._instance = Object.FindObjectOfType<RunTypeManager>();
			if (RunTypeManager._instance)
			{
				runTypeManager = RunTypeManager.Instance;
			}
		}
		if (!runTypeManager || runTypeManager.runProperties == null || runTypeManager.runProperties.Count == 0)
		{
			return false;
		}
		List<RunType.RunProperty> list = runTypeManager.runProperties;
		foreach (RunType.RunProperty.Type type in types)
		{
			bool flag = false;
			using (List<RunType.RunProperty>.Enumerator enumerator2 = list.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.type == type)
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000E21 RID: 3617 RVA: 0x0008C6D4 File Offset: 0x0008A8D4
	public static bool CheckIfRunPropertiesExist(List<RunType.RunProperty.Type> types)
	{
		if (types.Count == 0)
		{
			return true;
		}
		RunTypeManager runTypeManager = RunTypeManager.Instance;
		if (!runTypeManager)
		{
			RunTypeManager._instance = Object.FindObjectOfType<RunTypeManager>();
			if (RunTypeManager._instance)
			{
				runTypeManager = RunTypeManager.Instance;
			}
		}
		if (!runTypeManager || runTypeManager.runProperties == null || runTypeManager.runProperties.Count == 0)
		{
			return false;
		}
		foreach (RunType.RunProperty runProperty in runTypeManager.runProperties)
		{
			if (types.Contains(runProperty.type))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000E22 RID: 3618 RVA: 0x0008C788 File Offset: 0x0008A988
	public static void AddProperty(RunType.RunProperty.Type type, GameObject assignedPrefabs)
	{
		RunTypeManager.AddProperty(type, new List<GameObject> { assignedPrefabs });
	}

	// Token: 0x06000E23 RID: 3619 RVA: 0x0008C79C File Offset: 0x0008A99C
	public static void AddProperty(RunType.RunProperty.Type type, List<GameObject> assignedPrefabs = null)
	{
		RunTypeManager runTypeManager = RunTypeManager.Instance;
		if (!runTypeManager)
		{
			RunTypeManager._instance = Object.FindObjectOfType<RunTypeManager>();
			if (RunTypeManager._instance)
			{
				runTypeManager = RunTypeManager.Instance;
			}
		}
		if (!runTypeManager || runTypeManager.runProperties == null)
		{
			return;
		}
		List<RunType.RunProperty> list = runTypeManager.runProperties;
		foreach (RunType.RunProperty runProperty in list)
		{
			if (runProperty.type == type)
			{
				if (assignedPrefabs != null)
				{
					runProperty.assignedPrefabs.AddRange(assignedPrefabs);
				}
				return;
			}
		}
		RunType.RunProperty runProperty2 = new RunType.RunProperty();
		runProperty2.type = type;
		if (assignedPrefabs != null)
		{
			runProperty2.assignedPrefabs.AddRange(assignedPrefabs);
		}
		list.Add(runProperty2);
		runProperty2.showOnCard = false;
	}

	// Token: 0x06000E24 RID: 3620 RVA: 0x0008C86C File Offset: 0x0008AA6C
	public static void AddItemToProperty(GameObject item, RunType.RunProperty.Type type)
	{
		RunTypeManager runTypeManager = RunTypeManager.Instance;
		if (!runTypeManager)
		{
			RunTypeManager._instance = Object.FindObjectOfType<RunTypeManager>();
			if (RunTypeManager._instance)
			{
				runTypeManager = RunTypeManager.Instance;
			}
		}
		if (!runTypeManager || runTypeManager.runProperties == null || runTypeManager.runProperties.Count == 0)
		{
			return;
		}
		List<RunType.RunProperty> list = runTypeManager.runProperties;
		foreach (RunType.RunProperty runProperty in list)
		{
			if (runProperty.type == type)
			{
				runProperty.assignedGameObject.Add(item);
				return;
			}
		}
		list.Add(new RunType.RunProperty
		{
			type = type,
			assignedGameObject = { item }
		});
	}

	// Token: 0x06000E25 RID: 3621 RVA: 0x0008C93C File Offset: 0x0008AB3C
	public static RunType.RunProperty CheckForRunProperty(RunType.RunProperty.Type type)
	{
		RunTypeManager runTypeManager = RunTypeManager.Instance;
		if (!runTypeManager)
		{
			RunTypeManager._instance = Object.FindObjectOfType<RunTypeManager>();
			if (RunTypeManager._instance)
			{
				runTypeManager = RunTypeManager.Instance;
			}
		}
		if (!runTypeManager || runTypeManager.runProperties == null || runTypeManager.runProperties.Count == 0)
		{
			return null;
		}
		foreach (RunType.RunProperty runProperty in runTypeManager.runProperties)
		{
			if (runProperty.type == type)
			{
				return runProperty;
			}
		}
		return null;
	}

	// Token: 0x06000E26 RID: 3622 RVA: 0x0008C9E4 File Offset: 0x0008ABE4
	public static RunType.RunProperty CheckForRunPropertyInRunType(RunType.RunProperty.Type type, RunType runType)
	{
		if (!runType)
		{
			return null;
		}
		foreach (RunType.RunProperty runProperty in runType.runProperties)
		{
			if (runProperty.type == type)
			{
				return runProperty;
			}
		}
		return null;
	}

	// Token: 0x06000E27 RID: 3623 RVA: 0x0008CA4C File Offset: 0x0008AC4C
	public static bool CheckIfAssignedItemIsInProperty(RunType.RunProperty.Type type, GameObject checkGameObject)
	{
		RunType.RunProperty runProperty = RunTypeManager.CheckForRunProperty(type);
		return runProperty && runProperty.assignedGameObject.Contains(checkGameObject);
	}

	// Token: 0x06000E28 RID: 3624 RVA: 0x0008CA7C File Offset: 0x0008AC7C
	public void CreateNPC(RunType.RunProperty.Type type)
	{
		foreach (RunType.RunProperty runProperty in this.runProperties)
		{
			if (runProperty.type == type)
			{
				foreach (GameObject gameObject in runProperty.assignedPrefabs)
				{
					Player main = Player.main;
					GameManager main2 = GameManager.main;
					Object.Instantiate<GameObject>(gameObject, Vector3.zero, Quaternion.identity, main.transform.parent).transform.localPosition = new Vector3(main2.spawnPosition.position.x - 1f, gameObject.transform.position.y, -2.5f);
				}
			}
		}
	}

	// Token: 0x06000E29 RID: 3625 RVA: 0x0008CB7C File Offset: 0x0008AD7C
	private void CreateRequired(RunType.RunProperty.Type type)
	{
		foreach (RunType.RunProperty runProperty in this.runProperties)
		{
			if (runProperty.type == type)
			{
				foreach (GameObject gameObject in runProperty.assignedPrefabs)
				{
					GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, Vector3.zero, Quaternion.identity);
					SpriteRenderer component = gameObject2.GetComponent<SpriteRenderer>();
					if (component)
					{
						component.sharedMaterial = GameManager.main.outlineItemMaterial;
					}
					runProperty.assignedGameObject.Add(gameObject2);
				}
			}
		}
	}

	// Token: 0x06000E2A RID: 3626 RVA: 0x0008CC4C File Offset: 0x0008AE4C
	private void CreateStarterItems()
	{
		foreach (RunType.RunProperty runProperty in this.runProperties)
		{
			if (runProperty.type == RunType.RunProperty.Type.startWith)
			{
				foreach (GameObject gameObject in runProperty.assignedPrefabs)
				{
					GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, Vector3.zero, Quaternion.identity);
					if (gameObject2.GetComponent<Carving>())
					{
						base.StartCoroutine(this.AddToDeck(gameObject2));
					}
				}
			}
			if (runProperty.type == RunType.RunProperty.Type.startWithItemFromTown)
			{
				foreach (GameObject gameObject3 in Singleton.Instance.otherPrefabObjectsToSpawn)
				{
					Object.Instantiate<GameObject>(gameObject3, Vector3.zero, Quaternion.identity);
				}
			}
		}
	}

	// Token: 0x06000E2B RID: 3627 RVA: 0x0008CD68 File Offset: 0x0008AF68
	private IEnumerator AddToDeck(GameObject x)
	{
		yield return new WaitForSeconds(2f);
		Tote.main.AddNewCarvingToUndrawn(x);
		yield break;
	}

	// Token: 0x06000E2C RID: 3628 RVA: 0x0008CD77 File Offset: 0x0008AF77
	public void AssignRunTypeFromSingleton()
	{
		this.runType = Singleton.Instance.runType;
		this.AssignRunType();
	}

	// Token: 0x06000E2D RID: 3629 RVA: 0x0008CD8F File Offset: 0x0008AF8F
	public void ResetRunType()
	{
		this.runType = this.standardRunType;
		Singleton.Instance.runType = this.standardRunType;
	}

	// Token: 0x06000E2E RID: 3630 RVA: 0x0008CDB0 File Offset: 0x0008AFB0
	private void SetupMetaProgress()
	{
		if (MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedMatthew))
		{
			RunType.RunProperty runProperty = new RunType.RunProperty();
			runProperty.type = RunType.RunProperty.Type.startFromMatt;
			this.runProperties.Add(runProperty);
		}
	}

	// Token: 0x06000E2F RID: 3631 RVA: 0x0008CDE8 File Offset: 0x0008AFE8
	public void AssignRunType()
	{
		if (Singleton.Instance.runType == null)
		{
			Singleton.Instance.runType = this.standardRunType;
		}
		if (this.runType == null)
		{
			this.runType = this.standardRunType;
		}
		this.runProperties = new List<RunType.RunProperty>();
		if (Singleton.Instance.mission)
		{
			MetaProgressSaveManager.main.AddMission(Singleton.Instance.mission);
			this.missions = Singleton.Instance.mission;
			this.runType = null;
			foreach (MetaProgressSaveManager.MetaProgressMarker metaProgressMarker in this.missions.temporaryMetaProgressMarker)
			{
				MetaProgressSaveManager.main.AddMetaProgressMarkerTemporary(metaProgressMarker);
			}
			foreach (RunType.RunProperty runProperty in this.missions.GetAllRunProperties())
			{
				RunType.RunProperty runProperty2 = new RunType.RunProperty();
				runProperty2.Clone(runProperty);
				this.runProperties.Add(runProperty2);
			}
			if (!MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedDeepCave) && !MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedEnchantedSwamp))
			{
				RunType.RunProperty runProperty3 = new RunType.RunProperty();
				runProperty3.type = RunType.RunProperty.Type.runEndsAfterZone1;
				this.runProperties.Add(runProperty3);
			}
			if (!MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedMagmaCore) && !MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedFrozenHeart))
			{
				RunType.RunProperty runProperty4 = new RunType.RunProperty();
				runProperty4.type = RunType.RunProperty.Type.runEndsAfterZone2;
				this.runProperties.Add(runProperty4);
			}
		}
		else
		{
			foreach (RunType.RunProperty runProperty5 in this.runType.runProperties)
			{
				RunType.RunProperty runProperty6 = new RunType.RunProperty();
				runProperty6.Clone(runProperty5);
				this.runProperties.Add(runProperty6);
			}
			Debug.Log("Run type set  - " + this.runType.name);
		}
		this.SetRunText();
	}

	// Token: 0x06000E30 RID: 3632 RVA: 0x0008D020 File Offset: 0x0008B220
	public void SetRunText()
	{
		if (!this.text)
		{
			this.text = GameObject.FindGameObjectWithTag("RunTypeText");
		}
		if (this.text)
		{
			this.text.SetActive(true);
			if (!this.text2)
			{
				this.text2 = this.text.GetComponentInChildren<TextMeshProUGUI>();
			}
			if (this.text2)
			{
				if (this.missions && LangaugeManager.main.KeyExists(this.missions.runTypeLanguageKey))
				{
					this.text2.text = LangaugeManager.main.GetTextByKey(this.missions.runTypeLanguageKey);
				}
				else if (!this.runType || !LangaugeManager.main.KeyExists(this.runType.runTypeLanguageKey))
				{
					this.text2.text = "";
				}
				else
				{
					this.text2.text = LangaugeManager.main.GetTextByKey(this.runType.runTypeLanguageKey);
				}
				LangaugeManager.main.SetFont(this.text2.transform);
			}
		}
	}

	// Token: 0x06000E31 RID: 3633 RVA: 0x0008D148 File Offset: 0x0008B348
	public void StartNewRun()
	{
		this.CreateNPC(RunType.RunProperty.Type.startFromAssignedPrefab);
		Debug.Log("Created NPC");
		this.CreateRequired(RunType.RunProperty.Type.mustKeep);
		Debug.Log("Created must keep");
		this.CreateRequired(RunType.RunProperty.Type.puzzleMode);
		Debug.Log("Created puzzle mode");
		this.CreateStarterItems();
		Debug.Log("Created starter items");
		Player main = Player.main;
		main.NewRun();
		Debug.Log("Initialized player for run");
		RunType.RunProperty runProperty = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.increaseHP);
		if (runProperty)
		{
			Debug.Log("Added property " + runProperty.type.ToString());
			main.stats.SetMaxHP(main.stats.maxHealth + runProperty.value);
			main.stats.SetHealth(main.stats.maxHealth);
		}
		runProperty = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.setHP);
		if (runProperty)
		{
			Debug.Log("Added property " + runProperty.type.ToString());
			main.stats.SetMaxHP(runProperty.value);
			main.stats.ChangeHealth(runProperty.value, null, false);
		}
		runProperty = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.resetTutorials);
		if (runProperty)
		{
			Debug.Log("Added property " + runProperty.type.ToString());
			TutorialManager.main.completedTutorials = new List<string>();
		}
	}

	// Token: 0x06000E32 RID: 3634 RVA: 0x0008D2A5 File Offset: 0x0008B4A5
	private void Update()
	{
	}

	// Token: 0x04000B89 RID: 2953
	private static RunTypeManager _instance;

	// Token: 0x04000B8A RID: 2954
	public RunType runType;

	// Token: 0x04000B8B RID: 2955
	public Missions missions;

	// Token: 0x04000B8C RID: 2956
	[SerializeField]
	public RunType standardRunType;

	// Token: 0x04000B8D RID: 2957
	[SerializeField]
	public Missions standardMissions;

	// Token: 0x04000B8E RID: 2958
	public List<RunType.RunProperty> runProperties;

	// Token: 0x04000B8F RID: 2959
	private GameObject text;

	// Token: 0x04000B90 RID: 2960
	private TextMeshProUGUI text2;
}
