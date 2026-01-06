using System;

namespace System.Diagnostics.CodeAnalysis
{
	/// <summary>Suppresses reporting of a specific static analysis tool rule violation, allowing multiple suppressions on a single code artifact.</summary>
	// Token: 0x02000A02 RID: 2562
	[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
	[Conditional("CODE_ANALYSIS")]
	public sealed class SuppressMessageAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CodeAnalysis.SuppressMessageAttribute" /> class, specifying the category of the static analysis tool and the identifier for an analysis rule. </summary>
		/// <param name="category">The category for the attribute.</param>
		/// <param name="checkId">The identifier of the analysis tool rule the attribute applies to.</param>
		// Token: 0x06005B4E RID: 23374 RVA: 0x00134911 File Offset: 0x00132B11
		public SuppressMessageAttribute(string category, string checkId)
		{
			this.Category = category;
			this.CheckId = checkId;
		}

		/// <summary>Gets the category identifying the classification of the attribute.</summary>
		/// <returns>The category identifying the attribute.</returns>
		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x06005B4F RID: 23375 RVA: 0x00134927 File Offset: 0x00132B27
		public string Category { get; }

		/// <summary>Gets the identifier of the static analysis tool rule to be suppressed.</summary>
		/// <returns>The identifier of the static analysis tool rule to be suppressed.</returns>
		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x06005B50 RID: 23376 RVA: 0x0013492F File Offset: 0x00132B2F
		public string CheckId { get; }

		/// <summary>Gets or sets the scope of the code that is relevant for the attribute.</summary>
		/// <returns>The scope of the code that is relevant for the attribute.</returns>
		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x06005B51 RID: 23377 RVA: 0x00134937 File Offset: 0x00132B37
		// (set) Token: 0x06005B52 RID: 23378 RVA: 0x0013493F File Offset: 0x00132B3F
		public string Scope { get; set; }

		/// <summary>Gets or sets a fully qualified path that represents the target of the attribute.</summary>
		/// <returns>A fully qualified path that represents the target of the attribute.</returns>
		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x06005B53 RID: 23379 RVA: 0x00134948 File Offset: 0x00132B48
		// (set) Token: 0x06005B54 RID: 23380 RVA: 0x00134950 File Offset: 0x00132B50
		public string Target { get; set; }

		/// <summary>Gets or sets an optional argument expanding on exclusion criteria.</summary>
		/// <returns>A string containing the expanded exclusion criteria.</returns>
		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x06005B55 RID: 23381 RVA: 0x00134959 File Offset: 0x00132B59
		// (set) Token: 0x06005B56 RID: 23382 RVA: 0x00134961 File Offset: 0x00132B61
		public string MessageId { get; set; }

		/// <summary>Gets or sets the justification for suppressing the code analysis message.</summary>
		/// <returns>The justification for suppressing the message.</returns>
		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x06005B57 RID: 23383 RVA: 0x0013496A File Offset: 0x00132B6A
		// (set) Token: 0x06005B58 RID: 23384 RVA: 0x00134972 File Offset: 0x00132B72
		public string Justification { get; set; }
	}
}
