using System;

namespace CleverCrow.Fluid.Dialogues
{
	// Token: 0x02000004 RID: 4
	public class CreateMenuAttribute : Attribute
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000208B File Offset: 0x0000028B
		public string Path { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002093 File Offset: 0x00000293
		public int Priority { get; }

		// Token: 0x0600000E RID: 14 RVA: 0x0000209B File Offset: 0x0000029B
		public CreateMenuAttribute(string path, int priority = 0)
		{
			this.Path = path;
			this.Priority = priority;
		}
	}
}
