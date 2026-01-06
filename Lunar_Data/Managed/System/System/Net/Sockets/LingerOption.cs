using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies whether a <see cref="T:System.Net.Sockets.Socket" /> will remain connected after a call to the <see cref="M:System.Net.Sockets.Socket.Close" /> or <see cref="M:System.Net.Sockets.TcpClient.Close" /> methods and the length of time it will remain connected, if data remains to be sent.</summary>
	// Token: 0x020005B6 RID: 1462
	public class LingerOption
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.LingerOption" /> class.</summary>
		/// <param name="enable">true to remain connected after the <see cref="M:System.Net.Sockets.Socket.Close" /> method is called; otherwise, false. </param>
		/// <param name="seconds">The number of seconds to remain connected after the <see cref="M:System.Net.Sockets.Socket.Close" /> method is called. </param>
		// Token: 0x06002F17 RID: 12055 RVA: 0x000A7AB4 File Offset: 0x000A5CB4
		public LingerOption(bool enable, int seconds)
		{
			this.Enabled = enable;
			this.LingerTime = seconds;
		}

		/// <summary>Gets or sets a value that indicates whether to linger after the <see cref="T:System.Net.Sockets.Socket" /> is closed.</summary>
		/// <returns>true if the <see cref="T:System.Net.Sockets.Socket" /> should linger after <see cref="M:System.Net.Sockets.Socket.Close" /> is called; otherwise, false.</returns>
		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06002F18 RID: 12056 RVA: 0x000A7ACA File Offset: 0x000A5CCA
		// (set) Token: 0x06002F19 RID: 12057 RVA: 0x000A7AD2 File Offset: 0x000A5CD2
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		/// <summary>Gets or sets the amount of time to remain connected after calling the <see cref="M:System.Net.Sockets.Socket.Close" /> method if data remains to be sent.</summary>
		/// <returns>The amount of time, in seconds, to remain connected after calling <see cref="M:System.Net.Sockets.Socket.Close" />.</returns>
		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06002F1A RID: 12058 RVA: 0x000A7ADB File Offset: 0x000A5CDB
		// (set) Token: 0x06002F1B RID: 12059 RVA: 0x000A7AE3 File Offset: 0x000A5CE3
		public int LingerTime
		{
			get
			{
				return this.lingerTime;
			}
			set
			{
				this.lingerTime = value;
			}
		}

		// Token: 0x04001B9E RID: 7070
		private bool enabled;

		// Token: 0x04001B9F RID: 7071
		private int lingerTime;
	}
}
