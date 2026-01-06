using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000172 RID: 370
	[Obsolete("Set VariableKind via VariableDeclarations.Kind")]
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class VariableKindAttribute : Attribute
	{
		// Token: 0x060009DD RID: 2525 RVA: 0x000297A4 File Offset: 0x000279A4
		public VariableKindAttribute(VariableKind kind)
		{
			this.kind = kind;
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x000297B3 File Offset: 0x000279B3
		public VariableKind kind { get; }
	}
}
