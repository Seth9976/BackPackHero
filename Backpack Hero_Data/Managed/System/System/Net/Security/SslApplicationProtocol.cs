using System;
using System.Text;

namespace System.Net.Security
{
	// Token: 0x0200065F RID: 1631
	public readonly struct SslApplicationProtocol : IEquatable<SslApplicationProtocol>
	{
		// Token: 0x06003410 RID: 13328 RVA: 0x000BD300 File Offset: 0x000BB500
		internal SslApplicationProtocol(byte[] protocol, bool copy)
		{
			if (protocol == null)
			{
				throw new ArgumentNullException("protocol");
			}
			if (protocol.Length == 0 || protocol.Length > 255)
			{
				throw new ArgumentException("The application protocol value is invalid.", "protocol");
			}
			if (copy)
			{
				byte[] array = new byte[protocol.Length];
				Array.Copy(protocol, 0, array, 0, protocol.Length);
				this._readOnlyProtocol = new ReadOnlyMemory<byte>(array);
				return;
			}
			this._readOnlyProtocol = new ReadOnlyMemory<byte>(protocol);
		}

		// Token: 0x06003411 RID: 13329 RVA: 0x000BD36A File Offset: 0x000BB56A
		public SslApplicationProtocol(byte[] protocol)
		{
			this = new SslApplicationProtocol(protocol, true);
		}

		// Token: 0x06003412 RID: 13330 RVA: 0x000BD374 File Offset: 0x000BB574
		public SslApplicationProtocol(string protocol)
		{
			this = new SslApplicationProtocol(SslApplicationProtocol.s_utf8.GetBytes(protocol), false);
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06003413 RID: 13331 RVA: 0x000BD388 File Offset: 0x000BB588
		public ReadOnlyMemory<byte> Protocol
		{
			get
			{
				return this._readOnlyProtocol;
			}
		}

		// Token: 0x06003414 RID: 13332 RVA: 0x000BD390 File Offset: 0x000BB590
		public bool Equals(SslApplicationProtocol other)
		{
			return this._readOnlyProtocol.Length == other._readOnlyProtocol.Length && ((this._readOnlyProtocol.IsEmpty && other._readOnlyProtocol.IsEmpty) || this._readOnlyProtocol.Span.SequenceEqual(other._readOnlyProtocol.Span));
		}

		// Token: 0x06003415 RID: 13333 RVA: 0x000BD3F4 File Offset: 0x000BB5F4
		public override bool Equals(object obj)
		{
			if (obj is SslApplicationProtocol)
			{
				SslApplicationProtocol sslApplicationProtocol = (SslApplicationProtocol)obj;
				return this.Equals(sslApplicationProtocol);
			}
			return false;
		}

		// Token: 0x06003416 RID: 13334 RVA: 0x000BD41C File Offset: 0x000BB61C
		public unsafe override int GetHashCode()
		{
			if (this._readOnlyProtocol.Length == 0)
			{
				return 0;
			}
			int num = 0;
			ReadOnlySpan<byte> span = this._readOnlyProtocol.Span;
			for (int i = 0; i < this._readOnlyProtocol.Length; i++)
			{
				num = ((num << 5) + num) ^ (int)(*span[i]);
			}
			return num;
		}

		// Token: 0x06003417 RID: 13335 RVA: 0x000BD470 File Offset: 0x000BB670
		public unsafe override string ToString()
		{
			string text;
			try
			{
				if (this._readOnlyProtocol.Length == 0)
				{
					text = null;
				}
				else
				{
					text = SslApplicationProtocol.s_utf8.GetString(this._readOnlyProtocol.Span);
				}
			}
			catch
			{
				int num = this._readOnlyProtocol.Length * 5;
				char[] array = new char[num];
				int num2 = 0;
				ReadOnlySpan<byte> span = this._readOnlyProtocol.Span;
				for (int i = 0; i < num; i += 5)
				{
					byte b = *span[num2++];
					array[i] = '0';
					array[i + 1] = 'x';
					int num3;
					array[i + 2] = SslApplicationProtocol.GetHexValue(Math.DivRem((int)b, 16, out num3));
					array[i + 3] = SslApplicationProtocol.GetHexValue(num3);
					array[i + 4] = ' ';
				}
				text = new string(array, 0, num - 1);
			}
			return text;
		}

		// Token: 0x06003418 RID: 13336 RVA: 0x000BD548 File Offset: 0x000BB748
		private static char GetHexValue(int i)
		{
			if (i < 10)
			{
				return (char)(i + 48);
			}
			return (char)(i - 10 + 97);
		}

		// Token: 0x06003419 RID: 13337 RVA: 0x000BD55D File Offset: 0x000BB75D
		public static bool operator ==(SslApplicationProtocol left, SslApplicationProtocol right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600341A RID: 13338 RVA: 0x000BD567 File Offset: 0x000BB767
		public static bool operator !=(SslApplicationProtocol left, SslApplicationProtocol right)
		{
			return !(left == right);
		}

		// Token: 0x04001FAB RID: 8107
		private readonly ReadOnlyMemory<byte> _readOnlyProtocol;

		// Token: 0x04001FAC RID: 8108
		private static readonly Encoding s_utf8 = Encoding.GetEncoding(Encoding.UTF8.CodePage, EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback);

		// Token: 0x04001FAD RID: 8109
		public static readonly SslApplicationProtocol Http2 = new SslApplicationProtocol(new byte[] { 104, 50 }, false);

		// Token: 0x04001FAE RID: 8110
		public static readonly SslApplicationProtocol Http11 = new SslApplicationProtocol(new byte[] { 104, 116, 116, 112, 47, 49, 46, 49 }, false);
	}
}
