using System;
using System.Collections.Generic;
using System.Linq;

namespace Unity.VisualScripting
{
	// Token: 0x020000D1 RID: 209
	[UnitCategory("Math/Generic")]
	[UnitTitle("Add")]
	public sealed class GenericSum : Sum<object>
	{
		// Token: 0x0600063E RID: 1598 RVA: 0x0000C766 File Offset: 0x0000A966
		public override object Operation(object a, object b)
		{
			return OperatorUtility.Add(a, b);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0000C770 File Offset: 0x0000A970
		public override object Operation(IEnumerable<object> values)
		{
			List<object> list = values.ToList<object>();
			object obj = OperatorUtility.Add(list[0], list[1]);
			for (int i = 2; i < list.Count; i++)
			{
				obj = OperatorUtility.Add(obj, list[i]);
			}
			return obj;
		}
	}
}
