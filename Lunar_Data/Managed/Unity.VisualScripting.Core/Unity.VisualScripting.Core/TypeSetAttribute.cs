using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000046 RID: 70
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class TypeSetAttribute : Attribute
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x00004ECD File Offset: 0x000030CD
		public TypeSetAttribute(TypeSet typeSet)
		{
			this.typeSet = typeSet;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00004EDC File Offset: 0x000030DC
		public TypeSet typeSet { get; }
	}
}
