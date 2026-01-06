using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000012 RID: 18
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class UnitShortTitleAttribute : Attribute
	{
		// Token: 0x06000063 RID: 99 RVA: 0x0000285D File Offset: 0x00000A5D
		public UnitShortTitleAttribute(string title)
		{
			this.title = title;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000064 RID: 100 RVA: 0x0000286C File Offset: 0x00000A6C
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002874 File Offset: 0x00000A74
		public string title { get; private set; }
	}
}
