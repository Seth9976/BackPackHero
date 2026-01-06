using System;

namespace System.Threading
{
	// Token: 0x02000283 RID: 643
	public readonly struct AsyncLocalValueChangedArgs<T>
	{
		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06001D60 RID: 7520 RVA: 0x0006DB1E File Offset: 0x0006BD1E
		public T PreviousValue { get; }

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06001D61 RID: 7521 RVA: 0x0006DB26 File Offset: 0x0006BD26
		public T CurrentValue { get; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06001D62 RID: 7522 RVA: 0x0006DB2E File Offset: 0x0006BD2E
		public bool ThreadContextChanged { get; }

		// Token: 0x06001D63 RID: 7523 RVA: 0x0006DB36 File Offset: 0x0006BD36
		internal AsyncLocalValueChangedArgs(T previousValue, T currentValue, bool contextChanged)
		{
			this = default(AsyncLocalValueChangedArgs<T>);
			this.PreviousValue = previousValue;
			this.CurrentValue = currentValue;
			this.ThreadContextChanged = contextChanged;
		}
	}
}
