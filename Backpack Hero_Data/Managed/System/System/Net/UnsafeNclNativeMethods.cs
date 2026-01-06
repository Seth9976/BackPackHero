using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Net
{
	// Token: 0x02000470 RID: 1136
	internal static class UnsafeNclNativeMethods
	{
		// Token: 0x02000471 RID: 1137
		internal static class HttpApi
		{
			// Token: 0x040014FA RID: 5370
			private const int HttpHeaderRequestMaximum = 41;

			// Token: 0x040014FB RID: 5371
			private const int HttpHeaderResponseMaximum = 30;

			// Token: 0x040014FC RID: 5372
			private static string[] m_Strings = new string[]
			{
				"Cache-Control", "Connection", "Date", "Keep-Alive", "Pragma", "Trailer", "Transfer-Encoding", "Upgrade", "Via", "Warning",
				"Allow", "Content-Length", "Content-Type", "Content-Encoding", "Content-Language", "Content-Location", "Content-MD5", "Content-Range", "Expires", "Last-Modified",
				"Accept-Ranges", "Age", "ETag", "Location", "Proxy-Authenticate", "Retry-After", "Server", "Set-Cookie", "Vary", "WWW-Authenticate"
			};

			// Token: 0x02000472 RID: 1138
			internal static class HTTP_REQUEST_HEADER_ID
			{
				// Token: 0x06002419 RID: 9241 RVA: 0x000855A6 File Offset: 0x000837A6
				internal static string ToString(int position)
				{
					return UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.m_Strings[position];
				}

				// Token: 0x040014FD RID: 5373
				private static string[] m_Strings = new string[]
				{
					"Cache-Control", "Connection", "Date", "Keep-Alive", "Pragma", "Trailer", "Transfer-Encoding", "Upgrade", "Via", "Warning",
					"Allow", "Content-Length", "Content-Type", "Content-Encoding", "Content-Language", "Content-Location", "Content-MD5", "Content-Range", "Expires", "Last-Modified",
					"Accept", "Accept-Charset", "Accept-Encoding", "Accept-Language", "Authorization", "Cookie", "Expect", "From", "Host", "If-Match",
					"If-Modified-Since", "If-None-Match", "If-Range", "If-Unmodified-Since", "Max-Forwards", "Proxy-Authorization", "Referer", "Range", "Te", "Translate",
					"User-Agent"
				};
			}

			// Token: 0x02000473 RID: 1139
			internal static class HTTP_RESPONSE_HEADER_ID
			{
				// Token: 0x0600241B RID: 9243 RVA: 0x00085734 File Offset: 0x00083934
				static HTTP_RESPONSE_HEADER_ID()
				{
					for (int i = 0; i < 30; i++)
					{
						UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.m_Hashtable.Add(UnsafeNclNativeMethods.HttpApi.m_Strings[i], i);
					}
				}

				// Token: 0x0600241C RID: 9244 RVA: 0x00085774 File Offset: 0x00083974
				internal static int IndexOfKnownHeader(string HeaderName)
				{
					object obj = UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.m_Hashtable[HeaderName];
					if (obj != null)
					{
						return (int)obj;
					}
					return -1;
				}

				// Token: 0x0600241D RID: 9245 RVA: 0x00085798 File Offset: 0x00083998
				internal static string ToString(int position)
				{
					return UnsafeNclNativeMethods.HttpApi.m_Strings[position];
				}

				// Token: 0x040014FE RID: 5374
				private static Hashtable m_Hashtable = new Hashtable(30);
			}

			// Token: 0x02000474 RID: 1140
			internal enum Enum
			{
				// Token: 0x04001500 RID: 5376
				HttpHeaderCacheControl,
				// Token: 0x04001501 RID: 5377
				HttpHeaderConnection,
				// Token: 0x04001502 RID: 5378
				HttpHeaderDate,
				// Token: 0x04001503 RID: 5379
				HttpHeaderKeepAlive,
				// Token: 0x04001504 RID: 5380
				HttpHeaderPragma,
				// Token: 0x04001505 RID: 5381
				HttpHeaderTrailer,
				// Token: 0x04001506 RID: 5382
				HttpHeaderTransferEncoding,
				// Token: 0x04001507 RID: 5383
				HttpHeaderUpgrade,
				// Token: 0x04001508 RID: 5384
				HttpHeaderVia,
				// Token: 0x04001509 RID: 5385
				HttpHeaderWarning,
				// Token: 0x0400150A RID: 5386
				HttpHeaderAllow,
				// Token: 0x0400150B RID: 5387
				HttpHeaderContentLength,
				// Token: 0x0400150C RID: 5388
				HttpHeaderContentType,
				// Token: 0x0400150D RID: 5389
				HttpHeaderContentEncoding,
				// Token: 0x0400150E RID: 5390
				HttpHeaderContentLanguage,
				// Token: 0x0400150F RID: 5391
				HttpHeaderContentLocation,
				// Token: 0x04001510 RID: 5392
				HttpHeaderContentMd5,
				// Token: 0x04001511 RID: 5393
				HttpHeaderContentRange,
				// Token: 0x04001512 RID: 5394
				HttpHeaderExpires,
				// Token: 0x04001513 RID: 5395
				HttpHeaderLastModified,
				// Token: 0x04001514 RID: 5396
				HttpHeaderAcceptRanges,
				// Token: 0x04001515 RID: 5397
				HttpHeaderAge,
				// Token: 0x04001516 RID: 5398
				HttpHeaderEtag,
				// Token: 0x04001517 RID: 5399
				HttpHeaderLocation,
				// Token: 0x04001518 RID: 5400
				HttpHeaderProxyAuthenticate,
				// Token: 0x04001519 RID: 5401
				HttpHeaderRetryAfter,
				// Token: 0x0400151A RID: 5402
				HttpHeaderServer,
				// Token: 0x0400151B RID: 5403
				HttpHeaderSetCookie,
				// Token: 0x0400151C RID: 5404
				HttpHeaderVary,
				// Token: 0x0400151D RID: 5405
				HttpHeaderWwwAuthenticate,
				// Token: 0x0400151E RID: 5406
				HttpHeaderResponseMaximum,
				// Token: 0x0400151F RID: 5407
				HttpHeaderMaximum = 41
			}
		}

		// Token: 0x02000475 RID: 1141
		internal static class SecureStringHelper
		{
			// Token: 0x0600241E RID: 9246 RVA: 0x000857A4 File Offset: 0x000839A4
			internal static string CreateString(SecureString secureString)
			{
				IntPtr intPtr = IntPtr.Zero;
				if (secureString == null || secureString.Length == 0)
				{
					return string.Empty;
				}
				string text;
				try
				{
					intPtr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
					text = Marshal.PtrToStringUni(intPtr);
				}
				finally
				{
					if (intPtr != IntPtr.Zero)
					{
						Marshal.ZeroFreeGlobalAllocUnicode(intPtr);
					}
				}
				return text;
			}

			// Token: 0x0600241F RID: 9247 RVA: 0x00085800 File Offset: 0x00083A00
			internal unsafe static SecureString CreateSecureString(string plainString)
			{
				if (plainString == null || plainString.Length == 0)
				{
					return new SecureString();
				}
				SecureString secureString;
				fixed (string text = plainString)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					secureString = new SecureString(ptr, plainString.Length);
				}
				return secureString;
			}
		}
	}
}
