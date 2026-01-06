using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
	// Token: 0x02000010 RID: 16
	public class ChatModeratorActions : MessageData
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00005C1A File Offset: 0x00003E1A
		public string Type { get; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00005C22 File Offset: 0x00003E22
		public string ModerationAction { get; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00005C2A File Offset: 0x00003E2A
		public List<string> Args { get; } = new List<string>();

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00005C32 File Offset: 0x00003E32
		public string CreatedBy { get; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00005C3A File Offset: 0x00003E3A
		public string CreatedByUserId { get; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00005C42 File Offset: 0x00003E42
		public string TargetUserId { get; }

		// Token: 0x060000DB RID: 219 RVA: 0x00005C4C File Offset: 0x00003E4C
		public ChatModeratorActions(string jsonStr)
		{
			JToken jtoken = JObject.Parse(jsonStr).SelectToken("data");
			JToken jtoken2 = jtoken.SelectToken("type");
			this.Type = ((jtoken2 != null) ? jtoken2.ToString() : null);
			JToken jtoken3 = jtoken.SelectToken("moderation_action");
			this.ModerationAction = ((jtoken3 != null) ? jtoken3.ToString() : null);
			if (jtoken.SelectToken("args") != null)
			{
				foreach (JToken jtoken4 in jtoken.SelectToken("args"))
				{
					this.Args.Add(jtoken4.ToString());
				}
			}
			this.CreatedBy = jtoken.SelectToken("created_by").ToString();
			this.CreatedByUserId = jtoken.SelectToken("created_by_user_id").ToString();
			this.TargetUserId = jtoken.SelectToken("target_user_id").ToString();
		}
	}
}
