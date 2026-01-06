using System;

namespace System.Reflection
{
	/// <summary>Instructs obfuscation tools to take the specified actions for an assembly, type, or member.</summary>
	// Token: 0x020008B4 RID: 2228
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Parameter | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
	public sealed class ObfuscationAttribute : Attribute
	{
		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value indicating whether the obfuscation tool should remove this attribute after processing.</summary>
		/// <returns>true if an obfuscation tool should remove the attribute after processing; otherwise, false. The default is true.</returns>
		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x060049B7 RID: 18871 RVA: 0x000EF1AF File Offset: 0x000ED3AF
		// (set) Token: 0x060049B8 RID: 18872 RVA: 0x000EF1B7 File Offset: 0x000ED3B7
		public bool StripAfterObfuscation { get; set; } = true;

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value indicating whether the obfuscation tool should exclude the type or member from obfuscation.</summary>
		/// <returns>true if the type or member to which this attribute is applied should be excluded from obfuscation; otherwise, false. The default is true.</returns>
		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x060049B9 RID: 18873 RVA: 0x000EF1C0 File Offset: 0x000ED3C0
		// (set) Token: 0x060049BA RID: 18874 RVA: 0x000EF1C8 File Offset: 0x000ED3C8
		public bool Exclude { get; set; } = true;

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value indicating whether the attribute of a type is to apply to the members of the type.</summary>
		/// <returns>true if the attribute is to apply to the members of the type; otherwise, false. The default is true.</returns>
		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x060049BB RID: 18875 RVA: 0x000EF1D1 File Offset: 0x000ED3D1
		// (set) Token: 0x060049BC RID: 18876 RVA: 0x000EF1D9 File Offset: 0x000ED3D9
		public bool ApplyToMembers { get; set; } = true;

		/// <summary>Gets or sets a string value that is recognized by the obfuscation tool, and which specifies processing options. </summary>
		/// <returns>A string value that is recognized by the obfuscation tool, and which specifies processing options. The default is "all".</returns>
		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x060049BD RID: 18877 RVA: 0x000EF1E2 File Offset: 0x000ED3E2
		// (set) Token: 0x060049BE RID: 18878 RVA: 0x000EF1EA File Offset: 0x000ED3EA
		public string Feature { get; set; } = "all";
	}
}
