using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace Pathfinding.Jobs
{
	// Token: 0x0200016C RID: 364
	[BurstCompile]
	public struct JobCopyRectangle<T> : IJob where T : struct
	{
		// Token: 0x06000A78 RID: 2680 RVA: 0x0003B9E2 File Offset: 0x00039BE2
		public void Execute()
		{
			JobCopyRectangle<T>.Copy(this.input, this.output, this.inputSlice, this.outputSlice);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0003BA04 File Offset: 0x00039C04
		public static void Copy(NativeArray<T> input, NativeArray<T> output, Slice3D inputSlice, Slice3D outputSlice)
		{
			inputSlice.AssertMatchesOuter<T>(input);
			outputSlice.AssertMatchesOuter<T>(output);
			inputSlice.AssertSameSize(outputSlice);
			if (inputSlice.coversEverything && outputSlice.coversEverything)
			{
				input.CopyTo(output);
				return;
			}
			for (int i = 0; i < outputSlice.slice.size.y; i++)
			{
				for (int j = 0; j < outputSlice.slice.size.z; j++)
				{
					int num = inputSlice.InnerCoordinateToOuterIndex(0, i, j);
					int num2 = outputSlice.InnerCoordinateToOuterIndex(0, i, j);
					NativeArray<T>.Copy(input, num, output, num2, outputSlice.slice.size.x);
				}
			}
		}

		// Token: 0x0400070F RID: 1807
		[ReadOnly]
		[DisableUninitializedReadCheck]
		public NativeArray<T> input;

		// Token: 0x04000710 RID: 1808
		[WriteOnly]
		public NativeArray<T> output;

		// Token: 0x04000711 RID: 1809
		public Slice3D inputSlice;

		// Token: 0x04000712 RID: 1810
		public Slice3D outputSlice;
	}
}
