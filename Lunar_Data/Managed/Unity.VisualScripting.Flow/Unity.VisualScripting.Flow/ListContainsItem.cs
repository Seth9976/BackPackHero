using System;
using System.Collections;

namespace Unity.VisualScripting
{
	// Token: 0x0200002F RID: 47
	[UnitCategory("Collections/Lists")]
	[UnitSurtitle("List")]
	[UnitShortTitle("Contains Item")]
	[TypeIcon(typeof(IList))]
	public sealed class ListContainsItem : Unit
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00006095 File Offset: 0x00004295
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x0000609D File Offset: 0x0000429D
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput list { get; private set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x000060A6 File Offset: 0x000042A6
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x000060AE File Offset: 0x000042AE
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput item { get; private set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x000060B7 File Offset: 0x000042B7
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x000060BF File Offset: 0x000042BF
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput contains { get; private set; }

		// Token: 0x060001D7 RID: 471 RVA: 0x000060C8 File Offset: 0x000042C8
		protected override void Definition()
		{
			this.list = base.ValueInput<IList>("list");
			this.item = base.ValueInput<object>("item");
			this.contains = base.ValueOutput<bool>("contains", new Func<Flow, bool>(this.Contains));
			base.Requirement(this.list, this.contains);
			base.Requirement(this.item, this.contains);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00006138 File Offset: 0x00004338
		public bool Contains(Flow flow)
		{
			IList value = flow.GetValue<IList>(this.list);
			object value2 = flow.GetValue<object>(this.item);
			return value.Contains(value2);
		}
	}
}
