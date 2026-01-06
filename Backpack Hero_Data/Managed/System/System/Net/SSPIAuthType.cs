using System;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000361 RID: 865
	internal class SSPIAuthType : SSPIInterface
	{
		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001C96 RID: 7318 RVA: 0x00067864 File Offset: 0x00065A64
		// (set) Token: 0x06001C97 RID: 7319 RVA: 0x0006786D File Offset: 0x00065A6D
		public SecurityPackageInfoClass[] SecurityPackages
		{
			get
			{
				return SSPIAuthType.s_securityPackages;
			}
			set
			{
				SSPIAuthType.s_securityPackages = value;
			}
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x00067877 File Offset: 0x00065A77
		public int EnumerateSecurityPackages(out int pkgnum, out SafeFreeContextBuffer pkgArray)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, null, "EnumerateSecurityPackages");
			}
			return SafeFreeContextBuffer.EnumeratePackages(out pkgnum, out pkgArray);
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x00067893 File Offset: 0x00065A93
		public int AcquireCredentialsHandle(string moduleName, global::Interop.SspiCli.CredentialUse usage, ref global::Interop.SspiCli.SEC_WINNT_AUTH_IDENTITY_W authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x0006789F File Offset: 0x00065A9F
		public int AcquireCredentialsHandle(string moduleName, global::Interop.SspiCli.CredentialUse usage, ref SafeSspiAuthDataHandle authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x000678AB File Offset: 0x00065AAB
		public int AcquireDefaultCredential(string moduleName, global::Interop.SspiCli.CredentialUse usage, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireDefaultCredential(moduleName, usage, out outCredential);
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x000678B5 File Offset: 0x00065AB5
		public int AcquireCredentialsHandle(string moduleName, global::Interop.SspiCli.CredentialUse usage, ref global::Interop.SspiCli.SCHANNEL_CRED authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x000678C1 File Offset: 0x00065AC1
		public int AcceptSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer inputBuffer, global::Interop.SspiCli.ContextFlags inFlags, global::Interop.SspiCli.Endianness endianness, SecurityBuffer outputBuffer, ref global::Interop.SspiCli.ContextFlags outFlags)
		{
			return SafeDeleteContext.AcceptSecurityContext(ref credential, ref context, inFlags, endianness, inputBuffer, null, outputBuffer, ref outFlags);
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x000678D4 File Offset: 0x00065AD4
		public int AcceptSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer[] inputBuffers, global::Interop.SspiCli.ContextFlags inFlags, global::Interop.SspiCli.Endianness endianness, SecurityBuffer outputBuffer, ref global::Interop.SspiCli.ContextFlags outFlags)
		{
			return SafeDeleteContext.AcceptSecurityContext(ref credential, ref context, inFlags, endianness, null, inputBuffers, outputBuffer, ref outFlags);
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x000678E8 File Offset: 0x00065AE8
		public int InitializeSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, global::Interop.SspiCli.ContextFlags inFlags, global::Interop.SspiCli.Endianness endianness, SecurityBuffer inputBuffer, SecurityBuffer outputBuffer, ref global::Interop.SspiCli.ContextFlags outFlags)
		{
			return SafeDeleteContext.InitializeSecurityContext(ref credential, ref context, targetName, inFlags, endianness, inputBuffer, null, outputBuffer, ref outFlags);
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x00067908 File Offset: 0x00065B08
		public int InitializeSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, global::Interop.SspiCli.ContextFlags inFlags, global::Interop.SspiCli.Endianness endianness, SecurityBuffer[] inputBuffers, SecurityBuffer outputBuffer, ref global::Interop.SspiCli.ContextFlags outFlags)
		{
			return SafeDeleteContext.InitializeSecurityContext(ref credential, ref context, targetName, inFlags, endianness, null, inputBuffers, outputBuffer, ref outFlags);
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x0006792C File Offset: 0x00065B2C
		public int EncryptMessage(SafeDeleteContext context, ref global::Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber)
		{
			int num;
			try
			{
				bool flag = false;
				context.DangerousAddRef(ref flag);
				num = global::Interop.SspiCli.EncryptMessage(ref context._handle, 0U, ref inputOutput, sequenceNumber);
			}
			finally
			{
				context.DangerousRelease();
			}
			return num;
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x0006796C File Offset: 0x00065B6C
		public unsafe int DecryptMessage(SafeDeleteContext context, ref global::Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber)
		{
			int num = -2146893055;
			uint num2 = 0U;
			try
			{
				bool flag = false;
				context.DangerousAddRef(ref flag);
				num = global::Interop.SspiCli.DecryptMessage(ref context._handle, ref inputOutput, sequenceNumber, &num2);
			}
			finally
			{
				context.DangerousRelease();
			}
			if (num == 0 && num2 == 2147483649U)
			{
				NetEventSource.Fail(this, FormattableStringFactory.Create("Expected qop = 0, returned value = {0}", new object[] { num2 }), "DecryptMessage");
				throw new InvalidOperationException("Protocol error: A received message contains a valid signature but it was not encrypted as required by the effective Protection Level.");
			}
			return num;
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x000679F0 File Offset: 0x00065BF0
		public int MakeSignature(SafeDeleteContext context, ref global::Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber)
		{
			int num;
			try
			{
				bool flag = false;
				context.DangerousAddRef(ref flag);
				num = global::Interop.SspiCli.EncryptMessage(ref context._handle, 2147483649U, ref inputOutput, sequenceNumber);
			}
			finally
			{
				context.DangerousRelease();
			}
			return num;
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x00067A34 File Offset: 0x00065C34
		public unsafe int VerifySignature(SafeDeleteContext context, ref global::Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber)
		{
			int num2;
			try
			{
				bool flag = false;
				uint num = 0U;
				context.DangerousAddRef(ref flag);
				num2 = global::Interop.SspiCli.DecryptMessage(ref context._handle, ref inputOutput, sequenceNumber, &num);
			}
			finally
			{
				context.DangerousRelease();
			}
			return num2;
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x00067A78 File Offset: 0x00065C78
		public int QueryContextChannelBinding(SafeDeleteContext context, global::Interop.SspiCli.ContextAttribute attribute, out SafeFreeContextBufferChannelBinding binding)
		{
			binding = null;
			throw new NotSupportedException();
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x00067A84 File Offset: 0x00065C84
		public unsafe int QueryContextAttributes(SafeDeleteContext context, global::Interop.SspiCli.ContextAttribute attribute, byte[] buffer, Type handleType, out SafeHandle refHandle)
		{
			refHandle = null;
			if (handleType != null)
			{
				if (handleType == typeof(SafeFreeContextBuffer))
				{
					refHandle = SafeFreeContextBuffer.CreateEmptyHandle();
				}
				else
				{
					if (!(handleType == typeof(SafeFreeCertContext)))
					{
						throw new ArgumentException(SR.Format("'{0}' is not a supported handle type.", handleType.FullName), "handleType");
					}
					refHandle = new SafeFreeCertContext();
				}
			}
			byte* ptr;
			if (buffer == null || buffer.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &buffer[0];
			}
			return SafeFreeContextBuffer.QueryContextAttributes(context, attribute, ptr, refHandle);
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x00067B18 File Offset: 0x00065D18
		public int SetContextAttributes(SafeDeleteContext context, global::Interop.SspiCli.ContextAttribute attribute, byte[] buffer)
		{
			throw global::System.NotImplemented.ByDesignWithMessage("This method is not implemented by this class.");
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x00067B24 File Offset: 0x00065D24
		public int QuerySecurityContextToken(SafeDeleteContext phContext, out SecurityContextTokenHandle phToken)
		{
			return SSPIAuthType.GetSecurityContextToken(phContext, out phToken);
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x00067B2D File Offset: 0x00065D2D
		public int CompleteAuthToken(ref SafeDeleteContext refContext, SecurityBuffer[] inputBuffers)
		{
			return SafeDeleteContext.CompleteAuthToken(ref refContext, inputBuffers);
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x00067B38 File Offset: 0x00065D38
		private static int GetSecurityContextToken(SafeDeleteContext phContext, out SecurityContextTokenHandle safeHandle)
		{
			safeHandle = null;
			int num;
			try
			{
				bool flag = false;
				phContext.DangerousAddRef(ref flag);
				num = global::Interop.SspiCli.QuerySecurityContextToken(ref phContext._handle, out safeHandle);
			}
			finally
			{
				phContext.DangerousRelease();
			}
			return num;
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x000044FA File Offset: 0x000026FA
		public int ApplyControlToken(ref SafeDeleteContext refContext, SecurityBuffer[] inputBuffers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000E97 RID: 3735
		private static volatile SecurityPackageInfoClass[] s_securityPackages;
	}
}
