using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Net.Security
{
	// Token: 0x0200064F RID: 1615
	internal abstract class SafeFreeContextBuffer : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060033D7 RID: 13271 RVA: 0x00013AC8 File Offset: 0x00011CC8
		protected SafeFreeContextBuffer()
			: base(true)
		{
		}

		// Token: 0x060033D8 RID: 13272 RVA: 0x00013AF1 File Offset: 0x00011CF1
		internal void Set(IntPtr value)
		{
			this.handle = value;
		}

		// Token: 0x060033D9 RID: 13273 RVA: 0x000BC79C File Offset: 0x000BA99C
		internal static int EnumeratePackages(out int pkgnum, out SafeFreeContextBuffer pkgArray)
		{
			SafeFreeContextBuffer_SECURITY safeFreeContextBuffer_SECURITY = null;
			int num = global::Interop.SspiCli.EnumerateSecurityPackagesW(out pkgnum, out safeFreeContextBuffer_SECURITY);
			pkgArray = safeFreeContextBuffer_SECURITY;
			if (num != 0 && pkgArray != null)
			{
				pkgArray.SetHandleAsInvalid();
			}
			return num;
		}

		// Token: 0x060033DA RID: 13274 RVA: 0x000BC7C4 File Offset: 0x000BA9C4
		internal static SafeFreeContextBuffer CreateEmptyHandle()
		{
			return new SafeFreeContextBuffer_SECURITY();
		}

		// Token: 0x060033DB RID: 13275 RVA: 0x000BC7CC File Offset: 0x000BA9CC
		public unsafe static int QueryContextAttributes(SafeDeleteContext phContext, global::Interop.SspiCli.ContextAttribute contextAttribute, byte* buffer, SafeHandle refHandle)
		{
			int num = -2146893055;
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
				if (refHandle is SafeFreeContextBuffer)
				{
					((SafeFreeContextBuffer)refHandle).Set(*(IntPtr*)buffer);
				}
				else
				{
					((SafeFreeCertContext)refHandle).Set(*(IntPtr*)buffer);
				}
			}
			if (num != 0 && refHandle != null)
			{
				refHandle.SetHandleAsInvalid();
			}
			return num;
		}

		// Token: 0x060033DC RID: 13276 RVA: 0x000BC848 File Offset: 0x000BAA48
		public static int SetContextAttributes(SafeDeleteContext phContext, global::Interop.SspiCli.ContextAttribute contextAttribute, byte[] buffer)
		{
			int num;
			try
			{
				bool flag = false;
				phContext.DangerousAddRef(ref flag);
				num = global::Interop.SspiCli.SetContextAttributesW(ref phContext._handle, contextAttribute, buffer, buffer.Length);
			}
			finally
			{
				phContext.DangerousRelease();
			}
			return num;
		}
	}
}
