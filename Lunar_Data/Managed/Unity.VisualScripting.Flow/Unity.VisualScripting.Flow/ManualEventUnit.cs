using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200008F RID: 143
	public abstract class ManualEventUnit<TArgs> : EventUnit<TArgs>
	{
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x000096FD File Offset: 0x000078FD
		protected sealed override bool register
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000442 RID: 1090
		protected abstract string hookName { get; }

		// Token: 0x06000443 RID: 1091 RVA: 0x00009700 File Offset: 0x00007900
		public sealed override EventHook GetHook(GraphReference reference)
		{
			return this.hookName;
		}
	}
}
