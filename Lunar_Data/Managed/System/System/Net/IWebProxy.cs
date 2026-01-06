using System;

namespace System.Net
{
	/// <summary>Provides the base interface for implementation of proxy access for the <see cref="T:System.Net.WebRequest" /> class.</summary>
	// Token: 0x0200046C RID: 1132
	public interface IWebProxy
	{
		/// <summary>Returns the URI of a proxy.</summary>
		/// <returns>A <see cref="T:System.Uri" /> instance that contains the URI of the proxy used to contact <paramref name="destination" />.</returns>
		/// <param name="destination">A <see cref="T:System.Uri" /> that specifies the requested Internet resource. </param>
		// Token: 0x060023D0 RID: 9168
		Uri GetProxy(Uri destination);

		/// <summary>Indicates that the proxy should not be used for the specified host.</summary>
		/// <returns>true if the proxy server should not be used for <paramref name="host" />; otherwise, false.</returns>
		/// <param name="host">The <see cref="T:System.Uri" /> of the host to check for proxy use. </param>
		// Token: 0x060023D1 RID: 9169
		bool IsBypassed(Uri host);

		/// <summary>The credentials to submit to the proxy server for authentication.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> instance that contains the credentials that are needed to authenticate a request to the proxy server.</returns>
		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x060023D2 RID: 9170
		// (set) Token: 0x060023D3 RID: 9171
		ICredentials Credentials { get; set; }
	}
}
