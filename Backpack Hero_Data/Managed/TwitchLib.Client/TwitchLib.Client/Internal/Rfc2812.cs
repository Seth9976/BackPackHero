using System;
using System.Text;
using System.Text.RegularExpressions;

namespace TwitchLib.Client.Internal
{
	// Token: 0x02000004 RID: 4
	public sealed class Rfc2812
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00005F71 File Offset: 0x00004171
		private Rfc2812()
		{
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00005F79 File Offset: 0x00004179
		public static bool IsValidNickname(string nickname)
		{
			return !string.IsNullOrEmpty(nickname) && Rfc2812.NicknameRegex.Match(nickname).Success;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005F95 File Offset: 0x00004195
		public static string Pass(string password)
		{
			return "PASS " + password;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005FA2 File Offset: 0x000041A2
		public static string Nick(string nickname)
		{
			return "NICK " + nickname;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005FAF File Offset: 0x000041AF
		public static string User(string username, int usermode, string realname)
		{
			return string.Format("USER {0} {1} * :{2}", username, usermode, realname);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00005FC3 File Offset: 0x000041C3
		public static string Oper(string name, string password)
		{
			return "OPER " + name + " " + password;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00005FD6 File Offset: 0x000041D6
		public static string Privmsg(string destination, string message)
		{
			return "PRIVMSG " + destination + " :" + message;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00005FE9 File Offset: 0x000041E9
		public static string Notice(string destination, string message)
		{
			return "NOTICE " + destination + " :" + message;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005FFC File Offset: 0x000041FC
		public static string Join(string channel)
		{
			return "JOIN " + channel;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00006009 File Offset: 0x00004209
		public static string Join(string[] channels)
		{
			return "JOIN " + string.Join(",", channels);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00006020 File Offset: 0x00004220
		public static string Join(string channel, string key)
		{
			return "JOIN " + channel + " " + key;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00006033 File Offset: 0x00004233
		public static string Join(string[] channels, string[] keys)
		{
			return "JOIN " + string.Join(",", channels) + " " + string.Join(",", keys);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000605A File Offset: 0x0000425A
		public static string Part(string channel)
		{
			return "PART " + channel;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00006067 File Offset: 0x00004267
		public static string Part(string[] channels)
		{
			return "PART " + string.Join(",", channels);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000607E File Offset: 0x0000427E
		public static string Part(string channel, string partmessage)
		{
			return "PART " + channel + " :" + partmessage;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00006091 File Offset: 0x00004291
		public static string Part(string[] channels, string partmessage)
		{
			return "PART " + string.Join(",", channels) + " :" + partmessage;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000060AE File Offset: 0x000042AE
		public static string Kick(string channel, string nickname)
		{
			return "KICK " + channel + " " + nickname;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000060C1 File Offset: 0x000042C1
		public static string Kick(string channel, string nickname, string comment)
		{
			return string.Concat(new string[] { "KICK ", channel, " ", nickname, " :", comment });
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000060F2 File Offset: 0x000042F2
		public static string Kick(string[] channels, string nickname)
		{
			return "KICK " + string.Join(",", channels) + " " + nickname;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000610F File Offset: 0x0000430F
		public static string Kick(string[] channels, string nickname, string comment)
		{
			return string.Concat(new string[]
			{
				"KICK ",
				string.Join(",", channels),
				" ",
				nickname,
				" :",
				comment
			});
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000614A File Offset: 0x0000434A
		public static string Kick(string channel, string[] nicknames)
		{
			return "KICK " + channel + " " + string.Join(",", nicknames);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00006167 File Offset: 0x00004367
		public static string Kick(string channel, string[] nicknames, string comment)
		{
			return string.Concat(new string[]
			{
				"KICK ",
				channel,
				" ",
				string.Join(",", nicknames),
				" :",
				comment
			});
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000061A2 File Offset: 0x000043A2
		public static string Kick(string[] channels, string[] nicknames)
		{
			return "KICK " + string.Join(",", channels) + " " + string.Join(",", nicknames);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000061CC File Offset: 0x000043CC
		public static string Kick(string[] channels, string[] nicknames, string comment)
		{
			return string.Concat(new string[]
			{
				"KICK ",
				string.Join(",", channels),
				" ",
				string.Join(",", nicknames),
				" :",
				comment
			});
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000621C File Offset: 0x0000441C
		public static string Motd()
		{
			return "MOTD";
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00006223 File Offset: 0x00004423
		public static string Motd(string target)
		{
			return "MOTD " + target;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00006230 File Offset: 0x00004430
		public static string Lusers()
		{
			return "LUSERS";
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00006237 File Offset: 0x00004437
		public static string Lusers(string mask)
		{
			return "LUSER " + mask;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00006244 File Offset: 0x00004444
		public static string Lusers(string mask, string target)
		{
			return "LUSER " + mask + " " + target;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00006257 File Offset: 0x00004457
		public static string Version()
		{
			return "VERSION";
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000625E File Offset: 0x0000445E
		public static string Version(string target)
		{
			return "VERSION " + target;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000626B File Offset: 0x0000446B
		public static string Stats()
		{
			return "STATS";
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00006272 File Offset: 0x00004472
		public static string Stats(string query)
		{
			return "STATS " + query;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000627F File Offset: 0x0000447F
		public static string Stats(string query, string target)
		{
			return "STATS " + query + " " + target;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00006292 File Offset: 0x00004492
		public static string Links()
		{
			return "LINKS";
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00006299 File Offset: 0x00004499
		public static string Links(string servermask)
		{
			return "LINKS " + servermask;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000062A6 File Offset: 0x000044A6
		public static string Links(string remoteserver, string servermask)
		{
			return "LINKS " + remoteserver + " " + servermask;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000062B9 File Offset: 0x000044B9
		public static string Time()
		{
			return "TIME";
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000062C0 File Offset: 0x000044C0
		public static string Time(string target)
		{
			return "TIME " + target;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000062CD File Offset: 0x000044CD
		public static string Connect(string targetserver, string port)
		{
			return "CONNECT " + targetserver + " " + port;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000062E0 File Offset: 0x000044E0
		public static string Connect(string targetserver, string port, string remoteserver)
		{
			return string.Concat(new string[] { "CONNECT ", targetserver, " ", port, " ", remoteserver });
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00006311 File Offset: 0x00004511
		public static string Trace()
		{
			return "TRACE";
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00006318 File Offset: 0x00004518
		public static string Trace(string target)
		{
			return "TRACE " + target;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00006325 File Offset: 0x00004525
		public static string Admin()
		{
			return "ADMIN";
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000632C File Offset: 0x0000452C
		public static string Admin(string target)
		{
			return "ADMIN " + target;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00006339 File Offset: 0x00004539
		public static string Info()
		{
			return "INFO";
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00006340 File Offset: 0x00004540
		public static string Info(string target)
		{
			return "INFO " + target;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000634D File Offset: 0x0000454D
		public static string Servlist()
		{
			return "SERVLIST";
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00006354 File Offset: 0x00004554
		public static string Servlist(string mask)
		{
			return "SERVLIST " + mask;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00006361 File Offset: 0x00004561
		public static string Servlist(string mask, string type)
		{
			return "SERVLIST " + mask + " " + type;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00006374 File Offset: 0x00004574
		public static string Squery(string servicename, string servicetext)
		{
			return "SQUERY " + servicename + " :" + servicename;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00006387 File Offset: 0x00004587
		public static string List()
		{
			return "LIST";
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000638E File Offset: 0x0000458E
		public static string List(string channel)
		{
			return "LIST " + channel;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000639B File Offset: 0x0000459B
		public static string List(string[] channels)
		{
			return "LIST " + string.Join(",", channels);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000063B2 File Offset: 0x000045B2
		public static string List(string channel, string target)
		{
			return "LIST " + channel + " " + target;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000063C5 File Offset: 0x000045C5
		public static string List(string[] channels, string target)
		{
			return "LIST " + string.Join(",", channels) + " " + target;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000063E2 File Offset: 0x000045E2
		public static string Names()
		{
			return "NAMES";
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000063E9 File Offset: 0x000045E9
		public static string Names(string channel)
		{
			return "NAMES " + channel;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000063F6 File Offset: 0x000045F6
		public static string Names(string[] channels)
		{
			return "NAMES " + string.Join(",", channels);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000640D File Offset: 0x0000460D
		public static string Names(string channel, string target)
		{
			return "NAMES " + channel + " " + target;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00006420 File Offset: 0x00004620
		public static string Names(string[] channels, string target)
		{
			return "NAMES " + string.Join(",", channels) + " " + target;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000643D File Offset: 0x0000463D
		public static string Topic(string channel)
		{
			return "TOPIC " + channel;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000644A File Offset: 0x0000464A
		public static string Topic(string channel, string newtopic)
		{
			return "TOPIC " + channel + " :" + newtopic;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000645D File Offset: 0x0000465D
		public static string Mode(string target)
		{
			return "MODE " + target;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000646A File Offset: 0x0000466A
		public static string Mode(string target, string newmode)
		{
			return string.Concat(new string[] { "MODE ", target, " ", newmode, target, " ", newmode });
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000064A0 File Offset: 0x000046A0
		public static string Mode(string target, string[] newModes, string[] newModeParameters)
		{
			if (newModes == null)
			{
				throw new ArgumentNullException("newModes");
			}
			if (newModeParameters == null)
			{
				throw new ArgumentNullException("newModeParameters");
			}
			if (newModes.Length != newModeParameters.Length)
			{
				throw new ArgumentException("newModes and newModeParameters must have the same size.");
			}
			StringBuilder stringBuilder = new StringBuilder(newModes.Length);
			StringBuilder stringBuilder2 = new StringBuilder();
			if (newModes.Length > 3)
			{
				throw new ArgumentOutOfRangeException("Length", newModes.Length, string.Format("Mode change list is too large (> {0}).", 3));
			}
			for (int i = 0; i <= newModes.Length; i += 3)
			{
				int num = 0;
				while (num < 3 && i + num < newModes.Length)
				{
					stringBuilder.Append(newModes[i + num]);
					num++;
				}
				int num2 = 0;
				while (num2 < 3 && i + num2 < newModeParameters.Length)
				{
					stringBuilder2.Append(newModeParameters[i + num2]);
					stringBuilder2.Append(" ");
					num2++;
				}
			}
			if (stringBuilder2.Length <= 0)
			{
				return Rfc2812.Mode(target, stringBuilder.ToString());
			}
			StringBuilder stringBuilder3 = stringBuilder2;
			int length = stringBuilder3.Length;
			stringBuilder3.Length = length - 1;
			stringBuilder.Append(" ");
			stringBuilder.Append(stringBuilder2);
			return Rfc2812.Mode(target, stringBuilder.ToString());
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000065BC File Offset: 0x000047BC
		public static string Service(string nickname, string distribution, string info)
		{
			return string.Concat(new string[] { "SERVICE ", nickname, " * ", distribution, " * * :", info });
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000065ED File Offset: 0x000047ED
		public static string Invite(string nickname, string channel)
		{
			return "INVITE " + nickname + " " + channel;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00006600 File Offset: 0x00004800
		public static string Who()
		{
			return "WHO";
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00006607 File Offset: 0x00004807
		public static string Who(string mask)
		{
			return "WHO " + mask;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006614 File Offset: 0x00004814
		public static string Who(string mask, bool ircop)
		{
			if (!ircop)
			{
				return "WHO " + mask;
			}
			return "WHO " + mask + " o";
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00006635 File Offset: 0x00004835
		public static string Whois(string mask)
		{
			return "WHOIS " + mask;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00006642 File Offset: 0x00004842
		public static string Whois(string[] masks)
		{
			return "WHOIS " + string.Join(",", masks);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00006659 File Offset: 0x00004859
		public static string Whois(string target, string mask)
		{
			return "WHOIS " + target + " " + mask;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000666C File Offset: 0x0000486C
		public static string Whois(string target, string[] masks)
		{
			return "WHOIS " + target + " " + string.Join(",", masks);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00006689 File Offset: 0x00004889
		public static string Whowas(string nickname)
		{
			return "WHOWAS " + nickname;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00006696 File Offset: 0x00004896
		public static string Whowas(string[] nicknames)
		{
			return "WHOWAS " + string.Join(",", nicknames);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000066AD File Offset: 0x000048AD
		public static string Whowas(string nickname, string count)
		{
			return string.Concat(new string[] { "WHOWAS ", nickname, " ", count, " " });
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000066DA File Offset: 0x000048DA
		public static string Whowas(string[] nicknames, string count)
		{
			return string.Concat(new string[]
			{
				"WHOWAS ",
				string.Join(",", nicknames),
				" ",
				count,
				" "
			});
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00006711 File Offset: 0x00004911
		public static string Whowas(string nickname, string count, string target)
		{
			return string.Concat(new string[] { "WHOWAS ", nickname, " ", count, " ", target });
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00006742 File Offset: 0x00004942
		public static string Whowas(string[] nicknames, string count, string target)
		{
			return string.Concat(new string[]
			{
				"WHOWAS ",
				string.Join(",", nicknames),
				" ",
				count,
				" ",
				target
			});
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000677D File Offset: 0x0000497D
		public static string Kill(string nickname, string comment)
		{
			return "KILL " + nickname + " :" + comment;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006790 File Offset: 0x00004990
		public static string Ping(string server)
		{
			return "PING " + server;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000679D File Offset: 0x0000499D
		public static string Ping(string server, string server2)
		{
			return "PING " + server + " " + server2;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000067B0 File Offset: 0x000049B0
		public static string Pong(string server)
		{
			return "PONG " + server;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000067BD File Offset: 0x000049BD
		public static string Pong(string server, string server2)
		{
			return "PONG " + server + " " + server2;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000067D0 File Offset: 0x000049D0
		public static string Error(string errormessage)
		{
			return "ERROR :" + errormessage;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000067DD File Offset: 0x000049DD
		public static string Away()
		{
			return "AWAY";
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000067E4 File Offset: 0x000049E4
		public static string Away(string awaytext)
		{
			return "AWAY :" + awaytext;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000067F1 File Offset: 0x000049F1
		public static string Rehash()
		{
			return "REHASH";
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000067F8 File Offset: 0x000049F8
		public static string Die()
		{
			return "DIE";
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000067FF File Offset: 0x000049FF
		public static string Restart()
		{
			return "RESTART";
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00006806 File Offset: 0x00004A06
		public static string Summon(string user)
		{
			return "SUMMON " + user;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00006813 File Offset: 0x00004A13
		public static string Summon(string user, string target)
		{
			return string.Concat(new string[] { "SUMMON ", user, " ", target, user, " ", target });
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00006848 File Offset: 0x00004A48
		public static string Summon(string user, string target, string channel)
		{
			return string.Concat(new string[] { "SUMMON ", user, " ", target, " ", channel });
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006879 File Offset: 0x00004A79
		public static string Users()
		{
			return "USERS";
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00006880 File Offset: 0x00004A80
		public static string Users(string target)
		{
			return "USERS " + target;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000688D File Offset: 0x00004A8D
		public static string Wallops(string wallopstext)
		{
			return "WALLOPS :" + wallopstext;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000689A File Offset: 0x00004A9A
		public static string Userhost(string nickname)
		{
			return "USERHOST " + nickname;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000068A7 File Offset: 0x00004AA7
		public static string Userhost(string[] nicknames)
		{
			return "USERHOST " + string.Join(" ", nicknames);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000068BE File Offset: 0x00004ABE
		public static string Ison(string nickname)
		{
			return "ISON " + nickname;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000068CB File Offset: 0x00004ACB
		public static string Ison(string[] nicknames)
		{
			return "ISON " + string.Join(" ", nicknames);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000068E2 File Offset: 0x00004AE2
		public static string Quit()
		{
			return "QUIT";
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000068E9 File Offset: 0x00004AE9
		public static string Quit(string quitmessage)
		{
			return "QUIT :" + quitmessage;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000068F6 File Offset: 0x00004AF6
		public static string Squit(string server, string comment)
		{
			return "SQUIT " + server + " :" + comment;
		}

		// Token: 0x04000050 RID: 80
		private static readonly Regex NicknameRegex = new Regex("^[A-Za-z\\[\\]\\\\`_^{|}][A-Za-z0-9\\[\\]\\\\`_\\-^{|}]+$", 8);
	}
}
