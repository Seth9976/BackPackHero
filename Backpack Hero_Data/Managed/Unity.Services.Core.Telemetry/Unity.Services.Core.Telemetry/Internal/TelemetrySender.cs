using System;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Unity.Services.Core.Scheduler.Internal;
using UnityEngine.Networking;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x0200001C RID: 28
	internal class TelemetrySender
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003044 File Offset: 0x00001244
		public string TargetUrl { get; }

		// Token: 0x06000066 RID: 102 RVA: 0x0000304C File Offset: 0x0000124C
		public TelemetrySender([NotNull] string targetUrl, [NotNull] string servicePath, [NotNull] IActionScheduler scheduler, [NotNull] ExponentialBackOffRetryPolicy retryPolicy, [NotNull] IUnityWebRequestSender requestSender)
		{
			this.TargetUrl = targetUrl + "/" + servicePath;
			this.m_RetryPolicy = retryPolicy;
			this.m_Scheduler = scheduler;
			this.m_RequestSender = requestSender;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003080 File Offset: 0x00001280
		public Task SendAsync<TPayload>(TPayload payload) where TPayload : ITelemetryPayload
		{
			TelemetrySender.<>c__DisplayClass7_0<TPayload> CS$<>8__locals1 = new TelemetrySender.<>c__DisplayClass7_0<TPayload>();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.completionSource = new TaskCompletionSource<object>();
			CS$<>8__locals1.sendCount = 0;
			try
			{
				CS$<>8__locals1.serializedPayload = TelemetrySender.SerializePayload<TPayload>(payload);
				CS$<>8__locals1.<SendAsync>g__SendWebRequest|0();
			}
			catch (Exception ex)
			{
				CS$<>8__locals1.completionSource.TrySetException(ex);
			}
			return CS$<>8__locals1.completionSource.Task;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000030EC File Offset: 0x000012EC
		internal static byte[] SerializePayload<TPayload>(TPayload payload) where TPayload : ITelemetryPayload
		{
			string text = JsonConvert.SerializeObject(payload);
			return Encoding.UTF8.GetBytes(text);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003110 File Offset: 0x00001310
		internal UnityWebRequest CreateRequest(byte[] serializedPayload)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(this.TargetUrl, "POST");
			unityWebRequest.uploadHandler = new UploadHandlerRaw(serializedPayload)
			{
				contentType = "application/json"
			};
			unityWebRequest.downloadHandler = new DownloadHandlerBuffer();
			unityWebRequest.SetRequestHeader("Content-Type", "application/json");
			return unityWebRequest;
		}

		// Token: 0x04000032 RID: 50
		private readonly ExponentialBackOffRetryPolicy m_RetryPolicy;

		// Token: 0x04000033 RID: 51
		private readonly IActionScheduler m_Scheduler;

		// Token: 0x04000034 RID: 52
		private readonly IUnityWebRequestSender m_RequestSender;
	}
}
