using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000048 RID: 72
	[UnitCategory("Control")]
	[UnitOrder(16)]
	public sealed class Throw : Unit
	{
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00007BDF File Offset: 0x00005DDF
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x00007BE7 File Offset: 0x00005DE7
		[Serialize]
		[Inspectable]
		[UnitHeaderInspectable("Custom")]
		[InspectorToggleLeft]
		public bool custom { get; set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00007BF0 File Offset: 0x00005DF0
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x00007BF8 File Offset: 0x00005DF8
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput enter { get; private set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00007C01 File Offset: 0x00005E01
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x00007C09 File Offset: 0x00005E09
		[DoNotSerialize]
		public ValueInput message { get; private set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00007C12 File Offset: 0x00005E12
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x00007C1A File Offset: 0x00005E1A
		[DoNotSerialize]
		public ValueInput exception { get; private set; }

		// Token: 0x060002CA RID: 714 RVA: 0x00007C24 File Offset: 0x00005E24
		protected override void Definition()
		{
			if (this.custom)
			{
				this.enter = base.ControlInput("enter", new Func<Flow, ControlOutput>(this.ThrowCustom));
				this.exception = base.ValueInput<Exception>("exception");
				base.Requirement(this.exception, this.enter);
				return;
			}
			this.enter = base.ControlInput("enter", new Func<Flow, ControlOutput>(this.ThrowMessage));
			this.message = base.ValueInput<string>("message", string.Empty);
			base.Requirement(this.message, this.enter);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00007CBF File Offset: 0x00005EBF
		private ControlOutput ThrowCustom(Flow flow)
		{
			throw flow.GetValue<Exception>(this.exception);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00007CCD File Offset: 0x00005ECD
		private ControlOutput ThrowMessage(Flow flow)
		{
			throw new Exception(flow.GetValue<string>(this.message));
		}
	}
}
