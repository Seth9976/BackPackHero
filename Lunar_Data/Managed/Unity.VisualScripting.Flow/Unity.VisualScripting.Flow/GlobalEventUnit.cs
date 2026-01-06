using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200005D RID: 93
	public abstract class GlobalEventUnit<TArgs> : EventUnit<TArgs>
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000383 RID: 899 RVA: 0x00008E09 File Offset: 0x00007009
		protected override bool register
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000384 RID: 900 RVA: 0x00008E0C File Offset: 0x0000700C
		protected virtual string hookName
		{
			get
			{
				throw new InvalidImplementationException();
			}
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00008E13 File Offset: 0x00007013
		public override EventHook GetHook(GraphReference reference)
		{
			return this.hookName;
		}
	}
}
