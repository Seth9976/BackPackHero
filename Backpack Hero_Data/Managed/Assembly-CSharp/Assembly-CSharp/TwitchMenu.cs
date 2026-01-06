using System;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001A8 RID: 424
public class TwitchMenu : MonoBehaviour
{
	// Token: 0x060010C1 RID: 4289 RVA: 0x0009F5C8 File Offset: 0x0009D7C8
	private void Start()
	{
		this.eventBoxAnimator = base.GetComponentInChildren<Animator>();
		TwitchManager.Instance.LoadTwitchData();
		this.CheckForData();
		List<TMP_Dropdown.OptionData> list = new List<TMP_Dropdown.OptionData>();
		foreach (TwitchPollManager.PollSetting pollSetting in TwitchManager.Instance.pollManager.settingsPresets)
		{
			list.Add(new TMP_Dropdown.OptionData
			{
				text = LangaugeManager.main.GetTextByKey(pollSetting.name)
			});
		}
		this.presetDropdown.ClearOptions();
		this.presetDropdown.AddOptions(list);
		this.SettingsToUI();
	}

	// Token: 0x060010C2 RID: 4290 RVA: 0x0009F680 File Offset: 0x0009D880
	private void Update()
	{
		if (!this.eventBoxAnimator.gameObject.activeInHierarchy)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		if (this.tryingToAuth && TwitchManager.Instance.status == TwitchManager.Status.LoggedIn && !Singleton.Instance.twitchIntegrationEnabled)
		{
			this.tryingToAuth = false;
			this.ToggleTwitchIntegration();
		}
		string text;
		Color color;
		switch (TwitchManager.Instance.status)
		{
		case TwitchManager.Status.NoAuth:
			text = LangaugeManager.main.GetTextByKey("twitchSNoAuth");
			color = this.neutralColor;
			this.tryingToAuth = false;
			break;
		case TwitchManager.Status.AuthError:
			text = LangaugeManager.main.GetTextByKey("twitchSAuthFail").Replace("/y", TwitchManager.Instance.errorCode.ToString());
			color = this.errorColor;
			this.tryingToAuth = false;
			break;
		case TwitchManager.Status.NoConnection:
			text = LangaugeManager.main.GetTextByKey("twitchSNotConnected");
			color = this.neutralColor;
			break;
		case TwitchManager.Status.ConnectionError:
			text = LangaugeManager.main.GetTextByKey("twitchSConnectFail").Replace("/y", TwitchManager.Instance.errorCode.ToString());
			color = this.errorColor;
			break;
		case TwitchManager.Status.Authenticating:
			text = LangaugeManager.main.GetTextByKey("twitchSAuthenticating");
			color = this.busyColor;
			break;
		case TwitchManager.Status.LoggedIn:
			text = LangaugeManager.main.GetTextByKey("twitchSLoggedIn");
			color = this.neutralColor;
			break;
		case TwitchManager.Status.Connecting:
			text = LangaugeManager.main.GetTextByKey("twitchSConnecting");
			color = this.busyColor;
			break;
		case TwitchManager.Status.Connected:
			text = LangaugeManager.main.GetTextByKey("twitchSConnectedTo").Replace("/y", TwitchManager.Instance.displayName);
			color = this.successColor;
			break;
		default:
			text = "Unhandled Connection State (Error 99)";
			color = this.errorColor;
			break;
		}
		text = LangaugeManager.main.GetTextByKey("twitchStatus").Replace("/x", string.Concat(new string[]
		{
			"<color=#",
			ColorUtility.ToHtmlStringRGBA(color),
			">",
			text,
			"</color>"
		}));
		this.statusTextObj.GetComponent<TMP_Text>().text = text;
	}

	// Token: 0x060010C3 RID: 4291 RVA: 0x0009F8A4 File Offset: 0x0009DAA4
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

	// Token: 0x060010C4 RID: 4292 RVA: 0x0009F8FD File Offset: 0x0009DAFD
	public void CheckForData()
	{
		if (TwitchManager.Instance.hasValidToken())
		{
			this.clearDataButtonGroup.interactable = true;
			this.toggleGroup.interactable = true;
			return;
		}
		this.clearDataButtonGroup.interactable = false;
		this.toggleGroup.interactable = false;
	}

	// Token: 0x060010C5 RID: 4293 RVA: 0x0009F93C File Offset: 0x0009DB3C
	public void AuthenticateTwitch()
	{
		if (TwitchManager.Instance.TwitchDataExists() && TwitchManager.Instance.hasValidToken())
		{
			string textByKey = LangaugeManager.main.GetTextByKey("twitchAE");
			this.PopUp(textByKey, DigitalCursor.main.transform.position, 0.3f);
			return;
		}
		TwitchManager.Instance.AuthTwitch();
		string textByKey2 = LangaugeManager.main.GetTextByKey("twitchAU");
		this.PopUp(textByKey2, DigitalCursor.main.transform.position, 0.3f);
		this.tryingToAuth = true;
	}

	// Token: 0x060010C6 RID: 4294 RVA: 0x0009F9D3 File Offset: 0x0009DBD3
	public void EraseTwitchData()
	{
		TwitchManager.Instance.token = "";
		TwitchManager.Instance.EraseTwitchData();
		this.twitchToggle.isOn = false;
		this.CheckForData();
		this.SettingsToUI();
	}

	// Token: 0x060010C7 RID: 4295 RVA: 0x0009FA08 File Offset: 0x0009DC08
	public void ToggleTwitchIntegration()
	{
		if (Singleton.Instance.twitchIntegrationEnabled)
		{
			string textByKey = LangaugeManager.main.GetTextByKey("twitch4b");
			this.PopUp(textByKey, DigitalCursor.main.transform.position, 1f);
			this.twitchToggle.isOn = false;
			Singleton.Instance.twitchIntegrationEnabled = false;
			new Thread(delegate
			{
				Thread.CurrentThread.IsBackground = true;
				TwitchManager.Instance.Disconnect();
			}).Start();
			this.SettingsToUI();
			TwitchManager.Instance.SaveTwitchData();
			return;
		}
		if (TwitchManager.Instance.hasValidToken())
		{
			Singleton.Instance.twitchIntegrationEnabled = true;
			TwitchManager.Instance.SetupAndValidateAPIandUser();
			string textByKey2 = LangaugeManager.main.GetTextByKey("twitch4a");
			this.PopUp(textByKey2, DigitalCursor.main.transform.position, 1f);
			this.twitchToggle.isOn = true;
			this.SettingsToUI();
			TwitchManager.Instance.SaveTwitchData();
			return;
		}
		string textByKey3 = LangaugeManager.main.GetTextByKey("twitchEX");
		this.PopUp(textByKey3, DigitalCursor.main.transform.position, 0.3f);
		this.twitchToggle.isOn = false;
		Singleton.Instance.twitchIntegrationEnabled = false;
		this.SettingsToUI();
		TwitchManager.Instance.SaveTwitchData();
	}

	// Token: 0x060010C8 RID: 4296 RVA: 0x0009FB6B File Offset: 0x0009DD6B
	public void PresetDropDownChanged(TMP_Dropdown sender)
	{
		this.LoadPreset(sender.value);
	}

	// Token: 0x060010C9 RID: 4297 RVA: 0x0009FB7C File Offset: 0x0009DD7C
	public void LengthDropdownChanged(TMP_Dropdown sender)
	{
		TwitchManager.Instance.pollManager.setting.pollTimeout = (float)int.Parse(sender.options[sender.value].text);
		this.ChangeToCustom();
		this.SettingsToUI();
		TwitchManager.Instance.SaveTwitchData();
	}

	// Token: 0x060010CA RID: 4298 RVA: 0x0009FBD0 File Offset: 0x0009DDD0
	public void CountDropdownChanged(TMP_Dropdown sender)
	{
		TwitchManager.Instance.pollManager.setting.pollsPerCombat = int.Parse(sender.options[sender.value].text);
		this.ChangeToCustom();
		this.SettingsToUI();
		TwitchManager.Instance.SaveTwitchData();
	}

	// Token: 0x060010CB RID: 4299 RVA: 0x0009FC22 File Offset: 0x0009DE22
	public void LoadPreset(int index)
	{
		TwitchManager.Instance.pollManager.setting = TwitchPollManager.PollSetting.DeepCopy<TwitchPollManager.PollSetting>(TwitchManager.Instance.pollManager.settingsPresets[index]);
		this.SettingsToUI();
		TwitchManager.Instance.SaveTwitchData();
	}

	// Token: 0x060010CC RID: 4300 RVA: 0x0009FC60 File Offset: 0x0009DE60
	public void TwitchTogglePollSetting(GameObject sender)
	{
		if (sender != null)
		{
			TwitchMenu.ToggleConnection toggleConnection = this.toggleConnections.Find((TwitchMenu.ToggleConnection x) => x.toggle == sender);
			ref List<TwitchPollManager.ActionType> ptr = ref TwitchManager.Instance.pollManager.setting.battleActionsPositive;
			switch (toggleConnection.category)
			{
			case TwitchMenu.ToggleConnection.SettingCategory.BattlePositive:
				ptr = ref TwitchManager.Instance.pollManager.setting.battleActionsPositive;
				break;
			case TwitchMenu.ToggleConnection.SettingCategory.BattleNegative:
				ptr = ref TwitchManager.Instance.pollManager.setting.battleActionsNegative;
				break;
			case TwitchMenu.ToggleConnection.SettingCategory.Roaming:
				ptr = ref TwitchManager.Instance.pollManager.setting.roamingActions;
				break;
			case TwitchMenu.ToggleConnection.SettingCategory.Special:
				ptr = ref TwitchManager.Instance.pollManager.setting.specialActions;
				break;
			}
			int num = ptr.IndexOf(toggleConnection.action);
			if (num == -1)
			{
				ptr.Add(toggleConnection.action);
			}
			else
			{
				ptr.RemoveAt(num);
			}
		}
		this.ChangeToCustom();
		this.SettingsToUI();
		TwitchManager.Instance.SaveTwitchData();
	}

	// Token: 0x060010CD RID: 4301 RVA: 0x0009FD6C File Offset: 0x0009DF6C
	public void SettingsToUI()
	{
		this.twitchToggle.GetComponent<Toggle>().isOn = Singleton.Instance.twitchIntegrationEnabled;
		this.twitchPollToggle.GetComponent<Toggle>().isOn = Singleton.Instance.twitchEnablePolls;
		this.twitchDeathToggle.GetComponent<Toggle>().isOn = Singleton.Instance.twitchEnableDeathCounter;
		this.twitchEmoteToggle.GetComponent<Toggle>().isOn = Singleton.Instance.twitchEnableEmoteEffect;
		this.twitchDisableLootToggle.GetComponent<Toggle>().isOn = TwitchManager.Instance.pollManager.setting.disableLoot;
		this.presetDropdown.value = TwitchManager.Instance.pollManager.settingsPresets.FindIndex((TwitchPollManager.PollSetting x) => x.name == TwitchManager.Instance.pollManager.setting.name);
		foreach (TwitchMenu.ToggleConnection toggleConnection in this.toggleConnections)
		{
			ref List<TwitchPollManager.ActionType> ptr = ref TwitchManager.Instance.pollManager.setting.battleActionsPositive;
			switch (toggleConnection.category)
			{
			case TwitchMenu.ToggleConnection.SettingCategory.BattlePositive:
				ptr = ref TwitchManager.Instance.pollManager.setting.battleActionsPositive;
				break;
			case TwitchMenu.ToggleConnection.SettingCategory.BattleNegative:
				ptr = ref TwitchManager.Instance.pollManager.setting.battleActionsNegative;
				break;
			case TwitchMenu.ToggleConnection.SettingCategory.Roaming:
				ptr = ref TwitchManager.Instance.pollManager.setting.roamingActions;
				break;
			case TwitchMenu.ToggleConnection.SettingCategory.Special:
				ptr = ref TwitchManager.Instance.pollManager.setting.specialActions;
				break;
			}
			int num = ptr.IndexOf(toggleConnection.action);
			toggleConnection.toggle.GetComponent<Toggle>().isOn = num != -1;
		}
		string pollCountText = TwitchManager.Instance.pollManager.setting.pollsPerCombat.ToString();
		int num2 = 0;
		try
		{
			num2 = this.pollCountDropdown.options.FindIndex((TMP_Dropdown.OptionData x) => x.text == pollCountText);
		}
		catch
		{
			this.pollCountDropdown.AddOptions(new List<string> { pollCountText });
			num2 = this.pollCountDropdown.options.Count - 1;
		}
		this.pollCountDropdown.value = num2;
		string pollLengthText = TwitchManager.Instance.pollManager.setting.pollTimeout.ToString();
		int num3 = 0;
		try
		{
			num3 = this.pollLengthDropdown.options.FindIndex((TMP_Dropdown.OptionData x) => x.text == pollLengthText);
		}
		catch
		{
			this.pollLengthDropdown.AddOptions(new List<string> { pollLengthText });
			num3 = this.pollLengthDropdown.options.Count - 1;
		}
		this.pollLengthDropdown.value = num3;
	}

	// Token: 0x060010CE RID: 4302 RVA: 0x000A0064 File Offset: 0x0009E264
	public void ChangeToCustom()
	{
		TwitchManager.Instance.pollManager.setting.name = "twitchPresetCustom";
		TwitchManager.Instance.pollManager.settingsPresets[TwitchManager.Instance.pollManager.settingsPresets.FindIndex((TwitchPollManager.PollSetting x) => x.name == "twitchPresetCustom")] = TwitchManager.Instance.pollManager.setting;
	}

	// Token: 0x060010CF RID: 4303 RVA: 0x000A00E0 File Offset: 0x0009E2E0
	public void TogglePolls()
	{
		Singleton.Instance.twitchEnablePolls = !Singleton.Instance.twitchEnablePolls;
		this.twitchPollToggle.GetComponent<Toggle>().isOn = Singleton.Instance.twitchEnablePolls;
		TwitchManager.Instance.SaveTwitchData();
	}

	// Token: 0x060010D0 RID: 4304 RVA: 0x000A011D File Offset: 0x0009E31D
	public void ToggleDeath()
	{
		Singleton.Instance.twitchEnableDeathCounter = !Singleton.Instance.twitchEnableDeathCounter;
		this.twitchDeathToggle.GetComponent<Toggle>().isOn = Singleton.Instance.twitchEnableDeathCounter;
		TwitchManager.Instance.SaveTwitchData();
	}

	// Token: 0x060010D1 RID: 4305 RVA: 0x000A015A File Offset: 0x0009E35A
	public void ToggleEmotes()
	{
		Singleton.Instance.twitchEnableEmoteEffect = !Singleton.Instance.twitchEnableEmoteEffect;
		this.twitchEmoteToggle.GetComponent<Toggle>().isOn = Singleton.Instance.twitchEnableEmoteEffect;
		TwitchManager.Instance.SaveTwitchData();
	}

	// Token: 0x060010D2 RID: 4306 RVA: 0x000A0198 File Offset: 0x0009E398
	public void ToggleDisableLoot()
	{
		TwitchManager.Instance.pollManager.setting.disableLoot = !TwitchManager.Instance.pollManager.setting.disableLoot;
		this.twitchDisableLootToggle.GetComponent<Toggle>().isOn = TwitchManager.Instance.pollManager.setting.disableLoot;
		this.ChangeToCustom();
		this.SettingsToUI();
		TwitchManager.Instance.SaveTwitchData();
	}

	// Token: 0x060010D3 RID: 4307 RVA: 0x000A020C File Offset: 0x0009E40C
	public void EndEvent()
	{
		if (this.finished)
		{
			return;
		}
		SoundManager.main.PlaySFX("menuBlip");
		this.finished = true;
		this.eventBoxAnimator.Play("Out");
		Singleton.Instance.showingOptions = false;
		GameManager main = GameManager.main;
		if (main)
		{
			main.SetAllItemColliders(true);
			main.viewingEvent = false;
		}
		MenuManager menuManager = Object.FindObjectOfType<MenuManager>();
		if (menuManager)
		{
			menuManager.ShowButtons();
		}
	}

	// Token: 0x04000DAC RID: 3500
	[SerializeField]
	private Toggle twitchToggle;

	// Token: 0x04000DAD RID: 3501
	[SerializeField]
	private Toggle twitchPollToggle;

	// Token: 0x04000DAE RID: 3502
	[SerializeField]
	private Toggle twitchDeathToggle;

	// Token: 0x04000DAF RID: 3503
	[SerializeField]
	private Toggle twitchEmoteToggle;

	// Token: 0x04000DB0 RID: 3504
	[SerializeField]
	private Toggle twitchDisableLootToggle;

	// Token: 0x04000DB1 RID: 3505
	[SerializeField]
	private GameObject statusTextObj;

	// Token: 0x04000DB2 RID: 3506
	[SerializeField]
	private TMP_Dropdown presetDropdown;

	// Token: 0x04000DB3 RID: 3507
	[SerializeField]
	private TMP_Dropdown pollLengthDropdown;

	// Token: 0x04000DB4 RID: 3508
	[SerializeField]
	private TMP_Dropdown pollCountDropdown;

	// Token: 0x04000DB5 RID: 3509
	[SerializeField]
	private CanvasGroup clearDataButtonGroup;

	// Token: 0x04000DB6 RID: 3510
	[SerializeField]
	private CanvasGroup toggleGroup;

	// Token: 0x04000DB7 RID: 3511
	[SerializeField]
	private List<TwitchMenu.ToggleConnection> toggleConnections;

	// Token: 0x04000DB8 RID: 3512
	[SerializeField]
	private Color neutralColor;

	// Token: 0x04000DB9 RID: 3513
	[SerializeField]
	private Color errorColor;

	// Token: 0x04000DBA RID: 3514
	[SerializeField]
	private Color busyColor;

	// Token: 0x04000DBB RID: 3515
	[SerializeField]
	private Color successColor;

	// Token: 0x04000DBC RID: 3516
	private bool tryingToAuth;

	// Token: 0x04000DBD RID: 3517
	private Animator eventBoxAnimator;

	// Token: 0x04000DBE RID: 3518
	public bool finished;

	// Token: 0x0200047C RID: 1148
	[Serializable]
	public class ToggleConnection
	{
		// Token: 0x04001A63 RID: 6755
		public GameObject toggle;

		// Token: 0x04001A64 RID: 6756
		public TwitchPollManager.ActionType action;

		// Token: 0x04001A65 RID: 6757
		public TwitchMenu.ToggleConnection.SettingCategory category;

		// Token: 0x020004C2 RID: 1218
		public enum SettingCategory
		{
			// Token: 0x04001CBE RID: 7358
			BattlePositive,
			// Token: 0x04001CBF RID: 7359
			BattleNegative,
			// Token: 0x04001CC0 RID: 7360
			Roaming,
			// Token: 0x04001CC1 RID: 7361
			Special
		}
	}
}
