using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking.Match
{
	// Token: 0x0200001C RID: 28
	[Obsolete("The matchmaker and relay feature will be removed in the future, minimal support will continue until this can be safely done.")]
	public class NetworkMatch : MonoBehaviour
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00004940 File Offset: 0x00002B40
		// (set) Token: 0x06000169 RID: 361 RVA: 0x00004958 File Offset: 0x00002B58
		public Uri baseUri
		{
			get
			{
				return this.m_BaseUri;
			}
			set
			{
				this.m_BaseUri = value;
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00003C81 File Offset: 0x00001E81
		[Obsolete("This function is not used any longer to interface with the matchmaker. Please set up your project by logging in through the editor connect dialog.", true)]
		[EditorBrowsable(1)]
		public void SetProgramAppID(AppID programAppID)
		{
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00004964 File Offset: 0x00002B64
		public Coroutine CreateMatch(string matchName, uint matchSize, bool matchAdvertise, string matchPassword, string publicClientAddress, string privateClientAddress, int eloScoreForMatch, int requestDomain, NetworkMatch.DataResponseDelegate<MatchInfo> callback)
		{
			bool flag = Application.platform == RuntimePlatform.WebGLPlayer;
			Coroutine coroutine;
			if (flag)
			{
				Debug.LogError("Matchmaking is not supported on WebGL player.");
				coroutine = null;
			}
			else
			{
				coroutine = this.CreateMatch(new CreateMatchRequest
				{
					name = matchName,
					size = matchSize,
					advertise = matchAdvertise,
					password = matchPassword,
					publicAddress = publicClientAddress,
					privateAddress = privateClientAddress,
					eloScore = eloScoreForMatch,
					domain = requestDomain
				}, callback);
			}
			return coroutine;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000049E8 File Offset: 0x00002BE8
		internal Coroutine CreateMatch(CreateMatchRequest req, NetworkMatch.DataResponseDelegate<MatchInfo> callback)
		{
			bool flag = callback == null;
			Coroutine coroutine;
			if (flag)
			{
				Debug.Log("callback supplied is null, aborting CreateMatch Request.");
				coroutine = null;
			}
			else
			{
				Uri uri = new Uri(this.baseUri, "/json/reply/CreateMatchRequest");
				string text = "MatchMakingClient Create :";
				Uri uri2 = uri;
				Debug.Log(text + ((uri2 != null) ? uri2.ToString() : null));
				WWWForm wwwform = new WWWForm();
				wwwform.AddField("version", Request.currentVersion);
				wwwform.AddField("projectId", Application.cloudProjectId);
				wwwform.AddField("sourceId", Utility.GetSourceID().ToString());
				wwwform.AddField("accessTokenString", 0);
				wwwform.AddField("domain", req.domain);
				wwwform.AddField("name", req.name);
				wwwform.AddField("size", req.size.ToString());
				wwwform.AddField("advertise", req.advertise.ToString());
				wwwform.AddField("password", req.password);
				wwwform.AddField("publicAddress", req.publicAddress);
				wwwform.AddField("privateAddress", req.privateAddress);
				wwwform.AddField("eloScore", req.eloScore.ToString());
				wwwform.headers["Accept"] = "application/json";
				UnityWebRequest unityWebRequest = UnityWebRequest.Post(uri.ToString(), wwwform);
				coroutine = base.StartCoroutine(this.ProcessMatchResponse<CreateMatchResponse, NetworkMatch.DataResponseDelegate<MatchInfo>>(unityWebRequest, new NetworkMatch.InternalResponseDelegate<CreateMatchResponse, NetworkMatch.DataResponseDelegate<MatchInfo>>(this.OnMatchCreate), callback));
			}
			return coroutine;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00004B84 File Offset: 0x00002D84
		internal virtual void OnMatchCreate(CreateMatchResponse response, NetworkMatch.DataResponseDelegate<MatchInfo> userCallback)
		{
			bool success = response.success;
			if (success)
			{
				Utility.SetAccessTokenForNetwork((NetworkID)response.networkId, new NetworkAccessToken(response.accessTokenString));
			}
			userCallback(response.success, response.extendedInfo, new MatchInfo(response));
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00004BCC File Offset: 0x00002DCC
		public Coroutine JoinMatch(NetworkID netId, string matchPassword, string publicClientAddress, string privateClientAddress, int eloScoreForClient, int requestDomain, NetworkMatch.DataResponseDelegate<MatchInfo> callback)
		{
			return this.JoinMatch(new JoinMatchRequest
			{
				networkId = netId,
				password = matchPassword,
				publicAddress = publicClientAddress,
				privateAddress = privateClientAddress,
				eloScore = eloScoreForClient,
				domain = requestDomain
			}, callback);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00004C20 File Offset: 0x00002E20
		internal Coroutine JoinMatch(JoinMatchRequest req, NetworkMatch.DataResponseDelegate<MatchInfo> callback)
		{
			bool flag = callback == null;
			Coroutine coroutine;
			if (flag)
			{
				Debug.Log("callback supplied is null, aborting JoinMatch Request.");
				coroutine = null;
			}
			else
			{
				Uri uri = new Uri(this.baseUri, "/json/reply/JoinMatchRequest");
				string text = "MatchMakingClient Join :";
				Uri uri2 = uri;
				Debug.Log(text + ((uri2 != null) ? uri2.ToString() : null));
				WWWForm wwwform = new WWWForm();
				wwwform.AddField("version", Request.currentVersion);
				wwwform.AddField("projectId", Application.cloudProjectId);
				wwwform.AddField("sourceId", Utility.GetSourceID().ToString());
				wwwform.AddField("accessTokenString", 0);
				wwwform.AddField("domain", req.domain);
				wwwform.AddField("networkId", req.networkId.ToString());
				wwwform.AddField("password", req.password);
				wwwform.AddField("publicAddress", req.publicAddress);
				wwwform.AddField("privateAddress", req.privateAddress);
				wwwform.AddField("eloScore", req.eloScore.ToString());
				wwwform.headers["Accept"] = "application/json";
				UnityWebRequest unityWebRequest = UnityWebRequest.Post(uri.ToString(), wwwform);
				coroutine = base.StartCoroutine(this.ProcessMatchResponse<JoinMatchResponse, NetworkMatch.DataResponseDelegate<MatchInfo>>(unityWebRequest, new NetworkMatch.InternalResponseDelegate<JoinMatchResponse, NetworkMatch.DataResponseDelegate<MatchInfo>>(this.OnMatchJoined), callback));
			}
			return coroutine;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00004D94 File Offset: 0x00002F94
		internal void OnMatchJoined(JoinMatchResponse response, NetworkMatch.DataResponseDelegate<MatchInfo> userCallback)
		{
			bool success = response.success;
			if (success)
			{
				Utility.SetAccessTokenForNetwork((NetworkID)response.networkId, new NetworkAccessToken(response.accessTokenString));
			}
			userCallback(response.success, response.extendedInfo, new MatchInfo(response));
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00004DDC File Offset: 0x00002FDC
		public Coroutine DestroyMatch(NetworkID netId, int requestDomain, NetworkMatch.BasicResponseDelegate callback)
		{
			return this.DestroyMatch(new DestroyMatchRequest
			{
				networkId = netId,
				domain = requestDomain
			}, callback);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00004E0C File Offset: 0x0000300C
		internal Coroutine DestroyMatch(DestroyMatchRequest req, NetworkMatch.BasicResponseDelegate callback)
		{
			bool flag = callback == null;
			Coroutine coroutine;
			if (flag)
			{
				Debug.Log("callback supplied is null, aborting DestroyMatch Request.");
				coroutine = null;
			}
			else
			{
				Uri uri = new Uri(this.baseUri, "/json/reply/DestroyMatchRequest");
				string text = "MatchMakingClient Destroy :";
				Uri uri2 = uri;
				Debug.Log(text + ((uri2 != null) ? uri2.ToString() : null));
				WWWForm wwwform = new WWWForm();
				wwwform.AddField("version", Request.currentVersion);
				wwwform.AddField("projectId", Application.cloudProjectId);
				wwwform.AddField("sourceId", Utility.GetSourceID().ToString());
				wwwform.AddField("accessTokenString", Utility.GetAccessTokenForNetwork(req.networkId).GetByteString());
				wwwform.AddField("domain", req.domain);
				wwwform.AddField("networkId", req.networkId.ToString());
				wwwform.headers["Accept"] = "application/json";
				UnityWebRequest unityWebRequest = UnityWebRequest.Post(uri.ToString(), wwwform);
				coroutine = base.StartCoroutine(this.ProcessMatchResponse<BasicResponse, NetworkMatch.BasicResponseDelegate>(unityWebRequest, new NetworkMatch.InternalResponseDelegate<BasicResponse, NetworkMatch.BasicResponseDelegate>(this.OnMatchDestroyed), callback));
			}
			return coroutine;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00004F3E File Offset: 0x0000313E
		internal void OnMatchDestroyed(BasicResponse response, NetworkMatch.BasicResponseDelegate userCallback)
		{
			userCallback(response.success, response.extendedInfo);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00004F54 File Offset: 0x00003154
		public Coroutine DropConnection(NetworkID netId, NodeID dropNodeId, int requestDomain, NetworkMatch.BasicResponseDelegate callback)
		{
			return this.DropConnection(new DropConnectionRequest
			{
				networkId = netId,
				nodeId = dropNodeId,
				domain = requestDomain
			}, callback);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00004F8C File Offset: 0x0000318C
		internal Coroutine DropConnection(DropConnectionRequest req, NetworkMatch.BasicResponseDelegate callback)
		{
			bool flag = callback == null;
			Coroutine coroutine;
			if (flag)
			{
				Debug.Log("callback supplied is null, aborting DropConnection Request.");
				coroutine = null;
			}
			else
			{
				Uri uri = new Uri(this.baseUri, "/json/reply/DropConnectionRequest");
				string text = "MatchMakingClient DropConnection :";
				Uri uri2 = uri;
				Debug.Log(text + ((uri2 != null) ? uri2.ToString() : null));
				WWWForm wwwform = new WWWForm();
				wwwform.AddField("version", Request.currentVersion);
				wwwform.AddField("projectId", Application.cloudProjectId);
				wwwform.AddField("sourceId", Utility.GetSourceID().ToString());
				wwwform.AddField("accessTokenString", Utility.GetAccessTokenForNetwork(req.networkId).GetByteString());
				wwwform.AddField("domain", req.domain);
				wwwform.AddField("networkId", req.networkId.ToString());
				wwwform.AddField("nodeId", req.nodeId.ToString());
				wwwform.headers["Accept"] = "application/json";
				UnityWebRequest unityWebRequest = UnityWebRequest.Post(uri.ToString(), wwwform);
				coroutine = base.StartCoroutine(this.ProcessMatchResponse<DropConnectionResponse, NetworkMatch.BasicResponseDelegate>(unityWebRequest, new NetworkMatch.InternalResponseDelegate<DropConnectionResponse, NetworkMatch.BasicResponseDelegate>(this.OnDropConnection), callback));
			}
			return coroutine;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00004F3E File Offset: 0x0000313E
		internal void OnDropConnection(DropConnectionResponse response, NetworkMatch.BasicResponseDelegate userCallback)
		{
			userCallback(response.success, response.extendedInfo);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000050E0 File Offset: 0x000032E0
		public Coroutine ListMatches(int startPageNumber, int resultPageSize, string matchNameFilter, bool filterOutPrivateMatchesFromResults, int eloScoreTarget, int requestDomain, NetworkMatch.DataResponseDelegate<List<MatchInfoSnapshot>> callback)
		{
			bool flag = Application.platform == RuntimePlatform.WebGLPlayer;
			Coroutine coroutine;
			if (flag)
			{
				Debug.LogError("Matchmaking is not supported on WebGL player.");
				coroutine = null;
			}
			else
			{
				coroutine = this.ListMatches(new ListMatchRequest
				{
					pageNum = startPageNumber,
					pageSize = resultPageSize,
					nameFilter = matchNameFilter,
					filterOutPrivateMatches = filterOutPrivateMatchesFromResults,
					eloScore = eloScoreTarget,
					domain = requestDomain
				}, callback);
			}
			return coroutine;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005150 File Offset: 0x00003350
		internal Coroutine ListMatches(ListMatchRequest req, NetworkMatch.DataResponseDelegate<List<MatchInfoSnapshot>> callback)
		{
			bool flag = callback == null;
			Coroutine coroutine;
			if (flag)
			{
				Debug.Log("callback supplied is null, aborting ListMatch Request.");
				coroutine = null;
			}
			else
			{
				Uri uri = new Uri(this.baseUri, "/json/reply/ListMatchRequest");
				string text = "MatchMakingClient ListMatches :";
				Uri uri2 = uri;
				Debug.Log(text + ((uri2 != null) ? uri2.ToString() : null));
				WWWForm wwwform = new WWWForm();
				wwwform.AddField("version", Request.currentVersion);
				wwwform.AddField("projectId", Application.cloudProjectId);
				wwwform.AddField("sourceId", Utility.GetSourceID().ToString());
				wwwform.AddField("accessTokenString", 0);
				wwwform.AddField("domain", req.domain);
				wwwform.AddField("pageSize", req.pageSize);
				wwwform.AddField("pageNum", req.pageNum);
				wwwform.AddField("nameFilter", req.nameFilter);
				wwwform.AddField("filterOutPrivateMatches", req.filterOutPrivateMatches.ToString());
				wwwform.AddField("eloScore", req.eloScore.ToString());
				wwwform.headers["Accept"] = "application/json";
				UnityWebRequest unityWebRequest = UnityWebRequest.Post(uri.ToString(), wwwform);
				coroutine = base.StartCoroutine(this.ProcessMatchResponse<ListMatchResponse, NetworkMatch.DataResponseDelegate<List<MatchInfoSnapshot>>>(unityWebRequest, new NetworkMatch.InternalResponseDelegate<ListMatchResponse, NetworkMatch.DataResponseDelegate<List<MatchInfoSnapshot>>>(this.OnMatchList), callback));
			}
			return coroutine;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000052C0 File Offset: 0x000034C0
		internal void OnMatchList(ListMatchResponse response, NetworkMatch.DataResponseDelegate<List<MatchInfoSnapshot>> userCallback)
		{
			List<MatchInfoSnapshot> list = new List<MatchInfoSnapshot>();
			foreach (MatchDesc matchDesc in response.matches)
			{
				list.Add(new MatchInfoSnapshot(matchDesc));
			}
			userCallback(response.success, response.extendedInfo, list);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00005338 File Offset: 0x00003538
		public Coroutine SetMatchAttributes(NetworkID networkId, bool isListed, int requestDomain, NetworkMatch.BasicResponseDelegate callback)
		{
			return this.SetMatchAttributes(new SetMatchAttributesRequest
			{
				networkId = networkId,
				isListed = isListed,
				domain = requestDomain
			}, callback);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00005370 File Offset: 0x00003570
		internal Coroutine SetMatchAttributes(SetMatchAttributesRequest req, NetworkMatch.BasicResponseDelegate callback)
		{
			bool flag = callback == null;
			Coroutine coroutine;
			if (flag)
			{
				Debug.Log("callback supplied is null, aborting SetMatchAttributes Request.");
				coroutine = null;
			}
			else
			{
				Uri uri = new Uri(this.baseUri, "/json/reply/SetMatchAttributesRequest");
				string text = "MatchMakingClient SetMatchAttributes :";
				Uri uri2 = uri;
				Debug.Log(text + ((uri2 != null) ? uri2.ToString() : null));
				WWWForm wwwform = new WWWForm();
				wwwform.AddField("version", Request.currentVersion);
				wwwform.AddField("projectId", Application.cloudProjectId);
				wwwform.AddField("sourceId", Utility.GetSourceID().ToString());
				wwwform.AddField("accessTokenString", Utility.GetAccessTokenForNetwork(req.networkId).GetByteString());
				wwwform.AddField("domain", req.domain);
				wwwform.AddField("networkId", req.networkId.ToString());
				wwwform.AddField("isListed", req.isListed.ToString());
				wwwform.headers["Accept"] = "application/json";
				UnityWebRequest unityWebRequest = UnityWebRequest.Post(uri.ToString(), wwwform);
				coroutine = base.StartCoroutine(this.ProcessMatchResponse<BasicResponse, NetworkMatch.BasicResponseDelegate>(unityWebRequest, new NetworkMatch.InternalResponseDelegate<BasicResponse, NetworkMatch.BasicResponseDelegate>(this.OnSetMatchAttributes), callback));
			}
			return coroutine;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00004F3E File Offset: 0x0000313E
		internal void OnSetMatchAttributes(BasicResponse response, NetworkMatch.BasicResponseDelegate userCallback)
		{
			userCallback(response.success, response.extendedInfo);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000054BD File Offset: 0x000036BD
		private IEnumerator ProcessMatchResponse<JSONRESPONSE, USERRESPONSEDELEGATETYPE>(UnityWebRequest client, NetworkMatch.InternalResponseDelegate<JSONRESPONSE, USERRESPONSEDELEGATETYPE> internalCallback, USERRESPONSEDELEGATETYPE userCallback) where JSONRESPONSE : Response, new()
		{
			yield return client.SendWebRequest();
			JSONRESPONSE jsonInterface = new JSONRESPONSE();
			bool flag = client.result == UnityWebRequest.Result.Success;
			if (flag)
			{
				try
				{
					JsonUtility.FromJsonOverwrite(client.downloadHandler.text, jsonInterface);
				}
				catch (ArgumentException ex)
				{
					ArgumentException exception = ex;
					jsonInterface.SetFailure(UnityString.Format("ArgumentException:[{0}] ", new object[] { exception.ToString() }));
				}
			}
			else
			{
				jsonInterface.SetFailure(UnityString.Format("Request error:[{0}] Raw response:[{1}]", new object[]
				{
					client.error,
					client.downloadHandler.text
				}));
			}
			client.Dispose();
			internalCallback(jsonInterface, userCallback);
			yield break;
		}

		// Token: 0x0400008E RID: 142
		private Uri m_BaseUri = new Uri("https://mm.unet.unity3d.com");

		// Token: 0x0200001D RID: 29
		// (Invoke) Token: 0x06000180 RID: 384
		public delegate void BasicResponseDelegate(bool success, string extendedInfo);

		// Token: 0x0200001E RID: 30
		// (Invoke) Token: 0x06000184 RID: 388
		public delegate void DataResponseDelegate<T>(bool success, string extendedInfo, T responseData);

		// Token: 0x0200001F RID: 31
		// (Invoke) Token: 0x06000188 RID: 392
		private delegate void InternalResponseDelegate<T, U>(T response, U userCallback);
	}
}
