using System;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x02000290 RID: 656
	public struct TimeValue : IEquatable<TimeValue>
	{
		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x0005BB44 File Offset: 0x00059D44
		// (set) Token: 0x0600157F RID: 5503 RVA: 0x0005BB4C File Offset: 0x00059D4C
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

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001580 RID: 5504 RVA: 0x0005BB55 File Offset: 0x00059D55
		// (set) Token: 0x06001581 RID: 5505 RVA: 0x0005BB5D File Offset: 0x00059D5D
		public TimeUnit unit
		{
			get
			{
				return this.m_Unit;
			}
			set
			{
				this.m_Unit = value;
			}
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x0005BB66 File Offset: 0x00059D66
		public TimeValue(float value)
		{
			this = new TimeValue(value, TimeUnit.Second);
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x0005BB72 File Offset: 0x00059D72
		public TimeValue(float value, TimeUnit unit)
		{
			this.m_Value = value;
			this.m_Unit = unit;
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x0005BB84 File Offset: 0x00059D84
		public static implicit operator TimeValue(float value)
		{
			return new TimeValue(value, TimeUnit.Second);
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x0005BBA0 File Offset: 0x00059DA0
		public static bool operator ==(TimeValue lhs, TimeValue rhs)
		{
			return lhs.m_Value == rhs.m_Value && lhs.m_Unit == rhs.m_Unit;
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x0005BBD4 File Offset: 0x00059DD4
		public static bool operator !=(TimeValue lhs, TimeValue rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x0005BBF0 File Offset: 0x00059DF0
		public bool Equals(TimeValue other)
		{
			return other == this;
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x0005BC10 File Offset: 0x00059E10
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is TimeValue)
			{
				TimeValue timeValue = (TimeValue)obj;
				flag = this.Equals(timeValue);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x0005BC3C File Offset: 0x00059E3C
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Unit;
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x0005BC68 File Offset: 0x00059E68
		public override string ToString()
		{
			string text = this.value.ToString(CultureInfo.InvariantCulture.NumberFormat);
			string text2 = string.Empty;
			TimeUnit unit = this.unit;
			TimeUnit timeUnit = unit;
			if (timeUnit != TimeUnit.Second)
			{
				if (timeUnit == TimeUnit.Millisecond)
				{
					text2 = "ms";
				}
			}
			else
			{
				text2 = "s";
			}
			return text + text2;
		}

		// Token: 0x04000927 RID: 2343
		private float m_Value;

		// Token: 0x04000928 RID: 2344
		private TimeUnit m_Unit;
	}
}
