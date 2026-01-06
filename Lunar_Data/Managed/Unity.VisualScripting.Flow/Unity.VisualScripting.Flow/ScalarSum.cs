using System;
using System.Collections.Generic;
using System.Linq;

namespace Unity.VisualScripting
{
	// Token: 0x020000EC RID: 236
	[UnitCategory("Math/Scalar")]
	[UnitTitle("Add")]
	public sealed class ScalarSum : Sum<float>, IDefaultValue<float>
	{
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x0000D4AA File Offset: 0x0000B6AA
		[DoNotSerialize]
		public float defaultValue
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0000D4B1 File Offset: 0x0000B6B1
		public override float Operation(float a, float b)
		{
			return a + b;
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0000D4B6 File Offset: 0x0000B6B6
		public override float Operation(IEnumerable<float> values)
		{
			return values.Sum();
		}
	}
}
