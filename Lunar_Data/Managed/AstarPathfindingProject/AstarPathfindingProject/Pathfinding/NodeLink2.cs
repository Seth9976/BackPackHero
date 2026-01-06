using System;
using System.Collections.Generic;
using Pathfinding.Drawing;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200007B RID: 123
	[AddComponentMenu("Pathfinding/Link2")]
	[HelpURL("https://arongranberg.com/astar/documentation/stable/nodelink2.html")]
	public class NodeLink2 : GraphModifier
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x000144ED File Offset: 0x000126ED
		public Transform StartTransform
		{
			get
			{
				return base.transform;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x000146BC File Offset: 0x000128BC
		public Transform EndTransform
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x000146C4 File Offset: 0x000128C4
		public static NodeLink2 GetNodeLink(GraphNode node)
		{
			LinkNode linkNode = node as LinkNode;
			if (linkNode == null)
			{
				return null;
			}
			return linkNode.linkSource.component as NodeLink2;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x000146ED File Offset: 0x000128ED
		internal bool isActive
		{
			get
			{
				return this.linkSource != null && (this.linkSource.status & OffMeshLinks.OffMeshLinkStatus.Active) > (OffMeshLinks.OffMeshLinkStatus)0;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00014709 File Offset: 0x00012909
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x00014711 File Offset: 0x00012911
		public IOffMeshLinkHandler onTraverseOffMeshLink
		{
			get
			{
				return this.onTraverseOffMeshLinkHandler;
			}
			set
			{
				this.onTraverseOffMeshLinkHandler = value;
				if (this.linkSource != null)
				{
					this.linkSource.handler = value;
				}
			}
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0001472E File Offset: 0x0001292E
		public override void OnPostScan()
		{
			this.TryAddLink();
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00014736 File Offset: 0x00012936
		protected override void OnEnable()
		{
			base.OnEnable();
			if (Application.isPlaying && !BatchedEvents.Has<NodeLink2>(this))
			{
				BatchedEvents.Add<NodeLink2>(this, BatchedEvents.Event.Update, new Action<NodeLink2[], int>(NodeLink2.OnUpdate), 0);
			}
			this.TryAddLink();
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00014768 File Offset: 0x00012968
		private static void OnUpdate(NodeLink2[] components, int count)
		{
			if (Time.frameCount % 16 != 0)
			{
				return;
			}
			for (int i = 0; i < count; i++)
			{
				NodeLink2 nodeLink = components[i];
				Transform startTransform = nodeLink.StartTransform;
				Transform endTransform = nodeLink.EndTransform;
				bool flag = nodeLink.linkSource != null;
				if ((startTransform != null && endTransform != null) != flag || (flag && (startTransform.hasChanged || endTransform.hasChanged)))
				{
					if (startTransform != null)
					{
						startTransform.hasChanged = false;
					}
					if (endTransform != null)
					{
						endTransform.hasChanged = false;
					}
					nodeLink.RemoveLink();
					nodeLink.TryAddLink();
				}
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00014800 File Offset: 0x00012A00
		private void TryAddLink()
		{
			if (this.linkSource != null && (this.linkSource.status == OffMeshLinks.OffMeshLinkStatus.Inactive || (this.linkSource.status & OffMeshLinks.OffMeshLinkStatus.PendingRemoval) != (OffMeshLinks.OffMeshLinkStatus)0))
			{
				this.linkSource = null;
			}
			if (this.linkSource == null && AstarPath.active != null && this.EndTransform != null)
			{
				this.StartTransform.hasChanged = false;
				this.EndTransform.hasChanged = false;
				this.linkSource = new OffMeshLinks.OffMeshLinkSource
				{
					start = new OffMeshLinks.Anchor
					{
						center = this.StartTransform.position,
						rotation = this.StartTransform.rotation,
						width = 0f
					},
					end = new OffMeshLinks.Anchor
					{
						center = this.EndTransform.position,
						rotation = this.EndTransform.rotation,
						width = 0f
					},
					directionality = (this.oneWay ? OffMeshLinks.Directionality.OneWay : OffMeshLinks.Directionality.TwoWay),
					tag = this.pathfindingTag,
					costFactor = this.costFactor,
					graphMask = this.graphMask,
					maxSnappingDistance = 1f,
					component = this,
					handler = this.onTraverseOffMeshLink
				};
				AstarPath.active.offMeshLinks.Add(this.linkSource);
			}
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00014970 File Offset: 0x00012B70
		private void RemoveLink()
		{
			if (AstarPath.active != null && this.linkSource != null)
			{
				AstarPath.active.offMeshLinks.Remove(this.linkSource);
			}
			this.linkSource = null;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x000149A3 File Offset: 0x00012BA3
		protected override void OnDisable()
		{
			base.OnDisable();
			BatchedEvents.Remove<NodeLink2>(this);
			this.RemoveLink();
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x000149B7 File Offset: 0x00012BB7
		[ContextMenu("Recalculate neighbours")]
		private void ContextApplyForce()
		{
			this.Apply();
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x000149BF File Offset: 0x00012BBF
		public virtual void Apply()
		{
			this.RemoveLink();
			this.TryAddLink();
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x000149D0 File Offset: 0x00012BD0
		public override void DrawGizmos()
		{
			if (this.StartTransform == null || this.EndTransform == null)
			{
				return;
			}
			Vector3 position = this.StartTransform.position;
			Vector3 position2 = this.EndTransform.position;
			if (this.linkSource != null && Time.renderedFrameCount % 16 == 0 && Application.isEditor && (this.linkSource.start.center != position || this.linkSource.end.center != position2 || this.linkSource.directionality != (this.oneWay ? OffMeshLinks.Directionality.OneWay : OffMeshLinks.Directionality.TwoWay) || this.linkSource.costFactor != this.costFactor || this.linkSource.graphMask != this.graphMask || this.linkSource.tag != this.pathfindingTag))
			{
				this.Apply();
			}
			bool flag = GizmoContext.InActiveSelection(this);
			List<NavGraph> list = ((this.linkSource != null && AstarPath.active != null) ? AstarPath.active.offMeshLinks.ConnectedGraphs(this.linkSource) : null);
			Vector3 vector = Vector3.up;
			if (list != null)
			{
				for (int i = 0; i < list.Count; i++)
				{
					NavGraph navGraph = list[i];
					if (navGraph != null)
					{
						NavmeshBase navmeshBase = navGraph as NavmeshBase;
						if (navmeshBase != null)
						{
							vector = navmeshBase.transform.WorldUpAtGraphPosition(Vector3.zero);
							break;
						}
						GridGraph gridGraph = navGraph as GridGraph;
						if (gridGraph != null)
						{
							vector = gridGraph.transform.WorldUpAtGraphPosition(Vector3.zero);
							break;
						}
					}
				}
				ListPool<NavGraph>.Release(ref list);
			}
			bool flag2 = this.linkSource != null && this.linkSource.status == OffMeshLinks.OffMeshLinkStatus.Active;
			Color color = (flag ? NodeLink2.GizmosColorSelected : NodeLink2.GizmosColor);
			if (flag2)
			{
				color = Color.green;
			}
			Draw.Circle(position, vector, 0.4f, (this.linkSource != null && this.linkSource.status.HasFlag(OffMeshLinks.OffMeshLinkStatus.FailedToConnectStart)) ? Color.red : color);
			Draw.Circle(position2, vector, 0.4f, (this.linkSource != null && this.linkSource.status.HasFlag(OffMeshLinks.OffMeshLinkStatus.FailedToConnectEnd)) ? Color.red : color);
			NodeLink.DrawArch(position, position2, vector, color);
			if (flag)
			{
				Vector3 normalized = Vector3.Cross(vector, position2 - position).normalized;
				using (Draw.WithLineWidth(2f, true))
				{
					NodeLink.DrawArch(position + normalized * 0f, position2 + normalized * 0f, vector, color);
				}
			}
		}

		// Token: 0x0400029E RID: 670
		public Transform end;

		// Token: 0x0400029F RID: 671
		public float costFactor = 1f;

		// Token: 0x040002A0 RID: 672
		public bool oneWay;

		// Token: 0x040002A1 RID: 673
		public PathfindingTag pathfindingTag = 0U;

		// Token: 0x040002A2 RID: 674
		public GraphMask graphMask = -1;

		// Token: 0x040002A3 RID: 675
		protected OffMeshLinks.OffMeshLinkSource linkSource;

		// Token: 0x040002A4 RID: 676
		private IOffMeshLinkHandler onTraverseOffMeshLinkHandler;

		// Token: 0x040002A5 RID: 677
		private static readonly Color GizmosColor = new Color(0.80784315f, 0.53333336f, 0.1882353f, 0.5f);

		// Token: 0x040002A6 RID: 678
		private static readonly Color GizmosColorSelected = new Color(0.92156863f, 0.48235294f, 0.1254902f, 1f);
	}
}
