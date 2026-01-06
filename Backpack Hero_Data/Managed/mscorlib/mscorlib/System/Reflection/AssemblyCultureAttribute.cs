using System;

namespace System.Reflection
{
	/// <summary>Specifies which culture the assembly supports.</summary>
	// Token: 0x02000882 RID: 2178
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyCultureAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyCultureAttribute" /> class with the culture supported by the assembly being attributed.</summary>
		/// <param name="culture">The culture supported by the attributed assembly. </param>
		// Token: 0x06004857 RID: 18519 RVA: 0x000EE01D File Offset: 0x000EC21D
		public AssemblyCultureAttribute(string culture)
		{
			this.Culture = culture;
		}

		/// <summary>Gets the supported culture of the attributed assembly.</summary>
		/// <returns>A string containing the name of the supported culture.</returns>
		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06004858 RID: 18520 RVA: 0x000EE02C File Offset: 0x000EC22C
		public string Culture { get; }
	}
}
