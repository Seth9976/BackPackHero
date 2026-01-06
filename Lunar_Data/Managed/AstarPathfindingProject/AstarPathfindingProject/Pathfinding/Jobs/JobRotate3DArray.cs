using System;
using System.Runtime.CompilerServices;
using Pathfinding.Util;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace Pathfinding.Jobs
{
	// Token: 0x02000174 RID: 372
	[BurstCompile]
	public struct JobRotate3DArray<[IsUnmanaged] T> : IJob where T : struct, ValueType
	{
		// Token: 0x06000A82 RID: 2690 RVA: 0x0003BC9C File Offset: 0x00039E9C
		public void Execute()
		{
			int x = this.size.x;
			int y = this.size.y;
			int z = this.size.z;
			UnsafeSpan<T> unsafeSpan = this.arr.AsUnsafeSpan<T>();
			this.dx %= x;
			this.dz %= z;
			if (this.dx != 0)
			{
				if (this.dx < 0)
				{
					this.dx = x + this.dx;
				}
				UnsafeSpan<T> unsafeSpan2 = new NativeArray<T>(this.dx, Allocator.Temp, NativeArrayOptions.ClearMemory).AsUnsafeSpan<T>();
				for (int i = 0; i < y; i++)
				{
					int num = i * x * z;
					for (int j = 0; j < z; j++)
					{
						unsafeSpan.Slice(num + j * x + x - this.dx, this.dx).CopyTo(unsafeSpan2);
						unsafeSpan.Move(num + j * x, num + j * x + this.dx, x - this.dx);
						unsafeSpan2.CopyTo(unsafeSpan.Slice(num + j * x, this.dx));
					}
				}
			}
			if (this.dz != 0)
			{
				if (this.dz < 0)
				{
					this.dz = z + this.dz;
				}
				UnsafeSpan<T> unsafeSpan3 = new NativeArray<T>(this.dz * x, Allocator.Temp, NativeArrayOptions.ClearMemory).AsUnsafeSpan<T>();
				for (int k = 0; k < y; k++)
				{
					int num2 = k * x * z;
					unsafeSpan.Slice(num2 + (z - this.dz) * x, this.dz * x).CopyTo(unsafeSpan3);
					unsafeSpan.Move(num2, num2 + this.dz * x, (z - this.dz) * x);
					unsafeSpan3.CopyTo(unsafeSpan.Slice(num2, this.dz * x));
				}
			}
		}

		// Token: 0x04000725 RID: 1829
		public NativeArray<T> arr;

		// Token: 0x04000726 RID: 1830
		public int3 size;

		// Token: 0x04000727 RID: 1831
		public int dx;

		// Token: 0x04000728 RID: 1832
		public int dz;
	}
}
