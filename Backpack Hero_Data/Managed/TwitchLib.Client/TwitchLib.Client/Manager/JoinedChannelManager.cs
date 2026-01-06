using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Manager
{
	// Token: 0x02000003 RID: 3
	internal class JoinedChannelManager
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00005EE4 File Offset: 0x000040E4
		public JoinedChannelManager()
		{
			this._joinedChannels = new ConcurrentDictionary<string, JoinedChannel>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005EFC File Offset: 0x000040FC
		public void AddJoinedChannel(JoinedChannel joinedChannel)
		{
			this._joinedChannels.TryAdd(joinedChannel.Channel, joinedChannel);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005F14 File Offset: 0x00004114
		public JoinedChannel GetJoinedChannel(string channel)
		{
			JoinedChannel joinedChannel;
			this._joinedChannels.TryGetValue(channel, ref joinedChannel);
			return joinedChannel;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00005F31 File Offset: 0x00004131
		public IReadOnlyList<JoinedChannel> GetJoinedChannels()
		{
			return Enumerable.ToList<JoinedChannel>(this._joinedChannels.Values).AsReadOnly();
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00005F48 File Offset: 0x00004148
		public void RemoveJoinedChannel(string channel)
		{
			JoinedChannel joinedChannel;
			this._joinedChannels.TryRemove(channel, ref joinedChannel);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005F64 File Offset: 0x00004164
		public void Clear()
		{
			this._joinedChannels.Clear();
		}

		// Token: 0x0400004F RID: 79
		private readonly ConcurrentDictionary<string, JoinedChannel> _joinedChannels;
	}
}
