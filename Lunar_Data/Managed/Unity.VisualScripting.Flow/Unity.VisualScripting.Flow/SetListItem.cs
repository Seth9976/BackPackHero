using System;
using System.Collections;

namespace Unity.VisualScripting
{
	// Token: 0x02000033 RID: 51
	[UnitCategory("Collections/Lists")]
	[UnitSurtitle("List")]
	[UnitShortTitle("Set Item")]
	[UnitOrder(1)]
	[TypeIcon(typeof(IList))]
	public sealed class SetListItem : Unit
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00006556 File Offset: 0x00004756
		// (set) Token: 0x060001FA RID: 506 RVA: 0x0000655E File Offset: 0x0000475E
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput enter { get; private set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00006567 File Offset: 0x00004767
		// (set) Token: 0x060001FC RID: 508 RVA: 0x0000656F File Offset: 0x0000476F
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput list { get; private set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00006578 File Offset: 0x00004778
		// (set) Token: 0x060001FE RID: 510 RVA: 0x00006580 File Offset: 0x00004780
		[DoNotSerialize]
		public ValueInput index { get; private set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00006589 File Offset: 0x00004789
		// (set) Token: 0x06000200 RID: 512 RVA: 0x00006591 File Offset: 0x00004791
		[DoNotSerialize]
		public ValueInput item { get; private set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0000659A File Offset: 0x0000479A
		// (set) Token: 0x06000202 RID: 514 RVA: 0x000065A2 File Offset: 0x000047A2
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlOutput exit { get; private set; }

		// Token: 0x06000203 RID: 515 RVA: 0x000065AC File Offset: 0x000047AC
		protected override void Definition()
		{
			this.enter = base.ControlInput("enter", new Func<Flow, ControlOutput>(this.Set));
			this.list = base.ValueInput<IList>("list");
			this.index = base.ValueInput<int>("index", 0);
			this.item = base.ValueInput<object>("item");
			this.exit = base.ControlOutput("exit");
			base.Requirement(this.list, this.enter);
			base.Requirement(this.index, this.enter);
			base.Requirement(this.item, this.enter);
			base.Succession(this.enter, this.exit);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00006664 File Offset: 0x00004864
		public ControlOutput Set(Flow flow)
		{
			IList value = flow.GetValue<IList>(this.list);
			int value2 = flow.GetValue<int>(this.index);
			object value3 = flow.GetValue<object>(this.item);
			value[value2] = value3;
			return this.exit;
		}
	}
}
