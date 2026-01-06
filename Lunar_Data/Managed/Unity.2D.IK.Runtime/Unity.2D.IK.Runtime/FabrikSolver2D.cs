using System;
using System.Collections.Generic;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D.IK
{
	// Token: 0x02000003 RID: 3
	[MovedFrom("UnityEngine.Experimental.U2D.IK")]
	[Solver2DMenu("Chain (FABRIK)")]
	public class FabrikSolver2D : Solver2D
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002234 File Offset: 0x00000434
		// (set) Token: 0x0600000D RID: 13 RVA: 0x0000223C File Offset: 0x0000043C
		public int iterations
		{
			get
			{
				return this.m_Iterations;
			}
			set
			{
				this.m_Iterations = Mathf.Max(value, 1);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000224B File Offset: 0x0000044B
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002253 File Offset: 0x00000453
		public float tolerance
		{
			get
			{
				return this.m_Tolerance;
			}
			set
			{
				this.m_Tolerance = Mathf.Max(value, 0.001f);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002266 File Offset: 0x00000466
		protected override int GetChainCount()
		{
			return 1;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002269 File Offset: 0x00000469
		public override IKChain2D GetChain(int index)
		{
			return this.m_Chain;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002274 File Offset: 0x00000474
		protected override void DoPrepare()
		{
			if (this.m_Positions == null || this.m_Positions.Length != this.m_Chain.transformCount)
			{
				this.m_Positions = new Vector2[this.m_Chain.transformCount];
				this.m_Lengths = new float[this.m_Chain.transformCount - 1];
				this.m_WorldPositions = new Vector3[this.m_Chain.transformCount];
			}
			for (int i = 0; i < this.m_Chain.transformCount; i++)
			{
				this.m_Positions[i] = base.GetPointOnSolverPlane(this.m_Chain.transforms[i].position);
			}
			for (int j = 0; j < this.m_Chain.transformCount - 1; j++)
			{
				this.m_Lengths[j] = (this.m_Positions[j + 1] - this.m_Positions[j]).magnitude;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002368 File Offset: 0x00000568
		protected override void DoUpdateIK(List<Vector3> effectorPositions)
		{
			Vector3 vector = effectorPositions[0];
			vector = base.GetPointOnSolverPlane(vector);
			if (FABRIK2D.Solve(vector, this.iterations, this.tolerance, this.m_Lengths, ref this.m_Positions))
			{
				for (int i = 0; i < this.m_Positions.Length; i++)
				{
					this.m_WorldPositions[i] = base.GetWorldPositionFromSolverPlanePoint(this.m_Positions[i]);
				}
				for (int j = 0; j < this.m_Chain.transformCount - 1; j++)
				{
					Vector3 localPosition = this.m_Chain.transforms[j + 1].localPosition;
					Vector3 vector2 = this.m_Chain.transforms[j].InverseTransformPoint(this.m_WorldPositions[j + 1]);
					this.m_Chain.transforms[j].localRotation *= Quaternion.FromToRotation(localPosition, vector2);
				}
			}
		}

		// Token: 0x0400000A RID: 10
		private const float k_MinTolerance = 0.001f;

		// Token: 0x0400000B RID: 11
		private const int k_MinIterations = 1;

		// Token: 0x0400000C RID: 12
		[SerializeField]
		private IKChain2D m_Chain = new IKChain2D();

		// Token: 0x0400000D RID: 13
		[SerializeField]
		[Range(1f, 50f)]
		private int m_Iterations = 10;

		// Token: 0x0400000E RID: 14
		[SerializeField]
		[Range(0.001f, 0.1f)]
		private float m_Tolerance = 0.01f;

		// Token: 0x0400000F RID: 15
		private float[] m_Lengths;

		// Token: 0x04000010 RID: 16
		private Vector2[] m_Positions;

		// Token: 0x04000011 RID: 17
		private Vector3[] m_WorldPositions;
	}
}
