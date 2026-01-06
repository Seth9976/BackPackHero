using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000270 RID: 624
	public struct EasingFunction : IEquatable<EasingFunction>
	{
		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001343 RID: 4931 RVA: 0x00052C28 File Offset: 0x00050E28
		// (set) Token: 0x06001344 RID: 4932 RVA: 0x00052C30 File Offset: 0x00050E30
		public EasingMode mode
		{
			get
			{
				return this.m_Mode;
			}
			set
			{
				this.m_Mode = value;
			}
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x00052C39 File Offset: 0x00050E39
		public EasingFunction(EasingMode mode)
		{
			this.m_Mode = mode;
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x00052C44 File Offset: 0x00050E44
		public static implicit operator EasingFunction(EasingMode easingMode)
		{
			return new EasingFunction(easingMode);
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x00052C5C File Offset: 0x00050E5C
		public static bool operator ==(EasingFunction lhs, EasingFunction rhs)
		{
			return lhs.m_Mode == rhs.m_Mode;
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x00052C7C File Offset: 0x00050E7C
		public static bool operator !=(EasingFunction lhs, EasingFunction rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x00052C98 File Offset: 0x00050E98
		public bool Equals(EasingFunction other)
		{
			return other == this;
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x00052CB8 File Offset: 0x00050EB8
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is EasingFunction)
			{
				EasingFunction easingFunction = (EasingFunction)obj;
				flag = this.Equals(easingFunction);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x00052CE4 File Offset: 0x00050EE4
		public override string ToString()
		{
			return this.m_Mode.ToString();
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x00052D08 File Offset: 0x00050F08
		public override int GetHashCode()
		{
			return (int)this.m_Mode;
		}

		// Token: 0x040008D0 RID: 2256
		private EasingMode m_Mode;
	}
}
