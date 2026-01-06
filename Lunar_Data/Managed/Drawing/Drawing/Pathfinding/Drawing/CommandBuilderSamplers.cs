using System;
using Unity.Profiling;

namespace Pathfinding.Drawing
{
	// Token: 0x02000008 RID: 8
	internal static class CommandBuilderSamplers
	{
		// Token: 0x04000010 RID: 16
		internal static readonly ProfilerMarker MarkerConvert = new ProfilerMarker("Convert");

		// Token: 0x04000011 RID: 17
		internal static readonly ProfilerMarker MarkerSetLayout = new ProfilerMarker("SetLayout");

		// Token: 0x04000012 RID: 18
		internal static readonly ProfilerMarker MarkerUpdateVertices = new ProfilerMarker("UpdateVertices");

		// Token: 0x04000013 RID: 19
		internal static readonly ProfilerMarker MarkerUpdateIndices = new ProfilerMarker("UpdateIndices");

		// Token: 0x04000014 RID: 20
		internal static readonly ProfilerMarker MarkerSubmesh = new ProfilerMarker("Submesh");

		// Token: 0x04000015 RID: 21
		internal static readonly ProfilerMarker MarkerUpdateBuffer = new ProfilerMarker("UpdateComputeBuffer");

		// Token: 0x04000016 RID: 22
		internal static readonly ProfilerMarker MarkerProcessCommands = new ProfilerMarker("Commands");

		// Token: 0x04000017 RID: 23
		internal static readonly ProfilerMarker MarkerCreateTriangles = new ProfilerMarker("CreateTriangles");
	}
}
