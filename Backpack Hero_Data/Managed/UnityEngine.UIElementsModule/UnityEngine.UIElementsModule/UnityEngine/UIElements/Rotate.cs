using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000278 RID: 632
	public struct Rotate : IEquatable<Rotate>
	{
		// Token: 0x0600143E RID: 5182 RVA: 0x0005725A File Offset: 0x0005545A
		internal Rotate(Angle angle, Vector3 axis)
		{
			this.m_Angle = angle;
			this.m_Axis = axis;
			this.m_IsNone = false;
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x00057272 File Offset: 0x00055472
		public Rotate(Angle angle)
		{
			this.m_Angle = angle;
			this.m_Axis = Vector3.forward;
			this.m_IsNone = false;
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x00057290 File Offset: 0x00055490
		internal static Rotate Initial()
		{
			return new Rotate(0f);
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x000572B4 File Offset: 0x000554B4
		public static Rotate None()
		{
			Rotate rotate = Rotate.Initial();
			rotate.m_IsNone = true;
			return rotate;
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x000572D5 File Offset: 0x000554D5
		// (set) Token: 0x06001443 RID: 5187 RVA: 0x000572DD File Offset: 0x000554DD
		public Angle angle
		{
			get
			{
				return this.m_Angle;
			}
			set
			{
				this.m_Angle = value;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x000572E6 File Offset: 0x000554E6
		// (set) Token: 0x06001445 RID: 5189 RVA: 0x000572EE File Offset: 0x000554EE
		internal Vector3 axis
		{
			get
			{
				return this.m_Axis;
			}
			set
			{
				this.m_Axis = value;
			}
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x000572F7 File Offset: 0x000554F7
		internal bool IsNone()
		{
			return this.m_IsNone;
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x00057300 File Offset: 0x00055500
		public static bool operator ==(Rotate lhs, Rotate rhs)
		{
			return lhs.m_Angle == rhs.m_Angle && lhs.m_Axis == rhs.m_Axis && lhs.m_IsNone == rhs.m_IsNone;
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x0005734C File Offset: 0x0005554C
		public static bool operator !=(Rotate lhs, Rotate rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x00057368 File Offset: 0x00055568
		public bool Equals(Rotate other)
		{
			return other == this;
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x00057388 File Offset: 0x00055588
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is Rotate)
			{
				Rotate rotate = (Rotate)obj;
				flag = this.Equals(rotate);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x000573B4 File Offset: 0x000555B4
		public override int GetHashCode()
		{
			return (this.m_Angle.GetHashCode() * 793) ^ (this.m_Axis.GetHashCode() * 791) ^ (this.m_IsNone.GetHashCode() * 197);
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x00057408 File Offset: 0x00055608
		public override string ToString()
		{
			return this.m_Angle.ToString() + " " + this.m_Axis.ToString();
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x00057448 File Offset: 0x00055648
		internal Quaternion ToQuaternion()
		{
			return Quaternion.AngleAxis(this.m_Angle.ToDegrees(), this.m_Axis);
		}

		// Token: 0x040008F2 RID: 2290
		private Angle m_Angle;

		// Token: 0x040008F3 RID: 2291
		private Vector3 m_Axis;

		// Token: 0x040008F4 RID: 2292
		private bool m_IsNone;
	}
}
