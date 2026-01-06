using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000044 RID: 68
	[AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
	public sealed class TypeIconPriorityAttribute : Attribute
	{
		// Token: 0x060001E2 RID: 482 RVA: 0x00004EA7 File Offset: 0x000030A7
		public TypeIconPriorityAttribute(int priority)
		{
			this.priority = priority;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00004EB6 File Offset: 0x000030B6
		public TypeIconPriorityAttribute()
		{
			this.priority = 0;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00004EC5 File Offset: 0x000030C5
		public int priority { get; }
	}
}
