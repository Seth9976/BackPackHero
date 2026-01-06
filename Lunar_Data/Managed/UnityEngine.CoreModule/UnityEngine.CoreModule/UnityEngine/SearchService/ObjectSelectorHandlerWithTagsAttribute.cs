using System;

namespace UnityEngine.SearchService
{
	// Token: 0x020002D3 RID: 723
	[Obsolete("ObjectSelectorHandlerWithTagsAttribute has been deprecated. Use SearchContextAttribute instead.")]
	[AttributeUsage(256)]
	public class ObjectSelectorHandlerWithTagsAttribute : Attribute
	{
		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x000309B3 File Offset: 0x0002EBB3
		public string[] tags { get; }

		// Token: 0x06001DE4 RID: 7652 RVA: 0x000309BB File Offset: 0x0002EBBB
		public ObjectSelectorHandlerWithTagsAttribute(params string[] tags)
		{
			this.tags = tags;
		}
	}
}
