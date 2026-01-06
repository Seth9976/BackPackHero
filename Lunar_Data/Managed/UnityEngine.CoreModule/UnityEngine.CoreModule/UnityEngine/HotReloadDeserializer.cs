using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001B2 RID: 434
	[NativeType(Header = "Runtime/Export/HotReload/HotReload.bindings.h")]
	[NativeConditional("HOT_RELOAD_AVAILABLE")]
	internal static class HotReloadDeserializer
	{
		// Token: 0x06001326 RID: 4902
		[FreeFunction("HotReload::Prepare")]
		[MethodImpl(4096)]
		internal static extern void PrepareHotReload();

		// Token: 0x06001327 RID: 4903
		[FreeFunction("HotReload::Finish")]
		[MethodImpl(4096)]
		internal static extern void FinishHotReload(Type[] typesToReset);

		// Token: 0x06001328 RID: 4904
		[NativeThrows]
		[FreeFunction("HotReload::CreateEmptyAsset")]
		[MethodImpl(4096)]
		internal static extern Object CreateEmptyAsset(Type type);

		// Token: 0x06001329 RID: 4905
		[NativeThrows]
		[FreeFunction("HotReload::DeserializeAsset")]
		[MethodImpl(4096)]
		internal static extern void DeserializeAsset(Object asset, byte[] data);

		// Token: 0x0600132A RID: 4906
		[FreeFunction("HotReload::RemapInstanceIds")]
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void RemapInstanceIds(Object editorAsset, int[] editorToPlayerInstanceIdMapKeys, int[] editorToPlayerInstanceIdMapValues);

		// Token: 0x0600132B RID: 4907 RVA: 0x0001ACAF File Offset: 0x00018EAF
		internal static void RemapInstanceIds(Object editorAsset, Dictionary<int, int> editorToPlayerInstanceIdMap)
		{
			HotReloadDeserializer.RemapInstanceIds(editorAsset, Enumerable.ToArray<int>(editorToPlayerInstanceIdMap.Keys), Enumerable.ToArray<int>(editorToPlayerInstanceIdMap.Values));
		}

		// Token: 0x0600132C RID: 4908
		[FreeFunction("HotReload::FinalizeAssetCreation")]
		[MethodImpl(4096)]
		internal static extern void FinalizeAssetCreation(Object asset);

		// Token: 0x0600132D RID: 4909
		[FreeFunction("HotReload::GetDependencies")]
		[MethodImpl(4096)]
		internal static extern Object[] GetDependencies(Object asset);

		// Token: 0x0600132E RID: 4910
		[FreeFunction("HotReload::GetNullDependencies")]
		[MethodImpl(4096)]
		internal static extern int[] GetNullDependencies(Object asset);
	}
}
