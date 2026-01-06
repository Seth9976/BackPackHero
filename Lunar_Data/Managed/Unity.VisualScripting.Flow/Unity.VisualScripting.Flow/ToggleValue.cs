using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200004A RID: 74
	[UnitCategory("Control")]
	[UnitOrder(19)]
	[UnitFooterPorts(ControlInputs = true, ControlOutputs = true)]
	public sealed class ToggleValue : Unit, IGraphElementWithData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00007FC5 File Offset: 0x000061C5
		// (set) Token: 0x060002EB RID: 747 RVA: 0x00007FCD File Offset: 0x000061CD
		[Serialize]
		[Inspectable]
		[UnitHeaderInspectable("Start On")]
		[InspectorToggleLeft]
		public bool startOn { get; set; } = true;

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00007FD6 File Offset: 0x000061D6
		// (set) Token: 0x060002ED RID: 749 RVA: 0x00007FDE File Offset: 0x000061DE
		[DoNotSerialize]
		[PortLabel("On")]
		public ControlInput turnOn { get; private set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00007FE7 File Offset: 0x000061E7
		// (set) Token: 0x060002EF RID: 751 RVA: 0x00007FEF File Offset: 0x000061EF
		[DoNotSerialize]
		[PortLabel("Off")]
		public ControlInput turnOff { get; private set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x00007FF8 File Offset: 0x000061F8
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x00008000 File Offset: 0x00006200
		[DoNotSerialize]
		public ControlInput toggle { get; private set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00008009 File Offset: 0x00006209
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x00008011 File Offset: 0x00006211
		[DoNotSerialize]
		public ValueInput onValue { get; private set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000801A File Offset: 0x0000621A
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x00008022 File Offset: 0x00006222
		[DoNotSerialize]
		public ValueInput offValue { get; private set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000802B File Offset: 0x0000622B
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x00008033 File Offset: 0x00006233
		[DoNotSerialize]
		public ControlOutput turnedOn { get; private set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000803C File Offset: 0x0000623C
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x00008044 File Offset: 0x00006244
		[DoNotSerialize]
		public ControlOutput turnedOff { get; private set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000804D File Offset: 0x0000624D
		// (set) Token: 0x060002FB RID: 763 RVA: 0x00008055 File Offset: 0x00006255
		[DoNotSerialize]
		public ValueOutput isOn { get; private set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000805E File Offset: 0x0000625E
		// (set) Token: 0x060002FD RID: 765 RVA: 0x00008066 File Offset: 0x00006266
		[DoNotSerialize]
		public ValueOutput value { get; private set; }

		// Token: 0x060002FE RID: 766 RVA: 0x00008070 File Offset: 0x00006270
		protected override void Definition()
		{
			this.turnOn = base.ControlInput("turnOn", new Func<Flow, ControlOutput>(this.TurnOn));
			this.turnOff = base.ControlInput("turnOff", new Func<Flow, ControlOutput>(this.TurnOff));
			this.toggle = base.ControlInput("toggle", new Func<Flow, ControlOutput>(this.Toggle));
			this.onValue = base.ValueInput<object>("onValue");
			this.offValue = base.ValueInput<object>("offValue");
			this.turnedOn = base.ControlOutput("turnedOn");
			this.turnedOff = base.ControlOutput("turnedOff");
			this.isOn = base.ValueOutput<bool>("isOn", new Func<Flow, bool>(this.IsOn));
			this.value = base.ValueOutput<object>("value", new Func<Flow, object>(this.Value));
			base.Requirement(this.onValue, this.value);
			base.Requirement(this.offValue, this.value);
			base.Succession(this.turnOn, this.turnedOn);
			base.Succession(this.turnOff, this.turnedOff);
			base.Succession(this.toggle, this.turnedOn);
			base.Succession(this.toggle, this.turnedOff);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x000081BE File Offset: 0x000063BE
		public IGraphElementData CreateData()
		{
			return new ToggleValue.Data
			{
				isOn = this.startOn
			};
		}

		// Token: 0x06000300 RID: 768 RVA: 0x000081D1 File Offset: 0x000063D1
		private bool IsOn(Flow flow)
		{
			return flow.stack.GetElementData<ToggleValue.Data>(this).isOn;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x000081E4 File Offset: 0x000063E4
		private ControlOutput TurnOn(Flow flow)
		{
			ToggleValue.Data elementData = flow.stack.GetElementData<ToggleValue.Data>(this);
			if (elementData.isOn)
			{
				return null;
			}
			elementData.isOn = true;
			return this.turnedOn;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00008218 File Offset: 0x00006418
		private ControlOutput TurnOff(Flow flow)
		{
			ToggleValue.Data elementData = flow.stack.GetElementData<ToggleValue.Data>(this);
			if (!elementData.isOn)
			{
				return null;
			}
			elementData.isOn = false;
			return this.turnedOff;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00008249 File Offset: 0x00006449
		private ControlOutput Toggle(Flow flow)
		{
			ToggleValue.Data elementData = flow.stack.GetElementData<ToggleValue.Data>(this);
			elementData.isOn = !elementData.isOn;
			if (!elementData.isOn)
			{
				return this.turnedOff;
			}
			return this.turnedOn;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000827C File Offset: 0x0000647C
		private object Value(Flow flow)
		{
			ToggleValue.Data elementData = flow.stack.GetElementData<ToggleValue.Data>(this);
			return flow.GetValue(elementData.isOn ? this.onValue : this.offValue);
		}

		// Token: 0x020001B2 RID: 434
		public class Data : IGraphElementData
		{
			// Token: 0x0400039B RID: 923
			public bool isOn;
		}
	}
}
