using System;
using System.Collections;

namespace Unity.VisualScripting
{
	// Token: 0x02000031 RID: 49
	[UnitCategory("Collections/Lists")]
	[UnitSurtitle("List")]
	[UnitShortTitle("Remove Item")]
	[UnitOrder(4)]
	public sealed class RemoveListItem : Unit
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000623D File Offset: 0x0000443D
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x00006245 File Offset: 0x00004445
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput enter { get; private set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000624E File Offset: 0x0000444E
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x00006256 File Offset: 0x00004456
		[DoNotSerialize]
		[PortLabel("List")]
		[PortLabelHidden]
		public ValueInput listInput { get; private set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000625F File Offset: 0x0000445F
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x00006267 File Offset: 0x00004467
		[DoNotSerialize]
		[PortLabel("List")]
		[PortLabelHidden]
		public ValueOutput listOutput { get; private set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00006270 File Offset: 0x00004470
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x00006278 File Offset: 0x00004478
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput item { get; private set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00006281 File Offset: 0x00004481
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x00006289 File Offset: 0x00004489
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlOutput exit { get; private set; }

		// Token: 0x060001E9 RID: 489 RVA: 0x00006294 File Offset: 0x00004494
		protected override void Definition()
		{
			this.enter = base.ControlInput("enter", new Func<Flow, ControlOutput>(this.Remove));
			this.listInput = base.ValueInput<IList>("listInput");
			this.listOutput = base.ValueOutput<IList>("listOutput");
			this.item = base.ValueInput<object>("item");
			this.exit = base.ControlOutput("exit");
			base.Requirement(this.listInput, this.enter);
			base.Requirement(this.item, this.enter);
			base.Assignment(this.enter, this.listOutput);
			base.Succession(this.enter, this.exit);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000634C File Offset: 0x0000454C
		public ControlOutput Remove(Flow flow)
		{
			IList value = flow.GetValue<IList>(this.listInput);
			object value2 = flow.GetValue<object>(this.item);
			if (value is Array)
			{
				ArrayList arrayList = new ArrayList(value);
				arrayList.Remove(value2);
				flow.SetValue(this.listOutput, arrayList.ToArray(value.GetType().GetElementType()));
			}
			else
			{
				value.Remove(value2);
				flow.SetValue(this.listOutput, value);
			}
			return this.exit;
		}
	}
}
