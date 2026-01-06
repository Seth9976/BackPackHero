using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Configuration;
using System.Text;

namespace System.Net
{
	// Token: 0x020003E0 RID: 992
	internal sealed class HttpListenerRequestUriBuilder
	{
		// Token: 0x06002087 RID: 8327 RVA: 0x0007711C File Offset: 0x0007531C
		private HttpListenerRequestUriBuilder(string rawUri, string cookedUriScheme, string cookedUriHost, string cookedUriPath, string cookedUriQuery)
		{
			this.rawUri = rawUri;
			this.cookedUriScheme = cookedUriScheme;
			this.cookedUriHost = cookedUriHost;
			this.cookedUriPath = HttpListenerRequestUriBuilder.AddSlashToAsteriskOnlyPath(cookedUriPath);
			if (cookedUriQuery == null)
			{
				this.cookedUriQuery = string.Empty;
				return;
			}
			this.cookedUriQuery = cookedUriQuery;
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x00077169 File Offset: 0x00075369
		public static Uri GetRequestUri(string rawUri, string cookedUriScheme, string cookedUriHost, string cookedUriPath, string cookedUriQuery)
		{
			return new HttpListenerRequestUriBuilder(rawUri, cookedUriScheme, cookedUriHost, cookedUriPath, cookedUriQuery).Build();
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x0007717C File Offset: 0x0007537C
		private Uri Build()
		{
			if (HttpListenerRequestUriBuilder.useCookedRequestUrl)
			{
				this.BuildRequestUriUsingCookedPath();
				if (this.requestUri == null)
				{
					this.BuildRequestUriUsingRawPath();
				}
			}
			else
			{
				this.BuildRequestUriUsingRawPath();
				if (this.requestUri == null)
				{
					this.BuildRequestUriUsingCookedPath();
				}
			}
			return this.requestUri;
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x000771CC File Offset: 0x000753CC
		private void BuildRequestUriUsingCookedPath()
		{
			if (!Uri.TryCreate(string.Concat(new string[]
			{
				this.cookedUriScheme,
				Uri.SchemeDelimiter,
				this.cookedUriHost,
				this.cookedUriPath,
				this.cookedUriQuery
			}), UriKind.Absolute, out this.requestUri))
			{
				this.LogWarning("BuildRequestUriUsingCookedPath", "Can't create Uri from string '{0}://{1}{2}{3}'.", new object[] { this.cookedUriScheme, this.cookedUriHost, this.cookedUriPath, this.cookedUriQuery });
			}
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x00077258 File Offset: 0x00075458
		private void BuildRequestUriUsingRawPath()
		{
			this.rawPath = HttpListenerRequestUriBuilder.GetPath(this.rawUri);
			bool flag;
			if (this.rawPath == string.Empty)
			{
				string text = this.rawPath;
				if (text == string.Empty)
				{
					text = "/";
				}
				flag = Uri.TryCreate(string.Concat(new string[]
				{
					this.cookedUriScheme,
					Uri.SchemeDelimiter,
					this.cookedUriHost,
					text,
					this.cookedUriQuery
				}), UriKind.Absolute, out this.requestUri);
			}
			else
			{
				HttpListenerRequestUriBuilder.ParsingResult parsingResult = this.BuildRequestUriUsingRawPath(HttpListenerRequestUriBuilder.GetEncoding(HttpListenerRequestUriBuilder.EncodingType.Primary));
				if (parsingResult == HttpListenerRequestUriBuilder.ParsingResult.EncodingError)
				{
					Encoding encoding = HttpListenerRequestUriBuilder.GetEncoding(HttpListenerRequestUriBuilder.EncodingType.Secondary);
					parsingResult = this.BuildRequestUriUsingRawPath(encoding);
				}
				flag = parsingResult == HttpListenerRequestUriBuilder.ParsingResult.Success;
			}
			if (!flag)
			{
				this.LogWarning("BuildRequestUriUsingRawPath", "Can't create Uri from string '{0}://{1}{2}{3}'.", new object[] { this.cookedUriScheme, this.cookedUriHost, this.rawPath, this.cookedUriQuery });
			}
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x0007734A File Offset: 0x0007554A
		private static Encoding GetEncoding(HttpListenerRequestUriBuilder.EncodingType type)
		{
			if (type == HttpListenerRequestUriBuilder.EncodingType.Secondary)
			{
				return HttpListenerRequestUriBuilder.ansiEncoding;
			}
			return HttpListenerRequestUriBuilder.utf8Encoding;
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x00077360 File Offset: 0x00075560
		private HttpListenerRequestUriBuilder.ParsingResult BuildRequestUriUsingRawPath(Encoding encoding)
		{
			this.rawOctets = new List<byte>();
			this.requestUriString = new StringBuilder();
			this.requestUriString.Append(this.cookedUriScheme);
			this.requestUriString.Append(Uri.SchemeDelimiter);
			this.requestUriString.Append(this.cookedUriHost);
			HttpListenerRequestUriBuilder.ParsingResult parsingResult = this.ParseRawPath(encoding);
			if (parsingResult == HttpListenerRequestUriBuilder.ParsingResult.Success)
			{
				this.requestUriString.Append(this.cookedUriQuery);
				if (!Uri.TryCreate(this.requestUriString.ToString(), UriKind.Absolute, out this.requestUri))
				{
					parsingResult = HttpListenerRequestUriBuilder.ParsingResult.InvalidString;
				}
			}
			if (parsingResult != HttpListenerRequestUriBuilder.ParsingResult.Success)
			{
				this.LogWarning("BuildRequestUriUsingRawPath", "Can't convert Uri path '{0}' using encoding '{1}'.", new object[] { this.rawPath, encoding.EncodingName });
			}
			return parsingResult;
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x0007741C File Offset: 0x0007561C
		private HttpListenerRequestUriBuilder.ParsingResult ParseRawPath(Encoding encoding)
		{
			int i = 0;
			while (i < this.rawPath.Length)
			{
				char c = this.rawPath[i];
				if (c == '%')
				{
					i++;
					c = this.rawPath[i];
					if (c == 'u' || c == 'U')
					{
						if (!this.EmptyDecodeAndAppendRawOctetsList(encoding))
						{
							return HttpListenerRequestUriBuilder.ParsingResult.EncodingError;
						}
						if (!this.AppendUnicodeCodePointValuePercentEncoded(this.rawPath.Substring(i + 1, 4)))
						{
							return HttpListenerRequestUriBuilder.ParsingResult.InvalidString;
						}
						i += 5;
					}
					else
					{
						if (!this.AddPercentEncodedOctetToRawOctetsList(encoding, this.rawPath.Substring(i, 2)))
						{
							return HttpListenerRequestUriBuilder.ParsingResult.InvalidString;
						}
						i += 2;
					}
				}
				else
				{
					if (!this.EmptyDecodeAndAppendRawOctetsList(encoding))
					{
						return HttpListenerRequestUriBuilder.ParsingResult.EncodingError;
					}
					this.requestUriString.Append(c);
					i++;
				}
			}
			if (!this.EmptyDecodeAndAppendRawOctetsList(encoding))
			{
				return HttpListenerRequestUriBuilder.ParsingResult.EncodingError;
			}
			return HttpListenerRequestUriBuilder.ParsingResult.Success;
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x000774E0 File Offset: 0x000756E0
		private bool AppendUnicodeCodePointValuePercentEncoded(string codePoint)
		{
			int num;
			if (!int.TryParse(codePoint, NumberStyles.HexNumber, null, out num))
			{
				this.LogWarning("AppendUnicodeCodePointValuePercentEncoded", "Can't convert percent encoded value '{0}'.", new object[] { codePoint });
				return false;
			}
			string text = null;
			try
			{
				text = char.ConvertFromUtf32(num);
				HttpListenerRequestUriBuilder.AppendOctetsPercentEncoded(this.requestUriString, HttpListenerRequestUriBuilder.utf8Encoding.GetBytes(text));
				return true;
			}
			catch (ArgumentOutOfRangeException)
			{
				this.LogWarning("AppendUnicodeCodePointValuePercentEncoded", "Can't convert percent encoded value '{0}'.", new object[] { codePoint });
			}
			catch (EncoderFallbackException ex)
			{
				this.LogWarning("AppendUnicodeCodePointValuePercentEncoded", "Can't convert string '{0}' into UTF-8 bytes: {1}", new object[] { text, ex.Message });
			}
			return false;
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x000775A0 File Offset: 0x000757A0
		private bool AddPercentEncodedOctetToRawOctetsList(Encoding encoding, string escapedCharacter)
		{
			byte b;
			if (!byte.TryParse(escapedCharacter, NumberStyles.HexNumber, null, out b))
			{
				this.LogWarning("AddPercentEncodedOctetToRawOctetsList", "Can't convert percent encoded value '{0}'.", new object[] { escapedCharacter });
				return false;
			}
			this.rawOctets.Add(b);
			return true;
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x000775E8 File Offset: 0x000757E8
		private bool EmptyDecodeAndAppendRawOctetsList(Encoding encoding)
		{
			if (this.rawOctets.Count == 0)
			{
				return true;
			}
			string text = null;
			try
			{
				text = encoding.GetString(this.rawOctets.ToArray());
				if (encoding == HttpListenerRequestUriBuilder.utf8Encoding)
				{
					HttpListenerRequestUriBuilder.AppendOctetsPercentEncoded(this.requestUriString, this.rawOctets.ToArray());
				}
				else
				{
					HttpListenerRequestUriBuilder.AppendOctetsPercentEncoded(this.requestUriString, HttpListenerRequestUriBuilder.utf8Encoding.GetBytes(text));
				}
				this.rawOctets.Clear();
				return true;
			}
			catch (DecoderFallbackException ex)
			{
				this.LogWarning("EmptyDecodeAndAppendRawOctetsList", "Can't convert bytes '{0}' into UTF-16 characters: {1}", new object[]
				{
					HttpListenerRequestUriBuilder.GetOctetsAsString(this.rawOctets),
					ex.Message
				});
			}
			catch (EncoderFallbackException ex2)
			{
				this.LogWarning("EmptyDecodeAndAppendRawOctetsList", "Can't convert string '{0}' into UTF-8 bytes: {1}", new object[] { text, ex2.Message });
			}
			return false;
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x000776D4 File Offset: 0x000758D4
		private static void AppendOctetsPercentEncoded(StringBuilder target, IEnumerable<byte> octets)
		{
			foreach (byte b in octets)
			{
				target.Append('%');
				target.Append(b.ToString("X2", CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x00077738 File Offset: 0x00075938
		private static string GetOctetsAsString(IEnumerable<byte> octets)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (byte b in octets)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(b.ToString("X2", CultureInfo.InvariantCulture));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x000777B4 File Offset: 0x000759B4
		private static string GetPath(string uriString)
		{
			int num = 0;
			if (uriString[0] != '/')
			{
				int num2 = 0;
				if (uriString.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
				{
					num2 = 7;
				}
				else if (uriString.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
				{
					num2 = 8;
				}
				if (num2 > 0)
				{
					num = uriString.IndexOf('/', num2);
					if (num == -1)
					{
						num = uriString.Length;
					}
				}
				else
				{
					uriString = "/" + uriString;
				}
			}
			int num3 = uriString.IndexOf('?');
			if (num3 == -1)
			{
				num3 = uriString.Length;
			}
			return HttpListenerRequestUriBuilder.AddSlashToAsteriskOnlyPath(uriString.Substring(num, num3 - num));
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x0007783D File Offset: 0x00075A3D
		private static string AddSlashToAsteriskOnlyPath(string path)
		{
			if (path.Length == 1 && path[0] == '*')
			{
				return "/*";
			}
			return path;
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x0007785A File Offset: 0x00075A5A
		private void LogWarning(string methodName, string message, params object[] args)
		{
			bool on = Logging.On;
		}

		// Token: 0x0400116C RID: 4460
		private static readonly bool useCookedRequestUrl = SettingsSectionInternal.Section.HttpListenerUnescapeRequestUrl;

		// Token: 0x0400116D RID: 4461
		private static readonly Encoding utf8Encoding = new UTF8Encoding(false, true);

		// Token: 0x0400116E RID: 4462
		private static readonly Encoding ansiEncoding = Encoding.GetEncoding(0, new EncoderExceptionFallback(), new DecoderExceptionFallback());

		// Token: 0x0400116F RID: 4463
		private readonly string rawUri;

		// Token: 0x04001170 RID: 4464
		private readonly string cookedUriScheme;

		// Token: 0x04001171 RID: 4465
		private readonly string cookedUriHost;

		// Token: 0x04001172 RID: 4466
		private readonly string cookedUriPath;

		// Token: 0x04001173 RID: 4467
		private readonly string cookedUriQuery;

		// Token: 0x04001174 RID: 4468
		private StringBuilder requestUriString;

		// Token: 0x04001175 RID: 4469
		private List<byte> rawOctets;

		// Token: 0x04001176 RID: 4470
		private string rawPath;

		// Token: 0x04001177 RID: 4471
		private Uri requestUri;

		// Token: 0x020003E1 RID: 993
		private enum ParsingResult
		{
			// Token: 0x04001179 RID: 4473
			Success,
			// Token: 0x0400117A RID: 4474
			InvalidString,
			// Token: 0x0400117B RID: 4475
			EncodingError
		}

		// Token: 0x020003E2 RID: 994
		private enum EncodingType
		{
			// Token: 0x0400117D RID: 4477
			Primary,
			// Token: 0x0400117E RID: 4478
			Secondary
		}
	}
}
