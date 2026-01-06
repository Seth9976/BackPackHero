using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Token: 0x020000C9 RID: 201
public class EventNPC : CustomInputHandler
{
	// Token: 0x060005CB RID: 1483 RVA: 0x000399BE File Offset: 0x00037BBE
	private void Start()
	{
		this.gameManager = GameManager.main;
		this.StartUp();
		this.SetupGreetings();
	}

	// Token: 0x060005CC RID: 1484 RVA: 0x000399D7 File Offset: 0x00037BD7
	private void OnEnable()
	{
		LangaugeManager.OnLanguageChanged += this.SetupGreetings;
	}

	// Token: 0x060005CD RID: 1485 RVA: 0x000399EA File Offset: 0x00037BEA
	private void OnDisable()
	{
		LangaugeManager.OnLanguageChanged -= this.SetupGreetings;
	}

	// Token: 0x060005CE RID: 1486 RVA: 0x00039A00 File Offset: 0x00037C00
	private void SetupGreetings()
	{
		if (!this.eventPrefab)
		{
			return;
		}
		RandomEventMaster component = this.eventPrefab.GetComponent<RandomEventMaster>();
		if (!component)
		{
			return;
		}
		string text = component.eventTextKey;
		if (component.eventGreetingsAndOverridesTextKey.Length > 0)
		{
			text = component.eventGreetingsAndOverridesTextKey;
		}
		this.greetings = new List<string>();
		this.farewells = new List<string>();
		for (int i = 0; i < 10; i++)
		{
			string text2 = text + "i" + i.ToString();
			if (LangaugeManager.main.KeyExists(text2))
			{
				this.greetings.Add(LangaugeManager.main.GetTextByKey(text2));
			}
		}
		for (int j = 0; j < 10; j++)
		{
			string text3 = text + "f" + j.ToString();
			if (LangaugeManager.main.KeyExists(text3))
			{
				this.farewells.Add(LangaugeManager.main.GetTextByKey(text3));
			}
		}
		if (this.greetings.Count == 0)
		{
			Object.Destroy(this.conversationText.transform.parent.gameObject);
			return;
		}
		this.SetText(this.greetings);
	}

	// Token: 0x060005CF RID: 1487 RVA: 0x00039B24 File Offset: 0x00037D24
	private void Update()
	{
		this.time += Time.deltaTime;
		if (this.time > 0.0125f)
		{
			if (this.conversationText)
			{
				TextMeshProUGUI textMeshProUGUI = this.conversationText;
				int maxVisibleCharacters = textMeshProUGUI.maxVisibleCharacters;
				textMeshProUGUI.maxVisibleCharacters = maxVisibleCharacters + 1;
			}
			this.time = 0f;
		}
	}

	// Token: 0x060005D0 RID: 1488 RVA: 0x00039B7D File Offset: 0x00037D7D
	public void StartUp()
	{
		this.isLeaving = false;
		this.isOpen = false;
	}

	// Token: 0x060005D1 RID: 1489 RVA: 0x00039B8D File Offset: 0x00037D8D
	public void OnMouseUpAsButton()
	{
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		this.OpenEvent();
	}

	// Token: 0x060005D2 RID: 1490 RVA: 0x00039BA2 File Offset: 0x00037DA2
	public override void OnPressStart(string keyName, bool overrideKeyName = false)
	{
		if (keyName != "confirm" && !overrideKeyName)
		{
			return;
		}
		this.OpenEvent();
	}

	// Token: 0x060005D3 RID: 1491 RVA: 0x00039BBC File Offset: 0x00037DBC
	public void AllowReOpen()
	{
		this.isOpen = false;
		InputHandler componentInParent = base.GetComponentInParent<InputHandler>();
		if (componentInParent)
		{
			componentInParent.enabled = true;
		}
	}

	// Token: 0x060005D4 RID: 1492 RVA: 0x00039BE8 File Offset: 0x00037DE8
	private void OpenEvent()
	{
		if (this.isOpen || this.isLeaving || (this.dungeonEvent && this.dungeonEvent.GetEventProperty(DungeonEvent.EventProperty.Type.finished) != null) || this.gameManager.viewingEvent)
		{
			return;
		}
		InputHandler componentInParent = base.GetComponentInParent<InputHandler>();
		if (componentInParent)
		{
			componentInParent.enabled = false;
		}
		DigitalInputSelectOnButton componentInParent2 = base.GetComponentInParent<DigitalInputSelectOnButton>();
		if (componentInParent2)
		{
			componentInParent2.RemoveSymbol();
		}
		if (componentInParent)
		{
			componentInParent.LaunchCustomEvent();
		}
		this.isOpen = true;
		this.gameManager.ShowInventory();
		if (this.eventPrefab)
		{
			this.gameManager.viewingEvent = true;
			GameObject gameObject = Object.Instantiate<GameObject>(this.eventPrefab, Vector3.zero, Quaternion.identity, this.gameManager.eventsParent);
			RandomEventMaster component = gameObject.GetComponent<RandomEventMaster>();
			if (component)
			{
				component.npc = this;
				if (this.dungeonEvent)
				{
					component.dungeonEvent = this.dungeonEvent;
				}
				this.existingEvent = gameObject;
			}
		}
	}

	// Token: 0x060005D5 RID: 1493 RVA: 0x00039CE9 File Offset: 0x00037EE9
	public void GoodBye()
	{
		this.isLeaving = true;
		this.SetText(this.farewells);
	}

	// Token: 0x060005D6 RID: 1494 RVA: 0x00039D00 File Offset: 0x00037F00
	public void SetText(List<string> texts)
	{
		if (!this.conversationText)
		{
			return;
		}
		if (texts.Count == 0)
		{
			return;
		}
		string text = texts[Random.Range(0, texts.Count)];
		LangaugeManager.main.SetFont(this.conversationText.transform);
		this.conversationText.text = text;
		this.conversationText.maxVisibleCharacters = 0;
	}

	// Token: 0x04000490 RID: 1168
	private float time;

	// Token: 0x04000491 RID: 1169
	public bool isOpen;

	// Token: 0x04000492 RID: 1170
	[SerializeField]
	public EventManager.EventType eventType;

	// Token: 0x04000493 RID: 1171
	[SerializeField]
	public List<Character.CharacterName> validForCharacters = new List<Character.CharacterName>();

	// Token: 0x04000494 RID: 1172
	[SerializeField]
	public List<RunType> invalidForRunTypes = new List<RunType>();

	// Token: 0x04000495 RID: 1173
	[SerializeField]
	private TextMeshProUGUI conversationText;

	// Token: 0x04000496 RID: 1174
	[SerializeField]
	private Transform positionForText;

	// Token: 0x04000497 RID: 1175
	private GameManager gameManager;

	// Token: 0x04000498 RID: 1176
	[SerializeField]
	private List<string> greetings;

	// Token: 0x04000499 RID: 1177
	[SerializeField]
	private List<string> farewells;

	// Token: 0x0400049A RID: 1178
	[SerializeField]
	private GameObject eventPrefab;

	// Token: 0x0400049B RID: 1179
	public GameObject existingEvent;

	// Token: 0x0400049C RID: 1180
	public DungeonEvent dungeonEvent;

	// Token: 0x0400049D RID: 1181
	private bool isLeaving;
}
