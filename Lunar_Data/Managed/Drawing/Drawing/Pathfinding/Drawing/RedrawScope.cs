using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Pathfinding.Drawing
{
	// Token: 0x02000028 RID: 40
	public struct RedrawScope : IDisposable
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00008355 File Offset: 0x00006555
		public bool isValid
		{
			get
			{
				return this.id != 0;
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00008360 File Offset: 0x00006560
		internal RedrawScope(DrawingData gizmos, int id)
		{
			this.gizmos = gizmos.gizmosHandle;
			this.id = id;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00008375 File Offset: 0x00006575
		internal RedrawScope(DrawingData gizmos)
		{
			this.gizmos = gizmos.gizmosHandle;
			this.id = RedrawScope.idCounter++;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00008398 File Offset: 0x00006598
		internal void Draw()
		{
			if (this.gizmos.IsAllocated)
			{
				DrawingData drawingData = this.gizmos.Target as DrawingData;
				if (drawingData != null)
				{
					drawingData.Draw(this);
				}
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000083D4 File Offset: 0x000065D4
		public void Rewind()
		{
			GameObject gameObject = null;
			if (this.gizmos.IsAllocated)
			{
				DrawingData drawingData = this.gizmos.Target as DrawingData;
				if (drawingData != null)
				{
					gameObject = drawingData.GetAssociatedGameObject(this);
				}
			}
			this.Dispose();
			this = DrawingManager.GetRedrawScope(gameObject);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00008424 File Offset: 0x00006624
		internal void DrawUntilDispose(GameObject associatedGameObject)
		{
			DrawingData drawingData = this.gizmos.Target as DrawingData;
			if (drawingData != null)
			{
				drawingData.DrawUntilDisposed(this, associatedGameObject);
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00008454 File Offset: 0x00006654
		public void Dispose()
		{
			if (this.gizmos.IsAllocated)
			{
				DrawingData drawingData = this.gizmos.Target as DrawingData;
				if (drawingData != null)
				{
					drawingData.DisposeRedrawScope(this);
				}
			}
			this.gizmos = default(GCHandle);
			this.id = 0;
		}

		// Token: 0x04000078 RID: 120
		internal GCHandle gizmos;

		// Token: 0x04000079 RID: 121
		internal int id;

		// Token: 0x0400007A RID: 122
		private static int idCounter = 1;
	}
}
