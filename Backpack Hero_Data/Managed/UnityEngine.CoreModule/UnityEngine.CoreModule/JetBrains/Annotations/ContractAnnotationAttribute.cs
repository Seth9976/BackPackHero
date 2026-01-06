using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000BD RID: 189
	[AttributeUsage(64, AllowMultiple = true)]
	public sealed class ContractAnnotationAttribute : Attribute
	{
		// Token: 0x06000348 RID: 840 RVA: 0x00005C55 File Offset: 0x00003E55
		public ContractAnnotationAttribute([NotNull] string contract)
			: this(contract, false)
		{
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00005C61 File Offset: 0x00003E61
		public ContractAnnotationAttribute([NotNull] string contract, bool forceFullStates)
		{
			this.Contract = contract;
			this.ForceFullStates = forceFullStates;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600034A RID: 842 RVA: 0x00005C79 File Offset: 0x00003E79
		[NotNull]
		public string Contract { get; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00005C81 File Offset: 0x00003E81
		public bool ForceFullStates { get; }
	}
}
