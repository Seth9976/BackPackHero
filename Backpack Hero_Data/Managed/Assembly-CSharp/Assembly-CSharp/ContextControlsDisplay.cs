using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000019 RID: 25
public class ContextControlsDisplay : MonoBehaviour
{
	// Token: 0x060000A0 RID: 160 RVA: 0x00005EE6 File Offset: 0x000040E6
	private void onEnable()
	{
		LangaugeManager.OnLanguageChanged += this.OnLanguageChanged;
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x00005EF9 File Offset: 0x000040F9
	private void onDisable()
	{
		LangaugeManager.OnLanguageChanged -= this.OnLanguageChanged;
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x00005F0C File Offset: 0x0000410C
	private void OnLanguageChanged()
	{
		LangaugeManager.main.SetFont(base.transform);
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x00005F1E File Offset: 0x0000411E
	private void Awake()
	{
		ContextControlsDisplay.main = this;
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x00005F26 File Offset: 0x00004126
	private void OnDestory()
	{
		ContextControlsDisplay.main = null;
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x00005F2E File Offset: 0x0000412E
	private void Start()
	{
		this.canvasGroup = base.GetComponent<CanvasGroup>();
		this.ClearAllControls();
		this.OnLanguageChanged();
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x00005F48 File Offset: 0x00004148
	private Sprite GetSprite(string name)
	{
		if (name == "a")
		{
			return DigitalCursor.main.GetSpriteForKey("confirm");
		}
		if (name == "x")
		{
			return DigitalCursor.main.GetSpriteForKey("ContextMenu");
		}
		if (name == "b")
		{
			return DigitalCursor.main.GetSpriteForKey("cancel");
		}
		if (name == "y")
		{
			return DigitalCursor.main.GetSpriteForKey("contextualAction");
		}
		if (name == "lt")
		{
			return DigitalCursor.main.GetSpriteForKey("rotateLeft");
		}
		if (!(name == "rt"))
		{
			return null;
		}
		return DigitalCursor.main.GetSpriteForKey("rotateRight");
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x00006008 File Offset: 0x00004208
	public void ShowControl(List<string> buttonNames, string text)
	{
		foreach (string text2 in buttonNames)
		{
			this.ShowControl(text2, text);
		}
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x00006058 File Offset: 0x00004258
	public void ShowControl(string buttonName, string text)
	{
		this.ClearControl(buttonName, text);
		Sprite sprite = this.GetSprite(buttonName);
		if (sprite == null)
		{
			Debug.LogError("No sprite for " + buttonName);
			return;
		}
		foreach (ContextControlsDisplay.SavedControl savedControl in this.savedControls)
		{
			if (savedControl.text == text)
			{
				if (savedControl.buttonNames.Contains(buttonName))
				{
					return;
				}
				Transform contextControl = savedControl.contextControl;
				this.AddControlImage(sprite, contextControl);
				savedControl.buttonNames.Add(buttonName);
				return;
			}
		}
		GameObject gameObject = Object.Instantiate<GameObject>(this.contextControlPrefab, base.transform);
		LangaugeManager.main.SetFont(gameObject.transform);
		gameObject.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey(text);
		this.AddControlImage(sprite, gameObject.transform);
		ContextControlsDisplay.SavedControl savedControl2 = new ContextControlsDisplay.SavedControl();
		savedControl2.buttonNames = new List<string>();
		savedControl2.buttonNames.Add(buttonName);
		savedControl2.text = text;
		savedControl2.contextControl = gameObject.transform;
		this.savedControls.Add(savedControl2);
		this.OrderControls();
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x000061A0 File Offset: 0x000043A0
	public int GetSortKey(string key)
	{
		if (key == "a")
		{
			return 0;
		}
		if (key == "x")
		{
			return 1;
		}
		if (key == "b")
		{
			return 2;
		}
		if (!(key == "lt") && !(key == "rt"))
		{
			return 4;
		}
		return 3;
	}

	// Token: 0x060000AA RID: 170 RVA: 0x000061FC File Offset: 0x000043FC
	private void OrderControls()
	{
		new List<ContextControlsDisplay.SavedControl>();
		this.savedControls.Sort((ContextControlsDisplay.SavedControl x, ContextControlsDisplay.SavedControl y) => this.GetSortKey(x.buttonNames[0]).CompareTo(this.GetSortKey(y.buttonNames[0])));
		this.savedControls.Reverse();
		for (int i = 0; i < this.savedControls.Count; i++)
		{
			this.savedControls[i].contextControl.SetSiblingIndex(i);
		}
	}

	// Token: 0x060000AB RID: 171 RVA: 0x0000625E File Offset: 0x0000445E
	public void AddControlImage(Sprite sprite, Transform parent)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.contextControlImagePrefab, parent);
		gameObject.GetComponent<Image>().sprite = sprite;
		gameObject.transform.SetSiblingIndex(parent.transform.childCount - 2);
	}

	// Token: 0x060000AC RID: 172 RVA: 0x00006290 File Offset: 0x00004490
	public void RemoveControlImage(Sprite sprite, Transform parent)
	{
		foreach (object obj in parent)
		{
			Transform transform = (Transform)obj;
			Image component = transform.GetComponent<Image>();
			if (component && component.sprite == sprite)
			{
				Object.Destroy(transform.gameObject);
				break;
			}
		}
	}

	// Token: 0x060000AD RID: 173 RVA: 0x00006308 File Offset: 0x00004508
	public void ClearControl(string buttonName)
	{
		this.ClearControl(buttonName, "");
	}

	// Token: 0x060000AE RID: 174 RVA: 0x00006318 File Offset: 0x00004518
	public void ClearControl(string buttonName, string excludedKey)
	{
		foreach (ContextControlsDisplay.SavedControl savedControl in this.savedControls)
		{
			if (!(savedControl.text.ToLower().Trim() == excludedKey.ToLower().Trim()))
			{
				foreach (string text in savedControl.buttonNames)
				{
					if (text.ToLower().Trim() == buttonName.ToLower().Trim())
					{
						this.RemoveControlImage(this.GetSprite(buttonName), savedControl.contextControl);
						savedControl.buttonNames.Remove(text);
						if (savedControl.buttonNames.Count == 0)
						{
							Object.Destroy(savedControl.contextControl.gameObject);
							this.savedControls.Remove(savedControl);
						}
						return;
					}
				}
			}
		}
	}

	// Token: 0x060000AF RID: 175 RVA: 0x00006434 File Offset: 0x00004634
	public void ClearAllControls()
	{
		this.SetShow(true);
		this.savedControls.Clear();
		foreach (object obj in base.transform)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x000064A4 File Offset: 0x000046A4
	public void SetShow(bool show)
	{
		if (this.increaseTransparencyRoutine != null)
		{
			base.StopCoroutine(this.increaseTransparencyRoutine);
		}
		if (show)
		{
			this.canvasGroup.alpha = 1f;
			return;
		}
		this.canvasGroup.alpha = 0f;
	}

	// Token: 0x04000056 RID: 86
	public static ContextControlsDisplay main;

	// Token: 0x04000057 RID: 87
	[SerializeField]
	private GameObject contextControlPrefab;

	// Token: 0x04000058 RID: 88
	[SerializeField]
	private GameObject contextControlImagePrefab;

	// Token: 0x04000059 RID: 89
	private List<ContextControlsDisplay.SavedControl> savedControls = new List<ContextControlsDisplay.SavedControl>();

	// Token: 0x0400005A RID: 90
	private CanvasGroup canvasGroup;

	// Token: 0x0400005B RID: 91
	private Coroutine increaseTransparencyRoutine;

	// Token: 0x0200024A RID: 586
	public class SavedControl
	{
		// Token: 0x04000ECA RID: 3786
		public List<string> buttonNames;

		// Token: 0x04000ECB RID: 3787
		public string text;

		// Token: 0x04000ECC RID: 3788
		public Transform contextControl;
	}
}
