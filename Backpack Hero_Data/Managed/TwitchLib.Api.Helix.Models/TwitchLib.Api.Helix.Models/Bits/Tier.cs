using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Bits
{
	// Token: 0x020000D0 RID: 208
	public class Tier
	{
		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x00005C10 File Offset: 0x00003E10
		// (set) Token: 0x060006EC RID: 1772 RVA: 0x00005C18 File Offset: 0x00003E18
		[JsonProperty(PropertyName = "min_bits")]
		public int MinBits { get; protected set; }

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x00005C21 File Offset: 0x00003E21
		// (set) Token: 0x060006EE RID: 1774 RVA: 0x00005C29 File Offset: 0x00003E29
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x00005C32 File Offset: 0x00003E32
		// (set) Token: 0x060006F0 RID: 1776 RVA: 0x00005C3A File Offset: 0x00003E3A
		[JsonProperty(PropertyName = "color")]
		public string Color { get; protected set; }

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x00005C43 File Offset: 0x00003E43
		// (set) Token: 0x060006F2 RID: 1778 RVA: 0x00005C4B File Offset: 0x00003E4B
		[JsonProperty(PropertyName = "images")]
		public Images Images { get; protected set; }

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x00005C54 File Offset: 0x00003E54
		// (set) Token: 0x060006F4 RID: 1780 RVA: 0x00005C5C File Offset: 0x00003E5C
		[JsonProperty(PropertyName = "can_cheer")]
		public bool CanCheer { get; protected set; }

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x00005C65 File Offset: 0x00003E65
		// (set) Token: 0x060006F6 RID: 1782 RVA: 0x00005C6D File Offset: 0x00003E6D
		[JsonProperty(PropertyName = "show_in_bits_card")]
		public bool ShowInBitsCard { get; protected set; }
	}
}
