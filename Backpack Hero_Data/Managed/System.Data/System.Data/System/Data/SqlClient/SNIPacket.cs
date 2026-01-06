using System;
using System.Runtime.InteropServices;

namespace System.Data.SqlClient
{
	// Token: 0x0200021C RID: 540
	internal sealed class SNIPacket : SafeHandle
	{
		// Token: 0x060018FD RID: 6397 RVA: 0x0007DEFF File Offset: 0x0007C0FF
		internal SNIPacket(SafeHandle sniHandle)
			: base(IntPtr.Zero, true)
		{
			SNINativeMethodWrapper.SNIPacketAllocate(sniHandle, SNINativeMethodWrapper.IOType.WRITE, ref this.handle);
			if (IntPtr.Zero == this.handle)
			{
				throw SQL.SNIPacketAllocationFailure();
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060018FE RID: 6398 RVA: 0x0007DD20 File Offset: 0x0007BF20
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x0007DF34 File Offset: 0x0007C134
		protected override bool ReleaseHandle()
		{
			IntPtr handle = this.handle;
			this.handle = IntPtr.Zero;
			if (IntPtr.Zero != handle)
			{
				SNINativeMethodWrapper.SNIPacketRelease(handle);
			}
			return true;
		}
	}
}
