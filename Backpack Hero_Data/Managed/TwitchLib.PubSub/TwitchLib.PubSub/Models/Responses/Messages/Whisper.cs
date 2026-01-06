using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TwitchLib.PubSub.Enums;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
	// Token: 0x0200001B RID: 27
	public class Whisper : MessageData
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00006DB8 File Offset: 0x00004FB8
		public string Type { get; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00006DC0 File Offset: 0x00004FC0
		public WhisperType TypeEnum { get; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00006DC8 File Offset: 0x00004FC8
		public string Data { get; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00006DD0 File Offset: 0x00004FD0
		public Whisper.DataObjWhisperReceived DataObjectWhisperReceived { get; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00006DD8 File Offset: 0x00004FD8
		public Whisper.DataObjThread DataObjectThread { get; }

		// Token: 0x06000147 RID: 327 RVA: 0x00006DE0 File Offset: 0x00004FE0
		public Whisper(string jsonStr)
		{
			JObject jobject = JObject.Parse(jsonStr);
			this.Type = jobject.SelectToken("type").ToString();
			this.Data = jobject.SelectToken("data").ToString();
			string type = this.Type;
			if (type != null)
			{
				if (type == "whisper_received")
				{
					this.TypeEnum = WhisperType.WhisperReceived;
					this.DataObjectWhisperReceived = new Whisper.DataObjWhisperReceived(jobject.SelectToken("data_object"));
					return;
				}
				if (type == "thread")
				{
					this.TypeEnum = WhisperType.Thread;
					this.DataObjectThread = new Whisper.DataObjThread(jobject.SelectToken("data_object"));
					return;
				}
			}
			this.TypeEnum = WhisperType.Unknown;
		}

		// Token: 0x02000065 RID: 101
		public class DataObjThread
		{
			// Token: 0x170000BE RID: 190
			// (get) Token: 0x06000266 RID: 614 RVA: 0x00007657 File Offset: 0x00005857
			public string Id { get; }

			// Token: 0x170000BF RID: 191
			// (get) Token: 0x06000267 RID: 615 RVA: 0x0000765F File Offset: 0x0000585F
			public long LastRead { get; }

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x06000268 RID: 616 RVA: 0x00007667 File Offset: 0x00005867
			public bool Archived { get; }

			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x06000269 RID: 617 RVA: 0x0000766F File Offset: 0x0000586F
			public bool Muted { get; }

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x0600026A RID: 618 RVA: 0x00007677 File Offset: 0x00005877
			public Whisper.DataObjThread.SpamInfoObj SpamInfo { get; }

			// Token: 0x0600026B RID: 619 RVA: 0x00007680 File Offset: 0x00005880
			public DataObjThread(JToken json)
			{
				this.Id = json.SelectToken("id").ToString();
				this.LastRead = long.Parse(json.SelectToken("last_read").ToString());
				this.Archived = bool.Parse(json.SelectToken("archived").ToString());
				this.Muted = bool.Parse(json.SelectToken("muted").ToString());
				this.SpamInfo = new Whisper.DataObjThread.SpamInfoObj(json.SelectToken("spam_info"));
			}

			// Token: 0x02000067 RID: 103
			public class SpamInfoObj
			{
				// Token: 0x170000CB RID: 203
				// (get) Token: 0x0600027D RID: 637 RVA: 0x00007875 File Offset: 0x00005A75
				public string Likelihood { get; }

				// Token: 0x170000CC RID: 204
				// (get) Token: 0x0600027E RID: 638 RVA: 0x0000787D File Offset: 0x00005A7D
				public long LastMarkedNotSpam { get; }

				// Token: 0x0600027F RID: 639 RVA: 0x00007885 File Offset: 0x00005A85
				public SpamInfoObj(JToken json)
				{
					this.Likelihood = json.SelectToken("likelihood").ToString();
					this.LastMarkedNotSpam = long.Parse(json.SelectToken("last_marked_not_spam").ToString());
				}
			}
		}

		// Token: 0x02000066 RID: 102
		public class DataObjWhisperReceived
		{
			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x0600026C RID: 620 RVA: 0x00007710 File Offset: 0x00005910
			// (set) Token: 0x0600026D RID: 621 RVA: 0x00007718 File Offset: 0x00005918
			public string Id { get; protected set; }

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x0600026E RID: 622 RVA: 0x00007721 File Offset: 0x00005921
			// (set) Token: 0x0600026F RID: 623 RVA: 0x00007729 File Offset: 0x00005929
			public string ThreadId { get; protected set; }

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x06000270 RID: 624 RVA: 0x00007732 File Offset: 0x00005932
			// (set) Token: 0x06000271 RID: 625 RVA: 0x0000773A File Offset: 0x0000593A
			public string Body { get; protected set; }

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x06000272 RID: 626 RVA: 0x00007743 File Offset: 0x00005943
			// (set) Token: 0x06000273 RID: 627 RVA: 0x0000774B File Offset: 0x0000594B
			public long SentTs { get; protected set; }

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x06000274 RID: 628 RVA: 0x00007754 File Offset: 0x00005954
			// (set) Token: 0x06000275 RID: 629 RVA: 0x0000775C File Offset: 0x0000595C
			public string FromId { get; protected set; }

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x06000276 RID: 630 RVA: 0x00007765 File Offset: 0x00005965
			// (set) Token: 0x06000277 RID: 631 RVA: 0x0000776D File Offset: 0x0000596D
			public Whisper.DataObjWhisperReceived.TagsObj Tags { get; protected set; }

			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x06000278 RID: 632 RVA: 0x00007776 File Offset: 0x00005976
			// (set) Token: 0x06000279 RID: 633 RVA: 0x0000777E File Offset: 0x0000597E
			public Whisper.DataObjWhisperReceived.RecipientObj Recipient { get; protected set; }

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x0600027A RID: 634 RVA: 0x00007787 File Offset: 0x00005987
			// (set) Token: 0x0600027B RID: 635 RVA: 0x0000778F File Offset: 0x0000598F
			public string Nonce { get; protected set; }

			// Token: 0x0600027C RID: 636 RVA: 0x00007798 File Offset: 0x00005998
			public DataObjWhisperReceived(JToken json)
			{
				this.Id = json.SelectToken("id").ToString();
				JToken jtoken = json.SelectToken("thread_id");
				this.ThreadId = ((jtoken != null) ? jtoken.ToString() : null);
				JToken jtoken2 = json.SelectToken("body");
				this.Body = ((jtoken2 != null) ? jtoken2.ToString() : null);
				this.SentTs = long.Parse(json.SelectToken("sent_ts").ToString());
				this.FromId = json.SelectToken("from_id").ToString();
				this.Tags = new Whisper.DataObjWhisperReceived.TagsObj(json.SelectToken("tags"));
				this.Recipient = new Whisper.DataObjWhisperReceived.RecipientObj(json.SelectToken("recipient"));
				JToken jtoken3 = json.SelectToken("nonce");
				this.Nonce = ((jtoken3 != null) ? jtoken3.ToString() : null);
			}

			// Token: 0x02000068 RID: 104
			public class TagsObj
			{
				// Token: 0x170000CD RID: 205
				// (get) Token: 0x06000280 RID: 640 RVA: 0x000078BE File Offset: 0x00005ABE
				// (set) Token: 0x06000281 RID: 641 RVA: 0x000078C6 File Offset: 0x00005AC6
				public string Login { get; protected set; }

				// Token: 0x170000CE RID: 206
				// (get) Token: 0x06000282 RID: 642 RVA: 0x000078CF File Offset: 0x00005ACF
				// (set) Token: 0x06000283 RID: 643 RVA: 0x000078D7 File Offset: 0x00005AD7
				public string DisplayName { get; protected set; }

				// Token: 0x170000CF RID: 207
				// (get) Token: 0x06000284 RID: 644 RVA: 0x000078E0 File Offset: 0x00005AE0
				// (set) Token: 0x06000285 RID: 645 RVA: 0x000078E8 File Offset: 0x00005AE8
				public string Color { get; protected set; }

				// Token: 0x170000D0 RID: 208
				// (get) Token: 0x06000286 RID: 646 RVA: 0x000078F1 File Offset: 0x00005AF1
				// (set) Token: 0x06000287 RID: 647 RVA: 0x000078F9 File Offset: 0x00005AF9
				public string UserType { get; protected set; }

				// Token: 0x06000288 RID: 648 RVA: 0x00007904 File Offset: 0x00005B04
				public TagsObj(JToken json)
				{
					JToken jtoken = json.SelectToken("login");
					this.Login = ((jtoken != null) ? jtoken.ToString() : null);
					JToken jtoken2 = json.SelectToken("login");
					this.DisplayName = ((jtoken2 != null) ? jtoken2.ToString() : null);
					JToken jtoken3 = json.SelectToken("color");
					this.Color = ((jtoken3 != null) ? jtoken3.ToString() : null);
					JToken jtoken4 = json.SelectToken("user_type");
					this.UserType = ((jtoken4 != null) ? jtoken4.ToString() : null);
					foreach (JToken jtoken5 in json.SelectToken("emotes"))
					{
						this.Emotes.Add(new Whisper.DataObjWhisperReceived.TagsObj.EmoteObj(jtoken5));
					}
					foreach (JToken jtoken6 in json.SelectToken("badges"))
					{
						this.Badges.Add(new Whisper.DataObjWhisperReceived.Badge(jtoken6));
					}
				}

				// Token: 0x040001DB RID: 475
				public readonly List<Whisper.DataObjWhisperReceived.TagsObj.EmoteObj> Emotes = new List<Whisper.DataObjWhisperReceived.TagsObj.EmoteObj>();

				// Token: 0x040001DC RID: 476
				public readonly List<Whisper.DataObjWhisperReceived.Badge> Badges = new List<Whisper.DataObjWhisperReceived.Badge>();

				// Token: 0x0200006B RID: 107
				public class EmoteObj
				{
					// Token: 0x170000D8 RID: 216
					// (get) Token: 0x06000299 RID: 665 RVA: 0x00007BA1 File Offset: 0x00005DA1
					// (set) Token: 0x0600029A RID: 666 RVA: 0x00007BA9 File Offset: 0x00005DA9
					public string Id { get; protected set; }

					// Token: 0x170000D9 RID: 217
					// (get) Token: 0x0600029B RID: 667 RVA: 0x00007BB2 File Offset: 0x00005DB2
					// (set) Token: 0x0600029C RID: 668 RVA: 0x00007BBA File Offset: 0x00005DBA
					public int Start { get; protected set; }

					// Token: 0x170000DA RID: 218
					// (get) Token: 0x0600029D RID: 669 RVA: 0x00007BC3 File Offset: 0x00005DC3
					// (set) Token: 0x0600029E RID: 670 RVA: 0x00007BCB File Offset: 0x00005DCB
					public int End { get; protected set; }

					// Token: 0x0600029F RID: 671 RVA: 0x00007BD4 File Offset: 0x00005DD4
					public EmoteObj(JToken json)
					{
						this.Id = json.SelectToken("emote_id").ToString();
						this.Start = int.Parse(json.SelectToken("start").ToString());
						this.End = int.Parse(json.SelectToken("end").ToString());
					}
				}
			}

			// Token: 0x02000069 RID: 105
			public class RecipientObj
			{
				// Token: 0x170000D1 RID: 209
				// (get) Token: 0x06000289 RID: 649 RVA: 0x00007A3C File Offset: 0x00005C3C
				// (set) Token: 0x0600028A RID: 650 RVA: 0x00007A44 File Offset: 0x00005C44
				public string Id { get; protected set; }

				// Token: 0x170000D2 RID: 210
				// (get) Token: 0x0600028B RID: 651 RVA: 0x00007A4D File Offset: 0x00005C4D
				// (set) Token: 0x0600028C RID: 652 RVA: 0x00007A55 File Offset: 0x00005C55
				public string Username { get; protected set; }

				// Token: 0x170000D3 RID: 211
				// (get) Token: 0x0600028D RID: 653 RVA: 0x00007A5E File Offset: 0x00005C5E
				// (set) Token: 0x0600028E RID: 654 RVA: 0x00007A66 File Offset: 0x00005C66
				public string DisplayName { get; protected set; }

				// Token: 0x170000D4 RID: 212
				// (get) Token: 0x0600028F RID: 655 RVA: 0x00007A6F File Offset: 0x00005C6F
				// (set) Token: 0x06000290 RID: 656 RVA: 0x00007A77 File Offset: 0x00005C77
				public string Color { get; protected set; }

				// Token: 0x170000D5 RID: 213
				// (get) Token: 0x06000291 RID: 657 RVA: 0x00007A80 File Offset: 0x00005C80
				// (set) Token: 0x06000292 RID: 658 RVA: 0x00007A88 File Offset: 0x00005C88
				public string UserType { get; protected set; }

				// Token: 0x06000293 RID: 659 RVA: 0x00007A94 File Offset: 0x00005C94
				public RecipientObj(JToken json)
				{
					this.Id = json.SelectToken("id").ToString();
					JToken jtoken = json.SelectToken("username");
					this.Username = ((jtoken != null) ? jtoken.ToString() : null);
					JToken jtoken2 = json.SelectToken("display_name");
					this.DisplayName = ((jtoken2 != null) ? jtoken2.ToString() : null);
					JToken jtoken3 = json.SelectToken("color");
					this.Color = ((jtoken3 != null) ? jtoken3.ToString() : null);
					JToken jtoken4 = json.SelectToken("user_type");
					this.UserType = ((jtoken4 != null) ? jtoken4.ToString() : null);
				}
			}

			// Token: 0x0200006A RID: 106
			public class Badge
			{
				// Token: 0x170000D6 RID: 214
				// (get) Token: 0x06000294 RID: 660 RVA: 0x00007B31 File Offset: 0x00005D31
				// (set) Token: 0x06000295 RID: 661 RVA: 0x00007B39 File Offset: 0x00005D39
				public string Id { get; protected set; }

				// Token: 0x170000D7 RID: 215
				// (get) Token: 0x06000296 RID: 662 RVA: 0x00007B42 File Offset: 0x00005D42
				// (set) Token: 0x06000297 RID: 663 RVA: 0x00007B4A File Offset: 0x00005D4A
				public string Version { get; protected set; }

				// Token: 0x06000298 RID: 664 RVA: 0x00007B54 File Offset: 0x00005D54
				public Badge(JToken json)
				{
					JToken jtoken = json.SelectToken("id");
					this.Id = ((jtoken != null) ? jtoken.ToString() : null);
					JToken jtoken2 = json.SelectToken("version");
					this.Version = ((jtoken2 != null) ? jtoken2.ToString() : null);
				}
			}
		}
	}
}
