using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000015 RID: 21
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class UnitTitleAttribute : Attribute
	{
		// Token: 0x0600006C RID: 108 RVA: 0x000028BD File Offset: 0x00000ABD
		public UnitTitleAttribute(string title)
		{
			this.title = title;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000028CC File Offset: 0x00000ACC
		// (set) Token: 0x0600006E RID: 110 RVA: 0x000028D4 File Offset: 0x00000AD4
		public string title { get; private set; }
	}
}
