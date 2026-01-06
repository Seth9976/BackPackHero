using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200019E RID: 414
	[UsedByNativeCode]
	[Serializable]
	public struct BoneWeight : IEquatable<BoneWeight>
	{
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x00015B0C File Offset: 0x00013D0C
		// (set) Token: 0x0600108F RID: 4239 RVA: 0x00015B24 File Offset: 0x00013D24
		public float weight0
		{
			get
			{
				return this.m_Weight0;
			}
			set
			{
				this.m_Weight0 = value;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x00015B30 File Offset: 0x00013D30
		// (set) Token: 0x06001091 RID: 4241 RVA: 0x00015B48 File Offset: 0x00013D48
		public float weight1
		{
			get
			{
				return this.m_Weight1;
			}
			set
			{
				this.m_Weight1 = value;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06001092 RID: 4242 RVA: 0x00015B54 File Offset: 0x00013D54
		// (set) Token: 0x06001093 RID: 4243 RVA: 0x00015B6C File Offset: 0x00013D6C
		public float weight2
		{
			get
			{
				return this.m_Weight2;
			}
			set
			{
				this.m_Weight2 = value;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x00015B78 File Offset: 0x00013D78
		// (set) Token: 0x06001095 RID: 4245 RVA: 0x00015B90 File Offset: 0x00013D90
		public float weight3
		{
			get
			{
				return this.m_Weight3;
			}
			set
			{
				this.m_Weight3 = value;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06001096 RID: 4246 RVA: 0x00015B9C File Offset: 0x00013D9C
		// (set) Token: 0x06001097 RID: 4247 RVA: 0x00015BB4 File Offset: 0x00013DB4
		public int boneIndex0
		{
			get
			{
				return this.m_BoneIndex0;
			}
			set
			{
				this.m_BoneIndex0 = value;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06001098 RID: 4248 RVA: 0x00015BC0 File Offset: 0x00013DC0
		// (set) Token: 0x06001099 RID: 4249 RVA: 0x00015BD8 File Offset: 0x00013DD8
		public int boneIndex1
		{
			get
			{
				return this.m_BoneIndex1;
			}
			set
			{
				this.m_BoneIndex1 = value;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x00015BE4 File Offset: 0x00013DE4
		// (set) Token: 0x0600109B RID: 4251 RVA: 0x00015BFC File Offset: 0x00013DFC
		public int boneIndex2
		{
			get
			{
				return this.m_BoneIndex2;
			}
			set
			{
				this.m_BoneIndex2 = value;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x0600109C RID: 4252 RVA: 0x00015C08 File Offset: 0x00013E08
		// (set) Token: 0x0600109D RID: 4253 RVA: 0x00015C20 File Offset: 0x00013E20
		public int boneIndex3
		{
			get
			{
				return this.m_BoneIndex3;
			}
			set
			{
				this.m_BoneIndex3 = value;
			}
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x00015C2C File Offset: 0x00013E2C
		public override int GetHashCode()
		{
			return this.boneIndex0.GetHashCode() ^ (this.boneIndex1.GetHashCode() << 2) ^ (this.boneIndex2.GetHashCode() >> 2) ^ (this.boneIndex3.GetHashCode() >> 1) ^ (this.weight0.GetHashCode() << 5) ^ (this.weight1.GetHashCode() << 4) ^ (this.weight2.GetHashCode() >> 4) ^ (this.weight3.GetHashCode() >> 3);
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00015CC4 File Offset: 0x00013EC4
		public override bool Equals(object other)
		{
			return other is BoneWeight && this.Equals((BoneWeight)other);
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x00015CF0 File Offset: 0x00013EF0
		public bool Equals(BoneWeight other)
		{
			return this.boneIndex0.Equals(other.boneIndex0) && this.boneIndex1.Equals(other.boneIndex1) && this.boneIndex2.Equals(other.boneIndex2) && this.boneIndex3.Equals(other.boneIndex3) && new Vector4(this.weight0, this.weight1, this.weight2, this.weight3).Equals(new Vector4(other.weight0, other.weight1, other.weight2, other.weight3));
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x00015DAC File Offset: 0x00013FAC
		public static bool operator ==(BoneWeight lhs, BoneWeight rhs)
		{
			return lhs.boneIndex0 == rhs.boneIndex0 && lhs.boneIndex1 == rhs.boneIndex1 && lhs.boneIndex2 == rhs.boneIndex2 && lhs.boneIndex3 == rhs.boneIndex3 && new Vector4(lhs.weight0, lhs.weight1, lhs.weight2, lhs.weight3) == new Vector4(rhs.weight0, rhs.weight1, rhs.weight2, rhs.weight3);
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x00015E48 File Offset: 0x00014048
		public static bool operator !=(BoneWeight lhs, BoneWeight rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x040005A7 RID: 1447
		[SerializeField]
		private float m_Weight0;

		// Token: 0x040005A8 RID: 1448
		[SerializeField]
		private float m_Weight1;

		// Token: 0x040005A9 RID: 1449
		[SerializeField]
		private float m_Weight2;

		// Token: 0x040005AA RID: 1450
		[SerializeField]
		private float m_Weight3;

		// Token: 0x040005AB RID: 1451
		[SerializeField]
		private int m_BoneIndex0;

		// Token: 0x040005AC RID: 1452
		[SerializeField]
		private int m_BoneIndex1;

		// Token: 0x040005AD RID: 1453
		[SerializeField]
		private int m_BoneIndex2;

		// Token: 0x040005AE RID: 1454
		[SerializeField]
		private int m_BoneIndex3;
	}
}
