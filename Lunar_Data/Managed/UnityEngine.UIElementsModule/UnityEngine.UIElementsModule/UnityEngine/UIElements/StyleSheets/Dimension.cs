using System;
using System.Globalization;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000357 RID: 855
	[Serializable]
	internal struct Dimension : IEquatable<Dimension>
	{
		// Token: 0x06001B7C RID: 7036 RVA: 0x0007E6C5 File Offset: 0x0007C8C5
		public Dimension(float value, Dimension.Unit unit)
		{
			this.unit = unit;
			this.value = value;
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x0007E6D8 File Offset: 0x0007C8D8
		public Length ToLength()
		{
			LengthUnit lengthUnit = ((this.unit == Dimension.Unit.Percent) ? LengthUnit.Percent : LengthUnit.Pixel);
			return new Length(this.value, lengthUnit);
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x0007E704 File Offset: 0x0007C904
		public TimeValue ToTime()
		{
			TimeUnit timeUnit = ((this.unit == Dimension.Unit.Millisecond) ? TimeUnit.Millisecond : TimeUnit.Second);
			return new TimeValue(this.value, timeUnit);
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x0007E730 File Offset: 0x0007C930
		public Angle ToAngle()
		{
			Angle angle;
			switch (this.unit)
			{
			case Dimension.Unit.Degree:
				angle = new Angle(this.value, AngleUnit.Degree);
				break;
			case Dimension.Unit.Gradian:
				angle = new Angle(this.value, AngleUnit.Gradian);
				break;
			case Dimension.Unit.Radian:
				angle = new Angle(this.value, AngleUnit.Radian);
				break;
			case Dimension.Unit.Turn:
				angle = new Angle(this.value, AngleUnit.Turn);
				break;
			default:
				angle = new Angle(this.value, AngleUnit.Degree);
				break;
			}
			return angle;
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x0007E7B0 File Offset: 0x0007C9B0
		public static bool operator ==(Dimension lhs, Dimension rhs)
		{
			return lhs.value == rhs.value && lhs.unit == rhs.unit;
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x0007E7E4 File Offset: 0x0007C9E4
		public static bool operator !=(Dimension lhs, Dimension rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x0007E800 File Offset: 0x0007CA00
		public bool Equals(Dimension other)
		{
			return other == this;
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x0007E820 File Offset: 0x0007CA20
		public override bool Equals(object obj)
		{
			bool flag = !(obj is Dimension);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				Dimension dimension = (Dimension)obj;
				flag2 = dimension == this;
			}
			return flag2;
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x0007E85C File Offset: 0x0007CA5C
		public override int GetHashCode()
		{
			int num = -799583767;
			num = num * -1521134295 + this.unit.GetHashCode();
			return num * -1521134295 + this.value.GetHashCode();
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x0007E8A4 File Offset: 0x0007CAA4
		public override string ToString()
		{
			string text = string.Empty;
			switch (this.unit)
			{
			case Dimension.Unit.Unitless:
				text = string.Empty;
				break;
			case Dimension.Unit.Pixel:
				text = "px";
				break;
			case Dimension.Unit.Percent:
				text = "%";
				break;
			case Dimension.Unit.Second:
				text = "s";
				break;
			case Dimension.Unit.Millisecond:
				text = "ms";
				break;
			case Dimension.Unit.Degree:
				text = "deg";
				break;
			case Dimension.Unit.Gradian:
				text = "grad";
				break;
			case Dimension.Unit.Radian:
				text = "rad";
				break;
			case Dimension.Unit.Turn:
				text = "turn";
				break;
			}
			return this.value.ToString(CultureInfo.InvariantCulture.NumberFormat) + text;
		}

		// Token: 0x04000D9B RID: 3483
		public Dimension.Unit unit;

		// Token: 0x04000D9C RID: 3484
		public float value;

		// Token: 0x02000358 RID: 856
		public enum Unit
		{
			// Token: 0x04000D9E RID: 3486
			Unitless,
			// Token: 0x04000D9F RID: 3487
			Pixel,
			// Token: 0x04000DA0 RID: 3488
			Percent,
			// Token: 0x04000DA1 RID: 3489
			Second,
			// Token: 0x04000DA2 RID: 3490
			Millisecond,
			// Token: 0x04000DA3 RID: 3491
			Degree,
			// Token: 0x04000DA4 RID: 3492
			Gradian,
			// Token: 0x04000DA5 RID: 3493
			Radian,
			// Token: 0x04000DA6 RID: 3494
			Turn
		}
	}
}
