using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000AB RID: 171
	[UnitCategory("Graphs/Graph Nodes")]
	public abstract class GetGraphs<TGraph, TGraphAsset, TMachine> : Unit where TGraph : class, IGraph, new() where TGraphAsset : Macro<TGraph> where TMachine : Machine<TGraph, TGraphAsset>
	{
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x0000A937 File Offset: 0x00008B37
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x0000A93F File Offset: 0x00008B3F
		[DoNotSerialize]
		[PortLabelHidden]
		[NullMeansSelf]
		public ValueInput gameObject { get; protected set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x0000A948 File Offset: 0x00008B48
		// (set) Token: 0x06000502 RID: 1282 RVA: 0x0000A950 File Offset: 0x00008B50
		[DoNotSerialize]
		[PortLabel("Graphs")]
		[PortLabelHidden]
		public ValueOutput graphList { get; protected set; }

		// Token: 0x06000503 RID: 1283 RVA: 0x0000A959 File Offset: 0x00008B59
		protected override void Definition()
		{
			this.gameObject = base.ValueInput<GameObject>("gameObject", null).NullMeansSelf();
			this.graphList = base.ValueOutput<List<TGraphAsset>>("graphList", new Func<Flow, List<TGraphAsset>>(this.Get));
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000A990 File Offset: 0x00008B90
		private List<TGraphAsset> Get(Flow flow)
		{
			GameObject go = flow.GetValue<GameObject>(this.gameObject);
			return (from machine in go.GetComponents<TMachine>()
				where go.GetComponent<TMachine>().nest.macro != null
				select machine.nest.macro).ToList<TGraphAsset>();
		}
	}
}
