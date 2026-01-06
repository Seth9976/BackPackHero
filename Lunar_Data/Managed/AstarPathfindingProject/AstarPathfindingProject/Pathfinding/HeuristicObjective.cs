using System;
using System.Runtime.CompilerServices;
using Pathfinding.Graphs.Util;
using Pathfinding.Util;
using Unity.Burst;
using Unity.Mathematics;

namespace Pathfinding
{
	// Token: 0x0200009D RID: 157
	[BurstCompile]
	public readonly struct HeuristicObjective
	{
		// Token: 0x060004F3 RID: 1267 RVA: 0x00018594 File Offset: 0x00016794
		public HeuristicObjective(int3 point, Heuristic heuristic, float heuristicScale)
		{
			this.mx = point;
			this.mn = point;
			this.heuristic = heuristic;
			this.heuristicScale = heuristicScale;
			this.euclideanEmbeddingCosts = default(UnsafeSpan<uint>);
			this.euclideanEmbeddingPivots = 0U;
			this.targetNodeIndex = 0U;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x000185DC File Offset: 0x000167DC
		public HeuristicObjective(int3 point, Heuristic heuristic, float heuristicScale, uint targetNodeIndex, EuclideanEmbedding euclideanEmbedding)
		{
			this.mx = point;
			this.mn = point;
			this.heuristic = heuristic;
			this.heuristicScale = heuristicScale;
			this.euclideanEmbeddingCosts = ((euclideanEmbedding != null) ? euclideanEmbedding.costs.AsUnsafeSpanNoChecks<uint>() : default(UnsafeSpan<uint>));
			this.euclideanEmbeddingPivots = (uint)((euclideanEmbedding != null) ? euclideanEmbedding.pivotCount : 0);
			this.targetNodeIndex = targetNodeIndex;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00018644 File Offset: 0x00016844
		public HeuristicObjective(int3 mn, int3 mx, Heuristic heuristic, float heuristicScale, uint targetNodeIndex, EuclideanEmbedding euclideanEmbedding)
		{
			this.mn = mn;
			this.mx = mx;
			this.heuristic = heuristic;
			this.heuristicScale = heuristicScale;
			this.euclideanEmbeddingCosts = ((euclideanEmbedding != null) ? euclideanEmbedding.costs.AsUnsafeSpanNoChecks<uint>() : default(UnsafeSpan<uint>));
			this.euclideanEmbeddingPivots = (uint)((euclideanEmbedding != null) ? euclideanEmbedding.pivotCount : 0);
			this.targetNodeIndex = targetNodeIndex;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x000186AB File Offset: 0x000168AB
		public int Calculate(int3 point, uint nodeIndex)
		{
			return HeuristicObjective.Calculate(in this, ref point, nodeIndex);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x000186B6 File Offset: 0x000168B6
		[BurstCompile]
		public static int Calculate(in HeuristicObjective objective, ref int3 point, uint nodeIndex)
		{
			return HeuristicObjective.Calculate_000004C0$BurstDirectCall.Invoke(in objective, ref point, nodeIndex);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x000186C0 File Offset: 0x000168C0
		[BurstCompile]
		[MethodImpl(256)]
		public static int Calculate$BurstManaged(in HeuristicObjective objective, ref int3 point, uint nodeIndex)
		{
			int3 @int = math.clamp(point, objective.mn, objective.mx);
			int3 int2 = point - @int;
			int num;
			switch (objective.heuristic)
			{
			case Heuristic.Manhattan:
				num = (int)((float)math.csum(math.abs(int2)) * objective.heuristicScale);
				goto IL_00EC;
			case Heuristic.DiagonalManhattan:
			{
				int2 = math.abs(int2);
				int x = int2.x;
				int y = int2.y;
				int z = int2.z;
				if (x > y)
				{
					Memory.Swap<int>(ref x, ref y);
				}
				if (y > z)
				{
					Memory.Swap<int>(ref y, ref z);
				}
				if (x > y)
				{
					Memory.Swap<int>(ref x, ref y);
				}
				num = (int)(objective.heuristicScale * (1.7321f * (float)x + 1.4142f * (float)(y - x) + (float)(z - y - x)));
				goto IL_00EC;
			}
			case Heuristic.Euclidean:
				num = (int)(math.length(int2) * objective.heuristicScale);
				goto IL_00EC;
			}
			num = 0;
			IL_00EC:
			if (objective.euclideanEmbeddingPivots > 0U)
			{
				num = math.max(num, (int)EuclideanEmbedding.GetHeuristic(objective.euclideanEmbeddingCosts, objective.euclideanEmbeddingPivots, nodeIndex, objective.targetNodeIndex));
			}
			return num;
		}

		// Token: 0x04000341 RID: 833
		private readonly int3 mn;

		// Token: 0x04000342 RID: 834
		private readonly int3 mx;

		// Token: 0x04000343 RID: 835
		private readonly Heuristic heuristic;

		// Token: 0x04000344 RID: 836
		private readonly float heuristicScale;

		// Token: 0x04000345 RID: 837
		private readonly UnsafeSpan<uint> euclideanEmbeddingCosts;

		// Token: 0x04000346 RID: 838
		private readonly uint euclideanEmbeddingPivots;

		// Token: 0x04000347 RID: 839
		private readonly uint targetNodeIndex;

		// Token: 0x0200009E RID: 158
		// (Invoke) Token: 0x060004FA RID: 1274
		public delegate int Calculate_000004C0$PostfixBurstDelegate(in HeuristicObjective objective, ref int3 point, uint nodeIndex);

		// Token: 0x0200009F RID: 159
		internal static class Calculate_000004C0$BurstDirectCall
		{
			// Token: 0x060004FD RID: 1277 RVA: 0x000187E2 File Offset: 0x000169E2
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (HeuristicObjective.Calculate_000004C0$BurstDirectCall.Pointer == 0)
				{
					HeuristicObjective.Calculate_000004C0$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(HeuristicObjective.Calculate_000004C0$BurstDirectCall.DeferredCompilation, methodof(HeuristicObjective.Calculate$BurstManaged(ref HeuristicObjective, ref int3, uint)).MethodHandle, typeof(HeuristicObjective.Calculate_000004C0$PostfixBurstDelegate).TypeHandle);
				}
				A_0 = HeuristicObjective.Calculate_000004C0$BurstDirectCall.Pointer;
			}

			// Token: 0x060004FE RID: 1278 RVA: 0x00018810 File Offset: 0x00016A10
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				HeuristicObjective.Calculate_000004C0$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x060004FF RID: 1279 RVA: 0x00018828 File Offset: 0x00016A28
			public static void Constructor()
			{
				HeuristicObjective.Calculate_000004C0$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(HeuristicObjective.Calculate(ref HeuristicObjective, ref int3, uint)).MethodHandle);
			}

			// Token: 0x06000500 RID: 1280 RVA: 0x000033F6 File Offset: 0x000015F6
			public static void Initialize()
			{
			}

			// Token: 0x06000501 RID: 1281 RVA: 0x00018839 File Offset: 0x00016A39
			// Note: this type is marked as 'beforefieldinit'.
			static Calculate_000004C0$BurstDirectCall()
			{
				HeuristicObjective.Calculate_000004C0$BurstDirectCall.Constructor();
			}

			// Token: 0x06000502 RID: 1282 RVA: 0x00018840 File Offset: 0x00016A40
			public static int Invoke(in HeuristicObjective objective, ref int3 point, uint nodeIndex)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = HeuristicObjective.Calculate_000004C0$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						return calli(System.Int32(Pathfinding.HeuristicObjective&,Unity.Mathematics.int3&,System.UInt32), ref objective, ref point, nodeIndex, functionPointer);
					}
				}
				return HeuristicObjective.Calculate$BurstManaged(in objective, ref point, nodeIndex);
			}

			// Token: 0x04000348 RID: 840
			private static IntPtr Pointer;

			// Token: 0x04000349 RID: 841
			private static IntPtr DeferredCompilation;
		}
	}
}
