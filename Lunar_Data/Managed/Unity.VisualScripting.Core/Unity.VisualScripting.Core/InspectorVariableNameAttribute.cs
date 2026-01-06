using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000169 RID: 361
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
	public sealed class InspectorVariableNameAttribute : Attribute
	{
		// Token: 0x060009A0 RID: 2464 RVA: 0x00029020 File Offset: 0x00027220
		public InspectorVariableNameAttribute(ActionDirection direction)
		{
			this.direction = direction;
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x0002902F File Offset: 0x0002722F
		// (set) Token: 0x060009A2 RID: 2466 RVA: 0x00029037 File Offset: 0x00027237
		public ActionDirection direction { get; private set; }
	}
}
