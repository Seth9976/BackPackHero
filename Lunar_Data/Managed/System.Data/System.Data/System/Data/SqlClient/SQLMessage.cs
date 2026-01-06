using System;

namespace System.Data.SqlClient
{
	// Token: 0x020001E8 RID: 488
	internal sealed class SQLMessage
	{
		// Token: 0x060017A7 RID: 6055 RVA: 0x00003D55 File Offset: 0x00001F55
		private SQLMessage()
		{
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x00071C93 File Offset: 0x0006FE93
		internal static string CultureIdError()
		{
			return SR.GetString("The Collation specified by SQL Server is not supported.");
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x00071C9F File Offset: 0x0006FE9F
		internal static string EncryptionNotSupportedByClient()
		{
			return SR.GetString("The instance of SQL Server you attempted to connect to requires encryption but this machine does not support it.");
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x00071CAB File Offset: 0x0006FEAB
		internal static string EncryptionNotSupportedByServer()
		{
			return SR.GetString("The instance of SQL Server you attempted to connect to does not support encryption.");
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x00071CB7 File Offset: 0x0006FEB7
		internal static string OperationCancelled()
		{
			return SR.GetString("Operation cancelled by user.");
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x00071CC3 File Offset: 0x0006FEC3
		internal static string SevereError()
		{
			return SR.GetString("A severe error occurred on the current command.  The results, if any, should be discarded.");
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x00071CCF File Offset: 0x0006FECF
		internal static string SSPIInitializeError()
		{
			return SR.GetString("Cannot initialize SSPI package.");
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x00071CDB File Offset: 0x0006FEDB
		internal static string SSPIGenerateError()
		{
			return SR.GetString("Failed to generate SSPI context.");
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x00071CE7 File Offset: 0x0006FEE7
		internal static string SqlServerBrowserNotAccessible()
		{
			return SR.GetString("Cannot connect to SQL Server Browser. Ensure SQL Server Browser has been started.");
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x00071CF3 File Offset: 0x0006FEF3
		internal static string KerberosTicketMissingError()
		{
			return SR.GetString("Cannot authenticate using Kerberos. Ensure Kerberos has been initialized on the client with 'kinit' and a Service Principal Name has been registered for the SQL Server to allow Kerberos authentication.");
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x00071CFF File Offset: 0x0006FEFF
		internal static string Timeout()
		{
			return SR.GetString("Timeout expired.  The timeout period elapsed prior to completion of the operation or the server is not responding.");
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x00071D0B File Offset: 0x0006FF0B
		internal static string Timeout_PreLogin_Begin()
		{
			return SR.GetString("Connection Timeout Expired.  The timeout period elapsed at the start of the pre-login phase.  This could be because of insufficient time provided for connection timeout.");
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x00071D17 File Offset: 0x0006FF17
		internal static string Timeout_PreLogin_InitializeConnection()
		{
			return SR.GetString("Connection Timeout Expired.  The timeout period elapsed while attempting to create and initialize a socket to the server.  This could be either because the server was unreachable or unable to respond back in time.");
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x00071D23 File Offset: 0x0006FF23
		internal static string Timeout_PreLogin_SendHandshake()
		{
			return SR.GetString("Connection Timeout Expired.  The timeout period elapsed while making a pre-login handshake request.  This could be because the server was unable to respond back in time.");
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x00071D2F File Offset: 0x0006FF2F
		internal static string Timeout_PreLogin_ConsumeHandshake()
		{
			return SR.GetString("Connection Timeout Expired.  The timeout period elapsed while attempting to consume the pre-login handshake acknowledgement.  This could be because the pre-login handshake failed or the server was unable to respond back in time.");
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x00071D3B File Offset: 0x0006FF3B
		internal static string Timeout_Login_Begin()
		{
			return SR.GetString("Connection Timeout Expired.  The timeout period elapsed at the start of the login phase.  This could be because of insufficient time provided for connection timeout.");
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x00071D47 File Offset: 0x0006FF47
		internal static string Timeout_Login_ProcessConnectionAuth()
		{
			return SR.GetString("Connection Timeout Expired.  The timeout period elapsed while attempting to authenticate the login.  This could be because the server failed to authenticate the user or the server was unable to respond back in time.");
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x00071D53 File Offset: 0x0006FF53
		internal static string Timeout_PostLogin()
		{
			return SR.GetString("Connection Timeout Expired.  The timeout period elapsed during the post-login phase.  The connection could have timed out while waiting for server to complete the login process and respond; Or it could have timed out while attempting to create multiple active connections.");
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x00071D5F File Offset: 0x0006FF5F
		internal static string Timeout_FailoverInfo()
		{
			return SR.GetString("This failure occurred while attempting to connect to the {0} server.");
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x00071D6B File Offset: 0x0006FF6B
		internal static string Timeout_RoutingDestination()
		{
			return SR.GetString("This failure occurred while attempting to connect to the routing destination. The duration spent while attempting to connect to the original server was - [Pre-Login] initialization={0}; handshake={1}; [Login] initialization={2}; authentication={3}; [Post-Login] complete={4};  ");
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x00071D77 File Offset: 0x0006FF77
		internal static string Duration_PreLogin_Begin(long PreLoginBeginDuration)
		{
			return SR.GetString("The duration spent while attempting to connect to this server was - [Pre-Login] initialization={0};", new object[] { PreLoginBeginDuration });
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x00071D92 File Offset: 0x0006FF92
		internal static string Duration_PreLoginHandshake(long PreLoginBeginDuration, long PreLoginHandshakeDuration)
		{
			return SR.GetString("The duration spent while attempting to connect to this server was - [Pre-Login] initialization={0}; handshake={1}; ", new object[] { PreLoginBeginDuration, PreLoginHandshakeDuration });
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x00071DB6 File Offset: 0x0006FFB6
		internal static string Duration_Login_Begin(long PreLoginBeginDuration, long PreLoginHandshakeDuration, long LoginBeginDuration)
		{
			return SR.GetString("The duration spent while attempting to connect to this server was - [Pre-Login] initialization={0}; handshake={1}; [Login] initialization={2}; ", new object[] { PreLoginBeginDuration, PreLoginHandshakeDuration, LoginBeginDuration });
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x00071DE3 File Offset: 0x0006FFE3
		internal static string Duration_Login_ProcessConnectionAuth(long PreLoginBeginDuration, long PreLoginHandshakeDuration, long LoginBeginDuration, long LoginAuthDuration)
		{
			return SR.GetString("The duration spent while attempting to connect to this server was - [Pre-Login] initialization={0}; handshake={1}; [Login] initialization={2}; authentication={3}; ", new object[] { PreLoginBeginDuration, PreLoginHandshakeDuration, LoginBeginDuration, LoginAuthDuration });
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x00071E19 File Offset: 0x00070019
		internal static string Duration_PostLogin(long PreLoginBeginDuration, long PreLoginHandshakeDuration, long LoginBeginDuration, long LoginAuthDuration, long PostLoginDuration)
		{
			return SR.GetString("The duration spent while attempting to connect to this server was - [Pre-Login] initialization={0}; handshake={1}; [Login] initialization={2}; authentication={3}; [Post-Login] complete={4}; ", new object[] { PreLoginBeginDuration, PreLoginHandshakeDuration, LoginBeginDuration, LoginAuthDuration, PostLoginDuration });
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x00071E59 File Offset: 0x00070059
		internal static string UserInstanceFailure()
		{
			return SR.GetString("A user instance was requested in the connection string but the server specified does not support this option.");
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x00071E65 File Offset: 0x00070065
		internal static string PreloginError()
		{
			return SR.GetString("A connection was successfully established with the server, but then an error occurred during the pre-login handshake.");
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x00071E71 File Offset: 0x00070071
		internal static string ExClientConnectionId()
		{
			return SR.GetString("ClientConnectionId:{0}");
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x00071E7D File Offset: 0x0007007D
		internal static string ExErrorNumberStateClass()
		{
			return SR.GetString("Error Number:{0},State:{1},Class:{2}");
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x00071E89 File Offset: 0x00070089
		internal static string ExOriginalClientConnectionId()
		{
			return SR.GetString("ClientConnectionId before routing:{0}");
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x00071E95 File Offset: 0x00070095
		internal static string ExRoutingDestination()
		{
			return SR.GetString("Routing Destination:{0}");
		}
	}
}
