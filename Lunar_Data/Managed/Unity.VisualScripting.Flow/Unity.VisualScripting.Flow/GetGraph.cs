using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000AA RID: 170
	[UnitCategory("Graphs/Graph Nodes")]
	public abstract class GetGraph<TGraph, TGraphAsset, TMachine> : Unit where TGraph : class, IGraph, new() where TGraphAsset : Macro<TGraph> where TMachine : Machine<TGraph, TGraphAsset>
	{
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0000A8B5 File Offset: 0x00008AB5
		// (set) Token: 0x060004F9 RID: 1273 RVA: 0x0000A8BD File Offset: 0x00008ABD
		[DoNotSerialize]
		[PortLabelHidden]
		[NullMeansSelf]
		public ValueInput gameObject { get; protected set; }

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0000A8C6 File Offset: 0x00008AC6
		// (set) Token: 0x060004FB RID: 1275 RVA: 0x0000A8CE File Offset: 0x00008ACE
		[DoNotSerialize]
		[PortLabel("Graph")]
		[PortLabelHidden]
		public ValueOutput graphOutput { get; protected set; }

		// Token: 0x060004FC RID: 1276 RVA: 0x0000A8D7 File Offset: 0x00008AD7
		protected override void Definition()
		{
			this.gameObject = base.ValueInput<GameObject>("gameObject", null).NullMeansSelf();
			this.graphOutput = base.ValueOutput<TGraphAsset>("graphOutput", new Func<Flow, TGraphAsset>(this.Get));
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000A90D File Offset: 0x00008B0D
		private TGraphAsset Get(Flow flow)
		{
			return flow.GetValue<GameObject>(this.gameObject).GetComponent<TMachine>().nest.macro;
		}
	}
}
