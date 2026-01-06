using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Drawing
{
	// Token: 0x02000043 RID: 67
	public static class GizmoContext
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000AB39 File Offset: 0x00008D39
		// (set) Token: 0x06000247 RID: 583 RVA: 0x0000AB45 File Offset: 0x00008D45
		public static int selectionSize
		{
			get
			{
				GizmoContext.Refresh();
				return GizmoContext.selectionSizeInternal;
			}
			private set
			{
				GizmoContext.selectionSizeInternal = value;
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000AB4D File Offset: 0x00008D4D
		internal static void SetDirty()
		{
			GizmoContext.dirty = true;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00002094 File Offset: 0x00000294
		private static void Refresh()
		{
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000AB55 File Offset: 0x00008D55
		public static bool InSelection(Component c)
		{
			return GizmoContext.InSelection(c.transform);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000AB64 File Offset: 0x00008D64
		public static bool InSelection(Transform tr)
		{
			GizmoContext.Refresh();
			Transform transform = tr;
			while (tr != null)
			{
				if (GizmoContext.selectedTransforms.Contains(tr))
				{
					GizmoContext.selectedTransforms.Add(transform);
					return true;
				}
				tr = tr.parent;
			}
			return false;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000ABA7 File Offset: 0x00008DA7
		public static bool InActiveSelection(Component c)
		{
			return GizmoContext.InActiveSelection(c.transform);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000ABB4 File Offset: 0x00008DB4
		public static bool InActiveSelection(Transform tr)
		{
			return false;
		}

		// Token: 0x040000F4 RID: 244
		private static HashSet<Transform> selectedTransforms = new HashSet<Transform>();

		// Token: 0x040000F5 RID: 245
		internal static bool drawingGizmos;

		// Token: 0x040000F6 RID: 246
		internal static bool dirty;

		// Token: 0x040000F7 RID: 247
		private static int selectionSizeInternal;
	}
}
