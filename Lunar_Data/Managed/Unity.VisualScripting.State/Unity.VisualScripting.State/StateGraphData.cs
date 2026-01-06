using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000016 RID: 22
	public sealed class StateGraphData : GraphData<StateGraph>, IGraphEventListenerData, IGraphData
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002F24 File Offset: 0x00001124
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00002F2C File Offset: 0x0000112C
		public bool isListening { get; set; }

		// Token: 0x0600008A RID: 138 RVA: 0x00002F35 File Offset: 0x00001135
		public StateGraphData(StateGraph definition)
			: base(definition)
		{
		}
	}
}
