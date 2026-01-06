using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000279 RID: 633
	public struct Scale : IEquatable<Scale>
	{
		// Token: 0x0600144E RID: 5198 RVA: 0x00057470 File Offset: 0x00055670
		public Scale(Vector3 scale)
		{
			this.m_Scale = new Vector3(scale.x, scale.y, 1f);
			this.m_IsNone = false;
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x00057498 File Offset: 0x00055698
		internal static Scale Initial()
		{
			return new Scale(Vector3.one);
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x000574B4 File Offset: 0x000556B4
		public static Scale None()
		{
			Scale scale = Scale.Initial();
			scale.m_IsNone = true;
			return scale;
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x000574D5 File Offset: 0x000556D5
		// (set) Token: 0x06001452 RID: 5202 RVA: 0x000574DD File Offset: 0x000556DD
		public Vector3 value
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				this.m_Scale = new Vector3(value.x, value.y, 1f);
			}
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x000574FB File Offset: 0x000556FB
		internal bool IsNone()
		{
			return this.m_IsNone;
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x00057504 File Offset: 0x00055704
		public static bool operator ==(Scale lhs, Scale rhs)
		{
			return lhs.m_Scale == rhs.m_Scale;
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x00057528 File Offset: 0x00055728
		public static bool operator !=(Scale lhs, Scale rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x00057544 File Offset: 0x00055744
		public bool Equals(Scale other)
		{
			return other == this;
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x00057564 File Offset: 0x00055764
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is Scale)
			{
				Scale scale = (Scale)obj;
				flag = this.Equals(scale);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x00057590 File Offset: 0x00055790
		public override int GetHashCode()
		{
			return this.m_Scale.GetHashCode() * 793;
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x000575BC File Offset: 0x000557BC
		public override string ToString()
		{
			return this.m_Scale.ToString();
		}

		// Token: 0x040008F5 RID: 2293
		private Vector3 m_Scale;

		// Token: 0x040008F6 RID: 2294
		private bool m_IsNone;
	}
}
