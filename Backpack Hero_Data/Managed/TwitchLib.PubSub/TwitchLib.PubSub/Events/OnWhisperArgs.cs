using System;
using TwitchLib.PubSub.Models.Responses.Messages;

namespace TwitchLib.PubSub.Events
{
	// Token: 0x02000053 RID: 83
	public class OnWhisperArgs : EventArgs
	{
		// Token: 0x0400018E RID: 398
		public Whisper Whisper;

		// Token: 0x0400018F RID: 399
		public string ChannelId;
	}
}
