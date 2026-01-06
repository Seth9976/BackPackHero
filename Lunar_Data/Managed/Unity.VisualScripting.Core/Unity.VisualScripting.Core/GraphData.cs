using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200005C RID: 92
	public class GraphData<TGraph> : IGraphData where TGraph : class, IGraph
	{
		// Token: 0x060002A3 RID: 675 RVA: 0x000069B4 File Offset: 0x00004BB4
		public GraphData(TGraph definition)
		{
			this.definition = definition;
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x000069EF File Offset: 0x00004BEF
		protected TGraph definition { get; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x000069F7 File Offset: 0x00004BF7
		protected Dictionary<IGraphElementWithData, IGraphElementData> elementsData { get; } = new Dictionary<IGraphElementWithData, IGraphElementData>();

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x000069FF File Offset: 0x00004BFF
		protected Dictionary<IGraphParentElement, IGraphData> childrenGraphsData { get; } = new Dictionary<IGraphParentElement, IGraphData>();

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x00006A07 File Offset: 0x00004C07
		protected Dictionary<Guid, IGraphElementData> phantomElementsData { get; } = new Dictionary<Guid, IGraphElementData>();

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00006A0F File Offset: 0x00004C0F
		protected Dictionary<Guid, IGraphData> phantomChildrenGraphsData { get; } = new Dictionary<Guid, IGraphData>();

		// Token: 0x060002A9 RID: 681 RVA: 0x00006A17 File Offset: 0x00004C17
		public bool TryGetElementData(IGraphElementWithData element, out IGraphElementData data)
		{
			return this.elementsData.TryGetValue(element, out data);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00006A26 File Offset: 0x00004C26
		public bool TryGetChildGraphData(IGraphParentElement element, out IGraphData data)
		{
			return this.childrenGraphsData.TryGetValue(element, out data);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00006A38 File Offset: 0x00004C38
		public IGraphElementData CreateElementData(IGraphElementWithData element)
		{
			if (this.elementsData.ContainsKey(element))
			{
				throw new InvalidOperationException(string.Format("Graph data already contains element data for {0}.", element));
			}
			IGraphElementData graphElementData;
			if (this.phantomElementsData.TryGetValue(element.guid, out graphElementData))
			{
				this.phantomElementsData.Remove(element.guid);
			}
			else
			{
				graphElementData = element.CreateData();
			}
			this.elementsData.Add(element, graphElementData);
			return graphElementData;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00006AA4 File Offset: 0x00004CA4
		public void FreeElementData(IGraphElementWithData element)
		{
			IGraphElementData graphElementData;
			if (this.elementsData.TryGetValue(element, out graphElementData))
			{
				this.elementsData.Remove(element);
				this.phantomElementsData.Add(element.guid, graphElementData);
				return;
			}
			Debug.LogWarning(string.Format("Graph data does not contain element data to free for {0}.", element));
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00006AF4 File Offset: 0x00004CF4
		public IGraphData CreateChildGraphData(IGraphParentElement element)
		{
			if (this.childrenGraphsData.ContainsKey(element))
			{
				throw new InvalidOperationException(string.Format("Graph data already contains child graph data for {0}.", element));
			}
			IGraphData graphData;
			if (this.phantomChildrenGraphsData.TryGetValue(element.guid, out graphData))
			{
				this.phantomChildrenGraphsData.Remove(element.guid);
			}
			else
			{
				graphData = element.childGraph.CreateData();
			}
			this.childrenGraphsData.Add(element, graphData);
			return graphData;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00006B64 File Offset: 0x00004D64
		public void FreeChildGraphData(IGraphParentElement element)
		{
			IGraphData graphData;
			if (this.childrenGraphsData.TryGetValue(element, out graphData))
			{
				this.childrenGraphsData.Remove(element);
				this.phantomChildrenGraphsData.Add(element.guid, graphData);
				return;
			}
			Debug.LogWarning(string.Format("Graph data does not contain child graph data to free for {0}.", element));
		}
	}
}
