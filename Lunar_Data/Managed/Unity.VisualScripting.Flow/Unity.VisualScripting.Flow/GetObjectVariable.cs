using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200013C RID: 316
	[UnitSurtitle("Object")]
	public sealed class GetObjectVariable : GetVariableUnit, IObjectVariableUnit, IVariableUnit, IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x06000863 RID: 2147 RVA: 0x0000FA88 File Offset: 0x0000DC88
		public GetObjectVariable()
		{
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public GetObjectVariable(string name)
			: base(name)
		{
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x0000FA99 File Offset: 0x0000DC99
		// (set) Token: 0x06000866 RID: 2150 RVA: 0x0000FAA1 File Offset: 0x0000DCA1
		[DoNotSerialize]
		[PortLabelHidden]
		[NullMeansSelf]
		public ValueInput source { get; private set; }

		// Token: 0x06000867 RID: 2151 RVA: 0x0000FAAA File Offset: 0x0000DCAA
		protected override void Definition()
		{
			this.source = base.ValueInput<GameObject>("source", null).NullMeansSelf();
			base.Definition();
			base.Requirement(this.source, base.value);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0000FADB File Offset: 0x0000DCDB
		protected override VariableDeclarations GetDeclarations(Flow flow)
		{
			return Variables.Object(flow.GetValue<GameObject>(this.source));
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0000FAEE File Offset: 0x0000DCEE
		FlowGraph IUnit.get_graph()
		{
			return base.graph;
		}
	}
}
