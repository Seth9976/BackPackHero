using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000042 RID: 66
	internal class TimeFieldAttribute : PropertyAttribute
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x00009A05 File Offset: 0x00007C05
		public TimeFieldAttribute.UseEditMode useEditMode { get; }

		// Token: 0x060002B7 RID: 695 RVA: 0x00009A0D File Offset: 0x00007C0D
		public TimeFieldAttribute(TimeFieldAttribute.UseEditMode useEditMode = TimeFieldAttribute.UseEditMode.ApplyEditMode)
		{
			this.useEditMode = useEditMode;
		}

		// Token: 0x02000075 RID: 117
		public enum UseEditMode
		{
			// Token: 0x04000177 RID: 375
			None,
			// Token: 0x04000178 RID: 376
			ApplyEditMode
		}
	}
}
