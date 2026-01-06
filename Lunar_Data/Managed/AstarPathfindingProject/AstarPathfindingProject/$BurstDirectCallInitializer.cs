using System;
using Pathfinding;
using Pathfinding.Graphs.Navmesh;
using Pathfinding.RVO;
using Pathfinding.Util;
using UnityEngine;

// Token: 0x020002BB RID: 699
internal static class $BurstDirectCallInitializer
{
	// Token: 0x06001069 RID: 4201 RVA: 0x00066B50 File Offset: 0x00064D50
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
	private static void Initialize()
	{
		Polygon.ContainsPoint_000002C6$BurstDirectCall.Initialize();
		Polygon.ClosestPointOnTriangleByRef_000002CD$BurstDirectCall.Initialize();
		Polygon.ClosestPointOnTriangleProjected_000002CF$BurstDirectCall.Initialize();
		BinaryHeap.Add_000002DF$BurstDirectCall.Initialize();
		BinaryHeap.Remove_000002E2$BurstDirectCall.Initialize();
		HeuristicObjective.Calculate_000004C0$BurstDirectCall.Initialize();
		Path.OpenCandidateConnectionBurst_000004FC$BurstDirectCall.Initialize();
		TriangleMeshNode.InterpolateEdge_00000757$BurstDirectCall.Initialize();
		TriangleMeshNode.OpenSingleEdgeBurst_0000075C$BurstDirectCall.Initialize();
		TriangleMeshNode.CalculateBestEdgePosition_0000075D$BurstDirectCall.Initialize();
		NavmeshCutJobs.CalculateContour_00000875$BurstDirectCall.Initialize();
		Funnel.Calculate_0000090B$BurstDirectCall.Initialize();
		Funnel.FunnelState.PushStart_00000917$BurstDirectCall.Initialize();
		Funnel.FunnelState.ConvertCornerIndicesToPathProjected_00000921$BurstDirectCall.Initialize();
		PathTracer.ContainsAndProject_00000949$BurstDirectCall.Initialize();
		PathTracer.EstimateRemainingPath_0000095A$BurstDirectCall.Initialize();
		PathTracer.RemainingDistanceLowerBound_0000095E$BurstDirectCall.Initialize();
		BBTree.Build_00000A75$BurstDirectCall.Initialize();
		BBTree.ProjectionParams.SquaredRectPointDistanceOnPlane_00000A80$BurstDirectCall.Initialize();
		BBTree.Initialize$NearbyNodesIterator_MoveNext_00000A87$BurstDirectCall();
		ColliderMeshBuilder2D.GenerateMeshesFromShapes_00000A8F$BurstDirectCall.Initialize();
		RecastMeshGatherer.CalculateBounds_00000A9B$BurstDirectCall.Initialize();
		HierarchicalBitset.Iterator.MoveNextBurst_00000CDA$BurstDirectCall.Initialize();
		MeshUtility.MakeTrianglesClockwise_00000E12$BurstDirectCall.Initialize();
		RVOObstacleCache.TraceContours_00000EED$BurstDirectCall.Initialize();
	}
}
