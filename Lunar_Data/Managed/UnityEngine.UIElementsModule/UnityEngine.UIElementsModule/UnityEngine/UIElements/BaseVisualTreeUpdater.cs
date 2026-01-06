using System;
using System.Diagnostics;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x02000100 RID: 256
	internal abstract class BaseVisualTreeUpdater : IVisualTreeUpdater, IDisposable
	{
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060007DE RID: 2014 RVA: 0x0001CE6C File Offset: 0x0001B06C
		// (remove) Token: 0x060007DF RID: 2015 RVA: 0x0001CEA4 File Offset: 0x0001B0A4
		[field: DebuggerBrowsable(0)]
		public event Action<BaseVisualElementPanel> panelChanged;

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x0001CEDC File Offset: 0x0001B0DC
		// (set) Token: 0x060007E1 RID: 2017 RVA: 0x0001CEF4 File Offset: 0x0001B0F4
		public BaseVisualElementPanel panel
		{
			get
			{
				return this.m_Panel;
			}
			set
			{
				this.m_Panel = value;
				bool flag = this.panelChanged != null;
				if (flag)
				{
					this.panelChanged.Invoke(value);
				}
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x0001CF24 File Offset: 0x0001B124
		public VisualElement visualTree
		{
			get
			{
				return this.panel.visualTree;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060007E3 RID: 2019
		public abstract ProfilerMarker profilerMarker { get; }

		// Token: 0x060007E4 RID: 2020 RVA: 0x0001CF41 File Offset: 0x0001B141
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x000020E6 File Offset: 0x000002E6
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x060007E6 RID: 2022
		public abstract void Update();

		// Token: 0x060007E7 RID: 2023
		public abstract void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType);

		// Token: 0x04000344 RID: 836
		private BaseVisualElementPanel m_Panel;
	}
}
