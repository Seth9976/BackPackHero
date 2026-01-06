using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000138 RID: 312
	[UnitTitle("Has Variable")]
	public sealed class IsVariableDefined : UnifiedVariableUnit
	{
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x0000F914 File Offset: 0x0000DB14
		// (set) Token: 0x06000855 RID: 2133 RVA: 0x0000F91C File Offset: 0x0000DB1C
		[DoNotSerialize]
		[PortLabel("Defined")]
		[PortLabelHidden]
		[PortKey("isDefined")]
		public ValueOutput isVariableDefined { get; private set; }

		// Token: 0x06000856 RID: 2134 RVA: 0x0000F928 File Offset: 0x0000DB28
		protected override void Definition()
		{
			base.Definition();
			this.isVariableDefined = base.ValueOutput<bool>("isDefined", new Func<Flow, bool>(this.IsDefined));
			base.Requirement(base.name, this.isVariableDefined);
			if (base.kind == VariableKind.Object)
			{
				base.Requirement(base.@object, this.isVariableDefined);
			}
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0000F988 File Offset: 0x0000DB88
		private bool IsDefined(Flow flow)
		{
			string value = flow.GetValue<string>(base.name);
			switch (base.kind)
			{
			case VariableKind.Flow:
				return flow.variables.IsDefined(value);
			case VariableKind.Graph:
				return Variables.Graph(flow.stack).IsDefined(value);
			case VariableKind.Object:
				return Variables.Object(flow.GetValue<GameObject>(base.@object)).IsDefined(value);
			case VariableKind.Scene:
				return Variables.Scene(flow.stack.scene).IsDefined(value);
			case VariableKind.Application:
				return Variables.Application.IsDefined(value);
			case VariableKind.Saved:
				return Variables.Saved.IsDefined(value);
			default:
				throw new UnexpectedEnumValueException<VariableKind>(base.kind);
			}
		}
	}
}
