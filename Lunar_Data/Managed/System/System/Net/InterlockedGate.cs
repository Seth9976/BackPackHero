using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x020003F0 RID: 1008
	internal struct InterlockedGate
	{
		// Token: 0x060020BB RID: 8379 RVA: 0x00077C40 File Offset: 0x00075E40
		internal void Reset()
		{
			this.m_State = 0;
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x00077C4C File Offset: 0x00075E4C
		internal bool Trigger(bool exclusive)
		{
			int num = Interlocked.CompareExchange(ref this.m_State, 2, 0);
			if (exclusive && (num == 1 || num == 2))
			{
				throw new InternalException();
			}
			return num == 0;
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x00077C7C File Offset: 0x00075E7C
		internal bool StartTriggering(bool exclusive)
		{
			int num = Interlocked.CompareExchange(ref this.m_State, 1, 0);
			if (exclusive && (num == 1 || num == 2))
			{
				throw new InternalException();
			}
			return num == 0;
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x00077CAC File Offset: 0x00075EAC
		internal void FinishTriggering()
		{
			if (Interlocked.CompareExchange(ref this.m_State, 2, 1) != 1)
			{
				throw new InternalException();
			}
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x00077CC4 File Offset: 0x00075EC4
		internal bool StartSignaling(bool exclusive)
		{
			int num = Interlocked.CompareExchange(ref this.m_State, 3, 2);
			if (exclusive && (num == 3 || num == 4))
			{
				throw new InternalException();
			}
			return num == 2;
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x00077CF4 File Offset: 0x00075EF4
		internal void FinishSignaling()
		{
			if (Interlocked.CompareExchange(ref this.m_State, 4, 3) != 3)
			{
				throw new InternalException();
			}
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x00077D0C File Offset: 0x00075F0C
		internal bool Complete()
		{
			return Interlocked.CompareExchange(ref this.m_State, 5, 4) == 4;
		}

		// Token: 0x040011D6 RID: 4566
		private int m_State;

		// Token: 0x040011D7 RID: 4567
		internal const int Open = 0;

		// Token: 0x040011D8 RID: 4568
		internal const int Triggering = 1;

		// Token: 0x040011D9 RID: 4569
		internal const int Triggered = 2;

		// Token: 0x040011DA RID: 4570
		internal const int Signaling = 3;

		// Token: 0x040011DB RID: 4571
		internal const int Signaled = 4;

		// Token: 0x040011DC RID: 4572
		internal const int Completed = 5;
	}
}
