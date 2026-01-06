using System;
using System.Globalization;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000430 RID: 1072
	[UsedByNativeCode("FrameRate")]
	[NativeHeader("Runtime/Director/Core/FrameRate.h")]
	internal struct FrameRate : IEquatable<FrameRate>
	{
		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06002556 RID: 9558 RVA: 0x0003EF8B File Offset: 0x0003D18B
		public bool dropFrame
		{
			get
			{
				return this.m_Rate < 0;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06002557 RID: 9559 RVA: 0x0003EF96 File Offset: 0x0003D196
		public double rate
		{
			get
			{
				return this.dropFrame ? ((double)(-(double)this.m_Rate) * 0.999000999000999) : ((double)this.m_Rate);
			}
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x0003EFBB File Offset: 0x0003D1BB
		public FrameRate(uint frameRate = 0U, bool drop = false)
		{
			this.m_Rate = (int)((drop ? uint.MaxValue : 1U) * frameRate);
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x0003EFD0 File Offset: 0x0003D1D0
		public bool IsValid()
		{
			return this.m_Rate != 0;
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x0003EFEC File Offset: 0x0003D1EC
		public bool Equals(FrameRate other)
		{
			return this.m_Rate == other.m_Rate;
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x0003F00C File Offset: 0x0003D20C
		public override bool Equals(object obj)
		{
			return obj is FrameRate && this.Equals((FrameRate)obj);
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x0003F035 File Offset: 0x0003D235
		public static bool operator ==(FrameRate a, FrameRate b)
		{
			return a.Equals(b);
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x0003F03F File Offset: 0x0003D23F
		public static bool operator !=(FrameRate a, FrameRate b)
		{
			return !a.Equals(b);
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x0003F04C File Offset: 0x0003D24C
		public static bool operator <(FrameRate a, FrameRate b)
		{
			return a.rate < b.rate;
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x0003F05E File Offset: 0x0003D25E
		public static bool operator <=(FrameRate a, FrameRate b)
		{
			return a.rate <= b.rate;
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x0003F073 File Offset: 0x0003D273
		public static bool operator >(FrameRate a, FrameRate b)
		{
			return a.rate > b.rate;
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x0003F05E File Offset: 0x0003D25E
		public static bool operator >=(FrameRate a, FrameRate b)
		{
			return a.rate <= b.rate;
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x0003F088 File Offset: 0x0003D288
		public override int GetHashCode()
		{
			return this.m_Rate;
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x0003F0A0 File Offset: 0x0003D2A0
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x0003F0BC File Offset: 0x0003D2BC
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x0003F0D8 File Offset: 0x0003D2D8
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = (this.dropFrame ? "F2" : "F0");
			}
			bool flag2 = formatProvider == null;
			if (flag2)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("{0} Fps", new object[] { this.rate.ToString(format, formatProvider) });
		}

		// Token: 0x06002566 RID: 9574 RVA: 0x0003F144 File Offset: 0x0003D344
		internal static int FrameRateToInt(FrameRate framerate)
		{
			return framerate.m_Rate;
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x0003F15C File Offset: 0x0003D35C
		internal static FrameRate DoubleToFrameRate(double framerate)
		{
			uint num = (uint)Math.Ceiling(framerate);
			bool flag = num <= 0U;
			FrameRate frameRate;
			if (flag)
			{
				frameRate = new FrameRate(1U, false);
			}
			else
			{
				FrameRate frameRate2 = new FrameRate(num, true);
				bool flag2 = Math.Abs(framerate - frameRate2.rate) < Math.Abs(framerate - num);
				if (flag2)
				{
					frameRate = frameRate2;
				}
				else
				{
					frameRate = new FrameRate(num, false);
				}
			}
			return frameRate;
		}

		// Token: 0x04000DF9 RID: 3577
		[Ignore]
		public static readonly FrameRate k_24Fps = new FrameRate(24U, false);

		// Token: 0x04000DFA RID: 3578
		[Ignore]
		public static readonly FrameRate k_23_976Fps = new FrameRate(24U, true);

		// Token: 0x04000DFB RID: 3579
		[Ignore]
		public static readonly FrameRate k_25Fps = new FrameRate(25U, false);

		// Token: 0x04000DFC RID: 3580
		[Ignore]
		public static readonly FrameRate k_30Fps = new FrameRate(30U, false);

		// Token: 0x04000DFD RID: 3581
		[Ignore]
		public static readonly FrameRate k_29_97Fps = new FrameRate(30U, true);

		// Token: 0x04000DFE RID: 3582
		[Ignore]
		public static readonly FrameRate k_50Fps = new FrameRate(50U, false);

		// Token: 0x04000DFF RID: 3583
		[Ignore]
		public static readonly FrameRate k_60Fps = new FrameRate(60U, false);

		// Token: 0x04000E00 RID: 3584
		[Ignore]
		public static readonly FrameRate k_59_94Fps = new FrameRate(60U, true);

		// Token: 0x04000E01 RID: 3585
		[SerializeField]
		private int m_Rate;
	}
}
