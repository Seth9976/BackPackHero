using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000129 RID: 297
public class DebugMenu : MonoBehaviour
{
	// Token: 0x06000B21 RID: 2849 RVA: 0x00070A84 File Offset: 0x0006EC84
	private void Start()
	{
		this.buttonLists = new List<List<GameObject>> { this.dungeonButtons, this.battleButtons, this.roamingButtons };
		this.canvas = base.GetComponent<CanvasGroup>();
		base.GetComponent<Animator>().speed = 2f;
		this.canvas.alpha = 0f;
		this.canvas.interactable = false;
		this.canvas.blocksRaycasts = false;
	}

	// Token: 0x06000B22 RID: 2850 RVA: 0x00070B04 File Offset: 0x0006ED04
	private void Update()
	{
		if (!Singleton.Instance.modDebugging)
		{
			this.canvas.alpha = 0f;
			this.canvas.interactable = false;
			this.canvas.blocksRaycasts = false;
			this.visible = false;
			return;
		}
		if (((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown("d")) || this.toggleUI)
		{
			this.toggleUI = false;
			if (!this.visible)
			{
				ItemSpawnMenu itemSpawnMenu = Object.FindObjectOfType<ItemSpawnMenu>();
				if (itemSpawnMenu != null)
				{
					itemSpawnMenu.EndEvent();
				}
				ModSelection modSelection = Object.FindObjectOfType<ModSelection>();
				if (modSelection != null)
				{
					modSelection.EndEvent();
				}
				base.GetComponent<Animator>().Play("TwitchPollIn", 0, 0f);
				this.canvas.interactable = true;
				this.canvas.blocksRaycasts = true;
				this.visible = true;
			}
			else
			{
				base.GetComponent<Animator>().Play("TwitchPollOut", 0, 0f);
				this.canvas.interactable = false;
				this.canvas.blocksRaycasts = false;
				this.visible = false;
			}
		}
		if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown("s"))
		{
			if (Object.FindObjectOfType<ItemSpawnMenu>() == null)
			{
				this.OpenSpawnItemUI();
				ModSelection modSelection2 = Object.FindObjectOfType<ModSelection>();
				if (modSelection2 != null)
				{
					modSelection2.EndEvent();
				}
				if (this.visible)
				{
					this.toggleUI = true;
				}
			}
			else
			{
				ItemSpawnMenu itemSpawnMenu2 = Object.FindObjectOfType<ItemSpawnMenu>();
				if (itemSpawnMenu2 != null)
				{
					itemSpawnMenu2.EndEvent();
				}
			}
		}
		if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown("m"))
		{
			if (!Singleton.Instance.showingOptions && !GameManager.main.viewingEvent)
			{
				ItemSpawnMenu itemSpawnMenu3 = Object.FindObjectOfType<ItemSpawnMenu>();
				if (itemSpawnMenu3 != null)
				{
					itemSpawnMenu3.EndEvent();
				}
				if (this.visible)
				{
					this.toggleUI = true;
				}
				GameObject gameObject = Object.Instantiate<GameObject>(this.modSelectionMenu, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("UI Canvas").GetComponent<Canvas>().transform);
				gameObject.transform.SetAsLastSibling();
				gameObject.transform.localPosition = Vector3.zero;
				Singleton.Instance.showingOptions = true;
				GameManager.main.viewingEvent = true;
			}
			else
			{
				ModSelection modSelection3 = Object.FindObjectOfType<ModSelection>();
				if (modSelection3 != null)
				{
					modSelection3.EndEvent();
				}
			}
		}
		this.canvas.alpha = Mathf.Clamp(this.visible ? (this.canvas.alpha + 0.1f) : (this.canvas.alpha - 0.1f), -1f, 1f);
		List<DungeonEvent> list = Object.FindObjectsOfType<DungeonEvent>().ToList<DungeonEvent>();
		List<DungeonEvent> list2 = new List<DungeonEvent>();
		foreach (KeyValuePair<DungeonEvent, bool> keyValuePair in this.events)
		{
			if (!list.Contains(keyValuePair.Key))
			{
				list2.Add(keyValuePair.Key);
			}
		}
		if (this.walkThroughWalls)
		{
			using (List<DungeonEvent>.Enumerator enumerator2 = list.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					DungeonEvent dungeonEvent = enumerator2.Current;
					if (!this.events.ContainsKey(dungeonEvent))
					{
						this.events.Add(dungeonEvent, dungeonEvent.passable);
						dungeonEvent.passable = true;
					}
				}
				goto IL_0395;
			}
		}
		foreach (KeyValuePair<DungeonEvent, bool> keyValuePair2 in this.events)
		{
			try
			{
				keyValuePair2.Key.passable = keyValuePair2.Value;
			}
			catch
			{
			}
			list2.Add(keyValuePair2.Key);
		}
		IL_0395:
		foreach (DungeonEvent dungeonEvent2 in list2)
		{
			this.events.Remove(dungeonEvent2);
		}
		if (this.walkThroughWallsJustSwitched)
		{
			this.walkThroughWallsJustSwitched = false;
			Object.FindObjectOfType<DungeonPlayer>().FindReachableEvents();
		}
		List<string> list3 = new List<string>();
		if (ModLoader.main != null)
		{
			foreach (ModLoader.ModpackInfo modpackInfo in ModLoader.main.modpacks)
			{
				if (modpackInfo.loaded)
				{
					list3.Add(LangaugeManager.main.GetTextByKey(modpackInfo.displayName));
				}
			}
		}
		if (list3.Count == 0)
		{
			list3.Add("No modpack loaded");
			this.modpackSelector.enabled = false;
		}
		else
		{
			this.modpackSelector.enabled = true;
		}
		if (!list3.SequenceEqual(this.tempOptions))
		{
			int value = this.modpackSelector.value;
			this.modpackSelector.ClearOptions();
			this.modpackSelector.AddOptions(list3);
			this.tempOptions = new List<string>(list3);
			this.modpackSelector.value = value;
		}
		if (this.modpackSelector.value >= list3.Count)
		{
			this.modpackSelector.value = list3.Count;
		}
	}

	// Token: 0x06000B23 RID: 2851 RVA: 0x0007104C File Offset: 0x0006F24C
	public void ClickSpawnItem()
	{
		this.toggleUI = true;
		this.OpenSpawnItemUI();
	}

	// Token: 0x06000B24 RID: 2852 RVA: 0x0007105C File Offset: 0x0006F25C
	public void OpenSpawnItemUI()
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.itemSpawnBox, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("PrimaryCanvas").GetComponent<Canvas>().transform);
		gameObject.transform.SetAsLastSibling();
		gameObject.transform.localPosition = new Vector3(0f, -450f);
	}

	// Token: 0x06000B25 RID: 2853 RVA: 0x000710B8 File Offset: 0x0006F2B8
	public void DropSpawnLoot(TMP_Dropdown change)
	{
		if (GameManager.main.inventoryPhase != GameManager.InventoryPhase.open || GameManager.main.limitedItemReorganize)
		{
			return;
		}
		if (change.value == 0)
		{
			RunTypeManager runTypeManager = Object.FindObjectOfType<RunTypeManager>();
			RunType.RunProperty runProperty = new RunType.RunProperty();
			runProperty.type = RunType.RunProperty.Type.noRelics;
			runTypeManager.runProperties.Add(runProperty);
			GameManager.main.StartLimitedItemGetPeriod();
			runTypeManager.runProperties.Remove(runProperty);
			return;
		}
		if (change.value == 1)
		{
			DungeonLevel.Floor currentFloor = GameManager.main.dungeonLevel.currentFloor;
			GameManager.main.dungeonLevel.currentFloor = DungeonLevel.Floor.boss;
			GameManager.main.StartLimitedItemGetPeriod();
			GameManager.main.dungeonLevel.currentFloor = currentFloor;
			return;
		}
		Chest chest = base.gameObject.AddComponent<Chest>();
		SpriteRenderer spriteRenderer = base.gameObject.AddComponent<SpriteRenderer>();
		switch (change.value)
		{
		case 2:
			chest.type = Chest.Type.standard;
			break;
		case 3:
			chest.type = Chest.Type.carving;
			break;
		case 4:
			chest.type = Chest.Type.components;
			break;
		case 5:
			chest.type = Chest.Type.pets;
			break;
		}
		GameManager.main.ShowInventory();
		chest.Start();
		chest.OpenChest();
		Object.Destroy(chest);
		Object.Destroy(spriteRenderer);
	}

	// Token: 0x06000B26 RID: 2854 RVA: 0x000711E0 File Offset: 0x0006F3E0
	public void ClickHP()
	{
		Player.main.stats.ChangeHealth(10, null, false);
	}

	// Token: 0x06000B27 RID: 2855 RVA: 0x000711F8 File Offset: 0x0006F3F8
	public void ClickMaxHP()
	{
		Player main = Player.main;
		main.stats.SetMaxHP(main.stats.maxHealthBeforeItems + 5);
	}

	// Token: 0x06000B28 RID: 2856 RVA: 0x00071223 File Offset: 0x0006F423
	public void ClickAP()
	{
		Player.main.ChangeAP(1);
	}

	// Token: 0x06000B29 RID: 2857 RVA: 0x00071230 File Offset: 0x0006F430
	public void ClickFillMana()
	{
		foreach (ManaStone manaStone in Object.FindObjectsOfType<ManaStone>())
		{
			if (manaStone.gameObject.activeInHierarchy)
			{
				manaStone.currentPower = manaStone.maxPower;
			}
		}
	}

	// Token: 0x06000B2A RID: 2858 RVA: 0x00071270 File Offset: 0x0006F470
	public void ClickEXP()
	{
		Player main = Player.main;
		main.AddExperience(10, main.transform.position);
	}

	// Token: 0x06000B2B RID: 2859 RVA: 0x00071296 File Offset: 0x0006F496
	public void ClickGold()
	{
		GameManager.main.ChangeGold(20);
	}

	// Token: 0x06000B2C RID: 2860 RVA: 0x000712A4 File Offset: 0x0006F4A4
	public void DropAttackEnemies(TMP_Dropdown change)
	{
		foreach (Enemy enemy in Object.FindObjectsOfType<Enemy>())
		{
			switch (change.value)
			{
			case 0:
				enemy.stats.Attack(null, -10, null, false, false, false);
				break;
			case 1:
				enemy.stats.Attack(null, -99999, null, false, false, false);
				break;
			case 2:
				enemy.stats.ChangeHealth(-enemy.health, false, null);
				break;
			case 3:
				base.StartCoroutine(enemy.Die());
				break;
			case 4:
				base.StartCoroutine(enemy.RunAway());
				break;
			case 5:
				Object.Destroy(enemy.gameObject);
				break;
			}
		}
	}

	// Token: 0x06000B2D RID: 2861 RVA: 0x00071360 File Offset: 0x0006F560
	public void DropClearStatus(TMP_Dropdown change)
	{
		if (change.value == 0 || change.value == 1)
		{
			foreach (StatusEffect statusEffect in Player.main.stats.GetStatusEffects())
			{
				statusEffect.RemoveStatusEffect();
			}
		}
		if (change.value == 1 || change.value == 2)
		{
			foreach (CombatPet combatPet in CombatPet.GetPets())
			{
				foreach (StatusEffect statusEffect2 in combatPet.stats.GetStatusEffects())
				{
					statusEffect2.RemoveStatusEffect();
				}
			}
		}
		if (change.value == 3)
		{
			foreach (Enemy enemy in Enemy.GetEnemiesAlive())
			{
				foreach (StatusEffect statusEffect3 in enemy.stats.GetStatusEffects())
				{
					statusEffect3.RemoveStatusEffect();
				}
			}
		}
	}

	// Token: 0x06000B2E RID: 2862 RVA: 0x000714E0 File Offset: 0x0006F6E0
	public void ToggleWalkThroughWalls(Toggle toggle)
	{
		this.walkThroughWalls = toggle.isOn;
		this.walkThroughWallsJustSwitched = true;
	}

	// Token: 0x06000B2F RID: 2863 RVA: 0x000714F5 File Offset: 0x0006F6F5
	public void DropChangeFloor(TMP_Dropdown change)
	{
		this.PopUp("Currently not working. Sorry :(", change.gameObject.transform.position, 2f);
	}

	// Token: 0x06000B30 RID: 2864 RVA: 0x0007151C File Offset: 0x0006F71C
	private void ChangeFloorInternal(int num, int zone, bool isRelative, bool isIntro, bool changeZone)
	{
		GameManager main = GameManager.main;
	}

	// Token: 0x06000B31 RID: 2865 RVA: 0x00071528 File Offset: 0x0006F728
	private void PopUp(string text, Vector2 position, float time)
	{
		position = base.GetComponentInParent<Canvas>().transform.InverseTransformPoint(position);
		MenuManager menuManager = Object.FindObjectOfType<MenuManager>();
		if (menuManager)
		{
			menuManager.CreatePopUp(text, position, time);
			return;
		}
		GameManager main = GameManager.main;
		if (main)
		{
			main.CreatePopUp(text, position, time);
		}
	}

	// Token: 0x06000B32 RID: 2866 RVA: 0x00071581 File Offset: 0x0006F781
	public void ShowLogs()
	{
		Process.Start(Application.persistentDataPath + "/mods.log");
	}

	// Token: 0x06000B33 RID: 2867 RVA: 0x00071598 File Offset: 0x0006F798
	public void ReloadModpack()
	{
		if (!this.modpackSelector.enabled)
		{
			return;
		}
		ModLoader.main.ReloadModpack(ModLoader.main.modpacks.Where((ModLoader.ModpackInfo m) => m.loaded).ToList<ModLoader.ModpackInfo>()[this.modpackSelector.value]);
	}

	// Token: 0x06000B34 RID: 2868 RVA: 0x00071600 File Offset: 0x0006F800
	public void ReloadRespawnModpack()
	{
		if (!this.modpackSelector.enabled)
		{
			return;
		}
		this.ReloadModpack();
		base.StartCoroutine(this.RespawnItemsDelayed());
	}

	// Token: 0x06000B35 RID: 2869 RVA: 0x00071623 File Offset: 0x0006F823
	private IEnumerator RespawnItemsDelayed()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		ModItemLoader.main.ReplaceSpawnedItems();
		yield break;
	}

	// Token: 0x04000927 RID: 2343
	private List<GameObject> dungeonButtons;

	// Token: 0x04000928 RID: 2344
	private List<GameObject> battleButtons;

	// Token: 0x04000929 RID: 2345
	private List<GameObject> roamingButtons;

	// Token: 0x0400092A RID: 2346
	[HideInInspector]
	private List<List<GameObject>> buttonLists;

	// Token: 0x0400092B RID: 2347
	[SerializeField]
	private TMP_Dropdown modpackSelector;

	// Token: 0x0400092C RID: 2348
	[SerializeField]
	private GameObject itemSpawnBox;

	// Token: 0x0400092D RID: 2349
	[SerializeField]
	private GameObject modSelectionMenu;

	// Token: 0x0400092E RID: 2350
	[SerializeField]
	private GameObject modUIButtons;

	// Token: 0x0400092F RID: 2351
	private bool walkThroughWalls;

	// Token: 0x04000930 RID: 2352
	private bool walkThroughWallsJustSwitched;

	// Token: 0x04000931 RID: 2353
	private bool visible;

	// Token: 0x04000932 RID: 2354
	private bool toggleUI;

	// Token: 0x04000933 RID: 2355
	private List<string> tempOptions = new List<string>();

	// Token: 0x04000934 RID: 2356
	private CanvasGroup canvas;

	// Token: 0x04000935 RID: 2357
	private RunType.RunProperty property;

	// Token: 0x04000936 RID: 2358
	private Dictionary<DungeonEvent, bool> events = new Dictionary<DungeonEvent, bool>();
}
