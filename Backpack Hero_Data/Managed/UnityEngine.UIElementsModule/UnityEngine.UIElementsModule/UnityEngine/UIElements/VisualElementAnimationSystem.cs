using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Profiling;
using UnityEngine.UIElements.Experimental;

namespace UnityEngine.UIElements
{
	// Token: 0x020000F2 RID: 242
	internal class VisualElementAnimationSystem : BaseVisualTreeUpdater
	{
		// Token: 0x06000789 RID: 1929 RVA: 0x0001B708 File Offset: 0x00019908
		private long CurrentTimeMs()
		{
			return Panel.TimeSinceStartupMs();
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x0001B71F File Offset: 0x0001991F
		public override ProfilerMarker profilerMarker
		{
			get
			{
				return VisualElementAnimationSystem.s_ProfilerMarker;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x0001B726 File Offset: 0x00019926
		private static ProfilerMarker stylePropertyAnimationProfilerMarker
		{
			get
			{
				return VisualElementAnimationSystem.s_StylePropertyAnimationProfilerMarker;
			}
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001B72D File Offset: 0x0001992D
		public void UnregisterAnimation(IValueAnimationUpdate anim)
		{
			this.m_Animations.Remove(anim);
			this.m_IterationListDirty = true;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001B744 File Offset: 0x00019944
		public void UnregisterAnimations(List<IValueAnimationUpdate> anims)
		{
			foreach (IValueAnimationUpdate valueAnimationUpdate in anims)
			{
				this.m_Animations.Remove(valueAnimationUpdate);
			}
			this.m_IterationListDirty = true;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001B7A4 File Offset: 0x000199A4
		public void RegisterAnimation(IValueAnimationUpdate anim)
		{
			this.m_Animations.Add(anim);
			this.m_HasNewAnimations = true;
			this.m_IterationListDirty = true;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0001B7C4 File Offset: 0x000199C4
		public void RegisterAnimations(List<IValueAnimationUpdate> anims)
		{
			foreach (IValueAnimationUpdate valueAnimationUpdate in anims)
			{
				this.m_Animations.Add(valueAnimationUpdate);
			}
			this.m_HasNewAnimations = true;
			this.m_IterationListDirty = true;
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0001B82C File Offset: 0x00019A2C
		public override void Update()
		{
			long num = Panel.TimeSinceStartupMs();
			bool iterationListDirty = this.m_IterationListDirty;
			if (iterationListDirty)
			{
				this.m_IterationList = Enumerable.ToList<IValueAnimationUpdate>(this.m_Animations);
				this.m_IterationListDirty = false;
			}
			bool flag = this.m_HasNewAnimations || this.lastUpdate != num;
			if (flag)
			{
				foreach (IValueAnimationUpdate valueAnimationUpdate in this.m_IterationList)
				{
					valueAnimationUpdate.Tick(num);
				}
				this.m_HasNewAnimations = false;
				this.lastUpdate = num;
			}
			IStylePropertyAnimationSystem styleAnimationSystem = base.panel.styleAnimationSystem;
			using (VisualElementAnimationSystem.stylePropertyAnimationProfilerMarker.Auto())
			{
				styleAnimationSystem.Update();
			}
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x000020E6 File Offset: 0x000002E6
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
		}

		// Token: 0x04000306 RID: 774
		private HashSet<IValueAnimationUpdate> m_Animations = new HashSet<IValueAnimationUpdate>();

		// Token: 0x04000307 RID: 775
		private List<IValueAnimationUpdate> m_IterationList = new List<IValueAnimationUpdate>();

		// Token: 0x04000308 RID: 776
		private bool m_HasNewAnimations = false;

		// Token: 0x04000309 RID: 777
		private bool m_IterationListDirty = false;

		// Token: 0x0400030A RID: 778
		private static readonly string s_Description = "Animation Update";

		// Token: 0x0400030B RID: 779
		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(VisualElementAnimationSystem.s_Description);

		// Token: 0x0400030C RID: 780
		private static readonly string s_StylePropertyAnimationDescription = "StylePropertyAnimation Update";

		// Token: 0x0400030D RID: 781
		private static readonly ProfilerMarker s_StylePropertyAnimationProfilerMarker = new ProfilerMarker(VisualElementAnimationSystem.s_StylePropertyAnimationDescription);

		// Token: 0x0400030E RID: 782
		private long lastUpdate;
	}
}
