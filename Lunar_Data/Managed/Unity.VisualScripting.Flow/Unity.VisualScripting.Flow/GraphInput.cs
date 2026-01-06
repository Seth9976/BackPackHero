using System;
using System.Linq;

namespace Unity.VisualScripting
{
	// Token: 0x02000128 RID: 296
	[UnitCategory("Nesting")]
	[UnitOrder(1)]
	[UnitTitle("Input")]
	public sealed class GraphInput : Unit
	{
		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x0000E39C File Offset: 0x0000C59C
		public override bool canDefine
		{
			get
			{
				return base.graph != null;
			}
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0000E3A8 File Offset: 0x0000C5A8
		protected override void Definition()
		{
			this.isControlRoot = true;
			foreach (ControlInputDefinition controlInputDefinition in base.graph.validPortDefinitions.OfType<ControlInputDefinition>())
			{
				base.ControlOutput(controlInputDefinition.key);
			}
			foreach (ValueInputDefinition valueInputDefinition in base.graph.validPortDefinitions.OfType<ValueInputDefinition>())
			{
				string key = valueInputDefinition.key;
				Type type = valueInputDefinition.type;
				base.ValueOutput(type, key, delegate(Flow flow)
				{
					SubgraphUnit parent = flow.stack.GetParent<SubgraphUnit>();
					if (flow.enableDebug)
					{
						IUnitDebugData elementDebugData = flow.stack.GetElementDebugData<IUnitDebugData>(parent);
						elementDebugData.lastInvokeFrame = EditorTimeBinding.frame;
						elementDebugData.lastInvokeTime = EditorTimeBinding.time;
					}
					flow.stack.ExitParentElement();
					parent.EnsureDefined();
					object value = flow.GetValue(parent.valueInputs[key], type);
					flow.stack.EnterParentElement(parent);
					return value;
				});
			}
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0000E490 File Offset: 0x0000C690
		protected override void AfterDefine()
		{
			base.graph.onPortDefinitionsChanged += this.Define;
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0000E4AA File Offset: 0x0000C6AA
		protected override void BeforeUndefine()
		{
			base.graph.onPortDefinitionsChanged -= this.Define;
		}
	}
}
