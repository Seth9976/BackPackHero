using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Unity.Services.Core.Configuration
{
	// Token: 0x0200000C RID: 12
	internal static class StreamingAssetsUtils
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002440 File Offset: 0x00000640
		public static Task<string> GetFileTextFromStreamingAssetsAsync(string path)
		{
			string text = Path.Combine(Application.streamingAssetsPath, path);
			TaskCompletionSource<string> taskCompletionSource = new TaskCompletionSource<string>();
			try
			{
				string text2 = File.ReadAllText(text);
				taskCompletionSource.SetResult(text2);
			}
			catch (Exception ex)
			{
				taskCompletionSource.SetException(ex);
			}
			return taskCompletionSource.Task;
		}
	}
}
