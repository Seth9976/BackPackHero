using System;
using System.IO;
using Newtonsoft.Json;
using Unity.Services.Core.Internal;
using UnityEngine;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000004 RID: 4
	internal class FileCachePersister<TPayload> : FileCachePersister, ICachePersister<TPayload> where TPayload : ITelemetryPayload
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000020A9 File Offset: 0x000002A9
		public FileCachePersister(string fileName, CoreDiagnostics diagnostics)
		{
			this.FilePath = Path.Combine(FileCachePersister.GetPersistentDataPathFor(Application.platform), fileName);
			this.m_Diagnostics = diagnostics;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020DE File Offset: 0x000002DE
		public string FilePath { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020E6 File Offset: 0x000002E6
		public bool CanPersist { get; } = FileCachePersister.IsAvailableFor(Application.platform);

		// Token: 0x0600000C RID: 12 RVA: 0x000020F0 File Offset: 0x000002F0
		public void Persist(CachedPayload<TPayload> cache)
		{
			if (cache.IsEmpty<TPayload>())
			{
				return;
			}
			try
			{
				string text = JsonConvert.SerializeObject(cache);
				File.WriteAllText(this.FilePath, text);
			}
			catch (IOException ex) when (TelemetryUtils.LogTelemetryException(ex, false))
			{
				IOException ex2 = new IOException("This exception is most likely caused by a multiple instance file sharing violation.", ex);
				this.m_Diagnostics.SendCoreDiagnosticsAsync("telemetry_cache_file_multiple_instances_exception", ex2);
			}
			catch (Exception ex3) when (TelemetryUtils.LogTelemetryException(ex3, true))
			{
				this.m_Diagnostics.SendCoreDiagnosticsAsync("telemetry_cache_file_exception", ex3);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021A0 File Offset: 0x000003A0
		public bool TryFetch(out CachedPayload<TPayload> persistedCache)
		{
			persistedCache = null;
			if (!File.Exists(this.FilePath))
			{
				return false;
			}
			bool flag;
			try
			{
				string text = File.ReadAllText(this.FilePath);
				persistedCache = JsonConvert.DeserializeObject<CachedPayload<TPayload>>(text);
				flag = persistedCache != null;
			}
			catch (IOException ex)
			{
				IOException ex2 = new IOException("This exception is most likely caused by a multiple instance file sharing violation.", ex);
				this.m_Diagnostics.SendCoreDiagnosticsAsync("telemetry_cache_file_multiple_instances_exception", ex2);
				flag = false;
			}
			catch (Exception ex3) when (TelemetryUtils.LogTelemetryException(ex3, true))
			{
				this.m_Diagnostics.SendCoreDiagnosticsAsync("telemetry_cache_file_exception", ex3);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002250 File Offset: 0x00000450
		public void Delete()
		{
			if (!File.Exists(this.FilePath))
			{
				return;
			}
			try
			{
				File.Delete(this.FilePath);
			}
			catch (IOException ex)
			{
				IOException ex2 = new IOException("This exception is most likely caused by a multiple instance file sharing violation.", ex);
				this.m_Diagnostics.SendCoreDiagnosticsAsync("telemetry_cache_file_multiple_instances_exception", ex2);
			}
			catch (Exception ex3) when (TelemetryUtils.LogTelemetryException(ex3, true))
			{
				this.m_Diagnostics.SendCoreDiagnosticsAsync("telemetry_cache_file_exception", ex3);
			}
		}

		// Token: 0x04000002 RID: 2
		private const string k_MultipleInstanceDiagnosticsName = "telemetry_cache_file_multiple_instances_exception";

		// Token: 0x04000003 RID: 3
		private const string k_CacheFileException = "telemetry_cache_file_exception";

		// Token: 0x04000004 RID: 4
		private const string k_MultipleInstanceError = "This exception is most likely caused by a multiple instance file sharing violation.";

		// Token: 0x04000005 RID: 5
		private readonly CoreDiagnostics m_Diagnostics;
	}
}
