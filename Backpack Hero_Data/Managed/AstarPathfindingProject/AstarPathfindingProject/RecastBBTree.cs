using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000070 RID: 112
	public class RecastBBTree
	{
		// Token: 0x0600060E RID: 1550 RVA: 0x00023BF0 File Offset: 0x00021DF0
		public void QueryInBounds(Rect bounds, List<RecastMeshObj> buffer)
		{
			if (this.root == null)
			{
				return;
			}
			this.QueryBoxInBounds(this.root, bounds, buffer);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00023C0C File Offset: 0x00021E0C
		private void QueryBoxInBounds(RecastBBTreeBox box, Rect bounds, List<RecastMeshObj> boxes)
		{
			if (box.mesh != null)
			{
				if (RecastBBTree.RectIntersectsRect(box.rect, bounds))
				{
					boxes.Add(box.mesh);
					return;
				}
			}
			else
			{
				if (RecastBBTree.RectIntersectsRect(box.c1.rect, bounds))
				{
					this.QueryBoxInBounds(box.c1, bounds, boxes);
				}
				if (RecastBBTree.RectIntersectsRect(box.c2.rect, bounds))
				{
					this.QueryBoxInBounds(box.c2, bounds, boxes);
				}
			}
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00023C84 File Offset: 0x00021E84
		public bool Remove(RecastMeshObj mesh)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			if (this.root == null)
			{
				return false;
			}
			bool flag = false;
			Bounds bounds = mesh.GetBounds();
			Rect rect = Rect.MinMaxRect(bounds.min.x, bounds.min.z, bounds.max.x, bounds.max.z);
			this.root = this.RemoveBox(this.root, mesh, rect, ref flag);
			return flag;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00023D08 File Offset: 0x00021F08
		private RecastBBTreeBox RemoveBox(RecastBBTreeBox c, RecastMeshObj mesh, Rect bounds, ref bool found)
		{
			if (!RecastBBTree.RectIntersectsRect(c.rect, bounds))
			{
				return c;
			}
			if (c.mesh == mesh)
			{
				found = true;
				return null;
			}
			if (c.mesh == null && !found)
			{
				c.c1 = this.RemoveBox(c.c1, mesh, bounds, ref found);
				if (c.c1 == null)
				{
					return c.c2;
				}
				if (!found)
				{
					c.c2 = this.RemoveBox(c.c2, mesh, bounds, ref found);
					if (c.c2 == null)
					{
						return c.c1;
					}
				}
				if (found)
				{
					c.rect = RecastBBTree.ExpandToContain(c.c1.rect, c.c2.rect);
				}
			}
			return c;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00023DC4 File Offset: 0x00021FC4
		public void Insert(RecastMeshObj mesh)
		{
			RecastBBTreeBox recastBBTreeBox = new RecastBBTreeBox(mesh);
			if (this.root == null)
			{
				this.root = recastBBTreeBox;
				return;
			}
			RecastBBTreeBox recastBBTreeBox2 = this.root;
			for (;;)
			{
				recastBBTreeBox2.rect = RecastBBTree.ExpandToContain(recastBBTreeBox2.rect, recastBBTreeBox.rect);
				if (recastBBTreeBox2.mesh != null)
				{
					break;
				}
				float num = RecastBBTree.ExpansionRequired(recastBBTreeBox2.c1.rect, recastBBTreeBox.rect);
				float num2 = RecastBBTree.ExpansionRequired(recastBBTreeBox2.c2.rect, recastBBTreeBox.rect);
				if (num < num2)
				{
					recastBBTreeBox2 = recastBBTreeBox2.c1;
				}
				else if (num2 < num)
				{
					recastBBTreeBox2 = recastBBTreeBox2.c2;
				}
				else
				{
					recastBBTreeBox2 = ((RecastBBTree.RectArea(recastBBTreeBox2.c1.rect) < RecastBBTree.RectArea(recastBBTreeBox2.c2.rect)) ? recastBBTreeBox2.c1 : recastBBTreeBox2.c2);
				}
			}
			recastBBTreeBox2.c1 = recastBBTreeBox;
			RecastBBTreeBox recastBBTreeBox3 = new RecastBBTreeBox(recastBBTreeBox2.mesh);
			recastBBTreeBox2.c2 = recastBBTreeBox3;
			recastBBTreeBox2.mesh = null;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00023EBC File Offset: 0x000220BC
		private static bool RectIntersectsRect(Rect r, Rect r2)
		{
			return r.xMax > r2.xMin && r.yMax > r2.yMin && r2.xMax > r.xMin && r2.yMax > r.yMin;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00023F0C File Offset: 0x0002210C
		private static float ExpansionRequired(Rect r, Rect r2)
		{
			float num = Mathf.Min(r.xMin, r2.xMin);
			float num2 = Mathf.Max(r.xMax, r2.xMax);
			float num3 = Mathf.Min(r.yMin, r2.yMin);
			float num4 = Mathf.Max(r.yMax, r2.yMax);
			return (num2 - num) * (num4 - num3) - RecastBBTree.RectArea(r);
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00023F78 File Offset: 0x00022178
		private static Rect ExpandToContain(Rect r, Rect r2)
		{
			float num = Mathf.Min(r.xMin, r2.xMin);
			float num2 = Mathf.Max(r.xMax, r2.xMax);
			float num3 = Mathf.Min(r.yMin, r2.yMin);
			float num4 = Mathf.Max(r.yMax, r2.yMax);
			return Rect.MinMaxRect(num, num3, num2, num4);
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00023FDC File Offset: 0x000221DC
		private static float RectArea(Rect r)
		{
			return r.width * r.height;
		}

		// Token: 0x0400036B RID: 875
		private RecastBBTreeBox root;
	}
}
