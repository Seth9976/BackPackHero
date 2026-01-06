using System;
using Unity;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000134 RID: 308
	public sealed class SafeX509ChainHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600072D RID: 1837 RVA: 0x00013B18 File Offset: 0x00011D18
		internal SafeX509ChainHandle(IntPtr handle)
			: base(true)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0000822E File Offset: 0x0000642E
		[MonoTODO]
		protected override bool ReleaseHandle()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00013B26 File Offset: 0x00011D26
		internal SafeX509ChainHandle()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
