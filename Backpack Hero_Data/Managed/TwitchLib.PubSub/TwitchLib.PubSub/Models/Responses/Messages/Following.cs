using System;
using Newtonsoft.Json.Linq;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
	// Token: 0x02000012 RID: 18
	public class Following : MessageData
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00006144 File Offset: 0x00004344
		public string DisplayName { get; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x0000614C File Offset: 0x0000434C
		public string Username { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00006154 File Offset: 0x00004354
		public string UserId { get; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x0000615C File Offset: 0x0000435C
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00006164 File Offset: 0x00004364
		public string FollowedChannelId { get; internal set; }

		// Token: 0x060000FA RID: 250 RVA: 0x00006170 File Offset: 0x00004370
		public Following(string jsonStr)
		{
			JObject jobject = JObject.Parse(jsonStr);
			this.DisplayName = jobject["display_name"].ToString();
			this.Username = jobject["username"].ToString();
			this.UserId = jobject["user_id"].ToString();
		}
	}
}
