using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000ED RID: 237
	public abstract class OperatorHandler
	{
		// Token: 0x0600063B RID: 1595 RVA: 0x0001C190 File Offset: 0x0001A390
		protected OperatorHandler(string name, string verb, string symbol, string customMethodName)
		{
			Ensure.That("name").IsNotNull(name);
			Ensure.That("verb").IsNotNull(verb);
			Ensure.That("symbol").IsNotNull(symbol);
			this.name = name;
			this.verb = verb;
			this.symbol = symbol;
			this.customMethodName = customMethodName;
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x0001C1F0 File Offset: 0x0001A3F0
		public string name { get; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x0001C1F8 File Offset: 0x0001A3F8
		public string verb { get; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x0001C200 File Offset: 0x0001A400
		public string symbol { get; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x0001C208 File Offset: 0x0001A408
		public string customMethodName { get; }
	}
}
