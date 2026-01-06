using System;

namespace System
{
	// Token: 0x0200001F RID: 31
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x0000572A File Offset: 0x0000392A
		public MonoTODOAttribute()
		{
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00005732 File Offset: 0x00003932
		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00005741 File Offset: 0x00003941
		public string Comment
		{
			get
			{
				return this.comment;
			}
		}

		// Token: 0x04000428 RID: 1064
		private string comment;
	}
}
