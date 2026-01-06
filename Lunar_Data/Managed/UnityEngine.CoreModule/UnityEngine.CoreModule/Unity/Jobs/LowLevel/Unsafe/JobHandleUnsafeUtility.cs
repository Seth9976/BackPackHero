using System;

namespace Unity.Jobs.LowLevel.Unsafe
{
	// Token: 0x02000066 RID: 102
	public static class JobHandleUnsafeUtility
	{
		// Token: 0x0600018B RID: 395 RVA: 0x00003738 File Offset: 0x00001938
		public unsafe static JobHandle CombineDependencies(JobHandle* jobs, int count)
		{
			return JobHandle.CombineDependenciesInternalPtr((void*)jobs, count);
		}
	}
}
