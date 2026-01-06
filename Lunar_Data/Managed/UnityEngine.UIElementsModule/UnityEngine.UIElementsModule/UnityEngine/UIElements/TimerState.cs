using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000065 RID: 101
	public struct TimerState : IEquatable<TimerState>
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000AA06 File Offset: 0x00008C06
		// (set) Token: 0x060002DA RID: 730 RVA: 0x0000AA0E File Offset: 0x00008C0E
		public long start { readonly get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000AA17 File Offset: 0x00008C17
		// (set) Token: 0x060002DC RID: 732 RVA: 0x0000AA1F File Offset: 0x00008C1F
		public long now { readonly get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000AA28 File Offset: 0x00008C28
		public long deltaTime
		{
			get
			{
				return this.now - this.start;
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000AA48 File Offset: 0x00008C48
		public override bool Equals(object obj)
		{
			return obj is TimerState && this.Equals((TimerState)obj);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000AA74 File Offset: 0x00008C74
		public bool Equals(TimerState other)
		{
			return this.start == other.start && this.now == other.now && this.deltaTime == other.deltaTime;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000AAB8 File Offset: 0x00008CB8
		public override int GetHashCode()
		{
			int num = 540054806;
			num = num * -1521134295 + this.start.GetHashCode();
			num = num * -1521134295 + this.now.GetHashCode();
			return num * -1521134295 + this.deltaTime.GetHashCode();
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000AB18 File Offset: 0x00008D18
		public static bool operator ==(TimerState state1, TimerState state2)
		{
			return state1.Equals(state2);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000AB34 File Offset: 0x00008D34
		public static bool operator !=(TimerState state1, TimerState state2)
		{
			return !(state1 == state2);
		}
	}
}
