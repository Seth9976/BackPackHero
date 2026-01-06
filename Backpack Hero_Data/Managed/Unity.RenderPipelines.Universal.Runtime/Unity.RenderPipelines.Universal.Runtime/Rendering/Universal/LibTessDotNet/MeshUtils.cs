using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal.LibTessDotNet
{
	// Token: 0x020000F4 RID: 244
	internal static class MeshUtils
	{
		// Token: 0x0600070D RID: 1805 RVA: 0x00028140 File Offset: 0x00026340
		public static MeshUtils.Edge MakeEdge(MeshUtils.Edge eNext)
		{
			MeshUtils.EdgePair edgePair = MeshUtils.EdgePair.Create();
			MeshUtils.Edge e = edgePair._e;
			MeshUtils.Edge eSym = edgePair._eSym;
			MeshUtils.Edge.EnsureFirst(ref eNext);
			MeshUtils.Edge next = eNext._Sym._next;
			eSym._next = next;
			next._Sym._next = e;
			e._next = eNext;
			eNext._Sym._next = eSym;
			e._Sym = eSym;
			e._Onext = e;
			e._Lnext = eSym;
			e._Org = null;
			e._Lface = null;
			e._winding = 0;
			e._activeRegion = null;
			eSym._Sym = e;
			eSym._Onext = eSym;
			eSym._Lnext = e;
			eSym._Org = null;
			eSym._Lface = null;
			eSym._winding = 0;
			eSym._activeRegion = null;
			return e;
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x000281FC File Offset: 0x000263FC
		public static void Splice(MeshUtils.Edge a, MeshUtils.Edge b)
		{
			MeshUtils.Edge onext = a._Onext;
			MeshUtils.Edge onext2 = b._Onext;
			onext._Sym._Lnext = b;
			onext2._Sym._Lnext = a;
			a._Onext = onext2;
			b._Onext = onext;
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00028240 File Offset: 0x00026440
		public static void MakeVertex(MeshUtils.Edge eOrig, MeshUtils.Vertex vNext)
		{
			MeshUtils.Vertex vertex = MeshUtils.Pooled<MeshUtils.Vertex>.Create();
			MeshUtils.Vertex prev = vNext._prev;
			vertex._prev = prev;
			prev._next = vertex;
			vertex._next = vNext;
			vNext._prev = vertex;
			vertex._anEdge = eOrig;
			MeshUtils.Edge edge = eOrig;
			do
			{
				edge._Org = vertex;
				edge = edge._Onext;
			}
			while (edge != eOrig);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00028294 File Offset: 0x00026494
		public static void MakeFace(MeshUtils.Edge eOrig, MeshUtils.Face fNext)
		{
			MeshUtils.Face face = MeshUtils.Pooled<MeshUtils.Face>.Create();
			MeshUtils.Face prev = fNext._prev;
			face._prev = prev;
			prev._next = face;
			face._next = fNext;
			fNext._prev = face;
			face._anEdge = eOrig;
			face._trail = null;
			face._marked = false;
			face._inside = fNext._inside;
			MeshUtils.Edge edge = eOrig;
			do
			{
				edge._Lface = face;
				edge = edge._Lnext;
			}
			while (edge != eOrig);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00028300 File Offset: 0x00026500
		public static void KillEdge(MeshUtils.Edge eDel)
		{
			MeshUtils.Edge.EnsureFirst(ref eDel);
			MeshUtils.Edge next = eDel._next;
			MeshUtils.Edge next2 = eDel._Sym._next;
			next._Sym._next = next2;
			next2._Sym._next = next;
			eDel.Free();
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00028348 File Offset: 0x00026548
		public static void KillVertex(MeshUtils.Vertex vDel, MeshUtils.Vertex newOrg)
		{
			MeshUtils.Edge anEdge = vDel._anEdge;
			MeshUtils.Edge edge = anEdge;
			do
			{
				edge._Org = newOrg;
				edge = edge._Onext;
			}
			while (edge != anEdge);
			MeshUtils.Vertex prev = vDel._prev;
			MeshUtils.Vertex next = vDel._next;
			next._prev = prev;
			prev._next = next;
			vDel.Free();
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00028394 File Offset: 0x00026594
		public static void KillFace(MeshUtils.Face fDel, MeshUtils.Face newLFace)
		{
			MeshUtils.Edge anEdge = fDel._anEdge;
			MeshUtils.Edge edge = anEdge;
			do
			{
				edge._Lface = newLFace;
				edge = edge._Lnext;
			}
			while (edge != anEdge);
			MeshUtils.Face prev = fDel._prev;
			MeshUtils.Face next = fDel._next;
			next._prev = prev;
			prev._next = next;
			fDel.Free();
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x000283E0 File Offset: 0x000265E0
		public static float FaceArea(MeshUtils.Face f)
		{
			float num = 0f;
			MeshUtils.Edge edge = f._anEdge;
			do
			{
				num += (edge._Org._s - edge._Dst._s) * (edge._Org._t + edge._Dst._t);
				edge = edge._Lnext;
			}
			while (edge != f._anEdge);
			return num;
		}

		// Token: 0x040006AD RID: 1709
		public const int Undef = -1;

		// Token: 0x02000193 RID: 403
		public abstract class Pooled<T> where T : MeshUtils.Pooled<T>, new()
		{
			// Token: 0x060009FE RID: 2558
			public abstract void Reset();

			// Token: 0x060009FF RID: 2559 RVA: 0x00041E4B File Offset: 0x0004004B
			public virtual void OnFree()
			{
			}

			// Token: 0x06000A00 RID: 2560 RVA: 0x00041E4D File Offset: 0x0004004D
			public static T Create()
			{
				if (MeshUtils.Pooled<T>._stack != null && MeshUtils.Pooled<T>._stack.Count > 0)
				{
					return MeshUtils.Pooled<T>._stack.Pop();
				}
				return new T();
			}

			// Token: 0x06000A01 RID: 2561 RVA: 0x00041E73 File Offset: 0x00040073
			public void Free()
			{
				this.OnFree();
				this.Reset();
				if (MeshUtils.Pooled<T>._stack == null)
				{
					MeshUtils.Pooled<T>._stack = new Stack<T>();
				}
				MeshUtils.Pooled<T>._stack.Push((T)((object)this));
			}

			// Token: 0x04000A10 RID: 2576
			private static Stack<T> _stack;
		}

		// Token: 0x02000194 RID: 404
		public class Vertex : MeshUtils.Pooled<MeshUtils.Vertex>
		{
			// Token: 0x06000A03 RID: 2563 RVA: 0x00041EAC File Offset: 0x000400AC
			public override void Reset()
			{
				this._prev = (this._next = null);
				this._anEdge = null;
				this._coords = Vec3.Zero;
				this._s = 0f;
				this._t = 0f;
				this._pqHandle = default(PQHandle);
				this._n = 0;
				this._data = null;
			}

			// Token: 0x04000A11 RID: 2577
			internal MeshUtils.Vertex _prev;

			// Token: 0x04000A12 RID: 2578
			internal MeshUtils.Vertex _next;

			// Token: 0x04000A13 RID: 2579
			internal MeshUtils.Edge _anEdge;

			// Token: 0x04000A14 RID: 2580
			internal Vec3 _coords;

			// Token: 0x04000A15 RID: 2581
			internal float _s;

			// Token: 0x04000A16 RID: 2582
			internal float _t;

			// Token: 0x04000A17 RID: 2583
			internal PQHandle _pqHandle;

			// Token: 0x04000A18 RID: 2584
			internal int _n;

			// Token: 0x04000A19 RID: 2585
			internal object _data;
		}

		// Token: 0x02000195 RID: 405
		public class Face : MeshUtils.Pooled<MeshUtils.Face>
		{
			// Token: 0x17000235 RID: 565
			// (get) Token: 0x06000A05 RID: 2565 RVA: 0x00041F14 File Offset: 0x00040114
			internal int VertsCount
			{
				get
				{
					int num = 0;
					MeshUtils.Edge edge = this._anEdge;
					do
					{
						num++;
						edge = edge._Lnext;
					}
					while (edge != this._anEdge);
					return num;
				}
			}

			// Token: 0x06000A06 RID: 2566 RVA: 0x00041F40 File Offset: 0x00040140
			public override void Reset()
			{
				this._prev = (this._next = null);
				this._anEdge = null;
				this._trail = null;
				this._n = 0;
				this._marked = false;
				this._inside = false;
			}

			// Token: 0x04000A1A RID: 2586
			internal MeshUtils.Face _prev;

			// Token: 0x04000A1B RID: 2587
			internal MeshUtils.Face _next;

			// Token: 0x04000A1C RID: 2588
			internal MeshUtils.Edge _anEdge;

			// Token: 0x04000A1D RID: 2589
			internal MeshUtils.Face _trail;

			// Token: 0x04000A1E RID: 2590
			internal int _n;

			// Token: 0x04000A1F RID: 2591
			internal bool _marked;

			// Token: 0x04000A20 RID: 2592
			internal bool _inside;
		}

		// Token: 0x02000196 RID: 406
		public struct EdgePair
		{
			// Token: 0x06000A08 RID: 2568 RVA: 0x00041F88 File Offset: 0x00040188
			public static MeshUtils.EdgePair Create()
			{
				MeshUtils.EdgePair edgePair = default(MeshUtils.EdgePair);
				edgePair._e = MeshUtils.Pooled<MeshUtils.Edge>.Create();
				edgePair._e._pair = edgePair;
				edgePair._eSym = MeshUtils.Pooled<MeshUtils.Edge>.Create();
				edgePair._eSym._pair = edgePair;
				return edgePair;
			}

			// Token: 0x06000A09 RID: 2569 RVA: 0x00041FD0 File Offset: 0x000401D0
			public void Reset()
			{
				this._e = (this._eSym = null);
			}

			// Token: 0x04000A21 RID: 2593
			internal MeshUtils.Edge _e;

			// Token: 0x04000A22 RID: 2594
			internal MeshUtils.Edge _eSym;
		}

		// Token: 0x02000197 RID: 407
		public class Edge : MeshUtils.Pooled<MeshUtils.Edge>
		{
			// Token: 0x17000236 RID: 566
			// (get) Token: 0x06000A0A RID: 2570 RVA: 0x00041FED File Offset: 0x000401ED
			// (set) Token: 0x06000A0B RID: 2571 RVA: 0x00041FFA File Offset: 0x000401FA
			internal MeshUtils.Face _Rface
			{
				get
				{
					return this._Sym._Lface;
				}
				set
				{
					this._Sym._Lface = value;
				}
			}

			// Token: 0x17000237 RID: 567
			// (get) Token: 0x06000A0C RID: 2572 RVA: 0x00042008 File Offset: 0x00040208
			// (set) Token: 0x06000A0D RID: 2573 RVA: 0x00042015 File Offset: 0x00040215
			internal MeshUtils.Vertex _Dst
			{
				get
				{
					return this._Sym._Org;
				}
				set
				{
					this._Sym._Org = value;
				}
			}

			// Token: 0x17000238 RID: 568
			// (get) Token: 0x06000A0E RID: 2574 RVA: 0x00042023 File Offset: 0x00040223
			// (set) Token: 0x06000A0F RID: 2575 RVA: 0x00042030 File Offset: 0x00040230
			internal MeshUtils.Edge _Oprev
			{
				get
				{
					return this._Sym._Lnext;
				}
				set
				{
					this._Sym._Lnext = value;
				}
			}

			// Token: 0x17000239 RID: 569
			// (get) Token: 0x06000A10 RID: 2576 RVA: 0x0004203E File Offset: 0x0004023E
			// (set) Token: 0x06000A11 RID: 2577 RVA: 0x0004204B File Offset: 0x0004024B
			internal MeshUtils.Edge _Lprev
			{
				get
				{
					return this._Onext._Sym;
				}
				set
				{
					this._Onext._Sym = value;
				}
			}

			// Token: 0x1700023A RID: 570
			// (get) Token: 0x06000A12 RID: 2578 RVA: 0x00042059 File Offset: 0x00040259
			// (set) Token: 0x06000A13 RID: 2579 RVA: 0x00042066 File Offset: 0x00040266
			internal MeshUtils.Edge _Dprev
			{
				get
				{
					return this._Lnext._Sym;
				}
				set
				{
					this._Lnext._Sym = value;
				}
			}

			// Token: 0x1700023B RID: 571
			// (get) Token: 0x06000A14 RID: 2580 RVA: 0x00042074 File Offset: 0x00040274
			// (set) Token: 0x06000A15 RID: 2581 RVA: 0x00042081 File Offset: 0x00040281
			internal MeshUtils.Edge _Rprev
			{
				get
				{
					return this._Sym._Onext;
				}
				set
				{
					this._Sym._Onext = value;
				}
			}

			// Token: 0x1700023C RID: 572
			// (get) Token: 0x06000A16 RID: 2582 RVA: 0x0004208F File Offset: 0x0004028F
			// (set) Token: 0x06000A17 RID: 2583 RVA: 0x0004209C File Offset: 0x0004029C
			internal MeshUtils.Edge _Dnext
			{
				get
				{
					return this._Rprev._Sym;
				}
				set
				{
					this._Rprev._Sym = value;
				}
			}

			// Token: 0x1700023D RID: 573
			// (get) Token: 0x06000A18 RID: 2584 RVA: 0x000420AA File Offset: 0x000402AA
			// (set) Token: 0x06000A19 RID: 2585 RVA: 0x000420B7 File Offset: 0x000402B7
			internal MeshUtils.Edge _Rnext
			{
				get
				{
					return this._Oprev._Sym;
				}
				set
				{
					this._Oprev._Sym = value;
				}
			}

			// Token: 0x06000A1A RID: 2586 RVA: 0x000420C5 File Offset: 0x000402C5
			internal static void EnsureFirst(ref MeshUtils.Edge e)
			{
				if (e == e._pair._eSym)
				{
					e = e._Sym;
				}
			}

			// Token: 0x06000A1B RID: 2587 RVA: 0x000420E0 File Offset: 0x000402E0
			public override void Reset()
			{
				this._pair.Reset();
				this._next = (this._Sym = (this._Onext = (this._Lnext = null)));
				this._Org = null;
				this._Lface = null;
				this._activeRegion = null;
				this._winding = 0;
			}

			// Token: 0x04000A23 RID: 2595
			internal MeshUtils.EdgePair _pair;

			// Token: 0x04000A24 RID: 2596
			internal MeshUtils.Edge _next;

			// Token: 0x04000A25 RID: 2597
			internal MeshUtils.Edge _Sym;

			// Token: 0x04000A26 RID: 2598
			internal MeshUtils.Edge _Onext;

			// Token: 0x04000A27 RID: 2599
			internal MeshUtils.Edge _Lnext;

			// Token: 0x04000A28 RID: 2600
			internal MeshUtils.Vertex _Org;

			// Token: 0x04000A29 RID: 2601
			internal MeshUtils.Face _Lface;

			// Token: 0x04000A2A RID: 2602
			internal Tess.ActiveRegion _activeRegion;

			// Token: 0x04000A2B RID: 2603
			internal int _winding;
		}
	}
}
