using System;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace Unity.Services.Core.Internal
{
	// Token: 0x0200004B RID: 75
	internal static class UnityWebRequestUtils
	{
		// Token: 0x0600015E RID: 350 RVA: 0x00003E94 File Offset: 0x00002094
		public static bool HasSucceeded(this UnityWebRequest self)
		{
			return self.result == UnityWebRequest.Result.Success;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00003EA0 File Offset: 0x000020A0
		public static Task<string> GetTextAsync(string uri)
		{
			UnityWebRequestUtils.<>c__DisplayClass2_0 CS$<>8__locals1 = new UnityWebRequestUtils.<>c__DisplayClass2_0();
			CS$<>8__locals1.completionSource = new TaskCompletionSource<string>();
			UnityWebRequest.Get(uri).SendWebRequest().completed += CS$<>8__locals1.<GetTextAsync>g__CompleteFetchTaskOnRequestCompleted|0;
			return CS$<>8__locals1.completionSource.Task;
		}

		// Token: 0x04000067 RID: 103
		public const string JsonContentType = "application/json";
	}
}
