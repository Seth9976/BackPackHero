using System;
using System.Runtime.Remoting.Channels;

namespace System.Runtime.Remoting
{
	// Token: 0x02000557 RID: 1367
	[Serializable]
	internal class ChannelInfo : IChannelInfo
	{
		// Token: 0x060035CC RID: 13772 RVA: 0x000C21DD File Offset: 0x000C03DD
		public ChannelInfo()
		{
			this.channelData = ChannelServices.GetCurrentChannelInfo();
		}

		// Token: 0x060035CD RID: 13773 RVA: 0x000C21F0 File Offset: 0x000C03F0
		public ChannelInfo(object remoteChannelData)
		{
			this.channelData = new object[] { remoteChannelData };
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x060035CE RID: 13774 RVA: 0x000C2208 File Offset: 0x000C0408
		// (set) Token: 0x060035CF RID: 13775 RVA: 0x000C2210 File Offset: 0x000C0410
		public object[] ChannelData
		{
			get
			{
				return this.channelData;
			}
			set
			{
				this.channelData = value;
			}
		}

		// Token: 0x04002512 RID: 9490
		private object[] channelData;
	}
}
