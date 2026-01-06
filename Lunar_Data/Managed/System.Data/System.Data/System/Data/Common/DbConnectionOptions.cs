using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Data.Common
{
	// Token: 0x02000316 RID: 790
	internal class DbConnectionOptions
	{
		// Token: 0x060024EA RID: 9450 RVA: 0x000A6CC5 File Offset: 0x000A4EC5
		public string UsersConnectionString(bool hidePassword)
		{
			return this.UsersConnectionString(hidePassword, false);
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x000A6CD0 File Offset: 0x000A4ED0
		private string UsersConnectionString(bool hidePassword, bool forceHidePassword)
		{
			string usersConnectionString = this._usersConnectionString;
			if (this._hasPasswordKeyword && (forceHidePassword || (hidePassword && !this.HasPersistablePassword)))
			{
				this.ReplacePasswordPwd(out usersConnectionString, false);
			}
			return usersConnectionString ?? string.Empty;
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x060024EC RID: 9452 RVA: 0x000A6D0E File Offset: 0x000A4F0E
		internal bool HasPersistablePassword
		{
			get
			{
				return !this._hasPasswordKeyword || this.ConvertValueToBoolean("persist security info", false);
			}
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x000A6D28 File Offset: 0x000A4F28
		public bool ConvertValueToBoolean(string keyName, bool defaultValue)
		{
			string text;
			if (!this._parsetable.TryGetValue(keyName, out text))
			{
				return defaultValue;
			}
			return DbConnectionOptions.ConvertValueToBooleanInternal(keyName, text);
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x000A6D50 File Offset: 0x000A4F50
		internal static bool ConvertValueToBooleanInternal(string keyName, string stringValue)
		{
			if (DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "true") || DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "yes"))
			{
				return true;
			}
			if (DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "false") || DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "no"))
			{
				return false;
			}
			string text = stringValue.Trim();
			if (DbConnectionOptions.CompareInsensitiveInvariant(text, "true") || DbConnectionOptions.CompareInsensitiveInvariant(text, "yes"))
			{
				return true;
			}
			if (DbConnectionOptions.CompareInsensitiveInvariant(text, "false") || DbConnectionOptions.CompareInsensitiveInvariant(text, "no"))
			{
				return false;
			}
			throw ADP.InvalidConnectionOptionValue(keyName);
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x000A6DDA File Offset: 0x000A4FDA
		private static bool CompareInsensitiveInvariant(string strvalue, string strconst)
		{
			return StringComparer.OrdinalIgnoreCase.Compare(strvalue, strconst) == 0;
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x000A6DEC File Offset: 0x000A4FEC
		[Conditional("DEBUG")]
		[Conditional("DEBUG")]
		private static void DebugTraceKeyValuePair(string keyname, string keyvalue, Dictionary<string, string> synonyms)
		{
			string text = ((synonyms != null) ? synonyms[keyname] : keyname);
			if ("password" != text && "pwd" != text)
			{
				if (keyvalue != null)
				{
					DataCommonEventSource.Log.Trace<string, string>("<comm.DbConnectionOptions|INFO|ADV> KeyName='{0}', KeyValue='{1}'", keyname, keyvalue);
					return;
				}
				DataCommonEventSource.Log.Trace<string>("<comm.DbConnectionOptions|INFO|ADV> KeyName='{0}'", keyname);
			}
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x000A6E48 File Offset: 0x000A5048
		private static string GetKeyName(StringBuilder buffer)
		{
			int num = buffer.Length;
			while (0 < num && char.IsWhiteSpace(buffer[num - 1]))
			{
				num--;
			}
			return buffer.ToString(0, num).ToLower(CultureInfo.InvariantCulture);
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x000A6E88 File Offset: 0x000A5088
		private static string GetKeyValue(StringBuilder buffer, bool trimWhitespace)
		{
			int num = buffer.Length;
			int i = 0;
			if (trimWhitespace)
			{
				while (i < num)
				{
					if (!char.IsWhiteSpace(buffer[i]))
					{
						break;
					}
					i++;
				}
				while (0 < num && char.IsWhiteSpace(buffer[num - 1]))
				{
					num--;
				}
			}
			return buffer.ToString(i, num - i);
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x000A6EE0 File Offset: 0x000A50E0
		internal static int GetKeyValuePair(string connectionString, int currentPosition, StringBuilder buffer, bool useOdbcRules, out string keyname, out string keyvalue)
		{
			int num = currentPosition;
			buffer.Length = 0;
			keyname = null;
			keyvalue = null;
			char c = '\0';
			DbConnectionOptions.ParserState parserState = DbConnectionOptions.ParserState.NothingYet;
			int length = connectionString.Length;
			while (currentPosition < length)
			{
				c = connectionString[currentPosition];
				switch (parserState)
				{
				case DbConnectionOptions.ParserState.NothingYet:
					if (';' != c && !char.IsWhiteSpace(c))
					{
						if (c == '\0')
						{
							parserState = DbConnectionOptions.ParserState.NullTermination;
						}
						else
						{
							if (char.IsControl(c))
							{
								throw ADP.ConnectionStringSyntax(num);
							}
							num = currentPosition;
							if ('=' != c)
							{
								parserState = DbConnectionOptions.ParserState.Key;
								goto IL_0248;
							}
							parserState = DbConnectionOptions.ParserState.KeyEqual;
						}
					}
					break;
				case DbConnectionOptions.ParserState.Key:
					if ('=' == c)
					{
						parserState = DbConnectionOptions.ParserState.KeyEqual;
					}
					else
					{
						if (!char.IsWhiteSpace(c) && char.IsControl(c))
						{
							throw ADP.ConnectionStringSyntax(num);
						}
						goto IL_0248;
					}
					break;
				case DbConnectionOptions.ParserState.KeyEqual:
					if (!useOdbcRules && '=' == c)
					{
						parserState = DbConnectionOptions.ParserState.Key;
						goto IL_0248;
					}
					keyname = DbConnectionOptions.GetKeyName(buffer);
					if (string.IsNullOrEmpty(keyname))
					{
						throw ADP.ConnectionStringSyntax(num);
					}
					buffer.Length = 0;
					parserState = DbConnectionOptions.ParserState.KeyEnd;
					goto IL_0107;
				case DbConnectionOptions.ParserState.KeyEnd:
					goto IL_0107;
				case DbConnectionOptions.ParserState.UnquotedValue:
					if (char.IsWhiteSpace(c))
					{
						goto IL_0248;
					}
					if (char.IsControl(c))
					{
						goto IL_025C;
					}
					if (';' == c)
					{
						goto IL_025C;
					}
					goto IL_0248;
				case DbConnectionOptions.ParserState.DoubleQuoteValue:
					if ('"' == c)
					{
						parserState = DbConnectionOptions.ParserState.DoubleQuoteValueQuote;
					}
					else
					{
						if (c == '\0')
						{
							throw ADP.ConnectionStringSyntax(num);
						}
						goto IL_0248;
					}
					break;
				case DbConnectionOptions.ParserState.DoubleQuoteValueQuote:
					if ('"' == c)
					{
						parserState = DbConnectionOptions.ParserState.DoubleQuoteValue;
						goto IL_0248;
					}
					keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
					parserState = DbConnectionOptions.ParserState.QuotedValueEnd;
					goto IL_0212;
				case DbConnectionOptions.ParserState.SingleQuoteValue:
					if ('\'' == c)
					{
						parserState = DbConnectionOptions.ParserState.SingleQuoteValueQuote;
					}
					else
					{
						if (c == '\0')
						{
							throw ADP.ConnectionStringSyntax(num);
						}
						goto IL_0248;
					}
					break;
				case DbConnectionOptions.ParserState.SingleQuoteValueQuote:
					if ('\'' == c)
					{
						parserState = DbConnectionOptions.ParserState.SingleQuoteValue;
						goto IL_0248;
					}
					keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
					parserState = DbConnectionOptions.ParserState.QuotedValueEnd;
					goto IL_0212;
				case DbConnectionOptions.ParserState.BraceQuoteValue:
					if ('}' == c)
					{
						parserState = DbConnectionOptions.ParserState.BraceQuoteValueQuote;
						goto IL_0248;
					}
					if (c == '\0')
					{
						throw ADP.ConnectionStringSyntax(num);
					}
					goto IL_0248;
				case DbConnectionOptions.ParserState.BraceQuoteValueQuote:
					if ('}' == c)
					{
						parserState = DbConnectionOptions.ParserState.BraceQuoteValue;
						goto IL_0248;
					}
					keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
					parserState = DbConnectionOptions.ParserState.QuotedValueEnd;
					goto IL_0212;
				case DbConnectionOptions.ParserState.QuotedValueEnd:
					goto IL_0212;
				case DbConnectionOptions.ParserState.NullTermination:
					if (c != '\0' && !char.IsWhiteSpace(c))
					{
						throw ADP.ConnectionStringSyntax(currentPosition);
					}
					break;
				default:
					throw ADP.InternalError(ADP.InternalErrorCode.InvalidParserState1);
				}
				IL_0250:
				currentPosition++;
				continue;
				IL_0107:
				if (char.IsWhiteSpace(c))
				{
					goto IL_0250;
				}
				if (useOdbcRules)
				{
					if ('{' == c)
					{
						parserState = DbConnectionOptions.ParserState.BraceQuoteValue;
						goto IL_0248;
					}
				}
				else
				{
					if ('\'' == c)
					{
						parserState = DbConnectionOptions.ParserState.SingleQuoteValue;
						goto IL_0250;
					}
					if ('"' == c)
					{
						parserState = DbConnectionOptions.ParserState.DoubleQuoteValue;
						goto IL_0250;
					}
				}
				if (';' == c || c == '\0')
				{
					break;
				}
				if (char.IsControl(c))
				{
					throw ADP.ConnectionStringSyntax(num);
				}
				parserState = DbConnectionOptions.ParserState.UnquotedValue;
				goto IL_0248;
				IL_0212:
				if (char.IsWhiteSpace(c))
				{
					goto IL_0250;
				}
				if (';' == c)
				{
					break;
				}
				if (c == '\0')
				{
					parserState = DbConnectionOptions.ParserState.NullTermination;
					goto IL_0250;
				}
				throw ADP.ConnectionStringSyntax(num);
				IL_0248:
				buffer.Append(c);
				goto IL_0250;
			}
			IL_025C:
			switch (parserState)
			{
			case DbConnectionOptions.ParserState.NothingYet:
			case DbConnectionOptions.ParserState.KeyEnd:
			case DbConnectionOptions.ParserState.NullTermination:
				break;
			case DbConnectionOptions.ParserState.Key:
			case DbConnectionOptions.ParserState.DoubleQuoteValue:
			case DbConnectionOptions.ParserState.SingleQuoteValue:
			case DbConnectionOptions.ParserState.BraceQuoteValue:
				throw ADP.ConnectionStringSyntax(num);
			case DbConnectionOptions.ParserState.KeyEqual:
				keyname = DbConnectionOptions.GetKeyName(buffer);
				if (string.IsNullOrEmpty(keyname))
				{
					throw ADP.ConnectionStringSyntax(num);
				}
				break;
			case DbConnectionOptions.ParserState.UnquotedValue:
			{
				keyvalue = DbConnectionOptions.GetKeyValue(buffer, true);
				char c2 = keyvalue[keyvalue.Length - 1];
				if (!useOdbcRules && ('\'' == c2 || '"' == c2))
				{
					throw ADP.ConnectionStringSyntax(num);
				}
				break;
			}
			case DbConnectionOptions.ParserState.DoubleQuoteValueQuote:
			case DbConnectionOptions.ParserState.SingleQuoteValueQuote:
			case DbConnectionOptions.ParserState.BraceQuoteValueQuote:
			case DbConnectionOptions.ParserState.QuotedValueEnd:
				keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
				break;
			default:
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidParserState2);
			}
			if (';' == c && currentPosition < connectionString.Length)
			{
				currentPosition++;
			}
			return currentPosition;
		}

		// Token: 0x060024F4 RID: 9460 RVA: 0x000A7204 File Offset: 0x000A5404
		private static bool IsValueValidInternal(string keyvalue)
		{
			return keyvalue == null || -1 == keyvalue.IndexOf('\0');
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x000A7215 File Offset: 0x000A5415
		private static bool IsKeyNameValid(string keyname)
		{
			return keyname != null && (0 < keyname.Length && ';' != keyname[0] && !char.IsWhiteSpace(keyname[0])) && -1 == keyname.IndexOf('\0');
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x000A724C File Offset: 0x000A544C
		private static NameValuePair ParseInternal(Dictionary<string, string> parsetable, string connectionString, bool buildChain, Dictionary<string, string> synonyms, bool firstKey)
		{
			StringBuilder stringBuilder = new StringBuilder();
			NameValuePair nameValuePair = null;
			NameValuePair nameValuePair2 = null;
			int i = 0;
			int length = connectionString.Length;
			while (i < length)
			{
				int num = i;
				string text;
				string text2;
				i = DbConnectionOptions.GetKeyValuePair(connectionString, num, stringBuilder, firstKey, out text, out text2);
				if (string.IsNullOrEmpty(text))
				{
					break;
				}
				string text4;
				string text3 = ((synonyms != null) ? (synonyms.TryGetValue(text, out text4) ? text4 : null) : text);
				if (!DbConnectionOptions.IsKeyNameValid(text3))
				{
					throw ADP.KeywordNotSupported(text);
				}
				if (!firstKey || !parsetable.ContainsKey(text3))
				{
					parsetable[text3] = text2;
				}
				if (nameValuePair != null)
				{
					nameValuePair = (nameValuePair.Next = new NameValuePair(text3, text2, i - num));
				}
				else if (buildChain)
				{
					nameValuePair = (nameValuePair2 = new NameValuePair(text3, text2, i - num));
				}
			}
			return nameValuePair2;
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x000A730C File Offset: 0x000A550C
		internal NameValuePair ReplacePasswordPwd(out string constr, bool fakePassword)
		{
			int num = 0;
			NameValuePair nameValuePair = null;
			NameValuePair nameValuePair2 = null;
			NameValuePair nameValuePair3 = null;
			StringBuilder stringBuilder = new StringBuilder(this._usersConnectionString.Length);
			for (NameValuePair nameValuePair4 = this._keyChain; nameValuePair4 != null; nameValuePair4 = nameValuePair4.Next)
			{
				if ("password" != nameValuePair4.Name && "pwd" != nameValuePair4.Name)
				{
					stringBuilder.Append(this._usersConnectionString, num, nameValuePair4.Length);
					if (fakePassword)
					{
						nameValuePair3 = new NameValuePair(nameValuePair4.Name, nameValuePair4.Value, nameValuePair4.Length);
					}
				}
				else if (fakePassword)
				{
					stringBuilder.Append(nameValuePair4.Name).Append("=*;");
					nameValuePair3 = new NameValuePair(nameValuePair4.Name, "*", nameValuePair4.Name.Length + "=*;".Length);
				}
				if (fakePassword)
				{
					if (nameValuePair2 != null)
					{
						nameValuePair2 = (nameValuePair2.Next = nameValuePair3);
					}
					else
					{
						nameValuePair = (nameValuePair2 = nameValuePair3);
					}
				}
				num += nameValuePair4.Length;
			}
			constr = stringBuilder.ToString();
			return nameValuePair;
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x000A7420 File Offset: 0x000A5620
		public DbConnectionOptions(string connectionString, Dictionary<string, string> synonyms, bool useOdbcRules)
		{
			this._useOdbcRules = useOdbcRules;
			this._parsetable = new Dictionary<string, string>();
			this._usersConnectionString = ((connectionString != null) ? connectionString : "");
			if (0 < this._usersConnectionString.Length)
			{
				this._keyChain = DbConnectionOptions.ParseInternal(this._parsetable, this._usersConnectionString, true, synonyms, this._useOdbcRules);
				this._hasPasswordKeyword = this._parsetable.ContainsKey("password") || this._parsetable.ContainsKey("pwd");
				this._hasUserIdKeyword = this._parsetable.ContainsKey("user id") || this._parsetable.ContainsKey("uid");
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x060024F9 RID: 9465 RVA: 0x000A74D9 File Offset: 0x000A56D9
		internal Dictionary<string, string> Parsetable
		{
			get
			{
				return this._parsetable;
			}
		}

		// Token: 0x17000631 RID: 1585
		public string this[string keyword]
		{
			get
			{
				return this._parsetable[keyword];
			}
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x000A74F0 File Offset: 0x000A56F0
		internal static void AppendKeyValuePairBuilder(StringBuilder builder, string keyName, string keyValue, bool useOdbcRules)
		{
			ADP.CheckArgumentNull(builder, "builder");
			ADP.CheckArgumentLength(keyName, "keyName");
			if (keyName == null || !DbConnectionOptions.s_connectionStringValidKeyRegex.IsMatch(keyName))
			{
				throw ADP.InvalidKeyname(keyName);
			}
			if (keyValue != null && !DbConnectionOptions.IsValueValidInternal(keyValue))
			{
				throw ADP.InvalidValue(keyName);
			}
			if (0 < builder.Length && ';' != builder[builder.Length - 1])
			{
				builder.Append(';');
			}
			if (useOdbcRules)
			{
				builder.Append(keyName);
			}
			else
			{
				builder.Append(keyName.Replace("=", "=="));
			}
			builder.Append('=');
			if (keyValue != null)
			{
				if (useOdbcRules)
				{
					if (0 < keyValue.Length && ('{' == keyValue[0] || 0 <= keyValue.IndexOf(';') || string.Compare("Driver", keyName, StringComparison.OrdinalIgnoreCase) == 0) && !DbConnectionOptions.s_connectionStringQuoteOdbcValueRegex.IsMatch(keyValue))
					{
						builder.Append('{').Append(keyValue.Replace("}", "}}")).Append('}');
						return;
					}
					builder.Append(keyValue);
					return;
				}
				else
				{
					if (DbConnectionOptions.s_connectionStringQuoteValueRegex.IsMatch(keyValue))
					{
						builder.Append(keyValue);
						return;
					}
					if (-1 != keyValue.IndexOf('"') && -1 == keyValue.IndexOf('\''))
					{
						builder.Append('\'');
						builder.Append(keyValue);
						builder.Append('\'');
						return;
					}
					builder.Append('"');
					builder.Append(keyValue.Replace("\"", "\"\""));
					builder.Append('"');
				}
			}
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x000A766F File Offset: 0x000A586F
		protected internal virtual string Expand()
		{
			return this._usersConnectionString;
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x000A7678 File Offset: 0x000A5878
		internal string ExpandKeyword(string keyword, string replacementValue)
		{
			bool flag = false;
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder(this._usersConnectionString.Length);
			for (NameValuePair nameValuePair = this._keyChain; nameValuePair != null; nameValuePair = nameValuePair.Next)
			{
				if (nameValuePair.Name == keyword && nameValuePair.Value == this[keyword])
				{
					DbConnectionOptions.AppendKeyValuePairBuilder(stringBuilder, nameValuePair.Name, replacementValue, this._useOdbcRules);
					stringBuilder.Append(';');
					flag = true;
				}
				else
				{
					stringBuilder.Append(this._usersConnectionString, num, nameValuePair.Length);
				}
				num += nameValuePair.Length;
			}
			if (!flag)
			{
				DbConnectionOptions.AppendKeyValuePairBuilder(stringBuilder, keyword, replacementValue, this._useOdbcRules);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x000A7723 File Offset: 0x000A5923
		internal static void ValidateKeyValuePair(string keyword, string value)
		{
			if (keyword == null || !DbConnectionOptions.s_connectionStringValidKeyRegex.IsMatch(keyword))
			{
				throw ADP.InvalidKeyname(keyword);
			}
			if (value != null && !DbConnectionOptions.s_connectionStringValidValueRegex.IsMatch(value))
			{
				throw ADP.InvalidValue(keyword);
			}
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x000A7754 File Offset: 0x000A5954
		public DbConnectionOptions(string connectionString, Dictionary<string, string> synonyms)
		{
			this._parsetable = new Dictionary<string, string>();
			this._usersConnectionString = ((connectionString != null) ? connectionString : "");
			if (0 < this._usersConnectionString.Length)
			{
				this._keyChain = DbConnectionOptions.ParseInternal(this._parsetable, this._usersConnectionString, true, synonyms, false);
				this._hasPasswordKeyword = this._parsetable.ContainsKey("password") || this._parsetable.ContainsKey("pwd");
			}
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x000A77D6 File Offset: 0x000A59D6
		protected DbConnectionOptions(DbConnectionOptions connectionOptions)
		{
			this._usersConnectionString = connectionOptions._usersConnectionString;
			this._hasPasswordKeyword = connectionOptions._hasPasswordKeyword;
			this._parsetable = connectionOptions._parsetable;
			this._keyChain = connectionOptions._keyChain;
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06002501 RID: 9473 RVA: 0x000A780E File Offset: 0x000A5A0E
		public bool IsEmpty
		{
			get
			{
				return this._keyChain == null;
			}
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x000A7819 File Offset: 0x000A5A19
		internal bool TryGetParsetableValue(string key, out string value)
		{
			return this._parsetable.TryGetValue(key, out value);
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x000A7828 File Offset: 0x000A5A28
		public bool ConvertValueToIntegratedSecurity()
		{
			string text;
			return this._parsetable.TryGetValue("integrated security", out text) && text != null && this.ConvertValueToIntegratedSecurityInternal(text);
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x000A7858 File Offset: 0x000A5A58
		internal bool ConvertValueToIntegratedSecurityInternal(string stringValue)
		{
			if (DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "sspi") || DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "true") || DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "yes"))
			{
				return true;
			}
			if (DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "false") || DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "no"))
			{
				return false;
			}
			string text = stringValue.Trim();
			if (DbConnectionOptions.CompareInsensitiveInvariant(text, "sspi") || DbConnectionOptions.CompareInsensitiveInvariant(text, "true") || DbConnectionOptions.CompareInsensitiveInvariant(text, "yes"))
			{
				return true;
			}
			if (DbConnectionOptions.CompareInsensitiveInvariant(text, "false") || DbConnectionOptions.CompareInsensitiveInvariant(text, "no"))
			{
				return false;
			}
			throw ADP.InvalidConnectionOptionValue("integrated security");
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x000A7900 File Offset: 0x000A5B00
		public int ConvertValueToInt32(string keyName, int defaultValue)
		{
			string text;
			if (!this._parsetable.TryGetValue(keyName, out text) || text == null)
			{
				return defaultValue;
			}
			return DbConnectionOptions.ConvertToInt32Internal(keyName, text);
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x000A792C File Offset: 0x000A5B2C
		internal static int ConvertToInt32Internal(string keyname, string stringValue)
		{
			int num;
			try
			{
				num = int.Parse(stringValue, NumberStyles.Integer, CultureInfo.InvariantCulture);
			}
			catch (FormatException ex)
			{
				throw ADP.InvalidConnectionOptionValue(keyname, ex);
			}
			catch (OverflowException ex2)
			{
				throw ADP.InvalidConnectionOptionValue(keyname, ex2);
			}
			return num;
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x000A7978 File Offset: 0x000A5B78
		public string ConvertValueToString(string keyName, string defaultValue)
		{
			string text;
			if (!this._parsetable.TryGetValue(keyName, out text) || text == null)
			{
				return defaultValue;
			}
			return text;
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x000A799B File Offset: 0x000A5B9B
		public bool ContainsKey(string keyword)
		{
			return this._parsetable.ContainsKey(keyword);
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x000A79AC File Offset: 0x000A5BAC
		internal static string ExpandDataDirectory(string keyword, string value, ref string datadir)
		{
			string text = null;
			if (value != null && value.StartsWith("|datadirectory|", StringComparison.OrdinalIgnoreCase))
			{
				string text2 = datadir;
				if (text2 == null)
				{
					object data = AppDomain.CurrentDomain.GetData("DataDirectory");
					text2 = data as string;
					if (data != null && text2 == null)
					{
						throw ADP.InvalidDataDirectory();
					}
					if (string.IsNullOrEmpty(text2))
					{
						text2 = AppDomain.CurrentDomain.BaseDirectory;
					}
					if (text2 == null)
					{
						text2 = "";
					}
					datadir = text2;
				}
				int length = "|datadirectory|".Length;
				bool flag = 0 < text2.Length && text2[text2.Length - 1] == '\\';
				bool flag2 = length < value.Length && value[length] == '\\';
				if (!flag && !flag2)
				{
					text = text2 + "\\" + value.Substring(length);
				}
				else if (flag && flag2)
				{
					text = text2 + value.Substring(length + 1);
				}
				else
				{
					text = text2 + value.Substring(length);
				}
				if (!ADP.GetFullPath(text).StartsWith(text2, StringComparison.Ordinal))
				{
					throw ADP.InvalidConnectionOptionValue(keyword);
				}
			}
			return text;
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x000A7ABC File Offset: 0x000A5CBC
		internal string ExpandDataDirectories(ref string filename, ref int position)
		{
			StringBuilder stringBuilder = new StringBuilder(this._usersConnectionString.Length);
			string text = null;
			int num = 0;
			bool flag = false;
			string text2;
			for (NameValuePair nameValuePair = this._keyChain; nameValuePair != null; nameValuePair = nameValuePair.Next)
			{
				text2 = nameValuePair.Value;
				if (this._useOdbcRules)
				{
					string text3 = nameValuePair.Name;
					if (!(text3 == "driver") && !(text3 == "pwd") && !(text3 == "uid"))
					{
						text2 = DbConnectionOptions.ExpandDataDirectory(nameValuePair.Name, text2, ref text);
					}
				}
				else
				{
					string text3 = nameValuePair.Name;
					uint num2 = global::<PrivateImplementationDetails>.ComputeStringHash(text3);
					if (num2 <= 2781420622U)
					{
						if (num2 <= 1433271620U)
						{
							if (num2 != 910909208U)
							{
								if (num2 == 1433271620U)
								{
									if (text3 == "pwd")
									{
										goto IL_01AB;
									}
								}
							}
							else if (text3 == "password")
							{
								goto IL_01AB;
							}
						}
						else if (num2 != 1556604621U)
						{
							if (num2 == 2781420622U)
							{
								if (text3 == "data provider")
								{
									goto IL_01AB;
								}
							}
						}
						else if (text3 == "uid")
						{
							goto IL_01AB;
						}
					}
					else if (num2 <= 3082861500U)
					{
						if (num2 != 2906666283U)
						{
							if (num2 == 3082861500U)
							{
								if (text3 == "provider")
								{
									goto IL_01AB;
								}
							}
						}
						else if (text3 == "user id")
						{
							goto IL_01AB;
						}
					}
					else if (num2 != 4008387664U)
					{
						if (num2 == 4015305829U)
						{
							if (text3 == "extended properties")
							{
								goto IL_01AB;
							}
						}
					}
					else if (text3 == "remote provider")
					{
						goto IL_01AB;
					}
					text2 = DbConnectionOptions.ExpandDataDirectory(nameValuePair.Name, text2, ref text);
				}
				IL_01AB:
				if (text2 == null)
				{
					text2 = nameValuePair.Value;
				}
				if (this._useOdbcRules || "file name" != nameValuePair.Name)
				{
					if (text2 != nameValuePair.Value)
					{
						flag = true;
						DbConnectionOptions.AppendKeyValuePairBuilder(stringBuilder, nameValuePair.Name, text2, this._useOdbcRules);
						stringBuilder.Append(';');
					}
					else
					{
						stringBuilder.Append(this._usersConnectionString, num, nameValuePair.Length);
					}
				}
				else
				{
					flag = true;
					filename = text2;
					position = stringBuilder.Length;
				}
				num += nameValuePair.Length;
			}
			if (flag)
			{
				text2 = stringBuilder.ToString();
			}
			else
			{
				text2 = null;
			}
			return text2;
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x0600250B RID: 9483 RVA: 0x000A7D1C File Offset: 0x000A5F1C
		internal bool HasBlankPassword
		{
			get
			{
				if (this.ConvertValueToIntegratedSecurity())
				{
					return false;
				}
				if (this._parsetable.ContainsKey("password"))
				{
					return ADP.IsEmpty(this._parsetable["password"]);
				}
				if (this._parsetable.ContainsKey("pwd"))
				{
					return ADP.IsEmpty(this._parsetable["pwd"]);
				}
				return (this._parsetable.ContainsKey("user id") && !ADP.IsEmpty(this._parsetable["user id"])) || (this._parsetable.ContainsKey("uid") && !ADP.IsEmpty(this._parsetable["uid"]));
			}
		}

		// Token: 0x04001810 RID: 6160
		private const string ConnectionStringValidKeyPattern = "^(?![;\\s])[^\\p{Cc}]+(?<!\\s)$";

		// Token: 0x04001811 RID: 6161
		private const string ConnectionStringValidValuePattern = "^[^\0]*$";

		// Token: 0x04001812 RID: 6162
		private const string ConnectionStringQuoteValuePattern = "^[^\"'=;\\s\\p{Cc}]*$";

		// Token: 0x04001813 RID: 6163
		private const string ConnectionStringQuoteOdbcValuePattern = "^\\{([^\\}\0]|\\}\\})*\\}$";

		// Token: 0x04001814 RID: 6164
		internal const string DataDirectory = "|datadirectory|";

		// Token: 0x04001815 RID: 6165
		private static readonly Regex s_connectionStringValidKeyRegex = new Regex("^(?![;\\s])[^\\p{Cc}]+(?<!\\s)$", RegexOptions.Compiled);

		// Token: 0x04001816 RID: 6166
		private static readonly Regex s_connectionStringValidValueRegex = new Regex("^[^\0]*$", RegexOptions.Compiled);

		// Token: 0x04001817 RID: 6167
		private static readonly Regex s_connectionStringQuoteValueRegex = new Regex("^[^\"'=;\\s\\p{Cc}]*$", RegexOptions.Compiled);

		// Token: 0x04001818 RID: 6168
		private static readonly Regex s_connectionStringQuoteOdbcValueRegex = new Regex("^\\{([^\\}\0]|\\}\\})*\\}$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x04001819 RID: 6169
		private readonly string _usersConnectionString;

		// Token: 0x0400181A RID: 6170
		private readonly Dictionary<string, string> _parsetable;

		// Token: 0x0400181B RID: 6171
		internal readonly NameValuePair _keyChain;

		// Token: 0x0400181C RID: 6172
		internal readonly bool _hasPasswordKeyword;

		// Token: 0x0400181D RID: 6173
		internal readonly bool _useOdbcRules;

		// Token: 0x0400181E RID: 6174
		internal readonly bool _hasUserIdKeyword;

		// Token: 0x02000317 RID: 791
		private static class KEY
		{
			// Token: 0x0400181F RID: 6175
			internal const string Integrated_Security = "integrated security";

			// Token: 0x04001820 RID: 6176
			internal const string Password = "password";

			// Token: 0x04001821 RID: 6177
			internal const string Persist_Security_Info = "persist security info";

			// Token: 0x04001822 RID: 6178
			internal const string User_ID = "user id";
		}

		// Token: 0x02000318 RID: 792
		private static class SYNONYM
		{
			// Token: 0x04001823 RID: 6179
			internal const string Pwd = "pwd";

			// Token: 0x04001824 RID: 6180
			internal const string UID = "uid";
		}

		// Token: 0x02000319 RID: 793
		private enum ParserState
		{
			// Token: 0x04001826 RID: 6182
			NothingYet = 1,
			// Token: 0x04001827 RID: 6183
			Key,
			// Token: 0x04001828 RID: 6184
			KeyEqual,
			// Token: 0x04001829 RID: 6185
			KeyEnd,
			// Token: 0x0400182A RID: 6186
			UnquotedValue,
			// Token: 0x0400182B RID: 6187
			DoubleQuoteValue,
			// Token: 0x0400182C RID: 6188
			DoubleQuoteValueQuote,
			// Token: 0x0400182D RID: 6189
			SingleQuoteValue,
			// Token: 0x0400182E RID: 6190
			SingleQuoteValueQuote,
			// Token: 0x0400182F RID: 6191
			BraceQuoteValue,
			// Token: 0x04001830 RID: 6192
			BraceQuoteValueQuote,
			// Token: 0x04001831 RID: 6193
			QuotedValueEnd,
			// Token: 0x04001832 RID: 6194
			NullTermination
		}
	}
}
