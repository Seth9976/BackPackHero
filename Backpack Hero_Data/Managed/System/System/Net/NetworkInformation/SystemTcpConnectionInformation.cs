using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000515 RID: 1301
	internal class SystemTcpConnectionInformation : TcpConnectionInformation
	{
		// Token: 0x06002A07 RID: 10759 RVA: 0x0009A240 File Offset: 0x00098440
		public SystemTcpConnectionInformation(IPEndPoint local, IPEndPoint remote, TcpState state)
		{
			this.localEndPoint = local;
			this.remoteEndPoint = remote;
			this.state = state;
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06002A08 RID: 10760 RVA: 0x0009A25D File Offset: 0x0009845D
		public override TcpState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06002A09 RID: 10761 RVA: 0x0009A265 File Offset: 0x00098465
		public override IPEndPoint LocalEndPoint
		{
			get
			{
				return this.localEndPoint;
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06002A0A RID: 10762 RVA: 0x0009A26D File Offset: 0x0009846D
		public override IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.remoteEndPoint;
			}
		}

		// Token: 0x0400189A RID: 6298
		private IPEndPoint localEndPoint;

		// Token: 0x0400189B RID: 6299
		private IPEndPoint remoteEndPoint;

		// Token: 0x0400189C RID: 6300
		private TcpState state;
	}
}
