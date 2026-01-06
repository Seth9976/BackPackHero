using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.AutomodSettings
{
	// Token: 0x0200006A RID: 106
	public class AutomodSettings
	{
		// Token: 0x04000183 RID: 387
		[JsonProperty(PropertyName = "overall_level")]
		public int? OverallLevel;

		// Token: 0x04000184 RID: 388
		[JsonProperty(PropertyName = "disability")]
		public int? Disability;

		// Token: 0x04000185 RID: 389
		[JsonProperty(PropertyName = "aggression")]
		public int? Aggression;

		// Token: 0x04000186 RID: 390
		[JsonProperty(PropertyName = "sexuality_sex_or_gender")]
		public int? SexualitySexOrGender;

		// Token: 0x04000187 RID: 391
		[JsonProperty(PropertyName = "misogyny")]
		public int? Misogyny;

		// Token: 0x04000188 RID: 392
		[JsonProperty(PropertyName = "bullying")]
		public int? Bullying;

		// Token: 0x04000189 RID: 393
		[JsonProperty(PropertyName = "swearing")]
		public int? Swearing;

		// Token: 0x0400018A RID: 394
		[JsonProperty(PropertyName = "race_ethnicity_or_religion")]
		public int? RaceEthnicityOrReligion;

		// Token: 0x0400018B RID: 395
		[JsonProperty(PropertyName = "sex_based_terms")]
		public int? SexBasedTerms;
	}
}
