using System;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking.Match
{
	// Token: 0x02000021 RID: 33
	internal abstract class Request
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00005663 File Offset: 0x00003863
		// (set) Token: 0x06000192 RID: 402 RVA: 0x0000566B File Offset: 0x0000386B
		public int version { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00005674 File Offset: 0x00003874
		// (set) Token: 0x06000194 RID: 404 RVA: 0x0000567C File Offset: 0x0000387C
		public SourceID sourceId { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00005685 File Offset: 0x00003885
		// (set) Token: 0x06000196 RID: 406 RVA: 0x0000568D File Offset: 0x0000388D
		public string projectId { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00005696 File Offset: 0x00003896
		// (set) Token: 0x06000198 RID: 408 RVA: 0x0000569E File Offset: 0x0000389E
		public AppID appId { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000199 RID: 409 RVA: 0x000056A7 File Offset: 0x000038A7
		// (set) Token: 0x0600019A RID: 410 RVA: 0x000056AF File Offset: 0x000038AF
		public string accessTokenString { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000056B8 File Offset: 0x000038B8
		// (set) Token: 0x0600019C RID: 412 RVA: 0x000056C0 File Offset: 0x000038C0
		public int domain { get; set; }

		// Token: 0x0600019D RID: 413 RVA: 0x000056CC File Offset: 0x000038CC
		public virtual bool IsValid()
		{
			return this.sourceId != SourceID.Invalid;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000056EC File Offset: 0x000038EC
		public override string ToString()
		{
			return UnityString.Format("[{0}]-SourceID:0x{1},projectId:{2},accessTokenString.IsEmpty:{3},domain:{4}", new object[]
			{
				base.ToString(),
				this.sourceId.ToString("X"),
				this.projectId,
				string.IsNullOrEmpty(this.accessTokenString),
				this.domain
			});
		}

		// Token: 0x04000097 RID: 151
		public static readonly int currentVersion = 3;
	}
}
