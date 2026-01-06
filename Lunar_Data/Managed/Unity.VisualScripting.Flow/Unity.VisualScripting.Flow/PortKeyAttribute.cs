using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200000B RID: 11
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
	public class PortKeyAttribute : Attribute
	{
		// Token: 0x0600004B RID: 75 RVA: 0x0000275C File Offset: 0x0000095C
		public PortKeyAttribute(string key)
		{
			Ensure.That("key").IsNotNull(key);
			this.key = key;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600004C RID: 76 RVA: 0x0000277B File Offset: 0x0000097B
		public string key { get; }
	}
}
