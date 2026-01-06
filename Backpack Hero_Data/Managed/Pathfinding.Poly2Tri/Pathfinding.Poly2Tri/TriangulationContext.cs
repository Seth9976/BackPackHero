using System;
using System.Collections.Generic;

namespace Pathfinding.Poly2Tri
{
	// Token: 0x02000017 RID: 23
	public abstract class TriangulationContext
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00005148 File Offset: 0x00003348
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00005150 File Offset: 0x00003350
		public TriangulationDebugContext DebugContext { get; protected set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000515C File Offset: 0x0000335C
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00005164 File Offset: 0x00003364
		public TriangulationMode TriangulationMode { get; protected set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00005170 File Offset: 0x00003370
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00005178 File Offset: 0x00003378
		public Triangulatable Triangulatable { get; private set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00005184 File Offset: 0x00003384
		// (set) Token: 0x060000BE RID: 190 RVA: 0x0000518C File Offset: 0x0000338C
		public int StepCount { get; private set; }

		// Token: 0x060000BF RID: 191 RVA: 0x00005198 File Offset: 0x00003398
		public void Done()
		{
			this.StepCount++;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000C0 RID: 192
		public abstract TriangulationAlgorithm Algorithm { get; }

		// Token: 0x060000C1 RID: 193 RVA: 0x000051A8 File Offset: 0x000033A8
		public virtual void PrepareTriangulation(Triangulatable t)
		{
			this.Triangulatable = t;
			this.TriangulationMode = t.TriangulationMode;
			t.Prepare(this);
		}

		// Token: 0x060000C2 RID: 194
		public abstract TriangulationConstraint NewConstraint(TriangulationPoint a, TriangulationPoint b);

		// Token: 0x060000C3 RID: 195 RVA: 0x000051D0 File Offset: 0x000033D0
		public void Update(string message)
		{
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000051D4 File Offset: 0x000033D4
		public virtual void Clear()
		{
			this.Points.Clear();
			if (this.DebugContext != null)
			{
				this.DebugContext.Clear();
			}
			this.StepCount = 0;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x0000520C File Offset: 0x0000340C
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00005214 File Offset: 0x00003414
		public virtual bool IsDebugEnabled { get; protected set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00005220 File Offset: 0x00003420
		public DTSweepDebugContext DTDebugContext
		{
			get
			{
				return this.DebugContext as DTSweepDebugContext;
			}
		}

		// Token: 0x0400003A RID: 58
		public readonly List<DelaunayTriangle> Triangles = new List<DelaunayTriangle>();

		// Token: 0x0400003B RID: 59
		public readonly List<TriangulationPoint> Points = new List<TriangulationPoint>(200);
	}
}
