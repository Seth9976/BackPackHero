using System;
using System.Net.Security;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000363 RID: 867
	internal class SSPISecureChannelType : SSPIInterface
	{
		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x00067B7C File Offset: 0x00065D7C
		// (set) Token: 0x06001CC3 RID: 7363 RVA: 0x00067B85 File Offset: 0x00065D85
		public SecurityPackageInfoClass[] SecurityPackages
		{
			get
			{
				return SSPISecureChannelType.s_securityPackages;
			}
			set
			{
				SSPISecureChannelType.s_securityPackages = value;
			}
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x00067877 File Offset: 0x00065A77
		public int EnumerateSecurityPackages(out int pkgnum, out SafeFreeContextBuffer pkgArray)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, null, "EnumerateSecurityPackages");
			}
			return SafeFreeContextBuffer.EnumeratePackages(out pkgnum, out pkgArray);
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x00067893 File Offset: 0x00065A93
		public int AcquireCredentialsHandle(string moduleName, global::Interop.SspiCli.CredentialUse usage, ref global::Interop.SspiCli.SEC_WINNT_AUTH_IDENTITY_W authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x0006789F File Offset: 0x00065A9F
		public int AcquireCredentialsHandle(string moduleName, global::Interop.SspiCli.CredentialUse usage, ref SafeSspiAuthDataHandle authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x000678AB File Offset: 0x00065AAB
		public int AcquireDefaultCredential(string moduleName, global::Interop.SspiCli.CredentialUse usage, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireDefaultCredential(moduleName, usage, out outCredential);
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x000678B5 File Offset: 0x00065AB5
		public int AcquireCredentialsHandle(string moduleName, global::Interop.SspiCli.CredentialUse usage, ref global::Interop.SspiCli.SCHANNEL_CRED authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x000678C1 File Offset: 0x00065AC1
		public int AcceptSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer inputBuffer, global::Interop.SspiCli.ContextFlags inFlags, global::Interop.SspiCli.Endianness endianness, SecurityBuffer outputBuffer, ref global::Interop.SspiCli.ContextFlags outFlags)
		{
			return SafeDeleteContext.AcceptSecurityContext(ref credential, ref context, inFlags, endianness, inputBuffer, null, outputBuffer, ref outFlags);
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x000678D4 File Offset: 0x00065AD4
		public int AcceptSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer[] inputBuffers, global::Interop.SspiCli.ContextFlags inFlags, global::Interop.SspiCli.Endianness endianness, SecurityBuffer outputBuffer, ref global::Interop.SspiCli.ContextFlags outFlags)
		{
			return SafeDeleteContext.AcceptSecurityContext(ref credential, ref context, inFlags, endianness, null, inputBuffers, outputBuffer, ref outFlags);
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x00067B90 File Offset: 0x00065D90
		public int InitializeSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, global::Interop.SspiCli.ContextFlags inFlags, global::Interop.SspiCli.Endianness endianness, SecurityBuffer inputBuffer, SecurityBuffer outputBuffer, ref global::Interop.SspiCli.ContextFlags outFlags)
		{
			return SafeDeleteContext.InitializeSecurityContext(ref credential, ref context, targetName, inFlags, endianness, inputBuffer, null, outputBuffer, ref outFlags);
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x00067BB0 File Offset: 0x00065DB0
		public int InitializeSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, global::Interop.SspiCli.ContextFlags inFlags, global::Interop.SspiCli.Endianness endianness, SecurityBuffer[] inputBuffers, SecurityBuffer outputBuffer, ref global::Interop.SspiCli.ContextFlags outFlags)
		{
			return SafeDeleteContext.InitializeSecurityContext(ref credential, ref context, targetName, inFlags, endianness, null, inputBuffers, outputBuffer, ref outFlags);
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x00067BD4 File Offset: 0x00065DD4
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

		// Token: 0x06001CCE RID: 7374 RVA: 0x00067C14 File Offset: 0x00065E14
		public int DecryptMessage(SafeDeleteContext context, ref global::Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber)
		{
			int num;
			try
			{
				bool flag = false;
				context.DangerousAddRef(ref flag);
				num = global::Interop.SspiCli.DecryptMessage(ref context._handle, ref inputOutput, sequenceNumber, null);
			}
			finally
			{
				context.DangerousRelease();
			}
			return num;
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x00067B18 File Offset: 0x00065D18
		public int MakeSignature(SafeDeleteContext context, ref global::Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber)
		{
			throw global::System.NotImplemented.ByDesignWithMessage("This method is not implemented by this class.");
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x00067B18 File Offset: 0x00065D18
		public int VerifySignature(SafeDeleteContext context, ref global::Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber)
		{
			throw global::System.NotImplemented.ByDesignWithMessage("This method is not implemented by this class.");
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x00067C58 File Offset: 0x00065E58
		public unsafe int QueryContextChannelBinding(SafeDeleteContext phContext, global::Interop.SspiCli.ContextAttribute attribute, out SafeFreeContextBufferChannelBinding refHandle)
		{
			refHandle = SafeFreeContextBufferChannelBinding.CreateEmptyHandle();
			SecPkgContext_Bindings secPkgContext_Bindings = default(SecPkgContext_Bindings);
			return SafeFreeContextBufferChannelBinding.QueryContextChannelBinding(phContext, attribute, &secPkgContext_Bindings, refHandle);
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x00067C80 File Offset: 0x00065E80
		public unsafe int QueryContextAttributes(SafeDeleteContext phContext, global::Interop.SspiCli.ContextAttribute attribute, byte[] buffer, Type handleType, out SafeHandle refHandle)
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
			return SafeFreeContextBuffer.QueryContextAttributes(phContext, attribute, ptr, refHandle);
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x00067D14 File Offset: 0x00065F14
		public int SetContextAttributes(SafeDeleteContext phContext, global::Interop.SspiCli.ContextAttribute attribute, byte[] buffer)
		{
			return SafeFreeContextBuffer.SetContextAttributes(phContext, attribute, buffer);
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x000044FA File Offset: 0x000026FA
		public int QuerySecurityContextToken(SafeDeleteContext phContext, out SecurityContextTokenHandle phToken)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x000044FA File Offset: 0x000026FA
		public int CompleteAuthToken(ref SafeDeleteContext refContext, SecurityBuffer[] inputBuffers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x00067D1E File Offset: 0x00065F1E
		public int ApplyControlToken(ref SafeDeleteContext refContext, SecurityBuffer[] inputBuffers)
		{
			return SafeDeleteContext.ApplyControlToken(ref refContext, inputBuffers);
		}

		// Token: 0x04000E98 RID: 3736
		private static volatile SecurityPackageInfoClass[] s_securityPackages;
	}
}
