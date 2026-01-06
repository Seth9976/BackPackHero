using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TwitchLib.Api.Helix.Models.Helpers;

namespace TwitchLib.Api.Helpers
{
	// Token: 0x02000022 RID: 34
	public static class ExtensionAnalyticsHelper
	{
		// Token: 0x060000BB RID: 187 RVA: 0x000039E4 File Offset: 0x00001BE4
		public static Task<List<ExtensionAnalytics>> HandleUrlAsync(string url)
		{
			ExtensionAnalyticsHelper.<HandleUrlAsync>d__0 <HandleUrlAsync>d__;
			<HandleUrlAsync>d__.url = url;
			<HandleUrlAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<ExtensionAnalytics>>.Create();
			<HandleUrlAsync>d__.<>1__state = -1;
			<HandleUrlAsync>d__.<>t__builder.Start<ExtensionAnalyticsHelper.<HandleUrlAsync>d__0>(ref <HandleUrlAsync>d__);
			return <HandleUrlAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003A27 File Offset: 0x00001C27
		private static IEnumerable<string> ExtractData(IEnumerable<string> cnts)
		{
			return Enumerable.ToList<string>(Enumerable.Where<string>(cnts, (string line) => Enumerable.Any<char>(line, new Func<char, bool>(char.IsDigit))));
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003A54 File Offset: 0x00001C54
		private static Task<string[]> GetContentsAsync(string url)
		{
			ExtensionAnalyticsHelper.<GetContentsAsync>d__2 <GetContentsAsync>d__;
			<GetContentsAsync>d__.url = url;
			<GetContentsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string[]>.Create();
			<GetContentsAsync>d__.<>1__state = -1;
			<GetContentsAsync>d__.<>t__builder.Start<ExtensionAnalyticsHelper.<GetContentsAsync>d__2>(ref <GetContentsAsync>d__);
			return <GetContentsAsync>d__.<>t__builder.Task;
		}
	}
}
