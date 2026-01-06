using System;
using System.Collections.Generic;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Serialization;

namespace UnityEngine.U2D.IK
{
	// Token: 0x02000008 RID: 8
	[MovedFrom("UnityEngine.Experimental.U2D.IK")]
	public abstract class Solver2D : MonoBehaviour
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002CD0 File Offset: 0x00000ED0
		public int chainCount
		{
			get
			{
				return this.GetChainCount();
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002CD8 File Offset: 0x00000ED8
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public bool constrainRotation
		{
			get
			{
				return this.m_ConstrainRotation;
			}
			set
			{
				this.m_ConstrainRotation = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002CE9 File Offset: 0x00000EE9
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002CF1 File Offset: 0x00000EF1
		public bool solveFromDefaultPose
		{
			get
			{
				return this.m_SolveFromDefaultPose;
			}
			set
			{
				this.m_SolveFromDefaultPose = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002CFA File Offset: 0x00000EFA
		public bool isValid
		{
			get
			{
				return this.Validate();
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002D02 File Offset: 0x00000F02
		public bool allChainsHaveTargets
		{
			get
			{
				return this.HasTargets();
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002D0A File Offset: 0x00000F0A
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002D12 File Offset: 0x00000F12
		public float weight
		{
			get
			{
				return this.m_Weight;
			}
			set
			{
				this.m_Weight = Mathf.Clamp01(value);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002D20 File Offset: 0x00000F20
		protected virtual void OnValidate()
		{
			this.m_Weight = Mathf.Clamp01(this.m_Weight);
			if (!this.isValid)
			{
				this.Initialize();
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002D44 File Offset: 0x00000F44
		private bool Validate()
		{
			for (int i = 0; i < this.GetChainCount(); i++)
			{
				if (!this.GetChain(i).isValid)
				{
					return false;
				}
			}
			return this.DoValidate();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002D78 File Offset: 0x00000F78
		private bool HasTargets()
		{
			for (int i = 0; i < this.GetChainCount(); i++)
			{
				if (this.GetChain(i).target == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002DB0 File Offset: 0x00000FB0
		public void Initialize()
		{
			this.DoInitialize();
			for (int i = 0; i < this.GetChainCount(); i++)
			{
				this.GetChain(i).Initialize();
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002DE0 File Offset: 0x00000FE0
		private void Prepare()
		{
			Transform planeRootTransform = this.GetPlaneRootTransform();
			if (planeRootTransform != null)
			{
				this.m_Plane.normal = planeRootTransform.forward;
				this.m_Plane.distance = -Vector3.Dot(this.m_Plane.normal, planeRootTransform.position);
			}
			for (int i = 0; i < this.GetChainCount(); i++)
			{
				IKChain2D chain = this.GetChain(i);
				bool flag = this.constrainRotation && chain.target != null;
				if (this.m_SolveFromDefaultPose)
				{
					chain.RestoreDefaultPose(flag);
				}
			}
			this.DoPrepare();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002E78 File Offset: 0x00001078
		private void PrepareEffectorPositions()
		{
			this.m_TargetPositions.Clear();
			for (int i = 0; i < this.GetChainCount(); i++)
			{
				IKChain2D chain = this.GetChain(i);
				if (chain.target)
				{
					this.m_TargetPositions.Add(chain.target.position);
				}
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002ECC File Offset: 0x000010CC
		public void UpdateIK(float globalWeight)
		{
			if (this.allChainsHaveTargets)
			{
				this.PrepareEffectorPositions();
				this.UpdateIK(this.m_TargetPositions, globalWeight);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002EEC File Offset: 0x000010EC
		public void UpdateIK(List<Vector3> positions, float globalWeight)
		{
			if (positions.Count != this.chainCount)
			{
				return;
			}
			float num = globalWeight * this.weight;
			bool flag = Math.Abs(num - this.m_LastFinalWeight) > 0.0001f;
			this.m_LastFinalWeight = num;
			if (num == 0f && !flag)
			{
				return;
			}
			if (!this.isValid)
			{
				return;
			}
			this.Prepare();
			if (num < 1f)
			{
				this.StoreLocalRotations();
			}
			this.DoUpdateIK(positions);
			if (this.constrainRotation)
			{
				for (int i = 0; i < this.GetChainCount(); i++)
				{
					IKChain2D chain = this.GetChain(i);
					if (chain.target)
					{
						chain.effector.rotation = chain.target.rotation;
					}
				}
			}
			if (num < 1f)
			{
				this.BlendFkToIk(num);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002FB0 File Offset: 0x000011B0
		private void StoreLocalRotations()
		{
			for (int i = 0; i < this.GetChainCount(); i++)
			{
				this.GetChain(i).StoreLocalRotations();
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002FDC File Offset: 0x000011DC
		private void BlendFkToIk(float finalWeight)
		{
			for (int i = 0; i < this.GetChainCount(); i++)
			{
				IKChain2D chain = this.GetChain(i);
				bool flag = this.constrainRotation && chain.target != null;
				chain.BlendFkToIk(finalWeight, flag);
			}
		}

		// Token: 0x06000054 RID: 84
		public abstract IKChain2D GetChain(int index);

		// Token: 0x06000055 RID: 85
		protected abstract int GetChainCount();

		// Token: 0x06000056 RID: 86
		protected abstract void DoUpdateIK(List<Vector3> effectorPositions);

		// Token: 0x06000057 RID: 87 RVA: 0x00003022 File Offset: 0x00001222
		protected virtual bool DoValidate()
		{
			return true;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003025 File Offset: 0x00001225
		protected virtual void DoInitialize()
		{
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003027 File Offset: 0x00001227
		protected virtual void DoPrepare()
		{
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003029 File Offset: 0x00001229
		protected virtual Transform GetPlaneRootTransform()
		{
			if (this.chainCount <= 0)
			{
				return null;
			}
			return this.GetChain(0).rootTransform;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003042 File Offset: 0x00001242
		protected Vector3 GetPointOnSolverPlane(Vector3 worldPosition)
		{
			return this.GetPlaneRootTransform().InverseTransformPoint(this.m_Plane.ClosestPointOnPlane(worldPosition));
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000305B File Offset: 0x0000125B
		protected Vector3 GetWorldPositionFromSolverPlanePoint(Vector2 planePoint)
		{
			return this.GetPlaneRootTransform().TransformPoint(planePoint);
		}

		// Token: 0x04000020 RID: 32
		[SerializeField]
		private bool m_ConstrainRotation = true;

		// Token: 0x04000021 RID: 33
		[FormerlySerializedAs("m_RestoreDefaultPose")]
		[SerializeField]
		private bool m_SolveFromDefaultPose = true;

		// Token: 0x04000022 RID: 34
		[SerializeField]
		[Range(0f, 1f)]
		private float m_Weight = 1f;

		// Token: 0x04000023 RID: 35
		private Plane m_Plane;

		// Token: 0x04000024 RID: 36
		private List<Vector3> m_TargetPositions = new List<Vector3>();

		// Token: 0x04000025 RID: 37
		private float m_LastFinalWeight;
	}
}
