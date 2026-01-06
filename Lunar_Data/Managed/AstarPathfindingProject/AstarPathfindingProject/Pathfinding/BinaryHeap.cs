using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Pathfinding.Util;
using Unity.Burst;
using Unity.Burst.CompilerServices;
using Unity.Collections;
using Unity.Mathematics;

namespace Pathfinding
{
	// Token: 0x02000054 RID: 84
	[BurstCompile]
	public struct BinaryHeap
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000E747 File Offset: 0x0000C947
		public bool isEmpty
		{
			get
			{
				return this.numberOfItems <= 0;
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000E755 File Offset: 0x0000C955
		private static int RoundUpToNextMultipleMod1(int v)
		{
			return v + (4 - (v - 1) % 4) % 4;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000E762 File Offset: 0x0000C962
		public BinaryHeap(int capacity)
		{
			capacity = BinaryHeap.RoundUpToNextMultipleMod1(capacity);
			this.heap = new UnsafeSpan<BinaryHeap.HeapNode>(Allocator.Persistent, capacity);
			this.numberOfItems = 0;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000E780 File Offset: 0x0000C980
		public void Dispose()
		{
			AllocatorManager.Free<BinaryHeap.HeapNode>(Allocator.Persistent, this.heap.ptr, this.heap.Length);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000E7A4 File Offset: 0x0000C9A4
		public void Clear(UnsafeSpan<PathNode> pathNodes)
		{
			for (int i = 0; i < this.numberOfItems; i++)
			{
				pathNodes[this.heap[i].pathNodeIndex].heapIndex = ushort.MaxValue;
			}
			this.numberOfItems = 0;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000E7EB File Offset: 0x0000C9EB
		[MethodImpl(256)]
		public uint GetPathNodeIndex(int heapIndex)
		{
			return this.heap[heapIndex].pathNodeIndex;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000E7FE File Offset: 0x0000C9FE
		[MethodImpl(256)]
		public uint GetG(int heapIndex)
		{
			return this.heap[heapIndex].G;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000E811 File Offset: 0x0000CA11
		[MethodImpl(256)]
		public uint GetF(int heapIndex)
		{
			return this.heap[heapIndex].F;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000E824 File Offset: 0x0000CA24
		public void SetH(int heapIndex, uint h)
		{
			this.heap[heapIndex].F = this.heap[heapIndex].G + h;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000E84C File Offset: 0x0000CA4C
		private static void Expand(ref UnsafeSpan<BinaryHeap.HeapNode> heap)
		{
			int num = math.max(heap.Length + 4, math.min(65533, (int)math.round((float)heap.Length * 2f)));
			num = BinaryHeap.RoundUpToNextMultipleMod1(num);
			if (num > 65534)
			{
				throw new Exception("Binary Heap Size really large (>65534). A heap size this large is probably the cause of pathfinding running in an infinite loop. ");
			}
			UnsafeSpan<BinaryHeap.HeapNode> unsafeSpan = new UnsafeSpan<BinaryHeap.HeapNode>(Allocator.Persistent, num);
			unsafeSpan.CopyFrom(heap);
			AllocatorManager.Free<BinaryHeap.HeapNode>(Allocator.Persistent, heap.ptr, heap.Length);
			heap = unsafeSpan;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000E8D1 File Offset: 0x0000CAD1
		public void Add(UnsafeSpan<PathNode> nodes, uint pathNodeIndex, uint g, uint f)
		{
			BinaryHeap.Add(ref this, ref nodes, pathNodeIndex, g, f);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000E8DF File Offset: 0x0000CADF
		[BurstCompile]
		private static void Add(ref BinaryHeap binaryHeap, ref UnsafeSpan<PathNode> nodes, uint pathNodeIndex, uint g, uint f)
		{
			BinaryHeap.Add_000002DF$BurstDirectCall.Invoke(ref binaryHeap, ref nodes, pathNodeIndex, g, f);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000E8EC File Offset: 0x0000CAEC
		private unsafe static void DecreaseKey(UnsafeSpan<BinaryHeap.HeapNode> heap, UnsafeSpan<PathNode> nodes, BinaryHeap.HeapNode node, ushort index)
		{
			uint num;
			uint num2;
			for (num = (uint)index; num != 0U; num = num2)
			{
				num2 = (num - 1U) / 4U;
				Hint.Assume(num2 < heap.length);
				Hint.Assume(num < heap.length);
				if (node.sortKey >= heap[num2].sortKey)
				{
					break;
				}
				*heap[num] = *heap[num2];
				nodes[heap[num].pathNodeIndex].heapIndex = (ushort)num;
			}
			Hint.Assume(num < heap.length);
			*heap[num] = node;
			nodes[node.pathNodeIndex].heapIndex = (ushort)num;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000E99E File Offset: 0x0000CB9E
		public uint Remove(UnsafeSpan<PathNode> nodes, out uint g, out uint f)
		{
			return BinaryHeap.Remove(ref nodes, ref this, out g, out f);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000E9AA File Offset: 0x0000CBAA
		[BurstCompile]
		private static uint Remove(ref UnsafeSpan<PathNode> nodes, ref BinaryHeap binaryHeap, [NoAlias] out uint removedG, [NoAlias] out uint removedF)
		{
			return BinaryHeap.Remove_000002E2$BurstDirectCall.Invoke(ref nodes, ref binaryHeap, out removedG, out removedF);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000E9B8 File Offset: 0x0000CBB8
		[Conditional("VALIDATE_BINARY_HEAP")]
		private static void Validate(ref UnsafeSpan<PathNode> nodes, ref BinaryHeap binaryHeap)
		{
			for (int i = 1; i < binaryHeap.numberOfItems; i++)
			{
				int num = (i - 1) / 4;
				if (binaryHeap.heap[num].F > binaryHeap.heap[i].F)
				{
					throw new Exception(string.Concat(new string[]
					{
						"Invalid state at ",
						i.ToString(),
						":",
						num.ToString(),
						" ( ",
						binaryHeap.heap[num].F.ToString(),
						" > ",
						binaryHeap.heap[i].F.ToString(),
						" ) "
					}));
				}
				if (binaryHeap.heap[num].sortKey > binaryHeap.heap[i].sortKey)
				{
					throw new Exception(string.Concat(new string[]
					{
						"Invalid state at ",
						i.ToString(),
						":",
						num.ToString(),
						" ( ",
						binaryHeap.heap[num].F.ToString(),
						" > ",
						binaryHeap.heap[i].F.ToString(),
						" ) "
					}));
				}
				if ((int)nodes[binaryHeap.heap[i].pathNodeIndex].heapIndex != i)
				{
					throw new Exception("Invalid heap index");
				}
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000EB68 File Offset: 0x0000CD68
		public unsafe void Rebuild(UnsafeSpan<PathNode> nodes)
		{
			for (int i = 2; i < this.numberOfItems; i++)
			{
				int num = i;
				BinaryHeap.HeapNode heapNode = *this.heap[i];
				uint f = heapNode.F;
				while (num != 1)
				{
					int num2 = num / 4;
					if (f >= this.heap[num2].F)
					{
						break;
					}
					*this.heap[num] = *this.heap[num2];
					nodes[this.heap[num].pathNodeIndex].heapIndex = (ushort)num;
					*this.heap[num2] = heapNode;
					nodes[this.heap[num2].pathNodeIndex].heapIndex = (ushort)num2;
					num = num2;
				}
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000EC4C File Offset: 0x0000CE4C
		[BurstCompile]
		[MethodImpl(256)]
		public static void Add$BurstManaged(ref BinaryHeap binaryHeap, ref UnsafeSpan<PathNode> nodes, uint pathNodeIndex, uint g, uint f)
		{
			ref int ptr = ref binaryHeap.numberOfItems;
			ref UnsafeSpan<BinaryHeap.HeapNode> ptr2 = ref binaryHeap.heap;
			ref PathNode ptr3 = ref nodes[pathNodeIndex];
			if (ptr3.heapIndex != 65535)
			{
				BinaryHeap.HeapNode heapNode = new BinaryHeap.HeapNode(pathNodeIndex, g, f);
				BinaryHeap.DecreaseKey(ptr2, nodes, heapNode, ptr3.heapIndex);
				return;
			}
			if (ptr == ptr2.Length)
			{
				BinaryHeap.Expand(ref ptr2);
			}
			BinaryHeap.DecreaseKey(ptr2, nodes, new BinaryHeap.HeapNode(pathNodeIndex, g, f), (ushort)ptr);
			ptr++;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000ECD4 File Offset: 0x0000CED4
		[BurstCompile]
		[MethodImpl(256)]
		public unsafe static uint Remove$BurstManaged(ref UnsafeSpan<PathNode> nodes, ref BinaryHeap binaryHeap, [NoAlias] out uint removedG, [NoAlias] out uint removedF)
		{
			ref int ptr = ref binaryHeap.numberOfItems;
			UnsafeSpan<BinaryHeap.HeapNode> unsafeSpan = binaryHeap.heap;
			if (ptr == 0)
			{
				throw new InvalidOperationException("Removing item from empty heap");
			}
			Hint.Assume(0UL < (ulong)unsafeSpan.length);
			uint pathNodeIndex = unsafeSpan[0].pathNodeIndex;
			nodes[pathNodeIndex].heapIndex = ushort.MaxValue;
			removedG = unsafeSpan[0].G;
			removedF = unsafeSpan[0].F;
			ptr--;
			if (ptr == 0)
			{
				return pathNodeIndex;
			}
			Hint.Assume(ptr < (int)unsafeSpan.length);
			BinaryHeap.HeapNode heapNode = *unsafeSpan[ptr];
			uint num = 0U;
			ulong sortKey = heapNode.sortKey;
			for (;;)
			{
				uint num2 = num;
				uint num3 = num2 * 4U + 1U;
				if ((ulong)num3 >= (ulong)((long)ptr))
				{
					break;
				}
				Hint.Assume(num3 < unsafeSpan.length);
				ulong num4 = (unsafeSpan[num3].sortKey & 18446744073709551612UL) | 0UL;
				Hint.Assume(num3 + 1U < unsafeSpan.length);
				ulong num5 = (unsafeSpan[num3 + 1U].sortKey & 18446744073709551612UL) | 1UL;
				Hint.Assume(num3 + 2U < unsafeSpan.length);
				ulong num6 = (unsafeSpan[num3 + 2U].sortKey & 18446744073709551612UL) | 2UL;
				Hint.Assume(num3 + 3U < unsafeSpan.length);
				ulong num7 = (unsafeSpan[num3 + 3U].sortKey & 18446744073709551612UL) | 3UL;
				ulong num8 = num4;
				if ((ulong)(num3 + 1U) < (ulong)((long)ptr))
				{
					num8 = math.min(num8, num5);
				}
				if ((ulong)(num3 + 2U) < (ulong)((long)ptr))
				{
					num8 = math.min(num8, num6);
				}
				if ((ulong)(num3 + 3U) < (ulong)((long)ptr))
				{
					num8 = math.min(num8, num7);
				}
				if (num8 >= sortKey)
				{
					break;
				}
				num = num3 + (uint)(num8 & 3UL);
				Hint.Assume(num2 < unsafeSpan.length);
				Hint.Assume(num < unsafeSpan.length);
				*unsafeSpan[num2] = *unsafeSpan[num];
				Hint.Assume(num < unsafeSpan.length);
				nodes[unsafeSpan[num].pathNodeIndex].heapIndex = (ushort)num2;
			}
			Hint.Assume(num < unsafeSpan.length);
			*unsafeSpan[num] = heapNode;
			nodes[heapNode.pathNodeIndex].heapIndex = (ushort)num;
			return pathNodeIndex;
		}

		// Token: 0x040001DC RID: 476
		public int numberOfItems;

		// Token: 0x040001DD RID: 477
		public const float GrowthFactor = 2f;

		// Token: 0x040001DE RID: 478
		private const int D = 4;

		// Token: 0x040001DF RID: 479
		private const bool SortGScores = true;

		// Token: 0x040001E0 RID: 480
		public const ushort NotInHeap = 65535;

		// Token: 0x040001E1 RID: 481
		private UnsafeSpan<BinaryHeap.HeapNode> heap;

		// Token: 0x02000055 RID: 85
		private struct HeapNode
		{
			// Token: 0x06000308 RID: 776 RVA: 0x0000EF28 File Offset: 0x0000D128
			public HeapNode(uint pathNodeIndex, uint g, uint f)
			{
				this.pathNodeIndex = pathNodeIndex;
				this.sortKey = ((ulong)f << 32) | (ulong)g;
			}

			// Token: 0x1700009B RID: 155
			// (get) Token: 0x06000309 RID: 777 RVA: 0x0000EF3F File Offset: 0x0000D13F
			// (set) Token: 0x0600030A RID: 778 RVA: 0x0000EF4B File Offset: 0x0000D14B
			public uint F
			{
				get
				{
					return (uint)(this.sortKey >> 32);
				}
				set
				{
					this.sortKey = (this.sortKey & (ulong)(-1)) | ((ulong)value << 32);
				}
			}

			// Token: 0x1700009C RID: 156
			// (get) Token: 0x0600030B RID: 779 RVA: 0x0000EF62 File Offset: 0x0000D162
			public uint G
			{
				get
				{
					return (uint)this.sortKey;
				}
			}

			// Token: 0x040001E2 RID: 482
			public uint pathNodeIndex;

			// Token: 0x040001E3 RID: 483
			public ulong sortKey;
		}

		// Token: 0x02000056 RID: 86
		// (Invoke) Token: 0x0600030D RID: 781
		public delegate void Add_000002DF$PostfixBurstDelegate(ref BinaryHeap binaryHeap, ref UnsafeSpan<PathNode> nodes, uint pathNodeIndex, uint g, uint f);

		// Token: 0x02000057 RID: 87
		internal static class Add_000002DF$BurstDirectCall
		{
			// Token: 0x06000310 RID: 784 RVA: 0x0000EF6B File Offset: 0x0000D16B
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (BinaryHeap.Add_000002DF$BurstDirectCall.Pointer == 0)
				{
					BinaryHeap.Add_000002DF$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(BinaryHeap.Add_000002DF$BurstDirectCall.DeferredCompilation, methodof(BinaryHeap.Add$BurstManaged(ref BinaryHeap, ref UnsafeSpan<PathNode>, uint, uint, uint)).MethodHandle, typeof(BinaryHeap.Add_000002DF$PostfixBurstDelegate).TypeHandle);
				}
				A_0 = BinaryHeap.Add_000002DF$BurstDirectCall.Pointer;
			}

			// Token: 0x06000311 RID: 785 RVA: 0x0000EF98 File Offset: 0x0000D198
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				BinaryHeap.Add_000002DF$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x06000312 RID: 786 RVA: 0x0000EFB0 File Offset: 0x0000D1B0
			public static void Constructor()
			{
				BinaryHeap.Add_000002DF$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(BinaryHeap.Add(ref BinaryHeap, ref UnsafeSpan<PathNode>, uint, uint, uint)).MethodHandle);
			}

			// Token: 0x06000313 RID: 787 RVA: 0x000033F6 File Offset: 0x000015F6
			public static void Initialize()
			{
			}

			// Token: 0x06000314 RID: 788 RVA: 0x0000EFC1 File Offset: 0x0000D1C1
			// Note: this type is marked as 'beforefieldinit'.
			static Add_000002DF$BurstDirectCall()
			{
				BinaryHeap.Add_000002DF$BurstDirectCall.Constructor();
			}

			// Token: 0x06000315 RID: 789 RVA: 0x0000EFC8 File Offset: 0x0000D1C8
			public static void Invoke(ref BinaryHeap binaryHeap, ref UnsafeSpan<PathNode> nodes, uint pathNodeIndex, uint g, uint f)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = BinaryHeap.Add_000002DF$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						calli(System.Void(Pathfinding.BinaryHeap&,Pathfinding.Util.UnsafeSpan`1<Pathfinding.PathNode>&,System.UInt32,System.UInt32,System.UInt32), ref binaryHeap, ref nodes, pathNodeIndex, g, f, functionPointer);
						return;
					}
				}
				BinaryHeap.Add$BurstManaged(ref binaryHeap, ref nodes, pathNodeIndex, g, f);
			}

			// Token: 0x040001E4 RID: 484
			private static IntPtr Pointer;

			// Token: 0x040001E5 RID: 485
			private static IntPtr DeferredCompilation;
		}

		// Token: 0x02000058 RID: 88
		// (Invoke) Token: 0x06000317 RID: 791
		public delegate uint Remove_000002E2$PostfixBurstDelegate(ref UnsafeSpan<PathNode> nodes, ref BinaryHeap binaryHeap, [NoAlias] out uint removedG, [NoAlias] out uint removedF);

		// Token: 0x02000059 RID: 89
		internal static class Remove_000002E2$BurstDirectCall
		{
			// Token: 0x0600031A RID: 794 RVA: 0x0000F003 File Offset: 0x0000D203
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (BinaryHeap.Remove_000002E2$BurstDirectCall.Pointer == 0)
				{
					BinaryHeap.Remove_000002E2$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(BinaryHeap.Remove_000002E2$BurstDirectCall.DeferredCompilation, methodof(BinaryHeap.Remove$BurstManaged(ref UnsafeSpan<PathNode>, ref BinaryHeap, ref uint, ref uint)).MethodHandle, typeof(BinaryHeap.Remove_000002E2$PostfixBurstDelegate).TypeHandle);
				}
				A_0 = BinaryHeap.Remove_000002E2$BurstDirectCall.Pointer;
			}

			// Token: 0x0600031B RID: 795 RVA: 0x0000F030 File Offset: 0x0000D230
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				BinaryHeap.Remove_000002E2$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x0600031C RID: 796 RVA: 0x0000F048 File Offset: 0x0000D248
			public static void Constructor()
			{
				BinaryHeap.Remove_000002E2$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(BinaryHeap.Remove(ref UnsafeSpan<PathNode>, ref BinaryHeap, ref uint, ref uint)).MethodHandle);
			}

			// Token: 0x0600031D RID: 797 RVA: 0x000033F6 File Offset: 0x000015F6
			public static void Initialize()
			{
			}

			// Token: 0x0600031E RID: 798 RVA: 0x0000F059 File Offset: 0x0000D259
			// Note: this type is marked as 'beforefieldinit'.
			static Remove_000002E2$BurstDirectCall()
			{
				BinaryHeap.Remove_000002E2$BurstDirectCall.Constructor();
			}

			// Token: 0x0600031F RID: 799 RVA: 0x0000F060 File Offset: 0x0000D260
			public static uint Invoke(ref UnsafeSpan<PathNode> nodes, ref BinaryHeap binaryHeap, [NoAlias] out uint removedG, [NoAlias] out uint removedF)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = BinaryHeap.Remove_000002E2$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						return calli(System.UInt32(Pathfinding.Util.UnsafeSpan`1<Pathfinding.PathNode>&,Pathfinding.BinaryHeap&,System.UInt32&,System.UInt32&), ref nodes, ref binaryHeap, ref removedG, ref removedF, functionPointer);
					}
				}
				return BinaryHeap.Remove$BurstManaged(ref nodes, ref binaryHeap, out removedG, out removedF);
			}

			// Token: 0x040001E6 RID: 486
			private static IntPtr Pointer;

			// Token: 0x040001E7 RID: 487
			private static IntPtr DeferredCompilation;
		}
	}
}
