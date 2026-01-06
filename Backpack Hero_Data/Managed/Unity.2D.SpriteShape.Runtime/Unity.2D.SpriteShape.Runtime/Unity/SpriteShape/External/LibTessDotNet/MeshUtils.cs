using System;
using System.Collections.Generic;

namespace Unity.SpriteShape.External.LibTessDotNet
{
	// Token: 0x02000008 RID: 8
	internal static class MeshUtils
	{
		// Token: 0x06000037 RID: 55 RVA: 0x000033A4 File Offset: 0x000015A4
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

		// Token: 0x06000038 RID: 56 RVA: 0x00003460 File Offset: 0x00001660
		public static void Splice(MeshUtils.Edge a, MeshUtils.Edge b)
		{
			MeshUtils.Edge onext = a._Onext;
			MeshUtils.Edge onext2 = b._Onext;
			onext._Sym._Lnext = b;
			onext2._Sym._Lnext = a;
			a._Onext = onext2;
			b._Onext = onext;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000034A4 File Offset: 0x000016A4
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

		// Token: 0x0600003A RID: 58 RVA: 0x000034F8 File Offset: 0x000016F8
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

		// Token: 0x0600003B RID: 59 RVA: 0x00003564 File Offset: 0x00001764
		public static void KillEdge(MeshUtils.Edge eDel)
		{
			MeshUtils.Edge.EnsureFirst(ref eDel);
			MeshUtils.Edge next = eDel._next;
			MeshUtils.Edge next2 = eDel._Sym._next;
			next._Sym._next = next2;
			next2._Sym._next = next;
			eDel.Free();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000035AC File Offset: 0x000017AC
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

		// Token: 0x0600003D RID: 61 RVA: 0x000035F8 File Offset: 0x000017F8
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

		// Token: 0x0600003E RID: 62 RVA: 0x00003644 File Offset: 0x00001844
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

		// Token: 0x0400001C RID: 28
		public const int Undef = -1;

		// Token: 0x02000023 RID: 35
		public abstract class Pooled<T> where T : MeshUtils.Pooled<T>, new()
		{
			// Token: 0x06000181 RID: 385
			public abstract void Reset();

			// Token: 0x06000182 RID: 386 RVA: 0x0000E8B6 File Offset: 0x0000CAB6
			public virtual void OnFree()
			{
			}

			// Token: 0x06000183 RID: 387 RVA: 0x0000E8B8 File Offset: 0x0000CAB8
			public static T Create()
			{
				if (MeshUtils.Pooled<T>._stack != null && MeshUtils.Pooled<T>._stack.Count > 0)
				{
					return MeshUtils.Pooled<T>._stack.Pop();
				}
				return new T();
			}

			// Token: 0x06000184 RID: 388 RVA: 0x0000E8DE File Offset: 0x0000CADE
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

			// Token: 0x040000EF RID: 239
			private static Stack<T> _stack;
		}

		// Token: 0x02000024 RID: 36
		public class Vertex : MeshUtils.Pooled<MeshUtils.Vertex>
		{
			// Token: 0x06000186 RID: 390 RVA: 0x0000E918 File Offset: 0x0000CB18
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

			// Token: 0x040000F0 RID: 240
			internal MeshUtils.Vertex _prev;

			// Token: 0x040000F1 RID: 241
			internal MeshUtils.Vertex _next;

			// Token: 0x040000F2 RID: 242
			internal MeshUtils.Edge _anEdge;

			// Token: 0x040000F3 RID: 243
			internal Vec3 _coords;

			// Token: 0x040000F4 RID: 244
			internal float _s;

			// Token: 0x040000F5 RID: 245
			internal float _t;

			// Token: 0x040000F6 RID: 246
			internal PQHandle _pqHandle;

			// Token: 0x040000F7 RID: 247
			internal int _n;

			// Token: 0x040000F8 RID: 248
			internal object _data;
		}

		// Token: 0x02000025 RID: 37
		public class Face : MeshUtils.Pooled<MeshUtils.Face>
		{
			// Token: 0x17000048 RID: 72
			// (get) Token: 0x06000188 RID: 392 RVA: 0x0000E980 File Offset: 0x0000CB80
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

			// Token: 0x06000189 RID: 393 RVA: 0x0000E9AC File Offset: 0x0000CBAC
			public override void Reset()
			{
				this._prev = (this._next = null);
				this._anEdge = null;
				this._trail = null;
				this._n = 0;
				this._marked = false;
				this._inside = false;
			}

			// Token: 0x040000F9 RID: 249
			internal MeshUtils.Face _prev;

			// Token: 0x040000FA RID: 250
			internal MeshUtils.Face _next;

			// Token: 0x040000FB RID: 251
			internal MeshUtils.Edge _anEdge;

			// Token: 0x040000FC RID: 252
			internal MeshUtils.Face _trail;

			// Token: 0x040000FD RID: 253
			internal int _n;

			// Token: 0x040000FE RID: 254
			internal bool _marked;

			// Token: 0x040000FF RID: 255
			internal bool _inside;
		}

		// Token: 0x02000026 RID: 38
		public struct EdgePair
		{
			// Token: 0x0600018B RID: 395 RVA: 0x0000E9F4 File Offset: 0x0000CBF4
			public static MeshUtils.EdgePair Create()
			{
				MeshUtils.EdgePair edgePair = default(MeshUtils.EdgePair);
				edgePair._e = MeshUtils.Pooled<MeshUtils.Edge>.Create();
				edgePair._e._pair = edgePair;
				edgePair._eSym = MeshUtils.Pooled<MeshUtils.Edge>.Create();
				edgePair._eSym._pair = edgePair;
				return edgePair;
			}

			// Token: 0x0600018C RID: 396 RVA: 0x0000EA3C File Offset: 0x0000CC3C
			public void Reset()
			{
				this._e = (this._eSym = null);
			}

			// Token: 0x04000100 RID: 256
			internal MeshUtils.Edge _e;

			// Token: 0x04000101 RID: 257
			internal MeshUtils.Edge _eSym;
		}

		// Token: 0x02000027 RID: 39
		public class Edge : MeshUtils.Pooled<MeshUtils.Edge>
		{
			// Token: 0x17000049 RID: 73
			// (get) Token: 0x0600018D RID: 397 RVA: 0x0000EA59 File Offset: 0x0000CC59
			// (set) Token: 0x0600018E RID: 398 RVA: 0x0000EA66 File Offset: 0x0000CC66
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

			// Token: 0x1700004A RID: 74
			// (get) Token: 0x0600018F RID: 399 RVA: 0x0000EA74 File Offset: 0x0000CC74
			// (set) Token: 0x06000190 RID: 400 RVA: 0x0000EA81 File Offset: 0x0000CC81
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

			// Token: 0x1700004B RID: 75
			// (get) Token: 0x06000191 RID: 401 RVA: 0x0000EA8F File Offset: 0x0000CC8F
			// (set) Token: 0x06000192 RID: 402 RVA: 0x0000EA9C File Offset: 0x0000CC9C
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

			// Token: 0x1700004C RID: 76
			// (get) Token: 0x06000193 RID: 403 RVA: 0x0000EAAA File Offset: 0x0000CCAA
			// (set) Token: 0x06000194 RID: 404 RVA: 0x0000EAB7 File Offset: 0x0000CCB7
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

			// Token: 0x1700004D RID: 77
			// (get) Token: 0x06000195 RID: 405 RVA: 0x0000EAC5 File Offset: 0x0000CCC5
			// (set) Token: 0x06000196 RID: 406 RVA: 0x0000EAD2 File Offset: 0x0000CCD2
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

			// Token: 0x1700004E RID: 78
			// (get) Token: 0x06000197 RID: 407 RVA: 0x0000EAE0 File Offset: 0x0000CCE0
			// (set) Token: 0x06000198 RID: 408 RVA: 0x0000EAED File Offset: 0x0000CCED
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

			// Token: 0x1700004F RID: 79
			// (get) Token: 0x06000199 RID: 409 RVA: 0x0000EAFB File Offset: 0x0000CCFB
			// (set) Token: 0x0600019A RID: 410 RVA: 0x0000EB08 File Offset: 0x0000CD08
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

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x0600019B RID: 411 RVA: 0x0000EB16 File Offset: 0x0000CD16
			// (set) Token: 0x0600019C RID: 412 RVA: 0x0000EB23 File Offset: 0x0000CD23
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

			// Token: 0x0600019D RID: 413 RVA: 0x0000EB31 File Offset: 0x0000CD31
			internal static void EnsureFirst(ref MeshUtils.Edge e)
			{
				if (e == e._pair._eSym)
				{
					e = e._Sym;
				}
			}

			// Token: 0x0600019E RID: 414 RVA: 0x0000EB4C File Offset: 0x0000CD4C
			public override void Reset()
			{
				this._pair.Reset();
				this._next = (this._Sym = (this._Onext = (this._Lnext = null)));
				this._Org = null;
				this._Lface = null;
				this._activeRegion = null;
				this._winding = 0;
			}

			// Token: 0x04000102 RID: 258
			internal MeshUtils.EdgePair _pair;

			// Token: 0x04000103 RID: 259
			internal MeshUtils.Edge _next;

			// Token: 0x04000104 RID: 260
			internal MeshUtils.Edge _Sym;

			// Token: 0x04000105 RID: 261
			internal MeshUtils.Edge _Onext;

			// Token: 0x04000106 RID: 262
			internal MeshUtils.Edge _Lnext;

			// Token: 0x04000107 RID: 263
			internal MeshUtils.Vertex _Org;

			// Token: 0x04000108 RID: 264
			internal MeshUtils.Face _Lface;

			// Token: 0x04000109 RID: 265
			internal Tess.ActiveRegion _activeRegion;

			// Token: 0x0400010A RID: 266
			internal int _winding;
		}
	}
}
