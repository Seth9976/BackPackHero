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
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000287D File Offset: 0x00000A7D
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002885 File Offset: 0x00000A85
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
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002893 File Offset: 0x00000A93
		public List<Solver2D> solvers
		{
			get
			{
				return this.m_Solvers;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000289B File Offset: 0x00000A9B
		private void OnValidate()
		{
			this.m_Weight = Mathf.Clamp01(this.m_Weight);
			this.OnEditorDataValidate();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000028B4 File Offset: 0x00000AB4
		private void Reset()
		{
			this.FindChildSolvers();
			this.OnEditorDataValidate();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000028C4 File Offset: 0x00000AC4
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

		// Token: 0x0600002D RID: 45 RVA: 0x00002940 File Offset: 0x00000B40
		public void AddSolver(Solver2D solver)
		{
			if (!this.m_Solvers.Contains(solver))
			{
				this.m_Solvers.Add(solver);
				this.AddSolverEditorData();
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002962 File Offset: 0x00000B62
		public void RemoveSolver(Solver2D solver)
		{
			this.RemoveSolverEditorData(solver);
			this.m_Solvers.Remove(solver);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002978 File Offset: 0x00000B78
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

		// Token: 0x06000030 RID: 48 RVA: 0x000029F0 File Offset: 0x00000BF0
		private void LateUpdate()
		{
			this.UpdateManager();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000029F8 File Offset: 0x00000BF8
		private void OnEditorDataValidate()
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000029FA File Offset: 0x00000BFA
		private void AddSolverEditorData()
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000029FC File Offset: 0x00000BFC
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
