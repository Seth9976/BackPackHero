using System;
using System.Linq;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x020000D5 RID: 213
	internal class ExtensionMethodCache
	{
		// Token: 0x06000602 RID: 1538 RVA: 0x0000F870 File Offset: 0x0000DA70
		internal ExtensionMethodCache()
		{
			this.Cache = (from method in RuntimeCodebase.types.Where((Type type) => type.IsStatic() && !type.IsGenericType && !type.IsNested).SelectMany((Type type) => type.GetMethods())
				where method.IsExtension()
				select method).ToArray<MethodInfo>();
		}

		// Token: 0x04000152 RID: 338
		internal readonly MethodInfo[] Cache;
	}
}
