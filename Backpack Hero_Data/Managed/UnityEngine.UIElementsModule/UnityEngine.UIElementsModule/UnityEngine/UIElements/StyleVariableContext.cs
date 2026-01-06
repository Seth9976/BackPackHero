using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020002AF RID: 687
	internal class StyleVariableContext
	{
		// Token: 0x06001719 RID: 5913 RVA: 0x0005E440 File Offset: 0x0005C640
		public void Add(StyleVariable sv)
		{
			int hashCode = sv.GetHashCode();
			int num = this.m_SortedHash.BinarySearch(hashCode);
			bool flag = num >= 0;
			if (!flag)
			{
				this.m_SortedHash.Insert(~num, hashCode);
				this.m_Variables.Add(sv);
				this.m_VariableHash = ((this.m_Variables.Count == 0) ? sv.GetHashCode() : ((this.m_VariableHash * 397) ^ sv.GetHashCode()));
			}
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x0005E4D0 File Offset: 0x0005C6D0
		public void AddInitialRange(StyleVariableContext other)
		{
			bool flag = other.m_Variables.Count > 0;
			if (flag)
			{
				Debug.Assert(this.m_Variables.Count == 0);
				this.m_VariableHash = other.m_VariableHash;
				this.m_Variables.AddRange(other.m_Variables);
				this.m_SortedHash.AddRange(other.m_SortedHash);
			}
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x0005E538 File Offset: 0x0005C738
		public void Clear()
		{
			bool flag = this.m_Variables.Count > 0;
			if (flag)
			{
				this.m_Variables.Clear();
				this.m_VariableHash = 0;
				this.m_SortedHash.Clear();
			}
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x0005E579 File Offset: 0x0005C779
		public StyleVariableContext()
		{
			this.m_Variables = new List<StyleVariable>();
			this.m_VariableHash = 0;
			this.m_SortedHash = new List<int>();
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x0005E5A0 File Offset: 0x0005C7A0
		public StyleVariableContext(StyleVariableContext other)
		{
			this.m_Variables = new List<StyleVariable>(other.m_Variables);
			this.m_VariableHash = other.m_VariableHash;
			this.m_SortedHash = new List<int>(other.m_SortedHash);
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x0005E5D8 File Offset: 0x0005C7D8
		public bool TryFindVariable(string name, out StyleVariable v)
		{
			for (int i = this.m_Variables.Count - 1; i >= 0; i--)
			{
				bool flag = this.m_Variables[i].name == name;
				if (flag)
				{
					v = this.m_Variables[i];
					return true;
				}
			}
			v = default(StyleVariable);
			return false;
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x0005E648 File Offset: 0x0005C848
		public int GetVariableHash()
		{
			return this.m_VariableHash;
		}

		// Token: 0x040009DE RID: 2526
		public static readonly StyleVariableContext none = new StyleVariableContext();

		// Token: 0x040009DF RID: 2527
		private int m_VariableHash;

		// Token: 0x040009E0 RID: 2528
		private List<StyleVariable> m_Variables;

		// Token: 0x040009E1 RID: 2529
		private List<int> m_SortedHash;
	}
}
