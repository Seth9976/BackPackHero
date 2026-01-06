using System;

namespace Febucci.UI.Core
{
	// Token: 0x02000043 RID: 67
	[AttributeUsage(AttributeTargets.Class)]
	public class TagInfoAttribute : Attribute
	{
		// Token: 0x0600016C RID: 364 RVA: 0x00006EB9 File Offset: 0x000050B9
		public TagInfoAttribute(string tagID)
		{
			this.tagID = tagID;
		}

		// Token: 0x04000103 RID: 259
		public readonly string tagID;
	}
}
