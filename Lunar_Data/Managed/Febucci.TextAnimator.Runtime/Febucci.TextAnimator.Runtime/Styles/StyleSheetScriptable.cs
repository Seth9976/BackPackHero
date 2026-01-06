using System;
using System.Collections.Generic;
using UnityEngine;

namespace Febucci.UI.Styles
{
	// Token: 0x0200000A RID: 10
	[CreateAssetMenu(fileName = "TextAnimator StyleSheet", menuName = "Text Animator/StyleSheet", order = 100)]
	[Serializable]
	public class StyleSheetScriptable : ScriptableObject
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000024CA File Offset: 0x000006CA
		// (set) Token: 0x0600001C RID: 28 RVA: 0x000024D2 File Offset: 0x000006D2
		public Style[] Styles
		{
			get
			{
				return this.styles;
			}
			set
			{
				this.styles = value;
				this.built = false;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024E4 File Offset: 0x000006E4
		public void BuildOnce()
		{
			if (this.built)
			{
				return;
			}
			this.built = true;
			if (this.dictionary != null)
			{
				this.dictionary.Clear();
			}
			else
			{
				this.dictionary = new Dictionary<string, Style>();
			}
			if (this.styles == null)
			{
				return;
			}
			foreach (Style style in this.styles)
			{
				if (!string.IsNullOrEmpty(style.styleTag))
				{
					if (this.dictionary.ContainsKey(style.styleTag))
					{
						Debug.LogError("[TextAnimator] StyleSheetScriptable: duplicated style tag '" + style.styleTag, this);
					}
					else
					{
						this.dictionary.Add(style.styleTag, style);
					}
				}
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002591 File Offset: 0x00000791
		public void ForceBuildRefresh()
		{
			this.built = false;
			this.BuildOnce();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000025A0 File Offset: 0x000007A0
		public bool TryGetStyle(string tag, out Style result)
		{
			this.BuildOnce();
			return this.dictionary.TryGetValue(tag, out result);
		}

		// Token: 0x04000021 RID: 33
		[SerializeField]
		private Style[] styles = Array.Empty<Style>();

		// Token: 0x04000022 RID: 34
		private bool built;

		// Token: 0x04000023 RID: 35
		private Dictionary<string, Style> dictionary;
	}
}
