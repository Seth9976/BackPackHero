using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002CC RID: 716
	public abstract class TypedUxmlAttributeDescription<T> : UxmlAttributeDescription
	{
		// Token: 0x060017E6 RID: 6118
		public abstract T GetValueFromBag(IUxmlAttributes bag, CreationContext cc);

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x0006077F File Offset: 0x0005E97F
		// (set) Token: 0x060017E8 RID: 6120 RVA: 0x00060787 File Offset: 0x0005E987
		public T defaultValue { get; set; }

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060017E9 RID: 6121 RVA: 0x00060790 File Offset: 0x0005E990
		public override string defaultValueAsString
		{
			get
			{
				T defaultValue = this.defaultValue;
				return defaultValue.ToString();
			}
		}
	}
}
