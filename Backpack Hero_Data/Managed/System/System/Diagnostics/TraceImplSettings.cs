using System;

namespace System.Diagnostics
{
	// Token: 0x02000284 RID: 644
	internal class TraceImplSettings
	{
		// Token: 0x06001465 RID: 5221 RVA: 0x000531CD File Offset: 0x000513CD
		public TraceImplSettings()
		{
			this.Listeners.Add(new DefaultTraceListener
			{
				IndentSize = this.IndentSize
			});
		}

		// Token: 0x04000B86 RID: 2950
		public const string Key = ".__TraceInfoSettingsKey__.";

		// Token: 0x04000B87 RID: 2951
		public bool AutoFlush;

		// Token: 0x04000B88 RID: 2952
		public int IndentSize = 4;

		// Token: 0x04000B89 RID: 2953
		public TraceListenerCollection Listeners = new TraceListenerCollection();
	}
}
