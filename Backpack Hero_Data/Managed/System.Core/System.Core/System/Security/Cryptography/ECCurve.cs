using System;
using System.Diagnostics;

namespace System.Security.Cryptography
{
	// Token: 0x02000032 RID: 50
	[DebuggerDisplay("ECCurve: {Oid}")]
	public struct ECCurve
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00002450 File Offset: 0x00000650
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00002470 File Offset: 0x00000670
		public Oid Oid
		{
			get
			{
				return new Oid(this._oid.Value, this._oid.FriendlyName);
			}
			private set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Oid");
				}
				if (string.IsNullOrEmpty(value.Value) && string.IsNullOrEmpty(value.FriendlyName))
				{
					throw new ArgumentException(string.Format("The specified Oid is not valid. The Oid.FriendlyName or Oid.Value property must be set.", Array.Empty<object>()));
				}
				this._oid = value;
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000024C4 File Offset: 0x000006C4
		private static ECCurve Create(Oid oid)
		{
			return new ECCurve
			{
				CurveType = ECCurve.ECCurveType.Named,
				Oid = oid
			};
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000024EA File Offset: 0x000006EA
		public static ECCurve CreateFromOid(Oid curveOid)
		{
			return ECCurve.Create(new Oid(curveOid.Value, curveOid.FriendlyName));
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00002502 File Offset: 0x00000702
		public static ECCurve CreateFromFriendlyName(string oidFriendlyName)
		{
			if (oidFriendlyName == null)
			{
				throw new ArgumentNullException("oidFriendlyName");
			}
			return ECCurve.CreateFromValueAndName(null, oidFriendlyName);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00002519 File Offset: 0x00000719
		public static ECCurve CreateFromValue(string oidValue)
		{
			if (oidValue == null)
			{
				throw new ArgumentNullException("oidValue");
			}
			return ECCurve.CreateFromValueAndName(oidValue, null);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00002530 File Offset: 0x00000730
		private static ECCurve CreateFromValueAndName(string oidValue, string oidFriendlyName)
		{
			return ECCurve.Create(new Oid(oidValue, oidFriendlyName));
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x0000253E File Offset: 0x0000073E
		public bool IsPrime
		{
			get
			{
				return this.CurveType == ECCurve.ECCurveType.PrimeShortWeierstrass || this.CurveType == ECCurve.ECCurveType.PrimeMontgomery || this.CurveType == ECCurve.ECCurveType.PrimeTwistedEdwards;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x0000255D File Offset: 0x0000075D
		public bool IsCharacteristic2
		{
			get
			{
				return this.CurveType == ECCurve.ECCurveType.Characteristic2;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00002568 File Offset: 0x00000768
		public bool IsExplicit
		{
			get
			{
				return this.IsPrime || this.IsCharacteristic2;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000257A File Offset: 0x0000077A
		public bool IsNamed
		{
			get
			{
				return this.CurveType == ECCurve.ECCurveType.Named;
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00002588 File Offset: 0x00000788
		public void Validate()
		{
			if (this.IsNamed)
			{
				if (this.HasAnyExplicitParameters())
				{
					throw new CryptographicException("The specified named curve parameters are not valid. Only the Oid parameter must be set.");
				}
				if (this.Oid == null || (string.IsNullOrEmpty(this.Oid.FriendlyName) && string.IsNullOrEmpty(this.Oid.Value)))
				{
					throw new CryptographicException("The specified Oid is not valid. The Oid.FriendlyName or Oid.Value property must be set.");
				}
			}
			else if (this.IsExplicit)
			{
				bool flag = false;
				if (this.A == null || this.B == null || this.B.Length != this.A.Length || this.G.X == null || this.G.X.Length != this.A.Length || this.G.Y == null || this.G.Y.Length != this.A.Length || this.Order == null || this.Order.Length == 0 || this.Cofactor == null || this.Cofactor.Length == 0)
				{
					flag = true;
				}
				if (this.IsPrime)
				{
					if (!flag && (this.Prime == null || this.Prime.Length != this.A.Length))
					{
						flag = true;
					}
					if (flag)
					{
						throw new CryptographicException("The specified prime curve parameters are not valid. Prime, A, B, G.X, G.Y and Order are required and must be the same length, and the same length as Q.X, Q.Y and D if those are specified. Seed, Cofactor and Hash are optional. Other parameters are not allowed.");
					}
				}
				else if (this.IsCharacteristic2)
				{
					if (!flag && (this.Polynomial == null || this.Polynomial.Length == 0))
					{
						flag = true;
					}
					if (flag)
					{
						throw new CryptographicException("The specified Characteristic2 curve parameters are not valid. Polynomial, A, B, G.X, G.Y, and Order are required. A, B, G.X, G.Y must be the same length, and the same length as Q.X, Q.Y and D if those are specified. Seed, Cofactor and Hash are optional. Other parameters are not allowed.");
					}
				}
			}
			else if (this.HasAnyExplicitParameters() || this.Oid != null)
			{
				throw new CryptographicException(string.Format("The specified curve '{0}' or its parameters are not valid for this platform.", this.CurveType.ToString()));
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00002720 File Offset: 0x00000920
		private bool HasAnyExplicitParameters()
		{
			return this.A != null || this.B != null || this.G.X != null || this.G.Y != null || this.Order != null || this.Cofactor != null || this.Prime != null || this.Polynomial != null || this.Seed != null || this.Hash != null;
		}

		// Token: 0x040002BB RID: 699
		public byte[] A;

		// Token: 0x040002BC RID: 700
		public byte[] B;

		// Token: 0x040002BD RID: 701
		public ECPoint G;

		// Token: 0x040002BE RID: 702
		public byte[] Order;

		// Token: 0x040002BF RID: 703
		public byte[] Cofactor;

		// Token: 0x040002C0 RID: 704
		public byte[] Seed;

		// Token: 0x040002C1 RID: 705
		public ECCurve.ECCurveType CurveType;

		// Token: 0x040002C2 RID: 706
		public HashAlgorithmName? Hash;

		// Token: 0x040002C3 RID: 707
		public byte[] Polynomial;

		// Token: 0x040002C4 RID: 708
		public byte[] Prime;

		// Token: 0x040002C5 RID: 709
		private Oid _oid;

		// Token: 0x02000033 RID: 51
		public enum ECCurveType
		{
			// Token: 0x040002C7 RID: 711
			Implicit,
			// Token: 0x040002C8 RID: 712
			PrimeShortWeierstrass,
			// Token: 0x040002C9 RID: 713
			PrimeTwistedEdwards,
			// Token: 0x040002CA RID: 714
			PrimeMontgomery,
			// Token: 0x040002CB RID: 715
			Characteristic2,
			// Token: 0x040002CC RID: 716
			Named
		}

		// Token: 0x02000034 RID: 52
		public static class NamedCurves
		{
			// Token: 0x1700000B RID: 11
			// (get) Token: 0x060000A8 RID: 168 RVA: 0x0000278C File Offset: 0x0000098C
			public static ECCurve brainpoolP160r1
			{
				get
				{
					return ECCurve.CreateFromFriendlyName("brainpoolP160r1");
				}
			}

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x060000A9 RID: 169 RVA: 0x00002798 File Offset: 0x00000998
			public static ECCurve brainpoolP160t1
			{
				get
				{
					return ECCurve.CreateFromFriendlyName("brainpoolP160t1");
				}
			}

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x060000AA RID: 170 RVA: 0x000027A4 File Offset: 0x000009A4
			public static ECCurve brainpoolP192r1
			{
				get
				{
					return ECCurve.CreateFromFriendlyName("brainpoolP192r1");
				}
			}

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x060000AB RID: 171 RVA: 0x000027B0 File Offset: 0x000009B0
			public static ECCurve brainpoolP192t1
			{
				get
				{
					return ECCurve.CreateFromFriendlyName("brainpoolP192t1");
				}
			}

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x060000AC RID: 172 RVA: 0x000027BC File Offset: 0x000009BC
			public static ECCurve brainpoolP224r1
			{
				get
				{
					return ECCurve.CreateFromFriendlyName("brainpoolP224r1");
				}
			}

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x060000AD RID: 173 RVA: 0x000027C8 File Offset: 0x000009C8
			public static ECCurve brainpoolP224t1
			{
				get
				{
					return ECCurve.CreateFromFriendlyName("brainpoolP224t1");
				}
			}

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x060000AE RID: 174 RVA: 0x000027D4 File Offset: 0x000009D4
			public static ECCurve brainpoolP256r1
			{
				get
				{
					return ECCurve.CreateFromFriendlyName("brainpoolP256r1");
				}
			}

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x060000AF RID: 175 RVA: 0x000027E0 File Offset: 0x000009E0
			public static ECCurve brainpoolP256t1
			{
				get
				{
					return ECCurve.CreateFromFriendlyName("brainpoolP256t1");
				}
			}

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x060000B0 RID: 176 RVA: 0x000027EC File Offset: 0x000009EC
			public static ECCurve brainpoolP320r1
			{
				get
				{
					return ECCurve.CreateFromFriendlyName("brainpoolP320r1");
				}
			}

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x060000B1 RID: 177 RVA: 0x000027F8 File Offset: 0x000009F8
			public static ECCurve brainpoolP320t1
			{
				get
				{
					return ECCurve.CreateFromFriendlyName("brainpoolP320t1");
				}
			}

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x060000B2 RID: 178 RVA: 0x00002804 File Offset: 0x00000A04
			public static ECCurve brainpoolP384r1
			{
				get
				{
					return ECCurve.CreateFromFriendlyName("brainpoolP384r1");
				}
			}

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x060000B3 RID: 179 RVA: 0x00002810 File Offset: 0x00000A10
			public static ECCurve brainpoolP384t1
			{
				get
				{
					return ECCurve.CreateFromFriendlyName("brainpoolP384t1");
				}
			}

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x060000B4 RID: 180 RVA: 0x0000281C File Offset: 0x00000A1C
			public static ECCurve brainpoolP512r1
			{
				get
				{
					return ECCurve.CreateFromFriendlyName("brainpoolP512r1");
				}
			}

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x060000B5 RID: 181 RVA: 0x00002828 File Offset: 0x00000A28
			public static ECCurve brainpoolP512t1
			{
				get
				{
					return ECCurve.CreateFromFriendlyName("brainpoolP512t1");
				}
			}

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x060000B6 RID: 182 RVA: 0x00002834 File Offset: 0x00000A34
			public static ECCurve nistP256
			{
				get
				{
					return ECCurve.CreateFromValueAndName("1.2.840.10045.3.1.7", "nistP256");
				}
			}

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x060000B7 RID: 183 RVA: 0x00002845 File Offset: 0x00000A45
			public static ECCurve nistP384
			{
				get
				{
					return ECCurve.CreateFromValueAndName("1.3.132.0.34", "nistP384");
				}
			}

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x060000B8 RID: 184 RVA: 0x00002856 File Offset: 0x00000A56
			public static ECCurve nistP521
			{
				get
				{
					return ECCurve.CreateFromValueAndName("1.3.132.0.35", "nistP521");
				}
			}

			// Token: 0x040002CD RID: 717
			private const string ECDSA_P256_OID_VALUE = "1.2.840.10045.3.1.7";

			// Token: 0x040002CE RID: 718
			private const string ECDSA_P384_OID_VALUE = "1.3.132.0.34";

			// Token: 0x040002CF RID: 719
			private const string ECDSA_P521_OID_VALUE = "1.3.132.0.35";
		}
	}
}
