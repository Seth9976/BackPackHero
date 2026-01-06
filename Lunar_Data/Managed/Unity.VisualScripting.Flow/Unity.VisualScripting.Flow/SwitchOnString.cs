using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000046 RID: 70
	[UnitCategory("Control")]
	[UnitTitle("Switch On String")]
	[UnitShortTitle("Switch")]
	[UnitSubtitle("On String")]
	[UnitOrder(4)]
	public class SwitchOnString : SwitchUnit<string>
	{
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060002AE RID: 686 RVA: 0x00007979 File Offset: 0x00005B79
		// (set) Token: 0x060002AF RID: 687 RVA: 0x00007981 File Offset: 0x00005B81
		[Serialize]
		[Inspectable]
		[UnitHeaderInspectable("Ignore Case")]
		[InspectorToggleLeft]
		public bool ignoreCase { get; set; }

		// Token: 0x060002B0 RID: 688 RVA: 0x0000798A File Offset: 0x00005B8A
		protected override bool Matches(string a, string b)
		{
			return (string.IsNullOrEmpty(a) && string.IsNullOrEmpty(b)) || string.Equals(a, b, this.ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
		}
	}
}
