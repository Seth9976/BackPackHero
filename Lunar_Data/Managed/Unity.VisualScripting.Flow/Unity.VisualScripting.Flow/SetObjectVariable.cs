using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200014E RID: 334
	[UnitSurtitle("Object")]
	public sealed class SetObjectVariable : SetVariableUnit, IObjectVariableUnit, IVariableUnit, IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x0600089F RID: 2207 RVA: 0x0000FE21 File Offset: 0x0000E021
		public SetObjectVariable()
		{
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0000FE29 File Offset: 0x0000E029
		public SetObjectVariable(string name)
			: base(name)
		{
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x0000FE32 File Offset: 0x0000E032
		// (set) Token: 0x060008A2 RID: 2210 RVA: 0x0000FE3A File Offset: 0x0000E03A
		[DoNotSerialize]
		[PortLabelHidden]
		[NullMeansSelf]
		public ValueInput source { get; private set; }

		// Token: 0x060008A3 RID: 2211 RVA: 0x0000FE43 File Offset: 0x0000E043
		protected override void Definition()
		{
			this.source = base.ValueInput<GameObject>("source", null).NullMeansSelf();
			base.Definition();
			base.Requirement(this.source, base.assign);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0000FE74 File Offset: 0x0000E074
		protected override VariableDeclarations GetDeclarations(Flow flow)
		{
			return Variables.Object(flow.GetValue<GameObject>(this.source));
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0000FE87 File Offset: 0x0000E087
		FlowGraph IUnit.get_graph()
		{
			return base.graph;
		}
	}
}
