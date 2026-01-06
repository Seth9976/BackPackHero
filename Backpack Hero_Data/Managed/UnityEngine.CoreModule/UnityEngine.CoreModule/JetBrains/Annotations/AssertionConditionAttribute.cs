using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000D0 RID: 208
	[AttributeUsage(2048)]
	public sealed class AssertionConditionAttribute : Attribute
	{
		// Token: 0x06000375 RID: 885 RVA: 0x00005DF5 File Offset: 0x00003FF5
		public AssertionConditionAttribute(AssertionConditionType conditionType)
		{
			this.ConditionType = conditionType;
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000376 RID: 886 RVA: 0x00005E06 File Offset: 0x00004006
		public AssertionConditionType ConditionType { get; }
	}
}
