using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System.Net.Http.Headers
{
	/// <summary>Represents the value of the Cache-Control header.</summary>
	// Token: 0x02000034 RID: 52
	public class CacheControlHeaderValue : ICloneable
	{
		/// <summary>Cache-extension tokens, each with an optional assigned value.</summary>
		/// <returns>Returns <see cref="T:System.Collections.Generic.ICollection`1" />.A collection of cache-extension tokens each with an optional assigned value.</returns>
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00006B84 File Offset: 0x00004D84
		public ICollection<NameValueHeaderValue> Extensions
		{
			get
			{
				List<NameValueHeaderValue> list;
				if ((list = this.extensions) == null)
				{
					list = (this.extensions = new List<NameValueHeaderValue>());
				}
				return list;
			}
		}

		/// <summary>The maximum age, specified in seconds, that the HTTP client is willing to accept a response. </summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The time in seconds. </returns>
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00006BA9 File Offset: 0x00004DA9
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x00006BB1 File Offset: 0x00004DB1
		public TimeSpan? MaxAge { get; set; }

		/// <summary>Whether an HTTP client is willing to accept a response that has exceeded its expiration time.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the HTTP client is willing to accept a response that has exceed the expiration time; otherwise, false.</returns>
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00006BBA File Offset: 0x00004DBA
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00006BC2 File Offset: 0x00004DC2
		public bool MaxStale { get; set; }

		/// <summary>The maximum time, in seconds, an HTTP client is willing to accept a response that has exceeded its expiration time.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The time in seconds.</returns>
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00006BCB File Offset: 0x00004DCB
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00006BD3 File Offset: 0x00004DD3
		public TimeSpan? MaxStaleLimit { get; set; }

		/// <summary>The freshness lifetime, in seconds, that an HTTP client is willing to accept a response.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The time in seconds.</returns>
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00006BDC File Offset: 0x00004DDC
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00006BE4 File Offset: 0x00004DE4
		public TimeSpan? MinFresh { get; set; }

		/// <summary>Whether the origin server require revalidation of a cache entry on any subsequent use when the cache entry becomes stale.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the origin server requires revalidation of a cache entry on any subsequent use when the entry becomes stale; otherwise, false.</returns>
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00006BED File Offset: 0x00004DED
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00006BF5 File Offset: 0x00004DF5
		public bool MustRevalidate { get; set; }

		/// <summary>Whether an HTTP client is willing to accept a cached response.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the HTTP client is willing to accept a cached response; otherwise, false.</returns>
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00006BFE File Offset: 0x00004DFE
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00006C06 File Offset: 0x00004E06
		public bool NoCache { get; set; }

		/// <summary>A collection of fieldnames in the "no-cache" directive in a cache-control header field on an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.Collections.Generic.ICollection`1" />.A collection of fieldnames.</returns>
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00006C10 File Offset: 0x00004E10
		public ICollection<string> NoCacheHeaders
		{
			get
			{
				List<string> list;
				if ((list = this.no_cache_headers) == null)
				{
					list = (this.no_cache_headers = new List<string>());
				}
				return list;
			}
		}

		/// <summary>Whether a cache must not store any part of either the HTTP request mressage or any response.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if a cache must not store any part of either the HTTP request mressage or any response; otherwise, false.</returns>
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00006C35 File Offset: 0x00004E35
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00006C3D File Offset: 0x00004E3D
		public bool NoStore { get; set; }

		/// <summary>Whether a cache or proxy must not change any aspect of the entity-body.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if a cache or proxy must not change any aspect of the entity-body; otherwise, false.</returns>
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00006C46 File Offset: 0x00004E46
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00006C4E File Offset: 0x00004E4E
		public bool NoTransform { get; set; }

		/// <summary>Whether a cache should either respond using a cached entry that is consistent with the other constraints of the HTTP request, or respond with a 504 (Gateway Timeout) status.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if a cache should either respond using a cached entry that is consistent with the other constraints of the HTTP request, or respond with a 504 (Gateway Timeout) status; otherwise, false.</returns>
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00006C57 File Offset: 0x00004E57
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00006C5F File Offset: 0x00004E5F
		public bool OnlyIfCached { get; set; }

		/// <summary>Whether all or part of the HTTP response message is intended for a single user and must not be cached by a shared cache.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the HTTP response message is intended for a single user and must not be cached by a shared cache; otherwise, false.</returns>
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00006C68 File Offset: 0x00004E68
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x00006C70 File Offset: 0x00004E70
		public bool Private { get; set; }

		/// <summary>A collection fieldnames in the "private" directive in a cache-control header field on an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.Collections.Generic.ICollection`1" />.A collection of fieldnames.</returns>
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00006C7C File Offset: 0x00004E7C
		public ICollection<string> PrivateHeaders
		{
			get
			{
				List<string> list;
				if ((list = this.private_headers) == null)
				{
					list = (this.private_headers = new List<string>());
				}
				return list;
			}
		}

		/// <summary>Whether the origin server require revalidation of a cache entry on any subsequent use when the cache entry becomes stale for shared user agent caches.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the origin server requires revalidation of a cache entry on any subsequent use when the entry becomes stale for shared user agent caches; otherwise, false.</returns>
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00006CA1 File Offset: 0x00004EA1
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00006CA9 File Offset: 0x00004EA9
		public bool ProxyRevalidate { get; set; }

		/// <summary>Whether an HTTP response may be cached by any cache, even if it would normally be non-cacheable or cacheable only within a non- shared cache.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the HTTP response may be cached by any cache, even if it would normally be non-cacheable or cacheable only within a non- shared cache; otherwise, false.</returns>
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00006CB2 File Offset: 0x00004EB2
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00006CBA File Offset: 0x00004EBA
		public bool Public { get; set; }

		/// <summary>The shared maximum age, specified in seconds, in an HTTP response that overrides the "max-age" directive in a cache-control header or an Expires header for a shared cache.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The time in seconds.</returns>
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00006CC3 File Offset: 0x00004EC3
		// (set) Token: 0x060001BB RID: 443 RVA: 0x00006CCB File Offset: 0x00004ECB
		public TimeSpan? SharedMaxAge { get; set; }

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.CacheControlHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Object" />.A copy of the current instance.</returns>
		// Token: 0x060001BC RID: 444 RVA: 0x00006CD4 File Offset: 0x00004ED4
		object ICloneable.Clone()
		{
			CacheControlHeaderValue cacheControlHeaderValue = (CacheControlHeaderValue)base.MemberwiseClone();
			if (this.extensions != null)
			{
				cacheControlHeaderValue.extensions = new List<NameValueHeaderValue>();
				foreach (NameValueHeaderValue nameValueHeaderValue in this.extensions)
				{
					cacheControlHeaderValue.extensions.Add(nameValueHeaderValue);
				}
			}
			if (this.no_cache_headers != null)
			{
				cacheControlHeaderValue.no_cache_headers = new List<string>();
				foreach (string text in this.no_cache_headers)
				{
					cacheControlHeaderValue.no_cache_headers.Add(text);
				}
			}
			if (this.private_headers != null)
			{
				cacheControlHeaderValue.private_headers = new List<string>();
				foreach (string text2 in this.private_headers)
				{
					cacheControlHeaderValue.private_headers.Add(text2);
				}
			}
			return cacheControlHeaderValue;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.CacheControlHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current object.</param>
		// Token: 0x060001BD RID: 445 RVA: 0x00006E04 File Offset: 0x00005004
		public override bool Equals(object obj)
		{
			CacheControlHeaderValue cacheControlHeaderValue = obj as CacheControlHeaderValue;
			return cacheControlHeaderValue != null && (!(this.MaxAge != cacheControlHeaderValue.MaxAge) && this.MaxStale == cacheControlHeaderValue.MaxStale) && !(this.MaxStaleLimit != cacheControlHeaderValue.MaxStaleLimit) && (!(this.MinFresh != cacheControlHeaderValue.MinFresh) && this.MustRevalidate == cacheControlHeaderValue.MustRevalidate && this.NoCache == cacheControlHeaderValue.NoCache && this.NoStore == cacheControlHeaderValue.NoStore && this.NoTransform == cacheControlHeaderValue.NoTransform && this.OnlyIfCached == cacheControlHeaderValue.OnlyIfCached && this.Private == cacheControlHeaderValue.Private && this.ProxyRevalidate == cacheControlHeaderValue.ProxyRevalidate && this.Public == cacheControlHeaderValue.Public) && !(this.SharedMaxAge != cacheControlHeaderValue.SharedMaxAge) && (this.extensions.SequenceEqual(cacheControlHeaderValue.extensions) && this.no_cache_headers.SequenceEqual(cacheControlHeaderValue.no_cache_headers)) && this.private_headers.SequenceEqual(cacheControlHeaderValue.private_headers);
		}

		/// <summary>Serves as a hash function for a  <see cref="T:System.Net.Http.Headers.CacheControlHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.A hash code for the current object.</returns>
		// Token: 0x060001BE RID: 446 RVA: 0x00006FF4 File Offset: 0x000051F4
		public override int GetHashCode()
		{
			return (((((((((((((((29 * 29 + HashCodeCalculator.Calculate<NameValueHeaderValue>(this.extensions)) * 29 + this.MaxAge.GetHashCode()) * 29 + this.MaxStale.GetHashCode()) * 29 + this.MaxStaleLimit.GetHashCode()) * 29 + this.MinFresh.GetHashCode()) * 29 + this.MustRevalidate.GetHashCode()) * 29 + HashCodeCalculator.Calculate<string>(this.no_cache_headers)) * 29 + this.NoCache.GetHashCode()) * 29 + this.NoStore.GetHashCode()) * 29 + this.NoTransform.GetHashCode()) * 29 + this.OnlyIfCached.GetHashCode()) * 29 + this.Private.GetHashCode()) * 29 + HashCodeCalculator.Calculate<string>(this.private_headers)) * 29 + this.ProxyRevalidate.GetHashCode()) * 29 + this.Public.GetHashCode()) * 29 + this.SharedMaxAge.GetHashCode();
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.CacheControlHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.CacheControlHeaderValue" />.A <see cref="T:System.Net.Http.Headers.CacheControlHeaderValue" /> instance.</returns>
		/// <param name="input">A string that represents cache-control header value information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a null reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid cache-control header value information.</exception>
		// Token: 0x060001BF RID: 447 RVA: 0x00007134 File Offset: 0x00005334
		public static CacheControlHeaderValue Parse(string input)
		{
			CacheControlHeaderValue cacheControlHeaderValue;
			if (CacheControlHeaderValue.TryParse(input, out cacheControlHeaderValue))
			{
				return cacheControlHeaderValue;
			}
			throw new FormatException(input);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.CacheControlHeaderValue" /> information.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.CacheControlHeaderValue" /> information; otherwise, false.</returns>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.CacheControlHeaderValue" /> version of the string.</param>
		// Token: 0x060001C0 RID: 448 RVA: 0x00007154 File Offset: 0x00005354
		public static bool TryParse(string input, out CacheControlHeaderValue parsedValue)
		{
			parsedValue = null;
			if (input == null)
			{
				return true;
			}
			CacheControlHeaderValue cacheControlHeaderValue = new CacheControlHeaderValue();
			Lexer lexer = new Lexer(input);
			Token token;
			for (;;)
			{
				token = lexer.Scan(false);
				if (token != Token.Type.Token)
				{
					break;
				}
				string stringValue = lexer.GetStringValue(token);
				bool flag = false;
				uint num = global::<PrivateImplementationDetails>.ComputeStringHash(stringValue);
				if (num <= 1922561311U)
				{
					TimeSpan? timeSpan;
					if (num <= 719568158U)
					{
						if (num != 129047354U)
						{
							if (num != 412259456U)
							{
								if (num != 719568158U)
								{
									goto IL_03B1;
								}
								if (!(stringValue == "no-store"))
								{
									goto IL_03B1;
								}
								cacheControlHeaderValue.NoStore = true;
								goto IL_040A;
							}
							else if (!(stringValue == "s-maxage"))
							{
								goto IL_03B1;
							}
						}
						else if (!(stringValue == "min-fresh"))
						{
							goto IL_03B1;
						}
					}
					else if (num != 962188105U)
					{
						if (num != 1657474316U)
						{
							if (num != 1922561311U)
							{
								goto IL_03B1;
							}
							if (!(stringValue == "max-age"))
							{
								goto IL_03B1;
							}
						}
						else
						{
							if (!(stringValue == "private"))
							{
								goto IL_03B1;
							}
							goto IL_02FE;
						}
					}
					else
					{
						if (!(stringValue == "max-stale"))
						{
							goto IL_03B1;
						}
						cacheControlHeaderValue.MaxStale = true;
						token = lexer.Scan(false);
						if (token != Token.Type.SeparatorEqual)
						{
							flag = true;
							goto IL_040A;
						}
						token = lexer.Scan(false);
						if (token != Token.Type.Token)
						{
							return false;
						}
						timeSpan = lexer.TryGetTimeSpanValue(token);
						if (timeSpan == null)
						{
							return false;
						}
						cacheControlHeaderValue.MaxStaleLimit = timeSpan;
						goto IL_040A;
					}
					token = lexer.Scan(false);
					if (token != Token.Type.SeparatorEqual)
					{
						return false;
					}
					token = lexer.Scan(false);
					if (token != Token.Type.Token)
					{
						return false;
					}
					timeSpan = lexer.TryGetTimeSpanValue(token);
					if (timeSpan == null)
					{
						return false;
					}
					int i = stringValue.Length;
					if (i != 7)
					{
						if (i != 8)
						{
							cacheControlHeaderValue.MinFresh = timeSpan;
						}
						else
						{
							cacheControlHeaderValue.SharedMaxAge = timeSpan;
						}
					}
					else
					{
						cacheControlHeaderValue.MaxAge = timeSpan;
					}
				}
				else if (num <= 2802093227U)
				{
					if (num != 2033558065U)
					{
						if (num != 2154495528U)
						{
							if (num != 2802093227U)
							{
								goto IL_03B1;
							}
							if (!(stringValue == "no-transform"))
							{
								goto IL_03B1;
							}
							cacheControlHeaderValue.NoTransform = true;
						}
						else
						{
							if (!(stringValue == "must-revalidate"))
							{
								goto IL_03B1;
							}
							cacheControlHeaderValue.MustRevalidate = true;
						}
					}
					else
					{
						if (!(stringValue == "proxy-revalidate"))
						{
							goto IL_03B1;
						}
						cacheControlHeaderValue.ProxyRevalidate = true;
					}
				}
				else if (num != 2866772502U)
				{
					if (num != 3432027008U)
					{
						if (num != 3443516981U)
						{
							goto IL_03B1;
						}
						if (!(stringValue == "no-cache"))
						{
							goto IL_03B1;
						}
						goto IL_02FE;
					}
					else
					{
						if (!(stringValue == "public"))
						{
							goto IL_03B1;
						}
						cacheControlHeaderValue.Public = true;
					}
				}
				else
				{
					if (!(stringValue == "only-if-cached"))
					{
						goto IL_03B1;
					}
					cacheControlHeaderValue.OnlyIfCached = true;
				}
				IL_040A:
				if (!flag)
				{
					token = lexer.Scan(false);
				}
				if (token != Token.Type.SeparatorComma)
				{
					goto Block_46;
				}
				continue;
				IL_02FE:
				if (stringValue.Length == 7)
				{
					cacheControlHeaderValue.Private = true;
				}
				else
				{
					cacheControlHeaderValue.NoCache = true;
				}
				token = lexer.Scan(false);
				if (token != Token.Type.SeparatorEqual)
				{
					flag = true;
					goto IL_040A;
				}
				token = lexer.Scan(false);
				if (token != Token.Type.QuotedString)
				{
					return false;
				}
				string[] array = lexer.GetQuotedStringValue(token).Split(',', StringSplitOptions.None);
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i].Trim(new char[] { '\t', ' ' });
					if (stringValue.Length == 7)
					{
						cacheControlHeaderValue.PrivateHeaders.Add(text);
					}
					else
					{
						cacheControlHeaderValue.NoCache = true;
						cacheControlHeaderValue.NoCacheHeaders.Add(text);
					}
				}
				goto IL_040A;
				IL_03B1:
				string stringValue2 = lexer.GetStringValue(token);
				string text2 = null;
				token = lexer.Scan(false);
				if (token == Token.Type.SeparatorEqual)
				{
					token = lexer.Scan(false);
					Token.Type kind = token.Kind;
					if (kind - Token.Type.Token > 1)
					{
						return false;
					}
					text2 = lexer.GetStringValue(token);
				}
				else
				{
					flag = true;
				}
				cacheControlHeaderValue.Extensions.Add(NameValueHeaderValue.Create(stringValue2, text2));
				goto IL_040A;
			}
			return false;
			Block_46:
			if (token != Token.Type.End)
			{
				return false;
			}
			parsedValue = cacheControlHeaderValue;
			return true;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.CacheControlHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.String" />.A string that represents the current object.</returns>
		// Token: 0x060001C1 RID: 449 RVA: 0x00007594 File Offset: 0x00005794
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.NoStore)
			{
				stringBuilder.Append("no-store");
				stringBuilder.Append(", ");
			}
			if (this.NoTransform)
			{
				stringBuilder.Append("no-transform");
				stringBuilder.Append(", ");
			}
			if (this.OnlyIfCached)
			{
				stringBuilder.Append("only-if-cached");
				stringBuilder.Append(", ");
			}
			if (this.Public)
			{
				stringBuilder.Append("public");
				stringBuilder.Append(", ");
			}
			if (this.MustRevalidate)
			{
				stringBuilder.Append("must-revalidate");
				stringBuilder.Append(", ");
			}
			if (this.ProxyRevalidate)
			{
				stringBuilder.Append("proxy-revalidate");
				stringBuilder.Append(", ");
			}
			if (this.NoCache)
			{
				stringBuilder.Append("no-cache");
				if (this.no_cache_headers != null)
				{
					stringBuilder.Append("=\"");
					this.no_cache_headers.ToStringBuilder(stringBuilder);
					stringBuilder.Append("\"");
				}
				stringBuilder.Append(", ");
			}
			if (this.MaxAge != null)
			{
				stringBuilder.Append("max-age=");
				stringBuilder.Append(this.MaxAge.Value.TotalSeconds.ToString(CultureInfo.InvariantCulture));
				stringBuilder.Append(", ");
			}
			if (this.SharedMaxAge != null)
			{
				stringBuilder.Append("s-maxage=");
				stringBuilder.Append(this.SharedMaxAge.Value.TotalSeconds.ToString(CultureInfo.InvariantCulture));
				stringBuilder.Append(", ");
			}
			if (this.MaxStale)
			{
				stringBuilder.Append("max-stale");
				if (this.MaxStaleLimit != null)
				{
					stringBuilder.Append("=");
					stringBuilder.Append(this.MaxStaleLimit.Value.TotalSeconds.ToString(CultureInfo.InvariantCulture));
				}
				stringBuilder.Append(", ");
			}
			if (this.MinFresh != null)
			{
				stringBuilder.Append("min-fresh=");
				stringBuilder.Append(this.MinFresh.Value.TotalSeconds.ToString(CultureInfo.InvariantCulture));
				stringBuilder.Append(", ");
			}
			if (this.Private)
			{
				stringBuilder.Append("private");
				if (this.private_headers != null)
				{
					stringBuilder.Append("=\"");
					this.private_headers.ToStringBuilder(stringBuilder);
					stringBuilder.Append("\"");
				}
				stringBuilder.Append(", ");
			}
			this.extensions.ToStringBuilder(stringBuilder);
			if (stringBuilder.Length > 2 && stringBuilder[stringBuilder.Length - 2] == ',' && stringBuilder[stringBuilder.Length - 1] == ' ')
			{
				stringBuilder.Remove(stringBuilder.Length - 2, 2);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000DB RID: 219
		private List<NameValueHeaderValue> extensions;

		// Token: 0x040000DC RID: 220
		private List<string> no_cache_headers;

		// Token: 0x040000DD RID: 221
		private List<string> private_headers;
	}
}
