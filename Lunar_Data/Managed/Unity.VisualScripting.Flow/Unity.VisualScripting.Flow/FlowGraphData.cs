using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000018 RID: 24
	public sealed class FlowGraphData : GraphData<FlowGraph>, IGraphDataWithVariables, IGraphData, IGraphEventListenerData
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000039B0 File Offset: 0x00001BB0
		public VariableDeclarations variables { get; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000CA RID: 202 RVA: 0x000039B8 File Offset: 0x00001BB8
		// (set) Token: 0x060000CB RID: 203 RVA: 0x000039C0 File Offset: 0x00001BC0
		public bool isListening { get; set; }

		// Token: 0x060000CC RID: 204 RVA: 0x000039C9 File Offset: 0x00001BC9
		public FlowGraphData(FlowGraph definition)
			: base(definition)
		{
			this.variables = definition.variables.CloneViaFakeSerialization<VariableDeclarations>();
		}
	}
}
