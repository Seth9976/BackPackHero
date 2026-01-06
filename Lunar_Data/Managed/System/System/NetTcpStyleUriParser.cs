using System;

namespace System
{
	/// <summary>A parser based on the NetTcp scheme for the "Indigo" system.</summary>
	// Token: 0x02000165 RID: 357
	public class NetTcpStyleUriParser : UriParser
	{
		/// <summary>Create a parser based on the NetTcp scheme for the "Indigo" system.</summary>
		// Token: 0x06000991 RID: 2449 RVA: 0x0002A287 File Offset: 0x00028487
		public NetTcpStyleUriParser()
			: base(UriParser.NetTcpUri.Flags)
		{
		}
	}
}
