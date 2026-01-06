using System;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x02000249 RID: 585
	internal class SNIProxy
	{
		// Token: 0x06001AB0 RID: 6832 RVA: 0x000094D4 File Offset: 0x000076D4
		public void Terminate()
		{
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x000852EC File Offset: 0x000834EC
		public uint EnableSsl(SNIHandle handle, uint options)
		{
			uint num;
			try
			{
				num = handle.EnableSsl(options);
			}
			catch (Exception ex)
			{
				num = SNICommon.ReportSNIError(SNIProviders.SSL_PROV, 31U, ex);
			}
			return num;
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x00085324 File Offset: 0x00083524
		public uint DisableSsl(SNIHandle handle)
		{
			handle.DisableSsl();
			return 0U;
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x00085330 File Offset: 0x00083530
		public void GenSspiClientContext(SspiClientContextStatus sspiClientContextStatus, byte[] receivedBuff, ref byte[] sendBuff, byte[] serverName)
		{
			SafeDeleteContext securityContext = sspiClientContextStatus.SecurityContext;
			ContextFlagsPal contextFlags = sspiClientContextStatus.ContextFlags;
			SafeFreeCredentials safeFreeCredentials = sspiClientContextStatus.CredentialsHandle;
			string text = "Negotiate";
			if (securityContext == null)
			{
				safeFreeCredentials = NegotiateStreamPal.AcquireDefaultCredential(text, false);
			}
			SecurityBuffer[] array;
			if (receivedBuff != null)
			{
				array = new SecurityBuffer[]
				{
					new SecurityBuffer(receivedBuff, SecurityBufferType.SECBUFFER_TOKEN)
				};
			}
			else
			{
				array = new SecurityBuffer[0];
			}
			SecurityBuffer securityBuffer = new SecurityBuffer(NegotiateStreamPal.QueryMaxTokenSize(text), SecurityBufferType.SECBUFFER_TOKEN);
			ContextFlagsPal contextFlagsPal = ContextFlagsPal.Delegate | ContextFlagsPal.MutualAuth | ContextFlagsPal.Confidentiality | ContextFlagsPal.Connection;
			string @string = Encoding.UTF8.GetString(serverName);
			SecurityStatusPal securityStatusPal = NegotiateStreamPal.InitializeSecurityContext(safeFreeCredentials, ref securityContext, @string, contextFlagsPal, array, securityBuffer, ref contextFlags);
			if (securityStatusPal.ErrorCode == SecurityStatusPalErrorCode.CompleteNeeded || securityStatusPal.ErrorCode == SecurityStatusPalErrorCode.CompAndContinue)
			{
				array = new SecurityBuffer[] { securityBuffer };
				securityStatusPal = NegotiateStreamPal.CompleteAuthToken(ref securityContext, array);
				securityBuffer.token = null;
			}
			sendBuff = securityBuffer.token;
			if (sendBuff == null)
			{
				sendBuff = Array.Empty<byte>();
			}
			sspiClientContextStatus.SecurityContext = securityContext;
			sspiClientContextStatus.ContextFlags = contextFlags;
			sspiClientContextStatus.CredentialsHandle = safeFreeCredentials;
			if (!SNIProxy.IsErrorStatus(securityStatusPal.ErrorCode))
			{
				return;
			}
			if (securityStatusPal.ErrorCode == SecurityStatusPalErrorCode.InternalError)
			{
				throw new InvalidOperationException(SQLMessage.KerberosTicketMissingError() + "\n" + securityStatusPal.ToString());
			}
			throw new InvalidOperationException(SQLMessage.SSPIGenerateError() + "\n" + securityStatusPal.ToString());
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x00085476 File Offset: 0x00083676
		private static bool IsErrorStatus(SecurityStatusPalErrorCode errorCode)
		{
			return errorCode != SecurityStatusPalErrorCode.NotSet && errorCode != SecurityStatusPalErrorCode.OK && errorCode != SecurityStatusPalErrorCode.ContinueNeeded && errorCode != SecurityStatusPalErrorCode.CompleteNeeded && errorCode != SecurityStatusPalErrorCode.CompAndContinue && errorCode != SecurityStatusPalErrorCode.ContextExpired && errorCode != SecurityStatusPalErrorCode.CredentialsNeeded && errorCode != SecurityStatusPalErrorCode.Renegotiate;
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x00039889 File Offset: 0x00037A89
		public uint InitializeSspiPackage(ref uint maxLength)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x0008549C File Offset: 0x0008369C
		public uint SetConnectionBufferSize(SNIHandle handle, uint bufferSize)
		{
			handle.SetBufferSize((int)bufferSize);
			return 0U;
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x000854A8 File Offset: 0x000836A8
		public uint PacketGetData(SNIPacket packet, byte[] inBuff, ref uint dataSize)
		{
			int num = 0;
			packet.GetData(inBuff, ref num);
			dataSize = (uint)num;
			return 0U;
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x000854C4 File Offset: 0x000836C4
		public uint ReadSyncOverAsync(SNIHandle handle, out SNIPacket packet, int timeout)
		{
			return handle.Receive(out packet, timeout);
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x000854CE File Offset: 0x000836CE
		public uint GetConnectionId(SNIHandle handle, ref Guid clientConnectionId)
		{
			clientConnectionId = handle.ConnectionId;
			return 0U;
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x000854E0 File Offset: 0x000836E0
		public uint WritePacket(SNIHandle handle, SNIPacket packet, bool sync)
		{
			SNIPacket snipacket = packet.Clone();
			uint num;
			if (sync)
			{
				num = handle.Send(snipacket);
				snipacket.Dispose();
			}
			else
			{
				num = handle.SendAsync(snipacket, true, null);
			}
			return num;
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x00085514 File Offset: 0x00083714
		public SNIHandle CreateConnectionHandle(object callbackObject, string fullServerName, bool ignoreSniOpenTimeout, long timerExpire, out byte[] instanceName, ref byte[] spnBuffer, bool flushCache, bool async, bool parallel, bool isIntegratedSecurity)
		{
			instanceName = new byte[1];
			bool flag;
			string localDBDataSource = this.GetLocalDBDataSource(fullServerName, out flag);
			if (flag)
			{
				return null;
			}
			fullServerName = localDBDataSource ?? fullServerName;
			DataSource dataSource = DataSource.ParseServerName(fullServerName);
			if (dataSource == null)
			{
				return null;
			}
			SNIHandle snihandle = null;
			switch (dataSource.ConnectionProtocol)
			{
			case DataSource.Protocol.TCP:
			case DataSource.Protocol.None:
			case DataSource.Protocol.Admin:
				snihandle = this.CreateTcpHandle(dataSource, timerExpire, callbackObject, parallel);
				break;
			case DataSource.Protocol.NP:
				snihandle = this.CreateNpHandle(dataSource, timerExpire, callbackObject, parallel);
				break;
			}
			if (isIntegratedSecurity)
			{
				try
				{
					spnBuffer = SNIProxy.GetSqlServerSPN(dataSource);
				}
				catch (Exception ex)
				{
					SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.INVALID_PROV, 44U, ex);
				}
			}
			return snihandle;
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x000855C8 File Offset: 0x000837C8
		private static byte[] GetSqlServerSPN(DataSource dataSource)
		{
			string serverName = dataSource.ServerName;
			string text = null;
			if (dataSource.Port != -1)
			{
				text = dataSource.Port.ToString();
			}
			else if (!string.IsNullOrWhiteSpace(dataSource.InstanceName))
			{
				text = dataSource.InstanceName;
			}
			else if (dataSource.ConnectionProtocol == DataSource.Protocol.TCP)
			{
				text = 1433.ToString();
			}
			return SNIProxy.GetSqlServerSPN(serverName, text);
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x0008562C File Offset: 0x0008382C
		private static byte[] GetSqlServerSPN(string hostNameOrAddress, string portOrInstanceName)
		{
			IPHostEntry iphostEntry = null;
			string text;
			try
			{
				iphostEntry = Dns.GetHostEntry(hostNameOrAddress);
			}
			catch (SocketException)
			{
			}
			finally
			{
				text = ((iphostEntry != null) ? iphostEntry.HostName : null) ?? hostNameOrAddress;
			}
			string text2 = "MSSQLSvc/" + text;
			if (!string.IsNullOrWhiteSpace(portOrInstanceName))
			{
				text2 = text2 + ":" + portOrInstanceName;
			}
			return Encoding.UTF8.GetBytes(text2);
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x000856A4 File Offset: 0x000838A4
		private SNITCPHandle CreateTcpHandle(DataSource details, long timerExpire, object callbackObject, bool parallel)
		{
			string serverName = details.ServerName;
			if (string.IsNullOrWhiteSpace(serverName))
			{
				SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.TCP_PROV, 0U, 25U, string.Empty);
				return null;
			}
			int num = -1;
			bool flag = details.ConnectionProtocol == DataSource.Protocol.Admin;
			if (details.IsSsrpRequired)
			{
				try
				{
					num = (flag ? SSRP.GetDacPortByInstanceName(serverName, details.InstanceName) : SSRP.GetPortByInstanceName(serverName, details.InstanceName));
					goto IL_0098;
				}
				catch (SocketException ex)
				{
					SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.TCP_PROV, 25U, ex);
					return null;
				}
			}
			if (details.Port != -1)
			{
				num = details.Port;
			}
			else
			{
				num = (flag ? 1434 : 1433);
			}
			IL_0098:
			return new SNITCPHandle(serverName, num, timerExpire, callbackObject, parallel);
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x00085768 File Offset: 0x00083968
		private SNINpHandle CreateNpHandle(DataSource details, long timerExpire, object callbackObject, bool parallel)
		{
			if (parallel)
			{
				SNICommon.ReportSNIError(SNIProviders.NP_PROV, 0U, 49U, string.Empty);
				return null;
			}
			return new SNINpHandle(details.PipeHostName, details.PipeName, timerExpire, callbackObject);
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x00085792 File Offset: 0x00083992
		public uint ReadAsync(SNIHandle handle, out SNIPacket packet)
		{
			packet = null;
			return handle.ReceiveAsync(ref packet);
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x0008579E File Offset: 0x0008399E
		public void PacketSetData(SNIPacket packet, byte[] data, int length)
		{
			packet.SetData(data, length);
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x000857A8 File Offset: 0x000839A8
		public void PacketRelease(SNIPacket packet)
		{
			packet.Release();
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x000857B0 File Offset: 0x000839B0
		public uint CheckConnection(SNIHandle handle)
		{
			return handle.CheckConnection();
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x000857B8 File Offset: 0x000839B8
		public SNIError GetLastError()
		{
			return SNILoadHandle.SingletonInstance.LastError;
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x000857C4 File Offset: 0x000839C4
		private string GetLocalDBDataSource(string fullServerName, out bool error)
		{
			string text = null;
			bool flag;
			string localDBInstance = DataSource.GetLocalDBInstance(fullServerName, out flag);
			if (flag)
			{
				error = true;
				return null;
			}
			if (!string.IsNullOrEmpty(localDBInstance))
			{
				text = LocalDB.GetLocalDBConnectionString(localDBInstance);
				if (fullServerName == null)
				{
					error = true;
					return null;
				}
			}
			error = false;
			return text;
		}

		// Token: 0x04001335 RID: 4917
		private const int DefaultSqlServerPort = 1433;

		// Token: 0x04001336 RID: 4918
		private const int DefaultSqlServerDacPort = 1434;

		// Token: 0x04001337 RID: 4919
		private const string SqlServerSpnHeader = "MSSQLSvc";

		// Token: 0x04001338 RID: 4920
		public static readonly SNIProxy Singleton = new SNIProxy();

		// Token: 0x0200024A RID: 586
		internal class SspiClientContextResult
		{
			// Token: 0x04001339 RID: 4921
			internal const uint OK = 0U;

			// Token: 0x0400133A RID: 4922
			internal const uint Failed = 1U;

			// Token: 0x0400133B RID: 4923
			internal const uint KerberosTicketMissing = 2U;
		}
	}
}
