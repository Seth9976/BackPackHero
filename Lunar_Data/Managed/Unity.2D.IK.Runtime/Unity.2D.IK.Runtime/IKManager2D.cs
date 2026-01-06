using System;
using System.Collections.Generic;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D.IK
{
	// Token: 0x02000005 RID: 5
	[DefaultExecutionOrder(-2)]
	[MovedFrom("UnityEngine.Experimental.U2D.IK")]
	[ExecuteInEditMode]
	public class IKManager2D : MonoBehaviour
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000282D File Offset: 0x00000A2D
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002835 File Offset: 0x00000A35
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

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002843 File Offset: 0x00000A43
		public List<Solver2D> solvers
		{
			get
			{
				return this.m_Solvers;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000284B File Offset: 0x00000A4B
		private void OnValidate()
		{
			this.m_Weight = Mathf.Clamp01(this.m_Weight);
			this.OnEditorDataValidate();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002864 File Offset: 0x00000A64
		private void Reset()
		{
			this.FindChildSolvers();
			this.OnEditorDataValidate();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002874 File Offset: 0x00000A74
		private void FindChildSolvers()
		{
			this.m_Solvers.Clear();
			List<Solver2D> list = new List<Solver2D>();
			base.transform.GetComponentsInChildren<Solver2D>(true, list);
			foreach (Solver2D solver2D in list)
			{
				if (solver2D.GetComponentInParent<IKManager2D>() == this)
				{
					this.AddSolver(solver2D);
				}
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000028F0 File Offset: 0x00000AF0
		public void AddSolver(Solver2D solver)
		{
			if (!this.m_Solvers.Contains(solver))
			{
				this.m_Solvers.Add(solver);
				this.AddSolverEditorData();
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002912 File Offset: 0x00000B12
		public void RemoveSolver(Solver2D solver)
		{
			this.RemoveSolverEditorData(solver);
			this.m_Solvers.Remove(solver);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002928 File Offset: 0x00000B28
		public void UpdateManager()
		{
			foreach (Solver2D solver2D in this.m_Solvers)
			{
				if (!(solver2D == null) && solver2D.isActiveAndEnabled)
				{
					if (!solver2D.isValid)
					{
						solver2D.Initialize();
					}
					solver2D.UpdateIK(this.weight);
				}
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000029A0 File Offset: 0x00000BA0
		private void LateUpdate()
		{
			this.UpdateManager();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000029A8 File Offset: 0x00000BA8
		private void OnEditorDataValidate()
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000029AA File Offset: 0x00000BAA
		private void AddSolverEditorData()
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000029AC File Offset: 0x00000BAC
		private void RemoveSolverEditorData(Solver2D solver)
		{
		}

		// Token: 0x04000019 RID: 25
		[SerializeField]
		private List<Solver2D> m_Solvers = new List<Solver2D>();

		// Token: 0x0400001A RID: 26
		[SerializeField]
		[Range(0f, 1f)]
		private float m_Weight = 1f;
	}
}
