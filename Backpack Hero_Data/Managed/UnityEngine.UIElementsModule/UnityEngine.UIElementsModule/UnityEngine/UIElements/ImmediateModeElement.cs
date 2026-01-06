using System;
using System.Collections.Generic;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x0200003F RID: 63
	public abstract class ImmediateModeElement : VisualElement
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000806C File Offset: 0x0000626C
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00008084 File Offset: 0x00006284
		public bool cullingEnabled
		{
			get
			{
				return this.m_CullingEnabled;
			}
			set
			{
				this.m_CullingEnabled = value;
				base.IncrementVersion(VersionChangeType.Repaint);
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000809C File Offset: 0x0000629C
		public ImmediateModeElement()
		{
			base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
			Type type = base.GetType();
			bool flag = !ImmediateModeElement.s_Markers.TryGetValue(type, ref this.m_ImmediateRepaintMarker);
			if (flag)
			{
				this.m_ImmediateRepaintMarker = new ProfilerMarker(base.typeName + ".ImmediateRepaint");
				ImmediateModeElement.s_Markers[type] = this.m_ImmediateRepaintMarker;
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00008129 File Offset: 0x00006329
		private void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			mgc.painter.DrawImmediate(new Action(this.CallImmediateRepaint), this.cullingEnabled);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000814C File Offset: 0x0000634C
		private void CallImmediateRepaint()
		{
			using (this.m_ImmediateRepaintMarker.Auto())
			{
				this.ImmediateRepaint();
			}
		}

		// Token: 0x0600019D RID: 413
		protected abstract void ImmediateRepaint();

		// Token: 0x040000B7 RID: 183
		private static readonly Dictionary<Type, ProfilerMarker> s_Markers = new Dictionary<Type, ProfilerMarker>();

		// Token: 0x040000B8 RID: 184
		private readonly ProfilerMarker m_ImmediateRepaintMarker;

		// Token: 0x040000B9 RID: 185
		private bool m_CullingEnabled = false;
	}
}
