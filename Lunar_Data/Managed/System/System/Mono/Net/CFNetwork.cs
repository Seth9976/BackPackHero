using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;

namespace Mono.Net
{
	// Token: 0x02000082 RID: 130
	internal static class CFNetwork
	{
		// Token: 0x060001F5 RID: 501
		[DllImport("/System/Library/Frameworks/CoreServices.framework/Frameworks/CFNetwork.framework/CFNetwork", EntryPoint = "CFNetworkCopyProxiesForAutoConfigurationScript")]
		private static extern IntPtr CFNetworkCopyProxiesForAutoConfigurationScriptSequential(IntPtr proxyAutoConfigurationScript, IntPtr targetURL, out IntPtr error);

		// Token: 0x060001F6 RID: 502
		[DllImport("/System/Library/Frameworks/CoreServices.framework/Frameworks/CFNetwork.framework/CFNetwork")]
		private static extern IntPtr CFNetworkExecuteProxyAutoConfigurationURL(IntPtr proxyAutoConfigURL, IntPtr targetURL, CFNetwork.CFProxyAutoConfigurationResultCallback cb, ref CFStreamClientContext clientContext);

		// Token: 0x060001F7 RID: 503 RVA: 0x00005A90 File Offset: 0x00003C90
		private static void CFNetworkCopyProxiesForAutoConfigurationScriptThread()
		{
			bool flag = true;
			for (;;)
			{
				CFNetwork.proxy_event.WaitOne();
				do
				{
					object obj = CFNetwork.lock_obj;
					CFNetwork.GetProxyData getProxyData;
					lock (obj)
					{
						if (CFNetwork.get_proxy_queue.Count == 0)
						{
							break;
						}
						getProxyData = CFNetwork.get_proxy_queue.Dequeue();
						flag = CFNetwork.get_proxy_queue.Count > 0;
					}
					getProxyData.result = CFNetwork.CFNetworkCopyProxiesForAutoConfigurationScriptSequential(getProxyData.script, getProxyData.targetUri, out getProxyData.error);
					getProxyData.evt.Set();
				}
				while (flag);
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00005B2C File Offset: 0x00003D2C
		private static IntPtr CFNetworkCopyProxiesForAutoConfigurationScript(IntPtr proxyAutoConfigurationScript, IntPtr targetURL, out IntPtr error)
		{
			IntPtr result;
			using (CFNetwork.GetProxyData getProxyData = new CFNetwork.GetProxyData())
			{
				getProxyData.script = proxyAutoConfigurationScript;
				getProxyData.targetUri = targetURL;
				object obj = CFNetwork.lock_obj;
				lock (obj)
				{
					if (CFNetwork.get_proxy_queue == null)
					{
						CFNetwork.get_proxy_queue = new Queue<CFNetwork.GetProxyData>();
						CFNetwork.proxy_event = new AutoResetEvent(false);
						new Thread(new ThreadStart(CFNetwork.CFNetworkCopyProxiesForAutoConfigurationScriptThread))
						{
							IsBackground = true
						}.Start();
					}
					CFNetwork.get_proxy_queue.Enqueue(getProxyData);
					CFNetwork.proxy_event.Set();
				}
				getProxyData.evt.WaitOne();
				error = getProxyData.error;
				result = getProxyData.result;
			}
			return result;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00005BFC File Offset: 0x00003DFC
		private static CFArray CopyProxiesForAutoConfigurationScript(IntPtr proxyAutoConfigurationScript, CFUrl targetURL)
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr intPtr = CFNetwork.CFNetworkCopyProxiesForAutoConfigurationScript(proxyAutoConfigurationScript, targetURL.Handle, out zero);
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new CFArray(intPtr, true);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00005C34 File Offset: 0x00003E34
		public static CFProxy[] GetProxiesForAutoConfigurationScript(IntPtr proxyAutoConfigurationScript, CFUrl targetURL)
		{
			if (proxyAutoConfigurationScript == IntPtr.Zero)
			{
				throw new ArgumentNullException("proxyAutoConfigurationScript");
			}
			if (targetURL == null)
			{
				throw new ArgumentNullException("targetURL");
			}
			CFArray cfarray = CFNetwork.CopyProxiesForAutoConfigurationScript(proxyAutoConfigurationScript, targetURL);
			if (cfarray == null)
			{
				return null;
			}
			CFProxy[] array = new CFProxy[cfarray.Count];
			for (int i = 0; i < array.Length; i++)
			{
				CFDictionary cfdictionary = new CFDictionary(cfarray[i], false);
				array[i] = new CFProxy(cfdictionary);
			}
			cfarray.Dispose();
			return array;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00005CAC File Offset: 0x00003EAC
		public static CFProxy[] GetProxiesForAutoConfigurationScript(IntPtr proxyAutoConfigurationScript, Uri targetUri)
		{
			if (proxyAutoConfigurationScript == IntPtr.Zero)
			{
				throw new ArgumentNullException("proxyAutoConfigurationScript");
			}
			if (targetUri == null)
			{
				throw new ArgumentNullException("targetUri");
			}
			CFUrl cfurl = CFUrl.Create(targetUri.AbsoluteUri);
			CFProxy[] proxiesForAutoConfigurationScript = CFNetwork.GetProxiesForAutoConfigurationScript(proxyAutoConfigurationScript, cfurl);
			cfurl.Dispose();
			return proxiesForAutoConfigurationScript;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00005D00 File Offset: 0x00003F00
		public static CFProxy[] ExecuteProxyAutoConfigurationURL(IntPtr proxyAutoConfigURL, Uri targetURL)
		{
			CFUrl cfurl = CFUrl.Create(targetURL.AbsoluteUri);
			if (cfurl == null)
			{
				return null;
			}
			CFProxy[] proxies = null;
			CFRunLoop runLoop = CFRunLoop.CurrentRunLoop;
			CFNetwork.CFProxyAutoConfigurationResultCallback cfproxyAutoConfigurationResultCallback = delegate(IntPtr client, IntPtr proxyList, IntPtr error)
			{
				if (proxyList != IntPtr.Zero)
				{
					CFArray cfarray = new CFArray(proxyList, false);
					proxies = new CFProxy[cfarray.Count];
					for (int i = 0; i < proxies.Length; i++)
					{
						CFDictionary cfdictionary = new CFDictionary(cfarray[i], false);
						proxies[i] = new CFProxy(cfdictionary);
					}
					cfarray.Dispose();
				}
				runLoop.Stop();
			};
			CFStreamClientContext cfstreamClientContext = default(CFStreamClientContext);
			IntPtr intPtr = CFNetwork.CFNetworkExecuteProxyAutoConfigurationURL(proxyAutoConfigURL, cfurl.Handle, cfproxyAutoConfigurationResultCallback, ref cfstreamClientContext);
			CFString cfstring = CFString.Create("Mono.MacProxy");
			runLoop.AddSource(intPtr, cfstring);
			runLoop.RunInMode(cfstring, double.MaxValue, false);
			runLoop.RemoveSource(intPtr, cfstring);
			return proxies;
		}

		// Token: 0x060001FD RID: 509
		[DllImport("/System/Library/Frameworks/CoreServices.framework/Frameworks/CFNetwork.framework/CFNetwork")]
		private static extern IntPtr CFNetworkCopyProxiesForURL(IntPtr url, IntPtr proxySettings);

		// Token: 0x060001FE RID: 510 RVA: 0x00005DA4 File Offset: 0x00003FA4
		private static CFArray CopyProxiesForURL(CFUrl url, CFDictionary proxySettings)
		{
			IntPtr intPtr = CFNetwork.CFNetworkCopyProxiesForURL(url.Handle, (proxySettings != null) ? proxySettings.Handle : IntPtr.Zero);
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new CFArray(intPtr, true);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00005DE4 File Offset: 0x00003FE4
		public static CFProxy[] GetProxiesForURL(CFUrl url, CFProxySettings proxySettings)
		{
			if (url == null || url.Handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("url");
			}
			if (proxySettings == null)
			{
				proxySettings = CFNetwork.GetSystemProxySettings();
			}
			CFArray cfarray = CFNetwork.CopyProxiesForURL(url, proxySettings.Dictionary);
			if (cfarray == null)
			{
				return null;
			}
			CFProxy[] array = new CFProxy[cfarray.Count];
			for (int i = 0; i < array.Length; i++)
			{
				CFDictionary cfdictionary = new CFDictionary(cfarray[i], false);
				array[i] = new CFProxy(cfdictionary);
			}
			cfarray.Dispose();
			return array;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00005E68 File Offset: 0x00004068
		public static CFProxy[] GetProxiesForUri(Uri uri, CFProxySettings proxySettings)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			CFUrl cfurl = CFUrl.Create(uri.AbsoluteUri);
			if (cfurl == null)
			{
				return null;
			}
			CFProxy[] proxiesForURL = CFNetwork.GetProxiesForURL(cfurl, proxySettings);
			cfurl.Dispose();
			return proxiesForURL;
		}

		// Token: 0x06000201 RID: 513
		[DllImport("/System/Library/Frameworks/CoreServices.framework/Frameworks/CFNetwork.framework/CFNetwork")]
		private static extern IntPtr CFNetworkCopySystemProxySettings();

		// Token: 0x06000202 RID: 514 RVA: 0x00005EAC File Offset: 0x000040AC
		public static CFProxySettings GetSystemProxySettings()
		{
			IntPtr intPtr = CFNetwork.CFNetworkCopySystemProxySettings();
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new CFProxySettings(new CFDictionary(intPtr, true));
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00005EDA File Offset: 0x000040DA
		public static IWebProxy GetDefaultProxy()
		{
			return new CFNetwork.CFWebProxy();
		}

		// Token: 0x040001ED RID: 493
		public const string CFNetworkLibrary = "/System/Library/Frameworks/CoreServices.framework/Frameworks/CFNetwork.framework/CFNetwork";

		// Token: 0x040001EE RID: 494
		private static object lock_obj = new object();

		// Token: 0x040001EF RID: 495
		private static Queue<CFNetwork.GetProxyData> get_proxy_queue;

		// Token: 0x040001F0 RID: 496
		private static AutoResetEvent proxy_event;

		// Token: 0x02000083 RID: 131
		private class GetProxyData : IDisposable
		{
			// Token: 0x06000205 RID: 517 RVA: 0x00005EED File Offset: 0x000040ED
			public void Dispose()
			{
				this.evt.Close();
			}

			// Token: 0x040001F1 RID: 497
			public IntPtr script;

			// Token: 0x040001F2 RID: 498
			public IntPtr targetUri;

			// Token: 0x040001F3 RID: 499
			public IntPtr error;

			// Token: 0x040001F4 RID: 500
			public IntPtr result;

			// Token: 0x040001F5 RID: 501
			public ManualResetEvent evt = new ManualResetEvent(false);
		}

		// Token: 0x02000084 RID: 132
		// (Invoke) Token: 0x06000208 RID: 520
		private delegate void CFProxyAutoConfigurationResultCallback(IntPtr client, IntPtr proxyList, IntPtr error);

		// Token: 0x02000085 RID: 133
		private class CFWebProxy : IWebProxy
		{
			// Token: 0x1700004A RID: 74
			// (get) Token: 0x0600020C RID: 524 RVA: 0x00005F0E File Offset: 0x0000410E
			// (set) Token: 0x0600020D RID: 525 RVA: 0x00005F16 File Offset: 0x00004116
			public ICredentials Credentials
			{
				get
				{
					return this.credentials;
				}
				set
				{
					this.userSpecified = true;
					this.credentials = value;
				}
			}

			// Token: 0x0600020E RID: 526 RVA: 0x00005F28 File Offset: 0x00004128
			private static Uri GetProxyUri(CFProxy proxy, out NetworkCredential credentials)
			{
				CFProxyType proxyType = proxy.ProxyType;
				string text;
				if (proxyType != CFProxyType.FTP)
				{
					if (proxyType - CFProxyType.HTTP > 1)
					{
						credentials = null;
						return null;
					}
					text = "http://";
				}
				else
				{
					text = "ftp://";
				}
				string username = proxy.Username;
				string password = proxy.Password;
				string hostName = proxy.HostName;
				int port = proxy.Port;
				if (username != null)
				{
					credentials = new NetworkCredential(username, password);
				}
				else
				{
					credentials = null;
				}
				return new Uri(text + hostName + ((port != 0) ? (":" + port.ToString()) : string.Empty), UriKind.Absolute);
			}

			// Token: 0x0600020F RID: 527 RVA: 0x00005FB7 File Offset: 0x000041B7
			private static Uri GetProxyUriFromScript(IntPtr script, Uri targetUri, out NetworkCredential credentials)
			{
				return CFNetwork.CFWebProxy.SelectProxy(CFNetwork.GetProxiesForAutoConfigurationScript(script, targetUri), targetUri, out credentials);
			}

			// Token: 0x06000210 RID: 528 RVA: 0x00005FC7 File Offset: 0x000041C7
			private static Uri ExecuteProxyAutoConfigurationURL(IntPtr proxyAutoConfigURL, Uri targetUri, out NetworkCredential credentials)
			{
				return CFNetwork.CFWebProxy.SelectProxy(CFNetwork.ExecuteProxyAutoConfigurationURL(proxyAutoConfigURL, targetUri), targetUri, out credentials);
			}

			// Token: 0x06000211 RID: 529 RVA: 0x00005FD8 File Offset: 0x000041D8
			private static Uri SelectProxy(CFProxy[] proxies, Uri targetUri, out NetworkCredential credentials)
			{
				if (proxies == null)
				{
					credentials = null;
					return targetUri;
				}
				for (int i = 0; i < proxies.Length; i++)
				{
					switch (proxies[i].ProxyType)
					{
					case CFProxyType.None:
						credentials = null;
						return targetUri;
					case CFProxyType.FTP:
					case CFProxyType.HTTP:
					case CFProxyType.HTTPS:
						return CFNetwork.CFWebProxy.GetProxyUri(proxies[i], out credentials);
					}
				}
				credentials = null;
				return null;
			}

			// Token: 0x06000212 RID: 530 RVA: 0x0000603C File Offset: 0x0000423C
			public Uri GetProxy(Uri targetUri)
			{
				NetworkCredential networkCredential = null;
				Uri uri = null;
				if (targetUri == null)
				{
					throw new ArgumentNullException("targetUri");
				}
				try
				{
					CFProxySettings systemProxySettings = CFNetwork.GetSystemProxySettings();
					CFProxy[] proxiesForUri = CFNetwork.GetProxiesForUri(targetUri, systemProxySettings);
					if (proxiesForUri != null)
					{
						int num = 0;
						while (num < proxiesForUri.Length && uri == null)
						{
							switch (proxiesForUri[num].ProxyType)
							{
							case CFProxyType.None:
								uri = targetUri;
								break;
							case CFProxyType.AutoConfigurationUrl:
								uri = CFNetwork.CFWebProxy.ExecuteProxyAutoConfigurationURL(proxiesForUri[num].AutoConfigurationUrl, targetUri, out networkCredential);
								break;
							case CFProxyType.AutoConfigurationJavaScript:
								uri = CFNetwork.CFWebProxy.GetProxyUriFromScript(proxiesForUri[num].AutoConfigurationJavaScript, targetUri, out networkCredential);
								break;
							case CFProxyType.FTP:
							case CFProxyType.HTTP:
							case CFProxyType.HTTPS:
								uri = CFNetwork.CFWebProxy.GetProxyUri(proxiesForUri[num], out networkCredential);
								break;
							}
							num++;
						}
						if (uri == null)
						{
							uri = targetUri;
						}
					}
					else
					{
						uri = targetUri;
					}
				}
				catch
				{
					uri = targetUri;
				}
				if (!this.userSpecified)
				{
					this.credentials = networkCredential;
				}
				return uri;
			}

			// Token: 0x06000213 RID: 531 RVA: 0x00006130 File Offset: 0x00004330
			public bool IsBypassed(Uri targetUri)
			{
				if (targetUri == null)
				{
					throw new ArgumentNullException("targetUri");
				}
				return this.GetProxy(targetUri) == targetUri;
			}

			// Token: 0x040001F6 RID: 502
			private ICredentials credentials;

			// Token: 0x040001F7 RID: 503
			private bool userSpecified;
		}
	}
}
