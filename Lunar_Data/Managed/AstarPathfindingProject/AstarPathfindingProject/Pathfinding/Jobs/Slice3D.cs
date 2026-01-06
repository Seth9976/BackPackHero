using System;
using System.Runtime.CompilerServices;
using Pathfinding.Util;
using Unity.Collections;
using Unity.Mathematics;

namespace Pathfinding.Jobs
{
	// Token: 0x0200016A RID: 362
	public readonly struct Slice3D
	{
		// Token: 0x06000A66 RID: 2662 RVA: 0x0003B6F8 File Offset: 0x000398F8
		public Slice3D(IntBounds outer, IntBounds slice)
		{
			this = new Slice3D(outer.size, slice.Offset(-outer.min));
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0003B719 File Offset: 0x00039919
		public Slice3D(int3 outerSize, IntBounds slice)
		{
			this.outerSize = outerSize;
			this.slice = slice;
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x000033F6 File Offset: 0x000015F6
		public void AssertMatchesOuter<[IsUnmanaged] T>(UnsafeSpan<T> values) where T : struct, ValueType
		{
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x000033F6 File Offset: 0x000015F6
		public void AssertMatchesOuter<T>(NativeArray<T> values) where T : struct
		{
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x000033F6 File Offset: 0x000015F6
		public void AssertMatchesInner<T>(NativeArray<T> values) where T : struct
		{
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x000033F6 File Offset: 0x000015F6
		public void AssertSameSize(Slice3D other)
		{
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0003B72C File Offset: 0x0003992C
		public int InnerCoordinateToOuterIndex(int x, int y, int z)
		{
			ValueTuple<int, int, int> outerStrides = this.outerStrides;
			int item = outerStrides.Item1;
			int item2 = outerStrides.Item2;
			int item3 = outerStrides.Item3;
			return (x + this.slice.min.x) * item + (y + this.slice.min.y) * item2 + (z + this.slice.min.z) * item3;
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x0003B794 File Offset: 0x00039994
		public int length
		{
			get
			{
				return this.slice.size.x * this.slice.size.y * this.slice.size.z;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x0003B7DC File Offset: 0x000399DC
		public ValueTuple<int, int, int> outerStrides
		{
			get
			{
				return new ValueTuple<int, int, int>(1, this.outerSize.x * this.outerSize.z, this.outerSize.x);
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x0003B808 File Offset: 0x00039A08
		public ValueTuple<int, int, int> innerStrides
		{
			get
			{
				return new ValueTuple<int, int, int>(1, this.slice.size.x * this.slice.size.z, this.slice.size.x);
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x0003B858 File Offset: 0x00039A58
		public int outerStartIndex
		{
			get
			{
				ValueTuple<int, int, int> outerStrides = this.outerStrides;
				int item = outerStrides.Item1;
				int item2 = outerStrides.Item2;
				int item3 = outerStrides.Item3;
				return this.slice.min.x * item + this.slice.min.y * item2 + this.slice.min.z * item3;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x0003B8B8 File Offset: 0x00039AB8
		public bool coversEverything
		{
			get
			{
				return math.all(this.slice.size == this.outerSize);
			}
		}

		// Token: 0x0400070D RID: 1805
		public readonly int3 outerSize;

		// Token: 0x0400070E RID: 1806
		public readonly IntBounds slice;
	}
}
