using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200029A RID: 666
	internal struct TransitionData : IStyleDataGroup<TransitionData>, IEquatable<TransitionData>
	{
		// Token: 0x060016AB RID: 5803 RVA: 0x0005CD74 File Offset: 0x0005AF74
		public TransitionData Copy()
		{
			return new TransitionData
			{
				transitionDelay = new List<TimeValue>(this.transitionDelay),
				transitionDuration = new List<TimeValue>(this.transitionDuration),
				transitionProperty = new List<StylePropertyName>(this.transitionProperty),
				transitionTimingFunction = new List<EasingFunction>(this.transitionTimingFunction)
			};
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x0005CDD8 File Offset: 0x0005AFD8
		public void CopyFrom(ref TransitionData other)
		{
			bool flag = this.transitionDelay != other.transitionDelay;
			if (flag)
			{
				this.transitionDelay.Clear();
				this.transitionDelay.AddRange(other.transitionDelay);
			}
			bool flag2 = this.transitionDuration != other.transitionDuration;
			if (flag2)
			{
				this.transitionDuration.Clear();
				this.transitionDuration.AddRange(other.transitionDuration);
			}
			bool flag3 = this.transitionProperty != other.transitionProperty;
			if (flag3)
			{
				this.transitionProperty.Clear();
				this.transitionProperty.AddRange(other.transitionProperty);
			}
			bool flag4 = this.transitionTimingFunction != other.transitionTimingFunction;
			if (flag4)
			{
				this.transitionTimingFunction.Clear();
				this.transitionTimingFunction.AddRange(other.transitionTimingFunction);
			}
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x0005CEBC File Offset: 0x0005B0BC
		public static bool operator ==(TransitionData lhs, TransitionData rhs)
		{
			return lhs.transitionDelay == rhs.transitionDelay && lhs.transitionDuration == rhs.transitionDuration && lhs.transitionProperty == rhs.transitionProperty && lhs.transitionTimingFunction == rhs.transitionTimingFunction;
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x0005CF0C File Offset: 0x0005B10C
		public static bool operator !=(TransitionData lhs, TransitionData rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x0005CF28 File Offset: 0x0005B128
		public bool Equals(TransitionData other)
		{
			return other == this;
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x0005CF48 File Offset: 0x0005B148
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is TransitionData && this.Equals((TransitionData)obj);
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x0005CF80 File Offset: 0x0005B180
		public override int GetHashCode()
		{
			int num = this.transitionDelay.GetHashCode();
			num = (num * 397) ^ this.transitionDuration.GetHashCode();
			num = (num * 397) ^ this.transitionProperty.GetHashCode();
			return (num * 397) ^ this.transitionTimingFunction.GetHashCode();
		}

		// Token: 0x0400096D RID: 2413
		public List<TimeValue> transitionDelay;

		// Token: 0x0400096E RID: 2414
		public List<TimeValue> transitionDuration;

		// Token: 0x0400096F RID: 2415
		public List<StylePropertyName> transitionProperty;

		// Token: 0x04000970 RID: 2416
		public List<EasingFunction> transitionTimingFunction;
	}
}
