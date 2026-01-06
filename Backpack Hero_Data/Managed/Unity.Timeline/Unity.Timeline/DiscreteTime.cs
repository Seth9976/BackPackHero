using System;

namespace UnityEngine.Timeline
{
	// Token: 0x0200001B RID: 27
	internal struct DiscreteTime : IComparable
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00007428 File Offset: 0x00005628
		public static double tickValue
		{
			get
			{
				return 1E-12;
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00007433 File Offset: 0x00005633
		public DiscreteTime(DiscreteTime time)
		{
			this.m_DiscreteTime = time.m_DiscreteTime;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00007441 File Offset: 0x00005641
		private DiscreteTime(long time)
		{
			this.m_DiscreteTime = time;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000744A File Offset: 0x0000564A
		public DiscreteTime(double time)
		{
			this.m_DiscreteTime = DiscreteTime.DoubleToDiscreteTime(time);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00007458 File Offset: 0x00005658
		public DiscreteTime(float time)
		{
			this.m_DiscreteTime = DiscreteTime.FloatToDiscreteTime(time);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00007466 File Offset: 0x00005666
		public DiscreteTime(int time)
		{
			this.m_DiscreteTime = DiscreteTime.IntToDiscreteTime(time);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00007474 File Offset: 0x00005674
		public DiscreteTime(int frame, double fps)
		{
			this.m_DiscreteTime = DiscreteTime.DoubleToDiscreteTime((double)frame * fps);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00007485 File Offset: 0x00005685
		public DiscreteTime OneTickBefore()
		{
			return new DiscreteTime(this.m_DiscreteTime - 1L);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00007495 File Offset: 0x00005695
		public DiscreteTime OneTickAfter()
		{
			return new DiscreteTime(this.m_DiscreteTime + 1L);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000074A5 File Offset: 0x000056A5
		public long GetTick()
		{
			return this.m_DiscreteTime;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000074AD File Offset: 0x000056AD
		public static DiscreteTime FromTicks(long ticks)
		{
			return new DiscreteTime(ticks);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000074B5 File Offset: 0x000056B5
		public int CompareTo(object obj)
		{
			if (obj is DiscreteTime)
			{
				return this.m_DiscreteTime.CompareTo(((DiscreteTime)obj).m_DiscreteTime);
			}
			return 1;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000074D7 File Offset: 0x000056D7
		public bool Equals(DiscreteTime other)
		{
			return this.m_DiscreteTime == other.m_DiscreteTime;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000074E7 File Offset: 0x000056E7
		public override bool Equals(object obj)
		{
			return obj is DiscreteTime && this.Equals((DiscreteTime)obj);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00007500 File Offset: 0x00005700
		private static long DoubleToDiscreteTime(double time)
		{
			double num = time / 1E-12 + 0.5;
			if (num < 9.223372036854776E+18 && num > -9.223372036854776E+18)
			{
				return (long)num;
			}
			throw new ArgumentOutOfRangeException("Time is over the discrete range.");
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00007548 File Offset: 0x00005748
		private static long FloatToDiscreteTime(float time)
		{
			float num = time / 1E-12f + 0.5f;
			if (num < 9.223372E+18f && num > -9.223372E+18f)
			{
				return (long)num;
			}
			throw new ArgumentOutOfRangeException("Time is over the discrete range.");
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00007580 File Offset: 0x00005780
		private static long IntToDiscreteTime(int time)
		{
			return DiscreteTime.DoubleToDiscreteTime((double)time);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00007589 File Offset: 0x00005789
		private static double ToDouble(long time)
		{
			return (double)time * 1E-12;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00007597 File Offset: 0x00005797
		private static float ToFloat(long time)
		{
			return (float)DiscreteTime.ToDouble(time);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000075A0 File Offset: 0x000057A0
		public static explicit operator double(DiscreteTime b)
		{
			return DiscreteTime.ToDouble(b.m_DiscreteTime);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000075AD File Offset: 0x000057AD
		public static explicit operator float(DiscreteTime b)
		{
			return DiscreteTime.ToFloat(b.m_DiscreteTime);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000075BA File Offset: 0x000057BA
		public static explicit operator long(DiscreteTime b)
		{
			return b.m_DiscreteTime;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000075C2 File Offset: 0x000057C2
		public static explicit operator DiscreteTime(double time)
		{
			return new DiscreteTime(time);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000075CA File Offset: 0x000057CA
		public static explicit operator DiscreteTime(float time)
		{
			return new DiscreteTime(time);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000075D2 File Offset: 0x000057D2
		public static implicit operator DiscreteTime(int time)
		{
			return new DiscreteTime(time);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000075DA File Offset: 0x000057DA
		public static explicit operator DiscreteTime(long time)
		{
			return new DiscreteTime(time);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000075E2 File Offset: 0x000057E2
		public static bool operator ==(DiscreteTime lhs, DiscreteTime rhs)
		{
			return lhs.m_DiscreteTime == rhs.m_DiscreteTime;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000075F2 File Offset: 0x000057F2
		public static bool operator !=(DiscreteTime lhs, DiscreteTime rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000075FE File Offset: 0x000057FE
		public static bool operator >(DiscreteTime lhs, DiscreteTime rhs)
		{
			return lhs.m_DiscreteTime > rhs.m_DiscreteTime;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000760E File Offset: 0x0000580E
		public static bool operator <(DiscreteTime lhs, DiscreteTime rhs)
		{
			return lhs.m_DiscreteTime < rhs.m_DiscreteTime;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000761E File Offset: 0x0000581E
		public static bool operator <=(DiscreteTime lhs, DiscreteTime rhs)
		{
			return lhs.m_DiscreteTime <= rhs.m_DiscreteTime;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00007631 File Offset: 0x00005831
		public static bool operator >=(DiscreteTime lhs, DiscreteTime rhs)
		{
			return lhs.m_DiscreteTime >= rhs.m_DiscreteTime;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00007644 File Offset: 0x00005844
		public static DiscreteTime operator +(DiscreteTime lhs, DiscreteTime rhs)
		{
			return new DiscreteTime(lhs.m_DiscreteTime + rhs.m_DiscreteTime);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00007658 File Offset: 0x00005858
		public static DiscreteTime operator -(DiscreteTime lhs, DiscreteTime rhs)
		{
			return new DiscreteTime(lhs.m_DiscreteTime - rhs.m_DiscreteTime);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000766C File Offset: 0x0000586C
		public override string ToString()
		{
			return this.m_DiscreteTime.ToString();
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00007679 File Offset: 0x00005879
		public override int GetHashCode()
		{
			return this.m_DiscreteTime.GetHashCode();
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00007686 File Offset: 0x00005886
		public static DiscreteTime Min(DiscreteTime lhs, DiscreteTime rhs)
		{
			return new DiscreteTime(Math.Min(lhs.m_DiscreteTime, rhs.m_DiscreteTime));
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000769E File Offset: 0x0000589E
		public static DiscreteTime Max(DiscreteTime lhs, DiscreteTime rhs)
		{
			return new DiscreteTime(Math.Max(lhs.m_DiscreteTime, rhs.m_DiscreteTime));
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000076B6 File Offset: 0x000058B6
		public static double SnapToNearestTick(double time)
		{
			return DiscreteTime.ToDouble(DiscreteTime.DoubleToDiscreteTime(time));
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000076C3 File Offset: 0x000058C3
		public static float SnapToNearestTick(float time)
		{
			return DiscreteTime.ToFloat(DiscreteTime.FloatToDiscreteTime(time));
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000076D0 File Offset: 0x000058D0
		public static long GetNearestTick(double time)
		{
			return DiscreteTime.DoubleToDiscreteTime(time);
		}

		// Token: 0x040000AC RID: 172
		private const double k_Tick = 1E-12;

		// Token: 0x040000AD RID: 173
		public static readonly DiscreteTime kMaxTime = new DiscreteTime(long.MaxValue);

		// Token: 0x040000AE RID: 174
		private readonly long m_DiscreteTime;
	}
}
