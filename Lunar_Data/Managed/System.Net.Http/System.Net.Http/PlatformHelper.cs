using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading;

namespace System.Net.Http
{
	// Token: 0x02000010 RID: 16
	internal static class PlatformHelper
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x000039E2 File Offset: 0x00001BE2
		internal static bool IsContentHeader(string name)
		{
			return HttpHeaders.GetKnownHeaderKind(name) == HttpHeaderKind.Content;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000039ED File Offset: 0x00001BED
		internal static string GetSingleHeaderString(string name, IEnumerable<string> values)
		{
			return HttpHeaders.GetSingleHeaderString(name, values);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000039F6 File Offset: 0x00001BF6
		internal static StreamContent CreateStreamContent(Stream stream, CancellationToken cancellationToken)
		{
			return new StreamContent(stream, cancellationToken);
		}
	}
}
