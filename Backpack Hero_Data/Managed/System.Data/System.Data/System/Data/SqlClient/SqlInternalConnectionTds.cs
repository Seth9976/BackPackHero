using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Globalization;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace System.Data.SqlClient
{
	// Token: 0x020001B8 RID: 440
	internal sealed class SqlInternalConnectionTds : SqlInternalConnection, IDisposable
	{
		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001542 RID: 5442 RVA: 0x0006913C File Offset: 0x0006733C
		internal SessionData CurrentSessionData
		{
			get
			{
				if (this._currentSessionData != null)
				{
					this._currentSessionData._database = base.CurrentDatabase;
					this._currentSessionData._language = this._currentLanguage;
				}
				return this._currentSessionData;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001543 RID: 5443 RVA: 0x0006916E File Offset: 0x0006736E
		internal SqlConnectionTimeoutErrorInternal TimeoutErrorInternal
		{
			get
			{
				return this._timeoutErrorInternal;
			}
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x00069178 File Offset: 0x00067378
		internal SqlInternalConnectionTds(DbConnectionPoolIdentity identity, SqlConnectionString connectionOptions, SqlCredential credential, object providerInfo, string newPassword, SecureString newSecurePassword, bool redirectedUserInstance, SqlConnectionString userConnectionOptions = null, SessionData reconnectSessionData = null, bool applyTransientFaultHandling = false, string accessToken = null)
			: base(connectionOptions)
		{
			if (connectionOptions.ConnectRetryCount > 0)
			{
				this._recoverySessionData = reconnectSessionData;
				if (reconnectSessionData == null)
				{
					this._currentSessionData = new SessionData();
				}
				else
				{
					this._currentSessionData = new SessionData(this._recoverySessionData);
					this._originalDatabase = this._recoverySessionData._initialDatabase;
					this._originalLanguage = this._recoverySessionData._initialLanguage;
				}
			}
			if (accessToken != null)
			{
				this._accessTokenInBytes = Encoding.Unicode.GetBytes(accessToken);
			}
			this._identity = identity;
			this._poolGroupProviderInfo = (SqlConnectionPoolGroupProviderInfo)providerInfo;
			this._fResetConnection = connectionOptions.ConnectionReset;
			if (this._fResetConnection && this._recoverySessionData == null)
			{
				this._originalDatabase = connectionOptions.InitialCatalog;
				this._originalLanguage = connectionOptions.CurrentLanguage;
			}
			this._timeoutErrorInternal = new SqlConnectionTimeoutErrorInternal();
			this._credential = credential;
			this._parserLock.Wait(false);
			this.ThreadHasParserLockForClose = true;
			try
			{
				this._timeout = TimeoutTimer.StartSecondsTimeout(connectionOptions.ConnectTimeout);
				int num = (applyTransientFaultHandling ? (connectionOptions.ConnectRetryCount + 1) : 1);
				int num2 = connectionOptions.ConnectRetryInterval * 1000;
				for (int i = 0; i < num; i++)
				{
					try
					{
						this.OpenLoginEnlist(this._timeout, connectionOptions, credential, newPassword, newSecurePassword, redirectedUserInstance);
						break;
					}
					catch (SqlException ex)
					{
						if (i + 1 == num || !applyTransientFaultHandling || this._timeout.IsExpired || this._timeout.MillisecondsRemaining < (long)num2 || !this.IsTransientError(ex))
						{
							throw ex;
						}
						Thread.Sleep(num2);
					}
				}
			}
			finally
			{
				this.ThreadHasParserLockForClose = false;
				this._parserLock.Release();
			}
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x00069354 File Offset: 0x00067554
		private bool IsTransientError(SqlException exc)
		{
			if (exc == null)
			{
				return false;
			}
			foreach (object obj in exc.Errors)
			{
				SqlError sqlError = (SqlError)obj;
				if (SqlInternalConnectionTds.s_transientErrors.Contains(sqlError.Number))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001546 RID: 5446 RVA: 0x000693C4 File Offset: 0x000675C4
		internal Guid ClientConnectionId
		{
			get
			{
				return this._clientConnectionId;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001547 RID: 5447 RVA: 0x000693CC File Offset: 0x000675CC
		internal Guid OriginalClientConnectionId
		{
			get
			{
				return this._originalClientConnectionId;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001548 RID: 5448 RVA: 0x000693D4 File Offset: 0x000675D4
		internal string RoutingDestination
		{
			get
			{
				return this._routingDestination;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x000693DC File Offset: 0x000675DC
		internal override SqlInternalTransaction CurrentTransaction
		{
			get
			{
				return this._parser.CurrentTransaction;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x000693E9 File Offset: 0x000675E9
		internal override SqlInternalTransaction AvailableInternalTransaction
		{
			get
			{
				if (!this._parser._fResetConnection)
				{
					return this.CurrentTransaction;
				}
				return null;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x00069402 File Offset: 0x00067602
		internal override SqlInternalTransaction PendingTransaction
		{
			get
			{
				return this._parser.PendingTransaction;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x0600154C RID: 5452 RVA: 0x0006940F File Offset: 0x0006760F
		internal DbConnectionPoolIdentity Identity
		{
			get
			{
				return this._identity;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x00069417 File Offset: 0x00067617
		internal string InstanceName
		{
			get
			{
				return this._instanceName;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x0600154E RID: 5454 RVA: 0x0006941F File Offset: 0x0006761F
		internal override bool IsLockedForBulkCopy
		{
			get
			{
				return !this.Parser.MARSOn && this.Parser._physicalStateObj.BcpLock;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x00069440 File Offset: 0x00067640
		protected internal override bool IsNonPoolableTransactionRoot
		{
			get
			{
				return this.IsTransactionRoot && (!this.IsKatmaiOrNewer || base.Pool == null);
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x0006945F File Offset: 0x0006765F
		internal override bool IsKatmaiOrNewer
		{
			get
			{
				return this._parser.IsKatmaiOrNewer;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x0006946C File Offset: 0x0006766C
		internal int PacketSize
		{
			get
			{
				return this._currentPacketSize;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x00069474 File Offset: 0x00067674
		internal TdsParser Parser
		{
			get
			{
				return this._parser;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x0006947C File Offset: 0x0006767C
		internal string ServerProvidedFailOverPartner
		{
			get
			{
				return this._currentFailoverPartner;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x00069484 File Offset: 0x00067684
		internal SqlConnectionPoolGroupProviderInfo PoolGroupProviderInfo
		{
			get
			{
				return this._poolGroupProviderInfo;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001555 RID: 5461 RVA: 0x0006948C File Offset: 0x0006768C
		protected override bool ReadyToPrepareTransaction
		{
			get
			{
				return base.FindLiveReader(null) == null;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x00069498 File Offset: 0x00067698
		public override string ServerVersion
		{
			get
			{
				return string.Format(null, "{0:00}.{1:00}.{2:0000}", this._loginAck.majorVersion, (short)this._loginAck.minorVersion, this._loginAck.buildNum);
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001557 RID: 5463 RVA: 0x00005AE9 File Offset: 0x00003CE9
		protected override bool UnbindOnTransactionCompletion
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x000694D8 File Offset: 0x000676D8
		protected override void ChangeDatabaseInternal(string database)
		{
			database = SqlConnection.FixupDatabaseTransactionName(database);
			this._parser.TdsExecuteSQLBatch("use " + database, base.ConnectionOptions.ConnectTimeout, null, this._parser._physicalStateObj, true, false);
			this._parser.Run(RunBehavior.UntilDone, null, null, null, this._parser._physicalStateObj);
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x00069538 File Offset: 0x00067738
		public override void Dispose()
		{
			try
			{
				TdsParser tdsParser = Interlocked.Exchange<TdsParser>(ref this._parser, null);
				if (tdsParser != null)
				{
					tdsParser.Disconnect();
				}
			}
			finally
			{
				this._loginAck = null;
				this._fConnectionOpen = false;
			}
			base.Dispose();
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x00069584 File Offset: 0x00067784
		internal override void ValidateConnectionForExecute(SqlCommand command)
		{
			TdsParser parser = this._parser;
			if (parser == null || parser.State == TdsParserState.Broken || parser.State == TdsParserState.Closed)
			{
				throw ADP.ClosedConnectionError();
			}
			SqlDataReader sqlDataReader = null;
			if (parser.MARSOn)
			{
				if (command != null)
				{
					sqlDataReader = base.FindLiveReader(command);
				}
			}
			else
			{
				if (this._asyncCommandCount > 0)
				{
					throw SQL.MARSUnspportedOnConnection();
				}
				sqlDataReader = base.FindLiveReader(null);
			}
			if (sqlDataReader != null)
			{
				throw ADP.OpenReaderExists();
			}
			if (!parser.MARSOn && parser._physicalStateObj._pendingData)
			{
				parser.DrainData(parser._physicalStateObj);
			}
			parser.RollbackOrphanedAPITransactions();
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x00069610 File Offset: 0x00067810
		internal void CheckEnlistedTransactionBinding()
		{
			Transaction enlistedTransaction = base.EnlistedTransaction;
			if (enlistedTransaction != null)
			{
				if (base.ConnectionOptions.TransactionBinding == SqlConnectionString.TransactionBindingEnum.ExplicitUnbind)
				{
					Transaction transaction = Transaction.Current;
					if (enlistedTransaction.TransactionInformation.Status != TransactionStatus.Active || !enlistedTransaction.Equals(transaction))
					{
						throw ADP.TransactionConnectionMismatch();
					}
				}
				else if (enlistedTransaction.TransactionInformation.Status != TransactionStatus.Active)
				{
					if (base.EnlistedTransactionDisposed)
					{
						base.DetachTransaction(enlistedTransaction, true);
						return;
					}
					throw ADP.TransactionCompletedButNotDisposed();
				}
			}
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x00069683 File Offset: 0x00067883
		internal override bool IsConnectionAlive(bool throwOnException)
		{
			return this._parser._physicalStateObj.IsConnectionAlive(throwOnException);
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x00069696 File Offset: 0x00067896
		protected override void Activate(Transaction transaction)
		{
			if (null != transaction)
			{
				if (base.ConnectionOptions.Enlist)
				{
					base.Enlist(transaction);
					return;
				}
			}
			else
			{
				base.Enlist(null);
			}
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x000696BD File Offset: 0x000678BD
		protected override void InternalDeactivate()
		{
			if (this._asyncCommandCount != 0)
			{
				base.DoomThisConnection();
			}
			if (!this.IsNonPoolableTransactionRoot && this._parser != null)
			{
				this._parser.Deactivate(base.IsConnectionDoomed);
				if (!base.IsConnectionDoomed)
				{
					this.ResetConnection();
				}
			}
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x000696FC File Offset: 0x000678FC
		private void ResetConnection()
		{
			if (this._fResetConnection)
			{
				this._parser.PrepareResetConnection(this.IsTransactionRoot && !this.IsNonPoolableTransactionRoot);
				base.CurrentDatabase = this._originalDatabase;
				this._currentLanguage = this._originalLanguage;
			}
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x00069748 File Offset: 0x00067948
		internal void DecrementAsyncCount()
		{
			Interlocked.Decrement(ref this._asyncCommandCount);
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x00069756 File Offset: 0x00067956
		internal void IncrementAsyncCount()
		{
			Interlocked.Increment(ref this._asyncCommandCount);
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x00069764 File Offset: 0x00067964
		internal override void DisconnectTransaction(SqlInternalTransaction internalTransaction)
		{
			TdsParser parser = this.Parser;
			if (parser != null)
			{
				parser.DisconnectTransaction(internalTransaction);
			}
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x00069782 File Offset: 0x00067982
		internal void ExecuteTransaction(SqlInternalConnection.TransactionRequest transactionRequest, string name, IsolationLevel iso)
		{
			this.ExecuteTransaction(transactionRequest, name, iso, null, false);
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x00069790 File Offset: 0x00067990
		internal override void ExecuteTransaction(SqlInternalConnection.TransactionRequest transactionRequest, string name, IsolationLevel iso, SqlInternalTransaction internalTransaction, bool isDelegateControlRequest)
		{
			if (base.IsConnectionDoomed)
			{
				if (transactionRequest == SqlInternalConnection.TransactionRequest.Rollback || transactionRequest == SqlInternalConnection.TransactionRequest.IfRollback)
				{
					return;
				}
				throw SQL.ConnectionDoomed();
			}
			else
			{
				if ((transactionRequest == SqlInternalConnection.TransactionRequest.Commit || transactionRequest == SqlInternalConnection.TransactionRequest.Rollback || transactionRequest == SqlInternalConnection.TransactionRequest.IfRollback) && !this.Parser.MARSOn && this.Parser._physicalStateObj.BcpLock)
				{
					throw SQL.ConnectionLockedForBcpEvent();
				}
				string text = ((name == null) ? string.Empty : name);
				this.ExecuteTransactionYukon(transactionRequest, text, iso, internalTransaction, isDelegateControlRequest);
				return;
			}
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x00069800 File Offset: 0x00067A00
		internal void ExecuteTransactionYukon(SqlInternalConnection.TransactionRequest transactionRequest, string transactionName, IsolationLevel iso, SqlInternalTransaction internalTransaction, bool isDelegateControlRequest)
		{
			TdsEnums.TransactionManagerRequestType transactionManagerRequestType = TdsEnums.TransactionManagerRequestType.Begin;
			if (iso <= IsolationLevel.ReadUncommitted)
			{
				if (iso == IsolationLevel.Unspecified)
				{
					TdsEnums.TransactionManagerIsolationLevel transactionManagerIsolationLevel = TdsEnums.TransactionManagerIsolationLevel.Unspecified;
					goto IL_007E;
				}
				if (iso == IsolationLevel.Chaos)
				{
					throw SQL.NotSupportedIsolationLevel(iso);
				}
				if (iso == IsolationLevel.ReadUncommitted)
				{
					TdsEnums.TransactionManagerIsolationLevel transactionManagerIsolationLevel = TdsEnums.TransactionManagerIsolationLevel.ReadUncommitted;
					goto IL_007E;
				}
			}
			else if (iso <= IsolationLevel.RepeatableRead)
			{
				if (iso == IsolationLevel.ReadCommitted)
				{
					TdsEnums.TransactionManagerIsolationLevel transactionManagerIsolationLevel = TdsEnums.TransactionManagerIsolationLevel.ReadCommitted;
					goto IL_007E;
				}
				if (iso == IsolationLevel.RepeatableRead)
				{
					TdsEnums.TransactionManagerIsolationLevel transactionManagerIsolationLevel = TdsEnums.TransactionManagerIsolationLevel.RepeatableRead;
					goto IL_007E;
				}
			}
			else
			{
				if (iso == IsolationLevel.Serializable)
				{
					TdsEnums.TransactionManagerIsolationLevel transactionManagerIsolationLevel = TdsEnums.TransactionManagerIsolationLevel.Serializable;
					goto IL_007E;
				}
				if (iso == IsolationLevel.Snapshot)
				{
					TdsEnums.TransactionManagerIsolationLevel transactionManagerIsolationLevel = TdsEnums.TransactionManagerIsolationLevel.Snapshot;
					goto IL_007E;
				}
			}
			throw ADP.InvalidIsolationLevel(iso);
			IL_007E:
			TdsParserStateObject tdsParserStateObject = this._parser._physicalStateObj;
			TdsParser parser = this._parser;
			bool flag = false;
			bool releaseConnectionLock = false;
			if (!this.ThreadHasParserLockForClose)
			{
				this._parserLock.Wait(false);
				this.ThreadHasParserLockForClose = true;
				releaseConnectionLock = true;
			}
			try
			{
				switch (transactionRequest)
				{
				case SqlInternalConnection.TransactionRequest.Begin:
					transactionManagerRequestType = TdsEnums.TransactionManagerRequestType.Begin;
					break;
				case SqlInternalConnection.TransactionRequest.Promote:
					transactionManagerRequestType = TdsEnums.TransactionManagerRequestType.Promote;
					break;
				case SqlInternalConnection.TransactionRequest.Commit:
					transactionManagerRequestType = TdsEnums.TransactionManagerRequestType.Commit;
					break;
				case SqlInternalConnection.TransactionRequest.Rollback:
				case SqlInternalConnection.TransactionRequest.IfRollback:
					transactionManagerRequestType = TdsEnums.TransactionManagerRequestType.Rollback;
					break;
				case SqlInternalConnection.TransactionRequest.Save:
					transactionManagerRequestType = TdsEnums.TransactionManagerRequestType.Save;
					break;
				}
				if ((internalTransaction != null && internalTransaction.RestoreBrokenConnection) & releaseConnectionLock)
				{
					Task task = internalTransaction.Parent.Connection.ValidateAndReconnect(delegate
					{
						this.ThreadHasParserLockForClose = false;
						this._parserLock.Release();
						releaseConnectionLock = false;
					}, 0);
					if (task != null)
					{
						AsyncHelper.WaitForCompletion(task, 0, null, true);
						internalTransaction.ConnectionHasBeenRestored = true;
						return;
					}
				}
				if (internalTransaction != null && internalTransaction.IsDelegated)
				{
					if (this._parser.MARSOn)
					{
						tdsParserStateObject = this._parser.GetSession(this);
						flag = true;
					}
					else
					{
						int openResultsCount = internalTransaction.OpenResultsCount;
					}
				}
				TdsEnums.TransactionManagerIsolationLevel transactionManagerIsolationLevel;
				this._parser.TdsExecuteTransactionManagerRequest(null, transactionManagerRequestType, transactionName, transactionManagerIsolationLevel, base.ConnectionOptions.ConnectTimeout, internalTransaction, tdsParserStateObject, isDelegateControlRequest);
			}
			finally
			{
				if (flag)
				{
					parser.PutSession(tdsParserStateObject);
				}
				if (releaseConnectionLock)
				{
					this.ThreadHasParserLockForClose = false;
					this._parserLock.Release();
				}
			}
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x000699DC File Offset: 0x00067BDC
		internal override void DelegatedTransactionEnded()
		{
			base.DelegatedTransactionEnded();
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x000699E4 File Offset: 0x00067BE4
		protected override byte[] GetDTCAddress()
		{
			return this._parser.GetDTCAddress(base.ConnectionOptions.ConnectTimeout, this._parser.GetSession(this));
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x00069A08 File Offset: 0x00067C08
		protected override void PropagateTransactionCookie(byte[] cookie)
		{
			this._parser.PropagateDistributedTransaction(cookie, base.ConnectionOptions.ConnectTimeout, this._parser._physicalStateObj);
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x00069A2C File Offset: 0x00067C2C
		private void CompleteLogin(bool enlistOK)
		{
			this._parser.Run(RunBehavior.UntilDone, null, null, null, this._parser._physicalStateObj);
			if (this._routingInfo == null)
			{
				if (this._federatedAuthenticationRequested && !this._federatedAuthenticationAcknowledged)
				{
					throw SQL.ParsingError(ParsingErrorState.FedAuthNotAcknowledged);
				}
				if (!this._sessionRecoveryAcknowledged)
				{
					this._currentSessionData = null;
					if (this._recoverySessionData != null)
					{
						throw SQL.CR_NoCRAckAtReconnection(this);
					}
				}
				if (this._currentSessionData != null && this._recoverySessionData == null)
				{
					this._currentSessionData._initialDatabase = base.CurrentDatabase;
					this._currentSessionData._initialCollation = this._currentSessionData._collation;
					this._currentSessionData._initialLanguage = this._currentLanguage;
				}
				bool flag = this._parser.EncryptionOptions == EncryptionOptions.ON;
				if (this._recoverySessionData != null && this._recoverySessionData._encrypted != flag)
				{
					throw SQL.CR_EncryptionChanged(this);
				}
				if (this._currentSessionData != null)
				{
					this._currentSessionData._encrypted = flag;
				}
				this._recoverySessionData = null;
			}
			this._parser._physicalStateObj.SniContext = SniContext.Snix_EnableMars;
			this._parser.EnableMars();
			this._fConnectionOpen = true;
			if (enlistOK && base.ConnectionOptions.Enlist)
			{
				this._parser._physicalStateObj.SniContext = SniContext.Snix_AutoEnlist;
				Transaction currentTransaction = ADP.GetCurrentTransaction();
				base.Enlist(currentTransaction);
			}
			this._parser._physicalStateObj.SniContext = SniContext.Snix_Login;
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x00069B88 File Offset: 0x00067D88
		private void Login(ServerInfo server, TimeoutTimer timeout, string newPassword, SecureString newSecurePassword)
		{
			SqlLogin sqlLogin = new SqlLogin();
			base.CurrentDatabase = server.ResolvedDatabaseName;
			this._currentPacketSize = base.ConnectionOptions.PacketSize;
			this._currentLanguage = base.ConnectionOptions.CurrentLanguage;
			int num = 0;
			if (!timeout.IsInfinite)
			{
				long num2 = timeout.MillisecondsRemaining / 1000L;
				if (2147483647L > num2)
				{
					num = (int)num2;
				}
			}
			sqlLogin.timeout = num;
			sqlLogin.userInstance = base.ConnectionOptions.UserInstance;
			sqlLogin.hostName = base.ConnectionOptions.ObtainWorkstationId();
			sqlLogin.userName = base.ConnectionOptions.UserID;
			sqlLogin.password = base.ConnectionOptions.Password;
			sqlLogin.applicationName = base.ConnectionOptions.ApplicationName;
			sqlLogin.language = this._currentLanguage;
			if (!sqlLogin.userInstance)
			{
				sqlLogin.database = base.CurrentDatabase;
				sqlLogin.attachDBFilename = base.ConnectionOptions.AttachDBFilename;
			}
			sqlLogin.serverName = server.UserServerName;
			sqlLogin.useReplication = base.ConnectionOptions.Replication;
			sqlLogin.useSSPI = base.ConnectionOptions.IntegratedSecurity;
			sqlLogin.packetSize = this._currentPacketSize;
			sqlLogin.newPassword = newPassword;
			sqlLogin.readOnlyIntent = base.ConnectionOptions.ApplicationIntent == ApplicationIntent.ReadOnly;
			sqlLogin.credential = this._credential;
			if (newSecurePassword != null)
			{
				sqlLogin.newSecurePassword = newSecurePassword;
			}
			TdsEnums.FeatureExtension featureExtension = TdsEnums.FeatureExtension.None;
			if (base.ConnectionOptions.ConnectRetryCount > 0)
			{
				featureExtension |= TdsEnums.FeatureExtension.SessionRecovery;
				this._sessionRecoveryRequested = true;
			}
			if (this._accessTokenInBytes != null)
			{
				featureExtension |= TdsEnums.FeatureExtension.FedAuth;
				this._fedAuthFeatureExtensionData = new FederatedAuthenticationFeatureExtensionData?(new FederatedAuthenticationFeatureExtensionData
				{
					libraryType = TdsEnums.FedAuthLibrary.SecurityToken,
					fedAuthRequiredPreLoginResponse = this._fedAuthRequired,
					accessToken = this._accessTokenInBytes
				});
				this._federatedAuthenticationRequested = true;
			}
			featureExtension |= TdsEnums.FeatureExtension.GlobalTransactions;
			this._parser.TdsLogin(sqlLogin, featureExtension, this._recoverySessionData, this._fedAuthFeatureExtensionData);
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x00069D69 File Offset: 0x00067F69
		private void LoginFailure()
		{
			if (this._parser != null)
			{
				this._parser.Disconnect();
			}
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x00069D80 File Offset: 0x00067F80
		private void OpenLoginEnlist(TimeoutTimer timeout, SqlConnectionString connectionOptions, SqlCredential credential, string newPassword, SecureString newSecurePassword, bool redirectedUserInstance)
		{
			ServerInfo serverInfo = new ServerInfo(connectionOptions);
			bool flag;
			string text;
			if (this.PoolGroupProviderInfo != null)
			{
				flag = this.PoolGroupProviderInfo.UseFailoverPartner;
				text = this.PoolGroupProviderInfo.FailoverPartner;
			}
			else
			{
				flag = false;
				text = base.ConnectionOptions.FailoverPartner;
			}
			this._timeoutErrorInternal.SetInternalSourceType(flag ? SqlConnectionInternalSourceType.Failover : SqlConnectionInternalSourceType.Principle);
			bool flag2 = !string.IsNullOrEmpty(text);
			try
			{
				this._timeoutErrorInternal.SetAndBeginPhase(SqlConnectionTimeoutErrorPhase.PreLoginBegin);
				if (flag2)
				{
					this._timeoutErrorInternal.SetFailoverScenario(true);
					this.LoginWithFailover(flag, serverInfo, text, newPassword, newSecurePassword, redirectedUserInstance, connectionOptions, credential, timeout);
				}
				else
				{
					this._timeoutErrorInternal.SetFailoverScenario(false);
					this.LoginNoFailover(serverInfo, newPassword, newSecurePassword, redirectedUserInstance, connectionOptions, credential, timeout);
				}
				this._timeoutErrorInternal.EndPhase(SqlConnectionTimeoutErrorPhase.PostLogin);
			}
			catch (Exception ex)
			{
				if (ADP.IsCatchableExceptionType(ex))
				{
					this.LoginFailure();
				}
				throw;
			}
			this._timeoutErrorInternal.SetAllCompleteMarker();
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x00069E64 File Offset: 0x00068064
		private bool IsDoNotRetryConnectError(SqlException exc)
		{
			return 18456 == exc.Number || 18488 == exc.Number || 1346 == exc.Number || exc._doNotReconnect;
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x00069E98 File Offset: 0x00068098
		private void LoginNoFailover(ServerInfo serverInfo, string newPassword, SecureString newSecurePassword, bool redirectedUserInstance, SqlConnectionString connectionOptions, SqlCredential credential, TimeoutTimer timeout)
		{
			int num = 0;
			ServerInfo serverInfo2 = serverInfo;
			int num2 = 100;
			this.ResolveExtendedServerName(serverInfo, !redirectedUserInstance, connectionOptions);
			long num3 = 0L;
			int num4;
			TimeoutTimer timeoutTimer;
			checked
			{
				if (connectionOptions.MultiSubnetFailover)
				{
					if (timeout.IsInfinite)
					{
						num3 = 1200L;
					}
					else
					{
						num3 = (long)(unchecked(0.08f * (float)timeout.MillisecondsRemaining));
					}
				}
				num4 = 0;
				timeoutTimer = null;
			}
			for (;;)
			{
				if (connectionOptions.MultiSubnetFailover)
				{
					num4++;
					checked
					{
						long num5 = num3 * unchecked((long)num4);
						long millisecondsRemaining = timeout.MillisecondsRemaining;
						if (num5 > millisecondsRemaining)
						{
							num5 = millisecondsRemaining;
						}
						timeoutTimer = TimeoutTimer.StartMillisecondsTimeout(num5);
					}
				}
				if (this._parser != null)
				{
					this._parser.Disconnect();
				}
				this._parser = new TdsParser(base.ConnectionOptions.MARS, base.ConnectionOptions.Asynchronous);
				try
				{
					this.AttemptOneLogin(serverInfo, newPassword, newSecurePassword, !connectionOptions.MultiSubnetFailover, connectionOptions.MultiSubnetFailover ? timeoutTimer : timeout, false);
					if (connectionOptions.MultiSubnetFailover && this.ServerProvidedFailOverPartner != null)
					{
						throw SQL.MultiSubnetFailoverWithFailoverPartner(true, this);
					}
					if (this._routingInfo == null)
					{
						goto IL_0271;
					}
					if (num > 0)
					{
						throw SQL.ROR_RecursiveRoutingNotSupported(this);
					}
					if (timeout.IsExpired)
					{
						throw SQL.ROR_TimeoutAfterRoutingInfo(this);
					}
					serverInfo = new ServerInfo(base.ConnectionOptions, this._routingInfo, serverInfo.ResolvedServerName);
					this._timeoutErrorInternal.SetInternalSourceType(SqlConnectionInternalSourceType.RoutingDestination);
					this._originalClientConnectionId = this._clientConnectionId;
					this._routingDestination = serverInfo.UserServerName;
					this._currentPacketSize = base.ConnectionOptions.PacketSize;
					this._currentLanguage = (this._originalLanguage = base.ConnectionOptions.CurrentLanguage);
					base.CurrentDatabase = (this._originalDatabase = base.ConnectionOptions.InitialCatalog);
					this._currentFailoverPartner = null;
					this._instanceName = string.Empty;
					num++;
					continue;
				}
				catch (SqlException ex)
				{
					if (this._parser == null || this._parser.State != TdsParserState.Closed || this.IsDoNotRetryConnectError(ex) || timeout.IsExpired)
					{
						throw;
					}
					if (timeout.MillisecondsRemaining <= (long)num2)
					{
						throw;
					}
				}
				if (this.ServerProvidedFailOverPartner != null)
				{
					break;
				}
				Thread.Sleep(num2);
				num2 = ((num2 < 500) ? (num2 * 2) : 1000);
			}
			if (connectionOptions.MultiSubnetFailover)
			{
				throw SQL.MultiSubnetFailoverWithFailoverPartner(true, this);
			}
			this._timeoutErrorInternal.ResetAndRestartPhase();
			this._timeoutErrorInternal.SetAndBeginPhase(SqlConnectionTimeoutErrorPhase.PreLoginBegin);
			this._timeoutErrorInternal.SetInternalSourceType(SqlConnectionInternalSourceType.Failover);
			this._timeoutErrorInternal.SetFailoverScenario(true);
			this.LoginWithFailover(true, serverInfo, this.ServerProvidedFailOverPartner, newPassword, newSecurePassword, redirectedUserInstance, connectionOptions, credential, timeout);
			return;
			IL_0271:
			if (this.PoolGroupProviderInfo != null)
			{
				this.PoolGroupProviderInfo.FailoverCheck(this, false, connectionOptions, this.ServerProvidedFailOverPartner);
			}
			base.CurrentDataSource = serverInfo2.UserServerName;
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x0006A15C File Offset: 0x0006835C
		private void LoginWithFailover(bool useFailoverHost, ServerInfo primaryServerInfo, string failoverHost, string newPassword, SecureString newSecurePassword, bool redirectedUserInstance, SqlConnectionString connectionOptions, SqlCredential credential, TimeoutTimer timeout)
		{
			int num = 100;
			ServerInfo serverInfo = new ServerInfo(connectionOptions, failoverHost);
			this.ResolveExtendedServerName(primaryServerInfo, !redirectedUserInstance, connectionOptions);
			if (this.ServerProvidedFailOverPartner == null)
			{
				this.ResolveExtendedServerName(serverInfo, !redirectedUserInstance && failoverHost != primaryServerInfo.UserServerName, connectionOptions);
			}
			checked
			{
				long num2;
				if (timeout.IsInfinite)
				{
					num2 = (long)(unchecked(0.08f * (float)ADP.TimerFromSeconds(15)));
				}
				else
				{
					num2 = (long)(unchecked(0.08f * (float)timeout.MillisecondsRemaining));
				}
				int num3 = 0;
				for (;;)
				{
					long num4 = num2 * unchecked((long)(checked(num3 / 2 + 1)));
					long millisecondsRemaining = timeout.MillisecondsRemaining;
					if (num4 > millisecondsRemaining)
					{
						num4 = millisecondsRemaining;
					}
					TimeoutTimer timeoutTimer = TimeoutTimer.StartMillisecondsTimeout(num4);
					if (this._parser != null)
					{
						this._parser.Disconnect();
					}
					this._parser = new TdsParser(base.ConnectionOptions.MARS, base.ConnectionOptions.Asynchronous);
					ServerInfo serverInfo2;
					if (useFailoverHost)
					{
						if (this.ServerProvidedFailOverPartner != null && serverInfo.ResolvedServerName != this.ServerProvidedFailOverPartner)
						{
							serverInfo.SetDerivedNames(string.Empty, this.ServerProvidedFailOverPartner);
						}
						serverInfo2 = serverInfo;
						this._timeoutErrorInternal.SetInternalSourceType(SqlConnectionInternalSourceType.Failover);
					}
					else
					{
						serverInfo2 = primaryServerInfo;
						this._timeoutErrorInternal.SetInternalSourceType(SqlConnectionInternalSourceType.Principle);
					}
					unchecked
					{
						try
						{
							this.AttemptOneLogin(serverInfo2, newPassword, newSecurePassword, false, timeoutTimer, true);
							if (this._routingInfo != null)
							{
								throw SQL.ROR_UnexpectedRoutingInfo(this);
							}
							break;
						}
						catch (SqlException ex)
						{
							if (this.IsDoNotRetryConnectError(ex) || timeout.IsExpired)
							{
								throw;
							}
							if (base.IsConnectionDoomed)
							{
								throw;
							}
							if (1 == num3 % 2 && timeout.MillisecondsRemaining <= (long)num)
							{
								throw;
							}
						}
						if (1 == num3 % 2)
						{
							Thread.Sleep(num);
							num = ((num < 500) ? (num * 2) : 1000);
						}
						num3++;
						useFailoverHost = !useFailoverHost;
					}
				}
				if (useFailoverHost && this.ServerProvidedFailOverPartner == null)
				{
					throw SQL.InvalidPartnerConfiguration(failoverHost, base.CurrentDatabase);
				}
				if (this.PoolGroupProviderInfo != null)
				{
					this.PoolGroupProviderInfo.FailoverCheck(this, useFailoverHost, connectionOptions, this.ServerProvidedFailOverPartner);
				}
				base.CurrentDataSource = (useFailoverHost ? failoverHost : primaryServerInfo.UserServerName);
			}
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x0006A35C File Offset: 0x0006855C
		private void ResolveExtendedServerName(ServerInfo serverInfo, bool aliasLookup, SqlConnectionString options)
		{
			if (serverInfo.ExtendedServerName == null)
			{
				string userServerName = serverInfo.UserServerName;
				string userProtocol = serverInfo.UserProtocol;
				serverInfo.SetDerivedNames(userProtocol, userServerName);
			}
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x0006A388 File Offset: 0x00068588
		private void AttemptOneLogin(ServerInfo serverInfo, string newPassword, SecureString newSecurePassword, bool ignoreSniOpenTimeout, TimeoutTimer timeout, bool withFailover = false)
		{
			this._routingInfo = null;
			this._parser._physicalStateObj.SniContext = SniContext.Snix_Connect;
			this._parser.Connect(serverInfo, this, ignoreSniOpenTimeout, timeout.LegacyTimerExpire, base.ConnectionOptions.Encrypt, base.ConnectionOptions.TrustServerCertificate, base.ConnectionOptions.IntegratedSecurity, withFailover);
			this._timeoutErrorInternal.EndPhase(SqlConnectionTimeoutErrorPhase.ConsumePreLoginHandshake);
			this._timeoutErrorInternal.SetAndBeginPhase(SqlConnectionTimeoutErrorPhase.LoginBegin);
			this._parser._physicalStateObj.SniContext = SniContext.Snix_Login;
			this.Login(serverInfo, timeout, newPassword, newSecurePassword);
			this._timeoutErrorInternal.EndPhase(SqlConnectionTimeoutErrorPhase.ProcessConnectionAuth);
			this._timeoutErrorInternal.SetAndBeginPhase(SqlConnectionTimeoutErrorPhase.PostLogin);
			this.CompleteLogin(!base.ConnectionOptions.Pooling);
			this._timeoutErrorInternal.EndPhase(SqlConnectionTimeoutErrorPhase.PostLogin);
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x0006A452 File Offset: 0x00068652
		protected override object ObtainAdditionalLocksForClose()
		{
			bool flag = !this.ThreadHasParserLockForClose;
			if (flag)
			{
				this._parserLock.Wait(false);
				this.ThreadHasParserLockForClose = true;
			}
			return flag;
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x0006A478 File Offset: 0x00068678
		protected override void ReleaseAdditionalLocksForClose(object lockToken)
		{
			if ((bool)lockToken)
			{
				this.ThreadHasParserLockForClose = false;
				this._parserLock.Release();
			}
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x0006A494 File Offset: 0x00068694
		internal bool GetSessionAndReconnectIfNeeded(SqlConnection parent, int timeout = 0)
		{
			if (this.ThreadHasParserLockForClose)
			{
				return false;
			}
			this._parserLock.Wait(false);
			this.ThreadHasParserLockForClose = true;
			bool releaseConnectionLock = true;
			bool flag;
			try
			{
				Task task = parent.ValidateAndReconnect(delegate
				{
					this.ThreadHasParserLockForClose = false;
					this._parserLock.Release();
					releaseConnectionLock = false;
				}, timeout);
				if (task != null)
				{
					AsyncHelper.WaitForCompletion(task, timeout, null, true);
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			finally
			{
				if (releaseConnectionLock)
				{
					this.ThreadHasParserLockForClose = false;
					this._parserLock.Release();
				}
			}
			return flag;
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x0006A528 File Offset: 0x00068728
		internal void BreakConnection()
		{
			SqlConnection connection = base.Connection;
			base.DoomThisConnection();
			if (connection != null)
			{
				connection.Close();
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001576 RID: 5494 RVA: 0x0006A54B File Offset: 0x0006874B
		internal bool IgnoreEnvChange
		{
			get
			{
				return this._routingInfo != null;
			}
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x0006A558 File Offset: 0x00068758
		internal void OnEnvChange(SqlEnvChange rec)
		{
			switch (rec.type)
			{
			case 1:
				if (!this._fConnectionOpen && this._recoverySessionData == null)
				{
					this._originalDatabase = rec.newValue;
				}
				base.CurrentDatabase = rec.newValue;
				return;
			case 2:
				if (!this._fConnectionOpen && this._recoverySessionData == null)
				{
					this._originalLanguage = rec.newValue;
				}
				this._currentLanguage = rec.newValue;
				return;
			case 3:
			case 5:
			case 6:
			case 8:
			case 9:
			case 10:
			case 11:
			case 12:
			case 14:
			case 16:
			case 17:
				break;
			case 4:
				this._currentPacketSize = int.Parse(rec.newValue, CultureInfo.InvariantCulture);
				return;
			case 7:
				if (this._currentSessionData != null)
				{
					this._currentSessionData._collation = rec.newCollation;
					return;
				}
				break;
			case 13:
				if (base.ConnectionOptions.ApplicationIntent == ApplicationIntent.ReadOnly)
				{
					throw SQL.ROR_FailoverNotSupportedServer(this);
				}
				this._currentFailoverPartner = rec.newValue;
				return;
			case 15:
				base.PromotedDTCToken = rec.newBinValue;
				return;
			case 18:
				if (this._currentSessionData != null)
				{
					this._currentSessionData.Reset();
					return;
				}
				break;
			case 19:
				this._instanceName = rec.newValue;
				return;
			case 20:
				if (string.IsNullOrEmpty(rec.newRoutingInfo.ServerName) || rec.newRoutingInfo.Protocol != 0 || rec.newRoutingInfo.Port == 0)
				{
					throw SQL.ROR_InvalidRoutingInfo(this);
				}
				this._routingInfo = rec.newRoutingInfo;
				break;
			default:
				return;
			}
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x0006A6DC File Offset: 0x000688DC
		internal void OnLoginAck(SqlLoginAck rec)
		{
			this._loginAck = rec;
			if (this._recoverySessionData != null && this._recoverySessionData._tdsVersion != rec.tdsVersion)
			{
				throw SQL.CR_TDSVersionNotPreserved(this);
			}
			if (this._currentSessionData != null)
			{
				this._currentSessionData._tdsVersion = rec.tdsVersion;
			}
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x0006A72C File Offset: 0x0006892C
		internal void OnFeatureExtAck(int featureId, byte[] data)
		{
			if (this._routingInfo != null)
			{
				return;
			}
			switch (featureId)
			{
			case 1:
			{
				if (!this._sessionRecoveryRequested)
				{
					throw SQL.ParsingError();
				}
				this._sessionRecoveryAcknowledged = true;
				int i = 0;
				while (i < data.Length)
				{
					byte b = data[i];
					i++;
					byte b2 = data[i];
					i++;
					int num;
					if (b2 == 255)
					{
						num = BitConverter.ToInt32(data, i);
						i += 4;
					}
					else
					{
						num = (int)b2;
					}
					byte[] array = new byte[num];
					Buffer.BlockCopy(data, i, array, 0, num);
					i += num;
					if (this._recoverySessionData == null)
					{
						this._currentSessionData._initialState[(int)b] = array;
					}
					else
					{
						this._currentSessionData._delta[(int)b] = new SessionStateRecord
						{
							_data = array,
							_dataLength = num,
							_recoverable = true,
							_version = 0U
						};
						this._currentSessionData._deltaDirty = true;
					}
				}
				return;
			}
			case 2:
				if (!this._federatedAuthenticationRequested)
				{
					throw SQL.ParsingErrorFeatureId(ParsingErrorState.UnrequestedFeatureAckReceived, featureId);
				}
				if (this._fedAuthFeatureExtensionData.Value.libraryType != TdsEnums.FedAuthLibrary.SecurityToken)
				{
					throw SQL.ParsingErrorLibraryType(ParsingErrorState.FedAuthFeatureAckUnknownLibraryType, (int)this._fedAuthFeatureExtensionData.Value.libraryType);
				}
				if (data.Length != 0)
				{
					throw SQL.ParsingError(ParsingErrorState.FedAuthFeatureAckContainsExtraData);
				}
				this._federatedAuthenticationAcknowledged = true;
				return;
			case 5:
				if (data.Length < 1)
				{
					throw SQL.ParsingError();
				}
				base.IsGlobalTransaction = true;
				if (1 == data[0])
				{
					base.IsGlobalTransactionsEnabledForServer = true;
					return;
				}
				return;
			}
			throw SQL.ParsingError();
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x0006A893 File Offset: 0x00068A93
		// (set) Token: 0x0600157B RID: 5499 RVA: 0x0006A8A7 File Offset: 0x00068AA7
		internal bool ThreadHasParserLockForClose
		{
			get
			{
				return this._threadIdOwningParserLock == Thread.CurrentThread.ManagedThreadId;
			}
			set
			{
				if (value)
				{
					this._threadIdOwningParserLock = Thread.CurrentThread.ManagedThreadId;
					return;
				}
				if (this._threadIdOwningParserLock == Thread.CurrentThread.ManagedThreadId)
				{
					this._threadIdOwningParserLock = -1;
				}
			}
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x0006A8D6 File Offset: 0x00068AD6
		internal override bool TryReplaceConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			return base.TryOpenConnectionInternal(outerConnection, connectionFactory, retry, userOptions);
		}

		// Token: 0x04000E51 RID: 3665
		private readonly SqlConnectionPoolGroupProviderInfo _poolGroupProviderInfo;

		// Token: 0x04000E52 RID: 3666
		private TdsParser _parser;

		// Token: 0x04000E53 RID: 3667
		private SqlLoginAck _loginAck;

		// Token: 0x04000E54 RID: 3668
		private SqlCredential _credential;

		// Token: 0x04000E55 RID: 3669
		private FederatedAuthenticationFeatureExtensionData? _fedAuthFeatureExtensionData;

		// Token: 0x04000E56 RID: 3670
		private bool _sessionRecoveryRequested;

		// Token: 0x04000E57 RID: 3671
		internal bool _sessionRecoveryAcknowledged;

		// Token: 0x04000E58 RID: 3672
		internal SessionData _currentSessionData;

		// Token: 0x04000E59 RID: 3673
		private SessionData _recoverySessionData;

		// Token: 0x04000E5A RID: 3674
		internal bool _fedAuthRequired;

		// Token: 0x04000E5B RID: 3675
		internal bool _federatedAuthenticationRequested;

		// Token: 0x04000E5C RID: 3676
		internal bool _federatedAuthenticationAcknowledged;

		// Token: 0x04000E5D RID: 3677
		internal byte[] _accessTokenInBytes;

		// Token: 0x04000E5E RID: 3678
		private static readonly HashSet<int> s_transientErrors = new HashSet<int> { 4060, 10928, 10929, 40197, 40501, 40613 };

		// Token: 0x04000E5F RID: 3679
		private bool _fConnectionOpen;

		// Token: 0x04000E60 RID: 3680
		private bool _fResetConnection;

		// Token: 0x04000E61 RID: 3681
		private string _originalDatabase;

		// Token: 0x04000E62 RID: 3682
		private string _currentFailoverPartner;

		// Token: 0x04000E63 RID: 3683
		private string _originalLanguage;

		// Token: 0x04000E64 RID: 3684
		private string _currentLanguage;

		// Token: 0x04000E65 RID: 3685
		private int _currentPacketSize;

		// Token: 0x04000E66 RID: 3686
		private int _asyncCommandCount;

		// Token: 0x04000E67 RID: 3687
		private string _instanceName = string.Empty;

		// Token: 0x04000E68 RID: 3688
		private DbConnectionPoolIdentity _identity;

		// Token: 0x04000E69 RID: 3689
		internal SqlInternalConnectionTds.SyncAsyncLock _parserLock = new SqlInternalConnectionTds.SyncAsyncLock();

		// Token: 0x04000E6A RID: 3690
		private int _threadIdOwningParserLock = -1;

		// Token: 0x04000E6B RID: 3691
		private SqlConnectionTimeoutErrorInternal _timeoutErrorInternal;

		// Token: 0x04000E6C RID: 3692
		internal Guid _clientConnectionId = Guid.Empty;

		// Token: 0x04000E6D RID: 3693
		private RoutingInfo _routingInfo;

		// Token: 0x04000E6E RID: 3694
		private Guid _originalClientConnectionId = Guid.Empty;

		// Token: 0x04000E6F RID: 3695
		private string _routingDestination;

		// Token: 0x04000E70 RID: 3696
		private readonly TimeoutTimer _timeout;

		// Token: 0x020001B9 RID: 441
		internal class SyncAsyncLock
		{
			// Token: 0x0600157E RID: 5502 RVA: 0x0006A944 File Offset: 0x00068B44
			internal void Wait(bool canReleaseFromAnyThread)
			{
				Monitor.Enter(this._semaphore);
				if (canReleaseFromAnyThread || this._semaphore.CurrentCount == 0)
				{
					this._semaphore.Wait();
					if (canReleaseFromAnyThread)
					{
						Monitor.Exit(this._semaphore);
						return;
					}
					this._semaphore.Release();
				}
			}

			// Token: 0x0600157F RID: 5503 RVA: 0x0006A994 File Offset: 0x00068B94
			internal void Wait(bool canReleaseFromAnyThread, int timeout, ref bool lockTaken)
			{
				lockTaken = false;
				bool flag = false;
				try
				{
					Monitor.TryEnter(this._semaphore, timeout, ref flag);
					if (flag)
					{
						if (canReleaseFromAnyThread || this._semaphore.CurrentCount == 0)
						{
							if (this._semaphore.Wait(timeout))
							{
								if (canReleaseFromAnyThread)
								{
									Monitor.Exit(this._semaphore);
									flag = false;
								}
								else
								{
									this._semaphore.Release();
								}
								lockTaken = true;
							}
						}
						else
						{
							lockTaken = true;
						}
					}
				}
				finally
				{
					if (!lockTaken && flag)
					{
						Monitor.Exit(this._semaphore);
					}
				}
			}

			// Token: 0x06001580 RID: 5504 RVA: 0x0006AA24 File Offset: 0x00068C24
			internal void Release()
			{
				if (this._semaphore.CurrentCount == 0)
				{
					this._semaphore.Release();
					return;
				}
				Monitor.Exit(this._semaphore);
			}

			// Token: 0x17000410 RID: 1040
			// (get) Token: 0x06001581 RID: 5505 RVA: 0x0006AA4B File Offset: 0x00068C4B
			internal bool CanBeReleasedFromAnyThread
			{
				get
				{
					return this._semaphore.CurrentCount == 0;
				}
			}

			// Token: 0x06001582 RID: 5506 RVA: 0x0006AA5B File Offset: 0x00068C5B
			internal bool ThreadMayHaveLock()
			{
				return Monitor.IsEntered(this._semaphore) || this._semaphore.CurrentCount == 0;
			}

			// Token: 0x04000E71 RID: 3697
			private SemaphoreSlim _semaphore = new SemaphoreSlim(1);
		}
	}
}
