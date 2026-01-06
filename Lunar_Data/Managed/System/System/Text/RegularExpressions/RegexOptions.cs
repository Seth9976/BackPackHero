using System;

namespace System.Text.RegularExpressions
{
	/// <summary>Provides enumerated values to use to set regular expression options.</summary>
	// Token: 0x02000208 RID: 520
	[Flags]
	public enum RegexOptions
	{
		/// <summary>Specifies that no options are set.</summary>
		// Token: 0x04000937 RID: 2359
		None = 0,
		/// <summary>Specifies case-insensitive matching.</summary>
		// Token: 0x04000938 RID: 2360
		IgnoreCase = 1,
		/// <summary>Multiline mode. Changes the meaning of ^ and $ so they match at the beginning and end, respectively, of any line, and not just the beginning and end of the entire string.</summary>
		// Token: 0x04000939 RID: 2361
		Multiline = 2,
		/// <summary>Specifies that the only valid captures are explicitly named or numbered groups of the form (?&lt;name&gt;…). This allows unnamed parentheses to act as noncapturing groups without the syntactic clumsiness of the expression (?:…).</summary>
		// Token: 0x0400093A RID: 2362
		ExplicitCapture = 4,
		/// <summary>Specifies that the regular expression is compiled to an assembly. This yields faster execution but increases startup time. This value should not be assigned to the <see cref="P:System.Text.RegularExpressions.RegexCompilationInfo.Options" /> property when calling the <see cref="M:System.Text.RegularExpressions.Regex.CompileToAssembly(System.Text.RegularExpressions.RegexCompilationInfo[],System.Reflection.AssemblyName)" /> method. </summary>
		// Token: 0x0400093B RID: 2363
		Compiled = 8,
		/// <summary>Specifies single-line mode. Changes the meaning of the dot (.) so it matches every character (instead of every character except \n).</summary>
		// Token: 0x0400093C RID: 2364
		Singleline = 16,
		/// <summary>Eliminates unescaped white space from the pattern and enables comments marked with #. However, the <see cref="F:System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace" /> value does not affect or eliminate white space in character classes. </summary>
		// Token: 0x0400093D RID: 2365
		IgnorePatternWhitespace = 32,
		/// <summary>Specifies that the search will be from right to left instead of from left to right.</summary>
		// Token: 0x0400093E RID: 2366
		RightToLeft = 64,
		/// <summary>Enables ECMAScript-compliant behavior for the expression. This value can be used only in conjunction with the <see cref="F:System.Text.RegularExpressions.RegexOptions.IgnoreCase" />, <see cref="F:System.Text.RegularExpressions.RegexOptions.Multiline" />, and <see cref="F:System.Text.RegularExpressions.RegexOptions.Compiled" /> values. The use of this value with any other values results in an exception.</summary>
		// Token: 0x0400093F RID: 2367
		ECMAScript = 256,
		/// <summary>Specifies that cultural differences in language is ignored. See Regular Expression Options for more information.</summary>
		// Token: 0x04000940 RID: 2368
		CultureInvariant = 512
	}
}
