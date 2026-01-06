using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.AutomodSettings
{
	// Token: 0x0200006B RID: 107
	public class AutomodSettingsResponseModel
	{
		// Token: 0x0400018C RID: 396
		[JsonProperty(PropertyName = "broadcaster_id")]
		public string BroadcasterId;

		// Token: 0x0400018D RID: 397
		[JsonProperty(PropertyName = "moderator_id")]
		public string ModeratorId;

		// Token: 0x0400018E RID: 398
		[JsonProperty(PropertyName = "overall_level")]
		public int? OverallLevel;

		// Token: 0x0400018F RID: 399
		[JsonProperty(PropertyName = "disability")]
		public int? Disability;

		// Token: 0x04000190 RID: 400
		[JsonProperty(PropertyName = "aggression")]
		public int? Aggression;

		// Token: 0x04000191 RID: 401
		[JsonProperty(PropertyName = "sexuality_sex_or_gender")]
		public int? SexualitySexOrGender;

		// Token: 0x04000192 RID: 402
		[JsonProperty(PropertyName = "misogyny")]
		public int? Misogyny;

		// Token: 0x04000193 RID: 403
		[JsonProperty(PropertyName = "bullying")]
		public int? Bullying;

		// Token: 0x04000194 RID: 404
		[JsonProperty(PropertyName = "swearing")]
		public int? Swearing;

		// Token: 0x04000195 RID: 405
		[JsonProperty(PropertyName = "race_ethnicity_or_religion")]
		public int? RaceEthnicityOrReligion;

		// Token: 0x04000196 RID: 406
		[JsonProperty(PropertyName = "sex_based_terms")]
		public int? SexBasedTerms;
	}
}
