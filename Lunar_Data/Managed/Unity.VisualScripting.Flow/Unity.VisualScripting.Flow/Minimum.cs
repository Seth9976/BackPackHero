using System;
using System.Collections.Generic;
using System.Linq;

namespace Unity.VisualScripting
{
	// Token: 0x020000D4 RID: 212
	[UnitOrder(301)]
	public abstract class Minimum<T> : MultiInputUnit<T>
	{
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x0000CA06 File Offset: 0x0000AC06
		// (set) Token: 0x06000657 RID: 1623 RVA: 0x0000CA0E File Offset: 0x0000AC0E
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput minimum { get; private set; }

		// Token: 0x06000658 RID: 1624 RVA: 0x0000CA18 File Offset: 0x0000AC18
		protected override void Definition()
		{
			base.Definition();
			this.minimum = base.ValueOutput<T>("minimum", new Func<Flow, T>(this.Operation)).Predictable();
			foreach (ValueInput valueInput in base.multiInputs)
			{
				base.Requirement(valueInput, this.minimum);
			}
		}

		// Token: 0x06000659 RID: 1625
		public abstract T Operation(T a, T b);

		// Token: 0x0600065A RID: 1626
		public abstract T Operation(IEnumerable<T> values);

		// Token: 0x0600065B RID: 1627 RVA: 0x0000CA94 File Offset: 0x0000AC94
		public T Operation(Flow flow)
		{
			if (this.inputCount == 2)
			{
				return this.Operation(flow.GetValue<T>(base.multiInputs[0]), flow.GetValue<T>(base.multiInputs[1]));
			}
			return this.Operation(base.multiInputs.Select(new Func<ValueInput, T>(flow.GetValue<T>)));
		}
	}
}
