using System;
using System.ComponentModel;
using System.Globalization;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000364 RID: 868
	internal static class SSPIWrapper
	{
		// Token: 0x06001CD8 RID: 7384 RVA: 0x00067D28 File Offset: 0x00065F28
		internal static SecurityPackageInfoClass[] EnumerateSecurityPackages(SSPIInterface secModule)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(null, null, "EnumerateSecurityPackages");
			}
			if (secModule.SecurityPackages == null)
			{
				lock (secModule)
				{
					if (secModule.SecurityPackages == null)
					{
						int num = 0;
						SafeFreeContextBuffer safeFreeContextBuffer = null;
						try
						{
							int num2 = secModule.EnumerateSecurityPackages(out num, out safeFreeContextBuffer);
							if (NetEventSource.IsEnabled)
							{
								NetEventSource.Info(null, FormattableStringFactory.Create("arrayBase: {0}", new object[] { safeFreeContextBuffer }), "EnumerateSecurityPackages");
							}
							if (num2 != 0)
							{
								throw new Win32Exception(num2);
							}
							SecurityPackageInfoClass[] array = new SecurityPackageInfoClass[num];
							for (int i = 0; i < num; i++)
							{
								array[i] = new SecurityPackageInfoClass(safeFreeContextBuffer, i);
								if (NetEventSource.IsEnabled)
								{
									NetEventSource.Log.EnumerateSecurityPackages(array[i].Name);
								}
							}
							secModule.SecurityPackages = array;
						}
						finally
						{
							if (safeFreeContextBuffer != null)
							{
								safeFreeContextBuffer.Dispose();
							}
						}
					}
				}
			}
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Exit(null, null, "EnumerateSecurityPackages");
			}
			return secModule.SecurityPackages;
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x00067E44 File Offset: 0x00066044
		internal static SecurityPackageInfoClass GetVerifyPackageInfo(SSPIInterface secModule, string packageName)
		{
			return SSPIWrapper.GetVerifyPackageInfo(secModule, packageName, false);
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x00067E50 File Offset: 0x00066050
		internal static SecurityPackageInfoClass GetVerifyPackageInfo(SSPIInterface secModule, string packageName, bool throwIfMissing)
		{
			SecurityPackageInfoClass[] array = SSPIWrapper.EnumerateSecurityPackages(secModule);
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (string.Compare(array[i].Name, packageName, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return array[i];
					}
				}
			}
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.SspiPackageNotFound(packageName);
			}
			if (throwIfMissing)
			{
				throw new NotSupportedException("The requested security package is not supported.");
			}
			return null;
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x00067EAC File Offset: 0x000660AC
		public static SafeFreeCredentials AcquireDefaultCredential(SSPIInterface secModule, string package, global::Interop.SspiCli.CredentialUse intent)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(null, package, "AcquireDefaultCredential");
				NetEventSource.Log.AcquireDefaultCredential(package, intent);
			}
			SafeFreeCredentials safeFreeCredentials = null;
			int num = secModule.AcquireDefaultCredential(package, intent, out safeFreeCredentials);
			if (num != 0)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Error(null, SR.Format("{0} failed with error {1}.", "AcquireDefaultCredential", string.Format("0x{0:X}", num)), "AcquireDefaultCredential");
				}
				throw new Win32Exception(num);
			}
			return safeFreeCredentials;
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x00067F24 File Offset: 0x00066124
		public static SafeFreeCredentials AcquireCredentialsHandle(SSPIInterface secModule, string package, global::Interop.SspiCli.CredentialUse intent, ref global::Interop.SspiCli.SEC_WINNT_AUTH_IDENTITY_W authdata)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(null, package, "AcquireCredentialsHandle");
				NetEventSource.Log.AcquireCredentialsHandle(package, intent, authdata);
			}
			SafeFreeCredentials safeFreeCredentials = null;
			int num = secModule.AcquireCredentialsHandle(package, intent, ref authdata, out safeFreeCredentials);
			if (num != 0)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Error(null, SR.Format("{0} failed with error {1}.", "AcquireCredentialsHandle", string.Format("0x{0:X}", num)), "AcquireCredentialsHandle");
				}
				throw new Win32Exception(num);
			}
			return safeFreeCredentials;
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x00067FA8 File Offset: 0x000661A8
		public static SafeFreeCredentials AcquireCredentialsHandle(SSPIInterface secModule, string package, global::Interop.SspiCli.CredentialUse intent, ref SafeSspiAuthDataHandle authdata)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.AcquireCredentialsHandle(package, intent, authdata);
			}
			SafeFreeCredentials safeFreeCredentials = null;
			int num = secModule.AcquireCredentialsHandle(package, intent, ref authdata, out safeFreeCredentials);
			if (num != 0)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Error(null, SR.Format("{0} failed with error {1}.", "AcquireCredentialsHandle", string.Format("0x{0:X}", num)), "AcquireCredentialsHandle");
				}
				throw new Win32Exception(num);
			}
			return safeFreeCredentials;
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x00068014 File Offset: 0x00066214
		public static SafeFreeCredentials AcquireCredentialsHandle(SSPIInterface secModule, string package, global::Interop.SspiCli.CredentialUse intent, global::Interop.SspiCli.SCHANNEL_CRED scc)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(null, package, "AcquireCredentialsHandle");
				NetEventSource.Log.AcquireCredentialsHandle(package, intent, scc);
			}
			SafeFreeCredentials safeFreeCredentials = null;
			int num = secModule.AcquireCredentialsHandle(package, intent, ref scc, out safeFreeCredentials);
			if (num != 0)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Error(null, SR.Format("{0} failed with error {1}.", "AcquireCredentialsHandle", string.Format("0x{0:X}", num)), "AcquireCredentialsHandle");
				}
				throw new Win32Exception(num);
			}
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Exit(null, safeFreeCredentials, "AcquireCredentialsHandle");
			}
			return safeFreeCredentials;
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x000680A4 File Offset: 0x000662A4
		internal static int InitializeSecurityContext(SSPIInterface secModule, ref SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, global::Interop.SspiCli.ContextFlags inFlags, global::Interop.SspiCli.Endianness datarep, SecurityBuffer inputBuffer, SecurityBuffer outputBuffer, ref global::Interop.SspiCli.ContextFlags outFlags)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.InitializeSecurityContext(credential, context, targetName, inFlags);
			}
			int num = secModule.InitializeSecurityContext(ref credential, ref context, targetName, inFlags, datarep, inputBuffer, outputBuffer, ref outFlags);
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.SecurityContextInputBuffer("InitializeSecurityContext", (inputBuffer != null) ? inputBuffer.size : 0, outputBuffer.size, (global::Interop.SECURITY_STATUS)num);
			}
			return num;
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x0006810C File Offset: 0x0006630C
		internal static int InitializeSecurityContext(SSPIInterface secModule, SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, global::Interop.SspiCli.ContextFlags inFlags, global::Interop.SspiCli.Endianness datarep, SecurityBuffer[] inputBuffers, SecurityBuffer outputBuffer, ref global::Interop.SspiCli.ContextFlags outFlags)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.InitializeSecurityContext(credential, context, targetName, inFlags);
			}
			int num = secModule.InitializeSecurityContext(credential, ref context, targetName, inFlags, datarep, inputBuffers, outputBuffer, ref outFlags);
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.SecurityContextInputBuffers("InitializeSecurityContext", (inputBuffers != null) ? inputBuffers.Length : 0, outputBuffer.size, (global::Interop.SECURITY_STATUS)num);
			}
			return num;
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x00068170 File Offset: 0x00066370
		internal static int AcceptSecurityContext(SSPIInterface secModule, ref SafeFreeCredentials credential, ref SafeDeleteContext context, global::Interop.SspiCli.ContextFlags inFlags, global::Interop.SspiCli.Endianness datarep, SecurityBuffer inputBuffer, SecurityBuffer outputBuffer, ref global::Interop.SspiCli.ContextFlags outFlags)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.AcceptSecurityContext(credential, context, inFlags);
			}
			int num = secModule.AcceptSecurityContext(ref credential, ref context, inputBuffer, inFlags, datarep, outputBuffer, ref outFlags);
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.SecurityContextInputBuffer("AcceptSecurityContext", (inputBuffer != null) ? inputBuffer.size : 0, outputBuffer.size, (global::Interop.SECURITY_STATUS)num);
			}
			return num;
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x000681D4 File Offset: 0x000663D4
		internal static int AcceptSecurityContext(SSPIInterface secModule, SafeFreeCredentials credential, ref SafeDeleteContext context, global::Interop.SspiCli.ContextFlags inFlags, global::Interop.SspiCli.Endianness datarep, SecurityBuffer[] inputBuffers, SecurityBuffer outputBuffer, ref global::Interop.SspiCli.ContextFlags outFlags)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.AcceptSecurityContext(credential, context, inFlags);
			}
			int num = secModule.AcceptSecurityContext(credential, ref context, inputBuffers, inFlags, datarep, outputBuffer, ref outFlags);
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.SecurityContextInputBuffers("AcceptSecurityContext", (inputBuffers != null) ? inputBuffers.Length : 0, outputBuffer.size, (global::Interop.SECURITY_STATUS)num);
			}
			return num;
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x00068234 File Offset: 0x00066434
		internal static int CompleteAuthToken(SSPIInterface secModule, ref SafeDeleteContext context, SecurityBuffer[] inputBuffers)
		{
			int num = secModule.CompleteAuthToken(ref context, inputBuffers);
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.OperationReturnedSomething("CompleteAuthToken", (global::Interop.SECURITY_STATUS)num);
			}
			return num;
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x00068264 File Offset: 0x00066464
		internal static int ApplyControlToken(SSPIInterface secModule, ref SafeDeleteContext context, SecurityBuffer[] inputBuffers)
		{
			int num = secModule.ApplyControlToken(ref context, inputBuffers);
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.OperationReturnedSomething("ApplyControlToken", (global::Interop.SECURITY_STATUS)num);
			}
			return num;
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x00068292 File Offset: 0x00066492
		public static int QuerySecurityContextToken(SSPIInterface secModule, SafeDeleteContext context, out SecurityContextTokenHandle token)
		{
			return secModule.QuerySecurityContextToken(context, out token);
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x0006829C File Offset: 0x0006649C
		public static int EncryptMessage(SSPIInterface secModule, SafeDeleteContext context, SecurityBuffer[] input, uint sequenceNumber)
		{
			return SSPIWrapper.EncryptDecryptHelper(SSPIWrapper.OP.Encrypt, secModule, context, input, sequenceNumber);
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x000682A8 File Offset: 0x000664A8
		public static int DecryptMessage(SSPIInterface secModule, SafeDeleteContext context, SecurityBuffer[] input, uint sequenceNumber)
		{
			return SSPIWrapper.EncryptDecryptHelper(SSPIWrapper.OP.Decrypt, secModule, context, input, sequenceNumber);
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x000682B4 File Offset: 0x000664B4
		internal static int MakeSignature(SSPIInterface secModule, SafeDeleteContext context, SecurityBuffer[] input, uint sequenceNumber)
		{
			return SSPIWrapper.EncryptDecryptHelper(SSPIWrapper.OP.MakeSignature, secModule, context, input, sequenceNumber);
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x000682C0 File Offset: 0x000664C0
		public static int VerifySignature(SSPIInterface secModule, SafeDeleteContext context, SecurityBuffer[] input, uint sequenceNumber)
		{
			return SSPIWrapper.EncryptDecryptHelper(SSPIWrapper.OP.VerifySignature, secModule, context, input, sequenceNumber);
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x000682CC File Offset: 0x000664CC
		private unsafe static int EncryptDecryptHelper(SSPIWrapper.OP op, SSPIInterface secModule, SafeDeleteContext context, SecurityBuffer[] input, uint sequenceNumber)
		{
			global::Interop.SspiCli.SecBufferDesc secBufferDesc = new global::Interop.SspiCli.SecBufferDesc(input.Length);
			global::Interop.SspiCli.SecBuffer[] array = new global::Interop.SspiCli.SecBuffer[input.Length];
			global::Interop.SspiCli.SecBuffer[] array2;
			global::Interop.SspiCli.SecBuffer* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			secBufferDesc.pBuffers = (void*)ptr;
			GCHandle[] array3 = new GCHandle[input.Length];
			byte[][] array4 = new byte[input.Length][];
			int num2;
			try
			{
				for (int i = 0; i < input.Length; i++)
				{
					SecurityBuffer securityBuffer = input[i];
					array[i].cbBuffer = securityBuffer.size;
					array[i].BufferType = securityBuffer.type;
					if (securityBuffer.token == null || securityBuffer.token.Length == 0)
					{
						array[i].pvBuffer = IntPtr.Zero;
					}
					else
					{
						array3[i] = GCHandle.Alloc(securityBuffer.token, GCHandleType.Pinned);
						array[i].pvBuffer = Marshal.UnsafeAddrOfPinnedArrayElement<byte>(securityBuffer.token, securityBuffer.offset);
						array4[i] = securityBuffer.token;
					}
				}
				int num;
				switch (op)
				{
				case SSPIWrapper.OP.Encrypt:
					num = secModule.EncryptMessage(context, ref secBufferDesc, sequenceNumber);
					break;
				case SSPIWrapper.OP.Decrypt:
					num = secModule.DecryptMessage(context, ref secBufferDesc, sequenceNumber);
					break;
				case SSPIWrapper.OP.MakeSignature:
					num = secModule.MakeSignature(context, ref secBufferDesc, sequenceNumber);
					break;
				case SSPIWrapper.OP.VerifySignature:
					num = secModule.VerifySignature(context, ref secBufferDesc, sequenceNumber);
					break;
				default:
					NetEventSource.Fail(null, FormattableStringFactory.Create("Unknown OP: {0}", new object[] { op }), "EncryptDecryptHelper");
					throw global::System.NotImplemented.ByDesignWithMessage("This method is not implemented by this class.");
				}
				for (int j = 0; j < input.Length; j++)
				{
					SecurityBuffer securityBuffer2 = input[j];
					securityBuffer2.size = array[j].cbBuffer;
					securityBuffer2.type = array[j].BufferType;
					checked
					{
						if (securityBuffer2.size == 0)
						{
							securityBuffer2.offset = 0;
							securityBuffer2.token = null;
						}
						else
						{
							int k;
							for (k = 0; k < input.Length; k++)
							{
								if (array4[k] != null)
								{
									byte* ptr2 = (byte*)(void*)Marshal.UnsafeAddrOfPinnedArrayElement<byte>(array4[k], 0);
									if ((void*)array[j].pvBuffer >= (void*)ptr2 && (byte*)(void*)array[j].pvBuffer + securityBuffer2.size == ptr2 + array4[k].Length)
									{
										securityBuffer2.offset = (int)(unchecked((long)((byte*)(void*)array[j].pvBuffer - (byte*)ptr2)));
										securityBuffer2.token = array4[k];
										break;
									}
								}
							}
							if (k >= input.Length)
							{
								NetEventSource.Fail(null, "Output buffer out of range.", "EncryptDecryptHelper");
								securityBuffer2.size = 0;
								securityBuffer2.offset = 0;
								securityBuffer2.token = null;
							}
						}
						if (securityBuffer2.offset < 0 || securityBuffer2.offset > ((securityBuffer2.token == null) ? 0 : securityBuffer2.token.Length))
						{
							NetEventSource.Fail(null, FormattableStringFactory.Create("'offset' out of range.  [{0}]", new object[] { securityBuffer2.offset }), "EncryptDecryptHelper");
						}
					}
					if (securityBuffer2.size < 0 || securityBuffer2.size > ((securityBuffer2.token == null) ? 0 : (securityBuffer2.token.Length - securityBuffer2.offset)))
					{
						NetEventSource.Fail(null, FormattableStringFactory.Create("'size' out of range.  [{0}]", new object[] { securityBuffer2.size }), "EncryptDecryptHelper");
					}
				}
				if (NetEventSource.IsEnabled && num != 0)
				{
					if (num == 590625)
					{
						NetEventSource.Error(null, SR.Format("{0} returned {1}.", op, "SEC_I_RENEGOTIATE"), "EncryptDecryptHelper");
					}
					else
					{
						NetEventSource.Error(null, SR.Format("{0} failed with error {1}.", op, string.Format("0x{0:X}", 0)), "EncryptDecryptHelper");
					}
				}
				num2 = num;
			}
			finally
			{
				for (int l = 0; l < array3.Length; l++)
				{
					if (array3[l].IsAllocated)
					{
						array3[l].Free();
					}
				}
			}
			return num2;
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x000686E8 File Offset: 0x000668E8
		public static SafeFreeContextBufferChannelBinding QueryContextChannelBinding(SSPIInterface secModule, SafeDeleteContext securityContext, global::Interop.SspiCli.ContextAttribute contextAttribute)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(null, contextAttribute, "QueryContextChannelBinding");
			}
			SafeFreeContextBufferChannelBinding safeFreeContextBufferChannelBinding;
			int num = secModule.QueryContextChannelBinding(securityContext, contextAttribute, out safeFreeContextBufferChannelBinding);
			if (num != 0)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Exit(null, FormattableStringFactory.Create("ERROR = {0}", new object[] { SSPIWrapper.ErrorDescription(num) }), "QueryContextChannelBinding");
				}
				return null;
			}
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Exit(null, safeFreeContextBufferChannelBinding, "QueryContextChannelBinding");
			}
			return safeFreeContextBufferChannelBinding;
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x0006875C File Offset: 0x0006695C
		public static object QueryContextAttributes(SSPIInterface secModule, SafeDeleteContext securityContext, global::Interop.SspiCli.ContextAttribute contextAttribute)
		{
			int num;
			return SSPIWrapper.QueryContextAttributes(secModule, securityContext, contextAttribute, out num);
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x00068774 File Offset: 0x00066974
		public unsafe static object QueryContextAttributes(SSPIInterface secModule, SafeDeleteContext securityContext, global::Interop.SspiCli.ContextAttribute contextAttribute, out int errorCode)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(null, contextAttribute, "QueryContextAttributes");
			}
			int num = IntPtr.Size;
			Type type = null;
			if (contextAttribute <= global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_CLIENT_SPECIFIED_TARGET)
			{
				if (contextAttribute <= global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_PACKAGE_INFO)
				{
					switch (contextAttribute)
					{
					case global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_SIZES:
						num = SecPkgContext_Sizes.SizeOf;
						goto IL_0136;
					case global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_NAMES:
						type = typeof(SafeFreeContextBuffer);
						goto IL_0136;
					case global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_LIFESPAN:
					case global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_DCE_INFO:
						break;
					case global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_STREAM_SIZES:
						num = SecPkgContext_StreamSizes.SizeOf;
						goto IL_0136;
					default:
						if (contextAttribute == global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_PACKAGE_INFO)
						{
							type = typeof(SafeFreeContextBuffer);
							goto IL_0136;
						}
						break;
					}
				}
				else
				{
					if (contextAttribute == global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_NEGOTIATION_INFO)
					{
						type = typeof(SafeFreeContextBuffer);
						num = sizeof(SecPkgContext_NegotiationInfoW);
						goto IL_0136;
					}
					if (contextAttribute == global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_CLIENT_SPECIFIED_TARGET)
					{
						type = typeof(SafeFreeContextBuffer);
						goto IL_0136;
					}
				}
			}
			else if (contextAttribute <= global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_REMOTE_CERT_CONTEXT)
			{
				if (contextAttribute == global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_APPLICATION_PROTOCOL)
				{
					num = Marshal.SizeOf<global::Interop.SecPkgContext_ApplicationProtocol>();
					goto IL_0136;
				}
				if (contextAttribute == global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_REMOTE_CERT_CONTEXT)
				{
					type = typeof(SafeFreeCertContext);
					goto IL_0136;
				}
			}
			else
			{
				if (contextAttribute == global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_LOCAL_CERT_CONTEXT)
				{
					type = typeof(SafeFreeCertContext);
					goto IL_0136;
				}
				if (contextAttribute == global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_ISSUER_LIST_EX)
				{
					num = Marshal.SizeOf<global::Interop.SspiCli.SecPkgContext_IssuerListInfoEx>();
					type = typeof(SafeFreeContextBuffer);
					goto IL_0136;
				}
				if (contextAttribute == global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_CONNECTION_INFO)
				{
					num = Marshal.SizeOf<SecPkgContext_ConnectionInfo>();
					goto IL_0136;
				}
			}
			throw new ArgumentException(SR.Format("The specified value is not valid in the '{0}' enumeration.", "contextAttribute"), "contextAttribute");
			IL_0136:
			SafeHandle safeHandle = null;
			object obj = null;
			try
			{
				byte[] array = new byte[num];
				errorCode = secModule.QueryContextAttributes(securityContext, contextAttribute, array, type, out safeHandle);
				if (errorCode != 0)
				{
					if (NetEventSource.IsEnabled)
					{
						NetEventSource.Exit(null, FormattableStringFactory.Create("ERROR = {0}", new object[] { SSPIWrapper.ErrorDescription(errorCode) }), "QueryContextAttributes");
					}
					return null;
				}
				if (contextAttribute <= global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_CLIENT_SPECIFIED_TARGET)
				{
					if (contextAttribute <= global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_PACKAGE_INFO)
					{
						switch (contextAttribute)
						{
						case global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_SIZES:
							obj = new SecPkgContext_Sizes(array);
							break;
						case global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_NAMES:
							obj = Marshal.PtrToStringUni(safeHandle.DangerousGetHandle());
							break;
						case global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_LIFESPAN:
						case global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_DCE_INFO:
							break;
						case global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_STREAM_SIZES:
							obj = new SecPkgContext_StreamSizes(array);
							break;
						default:
							if (contextAttribute == global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_PACKAGE_INFO)
							{
								obj = new SecurityPackageInfoClass(safeHandle, 0);
							}
							break;
						}
					}
					else
					{
						if (contextAttribute != global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_NEGOTIATION_INFO)
						{
							if (contextAttribute != global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_CLIENT_SPECIFIED_TARGET)
							{
								goto IL_02B6;
							}
						}
						else
						{
							try
							{
								fixed (byte* ptr = &array[0])
								{
									void* ptr2 = (void*)ptr;
									obj = new NegotiationInfoClass(safeHandle, (int)((SecPkgContext_NegotiationInfoW*)ptr2)->NegotiationState);
									goto IL_02C2;
								}
							}
							finally
							{
								byte* ptr = null;
							}
						}
						obj = Marshal.PtrToStringUni(safeHandle.DangerousGetHandle());
					}
				}
				else if (contextAttribute <= global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_LOCAL_CERT_CONTEXT)
				{
					if (contextAttribute != global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_APPLICATION_PROTOCOL)
					{
						if (contextAttribute - global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_REMOTE_CERT_CONTEXT <= 1)
						{
							obj = safeHandle;
							safeHandle = null;
						}
					}
					else
					{
						try
						{
							byte[] array2;
							void* ptr3;
							if ((array2 = array) == null || array2.Length == 0)
							{
								ptr3 = null;
							}
							else
							{
								ptr3 = (void*)(&array2[0]);
							}
							obj = Marshal.PtrToStructure<global::Interop.SecPkgContext_ApplicationProtocol>(new IntPtr(ptr3));
						}
						finally
						{
							byte[] array2 = null;
						}
					}
				}
				else if (contextAttribute != global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_ISSUER_LIST_EX)
				{
					if (contextAttribute == global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_CONNECTION_INFO)
					{
						obj = new SecPkgContext_ConnectionInfo(array);
					}
				}
				else
				{
					obj = new global::Interop.SspiCli.SecPkgContext_IssuerListInfoEx(safeHandle, array);
					safeHandle = null;
				}
				IL_02B6:;
			}
			finally
			{
				if (safeHandle != null)
				{
					safeHandle.Dispose();
				}
			}
			IL_02C2:
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Exit(null, obj, "QueryContextAttributes");
			}
			return obj;
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x00068AA8 File Offset: 0x00066CA8
		public static string ErrorDescription(int errorCode)
		{
			if (errorCode == -1)
			{
				return "An exception when invoking Win32 API";
			}
			global::Interop.SECURITY_STATUS security_STATUS = (global::Interop.SECURITY_STATUS)errorCode;
			if (security_STATUS <= global::Interop.SECURITY_STATUS.MessageAltered)
			{
				switch (security_STATUS)
				{
				case global::Interop.SECURITY_STATUS.InvalidHandle:
					return "Invalid handle";
				case global::Interop.SECURITY_STATUS.Unsupported:
				case global::Interop.SECURITY_STATUS.InternalError:
					break;
				case global::Interop.SECURITY_STATUS.TargetUnknown:
					return "Target unknown";
				case global::Interop.SECURITY_STATUS.PackageNotFound:
					return "Package not found";
				default:
					if (security_STATUS == global::Interop.SECURITY_STATUS.InvalidToken)
					{
						return "Invalid token";
					}
					if (security_STATUS == global::Interop.SECURITY_STATUS.MessageAltered)
					{
						return "Message altered";
					}
					break;
				}
			}
			else
			{
				if (security_STATUS == global::Interop.SECURITY_STATUS.IncompleteMessage)
				{
					return "Message incomplete";
				}
				switch (security_STATUS)
				{
				case global::Interop.SECURITY_STATUS.BufferNotEnough:
					return "Buffer not enough";
				case global::Interop.SECURITY_STATUS.WrongPrincipal:
					return "Wrong principal";
				case (global::Interop.SECURITY_STATUS)(-2146893021):
				case global::Interop.SECURITY_STATUS.TimeSkew:
					break;
				case global::Interop.SECURITY_STATUS.UntrustedRoot:
					return "Untrusted root";
				default:
					if (security_STATUS == global::Interop.SECURITY_STATUS.ContinueNeeded)
					{
						return "Continue needed";
					}
					break;
				}
			}
			return "0x" + errorCode.ToString("x", NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x02000365 RID: 869
		private enum OP
		{
			// Token: 0x04000E9A RID: 3738
			Encrypt = 1,
			// Token: 0x04000E9B RID: 3739
			Decrypt,
			// Token: 0x04000E9C RID: 3740
			MakeSignature,
			// Token: 0x04000E9D RID: 3741
			VerifySignature
		}
	}
}
