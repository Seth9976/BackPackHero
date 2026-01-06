using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000C5 RID: 197
	[AttributeUsage(32767, Inherited = false)]
	[MeansImplicitUse(ImplicitUseTargetFlags.WithMembers)]
	public sealed class PublicAPIAttribute : Attribute
	{
		// Token: 0x0600035E RID: 862 RVA: 0x00002059 File Offset: 0x00000259
		public PublicAPIAttribute()
		{
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00005D5E File Offset: 0x00003F5E
		public PublicAPIAttribute([NotNull] string comment)
		{
			this.Comment = comment;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000360 RID: 864 RVA: 0x00005D6F File Offset: 0x00003F6F
		[CanBeNull]
		public string Comment { get; }
	}
}
