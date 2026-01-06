using System;
using System.Collections.Generic;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D.IK
{
	// Token: 0x02000007 RID: 7
	[MovedFrom("UnityEngine.Experimental.U2D.IK")]
	[Solver2DMenu("Limb")]
	public class LimbSolver2D : Solver2D
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002A5F File Offset: 0x00000C5F
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002A67 File Offset: 0x00000C67
		public bool flip
		{
			get
			{
				return this.m_Flip;
			}
			set
			{
				this.m_Flip = value;
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002A70 File Offset: 0x00000C70
		protected override void DoInitialize()
		{
			this.m_Chain.transformCount = ((this.m_Chain.effector == null || IKUtility.GetAncestorCount(this.m_Chain.effector) < 2) ? 0 : 3);
			base.DoInitialize();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002AAD File Offset: 0x00000CAD
		protected override int GetChainCount()
		{
			return 1;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002AB0 File Offset: 0x00000CB0
		public override IKChain2D GetChain(int index)
		{
			return this.m_Chain;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002AB8 File Offset: 0x00000CB8
		protected override void DoPrepare()
		{
			float[] lengths = this.m_Chain.lengths;
			this.m_Positions[0] = this.m_Chain.transforms[0].position;
			this.m_Positions[1] = this.m_Chain.transforms[1].position;
			this.m_Positions[2] = this.m_Chain.transforms[2].position;
			this.m_Lengths[0] = lengths[0];
			this.m_Lengths[1] = lengths[1];
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002B44 File Offset: 0x00000D44
		protected override void DoUpdateIK(List<Vector3> effectorPositions)
		{
			Vector3 vector = effectorPositions[0];
			Vector3 vector2 = this.m_Chain.transforms[0].InverseTransformPoint(vector);
			vector = this.m_Chain.transforms[0].TransformPoint(vector2);
			if (vector2.sqrMagnitude > 0f && Limb.Solve(vector, this.m_Lengths, this.m_Positions, ref this.m_Angles))
			{
				float num = (this.flip ? (-1f) : 1f);
				this.m_Chain.transforms[0].localRotation *= Quaternion.FromToRotation(Vector3.right, vector2) * Quaternion.FromToRotation(this.m_Chain.transforms[1].localPosition, Vector3.right);
				this.m_Chain.transforms[0].localRotation *= Quaternion.AngleAxis(num * this.m_Angles[0], Vector3.forward);
				this.m_Chain.transforms[1].localRotation *= Quaternion.FromToRotation(Vector3.right, this.m_Chain.transforms[1].InverseTransformPoint(vector)) * Quaternion.FromToRotation(this.m_Chain.transforms[2].localPosition, Vector3.right);
			}
		}

		// Token: 0x0400001B RID: 27
		[SerializeField]
		private IKChain2D m_Chain = new IKChain2D();

		// Token: 0x0400001C RID: 28
		[SerializeField]
		private bool m_Flip;

		// Token: 0x0400001D RID: 29
		private Vector3[] m_Positions = new Vector3[3];

		// Token: 0x0400001E RID: 30
		private float[] m_Lengths = new float[2];

		// Token: 0x0400001F RID: 31
		private float[] m_Angles = new float[2];
	}
}
