using System;
using System.Configuration;
using Unity;

namespace System.Net.Configuration
{
	/// <summary>Represents the HttpListener element in the configuration file. This class cannot be inherited.</summary>
	// Token: 0x02000871 RID: 2161
	public sealed class HttpListenerElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.HttpListenerElement" /> class.</summary>
		// Token: 0x060044A2 RID: 17570 RVA: 0x00013B26 File Offset: 0x00011D26
		public HttpListenerElement()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the default timeout elements used for an <see cref="T:System.Net.HttpListener" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Net.Configuration.HttpListenerTimeoutsElement" />.The timeout elements used for an <see cref="T:System.Net.HttpListener" /> object.</returns>
		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x060044A3 RID: 17571 RVA: 0x000327E0 File Offset: 0x000309E0
		public HttpListenerTimeoutsElement Timeouts
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a value that indicates if <see cref="T:System.Net.HttpListener" /> uses the raw unescaped URI instead of the converted URI.</summary>
		/// <returns>A Boolean value that indicates if <see cref="T:System.Net.HttpListener" /> uses the raw unescaped URI, rather than the converted URI.</returns>
		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x060044A4 RID: 17572 RVA: 0x000ECE84 File Offset: 0x000EB084
		public bool UnescapeRequestUrl
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}
	}
}
