using System;

namespace System.Reflection
{
	/// <summary>Instructs obfuscation tools to use their standard obfuscation rules for the appropriate assembly type.</summary>
	// Token: 0x020008B3 RID: 2227
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	public sealed class ObfuscateAssemblyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.ObfuscateAssemblyAttribute" /> class, specifying whether the assembly to be obfuscated is public or private.</summary>
		/// <param name="assemblyIsPrivate">true if the assembly is used within the scope of one application; otherwise, false.</param>
		// Token: 0x060049B2 RID: 18866 RVA: 0x000EF158 File Offset: 0x000ED358
		public ObfuscateAssemblyAttribute(bool assemblyIsPrivate)
		{
			this.AssemblyIsPrivate = assemblyIsPrivate;
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value indicating whether the assembly was marked private.</summary>
		/// <returns>true if the assembly was marked private; otherwise, false. </returns>
		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x060049B3 RID: 18867 RVA: 0x000EF16E File Offset: 0x000ED36E
		public bool AssemblyIsPrivate { get; }

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value indicating whether the obfuscation tool should remove the attribute after processing.</summary>
		/// <returns>true if the obfuscation tool should remove the attribute after processing; otherwise, false. The default value for this property is true.</returns>
		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x060049B4 RID: 18868 RVA: 0x000EF176 File Offset: 0x000ED376
		// (set) Token: 0x060049B5 RID: 18869 RVA: 0x000EF17E File Offset: 0x000ED37E
		public bool StripAfterObfuscation { get; set; } = true;
	}
}
