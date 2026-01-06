using System;
using UnityEngine.Networking;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x0200001B RID: 27
	internal interface IUnityWebRequestSender
	{
		// Token: 0x06000064 RID: 100
		void SendRequest(UnityWebRequest request, Action<WebRequest> callback);
	}
}
