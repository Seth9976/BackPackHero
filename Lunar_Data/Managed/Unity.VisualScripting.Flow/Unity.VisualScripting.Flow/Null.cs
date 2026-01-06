using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200012A RID: 298
	[UnitCategory("Nulls")]
	public sealed class Null : Unit
	{
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x0000E5F0 File Offset: 0x0000C7F0
		// (set) Token: 0x060007BA RID: 1978 RVA: 0x0000E5F8 File Offset: 0x0000C7F8
		[DoNotSerialize]
		public ValueOutput @null { get; private set; }

		// Token: 0x060007BB RID: 1979 RVA: 0x0000E601 File Offset: 0x0000C801
		protected override void Definition()
		{
			this.@null = base.ValueOutput<object>("null", (Flow recursion) => null).Predictable();
		}
	}
}
