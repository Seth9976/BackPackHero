using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200003C RID: 60
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
	public sealed class InspectorRangeAttribute : Attribute
	{
		// Token: 0x060001CC RID: 460 RVA: 0x00004DA8 File Offset: 0x00002FA8
		public InspectorRangeAttribute(float min, float max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00004DBE File Offset: 0x00002FBE
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00004DC6 File Offset: 0x00002FC6
		public float min { get; private set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00004DCF File Offset: 0x00002FCF
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x00004DD7 File Offset: 0x00002FD7
		public float max { get; private set; }
	}
}
