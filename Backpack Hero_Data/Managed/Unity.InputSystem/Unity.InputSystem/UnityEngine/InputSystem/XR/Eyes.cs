using System;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x0200006D RID: 109
	public struct Eyes
	{
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x00035F95 File Offset: 0x00034195
		// (set) Token: 0x060009D7 RID: 2519 RVA: 0x00035F9D File Offset: 0x0003419D
		public Vector3 leftEyePosition
		{
			get
			{
				return this.m_LeftEyePosition;
			}
			set
			{
				this.m_LeftEyePosition = value;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x00035FA6 File Offset: 0x000341A6
		// (set) Token: 0x060009D9 RID: 2521 RVA: 0x00035FAE File Offset: 0x000341AE
		public Quaternion leftEyeRotation
		{
			get
			{
				return this.m_LeftEyeRotation;
			}
			set
			{
				this.m_LeftEyeRotation = value;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x00035FB7 File Offset: 0x000341B7
		// (set) Token: 0x060009DB RID: 2523 RVA: 0x00035FBF File Offset: 0x000341BF
		public Vector3 rightEyePosition
		{
			get
			{
				return this.m_RightEyePosition;
			}
			set
			{
				this.m_RightEyePosition = value;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x00035FC8 File Offset: 0x000341C8
		// (set) Token: 0x060009DD RID: 2525 RVA: 0x00035FD0 File Offset: 0x000341D0
		public Quaternion rightEyeRotation
		{
			get
			{
				return this.m_RightEyeRotation;
			}
			set
			{
				this.m_RightEyeRotation = value;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x00035FD9 File Offset: 0x000341D9
		// (set) Token: 0x060009DF RID: 2527 RVA: 0x00035FE1 File Offset: 0x000341E1
		public Vector3 fixationPoint
		{
			get
			{
				return this.m_FixationPoint;
			}
			set
			{
				this.m_FixationPoint = value;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x00035FEA File Offset: 0x000341EA
		// (set) Token: 0x060009E1 RID: 2529 RVA: 0x00035FF2 File Offset: 0x000341F2
		public float leftEyeOpenAmount
		{
			get
			{
				return this.m_LeftEyeOpenAmount;
			}
			set
			{
				this.m_LeftEyeOpenAmount = value;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x00035FFB File Offset: 0x000341FB
		// (set) Token: 0x060009E3 RID: 2531 RVA: 0x00036003 File Offset: 0x00034203
		public float rightEyeOpenAmount
		{
			get
			{
				return this.m_RightEyeOpenAmount;
			}
			set
			{
				this.m_RightEyeOpenAmount = value;
			}
		}

		// Token: 0x04000347 RID: 839
		public Vector3 m_LeftEyePosition;

		// Token: 0x04000348 RID: 840
		public Quaternion m_LeftEyeRotation;

		// Token: 0x04000349 RID: 841
		public Vector3 m_RightEyePosition;

		// Token: 0x0400034A RID: 842
		public Quaternion m_RightEyeRotation;

		// Token: 0x0400034B RID: 843
		public Vector3 m_FixationPoint;

		// Token: 0x0400034C RID: 844
		public float m_LeftEyeOpenAmount;

		// Token: 0x0400034D RID: 845
		public float m_RightEyeOpenAmount;
	}
}
