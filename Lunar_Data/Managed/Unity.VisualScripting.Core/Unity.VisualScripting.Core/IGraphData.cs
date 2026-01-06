using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000069 RID: 105
	public interface IGraphData
	{
		// Token: 0x06000379 RID: 889
		bool TryGetElementData(IGraphElementWithData element, out IGraphElementData data);

		// Token: 0x0600037A RID: 890
		bool TryGetChildGraphData(IGraphParentElement element, out IGraphData data);

		// Token: 0x0600037B RID: 891
		IGraphElementData CreateElementData(IGraphElementWithData element);

		// Token: 0x0600037C RID: 892
		void FreeElementData(IGraphElementWithData element);

		// Token: 0x0600037D RID: 893
		IGraphData CreateChildGraphData(IGraphParentElement element);

		// Token: 0x0600037E RID: 894
		void FreeChildGraphData(IGraphParentElement element);
	}
}
