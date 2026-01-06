using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x0200044C RID: 1100
	internal class ServiceNameStore
	{
		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x060022B6 RID: 8886 RVA: 0x0007F1E7 File Offset: 0x0007D3E7
		public ServiceNameCollection ServiceNames
		{
			get
			{
				if (this.serviceNameCollection == null)
				{
					this.serviceNameCollection = new ServiceNameCollection(this.serviceNames);
				}
				return this.serviceNameCollection;
			}
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x0007F208 File Offset: 0x0007D408
		public ServiceNameStore()
		{
			this.serviceNames = new List<string>();
			this.serviceNameCollection = null;
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x0007F222 File Offset: 0x0007D422
		private bool AddSingleServiceName(string spn)
		{
			spn = ServiceNameCollection.NormalizeServiceName(spn);
			if (this.Contains(spn))
			{
				return false;
			}
			this.serviceNames.Add(spn);
			return true;
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x0007F244 File Offset: 0x0007D444
		public bool Add(string uriPrefix)
		{
			string[] array = this.BuildServiceNames(uriPrefix);
			bool flag = false;
			foreach (string text in array)
			{
				if (this.AddSingleServiceName(text))
				{
					flag = true;
					bool on = Logging.On;
				}
			}
			if (flag)
			{
				this.serviceNameCollection = null;
			}
			else
			{
				bool on2 = Logging.On;
			}
			return flag;
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x0007F294 File Offset: 0x0007D494
		public bool Remove(string uriPrefix)
		{
			string text = this.BuildSimpleServiceName(uriPrefix);
			text = ServiceNameCollection.NormalizeServiceName(text);
			bool flag = this.Contains(text);
			if (flag)
			{
				this.serviceNames.Remove(text);
				this.serviceNameCollection = null;
			}
			if (Logging.On)
			{
			}
			return flag;
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x0007F2D9 File Offset: 0x0007D4D9
		private bool Contains(string newServiceName)
		{
			return newServiceName != null && ServiceNameCollection.Contains(newServiceName, this.serviceNames);
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x0007F2EC File Offset: 0x0007D4EC
		public void Clear()
		{
			this.serviceNames.Clear();
			this.serviceNameCollection = null;
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x0007F300 File Offset: 0x0007D500
		private string ExtractHostname(string uriPrefix, bool allowInvalidUriStrings)
		{
			if (Uri.IsWellFormedUriString(uriPrefix, UriKind.Absolute))
			{
				return new Uri(uriPrefix).Host;
			}
			if (allowInvalidUriStrings)
			{
				int num = uriPrefix.IndexOf("://") + 3;
				int num2 = num;
				bool flag = false;
				while (num2 < uriPrefix.Length && uriPrefix[num2] != '/' && (uriPrefix[num2] != ':' || flag))
				{
					if (uriPrefix[num2] == '[')
					{
						if (flag)
						{
							num2 = num;
							break;
						}
						flag = true;
					}
					if (flag && uriPrefix[num2] == ']')
					{
						flag = false;
					}
					num2++;
				}
				return uriPrefix.Substring(num, num2 - num);
			}
			return null;
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x0007F394 File Offset: 0x0007D594
		public string BuildSimpleServiceName(string uriPrefix)
		{
			string text = this.ExtractHostname(uriPrefix, false);
			if (text != null)
			{
				return "HTTP/" + text;
			}
			return null;
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x0007F3BC File Offset: 0x0007D5BC
		public string[] BuildServiceNames(string uriPrefix)
		{
			string text = this.ExtractHostname(uriPrefix, true);
			IPAddress ipaddress = null;
			if (string.Compare(text, "*", StringComparison.InvariantCultureIgnoreCase) == 0 || string.Compare(text, "+", StringComparison.InvariantCultureIgnoreCase) == 0 || IPAddress.TryParse(text, out ipaddress))
			{
				try
				{
					string hostName = Dns.GetHostEntry(string.Empty).HostName;
					return new string[] { "HTTP/" + hostName };
				}
				catch (SocketException)
				{
					return new string[0];
				}
				catch (SecurityException)
				{
					return new string[0];
				}
			}
			if (!text.Contains("."))
			{
				try
				{
					string hostName2 = Dns.GetHostEntry(text).HostName;
					return new string[]
					{
						"HTTP/" + text,
						"HTTP/" + hostName2
					};
				}
				catch (SocketException)
				{
					return new string[] { "HTTP/" + text };
				}
				catch (SecurityException)
				{
					return new string[] { "HTTP/" + text };
				}
			}
			return new string[] { "HTTP/" + text };
		}

		// Token: 0x04001432 RID: 5170
		private List<string> serviceNames;

		// Token: 0x04001433 RID: 5171
		private ServiceNameCollection serviceNameCollection;
	}
}
