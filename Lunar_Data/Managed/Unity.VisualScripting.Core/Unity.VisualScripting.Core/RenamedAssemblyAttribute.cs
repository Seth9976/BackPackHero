using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000122 RID: 290
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public sealed class RenamedAssemblyAttribute : Attribute
	{
		// Token: 0x060007A2 RID: 1954 RVA: 0x000226EB File Offset: 0x000208EB
		public RenamedAssemblyAttribute(string previousName, string newName)
		{
			this.previousName = previousName;
			this.newName = newName;
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x00022701 File Offset: 0x00020901
		public string previousName { get; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x00022709 File Offset: 0x00020909
		public string newName { get; }
	}
}
