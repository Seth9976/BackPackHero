using System;
using System.Collections.Generic;
using System.Linq;

namespace Unity.VisualScripting
{
	// Token: 0x020000D3 RID: 211
	[UnitOrder(302)]
	public abstract class Maximum<T> : MultiInputUnit<T>
	{
		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x0000C911 File Offset: 0x0000AB11
		// (set) Token: 0x06000650 RID: 1616 RVA: 0x0000C919 File Offset: 0x0000AB19
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput maximum { get; private set; }

		// Token: 0x06000651 RID: 1617 RVA: 0x0000C924 File Offset: 0x0000AB24
		protected override void Definition()
		{
			base.Definition();
			this.maximum = base.ValueOutput<T>("maximum", new Func<Flow, T>(this.Operation)).Predictable();
			foreach (ValueInput valueInput in base.multiInputs)
			{
				base.Requirement(valueInput, this.maximum);
			}
		}

		// Token: 0x06000652 RID: 1618
		public abstract T Operation(T a, T b);

		// Token: 0x06000653 RID: 1619
		public abstract T Operation(IEnumerable<T> values);

		// Token: 0x06000654 RID: 1620 RVA: 0x0000C9A0 File Offset: 0x0000ABA0
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
