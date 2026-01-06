using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace System.Net
{
	// Token: 0x0200046F RID: 1135
	internal class AutoWebProxyScriptEngine
	{
		// Token: 0x06002408 RID: 9224 RVA: 0x0000219B File Offset: 0x0000039B
		public AutoWebProxyScriptEngine(WebProxy proxy, bool useRegistry)
		{
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06002409 RID: 9225 RVA: 0x000850CB File Offset: 0x000832CB
		// (set) Token: 0x0600240A RID: 9226 RVA: 0x000850D3 File Offset: 0x000832D3
		public Uri AutomaticConfigurationScript { get; set; }

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x0600240B RID: 9227 RVA: 0x000850DC File Offset: 0x000832DC
		// (set) Token: 0x0600240C RID: 9228 RVA: 0x000850E4 File Offset: 0x000832E4
		public bool AutomaticallyDetectSettings { get; set; }

		// Token: 0x0600240D RID: 9229 RVA: 0x000850F0 File Offset: 0x000832F0
		public bool GetProxies(Uri destination, out IList<string> proxyList)
		{
			int num = 0;
			return this.GetProxies(destination, out proxyList, ref num);
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x00085109 File Offset: 0x00083309
		public bool GetProxies(Uri destination, out IList<string> proxyList, ref int syncStatus)
		{
			proxyList = null;
			return false;
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x00003917 File Offset: 0x00001B17
		public void Close()
		{
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x00003917 File Offset: 0x00001B17
		public void Abort(ref int syncStatus)
		{
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x00003917 File Offset: 0x00001B17
		public void CheckForChanges()
		{
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x00085110 File Offset: 0x00083310
		public WebProxyData GetWebProxyData()
		{
			WebProxyData webProxyData;
			if (AutoWebProxyScriptEngine.IsWindows())
			{
				webProxyData = this.InitializeRegistryGlobalProxy();
				if (webProxyData != null)
				{
					return webProxyData;
				}
			}
			webProxyData = this.ReadEnvVariables();
			return webProxyData ?? new WebProxyData();
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x00085144 File Offset: 0x00083344
		private WebProxyData ReadEnvVariables()
		{
			string text = Environment.GetEnvironmentVariable("http_proxy") ?? Environment.GetEnvironmentVariable("HTTP_PROXY");
			if (text != null)
			{
				try
				{
					if (!text.StartsWith("http://"))
					{
						text = "http://" + text;
					}
					Uri uri = new Uri(text);
					IPAddress ipaddress;
					if (IPAddress.TryParse(uri.Host, out ipaddress))
					{
						if (IPAddress.Any.Equals(ipaddress))
						{
							uri = new UriBuilder(uri)
							{
								Host = "127.0.0.1"
							}.Uri;
						}
						else if (IPAddress.IPv6Any.Equals(ipaddress))
						{
							uri = new UriBuilder(uri)
							{
								Host = "[::1]"
							}.Uri;
						}
					}
					bool flag = false;
					ArrayList arrayList = new ArrayList();
					string text2 = Environment.GetEnvironmentVariable("no_proxy") ?? Environment.GetEnvironmentVariable("NO_PROXY");
					if (text2 != null)
					{
						foreach (string text3 in text2.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
						{
							if (text3 != "*.local")
							{
								arrayList.Add(text3);
							}
							else
							{
								flag = true;
							}
						}
					}
					return new WebProxyData
					{
						proxyAddress = uri,
						bypassOnLocal = flag,
						bypassList = AutoWebProxyScriptEngine.CreateBypassList(arrayList)
					};
				}
				catch (UriFormatException)
				{
				}
			}
			return null;
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x000852A4 File Offset: 0x000834A4
		private static bool IsWindows()
		{
			return Environment.OSVersion.Platform < PlatformID.Unix;
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x000852B4 File Offset: 0x000834B4
		private WebProxyData InitializeRegistryGlobalProxy()
		{
			if ((int)Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", "ProxyEnable", 0) <= 0)
			{
				return null;
			}
			string text = "";
			bool flag = false;
			ArrayList arrayList = new ArrayList();
			string text2 = (string)Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", "ProxyServer", null);
			if (text2 == null)
			{
				return null;
			}
			string text3 = (string)Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", "ProxyOverride", null);
			if (text2.Contains("="))
			{
				foreach (string text4 in text2.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
				{
					if (text4.StartsWith("http="))
					{
						text = text4.Substring(5);
						break;
					}
				}
			}
			else
			{
				text = text2;
			}
			if (text3 != null)
			{
				foreach (string text5 in text3.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
				{
					if (text5 != "<local>")
					{
						arrayList.Add(text5);
					}
					else
					{
						flag = true;
					}
				}
			}
			return new WebProxyData
			{
				proxyAddress = AutoWebProxyScriptEngine.ToUri(text),
				bypassOnLocal = flag,
				bypassList = AutoWebProxyScriptEngine.CreateBypassList(arrayList)
			};
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x000853ED File Offset: 0x000835ED
		private static Uri ToUri(string address)
		{
			if (address == null)
			{
				return null;
			}
			if (address.IndexOf("://", StringComparison.Ordinal) == -1)
			{
				address = "http://" + address;
			}
			return new Uri(address);
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x00085418 File Offset: 0x00083618
		private static ArrayList CreateBypassList(ArrayList al)
		{
			string[] array = al.ToArray(typeof(string)) as string[];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = "^" + Regex.Escape(array[i]).Replace("\\*", ".*").Replace("\\?", ".") + "$";
			}
			return new ArrayList(array);
		}
	}
}
