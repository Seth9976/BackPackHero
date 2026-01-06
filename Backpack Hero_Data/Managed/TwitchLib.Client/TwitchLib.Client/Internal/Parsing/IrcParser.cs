using System;
using System.Collections.Generic;
using TwitchLib.Client.Enums.Internal;
using TwitchLib.Client.Models.Internal;

namespace TwitchLib.Client.Internal.Parsing
{
	// Token: 0x02000005 RID: 5
	internal class IrcParser
	{
		// Token: 0x06000130 RID: 304 RVA: 0x0000691C File Offset: 0x00004B1C
		public IrcMessage ParseIrcMessage(string raw)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			IrcParser.ParserState parserState = IrcParser.ParserState.STATE_NONE;
			int[] array = new int[6];
			int[] array2 = new int[6];
			for (int i = 0; i < raw.Length; i++)
			{
				array2[(int)parserState] = i - array[(int)parserState] - 1;
				if (parserState == IrcParser.ParserState.STATE_NONE && raw.get_Chars(i) == '@')
				{
					parserState = IrcParser.ParserState.STATE_V3;
					i = (array[(int)parserState] = i + 1);
					int num = i;
					string text = null;
					while (i < raw.Length)
					{
						if (raw.get_Chars(i) == '=')
						{
							text = raw.Substring(num, i - num);
							num = i + 1;
						}
						else if (raw.get_Chars(i) == ';')
						{
							if (text == null)
							{
								dictionary[raw.Substring(num, i - num)] = "1";
							}
							else
							{
								dictionary[text] = raw.Substring(num, i - num);
							}
							num = i + 1;
						}
						else if (raw.get_Chars(i) == ' ')
						{
							if (text == null)
							{
								dictionary[raw.Substring(num, i - num)] = "1";
								break;
							}
							dictionary[text] = raw.Substring(num, i - num);
							break;
						}
						i++;
					}
				}
				else if (parserState < IrcParser.ParserState.STATE_PREFIX && raw.get_Chars(i) == ':')
				{
					parserState = IrcParser.ParserState.STATE_PREFIX;
					i = (array[(int)parserState] = i + 1);
				}
				else if (parserState < IrcParser.ParserState.STATE_COMMAND)
				{
					parserState = IrcParser.ParserState.STATE_COMMAND;
					array[(int)parserState] = i;
				}
				else
				{
					if (parserState < IrcParser.ParserState.STATE_TRAILING && raw.get_Chars(i) == ':')
					{
						parserState = IrcParser.ParserState.STATE_TRAILING;
						array[(int)parserState] = i + 1;
						break;
					}
					if ((parserState < IrcParser.ParserState.STATE_TRAILING && raw.get_Chars(i) == '+') || (parserState < IrcParser.ParserState.STATE_TRAILING && raw.get_Chars(i) == '-'))
					{
						parserState = IrcParser.ParserState.STATE_TRAILING;
						array[(int)parserState] = i;
						break;
					}
					if (parserState == IrcParser.ParserState.STATE_COMMAND)
					{
						parserState = IrcParser.ParserState.STATE_PARAM;
						array[(int)parserState] = i;
					}
				}
				while (i < raw.Length && raw.get_Chars(i) != ' ')
				{
					i++;
				}
			}
			array2[(int)parserState] = raw.Length - array[(int)parserState];
			string text2 = raw.Substring(array[3], array2[3]);
			IrcCommand ircCommand = IrcCommand.Unknown;
			if (text2 != null)
			{
				uint num2 = <PrivateImplementationDetails>.ComputeStringHash(text2);
				if (num2 <= 2744757958U)
				{
					if (num2 <= 1162767579U)
					{
						if (num2 <= 613830808U)
						{
							if (num2 != 43150473U)
							{
								if (num2 != 603386935U)
								{
									if (num2 == 613830808U)
									{
										if (text2 == "PASS")
										{
											ircCommand = IrcCommand.Pass;
										}
									}
								}
								else if (text2 == "PONG")
								{
									ircCommand = IrcCommand.Pong;
								}
							}
							else if (text2 == "PING")
							{
								ircCommand = IrcCommand.Ping;
							}
						}
						else if (num2 <= 1043585426U)
						{
							if (num2 != 630461332U)
							{
								if (num2 == 1043585426U)
								{
									if (text2 == "GLOBALUSERSTATE")
									{
										ircCommand = IrcCommand.GlobalUserState;
									}
								}
							}
							else if (text2 == "PART")
							{
								ircCommand = IrcCommand.Part;
							}
						}
						else if (num2 != 1146928018U)
						{
							if (num2 == 1162767579U)
							{
								if (text2 == "CAP")
								{
									ircCommand = IrcCommand.Cap;
								}
							}
						}
						else if (text2 == "353")
						{
							ircCommand = IrcCommand.RPL_353;
						}
					}
					else if (num2 <= 2029756220U)
					{
						if (num2 != 1958281977U)
						{
							if (num2 != 1998532429U)
							{
								if (num2 == 2029756220U)
								{
									if (text2 == "CLEARCHAT")
									{
										ircCommand = IrcCommand.ClearChat;
									}
								}
							}
							else if (text2 == "WHISPER")
							{
								ircCommand = IrcCommand.Whisper;
							}
						}
						else if (text2 == "JOIN")
						{
							ircCommand = IrcCommand.Join;
						}
					}
					else if (num2 <= 2509909495U)
					{
						if (num2 != 2132309426U)
						{
							if (num2 == 2509909495U)
							{
								if (text2 == "CLEARMSG")
								{
									ircCommand = IrcCommand.ClearMsg;
								}
							}
						}
						else if (text2 == "USERNOTICE")
						{
							ircCommand = IrcCommand.UserNotice;
						}
					}
					else if (num2 != 2561111319U)
					{
						if (num2 == 2744757958U)
						{
							if (text2 == "RECONNECT")
							{
								ircCommand = IrcCommand.Reconnect;
							}
						}
					}
					else if (text2 == "USERSTATE")
					{
						ircCommand = IrcCommand.UserState;
					}
				}
				else if (num2 <= 3593007890U)
				{
					if (num2 <= 3478472606U)
					{
						if (num2 != 2876042605U)
						{
							if (num2 != 2888337414U)
							{
								if (num2 == 3478472606U)
								{
									if (text2 == "366")
									{
										ircCommand = IrcCommand.RPL_366;
									}
								}
							}
							else if (text2 == "SERVERCHANGE")
							{
								ircCommand = IrcCommand.ServerChange;
							}
						}
						else if (text2 == "PRIVMSG")
						{
							ircCommand = IrcCommand.PrivMsg;
						}
					}
					else if (num2 <= 3528952558U)
					{
						if (num2 != 3512174939U)
						{
							if (num2 == 3528952558U)
							{
								if (text2 == "375")
								{
									ircCommand = IrcCommand.RPL_375;
								}
							}
						}
						else if (text2 == "376")
						{
							ircCommand = IrcCommand.RPL_376;
						}
					}
					else if (num2 != 3579285415U)
					{
						if (num2 == 3593007890U)
						{
							if (text2 == "MODE")
							{
								ircCommand = IrcCommand.Mode;
							}
						}
					}
					else if (text2 == "372")
					{
						ircCommand = IrcCommand.RPL_372;
					}
				}
				else if (num2 <= 3838523700U)
				{
					if (num2 != 3667541486U)
					{
						if (num2 != 3788190843U)
						{
							if (num2 == 3838523700U)
							{
								if (text2 == "001")
								{
									ircCommand = IrcCommand.RPL_001;
								}
							}
						}
						else if (text2 == "004")
						{
							ircCommand = IrcCommand.RPL_004;
						}
					}
					else if (text2 == "NICK")
					{
						ircCommand = IrcCommand.Nick;
					}
				}
				else if (num2 <= 3888856557U)
				{
					if (num2 != 3872078938U)
					{
						if (num2 == 3888856557U)
						{
							if (text2 == "002")
							{
								ircCommand = IrcCommand.RPL_002;
							}
						}
					}
					else if (text2 == "003")
					{
						ircCommand = IrcCommand.RPL_003;
					}
				}
				else if (num2 != 3981314255U)
				{
					if (num2 == 3983032113U)
					{
						if (text2 == "NOTICE")
						{
							ircCommand = IrcCommand.Notice;
						}
					}
				}
				else if (text2 == "ROOMSTATE")
				{
					ircCommand = IrcCommand.RoomState;
				}
			}
			string text3 = raw.Substring(array[4], array2[4]);
			string text4 = raw.Substring(array[5], array2[5]);
			string text5 = raw.Substring(array[2], array2[2]);
			return new IrcMessage(ircCommand, new string[] { text3, text4 }, text5, dictionary);
		}

		// Token: 0x02000061 RID: 97
		private enum ParserState
		{
			// Token: 0x040000CA RID: 202
			STATE_NONE,
			// Token: 0x040000CB RID: 203
			STATE_V3,
			// Token: 0x040000CC RID: 204
			STATE_PREFIX,
			// Token: 0x040000CD RID: 205
			STATE_COMMAND,
			// Token: 0x040000CE RID: 206
			STATE_PARAM,
			// Token: 0x040000CF RID: 207
			STATE_TRAILING
		}
	}
}
