using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001F1 RID: 497
	[AttributeUsage(64, AllowMultiple = true)]
	[RequiredByNativeCode]
	public sealed class ContextMenu : Attribute
	{
		// Token: 0x06001657 RID: 5719 RVA: 0x00023C6B File Offset: 0x00021E6B
		public ContextMenu(string itemName)
			: this(itemName, false)
		{
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x00023C77 File Offset: 0x00021E77
		public ContextMenu(string itemName, bool isValidateFunction)
			: this(itemName, isValidateFunction, 1000000)
		{
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x00023C88 File Offset: 0x00021E88
		public ContextMenu(string itemName, bool isValidateFunction, int priority)
		{
			this.menuItem = itemName;
			this.validate = isValidateFunction;
			this.priority = priority;
		}

		// Token: 0x040007D0 RID: 2000
		public readonly string menuItem;

		// Token: 0x040007D1 RID: 2001
		public readonly bool validate;

		// Token: 0x040007D2 RID: 2002
		public readonly int priority;
	}
}
