using System;
using System.Globalization;

namespace Unity.Services.Analytics.Internal
{
	// Token: 0x02000022 RID: 34
	internal static class Locale
	{
		// Token: 0x0600006B RID: 107 RVA: 0x00003694 File Offset: 0x00001894
		internal static string CurrentLanguageCode()
		{
			return CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000036A0 File Offset: 0x000018A0
		public static string AnalyticsRegionLanguageCode()
		{
			return Locale.CurrentLanguageCode() + "_ZZ";
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000036B1 File Offset: 0x000018B1
		[Obsolete("The 'language-regionSettingsCountry' code used by Analytics is non-standard, so this method may throw exceptions when used on systems with non-ISO language/region combinations. Prefer using AnalyticsRegionLanguageCode instead.")]
		public static CultureInfo CurrentCulture()
		{
			return CultureInfo.CurrentCulture;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000036B8 File Offset: 0x000018B8
		[Obsolete("The 'language-regionSettingsCountry' code used by Analytics is non-standard, so this method may throw exceptions when used on systems with non-ISO language/region combinations. Prefer using AnalyticsRegionLanguageCode instead.")]
		public static CultureInfo SystemCulture()
		{
			return CultureInfo.InvariantCulture;
		}
	}
}
