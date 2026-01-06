using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000154 RID: 340
	[UnitShortTitle("Set Variable")]
	public sealed class SetVariable : UnifiedVariableUnit
	{
		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x0001014B File Offset: 0x0000E34B
		// (set) Token: 0x060008CA RID: 2250 RVA: 0x00010153 File Offset: 0x0000E353
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput assign { get; set; }

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x0001015C File Offset: 0x0000E35C
		// (set) Token: 0x060008CC RID: 2252 RVA: 0x00010164 File Offset: 0x0000E364
		[DoNotSerialize]
		[PortLabel("New Value")]
		[PortLabelHidden]
		public ValueInput input { get; private set; }

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x0001016D File Offset: 0x0000E36D
		// (set) Token: 0x060008CE RID: 2254 RVA: 0x00010175 File Offset: 0x0000E375
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlOutput assigned { get; set; }

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x0001017E File Offset: 0x0000E37E
		// (set) Token: 0x060008D0 RID: 2256 RVA: 0x00010186 File Offset: 0x0000E386
		[DoNotSerialize]
		[PortLabel("Value")]
		[PortLabelHidden]
		public ValueOutput output { get; private set; }

		// Token: 0x060008D1 RID: 2257 RVA: 0x00010190 File Offset: 0x0000E390
		protected override void Definition()
		{
			base.Definition();
			this.assign = base.ControlInput("assign", new Func<Flow, ControlOutput>(this.Assign));
			this.input = base.ValueInput<object>("input").AllowsNull();
			this.output = base.ValueOutput<object>("output");
			this.assigned = base.ControlOutput("assigned");
			base.Requirement(base.name, this.assign);
			base.Requirement(this.input, this.assign);
			base.Assignment(this.assign, this.output);
			base.Succession(this.assign, this.assigned);
			if (base.kind == VariableKind.Object)
			{
				base.Requirement(base.@object, this.assign);
			}
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0001025C File Offset: 0x0000E45C
		private ControlOutput Assign(Flow flow)
		{
			string value = flow.GetValue<string>(base.name);
			object value2 = flow.GetValue(this.input);
			switch (base.kind)
			{
			case VariableKind.Flow:
				flow.variables.Set(value, value2);
				break;
			case VariableKind.Graph:
				Variables.Graph(flow.stack).Set(value, value2);
				break;
			case VariableKind.Object:
				Variables.Object(flow.GetValue<GameObject>(base.@object)).Set(value, value2);
				break;
			case VariableKind.Scene:
				Variables.Scene(flow.stack.scene).Set(value, value2);
				break;
			case VariableKind.Application:
				Variables.Application.Set(value, value2);
				break;
			case VariableKind.Saved:
				Variables.Saved.Set(value, value2);
				break;
			default:
				throw new UnexpectedEnumValueException<VariableKind>(base.kind);
			}
			flow.SetValue(this.output, value2);
			return this.assigned;
		}
	}
}
