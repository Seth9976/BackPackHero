using System;
using Pathfinding.Drawing;
using UnityEngine;

// Token: 0x0200005E RID: 94
internal static class $BurstDirectCallInitializer
{
	// Token: 0x060002A8 RID: 680 RVA: 0x000108C0 File Offset: 0x0000EAC0
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
	private static void Initialize()
	{
		CommandBuilder.Initialize$JobWireMesh_WireMesh_0000021A$BurstDirectCall();
		CommandBuilder.Initialize$JobWireMesh_Execute_0000021B$BurstDirectCall();
		DrawingData.BuilderData.AnyBuffersWrittenTo_00000237$BurstDirectCall.Initialize();
		DrawingData.BuilderData.ResetAllBuffers_00000238$BurstDirectCall.Initialize();
	}
}
