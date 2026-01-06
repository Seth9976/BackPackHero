using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.UIElements
{
	// Token: 0x020002EC RID: 748
	public class UxmlEnumeration : UxmlTypeRestriction
	{
		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001893 RID: 6291 RVA: 0x00061D3C File Offset: 0x0005FF3C
		// (set) Token: 0x06001894 RID: 6292 RVA: 0x00061D54 File Offset: 0x0005FF54
		public IEnumerable<string> values
		{
			get
			{
				return this.m_Values;
			}
			set
			{
				this.m_Values = Enumerable.ToList<string>(value);
			}
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x00061D64 File Offset: 0x0005FF64
		public override bool Equals(UxmlTypeRestriction other)
		{
			UxmlEnumeration uxmlEnumeration = other as UxmlEnumeration;
			bool flag = uxmlEnumeration == null;
			return !flag && Enumerable.All<string>(this.values, new Func<string, bool>(uxmlEnumeration.values.Contains<string>)) && Enumerable.Count<string>(this.values) == Enumerable.Count<string>(uxmlEnumeration.values);
		}

		// Token: 0x04000A72 RID: 2674
		private List<string> m_Values = new List<string>();
	}
}
