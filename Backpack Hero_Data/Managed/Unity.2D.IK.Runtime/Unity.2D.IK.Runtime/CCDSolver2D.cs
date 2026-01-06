using System;
using System.Collections.Generic;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D.IK
{
	// Token: 0x02000002 RID: 2
	[MovedFrom("UnityEngine.Experimental.U2D.IK")]
	[Solver2DMenu("Chain (CCD)")]
	public class CCDSolver2D : Solver2D
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
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

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002067 File Offset: 0x00000267
		// (set) Token: 0x06000004 RID: 4 RVA: 0x0000206F File Offset: 0x0000026F
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

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002082 File Offset: 0x00000282
		// (set) Token: 0x06000006 RID: 6 RVA: 0x0000208A File Offset: 0x0000028A
		public float velocity
		{
			get
			{
				return this.m_Velocity;
			}
			set
			{
				this.m_Velocity = Mathf.Clamp01(value);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002098 File Offset: 0x00000298
		protected override int GetChainCount()
		{
			return 1;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000209B File Offset: 0x0000029B
		public override IKChain2D GetChain(int index)
		{
			return this.m_Chain;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020A4 File Offset: 0x000002A4
		protected override void DoPrepare()
		{
			if (this.m_Positions == null || this.m_Positions.Length != this.m_Chain.transformCount)
			{
				this.m_Positions = new Vector3[this.m_Chain.transformCount];
			}
			Transform rootTransform = this.m_Chain.rootTransform;
			for (int i = 0; i < this.m_Chain.transformCount; i++)
			{
				this.m_Positions[i] = rootTransform.TransformPoint(rootTransform.InverseTransformPoint(this.m_Chain.transforms[i].position));
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000213C File Offset: 0x0000033C
		protected override void DoUpdateIK(List<Vector3> targetPositions)
		{
			Transform rootTransform = this.m_Chain.rootTransform;
			Vector3 vector = targetPositions[0];
			Vector2 vector2 = rootTransform.InverseTransformPoint(vector);
			vector = rootTransform.TransformPoint(vector2);
			if (CCD2D.Solve(vector, this.GetPlaneRootTransform().forward, this.iterations, this.tolerance, Mathf.Lerp(0.01f, 1f, this.m_Velocity), ref this.m_Positions))
			{
				for (int i = 0; i < this.m_Chain.transformCount - 1; i++)
				{
					Vector2 vector3 = this.m_Chain.transforms[i + 1].localPosition;
					Vector2 vector4 = this.m_Chain.transforms[i].InverseTransformPoint(this.m_Positions[i + 1]);
					this.m_Chain.transforms[i].localRotation *= Quaternion.FromToRotation(vector3, vector4);
				}
			}
		}

		// Token: 0x04000001 RID: 1
		private const int k_MinIterations = 1;

		// Token: 0x04000002 RID: 2
		private const float k_MinTolerance = 0.001f;

		// Token: 0x04000003 RID: 3
		private const float k_MinVelocity = 0.01f;

		// Token: 0x04000004 RID: 4
		private const float k_MaxVelocity = 1f;

		// Token: 0x04000005 RID: 5
		[SerializeField]
		private IKChain2D m_Chain = new IKChain2D();

		// Token: 0x04000006 RID: 6
		[SerializeField]
		[Range(1f, 50f)]
		private int m_Iterations = 10;

		// Token: 0x04000007 RID: 7
		[SerializeField]
		[Range(0.001f, 0.1f)]
		private float m_Tolerance = 0.01f;

		// Token: 0x04000008 RID: 8
		[SerializeField]
		[Range(0f, 1f)]
		private float m_Velocity = 0.5f;

		// Token: 0x04000009 RID: 9
		private Vector3[] m_Positions;
	}
}
