using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000C2 RID: 194
	[AttributeUsage(18436)]
	public sealed class MeansImplicitUseAttribute : Attribute
	{
		// Token: 0x06000358 RID: 856 RVA: 0x00005D12 File Offset: 0x00003F12
		public MeansImplicitUseAttribute()
			: this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00005D1E File Offset: 0x00003F1E
		public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags)
			: this(useKindFlags, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00005D2A File Offset: 0x00003F2A
		public MeansImplicitUseAttribute(ImplicitUseTargetFlags targetFlags)
			: this(ImplicitUseKindFlags.Default, targetFlags)
		{
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00005D36 File Offset: 0x00003F36
		public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
		{
			this.UseKindFlags = useKindFlags;
			this.TargetFlags = targetFlags;
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00005D4E File Offset: 0x00003F4E
		[UsedImplicitly]
		public ImplicitUseKindFlags UseKindFlags { get; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00005D56 File Offset: 0x00003F56
		[UsedImplicitly]
		public ImplicitUseTargetFlags TargetFlags { get; }
	}
}
