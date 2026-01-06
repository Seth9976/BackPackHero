using System;

namespace Pathfinding.Poly2Tri
{
	// Token: 0x0200000D RID: 13
	public class DTSweepDebugContext : TriangulationDebugContext
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00004DB4 File Offset: 0x00002FB4
		public DTSweepDebugContext(DTSweepContext tcx)
			: base(tcx)
		{
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00004DC0 File Offset: 0x00002FC0
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00004DC8 File Offset: 0x00002FC8
		public DelaunayTriangle PrimaryTriangle
		{
			get
			{
				return this._primaryTriangle;
			}
			set
			{
				this._primaryTriangle = value;
				this._tcx.Update("set PrimaryTriangle");
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00004DE4 File Offset: 0x00002FE4
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00004DEC File Offset: 0x00002FEC
		public DelaunayTriangle SecondaryTriangle
		{
			get
			{
				return this._secondaryTriangle;
			}
			set
			{
				this._secondaryTriangle = value;
				this._tcx.Update("set SecondaryTriangle");
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00004E08 File Offset: 0x00003008
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00004E10 File Offset: 0x00003010
		public TriangulationPoint ActivePoint
		{
			get
			{
				return this._activePoint;
			}
			set
			{
				this._activePoint = value;
				this._tcx.Update("set ActivePoint");
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00004E2C File Offset: 0x0000302C
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00004E34 File Offset: 0x00003034
		public AdvancingFrontNode ActiveNode
		{
			get
			{
				return this._activeNode;
			}
			set
			{
				this._activeNode = value;
				this._tcx.Update("set ActiveNode");
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00004E50 File Offset: 0x00003050
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00004E58 File Offset: 0x00003058
		public DTSweepConstraint ActiveConstraint
		{
			get
			{
				return this._activeConstraint;
			}
			set
			{
				this._activeConstraint = value;
				this._tcx.Update("set ActiveConstraint");
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00004E74 File Offset: 0x00003074
		public bool IsDebugContext
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004E78 File Offset: 0x00003078
		public override void Clear()
		{
			this.PrimaryTriangle = null;
			this.SecondaryTriangle = null;
			this.ActivePoint = null;
			this.ActiveNode = null;
			this.ActiveConstraint = null;
		}

		// Token: 0x04000025 RID: 37
		private DelaunayTriangle _primaryTriangle;

		// Token: 0x04000026 RID: 38
		private DelaunayTriangle _secondaryTriangle;

		// Token: 0x04000027 RID: 39
		private TriangulationPoint _activePoint;

		// Token: 0x04000028 RID: 40
		private AdvancingFrontNode _activeNode;

		// Token: 0x04000029 RID: 41
		private DTSweepConstraint _activeConstraint;
	}
}
