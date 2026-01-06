using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200012B RID: 299
	[UnitCategory("Nulls")]
	[TypeIcon(typeof(Null))]
	public sealed class NullCheck : Unit
	{
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x0000E640 File Offset: 0x0000C840
		// (set) Token: 0x060007BE RID: 1982 RVA: 0x0000E648 File Offset: 0x0000C848
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput input { get; private set; }

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x0000E651 File Offset: 0x0000C851
		// (set) Token: 0x060007C0 RID: 1984 RVA: 0x0000E659 File Offset: 0x0000C859
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput enter { get; private set; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x0000E662 File Offset: 0x0000C862
		// (set) Token: 0x060007C2 RID: 1986 RVA: 0x0000E66A File Offset: 0x0000C86A
		[DoNotSerialize]
		[PortLabel("Not Null")]
		public ControlOutput ifNotNull { get; private set; }

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x0000E673 File Offset: 0x0000C873
		// (set) Token: 0x060007C4 RID: 1988 RVA: 0x0000E67B File Offset: 0x0000C87B
		[DoNotSerialize]
		[PortLabel("Null")]
		public ControlOutput ifNull { get; private set; }

		// Token: 0x060007C5 RID: 1989 RVA: 0x0000E684 File Offset: 0x0000C884
		protected override void Definition()
		{
			this.enter = base.ControlInput("enter", new Func<Flow, ControlOutput>(this.Enter));
			this.input = base.ValueInput<object>("input").AllowsNull();
			this.ifNotNull = base.ControlOutput("ifNotNull");
			this.ifNull = base.ControlOutput("ifNull");
			base.Requirement(this.input, this.enter);
			base.Succession(this.enter, this.ifNotNull);
			base.Succession(this.enter, this.ifNull);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0000E71C File Offset: 0x0000C91C
		public ControlOutput Enter(Flow flow)
		{
			object value = flow.GetValue(this.input);
			bool flag;
			if (value is Object)
			{
				flag = (Object)value == null;
			}
			else
			{
				flag = value == null;
			}
			if (flag)
			{
				return this.ifNull;
			}
			return this.ifNotNull;
		}
	}
}
