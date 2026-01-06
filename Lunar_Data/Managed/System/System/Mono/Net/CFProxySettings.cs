using System;

namespace Mono.Net
{
	// Token: 0x02000081 RID: 129
	internal class CFProxySettings
	{
		// Token: 0x060001EC RID: 492 RVA: 0x00005900 File Offset: 0x00003B00
		static CFProxySettings()
		{
			IntPtr intPtr = CFObject.dlopen("/System/Library/Frameworks/CoreServices.framework/Frameworks/CFNetwork.framework/CFNetwork", 0);
			CFProxySettings.kCFNetworkProxiesHTTPEnable = CFObject.GetCFObjectHandle(intPtr, "kCFNetworkProxiesHTTPEnable");
			CFProxySettings.kCFNetworkProxiesHTTPPort = CFObject.GetCFObjectHandle(intPtr, "kCFNetworkProxiesHTTPPort");
			CFProxySettings.kCFNetworkProxiesHTTPProxy = CFObject.GetCFObjectHandle(intPtr, "kCFNetworkProxiesHTTPProxy");
			CFProxySettings.kCFNetworkProxiesProxyAutoConfigEnable = CFObject.GetCFObjectHandle(intPtr, "kCFNetworkProxiesProxyAutoConfigEnable");
			CFProxySettings.kCFNetworkProxiesProxyAutoConfigJavaScript = CFObject.GetCFObjectHandle(intPtr, "kCFNetworkProxiesProxyAutoConfigJavaScript");
			CFProxySettings.kCFNetworkProxiesProxyAutoConfigURLString = CFObject.GetCFObjectHandle(intPtr, "kCFNetworkProxiesProxyAutoConfigURLString");
			CFObject.dlclose(intPtr);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000597D File Offset: 0x00003B7D
		public CFProxySettings(CFDictionary settings)
		{
			this.settings = settings;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000598C File Offset: 0x00003B8C
		public CFDictionary Dictionary
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00005994 File Offset: 0x00003B94
		public bool HTTPEnable
		{
			get
			{
				return !(CFProxySettings.kCFNetworkProxiesHTTPEnable == IntPtr.Zero) && CFNumber.AsBool(this.settings[CFProxySettings.kCFNetworkProxiesHTTPEnable]);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x000059BE File Offset: 0x00003BBE
		public int HTTPPort
		{
			get
			{
				if (CFProxySettings.kCFNetworkProxiesHTTPPort == IntPtr.Zero)
				{
					return 0;
				}
				return CFNumber.AsInt32(this.settings[CFProxySettings.kCFNetworkProxiesHTTPPort]);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x000059E8 File Offset: 0x00003BE8
		public string HTTPProxy
		{
			get
			{
				if (CFProxySettings.kCFNetworkProxiesHTTPProxy == IntPtr.Zero)
				{
					return null;
				}
				return CFString.AsString(this.settings[CFProxySettings.kCFNetworkProxiesHTTPProxy]);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00005A12 File Offset: 0x00003C12
		public bool ProxyAutoConfigEnable
		{
			get
			{
				return !(CFProxySettings.kCFNetworkProxiesProxyAutoConfigEnable == IntPtr.Zero) && CFNumber.AsBool(this.settings[CFProxySettings.kCFNetworkProxiesProxyAutoConfigEnable]);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00005A3C File Offset: 0x00003C3C
		public string ProxyAutoConfigJavaScript
		{
			get
			{
				if (CFProxySettings.kCFNetworkProxiesProxyAutoConfigJavaScript == IntPtr.Zero)
				{
					return null;
				}
				return CFString.AsString(this.settings[CFProxySettings.kCFNetworkProxiesProxyAutoConfigJavaScript]);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00005A66 File Offset: 0x00003C66
		public string ProxyAutoConfigURLString
		{
			get
			{
				if (CFProxySettings.kCFNetworkProxiesProxyAutoConfigURLString == IntPtr.Zero)
				{
					return null;
				}
				return CFString.AsString(this.settings[CFProxySettings.kCFNetworkProxiesProxyAutoConfigURLString]);
			}
		}

		// Token: 0x040001E6 RID: 486
		private static IntPtr kCFNetworkProxiesHTTPEnable;

		// Token: 0x040001E7 RID: 487
		private static IntPtr kCFNetworkProxiesHTTPPort;

		// Token: 0x040001E8 RID: 488
		private static IntPtr kCFNetworkProxiesHTTPProxy;

		// Token: 0x040001E9 RID: 489
		private static IntPtr kCFNetworkProxiesProxyAutoConfigEnable;

		// Token: 0x040001EA RID: 490
		private static IntPtr kCFNetworkProxiesProxyAutoConfigJavaScript;

		// Token: 0x040001EB RID: 491
		private static IntPtr kCFNetworkProxiesProxyAutoConfigURLString;

		// Token: 0x040001EC RID: 492
		private CFDictionary settings;
	}
}
