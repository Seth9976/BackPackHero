using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000035 RID: 53
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class InspectableAttribute : Attribute, IInspectableAttribute
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00004CC7 File Offset: 0x00002EC7
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00004CCF File Offset: 0x00002ECF
		public int order { get; set; }
	}
}
