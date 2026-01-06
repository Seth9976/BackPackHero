using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000025 RID: 37
	[NativeHeader("Modules/Animation/OptimizeTransformHierarchy.h")]
	public class AnimatorUtility
	{
		// Token: 0x06000204 RID: 516
		[FreeFunction]
		[MethodImpl(4096)]
		public static extern void OptimizeTransformHierarchy([NotNull("NullExceptionObject")] GameObject go, string[] exposedTransforms);

		// Token: 0x06000205 RID: 517
		[FreeFunction]
		[MethodImpl(4096)]
		public static extern void DeoptimizeTransformHierarchy([NotNull("NullExceptionObject")] GameObject go);
	}
}
