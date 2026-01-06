using System;
using System.Net;
using System.Threading;

namespace Mono.Net.Dns
{
	// Token: 0x020000C9 RID: 201
	internal class SimpleResolverEventArgs : EventArgs
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060003EB RID: 1003 RVA: 0x0000C648 File Offset: 0x0000A848
		// (remove) Token: 0x060003EC RID: 1004 RVA: 0x0000C680 File Offset: 0x0000A880
		public event EventHandler<SimpleResolverEventArgs> Completed;

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000C6BD File Offset: 0x0000A8BD
		// (set) Token: 0x060003EF RID: 1007 RVA: 0x0000C6C5 File Offset: 0x0000A8C5
		public ResolverError ResolverError { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000C6CE File Offset: 0x0000A8CE
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x0000C6D6 File Offset: 0x0000A8D6
		public string ErrorMessage { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000C6DF File Offset: 0x0000A8DF
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x0000C6E7 File Offset: 0x0000A8E7
		public string HostName { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000C6F0 File Offset: 0x0000A8F0
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x0000C6F8 File Offset: 0x0000A8F8
		public IPHostEntry HostEntry { get; internal set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000C701 File Offset: 0x0000A901
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x0000C709 File Offset: 0x0000A909
		public object UserToken { get; set; }

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000C712 File Offset: 0x0000A912
		internal void Reset(ResolverAsyncOperation op)
		{
			this.ResolverError = ResolverError.NoError;
			this.ErrorMessage = null;
			this.HostEntry = null;
			this.LastOperation = op;
			this.QueryID = 0;
			this.Retries = 0;
			this.PTRAddress = null;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000C748 File Offset: 0x0000A948
		protected internal void OnCompleted(object sender)
		{
			EventHandler<SimpleResolverEventArgs> completed = this.Completed;
			if (completed != null)
			{
				completed(sender, this);
			}
		}

		// Token: 0x04000387 RID: 903
		public ResolverAsyncOperation LastOperation;

		// Token: 0x0400038B RID: 907
		internal ushort QueryID;

		// Token: 0x0400038C RID: 908
		internal ushort Retries;

		// Token: 0x0400038D RID: 909
		internal Timer Timer;

		// Token: 0x0400038E RID: 910
		internal IPAddress PTRAddress;
	}
}
