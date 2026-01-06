using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000123 RID: 291
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	public sealed class RenamedFromAttribute : Attribute
	{
		// Token: 0x060007A5 RID: 1957 RVA: 0x00022711 File Offset: 0x00020911
		public RenamedFromAttribute(string previousName)
		{
			this.previousName = previousName;
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x00022720 File Offset: 0x00020920
		public string previousName { get; }
	}
}
