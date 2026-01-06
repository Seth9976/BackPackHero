using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000038 RID: 56
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class InspectorAdaptiveWidthAttribute : Attribute
	{
		// Token: 0x060001BF RID: 447 RVA: 0x00004D20 File Offset: 0x00002F20
		public InspectorAdaptiveWidthAttribute(float width)
		{
			this.width = width;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00004D2F File Offset: 0x00002F2F
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x00004D37 File Offset: 0x00002F37
		public float width { get; private set; }
	}
}
