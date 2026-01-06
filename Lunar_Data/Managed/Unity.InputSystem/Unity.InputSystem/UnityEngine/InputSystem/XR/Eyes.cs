using System;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x0200006D RID: 109
	public struct Eyes
	{
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x00035F59 File Offset: 0x00034159
		// (set) Token: 0x060009D5 RID: 2517 RVA: 0x00035F61 File Offset: 0x00034161
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

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x00035F6A File Offset: 0x0003416A
		// (set) Token: 0x060009D7 RID: 2519 RVA: 0x00035F72 File Offset: 0x00034172
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

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x00035F7B File Offset: 0x0003417B
		// (set) Token: 0x060009D9 RID: 2521 RVA: 0x00035F83 File Offset: 0x00034183
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

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x00035F8C File Offset: 0x0003418C
		// (set) Token: 0x060009DB RID: 2523 RVA: 0x00035F94 File Offset: 0x00034194
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

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x00035F9D File Offset: 0x0003419D
		// (set) Token: 0x060009DD RID: 2525 RVA: 0x00035FA5 File Offset: 0x000341A5
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

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x00035FAE File Offset: 0x000341AE
		// (set) Token: 0x060009DF RID: 2527 RVA: 0x00035FB6 File Offset: 0x000341B6
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

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x00035FBF File Offset: 0x000341BF
		// (set) Token: 0x060009E1 RID: 2529 RVA: 0x00035FC7 File Offset: 0x000341C7
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
