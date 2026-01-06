using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000049 RID: 73
	[UnitCategory("Control")]
	[UnitOrder(18)]
	[UnitFooterPorts(ControlInputs = true, ControlOutputs = true)]
	public sealed class ToggleFlow : Unit, IGraphElementWithData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060002CE RID: 718 RVA: 0x00007CE8 File Offset: 0x00005EE8
		// (set) Token: 0x060002CF RID: 719 RVA: 0x00007CF0 File Offset: 0x00005EF0
		[Serialize]
		[Inspectable]
		[UnitHeaderInspectable("Start On")]
		[InspectorToggleLeft]
		public bool startOn { get; set; } = true;

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x00007CF9 File Offset: 0x00005EF9
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x00007D01 File Offset: 0x00005F01
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput enter { get; private set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00007D0A File Offset: 0x00005F0A
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x00007D12 File Offset: 0x00005F12
		[DoNotSerialize]
		[PortLabel("On")]
		public ControlInput turnOn { get; private set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x00007D1B File Offset: 0x00005F1B
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x00007D23 File Offset: 0x00005F23
		[DoNotSerialize]
		[PortLabel("Off")]
		public ControlInput turnOff { get; private set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x00007D2C File Offset: 0x00005F2C
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x00007D34 File Offset: 0x00005F34
		[DoNotSerialize]
		public ControlInput toggle { get; private set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x00007D3D File Offset: 0x00005F3D
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x00007D45 File Offset: 0x00005F45
		[DoNotSerialize]
		[PortLabel("On")]
		public ControlOutput exitOn { get; private set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060002DA RID: 730 RVA: 0x00007D4E File Offset: 0x00005F4E
		// (set) Token: 0x060002DB RID: 731 RVA: 0x00007D56 File Offset: 0x00005F56
		[DoNotSerialize]
		[PortLabel("Off")]
		public ControlOutput exitOff { get; private set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060002DC RID: 732 RVA: 0x00007D5F File Offset: 0x00005F5F
		// (set) Token: 0x060002DD RID: 733 RVA: 0x00007D67 File Offset: 0x00005F67
		[DoNotSerialize]
		public ControlOutput turnedOn { get; private set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00007D70 File Offset: 0x00005F70
		// (set) Token: 0x060002DF RID: 735 RVA: 0x00007D78 File Offset: 0x00005F78
		[DoNotSerialize]
		public ControlOutput turnedOff { get; private set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00007D81 File Offset: 0x00005F81
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x00007D89 File Offset: 0x00005F89
		[DoNotSerialize]
		public ValueOutput isOn { get; private set; }

		// Token: 0x060002E2 RID: 738 RVA: 0x00007D94 File Offset: 0x00005F94
		protected override void Definition()
		{
			this.enter = base.ControlInput("enter", new Func<Flow, ControlOutput>(this.Enter));
			this.turnOn = base.ControlInput("turnOn", new Func<Flow, ControlOutput>(this.TurnOn));
			this.turnOff = base.ControlInput("turnOff", new Func<Flow, ControlOutput>(this.TurnOff));
			this.toggle = base.ControlInput("toggle", new Func<Flow, ControlOutput>(this.Toggle));
			this.exitOn = base.ControlOutput("exitOn");
			this.exitOff = base.ControlOutput("exitOff");
			this.turnedOn = base.ControlOutput("turnedOn");
			this.turnedOff = base.ControlOutput("turnedOff");
			this.isOn = base.ValueOutput<bool>("isOn", new Func<Flow, bool>(this.IsOn));
			base.Succession(this.enter, this.exitOn);
			base.Succession(this.enter, this.exitOff);
			base.Succession(this.turnOn, this.turnedOn);
			base.Succession(this.turnOff, this.turnedOff);
			base.Succession(this.toggle, this.turnedOn);
			base.Succession(this.toggle, this.turnedOff);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00007EE2 File Offset: 0x000060E2
		public IGraphElementData CreateData()
		{
			return new ToggleFlow.Data
			{
				isOn = this.startOn
			};
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00007EF5 File Offset: 0x000060F5
		private bool IsOn(Flow flow)
		{
			return flow.stack.GetElementData<ToggleFlow.Data>(this).isOn;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00007F08 File Offset: 0x00006108
		private ControlOutput Enter(Flow flow)
		{
			if (!this.IsOn(flow))
			{
				return this.exitOff;
			}
			return this.exitOn;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00007F20 File Offset: 0x00006120
		private ControlOutput TurnOn(Flow flow)
		{
			ToggleFlow.Data elementData = flow.stack.GetElementData<ToggleFlow.Data>(this);
			if (elementData.isOn)
			{
				return null;
			}
			elementData.isOn = true;
			return this.turnedOn;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00007F54 File Offset: 0x00006154
		private ControlOutput TurnOff(Flow flow)
		{
			ToggleFlow.Data elementData = flow.stack.GetElementData<ToggleFlow.Data>(this);
			if (!elementData.isOn)
			{
				return null;
			}
			elementData.isOn = false;
			return this.turnedOff;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00007F85 File Offset: 0x00006185
		private ControlOutput Toggle(Flow flow)
		{
			ToggleFlow.Data elementData = flow.stack.GetElementData<ToggleFlow.Data>(this);
			elementData.isOn = !elementData.isOn;
			if (!elementData.isOn)
			{
				return this.turnedOff;
			}
			return this.turnedOn;
		}

		// Token: 0x020001B1 RID: 433
		public class Data : IGraphElementData
		{
			// Token: 0x0400039A RID: 922
			public bool isOn;
		}
	}
}
