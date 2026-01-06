using System;
using System.Collections.Generic;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000006 RID: 6
	internal class Diagnostics : IDiagnostics
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000022E4 File Offset: 0x000004E4
		internal DiagnosticsHandler Handler { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000022EC File Offset: 0x000004EC
		internal IDictionary<string, string> PackageTags { get; }

		// Token: 0x06000015 RID: 21 RVA: 0x000022F4 File Offset: 0x000004F4
		public Diagnostics(DiagnosticsHandler handler, IDictionary<string, string> packageTags)
		{
			this.Handler = handler;
			this.PackageTags = packageTags;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000230C File Offset: 0x0000050C
		public void SendDiagnostic(string name, string message, IDictionary<string, string> tags = null)
		{
			Diagnostic diagnostic = new Diagnostic
			{
				Content = ((tags == null) ? new Dictionary<string, string>(this.PackageTags) : new Dictionary<string, string>(tags).MergeAllowOverride(this.PackageTags))
			};
			diagnostic.Content.Add("name", name);
			if (message != null && message.Length > 10000)
			{
				message = message.Substring(0, 10000) + Environment.NewLine + "[truncated]";
			}
			diagnostic.Content.Add("message", message);
			this.Handler.Register(diagnostic);
		}

		// Token: 0x04000008 RID: 8
		internal const int MaxDiagnosticMessageLength = 10000;

		// Token: 0x04000009 RID: 9
		internal const string DiagnosticMessageTruncateSuffix = "[truncated]";
	}
}
