using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000032 RID: 50
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class ExpectedTypeAttribute : Attribute
	{
		// Token: 0x060001AF RID: 431 RVA: 0x00004C78 File Offset: 0x00002E78
		public ExpectedTypeAttribute(Type type)
		{
			Ensure.That("type").IsNotNull<Type>(type);
			this.type = type;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00004C97 File Offset: 0x00002E97
		public Type type { get; }
	}
}
