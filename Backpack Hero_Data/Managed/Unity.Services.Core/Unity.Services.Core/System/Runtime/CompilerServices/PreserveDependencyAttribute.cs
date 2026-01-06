using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000002 RID: 2
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = true)]
	internal sealed class PreserveDependencyAttribute : Attribute
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public PreserveDependencyAttribute(string memberSignature)
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public PreserveDependencyAttribute(string memberSignature, string typeName)
		{
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
		public PreserveDependencyAttribute(string memberSignature, string typeName, string assembly)
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002068 File Offset: 0x00000268
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002070 File Offset: 0x00000270
		public string Condition { get; set; }
	}
}
