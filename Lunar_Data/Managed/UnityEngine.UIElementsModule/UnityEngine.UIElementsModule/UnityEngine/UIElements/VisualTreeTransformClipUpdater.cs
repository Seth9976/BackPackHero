using System;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x020000FB RID: 251
	internal class VisualTreeTransformClipUpdater : BaseVisualTreeUpdater
	{
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x0001C9E1 File Offset: 0x0001ABE1
		public override ProfilerMarker profilerMarker
		{
			get
			{
				return VisualTreeTransformClipUpdater.s_ProfilerMarker;
			}
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0001C9E8 File Offset: 0x0001ABE8
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			bool flag = (versionChangeType & (VersionChangeType.Hierarchy | VersionChangeType.Overflow | VersionChangeType.BorderWidth | VersionChangeType.Transform | VersionChangeType.Size)) == (VersionChangeType)0;
			if (!flag)
			{
				bool flag2 = (versionChangeType & VersionChangeType.Transform) > (VersionChangeType)0;
				bool flag3 = (versionChangeType & (VersionChangeType.Overflow | VersionChangeType.BorderWidth | VersionChangeType.Transform | VersionChangeType.Size)) > (VersionChangeType)0;
				flag2 = flag2 && !ve.isWorldTransformDirty;
				flag3 = flag3 && !ve.isWorldClipDirty;
				bool flag4 = flag2 || flag3;
				if (flag4)
				{
					VisualTreeTransformClipUpdater.DirtyHierarchy(ve, flag2, flag3);
				}
				VisualTreeTransformClipUpdater.DirtyBoundingBoxHierarchy(ve);
				this.m_Version += 1U;
			}
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0001CA64 File Offset: 0x0001AC64
		private static void DirtyHierarchy(VisualElement ve, bool mustDirtyWorldTransform, bool mustDirtyWorldClip)
		{
			if (mustDirtyWorldTransform)
			{
				ve.isWorldTransformDirty = true;
				ve.isWorldBoundingBoxDirty = true;
			}
			if (mustDirtyWorldClip)
			{
				ve.isWorldClipDirty = true;
			}
			int childCount = ve.hierarchy.childCount;
			for (int i = 0; i < childCount; i++)
			{
				VisualElement visualElement = ve.hierarchy[i];
				bool flag = (mustDirtyWorldTransform && !visualElement.isWorldTransformDirty) || (mustDirtyWorldClip && !visualElement.isWorldClipDirty);
				if (flag)
				{
					VisualTreeTransformClipUpdater.DirtyHierarchy(visualElement, mustDirtyWorldTransform, mustDirtyWorldClip);
				}
			}
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0001CB00 File Offset: 0x0001AD00
		private static void DirtyBoundingBoxHierarchy(VisualElement ve)
		{
			ve.isBoundingBoxDirty = true;
			ve.isWorldBoundingBoxDirty = true;
			VisualElement visualElement = ve.hierarchy.parent;
			while (visualElement != null && !visualElement.isBoundingBoxDirty)
			{
				visualElement.isBoundingBoxDirty = true;
				visualElement.isWorldBoundingBoxDirty = true;
				visualElement = visualElement.hierarchy.parent;
			}
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0001CB64 File Offset: 0x0001AD64
		public override void Update()
		{
			bool flag = this.m_Version == this.m_LastVersion;
			if (!flag)
			{
				this.m_LastVersion = this.m_Version;
				base.panel.UpdateElementUnderPointers();
				base.panel.visualTree.UpdateBoundingBox();
			}
		}

		// Token: 0x04000333 RID: 819
		private uint m_Version = 0U;

		// Token: 0x04000334 RID: 820
		private uint m_LastVersion = 0U;

		// Token: 0x04000335 RID: 821
		private static readonly string s_Description = "Update Transform";

		// Token: 0x04000336 RID: 822
		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(VisualTreeTransformClipUpdater.s_Description);
	}
}
