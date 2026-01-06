using System;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x0200005D RID: 93
	public class GraphDebugData : IGraphDebugData
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00006BB1 File Offset: 0x00004DB1
		protected Dictionary<IGraphElementWithDebugData, IGraphElementDebugData> elementsData { get; } = new Dictionary<IGraphElementWithDebugData, IGraphElementDebugData>();

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00006BB9 File Offset: 0x00004DB9
		protected Dictionary<IGraphParentElement, IGraphDebugData> childrenGraphsData { get; } = new Dictionary<IGraphParentElement, IGraphDebugData>();

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00006BC1 File Offset: 0x00004DC1
		IEnumerable<IGraphElementDebugData> IGraphDebugData.elementsData
		{
			get
			{
				return this.elementsData.Values;
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00006BCE File Offset: 0x00004DCE
		public GraphDebugData(IGraph definition)
		{
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00006BEC File Offset: 0x00004DEC
		public IGraphElementDebugData GetOrCreateElementData(IGraphElementWithDebugData element)
		{
			IGraphElementDebugData graphElementDebugData;
			if (!this.elementsData.TryGetValue(element, out graphElementDebugData))
			{
				graphElementDebugData = element.CreateDebugData();
				this.elementsData.Add(element, graphElementDebugData);
			}
			return graphElementDebugData;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00006C20 File Offset: 0x00004E20
		public IGraphDebugData GetOrCreateChildGraphData(IGraphParentElement element)
		{
			IGraphDebugData graphDebugData;
			if (!this.childrenGraphsData.TryGetValue(element, out graphDebugData))
			{
				graphDebugData = new GraphDebugData(element.childGraph);
				this.childrenGraphsData.Add(element, graphDebugData);
			}
			return graphDebugData;
		}
	}
}
