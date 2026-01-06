using System;

namespace System.Diagnostics
{
	// Token: 0x02000285 RID: 645
	internal class TraceSourceInfo
	{
		// Token: 0x06001466 RID: 5222 RVA: 0x00053204 File Offset: 0x00051404
		public TraceSourceInfo(string name, SourceLevels levels)
		{
			this.name = name;
			this.levels = levels;
			this.listeners = new TraceListenerCollection();
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x00053225 File Offset: 0x00051425
		internal TraceSourceInfo(string name, SourceLevels levels, TraceImplSettings settings)
		{
			this.name = name;
			this.levels = levels;
			this.listeners = new TraceListenerCollection();
			this.listeners.Add(new DefaultTraceListener
			{
				IndentSize = settings.IndentSize
			});
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x00053263 File Offset: 0x00051463
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x0005326B File Offset: 0x0005146B
		public SourceLevels Levels
		{
			get
			{
				return this.levels;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x00053273 File Offset: 0x00051473
		public TraceListenerCollection Listeners
		{
			get
			{
				return this.listeners;
			}
		}

		// Token: 0x04000B8A RID: 2954
		private string name;

		// Token: 0x04000B8B RID: 2955
		private SourceLevels levels;

		// Token: 0x04000B8C RID: 2956
		private TraceListenerCollection listeners;
	}
}
