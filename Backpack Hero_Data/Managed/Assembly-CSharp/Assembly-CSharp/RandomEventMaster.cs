using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000CF RID: 207
public class RandomEventMaster : MonoBehaviour
{
	// Token: 0x060005F5 RID: 1525 RVA: 0x0003AA5A File Offset: 0x00038C5A
	private void OnEnable()
	{
		if (this.finished)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		DigitalCursor.main.SelectFirstSelectableInElement(base.transform);
	}

	// Token: 0x060005F6 RID: 1526 RVA: 0x0003AA80 File Offset: 0x00038C80
	private void Start()
	{
		if (this.runEventToAdd != MetaProgressSaveManager.LastRun.RunEvents.none)
		{
			MetaProgressSaveManager.main.AddRunEvent(this.runEventToAdd);
		}
		if (this.dungeonEvent && this.dungeonEvent.GetEventProperty(DungeonEvent.EventProperty.Type.selectedType) != null)
		{
			int eventPropertyValue = this.dungeonEvent.GetEventPropertyValue(DungeonEvent.EventProperty.Type.selectedType);
			this.randomType = (Item2.ItemType)eventPropertyValue;
		}
		else
		{
			if (!Singleton.Instance.storyMode || MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedArchery))
			{
				this.randomTypes.Add(Item2.ItemType.Arrow);
				this.randomTypes.Add(Item2.ItemType.Bow);
			}
			if (!Singleton.Instance.storyMode || MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedMagic))
			{
				this.randomTypes.Add(Item2.ItemType.ManaStone);
				this.randomTypes.Add(Item2.ItemType.Magic);
			}
			this.randomType = this.randomTypes[Random.Range(0, this.randomTypes.Count)];
			if (this.dungeonEvent)
			{
				this.dungeonEvent.AddEventProperty(DungeonEvent.EventProperty.Type.selectedType, (int)this.randomType);
			}
		}
		string text = Player.main.characterName.ToString();
		if (this.eventName)
		{
			if (LangaugeManager.main.KeyExists(this.eventTextKey + "n" + text))
			{
				this.eventName.text = LangaugeManager.main.GetTextByKey(this.eventTextKey + "n" + text);
			}
			else if (LangaugeManager.main.KeyExists(this.eventTextKey + "n"))
			{
				this.eventName.text = LangaugeManager.main.GetTextByKey(this.eventTextKey + "n");
			}
		}
		if (this.flavorText)
		{
			if (LangaugeManager.main.KeyExists(this.eventTextKey + "o1" + text))
			{
				this.flavorText.text = LangaugeManager.main.GetTextByKey(this.eventTextKey + "o1" + text);
			}
			else if (LangaugeManager.main.KeyExists(this.eventTextKey + "o1"))
			{
				this.flavorText.text = LangaugeManager.main.GetTextByKey(this.eventTextKey + "o1");
			}
		}
		if (this.buttons)
		{
			this.UpdateGoldCosts();
			this.SetButtonText();
			TutorialManager tutorialManager = Object.FindObjectOfType<TutorialManager>();
			if (this.maxButtons != -1 && (tutorialManager.playType != TutorialManager.PlayType.testing || !tutorialManager.showAllEventOptions))
			{
				List<GameObject> list = new List<GameObject>();
				for (int i = 0; i < this.buttons.childCount; i++)
				{
					GameObject gameObject = this.buttons.GetChild(i).gameObject;
					if (gameObject.GetComponent<EventButton>().alwaysOn)
					{
						gameObject.SetActive(true);
					}
					else if (gameObject.activeInHierarchy)
					{
						list.Add(gameObject);
						gameObject.SetActive(false);
					}
				}
				int num = 0;
				List<int> list2 = new List<int>();
				if (this.dungeonEvent)
				{
					DungeonEvent.EventProperty eventProperty = this.dungeonEvent.GetEventProperty(DungeonEvent.EventProperty.Type.randomSeed);
					if (eventProperty != null)
					{
						list2 = eventProperty.values;
					}
				}
				while (num < this.maxButtons && list.Count > 0)
				{
					int num2 = Random.Range(0, list.Count);
					if (num < list2.Count)
					{
						num2 = list2[num];
					}
					else
					{
						list2.Add(num2);
					}
					list[num2].SetActive(true);
					list.RemoveAt(num2);
					num++;
				}
				if (this.dungeonEvent)
				{
					this.dungeonEvent.AddEventProperty(DungeonEvent.EventProperty.Type.randomSeed, list2);
				}
			}
		}
		if (!this.gameManager)
		{
			this.gameManager = GameManager.main;
		}
		this.clickMe = GameObject.FindGameObjectWithTag("ClickMe");
		if (this.clickMe)
		{
			this.clickMe.SetActive(false);
		}
		this.time = 0f;
		if (this.flavorText)
		{
			this.flavorText.maxVisibleCharacters = 0;
		}
		base.transform.localPosition = Vector3.zero;
		LangaugeManager.main.SetFont(base.transform);
		this.Open();
		if (this.buttons)
		{
			DigitalCursor.main.SelectUIElement(this.buttons.GetComponentInChildren<Button>().gameObject);
			Transform child = this.buttons.GetChild(this.buttons.childCount - 1);
			if (child)
			{
				DigitalInputSelectOnButton component = child.GetComponent<DigitalInputSelectOnButton>();
				if (component != null)
				{
					component.SelectMeOnCancel();
				}
			}
		}
		if (this.eventTextKey == "ev39")
		{
			this.flavorText.text = this.flavorText.text.Replace("/x", LangaugeManager.main.GetTextByKey(this.randomType.ToString()));
			this.buttonToChangeRequirementAndOutcome.SetItemType(this.randomType);
		}
	}

	// Token: 0x060005F7 RID: 1527 RVA: 0x0003AF5C File Offset: 0x0003915C
	private void SetButtonText()
	{
		string text = Player.main.characterName.ToString();
		int i = 0;
		while (i < this.buttons.childCount)
		{
			this.buttons.gameObject.SetActive(true);
			TextMeshProUGUI componentInChildren = this.buttons.GetChild(i).GetComponentInChildren<TextMeshProUGUI>();
			EventButton componentInChildren2 = this.buttons.GetChild(i).GetComponentInChildren<EventButton>();
			if (!componentInChildren || !componentInChildren2)
			{
				goto IL_01D4;
			}
			if (componentInChildren2.validForCharacters.Contains(Player.main.characterName) || componentInChildren2.validForCharacters.Count <= 0 || componentInChildren2.validForCharacters.Contains(Character.CharacterName.Any))
			{
				string text2 = "";
				if (componentInChildren2.requiredGold > 0)
				{
					text2 = "<color=\"yellow\">[" + LangaugeManager.main.GetTextByKey("evg").Replace("/x", componentInChildren2.requiredGold.ToString() ?? "") + "]</color> ";
				}
				string text3 = this.eventTextKey + "b" + (i + 1).ToString();
				if (componentInChildren2.overrideButtonTextKey.Length > 1)
				{
					text3 = componentInChildren2.overrideButtonTextKey;
				}
				else
				{
					componentInChildren2.overrideButtonTextKey = text3;
				}
				string text4;
				if (LangaugeManager.main.KeyExists(text3 + text))
				{
					text4 = LangaugeManager.main.GetTextByKey(text3 + text);
				}
				else
				{
					text4 = LangaugeManager.main.GetTextByKey(text3);
				}
				text4 = text4.Replace("/x", componentInChildren2.GetModifierValue(0).ToString() ?? "");
				text4 = text4.Replace("/y", componentInChildren2.GetModifierValue(1).ToString() ?? "");
				componentInChildren.text = text2 + text4;
				goto IL_01D4;
			}
			componentInChildren2.gameObject.SetActive(false);
			IL_020C:
			i++;
			continue;
			IL_01D4:
			if (i == this.buttons.childCount - 1)
			{
				goto IL_020C;
			}
			InputHandler component = this.buttons.GetChild(i).gameObject.GetComponent<InputHandler>();
			if (component)
			{
				Object.Destroy(component);
				goto IL_020C;
			}
			goto IL_020C;
		}
	}

	// Token: 0x060005F8 RID: 1528 RVA: 0x0003B18C File Offset: 0x0003938C
	public void UpdateGoldCosts()
	{
		int num = -1;
		if (this.dungeonEvent)
		{
			num = this.dungeonEvent.GetEventPropertyValue(DungeonEvent.EventProperty.Type.increaseCost);
		}
		for (int i = 0; i < this.buttons.childCount; i++)
		{
			bool activeInHierarchy = this.buttons.GetChild(i).gameObject.activeInHierarchy;
			this.buttons.GetChild(i).gameObject.SetActive(true);
			EventButton componentInChildren = this.buttons.GetChild(i).GetComponentInChildren<EventButton>();
			if (componentInChildren)
			{
				if (componentInChildren.startingGold == -1)
				{
					componentInChildren.startingGold = componentInChildren.requiredGold;
				}
				if (num != -1 && componentInChildren.requiredGold > 0)
				{
					componentInChildren.requiredGold = componentInChildren.startingGold + num;
				}
			}
			this.buttons.GetChild(i).gameObject.SetActive(activeInHierarchy);
		}
		this.SetButtonText();
	}

	// Token: 0x060005F9 RID: 1529 RVA: 0x0003B266 File Offset: 0x00039466
	public static bool IsOpen()
	{
		return Object.FindObjectOfType<RandomEventMaster>();
	}

	// Token: 0x060005FA RID: 1530 RVA: 0x0003B277 File Offset: 0x00039477
	public void Open()
	{
		if (!this.gameManager)
		{
			this.gameManager = GameManager.main;
		}
		this.gameManager.viewingEvent = true;
	}

	// Token: 0x060005FB RID: 1531 RVA: 0x0003B29D File Offset: 0x0003949D
	public void Close()
	{
		if (!this.gameManager)
		{
			this.gameManager = GameManager.main;
		}
		this.gameManager.viewingEvent = false;
	}

	// Token: 0x060005FC RID: 1532 RVA: 0x0003B2C4 File Offset: 0x000394C4
	private void Update()
	{
		if ((this.eventBoxAnimator && this.gameManager && !this.eventBoxAnimator.gameObject.activeInHierarchy) || (this.finished && this.finishedTime > 0.6f))
		{
			Object.Destroy(base.gameObject);
			return;
		}
		if (this.finished)
		{
			this.finishedTime += Time.deltaTime;
		}
		this.time += Time.deltaTime;
		if (this.time > 0.015f || (Input.GetMouseButton(0) && this.time > 0.0075f))
		{
			if (this.flavorText)
			{
				TextMeshProUGUI textMeshProUGUI = this.flavorText;
				int maxVisibleCharacters = textMeshProUGUI.maxVisibleCharacters;
				textMeshProUGUI.maxVisibleCharacters = maxVisibleCharacters + 1;
			}
			this.time = 0f;
		}
	}

	// Token: 0x060005FD RID: 1533 RVA: 0x0003B399 File Offset: 0x00039599
	public void NewText(EventButton buttonPressed, string text)
	{
		if (this.chosenButton)
		{
			return;
		}
		this.chosenButton = buttonPressed;
		this.flavorText.maxVisibleCharacters = 0;
		this.flavorText.text = text;
	}

	// Token: 0x060005FE RID: 1534 RVA: 0x0003B3C8 File Offset: 0x000395C8
	public void NewTextBlank(string text)
	{
		this.flavorText.maxVisibleCharacters = 0;
		this.flavorText.text = text;
	}

	// Token: 0x060005FF RID: 1535 RVA: 0x0003B3E4 File Offset: 0x000395E4
	public void RemoveButtons()
	{
		bool flag = true;
		using (List<EventButton.EventButtonAction>.Enumerator enumerator = this.chosenButton.chosenOutCome.eventButtonActions.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.action == EventButton.EventButtonAction.Action.repeatWithFlavorText)
				{
					flag = false;
				}
			}
		}
		if (!flag)
		{
			this.chosenButton.FullEffect(this.dungeonEvent);
			this.chosenButton = null;
			this.finished = false;
			this.buttons.gameObject.SetActive(true);
			EventButton[] componentsInChildren = base.GetComponentsInChildren<EventButton>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].played = false;
			}
			this.UpdateGoldCosts();
			return;
		}
		this.buttons.GetComponent<Animator>().Play("Out");
		this.doneButton.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey("b7");
		this.doneButton.SetActive(true);
		DigitalCursor.main.SelectUIElement(this.doneButton);
	}

	// Token: 0x06000600 RID: 1536 RVA: 0x0003B4F0 File Offset: 0x000396F0
	public void PlaySpecials()
	{
		DungeonEventSpecial component = base.GetComponent<DungeonEventSpecial>();
		if (component)
		{
			component.Play();
		}
	}

	// Token: 0x06000601 RID: 1537 RVA: 0x0003B514 File Offset: 0x00039714
	public void EndChoose()
	{
		if (GameManager.main && GameManager.main.inventoryPhase == GameManager.InventoryPhase.choose && GameManager.main.eventButton && GameManager.main.eventButton.requirement == EventButton.Requirements.carvingSelectNonForged)
		{
			GameManager.main.inventoryPhase = GameManager.InventoryPhase.open;
		}
		this.EndEvent();
	}

	// Token: 0x06000602 RID: 1538 RVA: 0x0003B570 File Offset: 0x00039770
	public void EndEvent()
	{
		if (this.finished)
		{
			return;
		}
		GameObject gameObject = GameObject.FindGameObjectWithTag("PopUpSpriteMask");
		if (gameObject)
		{
			gameObject.GetComponent<SpriteMask>().enabled = false;
		}
		GameManager.main.viewingEvent = false;
		this.finished = true;
		if (this.chosenButton)
		{
			this.chosenButton.FullEffect(this.dungeonEvent);
		}
		if (this.eventBoxAnimator)
		{
			this.eventBoxAnimator.Play("Out");
		}
	}

	// Token: 0x040004C6 RID: 1222
	[SerializeField]
	private MetaProgressSaveManager.LastRun.RunEvents runEventToAdd;

	// Token: 0x040004C7 RID: 1223
	[SerializeField]
	public Transform buttons;

	// Token: 0x040004C8 RID: 1224
	[SerializeField]
	private TextMeshProUGUI flavorText;

	// Token: 0x040004C9 RID: 1225
	[SerializeField]
	public GameObject doneButton;

	// Token: 0x040004CA RID: 1226
	[SerializeField]
	public Animator eventBoxAnimator;

	// Token: 0x040004CB RID: 1227
	[SerializeField]
	public string reopenText = "";

	// Token: 0x040004CC RID: 1228
	[HideInInspector]
	public EventNPC npc;

	// Token: 0x040004CD RID: 1229
	public DungeonEvent dungeonEvent;

	// Token: 0x040004CE RID: 1230
	[HideInInspector]
	public EventButton chosenButton;

	// Token: 0x040004CF RID: 1231
	public bool finished;

	// Token: 0x040004D0 RID: 1232
	private float finishedTime;

	// Token: 0x040004D1 RID: 1233
	private GameManager gameManager;

	// Token: 0x040004D2 RID: 1234
	private float time;

	// Token: 0x040004D3 RID: 1235
	private GameObject clickMe;

	// Token: 0x040004D4 RID: 1236
	[SerializeField]
	private TextMeshProUGUI eventName;

	// Token: 0x040004D5 RID: 1237
	[SerializeField]
	public string eventTextKey;

	// Token: 0x040004D6 RID: 1238
	[SerializeField]
	public string eventGreetingsAndOverridesTextKey;

	// Token: 0x040004D7 RID: 1239
	[SerializeField]
	private int maxButtons = -1;

	// Token: 0x040004D8 RID: 1240
	private List<Item2.ItemType> randomTypes = new List<Item2.ItemType>
	{
		Item2.ItemType.Accessory,
		Item2.ItemType.Armor,
		Item2.ItemType.Weapon,
		Item2.ItemType.Shield,
		Item2.ItemType.Consumable
	};

	// Token: 0x040004D9 RID: 1241
	private Item2.ItemType randomType = Item2.ItemType.Accessory;

	// Token: 0x040004DA RID: 1242
	[SerializeField]
	private EventButton buttonToChangeRequirementAndOutcome;
}
