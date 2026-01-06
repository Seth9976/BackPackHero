using System;

namespace Unity.Profiling
{
	// Token: 0x0200004E RID: 78
	internal sealed class ProfilerRecorderDebugView
	{
		// Token: 0x0600011B RID: 283 RVA: 0x00002F9F File Offset: 0x0000119F
		public ProfilerRecorderDebugView(ProfilerRecorder r)
		{
			this.m_Recorder = r;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00002FB0 File Offset: 0x000011B0
		public ProfilerRecorderSample[] Items
		{
			get
			{
				return this.m_Recorder.ToArray();
			}
		}

		// Token: 0x0400013A RID: 314
		private ProfilerRecorder m_Recorder;
	}
}
