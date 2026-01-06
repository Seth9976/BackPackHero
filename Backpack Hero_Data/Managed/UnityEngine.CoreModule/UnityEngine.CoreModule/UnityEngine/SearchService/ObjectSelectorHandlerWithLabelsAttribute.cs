using System;

namespace UnityEngine.SearchService
{
	// Token: 0x020002D2 RID: 722
	[Obsolete("ObjectSelectorHandlerWithLabelsAttribute has been deprecated. Use SearchContextAttribute instead.")]
	[AttributeUsage(256)]
	public class ObjectSelectorHandlerWithLabelsAttribute : Attribute
	{
		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001DDF RID: 7647 RVA: 0x00030973 File Offset: 0x0002EB73
		public string[] labels { get; }

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001DE0 RID: 7648 RVA: 0x0003097B File Offset: 0x0002EB7B
		public bool matchAll { get; }

		// Token: 0x06001DE1 RID: 7649 RVA: 0x00030983 File Offset: 0x0002EB83
		public ObjectSelectorHandlerWithLabelsAttribute(params string[] labels)
		{
			this.labels = labels;
			this.matchAll = true;
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x0003099B File Offset: 0x0002EB9B
		public ObjectSelectorHandlerWithLabelsAttribute(bool matchAll, params string[] labels)
		{
			this.labels = labels;
			this.matchAll = matchAll;
		}
	}
}
