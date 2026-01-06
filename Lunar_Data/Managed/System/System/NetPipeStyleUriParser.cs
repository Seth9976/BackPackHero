using System;

namespace System
{
	/// <summary>A parser based on the NetPipe scheme for the "Indigo" system.</summary>
	// Token: 0x02000164 RID: 356
	public class NetPipeStyleUriParser : UriParser
	{
		/// <summary>Create a parser based on the NetPipe scheme for the "Indigo" system.</summary>
		// Token: 0x06000990 RID: 2448 RVA: 0x0002A275 File Offset: 0x00028475
		public NetPipeStyleUriParser()
			: base(UriParser.NetPipeUri.Flags)
		{
		}
	}
}
