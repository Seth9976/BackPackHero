using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000637 RID: 1591
	internal class ServerObjectTerminatorSink : IMessageSink
	{
		// Token: 0x06003C12 RID: 15378 RVA: 0x000D0DBA File Offset: 0x000CEFBA
		public ServerObjectTerminatorSink(IMessageSink nextSink)
		{
			this._nextSink = nextSink;
		}

		// Token: 0x06003C13 RID: 15379 RVA: 0x000D0DCC File Offset: 0x000CEFCC
		public IMessage SyncProcessMessage(IMessage msg)
		{
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			serverIdentity.NotifyServerDynamicSinks(true, msg, false, false);
			IMessage message = this._nextSink.SyncProcessMessage(msg);
			serverIdentity.NotifyServerDynamicSinks(false, msg, false, false);
			return message;
		}

		// Token: 0x06003C14 RID: 15380 RVA: 0x000D0E08 File Offset: 0x000CF008
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			if (serverIdentity.HasServerDynamicSinks)
			{
				serverIdentity.NotifyServerDynamicSinks(true, msg, false, true);
				if (replySink != null)
				{
					replySink = new ServerObjectReplySink(serverIdentity, replySink);
				}
			}
			IMessageCtrl messageCtrl = this._nextSink.AsyncProcessMessage(msg, replySink);
			if (replySink == null)
			{
				serverIdentity.NotifyServerDynamicSinks(false, msg, true, true);
			}
			return messageCtrl;
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06003C15 RID: 15381 RVA: 0x000D0E59 File Offset: 0x000CF059
		public IMessageSink NextSink
		{
			get
			{
				return this._nextSink;
			}
		}

		// Token: 0x040026E9 RID: 9961
		private IMessageSink _nextSink;
	}
}
