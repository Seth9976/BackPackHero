using System;
using System.Linq;

namespace Unity.VisualScripting
{
	// Token: 0x02000129 RID: 297
	[UnitCategory("Nesting")]
	[UnitOrder(2)]
	[UnitTitle("Output")]
	public sealed class GraphOutput : Unit
	{
		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x0000E4CC File Offset: 0x0000C6CC
		public override bool canDefine
		{
			get
			{
				return base.graph != null;
			}
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0000E4D8 File Offset: 0x0000C6D8
		protected override void Definition()
		{
			this.isControlRoot = true;
			foreach (ControlOutputDefinition controlOutputDefinition in base.graph.validPortDefinitions.OfType<ControlOutputDefinition>())
			{
				string key = controlOutputDefinition.key;
				base.ControlInput(key, delegate(Flow flow)
				{
					SubgraphUnit parent = flow.stack.GetParent<SubgraphUnit>();
					flow.stack.ExitParentElement();
					parent.EnsureDefined();
					return parent.controlOutputs[key];
				});
			}
			foreach (ValueOutputDefinition valueOutputDefinition in base.graph.validPortDefinitions.OfType<ValueOutputDefinition>())
			{
				string key2 = valueOutputDefinition.key;
				Type type = valueOutputDefinition.type;
				base.ValueInput(type, key2);
			}
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0000E5B4 File Offset: 0x0000C7B4
		protected override void AfterDefine()
		{
			base.graph.onPortDefinitionsChanged += this.Define;
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0000E5CE File Offset: 0x0000C7CE
		protected override void BeforeUndefine()
		{
			base.graph.onPortDefinitionsChanged -= this.Define;
		}
	}
}
