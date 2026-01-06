using System;
using System.Runtime.InteropServices;

namespace System.Data.SqlClient
{
	// Token: 0x0200021B RID: 539
	internal sealed class SNIHandle : SafeHandle
	{
		// Token: 0x060018F8 RID: 6392 RVA: 0x0007DE04 File Offset: 0x0007C004
		internal SNIHandle(SNINativeMethodWrapper.ConsumerInfo myInfo, string serverName, byte[] spnBuffer, bool ignoreSniOpenTimeout, int timeout, out byte[] instanceName, bool flushCache, bool fSync, bool fParallel)
			: base(IntPtr.Zero, true)
		{
			try
			{
			}
			finally
			{
				this._fSync = fSync;
				instanceName = new byte[256];
				if (ignoreSniOpenTimeout)
				{
					timeout = -1;
				}
				this._status = SNINativeMethodWrapper.SNIOpenSyncEx(myInfo, serverName, ref this.handle, spnBuffer, instanceName, flushCache, fSync, timeout, fParallel);
			}
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x0007DE74 File Offset: 0x0007C074
		internal SNIHandle(SNINativeMethodWrapper.ConsumerInfo myInfo, SNIHandle parent)
			: base(IntPtr.Zero, true)
		{
			try
			{
			}
			finally
			{
				this._status = SNINativeMethodWrapper.SNIOpenMarsSession(myInfo, parent, ref this.handle, parent._fSync);
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060018FA RID: 6394 RVA: 0x0007DD20 File Offset: 0x0007BF20
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x0007DEC0 File Offset: 0x0007C0C0
		protected override bool ReleaseHandle()
		{
			IntPtr handle = this.handle;
			this.handle = IntPtr.Zero;
			return !(IntPtr.Zero != handle) || SNINativeMethodWrapper.SNIClose(handle) == 0U;
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060018FC RID: 6396 RVA: 0x0007DEF7 File Offset: 0x0007C0F7
		internal uint Status
		{
			get
			{
				return this._status;
			}
		}

		// Token: 0x04001214 RID: 4628
		private readonly uint _status = uint.MaxValue;

		// Token: 0x04001215 RID: 4629
		private readonly bool _fSync;
	}
}
