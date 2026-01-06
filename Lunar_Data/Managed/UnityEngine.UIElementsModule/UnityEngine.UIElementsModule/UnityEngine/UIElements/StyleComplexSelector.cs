using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.UIElements
{
	// Token: 0x0200029C RID: 668
	[Serializable]
	internal class StyleComplexSelector
	{
		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x060016B9 RID: 5817 RVA: 0x0005D2A8 File Offset: 0x0005B4A8
		// (set) Token: 0x060016BA RID: 5818 RVA: 0x0005D2C0 File Offset: 0x0005B4C0
		public int specificity
		{
			get
			{
				return this.m_Specificity;
			}
			internal set
			{
				this.m_Specificity = value;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x060016BB RID: 5819 RVA: 0x0005D2CA File Offset: 0x0005B4CA
		// (set) Token: 0x060016BC RID: 5820 RVA: 0x0005D2D2 File Offset: 0x0005B4D2
		public StyleRule rule { get; internal set; }

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x060016BD RID: 5821 RVA: 0x0005D2DC File Offset: 0x0005B4DC
		public bool isSimple
		{
			get
			{
				return this.selectors.Length == 1;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x060016BE RID: 5822 RVA: 0x0005D2FC File Offset: 0x0005B4FC
		// (set) Token: 0x060016BF RID: 5823 RVA: 0x0005D314 File Offset: 0x0005B514
		public StyleSelector[] selectors
		{
			get
			{
				return this.m_Selectors;
			}
			internal set
			{
				this.m_Selectors = value;
			}
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x0005D320 File Offset: 0x0005B520
		internal void CachePseudoStateMasks()
		{
			bool flag = StyleComplexSelector.s_PseudoStates == null;
			if (flag)
			{
				StyleComplexSelector.s_PseudoStates = new Dictionary<string, StyleComplexSelector.PseudoStateData>();
				StyleComplexSelector.s_PseudoStates["active"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Active, false);
				StyleComplexSelector.s_PseudoStates["hover"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Hover, false);
				StyleComplexSelector.s_PseudoStates["checked"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Checked, false);
				StyleComplexSelector.s_PseudoStates["selected"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Checked, false);
				StyleComplexSelector.s_PseudoStates["disabled"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Disabled, false);
				StyleComplexSelector.s_PseudoStates["focus"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Focus, false);
				StyleComplexSelector.s_PseudoStates["root"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Root, false);
				StyleComplexSelector.s_PseudoStates["inactive"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Active, true);
				StyleComplexSelector.s_PseudoStates["enabled"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Disabled, true);
			}
			int i = 0;
			int num = this.selectors.Length;
			while (i < num)
			{
				StyleSelector styleSelector = this.selectors[i];
				StyleSelectorPart[] parts = styleSelector.parts;
				PseudoStates pseudoStates = (PseudoStates)0;
				PseudoStates pseudoStates2 = (PseudoStates)0;
				for (int j = 0; j < styleSelector.parts.Length; j++)
				{
					bool flag2 = styleSelector.parts[j].type == StyleSelectorType.PseudoClass;
					if (flag2)
					{
						StyleComplexSelector.PseudoStateData pseudoStateData;
						bool flag3 = StyleComplexSelector.s_PseudoStates.TryGetValue(parts[j].value, ref pseudoStateData);
						if (flag3)
						{
							bool flag4 = !pseudoStateData.negate;
							if (flag4)
							{
								pseudoStates |= pseudoStateData.state;
							}
							else
							{
								pseudoStates2 |= pseudoStateData.state;
							}
						}
						else
						{
							Debug.LogWarningFormat("Unknown pseudo class \"{0}\"", new object[] { parts[j].value });
						}
					}
				}
				styleSelector.pseudoStateMask = (int)pseudoStates;
				styleSelector.negatedPseudoStateMask = (int)pseudoStates2;
				i++;
			}
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x0005D520 File Offset: 0x0005B720
		public override string ToString()
		{
			return string.Format("[{0}]", string.Join(", ", Enumerable.ToArray<string>(Enumerable.Select<StyleSelector, string>(this.m_Selectors, (StyleSelector x) => x.ToString()))));
		}

		// Token: 0x0400097D RID: 2429
		[SerializeField]
		private int m_Specificity;

		// Token: 0x0400097F RID: 2431
		[SerializeField]
		private StyleSelector[] m_Selectors;

		// Token: 0x04000980 RID: 2432
		[SerializeField]
		internal int ruleIndex;

		// Token: 0x04000981 RID: 2433
		[NonSerialized]
		internal StyleComplexSelector nextInTable;

		// Token: 0x04000982 RID: 2434
		[NonSerialized]
		internal int orderInStyleSheet;

		// Token: 0x04000983 RID: 2435
		private static Dictionary<string, StyleComplexSelector.PseudoStateData> s_PseudoStates;

		// Token: 0x0200029D RID: 669
		private struct PseudoStateData
		{
			// Token: 0x060016C3 RID: 5827 RVA: 0x0005D575 File Offset: 0x0005B775
			public PseudoStateData(PseudoStates state, bool negate)
			{
				this.state = state;
				this.negate = negate;
			}

			// Token: 0x04000984 RID: 2436
			public readonly PseudoStates state;

			// Token: 0x04000985 RID: 2437
			public readonly bool negate;
		}
	}
}
