using System;

namespace Unity.Services.Core.Networking.Internal
{
	// Token: 0x02000017 RID: 23
	internal static class HttpRequestExtensions
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000020F1 File Offset: 0x000002F1
		public static HttpRequest AsGet(this HttpRequest self)
		{
			return self.SetMethod("GET");
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000020FE File Offset: 0x000002FE
		public static HttpRequest AsPost(this HttpRequest self)
		{
			return self.SetMethod("POST");
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000210B File Offset: 0x0000030B
		public static HttpRequest AsPut(this HttpRequest self)
		{
			return self.SetMethod("PUT");
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002118 File Offset: 0x00000318
		public static HttpRequest AsDelete(this HttpRequest self)
		{
			return self.SetMethod("DELETE");
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002125 File Offset: 0x00000325
		public static HttpRequest AsPatch(this HttpRequest self)
		{
			return self.SetMethod("PATCH");
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002132 File Offset: 0x00000332
		public static HttpRequest AsHead(this HttpRequest self)
		{
			return self.SetMethod("HEAD");
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000213F File Offset: 0x0000033F
		public static HttpRequest AsConnect(this HttpRequest self)
		{
			return self.SetMethod("CONNECT");
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000214C File Offset: 0x0000034C
		public static HttpRequest AsOptions(this HttpRequest self)
		{
			return self.SetMethod("OPTIONS");
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002159 File Offset: 0x00000359
		public static HttpRequest AsTrace(this HttpRequest self)
		{
			return self.SetMethod("TRACE");
		}
	}
}
