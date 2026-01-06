using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200007E RID: 126
	public abstract class NavmeshClipper : VersionedMonoBehaviour
	{
		// Token: 0x06000667 RID: 1639 RVA: 0x00026982 File Offset: 0x00024B82
		public static void AddEnableCallback(Action<NavmeshClipper> onEnable, Action<NavmeshClipper> onDisable)
		{
			NavmeshClipper.OnEnableCallback = (Action<NavmeshClipper>)Delegate.Combine(NavmeshClipper.OnEnableCallback, onEnable);
			NavmeshClipper.OnDisableCallback = (Action<NavmeshClipper>)Delegate.Combine(NavmeshClipper.OnDisableCallback, onDisable);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x000269AE File Offset: 0x00024BAE
		public static void RemoveEnableCallback(Action<NavmeshClipper> onEnable, Action<NavmeshClipper> onDisable)
		{
			NavmeshClipper.OnEnableCallback = (Action<NavmeshClipper>)Delegate.Remove(NavmeshClipper.OnEnableCallback, onEnable);
			NavmeshClipper.OnDisableCallback = (Action<NavmeshClipper>)Delegate.Remove(NavmeshClipper.OnDisableCallback, onDisable);
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x000269DA File Offset: 0x00024BDA
		public static List<NavmeshClipper> allEnabled
		{
			get
			{
				return NavmeshClipper.all;
			}
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x000269E1 File Offset: 0x00024BE1
		protected virtual void OnEnable()
		{
			if (NavmeshClipper.OnEnableCallback != null)
			{
				NavmeshClipper.OnEnableCallback(this);
			}
			this.listIndex = NavmeshClipper.all.Count;
			NavmeshClipper.all.Add(this);
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00026A10 File Offset: 0x00024C10
		protected virtual void OnDisable()
		{
			NavmeshClipper.all[this.listIndex] = NavmeshClipper.all[NavmeshClipper.all.Count - 1];
			NavmeshClipper.all[this.listIndex].listIndex = this.listIndex;
			NavmeshClipper.all.RemoveAt(NavmeshClipper.all.Count - 1);
			this.listIndex = -1;
			if (NavmeshClipper.OnDisableCallback != null)
			{
				NavmeshClipper.OnDisableCallback(this);
			}
		}

		// Token: 0x0600066C RID: 1644
		internal abstract void NotifyUpdated();

		// Token: 0x0600066D RID: 1645
		public abstract Rect GetBounds(GraphTransform transform);

		// Token: 0x0600066E RID: 1646
		public abstract bool RequiresUpdate();

		// Token: 0x0600066F RID: 1647
		public abstract void ForceUpdate();

		// Token: 0x040003B8 RID: 952
		private static Action<NavmeshClipper> OnEnableCallback;

		// Token: 0x040003B9 RID: 953
		private static Action<NavmeshClipper> OnDisableCallback;

		// Token: 0x040003BA RID: 954
		private static readonly List<NavmeshClipper> all = new List<NavmeshClipper>();

		// Token: 0x040003BB RID: 955
		private int listIndex = -1;

		// Token: 0x040003BC RID: 956
		public GraphMask graphMask = GraphMask.everything;
	}
}
