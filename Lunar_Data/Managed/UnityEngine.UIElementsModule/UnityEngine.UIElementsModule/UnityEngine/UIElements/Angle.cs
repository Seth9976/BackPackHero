using System;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x02000266 RID: 614
	public struct Angle : IEquatable<Angle>
	{
		// Token: 0x06001266 RID: 4710 RVA: 0x00048538 File Offset: 0x00046738
		public static Angle Degrees(float value)
		{
			return new Angle(value, AngleUnit.Degree);
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00048554 File Offset: 0x00046754
		internal static Angle None()
		{
			return new Angle(0f, Angle.Unit.None);
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001268 RID: 4712 RVA: 0x00048571 File Offset: 0x00046771
		// (set) Token: 0x06001269 RID: 4713 RVA: 0x00048579 File Offset: 0x00046779
		public float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x00048582 File Offset: 0x00046782
		// (set) Token: 0x0600126B RID: 4715 RVA: 0x0004858A File Offset: 0x0004678A
		public AngleUnit unit
		{
			get
			{
				return (AngleUnit)this.m_Unit;
			}
			set
			{
				this.m_Unit = (Angle.Unit)value;
			}
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00048593 File Offset: 0x00046793
		internal bool IsNone()
		{
			return this.m_Unit == Angle.Unit.None;
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x0004859E File Offset: 0x0004679E
		public Angle(float value)
		{
			this = new Angle(value, Angle.Unit.Degree);
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x000485AA File Offset: 0x000467AA
		public Angle(float value, AngleUnit unit)
		{
			this = new Angle(value, (Angle.Unit)unit);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x000485B6 File Offset: 0x000467B6
		private Angle(float value, Angle.Unit unit)
		{
			this.m_Value = value;
			this.m_Unit = unit;
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x000485C8 File Offset: 0x000467C8
		public float ToDegrees()
		{
			float num;
			switch (this.m_Unit)
			{
			case Angle.Unit.Degree:
				num = this.m_Value;
				break;
			case Angle.Unit.Gradian:
				num = this.m_Value * 360f / 400f;
				break;
			case Angle.Unit.Radian:
				num = this.m_Value * 180f / 3.1415927f;
				break;
			case Angle.Unit.Turn:
				num = this.m_Value * 360f;
				break;
			case Angle.Unit.None:
				num = 0f;
				break;
			default:
				num = 0f;
				break;
			}
			return num;
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x00048650 File Offset: 0x00046850
		public static implicit operator Angle(float value)
		{
			return new Angle(value, AngleUnit.Degree);
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x0004866C File Offset: 0x0004686C
		public static bool operator ==(Angle lhs, Angle rhs)
		{
			return lhs.m_Value == rhs.m_Value && lhs.m_Unit == rhs.m_Unit;
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x000486A0 File Offset: 0x000468A0
		public static bool operator !=(Angle lhs, Angle rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x000486BC File Offset: 0x000468BC
		public bool Equals(Angle other)
		{
			return other == this;
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x000486DC File Offset: 0x000468DC
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is Angle)
			{
				Angle angle = (Angle)obj;
				flag = this.Equals(angle);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00048708 File Offset: 0x00046908
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Unit;
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x00048734 File Offset: 0x00046934
		public override string ToString()
		{
			string text = this.value.ToString(CultureInfo.InvariantCulture.NumberFormat);
			string text2 = string.Empty;
			switch (this.m_Unit)
			{
			case Angle.Unit.Degree:
			{
				bool flag = !Mathf.Approximately(0f, this.value);
				if (flag)
				{
					text2 = "deg";
				}
				break;
			}
			case Angle.Unit.Gradian:
				text2 = "grad";
				break;
			case Angle.Unit.Radian:
				text2 = "rad";
				break;
			case Angle.Unit.Turn:
				text2 = "turn";
				break;
			case Angle.Unit.None:
				text = "";
				break;
			}
			return text + text2;
		}

		// Token: 0x04000883 RID: 2179
		private float m_Value;

		// Token: 0x04000884 RID: 2180
		private Angle.Unit m_Unit;

		// Token: 0x02000267 RID: 615
		private enum Unit
		{
			// Token: 0x04000886 RID: 2182
			Degree,
			// Token: 0x04000887 RID: 2183
			Gradian,
			// Token: 0x04000888 RID: 2184
			Radian,
			// Token: 0x04000889 RID: 2185
			Turn,
			// Token: 0x0400088A RID: 2186
			None
		}
	}
}
