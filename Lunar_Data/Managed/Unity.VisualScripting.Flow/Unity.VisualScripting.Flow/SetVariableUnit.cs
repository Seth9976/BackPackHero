using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000151 RID: 337
	[UnitShortTitle("Set Variable")]
	public abstract class SetVariableUnit : VariableUnit
	{
		// Token: 0x060008AE RID: 2222 RVA: 0x0000FEFD File Offset: 0x0000E0FD
		protected SetVariableUnit()
		{
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0000FF05 File Offset: 0x0000E105
		protected SetVariableUnit(string defaultName)
			: base(defaultName)
		{
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x0000FF0E File Offset: 0x0000E10E
		// (set) Token: 0x060008B1 RID: 2225 RVA: 0x0000FF16 File Offset: 0x0000E116
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput assign { get; set; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x0000FF1F File Offset: 0x0000E11F
		// (set) Token: 0x060008B3 RID: 2227 RVA: 0x0000FF27 File Offset: 0x0000E127
		[DoNotSerialize]
		[PortLabel("New Value")]
		[PortLabelHidden]
		public ValueInput input { get; private set; }

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x0000FF30 File Offset: 0x0000E130
		// (set) Token: 0x060008B5 RID: 2229 RVA: 0x0000FF38 File Offset: 0x0000E138
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlOutput assigned { get; set; }

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x0000FF41 File Offset: 0x0000E141
		// (set) Token: 0x060008B7 RID: 2231 RVA: 0x0000FF49 File Offset: 0x0000E149
		[DoNotSerialize]
		[PortLabel("Value")]
		[PortLabelHidden]
		public ValueOutput output { get; private set; }

		// Token: 0x060008B8 RID: 2232 RVA: 0x0000FF54 File Offset: 0x0000E154
		protected override void Definition()
		{
			base.Definition();
			this.assign = base.ControlInput("assign", new Func<Flow, ControlOutput>(this.Assign));
			this.input = base.ValueInput<object>("input");
			this.output = base.ValueOutput<object>("output");
			this.assigned = base.ControlOutput("assigned");
			base.Requirement(this.input, this.assign);
			base.Requirement(base.name, this.assign);
			base.Assignment(this.assign, this.output);
			base.Succession(this.assign, this.assigned);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00010000 File Offset: 0x0000E200
		protected virtual ControlOutput Assign(Flow flow)
		{
			object value = flow.GetValue<object>(this.input);
			string value2 = flow.GetValue<string>(base.name);
			this.GetDeclarations(flow).Set(value2, value);
			flow.SetValue(this.output, value);
			return this.assigned;
		}
	}
}
