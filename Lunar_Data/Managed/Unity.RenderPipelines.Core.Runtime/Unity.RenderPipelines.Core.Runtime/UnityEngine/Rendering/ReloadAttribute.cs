using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000AF RID: 175
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class ReloadAttribute : Attribute
	{
		// Token: 0x060005EE RID: 1518 RVA: 0x0001C413 File Offset: 0x0001A613
		public ReloadAttribute(string[] paths, ReloadAttribute.Package package = ReloadAttribute.Package.Root)
		{
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001C41B File Offset: 0x0001A61B
		public ReloadAttribute(string path, ReloadAttribute.Package package = ReloadAttribute.Package.Root)
			: this(new string[] { path }, package)
		{
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001C42E File Offset: 0x0001A62E
		public ReloadAttribute(string pathFormat, int rangeMin, int rangeMax, ReloadAttribute.Package package = ReloadAttribute.Package.Root)
		{
		}

		// Token: 0x0200017B RID: 379
		public enum Package
		{
			// Token: 0x040005B7 RID: 1463
			Builtin,
			// Token: 0x040005B8 RID: 1464
			Root
		}
	}
}
