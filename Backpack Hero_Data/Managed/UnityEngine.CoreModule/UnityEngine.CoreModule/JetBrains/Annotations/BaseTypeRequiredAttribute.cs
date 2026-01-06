using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000C0 RID: 192
	[AttributeUsage(4, AllowMultiple = true)]
	[BaseTypeRequired(typeof(Attribute))]
	public sealed class BaseTypeRequiredAttribute : Attribute
	{
		// Token: 0x06000350 RID: 848 RVA: 0x00005CAD File Offset: 0x00003EAD
		public BaseTypeRequiredAttribute([NotNull] Type baseType)
		{
			this.BaseType = baseType;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00005CBE File Offset: 0x00003EBE
		[NotNull]
		public Type BaseType { get; }
	}
}
