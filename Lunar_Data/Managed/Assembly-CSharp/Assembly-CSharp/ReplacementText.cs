using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Token: 0x0200007F RID: 127
public class ReplacementText : MonoBehaviour
{
	// Token: 0x06000375 RID: 885 RVA: 0x000113E8 File Offset: 0x0000F5E8
	private void OnEnable()
	{
		LanguageManager.OnReplaceText += this.ReplaceText;
		ControllerSpriteManager.instance.spriteSetChanged += this.ReplaceText;
		this.key = this.key.ToLower().Trim();
		this.ReplaceText();
	}

	// Token: 0x06000376 RID: 886 RVA: 0x00011438 File Offset: 0x0000F638
	private void OnDisable()
	{
		if (!LanguageManager.main)
		{
			return;
		}
		if (!ControllerSpriteManager.instance)
		{
			return;
		}
		LanguageManager.OnReplaceText -= this.ReplaceText;
		ControllerSpriteManager.instance.spriteSetChanged -= this.ReplaceText;
	}

	// Token: 0x06000377 RID: 887 RVA: 0x00011486 File Offset: 0x0000F686
	public void SetKey(string key)
	{
		this.key = key;
		this.ReplaceText();
	}

	// Token: 0x06000378 RID: 888 RVA: 0x00011495 File Offset: 0x0000F695
	public void ResetAdditionalText()
	{
		this.additionalTexts.Clear();
	}

	// Token: 0x06000379 RID: 889 RVA: 0x000114A4 File Offset: 0x0000F6A4
	public void AddAdditionalText(string text, ReplacementText.AdditionalText.position pos)
	{
		ReplacementText.AdditionalText additionalText = new ReplacementText.AdditionalText();
		additionalText.text = text;
		additionalText.pos = pos;
		this.additionalTexts.Add(additionalText);
		this.ReplaceText();
	}

	// Token: 0x0600037A RID: 890 RVA: 0x000114D8 File Offset: 0x0000F6D8
	public void ReplaceText()
	{
		if (this.key == "")
		{
			return;
		}
		TextMeshProUGUI componentInChildren = base.GetComponentInChildren<TextMeshProUGUI>();
		if (!componentInChildren)
		{
			Debug.LogWarning("No TextMeshProUGUI component found for ReplacementText component on " + base.gameObject.name);
			return;
		}
		string text = LanguageManager.main.GetText(this.key);
		text = this.ReplaceVariableWithSymbol(text);
		componentInChildren.text = text;
		foreach (ReplacementText.AdditionalText additionalText in this.additionalTexts)
		{
			if (additionalText.pos == ReplacementText.AdditionalText.position.Before)
			{
				componentInChildren.text = additionalText.text + componentInChildren.text;
			}
			else if (additionalText.pos == ReplacementText.AdditionalText.position.After)
			{
				componentInChildren.text += additionalText.text;
			}
			else if (additionalText.pos == ReplacementText.AdditionalText.position.ReplaceVariable)
			{
				componentInChildren.text = componentInChildren.text.Replace("{v}", additionalText.text);
			}
		}
		componentInChildren.font = LanguageManager.main.chosenFont;
		componentInChildren.fontStyle = FontStyles.Normal;
	}

	// Token: 0x0600037B RID: 891 RVA: 0x00011600 File Offset: 0x0000F800
	private string ReplaceVariableWithSymbol(string x)
	{
		if (ControllerSpriteManager.instance.isUsingController)
		{
			switch (ControllerSpriteManager.instance.currentSpriteSet)
			{
			case ControllerSpriteManager.ControllerSpriteType.Xbox:
				x = x.Replace("{a}", " <sprite name=\"buttons_0\">");
				x = x.Replace("{b}", " <sprite name=\"buttons_1\">");
				break;
			case ControllerSpriteManager.ControllerSpriteType.Switch:
				x = x.Replace("{a}", " <sprite name=\"buttons_4\">");
				x = x.Replace("{b}", " <sprite name=\"buttons_5\">");
				break;
			case ControllerSpriteManager.ControllerSpriteType.PS:
				x = x.Replace("{a}", " <sprite name=\"buttons_8\">");
				x = x.Replace("{b}", " <sprite name=\"buttons_9\">");
				break;
			}
		}
		else
		{
			x = x.Replace("{a}", " <sprite name=\"buttons_13\">");
			x = x.Replace("{b}", " <sprite name=\"buttons_12\">");
		}
		return x;
	}

	// Token: 0x040002A4 RID: 676
	[SerializeField]
	public string key;

	// Token: 0x040002A5 RID: 677
	public List<ReplacementText.AdditionalText> additionalTexts = new List<ReplacementText.AdditionalText>();

	// Token: 0x02000106 RID: 262
	[Serializable]
	public class AdditionalText
	{
		// Token: 0x040004CD RID: 1229
		public string text;

		// Token: 0x040004CE RID: 1230
		public ReplacementText.AdditionalText.position pos;

		// Token: 0x0200012C RID: 300
		public enum position
		{
			// Token: 0x0400055F RID: 1375
			Before,
			// Token: 0x04000560 RID: 1376
			After,
			// Token: 0x04000561 RID: 1377
			ReplaceVariable
		}
	}
}
