using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001E2 RID: 482
	[NativeHeader("Runtime/Utilities/PropertyName.h")]
	internal class PropertyNameUtils
	{
		// Token: 0x060015DE RID: 5598 RVA: 0x0002312C File Offset: 0x0002132C
		[FreeFunction(IsThreadSafe = true)]
		public static PropertyName PropertyNameFromString([Unmarshalled] string name)
		{
			PropertyName propertyName;
			PropertyNameUtils.PropertyNameFromString_Injected(name, out propertyName);
			return propertyName;
		}

		// Token: 0x060015E0 RID: 5600
		[MethodImpl(4096)]
		private static extern void PropertyNameFromString_Injected(string name, out PropertyName ret);
	}
}
