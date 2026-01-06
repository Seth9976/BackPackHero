using System;

namespace UnityEngine.Rendering.Universal.LibTessDotNet
{
	// Token: 0x020000F8 RID: 248
	internal class Tess
	{
		// Token: 0x06000727 RID: 1831 RVA: 0x00028DB1 File Offset: 0x00026FB1
		private Tess.ActiveRegion RegionBelow(Tess.ActiveRegion reg)
		{
			return reg._nodeUp._prev._key;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00028DC3 File Offset: 0x00026FC3
		private Tess.ActiveRegion RegionAbove(Tess.ActiveRegion reg)
		{
			return reg._nodeUp._next._key;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00028DD8 File Offset: 0x00026FD8
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

		// Token: 0x0600072A RID: 1834 RVA: 0x00028EF0 File Offset: 0x000270F0
		private void DeleteRegion(Tess.ActiveRegion reg)
		{
			bool fixUpperEdge = reg._fixUpperEdge;
			reg._eUp._activeRegion = null;
			this._dict.Remove(reg._nodeUp);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00028F16 File Offset: 0x00027116
		private void FixUpperEdge(Tess.ActiveRegion reg, MeshUtils.Edge newEdge)
		{
			this._mesh.Delete(reg._eUp);
			reg._fixUpperEdge = false;
			reg._eUp = newEdge;
			newEdge._activeRegion = reg;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00028F40 File Offset: 0x00027140
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

		// Token: 0x0600072D RID: 1837 RVA: 0x00028FB4 File Offset: 0x000271B4
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

		// Token: 0x0600072E RID: 1838 RVA: 0x00028FE8 File Offset: 0x000271E8
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

		// Token: 0x0600072F RID: 1839 RVA: 0x00029037 File Offset: 0x00027237
		private void ComputeWinding(Tess.ActiveRegion reg)
		{
			reg._windingNumber = this.RegionAbove(reg)._windingNumber + reg._eUp._winding;
			reg._inside = Geom.IsWindingInside(this._windingRule, reg._windingNumber);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00029070 File Offset: 0x00027270
		private void FinishRegion(Tess.ActiveRegion reg)
		{
			MeshUtils.Edge eUp = reg._eUp;
			MeshUtils.Face lface = eUp._Lface;
			lface._inside = reg._inside;
			lface._anEdge = eUp;
			this.DeleteRegion(reg);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x000290A4 File Offset: 0x000272A4
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

		// Token: 0x06000732 RID: 1842 RVA: 0x00029154 File Offset: 0x00027354
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

		// Token: 0x06000733 RID: 1843 RVA: 0x00029267 File Offset: 0x00027467
		private void SpliceMergeVertices(MeshUtils.Edge e1, MeshUtils.Edge e2)
		{
			this._mesh.Splice(e1, e2);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00029278 File Offset: 0x00027478
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

		// Token: 0x06000735 RID: 1845 RVA: 0x0002933C File Offset: 0x0002753C
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

		// Token: 0x06000736 RID: 1846 RVA: 0x000293D4 File Offset: 0x000275D4
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

		// Token: 0x06000737 RID: 1847 RVA: 0x00029508 File Offset: 0x00027708
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

		// Token: 0x06000738 RID: 1848 RVA: 0x00029610 File Offset: 0x00027810
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

		// Token: 0x06000739 RID: 1849 RVA: 0x00029A2C File Offset: 0x00027C2C
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

		// Token: 0x0600073A RID: 1850 RVA: 0x00029B9C File Offset: 0x00027D9C
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

		// Token: 0x0600073B RID: 1851 RVA: 0x00029CDC File Offset: 0x00027EDC
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

		// Token: 0x0600073C RID: 1852 RVA: 0x00029D70 File Offset: 0x00027F70
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

		// Token: 0x0600073D RID: 1853 RVA: 0x00029E94 File Offset: 0x00028094
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

		// Token: 0x0600073E RID: 1854 RVA: 0x00029F1C File Offset: 0x0002811C
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

		// Token: 0x0600073F RID: 1855 RVA: 0x00029FB4 File Offset: 0x000281B4
		private void InitEdgeDict()
		{
			this._dict = new Dict<Tess.ActiveRegion>(new Dict<Tess.ActiveRegion>.LessOrEqual(this.EdgeLeq));
			this.AddSentinel(-this.SentinelCoord, this.SentinelCoord, -this.SentinelCoord);
			this.AddSentinel(-this.SentinelCoord, this.SentinelCoord, this.SentinelCoord);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0002A00C File Offset: 0x0002820C
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

		// Token: 0x06000741 RID: 1857 RVA: 0x0002A044 File Offset: 0x00028244
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

		// Token: 0x06000742 RID: 1858 RVA: 0x0002A10C File Offset: 0x0002830C
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

		// Token: 0x06000743 RID: 1859 RVA: 0x0002A1B5 File Offset: 0x000283B5
		private void DonePriorityQ()
		{
			this._pq = null;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0002A1C0 File Offset: 0x000283C0
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

		// Token: 0x06000745 RID: 1861 RVA: 0x0002A224 File Offset: 0x00028424
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

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x0002A2AB File Offset: 0x000284AB
		// (set) Token: 0x06000747 RID: 1863 RVA: 0x0002A2B3 File Offset: 0x000284B3
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

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x0002A2BC File Offset: 0x000284BC
		public ContourVertex[] Vertices
		{
			get
			{
				return this._vertices;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x0002A2C4 File Offset: 0x000284C4
		public int VertexCount
		{
			get
			{
				return this._vertexCount;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x0002A2CC File Offset: 0x000284CC
		public int[] Elements
		{
			get
			{
				return this._elements;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x0002A2D4 File Offset: 0x000284D4
		public int ElementCount
		{
			get
			{
				return this._elementCount;
			}
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0002A2DC File Offset: 0x000284DC
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

		// Token: 0x0600074D RID: 1869 RVA: 0x0002A360 File Offset: 0x00028560
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

		// Token: 0x0600074E RID: 1870 RVA: 0x0002A6A4 File Offset: 0x000288A4
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

		// Token: 0x0600074F RID: 1871 RVA: 0x0002A740 File Offset: 0x00028940
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

		// Token: 0x06000750 RID: 1872 RVA: 0x0002A984 File Offset: 0x00028B84
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

		// Token: 0x06000751 RID: 1873 RVA: 0x0002AAD8 File Offset: 0x00028CD8
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

		// Token: 0x06000752 RID: 1874 RVA: 0x0002AB20 File Offset: 0x00028D20
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

		// Token: 0x06000753 RID: 1875 RVA: 0x0002AB6C File Offset: 0x00028D6C
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

		// Token: 0x06000754 RID: 1876 RVA: 0x0002ABED File Offset: 0x00028DED
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

		// Token: 0x06000755 RID: 1877 RVA: 0x0002AC14 File Offset: 0x00028E14
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

		// Token: 0x06000756 RID: 1878 RVA: 0x0002AEAC File Offset: 0x000290AC
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

		// Token: 0x06000757 RID: 1879 RVA: 0x0002B014 File Offset: 0x00029214
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

		// Token: 0x06000758 RID: 1880 RVA: 0x0002B086 File Offset: 0x00029286
		public void AddContour(ContourVertex[] vertices)
		{
			this.AddContour(vertices, ContourOrientation.Original);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0002B090 File Offset: 0x00029290
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

		// Token: 0x0600075A RID: 1882 RVA: 0x0002B17B File Offset: 0x0002937B
		public void Tessellate(WindingRule windingRule, ElementType elementType, int polySize)
		{
			this.Tessellate(windingRule, elementType, polySize, null);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0002B188 File Offset: 0x00029388
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

		// Token: 0x040006BE RID: 1726
		private Mesh _mesh;

		// Token: 0x040006BF RID: 1727
		private Vec3 _normal;

		// Token: 0x040006C0 RID: 1728
		private Vec3 _sUnit;

		// Token: 0x040006C1 RID: 1729
		private Vec3 _tUnit;

		// Token: 0x040006C2 RID: 1730
		private float _bminX;

		// Token: 0x040006C3 RID: 1731
		private float _bminY;

		// Token: 0x040006C4 RID: 1732
		private float _bmaxX;

		// Token: 0x040006C5 RID: 1733
		private float _bmaxY;

		// Token: 0x040006C6 RID: 1734
		private WindingRule _windingRule;

		// Token: 0x040006C7 RID: 1735
		private Dict<Tess.ActiveRegion> _dict;

		// Token: 0x040006C8 RID: 1736
		private PriorityQueue<MeshUtils.Vertex> _pq;

		// Token: 0x040006C9 RID: 1737
		private MeshUtils.Vertex _event;

		// Token: 0x040006CA RID: 1738
		private CombineCallback _combineCallback;

		// Token: 0x040006CB RID: 1739
		private ContourVertex[] _vertices;

		// Token: 0x040006CC RID: 1740
		private int _vertexCount;

		// Token: 0x040006CD RID: 1741
		private int[] _elements;

		// Token: 0x040006CE RID: 1742
		private int _elementCount;

		// Token: 0x040006CF RID: 1743
		public float SUnitX = 1f;

		// Token: 0x040006D0 RID: 1744
		public float SUnitY;

		// Token: 0x040006D1 RID: 1745
		public float SentinelCoord = 4E+30f;

		// Token: 0x040006D2 RID: 1746
		public bool NoEmptyPolygons;

		// Token: 0x040006D3 RID: 1747
		public bool UsePooling;

		// Token: 0x0200019B RID: 411
		internal class ActiveRegion
		{
			// Token: 0x04000A30 RID: 2608
			internal MeshUtils.Edge _eUp;

			// Token: 0x04000A31 RID: 2609
			internal Dict<Tess.ActiveRegion>.Node _nodeUp;

			// Token: 0x04000A32 RID: 2610
			internal int _windingNumber;

			// Token: 0x04000A33 RID: 2611
			internal bool _inside;

			// Token: 0x04000A34 RID: 2612
			internal bool _sentinel;

			// Token: 0x04000A35 RID: 2613
			internal bool _dirty;

			// Token: 0x04000A36 RID: 2614
			internal bool _fixUpperEdge;
		}
	}
}
