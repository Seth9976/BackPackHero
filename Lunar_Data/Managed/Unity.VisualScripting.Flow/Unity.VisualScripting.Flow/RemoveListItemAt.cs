using System;
using System.Collections;

namespace Unity.VisualScripting
{
	// Token: 0x02000032 RID: 50
	[UnitCategory("Collections/Lists")]
	[UnitSurtitle("List")]
	[UnitShortTitle("Remove Item At Index")]
	[UnitOrder(5)]
	[TypeIcon(typeof(RemoveListItem))]
	public sealed class RemoveListItemAt : Unit
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001EC RID: 492 RVA: 0x000063CA File Offset: 0x000045CA
		// (set) Token: 0x060001ED RID: 493 RVA: 0x000063D2 File Offset: 0x000045D2
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput enter { get; private set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001EE RID: 494 RVA: 0x000063DB File Offset: 0x000045DB
		// (set) Token: 0x060001EF RID: 495 RVA: 0x000063E3 File Offset: 0x000045E3
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput listInput { get; private set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x000063EC File Offset: 0x000045EC
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x000063F4 File Offset: 0x000045F4
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput listOutput { get; private set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x000063FD File Offset: 0x000045FD
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x00006405 File Offset: 0x00004605
		[DoNotSerialize]
		public ValueInput index { get; private set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000640E File Offset: 0x0000460E
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00006416 File Offset: 0x00004616
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlOutput exit { get; private set; }

		// Token: 0x060001F6 RID: 502 RVA: 0x00006420 File Offset: 0x00004620
		protected override void Definition()
		{
			this.enter = base.ControlInput("enter", new Func<Flow, ControlOutput>(this.RemoveAt));
			this.listInput = base.ValueInput<IList>("listInput");
			this.listOutput = base.ValueOutput<IList>("listOutput");
			this.index = base.ValueInput<int>("index", 0);
			this.exit = base.ControlOutput("exit");
			base.Requirement(this.listInput, this.enter);
			base.Requirement(this.index, this.enter);
			base.Assignment(this.enter, this.listOutput);
			base.Succession(this.enter, this.exit);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x000064D8 File Offset: 0x000046D8
		public ControlOutput RemoveAt(Flow flow)
		{
			IList value = flow.GetValue<IList>(this.listInput);
			int value2 = flow.GetValue<int>(this.index);
			if (value is Array)
			{
				ArrayList arrayList = new ArrayList(value);
				arrayList.RemoveAt(value2);
				flow.SetValue(this.listOutput, arrayList.ToArray(value.GetType().GetElementType()));
			}
			else
			{
				value.RemoveAt(value2);
				flow.SetValue(this.listOutput, value);
			}
			return this.exit;
		}
	}
}
