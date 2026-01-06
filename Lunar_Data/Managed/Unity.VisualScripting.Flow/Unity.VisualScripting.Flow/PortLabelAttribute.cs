using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200000C RID: 12
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
	public class PortLabelAttribute : Attribute
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002783 File Offset: 0x00000983
		public PortLabelAttribute(string label)
		{
			this.label = label;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002792 File Offset: 0x00000992
		// (set) Token: 0x0600004F RID: 79 RVA: 0x0000279A File Offset: 0x0000099A
		public string label { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000027A3 File Offset: 0x000009A3
		// (set) Token: 0x06000051 RID: 81 RVA: 0x000027AB File Offset: 0x000009AB
		public bool hidden { get; set; }
	}
}
