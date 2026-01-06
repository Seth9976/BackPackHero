using System;

namespace System.Security
{
	/// <summary>Indicates the set of security rules the common language runtime should enforce for an assembly.  </summary>
	// Token: 0x020003D8 RID: 984
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	public sealed class SecurityRulesAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityRulesAttribute" /> class using the specified rule set value. </summary>
		/// <param name="ruleSet">One of the enumeration values that specifies the transparency rules set. </param>
		// Token: 0x0600286C RID: 10348 RVA: 0x00092A4D File Offset: 0x00090C4D
		public SecurityRulesAttribute(SecurityRuleSet ruleSet)
		{
			this.m_ruleSet = ruleSet;
		}

		/// <summary>Determines whether fully trusted transparent code should skip Microsoft intermediate language (MSIL) verification.</summary>
		/// <returns>true if MSIL verification should be skipped; otherwise, false. The default is false.</returns>
		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x0600286D RID: 10349 RVA: 0x00092A5C File Offset: 0x00090C5C
		// (set) Token: 0x0600286E RID: 10350 RVA: 0x00092A64 File Offset: 0x00090C64
		public bool SkipVerificationInFullTrust
		{
			get
			{
				return this.m_skipVerificationInFullTrust;
			}
			set
			{
				this.m_skipVerificationInFullTrust = value;
			}
		}

		/// <summary>Gets the rule set to be applied.</summary>
		/// <returns>One of the enumeration values that specifies the transparency rules to be applied.</returns>
		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x0600286F RID: 10351 RVA: 0x00092A6D File Offset: 0x00090C6D
		public SecurityRuleSet RuleSet
		{
			get
			{
				return this.m_ruleSet;
			}
		}

		// Token: 0x04001EA6 RID: 7846
		private SecurityRuleSet m_ruleSet;

		// Token: 0x04001EA7 RID: 7847
		private bool m_skipVerificationInFullTrust;
	}
}
