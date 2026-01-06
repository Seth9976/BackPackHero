using System;

namespace Unity.Burst.Intrinsics
{
	// Token: 0x0200001A RID: 26
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	internal sealed class BurstTargetCpuAttribute : Attribute
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00005685 File Offset: 0x00003885
		public BurstTargetCpuAttribute(BurstTargetCpu TargetCpu)
		{
			this.TargetCpu = TargetCpu;
		}

		// Token: 0x0400013E RID: 318
		public readonly BurstTargetCpu TargetCpu;
	}
}
