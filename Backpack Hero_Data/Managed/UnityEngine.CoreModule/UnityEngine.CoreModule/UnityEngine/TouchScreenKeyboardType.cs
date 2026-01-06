using System;

namespace UnityEngine
{
	// Token: 0x02000253 RID: 595
	public enum TouchScreenKeyboardType
	{
		// Token: 0x0400087F RID: 2175
		Default,
		// Token: 0x04000880 RID: 2176
		ASCIICapable,
		// Token: 0x04000881 RID: 2177
		NumbersAndPunctuation,
		// Token: 0x04000882 RID: 2178
		URL,
		// Token: 0x04000883 RID: 2179
		NumberPad,
		// Token: 0x04000884 RID: 2180
		PhonePad,
		// Token: 0x04000885 RID: 2181
		NamePhonePad,
		// Token: 0x04000886 RID: 2182
		EmailAddress,
		// Token: 0x04000887 RID: 2183
		[Obsolete("Wii U is no longer supported as of Unity 2018.1.")]
		NintendoNetworkAccount,
		// Token: 0x04000888 RID: 2184
		Social,
		// Token: 0x04000889 RID: 2185
		Search,
		// Token: 0x0400088A RID: 2186
		DecimalPad,
		// Token: 0x0400088B RID: 2187
		OneTimeCode
	}
}
