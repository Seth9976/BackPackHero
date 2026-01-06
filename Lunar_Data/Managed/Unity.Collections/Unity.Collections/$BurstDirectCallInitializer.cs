using System;
using Unity.Collections;
using UnityEngine;

// Token: 0x0200015A RID: 346
internal static class $BurstDirectCallInitializer
{
	// Token: 0x06000C25 RID: 3109 RVA: 0x000244A5 File Offset: 0x000226A5
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
	private static void Initialize()
	{
		AllocatorManager.StackAllocator.Try_00000A45$BurstDirectCall.Initialize();
		AllocatorManager.SlabAllocator.Try_00000A53$BurstDirectCall.Initialize();
		RewindableAllocator.Try_00000756$BurstDirectCall.Initialize();
		xxHash3.Hash64Long_0000078D$BurstDirectCall.Initialize();
		xxHash3.Hash128Long_00000794$BurstDirectCall.Initialize();
	}
}
