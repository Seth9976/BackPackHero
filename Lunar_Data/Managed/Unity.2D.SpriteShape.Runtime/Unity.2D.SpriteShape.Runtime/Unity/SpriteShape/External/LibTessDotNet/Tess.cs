using System;

namespace Unity.SpriteShape.External.LibTessDotNet
{
	// Token: 0x0200000C RID: 12
	internal class Tess
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00004015 File Offset: 0x00002215
		private Tess.ActiveRegion RegionBelow(Tess.ActiveRegion reg)
		{
			return reg._nodeUp._prev._key;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00004027 File Offset: 0x00002227
		private Tess.ActiveRegion RegionAbove(Tess.ActiveRegion reg)
		{
			return reg._nodeUp._next._key;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000403C File Offset: 0x0000223C
		private bool EdgeLeq(Tess.ActiveRegion reg1, Tess.ActiveRegion reg2)
		{
			MeshUtils.Edge eUp = reg1._eUp;
			MeshUtils.Edge eUp2 = reg2._eUp;
			if (eUp._Dst == this._event)
			{
				if (eUp2._Dst != this._event)
				{
					return Geom.EdgeSign(eUp2._Dst, this._event, eUp2._Org) <= 0f;
				}
				if (Geom.VertLeq(eUp._Org, eUp2._Org))
				{
					return Geom.EdgeSign(eUp2._Dst, eUp._Org, eUp2._Org) <= 0f;
				}
				return Geom.EdgeSign(eUp._Dst, eUp2._Org, eUp._Org) >= 0f;
			}
			else
			{
				if (eUp2._Dst == this._event)
				{
					return Geom.EdgeSign(eUp._Dst, this._event, eUp._Org) >= 0f;
				}
				float num = Geom.EdgeEval(eUp._Dst, this._event, eUp._Org);
				float num2 = Geom.EdgeEval(eUp2._Dst, this._event, eUp2._Org);
				return num >= num2;
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00004154 File Offset: 0x00002354
		private void DeleteRegion(Tess.ActiveRegion reg)
		{
			bool fixUpperEdge = reg._fixUpperEdge;
			reg._eUp._activeRegion = null;
			this._dict.Remove(reg._nodeUp);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000417A File Offset: 0x0000237A
		private void FixUpperEdge(Tess.ActiveRegion reg, MeshUtils.Edge newEdge)
		{
			this._mesh.Delete(reg._eUp);
			reg._fixUpperEdge = false;
			reg._eUp = newEdge;
			newEdge._activeRegion = reg;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000041A4 File Offset: 0x000023A4
		private Tess.ActiveRegion TopLeftRegion(Tess.ActiveRegion reg)
		{
			MeshUtils.Vertex org = reg._eUp._Org;
			do
			{
				reg = this.RegionAbove(reg);
			}
			while (reg._eUp._Org == org);
			if (reg._fixUpperEdge)
			{
				MeshUtils.Edge edge = this._mesh.Connect(this.RegionBelow(reg)._eUp._Sym, reg._eUp._Lnext);
				this.FixUpperEdge(reg, edge);
				reg = this.RegionAbove(reg);
			}
			return reg;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004218 File Offset: 0x00002418
		private Tess.ActiveRegion TopRightRegion(Tess.ActiveRegion reg)
		{
			MeshUtils.Vertex dst = reg._eUp._Dst;
			do
			{
				reg = this.RegionAbove(reg);
			}
			while (reg._eUp._Dst == dst);
			return reg;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000424C File Offset: 0x0000244C
		private Tess.ActiveRegion AddRegionBelow(Tess.ActiveRegion regAbove, MeshUtils.Edge eNewUp)
		{
			Tess.ActiveRegion activeRegion = new Tess.ActiveRegion();
			activeRegion._eUp = eNewUp;
			activeRegion._nodeUp = this._dict.InsertBefore(regAbove._nodeUp, activeRegion);
			activeRegion._fixUpperEdge = false;
			activeRegion._sentinel = false;
			activeRegion._dirty = false;
			eNewUp._activeRegion = activeRegion;
			return activeRegion;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000429B File Offset: 0x0000249B
		private void ComputeWinding(Tess.ActiveRegion reg)
		{
			reg._windingNumber = this.RegionAbove(reg)._windingNumber + reg._eUp._winding;
			reg._inside = Geom.IsWindingInside(this._windingRule, reg._windingNumber);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000042D4 File Offset: 0x000024D4
		private void FinishRegion(Tess.ActiveRegion reg)
		{
			MeshUtils.Edge eUp = reg._eUp;
			MeshUtils.Face lface = eUp._Lface;
			lface._inside = reg._inside;
			lface._anEdge = eUp;
			this.DeleteRegion(reg);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004308 File Offset: 0x00002508
		private MeshUtils.Edge FinishLeftRegions(Tess.ActiveRegion regFirst, Tess.ActiveRegion regLast)
		{
			Tess.ActiveRegion activeRegion = regFirst;
			MeshUtils.Edge edge = regFirst._eUp;
			while (activeRegion != regLast)
			{
				activeRegion._fixUpperEdge = false;
				Tess.ActiveRegion activeRegion2 = this.RegionBelow(activeRegion);
				MeshUtils.Edge edge2 = activeRegion2._eUp;
				if (edge2._Org != edge._Org)
				{
					if (!activeRegion2._fixUpperEdge)
					{
						this.FinishRegion(activeRegion);
						break;
					}
					edge2 = this._mesh.Connect(edge._Lprev, edge2._Sym);
					this.FixUpperEdge(activeRegion2, edge2);
				}
				if (edge._Onext != edge2)
				{
					this._mesh.Splice(edge2._Oprev, edge2);
					this._mesh.Splice(edge, edge2);
				}
				this.FinishRegion(activeRegion);
				edge = activeRegion2._eUp;
				activeRegion = activeRegion2;
			}
			return edge;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000043B8 File Offset: 0x000025B8
		private void AddRightEdges(Tess.ActiveRegion regUp, MeshUtils.Edge eFirst, MeshUtils.Edge eLast, MeshUtils.Edge eTopLeft, bool cleanUp)
		{
			bool flag = true;
			MeshUtils.Edge edge = eFirst;
			do
			{
				this.AddRegionBelow(regUp, edge._Sym);
				edge = edge._Onext;
			}
			while (edge != eLast);
			if (eTopLeft == null)
			{
				eTopLeft = this.RegionBelow(regUp)._eUp._Rprev;
			}
			Tess.ActiveRegion activeRegion = regUp;
			MeshUtils.Edge edge2 = eTopLeft;
			for (;;)
			{
				Tess.ActiveRegion activeRegion2 = this.RegionBelow(activeRegion);
				edge = activeRegion2._eUp._Sym;
				if (edge._Org != edge2._Org)
				{
					break;
				}
				if (edge._Onext != edge2)
				{
					this._mesh.Splice(edge._Oprev, edge);
					this._mesh.Splice(edge2._Oprev, edge);
				}
				activeRegion2._windingNumber = activeRegion._windingNumber - edge._winding;
				activeRegion2._inside = Geom.IsWindingInside(this._windingRule, activeRegion2._windingNumber);
				activeRegion._dirty = true;
				if (!flag && this.CheckForRightSplice(activeRegion))
				{
					Geom.AddWinding(edge, edge2);
					this.DeleteRegion(activeRegion);
					this._mesh.Delete(edge2);
				}
				flag = false;
				activeRegion = activeRegion2;
				edge2 = edge;
			}
			activeRegion._dirty = true;
			if (cleanUp)
			{
				this.WalkDirtyRegions(activeRegion);
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000044CB File Offset: 0x000026CB
		private void SpliceMergeVertices(MeshUtils.Edge e1, MeshUtils.Edge e2)
		{
			this._mesh.Splice(e1, e2);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000044DC File Offset: 0x000026DC
		private void VertexWeights(MeshUtils.Vertex isect, MeshUtils.Vertex org, MeshUtils.Vertex dst, out float w0, out float w1)
		{
			float num = Geom.VertL1dist(org, isect);
			float num2 = Geom.VertL1dist(dst, isect);
			w0 = num2 / (num + num2) / 2f;
			w1 = num / (num + num2) / 2f;
			isect._coords.X = isect._coords.X + (w0 * org._coords.X + w1 * dst._coords.X);
			isect._coords.Y = isect._coords.Y + (w0 * org._coords.Y + w1 * dst._coords.Y);
			isect._coords.Z = isect._coords.Z + (w0 * org._coords.Z + w1 * dst._coords.Z);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000045A0 File Offset: 0x000027A0
		private void GetIntersectData(MeshUtils.Vertex isect, MeshUtils.Vertex orgUp, MeshUtils.Vertex dstUp, MeshUtils.Vertex orgLo, MeshUtils.Vertex dstLo)
		{
			isect._coords = Vec3.Zero;
			float num;
			float num2;
			this.VertexWeights(isect, orgUp, dstUp, out num, out num2);
			float num3;
			float num4;
			this.VertexWeights(isect, orgLo, dstLo, out num3, out num4);
			if (this._combineCallback != null)
			{
				isect._data = this._combineCallback(isect._coords, new object[] { orgUp._data, dstUp._data, orgLo._data, dstLo._data }, new float[] { num, num2, num3, num4 });
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004638 File Offset: 0x00002838
		private bool CheckForRightSplice(Tess.ActiveRegion regUp)
		{
			Tess.ActiveRegion activeRegion = this.RegionBelow(regUp);
			MeshUtils.Edge eUp = regUp._eUp;
			MeshUtils.Edge eUp2 = activeRegion._eUp;
			if (Geom.VertLeq(eUp._Org, eUp2._Org))
			{
				if (Geom.EdgeSign(eUp2._Dst, eUp._Org, eUp2._Org) > 0f)
				{
					return false;
				}
				if (!Geom.VertEq(eUp._Org, eUp2._Org))
				{
					this._mesh.SplitEdge(eUp2._Sym);
					this._mesh.Splice(eUp, eUp2._Oprev);
					regUp._dirty = (activeRegion._dirty = true);
				}
				else if (eUp._Org != eUp2._Org)
				{
					this._pq.Remove(eUp._Org._pqHandle);
					this.SpliceMergeVertices(eUp2._Oprev, eUp);
				}
			}
			else
			{
				if (Geom.EdgeSign(eUp._Dst, eUp2._Org, eUp._Org) < 0f)
				{
					return false;
				}
				this.RegionAbove(regUp)._dirty = (regUp._dirty = true);
				this._mesh.SplitEdge(eUp._Sym);
				this._mesh.Splice(eUp2._Oprev, eUp);
			}
			return true;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000476C File Offset: 0x0000296C
		private bool CheckForLeftSplice(Tess.ActiveRegion regUp)
		{
			Tess.ActiveRegion activeRegion = this.RegionBelow(regUp);
			MeshUtils.Edge eUp = regUp._eUp;
			MeshUtils.Edge eUp2 = activeRegion._eUp;
			if (Geom.VertLeq(eUp._Dst, eUp2._Dst))
			{
				if (Geom.EdgeSign(eUp._Dst, eUp2._Dst, eUp._Org) < 0f)
				{
					return false;
				}
				this.RegionAbove(regUp)._dirty = (regUp._dirty = true);
				MeshUtils.Edge edge = this._mesh.SplitEdge(eUp);
				this._mesh.Splice(eUp2._Sym, edge);
				edge._Lface._inside = regUp._inside;
			}
			else
			{
				if (Geom.EdgeSign(eUp2._Dst, eUp._Dst, eUp2._Org) > 0f)
				{
					return false;
				}
				regUp._dirty = (activeRegion._dirty = true);
				MeshUtils.Edge edge2 = this._mesh.SplitEdge(eUp2);
				this._mesh.Splice(eUp._Lnext, eUp2._Sym);
				edge2._Rface._inside = regUp._inside;
			}
			return true;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004874 File Offset: 0x00002A74
		private bool CheckForIntersect(Tess.ActiveRegion regUp)
		{
			Tess.ActiveRegion activeRegion = this.RegionBelow(regUp);
			MeshUtils.Edge edge = regUp._eUp;
			MeshUtils.Edge edge2 = activeRegion._eUp;
			MeshUtils.Vertex org = edge._Org;
			MeshUtils.Vertex org2 = edge2._Org;
			MeshUtils.Vertex dst = edge._Dst;
			MeshUtils.Vertex dst2 = edge2._Dst;
			if (org == org2)
			{
				return false;
			}
			float num = Math.Min(org._t, dst._t);
			float num2 = Math.Max(org2._t, dst2._t);
			if (num > num2)
			{
				return false;
			}
			if (Geom.VertLeq(org, org2))
			{
				if (Geom.EdgeSign(dst2, org, org2) > 0f)
				{
					return false;
				}
			}
			else if (Geom.EdgeSign(dst, org2, org) < 0f)
			{
				return false;
			}
			MeshUtils.Vertex vertex = MeshUtils.Pooled<MeshUtils.Vertex>.Create();
			Geom.EdgeIntersect(dst, org, dst2, org2, vertex);
			if (Geom.VertLeq(vertex, this._event))
			{
				vertex._s = this._event._s;
				vertex._t = this._event._t;
			}
			MeshUtils.Vertex vertex2 = (Geom.VertLeq(org, org2) ? org : org2);
			if (Geom.VertLeq(vertex2, vertex))
			{
				vertex._s = vertex2._s;
				vertex._t = vertex2._t;
			}
			if (Geom.VertEq(vertex, org) || Geom.VertEq(vertex, org2))
			{
				this.CheckForRightSplice(regUp);
				return false;
			}
			if ((!Geom.VertEq(dst, this._event) && Geom.EdgeSign(dst, this._event, vertex) >= 0f) || (!Geom.VertEq(dst2, this._event) && Geom.EdgeSign(dst2, this._event, vertex) <= 0f))
			{
				if (dst2 == this._event)
				{
					this._mesh.SplitEdge(edge._Sym);
					this._mesh.Splice(edge2._Sym, edge);
					regUp = this.TopLeftRegion(regUp);
					edge = this.RegionBelow(regUp)._eUp;
					this.FinishLeftRegions(this.RegionBelow(regUp), activeRegion);
					this.AddRightEdges(regUp, edge._Oprev, edge, edge, true);
					return true;
				}
				if (dst == this._event)
				{
					this._mesh.SplitEdge(edge2._Sym);
					this._mesh.Splice(edge._Lnext, edge2._Oprev);
					activeRegion = regUp;
					regUp = this.TopRightRegion(regUp);
					MeshUtils.Edge rprev = this.RegionBelow(regUp)._eUp._Rprev;
					activeRegion._eUp = edge2._Oprev;
					edge2 = this.FinishLeftRegions(activeRegion, null);
					this.AddRightEdges(regUp, edge2._Onext, edge._Rprev, rprev, true);
					return true;
				}
				if (Geom.EdgeSign(dst, this._event, vertex) >= 0f)
				{
					this.RegionAbove(regUp)._dirty = (regUp._dirty = true);
					this._mesh.SplitEdge(edge._Sym);
					edge._Org._s = this._event._s;
					edge._Org._t = this._event._t;
				}
				if (Geom.EdgeSign(dst2, this._event, vertex) <= 0f)
				{
					regUp._dirty = (activeRegion._dirty = true);
					this._mesh.SplitEdge(edge2._Sym);
					edge2._Org._s = this._event._s;
					edge2._Org._t = this._event._t;
				}
				return false;
			}
			else
			{
				this._mesh.SplitEdge(edge._Sym);
				this._mesh.SplitEdge(edge2._Sym);
				this._mesh.Splice(edge2._Oprev, edge);
				edge._Org._s = vertex._s;
				edge._Org._t = vertex._t;
				edge._Org._pqHandle = this._pq.Insert(edge._Org);
				if (edge._Org._pqHandle._handle == PQHandle.Invalid)
				{
					throw new InvalidOperationException("PQHandle should not be invalid");
				}
				this.GetIntersectData(edge._Org, org, dst, org2, dst2);
				this.RegionAbove(regUp)._dirty = (regUp._dirty = (activeRegion._dirty = true));
				return false;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004C90 File Offset: 0x00002E90
		private void WalkDirtyRegions(Tess.ActiveRegion regUp)
		{
			Tess.ActiveRegion activeRegion = this.RegionBelow(regUp);
			for (;;)
			{
				if (!activeRegion._dirty)
				{
					if (!regUp._dirty)
					{
						activeRegion = regUp;
						regUp = this.RegionAbove(regUp);
						if (regUp == null || !regUp._dirty)
						{
							break;
						}
					}
					regUp._dirty = false;
					MeshUtils.Edge edge = regUp._eUp;
					MeshUtils.Edge edge2 = activeRegion._eUp;
					if (edge._Dst != edge2._Dst && this.CheckForLeftSplice(regUp))
					{
						if (activeRegion._fixUpperEdge)
						{
							this.DeleteRegion(activeRegion);
							this._mesh.Delete(edge2);
							activeRegion = this.RegionBelow(regUp);
							edge2 = activeRegion._eUp;
						}
						else if (regUp._fixUpperEdge)
						{
							this.DeleteRegion(regUp);
							this._mesh.Delete(edge);
							regUp = this.RegionAbove(activeRegion);
							edge = regUp._eUp;
						}
					}
					if (edge._Org != edge2._Org)
					{
						if (edge._Dst != edge2._Dst && !regUp._fixUpperEdge && !activeRegion._fixUpperEdge && (edge._Dst == this._event || edge2._Dst == this._event))
						{
							if (this.CheckForIntersect(regUp))
							{
								return;
							}
						}
						else
						{
							this.CheckForRightSplice(regUp);
						}
					}
					if (edge._Org == edge2._Org && edge._Dst == edge2._Dst)
					{
						Geom.AddWinding(edge2, edge);
						this.DeleteRegion(regUp);
						this._mesh.Delete(edge);
						regUp = this.RegionAbove(activeRegion);
					}
				}
				else
				{
					regUp = activeRegion;
					activeRegion = this.RegionBelow(activeRegion);
				}
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004E00 File Offset: 0x00003000
		private void ConnectRightVertex(Tess.ActiveRegion regUp, MeshUtils.Edge eBottomLeft)
		{
			MeshUtils.Edge edge = eBottomLeft._Onext;
			Tess.ActiveRegion activeRegion = this.RegionBelow(regUp);
			MeshUtils.Edge eUp = regUp._eUp;
			MeshUtils.Edge eUp2 = activeRegion._eUp;
			bool flag = false;
			if (eUp._Dst != eUp2._Dst)
			{
				this.CheckForIntersect(regUp);
			}
			if (Geom.VertEq(eUp._Org, this._event))
			{
				this._mesh.Splice(edge._Oprev, eUp);
				regUp = this.TopLeftRegion(regUp);
				edge = this.RegionBelow(regUp)._eUp;
				this.FinishLeftRegions(this.RegionBelow(regUp), activeRegion);
				flag = true;
			}
			if (Geom.VertEq(eUp2._Org, this._event))
			{
				this._mesh.Splice(eBottomLeft, eUp2._Oprev);
				eBottomLeft = this.FinishLeftRegions(activeRegion, null);
				flag = true;
			}
			if (flag)
			{
				this.AddRightEdges(regUp, eBottomLeft._Onext, edge, edge, true);
				return;
			}
			MeshUtils.Edge edge2;
			if (Geom.VertLeq(eUp2._Org, eUp._Org))
			{
				edge2 = eUp2._Oprev;
			}
			else
			{
				edge2 = eUp;
			}
			edge2 = this._mesh.Connect(eBottomLeft._Lprev, edge2);
			this.AddRightEdges(regUp, edge2, edge2._Onext, edge2._Onext, false);
			edge2._Sym._activeRegion._fixUpperEdge = true;
			this.WalkDirtyRegions(regUp);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004F40 File Offset: 0x00003140
		private void ConnectLeftDegenerate(Tess.ActiveRegion regUp, MeshUtils.Vertex vEvent)
		{
			MeshUtils.Edge eUp = regUp._eUp;
			if (Geom.VertEq(eUp._Org, vEvent))
			{
				throw new InvalidOperationException("Vertices should have been merged before");
			}
			if (!Geom.VertEq(eUp._Dst, vEvent))
			{
				this._mesh.SplitEdge(eUp._Sym);
				if (regUp._fixUpperEdge)
				{
					this._mesh.Delete(eUp._Onext);
					regUp._fixUpperEdge = false;
				}
				this._mesh.Splice(vEvent._anEdge, eUp);
				this.SweepEvent(vEvent);
				return;
			}
			throw new InvalidOperationException("Vertices should have been merged before");
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004FD4 File Offset: 0x000031D4
		private void ConnectLeftVertex(MeshUtils.Vertex vEvent)
		{
			Tess.ActiveRegion activeRegion = new Tess.ActiveRegion();
			activeRegion._eUp = vEvent._anEdge._Sym;
			Tess.ActiveRegion key = this._dict.Find(activeRegion).Key;
			Tess.ActiveRegion activeRegion2 = this.RegionBelow(key);
			if (activeRegion2 == null)
			{
				return;
			}
			MeshUtils.Edge eUp = key._eUp;
			MeshUtils.Edge eUp2 = activeRegion2._eUp;
			if (Geom.EdgeSign(eUp._Dst, vEvent, eUp._Org) == 0f)
			{
				this.ConnectLeftDegenerate(key, vEvent);
				return;
			}
			Tess.ActiveRegion activeRegion3 = (Geom.VertLeq(eUp2._Dst, eUp._Dst) ? key : activeRegion2);
			if (key._inside || activeRegion3._fixUpperEdge)
			{
				MeshUtils.Edge edge;
				if (activeRegion3 == key)
				{
					edge = this._mesh.Connect(vEvent._anEdge._Sym, eUp._Lnext);
				}
				else
				{
					edge = this._mesh.Connect(eUp2._Dnext, vEvent._anEdge)._Sym;
				}
				if (activeRegion3._fixUpperEdge)
				{
					this.FixUpperEdge(activeRegion3, edge);
				}
				else
				{
					this.ComputeWinding(this.AddRegionBelow(key, edge));
				}
				this.SweepEvent(vEvent);
				return;
			}
			this.AddRightEdges(key, vEvent._anEdge, vEvent._anEdge, null, true);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000050F8 File Offset: 0x000032F8
		private void SweepEvent(MeshUtils.Vertex vEvent)
		{
			this._event = vEvent;
			MeshUtils.Edge edge = vEvent._anEdge;
			while (edge._activeRegion == null)
			{
				edge = edge._Onext;
				if (edge == vEvent._anEdge)
				{
					this.ConnectLeftVertex(vEvent);
					return;
				}
			}
			Tess.ActiveRegion activeRegion = this.TopLeftRegion(edge._activeRegion);
			Tess.ActiveRegion activeRegion2 = this.RegionBelow(activeRegion);
			MeshUtils.Edge eUp = activeRegion2._eUp;
			MeshUtils.Edge edge2 = this.FinishLeftRegions(activeRegion2, null);
			if (edge2._Onext == eUp)
			{
				this.ConnectRightVertex(activeRegion, edge2);
				return;
			}
			this.AddRightEdges(activeRegion, edge2._Onext, eUp, eUp, true);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00005180 File Offset: 0x00003380
		private void AddSentinel(float smin, float smax, float t)
		{
			MeshUtils.Edge edge = this._mesh.MakeEdge();
			edge._Org._s = smax;
			edge._Org._t = t;
			edge._Dst._s = smin;
			edge._Dst._t = t;
			this._event = edge._Dst;
			Tess.ActiveRegion activeRegion = new Tess.ActiveRegion();
			activeRegion._eUp = edge;
			activeRegion._windingNumber = 0;
			activeRegion._inside = false;
			activeRegion._fixUpperEdge = false;
			activeRegion._sentinel = true;
			activeRegion._dirty = false;
			activeRegion._nodeUp = this._dict.Insert(activeRegion);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00005218 File Offset: 0x00003418
		private void InitEdgeDict()
		{
			this._dict = new Dict<Tess.ActiveRegion>(new Dict<Tess.ActiveRegion>.LessOrEqual(this.EdgeLeq));
			this.AddSentinel(-this.SentinelCoord, this.SentinelCoord, -this.SentinelCoord);
			this.AddSentinel(-this.SentinelCoord, this.SentinelCoord, this.SentinelCoord);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00005270 File Offset: 0x00003470
		private void DoneEdgeDict()
		{
			Tess.ActiveRegion key;
			while ((key = this._dict.Min().Key) != null)
			{
				bool sentinel = key._sentinel;
				this.DeleteRegion(key);
			}
			this._dict = null;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000052A8 File Offset: 0x000034A8
		private void RemoveDegenerateEdges()
		{
			MeshUtils.Edge eHead = this._mesh._eHead;
			MeshUtils.Edge edge2;
			for (MeshUtils.Edge edge = eHead._next; edge != eHead; edge = edge2)
			{
				edge2 = edge._next;
				MeshUtils.Edge edge3 = edge._Lnext;
				if (Geom.VertEq(edge._Org, edge._Dst) && edge._Lnext._Lnext != edge)
				{
					this.SpliceMergeVertices(edge3, edge);
					this._mesh.Delete(edge);
					edge = edge3;
					edge3 = edge._Lnext;
				}
				if (edge3._Lnext == edge)
				{
					if (edge3 != edge)
					{
						if (edge3 == edge2 || edge3 == edge2._Sym)
						{
							edge2 = edge2._next;
						}
						this._mesh.Delete(edge3);
					}
					if (edge == edge2 || edge == edge2._Sym)
					{
						edge2 = edge2._next;
					}
					this._mesh.Delete(edge);
				}
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00005370 File Offset: 0x00003570
		private void InitPriorityQ()
		{
			MeshUtils.Vertex vertex = this._mesh._vHead;
			int num = 0;
			for (MeshUtils.Vertex vertex2 = vertex._next; vertex2 != vertex; vertex2 = vertex2._next)
			{
				num++;
			}
			num += 8;
			this._pq = new PriorityQueue<MeshUtils.Vertex>(num, new PriorityHeap<MeshUtils.Vertex>.LessOrEqual(Geom.VertLeq));
			vertex = this._mesh._vHead;
			for (MeshUtils.Vertex vertex2 = vertex._next; vertex2 != vertex; vertex2 = vertex2._next)
			{
				vertex2._pqHandle = this._pq.Insert(vertex2);
				if (vertex2._pqHandle._handle == PQHandle.Invalid)
				{
					throw new InvalidOperationException("PQHandle should not be invalid");
				}
			}
			this._pq.Init();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00005419 File Offset: 0x00003619
		private void DonePriorityQ()
		{
			this._pq = null;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00005424 File Offset: 0x00003624
		private void RemoveDegenerateFaces()
		{
			MeshUtils.Face next;
			for (MeshUtils.Face face = this._mesh._fHead._next; face != this._mesh._fHead; face = next)
			{
				next = face._next;
				MeshUtils.Edge anEdge = face._anEdge;
				if (anEdge._Lnext._Lnext == anEdge)
				{
					Geom.AddWinding(anEdge._Onext, anEdge);
					this._mesh.Delete(anEdge);
				}
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00005488 File Offset: 0x00003688
		protected void ComputeInterior()
		{
			this.RemoveDegenerateEdges();
			this.InitPriorityQ();
			this.RemoveDegenerateFaces();
			this.InitEdgeDict();
			MeshUtils.Vertex vertex;
			while ((vertex = this._pq.ExtractMin()) != null)
			{
				for (;;)
				{
					MeshUtils.Vertex vertex2 = this._pq.Minimum();
					if (vertex2 == null || !Geom.VertEq(vertex2, vertex))
					{
						break;
					}
					vertex2 = this._pq.ExtractMin();
					this.SpliceMergeVertices(vertex._anEdge, vertex2._anEdge);
				}
				this.SweepEvent(vertex);
			}
			this.DoneEdgeDict();
			this.DonePriorityQ();
			this.RemoveDegenerateFaces();
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000550F File Offset: 0x0000370F
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00005517 File Offset: 0x00003717
		public Vec3 Normal
		{
			get
			{
				return this._normal;
			}
			set
			{
				this._normal = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00005520 File Offset: 0x00003720
		public ContourVertex[] Vertices
		{
			get
			{
				return this._vertices;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00005528 File Offset: 0x00003728
		public int VertexCount
		{
			get
			{
				return this._vertexCount;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00005530 File Offset: 0x00003730
		public int[] Elements
		{
			get
			{
				return this._elements;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00005538 File Offset: 0x00003738
		public int ElementCount
		{
			get
			{
				return this._elementCount;
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00005540 File Offset: 0x00003740
		public Tess()
		{
			this._normal = Vec3.Zero;
			this._bminX = (this._bminY = (this._bmaxX = (this._bmaxY = 0f)));
			this._windingRule = WindingRule.EvenOdd;
			this._mesh = null;
			this._vertices = null;
			this._vertexCount = 0;
			this._elements = null;
			this._elementCount = 0;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000055C4 File Offset: 0x000037C4
		private void ComputeNormal(ref Vec3 norm)
		{
			MeshUtils.Vertex vertex = this._mesh._vHead._next;
			float[] array = new float[]
			{
				vertex._coords.X,
				vertex._coords.Y,
				vertex._coords.Z
			};
			MeshUtils.Vertex[] array2 = new MeshUtils.Vertex[] { vertex, vertex, vertex };
			float[] array3 = new float[]
			{
				vertex._coords.X,
				vertex._coords.Y,
				vertex._coords.Z
			};
			MeshUtils.Vertex[] array4 = new MeshUtils.Vertex[] { vertex, vertex, vertex };
			while (vertex != this._mesh._vHead)
			{
				if (vertex._coords.X < array[0])
				{
					array[0] = vertex._coords.X;
					array2[0] = vertex;
				}
				if (vertex._coords.Y < array[1])
				{
					array[1] = vertex._coords.Y;
					array2[1] = vertex;
				}
				if (vertex._coords.Z < array[2])
				{
					array[2] = vertex._coords.Z;
					array2[2] = vertex;
				}
				if (vertex._coords.X > array3[0])
				{
					array3[0] = vertex._coords.X;
					array4[0] = vertex;
				}
				if (vertex._coords.Y > array3[1])
				{
					array3[1] = vertex._coords.Y;
					array4[1] = vertex;
				}
				if (vertex._coords.Z > array3[2])
				{
					array3[2] = vertex._coords.Z;
					array4[2] = vertex;
				}
				vertex = vertex._next;
			}
			int num = 0;
			if (array3[1] - array[1] > array3[0] - array[0])
			{
				num = 1;
			}
			if (array3[2] - array[2] > array3[num] - array[num])
			{
				num = 2;
			}
			if (array[num] >= array3[num])
			{
				norm = new Vec3
				{
					X = 0f,
					Y = 0f,
					Z = 1f
				};
				return;
			}
			float num2 = 0f;
			MeshUtils.Vertex vertex2 = array2[num];
			MeshUtils.Vertex vertex3 = array4[num];
			Vec3 vec;
			Vec3.Sub(ref vertex2._coords, ref vertex3._coords, out vec);
			for (vertex = this._mesh._vHead._next; vertex != this._mesh._vHead; vertex = vertex._next)
			{
				Vec3 vec2;
				Vec3.Sub(ref vertex._coords, ref vertex3._coords, out vec2);
				Vec3 vec3;
				vec3.X = vec.Y * vec2.Z - vec.Z * vec2.Y;
				vec3.Y = vec.Z * vec2.X - vec.X * vec2.Z;
				vec3.Z = vec.X * vec2.Y - vec.Y * vec2.X;
				float num3 = vec3.X * vec3.X + vec3.Y * vec3.Y + vec3.Z * vec3.Z;
				if (num3 > num2)
				{
					num2 = num3;
					norm = vec3;
				}
			}
			if (num2 <= 0f)
			{
				norm = Vec3.Zero;
				num = Vec3.LongAxis(ref vec);
				norm[num] = 1f;
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00005908 File Offset: 0x00003B08
		private void CheckOrientation()
		{
			float num = 0f;
			for (MeshUtils.Face face = this._mesh._fHead._next; face != this._mesh._fHead; face = face._next)
			{
				if (face._anEdge._winding > 0)
				{
					num += MeshUtils.FaceArea(face);
				}
			}
			if (num < 0f)
			{
				for (MeshUtils.Vertex vertex = this._mesh._vHead._next; vertex != this._mesh._vHead; vertex = vertex._next)
				{
					vertex._t = -vertex._t;
				}
				Vec3.Neg(ref this._tUnit);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000059A4 File Offset: 0x00003BA4
		private void ProjectPolygon()
		{
			Vec3 normal = this._normal;
			bool flag = false;
			if (normal.X == 0f && normal.Y == 0f && normal.Z == 0f)
			{
				this.ComputeNormal(ref normal);
				this._normal = normal;
				flag = true;
			}
			int num = Vec3.LongAxis(ref normal);
			this._sUnit[num] = 0f;
			this._sUnit[(num + 1) % 3] = this.SUnitX;
			this._sUnit[(num + 2) % 3] = this.SUnitY;
			this._tUnit[num] = 0f;
			this._tUnit[(num + 1) % 3] = ((normal[num] > 0f) ? (-this.SUnitY) : this.SUnitY);
			this._tUnit[(num + 2) % 3] = ((normal[num] > 0f) ? this.SUnitX : (-this.SUnitX));
			for (MeshUtils.Vertex vertex = this._mesh._vHead._next; vertex != this._mesh._vHead; vertex = vertex._next)
			{
				Vec3.Dot(ref vertex._coords, ref this._sUnit, out vertex._s);
				Vec3.Dot(ref vertex._coords, ref this._tUnit, out vertex._t);
			}
			if (flag)
			{
				this.CheckOrientation();
			}
			bool flag2 = true;
			for (MeshUtils.Vertex vertex2 = this._mesh._vHead._next; vertex2 != this._mesh._vHead; vertex2 = vertex2._next)
			{
				if (flag2)
				{
					this._bminX = (this._bmaxX = vertex2._s);
					this._bminY = (this._bmaxY = vertex2._t);
					flag2 = false;
				}
				else
				{
					if (vertex2._s < this._bminX)
					{
						this._bminX = vertex2._s;
					}
					if (vertex2._s > this._bmaxX)
					{
						this._bmaxX = vertex2._s;
					}
					if (vertex2._t < this._bminY)
					{
						this._bminY = vertex2._t;
					}
					if (vertex2._t > this._bmaxY)
					{
						this._bmaxY = vertex2._t;
					}
				}
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00005BE8 File Offset: 0x00003DE8
		private void TessellateMonoRegion(MeshUtils.Face face)
		{
			MeshUtils.Edge edge = face._anEdge;
			while (Geom.VertLeq(edge._Dst, edge._Org))
			{
				edge = edge._Lprev;
			}
			while (Geom.VertLeq(edge._Org, edge._Dst))
			{
				edge = edge._Lnext;
			}
			MeshUtils.Edge edge2 = edge._Lprev;
			while (edge._Lnext != edge2)
			{
				if (Geom.VertLeq(edge._Dst, edge2._Org))
				{
					while (edge2._Lnext != edge && (Geom.EdgeGoesLeft(edge2._Lnext) || Geom.EdgeSign(edge2._Org, edge2._Dst, edge2._Lnext._Dst) <= 0f))
					{
						edge2 = this._mesh.Connect(edge2._Lnext, edge2)._Sym;
					}
					edge2 = edge2._Lprev;
				}
				else
				{
					while (edge2._Lnext != edge && (Geom.EdgeGoesRight(edge._Lprev) || Geom.EdgeSign(edge._Dst, edge._Org, edge._Lprev._Org) >= 0f))
					{
						edge = this._mesh.Connect(edge, edge._Lprev)._Sym;
					}
					edge = edge._Lnext;
				}
			}
			while (edge2._Lnext._Lnext != edge)
			{
				edge2 = this._mesh.Connect(edge2._Lnext, edge2)._Sym;
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00005D3C File Offset: 0x00003F3C
		private void TessellateInterior()
		{
			MeshUtils.Face next;
			for (MeshUtils.Face face = this._mesh._fHead._next; face != this._mesh._fHead; face = next)
			{
				next = face._next;
				if (face._inside)
				{
					this.TessellateMonoRegion(face);
				}
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00005D84 File Offset: 0x00003F84
		private void DiscardExterior()
		{
			MeshUtils.Face next;
			for (MeshUtils.Face face = this._mesh._fHead._next; face != this._mesh._fHead; face = next)
			{
				next = face._next;
				if (!face._inside)
				{
					this._mesh.ZapFace(face);
				}
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00005DD0 File Offset: 0x00003FD0
		private void SetWindingNumber(int value, bool keepOnlyBoundary)
		{
			MeshUtils.Edge next;
			for (MeshUtils.Edge edge = this._mesh._eHead._next; edge != this._mesh._eHead; edge = next)
			{
				next = edge._next;
				if (edge._Rface._inside != edge._Lface._inside)
				{
					edge._winding = (edge._Lface._inside ? value : (-value));
				}
				else if (!keepOnlyBoundary)
				{
					edge._winding = 0;
				}
				else
				{
					this._mesh.Delete(edge);
				}
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00005E51 File Offset: 0x00004051
		private int GetNeighbourFace(MeshUtils.Edge edge)
		{
			if (edge._Rface == null)
			{
				return -1;
			}
			if (!edge._Rface._inside)
			{
				return -1;
			}
			return edge._Rface._n;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00005E78 File Offset: 0x00004078
		private void OutputPolymesh(ElementType elementType, int polySize)
		{
			int num = 0;
			int num2 = 0;
			if (polySize < 3)
			{
				polySize = 3;
			}
			if (polySize > 3)
			{
				this._mesh.MergeConvexFaces(polySize);
			}
			for (MeshUtils.Vertex vertex = this._mesh._vHead._next; vertex != this._mesh._vHead; vertex = vertex._next)
			{
				vertex._n = -1;
			}
			for (MeshUtils.Face face = this._mesh._fHead._next; face != this._mesh._fHead; face = face._next)
			{
				face._n = -1;
				if (face._inside && (!this.NoEmptyPolygons || Math.Abs(MeshUtils.FaceArea(face)) >= 1E-45f))
				{
					MeshUtils.Edge edge = face._anEdge;
					int num3 = 0;
					do
					{
						MeshUtils.Vertex vertex = edge._Org;
						if (vertex._n == -1)
						{
							vertex._n = num2;
							num2++;
						}
						num3++;
						edge = edge._Lnext;
					}
					while (edge != face._anEdge);
					face._n = num;
					num++;
				}
			}
			this._elementCount = num;
			if (elementType == ElementType.ConnectedPolygons)
			{
				num *= 2;
			}
			this._elements = new int[num * polySize];
			this._vertexCount = num2;
			this._vertices = new ContourVertex[this._vertexCount];
			for (MeshUtils.Vertex vertex = this._mesh._vHead._next; vertex != this._mesh._vHead; vertex = vertex._next)
			{
				if (vertex._n != -1)
				{
					this._vertices[vertex._n].Position = vertex._coords;
					this._vertices[vertex._n].Data = vertex._data;
				}
			}
			int num4 = 0;
			for (MeshUtils.Face face = this._mesh._fHead._next; face != this._mesh._fHead; face = face._next)
			{
				if (face._inside && (!this.NoEmptyPolygons || Math.Abs(MeshUtils.FaceArea(face)) >= 1E-45f))
				{
					MeshUtils.Edge edge = face._anEdge;
					int num3 = 0;
					do
					{
						MeshUtils.Vertex vertex = edge._Org;
						this._elements[num4++] = vertex._n;
						num3++;
						edge = edge._Lnext;
					}
					while (edge != face._anEdge);
					for (int i = num3; i < polySize; i++)
					{
						this._elements[num4++] = -1;
					}
					if (elementType == ElementType.ConnectedPolygons)
					{
						edge = face._anEdge;
						do
						{
							this._elements[num4++] = this.GetNeighbourFace(edge);
							edge = edge._Lnext;
						}
						while (edge != face._anEdge);
						for (int i = num3; i < polySize; i++)
						{
							this._elements[num4++] = -1;
						}
					}
				}
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00006110 File Offset: 0x00004310
		private void OutputContours()
		{
			this._vertexCount = 0;
			this._elementCount = 0;
			for (MeshUtils.Face face = this._mesh._fHead._next; face != this._mesh._fHead; face = face._next)
			{
				if (face._inside)
				{
					MeshUtils.Edge edge2;
					MeshUtils.Edge edge = (edge2 = face._anEdge);
					do
					{
						this._vertexCount++;
						edge = edge._Lnext;
					}
					while (edge != edge2);
					this._elementCount++;
				}
			}
			this._elements = new int[this._elementCount * 2];
			this._vertices = new ContourVertex[this._vertexCount];
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			for (MeshUtils.Face face = this._mesh._fHead._next; face != this._mesh._fHead; face = face._next)
			{
				if (face._inside)
				{
					int num4 = 0;
					MeshUtils.Edge edge2;
					MeshUtils.Edge edge = (edge2 = face._anEdge);
					do
					{
						this._vertices[num].Position = edge._Org._coords;
						this._vertices[num].Data = edge._Org._data;
						num++;
						num4++;
						edge = edge._Lnext;
					}
					while (edge != edge2);
					this._elements[num2++] = num3;
					this._elements[num2++] = num4;
					num3 += num4;
				}
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00006278 File Offset: 0x00004478
		private float SignedArea(ContourVertex[] vertices)
		{
			float num = 0f;
			for (int i = 0; i < vertices.Length; i++)
			{
				ContourVertex contourVertex = vertices[i];
				ContourVertex contourVertex2 = vertices[(i + 1) % vertices.Length];
				num += contourVertex.Position.X * contourVertex2.Position.Y;
				num -= contourVertex.Position.Y * contourVertex2.Position.X;
			}
			return 0.5f * num;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000062EA File Offset: 0x000044EA
		public void AddContour(ContourVertex[] vertices)
		{
			this.AddContour(vertices, ContourOrientation.Original);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000062F4 File Offset: 0x000044F4
		public void AddContour(ContourVertex[] vertices, ContourOrientation forceOrientation)
		{
			if (this._mesh == null)
			{
				this._mesh = new Mesh();
			}
			bool flag = false;
			if (forceOrientation != ContourOrientation.Original)
			{
				float num = this.SignedArea(vertices);
				flag = (forceOrientation == ContourOrientation.Clockwise && num < 0f) || (forceOrientation == ContourOrientation.CounterClockwise && num > 0f);
			}
			MeshUtils.Edge edge = null;
			for (int i = 0; i < vertices.Length; i++)
			{
				if (edge == null)
				{
					edge = this._mesh.MakeEdge();
					this._mesh.Splice(edge, edge._Sym);
				}
				else
				{
					this._mesh.SplitEdge(edge);
					edge = edge._Lnext;
				}
				int num2 = (flag ? (vertices.Length - 1 - i) : i);
				edge._Org._coords = vertices[num2].Position;
				edge._Org._data = vertices[num2].Data;
				edge._winding = 1;
				edge._Sym._winding = -1;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000063DF File Offset: 0x000045DF
		public void Tessellate(WindingRule windingRule, ElementType elementType, int polySize)
		{
			this.Tessellate(windingRule, elementType, polySize, null);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000063EC File Offset: 0x000045EC
		public void Tessellate(WindingRule windingRule, ElementType elementType, int polySize, CombineCallback combineCallback)
		{
			this._normal = Vec3.Zero;
			this._vertices = null;
			this._elements = null;
			this._windingRule = windingRule;
			this._combineCallback = combineCallback;
			if (this._mesh == null)
			{
				return;
			}
			this.ProjectPolygon();
			this.ComputeInterior();
			if (elementType == ElementType.BoundaryContours)
			{
				this.SetWindingNumber(1, true);
			}
			else
			{
				this.TessellateInterior();
			}
			if (elementType == ElementType.BoundaryContours)
			{
				this.OutputContours();
			}
			else
			{
				this.OutputPolymesh(elementType, polySize);
			}
			if (this.UsePooling)
			{
				this._mesh.Free();
			}
			this._mesh = null;
		}

		// Token: 0x0400002D RID: 45
		private Mesh _mesh;

		// Token: 0x0400002E RID: 46
		private Vec3 _normal;

		// Token: 0x0400002F RID: 47
		private Vec3 _sUnit;

		// Token: 0x04000030 RID: 48
		private Vec3 _tUnit;

		// Token: 0x04000031 RID: 49
		private float _bminX;

		// Token: 0x04000032 RID: 50
		private float _bminY;

		// Token: 0x04000033 RID: 51
		private float _bmaxX;

		// Token: 0x04000034 RID: 52
		private float _bmaxY;

		// Token: 0x04000035 RID: 53
		private WindingRule _windingRule;

		// Token: 0x04000036 RID: 54
		private Dict<Tess.ActiveRegion> _dict;

		// Token: 0x04000037 RID: 55
		private PriorityQueue<MeshUtils.Vertex> _pq;

		// Token: 0x04000038 RID: 56
		private MeshUtils.Vertex _event;

		// Token: 0x04000039 RID: 57
		private CombineCallback _combineCallback;

		// Token: 0x0400003A RID: 58
		private ContourVertex[] _vertices;

		// Token: 0x0400003B RID: 59
		private int _vertexCount;

		// Token: 0x0400003C RID: 60
		private int[] _elements;

		// Token: 0x0400003D RID: 61
		private int _elementCount;

		// Token: 0x0400003E RID: 62
		public float SUnitX = 1f;

		// Token: 0x0400003F RID: 63
		public float SUnitY;

		// Token: 0x04000040 RID: 64
		public float SentinelCoord = 4E+30f;

		// Token: 0x04000041 RID: 65
		public bool NoEmptyPolygons;

		// Token: 0x04000042 RID: 66
		public bool UsePooling;

		// Token: 0x0200002B RID: 43
		internal class ActiveRegion
		{
			// Token: 0x0400010F RID: 271
			internal MeshUtils.Edge _eUp;

			// Token: 0x04000110 RID: 272
			internal Dict<Tess.ActiveRegion>.Node _nodeUp;

			// Token: 0x04000111 RID: 273
			internal int _windingNumber;

			// Token: 0x04000112 RID: 274
			internal bool _inside;

			// Token: 0x04000113 RID: 275
			internal bool _sentinel;

			// Token: 0x04000114 RID: 276
			internal bool _dirty;

			// Token: 0x04000115 RID: 277
			internal bool _fixUpperEdge;
		}
	}
}
