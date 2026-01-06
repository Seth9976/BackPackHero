using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000147 RID: 327
	[UnitSurtitle("Object")]
	public sealed class IsObjectVariableDefined : IsVariableDefinedUnit, IObjectVariableUnit, IVariableUnit, IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x06000881 RID: 2177 RVA: 0x0000FC7D File Offset: 0x0000DE7D
		public IsObjectVariableDefined()
		{
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0000FC85 File Offset: 0x0000DE85
		public IsObjectVariableDefined(string name)
			: base(name)
		{
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x0000FC8E File Offset: 0x0000DE8E
		// (set) Token: 0x06000884 RID: 2180 RVA: 0x0000FC96 File Offset: 0x0000DE96
		[DoNotSerialize]
		[PortLabelHidden]
		[NullMeansSelf]
		public ValueInput source { get; private set; }

		// Token: 0x06000885 RID: 2181 RVA: 0x0000FC9F File Offset: 0x0000DE9F
		protected override void Definition()
		{
			this.source = base.ValueInput<GameObject>("source", null).NullMeansSelf();
			base.Definition();
			base.Requirement(this.source, base.isDefined);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0000FCD0 File Offset: 0x0000DED0
		protected override VariableDeclarations GetDeclarations(Flow flow)
		{
			return Variables.Object(flow.GetValue<GameObject>(this.source));
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0000FCE3 File Offset: 0x0000DEE3
		FlowGraph IUnit.get_graph()
		{
			return base.graph;
		}
	}
}
