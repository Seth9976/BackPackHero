using System;
using System.Collections;

namespace Unity.VisualScripting
{
	// Token: 0x0200002E RID: 46
	[UnitCategory("Collections/Lists")]
	[UnitSurtitle("List")]
	[UnitShortTitle("Insert Item")]
	[UnitOrder(3)]
	[TypeIcon(typeof(AddListItem))]
	public sealed class InsertListItem : Unit
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00005EC4 File Offset: 0x000040C4
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x00005ECC File Offset: 0x000040CC
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput enter { get; private set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00005ED5 File Offset: 0x000040D5
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x00005EDD File Offset: 0x000040DD
		[DoNotSerialize]
		[PortLabel("List")]
		[PortLabelHidden]
		public ValueInput listInput { get; private set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00005EE6 File Offset: 0x000040E6
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x00005EEE File Offset: 0x000040EE
		[DoNotSerialize]
		[PortLabel("List")]
		[PortLabelHidden]
		public ValueOutput listOutput { get; private set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00005EF7 File Offset: 0x000040F7
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x00005EFF File Offset: 0x000040FF
		[DoNotSerialize]
		public ValueInput index { get; private set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00005F08 File Offset: 0x00004108
		// (set) Token: 0x060001CB RID: 459 RVA: 0x00005F10 File Offset: 0x00004110
		[DoNotSerialize]
		public ValueInput item { get; private set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00005F19 File Offset: 0x00004119
		// (set) Token: 0x060001CD RID: 461 RVA: 0x00005F21 File Offset: 0x00004121
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlOutput exit { get; private set; }

		// Token: 0x060001CE RID: 462 RVA: 0x00005F2C File Offset: 0x0000412C
		protected override void Definition()
		{
			this.enter = base.ControlInput("enter", new Func<Flow, ControlOutput>(this.Insert));
			this.listInput = base.ValueInput<IList>("listInput");
			this.item = base.ValueInput<object>("item");
			this.index = base.ValueInput<int>("index", 0);
			this.listOutput = base.ValueOutput<IList>("listOutput");
			this.exit = base.ControlOutput("exit");
			base.Requirement(this.listInput, this.enter);
			base.Requirement(this.item, this.enter);
			base.Requirement(this.index, this.enter);
			base.Assignment(this.enter, this.listOutput);
			base.Succession(this.enter, this.exit);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00006008 File Offset: 0x00004208
		public ControlOutput Insert(Flow flow)
		{
			IList value = flow.GetValue<IList>(this.listInput);
			int value2 = flow.GetValue<int>(this.index);
			object value3 = flow.GetValue<object>(this.item);
			if (value is Array)
			{
				ArrayList arrayList = new ArrayList(value);
				arrayList.Insert(value2, value3);
				flow.SetValue(this.listOutput, arrayList.ToArray(value.GetType().GetElementType()));
			}
			else
			{
				value.Insert(value2, value3);
				flow.SetValue(this.listOutput, value);
			}
			return this.exit;
		}
	}
}
