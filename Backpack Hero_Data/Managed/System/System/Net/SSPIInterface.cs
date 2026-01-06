using System;
using System.Net.Security;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000362 RID: 866
	internal interface SSPIInterface
	{
		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001CAD RID: 7341
		// (set) Token: 0x06001CAE RID: 7342
		SecurityPackageInfoClass[] SecurityPackages { get; set; }

		// Token: 0x06001CAF RID: 7343
		int EnumerateSecurityPackages(out int pkgnum, out SafeFreeContextBuffer pkgArray);

		// Token: 0x06001CB0 RID: 7344
		int AcquireCredentialsHandle(string moduleName, global::Interop.SspiCli.CredentialUse usage, ref global::Interop.SspiCli.SEC_WINNT_AUTH_IDENTITY_W authdata, out SafeFreeCredentials outCredential);

		// Token: 0x06001CB1 RID: 7345
		int AcquireCredentialsHandle(string moduleName, global::Interop.SspiCli.CredentialUse usage, ref SafeSspiAuthDataHandle authdata, out SafeFreeCredentials outCredential);

		// Token: 0x06001CB2 RID: 7346
		int AcquireDefaultCredential(string moduleName, global::Interop.SspiCli.CredentialUse usage, out SafeFreeCredentials outCredential);

		// Token: 0x06001CB3 RID: 7347
		int AcquireCredentialsHandle(string moduleName, global::Interop.SspiCli.CredentialUse usage, ref global::Interop.SspiCli.SCHANNEL_CRED authdata, out SafeFreeCredentials outCredential);

		// Token: 0x06001CB4 RID: 7348
		int AcceptSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer inputBuffer, global::Interop.SspiCli.ContextFlags inFlags, global::Interop.SspiCli.Endianness endianness, SecurityBuffer outputBuffer, ref global::Interop.SspiCli.ContextFlags outFlags);

		// Token: 0x06001CB5 RID: 7349
		int AcceptSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer[] inputBuffers, global::Interop.SspiCli.ContextFlags inFlags, global::Interop.SspiCli.Endianness endianness, SecurityBuffer outputBuffer, ref global::Interop.SspiCli.ContextFlags outFlags);

		// Token: 0x06001CB6 RID: 7350
		int InitializeSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, global::Interop.SspiCli.ContextFlags inFlags, global::Interop.SspiCli.Endianness endianness, SecurityBuffer inputBuffer, SecurityBuffer outputBuffer, ref global::Interop.SspiCli.ContextFlags outFlags);

		// Token: 0x06001CB7 RID: 7351
		int InitializeSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, global::Interop.SspiCli.ContextFlags inFlags, global::Interop.SspiCli.Endianness endianness, SecurityBuffer[] inputBuffers, SecurityBuffer outputBuffer, ref global::Interop.SspiCli.ContextFlags outFlags);

		// Token: 0x06001CB8 RID: 7352
		int EncryptMessage(SafeDeleteContext context, ref global::Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber);

		// Token: 0x06001CB9 RID: 7353
		int DecryptMessage(SafeDeleteContext context, ref global::Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber);

		// Token: 0x06001CBA RID: 7354
		int MakeSignature(SafeDeleteContext context, ref global::Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber);

		// Token: 0x06001CBB RID: 7355
		int VerifySignature(SafeDeleteContext context, ref global::Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber);

		// Token: 0x06001CBC RID: 7356
		int QueryContextChannelBinding(SafeDeleteContext phContext, global::Interop.SspiCli.ContextAttribute attribute, out SafeFreeContextBufferChannelBinding refHandle);

		// Token: 0x06001CBD RID: 7357
		int QueryContextAttributes(SafeDeleteContext phContext, global::Interop.SspiCli.ContextAttribute attribute, byte[] buffer, Type handleType, out SafeHandle refHandle);

		// Token: 0x06001CBE RID: 7358
		int SetContextAttributes(SafeDeleteContext phContext, global::Interop.SspiCli.ContextAttribute attribute, byte[] buffer);

		// Token: 0x06001CBF RID: 7359
		int QuerySecurityContextToken(SafeDeleteContext phContext, out SecurityContextTokenHandle phToken);

		// Token: 0x06001CC0 RID: 7360
		int CompleteAuthToken(ref SafeDeleteContext refContext, SecurityBuffer[] inputBuffers);

		// Token: 0x06001CC1 RID: 7361
		int ApplyControlToken(ref SafeDeleteContext refContext, SecurityBuffer[] inputBuffers);
	}
}
