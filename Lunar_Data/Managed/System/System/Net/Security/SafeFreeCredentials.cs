using System;
using System.Runtime.InteropServices;

namespace System.Net.Security
{
	// Token: 0x02000652 RID: 1618
	internal abstract class SafeFreeCredentials : SafeHandle
	{
		// Token: 0x060033E2 RID: 13282 RVA: 0x000BC8B3 File Offset: 0x000BAAB3
		protected SafeFreeCredentials()
			: base(IntPtr.Zero, true)
		{
			this._handle = default(global::Interop.SspiCli.CredHandle);
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x060033E3 RID: 13283 RVA: 0x000BC8CD File Offset: 0x000BAACD
		public override bool IsInvalid
		{
			get
			{
				return base.IsClosed || this._handle.IsZero;
			}
		}

		// Token: 0x060033E4 RID: 13284 RVA: 0x000BC8E4 File Offset: 0x000BAAE4
		public static int AcquireCredentialsHandle(string package, global::Interop.SspiCli.CredentialUse intent, ref global::Interop.SspiCli.SEC_WINNT_AUTH_IDENTITY_W authdata, out SafeFreeCredentials outCredential)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(null, package, intent, authdata, "AcquireCredentialsHandle");
			}
			outCredential = new SafeFreeCredential_SECURITY();
			long num2;
			int num = global::Interop.SspiCli.AcquireCredentialsHandleW(null, package, (int)intent, null, ref authdata, null, null, ref outCredential._handle, out num2);
			if (num != 0)
			{
				outCredential.SetHandleAsInvalid();
			}
			return num;
		}

		// Token: 0x060033E5 RID: 13285 RVA: 0x000BC940 File Offset: 0x000BAB40
		public static int AcquireDefaultCredential(string package, global::Interop.SspiCli.CredentialUse intent, out SafeFreeCredentials outCredential)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(null, package, intent, "AcquireDefaultCredential");
			}
			outCredential = new SafeFreeCredential_SECURITY();
			long num2;
			int num = global::Interop.SspiCli.AcquireCredentialsHandleW(null, package, (int)intent, null, IntPtr.Zero, null, null, ref outCredential._handle, out num2);
			if (num != 0)
			{
				outCredential.SetHandleAsInvalid();
			}
			return num;
		}

		// Token: 0x060033E6 RID: 13286 RVA: 0x000BC994 File Offset: 0x000BAB94
		public static int AcquireCredentialsHandle(string package, global::Interop.SspiCli.CredentialUse intent, ref SafeSspiAuthDataHandle authdata, out SafeFreeCredentials outCredential)
		{
			outCredential = new SafeFreeCredential_SECURITY();
			long num2;
			int num = global::Interop.SspiCli.AcquireCredentialsHandleW(null, package, (int)intent, null, authdata, null, null, ref outCredential._handle, out num2);
			if (num != 0)
			{
				outCredential.SetHandleAsInvalid();
			}
			return num;
		}

		// Token: 0x060033E7 RID: 13287 RVA: 0x000BC9CC File Offset: 0x000BABCC
		public unsafe static int AcquireCredentialsHandle(string package, global::Interop.SspiCli.CredentialUse intent, ref global::Interop.SspiCli.SCHANNEL_CRED authdata, out SafeFreeCredentials outCredential)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(null, package, intent, authdata, "AcquireCredentialsHandle");
			}
			int num = -1;
			IntPtr paCred = authdata.paCred;
			try
			{
				IntPtr intPtr = new IntPtr((void*)(&paCred));
				if (paCred != IntPtr.Zero)
				{
					authdata.paCred = intPtr;
				}
				outCredential = new SafeFreeCredential_SECURITY();
				long num2;
				num = global::Interop.SspiCli.AcquireCredentialsHandleW(null, package, (int)intent, null, ref authdata, null, null, ref outCredential._handle, out num2);
			}
			finally
			{
				authdata.paCred = paCred;
			}
			if (num != 0)
			{
				outCredential.SetHandleAsInvalid();
			}
			return num;
		}

		// Token: 0x04001F8D RID: 8077
		internal global::Interop.SspiCli.CredHandle _handle;
	}
}
