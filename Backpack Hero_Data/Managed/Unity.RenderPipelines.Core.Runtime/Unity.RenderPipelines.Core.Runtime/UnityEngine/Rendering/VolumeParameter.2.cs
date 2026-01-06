using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000BE RID: 190
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class VolumeParameter<T> : VolumeParameter, IEquatable<VolumeParameter<T>>
	{
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x0001DA37 File Offset: 0x0001BC37
		// (set) Token: 0x06000653 RID: 1619 RVA: 0x0001DA3F File Offset: 0x0001BC3F
		public virtual T value
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

		// Token: 0x06000654 RID: 1620 RVA: 0x0001DA48 File Offset: 0x0001BC48
		public VolumeParameter()
			: this(default(T), false)
		{
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0001DA65 File Offset: 0x0001BC65
		protected VolumeParameter(T value, bool overrideState)
		{
			this.m_Value = value;
			this.overrideState = overrideState;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0001DA7B File Offset: 0x0001BC7B
		internal override void Interp(VolumeParameter from, VolumeParameter to, float t)
		{
			this.Interp(from.GetValue<T>(), to.GetValue<T>(), t);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0001DA90 File Offset: 0x0001BC90
		public virtual void Interp(T from, T to, float t)
		{
			this.m_Value = ((t > 0f) ? to : from);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0001DAA4 File Offset: 0x0001BCA4
		public void Override(T x)
		{
			this.overrideState = true;
			this.m_Value = x;
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0001DAB4 File Offset: 0x0001BCB4
		public override void SetValue(VolumeParameter parameter)
		{
			this.m_Value = parameter.GetValue<T>();
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0001DAC4 File Offset: 0x0001BCC4
		public override int GetHashCode()
		{
			int num = 17;
			num = num * 23 + this.overrideState.GetHashCode();
			if (!EqualityComparer<T>.Default.Equals(this.value, default(T)))
			{
				int num2 = num * 23;
				T value = this.value;
				num = num2 + value.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0001DB1E File Offset: 0x0001BD1E
		public override string ToString()
		{
			return string.Format("{0} ({1})", this.value, this.overrideState);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0001DB40 File Offset: 0x0001BD40
		public static bool operator ==(VolumeParameter<T> lhs, T rhs)
		{
			if (lhs != null && lhs.value != null)
			{
				T value = lhs.value;
				return value.Equals(rhs);
			}
			return false;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0001DB79 File Offset: 0x0001BD79
		public static bool operator !=(VolumeParameter<T> lhs, T rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0001DB85 File Offset: 0x0001BD85
		public bool Equals(VolumeParameter<T> other)
		{
			return other != null && (this == other || EqualityComparer<T>.Default.Equals(this.m_Value, other.m_Value));
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0001DBA8 File Offset: 0x0001BDA8
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((VolumeParameter<T>)obj)));
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0001DBD6 File Offset: 0x0001BDD6
		public static explicit operator T(VolumeParameter<T> prop)
		{
			return prop.m_Value;
		}

		// Token: 0x040003A3 RID: 931
		[SerializeField]
		protected T m_Value;
	}
}
