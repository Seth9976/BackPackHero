using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000082 RID: 130
	[Obsolete("Use AstarPath.navmeshUpdates instead. You can safely remove this component.")]
	[HelpURL("http://arongranberg.com/astar/documentation/stable/class_pathfinding_1_1_tile_handler_helper.php")]
	public class TileHandlerHelper : VersionedMonoBehaviour
	{
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x00027AD2 File Offset: 0x00025CD2
		// (set) Token: 0x06000696 RID: 1686 RVA: 0x00027AE3 File Offset: 0x00025CE3
		public float updateInterval
		{
			get
			{
				return AstarPath.active.navmeshUpdates.updateInterval;
			}
			set
			{
				AstarPath.active.navmeshUpdates.updateInterval = value;
			}
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00027AF5 File Offset: 0x00025CF5
		[Obsolete("All navmesh/recast graphs now use navmesh cutting")]
		public void UseSpecifiedHandler(TileHandler newHandler)
		{
			throw new Exception("All navmesh/recast graphs now use navmesh cutting");
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00027B01 File Offset: 0x00025D01
		public void DiscardPending()
		{
			AstarPath.active.navmeshUpdates.DiscardPending();
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00027B12 File Offset: 0x00025D12
		public void ForceUpdate()
		{
			AstarPath.active.navmeshUpdates.ForceUpdate();
		}
	}
}
