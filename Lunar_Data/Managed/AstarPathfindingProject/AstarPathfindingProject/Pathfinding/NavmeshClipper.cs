using System;
using System.Collections.Generic;
using Pathfinding.Graphs.Util;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200011D RID: 285
	[ExecuteAlways]
	public abstract class NavmeshClipper : VersionedMonoBehaviour
	{
		// Token: 0x060008C8 RID: 2248 RVA: 0x0002F902 File Offset: 0x0002DB02
		public static void AddEnableCallback(Action<NavmeshClipper> onEnable, Action<NavmeshClipper> onDisable)
		{
			NavmeshClipper.OnEnableCallback = (Action<NavmeshClipper>)Delegate.Combine(NavmeshClipper.OnEnableCallback, onEnable);
			NavmeshClipper.OnDisableCallback = (Action<NavmeshClipper>)Delegate.Combine(NavmeshClipper.OnDisableCallback, onDisable);
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0002F92E File Offset: 0x0002DB2E
		public static void RemoveEnableCallback(Action<NavmeshClipper> onEnable, Action<NavmeshClipper> onDisable)
		{
			NavmeshClipper.OnEnableCallback = (Action<NavmeshClipper>)Delegate.Remove(NavmeshClipper.OnEnableCallback, onEnable);
			NavmeshClipper.OnDisableCallback = (Action<NavmeshClipper>)Delegate.Remove(NavmeshClipper.OnDisableCallback, onDisable);
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x0002F95A File Offset: 0x0002DB5A
		public static List<NavmeshClipper> allEnabled
		{
			get
			{
				return NavmeshClipper.all;
			}
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0002F961 File Offset: 0x0002DB61
		protected virtual void OnEnable()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			if (NavmeshClipper.OnEnableCallback != null)
			{
				NavmeshClipper.OnEnableCallback(this);
			}
			this.listIndex = NavmeshClipper.all.Count;
			NavmeshClipper.all.Add(this);
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0002F998 File Offset: 0x0002DB98
		protected virtual void OnDisable()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			NavmeshClipper.all[this.listIndex] = NavmeshClipper.all[NavmeshClipper.all.Count - 1];
			NavmeshClipper.all[this.listIndex].listIndex = this.listIndex;
			NavmeshClipper.all.RemoveAt(NavmeshClipper.all.Count - 1);
			this.listIndex = -1;
			if (NavmeshClipper.OnDisableCallback != null)
			{
				NavmeshClipper.OnDisableCallback(this);
			}
		}

		// Token: 0x060008CD RID: 2253
		internal abstract void NotifyUpdated(GridLookup<NavmeshClipper>.Root previousState);

		// Token: 0x060008CE RID: 2254
		public abstract Rect GetBounds(GraphTransform transform, float radiusMargin);

		// Token: 0x060008CF RID: 2255
		public abstract bool RequiresUpdate(GridLookup<NavmeshClipper>.Root previousState);

		// Token: 0x060008D0 RID: 2256
		public abstract void ForceUpdate();

		// Token: 0x040005FB RID: 1531
		private static Action<NavmeshClipper> OnEnableCallback;

		// Token: 0x040005FC RID: 1532
		private static Action<NavmeshClipper> OnDisableCallback;

		// Token: 0x040005FD RID: 1533
		private static readonly List<NavmeshClipper> all = new List<NavmeshClipper>();

		// Token: 0x040005FE RID: 1534
		private int listIndex = -1;

		// Token: 0x040005FF RID: 1535
		public GraphMask graphMask = GraphMask.everything;
	}
}
