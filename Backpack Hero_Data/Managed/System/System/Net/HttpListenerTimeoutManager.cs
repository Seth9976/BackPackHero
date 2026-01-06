using System;

namespace System.Net
{
	/// <summary>The timeout manager to use for an <see cref="T:System.Net.HttpListener" /> object.</summary>
	// Token: 0x020004A2 RID: 1186
	public class HttpListenerTimeoutManager
	{
		/// <summary>Gets or sets the time, in seconds, allowed for the request entity body to arrive.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The time, in seconds, allowed for the request entity body to arrive.</returns>
		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x060025CD RID: 9677 RVA: 0x0000822E File Offset: 0x0000642E
		// (set) Token: 0x060025CE RID: 9678 RVA: 0x0000822E File Offset: 0x0000642E
		[MonoTODO]
		public TimeSpan EntityBody
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" />  to drain the entity body on a Keep-Alive connection.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" />  to drain the entity body on a Keep-Alive connection.</returns>
		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x060025CF RID: 9679 RVA: 0x0000822E File Offset: 0x0000642E
		// (set) Token: 0x060025D0 RID: 9680 RVA: 0x0000822E File Offset: 0x0000642E
		[MonoTODO]
		public TimeSpan DrainEntityBody
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the time, in seconds, allowed for the request to remain in the request queue before the <see cref="T:System.Net.HttpListener" /> picks it up.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The time, in seconds, allowed for the request to remain in the request queue before the <see cref="T:System.Net.HttpListener" /> picks it up.</returns>
		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x060025D1 RID: 9681 RVA: 0x0000822E File Offset: 0x0000642E
		// (set) Token: 0x060025D2 RID: 9682 RVA: 0x0000822E File Offset: 0x0000642E
		[MonoTODO]
		public TimeSpan RequestQueue
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the time, in seconds, allowed for an idle connection.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The time, in seconds, allowed for an idle connection.</returns>
		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x060025D3 RID: 9683 RVA: 0x0000822E File Offset: 0x0000642E
		// (set) Token: 0x060025D4 RID: 9684 RVA: 0x0000822E File Offset: 0x0000642E
		[MonoTODO]
		public TimeSpan IdleConnection
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" /> to parse the request header.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" /> to parse the request header.</returns>
		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x060025D5 RID: 9685 RVA: 0x0000822E File Offset: 0x0000642E
		// (set) Token: 0x060025D6 RID: 9686 RVA: 0x0000822E File Offset: 0x0000642E
		[MonoTODO]
		public TimeSpan HeaderWait
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the minimum send rate, in bytes-per-second, for the response. </summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The minimum send rate, in bytes-per-second, for the response.</returns>
		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x060025D7 RID: 9687 RVA: 0x0000822E File Offset: 0x0000642E
		// (set) Token: 0x060025D8 RID: 9688 RVA: 0x0000822E File Offset: 0x0000642E
		[MonoTODO]
		public long MinSendBytesPerSecond
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}
	}
}
