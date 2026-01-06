using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200004B RID: 75
	public static class ExceptionMessages
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00005CB6 File Offset: 0x00003EB6
		public static string Common_IsNull_Failed { get; } = "Value must be null.";

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00005CBD File Offset: 0x00003EBD
		public static string Common_IsNotNull_Failed { get; } = "Value cannot be null.";

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00005CC4 File Offset: 0x00003EC4
		public static string Booleans_IsTrueFailed { get; } = "Expected an expression that evaluates to true.";

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00005CCB File Offset: 0x00003ECB
		public static string Booleans_IsFalseFailed { get; } = "Expected an expression that evaluates to false.";

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00005CD2 File Offset: 0x00003ED2
		public static string Collections_Any_Failed { get; } = "The predicate did not match any elements.";

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00005CD9 File Offset: 0x00003ED9
		public static string Collections_ContainsKey_Failed { get; } = "{1} '{0}' was not found.";

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00005CE0 File Offset: 0x00003EE0
		public static string Collections_HasItemsFailed { get; } = "Empty collection is not allowed.";

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00005CE7 File Offset: 0x00003EE7
		public static string Collections_HasNoNullItemFailed { get; } = "Collection with null items is not allowed.";

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00005CEE File Offset: 0x00003EEE
		public static string Collections_SizeIs_Failed { get; } = "Expected size '{0}' but found '{1}'.";

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00005CF5 File Offset: 0x00003EF5
		public static string Comp_Is_Failed { get; } = "Value '{0}' is not '{1}'.";

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00005CFC File Offset: 0x00003EFC
		public static string Comp_IsNot_Failed { get; } = "Value '{0}' is '{1}', which was not expected.";

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00005D03 File Offset: 0x00003F03
		public static string Comp_IsNotLt { get; } = "Value '{0}' is not lower than limit '{1}'.";

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00005D0A File Offset: 0x00003F0A
		public static string Comp_IsNotLte { get; } = "Value '{0}' is not lower than or equal to limit '{1}'.";

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00005D11 File Offset: 0x00003F11
		public static string Comp_IsNotGt { get; } = "Value '{0}' is not greater than limit '{1}'.";

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00005D18 File Offset: 0x00003F18
		public static string Comp_IsNotGte { get; } = "Value '{0}' is not greater than or equal to limit '{1}'.";

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00005D1F File Offset: 0x00003F1F
		public static string Comp_IsNotInRange_ToLow { get; } = "Value '{0}' is < min '{1}'.";

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00005D26 File Offset: 0x00003F26
		public static string Comp_IsNotInRange_ToHigh { get; } = "Value '{0}' is > max '{1}'.";

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00005D2D File Offset: 0x00003F2D
		public static string Guids_IsNotEmpty_Failed { get; } = "An empty GUID is not allowed.";

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00005D34 File Offset: 0x00003F34
		public static string Strings_IsEqualTo_Failed { get; } = "Value '{0}' is not '{1}'.";

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00005D3B File Offset: 0x00003F3B
		public static string Strings_IsNotEqualTo_Failed { get; } = "Value '{0}' is '{1}', which was not expected.";

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00005D42 File Offset: 0x00003F42
		public static string Strings_SizeIs_Failed { get; } = "Expected length '{0}' but got '{1}'.";

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00005D49 File Offset: 0x00003F49
		public static string Strings_IsNotNullOrWhiteSpace_Failed { get; } = "The string can't be left empty, null or consist of only whitespaces.";

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00005D50 File Offset: 0x00003F50
		public static string Strings_IsNotNullOrEmpty_Failed { get; } = "The string can't be null or empty.";

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00005D57 File Offset: 0x00003F57
		public static string Strings_HasLengthBetween_Failed_ToShort { get; } = "The string is not long enough. Must be between '{0}' and '{1}' but was '{2}' characters long.";

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00005D5E File Offset: 0x00003F5E
		public static string Strings_HasLengthBetween_Failed_ToLong { get; } = "The string is too long. Must be between '{0}' and  '{1}'. Must be between '{0}' and '{1}' but was '{2}' characters long.";

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00005D65 File Offset: 0x00003F65
		public static string Strings_Matches_Failed { get; } = "Value '{0}' does not match '{1}'";

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00005D6C File Offset: 0x00003F6C
		public static string Strings_IsNotEmpty_Failed { get; } = "Empty String is not allowed.";

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00005D73 File Offset: 0x00003F73
		public static string Strings_IsGuid_Failed { get; } = "Value '{0}' is not a valid GUID.";

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00005D7A File Offset: 0x00003F7A
		public static string Types_IsOfType_Failed { get; } = "Expected a '{0}' but got '{1}'.";

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00005D81 File Offset: 0x00003F81
		public static string Reflection_HasAttribute_Failed { get; } = "Type '{0}' does not define the [{1}] attribute.";

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00005D88 File Offset: 0x00003F88
		public static string Reflection_HasConstructor_Failed { get; } = "Type '{0}' does not provide a constructor accepting ({1}).";

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00005D8F File Offset: 0x00003F8F
		public static string Reflection_HasPublicConstructor_Failed { get; } = "Type '{0}' does not provide a public constructor accepting ({1}).";

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00005D96 File Offset: 0x00003F96
		public static string ValueTypes_IsNotDefault_Failed { get; } = "The param was expected to not be of default value.";
	}
}
