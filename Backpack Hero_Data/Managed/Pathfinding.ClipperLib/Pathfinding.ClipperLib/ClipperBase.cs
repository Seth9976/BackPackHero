using System;
using System.Collections.Generic;

namespace Pathfinding.ClipperLib
{
	// Token: 0x02000016 RID: 22
	public class ClipperBase
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00002B68 File Offset: 0x00000D68
		internal ClipperBase()
		{
			this.m_MinimaList = null;
			this.m_CurrentLM = null;
			this.m_UseFullRange = false;
			this.m_HasOpenPaths = false;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002B98 File Offset: 0x00000D98
		internal static bool near_zero(double val)
		{
			return val > -1E-20 && val < 1E-20;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002BB8 File Offset: 0x00000DB8
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002BC0 File Offset: 0x00000DC0
		public bool PreserveCollinear { get; set; }

		// Token: 0x06000039 RID: 57 RVA: 0x00002BCC File Offset: 0x00000DCC
		internal static bool IsHorizontal(TEdge e)
		{
			return e.Delta.Y == 0L;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002BE0 File Offset: 0x00000DE0
		internal bool PointIsVertex(IntPoint pt, OutPt pp)
		{
			OutPt outPt = pp;
			while (!(outPt.Pt == pt))
			{
				outPt = outPt.Next;
				if (outPt == pp)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002C14 File Offset: 0x00000E14
		internal bool PointOnLineSegment(IntPoint pt, IntPoint linePt1, IntPoint linePt2, bool UseFullRange)
		{
			if (UseFullRange)
			{
				return (pt.X == linePt1.X && pt.Y == linePt1.Y) || (pt.X == linePt2.X && pt.Y == linePt2.Y) || (pt.X > linePt1.X == pt.X < linePt2.X && pt.Y > linePt1.Y == pt.Y < linePt2.Y && Int128.Int128Mul(pt.X - linePt1.X, linePt2.Y - linePt1.Y) == Int128.Int128Mul(linePt2.X - linePt1.X, pt.Y - linePt1.Y));
			}
			return (pt.X == linePt1.X && pt.Y == linePt1.Y) || (pt.X == linePt2.X && pt.Y == linePt2.Y) || (pt.X > linePt1.X == pt.X < linePt2.X && pt.Y > linePt1.Y == pt.Y < linePt2.Y && (pt.X - linePt1.X) * (linePt2.Y - linePt1.Y) == (linePt2.X - linePt1.X) * (pt.Y - linePt1.Y));
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002DEC File Offset: 0x00000FEC
		internal bool PointOnPolygon(IntPoint pt, OutPt pp, bool UseFullRange)
		{
			OutPt outPt = pp;
			while (!this.PointOnLineSegment(pt, outPt.Pt, outPt.Next.Pt, UseFullRange))
			{
				outPt = outPt.Next;
				if (outPt == pp)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002E34 File Offset: 0x00001034
		internal bool PointInPolygon(IntPoint pt, OutPt pp, bool UseFullRange)
		{
			OutPt outPt = pp;
			bool flag = false;
			if (UseFullRange)
			{
				do
				{
					if (outPt.Pt.Y > pt.Y != outPt.Prev.Pt.Y > pt.Y && new Int128(pt.X - outPt.Pt.X) < Int128.Int128Mul(outPt.Prev.Pt.X - outPt.Pt.X, pt.Y - outPt.Pt.Y) / new Int128(outPt.Prev.Pt.Y - outPt.Pt.Y))
					{
						flag = !flag;
					}
					outPt = outPt.Next;
				}
				while (outPt != pp);
			}
			else
			{
				do
				{
					if (outPt.Pt.Y > pt.Y != outPt.Prev.Pt.Y > pt.Y && pt.X - outPt.Pt.X < (outPt.Prev.Pt.X - outPt.Pt.X) * (pt.Y - outPt.Pt.Y) / (outPt.Prev.Pt.Y - outPt.Pt.Y))
					{
						flag = !flag;
					}
					outPt = outPt.Next;
				}
				while (outPt != pp);
			}
			return flag;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002FBC File Offset: 0x000011BC
		internal static bool SlopesEqual(TEdge e1, TEdge e2, bool UseFullRange)
		{
			if (UseFullRange)
			{
				return Int128.Int128Mul(e1.Delta.Y, e2.Delta.X) == Int128.Int128Mul(e1.Delta.X, e2.Delta.Y);
			}
			return e1.Delta.Y * e2.Delta.X == e1.Delta.X * e2.Delta.Y;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000303C File Offset: 0x0000123C
		protected static bool SlopesEqual(IntPoint pt1, IntPoint pt2, IntPoint pt3, bool UseFullRange)
		{
			if (UseFullRange)
			{
				return Int128.Int128Mul(pt1.Y - pt2.Y, pt2.X - pt3.X) == Int128.Int128Mul(pt1.X - pt2.X, pt2.Y - pt3.Y);
			}
			return (pt1.Y - pt2.Y) * (pt2.X - pt3.X) - (pt1.X - pt2.X) * (pt2.Y - pt3.Y) == 0L;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000030E0 File Offset: 0x000012E0
		protected static bool SlopesEqual(IntPoint pt1, IntPoint pt2, IntPoint pt3, IntPoint pt4, bool UseFullRange)
		{
			if (UseFullRange)
			{
				return Int128.Int128Mul(pt1.Y - pt2.Y, pt3.X - pt4.X) == Int128.Int128Mul(pt1.X - pt2.X, pt3.Y - pt4.Y);
			}
			return (pt1.Y - pt2.Y) * (pt3.X - pt4.X) - (pt1.X - pt2.X) * (pt3.Y - pt4.Y) == 0L;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003184 File Offset: 0x00001384
		public virtual void Clear()
		{
			this.DisposeLocalMinimaList();
			for (int i = 0; i < this.m_edges.Count; i++)
			{
				for (int j = 0; j < this.m_edges[i].Count; j++)
				{
					this.m_edges[i][j] = null;
				}
				this.m_edges[i].Clear();
			}
			this.m_edges.Clear();
			this.m_UseFullRange = false;
			this.m_HasOpenPaths = false;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003214 File Offset: 0x00001414
		private void DisposeLocalMinimaList()
		{
			while (this.m_MinimaList != null)
			{
				LocalMinima next = this.m_MinimaList.Next;
				this.m_MinimaList = null;
				this.m_MinimaList = next;
			}
			this.m_CurrentLM = null;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003254 File Offset: 0x00001454
		private void RangeTest(IntPoint Pt, ref bool useFullRange)
		{
			if (useFullRange)
			{
				if (Pt.X > 4611686018427387903L || Pt.Y > 4611686018427387903L || -Pt.X > 4611686018427387903L || -Pt.Y > 4611686018427387903L)
				{
					throw new ClipperException("Coordinate outside allowed range");
				}
			}
			else if (Pt.X > 1073741823L || Pt.Y > 1073741823L || -Pt.X > 1073741823L || -Pt.Y > 1073741823L)
			{
				useFullRange = true;
				this.RangeTest(Pt, ref useFullRange);
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003324 File Offset: 0x00001524
		private void InitEdge(TEdge e, TEdge eNext, TEdge ePrev, IntPoint pt)
		{
			e.Next = eNext;
			e.Prev = ePrev;
			e.Curr = pt;
			e.OutIdx = -1;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003344 File Offset: 0x00001544
		private void InitEdge2(TEdge e, PolyType polyType)
		{
			if (e.Curr.Y >= e.Next.Curr.Y)
			{
				e.Bot = e.Curr;
				e.Top = e.Next.Curr;
			}
			else
			{
				e.Top = e.Curr;
				e.Bot = e.Next.Curr;
			}
			this.SetDx(e);
			e.PolyTyp = polyType;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000033C0 File Offset: 0x000015C0
		public bool AddPath(List<IntPoint> pg, PolyType polyType, bool Closed)
		{
			if (!Closed)
			{
				throw new ClipperException("AddPath: Open paths have been disabled.");
			}
			int num = pg.Count - 1;
			bool flag = num > 0 && (Closed || pg[0] == pg[num]);
			while (num > 0 && pg[num] == pg[0])
			{
				num--;
			}
			while (num > 0 && pg[num] == pg[num - 1])
			{
				num--;
			}
			if ((Closed && num < 2) || (!Closed && num < 1))
			{
				return false;
			}
			List<TEdge> list = new List<TEdge>(num + 1);
			for (int i = 0; i <= num; i++)
			{
				list.Add(new TEdge());
			}
			try
			{
				list[1].Curr = pg[1];
				this.RangeTest(pg[0], ref this.m_UseFullRange);
				this.RangeTest(pg[num], ref this.m_UseFullRange);
				this.InitEdge(list[0], list[1], list[num], pg[0]);
				this.InitEdge(list[num], list[0], list[num - 1], pg[num]);
				for (int j = num - 1; j >= 1; j--)
				{
					this.RangeTest(pg[j], ref this.m_UseFullRange);
					this.InitEdge(list[j], list[j + 1], list[j - 1], pg[j]);
				}
			}
			catch
			{
				return false;
			}
			TEdge tedge = list[0];
			if (!flag)
			{
				tedge.Prev.OutIdx = -2;
			}
			TEdge tedge2 = tedge;
			TEdge tedge3 = tedge;
			for (;;)
			{
				if (tedge2.Curr == tedge2.Next.Curr)
				{
					if (tedge2 == tedge2.Next)
					{
						break;
					}
					if (tedge2 == tedge)
					{
						tedge = tedge2.Next;
					}
					tedge2 = this.RemoveEdge(tedge2);
					tedge3 = tedge2;
				}
				else
				{
					if (tedge2.Prev == tedge2.Next)
					{
						break;
					}
					if ((flag || (tedge2.Prev.OutIdx != -2 && tedge2.OutIdx != -2 && tedge2.Next.OutIdx != -2)) && ClipperBase.SlopesEqual(tedge2.Prev.Curr, tedge2.Curr, tedge2.Next.Curr, this.m_UseFullRange) && Closed && (!this.PreserveCollinear || !this.Pt2IsBetweenPt1AndPt3(tedge2.Prev.Curr, tedge2.Curr, tedge2.Next.Curr)))
					{
						if (tedge2 == tedge)
						{
							tedge = tedge2.Next;
						}
						tedge2 = this.RemoveEdge(tedge2);
						tedge2 = tedge2.Prev;
						tedge3 = tedge2;
					}
					else
					{
						tedge2 = tedge2.Next;
						if (tedge2 == tedge3)
						{
							break;
						}
					}
				}
			}
			if ((!Closed && tedge2 == tedge2.Next) || (Closed && tedge2.Prev == tedge2.Next))
			{
				return false;
			}
			this.m_edges.Add(list);
			if (!Closed)
			{
				this.m_HasOpenPaths = true;
			}
			TEdge tedge4 = tedge;
			tedge2 = tedge;
			do
			{
				this.InitEdge2(tedge2, polyType);
				if (tedge2.Top.Y < tedge4.Top.Y)
				{
					tedge4 = tedge2;
				}
				tedge2 = tedge2.Next;
			}
			while (tedge2 != tedge);
			if (this.AllHorizontal(tedge2))
			{
				if (flag)
				{
					tedge2.Prev.OutIdx = -2;
				}
				this.AscendToMax(ref tedge2, false, false);
				return true;
			}
			tedge2 = tedge.Prev;
			if (tedge2.Prev == tedge2.Next)
			{
				tedge4 = tedge2.Next;
			}
			else if (!flag && tedge2.Top.Y == tedge4.Top.Y)
			{
				if ((ClipperBase.IsHorizontal(tedge2) || ClipperBase.IsHorizontal(tedge2.Next)) && tedge2.Next.Bot.Y == tedge4.Top.Y)
				{
					tedge4 = tedge2.Next;
				}
				else if (this.SharedVertWithPrevAtTop(tedge2))
				{
					tedge4 = tedge2;
				}
				else if (tedge2.Top == tedge2.Prev.Top)
				{
					tedge4 = tedge2.Prev;
				}
				else
				{
					tedge4 = tedge2.Next;
				}
			}
			else
			{
				tedge2 = tedge4;
				while (ClipperBase.IsHorizontal(tedge4) || tedge4.Top == tedge4.Next.Top || tedge4.Top == tedge4.Next.Bot)
				{
					tedge4 = tedge4.Next;
					if (tedge4 == tedge2)
					{
						while (ClipperBase.IsHorizontal(tedge4) || !this.SharedVertWithPrevAtTop(tedge4))
						{
							tedge4 = tedge4.Next;
						}
						break;
					}
				}
			}
			tedge2 = tedge4;
			do
			{
				tedge2 = this.AddBoundsToLML(tedge2, Closed);
			}
			while (tedge2 != tedge4);
			return true;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003978 File Offset: 0x00001B78
		public bool AddPaths(List<List<IntPoint>> ppg, PolyType polyType, bool closed)
		{
			bool flag = false;
			for (int i = 0; i < ppg.Count; i++)
			{
				if (this.AddPath(ppg[i], polyType, closed))
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000039B8 File Offset: 0x00001BB8
		public bool AddPolygon(List<IntPoint> pg, PolyType polyType)
		{
			return this.AddPath(pg, polyType, true);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000039C4 File Offset: 0x00001BC4
		public bool AddPolygons(List<List<IntPoint>> ppg, PolyType polyType)
		{
			bool flag = false;
			for (int i = 0; i < ppg.Count; i++)
			{
				if (this.AddPath(ppg[i], polyType, true))
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003A04 File Offset: 0x00001C04
		internal bool Pt2IsBetweenPt1AndPt3(IntPoint pt1, IntPoint pt2, IntPoint pt3)
		{
			if (pt1 == pt3 || pt1 == pt2 || pt3 == pt2)
			{
				return false;
			}
			if (pt1.X != pt3.X)
			{
				return pt2.X > pt1.X == pt2.X < pt3.X;
			}
			return pt2.Y > pt1.Y == pt2.Y < pt3.Y;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003A90 File Offset: 0x00001C90
		private TEdge RemoveEdge(TEdge e)
		{
			e.Prev.Next = e.Next;
			e.Next.Prev = e.Prev;
			TEdge next = e.Next;
			e.Prev = null;
			return next;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003AD0 File Offset: 0x00001CD0
		private TEdge GetLastHorz(TEdge Edge)
		{
			TEdge tedge = Edge;
			while (tedge.OutIdx != -2 && tedge.Next != Edge && ClipperBase.IsHorizontal(tedge.Next))
			{
				tedge = tedge.Next;
			}
			return tedge;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003B18 File Offset: 0x00001D18
		private bool SharedVertWithPrevAtTop(TEdge Edge)
		{
			TEdge tedge = Edge;
			bool flag = true;
			while (tedge.Prev != Edge)
			{
				if (tedge.Top == tedge.Prev.Top)
				{
					if (tedge.Bot == tedge.Prev.Bot)
					{
						tedge = tedge.Prev;
						continue;
					}
					flag = true;
				}
				else
				{
					flag = false;
				}
				break;
			}
			while (tedge != Edge)
			{
				flag = !flag;
				tedge = tedge.Next;
			}
			return flag;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003BA4 File Offset: 0x00001DA4
		private bool SharedVertWithNextIsBot(TEdge Edge)
		{
			bool flag = true;
			TEdge tedge = Edge;
			while (tedge.Prev != Edge)
			{
				bool flag2 = tedge.Next.Bot == tedge.Bot;
				bool flag3 = tedge.Prev.Bot == tedge.Bot;
				if (flag2 != flag3)
				{
					flag = flag2;
					break;
				}
				flag2 = tedge.Next.Top == tedge.Top;
				flag3 = tedge.Prev.Top == tedge.Top;
				if (flag2 != flag3)
				{
					flag = flag3;
					break;
				}
				tedge = tedge.Prev;
			}
			while (tedge != Edge)
			{
				flag = !flag;
				tedge = tedge.Next;
			}
			return flag;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003C60 File Offset: 0x00001E60
		private bool MoreBelow(TEdge Edge)
		{
			TEdge tedge = Edge;
			if (ClipperBase.IsHorizontal(tedge))
			{
				while (ClipperBase.IsHorizontal(tedge.Next))
				{
					tedge = tedge.Next;
				}
				return tedge.Next.Bot.Y > tedge.Bot.Y;
			}
			if (ClipperBase.IsHorizontal(tedge.Next))
			{
				while (ClipperBase.IsHorizontal(tedge.Next))
				{
					tedge = tedge.Next;
				}
				return tedge.Next.Bot.Y > tedge.Bot.Y;
			}
			return tedge.Bot == tedge.Next.Top;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003D14 File Offset: 0x00001F14
		private bool JustBeforeLocMin(TEdge Edge)
		{
			TEdge tedge = Edge;
			if (ClipperBase.IsHorizontal(tedge))
			{
				while (ClipperBase.IsHorizontal(tedge.Next))
				{
					tedge = tedge.Next;
				}
				return tedge.Next.Top.Y < tedge.Bot.Y;
			}
			return this.SharedVertWithNextIsBot(tedge);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003D70 File Offset: 0x00001F70
		private bool MoreAbove(TEdge Edge)
		{
			if (ClipperBase.IsHorizontal(Edge))
			{
				Edge = this.GetLastHorz(Edge);
				return Edge.Next.Top.Y < Edge.Top.Y;
			}
			if (ClipperBase.IsHorizontal(Edge.Next))
			{
				Edge = this.GetLastHorz(Edge.Next);
				return Edge.Next.Top.Y < Edge.Top.Y;
			}
			return Edge.Next.Top.Y < Edge.Top.Y;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003E08 File Offset: 0x00002008
		private bool AllHorizontal(TEdge Edge)
		{
			if (!ClipperBase.IsHorizontal(Edge))
			{
				return false;
			}
			for (TEdge tedge = Edge.Next; tedge != Edge; tedge = tedge.Next)
			{
				if (!ClipperBase.IsHorizontal(tedge))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003E4C File Offset: 0x0000204C
		private void SetDx(TEdge e)
		{
			e.Delta.X = e.Top.X - e.Bot.X;
			e.Delta.Y = e.Top.Y - e.Bot.Y;
			if (e.Delta.Y == 0L)
			{
				e.Dx = -3.4E+38;
			}
			else
			{
				e.Dx = (double)e.Delta.X / (double)e.Delta.Y;
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003EE4 File Offset: 0x000020E4
		private void DoMinimaLML(TEdge E1, TEdge E2, bool IsClosed)
		{
			if (E1 == null)
			{
				if (E2 == null)
				{
					return;
				}
				LocalMinima localMinima = new LocalMinima();
				localMinima.Next = null;
				localMinima.Y = E2.Bot.Y;
				localMinima.LeftBound = null;
				E2.WindDelta = 0;
				localMinima.RightBound = E2;
				this.InsertLocalMinima(localMinima);
			}
			else
			{
				LocalMinima localMinima2 = new LocalMinima();
				localMinima2.Y = E1.Bot.Y;
				localMinima2.Next = null;
				if (ClipperBase.IsHorizontal(E2))
				{
					if (E2.Bot.X != E1.Bot.X)
					{
						this.ReverseHorizontal(E2);
					}
					localMinima2.LeftBound = E1;
					localMinima2.RightBound = E2;
				}
				else if (E2.Dx < E1.Dx)
				{
					localMinima2.LeftBound = E1;
					localMinima2.RightBound = E2;
				}
				else
				{
					localMinima2.LeftBound = E2;
					localMinima2.RightBound = E1;
				}
				localMinima2.LeftBound.Side = EdgeSide.esLeft;
				localMinima2.RightBound.Side = EdgeSide.esRight;
				if (!IsClosed)
				{
					localMinima2.LeftBound.WindDelta = 0;
				}
				else if (localMinima2.LeftBound.Next == localMinima2.RightBound)
				{
					localMinima2.LeftBound.WindDelta = -1;
				}
				else
				{
					localMinima2.LeftBound.WindDelta = 1;
				}
				localMinima2.RightBound.WindDelta = -localMinima2.LeftBound.WindDelta;
				this.InsertLocalMinima(localMinima2);
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00004050 File Offset: 0x00002250
		private TEdge DescendToMin(ref TEdge E)
		{
			E.NextInLML = null;
			if (ClipperBase.IsHorizontal(E))
			{
				TEdge tedge = E;
				while (ClipperBase.IsHorizontal(tedge.Next))
				{
					tedge = tedge.Next;
				}
				if (tedge.Bot != tedge.Next.Top)
				{
					this.ReverseHorizontal(E);
				}
			}
			for (;;)
			{
				E = E.Next;
				if (E.OutIdx == -2)
				{
					break;
				}
				if (ClipperBase.IsHorizontal(E))
				{
					TEdge tedge = this.GetLastHorz(E);
					if (tedge == E.Prev || (tedge.Next.Top.Y < E.Top.Y && tedge.Next.Bot.X > E.Prev.Bot.X))
					{
						break;
					}
					if (E.Top.X != E.Prev.Bot.X)
					{
						this.ReverseHorizontal(E);
					}
					if (tedge.OutIdx == -2)
					{
						tedge = tedge.Prev;
					}
					while (E != tedge)
					{
						E.NextInLML = E.Prev;
						E = E.Next;
						if (E.Top.X != E.Prev.Bot.X)
						{
							this.ReverseHorizontal(E);
						}
					}
				}
				else if (E.Bot.Y == E.Prev.Bot.Y)
				{
					break;
				}
				E.NextInLML = E.Prev;
			}
			return E.Prev;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00004214 File Offset: 0x00002414
		private void AscendToMax(ref TEdge E, bool Appending, bool IsClosed)
		{
			if (E.OutIdx == -2)
			{
				E = E.Next;
				if (!this.MoreAbove(E.Prev))
				{
					return;
				}
			}
			if (ClipperBase.IsHorizontal(E) && Appending && E.Bot != E.Prev.Bot)
			{
				this.ReverseHorizontal(E);
			}
			TEdge tedge = E;
			while (E.Next.OutIdx != -2 && (E.Next.Top.Y != E.Top.Y || ClipperBase.IsHorizontal(E.Next)))
			{
				E.NextInLML = E.Next;
				E = E.Next;
				if (ClipperBase.IsHorizontal(E) && E.Bot.X != E.Prev.Top.X)
				{
					this.ReverseHorizontal(E);
				}
			}
			if (!Appending)
			{
				if (tedge.OutIdx == -2)
				{
					tedge = tedge.Next;
				}
				if (tedge != E.Next)
				{
					this.DoMinimaLML(null, tedge, IsClosed);
				}
			}
			E = E.Next;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004360 File Offset: 0x00002560
		private TEdge AddBoundsToLML(TEdge E, bool Closed)
		{
			TEdge tedge;
			if (E.OutIdx == -2)
			{
				if (this.MoreBelow(E))
				{
					E = E.Next;
					tedge = this.DescendToMin(ref E);
				}
				else
				{
					tedge = null;
				}
			}
			else
			{
				tedge = this.DescendToMin(ref E);
			}
			bool flag;
			if (E.OutIdx == -2)
			{
				this.DoMinimaLML(null, tedge, Closed);
				flag = false;
				if (E.Bot != E.Prev.Bot && this.MoreBelow(E))
				{
					E = E.Next;
					tedge = this.DescendToMin(ref E);
					this.DoMinimaLML(tedge, E, Closed);
					flag = true;
				}
				else if (this.JustBeforeLocMin(E))
				{
					E = E.Next;
				}
			}
			else
			{
				this.DoMinimaLML(tedge, E, Closed);
				flag = true;
			}
			this.AscendToMax(ref E, flag, Closed);
			if (E.OutIdx == -2 && E.Top != E.Prev.Top)
			{
				if (this.MoreAbove(E))
				{
					E = E.Next;
					this.AscendToMax(ref E, false, Closed);
				}
				else if (E.Top == E.Next.Top || (ClipperBase.IsHorizontal(E.Next) && E.Top == E.Next.Bot))
				{
					E = E.Next;
				}
			}
			return E;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000044D4 File Offset: 0x000026D4
		private void InsertLocalMinima(LocalMinima newLm)
		{
			if (this.m_MinimaList == null)
			{
				this.m_MinimaList = newLm;
			}
			else if (newLm.Y >= this.m_MinimaList.Y)
			{
				newLm.Next = this.m_MinimaList;
				this.m_MinimaList = newLm;
			}
			else
			{
				LocalMinima localMinima = this.m_MinimaList;
				while (localMinima.Next != null && newLm.Y < localMinima.Next.Y)
				{
					localMinima = localMinima.Next;
				}
				newLm.Next = localMinima.Next;
				localMinima.Next = newLm;
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00004570 File Offset: 0x00002770
		protected void PopLocalMinima()
		{
			if (this.m_CurrentLM == null)
			{
				return;
			}
			this.m_CurrentLM = this.m_CurrentLM.Next;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00004590 File Offset: 0x00002790
		private void ReverseHorizontal(TEdge e)
		{
			long x = e.Top.X;
			e.Top.X = e.Bot.X;
			e.Bot.X = x;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000045CC File Offset: 0x000027CC
		protected virtual void Reset()
		{
			this.m_CurrentLM = this.m_MinimaList;
			if (this.m_CurrentLM == null)
			{
				return;
			}
			for (LocalMinima localMinima = this.m_MinimaList; localMinima != null; localMinima = localMinima.Next)
			{
				TEdge tedge = localMinima.LeftBound;
				if (tedge != null)
				{
					tedge.Curr = tedge.Bot;
					tedge.Side = EdgeSide.esLeft;
					if (tedge.OutIdx != -2)
					{
						tedge.OutIdx = -1;
					}
				}
				tedge = localMinima.RightBound;
				tedge.Curr = tedge.Bot;
				tedge.Side = EdgeSide.esRight;
				if (tedge.OutIdx != -2)
				{
					tedge.OutIdx = -1;
				}
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000466C File Offset: 0x0000286C
		public IntRect GetBounds()
		{
			IntRect intRect = default(IntRect);
			LocalMinima localMinima = this.m_MinimaList;
			if (localMinima == null)
			{
				return intRect;
			}
			intRect.left = localMinima.LeftBound.Bot.X;
			intRect.top = localMinima.LeftBound.Bot.Y;
			intRect.right = localMinima.LeftBound.Bot.X;
			intRect.bottom = localMinima.LeftBound.Bot.Y;
			while (localMinima != null)
			{
				if (localMinima.LeftBound.Bot.Y > intRect.bottom)
				{
					intRect.bottom = localMinima.LeftBound.Bot.Y;
				}
				TEdge tedge = localMinima.LeftBound;
				for (;;)
				{
					TEdge tedge2 = tedge;
					while (tedge.NextInLML != null)
					{
						if (tedge.Bot.X < intRect.left)
						{
							intRect.left = tedge.Bot.X;
						}
						if (tedge.Bot.X > intRect.right)
						{
							intRect.right = tedge.Bot.X;
						}
						tedge = tedge.NextInLML;
					}
					if (tedge.Bot.X < intRect.left)
					{
						intRect.left = tedge.Bot.X;
					}
					if (tedge.Bot.X > intRect.right)
					{
						intRect.right = tedge.Bot.X;
					}
					if (tedge.Top.X < intRect.left)
					{
						intRect.left = tedge.Top.X;
					}
					if (tedge.Top.X > intRect.right)
					{
						intRect.right = tedge.Top.X;
					}
					if (tedge.Top.Y < intRect.top)
					{
						intRect.top = tedge.Top.Y;
					}
					if (tedge2 != localMinima.LeftBound)
					{
						break;
					}
					tedge = localMinima.RightBound;
				}
				localMinima = localMinima.Next;
			}
			return intRect;
		}

		// Token: 0x04000057 RID: 87
		protected const double horizontal = -3.4E+38;

		// Token: 0x04000058 RID: 88
		protected const int Skip = -2;

		// Token: 0x04000059 RID: 89
		protected const int Unassigned = -1;

		// Token: 0x0400005A RID: 90
		protected const double tolerance = 1E-20;

		// Token: 0x0400005B RID: 91
		internal const long loRange = 1073741823L;

		// Token: 0x0400005C RID: 92
		internal const long hiRange = 4611686018427387903L;

		// Token: 0x0400005D RID: 93
		internal LocalMinima m_MinimaList;

		// Token: 0x0400005E RID: 94
		internal LocalMinima m_CurrentLM;

		// Token: 0x0400005F RID: 95
		internal List<List<TEdge>> m_edges = new List<List<TEdge>>();

		// Token: 0x04000060 RID: 96
		internal bool m_UseFullRange;

		// Token: 0x04000061 RID: 97
		internal bool m_HasOpenPaths;
	}
}
