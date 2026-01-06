using System;
using System.Collections;

namespace Unity.VisualScripting
{
	// Token: 0x0200002B RID: 43
	[UnitCategory("Collections/Lists")]
	[UnitSurtitle("List")]
	[UnitShortTitle("Clear")]
	[UnitOrder(6)]
	[TypeIcon(typeof(RemoveListItem))]
	public sealed class ClearList : Unit
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00005BC4 File Offset: 0x00003DC4
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00005BCC File Offset: 0x00003DCC
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput enter { get; private set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00005BD5 File Offset: 0x00003DD5
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00005BDD File Offset: 0x00003DDD
		[DoNotSerialize]
		[PortLabel("List")]
		[PortLabelHidden]
		public ValueInput listInput { get; private set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00005BE6 File Offset: 0x00003DE6
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00005BEE File Offset: 0x00003DEE
		[DoNotSerialize]
		[PortLabel("List")]
		[PortLabelHidden]
		public ValueOutput listOutput { get; private set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00005BF7 File Offset: 0x00003DF7
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00005BFF File Offset: 0x00003DFF
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlOutput exit { get; private set; }

		// Token: 0x060001AE RID: 430 RVA: 0x00005C08 File Offset: 0x00003E08
		protected override void Definition()
		{
			this.enter = base.ControlInput("enter", new Func<Flow, ControlOutput>(this.Clear));
			this.listInput = base.ValueInput<IList>("listInput");
			this.listOutput = base.ValueOutput<IList>("listOutput");
			this.exit = base.ControlOutput("exit");
			base.Requirement(this.listInput, this.enter);
			base.Assignment(this.enter, this.listOutput);
			base.Succession(this.enter, this.exit);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00005C9C File Offset: 0x00003E9C
		public ControlOutput Clear(Flow flow)
		{
			IList value = flow.GetValue<IList>(this.listInput);
			if (value is Array)
			{
				flow.SetValue(this.listOutput, Array.CreateInstance(value.GetType().GetElementType(), 0));
			}
			else
			{
				value.Clear();
				flow.SetValue(this.listOutput, value);
			}
			return this.exit;
		}
	}
}
