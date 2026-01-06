using System;

namespace Unity.Burst.Intrinsics
{
	// Token: 0x0200001B RID: 27
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	internal sealed class BurstTargetCpuAttribute : Attribute
	{
		// Token: 0x060000BE RID: 190 RVA: 0x000056BD File Offset: 0x000038BD
		public BurstTargetCpuAttribute(BurstTargetCpu TargetCpu)
		{
			this.TargetCpu = TargetCpu;
		}

		// Token: 0x04000141 RID: 321
		public readonly BurstTargetCpu TargetCpu;
	}
}
