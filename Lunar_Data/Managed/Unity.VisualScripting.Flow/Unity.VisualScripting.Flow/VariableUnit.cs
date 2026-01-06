using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000152 RID: 338
	[SpecialUnit]
	[Obsolete("Use the new unified variable nodes instead.")]
	public abstract class VariableUnit : Unit, IVariableUnit, IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x060008BA RID: 2234 RVA: 0x00010048 File Offset: 0x0000E248
		protected VariableUnit()
		{
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0001005B File Offset: 0x0000E25B
		protected VariableUnit(string defaultName)
		{
			Ensure.That("defaultName").IsNotNull(defaultName);
			this.defaultName = defaultName;
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x00010085 File Offset: 0x0000E285
		[DoNotSerialize]
		public string defaultName { get; } = string.Empty;

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x0001008D File Offset: 0x0000E28D
		// (set) Token: 0x060008BE RID: 2238 RVA: 0x00010095 File Offset: 0x0000E295
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput name { get; private set; }

		// Token: 0x060008BF RID: 2239
		protected abstract VariableDeclarations GetDeclarations(Flow flow);

		// Token: 0x060008C0 RID: 2240 RVA: 0x0001009E File Offset: 0x0000E29E
		protected override void Definition()
		{
			this.name = base.ValueInput<string>("name", this.defaultName);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x000100B7 File Offset: 0x0000E2B7
		FlowGraph IUnit.get_graph()
		{
			return base.graph;
		}
	}
}
