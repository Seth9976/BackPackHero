using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Token: 0x02000116 RID: 278
public class ReplacementText : MonoBehaviour
{
	// Token: 0x06000975 RID: 2421 RVA: 0x00060F80 File Offset: 0x0005F180
	private void Start()
	{
		if (this.setKeyFromStartingText)
		{
			this.key = base.GetComponentInChildren<TextMeshProUGUI>().text;
		}
		this.key = this.key.ToLower().Trim();
		this.ReplaceText();
		this.setup = true;
	}

	// Token: 0x06000976 RID: 2422 RVA: 0x00060FBE File Offset: 0x0005F1BE
	private void OnEnable()
	{
		if (this.setup)
		{
			this.ReplaceText();
		}
	}

	// Token: 0x06000977 RID: 2423 RVA: 0x00060FD0 File Offset: 0x0005F1D0
	public void ReplaceText()
	{
		TextMeshProUGUI componentInChildren = base.GetComponentInChildren<TextMeshProUGUI>();
		if (componentInChildren)
		{
			if (this.key != "")
			{
				string textByKey = LangaugeManager.main.GetTextByKey(this.key);
				componentInChildren.text = this.nonKeyPrefix + textByKey + this.nonKeySuffix;
			}
			componentInChildren.font = LangaugeManager.main.chosenFont;
			foreach (ReplacementText.FontReplacement fontReplacement in this.fontReplacements)
			{
				if (componentInChildren.font == fontReplacement.fontToReplace)
				{
					componentInChildren.font = fontReplacement.replacementFont;
				}
			}
			componentInChildren.fontStyle = FontStyles.Normal;
			if (this.textPreprocessor != null)
			{
				componentInChildren.text = this.textPreprocessor(componentInChildren.text);
				return;
			}
		}
		else
		{
			TextMeshPro componentInChildren2 = base.GetComponentInChildren<TextMeshPro>();
			if (componentInChildren2)
			{
				if (this.key != "")
				{
					string textByKey2 = LangaugeManager.main.GetTextByKey(this.key);
					componentInChildren2.text = this.nonKeyPrefix + textByKey2 + this.nonKeySuffix;
				}
				componentInChildren2.font = LangaugeManager.main.chosenFont;
				componentInChildren2.fontStyle = FontStyles.Normal;
			}
			if (this.textPreprocessor != null)
			{
				componentInChildren2.text = this.textPreprocessor(componentInChildren2.text);
			}
		}
	}

	// Token: 0x04000782 RID: 1922
	[SerializeField]
	private List<ReplacementText.FontReplacement> fontReplacements = new List<ReplacementText.FontReplacement>();

	// Token: 0x04000783 RID: 1923
	[SerializeField]
	public string nonKeyPrefix = "";

	// Token: 0x04000784 RID: 1924
	[SerializeField]
	public string key;

	// Token: 0x04000785 RID: 1925
	[SerializeField]
	public string nonKeySuffix = "";

	// Token: 0x04000786 RID: 1926
	[SerializeField]
	public bool setKeyFromStartingText;

	// Token: 0x04000787 RID: 1927
	[SerializeField]
	private TMP_FontAsset font;

	// Token: 0x04000788 RID: 1928
	public ReplacementText.TextPreprocessDelegate textPreprocessor;

	// Token: 0x04000789 RID: 1929
	private bool setup;

	// Token: 0x02000388 RID: 904
	[Serializable]
	private class FontReplacement
	{
		// Token: 0x04001546 RID: 5446
		public TMP_FontAsset fontToReplace;

		// Token: 0x04001547 RID: 5447
		public TMP_FontAsset replacementFont;
	}

	// Token: 0x02000389 RID: 905
	// (Invoke) Token: 0x06001731 RID: 5937
	public delegate string TextPreprocessDelegate(string text);
}
