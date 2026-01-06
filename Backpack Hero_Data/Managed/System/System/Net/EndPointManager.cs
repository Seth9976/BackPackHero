using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x02000492 RID: 1170
	internal sealed class EndPointManager
	{
		// Token: 0x060024E5 RID: 9445 RVA: 0x0000219B File Offset: 0x0000039B
		private EndPointManager()
		{
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x00088330 File Offset: 0x00086530
		public static void AddListener(HttpListener listener)
		{
			ArrayList arrayList = new ArrayList();
			try
			{
				Hashtable hashtable = EndPointManager.ip_to_endpoints;
				lock (hashtable)
				{
					foreach (string text in listener.Prefixes)
					{
						EndPointManager.AddPrefixInternal(text, listener);
						arrayList.Add(text);
					}
				}
			}
			catch
			{
				foreach (object obj in arrayList)
				{
					EndPointManager.RemovePrefix((string)obj, listener);
				}
				throw;
			}
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x00088410 File Offset: 0x00086610
		public static void AddPrefix(string prefix, HttpListener listener)
		{
			Hashtable hashtable = EndPointManager.ip_to_endpoints;
			lock (hashtable)
			{
				EndPointManager.AddPrefixInternal(prefix, listener);
			}
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x00088450 File Offset: 0x00086650
		private static void AddPrefixInternal(string p, HttpListener listener)
		{
			ListenerPrefix listenerPrefix = new ListenerPrefix(p);
			if (listenerPrefix.Path.IndexOf('%') != -1)
			{
				throw new HttpListenerException(400, "Invalid path.");
			}
			if (listenerPrefix.Path.IndexOf("//", StringComparison.Ordinal) != -1)
			{
				throw new HttpListenerException(400, "Invalid path.");
			}
			EndPointManager.GetEPListener(listenerPrefix.Host, listenerPrefix.Port, listener, listenerPrefix.Secure).AddPrefix(listenerPrefix, listener);
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x000884C8 File Offset: 0x000866C8
		private static EndPointListener GetEPListener(string host, int port, HttpListener listener, bool secure)
		{
			IPAddress ipaddress;
			if (host == "*")
			{
				ipaddress = IPAddress.Any;
			}
			else if (!IPAddress.TryParse(host, out ipaddress))
			{
				try
				{
					IPHostEntry hostByName = Dns.GetHostByName(host);
					if (hostByName != null)
					{
						ipaddress = hostByName.AddressList[0];
					}
					else
					{
						ipaddress = IPAddress.Any;
					}
				}
				catch
				{
					ipaddress = IPAddress.Any;
				}
			}
			Hashtable hashtable;
			if (EndPointManager.ip_to_endpoints.ContainsKey(ipaddress))
			{
				hashtable = (Hashtable)EndPointManager.ip_to_endpoints[ipaddress];
			}
			else
			{
				hashtable = new Hashtable();
				EndPointManager.ip_to_endpoints[ipaddress] = hashtable;
			}
			EndPointListener endPointListener;
			if (hashtable.ContainsKey(port))
			{
				endPointListener = (EndPointListener)hashtable[port];
			}
			else
			{
				endPointListener = new EndPointListener(listener, ipaddress, port, secure);
				hashtable[port] = endPointListener;
			}
			return endPointListener;
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x0008859C File Offset: 0x0008679C
		public static void RemoveEndPoint(EndPointListener epl, IPEndPoint ep)
		{
			Hashtable hashtable = EndPointManager.ip_to_endpoints;
			lock (hashtable)
			{
				Hashtable hashtable2 = (Hashtable)EndPointManager.ip_to_endpoints[ep.Address];
				hashtable2.Remove(ep.Port);
				if (hashtable2.Count == 0)
				{
					EndPointManager.ip_to_endpoints.Remove(ep.Address);
				}
				epl.Close();
			}
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x00088618 File Offset: 0x00086818
		public static void RemoveListener(HttpListener listener)
		{
			Hashtable hashtable = EndPointManager.ip_to_endpoints;
			lock (hashtable)
			{
				foreach (string text in listener.Prefixes)
				{
					EndPointManager.RemovePrefixInternal(text, listener);
				}
			}
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x0008868C File Offset: 0x0008688C
		public static void RemovePrefix(string prefix, HttpListener listener)
		{
			Hashtable hashtable = EndPointManager.ip_to_endpoints;
			lock (hashtable)
			{
				EndPointManager.RemovePrefixInternal(prefix, listener);
			}
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x000886CC File Offset: 0x000868CC
		private static void RemovePrefixInternal(string prefix, HttpListener listener)
		{
			ListenerPrefix listenerPrefix = new ListenerPrefix(prefix);
			if (listenerPrefix.Path.IndexOf('%') != -1)
			{
				return;
			}
			if (listenerPrefix.Path.IndexOf("//", StringComparison.Ordinal) != -1)
			{
				return;
			}
			EndPointManager.GetEPListener(listenerPrefix.Host, listenerPrefix.Port, listener, listenerPrefix.Secure).RemovePrefix(listenerPrefix, listener);
		}

		// Token: 0x04001567 RID: 5479
		private static Hashtable ip_to_endpoints = new Hashtable();
	}
}
