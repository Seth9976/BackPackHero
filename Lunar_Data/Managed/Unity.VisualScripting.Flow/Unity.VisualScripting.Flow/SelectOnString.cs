using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000040 RID: 64
	[UnitCategory("Control")]
	[UnitTitle("Select On String")]
	[UnitShortTitle("Select")]
	[UnitSubtitle("On String")]
	[UnitOrder(7)]
	public class SelectOnString : SelectUnit<string>
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000275 RID: 629 RVA: 0x000072DE File Offset: 0x000054DE
		// (set) Token: 0x06000276 RID: 630 RVA: 0x000072E6 File Offset: 0x000054E6
		[Serialize]
		[Inspectable]
		[UnitHeaderInspectable("Ignore Case")]
		[InspectorToggleLeft]
		public bool ignoreCase { get; set; }

		// Token: 0x06000277 RID: 631 RVA: 0x000072EF File Offset: 0x000054EF
		protected override bool Matches(string a, string b)
		{
			return (string.IsNullOrEmpty(a) && string.IsNullOrEmpty(b)) || string.Equals(a, b, this.ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
		}
	}
}
