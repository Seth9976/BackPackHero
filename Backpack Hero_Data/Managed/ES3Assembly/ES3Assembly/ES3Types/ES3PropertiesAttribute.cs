using System;

namespace ES3Types
{
	// Token: 0x02000024 RID: 36
	[AttributeUsage(AttributeTargets.Class)]
	public class ES3PropertiesAttribute : Attribute
	{
		// Token: 0x06000229 RID: 553 RVA: 0x000085C7 File Offset: 0x000067C7
		public ES3PropertiesAttribute(params string[] members)
		{
			this.members = members;
		}

		// Token: 0x0400005C RID: 92
		public readonly string[] members;
	}
}
