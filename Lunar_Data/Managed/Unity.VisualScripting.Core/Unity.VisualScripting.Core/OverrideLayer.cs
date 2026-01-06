using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200015C RID: 348
	public struct OverrideLayer<T> : IDisposable
	{
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x000281AF File Offset: 0x000263AF
		public readonly OverrideStack<T> stack { get; }

		// Token: 0x0600093E RID: 2366 RVA: 0x000281B7 File Offset: 0x000263B7
		internal OverrideLayer(OverrideStack<T> stack, T item)
		{
			Ensure.That("stack").IsNotNull<OverrideStack<T>>(stack);
			this.stack = stack;
			stack.BeginOverride(item);
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x000281D7 File Offset: 0x000263D7
		public void Dispose()
		{
			this.stack.EndOverride();
		}
	}
}
