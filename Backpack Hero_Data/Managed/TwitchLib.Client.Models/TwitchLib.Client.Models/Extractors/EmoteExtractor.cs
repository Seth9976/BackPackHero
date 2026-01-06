using System;
using System.Collections.Generic;
using TwitchLib.Client.Models.Builders;

namespace TwitchLib.Client.Models.Extractors
{
	// Token: 0x02000027 RID: 39
	public class EmoteExtractor
	{
		// Token: 0x06000173 RID: 371 RVA: 0x00007940 File Offset: 0x00005B40
		public IEnumerable<Emote> Extract(string rawEmoteSetString, string message)
		{
			if (string.IsNullOrEmpty(rawEmoteSetString) || string.IsNullOrEmpty(message))
			{
				yield break;
			}
			if (rawEmoteSetString.Contains("/"))
			{
				foreach (string text in rawEmoteSetString.Split(new char[] { '/' }))
				{
					string emoteId = text.Split(new char[] { ':' })[0];
					if (text.Contains(","))
					{
						foreach (string text2 in text.Replace(emoteId + ":", "").Split(new char[] { ',' }))
						{
							yield return this.GetEmote(text2, emoteId, message, false);
						}
						string[] array2 = null;
					}
					else
					{
						yield return this.GetEmote(text, emoteId, message, true);
					}
					emoteId = null;
				}
				string[] array = null;
			}
			else
			{
				string emoteId = rawEmoteSetString.Split(new char[] { ':' })[0];
				if (rawEmoteSetString.Contains(","))
				{
					foreach (string text3 in rawEmoteSetString.Replace(emoteId + ":", "").Split(new char[] { ',' }))
					{
						yield return this.GetEmote(text3, emoteId, message, false);
					}
					string[] array = null;
				}
				else
				{
					yield return this.GetEmote(rawEmoteSetString, emoteId, message, true);
				}
				emoteId = null;
			}
			yield break;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00007960 File Offset: 0x00005B60
		private Emote GetEmote(string emoteData, string emoteId, string message, bool single = false)
		{
			int num;
			int num2;
			if (single)
			{
				num = int.Parse(emoteData.Split(new char[] { ':' })[1].Split(new char[] { '-' })[0]);
				num2 = int.Parse(emoteData.Split(new char[] { ':' })[1].Split(new char[] { '-' })[1]);
			}
			else
			{
				num = int.Parse(emoteData.Split(new char[] { '-' })[0]);
				num2 = int.Parse(emoteData.Split(new char[] { '-' })[1]);
			}
			string text = message.Substring(num, num2 - num + 1);
			return EmoteBuilder.Create().WithId(emoteId).WithName(text)
				.WithStartIndex(num)
				.WithEndIndex(num2)
				.Build();
		}
	}
}
