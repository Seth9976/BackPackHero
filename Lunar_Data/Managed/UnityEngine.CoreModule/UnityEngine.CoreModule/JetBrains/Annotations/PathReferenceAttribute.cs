using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000CA RID: 202
	[AttributeUsage(2048)]
	public sealed class PathReferenceAttribute : Attribute
	{
		// Token: 0x06000367 RID: 871 RVA: 0x00002059 File Offset: 0x00000259
		public PathReferenceAttribute()
		{
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00005D90 File Offset: 0x00003F90
		public PathReferenceAttribute([NotNull] [PathReference] string basePath)
		{
			this.BasePath = basePath;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00005DA1 File Offset: 0x00003FA1
		[CanBeNull]
		public string BasePath { get; }
	}
}
