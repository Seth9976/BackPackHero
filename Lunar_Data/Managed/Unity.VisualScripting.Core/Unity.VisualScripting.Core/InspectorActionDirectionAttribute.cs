using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000037 RID: 55
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
	public sealed class InspectorActionDirectionAttribute : Attribute
	{
		// Token: 0x060001BC RID: 444 RVA: 0x00004D00 File Offset: 0x00002F00
		public InspectorActionDirectionAttribute(ActionDirection direction)
		{
			this.direction = direction;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00004D0F File Offset: 0x00002F0F
		// (set) Token: 0x060001BE RID: 446 RVA: 0x00004D17 File Offset: 0x00002F17
		public ActionDirection direction { get; private set; }
	}
}
