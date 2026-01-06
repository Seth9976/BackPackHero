using System;
using System.Collections;

namespace UnityEngine
{
	// Token: 0x020001FF RID: 511
	public abstract class CustomYieldInstruction : IEnumerator
	{
		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060016A0 RID: 5792
		public abstract bool keepWaiting { get; }

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x00024220 File Offset: 0x00022420
		public object Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x00024234 File Offset: 0x00022434
		public bool MoveNext()
		{
			return this.keepWaiting;
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x00004557 File Offset: 0x00002757
		public virtual void Reset()
		{
		}
	}
}
