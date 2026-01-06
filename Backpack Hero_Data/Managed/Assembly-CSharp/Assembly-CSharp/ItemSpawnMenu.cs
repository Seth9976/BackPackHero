using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

// Token: 0x02000066 RID: 102
public class ItemSpawnMenu : MonoBehaviour
{
	// Token: 0x060001FF RID: 511 RVA: 0x0000C528 File Offset: 0x0000A728
	private void Start()
	{
		this.eventBoxAnimator = base.GetComponentInChildren<Animator>();
		this.tutorialManager = Object.FindObjectOfType<TutorialManager>();
		ItemSpawnMenu.main = this;
		this.TextInput(true);
		base.StartCoroutine(this.DelayedInput());
	}

	// Token: 0x06000200 RID: 512 RVA: 0x0000C55B File Offset: 0x0000A75B
	private IEnumerator DelayedInput()
	{
		yield return new WaitForSeconds(0.2f);
		this.TextInput(false);
		yield break;
	}

	// Token: 0x06000201 RID: 513 RVA: 0x0000C56C File Offset: 0x0000A76C
	private void Update()
	{
		if (!this.eventBoxAnimator.gameObject.activeInHierarchy)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		List<string> list = new List<string> { "All", "Base Game" };
		if (ModLoader.main != null)
		{
			foreach (ModLoader.ModpackInfo modpackInfo in ModLoader.main.modpacks)
			{
				if (modpackInfo.loaded)
				{
					list.Add(LangaugeManager.main.GetTextByKey(modpackInfo.displayName));
				}
			}
		}
		if (!list.SequenceEqual(this.tempOptions))
		{
			int value = this.modpackSelector.value;
			this.modpackSelector.ClearOptions();
			this.modpackSelector.AddOptions(list);
			this.tempOptions = new List<string>(list);
			this.modpackSelector.value = value;
		}
		if (this.modpackSelector.value >= list.Count)
		{
			this.modpackSelector.value = list.Count;
		}
	}

	// Token: 0x06000202 RID: 514 RVA: 0x0000C690 File Offset: 0x0000A890
	public void TextInput(bool limit = false)
	{
		List<ModLoader.ModpackInfo> modpacks = new List<ModLoader.ModpackInfo>();
		if (this.modpackSelector.value == 0)
		{
			modpacks = ModLoader.main.modpacks.Where((ModLoader.ModpackInfo m) => m.loaded).ToList<ModLoader.ModpackInfo>();
		}
		if (this.modpackSelector.value > 1)
		{
			modpacks.Add(ModLoader.main.modpacks.Where((ModLoader.ModpackInfo m) => m.loaded).ToList<ModLoader.ModpackInfo>()[this.modpackSelector.value - 2]);
		}
		string text = this.inputField.text.Trim().ToLower();
		IEnumerable<Item2> enumerable = Enumerable.Empty<Item2>();
		if (this.modpackSelector.value <= 1)
		{
			enumerable = DebugItemManager.main.item2s.Where((Item2 i) => LangaugeManager.main.GetTextByKey(Item2.GetDisplayName(i.name)).Contains(text, StringComparison.CurrentCultureIgnoreCase));
		}
		IEnumerable<Item2> enumerable2 = Enumerable.Empty<Item2>();
		if (modpacks.Count > 0)
		{
			enumerable2 = ModItemLoader.main.modItems.Where((Item2 i) => LangaugeManager.main.GetTextByKey(Item2.GetDisplayName(i.name)).Contains(text, StringComparison.CurrentCultureIgnoreCase) && modpacks.Contains(i.GetComponent<ModdedItem>().fromModpack));
		}
		Debug.Log("Base: " + enumerable.Count<Item2>().ToString() + " Mod: " + enumerable2.Count<Item2>().ToString());
		List<Item2> list = (from i in enumerable.Concat(enumerable2)
			orderby LangaugeManager.main.GetTextByKey(Item2.GetDisplayName(i.name))
			select i).ToList<Item2>();
		while (this.itemButtonsParent.childCount > 0)
		{
			Object.DestroyImmediate(this.itemButtonsParent.GetChild(0).gameObject);
		}
		this.buttons = new List<GameObject>();
		int num = 0;
		foreach (Item2 item in list)
		{
			if (limit && num > 20)
			{
				break;
			}
			GameObject gameObject = Object.Instantiate<GameObject>(this.itemButtonPlaceholder, Vector3.zero, Quaternion.identity, this.itemButtonsParent);
			ItemSpawnMenuButton component = gameObject.GetComponent<ItemSpawnMenuButton>();
			component.placeholder = true;
			component.item = item;
			this.buttons.Add(gameObject);
			if (num < 20)
			{
				component.ReplaceWithButton();
			}
			num++;
		}
	}

	// Token: 0x06000203 RID: 515 RVA: 0x0000C904 File Offset: 0x0000AB04
	public void SpawnItem(Item2 item, Transform button)
	{
		ItemSpawner.InstantiateItemsFree(new List<Item2> { item }, true, default(Vector2));
	}

	// Token: 0x06000204 RID: 516 RVA: 0x0000C930 File Offset: 0x0000AB30
	private void PopUp(string text, Vector2 position, float time)
	{
		position = base.GetComponentInParent<Canvas>().transform.InverseTransformPoint(position);
		MenuManager menuManager = Object.FindObjectOfType<MenuManager>();
		if (menuManager)
		{
			menuManager.CreatePopUp(text, position, time);
			return;
		}
		GameManager gameManager = GameManager.main;
		if (gameManager)
		{
			gameManager.CreatePopUp(text, position, time);
		}
	}

	// Token: 0x06000205 RID: 517 RVA: 0x0000C98C File Offset: 0x0000AB8C
	public void EndEvent()
	{
		ItemSpawnMenu.main = null;
		if (this.finished)
		{
			return;
		}
		foreach (ItemSpawnMenuButton itemSpawnMenuButton in base.GetComponentsInChildren<ItemSpawnMenuButton>())
		{
			if (itemSpawnMenuButton.placeholder && itemSpawnMenuButton.transform.position.y < 0f)
			{
				Object.Destroy(itemSpawnMenuButton.gameObject);
			}
		}
		SoundManager.main.PlaySFX("menuBlip");
		this.finished = true;
		this.eventBoxAnimator.Play("Out");
		Singleton.Instance.showingOptions = false;
		GameManager gameManager = GameManager.main;
		if (gameManager)
		{
			gameManager.SetAllItemColliders(true);
			gameManager.viewingEvent = false;
		}
		MenuManager menuManager = Object.FindObjectOfType<MenuManager>();
		if (menuManager)
		{
			menuManager.ShowButtons();
		}
	}

	// Token: 0x0400014D RID: 333
	public static ItemSpawnMenu main;

	// Token: 0x0400014E RID: 334
	[SerializeField]
	private TMP_InputField inputField;

	// Token: 0x0400014F RID: 335
	private Animator eventBoxAnimator;

	// Token: 0x04000150 RID: 336
	[SerializeField]
	private Transform itemButtonsParent;

	// Token: 0x04000151 RID: 337
	[SerializeField]
	private GameObject itemButtonPlaceholder;

	// Token: 0x04000152 RID: 338
	[SerializeField]
	private TMP_Dropdown modpackSelector;

	// Token: 0x04000153 RID: 339
	private List<GameObject> buttons = new List<GameObject>();

	// Token: 0x04000154 RID: 340
	private TutorialManager tutorialManager;

	// Token: 0x04000155 RID: 341
	public bool finished;

	// Token: 0x04000156 RID: 342
	private List<string> tempOptions = new List<string>();
}
