using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000155 RID: 341
	[SpecialUnit]
	public abstract class UnifiedVariableUnit : Unit, IUnifiedVariableUnit, IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x00010343 File Offset: 0x0000E543
		// (set) Token: 0x060008D5 RID: 2261 RVA: 0x0001034B File Offset: 0x0000E54B
		[Serialize]
		[Inspectable]
		[UnitHeaderInspectable]
		public VariableKind kind { get; set; }

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x00010354 File Offset: 0x0000E554
		// (set) Token: 0x060008D7 RID: 2263 RVA: 0x0001035C File Offset: 0x0000E55C
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput name { get; private set; }

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x00010365 File Offset: 0x0000E565
		// (set) Token: 0x060008D9 RID: 2265 RVA: 0x0001036D File Offset: 0x0000E56D
		[DoNotSerialize]
		[PortLabelHidden]
		[NullMeansSelf]
		public ValueInput @object { get; private set; }

		// Token: 0x060008DA RID: 2266 RVA: 0x00010376 File Offset: 0x0000E576
		protected override void Definition()
		{
			this.name = base.ValueInput<string>("name", string.Empty);
			if (this.kind == VariableKind.Object)
			{
				this.@object = base.ValueInput<GameObject>("object", null).NullMeansSelf();
			}
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x000103B6 File Offset: 0x0000E5B6
		FlowGraph IUnit.get_graph()
		{
			return base.graph;
		}
	}
}
