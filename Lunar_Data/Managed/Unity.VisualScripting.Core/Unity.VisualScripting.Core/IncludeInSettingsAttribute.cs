using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000034 RID: 52
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
	public sealed class IncludeInSettingsAttribute : Attribute
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x00004C9F File Offset: 0x00002E9F
		public IncludeInSettingsAttribute(bool include)
		{
			this.include = include;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00004CAE File Offset: 0x00002EAE
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x00004CB6 File Offset: 0x00002EB6
		public bool include { get; private set; }
	}
}
