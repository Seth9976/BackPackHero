using System;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x020000FF RID: 255
	internal interface IVisualTreeUpdater : IDisposable
	{
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060007D9 RID: 2009
		// (set) Token: 0x060007DA RID: 2010
		BaseVisualElementPanel panel { get; set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060007DB RID: 2011
		ProfilerMarker profilerMarker { get; }

		// Token: 0x060007DC RID: 2012
		void Update();

		// Token: 0x060007DD RID: 2013
		void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType);
	}
}
