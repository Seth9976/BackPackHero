using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000C8 RID: 200
	[AttributeUsage(64)]
	public sealed class MustUseReturnValueAttribute : Attribute
	{
		// Token: 0x06000363 RID: 867 RVA: 0x00002059 File Offset: 0x00000259
		public MustUseReturnValueAttribute()
		{
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00005D77 File Offset: 0x00003F77
		public MustUseReturnValueAttribute([NotNull] string justification)
		{
			this.Justification = justification;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000365 RID: 869 RVA: 0x00005D88 File Offset: 0x00003F88
		[CanBeNull]
		public string Justification { get; }
	}
}
