using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x020001E8 RID: 488
	[NativeHeader("Runtime/Export/Resources/Resources.bindings.h")]
	[NativeHeader("Runtime/Misc/ResourceManagerUtility.h")]
	internal static class ResourcesAPIInternal
	{
		// Token: 0x0600160F RID: 5647
		[FreeFunction("Resources_Bindings::FindObjectsOfTypeAll")]
		[TypeInferenceRule(TypeInferenceRules.ArrayOfTypeReferencedByFirstArgument)]
		[MethodImpl(4096)]
		public static extern Object[] FindObjectsOfTypeAll(Type type);

		// Token: 0x06001610 RID: 5648
		[FreeFunction("GetShaderNameRegistry().FindShader")]
		[MethodImpl(4096)]
		public static extern Shader FindShaderByName(string name);

		// Token: 0x06001611 RID: 5649
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedBySecondArgument)]
		[NativeThrows]
		[FreeFunction("Resources_Bindings::Load")]
		[MethodImpl(4096)]
		public static extern Object Load(string path, [NotNull("ArgumentNullException")] Type systemTypeInstance);

		// Token: 0x06001612 RID: 5650
		[NativeThrows]
		[FreeFunction("Resources_Bindings::LoadAll")]
		[MethodImpl(4096)]
		public static extern Object[] LoadAll([NotNull("ArgumentNullException")] string path, [NotNull("ArgumentNullException")] Type systemTypeInstance);

		// Token: 0x06001613 RID: 5651
		[FreeFunction("Resources_Bindings::LoadAsyncInternal")]
		[MethodImpl(4096)]
		internal static extern ResourceRequest LoadAsyncInternal(string path, Type type);

		// Token: 0x06001614 RID: 5652
		[FreeFunction("Scripting::UnloadAssetFromScripting")]
		[MethodImpl(4096)]
		public static extern void UnloadAsset(Object assetToUnload);
	}
}
