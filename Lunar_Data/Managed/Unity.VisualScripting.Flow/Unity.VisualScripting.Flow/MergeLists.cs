using System;
using System.Collections;

namespace Unity.VisualScripting
{
	// Token: 0x02000030 RID: 48
	[UnitCategory("Collections/Lists")]
	[UnitOrder(7)]
	public sealed class MergeLists : MultiInputUnit<IEnumerable>
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000616C File Offset: 0x0000436C
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00006174 File Offset: 0x00004374
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput list { get; private set; }

		// Token: 0x060001DC RID: 476 RVA: 0x00006180 File Offset: 0x00004380
		protected override void Definition()
		{
			this.list = base.ValueOutput<IList>("list", new Func<Flow, IList>(this.Merge));
			base.Definition();
			foreach (ValueInput valueInput in base.multiInputs)
			{
				base.Requirement(valueInput, this.list);
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000061F8 File Offset: 0x000043F8
		public IList Merge(Flow flow)
		{
			AotList aotList = new AotList();
			for (int i = 0; i < this.inputCount; i++)
			{
				aotList.AddRange(flow.GetValue<IEnumerable>(base.multiInputs[i]));
			}
			return aotList;
		}
	}
}
