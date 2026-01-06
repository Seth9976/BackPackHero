using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Core.Telemetry.Internal;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000046 RID: 70
	internal class CoreDiagnostics
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000037C3 File Offset: 0x000019C3
		// (set) Token: 0x06000131 RID: 305 RVA: 0x000037CA File Offset: 0x000019CA
		public static CoreDiagnostics Instance { get; internal set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000037D2 File Offset: 0x000019D2
		public IDictionary<string, string> CoreTags { get; } = new Dictionary<string, string>();

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000037DA File Offset: 0x000019DA
		// (set) Token: 0x06000134 RID: 308 RVA: 0x000037E2 File Offset: 0x000019E2
		internal IDiagnosticsComponentProvider DiagnosticsComponentProvider { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000135 RID: 309 RVA: 0x000037EB File Offset: 0x000019EB
		// (set) Token: 0x06000136 RID: 310 RVA: 0x000037F3 File Offset: 0x000019F3
		internal IDiagnostics Diagnostics { get; set; }

		// Token: 0x06000137 RID: 311 RVA: 0x000037FC File Offset: 0x000019FC
		public void SetProjectConfiguration(string serializedProjectConfig)
		{
			this.CoreTags["project_config"] = serializedProjectConfig;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000380F File Offset: 0x00001A0F
		public void SendCircularDependencyDiagnostics(Exception exception)
		{
			this.SendCoreDiagnosticsAsync("circular_dependency", exception).ContinueWith(new Action<Task>(CoreDiagnostics.OnSendFailed), TaskContinuationOptions.OnlyOnFaulted);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00003834 File Offset: 0x00001A34
		public void SendCorePackageInitDiagnostics(Exception exception)
		{
			this.SendCoreDiagnosticsAsync("core_package_init", exception).ContinueWith(new Action<Task>(CoreDiagnostics.OnSendFailed), TaskContinuationOptions.OnlyOnFaulted);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00003859 File Offset: 0x00001A59
		public void SendOperateServicesInitDiagnostics(Exception exception)
		{
			this.SendCoreDiagnosticsAsync("operate_services_init", exception).ContinueWith(new Action<Task>(CoreDiagnostics.OnSendFailed), TaskContinuationOptions.OnlyOnFaulted);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00003880 File Offset: 0x00001A80
		internal async Task SendCoreDiagnosticsAsync(string diagnosticName, Exception exception)
		{
			IDiagnostics diagnostics = await this.GetOrCreateDiagnosticsAsync();
			if (diagnostics != null)
			{
				diagnostics.SendDiagnostic(diagnosticName, (exception != null) ? exception.ToString() : null, this.CoreTags);
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000038D3 File Offset: 0x00001AD3
		private static void OnSendFailed(Task failedSendTask)
		{
			CoreLogger.LogException(failedSendTask.Exception);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000038E0 File Offset: 0x00001AE0
		internal async Task<IDiagnostics> GetOrCreateDiagnosticsAsync()
		{
			IDiagnostics diagnostics;
			if (this.Diagnostics != null)
			{
				diagnostics = this.Diagnostics;
			}
			else if (this.DiagnosticsComponentProvider == null)
			{
				diagnostics = null;
			}
			else
			{
				IDiagnosticsFactory diagnosticsFactory = await this.DiagnosticsComponentProvider.CreateDiagnosticsComponents();
				this.Diagnostics = diagnosticsFactory.Create("com.unity.services.core");
				this.SetProjectConfiguration(await this.DiagnosticsComponentProvider.GetSerializedProjectConfigurationAsync());
				diagnostics = this.Diagnostics;
			}
			return diagnostics;
		}

		// Token: 0x0400004D RID: 77
		internal const string CorePackageName = "com.unity.services.core";

		// Token: 0x0400004E RID: 78
		internal const string CircularDependencyDiagnosticName = "circular_dependency";

		// Token: 0x0400004F RID: 79
		internal const string CorePackageInitDiagnosticName = "core_package_init";

		// Token: 0x04000050 RID: 80
		internal const string OperateServicesInitDiagnosticName = "operate_services_init";

		// Token: 0x04000051 RID: 81
		internal const string ProjectConfigTagName = "project_config";
	}
}
