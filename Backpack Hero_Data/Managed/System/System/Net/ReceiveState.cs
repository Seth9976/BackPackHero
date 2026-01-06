using System;

namespace System.Net
{
	// Token: 0x02000396 RID: 918
	internal class ReceiveState
	{
		// Token: 0x06001E38 RID: 7736 RVA: 0x0006E1E8 File Offset: 0x0006C3E8
		internal ReceiveState(CommandStream connection)
		{
			this.Connection = connection;
			this.Resp = new ResponseDescription();
			this.Buffer = new byte[1024];
			this.ValidThrough = 0;
		}

		// Token: 0x04000FEC RID: 4076
		private const int bufferSize = 1024;

		// Token: 0x04000FED RID: 4077
		internal ResponseDescription Resp;

		// Token: 0x04000FEE RID: 4078
		internal int ValidThrough;

		// Token: 0x04000FEF RID: 4079
		internal byte[] Buffer;

		// Token: 0x04000FF0 RID: 4080
		internal CommandStream Connection;
	}
}
