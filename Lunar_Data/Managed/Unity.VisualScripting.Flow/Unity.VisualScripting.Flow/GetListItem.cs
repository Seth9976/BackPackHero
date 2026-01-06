using System;
using System.Collections;

namespace Unity.VisualScripting
{
	// Token: 0x0200002D RID: 45
	[UnitCategory("Collections/Lists")]
	[UnitSurtitle("List")]
	[UnitShortTitle("Get Item")]
	[UnitOrder(0)]
	[TypeIcon(typeof(IList))]
	public sealed class GetListItem : Unit
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00005DE6 File Offset: 0x00003FE6
		// (set) Token: 0x060001BA RID: 442 RVA: 0x00005DEE File Offset: 0x00003FEE
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput list { get; private set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00005DF7 File Offset: 0x00003FF7
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00005DFF File Offset: 0x00003FFF
		[DoNotSerialize]
		public ValueInput index { get; private set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00005E08 File Offset: 0x00004008
		// (set) Token: 0x060001BE RID: 446 RVA: 0x00005E10 File Offset: 0x00004010
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput item { get; private set; }

		// Token: 0x060001BF RID: 447 RVA: 0x00005E1C File Offset: 0x0000401C
		protected override void Definition()
		{
			this.list = base.ValueInput<IList>("list");
			this.index = base.ValueInput<int>("index", 0);
			this.item = base.ValueOutput<object>("item", new Func<Flow, object>(this.Get));
			base.Requirement(this.list, this.item);
			base.Requirement(this.index, this.item);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00005E90 File Offset: 0x00004090
		public object Get(Flow flow)
		{
			IList value = flow.GetValue<IList>(this.list);
			int value2 = flow.GetValue<int>(this.index);
			return value[value2];
		}
	}
}
