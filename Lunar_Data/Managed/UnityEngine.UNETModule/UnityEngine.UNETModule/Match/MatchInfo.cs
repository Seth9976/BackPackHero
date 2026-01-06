using System;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking.Match
{
	// Token: 0x02000019 RID: 25
	[Obsolete("The matchmaker and relay feature will be removed in the future, minimal support will continue until this can be safely done.")]
	public class MatchInfo
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00004579 File Offset: 0x00002779
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00004581 File Offset: 0x00002781
		public string address { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600013A RID: 314 RVA: 0x0000458A File Offset: 0x0000278A
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00004592 File Offset: 0x00002792
		public int port { get; private set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000459B File Offset: 0x0000279B
		// (set) Token: 0x0600013D RID: 317 RVA: 0x000045A3 File Offset: 0x000027A3
		public int domain { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600013E RID: 318 RVA: 0x000045AC File Offset: 0x000027AC
		// (set) Token: 0x0600013F RID: 319 RVA: 0x000045B4 File Offset: 0x000027B4
		public NetworkID networkId { get; private set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000045BD File Offset: 0x000027BD
		// (set) Token: 0x06000141 RID: 321 RVA: 0x000045C5 File Offset: 0x000027C5
		public NetworkAccessToken accessToken { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000142 RID: 322 RVA: 0x000045CE File Offset: 0x000027CE
		// (set) Token: 0x06000143 RID: 323 RVA: 0x000045D6 File Offset: 0x000027D6
		public NodeID nodeId { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000144 RID: 324 RVA: 0x000045DF File Offset: 0x000027DF
		// (set) Token: 0x06000145 RID: 325 RVA: 0x000045E7 File Offset: 0x000027E7
		public bool usingRelay { get; private set; }

		// Token: 0x06000146 RID: 326 RVA: 0x00002371 File Offset: 0x00000571
		public MatchInfo()
		{
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000045F0 File Offset: 0x000027F0
		internal MatchInfo(CreateMatchResponse matchResponse)
		{
			this.address = matchResponse.address;
			this.port = matchResponse.port;
			this.domain = matchResponse.domain;
			this.networkId = (NetworkID)matchResponse.networkId;
			this.accessToken = new NetworkAccessToken(matchResponse.accessTokenString);
			this.nodeId = matchResponse.nodeId;
			this.usingRelay = matchResponse.usingRelay;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004668 File Offset: 0x00002868
		internal MatchInfo(JoinMatchResponse matchResponse)
		{
			this.address = matchResponse.address;
			this.port = matchResponse.port;
			this.domain = matchResponse.domain;
			this.networkId = (NetworkID)matchResponse.networkId;
			this.accessToken = new NetworkAccessToken(matchResponse.accessTokenString);
			this.nodeId = matchResponse.nodeId;
			this.usingRelay = matchResponse.usingRelay;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000046E0 File Offset: 0x000028E0
		public override string ToString()
		{
			return UnityString.Format("{0} @ {1}:{2} [{3},{4}]", new object[] { this.networkId, this.address, this.port, this.nodeId, this.usingRelay });
		}
	}
}
