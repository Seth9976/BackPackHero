using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002A3 RID: 675
	[Serializable]
	internal struct StyleSelectorPart
	{
		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060016DA RID: 5850 RVA: 0x0005D6F4 File Offset: 0x0005B8F4
		// (set) Token: 0x060016DB RID: 5851 RVA: 0x0005D70C File Offset: 0x0005B90C
		public string value
		{
			get
			{
				return this.m_Value;
			}
			internal set
			{
				this.m_Value = value;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060016DC RID: 5852 RVA: 0x0005D718 File Offset: 0x0005B918
		// (set) Token: 0x060016DD RID: 5853 RVA: 0x0005D730 File Offset: 0x0005B930
		public StyleSelectorType type
		{
			get
			{
				return this.m_Type;
			}
			internal set
			{
				this.m_Type = value;
			}
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x0005D73C File Offset: 0x0005B93C
		public override string ToString()
		{
			return UnityString.Format("[StyleSelectorPart: value={0}, type={1}]", new object[] { this.value, this.type });
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x0005D778 File Offset: 0x0005B978
		public static StyleSelectorPart CreateClass(string className)
		{
			return new StyleSelectorPart
			{
				m_Type = StyleSelectorType.Class,
				m_Value = className
			};
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x0005D7A4 File Offset: 0x0005B9A4
		public static StyleSelectorPart CreatePseudoClass(string className)
		{
			return new StyleSelectorPart
			{
				m_Type = StyleSelectorType.PseudoClass,
				m_Value = className
			};
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x0005D7D0 File Offset: 0x0005B9D0
		public static StyleSelectorPart CreateId(string Id)
		{
			return new StyleSelectorPart
			{
				m_Type = StyleSelectorType.ID,
				m_Value = Id
			};
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x0005D7FC File Offset: 0x0005B9FC
		public static StyleSelectorPart CreateType(Type t)
		{
			return new StyleSelectorPart
			{
				m_Type = StyleSelectorType.Type,
				m_Value = t.Name
			};
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x0005D82C File Offset: 0x0005BA2C
		public static StyleSelectorPart CreateType(string typeName)
		{
			return new StyleSelectorPart
			{
				m_Type = StyleSelectorType.Type,
				m_Value = typeName
			};
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x0005D858 File Offset: 0x0005BA58
		public static StyleSelectorPart CreatePredicate(object predicate)
		{
			return new StyleSelectorPart
			{
				m_Type = StyleSelectorType.Predicate,
				tempData = predicate
			};
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x0005D884 File Offset: 0x0005BA84
		public static StyleSelectorPart CreateWildCard()
		{
			return new StyleSelectorPart
			{
				m_Type = StyleSelectorType.Wildcard
			};
		}

		// Token: 0x04000996 RID: 2454
		[SerializeField]
		private string m_Value;

		// Token: 0x04000997 RID: 2455
		[SerializeField]
		private StyleSelectorType m_Type;

		// Token: 0x04000998 RID: 2456
		internal object tempData;
	}
}
