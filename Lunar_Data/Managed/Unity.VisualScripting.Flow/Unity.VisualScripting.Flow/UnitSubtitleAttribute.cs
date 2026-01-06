using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000013 RID: 19
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class UnitSubtitleAttribute : Attribute
	{
		// Token: 0x06000066 RID: 102 RVA: 0x0000287D File Offset: 0x00000A7D
		public UnitSubtitleAttribute(string subtitle)
		{
			this.subtitle = subtitle;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000067 RID: 103 RVA: 0x0000288C File Offset: 0x00000A8C
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002894 File Offset: 0x00000A94
		public string subtitle { get; private set; }
	}
}
