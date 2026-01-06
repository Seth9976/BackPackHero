using System;
using System.Runtime.InteropServices;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net.Security
{
	// Token: 0x02000656 RID: 1622
	internal abstract class SafeFreeContextBufferChannelBinding : ChannelBinding
	{
		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x060033EF RID: 13295 RVA: 0x000BCB24 File Offset: 0x000BAD24
		public override int Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x060033F0 RID: 13296 RVA: 0x000BCB2C File Offset: 0x000BAD2C
		public override bool IsInvalid
		{
			get
			{
				return this.handle == new IntPtr(0) || this.handle == new IntPtr(-1);
			}
		}

		// Token: 0x060033F1 RID: 13297 RVA: 0x00013AF1 File Offset: 0x00011CF1
		internal void Set(IntPtr value)
		{
			this.handle = value;
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x000BCB54 File Offset: 0x000BAD54
		internal static SafeFreeContextBufferChannelBinding CreateEmptyHandle()
		{
			return new SafeFreeContextBufferChannelBinding_SECURITY();
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x000BCB5C File Offset: 0x000BAD5C
		public unsafe static int QueryContextChannelBinding(SafeDeleteContext phContext, global::Interop.SspiCli.ContextAttribute contextAttribute, SecPkgContext_Bindings* buffer, SafeFreeContextBufferChannelBinding refHandle)
		{
			int num = -2146893055;
			if (contextAttribute != global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_ENDPOINT_BINDINGS && contextAttribute != global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_UNIQUE_BINDINGS)
			{
				return num;
			}
			try
			{
				bool flag = false;
				phContext.DangerousAddRef(ref flag);
				num = global::Interop.SspiCli.QueryContextAttributesW(ref phContext._handle, contextAttribute, (void*)buffer);
			}
			finally
			{
				phContext.DangerousRelease();
			}
			if (num == 0 && refHandle != null)
			{
				refHandle.Set(buffer->Bindings);
				refHandle._size = buffer->BindingsLength;
			}
			if (num != 0 && refHandle != null)
			{
				refHandle.SetHandleAsInvalid();
			}
			return num;
		}

		// Token: 0x060033F4 RID: 13300 RVA: 0x000BCBD8 File Offset: 0x000BADD8
		public override string ToString()
		{
			if (this.IsInvalid)
			{
				return null;
			}
			byte[] array = new byte[this._size];
			Marshal.Copy(this.handle, array, 0, array.Length);
			return BitConverter.ToString(array).Replace('-', ' ');
		}

		// Token: 0x04001F8F RID: 8079
		private int _size;
	}
}
