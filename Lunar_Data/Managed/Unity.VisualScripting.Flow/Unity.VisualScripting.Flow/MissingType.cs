using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000127 RID: 295
	[SpecialUnit]
	[UnitTitle("Node script is missing!")]
	[UnitShortTitle("Missing Script!")]
	public sealed class MissingType : Unit
	{
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x0000E370 File Offset: 0x0000C570
		// (set) Token: 0x060007AA RID: 1962 RVA: 0x0000E378 File Offset: 0x0000C578
		[Serialize]
		public string formerType { get; private set; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x0000E381 File Offset: 0x0000C581
		// (set) Token: 0x060007AC RID: 1964 RVA: 0x0000E389 File Offset: 0x0000C589
		[Serialize]
		public string formerValue { get; private set; }

		// Token: 0x060007AD RID: 1965 RVA: 0x0000E392 File Offset: 0x0000C592
		protected override void Definition()
		{
		}
	}
}
