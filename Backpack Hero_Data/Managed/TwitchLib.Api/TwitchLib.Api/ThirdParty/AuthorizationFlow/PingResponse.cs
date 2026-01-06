using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.ThirdParty.AuthorizationFlow
{
	// Token: 0x0200000D RID: 13
	public class PingResponse
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000024AF File Offset: 0x000006AF
		// (set) Token: 0x0600004C RID: 76 RVA: 0x000024B7 File Offset: 0x000006B7
		public bool Success { get; protected set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000024C0 File Offset: 0x000006C0
		// (set) Token: 0x0600004E RID: 78 RVA: 0x000024C8 File Offset: 0x000006C8
		public string Id { get; protected set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000024D1 File Offset: 0x000006D1
		// (set) Token: 0x06000050 RID: 80 RVA: 0x000024D9 File Offset: 0x000006D9
		public int Error { get; protected set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000024E2 File Offset: 0x000006E2
		// (set) Token: 0x06000052 RID: 82 RVA: 0x000024EA File Offset: 0x000006EA
		public string Message { get; protected set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000024F3 File Offset: 0x000006F3
		// (set) Token: 0x06000054 RID: 84 RVA: 0x000024FB File Offset: 0x000006FB
		public List<AuthScopes> Scopes { get; protected set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002504 File Offset: 0x00000704
		// (set) Token: 0x06000056 RID: 86 RVA: 0x0000250C File Offset: 0x0000070C
		public string Token { get; protected set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002515 File Offset: 0x00000715
		// (set) Token: 0x06000058 RID: 88 RVA: 0x0000251D File Offset: 0x0000071D
		public string Refresh { get; protected set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002526 File Offset: 0x00000726
		// (set) Token: 0x0600005A RID: 90 RVA: 0x0000252E File Offset: 0x0000072E
		public string Username { get; protected set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002537 File Offset: 0x00000737
		// (set) Token: 0x0600005C RID: 92 RVA: 0x0000253F File Offset: 0x0000073F
		public string ClientId { get; protected set; }

		// Token: 0x0600005D RID: 93 RVA: 0x00002548 File Offset: 0x00000748
		public PingResponse(string jsonStr)
		{
			JObject jobject = JObject.Parse(jsonStr);
			this.Success = bool.Parse(jobject.SelectToken("success").ToString());
			if (!this.Success)
			{
				this.Error = int.Parse(jobject.SelectToken("error").ToString());
				this.Message = jobject.SelectToken("message").ToString();
				return;
			}
			this.Scopes = new List<AuthScopes>();
			foreach (JToken jtoken in jobject.SelectToken("scopes"))
			{
				this.Scopes.Add(this.StringToScope(jtoken.ToString()));
			}
			this.Token = jobject.SelectToken("token").ToString();
			this.Refresh = jobject.SelectToken("refresh").ToString();
			this.Username = jobject.SelectToken("username").ToString();
			this.ClientId = jobject.SelectToken("client_id").ToString();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002670 File Offset: 0x00000870
		private AuthScopes StringToScope(string scope)
		{
			if (scope != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(scope);
				if (num <= 2271530158U)
				{
					if (num <= 1093200473U)
					{
						if (num <= 753741398U)
						{
							if (num <= 529226374U)
							{
								if (num != 462412497U)
								{
									if (num == 529226374U)
									{
										if (scope == "channel:read:subscriptions")
										{
											return AuthScopes.Helix_Channel_Read_Subscriptions;
										}
									}
								}
								else if (scope == "channel:manage:videos")
								{
									return AuthScopes.Helix_Channel_Manage_Videos;
								}
							}
							else if (num != 604561354U)
							{
								if (num != 644703415U)
								{
									if (num == 753741398U)
									{
										if (scope == "channel_feed_edit")
										{
											return AuthScopes.Channel_Feed_Edit;
										}
									}
								}
								else if (scope == "chat:read")
								{
									return AuthScopes.Chat_Read;
								}
							}
							else if (scope == "analytics:read:games")
							{
								return AuthScopes.Helix_Analytics_Read_Games;
							}
						}
						else if (num <= 887442718U)
						{
							if (num != 829707769U)
							{
								if (num == 887442718U)
								{
									if (scope == "moderator:read:chatters")
									{
										return AuthScopes.Helix_Moderator_Read_Chatters;
									}
								}
							}
							else if (scope == "channel_stream")
							{
								return AuthScopes.Channel_Subscriptions;
							}
						}
						else if (num != 898790762U)
						{
							if (num != 965238838U)
							{
								if (num == 1093200473U)
								{
									if (scope == "bits:read")
									{
										return AuthScopes.Helix_Bits_Read;
									}
								}
							}
							else if (scope == "moderator:manage:automod")
							{
								return AuthScopes.Helix_Moderator_Manage_Automod;
							}
						}
						else if (scope == "channel:read:predictions")
						{
							return AuthScopes.Helix_Channel_Read_Predictions;
						}
					}
					else if (num <= 1806063621U)
					{
						if (num <= 1454101501U)
						{
							if (num != 1402489448U)
							{
								if (num == 1454101501U)
								{
									if (scope == "channel_check_subscription")
									{
										return AuthScopes.Channel_Check_Subscription;
									}
								}
							}
							else if (scope == "user_follows_edit")
							{
								return AuthScopes.User_Follows_Edit;
							}
						}
						else if (num != 1517427295U)
						{
							if (num != 1782317826U)
							{
								if (num == 1806063621U)
								{
									if (scope == "channel:manage:polls")
									{
										return AuthScopes.Helix_Channel_Manage_Polls;
									}
								}
							}
							else if (scope == "user_blocks_read")
							{
								return AuthScopes.User_Blocks_Read;
							}
						}
						else if (scope == "chat:edit")
						{
							return AuthScopes.Chat_Edit;
						}
					}
					else if (num <= 1901018726U)
					{
						if (num != 1834540039U)
						{
							if (num != 1881179898U)
							{
								if (num == 1901018726U)
								{
									if (scope == "communities_moderate")
									{
										return AuthScopes.Communities_Moderate;
									}
								}
							}
							else if (scope == "clips:edit")
							{
								return AuthScopes.Helix_Clips_Edit;
							}
						}
						else if (scope == "collections_edit")
						{
							return AuthScopes.Collections_Edit;
						}
					}
					else if (num != 2081117854U)
					{
						if (num != 2166136261U)
						{
							if (num == 2271530158U)
							{
								if (scope == "user:read:email")
								{
									return AuthScopes.Helix_User_Read_Email;
								}
							}
						}
						else if (scope != null)
						{
							if (scope.Length == 0)
							{
								return AuthScopes.None;
							}
						}
					}
					else if (scope == "chat:moderate")
					{
						return AuthScopes.Chat_Moderate;
					}
				}
				else if (num <= 3326761964U)
				{
					if (num <= 2852402105U)
					{
						if (num <= 2636620404U)
						{
							if (num != 2606799319U)
							{
								if (num == 2636620404U)
								{
									if (scope == "channel_editor")
									{
										return AuthScopes.Channel_Editor;
									}
								}
							}
							else if (scope == "communities_edit")
							{
								return AuthScopes.Communities_Edit;
							}
						}
						else if (num != 2728386193U)
						{
							if (num != 2800940182U)
							{
								if (num == 2852402105U)
								{
									if (scope == "channel:manage:predictions")
									{
										return AuthScopes.Helix_Channel_Manage_Predictions;
									}
								}
							}
							else if (scope == "channel:read:editors")
							{
								return AuthScopes.Helix_Channel_Read_Editors;
							}
						}
						else if (scope == "user:read:blocked_users")
						{
							return AuthScopes.Helix_User_Read_BlockedUsers;
						}
					}
					else if (num <= 2922268857U)
					{
						if (num != 2865708859U)
						{
							if (num == 2922268857U)
							{
								if (scope == "channel_subscriptions")
								{
									return AuthScopes.Channel_Subscriptions;
								}
							}
						}
						else if (scope == "user_read")
						{
							return AuthScopes.User_Read;
						}
					}
					else if (num != 3210808076U)
					{
						if (num != 3255678693U)
						{
							if (num == 3326761964U)
							{
								if (scope == "user:edit")
								{
									return AuthScopes.Helix_User_Edit;
								}
							}
						}
						else if (scope == "channel:manage:redemptions")
						{
							return AuthScopes.Helix_Channel_Manage_Redemptions;
						}
					}
					else if (scope == "channel:read:stream_key")
					{
						return AuthScopes.Helix_Channel_Read_Stream_Key;
					}
				}
				else if (num <= 3829858081U)
				{
					if (num <= 3454609462U)
					{
						if (num != 3376417058U)
						{
							if (num == 3454609462U)
							{
								if (scope == "user_blocks_edit")
								{
									return AuthScopes.User_Blocks_Edit;
								}
							}
						}
						else if (scope == "channel_feed_read")
						{
							return AuthScopes.Channel_Feed_Read;
						}
					}
					else if (num != 3557617547U)
					{
						if (num != 3661274983U)
						{
							if (num == 3829858081U)
							{
								if (scope == "channel_read")
								{
									return AuthScopes.Channel_Read;
								}
							}
						}
						else if (scope == "channel_commercial")
						{
							return AuthScopes.Channel_Commercial;
						}
					}
					else if (scope == "user_subscriptions")
					{
						return AuthScopes.User_Subscriptions;
					}
				}
				else if (num <= 4075560778U)
				{
					if (num != 3841271025U)
					{
						if (num != 4035273701U)
						{
							if (num == 4075560778U)
							{
								if (scope == "user:manage:blocked_users")
								{
									return AuthScopes.Helix_User_Manage_BlockedUsers;
								}
							}
						}
						else if (scope == "channel:read:hype_train")
						{
							return AuthScopes.Helix_Channel_Read_Hype_Train;
						}
					}
					else if (scope == "viewing_activity_read")
					{
						return AuthScopes.Viewing_Activity_Read;
					}
				}
				else if (num != 4146048300U)
				{
					if (num != 4176690178U)
					{
						if (num == 4178990446U)
						{
							if (scope == "channel:edit:commercial")
							{
								return AuthScopes.Helix_Channel_Edit_Commercial;
							}
						}
					}
					else if (scope == "channel:read:polls")
					{
						return AuthScopes.Helix_Channel_Read_Polls;
					}
				}
				else if (scope == "user:read:subscriptions")
				{
					return AuthScopes.Helix_User_Read_Subscriptions;
				}
			}
			throw new Exception("Unknown scope");
		}
	}
}
