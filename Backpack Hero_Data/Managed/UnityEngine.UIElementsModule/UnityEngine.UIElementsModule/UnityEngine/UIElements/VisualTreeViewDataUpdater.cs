using System;
using System.Collections.Generic;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x02000101 RID: 257
	internal class VisualTreeViewDataUpdater : BaseVisualTreeUpdater
	{
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x0001CF53 File Offset: 0x0001B153
		public override ProfilerMarker profilerMarker
		{
			get
			{
				return VisualTreeViewDataUpdater.s_ProfilerMarker;
			}
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0001CF5C File Offset: 0x0001B15C
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			bool flag = (versionChangeType & VersionChangeType.ViewData) != VersionChangeType.ViewData;
			if (!flag)
			{
				this.m_Version += 1U;
				this.m_UpdateList.Add(ve);
				this.PropagateToParents(ve);
			}
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0001CF9C File Offset: 0x0001B19C
		public override void Update()
		{
			bool flag = this.m_Version == this.m_LastVersion;
			if (!flag)
			{
				int num = 0;
				while (this.m_LastVersion != this.m_Version)
				{
					this.m_LastVersion = this.m_Version;
					this.ValidateViewDataOnSubTree(base.visualTree, true);
					num++;
					bool flag2 = num > 5;
					if (flag2)
					{
						string text = "UIElements: Too many children recursively added that rely on persistent view data: ";
						VisualElement visualTree = base.visualTree;
						Debug.LogError(text + ((visualTree != null) ? visualTree.ToString() : null));
						break;
					}
				}
				this.m_UpdateList.Clear();
				this.m_ParentList.Clear();
			}
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0001D03C File Offset: 0x0001B23C
		private void ValidateViewDataOnSubTree(VisualElement ve, bool enablePersistence)
		{
			enablePersistence = ve.IsViewDataPersitenceSupportedOnChildren(enablePersistence);
			bool flag = this.m_UpdateList.Contains(ve);
			if (flag)
			{
				this.m_UpdateList.Remove(ve);
				ve.OnViewDataReady(enablePersistence);
			}
			bool flag2 = this.m_ParentList.Contains(ve);
			if (flag2)
			{
				this.m_ParentList.Remove(ve);
				int childCount = ve.hierarchy.childCount;
				for (int i = 0; i < childCount; i++)
				{
					this.ValidateViewDataOnSubTree(ve.hierarchy[i], enablePersistence);
				}
			}
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0001D0D8 File Offset: 0x0001B2D8
		private void PropagateToParents(VisualElement ve)
		{
			for (VisualElement visualElement = ve.hierarchy.parent; visualElement != null; visualElement = visualElement.hierarchy.parent)
			{
				bool flag = !this.m_ParentList.Add(visualElement);
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x04000345 RID: 837
		private HashSet<VisualElement> m_UpdateList = new HashSet<VisualElement>();

		// Token: 0x04000346 RID: 838
		private HashSet<VisualElement> m_ParentList = new HashSet<VisualElement>();

		// Token: 0x04000347 RID: 839
		private const int kMaxValidatePersistentDataCount = 5;

		// Token: 0x04000348 RID: 840
		private uint m_Version = 0U;

		// Token: 0x04000349 RID: 841
		private uint m_LastVersion = 0U;

		// Token: 0x0400034A RID: 842
		private static readonly string s_Description = "Update ViewData";

		// Token: 0x0400034B RID: 843
		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(VisualTreeViewDataUpdater.s_Description);
	}
}
