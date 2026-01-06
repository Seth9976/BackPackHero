using System;
using System.Collections.Generic;
using System.Linq;

namespace Unity.VisualScripting
{
	// Token: 0x020000C7 RID: 199
	[UnitOrder(304)]
	public abstract class Average<T> : MultiInputUnit<T>
	{
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x0000C29D File Offset: 0x0000A49D
		// (set) Token: 0x06000604 RID: 1540 RVA: 0x0000C2A5 File Offset: 0x0000A4A5
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput average { get; private set; }

		// Token: 0x06000605 RID: 1541 RVA: 0x0000C2B0 File Offset: 0x0000A4B0
		protected override void Definition()
		{
			base.Definition();
			this.average = base.ValueOutput<T>("average", new Func<Flow, T>(this.Operation)).Predictable();
			foreach (ValueInput valueInput in base.multiInputs)
			{
				base.Requirement(valueInput, this.average);
			}
		}

		// Token: 0x06000606 RID: 1542
		public abstract T Operation(T a, T b);

		// Token: 0x06000607 RID: 1543
		public abstract T Operation(IEnumerable<T> values);

		// Token: 0x06000608 RID: 1544 RVA: 0x0000C32C File Offset: 0x0000A52C
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
