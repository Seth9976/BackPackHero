using System;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x02000292 RID: 658
	public struct Translate : IEquatable<Translate>
	{
		// Token: 0x0600159A RID: 5530 RVA: 0x0005BECF File Offset: 0x0005A0CF
		public Translate(Length x, Length y, float z)
		{
			this.m_X = x;
			this.m_Y = y;
			this.m_Z = z;
			this.m_isNone = false;
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x0005BEEE File Offset: 0x0005A0EE
		public Translate(Length x, Length y)
		{
			this = new Translate(x, y, 0f);
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x0005BF00 File Offset: 0x0005A100
		public static Translate None()
		{
			return new Translate
			{
				m_isNone = true
			};
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x0600159D RID: 5533 RVA: 0x0005BF23 File Offset: 0x0005A123
		// (set) Token: 0x0600159E RID: 5534 RVA: 0x0005BF2B File Offset: 0x0005A12B
		public Length x
		{
			get
			{
				return this.m_X;
			}
			set
			{
				this.m_X = value;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x0600159F RID: 5535 RVA: 0x0005BF34 File Offset: 0x0005A134
		// (set) Token: 0x060015A0 RID: 5536 RVA: 0x0005BF3C File Offset: 0x0005A13C
		public Length y
		{
			get
			{
				return this.m_Y;
			}
			set
			{
				this.m_Y = value;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060015A1 RID: 5537 RVA: 0x0005BF45 File Offset: 0x0005A145
		// (set) Token: 0x060015A2 RID: 5538 RVA: 0x0005BF4D File Offset: 0x0005A14D
		public float z
		{
			get
			{
				return this.m_Z;
			}
			set
			{
				this.m_Z = value;
			}
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x0005BF56 File Offset: 0x0005A156
		internal bool IsNone()
		{
			return this.m_isNone;
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x0005BF60 File Offset: 0x0005A160
		public static bool operator ==(Translate lhs, Translate rhs)
		{
			return lhs.m_X == rhs.m_X && lhs.m_Y == rhs.m_Y && lhs.m_Z == rhs.m_Z && lhs.m_isNone == rhs.m_isNone;
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x0005BFB8 File Offset: 0x0005A1B8
		public static bool operator !=(Translate lhs, Translate rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x0005BFD4 File Offset: 0x0005A1D4
		public bool Equals(Translate other)
		{
			return other == this;
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x0005BFF4 File Offset: 0x0005A1F4
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is Translate)
			{
				Translate translate = (Translate)obj;
				flag = this.Equals(translate);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x0005C020 File Offset: 0x0005A220
		public override int GetHashCode()
		{
			return (this.m_X.GetHashCode() * 793) ^ (this.m_Y.GetHashCode() * 791) ^ (this.m_Z.GetHashCode() * 571);
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x0005C074 File Offset: 0x0005A274
		public override string ToString()
		{
			string text = this.m_Z.ToString(CultureInfo.InvariantCulture.NumberFormat);
			return string.Concat(new string[]
			{
				this.m_X.ToString(),
				" ",
				this.m_Y.ToString(),
				" ",
				text
			});
		}

		// Token: 0x0400092C RID: 2348
		private Length m_X;

		// Token: 0x0400092D RID: 2349
		private Length m_Y;

		// Token: 0x0400092E RID: 2350
		private float m_Z;

		// Token: 0x0400092F RID: 2351
		private bool m_isNone;
	}
}
