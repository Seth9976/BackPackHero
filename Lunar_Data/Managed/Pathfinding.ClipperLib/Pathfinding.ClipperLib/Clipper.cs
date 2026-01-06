using System;
using System.Collections.Generic;

namespace Pathfinding.ClipperLib
{
	// Token: 0x02000017 RID: 23
	public class Clipper : ClipperBase
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00004898 File Offset: 0x00002A98
		public Clipper(int InitOptions = 0)
		{
			this.m_Scanbeam = null;
			this.m_ActiveEdges = null;
			this.m_SortedEdges = null;
			this.m_IntersectNodes = null;
			this.m_ExecuteLocked = false;
			this.m_UsingPolyTree = false;
			this.m_PolyOuts = new List<OutRec>();
			this.m_Joins = new List<Join>();
			this.m_GhostJoins = new List<Join>();
			this.ReverseSolution = (1 & InitOptions) != 0;
			this.StrictlySimple = (2 & InitOptions) != 0;
			base.PreserveCollinear = (4 & InitOptions) != 0;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004924 File Offset: 0x00002B24
		public override void Clear()
		{
			if (this.m_edges.Count == 0)
			{
				return;
			}
			this.DisposeAllPolyPts();
			base.Clear();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004944 File Offset: 0x00002B44
		private void DisposeScanbeamList()
		{
			while (this.m_Scanbeam != null)
			{
				Scanbeam next = this.m_Scanbeam.Next;
				this.m_Scanbeam = null;
				this.m_Scanbeam = next;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000497C File Offset: 0x00002B7C
		protected override void Reset()
		{
			base.Reset();
			this.m_Scanbeam = null;
			this.m_ActiveEdges = null;
			this.m_SortedEdges = null;
			this.DisposeAllPolyPts();
			for (LocalMinima localMinima = this.m_MinimaList; localMinima != null; localMinima = localMinima.Next)
			{
				this.InsertScanbeam(localMinima.Y);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000049D0 File Offset: 0x00002BD0
		// (set) Token: 0x06000062 RID: 98 RVA: 0x000049D8 File Offset: 0x00002BD8
		public bool ReverseSolution { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000049E4 File Offset: 0x00002BE4
		// (set) Token: 0x06000064 RID: 100 RVA: 0x000049EC File Offset: 0x00002BEC
		public bool StrictlySimple { get; set; }

		// Token: 0x06000065 RID: 101 RVA: 0x000049F8 File Offset: 0x00002BF8
		private void InsertScanbeam(long Y)
		{
			if (this.m_Scanbeam == null)
			{
				this.m_Scanbeam = new Scanbeam();
				this.m_Scanbeam.Next = null;
				this.m_Scanbeam.Y = Y;
			}
			else if (Y > this.m_Scanbeam.Y)
			{
				this.m_Scanbeam = new Scanbeam
				{
					Y = Y,
					Next = this.m_Scanbeam
				};
			}
			else
			{
				Scanbeam scanbeam = this.m_Scanbeam;
				while (scanbeam.Next != null && Y <= scanbeam.Next.Y)
				{
					scanbeam = scanbeam.Next;
				}
				if (Y == scanbeam.Y)
				{
					return;
				}
				scanbeam.Next = new Scanbeam
				{
					Y = Y,
					Next = scanbeam.Next
				};
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004ACC File Offset: 0x00002CCC
		public bool Execute(ClipType clipType, List<List<IntPoint>> solution, PolyFillType subjFillType, PolyFillType clipFillType)
		{
			if (this.m_ExecuteLocked)
			{
				return false;
			}
			if (this.m_HasOpenPaths)
			{
				throw new ClipperException("Error: PolyTree struct is need for open path clipping.");
			}
			this.m_ExecuteLocked = true;
			solution.Clear();
			this.m_SubjFillType = subjFillType;
			this.m_ClipFillType = clipFillType;
			this.m_ClipType = clipType;
			this.m_UsingPolyTree = false;
			bool flag = this.ExecuteInternal();
			if (flag)
			{
				this.BuildResult(solution);
			}
			this.m_ExecuteLocked = false;
			return flag;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004B44 File Offset: 0x00002D44
		public bool Execute(ClipType clipType, PolyTree polytree, PolyFillType subjFillType, PolyFillType clipFillType)
		{
			if (this.m_ExecuteLocked)
			{
				return false;
			}
			this.m_ExecuteLocked = true;
			this.m_SubjFillType = subjFillType;
			this.m_ClipFillType = clipFillType;
			this.m_ClipType = clipType;
			this.m_UsingPolyTree = true;
			bool flag = this.ExecuteInternal();
			if (flag)
			{
				this.BuildResult2(polytree);
			}
			this.m_ExecuteLocked = false;
			return flag;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004BA0 File Offset: 0x00002DA0
		public bool Execute(ClipType clipType, List<List<IntPoint>> solution)
		{
			return this.Execute(clipType, solution, PolyFillType.pftEvenOdd, PolyFillType.pftEvenOdd);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004BAC File Offset: 0x00002DAC
		public bool Execute(ClipType clipType, PolyTree polytree)
		{
			return this.Execute(clipType, polytree, PolyFillType.pftEvenOdd, PolyFillType.pftEvenOdd);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004BB8 File Offset: 0x00002DB8
		internal void FixHoleLinkage(OutRec outRec)
		{
			if (outRec.FirstLeft == null || (outRec.IsHole != outRec.FirstLeft.IsHole && outRec.FirstLeft.Pts != null))
			{
				return;
			}
			OutRec outRec2 = outRec.FirstLeft;
			while (outRec2 != null && (outRec2.IsHole == outRec.IsHole || outRec2.Pts == null))
			{
				outRec2 = outRec2.FirstLeft;
			}
			outRec.FirstLeft = outRec2;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004C34 File Offset: 0x00002E34
		private bool ExecuteInternal()
		{
			bool flag;
			try
			{
				this.Reset();
				if (this.m_CurrentLM == null)
				{
					flag = false;
				}
				else
				{
					long num = this.PopScanbeam();
					do
					{
						this.InsertLocalMinimaIntoAEL(num);
						this.m_GhostJoins.Clear();
						this.ProcessHorizontals(false);
						if (this.m_Scanbeam == null)
						{
							break;
						}
						long num2 = this.PopScanbeam();
						if (!this.ProcessIntersections(num, num2))
						{
							goto Block_4;
						}
						this.ProcessEdgesAtTopOfScanbeam(num2);
						num = num2;
					}
					while (this.m_Scanbeam != null || this.m_CurrentLM != null);
					goto IL_0082;
					Block_4:
					return false;
					IL_0082:
					for (int i = 0; i < this.m_PolyOuts.Count; i++)
					{
						OutRec outRec = this.m_PolyOuts[i];
						if (outRec.Pts != null && !outRec.IsOpen)
						{
							if ((outRec.IsHole ^ this.ReverseSolution) == this.Area(outRec) > 0.0)
							{
								this.ReversePolyPtLinks(outRec.Pts);
							}
						}
					}
					this.JoinCommonEdges();
					for (int j = 0; j < this.m_PolyOuts.Count; j++)
					{
						OutRec outRec2 = this.m_PolyOuts[j];
						if (outRec2.Pts != null && !outRec2.IsOpen)
						{
							this.FixupOutPolygon(outRec2);
						}
					}
					if (this.StrictlySimple)
					{
						this.DoSimplePolygons();
					}
					flag = true;
				}
			}
			finally
			{
				this.m_Joins.Clear();
				this.m_GhostJoins.Clear();
			}
			return flag;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004DE4 File Offset: 0x00002FE4
		private long PopScanbeam()
		{
			long y = this.m_Scanbeam.Y;
			Scanbeam scanbeam = this.m_Scanbeam;
			this.m_Scanbeam = this.m_Scanbeam.Next;
			return y;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004E18 File Offset: 0x00003018
		private void DisposeAllPolyPts()
		{
			for (int i = 0; i < this.m_PolyOuts.Count; i++)
			{
				this.DisposeOutRec(i);
			}
			this.m_PolyOuts.Clear();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004E54 File Offset: 0x00003054
		private void DisposeOutRec(int index)
		{
			OutRec outRec = this.m_PolyOuts[index];
			if (outRec.Pts != null)
			{
				this.DisposeOutPts(outRec.Pts);
			}
			this.m_PolyOuts[index] = null;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004E94 File Offset: 0x00003094
		private void DisposeOutPts(OutPt pp)
		{
			if (pp == null)
			{
				return;
			}
			pp.Prev.Next = null;
			while (pp != null)
			{
				pp = pp.Next;
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004ED0 File Offset: 0x000030D0
		private void AddJoin(OutPt Op1, OutPt Op2, IntPoint OffPt)
		{
			Join join = new Join();
			join.OutPt1 = Op1;
			join.OutPt2 = Op2;
			join.OffPt = OffPt;
			this.m_Joins.Add(join);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004F04 File Offset: 0x00003104
		private void AddGhostJoin(OutPt Op, IntPoint OffPt)
		{
			Join join = new Join();
			join.OutPt1 = Op;
			join.OffPt = OffPt;
			this.m_GhostJoins.Add(join);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004F34 File Offset: 0x00003134
		private void InsertLocalMinimaIntoAEL(long botY)
		{
			while (this.m_CurrentLM != null && this.m_CurrentLM.Y == botY)
			{
				TEdge leftBound = this.m_CurrentLM.LeftBound;
				TEdge rightBound = this.m_CurrentLM.RightBound;
				base.PopLocalMinima();
				OutPt outPt = null;
				if (leftBound == null)
				{
					this.InsertEdgeIntoAEL(rightBound, null);
					this.SetWindingCount(rightBound);
					if (this.IsContributing(rightBound))
					{
						outPt = this.AddOutPt(rightBound, rightBound.Bot);
					}
				}
				else
				{
					this.InsertEdgeIntoAEL(leftBound, null);
					this.InsertEdgeIntoAEL(rightBound, leftBound);
					this.SetWindingCount(leftBound);
					rightBound.WindCnt = leftBound.WindCnt;
					rightBound.WindCnt2 = leftBound.WindCnt2;
					if (this.IsContributing(leftBound))
					{
						outPt = this.AddLocalMinPoly(leftBound, rightBound, leftBound.Bot);
					}
					this.InsertScanbeam(leftBound.Top.Y);
				}
				if (ClipperBase.IsHorizontal(rightBound))
				{
					this.AddEdgeToSEL(rightBound);
				}
				else
				{
					this.InsertScanbeam(rightBound.Top.Y);
				}
				if (leftBound != null)
				{
					if (outPt != null && ClipperBase.IsHorizontal(rightBound) && this.m_GhostJoins.Count > 0 && rightBound.WindDelta != 0)
					{
						for (int i = 0; i < this.m_GhostJoins.Count; i++)
						{
							Join join = this.m_GhostJoins[i];
							if (this.HorzSegmentsOverlap(join.OutPt1.Pt, join.OffPt, rightBound.Bot, rightBound.Top))
							{
								this.AddJoin(join.OutPt1, outPt, join.OffPt);
							}
						}
					}
					if (leftBound.OutIdx >= 0 && leftBound.PrevInAEL != null && leftBound.PrevInAEL.Curr.X == leftBound.Bot.X && leftBound.PrevInAEL.OutIdx >= 0 && ClipperBase.SlopesEqual(leftBound.PrevInAEL, leftBound, this.m_UseFullRange) && leftBound.WindDelta != 0 && leftBound.PrevInAEL.WindDelta != 0)
					{
						OutPt outPt2 = this.AddOutPt(leftBound.PrevInAEL, leftBound.Bot);
						this.AddJoin(outPt, outPt2, leftBound.Top);
					}
					if (leftBound.NextInAEL != rightBound)
					{
						if (rightBound.OutIdx >= 0 && rightBound.PrevInAEL.OutIdx >= 0 && ClipperBase.SlopesEqual(rightBound.PrevInAEL, rightBound, this.m_UseFullRange) && rightBound.WindDelta != 0 && rightBound.PrevInAEL.WindDelta != 0)
						{
							OutPt outPt3 = this.AddOutPt(rightBound.PrevInAEL, rightBound.Bot);
							this.AddJoin(outPt, outPt3, rightBound.Top);
						}
						TEdge tedge = leftBound.NextInAEL;
						if (tedge != null)
						{
							while (tedge != rightBound)
							{
								this.IntersectEdges(rightBound, tedge, leftBound.Curr, false);
								tedge = tedge.NextInAEL;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000522C File Offset: 0x0000342C
		private void InsertEdgeIntoAEL(TEdge edge, TEdge startEdge)
		{
			if (this.m_ActiveEdges == null)
			{
				edge.PrevInAEL = null;
				edge.NextInAEL = null;
				this.m_ActiveEdges = edge;
			}
			else if (startEdge == null && this.E2InsertsBeforeE1(this.m_ActiveEdges, edge))
			{
				edge.PrevInAEL = null;
				edge.NextInAEL = this.m_ActiveEdges;
				this.m_ActiveEdges.PrevInAEL = edge;
				this.m_ActiveEdges = edge;
			}
			else
			{
				if (startEdge == null)
				{
					startEdge = this.m_ActiveEdges;
				}
				while (startEdge.NextInAEL != null && !this.E2InsertsBeforeE1(startEdge.NextInAEL, edge))
				{
					startEdge = startEdge.NextInAEL;
				}
				edge.NextInAEL = startEdge.NextInAEL;
				if (startEdge.NextInAEL != null)
				{
					startEdge.NextInAEL.PrevInAEL = edge;
				}
				edge.PrevInAEL = startEdge;
				startEdge.NextInAEL = edge;
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000530C File Offset: 0x0000350C
		private bool E2InsertsBeforeE1(TEdge e1, TEdge e2)
		{
			if (e2.Curr.X != e1.Curr.X)
			{
				return e2.Curr.X < e1.Curr.X;
			}
			if (e2.Top.Y > e1.Top.Y)
			{
				return e2.Top.X < Clipper.TopX(e1, e2.Top.Y);
			}
			return e1.Top.X > Clipper.TopX(e2, e1.Top.Y);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000053A8 File Offset: 0x000035A8
		private bool IsEvenOddFillType(TEdge edge)
		{
			if (edge.PolyTyp == PolyType.ptSubject)
			{
				return this.m_SubjFillType == PolyFillType.pftEvenOdd;
			}
			return this.m_ClipFillType == PolyFillType.pftEvenOdd;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000053C8 File Offset: 0x000035C8
		private bool IsEvenOddAltFillType(TEdge edge)
		{
			if (edge.PolyTyp == PolyType.ptSubject)
			{
				return this.m_ClipFillType == PolyFillType.pftEvenOdd;
			}
			return this.m_SubjFillType == PolyFillType.pftEvenOdd;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000053E8 File Offset: 0x000035E8
		private bool IsContributing(TEdge edge)
		{
			PolyFillType polyFillType;
			PolyFillType polyFillType2;
			if (edge.PolyTyp == PolyType.ptSubject)
			{
				polyFillType = this.m_SubjFillType;
				polyFillType2 = this.m_ClipFillType;
			}
			else
			{
				polyFillType = this.m_ClipFillType;
				polyFillType2 = this.m_SubjFillType;
			}
			switch (polyFillType)
			{
			case PolyFillType.pftEvenOdd:
				if (edge.WindDelta == 0 && edge.WindCnt != 1)
				{
					return false;
				}
				break;
			case PolyFillType.pftNonZero:
				if (Math.Abs(edge.WindCnt) != 1)
				{
					return false;
				}
				break;
			case PolyFillType.pftPositive:
				if (edge.WindCnt != 1)
				{
					return false;
				}
				break;
			default:
				if (edge.WindCnt != -1)
				{
					return false;
				}
				break;
			}
			switch (this.m_ClipType)
			{
			case ClipType.ctIntersection:
				switch (polyFillType2)
				{
				case PolyFillType.pftEvenOdd:
				case PolyFillType.pftNonZero:
					return edge.WindCnt2 != 0;
				case PolyFillType.pftPositive:
					return edge.WindCnt2 > 0;
				default:
					return edge.WindCnt2 < 0;
				}
				break;
			case ClipType.ctUnion:
				switch (polyFillType2)
				{
				case PolyFillType.pftEvenOdd:
				case PolyFillType.pftNonZero:
					return edge.WindCnt2 == 0;
				case PolyFillType.pftPositive:
					return edge.WindCnt2 <= 0;
				default:
					return edge.WindCnt2 >= 0;
				}
				break;
			case ClipType.ctDifference:
				if (edge.PolyTyp == PolyType.ptSubject)
				{
					switch (polyFillType2)
					{
					case PolyFillType.pftEvenOdd:
					case PolyFillType.pftNonZero:
						return edge.WindCnt2 == 0;
					case PolyFillType.pftPositive:
						return edge.WindCnt2 <= 0;
					default:
						return edge.WindCnt2 >= 0;
					}
				}
				else
				{
					switch (polyFillType2)
					{
					case PolyFillType.pftEvenOdd:
					case PolyFillType.pftNonZero:
						return edge.WindCnt2 != 0;
					case PolyFillType.pftPositive:
						return edge.WindCnt2 > 0;
					default:
						return edge.WindCnt2 < 0;
					}
				}
				break;
			case ClipType.ctXor:
				if (edge.WindDelta != 0)
				{
					return true;
				}
				switch (polyFillType2)
				{
				case PolyFillType.pftEvenOdd:
				case PolyFillType.pftNonZero:
					return edge.WindCnt2 == 0;
				case PolyFillType.pftPositive:
					return edge.WindCnt2 <= 0;
				default:
					return edge.WindCnt2 >= 0;
				}
				break;
			default:
				return true;
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000055FC File Offset: 0x000037FC
		private void SetWindingCount(TEdge edge)
		{
			TEdge tedge = edge.PrevInAEL;
			while (tedge != null && (tedge.PolyTyp != edge.PolyTyp || tedge.WindDelta == 0))
			{
				tedge = tedge.PrevInAEL;
			}
			if (tedge == null)
			{
				edge.WindCnt = ((edge.WindDelta != 0) ? edge.WindDelta : 1);
				edge.WindCnt2 = 0;
				tedge = this.m_ActiveEdges;
			}
			else if (edge.WindDelta == 0 && this.m_ClipType != ClipType.ctUnion)
			{
				edge.WindCnt = 1;
				edge.WindCnt2 = tedge.WindCnt2;
				tedge = tedge.NextInAEL;
			}
			else if (this.IsEvenOddFillType(edge))
			{
				if (edge.WindDelta == 0)
				{
					bool flag = true;
					for (TEdge tedge2 = tedge.PrevInAEL; tedge2 != null; tedge2 = tedge2.PrevInAEL)
					{
						if (tedge2.PolyTyp == tedge.PolyTyp && tedge2.WindDelta != 0)
						{
							flag = !flag;
						}
					}
					edge.WindCnt = ((!flag) ? 1 : 0);
				}
				else
				{
					edge.WindCnt = edge.WindDelta;
				}
				edge.WindCnt2 = tedge.WindCnt2;
				tedge = tedge.NextInAEL;
			}
			else
			{
				if (tedge.WindCnt * tedge.WindDelta < 0)
				{
					if (Math.Abs(tedge.WindCnt) > 1)
					{
						if (tedge.WindDelta * edge.WindDelta < 0)
						{
							edge.WindCnt = tedge.WindCnt;
						}
						else
						{
							edge.WindCnt = tedge.WindCnt + edge.WindDelta;
						}
					}
					else
					{
						edge.WindCnt = ((edge.WindDelta != 0) ? edge.WindDelta : 1);
					}
				}
				else if (edge.WindDelta == 0)
				{
					edge.WindCnt = ((tedge.WindCnt >= 0) ? (tedge.WindCnt + 1) : (tedge.WindCnt - 1));
				}
				else if (tedge.WindDelta * edge.WindDelta < 0)
				{
					edge.WindCnt = tedge.WindCnt;
				}
				else
				{
					edge.WindCnt = tedge.WindCnt + edge.WindDelta;
				}
				edge.WindCnt2 = tedge.WindCnt2;
				tedge = tedge.NextInAEL;
			}
			if (this.IsEvenOddAltFillType(edge))
			{
				while (tedge != edge)
				{
					if (tedge.WindDelta != 0)
					{
						edge.WindCnt2 = ((edge.WindCnt2 != 0) ? 0 : 1);
					}
					tedge = tedge.NextInAEL;
				}
			}
			else
			{
				while (tedge != edge)
				{
					edge.WindCnt2 += tedge.WindDelta;
					tedge = tedge.NextInAEL;
				}
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000058AC File Offset: 0x00003AAC
		private void AddEdgeToSEL(TEdge edge)
		{
			if (this.m_SortedEdges == null)
			{
				this.m_SortedEdges = edge;
				edge.PrevInSEL = null;
				edge.NextInSEL = null;
			}
			else
			{
				edge.NextInSEL = this.m_SortedEdges;
				edge.PrevInSEL = null;
				this.m_SortedEdges.PrevInSEL = edge;
				this.m_SortedEdges = edge;
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00005904 File Offset: 0x00003B04
		private void CopyAELToSEL()
		{
			TEdge tedge = this.m_ActiveEdges;
			this.m_SortedEdges = tedge;
			while (tedge != null)
			{
				tedge.PrevInSEL = tedge.PrevInAEL;
				tedge.NextInSEL = tedge.NextInAEL;
				tedge = tedge.NextInAEL;
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000594C File Offset: 0x00003B4C
		private void SwapPositionsInAEL(TEdge edge1, TEdge edge2)
		{
			if (edge1.NextInAEL == edge1.PrevInAEL || edge2.NextInAEL == edge2.PrevInAEL)
			{
				return;
			}
			if (edge1.NextInAEL == edge2)
			{
				TEdge nextInAEL = edge2.NextInAEL;
				if (nextInAEL != null)
				{
					nextInAEL.PrevInAEL = edge1;
				}
				TEdge prevInAEL = edge1.PrevInAEL;
				if (prevInAEL != null)
				{
					prevInAEL.NextInAEL = edge2;
				}
				edge2.PrevInAEL = prevInAEL;
				edge2.NextInAEL = edge1;
				edge1.PrevInAEL = edge2;
				edge1.NextInAEL = nextInAEL;
			}
			else if (edge2.NextInAEL == edge1)
			{
				TEdge nextInAEL2 = edge1.NextInAEL;
				if (nextInAEL2 != null)
				{
					nextInAEL2.PrevInAEL = edge2;
				}
				TEdge prevInAEL2 = edge2.PrevInAEL;
				if (prevInAEL2 != null)
				{
					prevInAEL2.NextInAEL = edge1;
				}
				edge1.PrevInAEL = prevInAEL2;
				edge1.NextInAEL = edge2;
				edge2.PrevInAEL = edge1;
				edge2.NextInAEL = nextInAEL2;
			}
			else
			{
				TEdge nextInAEL3 = edge1.NextInAEL;
				TEdge prevInAEL3 = edge1.PrevInAEL;
				edge1.NextInAEL = edge2.NextInAEL;
				if (edge1.NextInAEL != null)
				{
					edge1.NextInAEL.PrevInAEL = edge1;
				}
				edge1.PrevInAEL = edge2.PrevInAEL;
				if (edge1.PrevInAEL != null)
				{
					edge1.PrevInAEL.NextInAEL = edge1;
				}
				edge2.NextInAEL = nextInAEL3;
				if (edge2.NextInAEL != null)
				{
					edge2.NextInAEL.PrevInAEL = edge2;
				}
				edge2.PrevInAEL = prevInAEL3;
				if (edge2.PrevInAEL != null)
				{
					edge2.PrevInAEL.NextInAEL = edge2;
				}
			}
			if (edge1.PrevInAEL == null)
			{
				this.m_ActiveEdges = edge1;
			}
			else if (edge2.PrevInAEL == null)
			{
				this.m_ActiveEdges = edge2;
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00005AE4 File Offset: 0x00003CE4
		private void SwapPositionsInSEL(TEdge edge1, TEdge edge2)
		{
			if (edge1.NextInSEL == null && edge1.PrevInSEL == null)
			{
				return;
			}
			if (edge2.NextInSEL == null && edge2.PrevInSEL == null)
			{
				return;
			}
			if (edge1.NextInSEL == edge2)
			{
				TEdge nextInSEL = edge2.NextInSEL;
				if (nextInSEL != null)
				{
					nextInSEL.PrevInSEL = edge1;
				}
				TEdge prevInSEL = edge1.PrevInSEL;
				if (prevInSEL != null)
				{
					prevInSEL.NextInSEL = edge2;
				}
				edge2.PrevInSEL = prevInSEL;
				edge2.NextInSEL = edge1;
				edge1.PrevInSEL = edge2;
				edge1.NextInSEL = nextInSEL;
			}
			else if (edge2.NextInSEL == edge1)
			{
				TEdge nextInSEL2 = edge1.NextInSEL;
				if (nextInSEL2 != null)
				{
					nextInSEL2.PrevInSEL = edge2;
				}
				TEdge prevInSEL2 = edge2.PrevInSEL;
				if (prevInSEL2 != null)
				{
					prevInSEL2.NextInSEL = edge1;
				}
				edge1.PrevInSEL = prevInSEL2;
				edge1.NextInSEL = edge2;
				edge2.PrevInSEL = edge1;
				edge2.NextInSEL = nextInSEL2;
			}
			else
			{
				TEdge nextInSEL3 = edge1.NextInSEL;
				TEdge prevInSEL3 = edge1.PrevInSEL;
				edge1.NextInSEL = edge2.NextInSEL;
				if (edge1.NextInSEL != null)
				{
					edge1.NextInSEL.PrevInSEL = edge1;
				}
				edge1.PrevInSEL = edge2.PrevInSEL;
				if (edge1.PrevInSEL != null)
				{
					edge1.PrevInSEL.NextInSEL = edge1;
				}
				edge2.NextInSEL = nextInSEL3;
				if (edge2.NextInSEL != null)
				{
					edge2.NextInSEL.PrevInSEL = edge2;
				}
				edge2.PrevInSEL = prevInSEL3;
				if (edge2.PrevInSEL != null)
				{
					edge2.PrevInSEL.NextInSEL = edge2;
				}
			}
			if (edge1.PrevInSEL == null)
			{
				this.m_SortedEdges = edge1;
			}
			else if (edge2.PrevInSEL == null)
			{
				this.m_SortedEdges = edge2;
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00005C88 File Offset: 0x00003E88
		private void AddLocalMaxPoly(TEdge e1, TEdge e2, IntPoint pt)
		{
			this.AddOutPt(e1, pt);
			if (e1.OutIdx == e2.OutIdx)
			{
				e1.OutIdx = -1;
				e2.OutIdx = -1;
			}
			else if (e1.OutIdx < e2.OutIdx)
			{
				this.AppendPolygon(e1, e2);
			}
			else
			{
				this.AppendPolygon(e2, e1);
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00005CE8 File Offset: 0x00003EE8
		private OutPt AddLocalMinPoly(TEdge e1, TEdge e2, IntPoint pt)
		{
			OutPt outPt;
			TEdge tedge;
			TEdge tedge2;
			if (ClipperBase.IsHorizontal(e2) || e1.Dx > e2.Dx)
			{
				outPt = this.AddOutPt(e1, pt);
				e2.OutIdx = e1.OutIdx;
				e1.Side = EdgeSide.esLeft;
				e2.Side = EdgeSide.esRight;
				tedge = e1;
				if (tedge.PrevInAEL == e2)
				{
					tedge2 = e2.PrevInAEL;
				}
				else
				{
					tedge2 = tedge.PrevInAEL;
				}
			}
			else
			{
				outPt = this.AddOutPt(e2, pt);
				e1.OutIdx = e2.OutIdx;
				e1.Side = EdgeSide.esRight;
				e2.Side = EdgeSide.esLeft;
				tedge = e2;
				if (tedge.PrevInAEL == e1)
				{
					tedge2 = e1.PrevInAEL;
				}
				else
				{
					tedge2 = tedge.PrevInAEL;
				}
			}
			if (tedge2 != null && tedge2.OutIdx >= 0 && Clipper.TopX(tedge2, pt.Y) == Clipper.TopX(tedge, pt.Y) && ClipperBase.SlopesEqual(tedge, tedge2, this.m_UseFullRange) && tedge.WindDelta != 0 && tedge2.WindDelta != 0)
			{
				OutPt outPt2 = this.AddOutPt(tedge2, pt);
				this.AddJoin(outPt, outPt2, tedge.Top);
			}
			return outPt;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00005E10 File Offset: 0x00004010
		private OutRec CreateOutRec()
		{
			OutRec outRec = new OutRec();
			outRec.Idx = -1;
			outRec.IsHole = false;
			outRec.IsOpen = false;
			outRec.FirstLeft = null;
			outRec.Pts = null;
			outRec.BottomPt = null;
			outRec.PolyNode = null;
			this.m_PolyOuts.Add(outRec);
			outRec.Idx = this.m_PolyOuts.Count - 1;
			return outRec;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00005E74 File Offset: 0x00004074
		private OutPt AddOutPt(TEdge e, IntPoint pt)
		{
			bool flag = e.Side == EdgeSide.esLeft;
			if (e.OutIdx < 0)
			{
				OutRec outRec = this.CreateOutRec();
				outRec.IsOpen = e.WindDelta == 0;
				OutPt outPt = new OutPt();
				outRec.Pts = outPt;
				outPt.Idx = outRec.Idx;
				outPt.Pt = pt;
				outPt.Next = outPt;
				outPt.Prev = outPt;
				if (!outRec.IsOpen)
				{
					this.SetHoleState(e, outRec);
				}
				e.OutIdx = outRec.Idx;
				return outPt;
			}
			OutRec outRec2 = this.m_PolyOuts[e.OutIdx];
			OutPt pts = outRec2.Pts;
			if (flag && pt == pts.Pt)
			{
				return pts;
			}
			if (!flag && pt == pts.Prev.Pt)
			{
				return pts.Prev;
			}
			OutPt outPt2 = new OutPt();
			outPt2.Idx = outRec2.Idx;
			outPt2.Pt = pt;
			outPt2.Next = pts;
			outPt2.Prev = pts.Prev;
			outPt2.Prev.Next = outPt2;
			pts.Prev = outPt2;
			if (flag)
			{
				outRec2.Pts = outPt2;
			}
			return outPt2;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00005FB0 File Offset: 0x000041B0
		internal void SwapPoints(ref IntPoint pt1, ref IntPoint pt2)
		{
			IntPoint intPoint = new IntPoint(pt1);
			pt1 = pt2;
			pt2 = intPoint;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00005FE0 File Offset: 0x000041E0
		private bool HorzSegmentsOverlap(IntPoint Pt1a, IntPoint Pt1b, IntPoint Pt2a, IntPoint Pt2b)
		{
			return Pt1a.X > Pt2a.X == Pt1a.X < Pt2b.X || Pt1b.X > Pt2a.X == Pt1b.X < Pt2b.X || Pt2a.X > Pt1a.X == Pt2a.X < Pt1b.X || Pt2b.X > Pt1a.X == Pt2b.X < Pt1b.X || (Pt1a.X == Pt2a.X && Pt1b.X == Pt2b.X) || (Pt1a.X == Pt2b.X && Pt1b.X == Pt2a.X);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000060DC File Offset: 0x000042DC
		private OutPt InsertPolyPtBetween(OutPt p1, OutPt p2, IntPoint pt)
		{
			OutPt outPt = new OutPt();
			outPt.Pt = pt;
			if (p2 == p1.Next)
			{
				p1.Next = outPt;
				p2.Prev = outPt;
				outPt.Next = p2;
				outPt.Prev = p1;
			}
			else
			{
				p2.Next = outPt;
				p1.Prev = outPt;
				outPt.Next = p1;
				outPt.Prev = p2;
			}
			return outPt;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00006140 File Offset: 0x00004340
		private void SetHoleState(TEdge e, OutRec outRec)
		{
			bool flag = false;
			for (TEdge tedge = e.PrevInAEL; tedge != null; tedge = tedge.PrevInAEL)
			{
				if (tedge.OutIdx >= 0)
				{
					flag = !flag;
					if (outRec.FirstLeft == null)
					{
						outRec.FirstLeft = this.m_PolyOuts[tedge.OutIdx];
					}
				}
			}
			if (flag)
			{
				outRec.IsHole = true;
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000061A8 File Offset: 0x000043A8
		private double GetDx(IntPoint pt1, IntPoint pt2)
		{
			if (pt1.Y == pt2.Y)
			{
				return -3.4E+38;
			}
			return (double)(pt2.X - pt1.X) / (double)(pt2.Y - pt1.Y);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000061F4 File Offset: 0x000043F4
		private bool FirstIsBottomPt(OutPt btmPt1, OutPt btmPt2)
		{
			OutPt outPt = btmPt1.Prev;
			while (outPt.Pt == btmPt1.Pt && outPt != btmPt1)
			{
				outPt = outPt.Prev;
			}
			double num = Math.Abs(this.GetDx(btmPt1.Pt, outPt.Pt));
			outPt = btmPt1.Next;
			while (outPt.Pt == btmPt1.Pt && outPt != btmPt1)
			{
				outPt = outPt.Next;
			}
			double num2 = Math.Abs(this.GetDx(btmPt1.Pt, outPt.Pt));
			outPt = btmPt2.Prev;
			while (outPt.Pt == btmPt2.Pt && outPt != btmPt2)
			{
				outPt = outPt.Prev;
			}
			double num3 = Math.Abs(this.GetDx(btmPt2.Pt, outPt.Pt));
			outPt = btmPt2.Next;
			while (outPt.Pt == btmPt2.Pt && outPt != btmPt2)
			{
				outPt = outPt.Next;
			}
			double num4 = Math.Abs(this.GetDx(btmPt2.Pt, outPt.Pt));
			return (num >= num3 && num >= num4) || (num2 >= num3 && num2 >= num4);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00006348 File Offset: 0x00004548
		private OutPt GetBottomPt(OutPt pp)
		{
			OutPt outPt = null;
			OutPt outPt2;
			for (outPt2 = pp.Next; outPt2 != pp; outPt2 = outPt2.Next)
			{
				if (outPt2.Pt.Y > pp.Pt.Y)
				{
					pp = outPt2;
					outPt = null;
				}
				else if (outPt2.Pt.Y == pp.Pt.Y && outPt2.Pt.X <= pp.Pt.X)
				{
					if (outPt2.Pt.X < pp.Pt.X)
					{
						outPt = null;
						pp = outPt2;
					}
					else if (outPt2.Next != pp && outPt2.Prev != pp)
					{
						outPt = outPt2;
					}
				}
			}
			if (outPt != null)
			{
				while (outPt != outPt2)
				{
					if (!this.FirstIsBottomPt(outPt2, outPt))
					{
						pp = outPt;
					}
					outPt = outPt.Next;
					while (outPt.Pt != pp.Pt)
					{
						outPt = outPt.Next;
					}
				}
			}
			return pp;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00006458 File Offset: 0x00004658
		private OutRec GetLowermostRec(OutRec outRec1, OutRec outRec2)
		{
			if (outRec1.BottomPt == null)
			{
				outRec1.BottomPt = this.GetBottomPt(outRec1.Pts);
			}
			if (outRec2.BottomPt == null)
			{
				outRec2.BottomPt = this.GetBottomPt(outRec2.Pts);
			}
			OutPt bottomPt = outRec1.BottomPt;
			OutPt bottomPt2 = outRec2.BottomPt;
			if (bottomPt.Pt.Y > bottomPt2.Pt.Y)
			{
				return outRec1;
			}
			if (bottomPt.Pt.Y < bottomPt2.Pt.Y)
			{
				return outRec2;
			}
			if (bottomPt.Pt.X < bottomPt2.Pt.X)
			{
				return outRec1;
			}
			if (bottomPt.Pt.X > bottomPt2.Pt.X)
			{
				return outRec2;
			}
			if (bottomPt.Next == bottomPt)
			{
				return outRec2;
			}
			if (bottomPt2.Next == bottomPt2)
			{
				return outRec1;
			}
			if (this.FirstIsBottomPt(bottomPt, bottomPt2))
			{
				return outRec1;
			}
			return outRec2;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00006550 File Offset: 0x00004750
		private bool Param1RightOfParam2(OutRec outRec1, OutRec outRec2)
		{
			for (;;)
			{
				outRec1 = outRec1.FirstLeft;
				if (outRec1 == outRec2)
				{
					break;
				}
				if (outRec1 == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000656C File Offset: 0x0000476C
		private OutRec GetOutRec(int idx)
		{
			OutRec outRec;
			for (outRec = this.m_PolyOuts[idx]; outRec != this.m_PolyOuts[outRec.Idx]; outRec = this.m_PolyOuts[outRec.Idx])
			{
			}
			return outRec;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000065B8 File Offset: 0x000047B8
		private void AppendPolygon(TEdge e1, TEdge e2)
		{
			OutRec outRec = this.m_PolyOuts[e1.OutIdx];
			OutRec outRec2 = this.m_PolyOuts[e2.OutIdx];
			OutRec outRec3;
			if (this.Param1RightOfParam2(outRec, outRec2))
			{
				outRec3 = outRec2;
			}
			else if (this.Param1RightOfParam2(outRec2, outRec))
			{
				outRec3 = outRec;
			}
			else
			{
				outRec3 = this.GetLowermostRec(outRec, outRec2);
			}
			OutPt pts = outRec.Pts;
			OutPt prev = pts.Prev;
			OutPt pts2 = outRec2.Pts;
			OutPt prev2 = pts2.Prev;
			EdgeSide edgeSide;
			if (e1.Side == EdgeSide.esLeft)
			{
				if (e2.Side == EdgeSide.esLeft)
				{
					this.ReversePolyPtLinks(pts2);
					pts2.Next = pts;
					pts.Prev = pts2;
					prev.Next = prev2;
					prev2.Prev = prev;
					outRec.Pts = prev2;
				}
				else
				{
					prev2.Next = pts;
					pts.Prev = prev2;
					pts2.Prev = prev;
					prev.Next = pts2;
					outRec.Pts = pts2;
				}
				edgeSide = EdgeSide.esLeft;
			}
			else
			{
				if (e2.Side == EdgeSide.esRight)
				{
					this.ReversePolyPtLinks(pts2);
					prev.Next = prev2;
					prev2.Prev = prev;
					pts2.Next = pts;
					pts.Prev = pts2;
				}
				else
				{
					prev.Next = pts2;
					pts2.Prev = prev;
					pts.Prev = prev2;
					prev2.Next = pts;
				}
				edgeSide = EdgeSide.esRight;
			}
			outRec.BottomPt = null;
			if (outRec3 == outRec2)
			{
				if (outRec2.FirstLeft != outRec)
				{
					outRec.FirstLeft = outRec2.FirstLeft;
				}
				outRec.IsHole = outRec2.IsHole;
			}
			outRec2.Pts = null;
			outRec2.BottomPt = null;
			outRec2.FirstLeft = outRec;
			int outIdx = e1.OutIdx;
			int outIdx2 = e2.OutIdx;
			e1.OutIdx = -1;
			e2.OutIdx = -1;
			for (TEdge tedge = this.m_ActiveEdges; tedge != null; tedge = tedge.NextInAEL)
			{
				if (tedge.OutIdx == outIdx2)
				{
					tedge.OutIdx = outIdx;
					tedge.Side = edgeSide;
					break;
				}
			}
			outRec2.Idx = outRec.Idx;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000067CC File Offset: 0x000049CC
		private void ReversePolyPtLinks(OutPt pp)
		{
			if (pp == null)
			{
				return;
			}
			OutPt outPt = pp;
			do
			{
				OutPt next = outPt.Next;
				outPt.Next = outPt.Prev;
				outPt.Prev = next;
				outPt = next;
			}
			while (outPt != pp);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00006808 File Offset: 0x00004A08
		private static void SwapSides(TEdge edge1, TEdge edge2)
		{
			EdgeSide side = edge1.Side;
			edge1.Side = edge2.Side;
			edge2.Side = side;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00006830 File Offset: 0x00004A30
		private static void SwapPolyIndexes(TEdge edge1, TEdge edge2)
		{
			int outIdx = edge1.OutIdx;
			edge1.OutIdx = edge2.OutIdx;
			edge2.OutIdx = outIdx;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00006858 File Offset: 0x00004A58
		private void IntersectEdges(TEdge e1, TEdge e2, IntPoint pt, bool protect = false)
		{
			bool flag = !protect && e1.NextInLML == null && e1.Top.X == pt.X && e1.Top.Y == pt.Y;
			bool flag2 = !protect && e2.NextInLML == null && e2.Top.X == pt.X && e2.Top.Y == pt.Y;
			bool flag3 = e1.OutIdx >= 0;
			bool flag4 = e2.OutIdx >= 0;
			if (e1.PolyTyp == e2.PolyTyp)
			{
				if (this.IsEvenOddFillType(e1))
				{
					int windCnt = e1.WindCnt;
					e1.WindCnt = e2.WindCnt;
					e2.WindCnt = windCnt;
				}
				else
				{
					if (e1.WindCnt + e2.WindDelta == 0)
					{
						e1.WindCnt = -e1.WindCnt;
					}
					else
					{
						e1.WindCnt += e2.WindDelta;
					}
					if (e2.WindCnt - e1.WindDelta == 0)
					{
						e2.WindCnt = -e2.WindCnt;
					}
					else
					{
						e2.WindCnt -= e1.WindDelta;
					}
				}
			}
			else
			{
				if (!this.IsEvenOddFillType(e2))
				{
					e1.WindCnt2 += e2.WindDelta;
				}
				else
				{
					e1.WindCnt2 = ((e1.WindCnt2 != 0) ? 0 : 1);
				}
				if (!this.IsEvenOddFillType(e1))
				{
					e2.WindCnt2 -= e1.WindDelta;
				}
				else
				{
					e2.WindCnt2 = ((e2.WindCnt2 != 0) ? 0 : 1);
				}
			}
			PolyFillType polyFillType;
			PolyFillType polyFillType2;
			if (e1.PolyTyp == PolyType.ptSubject)
			{
				polyFillType = this.m_SubjFillType;
				polyFillType2 = this.m_ClipFillType;
			}
			else
			{
				polyFillType = this.m_ClipFillType;
				polyFillType2 = this.m_SubjFillType;
			}
			PolyFillType polyFillType3;
			PolyFillType polyFillType4;
			if (e2.PolyTyp == PolyType.ptSubject)
			{
				polyFillType3 = this.m_SubjFillType;
				polyFillType4 = this.m_ClipFillType;
			}
			else
			{
				polyFillType3 = this.m_ClipFillType;
				polyFillType4 = this.m_SubjFillType;
			}
			PolyFillType polyFillType5 = polyFillType;
			int num;
			if (polyFillType5 != PolyFillType.pftPositive)
			{
				if (polyFillType5 != PolyFillType.pftNegative)
				{
					num = Math.Abs(e1.WindCnt);
				}
				else
				{
					num = -e1.WindCnt;
				}
			}
			else
			{
				num = e1.WindCnt;
			}
			polyFillType5 = polyFillType3;
			int num2;
			if (polyFillType5 != PolyFillType.pftPositive)
			{
				if (polyFillType5 != PolyFillType.pftNegative)
				{
					num2 = Math.Abs(e2.WindCnt);
				}
				else
				{
					num2 = -e2.WindCnt;
				}
			}
			else
			{
				num2 = e2.WindCnt;
			}
			if (flag3 && flag4)
			{
				if (flag || flag2 || (num != 0 && num != 1) || (num2 != 0 && num2 != 1) || (e1.PolyTyp != e2.PolyTyp && this.m_ClipType != ClipType.ctXor))
				{
					this.AddLocalMaxPoly(e1, e2, pt);
				}
				else
				{
					this.AddOutPt(e1, pt);
					this.AddOutPt(e2, pt);
					Clipper.SwapSides(e1, e2);
					Clipper.SwapPolyIndexes(e1, e2);
				}
			}
			else if (flag3)
			{
				if (num2 == 0 || num2 == 1)
				{
					this.AddOutPt(e1, pt);
					Clipper.SwapSides(e1, e2);
					Clipper.SwapPolyIndexes(e1, e2);
				}
			}
			else if (flag4)
			{
				if (num == 0 || num == 1)
				{
					this.AddOutPt(e2, pt);
					Clipper.SwapSides(e1, e2);
					Clipper.SwapPolyIndexes(e1, e2);
				}
			}
			else if ((num == 0 || num == 1) && (num2 == 0 || num2 == 1) && !flag && !flag2)
			{
				polyFillType5 = polyFillType2;
				long num3;
				if (polyFillType5 != PolyFillType.pftPositive)
				{
					if (polyFillType5 != PolyFillType.pftNegative)
					{
						num3 = (long)Math.Abs(e1.WindCnt2);
					}
					else
					{
						num3 = (long)(-(long)e1.WindCnt2);
					}
				}
				else
				{
					num3 = (long)e1.WindCnt2;
				}
				polyFillType5 = polyFillType4;
				long num4;
				if (polyFillType5 != PolyFillType.pftPositive)
				{
					if (polyFillType5 != PolyFillType.pftNegative)
					{
						num4 = (long)Math.Abs(e2.WindCnt2);
					}
					else
					{
						num4 = (long)(-(long)e2.WindCnt2);
					}
				}
				else
				{
					num4 = (long)e2.WindCnt2;
				}
				if (e1.PolyTyp != e2.PolyTyp)
				{
					this.AddLocalMinPoly(e1, e2, pt);
				}
				else if (num == 1 && num2 == 1)
				{
					switch (this.m_ClipType)
					{
					case ClipType.ctIntersection:
						if (num3 > 0L && num4 > 0L)
						{
							this.AddLocalMinPoly(e1, e2, pt);
						}
						break;
					case ClipType.ctUnion:
						if (num3 <= 0L && num4 <= 0L)
						{
							this.AddLocalMinPoly(e1, e2, pt);
						}
						break;
					case ClipType.ctDifference:
						if ((e1.PolyTyp == PolyType.ptClip && num3 > 0L && num4 > 0L) || (e1.PolyTyp == PolyType.ptSubject && num3 <= 0L && num4 <= 0L))
						{
							this.AddLocalMinPoly(e1, e2, pt);
						}
						break;
					case ClipType.ctXor:
						this.AddLocalMinPoly(e1, e2, pt);
						break;
					}
				}
				else
				{
					Clipper.SwapSides(e1, e2);
				}
			}
			if (flag != flag2 && ((flag && e1.OutIdx >= 0) || (flag2 && e2.OutIdx >= 0)))
			{
				Clipper.SwapSides(e1, e2);
				Clipper.SwapPolyIndexes(e1, e2);
			}
			if (flag)
			{
				this.DeleteFromAEL(e1);
			}
			if (flag2)
			{
				this.DeleteFromAEL(e2);
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00006E08 File Offset: 0x00005008
		private void DeleteFromAEL(TEdge e)
		{
			TEdge prevInAEL = e.PrevInAEL;
			TEdge nextInAEL = e.NextInAEL;
			if (prevInAEL == null && nextInAEL == null && e != this.m_ActiveEdges)
			{
				return;
			}
			if (prevInAEL != null)
			{
				prevInAEL.NextInAEL = nextInAEL;
			}
			else
			{
				this.m_ActiveEdges = nextInAEL;
			}
			if (nextInAEL != null)
			{
				nextInAEL.PrevInAEL = prevInAEL;
			}
			e.NextInAEL = null;
			e.PrevInAEL = null;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00006E70 File Offset: 0x00005070
		private void DeleteFromSEL(TEdge e)
		{
			TEdge prevInSEL = e.PrevInSEL;
			TEdge nextInSEL = e.NextInSEL;
			if (prevInSEL == null && nextInSEL == null && e != this.m_SortedEdges)
			{
				return;
			}
			if (prevInSEL != null)
			{
				prevInSEL.NextInSEL = nextInSEL;
			}
			else
			{
				this.m_SortedEdges = nextInSEL;
			}
			if (nextInSEL != null)
			{
				nextInSEL.PrevInSEL = prevInSEL;
			}
			e.NextInSEL = null;
			e.PrevInSEL = null;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00006ED8 File Offset: 0x000050D8
		private void UpdateEdgeIntoAEL(ref TEdge e)
		{
			if (e.NextInLML == null)
			{
				throw new ClipperException("UpdateEdgeIntoAEL: invalid call");
			}
			TEdge prevInAEL = e.PrevInAEL;
			TEdge nextInAEL = e.NextInAEL;
			e.NextInLML.OutIdx = e.OutIdx;
			if (prevInAEL != null)
			{
				prevInAEL.NextInAEL = e.NextInLML;
			}
			else
			{
				this.m_ActiveEdges = e.NextInLML;
			}
			if (nextInAEL != null)
			{
				nextInAEL.PrevInAEL = e.NextInLML;
			}
			e.NextInLML.Side = e.Side;
			e.NextInLML.WindDelta = e.WindDelta;
			e.NextInLML.WindCnt = e.WindCnt;
			e.NextInLML.WindCnt2 = e.WindCnt2;
			e = e.NextInLML;
			e.Curr = e.Bot;
			e.PrevInAEL = prevInAEL;
			e.NextInAEL = nextInAEL;
			if (!ClipperBase.IsHorizontal(e))
			{
				this.InsertScanbeam(e.Top.Y);
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00006FE8 File Offset: 0x000051E8
		private void ProcessHorizontals(bool isTopOfScanbeam)
		{
			for (TEdge tedge = this.m_SortedEdges; tedge != null; tedge = this.m_SortedEdges)
			{
				this.DeleteFromSEL(tedge);
				this.ProcessHorizontal(tedge, isTopOfScanbeam);
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00007020 File Offset: 0x00005220
		private void GetHorzDirection(TEdge HorzEdge, out Direction Dir, out long Left, out long Right)
		{
			if (HorzEdge.Bot.X < HorzEdge.Top.X)
			{
				Left = HorzEdge.Bot.X;
				Right = HorzEdge.Top.X;
				Dir = Direction.dLeftToRight;
			}
			else
			{
				Left = HorzEdge.Top.X;
				Right = HorzEdge.Bot.X;
				Dir = Direction.dRightToLeft;
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000708C File Offset: 0x0000528C
		private void PrepareHorzJoins(TEdge horzEdge, bool isTopOfScanbeam)
		{
			OutPt outPt = this.m_PolyOuts[horzEdge.OutIdx].Pts;
			if (horzEdge.Side != EdgeSide.esLeft)
			{
				outPt = outPt.Prev;
			}
			for (int i = 0; i < this.m_GhostJoins.Count; i++)
			{
				Join join = this.m_GhostJoins[i];
				if (this.HorzSegmentsOverlap(join.OutPt1.Pt, join.OffPt, horzEdge.Bot, horzEdge.Top))
				{
					this.AddJoin(join.OutPt1, outPt, join.OffPt);
				}
			}
			if (isTopOfScanbeam)
			{
				if (outPt.Pt == horzEdge.Top)
				{
					this.AddGhostJoin(outPt, horzEdge.Bot);
				}
				else
				{
					this.AddGhostJoin(outPt, horzEdge.Top);
				}
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00007164 File Offset: 0x00005364
		private void ProcessHorizontal(TEdge horzEdge, bool isTopOfScanbeam)
		{
			Direction direction;
			long num;
			long num2;
			this.GetHorzDirection(horzEdge, out direction, out num, out num2);
			TEdge tedge = horzEdge;
			TEdge tedge2 = null;
			while (tedge.NextInLML != null && ClipperBase.IsHorizontal(tedge.NextInLML))
			{
				tedge = tedge.NextInLML;
			}
			if (tedge.NextInLML == null)
			{
				tedge2 = this.GetMaximaPair(tedge);
			}
			TEdge tedge3;
			for (;;)
			{
				bool flag = horzEdge == tedge;
				TEdge nextInAEL;
				for (tedge3 = this.GetNextInAEL(horzEdge, direction); tedge3 != null; tedge3 = nextInAEL)
				{
					if (tedge3.Curr.X == horzEdge.Top.X && horzEdge.NextInLML != null && tedge3.Dx < horzEdge.NextInLML.Dx)
					{
						break;
					}
					nextInAEL = this.GetNextInAEL(tedge3, direction);
					if ((direction == Direction.dLeftToRight && tedge3.Curr.X <= num2) || (direction == Direction.dRightToLeft && tedge3.Curr.X >= num))
					{
						if (tedge3 == tedge2 && flag)
						{
							goto Block_9;
						}
						if (direction == Direction.dLeftToRight)
						{
							IntPoint intPoint = new IntPoint(tedge3.Curr.X, horzEdge.Curr.Y);
							this.IntersectEdges(horzEdge, tedge3, intPoint, true);
						}
						else
						{
							IntPoint intPoint2 = new IntPoint(tedge3.Curr.X, horzEdge.Curr.Y);
							this.IntersectEdges(tedge3, horzEdge, intPoint2, true);
						}
						this.SwapPositionsInAEL(horzEdge, tedge3);
					}
					else if ((direction == Direction.dLeftToRight && tedge3.Curr.X >= num2) || (direction == Direction.dRightToLeft && tedge3.Curr.X <= num))
					{
						break;
					}
				}
				if (horzEdge.OutIdx >= 0 && horzEdge.WindDelta != 0)
				{
					this.PrepareHorzJoins(horzEdge, isTopOfScanbeam);
				}
				if (horzEdge.NextInLML == null || !ClipperBase.IsHorizontal(horzEdge.NextInLML))
				{
					goto IL_0279;
				}
				this.UpdateEdgeIntoAEL(ref horzEdge);
				if (horzEdge.OutIdx >= 0)
				{
					this.AddOutPt(horzEdge, horzEdge.Bot);
				}
				this.GetHorzDirection(horzEdge, out direction, out num, out num2);
			}
			Block_9:
			if (horzEdge.OutIdx >= 0 && horzEdge.WindDelta != 0)
			{
				this.PrepareHorzJoins(horzEdge, isTopOfScanbeam);
			}
			if (direction == Direction.dLeftToRight)
			{
				this.IntersectEdges(horzEdge, tedge3, tedge3.Top, false);
			}
			else
			{
				this.IntersectEdges(tedge3, horzEdge, tedge3.Top, false);
			}
			if (tedge2.OutIdx >= 0)
			{
				throw new ClipperException("ProcessHorizontal error");
			}
			return;
			IL_0279:
			if (horzEdge.NextInLML != null)
			{
				if (horzEdge.OutIdx >= 0)
				{
					OutPt outPt = this.AddOutPt(horzEdge, horzEdge.Top);
					this.UpdateEdgeIntoAEL(ref horzEdge);
					if (horzEdge.WindDelta == 0)
					{
						return;
					}
					TEdge prevInAEL = horzEdge.PrevInAEL;
					TEdge nextInAEL2 = horzEdge.NextInAEL;
					if (prevInAEL != null && prevInAEL.Curr.X == horzEdge.Bot.X && prevInAEL.Curr.Y == horzEdge.Bot.Y && prevInAEL.WindDelta != 0 && prevInAEL.OutIdx >= 0 && prevInAEL.Curr.Y > prevInAEL.Top.Y && ClipperBase.SlopesEqual(horzEdge, prevInAEL, this.m_UseFullRange))
					{
						OutPt outPt2 = this.AddOutPt(prevInAEL, horzEdge.Bot);
						this.AddJoin(outPt, outPt2, horzEdge.Top);
					}
					else if (nextInAEL2 != null && nextInAEL2.Curr.X == horzEdge.Bot.X && nextInAEL2.Curr.Y == horzEdge.Bot.Y && nextInAEL2.WindDelta != 0 && nextInAEL2.OutIdx >= 0 && nextInAEL2.Curr.Y > nextInAEL2.Top.Y && ClipperBase.SlopesEqual(horzEdge, nextInAEL2, this.m_UseFullRange))
					{
						OutPt outPt3 = this.AddOutPt(nextInAEL2, horzEdge.Bot);
						this.AddJoin(outPt, outPt3, horzEdge.Top);
					}
				}
				else
				{
					this.UpdateEdgeIntoAEL(ref horzEdge);
				}
			}
			else if (tedge2 != null)
			{
				if (tedge2.OutIdx >= 0)
				{
					if (direction == Direction.dLeftToRight)
					{
						this.IntersectEdges(horzEdge, tedge2, horzEdge.Top, false);
					}
					else
					{
						this.IntersectEdges(tedge2, horzEdge, horzEdge.Top, false);
					}
					if (tedge2.OutIdx >= 0)
					{
						throw new ClipperException("ProcessHorizontal error");
					}
				}
				else
				{
					this.DeleteFromAEL(horzEdge);
					this.DeleteFromAEL(tedge2);
				}
			}
			else
			{
				if (horzEdge.OutIdx >= 0)
				{
					this.AddOutPt(horzEdge, horzEdge.Top);
				}
				this.DeleteFromAEL(horzEdge);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00007638 File Offset: 0x00005838
		private TEdge GetNextInAEL(TEdge e, Direction Direction)
		{
			return (Direction != Direction.dLeftToRight) ? e.PrevInAEL : e.NextInAEL;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00007654 File Offset: 0x00005854
		private bool IsMinima(TEdge e)
		{
			return e != null && e.Prev.NextInLML != e && e.Next.NextInLML != e;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00007684 File Offset: 0x00005884
		private bool IsMaxima(TEdge e, double Y)
		{
			return e != null && (double)e.Top.Y == Y && e.NextInLML == null;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000076B8 File Offset: 0x000058B8
		private bool IsIntermediate(TEdge e, double Y)
		{
			return (double)e.Top.Y == Y && e.NextInLML != null;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000076DC File Offset: 0x000058DC
		private TEdge GetMaximaPair(TEdge e)
		{
			TEdge tedge = null;
			if (e.Next.Top == e.Top && e.Next.NextInLML == null)
			{
				tedge = e.Next;
			}
			else if (e.Prev.Top == e.Top && e.Prev.NextInLML == null)
			{
				tedge = e.Prev;
			}
			if (tedge != null && (tedge.OutIdx == -2 || (tedge.NextInAEL == tedge.PrevInAEL && !ClipperBase.IsHorizontal(tedge))))
			{
				return null;
			}
			return tedge;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00007788 File Offset: 0x00005988
		private bool ProcessIntersections(long botY, long topY)
		{
			if (this.m_ActiveEdges == null)
			{
				return true;
			}
			try
			{
				this.BuildIntersectList(botY, topY);
				if (this.m_IntersectNodes == null)
				{
					return true;
				}
				if (this.m_IntersectNodes.Next != null && !this.FixupIntersectionOrder())
				{
					return false;
				}
				this.ProcessIntersectList();
			}
			catch
			{
				this.m_SortedEdges = null;
				this.DisposeIntersectNodes();
				throw new ClipperException("ProcessIntersections error");
			}
			this.m_SortedEdges = null;
			return true;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00007834 File Offset: 0x00005A34
		private void BuildIntersectList(long botY, long topY)
		{
			if (this.m_ActiveEdges == null)
			{
				return;
			}
			TEdge tedge = this.m_ActiveEdges;
			this.m_SortedEdges = tedge;
			while (tedge != null)
			{
				tedge.PrevInSEL = tedge.PrevInAEL;
				tedge.NextInSEL = tedge.NextInAEL;
				tedge.Curr.X = Clipper.TopX(tedge, topY);
				tedge = tedge.NextInAEL;
			}
			bool flag = true;
			while (flag && this.m_SortedEdges != null)
			{
				flag = false;
				tedge = this.m_SortedEdges;
				while (tedge.NextInSEL != null)
				{
					TEdge nextInSEL = tedge.NextInSEL;
					if (tedge.Curr.X > nextInSEL.Curr.X)
					{
						IntPoint intPoint;
						if (!this.IntersectPoint(tedge, nextInSEL, out intPoint) && tedge.Curr.X > nextInSEL.Curr.X + 1L)
						{
							throw new ClipperException("Intersection error");
						}
						if (intPoint.Y > botY)
						{
							intPoint.Y = botY;
							if (Math.Abs(tedge.Dx) > Math.Abs(nextInSEL.Dx))
							{
								intPoint.X = Clipper.TopX(nextInSEL, botY);
							}
							else
							{
								intPoint.X = Clipper.TopX(tedge, botY);
							}
						}
						this.InsertIntersectNode(tedge, nextInSEL, intPoint);
						this.SwapPositionsInSEL(tedge, nextInSEL);
						flag = true;
					}
					else
					{
						tedge = nextInSEL;
					}
				}
				if (tedge.PrevInSEL == null)
				{
					break;
				}
				tedge.PrevInSEL.NextInSEL = null;
			}
			this.m_SortedEdges = null;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000079B8 File Offset: 0x00005BB8
		private bool EdgesAdjacent(IntersectNode inode)
		{
			return inode.Edge1.NextInSEL == inode.Edge2 || inode.Edge1.PrevInSEL == inode.Edge2;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000079F4 File Offset: 0x00005BF4
		private bool FixupIntersectionOrder()
		{
			IntersectNode intersectNode = this.m_IntersectNodes;
			this.CopyAELToSEL();
			while (intersectNode != null)
			{
				if (!this.EdgesAdjacent(intersectNode))
				{
					IntersectNode intersectNode2 = intersectNode.Next;
					while (intersectNode2 != null && !this.EdgesAdjacent(intersectNode2))
					{
						intersectNode2 = intersectNode2.Next;
					}
					if (intersectNode2 == null)
					{
						return false;
					}
					this.SwapIntersectNodes(intersectNode, intersectNode2);
				}
				this.SwapPositionsInSEL(intersectNode.Edge1, intersectNode.Edge2);
				intersectNode = intersectNode.Next;
			}
			return true;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00007A74 File Offset: 0x00005C74
		private void ProcessIntersectList()
		{
			while (this.m_IntersectNodes != null)
			{
				IntersectNode next = this.m_IntersectNodes.Next;
				this.IntersectEdges(this.m_IntersectNodes.Edge1, this.m_IntersectNodes.Edge2, this.m_IntersectNodes.Pt, true);
				this.SwapPositionsInAEL(this.m_IntersectNodes.Edge1, this.m_IntersectNodes.Edge2);
				this.m_IntersectNodes = null;
				this.m_IntersectNodes = next;
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00007AF0 File Offset: 0x00005CF0
		internal static long Round(double value)
		{
			return (value >= 0.0) ? ((long)(value + 0.5)) : ((long)(value - 0.5));
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00007B2C File Offset: 0x00005D2C
		private static long TopX(TEdge edge, long currentY)
		{
			if (currentY == edge.Top.Y)
			{
				return edge.Top.X;
			}
			return edge.Bot.X + Clipper.Round(edge.Dx * (double)(currentY - edge.Bot.Y));
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00007B7C File Offset: 0x00005D7C
		private void InsertIntersectNode(TEdge e1, TEdge e2, IntPoint pt)
		{
			IntersectNode intersectNode = new IntersectNode();
			intersectNode.Edge1 = e1;
			intersectNode.Edge2 = e2;
			intersectNode.Pt = pt;
			intersectNode.Next = null;
			if (this.m_IntersectNodes == null)
			{
				this.m_IntersectNodes = intersectNode;
			}
			else if (intersectNode.Pt.Y > this.m_IntersectNodes.Pt.Y)
			{
				intersectNode.Next = this.m_IntersectNodes;
				this.m_IntersectNodes = intersectNode;
			}
			else
			{
				IntersectNode intersectNode2 = this.m_IntersectNodes;
				while (intersectNode2.Next != null && intersectNode.Pt.Y < intersectNode2.Next.Pt.Y)
				{
					intersectNode2 = intersectNode2.Next;
				}
				intersectNode.Next = intersectNode2.Next;
				intersectNode2.Next = intersectNode;
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00007C4C File Offset: 0x00005E4C
		private void SwapIntersectNodes(IntersectNode int1, IntersectNode int2)
		{
			TEdge edge = int1.Edge1;
			TEdge edge2 = int1.Edge2;
			IntPoint intPoint = new IntPoint(int1.Pt);
			int1.Edge1 = int2.Edge1;
			int1.Edge2 = int2.Edge2;
			int1.Pt = int2.Pt;
			int2.Edge1 = edge;
			int2.Edge2 = edge2;
			int2.Pt = intPoint;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00007CB0 File Offset: 0x00005EB0
		private bool IntersectPoint(TEdge edge1, TEdge edge2, out IntPoint ip)
		{
			ip = default(IntPoint);
			if (ClipperBase.SlopesEqual(edge1, edge2, this.m_UseFullRange) || edge1.Dx == edge2.Dx)
			{
				if (edge2.Bot.Y > edge1.Bot.Y)
				{
					ip.Y = edge2.Bot.Y;
				}
				else
				{
					ip.Y = edge1.Bot.Y;
				}
				return false;
			}
			if (edge1.Delta.X == 0L)
			{
				ip.X = edge1.Bot.X;
				if (ClipperBase.IsHorizontal(edge2))
				{
					ip.Y = edge2.Bot.Y;
				}
				else
				{
					double num = (double)edge2.Bot.Y - (double)edge2.Bot.X / edge2.Dx;
					ip.Y = Clipper.Round((double)ip.X / edge2.Dx + num);
				}
			}
			else if (edge2.Delta.X == 0L)
			{
				ip.X = edge2.Bot.X;
				if (ClipperBase.IsHorizontal(edge1))
				{
					ip.Y = edge1.Bot.Y;
				}
				else
				{
					double num2 = (double)edge1.Bot.Y - (double)edge1.Bot.X / edge1.Dx;
					ip.Y = Clipper.Round((double)ip.X / edge1.Dx + num2);
				}
			}
			else
			{
				double num2 = (double)edge1.Bot.X - (double)edge1.Bot.Y * edge1.Dx;
				double num = (double)edge2.Bot.X - (double)edge2.Bot.Y * edge2.Dx;
				double num3 = (num - num2) / (edge1.Dx - edge2.Dx);
				ip.Y = Clipper.Round(num3);
				if (Math.Abs(edge1.Dx) < Math.Abs(edge2.Dx))
				{
					ip.X = Clipper.Round(edge1.Dx * num3 + num2);
				}
				else
				{
					ip.X = Clipper.Round(edge2.Dx * num3 + num);
				}
			}
			if (ip.Y >= edge1.Top.Y && ip.Y >= edge2.Top.Y)
			{
				return true;
			}
			if (edge1.Top.Y > edge2.Top.Y)
			{
				ip.Y = edge1.Top.Y;
				ip.X = Clipper.TopX(edge2, edge1.Top.Y);
				return ip.X < edge1.Top.X;
			}
			ip.Y = edge2.Top.Y;
			ip.X = Clipper.TopX(edge1, edge2.Top.Y);
			return ip.X > edge2.Top.X;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00007FA0 File Offset: 0x000061A0
		private void DisposeIntersectNodes()
		{
			while (this.m_IntersectNodes != null)
			{
				IntersectNode next = this.m_IntersectNodes.Next;
				this.m_IntersectNodes = null;
				this.m_IntersectNodes = next;
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00007FD8 File Offset: 0x000061D8
		private void ProcessEdgesAtTopOfScanbeam(long topY)
		{
			TEdge tedge = this.m_ActiveEdges;
			while (tedge != null)
			{
				bool flag = this.IsMaxima(tedge, (double)topY);
				if (flag)
				{
					TEdge maximaPair = this.GetMaximaPair(tedge);
					flag = maximaPair == null || !ClipperBase.IsHorizontal(maximaPair);
				}
				if (flag)
				{
					TEdge prevInAEL = tedge.PrevInAEL;
					this.DoMaxima(tedge);
					if (prevInAEL == null)
					{
						tedge = this.m_ActiveEdges;
					}
					else
					{
						tedge = prevInAEL.NextInAEL;
					}
				}
				else
				{
					if (this.IsIntermediate(tedge, (double)topY) && ClipperBase.IsHorizontal(tedge.NextInLML))
					{
						this.UpdateEdgeIntoAEL(ref tedge);
						if (tedge.OutIdx >= 0)
						{
							this.AddOutPt(tedge, tedge.Bot);
						}
						this.AddEdgeToSEL(tedge);
					}
					else
					{
						tedge.Curr.X = Clipper.TopX(tedge, topY);
						tedge.Curr.Y = topY;
					}
					if (this.StrictlySimple)
					{
						TEdge prevInAEL2 = tedge.PrevInAEL;
						if (tedge.OutIdx >= 0 && tedge.WindDelta != 0 && prevInAEL2 != null && prevInAEL2.OutIdx >= 0 && prevInAEL2.Curr.X == tedge.Curr.X && prevInAEL2.WindDelta != 0)
						{
							OutPt outPt = this.AddOutPt(prevInAEL2, tedge.Curr);
							OutPt outPt2 = this.AddOutPt(tedge, tedge.Curr);
							this.AddJoin(outPt, outPt2, tedge.Curr);
						}
					}
					tedge = tedge.NextInAEL;
				}
			}
			this.ProcessHorizontals(true);
			for (tedge = this.m_ActiveEdges; tedge != null; tedge = tedge.NextInAEL)
			{
				if (this.IsIntermediate(tedge, (double)topY))
				{
					OutPt outPt3 = null;
					if (tedge.OutIdx >= 0)
					{
						outPt3 = this.AddOutPt(tedge, tedge.Top);
					}
					this.UpdateEdgeIntoAEL(ref tedge);
					TEdge prevInAEL3 = tedge.PrevInAEL;
					TEdge nextInAEL = tedge.NextInAEL;
					if (prevInAEL3 != null && prevInAEL3.Curr.X == tedge.Bot.X && prevInAEL3.Curr.Y == tedge.Bot.Y && outPt3 != null && prevInAEL3.OutIdx >= 0 && prevInAEL3.Curr.Y > prevInAEL3.Top.Y && ClipperBase.SlopesEqual(tedge, prevInAEL3, this.m_UseFullRange) && tedge.WindDelta != 0 && prevInAEL3.WindDelta != 0)
					{
						OutPt outPt4 = this.AddOutPt(prevInAEL3, tedge.Bot);
						this.AddJoin(outPt3, outPt4, tedge.Top);
					}
					else if (nextInAEL != null && nextInAEL.Curr.X == tedge.Bot.X && nextInAEL.Curr.Y == tedge.Bot.Y && outPt3 != null && nextInAEL.OutIdx >= 0 && nextInAEL.Curr.Y > nextInAEL.Top.Y && ClipperBase.SlopesEqual(tedge, nextInAEL, this.m_UseFullRange) && tedge.WindDelta != 0 && nextInAEL.WindDelta != 0)
					{
						OutPt outPt5 = this.AddOutPt(nextInAEL, tedge.Bot);
						this.AddJoin(outPt3, outPt5, tedge.Top);
					}
				}
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00008338 File Offset: 0x00006538
		private void DoMaxima(TEdge e)
		{
			TEdge maximaPair = this.GetMaximaPair(e);
			if (maximaPair == null)
			{
				if (e.OutIdx >= 0)
				{
					this.AddOutPt(e, e.Top);
				}
				this.DeleteFromAEL(e);
				return;
			}
			TEdge tedge = e.NextInAEL;
			while (tedge != null && tedge != maximaPair)
			{
				this.IntersectEdges(e, tedge, e.Top, true);
				this.SwapPositionsInAEL(e, tedge);
				tedge = e.NextInAEL;
			}
			if (e.OutIdx == -1 && maximaPair.OutIdx == -1)
			{
				this.DeleteFromAEL(e);
				this.DeleteFromAEL(maximaPair);
			}
			else
			{
				if (e.OutIdx < 0 || maximaPair.OutIdx < 0)
				{
					throw new ClipperException("DoMaxima error");
				}
				this.IntersectEdges(e, maximaPair, e.Top, false);
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00008410 File Offset: 0x00006610
		public static void ReversePaths(List<List<IntPoint>> polys)
		{
			polys.ForEach(delegate(List<IntPoint> poly)
			{
				poly.Reverse();
			});
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00008438 File Offset: 0x00006638
		public static bool Orientation(List<IntPoint> poly)
		{
			return Clipper.Area(poly) >= 0.0;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00008450 File Offset: 0x00006650
		private int PointCount(OutPt pts)
		{
			if (pts == null)
			{
				return 0;
			}
			int num = 0;
			OutPt outPt = pts;
			do
			{
				num++;
				outPt = outPt.Next;
			}
			while (outPt != pts);
			return num;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000847C File Offset: 0x0000667C
		private void BuildResult(List<List<IntPoint>> polyg)
		{
			polyg.Clear();
			polyg.Capacity = this.m_PolyOuts.Count;
			for (int i = 0; i < this.m_PolyOuts.Count; i++)
			{
				OutRec outRec = this.m_PolyOuts[i];
				if (outRec.Pts != null)
				{
					OutPt outPt = outRec.Pts;
					int num = this.PointCount(outPt);
					if (num >= 2)
					{
						List<IntPoint> list = new List<IntPoint>(num);
						for (int j = 0; j < num; j++)
						{
							list.Add(outPt.Pt);
							outPt = outPt.Prev;
						}
						polyg.Add(list);
					}
				}
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00008530 File Offset: 0x00006730
		private void BuildResult2(PolyTree polytree)
		{
			polytree.Clear();
			polytree.m_AllPolys.Capacity = this.m_PolyOuts.Count;
			for (int i = 0; i < this.m_PolyOuts.Count; i++)
			{
				OutRec outRec = this.m_PolyOuts[i];
				int num = this.PointCount(outRec.Pts);
				if ((!outRec.IsOpen || num >= 2) && (outRec.IsOpen || num >= 3))
				{
					this.FixHoleLinkage(outRec);
					PolyNode polyNode = new PolyNode();
					polytree.m_AllPolys.Add(polyNode);
					outRec.PolyNode = polyNode;
					polyNode.m_polygon.Capacity = num;
					OutPt outPt = outRec.Pts.Prev;
					for (int j = 0; j < num; j++)
					{
						polyNode.m_polygon.Add(outPt.Pt);
						outPt = outPt.Prev;
					}
				}
			}
			polytree.m_Childs.Capacity = this.m_PolyOuts.Count;
			for (int k = 0; k < this.m_PolyOuts.Count; k++)
			{
				OutRec outRec2 = this.m_PolyOuts[k];
				if (outRec2.PolyNode != null)
				{
					if (outRec2.IsOpen)
					{
						outRec2.PolyNode.IsOpen = true;
						polytree.AddChild(outRec2.PolyNode);
					}
					else if (outRec2.FirstLeft != null)
					{
						outRec2.FirstLeft.PolyNode.AddChild(outRec2.PolyNode);
					}
					else
					{
						polytree.AddChild(outRec2.PolyNode);
					}
				}
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000086DC File Offset: 0x000068DC
		private void FixupOutPolygon(OutRec outRec)
		{
			OutPt outPt = null;
			outRec.BottomPt = null;
			OutPt outPt2 = outRec.Pts;
			while (outPt2.Prev != outPt2 && outPt2.Prev != outPt2.Next)
			{
				if (outPt2.Pt == outPt2.Next.Pt || outPt2.Pt == outPt2.Prev.Pt || (ClipperBase.SlopesEqual(outPt2.Prev.Pt, outPt2.Pt, outPt2.Next.Pt, this.m_UseFullRange) && (!base.PreserveCollinear || !base.Pt2IsBetweenPt1AndPt3(outPt2.Prev.Pt, outPt2.Pt, outPt2.Next.Pt))))
				{
					outPt = null;
					outPt2.Prev.Next = outPt2.Next;
					outPt2.Next.Prev = outPt2.Prev;
					outPt2 = outPt2.Prev;
				}
				else
				{
					if (outPt2 == outPt)
					{
						outRec.Pts = outPt2;
						return;
					}
					if (outPt == null)
					{
						outPt = outPt2;
					}
					outPt2 = outPt2.Next;
				}
			}
			this.DisposeOutPts(outPt2);
			outRec.Pts = null;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000881C File Offset: 0x00006A1C
		private OutPt DupOutPt(OutPt outPt, bool InsertAfter)
		{
			OutPt outPt2 = new OutPt();
			outPt2.Pt = outPt.Pt;
			outPt2.Idx = outPt.Idx;
			if (InsertAfter)
			{
				outPt2.Next = outPt.Next;
				outPt2.Prev = outPt;
				outPt.Next.Prev = outPt2;
				outPt.Next = outPt2;
			}
			else
			{
				outPt2.Prev = outPt.Prev;
				outPt2.Next = outPt;
				outPt.Prev.Next = outPt2;
				outPt.Prev = outPt2;
			}
			return outPt2;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000088A0 File Offset: 0x00006AA0
		private bool GetOverlap(long a1, long a2, long b1, long b2, out long Left, out long Right)
		{
			if (a1 < a2)
			{
				if (b1 < b2)
				{
					Left = Math.Max(a1, b1);
					Right = Math.Min(a2, b2);
				}
				else
				{
					Left = Math.Max(a1, b2);
					Right = Math.Min(a2, b1);
				}
			}
			else if (b1 < b2)
			{
				Left = Math.Max(a2, b1);
				Right = Math.Min(a1, b2);
			}
			else
			{
				Left = Math.Max(a2, b2);
				Right = Math.Min(a1, b1);
			}
			return Left < Right;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00008930 File Offset: 0x00006B30
		private bool JoinHorz(OutPt op1, OutPt op1b, OutPt op2, OutPt op2b, IntPoint Pt, bool DiscardLeft)
		{
			Direction direction = ((op1.Pt.X <= op1b.Pt.X) ? Direction.dLeftToRight : Direction.dRightToLeft);
			Direction direction2 = ((op2.Pt.X <= op2b.Pt.X) ? Direction.dLeftToRight : Direction.dRightToLeft);
			if (direction == direction2)
			{
				return false;
			}
			if (direction == Direction.dLeftToRight)
			{
				while (op1.Next.Pt.X <= Pt.X && op1.Next.Pt.X >= op1.Pt.X && op1.Next.Pt.Y == Pt.Y)
				{
					op1 = op1.Next;
				}
				if (DiscardLeft && op1.Pt.X != Pt.X)
				{
					op1 = op1.Next;
				}
				op1b = this.DupOutPt(op1, !DiscardLeft);
				if (op1b.Pt != Pt)
				{
					op1 = op1b;
					op1.Pt = Pt;
					op1b = this.DupOutPt(op1, !DiscardLeft);
				}
			}
			else
			{
				while (op1.Next.Pt.X >= Pt.X && op1.Next.Pt.X <= op1.Pt.X && op1.Next.Pt.Y == Pt.Y)
				{
					op1 = op1.Next;
				}
				if (!DiscardLeft && op1.Pt.X != Pt.X)
				{
					op1 = op1.Next;
				}
				op1b = this.DupOutPt(op1, DiscardLeft);
				if (op1b.Pt != Pt)
				{
					op1 = op1b;
					op1.Pt = Pt;
					op1b = this.DupOutPt(op1, DiscardLeft);
				}
			}
			if (direction2 == Direction.dLeftToRight)
			{
				while (op2.Next.Pt.X <= Pt.X && op2.Next.Pt.X >= op2.Pt.X && op2.Next.Pt.Y == Pt.Y)
				{
					op2 = op2.Next;
				}
				if (DiscardLeft && op2.Pt.X != Pt.X)
				{
					op2 = op2.Next;
				}
				op2b = this.DupOutPt(op2, !DiscardLeft);
				if (op2b.Pt != Pt)
				{
					op2 = op2b;
					op2.Pt = Pt;
					op2b = this.DupOutPt(op2, !DiscardLeft);
				}
			}
			else
			{
				while (op2.Next.Pt.X >= Pt.X && op2.Next.Pt.X <= op2.Pt.X && op2.Next.Pt.Y == Pt.Y)
				{
					op2 = op2.Next;
				}
				if (!DiscardLeft && op2.Pt.X != Pt.X)
				{
					op2 = op2.Next;
				}
				op2b = this.DupOutPt(op2, DiscardLeft);
				if (op2b.Pt != Pt)
				{
					op2 = op2b;
					op2.Pt = Pt;
					op2b = this.DupOutPt(op2, DiscardLeft);
				}
			}
			if (direction == Direction.dLeftToRight == DiscardLeft)
			{
				op1.Prev = op2;
				op2.Next = op1;
				op1b.Next = op2b;
				op2b.Prev = op1b;
			}
			else
			{
				op1.Next = op2;
				op2.Prev = op1;
				op1b.Prev = op2b;
				op2b.Next = op1b;
			}
			return true;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00008CFC File Offset: 0x00006EFC
		private bool JoinPoints(Join j, out OutPt p1, out OutPt p2)
		{
			OutRec outRec = this.GetOutRec(j.OutPt1.Idx);
			OutRec outRec2 = this.GetOutRec(j.OutPt2.Idx);
			OutPt outPt = j.OutPt1;
			OutPt outPt2 = j.OutPt2;
			p1 = null;
			p2 = null;
			bool flag = j.OutPt1.Pt.Y == j.OffPt.Y;
			if (flag && j.OffPt == j.OutPt1.Pt && j.OffPt == j.OutPt2.Pt)
			{
				OutPt outPt3 = j.OutPt1.Next;
				while (outPt3 != outPt && outPt3.Pt == j.OffPt)
				{
					outPt3 = outPt3.Next;
				}
				bool flag2 = outPt3.Pt.Y > j.OffPt.Y;
				OutPt outPt4 = j.OutPt2.Next;
				while (outPt4 != outPt2 && outPt4.Pt == j.OffPt)
				{
					outPt4 = outPt4.Next;
				}
				bool flag3 = outPt4.Pt.Y > j.OffPt.Y;
				if (flag2 == flag3)
				{
					return false;
				}
				if (flag2)
				{
					outPt3 = this.DupOutPt(outPt, false);
					outPt4 = this.DupOutPt(outPt2, true);
					outPt.Prev = outPt2;
					outPt2.Next = outPt;
					outPt3.Next = outPt4;
					outPt4.Prev = outPt3;
					p1 = outPt;
					p2 = outPt3;
					return true;
				}
				outPt3 = this.DupOutPt(outPt, true);
				outPt4 = this.DupOutPt(outPt2, false);
				outPt.Next = outPt2;
				outPt2.Prev = outPt;
				outPt3.Prev = outPt4;
				outPt4.Next = outPt3;
				p1 = outPt;
				p2 = outPt3;
				return true;
			}
			else if (flag)
			{
				OutPt outPt3 = outPt;
				while (outPt.Prev.Pt.Y == outPt.Pt.Y && outPt.Prev != outPt3 && outPt.Prev != outPt2)
				{
					outPt = outPt.Prev;
				}
				while (outPt3.Next.Pt.Y == outPt3.Pt.Y && outPt3.Next != outPt && outPt3.Next != outPt2)
				{
					outPt3 = outPt3.Next;
				}
				if (outPt3.Next == outPt || outPt3.Next == outPt2)
				{
					return false;
				}
				OutPt outPt4 = outPt2;
				while (outPt2.Prev.Pt.Y == outPt2.Pt.Y && outPt2.Prev != outPt4 && outPt2.Prev != outPt3)
				{
					outPt2 = outPt2.Prev;
				}
				while (outPt4.Next.Pt.Y == outPt4.Pt.Y && outPt4.Next != outPt2 && outPt4.Next != outPt)
				{
					outPt4 = outPt4.Next;
				}
				if (outPt4.Next == outPt2 || outPt4.Next == outPt)
				{
					return false;
				}
				long num;
				long num2;
				if (!this.GetOverlap(outPt.Pt.X, outPt3.Pt.X, outPt2.Pt.X, outPt4.Pt.X, out num, out num2))
				{
					return false;
				}
				IntPoint intPoint;
				bool flag4;
				if (outPt.Pt.X >= num && outPt.Pt.X <= num2)
				{
					intPoint = outPt.Pt;
					flag4 = outPt.Pt.X > outPt3.Pt.X;
				}
				else if (outPt2.Pt.X >= num && outPt2.Pt.X <= num2)
				{
					intPoint = outPt2.Pt;
					flag4 = outPt2.Pt.X > outPt4.Pt.X;
				}
				else if (outPt3.Pt.X >= num && outPt3.Pt.X <= num2)
				{
					intPoint = outPt3.Pt;
					flag4 = outPt3.Pt.X > outPt.Pt.X;
				}
				else
				{
					intPoint = outPt4.Pt;
					flag4 = outPt4.Pt.X > outPt2.Pt.X;
				}
				p1 = outPt;
				p2 = outPt2;
				return this.JoinHorz(outPt, outPt3, outPt2, outPt4, intPoint, flag4);
			}
			else
			{
				OutPt outPt3 = outPt.Next;
				while (outPt3.Pt == outPt.Pt && outPt3 != outPt)
				{
					outPt3 = outPt3.Next;
				}
				bool flag5 = outPt3.Pt.Y > outPt.Pt.Y || !ClipperBase.SlopesEqual(outPt.Pt, outPt3.Pt, j.OffPt, this.m_UseFullRange);
				if (flag5)
				{
					outPt3 = outPt.Prev;
					while (outPt3.Pt == outPt.Pt && outPt3 != outPt)
					{
						outPt3 = outPt3.Prev;
					}
					if (outPt3.Pt.Y > outPt.Pt.Y || !ClipperBase.SlopesEqual(outPt.Pt, outPt3.Pt, j.OffPt, this.m_UseFullRange))
					{
						return false;
					}
				}
				OutPt outPt4 = outPt2.Next;
				while (outPt4.Pt == outPt2.Pt && outPt4 != outPt2)
				{
					outPt4 = outPt4.Next;
				}
				bool flag6 = outPt4.Pt.Y > outPt2.Pt.Y || !ClipperBase.SlopesEqual(outPt2.Pt, outPt4.Pt, j.OffPt, this.m_UseFullRange);
				if (flag6)
				{
					outPt4 = outPt2.Prev;
					while (outPt4.Pt == outPt2.Pt && outPt4 != outPt2)
					{
						outPt4 = outPt4.Prev;
					}
					if (outPt4.Pt.Y > outPt2.Pt.Y || !ClipperBase.SlopesEqual(outPt2.Pt, outPt4.Pt, j.OffPt, this.m_UseFullRange))
					{
						return false;
					}
				}
				if (outPt3 == outPt || outPt4 == outPt2 || outPt3 == outPt4 || (outRec == outRec2 && flag5 == flag6))
				{
					return false;
				}
				if (flag5)
				{
					outPt3 = this.DupOutPt(outPt, false);
					outPt4 = this.DupOutPt(outPt2, true);
					outPt.Prev = outPt2;
					outPt2.Next = outPt;
					outPt3.Next = outPt4;
					outPt4.Prev = outPt3;
					p1 = outPt;
					p2 = outPt3;
					return true;
				}
				outPt3 = this.DupOutPt(outPt, true);
				outPt4 = this.DupOutPt(outPt2, false);
				outPt.Next = outPt2;
				outPt2.Prev = outPt;
				outPt3.Prev = outPt4;
				outPt4.Next = outPt3;
				p1 = outPt;
				p2 = outPt3;
				return true;
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00009420 File Offset: 0x00007620
		private bool Poly2ContainsPoly1(OutPt outPt1, OutPt outPt2, bool UseFullRange)
		{
			OutPt outPt3 = outPt1;
			if (base.PointOnPolygon(outPt3.Pt, outPt2, UseFullRange))
			{
				outPt3 = outPt3.Next;
				while (outPt3 != outPt1 && base.PointOnPolygon(outPt3.Pt, outPt2, UseFullRange))
				{
					outPt3 = outPt3.Next;
				}
				if (outPt3 == outPt1)
				{
					return true;
				}
			}
			return base.PointInPolygon(outPt3.Pt, outPt2, UseFullRange);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00009488 File Offset: 0x00007688
		private void FixupFirstLefts1(OutRec OldOutRec, OutRec NewOutRec)
		{
			for (int i = 0; i < this.m_PolyOuts.Count; i++)
			{
				OutRec outRec = this.m_PolyOuts[i];
				if (outRec.Pts != null && outRec.FirstLeft == OldOutRec && this.Poly2ContainsPoly1(outRec.Pts, NewOutRec.Pts, this.m_UseFullRange))
				{
					outRec.FirstLeft = NewOutRec;
				}
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000094FC File Offset: 0x000076FC
		private void FixupFirstLefts2(OutRec OldOutRec, OutRec NewOutRec)
		{
			foreach (OutRec outRec in this.m_PolyOuts)
			{
				if (outRec.FirstLeft == OldOutRec)
				{
					outRec.FirstLeft = NewOutRec;
				}
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00009570 File Offset: 0x00007770
		private void JoinCommonEdges()
		{
			for (int i = 0; i < this.m_Joins.Count; i++)
			{
				Join join = this.m_Joins[i];
				OutRec outRec = this.GetOutRec(join.OutPt1.Idx);
				OutRec outRec2 = this.GetOutRec(join.OutPt2.Idx);
				if (outRec.Pts != null && outRec2.Pts != null)
				{
					OutRec outRec3;
					if (outRec == outRec2)
					{
						outRec3 = outRec;
					}
					else if (this.Param1RightOfParam2(outRec, outRec2))
					{
						outRec3 = outRec2;
					}
					else if (this.Param1RightOfParam2(outRec2, outRec))
					{
						outRec3 = outRec;
					}
					else
					{
						outRec3 = this.GetLowermostRec(outRec, outRec2);
					}
					OutPt outPt;
					OutPt outPt2;
					if (this.JoinPoints(join, out outPt, out outPt2))
					{
						if (outRec == outRec2)
						{
							outRec.Pts = outPt;
							outRec.BottomPt = null;
							outRec2 = this.CreateOutRec();
							outRec2.Pts = outPt2;
							this.UpdateOutPtIdxs(outRec2);
							if (this.Poly2ContainsPoly1(outRec2.Pts, outRec.Pts, this.m_UseFullRange))
							{
								outRec2.IsHole = !outRec.IsHole;
								outRec2.FirstLeft = outRec;
								if (this.m_UsingPolyTree)
								{
									this.FixupFirstLefts2(outRec2, outRec);
								}
								if ((outRec2.IsHole ^ this.ReverseSolution) == this.Area(outRec2) > 0.0)
								{
									this.ReversePolyPtLinks(outRec2.Pts);
								}
							}
							else if (this.Poly2ContainsPoly1(outRec.Pts, outRec2.Pts, this.m_UseFullRange))
							{
								outRec2.IsHole = outRec.IsHole;
								outRec.IsHole = !outRec2.IsHole;
								outRec2.FirstLeft = outRec.FirstLeft;
								outRec.FirstLeft = outRec2;
								if (this.m_UsingPolyTree)
								{
									this.FixupFirstLefts2(outRec, outRec2);
								}
								if ((outRec.IsHole ^ this.ReverseSolution) == this.Area(outRec) > 0.0)
								{
									this.ReversePolyPtLinks(outRec.Pts);
								}
							}
							else
							{
								outRec2.IsHole = outRec.IsHole;
								outRec2.FirstLeft = outRec.FirstLeft;
								if (this.m_UsingPolyTree)
								{
									this.FixupFirstLefts1(outRec, outRec2);
								}
							}
						}
						else
						{
							outRec2.Pts = null;
							outRec2.BottomPt = null;
							outRec2.Idx = outRec.Idx;
							outRec.IsHole = outRec3.IsHole;
							if (outRec3 == outRec2)
							{
								outRec.FirstLeft = outRec2.FirstLeft;
							}
							outRec2.FirstLeft = outRec;
							if (this.m_UsingPolyTree)
							{
								this.FixupFirstLefts2(outRec2, outRec);
							}
						}
					}
				}
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000097FC File Offset: 0x000079FC
		private void UpdateOutPtIdxs(OutRec outrec)
		{
			OutPt outPt = outrec.Pts;
			do
			{
				outPt.Idx = outrec.Idx;
				outPt = outPt.Prev;
			}
			while (outPt != outrec.Pts);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00009830 File Offset: 0x00007A30
		private void DoSimplePolygons()
		{
			int i = 0;
			while (i < this.m_PolyOuts.Count)
			{
				OutRec outRec = this.m_PolyOuts[i++];
				OutPt outPt = outRec.Pts;
				if (outPt != null)
				{
					do
					{
						for (OutPt outPt2 = outPt.Next; outPt2 != outRec.Pts; outPt2 = outPt2.Next)
						{
							if (outPt.Pt == outPt2.Pt && outPt2.Next != outPt && outPt2.Prev != outPt)
							{
								OutPt prev = outPt.Prev;
								OutPt prev2 = outPt2.Prev;
								outPt.Prev = prev2;
								prev2.Next = outPt;
								outPt2.Prev = prev;
								prev.Next = outPt2;
								outRec.Pts = outPt;
								OutRec outRec2 = this.CreateOutRec();
								outRec2.Pts = outPt2;
								this.UpdateOutPtIdxs(outRec2);
								if (this.Poly2ContainsPoly1(outRec2.Pts, outRec.Pts, this.m_UseFullRange))
								{
									outRec2.IsHole = !outRec.IsHole;
									outRec2.FirstLeft = outRec;
								}
								else if (this.Poly2ContainsPoly1(outRec.Pts, outRec2.Pts, this.m_UseFullRange))
								{
									outRec2.IsHole = outRec.IsHole;
									outRec.IsHole = !outRec2.IsHole;
									outRec2.FirstLeft = outRec.FirstLeft;
									outRec.FirstLeft = outRec2;
								}
								else
								{
									outRec2.IsHole = outRec.IsHole;
									outRec2.FirstLeft = outRec.FirstLeft;
								}
								outPt2 = outPt;
							}
						}
						outPt = outPt.Next;
					}
					while (outPt != outRec.Pts);
				}
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000099D4 File Offset: 0x00007BD4
		public static double Area(List<IntPoint> poly)
		{
			int num = poly.Count - 1;
			if (num < 2)
			{
				return 0.0;
			}
			double num2 = ((double)poly[num].X + (double)poly[0].X) * ((double)poly[0].Y - (double)poly[num].Y);
			for (int i = 1; i <= num; i++)
			{
				num2 += ((double)poly[i - 1].X + (double)poly[i].X) * ((double)poly[i].Y - (double)poly[i - 1].Y);
			}
			return num2 / 2.0;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00009AB4 File Offset: 0x00007CB4
		private double Area(OutRec outRec)
		{
			OutPt outPt = outRec.Pts;
			if (outPt == null)
			{
				return 0.0;
			}
			double num = 0.0;
			do
			{
				num += (double)(outPt.Pt.X + outPt.Prev.Pt.X) * (double)(outPt.Prev.Pt.Y - outPt.Pt.Y);
				outPt = outPt.Next;
			}
			while (outPt != outRec.Pts);
			return num / 2.0;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00009B40 File Offset: 0x00007D40
		internal static DoublePoint GetUnitNormal(IntPoint pt1, IntPoint pt2)
		{
			double num = (double)(pt2.X - pt1.X);
			double num2 = (double)(pt2.Y - pt1.Y);
			if (num == 0.0 && num2 == 0.0)
			{
				return default(DoublePoint);
			}
			double num3 = 1.0 / Math.Sqrt(num * num + num2 * num2);
			num *= num3;
			num2 *= num3;
			return new DoublePoint(num2, -num);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00009BC0 File Offset: 0x00007DC0
		internal static bool UpdateBotPt(IntPoint pt, ref IntPoint botPt)
		{
			if (pt.Y > botPt.Y || (pt.Y == botPt.Y && pt.X < botPt.X))
			{
				botPt = pt;
				return true;
			}
			return false;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00009C10 File Offset: 0x00007E10
		internal static bool StripDupsAndGetBotPt(List<IntPoint> in_path, List<IntPoint> out_path, bool closed, out IntPoint botPt)
		{
			botPt = new IntPoint(0L, 0L);
			int num = in_path.Count;
			if (closed)
			{
				while (num > 0 && in_path[0] == in_path[num - 1])
				{
					num--;
				}
			}
			if (num == 0)
			{
				return false;
			}
			out_path.Capacity = num;
			int num2 = 0;
			out_path.Add(in_path[0]);
			botPt = in_path[0];
			for (int i = 1; i < num; i++)
			{
				if (in_path[i] != out_path[num2])
				{
					out_path.Add(in_path[i]);
					num2++;
					if (out_path[num2].Y > botPt.Y || (out_path[num2].Y == botPt.Y && out_path[num2].X < botPt.X))
					{
						botPt = out_path[num2];
					}
				}
			}
			num2++;
			if (num2 < 2 || (closed && num2 == 2))
			{
				num2 = 0;
			}
			while (out_path.Count > num2)
			{
				out_path.RemoveAt(num2);
			}
			return num2 > 0;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00009D58 File Offset: 0x00007F58
		public static List<List<IntPoint>> OffsetPaths(List<List<IntPoint>> polys, double delta, JoinType jointype, EndType endtype, double MiterLimit)
		{
			List<List<IntPoint>> list = new List<List<IntPoint>>(polys.Count);
			IntPoint intPoint = default(IntPoint);
			int num = -1;
			for (int i = 0; i < polys.Count; i++)
			{
				list.Add(new List<IntPoint>());
				IntPoint intPoint2;
				if (Clipper.StripDupsAndGetBotPt(polys[i], list[i], endtype == EndType.etClosed, out intPoint2) && (num < 0 || intPoint2.Y > intPoint.Y || (intPoint2.Y == intPoint.Y && intPoint2.X < intPoint.X)))
				{
					intPoint = intPoint2;
					num = i;
				}
			}
			if (endtype == EndType.etClosed && num >= 0 && !Clipper.Orientation(list[num]))
			{
				Clipper.ReversePaths(list);
			}
			List<List<IntPoint>> list2;
			new Clipper.PolyOffsetBuilder(list, out list2, delta, jointype, endtype, MiterLimit);
			return list2;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00009E3C File Offset: 0x0000803C
		public static List<List<IntPoint>> OffsetPolygons(List<List<IntPoint>> poly, double delta, JoinType jointype, double MiterLimit, bool AutoFix)
		{
			return Clipper.OffsetPaths(poly, delta, jointype, EndType.etClosed, MiterLimit);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00009E48 File Offset: 0x00008048
		public static List<List<IntPoint>> OffsetPolygons(List<List<IntPoint>> poly, double delta, JoinType jointype, double MiterLimit)
		{
			return Clipper.OffsetPaths(poly, delta, jointype, EndType.etClosed, MiterLimit);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00009E54 File Offset: 0x00008054
		public static List<List<IntPoint>> OffsetPolygons(List<List<IntPoint>> polys, double delta, JoinType jointype)
		{
			return Clipper.OffsetPaths(polys, delta, jointype, EndType.etClosed, 0.0);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00009E68 File Offset: 0x00008068
		public static List<List<IntPoint>> OffsetPolygons(List<List<IntPoint>> polys, double delta)
		{
			return Clipper.OffsetPolygons(polys, delta, JoinType.jtSquare, 0.0, true);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00009E7C File Offset: 0x0000807C
		public static void ReversePolygons(List<List<IntPoint>> polys)
		{
			polys.ForEach(delegate(List<IntPoint> poly)
			{
				poly.Reverse();
			});
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00009EA4 File Offset: 0x000080A4
		public static void PolyTreeToPolygons(PolyTree polytree, List<List<IntPoint>> polys)
		{
			polys.Clear();
			polys.Capacity = polytree.Total;
			Clipper.AddPolyNodeToPaths(polytree, Clipper.NodeType.ntAny, polys);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00009ECC File Offset: 0x000080CC
		public static List<List<IntPoint>> SimplifyPolygon(List<IntPoint> poly, PolyFillType fillType = PolyFillType.pftEvenOdd)
		{
			List<List<IntPoint>> list = new List<List<IntPoint>>();
			Clipper clipper = new Clipper(0);
			clipper.StrictlySimple = true;
			clipper.AddPath(poly, PolyType.ptSubject, true);
			clipper.Execute(ClipType.ctUnion, list, fillType, fillType);
			return list;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00009F04 File Offset: 0x00008104
		public static List<List<IntPoint>> SimplifyPolygons(List<List<IntPoint>> polys, PolyFillType fillType = PolyFillType.pftEvenOdd)
		{
			List<List<IntPoint>> list = new List<List<IntPoint>>();
			Clipper clipper = new Clipper(0);
			clipper.StrictlySimple = true;
			clipper.AddPaths(polys, PolyType.ptSubject, true);
			clipper.Execute(ClipType.ctUnion, list, fillType, fillType);
			return list;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00009F3C File Offset: 0x0000813C
		private static double DistanceSqrd(IntPoint pt1, IntPoint pt2)
		{
			double num = (double)pt1.X - (double)pt2.X;
			double num2 = (double)pt1.Y - (double)pt2.Y;
			return num * num + num2 * num2;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00009F74 File Offset: 0x00008174
		private static DoublePoint ClosestPointOnLine(IntPoint pt, IntPoint linePt1, IntPoint linePt2)
		{
			double num = (double)linePt2.X - (double)linePt1.X;
			double num2 = (double)linePt2.Y - (double)linePt1.Y;
			if (num == 0.0 && num2 == 0.0)
			{
				return new DoublePoint((double)linePt1.X, (double)linePt1.Y);
			}
			double num3 = ((double)(pt.X - linePt1.X) * num + (double)(pt.Y - linePt1.Y) * num2) / (num * num + num2 * num2);
			return new DoublePoint((1.0 - num3) * (double)linePt1.X + num3 * (double)linePt2.X, (1.0 - num3) * (double)linePt1.Y + num3 * (double)linePt2.Y);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000A04C File Offset: 0x0000824C
		private static bool SlopesNearCollinear(IntPoint pt1, IntPoint pt2, IntPoint pt3, double distSqrd)
		{
			if (Clipper.DistanceSqrd(pt1, pt2) > Clipper.DistanceSqrd(pt1, pt3))
			{
				return false;
			}
			DoublePoint doublePoint = Clipper.ClosestPointOnLine(pt2, pt1, pt3);
			double num = (double)pt2.X - doublePoint.X;
			double num2 = (double)pt2.Y - doublePoint.Y;
			return num * num + num2 * num2 < distSqrd;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000A0A4 File Offset: 0x000082A4
		private static bool PointsAreClose(IntPoint pt1, IntPoint pt2, double distSqrd)
		{
			double num = (double)pt1.X - (double)pt2.X;
			double num2 = (double)pt1.Y - (double)pt2.Y;
			return num * num + num2 * num2 <= distSqrd;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000A0E4 File Offset: 0x000082E4
		public static List<IntPoint> CleanPolygon(List<IntPoint> path, double distance = 1.415)
		{
			double num = distance * distance;
			int num2 = path.Count - 1;
			List<IntPoint> list = new List<IntPoint>(num2 + 1);
			while (num2 > 0 && Clipper.PointsAreClose(path[num2], path[0], num))
			{
				num2--;
			}
			if (num2 < 2)
			{
				return list;
			}
			IntPoint intPoint = path[num2];
			int num3 = 0;
			for (;;)
			{
				while (num3 < num2 && Clipper.PointsAreClose(intPoint, path[num3], num))
				{
					num3 += 2;
				}
				int num4 = num3;
				while (num3 < num2 && (Clipper.PointsAreClose(path[num3], path[num3 + 1], num) || Clipper.SlopesNearCollinear(intPoint, path[num3], path[num3 + 1], num)))
				{
					num3++;
				}
				if (num3 >= num2)
				{
					break;
				}
				if (num3 == num4)
				{
					intPoint = path[num3++];
					list.Add(intPoint);
				}
			}
			if (num3 <= num2)
			{
				list.Add(path[num3]);
			}
			num3 = list.Count;
			if (num3 > 2 && Clipper.SlopesNearCollinear(list[num3 - 2], list[num3 - 1], list[0], num))
			{
				list.RemoveAt(num3 - 1);
			}
			if (list.Count < 3)
			{
				list.Clear();
			}
			return list;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000A260 File Offset: 0x00008460
		internal static List<List<IntPoint>> Minkowki(List<IntPoint> poly, List<IntPoint> path, bool IsSum, bool IsClosed)
		{
			int num = ((!IsClosed) ? 0 : 1);
			int count = poly.Count;
			int count2 = path.Count;
			List<List<IntPoint>> list = new List<List<IntPoint>>(count2);
			if (IsSum)
			{
				for (int i = 0; i < count2; i++)
				{
					List<IntPoint> list2 = new List<IntPoint>(count);
					foreach (IntPoint intPoint in poly)
					{
						list2.Add(new IntPoint(path[i].X + intPoint.X, path[i].Y + intPoint.Y));
					}
					list.Add(list2);
				}
			}
			else
			{
				for (int j = 0; j < count2; j++)
				{
					List<IntPoint> list3 = new List<IntPoint>(count);
					foreach (IntPoint intPoint2 in poly)
					{
						list3.Add(new IntPoint(path[j].X - intPoint2.X, path[j].Y - intPoint2.Y));
					}
					list.Add(list3);
				}
			}
			List<List<IntPoint>> list4 = new List<List<IntPoint>>((count2 + num) * (count + 1));
			for (int k = 0; k <= count2 - 2 + num; k++)
			{
				for (int l = 0; l <= count - 1; l++)
				{
					List<IntPoint> list5 = new List<IntPoint>(4);
					list5.Add(list[k % count2][l % count]);
					list5.Add(list[(k + 1) % count2][l % count]);
					list5.Add(list[(k + 1) % count2][(l + 1) % count]);
					list5.Add(list[k % count2][(l + 1) % count]);
					if (!Clipper.Orientation(list5))
					{
						list5.Reverse();
					}
					list4.Add(list5);
				}
			}
			Clipper clipper = new Clipper(0);
			clipper.AddPaths(list4, PolyType.ptSubject, true);
			clipper.Execute(ClipType.ctUnion, list, PolyFillType.pftNonZero, PolyFillType.pftNonZero);
			return list;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000A4F8 File Offset: 0x000086F8
		public static List<List<IntPoint>> MinkowkiSum(List<IntPoint> poly, List<IntPoint> path, bool IsClosed)
		{
			return Clipper.Minkowki(poly, path, true, IsClosed);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000A504 File Offset: 0x00008704
		public static List<List<IntPoint>> MinkowkiDiff(List<IntPoint> poly, List<IntPoint> path, bool IsClosed)
		{
			return Clipper.Minkowki(poly, path, false, IsClosed);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000A510 File Offset: 0x00008710
		public static List<List<IntPoint>> CleanPolygons(List<List<IntPoint>> polys, double distance = 1.415)
		{
			List<List<IntPoint>> list = new List<List<IntPoint>>(polys.Count);
			for (int i = 0; i < polys.Count; i++)
			{
				list.Add(Clipper.CleanPolygon(polys[i], distance));
			}
			return list;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000A554 File Offset: 0x00008754
		public static List<List<IntPoint>> PolyTreeToPaths(PolyTree polytree)
		{
			List<List<IntPoint>> list = new List<List<IntPoint>>();
			list.Capacity = polytree.Total;
			Clipper.AddPolyNodeToPaths(polytree, Clipper.NodeType.ntAny, list);
			return list;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000A57C File Offset: 0x0000877C
		internal static void AddPolyNodeToPaths(PolyNode polynode, Clipper.NodeType nt, List<List<IntPoint>> paths)
		{
			bool flag = true;
			if (nt != Clipper.NodeType.ntOpen)
			{
				if (nt == Clipper.NodeType.ntClosed)
				{
					flag = !polynode.IsOpen;
				}
				if (polynode.Contour.Count > 0 && flag)
				{
					paths.Add(polynode.Contour);
				}
				foreach (PolyNode polyNode in polynode.Childs)
				{
					Clipper.AddPolyNodeToPaths(polyNode, nt, paths);
				}
				return;
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000A634 File Offset: 0x00008834
		public static List<List<IntPoint>> OpenPathsFromPolyTree(PolyTree polytree)
		{
			List<List<IntPoint>> list = new List<List<IntPoint>>();
			list.Capacity = polytree.ChildCount;
			for (int i = 0; i < polytree.ChildCount; i++)
			{
				if (polytree.Childs[i].IsOpen)
				{
					list.Add(polytree.Childs[i].Contour);
				}
			}
			return list;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000A698 File Offset: 0x00008898
		public static List<List<IntPoint>> ClosedPathsFromPolyTree(PolyTree polytree)
		{
			List<List<IntPoint>> list = new List<List<IntPoint>>();
			list.Capacity = polytree.Total;
			Clipper.AddPolyNodeToPaths(polytree, Clipper.NodeType.ntClosed, list);
			return list;
		}

		// Token: 0x04000063 RID: 99
		public const int ioReverseSolution = 1;

		// Token: 0x04000064 RID: 100
		public const int ioStrictlySimple = 2;

		// Token: 0x04000065 RID: 101
		public const int ioPreserveCollinear = 4;

		// Token: 0x04000066 RID: 102
		private List<OutRec> m_PolyOuts;

		// Token: 0x04000067 RID: 103
		private ClipType m_ClipType;

		// Token: 0x04000068 RID: 104
		private Scanbeam m_Scanbeam;

		// Token: 0x04000069 RID: 105
		private TEdge m_ActiveEdges;

		// Token: 0x0400006A RID: 106
		private TEdge m_SortedEdges;

		// Token: 0x0400006B RID: 107
		private IntersectNode m_IntersectNodes;

		// Token: 0x0400006C RID: 108
		private bool m_ExecuteLocked;

		// Token: 0x0400006D RID: 109
		private PolyFillType m_ClipFillType;

		// Token: 0x0400006E RID: 110
		private PolyFillType m_SubjFillType;

		// Token: 0x0400006F RID: 111
		private List<Join> m_Joins;

		// Token: 0x04000070 RID: 112
		private List<Join> m_GhostJoins;

		// Token: 0x04000071 RID: 113
		private bool m_UsingPolyTree;

		// Token: 0x02000018 RID: 24
		private class PolyOffsetBuilder
		{
			// Token: 0x060000D6 RID: 214 RVA: 0x0000A6D0 File Offset: 0x000088D0
			public PolyOffsetBuilder(List<List<IntPoint>> pts, out List<List<IntPoint>> solution, double delta, JoinType jointype, EndType endtype, double limit = 0.0)
			{
				solution = new List<List<IntPoint>>();
				if (ClipperBase.near_zero(delta))
				{
					solution = pts;
					return;
				}
				this.m_p = pts;
				if (endtype != EndType.etClosed && delta < 0.0)
				{
					delta = -delta;
				}
				this.m_delta = delta;
				if (jointype == JoinType.jtMiter)
				{
					if (limit > 2.0)
					{
						this.m_miterLim = 2.0 / (limit * limit);
					}
					else
					{
						this.m_miterLim = 0.5;
					}
					if (endtype == EndType.etRound)
					{
						limit = 0.25;
					}
				}
				if (jointype == JoinType.jtRound || endtype == EndType.etRound)
				{
					if (limit <= 0.0)
					{
						limit = 0.25;
					}
					else if (limit > Math.Abs(delta) * 0.25)
					{
						limit = Math.Abs(delta) * 0.25;
					}
					this.m_Steps360 = 3.141592653589793 / Math.Acos(1.0 - limit / Math.Abs(delta));
					this.m_sin = Math.Sin(6.283185307179586 / this.m_Steps360);
					this.m_cos = Math.Cos(6.283185307179586 / this.m_Steps360);
					this.m_Steps360 /= 6.283185307179586;
					if (delta < 0.0)
					{
						this.m_sin = -this.m_sin;
					}
				}
				double num = delta * delta;
				solution.Capacity = pts.Count;
				this.m_i = 0;
				while (this.m_i < pts.Count)
				{
					int count = pts[this.m_i].Count;
					if (count != 0 && (count >= 3 || delta > 0.0))
					{
						if (count == 1)
						{
							if (jointype == JoinType.jtRound)
							{
								double num2 = 1.0;
								double num3 = 0.0;
								for (long num4 = 1L; num4 <= Clipper.Round(this.m_Steps360 * 2.0 * 3.141592653589793); num4 += 1L)
								{
									this.AddPoint(new IntPoint(Clipper.Round((double)this.m_p[this.m_i][0].X + num2 * delta), Clipper.Round((double)this.m_p[this.m_i][0].Y + num3 * delta)));
									double num5 = num2;
									num2 = num2 * this.m_cos - this.m_sin * num3;
									num3 = num5 * this.m_sin + num3 * this.m_cos;
								}
							}
							else
							{
								double num6 = -1.0;
								double num7 = -1.0;
								for (int i = 0; i < 4; i++)
								{
									this.AddPoint(new IntPoint(Clipper.Round((double)this.m_p[this.m_i][0].X + num6 * delta), Clipper.Round((double)this.m_p[this.m_i][0].Y + num7 * delta)));
									if (num6 < 0.0)
									{
										num6 = 1.0;
									}
									else if (num7 < 0.0)
									{
										num7 = 1.0;
									}
									else
									{
										num6 = -1.0;
									}
								}
							}
						}
						else
						{
							this.normals.Clear();
							this.normals.Capacity = count;
							for (int j = 0; j < count - 1; j++)
							{
								this.normals.Add(Clipper.GetUnitNormal(pts[this.m_i][j], pts[this.m_i][j + 1]));
							}
							if (endtype == EndType.etClosed)
							{
								this.normals.Add(Clipper.GetUnitNormal(pts[this.m_i][count - 1], pts[this.m_i][0]));
							}
							else
							{
								this.normals.Add(new DoublePoint(this.normals[count - 2]));
							}
							this.currentPoly = new List<IntPoint>();
							if (endtype == EndType.etClosed)
							{
								this.m_k = count - 1;
								this.m_j = 0;
								while (this.m_j < count)
								{
									this.OffsetPoint(jointype);
									this.m_j++;
								}
								solution.Add(this.currentPoly);
							}
							else
							{
								this.m_k = 0;
								this.m_j = 1;
								while (this.m_j < count - 1)
								{
									this.OffsetPoint(jointype);
									this.m_j++;
								}
								if (endtype == EndType.etButt)
								{
									this.m_j = count - 1;
									IntPoint intPoint = new IntPoint(Clipper.Round((double)pts[this.m_i][this.m_j].X + this.normals[this.m_j].X * delta), Clipper.Round((double)pts[this.m_i][this.m_j].Y + this.normals[this.m_j].Y * delta));
									this.AddPoint(intPoint);
									intPoint = new IntPoint(Clipper.Round((double)pts[this.m_i][this.m_j].X - this.normals[this.m_j].X * delta), Clipper.Round((double)pts[this.m_i][this.m_j].Y - this.normals[this.m_j].Y * delta));
									this.AddPoint(intPoint);
								}
								else
								{
									this.m_j = count - 1;
									this.m_k = count - 2;
									this.m_sinA = 0.0;
									this.normals[this.m_j] = new DoublePoint(-this.normals[this.m_j].X, -this.normals[this.m_j].Y);
									if (endtype == EndType.etSquare)
									{
										this.DoSquare();
									}
									else
									{
										this.DoRound();
									}
								}
								for (int k = count - 1; k > 0; k--)
								{
									this.normals[k] = new DoublePoint(-this.normals[k - 1].X, -this.normals[k - 1].Y);
								}
								this.normals[0] = new DoublePoint(-this.normals[1].X, -this.normals[1].Y);
								this.m_k = count - 1;
								this.m_j = this.m_k - 1;
								while (this.m_j > 0)
								{
									this.OffsetPoint(jointype);
									this.m_j--;
								}
								if (endtype == EndType.etButt)
								{
									IntPoint intPoint = new IntPoint(Clipper.Round((double)pts[this.m_i][0].X - this.normals[0].X * delta), Clipper.Round((double)pts[this.m_i][0].Y - this.normals[0].Y * delta));
									this.AddPoint(intPoint);
									intPoint = new IntPoint(Clipper.Round((double)pts[this.m_i][0].X + this.normals[0].X * delta), Clipper.Round((double)pts[this.m_i][0].Y + this.normals[0].Y * delta));
									this.AddPoint(intPoint);
								}
								else
								{
									this.m_k = 1;
									this.m_sinA = 0.0;
									if (endtype == EndType.etSquare)
									{
										this.DoSquare();
									}
									else
									{
										this.DoRound();
									}
								}
								solution.Add(this.currentPoly);
							}
						}
					}
					this.m_i++;
				}
				Clipper clipper = new Clipper(0);
				clipper.AddPaths(solution, PolyType.ptSubject, true);
				if (delta > 0.0)
				{
					clipper.Execute(ClipType.ctUnion, solution, PolyFillType.pftPositive, PolyFillType.pftPositive);
				}
				else
				{
					IntRect bounds = clipper.GetBounds();
					List<IntPoint> list = new List<IntPoint>(4);
					list.Add(new IntPoint(bounds.left - 10L, bounds.bottom + 10L));
					list.Add(new IntPoint(bounds.right + 10L, bounds.bottom + 10L));
					list.Add(new IntPoint(bounds.right + 10L, bounds.top - 10L));
					list.Add(new IntPoint(bounds.left - 10L, bounds.top - 10L));
					clipper.AddPath(list, PolyType.ptSubject, true);
					clipper.ReverseSolution = true;
					clipper.Execute(ClipType.ctUnion, solution, PolyFillType.pftNegative, PolyFillType.pftNegative);
					if (solution.Count > 0)
					{
						solution.RemoveAt(0);
					}
				}
			}

			// Token: 0x060000D7 RID: 215 RVA: 0x0000B0E0 File Offset: 0x000092E0
			private void OffsetPoint(JoinType jointype)
			{
				this.m_sinA = this.normals[this.m_k].X * this.normals[this.m_j].Y - this.normals[this.m_j].X * this.normals[this.m_k].Y;
				if (this.m_sinA < 5E-05 && this.m_sinA > -5E-05)
				{
					return;
				}
				if (this.m_sinA > 1.0)
				{
					this.m_sinA = 1.0;
				}
				else if (this.m_sinA < -1.0)
				{
					this.m_sinA = -1.0;
				}
				if (this.m_sinA * this.m_delta < 0.0)
				{
					this.AddPoint(new IntPoint(Clipper.Round((double)this.m_p[this.m_i][this.m_j].X + this.normals[this.m_k].X * this.m_delta), Clipper.Round((double)this.m_p[this.m_i][this.m_j].Y + this.normals[this.m_k].Y * this.m_delta)));
					this.AddPoint(this.m_p[this.m_i][this.m_j]);
					this.AddPoint(new IntPoint(Clipper.Round((double)this.m_p[this.m_i][this.m_j].X + this.normals[this.m_j].X * this.m_delta), Clipper.Round((double)this.m_p[this.m_i][this.m_j].Y + this.normals[this.m_j].Y * this.m_delta)));
				}
				else
				{
					switch (jointype)
					{
					case JoinType.jtSquare:
						this.DoSquare();
						break;
					case JoinType.jtRound:
						this.DoRound();
						break;
					case JoinType.jtMiter:
					{
						double num = 1.0 + (this.normals[this.m_j].X * this.normals[this.m_k].X + this.normals[this.m_j].Y * this.normals[this.m_k].Y);
						if (num >= this.m_miterLim)
						{
							this.DoMiter(num);
						}
						else
						{
							this.DoSquare();
						}
						break;
					}
					}
				}
				this.m_k = this.m_j;
			}

			// Token: 0x060000D8 RID: 216 RVA: 0x0000B434 File Offset: 0x00009634
			internal void AddPoint(IntPoint pt)
			{
				if (this.currentPoly.Count == this.currentPoly.Capacity)
				{
					this.currentPoly.Capacity += 128;
				}
				this.currentPoly.Add(pt);
			}

			// Token: 0x060000D9 RID: 217 RVA: 0x0000B480 File Offset: 0x00009680
			internal void DoSquare()
			{
				double num = Math.Tan(Math.Atan2(this.m_sinA, this.normals[this.m_k].X * this.normals[this.m_j].X + this.normals[this.m_k].Y * this.normals[this.m_j].Y) / 4.0);
				this.AddPoint(new IntPoint(Clipper.Round((double)this.m_p[this.m_i][this.m_j].X + this.m_delta * (this.normals[this.m_k].X - this.normals[this.m_k].Y * num)), Clipper.Round((double)this.m_p[this.m_i][this.m_j].Y + this.m_delta * (this.normals[this.m_k].Y + this.normals[this.m_k].X * num))));
				this.AddPoint(new IntPoint(Clipper.Round((double)this.m_p[this.m_i][this.m_j].X + this.m_delta * (this.normals[this.m_j].X + this.normals[this.m_j].Y * num)), Clipper.Round((double)this.m_p[this.m_i][this.m_j].Y + this.m_delta * (this.normals[this.m_j].Y - this.normals[this.m_j].X * num))));
			}

			// Token: 0x060000DA RID: 218 RVA: 0x0000B6D0 File Offset: 0x000098D0
			internal void DoMiter(double r)
			{
				double num = this.m_delta / r;
				this.AddPoint(new IntPoint(Clipper.Round((double)this.m_p[this.m_i][this.m_j].X + (this.normals[this.m_k].X + this.normals[this.m_j].X) * num), Clipper.Round((double)this.m_p[this.m_i][this.m_j].Y + (this.normals[this.m_k].Y + this.normals[this.m_j].Y) * num)));
			}

			// Token: 0x060000DB RID: 219 RVA: 0x0000B7B4 File Offset: 0x000099B4
			internal void DoRound()
			{
				double num = Math.Atan2(this.m_sinA, this.normals[this.m_k].X * this.normals[this.m_j].X + this.normals[this.m_k].Y * this.normals[this.m_j].Y);
				int num2 = (int)Clipper.Round(this.m_Steps360 * Math.Abs(num));
				double num3 = this.normals[this.m_k].X;
				double num4 = this.normals[this.m_k].Y;
				for (int i = 0; i < num2; i++)
				{
					this.AddPoint(new IntPoint(Clipper.Round((double)this.m_p[this.m_i][this.m_j].X + num3 * this.m_delta), Clipper.Round((double)this.m_p[this.m_i][this.m_j].Y + num4 * this.m_delta)));
					double num5 = num3;
					num3 = num3 * this.m_cos - this.m_sin * num4;
					num4 = num5 * this.m_sin + num4 * this.m_cos;
				}
				this.AddPoint(new IntPoint(Clipper.Round((double)this.m_p[this.m_i][this.m_j].X + this.normals[this.m_j].X * this.m_delta), Clipper.Round((double)this.m_p[this.m_i][this.m_j].Y + this.normals[this.m_j].Y * this.m_delta)));
			}

			// Token: 0x04000076 RID: 118
			private const int m_buffLength = 128;

			// Token: 0x04000077 RID: 119
			private List<List<IntPoint>> m_p;

			// Token: 0x04000078 RID: 120
			private List<IntPoint> currentPoly;

			// Token: 0x04000079 RID: 121
			private List<DoublePoint> normals = new List<DoublePoint>();

			// Token: 0x0400007A RID: 122
			private double m_delta;

			// Token: 0x0400007B RID: 123
			private double m_sinA;

			// Token: 0x0400007C RID: 124
			private double m_sin;

			// Token: 0x0400007D RID: 125
			private double m_cos;

			// Token: 0x0400007E RID: 126
			private double m_miterLim;

			// Token: 0x0400007F RID: 127
			private double m_Steps360;

			// Token: 0x04000080 RID: 128
			private int m_i;

			// Token: 0x04000081 RID: 129
			private int m_j;

			// Token: 0x04000082 RID: 130
			private int m_k;
		}

		// Token: 0x02000019 RID: 25
		internal enum NodeType
		{
			// Token: 0x04000084 RID: 132
			ntAny,
			// Token: 0x04000085 RID: 133
			ntOpen,
			// Token: 0x04000086 RID: 134
			ntClosed
		}
	}
}
