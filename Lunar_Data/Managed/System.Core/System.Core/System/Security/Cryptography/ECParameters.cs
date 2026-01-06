using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000035 RID: 53
	public struct ECParameters
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00002868 File Offset: 0x00000A68
		public void Validate()
		{
			bool flag = false;
			if (this.Q.X == null || this.Q.Y == null || this.Q.X.Length != this.Q.Y.Length)
			{
				flag = true;
			}
			if (!flag)
			{
				if (this.Curve.IsExplicit)
				{
					flag = this.D != null && this.D.Length != this.Curve.Order.Length;
				}
				else if (this.Curve.IsNamed)
				{
					flag = this.D != null && this.D.Length != this.Q.X.Length;
				}
			}
			if (flag)
			{
				throw new CryptographicException("The specified key parameters are not valid. Q.X and Q.Y are required fields. Q.X, Q.Y must be the same length. If D is specified it must be the same length as Q.X and Q.Y for named curves or the same length as Order for explicit curves.");
			}
			this.Curve.Validate();
		}

		// Token: 0x040002D0 RID: 720
		public ECPoint Q;

		// Token: 0x040002D1 RID: 721
		public byte[] D;

		// Token: 0x040002D2 RID: 722
		public ECCurve Curve;
	}
}
