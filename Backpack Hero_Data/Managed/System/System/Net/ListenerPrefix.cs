using System;

namespace System.Net
{
	// Token: 0x020004B2 RID: 1202
	internal sealed class ListenerPrefix
	{
		// Token: 0x060026C9 RID: 9929 RVA: 0x0008FCDF File Offset: 0x0008DEDF
		public ListenerPrefix(string prefix)
		{
			this.original = prefix;
			this.Parse(prefix);
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x0008FCF5 File Offset: 0x0008DEF5
		public override string ToString()
		{
			return this.original;
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x060026CB RID: 9931 RVA: 0x0008FCFD File Offset: 0x0008DEFD
		// (set) Token: 0x060026CC RID: 9932 RVA: 0x0008FD05 File Offset: 0x0008DF05
		public IPAddress[] Addresses
		{
			get
			{
				return this.addresses;
			}
			set
			{
				this.addresses = value;
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x060026CD RID: 9933 RVA: 0x0008FD0E File Offset: 0x0008DF0E
		public bool Secure
		{
			get
			{
				return this.secure;
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x060026CE RID: 9934 RVA: 0x0008FD16 File Offset: 0x0008DF16
		public string Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x060026CF RID: 9935 RVA: 0x0008FD1E File Offset: 0x0008DF1E
		public int Port
		{
			get
			{
				return (int)this.port;
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x060026D0 RID: 9936 RVA: 0x0008FD26 File Offset: 0x0008DF26
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x060026D1 RID: 9937 RVA: 0x0008FD30 File Offset: 0x0008DF30
		public override bool Equals(object o)
		{
			ListenerPrefix listenerPrefix = o as ListenerPrefix;
			return listenerPrefix != null && this.original == listenerPrefix.original;
		}

		// Token: 0x060026D2 RID: 9938 RVA: 0x0008FD5A File Offset: 0x0008DF5A
		public override int GetHashCode()
		{
			return this.original.GetHashCode();
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x0008FD68 File Offset: 0x0008DF68
		private void Parse(string uri)
		{
			ushort num = 80;
			if (uri.StartsWith("https://"))
			{
				num = 443;
				this.secure = true;
			}
			int length = uri.Length;
			int num2 = uri.IndexOf(':') + 3;
			if (num2 >= length)
			{
				throw new ArgumentException("No host specified.");
			}
			int num3 = uri.IndexOf(':', num2, length - num2);
			if (uri[num2] == '[')
			{
				num3 = uri.IndexOf("]:") + 1;
			}
			if (num2 == num3)
			{
				throw new ArgumentException("No host specified.");
			}
			int num4 = uri.IndexOf('/', num2, length - num2);
			if (num4 == -1)
			{
				throw new ArgumentException("No path specified.");
			}
			if (num3 > 0)
			{
				this.host = uri.Substring(num2, num3 - num2).Trim(new char[] { '[', ']' });
				this.port = ushort.Parse(uri.Substring(num3 + 1, num4 - num3 - 1));
			}
			else
			{
				this.host = uri.Substring(num2, num4 - num2).Trim(new char[] { '[', ']' });
				this.port = num;
			}
			this.path = uri.Substring(num4);
			if (this.path.Length != 1)
			{
				this.path = this.path.Substring(0, this.path.Length - 1);
			}
		}

		// Token: 0x060026D4 RID: 9940 RVA: 0x0008FEB4 File Offset: 0x0008E0B4
		public static void CheckUri(string uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uriPrefix");
			}
			if (!uri.StartsWith("http://") && !uri.StartsWith("https://"))
			{
				throw new ArgumentException("Only 'http' and 'https' schemes are supported.");
			}
			int length = uri.Length;
			int num = uri.IndexOf(':') + 3;
			if (num >= length)
			{
				throw new ArgumentException("No host specified.");
			}
			int num2 = uri.IndexOf(':', num, length - num);
			if (uri[num] == '[')
			{
				num2 = uri.IndexOf("]:") + 1;
			}
			if (num == num2)
			{
				throw new ArgumentException("No host specified.");
			}
			int num3 = uri.IndexOf('/', num, length - num);
			if (num3 == -1)
			{
				throw new ArgumentException("No path specified.");
			}
			if (num2 > 0)
			{
				try
				{
					int num4 = int.Parse(uri.Substring(num2 + 1, num3 - num2 - 1));
					if (num4 <= 0 || num4 >= 65536)
					{
						throw new Exception();
					}
				}
				catch
				{
					throw new ArgumentException("Invalid port.");
				}
			}
			if (uri[uri.Length - 1] != '/')
			{
				throw new ArgumentException("The prefix must end with '/'");
			}
		}

		// Token: 0x04001674 RID: 5748
		private string original;

		// Token: 0x04001675 RID: 5749
		private string host;

		// Token: 0x04001676 RID: 5750
		private ushort port;

		// Token: 0x04001677 RID: 5751
		private string path;

		// Token: 0x04001678 RID: 5752
		private bool secure;

		// Token: 0x04001679 RID: 5753
		private IPAddress[] addresses;

		// Token: 0x0400167A RID: 5754
		public HttpListener Listener;
	}
}
