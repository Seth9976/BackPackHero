using System;

namespace System.Net.Sockets
{
	/// <summary>Presents UDP receive result information from a call to the <see cref="M:System.Net.Sockets.UdpClient.ReceiveAsync" /> method.</summary>
	// Token: 0x020005CC RID: 1484
	public struct UdpReceiveResult : IEquatable<UdpReceiveResult>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.UdpReceiveResult" /> class.</summary>
		/// <param name="buffer">A buffer for data to receive in the UDP packet.</param>
		/// <param name="remoteEndPoint">The remote endpoint of the UDP packet.</param>
		// Token: 0x06002FBF RID: 12223 RVA: 0x000A9991 File Offset: 0x000A7B91
		public UdpReceiveResult(byte[] buffer, IPEndPoint remoteEndPoint)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (remoteEndPoint == null)
			{
				throw new ArgumentNullException("remoteEndPoint");
			}
			this.m_buffer = buffer;
			this.m_remoteEndPoint = remoteEndPoint;
		}

		/// <summary>Gets a buffer with the data received in the UDP packet.</summary>
		/// <returns>Returns <see cref="T:System.Byte" />.A <see cref="T:System.Byte" /> array with the data received in the UDP packet.</returns>
		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06002FC0 RID: 12224 RVA: 0x000A99BD File Offset: 0x000A7BBD
		public byte[] Buffer
		{
			get
			{
				return this.m_buffer;
			}
		}

		/// <summary>Gets the remote endpoint from which the UDP packet was received. </summary>
		/// <returns>Returns <see cref="T:System.Net.IPEndPoint" />.The remote endpoint from which the UDP packet was received.</returns>
		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06002FC1 RID: 12225 RVA: 0x000A99C5 File Offset: 0x000A7BC5
		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.m_remoteEndPoint;
			}
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.The hash code.</returns>
		// Token: 0x06002FC2 RID: 12226 RVA: 0x000A99CD File Offset: 0x000A7BCD
		public override int GetHashCode()
		{
			if (this.m_buffer == null)
			{
				return 0;
			}
			return this.m_buffer.GetHashCode() ^ this.m_remoteEndPoint.GetHashCode();
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="obj" /> is an instance of <see cref="T:System.Net.Sockets.UdpReceiveResult" /> and equals the value of the instance; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this instance.</param>
		// Token: 0x06002FC3 RID: 12227 RVA: 0x000A99F0 File Offset: 0x000A7BF0
		public override bool Equals(object obj)
		{
			return obj is UdpReceiveResult && this.Equals((UdpReceiveResult)obj);
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="other" /> is an instance of <see cref="T:System.Net.Sockets.UdpReceiveResult" /> and equals the value of the instance; otherwise, false.</returns>
		/// <param name="other">The object to compare with this instance.</param>
		// Token: 0x06002FC4 RID: 12228 RVA: 0x000A9A08 File Offset: 0x000A7C08
		public bool Equals(UdpReceiveResult other)
		{
			return object.Equals(this.m_buffer, other.m_buffer) && object.Equals(this.m_remoteEndPoint, other.m_remoteEndPoint);
		}

		/// <summary>Tests whether two specified <see cref="T:System.Net.Sockets.UdpReceiveResult" /> instances are equivalent.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, false.</returns>
		/// <param name="left">The <see cref="T:System.Net.Sockets.UdpReceiveResult" /> instance that is to the left of the equality operator.</param>
		/// <param name="right">The <see cref="T:System.Net.Sockets.UdpReceiveResult" /> instance that is to the right of the equality operator.</param>
		// Token: 0x06002FC5 RID: 12229 RVA: 0x000A9A30 File Offset: 0x000A7C30
		public static bool operator ==(UdpReceiveResult left, UdpReceiveResult right)
		{
			return left.Equals(right);
		}

		/// <summary>Tests whether two specified <see cref="T:System.Net.Sockets.UdpReceiveResult" /> instances are not equal.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="left" /> and <paramref name="right" /> are unequal; otherwise, false.</returns>
		/// <param name="left">The <see cref="T:System.Net.Sockets.UdpReceiveResult" /> instance that is to the left of the not equal operator.</param>
		/// <param name="right">The <see cref="T:System.Net.Sockets.UdpReceiveResult" /> instance that is to the right of the not equal operator.</param>
		// Token: 0x06002FC6 RID: 12230 RVA: 0x000A9A3A File Offset: 0x000A7C3A
		public static bool operator !=(UdpReceiveResult left, UdpReceiveResult right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04001C95 RID: 7317
		private byte[] m_buffer;

		// Token: 0x04001C96 RID: 7318
		private IPEndPoint m_remoteEndPoint;
	}
}
