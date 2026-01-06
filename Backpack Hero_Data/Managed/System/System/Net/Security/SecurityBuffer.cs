using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net.Security
{
	// Token: 0x0200065B RID: 1627
	internal class SecurityBuffer
	{
		// Token: 0x06003408 RID: 13320 RVA: 0x000BD0C8 File Offset: 0x000BB2C8
		public SecurityBuffer(byte[] data, int offset, int size, SecurityBufferType tokentype)
		{
			if (offset < 0 || offset > ((data == null) ? 0 : data.Length))
			{
				NetEventSource.Fail(this, FormattableStringFactory.Create("'offset' out of range.  [{0}]", new object[] { offset }), ".ctor");
			}
			if (size < 0 || size > ((data == null) ? 0 : (data.Length - offset)))
			{
				NetEventSource.Fail(this, FormattableStringFactory.Create("'size' out of range.  [{0}]", new object[] { size }), ".ctor");
			}
			this.offset = ((data == null || offset < 0) ? 0 : Math.Min(offset, data.Length));
			this.size = ((data == null || size < 0) ? 0 : Math.Min(size, data.Length - this.offset));
			this.type = tokentype;
			this.token = ((size == 0) ? null : data);
		}

		// Token: 0x06003409 RID: 13321 RVA: 0x000BD193 File Offset: 0x000BB393
		public SecurityBuffer(byte[] data, SecurityBufferType tokentype)
		{
			this.size = ((data == null) ? 0 : data.Length);
			this.type = tokentype;
			this.token = ((this.size == 0) ? null : data);
		}

		// Token: 0x0600340A RID: 13322 RVA: 0x000BD1C4 File Offset: 0x000BB3C4
		public SecurityBuffer(int size, SecurityBufferType tokentype)
		{
			if (size < 0)
			{
				NetEventSource.Fail(this, FormattableStringFactory.Create("'size' out of range.  [{0}]", new object[] { size }), ".ctor");
			}
			this.size = size;
			this.type = tokentype;
			this.token = ((size == 0) ? null : new byte[size]);
		}

		// Token: 0x0600340B RID: 13323 RVA: 0x000BD21F File Offset: 0x000BB41F
		public SecurityBuffer(ChannelBinding binding)
		{
			this.size = ((binding == null) ? 0 : binding.Size);
			this.type = SecurityBufferType.SECBUFFER_CHANNEL_BINDINGS;
			this.unmanagedToken = binding;
		}

		// Token: 0x04001F94 RID: 8084
		public int size;

		// Token: 0x04001F95 RID: 8085
		public SecurityBufferType type;

		// Token: 0x04001F96 RID: 8086
		public byte[] token;

		// Token: 0x04001F97 RID: 8087
		public SafeHandle unmanagedToken;

		// Token: 0x04001F98 RID: 8088
		public int offset;
	}
}
