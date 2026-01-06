using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000124 RID: 292
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public sealed class RenamedNamespaceAttribute : Attribute
	{
		// Token: 0x060007A7 RID: 1959 RVA: 0x00022728 File Offset: 0x00020928
		public RenamedNamespaceAttribute(string previousName, string newName)
		{
			this.previousName = previousName;
			this.newName = newName;
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0002273E File Offset: 0x0002093E
		public string previousName { get; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x00022746 File Offset: 0x00020946
		public string newName { get; }
	}
}
