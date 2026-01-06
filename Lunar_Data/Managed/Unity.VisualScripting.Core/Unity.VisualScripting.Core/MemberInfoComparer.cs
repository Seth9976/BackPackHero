using System;
using System.Collections.Generic;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x020000D3 RID: 211
	public class MemberInfoComparer : EqualityComparer<MemberInfo>
	{
		// Token: 0x060005C5 RID: 1477 RVA: 0x0000EC44 File Offset: 0x0000CE44
		public override bool Equals(MemberInfo x, MemberInfo y)
		{
			int? num = ((x != null) ? new int?(x.MetadataToken) : null);
			int? num2 = ((y != null) ? new int?(y.MetadataToken) : null);
			return (num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null));
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0000ECA6 File Offset: 0x0000CEA6
		public override int GetHashCode(MemberInfo obj)
		{
			return obj.MetadataToken;
		}
	}
}
