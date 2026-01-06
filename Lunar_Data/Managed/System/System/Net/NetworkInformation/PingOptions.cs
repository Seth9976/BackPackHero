using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Used to control how <see cref="T:System.Net.NetworkInformation.Ping" /> data packets are transmitted.</summary>
	// Token: 0x0200050D RID: 1293
	public class PingOptions
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PingOptions" /> class and sets the Time to Live and fragmentation values.</summary>
		/// <param name="ttl">An <see cref="T:System.Int32" /> value greater than zero that specifies the number of times that the <see cref="T:System.Net.NetworkInformation.Ping" /> data packets can be forwarded.</param>
		/// <param name="dontFragment">true to prevent data sent to the remote host from being fragmented; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="ttl " />is less than or equal to zero.</exception>
		// Token: 0x060029E4 RID: 10724 RVA: 0x00099F20 File Offset: 0x00098120
		public PingOptions(int ttl, bool dontFragment)
		{
			if (ttl <= 0)
			{
				throw new ArgumentOutOfRangeException("ttl");
			}
			this.ttl = ttl;
			this.dontFragment = dontFragment;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PingOptions" /> class.</summary>
		// Token: 0x060029E5 RID: 10725 RVA: 0x00099F50 File Offset: 0x00098150
		public PingOptions()
		{
		}

		/// <summary>Gets or sets the number of routing nodes that can forward the <see cref="T:System.Net.NetworkInformation.Ping" /> data before it is discarded.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that specifies the number of times the <see cref="T:System.Net.NetworkInformation.Ping" /> data packets can be forwarded. The default is 128.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than or equal to zero.</exception>
		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x060029E6 RID: 10726 RVA: 0x00099F63 File Offset: 0x00098163
		// (set) Token: 0x060029E7 RID: 10727 RVA: 0x00099F6B File Offset: 0x0009816B
		public int Ttl
		{
			get
			{
				return this.ttl;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ttl = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls fragmentation of the data sent to the remote host.</summary>
		/// <returns>true if the data cannot be sent in multiple packets; otherwise false. The default is false.</returns>
		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x060029E8 RID: 10728 RVA: 0x00099F83 File Offset: 0x00098183
		// (set) Token: 0x060029E9 RID: 10729 RVA: 0x00099F8B File Offset: 0x0009818B
		public bool DontFragment
		{
			get
			{
				return this.dontFragment;
			}
			set
			{
				this.dontFragment = value;
			}
		}

		// Token: 0x04001877 RID: 6263
		private const int DontFragmentFlag = 2;

		// Token: 0x04001878 RID: 6264
		private int ttl = 128;

		// Token: 0x04001879 RID: 6265
		private bool dontFragment;
	}
}
