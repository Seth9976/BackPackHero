using System;
using System.Collections;

namespace Unity.VisualScripting
{
	// Token: 0x0200002A RID: 42
	[UnitCategory("Collections/Lists")]
	[UnitSurtitle("List")]
	[UnitShortTitle("Add Item")]
	[UnitOrder(2)]
	public sealed class AddListItem : Unit
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00005A35 File Offset: 0x00003C35
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00005A3D File Offset: 0x00003C3D
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput enter { get; private set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00005A46 File Offset: 0x00003C46
		// (set) Token: 0x0600019C RID: 412 RVA: 0x00005A4E File Offset: 0x00003C4E
		[DoNotSerialize]
		[PortLabel("List")]
		[PortLabelHidden]
		public ValueInput listInput { get; private set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00005A57 File Offset: 0x00003C57
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00005A5F File Offset: 0x00003C5F
		[DoNotSerialize]
		[PortLabel("List")]
		[PortLabelHidden]
		public ValueOutput listOutput { get; private set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00005A68 File Offset: 0x00003C68
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00005A70 File Offset: 0x00003C70
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput item { get; private set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00005A79 File Offset: 0x00003C79
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x00005A81 File Offset: 0x00003C81
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlOutput exit { get; private set; }

		// Token: 0x060001A3 RID: 419 RVA: 0x00005A8C File Offset: 0x00003C8C
		protected override void Definition()
		{
			this.enter = base.ControlInput("enter", new Func<Flow, ControlOutput>(this.Add));
			this.listInput = base.ValueInput<IList>("listInput");
			this.item = base.ValueInput<object>("item");
			this.listOutput = base.ValueOutput<IList>("listOutput");
			this.exit = base.ControlOutput("exit");
			base.Requirement(this.listInput, this.enter);
			base.Requirement(this.item, this.enter);
			base.Assignment(this.enter, this.listOutput);
			base.Succession(this.enter, this.exit);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00005B44 File Offset: 0x00003D44
		public ControlOutput Add(Flow flow)
		{
			IList value = flow.GetValue<IList>(this.listInput);
			object value2 = flow.GetValue<object>(this.item);
			if (value is Array)
			{
				ArrayList arrayList = new ArrayList(value);
				arrayList.Add(value2);
				flow.SetValue(this.listOutput, arrayList.ToArray(value.GetType().GetElementType()));
			}
			else
			{
				value.Add(value2);
				flow.SetValue(this.listOutput, value);
			}
			return this.exit;
		}
	}
}
