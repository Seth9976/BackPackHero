using System;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Serialization;

namespace UnityEngine.U2D.IK
{
	// Token: 0x02000004 RID: 4
	[MovedFrom("UnityEngine.Experimental.U2D.IK")]
	[Serializable]
	public class IKChain2D
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002478 File Offset: 0x00000678
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002480 File Offset: 0x00000680
		public Transform effector
		{
			get
			{
				return this.m_EffectorTransform;
			}
			set
			{
				this.m_EffectorTransform = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002489 File Offset: 0x00000689
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002491 File Offset: 0x00000691
		public Transform target
		{
			get
			{
				return this.m_TargetTransform;
			}
			set
			{
				this.m_TargetTransform = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000249A File Offset: 0x0000069A
		public Transform[] transforms
		{
			get
			{
				return this.m_Transforms;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000024A2 File Offset: 0x000006A2
		public Transform rootTransform
		{
			get
			{
				if (this.m_Transforms != null && this.transformCount > 0 && this.m_Transforms.Length == this.transformCount)
				{
					return this.m_Transforms[0];
				}
				return null;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000024CF File Offset: 0x000006CF
		private Transform lastTransform
		{
			get
			{
				if (this.m_Transforms != null && this.transformCount > 0 && this.m_Transforms.Length == this.transformCount)
				{
					return this.m_Transforms[this.transformCount - 1];
				}
				return null;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002503 File Offset: 0x00000703
		// (set) Token: 0x0600001D RID: 29 RVA: 0x0000250B File Offset: 0x0000070B
		public int transformCount
		{
			get
			{
				return this.m_TransformCount;
			}
			set
			{
				this.m_TransformCount = Mathf.Max(0, value);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000251A File Offset: 0x0000071A
		public bool isValid
		{
			get
			{
				return this.Validate();
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002522 File Offset: 0x00000722
		public float[] lengths
		{
			get
			{
				if (this.isValid)
				{
					this.PrepareLengths();
					return this.m_Lengths;
				}
				return null;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000253C File Offset: 0x0000073C
		private bool Validate()
		{
			return !(this.effector == null) && this.transformCount != 0 && this.m_Transforms != null && this.m_Transforms.Length == this.transformCount && this.m_DefaultLocalRotations != null && this.m_DefaultLocalRotations.Length == this.transformCount && this.m_StoredLocalRotations != null && this.m_StoredLocalRotations.Length == this.transformCount && !(this.rootTransform == null) && !(this.lastTransform != this.effector) && (!this.target || !IKUtility.IsDescendentOf(this.target, this.rootTransform));
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000025FC File Offset: 0x000007FC
		public void Initialize()
		{
			if (this.effector == null || this.transformCount == 0 || IKUtility.GetAncestorCount(this.effector) < this.transformCount - 1)
			{
				return;
			}
			this.m_Transforms = new Transform[this.transformCount];
			this.m_DefaultLocalRotations = new Quaternion[this.transformCount];
			this.m_StoredLocalRotations = new Quaternion[this.transformCount];
			Transform transform = this.effector;
			int num = this.transformCount - 1;
			while (transform && num >= 0)
			{
				this.m_Transforms[num] = transform;
				this.m_DefaultLocalRotations[num] = transform.localRotation;
				transform = transform.parent;
				num--;
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000026AC File Offset: 0x000008AC
		private void PrepareLengths()
		{
			Transform transform = this.effector;
			int num = this.transformCount - 1;
			if (this.m_Lengths == null || this.m_Lengths.Length != this.transformCount - 1)
			{
				this.m_Lengths = new float[this.transformCount - 1];
			}
			while (transform && num >= 0)
			{
				if (transform.parent && num > 0)
				{
					this.m_Lengths[num - 1] = (transform.position - transform.parent.position).magnitude;
				}
				transform = transform.parent;
				num--;
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002748 File Offset: 0x00000948
		public void RestoreDefaultPose(bool targetRotationIsConstrained)
		{
			int num = (targetRotationIsConstrained ? this.transformCount : (this.transformCount - 1));
			for (int i = 0; i < num; i++)
			{
				this.m_Transforms[i].localRotation = this.m_DefaultLocalRotations[i];
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002790 File Offset: 0x00000990
		public void StoreLocalRotations()
		{
			for (int i = 0; i < this.m_Transforms.Length; i++)
			{
				this.m_StoredLocalRotations[i] = this.m_Transforms[i].localRotation;
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000027CC File Offset: 0x000009CC
		public void BlendFkToIk(float finalWeight, bool targetRotationIsConstrained)
		{
			int num = (targetRotationIsConstrained ? this.transformCount : (this.transformCount - 1));
			for (int i = 0; i < num; i++)
			{
				this.m_Transforms[i].localRotation = Quaternion.Slerp(this.m_StoredLocalRotations[i], this.m_Transforms[i].localRotation, finalWeight);
			}
		}

		// Token: 0x04000012 RID: 18
		[SerializeField]
		[FormerlySerializedAs("m_Target")]
		private Transform m_EffectorTransform;

		// Token: 0x04000013 RID: 19
		[SerializeField]
		[FormerlySerializedAs("m_Effector")]
		private Transform m_TargetTransform;

		// Token: 0x04000014 RID: 20
		[SerializeField]
		private int m_TransformCount;

		// Token: 0x04000015 RID: 21
		[SerializeField]
		private Transform[] m_Transforms;

		// Token: 0x04000016 RID: 22
		[SerializeField]
		private Quaternion[] m_DefaultLocalRotations;

		// Token: 0x04000017 RID: 23
		[SerializeField]
		private Quaternion[] m_StoredLocalRotations;

		// Token: 0x04000018 RID: 24
		protected float[] m_Lengths;
	}
}
