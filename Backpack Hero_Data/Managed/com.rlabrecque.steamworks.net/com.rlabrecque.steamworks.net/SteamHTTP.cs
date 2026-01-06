using System;

namespace Steamworks
{
	// Token: 0x02000011 RID: 17
	public static class SteamHTTP
	{
		// Token: 0x06000228 RID: 552 RVA: 0x00006C58 File Offset: 0x00004E58
		public static HTTPRequestHandle CreateHTTPRequest(EHTTPMethod eHTTPRequestMethod, string pchAbsoluteURL)
		{
			InteropHelp.TestIfAvailableClient();
			HTTPRequestHandle httprequestHandle;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchAbsoluteURL))
			{
				httprequestHandle = (HTTPRequestHandle)NativeMethods.ISteamHTTP_CreateHTTPRequest(CSteamAPIContext.GetSteamHTTP(), eHTTPRequestMethod, utf8StringHandle);
			}
			return httprequestHandle;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00006CA0 File Offset: 0x00004EA0
		public static bool SetHTTPRequestContextValue(HTTPRequestHandle hRequest, ulong ulContextValue)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_SetHTTPRequestContextValue(CSteamAPIContext.GetSteamHTTP(), hRequest, ulContextValue);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00006CB3 File Offset: 0x00004EB3
		public static bool SetHTTPRequestNetworkActivityTimeout(HTTPRequestHandle hRequest, uint unTimeoutSeconds)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_SetHTTPRequestNetworkActivityTimeout(CSteamAPIContext.GetSteamHTTP(), hRequest, unTimeoutSeconds);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00006CC8 File Offset: 0x00004EC8
		public static bool SetHTTPRequestHeaderValue(HTTPRequestHandle hRequest, string pchHeaderName, string pchHeaderValue)
		{
			InteropHelp.TestIfAvailableClient();
			bool flag;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchHeaderName))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchHeaderValue))
				{
					flag = NativeMethods.ISteamHTTP_SetHTTPRequestHeaderValue(CSteamAPIContext.GetSteamHTTP(), hRequest, utf8StringHandle, utf8StringHandle2);
				}
			}
			return flag;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00006D2C File Offset: 0x00004F2C
		public static bool SetHTTPRequestGetOrPostParameter(HTTPRequestHandle hRequest, string pchParamName, string pchParamValue)
		{
			InteropHelp.TestIfAvailableClient();
			bool flag;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchParamName))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchParamValue))
				{
					flag = NativeMethods.ISteamHTTP_SetHTTPRequestGetOrPostParameter(CSteamAPIContext.GetSteamHTTP(), hRequest, utf8StringHandle, utf8StringHandle2);
				}
			}
			return flag;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00006D90 File Offset: 0x00004F90
		public static bool SendHTTPRequest(HTTPRequestHandle hRequest, out SteamAPICall_t pCallHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_SendHTTPRequest(CSteamAPIContext.GetSteamHTTP(), hRequest, out pCallHandle);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00006DA3 File Offset: 0x00004FA3
		public static bool SendHTTPRequestAndStreamResponse(HTTPRequestHandle hRequest, out SteamAPICall_t pCallHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_SendHTTPRequestAndStreamResponse(CSteamAPIContext.GetSteamHTTP(), hRequest, out pCallHandle);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00006DB6 File Offset: 0x00004FB6
		public static bool DeferHTTPRequest(HTTPRequestHandle hRequest)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_DeferHTTPRequest(CSteamAPIContext.GetSteamHTTP(), hRequest);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00006DC8 File Offset: 0x00004FC8
		public static bool PrioritizeHTTPRequest(HTTPRequestHandle hRequest)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_PrioritizeHTTPRequest(CSteamAPIContext.GetSteamHTTP(), hRequest);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00006DDC File Offset: 0x00004FDC
		public static bool GetHTTPResponseHeaderSize(HTTPRequestHandle hRequest, string pchHeaderName, out uint unResponseHeaderSize)
		{
			InteropHelp.TestIfAvailableClient();
			bool flag;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchHeaderName))
			{
				flag = NativeMethods.ISteamHTTP_GetHTTPResponseHeaderSize(CSteamAPIContext.GetSteamHTTP(), hRequest, utf8StringHandle, out unResponseHeaderSize);
			}
			return flag;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00006E20 File Offset: 0x00005020
		public static bool GetHTTPResponseHeaderValue(HTTPRequestHandle hRequest, string pchHeaderName, byte[] pHeaderValueBuffer, uint unBufferSize)
		{
			InteropHelp.TestIfAvailableClient();
			bool flag;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchHeaderName))
			{
				flag = NativeMethods.ISteamHTTP_GetHTTPResponseHeaderValue(CSteamAPIContext.GetSteamHTTP(), hRequest, utf8StringHandle, pHeaderValueBuffer, unBufferSize);
			}
			return flag;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00006E68 File Offset: 0x00005068
		public static bool GetHTTPResponseBodySize(HTTPRequestHandle hRequest, out uint unBodySize)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_GetHTTPResponseBodySize(CSteamAPIContext.GetSteamHTTP(), hRequest, out unBodySize);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00006E7B File Offset: 0x0000507B
		public static bool GetHTTPResponseBodyData(HTTPRequestHandle hRequest, byte[] pBodyDataBuffer, uint unBufferSize)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_GetHTTPResponseBodyData(CSteamAPIContext.GetSteamHTTP(), hRequest, pBodyDataBuffer, unBufferSize);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00006E8F File Offset: 0x0000508F
		public static bool GetHTTPStreamingResponseBodyData(HTTPRequestHandle hRequest, uint cOffset, byte[] pBodyDataBuffer, uint unBufferSize)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_GetHTTPStreamingResponseBodyData(CSteamAPIContext.GetSteamHTTP(), hRequest, cOffset, pBodyDataBuffer, unBufferSize);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00006EA4 File Offset: 0x000050A4
		public static bool ReleaseHTTPRequest(HTTPRequestHandle hRequest)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_ReleaseHTTPRequest(CSteamAPIContext.GetSteamHTTP(), hRequest);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00006EB6 File Offset: 0x000050B6
		public static bool GetHTTPDownloadProgressPct(HTTPRequestHandle hRequest, out float pflPercentOut)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_GetHTTPDownloadProgressPct(CSteamAPIContext.GetSteamHTTP(), hRequest, out pflPercentOut);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00006ECC File Offset: 0x000050CC
		public static bool SetHTTPRequestRawPostBody(HTTPRequestHandle hRequest, string pchContentType, byte[] pubBody, uint unBodyLen)
		{
			InteropHelp.TestIfAvailableClient();
			bool flag;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchContentType))
			{
				flag = NativeMethods.ISteamHTTP_SetHTTPRequestRawPostBody(CSteamAPIContext.GetSteamHTTP(), hRequest, utf8StringHandle, pubBody, unBodyLen);
			}
			return flag;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00006F14 File Offset: 0x00005114
		public static HTTPCookieContainerHandle CreateCookieContainer(bool bAllowResponsesToModify)
		{
			InteropHelp.TestIfAvailableClient();
			return (HTTPCookieContainerHandle)NativeMethods.ISteamHTTP_CreateCookieContainer(CSteamAPIContext.GetSteamHTTP(), bAllowResponsesToModify);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00006F2B File Offset: 0x0000512B
		public static bool ReleaseCookieContainer(HTTPCookieContainerHandle hCookieContainer)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_ReleaseCookieContainer(CSteamAPIContext.GetSteamHTTP(), hCookieContainer);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00006F40 File Offset: 0x00005140
		public static bool SetCookie(HTTPCookieContainerHandle hCookieContainer, string pchHost, string pchUrl, string pchCookie)
		{
			InteropHelp.TestIfAvailableClient();
			bool flag;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchHost))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchUrl))
				{
					using (InteropHelp.UTF8StringHandle utf8StringHandle3 = new InteropHelp.UTF8StringHandle(pchCookie))
					{
						flag = NativeMethods.ISteamHTTP_SetCookie(CSteamAPIContext.GetSteamHTTP(), hCookieContainer, utf8StringHandle, utf8StringHandle2, utf8StringHandle3);
					}
				}
			}
			return flag;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00006FC0 File Offset: 0x000051C0
		public static bool SetHTTPRequestCookieContainer(HTTPRequestHandle hRequest, HTTPCookieContainerHandle hCookieContainer)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_SetHTTPRequestCookieContainer(CSteamAPIContext.GetSteamHTTP(), hRequest, hCookieContainer);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00006FD4 File Offset: 0x000051D4
		public static bool SetHTTPRequestUserAgentInfo(HTTPRequestHandle hRequest, string pchUserAgentInfo)
		{
			InteropHelp.TestIfAvailableClient();
			bool flag;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchUserAgentInfo))
			{
				flag = NativeMethods.ISteamHTTP_SetHTTPRequestUserAgentInfo(CSteamAPIContext.GetSteamHTTP(), hRequest, utf8StringHandle);
			}
			return flag;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00007018 File Offset: 0x00005218
		public static bool SetHTTPRequestRequiresVerifiedCertificate(HTTPRequestHandle hRequest, bool bRequireVerifiedCertificate)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_SetHTTPRequestRequiresVerifiedCertificate(CSteamAPIContext.GetSteamHTTP(), hRequest, bRequireVerifiedCertificate);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000702B File Offset: 0x0000522B
		public static bool SetHTTPRequestAbsoluteTimeoutMS(HTTPRequestHandle hRequest, uint unMilliseconds)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_SetHTTPRequestAbsoluteTimeoutMS(CSteamAPIContext.GetSteamHTTP(), hRequest, unMilliseconds);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000703E File Offset: 0x0000523E
		public static bool GetHTTPRequestWasTimedOut(HTTPRequestHandle hRequest, out bool pbWasTimedOut)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamHTTP_GetHTTPRequestWasTimedOut(CSteamAPIContext.GetSteamHTTP(), hRequest, out pbWasTimedOut);
		}
	}
}
