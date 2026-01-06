using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.SqlClient
{
	// Token: 0x02000220 RID: 544
	internal abstract class TdsParserStateObject
	{
		// Token: 0x0600190D RID: 6413 RVA: 0x0007E30C File Offset: 0x0007C50C
		internal TdsParserStateObject(TdsParser parser)
		{
			this._parser = parser;
			this.SetPacketSize(4096);
			this.IncrementPendingCallbacks();
			this._lastSuccessfulIOTimer = new LastIOTimer();
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x0007E3C8 File Offset: 0x0007C5C8
		internal TdsParserStateObject(TdsParser parser, TdsParserStateObject physicalConnection, bool async)
		{
			this._parser = parser;
			this.SniContext = SniContext.Snix_GetMarsSession;
			this.SetPacketSize(this._parser._physicalStateObj._outBuff.Length);
			this.CreateSessionHandle(physicalConnection, async);
			if (this.IsFailedHandle())
			{
				this.AddError(parser.ProcessSNIError(this));
				this.ThrowExceptionAndWarning(false, false);
			}
			this.IncrementPendingCallbacks();
			this._lastSuccessfulIOTimer = parser._physicalStateObj._lastSuccessfulIOTimer;
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x0600190F RID: 6415 RVA: 0x0007E4C2 File Offset: 0x0007C6C2
		// (set) Token: 0x06001910 RID: 6416 RVA: 0x0007E4CA File Offset: 0x0007C6CA
		internal bool BcpLock
		{
			get
			{
				return this._bcpLock;
			}
			set
			{
				this._bcpLock = value;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001911 RID: 6417 RVA: 0x0007E4D3 File Offset: 0x0007C6D3
		internal bool HasOpenResult
		{
			get
			{
				return this._hasOpenResult;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001912 RID: 6418 RVA: 0x0007E4DB File Offset: 0x0007C6DB
		internal bool IsOrphaned
		{
			get
			{
				return this._activateCount != 0 && !this._owner.IsAlive;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (set) Token: 0x06001913 RID: 6419 RVA: 0x0007E4F8 File Offset: 0x0007C6F8
		internal object Owner
		{
			set
			{
				SqlDataReader sqlDataReader = value as SqlDataReader;
				if (sqlDataReader == null)
				{
					this._readerState = null;
				}
				else
				{
					this._readerState = sqlDataReader._sharedState;
				}
				this._owner.Target = value;
			}
		}

		// Token: 0x06001914 RID: 6420
		internal abstract uint DisabeSsl();

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001915 RID: 6421 RVA: 0x0007E530 File Offset: 0x0007C730
		internal bool HasOwner
		{
			get
			{
				return this._owner.IsAlive;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001916 RID: 6422 RVA: 0x0007E53D File Offset: 0x0007C73D
		internal TdsParser Parser
		{
			get
			{
				return this._parser;
			}
		}

		// Token: 0x06001917 RID: 6423
		internal abstract uint EnableMars(ref uint info);

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001918 RID: 6424 RVA: 0x0007E545 File Offset: 0x0007C745
		// (set) Token: 0x06001919 RID: 6425 RVA: 0x0007E54D File Offset: 0x0007C74D
		internal SniContext SniContext
		{
			get
			{
				return this._sniContext;
			}
			set
			{
				this._sniContext = value;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x0600191A RID: 6426
		internal abstract uint Status { get; }

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x0600191B RID: 6427
		internal abstract object SessionHandle { get; }

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x0600191C RID: 6428 RVA: 0x0007E556 File Offset: 0x0007C756
		internal bool TimeoutHasExpired
		{
			get
			{
				return TdsParserStaticMethods.TimeoutHasExpired(this._timeoutTime);
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x0600191D RID: 6429 RVA: 0x0007E563 File Offset: 0x0007C763
		// (set) Token: 0x0600191E RID: 6430 RVA: 0x0007E58C File Offset: 0x0007C78C
		internal long TimeoutTime
		{
			get
			{
				if (this._timeoutMilliseconds != 0L)
				{
					this._timeoutTime = TdsParserStaticMethods.GetTimeout(this._timeoutMilliseconds);
					this._timeoutMilliseconds = 0L;
				}
				return this._timeoutTime;
			}
			set
			{
				this._timeoutMilliseconds = 0L;
				this._timeoutTime = value;
			}
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x0007E5A0 File Offset: 0x0007C7A0
		internal int GetTimeoutRemaining()
		{
			int num;
			if (this._timeoutMilliseconds != 0L)
			{
				num = (int)Math.Min(2147483647L, this._timeoutMilliseconds);
				this._timeoutTime = TdsParserStaticMethods.GetTimeout(this._timeoutMilliseconds);
				this._timeoutMilliseconds = 0L;
			}
			else
			{
				num = TdsParserStaticMethods.GetTimeoutMilliseconds(this._timeoutTime);
			}
			return num;
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x0007E5F0 File Offset: 0x0007C7F0
		internal bool TryStartNewRow(bool isNullCompressed, int nullBitmapColumnsCount = 0)
		{
			if (this._snapshot != null)
			{
				this._snapshot.CloneNullBitmapInfo();
			}
			if (isNullCompressed)
			{
				if (!this._nullBitmapInfo.TryInitialize(this, nullBitmapColumnsCount))
				{
					return false;
				}
			}
			else
			{
				this._nullBitmapInfo.Clean();
			}
			return true;
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x0007E628 File Offset: 0x0007C828
		internal bool IsRowTokenReady()
		{
			int num = Math.Min(this._inBytesPacket, this._inBytesRead - this._inBytesUsed) - 1;
			if (num > 0)
			{
				if (this._inBuff[this._inBytesUsed] == 209)
				{
					return true;
				}
				if (this._inBuff[this._inBytesUsed] == 210)
				{
					return 1 + (this._cleanupMetaData.Length + 7) / 8 <= num;
				}
			}
			return false;
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x0007E697 File Offset: 0x0007C897
		internal bool IsNullCompressionBitSet(int columnOrdinal)
		{
			return this._nullBitmapInfo.IsGuaranteedNull(columnOrdinal);
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x0007E6A5 File Offset: 0x0007C8A5
		internal void Activate(object owner)
		{
			this.Owner = owner;
			Interlocked.Increment(ref this._activateCount);
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x0007E6BC File Offset: 0x0007C8BC
		internal void Cancel(object caller)
		{
			bool flag = false;
			try
			{
				while (!flag && this._parser.State != TdsParserState.Closed && this._parser.State != TdsParserState.Broken)
				{
					Monitor.TryEnter(this, 100, ref flag);
					if (flag && !this._cancelled && this._cancellationOwner.Target == caller)
					{
						this._cancelled = true;
						if (this._pendingData && !this._attentionSent)
						{
							bool flag2 = false;
							while (!flag2 && this._parser.State != TdsParserState.Closed && this._parser.State != TdsParserState.Broken)
							{
								try
								{
									this._parser.Connection._parserLock.Wait(false, 100, ref flag2);
									if (flag2)
									{
										this._parser.Connection.ThreadHasParserLockForClose = true;
										this.SendAttention(false);
									}
								}
								finally
								{
									if (flag2)
									{
										if (this._parser.Connection.ThreadHasParserLockForClose)
										{
											this._parser.Connection.ThreadHasParserLockForClose = false;
										}
										this._parser.Connection._parserLock.Release();
									}
								}
							}
						}
					}
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x0007E818 File Offset: 0x0007CA18
		internal void CancelRequest()
		{
			this.ResetBuffer();
			this._outputPacketNumber = 1;
			if (!this._bulkCopyWriteTimeout)
			{
				this.SendAttention(false);
				this.Parser.ProcessPendingAck(this);
			}
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x0007E844 File Offset: 0x0007CA44
		public void CheckSetResetConnectionState(uint error, CallbackType callbackType)
		{
			if (this._fResetEventOwned)
			{
				if (callbackType == CallbackType.Read && error == 0U)
				{
					this._parser._fResetConnection = false;
					this._fResetConnectionSent = false;
					this._fResetEventOwned = !this._parser._resetConnectionEvent.Set();
				}
				if (error != 0U)
				{
					this._fResetConnectionSent = false;
					this._fResetEventOwned = !this._parser._resetConnectionEvent.Set();
				}
			}
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x0007E8BA File Offset: 0x0007CABA
		internal void CloseSession()
		{
			this.ResetCancelAndProcessAttention();
			this.Parser.PutSession(this);
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x0007E8D0 File Offset: 0x0007CAD0
		private void ResetCancelAndProcessAttention()
		{
			lock (this)
			{
				this._cancelled = false;
				this._cancellationOwner.Target = null;
				if (this._attentionSent)
				{
					this.Parser.ProcessPendingAck(this);
				}
				this._internalTimeout = false;
			}
		}

		// Token: 0x06001929 RID: 6441
		internal abstract void CreatePhysicalSNIHandle(string serverName, bool ignoreSniOpenTimeout, long timerExpire, out byte[] instanceName, ref byte[] spnBuffer, bool flushCache, bool async, bool fParallel, bool isIntegratedSecurity = false);

		// Token: 0x0600192A RID: 6442
		internal abstract uint SniGetConnectionId(ref Guid clientConnectionId);

		// Token: 0x0600192B RID: 6443
		internal abstract bool IsFailedHandle();

		// Token: 0x0600192C RID: 6444
		protected abstract void CreateSessionHandle(TdsParserStateObject physicalConnection, bool async);

		// Token: 0x0600192D RID: 6445
		protected abstract void FreeGcHandle(int remaining, bool release);

		// Token: 0x0600192E RID: 6446
		internal abstract uint EnableSsl(ref uint info);

		// Token: 0x0600192F RID: 6447
		internal abstract uint WaitForSSLHandShakeToComplete();

		// Token: 0x06001930 RID: 6448
		internal abstract void Dispose();

		// Token: 0x06001931 RID: 6449
		internal abstract void DisposePacketCache();

		// Token: 0x06001932 RID: 6450
		internal abstract bool IsPacketEmpty(object readPacket);

		// Token: 0x06001933 RID: 6451
		internal abstract object ReadSyncOverAsync(int timeoutRemaining, out uint error);

		// Token: 0x06001934 RID: 6452
		internal abstract object ReadAsync(out uint error, ref object handle);

		// Token: 0x06001935 RID: 6453
		internal abstract uint CheckConnection();

		// Token: 0x06001936 RID: 6454
		internal abstract uint SetConnectionBufferSize(ref uint unsignedPacketSize);

		// Token: 0x06001937 RID: 6455
		internal abstract void ReleasePacket(object syncReadPacket);

		// Token: 0x06001938 RID: 6456
		protected abstract uint SNIPacketGetData(object packet, byte[] _inBuff, ref uint dataSize);

		// Token: 0x06001939 RID: 6457
		internal abstract object GetResetWritePacket();

		// Token: 0x0600193A RID: 6458
		internal abstract void ClearAllWritePackets();

		// Token: 0x0600193B RID: 6459
		internal abstract object AddPacketToPendingList(object packet);

		// Token: 0x0600193C RID: 6460
		protected abstract void RemovePacketFromPendingList(object pointer);

		// Token: 0x0600193D RID: 6461
		internal abstract uint GenerateSspiClientContext(byte[] receivedBuff, uint receivedLength, ref byte[] sendBuff, ref uint sendLength, byte[] _sniSpnBuffer);

		// Token: 0x0600193E RID: 6462 RVA: 0x0007E938 File Offset: 0x0007CB38
		internal bool Deactivate()
		{
			bool flag = false;
			try
			{
				TdsParserState state = this.Parser.State;
				if (state != TdsParserState.Broken && state != TdsParserState.Closed)
				{
					if (this._pendingData)
					{
						this.Parser.DrainData(this);
					}
					if (this.HasOpenResult)
					{
						this.DecrementOpenResultCount();
					}
					this.ResetCancelAndProcessAttention();
					flag = true;
				}
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
			}
			return flag;
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x0007E9A4 File Offset: 0x0007CBA4
		internal void RemoveOwner()
		{
			if (this._parser.MARSOn)
			{
				Interlocked.Decrement(ref this._activateCount);
			}
			this.Owner = null;
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x0007E9C6 File Offset: 0x0007CBC6
		internal void DecrementOpenResultCount()
		{
			if (this._executedUnderTransaction == null)
			{
				this._parser.DecrementNonTransactedOpenResultCount();
			}
			else
			{
				this._executedUnderTransaction.DecrementAndObtainOpenResultCount();
				this._executedUnderTransaction = null;
			}
			this._hasOpenResult = false;
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x0007E9F8 File Offset: 0x0007CBF8
		internal int DecrementPendingCallbacks(bool release)
		{
			int num = Interlocked.Decrement(ref this._pendingCallbacks);
			this.FreeGcHandle(num, release);
			return num;
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x0007EA1C File Offset: 0x0007CC1C
		internal void DisposeCounters()
		{
			Timer networkPacketTimeout = this._networkPacketTimeout;
			if (networkPacketTimeout != null)
			{
				this._networkPacketTimeout = null;
				networkPacketTimeout.Dispose();
			}
			if (Volatile.Read(ref this._readingCount) > 0)
			{
				SpinWait.SpinUntil(() => Volatile.Read(ref this._readingCount) == 0);
			}
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x0007EA5F File Offset: 0x0007CC5F
		internal int IncrementAndObtainOpenResultCount(SqlInternalTransaction transaction)
		{
			this._hasOpenResult = true;
			if (transaction == null)
			{
				return this._parser.IncrementNonTransactedOpenResultCount();
			}
			this._executedUnderTransaction = transaction;
			return transaction.IncrementAndObtainOpenResultCount();
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x0007EA84 File Offset: 0x0007CC84
		internal int IncrementPendingCallbacks()
		{
			return Interlocked.Increment(ref this._pendingCallbacks);
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x0007EA91 File Offset: 0x0007CC91
		internal void SetTimeoutSeconds(int timeout)
		{
			this.SetTimeoutMilliseconds((long)timeout * 1000L);
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x0007EAA2 File Offset: 0x0007CCA2
		internal void SetTimeoutMilliseconds(long timeout)
		{
			if (timeout <= 0L)
			{
				this._timeoutMilliseconds = 0L;
				this._timeoutTime = long.MaxValue;
				return;
			}
			this._timeoutMilliseconds = timeout;
			this._timeoutTime = 0L;
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x0007EAD0 File Offset: 0x0007CCD0
		internal void StartSession(object cancellationOwner)
		{
			this._cancellationOwner.Target = cancellationOwner;
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x0007EADE File Offset: 0x0007CCDE
		internal void ThrowExceptionAndWarning(bool callerHasConnectionLock = false, bool asyncClose = false)
		{
			this._parser.ThrowExceptionAndWarning(this, callerHasConnectionLock, asyncClose);
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x0007EAF0 File Offset: 0x0007CCF0
		internal Task ExecuteFlush()
		{
			Task task2;
			lock (this)
			{
				if (this._cancelled && 1 == this._outputPacketNumber)
				{
					this.ResetBuffer();
					this._cancelled = false;
					throw SQL.OperationCancelled();
				}
				Task task = this.WritePacket(1, false);
				if (task == null)
				{
					this._pendingData = true;
					this._messageStatus = 0;
					task2 = null;
				}
				else
				{
					task2 = AsyncHelper.CreateContinuationTask(task, delegate
					{
						this._pendingData = true;
						this._messageStatus = 0;
					}, null, null);
				}
			}
			return task2;
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x0007EB80 File Offset: 0x0007CD80
		internal bool TryProcessHeader()
		{
			if (this._partialHeaderBytesRead > 0 || this._inBytesUsed + this._inputHeaderLen > this._inBytesRead)
			{
				for (;;)
				{
					int num = Math.Min(this._inBytesRead - this._inBytesUsed, this._inputHeaderLen - this._partialHeaderBytesRead);
					Buffer.BlockCopy(this._inBuff, this._inBytesUsed, this._partialHeaderBuffer, this._partialHeaderBytesRead, num);
					this._partialHeaderBytesRead += num;
					this._inBytesUsed += num;
					if (this._partialHeaderBytesRead == this._inputHeaderLen)
					{
						this._partialHeaderBytesRead = 0;
						this._inBytesPacket = (((int)this._partialHeaderBuffer[2] << 8) | (int)this._partialHeaderBuffer[3]) - this._inputHeaderLen;
						this._messageStatus = this._partialHeaderBuffer[1];
					}
					else
					{
						if (this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
						{
							break;
						}
						if (!this.TryReadNetworkPacket())
						{
							return false;
						}
						if (this._internalTimeout)
						{
							goto Block_5;
						}
					}
					if (this._partialHeaderBytesRead == 0)
					{
						goto Block_6;
					}
				}
				this.ThrowExceptionAndWarning(false, false);
				return true;
				Block_5:
				this.ThrowExceptionAndWarning(false, false);
				return true;
				Block_6:;
			}
			else
			{
				this._messageStatus = this._inBuff[this._inBytesUsed + 1];
				this._inBytesPacket = (((int)this._inBuff[this._inBytesUsed + 2] << 8) | (int)this._inBuff[this._inBytesUsed + 2 + 1]) - this._inputHeaderLen;
				this._inBytesUsed += this._inputHeaderLen;
			}
			if (this._inBytesPacket < 0)
			{
				throw SQL.ParsingError();
			}
			return true;
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x0007ED04 File Offset: 0x0007CF04
		internal bool TryPrepareBuffer()
		{
			if (this._inBytesPacket == 0 && this._inBytesUsed < this._inBytesRead && !this.TryProcessHeader())
			{
				return false;
			}
			if (this._inBytesUsed == this._inBytesRead)
			{
				if (this._inBytesPacket > 0)
				{
					if (!this.TryReadNetworkPacket())
					{
						return false;
					}
				}
				else if (this._inBytesPacket == 0)
				{
					if (!this.TryReadNetworkPacket())
					{
						return false;
					}
					if (!this.TryProcessHeader())
					{
						return false;
					}
					if (this._inBytesUsed == this._inBytesRead && !this.TryReadNetworkPacket())
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x0007ED87 File Offset: 0x0007CF87
		internal void ResetBuffer()
		{
			this._outBytesUsed = this._outputHeaderLen;
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x0007ED98 File Offset: 0x0007CF98
		internal bool SetPacketSize(int size)
		{
			if (size > 32768)
			{
				throw SQL.InvalidPacketSize();
			}
			if (this._inBuff == null || this._inBuff.Length != size)
			{
				if (this._inBuff == null)
				{
					this._inBuff = new byte[size];
					this._inBytesRead = 0;
					this._inBytesUsed = 0;
				}
				else if (size != this._inBuff.Length)
				{
					if (this._inBytesRead > this._inBytesUsed)
					{
						byte[] inBuff = this._inBuff;
						this._inBuff = new byte[size];
						int num = this._inBytesRead - this._inBytesUsed;
						if (inBuff.Length < this._inBytesUsed + num || this._inBuff.Length < num)
						{
							throw SQL.InvalidInternalPacketSize(string.Concat(new string[]
							{
								SR.GetString("Invalid internal packet size:"),
								" ",
								inBuff.Length.ToString(),
								", ",
								this._inBytesUsed.ToString(),
								", ",
								num.ToString(),
								", ",
								this._inBuff.Length.ToString()
							}));
						}
						Buffer.BlockCopy(inBuff, this._inBytesUsed, this._inBuff, 0, num);
						this._inBytesRead -= this._inBytesUsed;
						this._inBytesUsed = 0;
					}
					else
					{
						this._inBuff = new byte[size];
						this._inBytesRead = 0;
						this._inBytesUsed = 0;
					}
				}
				this._outBuff = new byte[size];
				this._outBytesUsed = this._outputHeaderLen;
				return true;
			}
			return false;
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x0007EF25 File Offset: 0x0007D125
		internal bool TryPeekByte(out byte value)
		{
			if (!this.TryReadByte(out value))
			{
				return false;
			}
			this._inBytesPacket++;
			this._inBytesUsed--;
			return true;
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x0007EF50 File Offset: 0x0007D150
		public bool TryReadByteArray(byte[] buff, int offset, int len)
		{
			int num;
			return this.TryReadByteArray(buff, offset, len, out num);
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x0007EF68 File Offset: 0x0007D168
		public bool TryReadByteArray(byte[] buff, int offset, int len, out int totalRead)
		{
			totalRead = 0;
			while (len > 0)
			{
				if ((this._inBytesPacket == 0 || this._inBytesUsed == this._inBytesRead) && !this.TryPrepareBuffer())
				{
					return false;
				}
				int num = Math.Min(len, Math.Min(this._inBytesPacket, this._inBytesRead - this._inBytesUsed));
				if (buff != null)
				{
					Buffer.BlockCopy(this._inBuff, this._inBytesUsed, buff, offset + totalRead, num);
				}
				totalRead += num;
				this._inBytesUsed += num;
				this._inBytesPacket -= num;
				len -= num;
			}
			return this._messageStatus == 1 || (this._inBytesPacket != 0 && this._inBytesUsed != this._inBytesRead) || this.TryPrepareBuffer();
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x0007F034 File Offset: 0x0007D234
		internal bool TryReadByte(out byte value)
		{
			value = 0;
			if ((this._inBytesPacket == 0 || this._inBytesUsed == this._inBytesRead) && !this.TryPrepareBuffer())
			{
				return false;
			}
			this._inBytesPacket--;
			byte[] inBuff = this._inBuff;
			int inBytesUsed = this._inBytesUsed;
			this._inBytesUsed = inBytesUsed + 1;
			value = inBuff[inBytesUsed];
			return true;
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x0007F090 File Offset: 0x0007D290
		internal bool TryReadChar(out char value)
		{
			byte[] array;
			int num;
			if (this._inBytesUsed + 2 > this._inBytesRead || this._inBytesPacket < 2)
			{
				if (!this.TryReadByteArray(this._bTmp, 0, 2))
				{
					value = '\0';
					return false;
				}
				array = this._bTmp;
				num = 0;
			}
			else
			{
				array = this._inBuff;
				num = this._inBytesUsed;
				this._inBytesUsed += 2;
				this._inBytesPacket -= 2;
			}
			value = (char)(((int)array[num + 1] << 8) + (int)array[num]);
			return true;
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x0007F110 File Offset: 0x0007D310
		internal bool TryReadInt16(out short value)
		{
			byte[] array;
			int num;
			if (this._inBytesUsed + 2 > this._inBytesRead || this._inBytesPacket < 2)
			{
				if (!this.TryReadByteArray(this._bTmp, 0, 2))
				{
					value = 0;
					return false;
				}
				array = this._bTmp;
				num = 0;
			}
			else
			{
				array = this._inBuff;
				num = this._inBytesUsed;
				this._inBytesUsed += 2;
				this._inBytesPacket -= 2;
			}
			value = (short)(((int)array[num + 1] << 8) + (int)array[num]);
			return true;
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x0007F190 File Offset: 0x0007D390
		internal bool TryReadInt32(out int value)
		{
			if (this._inBytesUsed + 4 <= this._inBytesRead && this._inBytesPacket >= 4)
			{
				value = BitConverter.ToInt32(this._inBuff, this._inBytesUsed);
				this._inBytesUsed += 4;
				this._inBytesPacket -= 4;
				return true;
			}
			if (!this.TryReadByteArray(this._bTmp, 0, 4))
			{
				value = 0;
				return false;
			}
			value = BitConverter.ToInt32(this._bTmp, 0);
			return true;
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x0007F20C File Offset: 0x0007D40C
		internal bool TryReadInt64(out long value)
		{
			if ((this._inBytesPacket == 0 || this._inBytesUsed == this._inBytesRead) && !this.TryPrepareBuffer())
			{
				value = 0L;
				return false;
			}
			if (this._bTmpRead <= 0 && this._inBytesUsed + 8 <= this._inBytesRead && this._inBytesPacket >= 8)
			{
				value = BitConverter.ToInt64(this._inBuff, this._inBytesUsed);
				this._inBytesUsed += 8;
				this._inBytesPacket -= 8;
				return true;
			}
			int num = 0;
			if (!this.TryReadByteArray(this._bTmp, this._bTmpRead, 8 - this._bTmpRead, out num))
			{
				this._bTmpRead += num;
				value = 0L;
				return false;
			}
			this._bTmpRead = 0;
			value = BitConverter.ToInt64(this._bTmp, 0);
			return true;
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x0007F2DC File Offset: 0x0007D4DC
		internal bool TryReadUInt16(out ushort value)
		{
			byte[] array;
			int num;
			if (this._inBytesUsed + 2 > this._inBytesRead || this._inBytesPacket < 2)
			{
				if (!this.TryReadByteArray(this._bTmp, 0, 2))
				{
					value = 0;
					return false;
				}
				array = this._bTmp;
				num = 0;
			}
			else
			{
				array = this._inBuff;
				num = this._inBytesUsed;
				this._inBytesUsed += 2;
				this._inBytesPacket -= 2;
			}
			value = (ushort)(((int)array[num + 1] << 8) + (int)array[num]);
			return true;
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x0007F35C File Offset: 0x0007D55C
		internal bool TryReadUInt32(out uint value)
		{
			if ((this._inBytesPacket == 0 || this._inBytesUsed == this._inBytesRead) && !this.TryPrepareBuffer())
			{
				value = 0U;
				return false;
			}
			if (this._bTmpRead <= 0 && this._inBytesUsed + 4 <= this._inBytesRead && this._inBytesPacket >= 4)
			{
				value = BitConverter.ToUInt32(this._inBuff, this._inBytesUsed);
				this._inBytesUsed += 4;
				this._inBytesPacket -= 4;
				return true;
			}
			int num = 0;
			if (!this.TryReadByteArray(this._bTmp, this._bTmpRead, 4 - this._bTmpRead, out num))
			{
				this._bTmpRead += num;
				value = 0U;
				return false;
			}
			this._bTmpRead = 0;
			value = BitConverter.ToUInt32(this._bTmp, 0);
			return true;
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x0007F428 File Offset: 0x0007D628
		internal bool TryReadSingle(out float value)
		{
			if (this._inBytesUsed + 4 <= this._inBytesRead && this._inBytesPacket >= 4)
			{
				value = BitConverter.ToSingle(this._inBuff, this._inBytesUsed);
				this._inBytesUsed += 4;
				this._inBytesPacket -= 4;
				return true;
			}
			if (!this.TryReadByteArray(this._bTmp, 0, 4))
			{
				value = 0f;
				return false;
			}
			value = BitConverter.ToSingle(this._bTmp, 0);
			return true;
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x0007F4A8 File Offset: 0x0007D6A8
		internal bool TryReadDouble(out double value)
		{
			if (this._inBytesUsed + 8 <= this._inBytesRead && this._inBytesPacket >= 8)
			{
				value = BitConverter.ToDouble(this._inBuff, this._inBytesUsed);
				this._inBytesUsed += 8;
				this._inBytesPacket -= 8;
				return true;
			}
			if (!this.TryReadByteArray(this._bTmp, 0, 8))
			{
				value = 0.0;
				return false;
			}
			value = BitConverter.ToDouble(this._bTmp, 0);
			return true;
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x0007F52C File Offset: 0x0007D72C
		internal bool TryReadString(int length, out string value)
		{
			int num = length << 1;
			int num2 = 0;
			byte[] array;
			if (this._inBytesUsed + num > this._inBytesRead || this._inBytesPacket < num)
			{
				if (this._bTmp == null || this._bTmp.Length < num)
				{
					this._bTmp = new byte[num];
				}
				if (!this.TryReadByteArray(this._bTmp, 0, num))
				{
					value = null;
					return false;
				}
				array = this._bTmp;
			}
			else
			{
				array = this._inBuff;
				num2 = this._inBytesUsed;
				this._inBytesUsed += num;
				this._inBytesPacket -= num;
			}
			value = Encoding.Unicode.GetString(array, num2, num);
			return true;
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x0007F5D0 File Offset: 0x0007D7D0
		internal bool TryReadStringWithEncoding(int length, Encoding encoding, bool isPlp, out string value)
		{
			if (encoding == null)
			{
				if (isPlp)
				{
					ulong num;
					if (!this._parser.TrySkipPlpValue((ulong)((long)length), this, out num))
					{
						value = null;
						return false;
					}
				}
				else if (!this.TrySkipBytes(length))
				{
					value = null;
					return false;
				}
				this._parser.ThrowUnsupportedCollationEncountered(this);
			}
			byte[] array = null;
			int num2 = 0;
			if (isPlp)
			{
				if (!this.TryReadPlpBytes(ref array, 0, 2147483647, out length))
				{
					value = null;
					return false;
				}
			}
			else if (this._inBytesUsed + length > this._inBytesRead || this._inBytesPacket < length)
			{
				if (this._bTmp == null || this._bTmp.Length < length)
				{
					this._bTmp = new byte[length];
				}
				if (!this.TryReadByteArray(this._bTmp, 0, length))
				{
					value = null;
					return false;
				}
				array = this._bTmp;
			}
			else
			{
				array = this._inBuff;
				num2 = this._inBytesUsed;
				this._inBytesUsed += length;
				this._inBytesPacket -= length;
			}
			value = encoding.GetString(array, num2, length);
			return true;
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x0007F6C8 File Offset: 0x0007D8C8
		internal ulong ReadPlpLength(bool returnPlpNullIfNull)
		{
			ulong num;
			if (!this.TryReadPlpLength(returnPlpNullIfNull, out num))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return num;
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x0007F6E8 File Offset: 0x0007D8E8
		internal bool TryReadPlpLength(bool returnPlpNullIfNull, out ulong lengthLeft)
		{
			bool flag = false;
			if (this._longlen == 0UL)
			{
				long num;
				if (!this.TryReadInt64(out num))
				{
					lengthLeft = 0UL;
					return false;
				}
				this._longlen = (ulong)num;
			}
			if (this._longlen == 18446744073709551615UL)
			{
				this._longlen = 0UL;
				this._longlenleft = 0UL;
				flag = true;
			}
			else
			{
				uint num2;
				if (!this.TryReadUInt32(out num2))
				{
					lengthLeft = 0UL;
					return false;
				}
				if (num2 == 0U)
				{
					this._longlenleft = 0UL;
					this._longlen = 0UL;
				}
				else
				{
					this._longlenleft = (ulong)num2;
				}
			}
			if (flag && returnPlpNullIfNull)
			{
				lengthLeft = ulong.MaxValue;
				return true;
			}
			lengthLeft = this._longlenleft;
			return true;
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x0007F778 File Offset: 0x0007D978
		internal int ReadPlpBytesChunk(byte[] buff, int offset, int len)
		{
			int num = (int)Math.Min(this._longlenleft, (ulong)((long)len));
			int num2;
			bool flag = this.TryReadByteArray(buff, offset, num, out num2);
			this._longlenleft -= (ulong)((long)num);
			if (!flag)
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return num2;
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x0007F7B8 File Offset: 0x0007D9B8
		internal bool TryReadPlpBytes(ref byte[] buff, int offset, int len, out int totalBytesRead)
		{
			int num = 0;
			if (this._longlen == 0UL)
			{
				if (buff == null)
				{
					buff = Array.Empty<byte>();
				}
				totalBytesRead = 0;
				return true;
			}
			int i = len;
			if (buff == null && this._longlen != 18446744073709551614UL)
			{
				buff = new byte[Math.Min((int)this._longlen, len)];
			}
			if (this._longlenleft == 0UL)
			{
				ulong num2;
				if (!this.TryReadPlpLength(false, out num2))
				{
					totalBytesRead = 0;
					return false;
				}
				if (this._longlenleft == 0UL)
				{
					totalBytesRead = 0;
					return true;
				}
			}
			if (buff == null)
			{
				buff = new byte[this._longlenleft];
			}
			totalBytesRead = 0;
			while (i > 0)
			{
				int num3 = (int)Math.Min(this._longlenleft, (ulong)((long)i));
				if (buff.Length < offset + num3)
				{
					byte[] array = new byte[offset + num3];
					Buffer.BlockCopy(buff, 0, array, 0, offset);
					buff = array;
				}
				bool flag = this.TryReadByteArray(buff, offset, num3, out num);
				i -= num;
				offset += num;
				totalBytesRead += num;
				this._longlenleft -= (ulong)((long)num);
				if (!flag)
				{
					return false;
				}
				ulong num2;
				if (this._longlenleft == 0UL && !this.TryReadPlpLength(false, out num2))
				{
					return false;
				}
				if (this._longlenleft == 0UL)
				{
					break;
				}
			}
			return true;
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x0007F8D0 File Offset: 0x0007DAD0
		internal bool TrySkipLongBytes(long num)
		{
			while (num > 0L)
			{
				int num2 = (int)Math.Min(2147483647L, num);
				if (!this.TryReadByteArray(null, 0, num2))
				{
					return false;
				}
				num -= (long)num2;
			}
			return true;
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x0007F908 File Offset: 0x0007DB08
		internal bool TrySkipBytes(int num)
		{
			return this.TryReadByteArray(null, 0, num);
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x0007F913 File Offset: 0x0007DB13
		internal void SetSnapshot()
		{
			this._snapshot = new TdsParserStateObject.StateSnapshot(this);
			this._snapshot.Snap();
			this._snapshotReplay = false;
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x0007F933 File Offset: 0x0007DB33
		internal void ResetSnapshot()
		{
			this._snapshot = null;
			this._snapshotReplay = false;
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0007F944 File Offset: 0x0007DB44
		internal bool TryReadNetworkPacket()
		{
			if (this._snapshot != null)
			{
				if (this._snapshotReplay && this._snapshot.Replay())
				{
					return true;
				}
				this._inBuff = new byte[this._inBuff.Length];
			}
			if (this._syncOverAsync)
			{
				this.ReadSniSyncOverAsync();
				return true;
			}
			this.ReadSni(new TaskCompletionSource<object>());
			return false;
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x0007F99F File Offset: 0x0007DB9F
		internal void PrepareReplaySnapshot()
		{
			this._networkPacketTaskSource = null;
			this._snapshot.PrepareReplay();
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x0007F9B4 File Offset: 0x0007DBB4
		internal void ReadSniSyncOverAsync()
		{
			if (this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
			{
				throw ADP.ClosedConnectionError();
			}
			object obj = null;
			bool flag = false;
			try
			{
				Interlocked.Increment(ref this._readingCount);
				flag = true;
				uint num;
				obj = this.ReadSyncOverAsync(this.GetTimeoutRemaining(), out num);
				Interlocked.Decrement(ref this._readingCount);
				flag = false;
				if (this._parser.MARSOn)
				{
					this.CheckSetResetConnectionState(num, CallbackType.Read);
				}
				if (num == 0U)
				{
					this.ProcessSniPacket(obj, 0U);
				}
				else
				{
					this.ReadSniError(this, num);
				}
			}
			finally
			{
				if (flag)
				{
					Interlocked.Decrement(ref this._readingCount);
				}
				if (!this.IsPacketEmpty(obj))
				{
					this.ReleasePacket(obj);
				}
			}
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x0007FA70 File Offset: 0x0007DC70
		internal void OnConnectionClosed()
		{
			this.Parser.State = TdsParserState.Broken;
			this.Parser.Connection.BreakConnection();
			Interlocked.MemoryBarrier();
			TaskCompletionSource<object> taskCompletionSource = this._networkPacketTaskSource;
			if (taskCompletionSource != null)
			{
				taskCompletionSource.TrySetException(ADP.ExceptionWithStackTrace(ADP.ClosedConnectionError()));
			}
			taskCompletionSource = this._writeCompletionSource;
			if (taskCompletionSource != null)
			{
				taskCompletionSource.TrySetException(ADP.ExceptionWithStackTrace(ADP.ClosedConnectionError()));
			}
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x0007FAD8 File Offset: 0x0007DCD8
		private void OnTimeout(object state)
		{
			if (!this._internalTimeout)
			{
				this._internalTimeout = true;
				lock (this)
				{
					if (!this._attentionSent)
					{
						this.AddError(new SqlError(-2, 0, 11, this._parser.Server, this._parser.Connection.TimeoutErrorInternal.GetErrorMessage(), "", 0, 258U, null));
						TaskCompletionSource<object> source = this._networkPacketTaskSource;
						if (this._parser.Connection.IsInPool)
						{
							this._parser.State = TdsParserState.Broken;
							this._parser.Connection.BreakConnection();
							if (source != null)
							{
								source.TrySetCanceled();
							}
						}
						else if (this._parser.State == TdsParserState.OpenLoggedIn)
						{
							try
							{
								this.SendAttention(true);
							}
							catch (Exception ex)
							{
								if (!ADP.IsCatchableExceptionType(ex))
								{
									throw;
								}
								if (source != null)
								{
									source.TrySetCanceled();
								}
							}
						}
						if (source != null)
						{
							Task.Delay(5000).ContinueWith(delegate(Task _)
							{
								if (!source.Task.IsCompleted)
								{
									int num = this.IncrementPendingCallbacks();
									try
									{
										if (num == 3 && !source.Task.IsCompleted)
										{
											bool flag2 = false;
											try
											{
												this.CheckThrowSNIException();
											}
											catch (Exception ex2)
											{
												if (source.TrySetException(ex2))
												{
													flag2 = true;
												}
											}
											this._parser.State = TdsParserState.Broken;
											this._parser.Connection.BreakConnection();
											if (!flag2)
											{
												source.TrySetCanceled();
											}
										}
									}
									finally
									{
										this.DecrementPendingCallbacks(false);
									}
								}
							});
						}
					}
				}
			}
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x0007FC2C File Offset: 0x0007DE2C
		internal void ReadSni(TaskCompletionSource<object> completion)
		{
			this._networkPacketTaskSource = completion;
			Interlocked.MemoryBarrier();
			if (this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
			{
				throw ADP.ClosedConnectionError();
			}
			object obj = null;
			uint num = 0U;
			try
			{
				if (this._networkPacketTimeout == null)
				{
					this._networkPacketTimeout = ADP.UnsafeCreateTimer(new TimerCallback(this.OnTimeout), null, -1, -1);
				}
				int timeoutRemaining = this.GetTimeoutRemaining();
				if (timeoutRemaining > 0)
				{
					this.ChangeNetworkPacketTimeout(timeoutRemaining, -1);
				}
				object obj2 = null;
				Interlocked.Increment(ref this._readingCount);
				obj2 = this.SessionHandle;
				if (obj2 != null)
				{
					this.IncrementPendingCallbacks();
					obj = this.ReadAsync(out num, ref obj2);
					if (num != 0U && 997U != num)
					{
						this.DecrementPendingCallbacks(false);
					}
				}
				Interlocked.Decrement(ref this._readingCount);
				if (obj2 == null)
				{
					throw ADP.ClosedConnectionError();
				}
				if (num == 0U)
				{
					this.ReadAsyncCallback<object>(IntPtr.Zero, obj, 0U);
				}
				else if (997U != num)
				{
					this.ReadSniError(this, num);
					this._networkPacketTaskSource.TrySetResult(null);
					this.ChangeNetworkPacketTimeout(-1, -1);
				}
				else if (timeoutRemaining == 0)
				{
					this.ChangeNetworkPacketTimeout(0, -1);
				}
			}
			finally
			{
				if (!TdsParserStateObjectFactory.UseManagedSNI && !this.IsPacketEmpty(obj))
				{
					this.ReleasePacket(obj);
				}
			}
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x0007FD60 File Offset: 0x0007DF60
		internal bool IsConnectionAlive(bool throwOnException)
		{
			bool flag = true;
			if (DateTime.UtcNow.Ticks - this._lastSuccessfulIOTimer._value > 50000L)
			{
				if (this._parser == null || this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
				{
					flag = false;
					if (throwOnException)
					{
						throw SQL.ConnectionDoomed();
					}
				}
				else if (this._pendingCallbacks <= 1 && (this._parser.Connection == null || this._parser.Connection.IsInPool))
				{
					object emptyReadPacket = this.EmptyReadPacket;
					try
					{
						this.SniContext = SniContext.Snix_Connect;
						uint num = this.CheckConnection();
						if (num != 0U && num != 258U)
						{
							flag = false;
							if (throwOnException)
							{
								this.AddError(this._parser.ProcessSNIError(this));
								this.ThrowExceptionAndWarning(false, false);
							}
						}
						else
						{
							this._lastSuccessfulIOTimer._value = DateTime.UtcNow.Ticks;
						}
					}
					finally
					{
						if (!this.IsPacketEmpty(emptyReadPacket))
						{
							this.ReleasePacket(emptyReadPacket);
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x0007FE6C File Offset: 0x0007E06C
		internal bool ValidateSNIConnection()
		{
			if (this._parser == null || this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
			{
				return false;
			}
			if (DateTime.UtcNow.Ticks - this._lastSuccessfulIOTimer._value <= 50000L)
			{
				return true;
			}
			uint num = 0U;
			this.SniContext = SniContext.Snix_Connect;
			try
			{
				Interlocked.Increment(ref this._readingCount);
				num = this.CheckConnection();
			}
			finally
			{
				Interlocked.Decrement(ref this._readingCount);
			}
			return num == 0U || num == 258U;
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x0007FF0C File Offset: 0x0007E10C
		private void ReadSniError(TdsParserStateObject stateObj, uint error)
		{
			if (258U == error)
			{
				bool flag = false;
				if (this._internalTimeout)
				{
					flag = true;
				}
				else
				{
					stateObj._internalTimeout = true;
					this.AddError(new SqlError(-2, 0, 11, this._parser.Server, this._parser.Connection.TimeoutErrorInternal.GetErrorMessage(), "", 0, 258U, null));
					if (!stateObj._attentionSent)
					{
						if (stateObj.Parser.State == TdsParserState.OpenLoggedIn)
						{
							stateObj.SendAttention(true);
							object obj = null;
							bool flag2 = false;
							try
							{
								Interlocked.Increment(ref this._readingCount);
								flag2 = true;
								obj = this.ReadSyncOverAsync(stateObj.GetTimeoutRemaining(), out error);
								Interlocked.Decrement(ref this._readingCount);
								flag2 = false;
								if (error == 0U)
								{
									stateObj.ProcessSniPacket(obj, 0U);
									return;
								}
								flag = true;
								goto IL_0132;
							}
							finally
							{
								if (flag2)
								{
									Interlocked.Decrement(ref this._readingCount);
								}
								if (!this.IsPacketEmpty(obj))
								{
									this.ReleasePacket(obj);
								}
							}
						}
						if (this._parser._loginWithFailover)
						{
							this._parser.Disconnect();
						}
						else if (this._parser.State == TdsParserState.OpenNotLoggedIn && this._parser.Connection.ConnectionOptions.MultiSubnetFailover)
						{
							this._parser.Disconnect();
						}
						else
						{
							flag = true;
						}
					}
				}
				IL_0132:
				if (flag)
				{
					this._parser.State = TdsParserState.Broken;
					this._parser.Connection.BreakConnection();
				}
			}
			else
			{
				this.AddError(this._parser.ProcessSNIError(stateObj));
			}
			this.ThrowExceptionAndWarning(false, false);
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x00080098 File Offset: 0x0007E298
		public void ProcessSniPacket(object packet, uint error)
		{
			if (error != 0U)
			{
				if (this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken)
				{
					return;
				}
				this.AddError(this._parser.ProcessSNIError(this));
				return;
			}
			else
			{
				uint num = 0U;
				if (this.SNIPacketGetData(packet, this._inBuff, ref num) != 0U)
				{
					throw SQL.ParsingError();
				}
				if ((long)this._inBuff.Length < (long)((ulong)num))
				{
					throw SQL.InvalidInternalPacketSize(SR.GetString("Invalid array size."));
				}
				this._lastSuccessfulIOTimer._value = DateTime.UtcNow.Ticks;
				this._inBytesRead = (int)num;
				this._inBytesUsed = 0;
				if (this._snapshot != null)
				{
					this._snapshot.PushBuffer(this._inBuff, this._inBytesRead);
					if (this._snapshotReplay)
					{
						this._snapshot.Replay();
					}
				}
				this.SniReadStatisticsAndTracing();
				return;
			}
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x0008016C File Offset: 0x0007E36C
		private void ChangeNetworkPacketTimeout(int dueTime, int period)
		{
			Timer networkPacketTimeout = this._networkPacketTimeout;
			if (networkPacketTimeout != null)
			{
				try
				{
					networkPacketTimeout.Change(dueTime, period);
				}
				catch (ObjectDisposedException)
				{
				}
			}
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x000801A4 File Offset: 0x0007E3A4
		private void SetBufferSecureStrings()
		{
			if (this._securePasswords != null)
			{
				for (int i = 0; i < this._securePasswords.Length; i++)
				{
					if (this._securePasswords[i] != null)
					{
						IntPtr intPtr = IntPtr.Zero;
						try
						{
							intPtr = Marshal.SecureStringToBSTR(this._securePasswords[i]);
							byte[] array = new byte[this._securePasswords[i].Length * 2];
							Marshal.Copy(intPtr, array, 0, this._securePasswords[i].Length * 2);
							TdsParserStaticMethods.ObfuscatePassword(array);
							array.CopyTo(this._outBuff, this._securePasswordOffsetsInBuffer[i]);
						}
						finally
						{
							Marshal.ZeroFreeBSTR(intPtr);
						}
					}
				}
			}
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x00080250 File Offset: 0x0007E450
		public void ReadAsyncCallback<T>(T packet, uint error)
		{
			this.ReadAsyncCallback<T>(IntPtr.Zero, packet, error);
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x00080260 File Offset: 0x0007E460
		public void ReadAsyncCallback<T>(IntPtr key, T packet, uint error)
		{
			TaskCompletionSource<object> source = this._networkPacketTaskSource;
			if (source == null && this._parser._pMarsPhysicalConObj == this)
			{
				return;
			}
			bool flag = true;
			try
			{
				if (this._parser.MARSOn)
				{
					this.CheckSetResetConnectionState(error, CallbackType.Read);
				}
				this.ChangeNetworkPacketTimeout(-1, -1);
				this.ProcessSniPacket(packet, error);
			}
			catch (Exception ex)
			{
				flag = ADP.IsCatchableExceptionType(ex);
				throw;
			}
			finally
			{
				int num = this.DecrementPendingCallbacks(false);
				if (flag && source != null && num < 2)
				{
					if (error == 0U)
					{
						if (this._executionContext != null)
						{
							ExecutionContext.Run(this._executionContext, delegate(object state)
							{
								source.TrySetResult(null);
							}, null);
						}
						else
						{
							source.TrySetResult(null);
						}
					}
					else if (this._executionContext != null)
					{
						ExecutionContext.Run(this._executionContext, delegate(object state)
						{
							this.ReadAsyncCallbackCaptureException(source);
						}, null);
					}
					else
					{
						this.ReadAsyncCallbackCaptureException(source);
					}
				}
			}
		}

		// Token: 0x06001972 RID: 6514
		protected abstract bool CheckPacket(object packet, TaskCompletionSource<object> source);

		// Token: 0x06001973 RID: 6515 RVA: 0x0008036C File Offset: 0x0007E56C
		private void ReadAsyncCallbackCaptureException(TaskCompletionSource<object> source)
		{
			bool flag = false;
			try
			{
				if (this._hasErrorOrWarning)
				{
					this.ThrowExceptionAndWarning(false, true);
				}
				else if (this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken)
				{
					throw ADP.ClosedConnectionError();
				}
			}
			catch (Exception ex)
			{
				if (source.TrySetException(ex))
				{
					flag = true;
				}
			}
			if (!flag)
			{
				Task.Factory.StartNew(delegate
				{
					this._parser.State = TdsParserState.Broken;
					this._parser.Connection.BreakConnection();
					source.TrySetCanceled();
				});
			}
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x00080404 File Offset: 0x0007E604
		public void WriteAsyncCallback<T>(T packet, uint sniError)
		{
			this.WriteAsyncCallback<T>(IntPtr.Zero, packet, sniError);
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x00080414 File Offset: 0x0007E614
		public void WriteAsyncCallback<T>(IntPtr key, T packet, uint sniError)
		{
			this.RemovePacketFromPendingList(packet);
			try
			{
				if (sniError != 0U)
				{
					try
					{
						this.AddError(this._parser.ProcessSNIError(this));
						this.ThrowExceptionAndWarning(false, true);
						goto IL_009E;
					}
					catch (Exception ex)
					{
						TaskCompletionSource<object> taskCompletionSource = this._writeCompletionSource;
						if (taskCompletionSource != null)
						{
							taskCompletionSource.TrySetException(ex);
						}
						else
						{
							this._delayedWriteAsyncCallbackException = ex;
							Interlocked.MemoryBarrier();
							taskCompletionSource = this._writeCompletionSource;
							if (taskCompletionSource != null)
							{
								Exception ex2 = Interlocked.Exchange<Exception>(ref this._delayedWriteAsyncCallbackException, null);
								if (ex2 != null)
								{
									taskCompletionSource.TrySetException(ex2);
								}
							}
						}
						return;
					}
				}
				this._lastSuccessfulIOTimer._value = DateTime.UtcNow.Ticks;
			}
			finally
			{
				Interlocked.Decrement(ref this._asyncWriteCount);
			}
			IL_009E:
			TaskCompletionSource<object> writeCompletionSource = this._writeCompletionSource;
			if (this._asyncWriteCount == 0 && writeCompletionSource != null)
			{
				writeCompletionSource.TrySetResult(null);
			}
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x000804FC File Offset: 0x0007E6FC
		internal void WriteSecureString(SecureString secureString)
		{
			int num = ((this._securePasswords[0] != null) ? 1 : 0);
			this._securePasswords[num] = secureString;
			this._securePasswordOffsetsInBuffer[num] = this._outBytesUsed;
			int num2 = secureString.Length * 2;
			this._outBytesUsed += num2;
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x00080548 File Offset: 0x0007E748
		internal void ResetSecurePasswordsInformation()
		{
			for (int i = 0; i < this._securePasswords.Length; i++)
			{
				this._securePasswords[i] = null;
				this._securePasswordOffsetsInBuffer[i] = 0;
			}
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x0008057C File Offset: 0x0007E77C
		internal Task WaitForAccumulatedWrites()
		{
			Exception ex = Interlocked.Exchange<Exception>(ref this._delayedWriteAsyncCallbackException, null);
			if (ex != null)
			{
				throw ex;
			}
			if (this._asyncWriteCount == 0)
			{
				return null;
			}
			this._writeCompletionSource = new TaskCompletionSource<object>();
			Task task = this._writeCompletionSource.Task;
			Interlocked.MemoryBarrier();
			if (this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken)
			{
				throw ADP.ClosedConnectionError();
			}
			ex = Interlocked.Exchange<Exception>(ref this._delayedWriteAsyncCallbackException, null);
			if (ex != null)
			{
				throw ex;
			}
			if (this._asyncWriteCount == 0 && (!task.IsCompleted || task.Exception == null))
			{
				task = null;
			}
			return task;
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x00080618 File Offset: 0x0007E818
		internal void WriteByte(byte b)
		{
			if (this._outBytesUsed == this._outBuff.Length)
			{
				this.WritePacket(0, true);
			}
			byte[] outBuff = this._outBuff;
			int outBytesUsed = this._outBytesUsed;
			this._outBytesUsed = outBytesUsed + 1;
			outBuff[outBytesUsed] = b;
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x00080658 File Offset: 0x0007E858
		internal Task WriteByteArray(byte[] b, int len, int offsetBuffer, bool canAccumulate = true, TaskCompletionSource<object> completion = null)
		{
			Task task3;
			try
			{
				bool asyncWrite = this._parser._asyncWrite;
				int num = offsetBuffer;
				while (this._outBytesUsed + len > this._outBuff.Length)
				{
					int num2 = this._outBuff.Length - this._outBytesUsed;
					Buffer.BlockCopy(b, num, this._outBuff, this._outBytesUsed, num2);
					num += num2;
					this._outBytesUsed += num2;
					len -= num2;
					Task task = this.WritePacket(0, canAccumulate);
					if (task != null)
					{
						Task task2 = null;
						if (completion == null)
						{
							completion = new TaskCompletionSource<object>();
							task2 = completion.Task;
						}
						this.WriteByteArraySetupContinuation(b, len, completion, num, task);
						return task2;
					}
					if (len <= 0)
					{
						IL_00B9:
						if (completion != null)
						{
							completion.SetResult(null);
						}
						return null;
					}
				}
				Buffer.BlockCopy(b, num, this._outBuff, this._outBytesUsed, len);
				this._outBytesUsed += len;
				goto IL_00B9;
			}
			catch (Exception ex)
			{
				if (completion == null)
				{
					throw;
				}
				completion.SetException(ex);
				task3 = null;
			}
			return task3;
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x00080758 File Offset: 0x0007E958
		private void WriteByteArraySetupContinuation(byte[] b, int len, TaskCompletionSource<object> completion, int offset, Task packetTask)
		{
			AsyncHelper.ContinueTask(packetTask, completion, delegate
			{
				this.WriteByteArray(b, len, offset, false, completion);
			}, this._parser.Connection, null, null, null, null);
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x000807B8 File Offset: 0x0007E9B8
		internal Task WritePacket(byte flushMode, bool canAccumulate = false)
		{
			if (this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken)
			{
				throw ADP.ClosedConnectionError();
			}
			if ((this._parser.State == TdsParserState.OpenLoggedIn && !this._bulkCopyOpperationInProgress && this._outBytesUsed == this._outputHeaderLen + BitConverter.ToInt32(this._outBuff, this._outputHeaderLen) && this._outputPacketNumber == 1) || (this._outBytesUsed == this._outputHeaderLen && this._outputPacketNumber == 1))
			{
				return null;
			}
			byte outputPacketNumber = this._outputPacketNumber;
			bool flag = this._cancelled && this._parser._asyncWrite;
			byte b;
			if (flag)
			{
				b = 3;
				this._outputPacketNumber = 1;
			}
			else if (1 == flushMode)
			{
				b = 1;
				this._outputPacketNumber = 1;
			}
			else if (flushMode == 0)
			{
				b = 4;
				this._outputPacketNumber += 1;
			}
			else
			{
				b = 1;
			}
			this._outBuff[0] = this._outputMessageType;
			this._outBuff[1] = b;
			this._outBuff[2] = (byte)(this._outBytesUsed >> 8);
			this._outBuff[3] = (byte)(this._outBytesUsed & 255);
			this._outBuff[4] = 0;
			this._outBuff[5] = 0;
			this._outBuff[6] = outputPacketNumber;
			this._outBuff[7] = 0;
			this._parser.CheckResetConnection(this);
			Task task = this.WriteSni(canAccumulate);
			if (flag)
			{
				task = AsyncHelper.CreateContinuationTask(task, new Action(this.CancelWritePacket), this._parser.Connection, null);
			}
			return task;
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x0008092C File Offset: 0x0007EB2C
		private void CancelWritePacket()
		{
			this._parser.Connection.ThreadHasParserLockForClose = true;
			try
			{
				this.SendAttention(false);
				this.ResetCancelAndProcessAttention();
				throw SQL.OperationCancelled();
			}
			finally
			{
				this._parser.Connection.ThreadHasParserLockForClose = false;
			}
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x00080980 File Offset: 0x0007EB80
		private Task SNIWritePacket(object packet, out uint sniError, bool canAccumulate, bool callerHasConnectionLock)
		{
			Exception ex = Interlocked.Exchange<Exception>(ref this._delayedWriteAsyncCallbackException, null);
			if (ex != null)
			{
				throw ex;
			}
			Task task = null;
			this._writeCompletionSource = null;
			object obj = this.EmptyReadPacket;
			bool flag = !this._parser._asyncWrite;
			if (flag && this._asyncWriteCount > 0)
			{
				Task task2 = this.WaitForAccumulatedWrites();
				if (task2 != null)
				{
					try
					{
						task2.Wait();
					}
					catch (AggregateException ex2)
					{
						throw ex2.InnerException;
					}
				}
			}
			if (!flag)
			{
				obj = this.AddPacketToPendingList(packet);
			}
			try
			{
			}
			finally
			{
				sniError = this.WritePacket(packet, flag);
			}
			if (sniError == 997U)
			{
				Interlocked.Increment(ref this._asyncWriteCount);
				if (!canAccumulate)
				{
					this._writeCompletionSource = new TaskCompletionSource<object>();
					task = this._writeCompletionSource.Task;
					Interlocked.MemoryBarrier();
					ex = Interlocked.Exchange<Exception>(ref this._delayedWriteAsyncCallbackException, null);
					if (ex != null)
					{
						throw ex;
					}
					if (this._asyncWriteCount == 0 && (!task.IsCompleted || task.Exception == null))
					{
						task = null;
					}
				}
			}
			else
			{
				if (this._parser.MARSOn)
				{
					this.CheckSetResetConnectionState(sniError, CallbackType.Write);
				}
				if (sniError == 0U)
				{
					this._lastSuccessfulIOTimer._value = DateTime.UtcNow.Ticks;
					if (!flag)
					{
						this.RemovePacketFromPendingList(obj);
					}
				}
				else
				{
					this.AddError(this._parser.ProcessSNIError(this));
					this.ThrowExceptionAndWarning(callerHasConnectionLock, false);
				}
			}
			return task;
		}

		// Token: 0x0600197F RID: 6527
		internal abstract bool IsValidPacket(object packetPointer);

		// Token: 0x06001980 RID: 6528
		internal abstract uint WritePacket(object packet, bool sync);

		// Token: 0x06001981 RID: 6529 RVA: 0x00080AE4 File Offset: 0x0007ECE4
		internal void SendAttention(bool mustTakeWriteLock = false)
		{
			if (!this._attentionSent)
			{
				if (this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken)
				{
					return;
				}
				object obj = this.CreateAndSetAttentionPacket();
				try
				{
					this._attentionSending = true;
					bool flag = false;
					if (mustTakeWriteLock && !this._parser.Connection.ThreadHasParserLockForClose)
					{
						flag = true;
						this._parser.Connection._parserLock.Wait(false);
						this._parser.Connection.ThreadHasParserLockForClose = true;
					}
					try
					{
						if (this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken)
						{
							return;
						}
						this._parser._asyncWrite = false;
						uint num;
						this.SNIWritePacket(obj, out num, false, false);
					}
					finally
					{
						if (flag)
						{
							this._parser.Connection.ThreadHasParserLockForClose = false;
							this._parser.Connection._parserLock.Release();
						}
					}
					this.SetTimeoutSeconds(5);
					this._attentionSent = true;
				}
				finally
				{
					this._attentionSending = false;
				}
			}
		}

		// Token: 0x06001982 RID: 6530
		internal abstract object CreateAndSetAttentionPacket();

		// Token: 0x06001983 RID: 6531
		internal abstract void SetPacketData(object packet, byte[] buffer, int bytesUsed);

		// Token: 0x06001984 RID: 6532 RVA: 0x00080C04 File Offset: 0x0007EE04
		private Task WriteSni(bool canAccumulate)
		{
			object resetWritePacket = this.GetResetWritePacket();
			this.SetBufferSecureStrings();
			this.SetPacketData(resetWritePacket, this._outBuff, this._outBytesUsed);
			uint num;
			Task task = this.SNIWritePacket(resetWritePacket, out num, canAccumulate, true);
			if (this._bulkCopyOpperationInProgress && this.GetTimeoutRemaining() == 0)
			{
				this._parser.Connection.ThreadHasParserLockForClose = true;
				try
				{
					this.AddError(new SqlError(-2, 0, 11, this._parser.Server, this._parser.Connection.TimeoutErrorInternal.GetErrorMessage(), "", 0, 258U, null));
					this._bulkCopyWriteTimeout = true;
					this.SendAttention(false);
					this._parser.ProcessPendingAck(this);
					this.ThrowExceptionAndWarning(false, false);
				}
				finally
				{
					this._parser.Connection.ThreadHasParserLockForClose = false;
				}
			}
			if (this._parser.State == TdsParserState.OpenNotLoggedIn && this._parser.EncryptionOptions == EncryptionOptions.LOGIN)
			{
				this._parser.RemoveEncryption();
				this._parser.EncryptionOptions = EncryptionOptions.OFF;
				this.ClearAllWritePackets();
			}
			this.SniWriteStatisticsAndTracing();
			this.ResetBuffer();
			return task;
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x00080D2C File Offset: 0x0007EF2C
		private void SniReadStatisticsAndTracing()
		{
			SqlStatistics statistics = this.Parser.Statistics;
			if (statistics != null)
			{
				if (statistics.WaitForReply)
				{
					statistics.SafeIncrement(ref statistics._serverRoundtrips);
					statistics.ReleaseAndUpdateNetworkServerTimer();
				}
				statistics.SafeAdd(ref statistics._bytesReceived, (long)this._inBytesRead);
				statistics.SafeIncrement(ref statistics._buffersReceived);
			}
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x00080D84 File Offset: 0x0007EF84
		private void SniWriteStatisticsAndTracing()
		{
			SqlStatistics statistics = this._parser.Statistics;
			if (statistics != null)
			{
				statistics.SafeIncrement(ref statistics._buffersSent);
				statistics.SafeAdd(ref statistics._bytesSent, (long)this._outBytesUsed);
				statistics.RequestNetworkServerTimer();
			}
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x00080DC8 File Offset: 0x0007EFC8
		[Conditional("DEBUG")]
		private void AssertValidState()
		{
			if (this._inBytesUsed < 0 || this._inBytesRead < 0)
			{
				string text = string.Format(CultureInfo.InvariantCulture, "either _inBytesUsed or _inBytesRead is negative: {0}, {1}", this._inBytesUsed, this._inBytesRead);
			}
			else if (this._inBytesUsed > this._inBytesRead)
			{
				string text = string.Format(CultureInfo.InvariantCulture, "_inBytesUsed > _inBytesRead: {0} > {1}", this._inBytesUsed, this._inBytesRead);
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001988 RID: 6536 RVA: 0x00080E47 File Offset: 0x0007F047
		internal bool HasErrorOrWarning
		{
			get
			{
				return this._hasErrorOrWarning;
			}
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x00080E50 File Offset: 0x0007F050
		internal void AddError(SqlError error)
		{
			this._syncOverAsync = true;
			object errorAndWarningsLock = this._errorAndWarningsLock;
			lock (errorAndWarningsLock)
			{
				this._hasErrorOrWarning = true;
				if (this._errors == null)
				{
					this._errors = new SqlErrorCollection();
				}
				this._errors.Add(error);
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x0600198A RID: 6538 RVA: 0x00080EB8 File Offset: 0x0007F0B8
		internal int ErrorCount
		{
			get
			{
				int num = 0;
				object errorAndWarningsLock = this._errorAndWarningsLock;
				lock (errorAndWarningsLock)
				{
					if (this._errors != null)
					{
						num = this._errors.Count;
					}
				}
				return num;
			}
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x00080F0C File Offset: 0x0007F10C
		internal void AddWarning(SqlError error)
		{
			this._syncOverAsync = true;
			object errorAndWarningsLock = this._errorAndWarningsLock;
			lock (errorAndWarningsLock)
			{
				this._hasErrorOrWarning = true;
				if (this._warnings == null)
				{
					this._warnings = new SqlErrorCollection();
				}
				this._warnings.Add(error);
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x0600198C RID: 6540 RVA: 0x00080F74 File Offset: 0x0007F174
		internal int WarningCount
		{
			get
			{
				int num = 0;
				object errorAndWarningsLock = this._errorAndWarningsLock;
				lock (errorAndWarningsLock)
				{
					if (this._warnings != null)
					{
						num = this._warnings.Count;
					}
				}
				return num;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x0600198D RID: 6541
		protected abstract object EmptyReadPacket { get; }

		// Token: 0x0600198E RID: 6542 RVA: 0x00080FC8 File Offset: 0x0007F1C8
		internal SqlErrorCollection GetFullErrorAndWarningCollection(out bool broken)
		{
			SqlErrorCollection sqlErrorCollection = new SqlErrorCollection();
			broken = false;
			object errorAndWarningsLock = this._errorAndWarningsLock;
			lock (errorAndWarningsLock)
			{
				this._hasErrorOrWarning = false;
				this.AddErrorsToCollection(this._errors, ref sqlErrorCollection, ref broken);
				this.AddErrorsToCollection(this._warnings, ref sqlErrorCollection, ref broken);
				this._errors = null;
				this._warnings = null;
				this.AddErrorsToCollection(this._preAttentionErrors, ref sqlErrorCollection, ref broken);
				this.AddErrorsToCollection(this._preAttentionWarnings, ref sqlErrorCollection, ref broken);
				this._preAttentionErrors = null;
				this._preAttentionWarnings = null;
			}
			return sqlErrorCollection;
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x0008106C File Offset: 0x0007F26C
		private void AddErrorsToCollection(SqlErrorCollection inCollection, ref SqlErrorCollection collectionToAddTo, ref bool broken)
		{
			if (inCollection != null)
			{
				foreach (object obj in inCollection)
				{
					SqlError sqlError = (SqlError)obj;
					collectionToAddTo.Add(sqlError);
					broken |= sqlError.Class >= 20;
				}
			}
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x000810D8 File Offset: 0x0007F2D8
		internal void StoreErrorAndWarningForAttention()
		{
			object errorAndWarningsLock = this._errorAndWarningsLock;
			lock (errorAndWarningsLock)
			{
				this._hasErrorOrWarning = false;
				this._preAttentionErrors = this._errors;
				this._preAttentionWarnings = this._warnings;
				this._errors = null;
				this._warnings = null;
			}
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x00081140 File Offset: 0x0007F340
		internal void RestoreErrorAndWarningAfterAttention()
		{
			object errorAndWarningsLock = this._errorAndWarningsLock;
			lock (errorAndWarningsLock)
			{
				this._hasErrorOrWarning = (this._preAttentionErrors != null && this._preAttentionErrors.Count > 0) || (this._preAttentionWarnings != null && this._preAttentionWarnings.Count > 0);
				this._errors = this._preAttentionErrors;
				this._warnings = this._preAttentionWarnings;
				this._preAttentionErrors = null;
				this._preAttentionWarnings = null;
			}
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x000811D8 File Offset: 0x0007F3D8
		internal void CheckThrowSNIException()
		{
			if (this.HasErrorOrWarning)
			{
				this.ThrowExceptionAndWarning(false, false);
			}
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x000811EC File Offset: 0x0007F3EC
		[Conditional("DEBUG")]
		internal void AssertStateIsClean()
		{
			TdsParser parser = this._parser;
			if (parser != null && parser.State != TdsParserState.Closed)
			{
				TdsParserState state = parser.State;
			}
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x00081214 File Offset: 0x0007F414
		internal void CloneCleanupAltMetaDataSetArray()
		{
			if (this._snapshot != null)
			{
				this._snapshot.CloneCleanupAltMetaDataSetArray();
			}
		}

		// Token: 0x0400121F RID: 4639
		private const int AttentionTimeoutSeconds = 5;

		// Token: 0x04001220 RID: 4640
		private const long CheckConnectionWindow = 50000L;

		// Token: 0x04001221 RID: 4641
		protected readonly TdsParser _parser;

		// Token: 0x04001222 RID: 4642
		private readonly WeakReference _owner = new WeakReference(null);

		// Token: 0x04001223 RID: 4643
		internal SqlDataReader.SharedState _readerState;

		// Token: 0x04001224 RID: 4644
		private int _activateCount;

		// Token: 0x04001225 RID: 4645
		internal readonly int _inputHeaderLen = 8;

		// Token: 0x04001226 RID: 4646
		internal readonly int _outputHeaderLen = 8;

		// Token: 0x04001227 RID: 4647
		internal byte[] _outBuff;

		// Token: 0x04001228 RID: 4648
		internal int _outBytesUsed = 8;

		// Token: 0x04001229 RID: 4649
		protected byte[] _inBuff;

		// Token: 0x0400122A RID: 4650
		internal int _inBytesUsed;

		// Token: 0x0400122B RID: 4651
		internal int _inBytesRead;

		// Token: 0x0400122C RID: 4652
		internal int _inBytesPacket;

		// Token: 0x0400122D RID: 4653
		internal byte _outputMessageType;

		// Token: 0x0400122E RID: 4654
		internal byte _messageStatus;

		// Token: 0x0400122F RID: 4655
		internal byte _outputPacketNumber = 1;

		// Token: 0x04001230 RID: 4656
		internal bool _pendingData;

		// Token: 0x04001231 RID: 4657
		internal volatile bool _fResetEventOwned;

		// Token: 0x04001232 RID: 4658
		internal volatile bool _fResetConnectionSent;

		// Token: 0x04001233 RID: 4659
		internal bool _errorTokenReceived;

		// Token: 0x04001234 RID: 4660
		internal bool _bulkCopyOpperationInProgress;

		// Token: 0x04001235 RID: 4661
		internal bool _bulkCopyWriteTimeout;

		// Token: 0x04001236 RID: 4662
		protected readonly object _writePacketLockObject = new object();

		// Token: 0x04001237 RID: 4663
		private int _pendingCallbacks;

		// Token: 0x04001238 RID: 4664
		private long _timeoutMilliseconds;

		// Token: 0x04001239 RID: 4665
		private long _timeoutTime;

		// Token: 0x0400123A RID: 4666
		internal volatile bool _attentionSent;

		// Token: 0x0400123B RID: 4667
		internal bool _attentionReceived;

		// Token: 0x0400123C RID: 4668
		internal volatile bool _attentionSending;

		// Token: 0x0400123D RID: 4669
		internal bool _internalTimeout;

		// Token: 0x0400123E RID: 4670
		private readonly LastIOTimer _lastSuccessfulIOTimer;

		// Token: 0x0400123F RID: 4671
		private SecureString[] _securePasswords = new SecureString[2];

		// Token: 0x04001240 RID: 4672
		private int[] _securePasswordOffsetsInBuffer = new int[2];

		// Token: 0x04001241 RID: 4673
		private bool _cancelled;

		// Token: 0x04001242 RID: 4674
		private const int _waitForCancellationLockPollTimeout = 100;

		// Token: 0x04001243 RID: 4675
		private WeakReference _cancellationOwner = new WeakReference(null);

		// Token: 0x04001244 RID: 4676
		internal bool _hasOpenResult;

		// Token: 0x04001245 RID: 4677
		internal SqlInternalTransaction _executedUnderTransaction;

		// Token: 0x04001246 RID: 4678
		internal ulong _longlen;

		// Token: 0x04001247 RID: 4679
		internal ulong _longlenleft;

		// Token: 0x04001248 RID: 4680
		internal int[] _decimalBits;

		// Token: 0x04001249 RID: 4681
		internal byte[] _bTmp = new byte[12];

		// Token: 0x0400124A RID: 4682
		internal int _bTmpRead;

		// Token: 0x0400124B RID: 4683
		internal Decoder _plpdecoder;

		// Token: 0x0400124C RID: 4684
		internal bool _accumulateInfoEvents;

		// Token: 0x0400124D RID: 4685
		internal List<SqlError> _pendingInfoEvents;

		// Token: 0x0400124E RID: 4686
		private byte[] _partialHeaderBuffer = new byte[8];

		// Token: 0x0400124F RID: 4687
		internal int _partialHeaderBytesRead;

		// Token: 0x04001250 RID: 4688
		internal _SqlMetaDataSet _cleanupMetaData;

		// Token: 0x04001251 RID: 4689
		internal _SqlMetaDataSetCollection _cleanupAltMetaDataSetArray;

		// Token: 0x04001252 RID: 4690
		internal bool _receivedColMetaData;

		// Token: 0x04001253 RID: 4691
		private SniContext _sniContext;

		// Token: 0x04001254 RID: 4692
		private bool _bcpLock;

		// Token: 0x04001255 RID: 4693
		private TdsParserStateObject.NullBitmap _nullBitmapInfo;

		// Token: 0x04001256 RID: 4694
		internal TaskCompletionSource<object> _networkPacketTaskSource;

		// Token: 0x04001257 RID: 4695
		private Timer _networkPacketTimeout;

		// Token: 0x04001258 RID: 4696
		internal bool _syncOverAsync = true;

		// Token: 0x04001259 RID: 4697
		private bool _snapshotReplay;

		// Token: 0x0400125A RID: 4698
		private TdsParserStateObject.StateSnapshot _snapshot;

		// Token: 0x0400125B RID: 4699
		internal ExecutionContext _executionContext;

		// Token: 0x0400125C RID: 4700
		internal bool _asyncReadWithoutSnapshot;

		// Token: 0x0400125D RID: 4701
		internal SqlErrorCollection _errors;

		// Token: 0x0400125E RID: 4702
		internal SqlErrorCollection _warnings;

		// Token: 0x0400125F RID: 4703
		internal object _errorAndWarningsLock = new object();

		// Token: 0x04001260 RID: 4704
		private bool _hasErrorOrWarning;

		// Token: 0x04001261 RID: 4705
		internal SqlErrorCollection _preAttentionErrors;

		// Token: 0x04001262 RID: 4706
		internal SqlErrorCollection _preAttentionWarnings;

		// Token: 0x04001263 RID: 4707
		private volatile TaskCompletionSource<object> _writeCompletionSource;

		// Token: 0x04001264 RID: 4708
		protected volatile int _asyncWriteCount;

		// Token: 0x04001265 RID: 4709
		private volatile Exception _delayedWriteAsyncCallbackException;

		// Token: 0x04001266 RID: 4710
		private int _readingCount;

		// Token: 0x02000221 RID: 545
		private struct NullBitmap
		{
			// Token: 0x06001997 RID: 6551 RVA: 0x0008124C File Offset: 0x0007F44C
			internal bool TryInitialize(TdsParserStateObject stateObj, int columnsCount)
			{
				this._columnsCount = columnsCount;
				int num = (columnsCount + 7) / 8;
				if (this._nullBitmap == null || this._nullBitmap.Length != num)
				{
					this._nullBitmap = new byte[num];
				}
				return stateObj.TryReadByteArray(this._nullBitmap, 0, this._nullBitmap.Length);
			}

			// Token: 0x06001998 RID: 6552 RVA: 0x0008129F File Offset: 0x0007F49F
			internal bool ReferenceEquals(TdsParserStateObject.NullBitmap obj)
			{
				return this._nullBitmap == obj._nullBitmap;
			}

			// Token: 0x06001999 RID: 6553 RVA: 0x000812B0 File Offset: 0x0007F4B0
			internal TdsParserStateObject.NullBitmap Clone()
			{
				return new TdsParserStateObject.NullBitmap
				{
					_nullBitmap = ((this._nullBitmap == null) ? null : ((byte[])this._nullBitmap.Clone())),
					_columnsCount = this._columnsCount
				};
			}

			// Token: 0x0600199A RID: 6554 RVA: 0x000812F5 File Offset: 0x0007F4F5
			internal void Clean()
			{
				this._columnsCount = 0;
			}

			// Token: 0x0600199B RID: 6555 RVA: 0x00081300 File Offset: 0x0007F500
			internal bool IsGuaranteedNull(int columnOrdinal)
			{
				if (this._columnsCount == 0)
				{
					return false;
				}
				byte b = (byte)(1 << (columnOrdinal & 7));
				byte b2 = this._nullBitmap[columnOrdinal >> 3];
				return (b & b2) > 0;
			}

			// Token: 0x04001267 RID: 4711
			private byte[] _nullBitmap;

			// Token: 0x04001268 RID: 4712
			private int _columnsCount;
		}

		// Token: 0x02000222 RID: 546
		private class PacketData
		{
			// Token: 0x04001269 RID: 4713
			public byte[] Buffer;

			// Token: 0x0400126A RID: 4714
			public int Read;
		}

		// Token: 0x02000223 RID: 547
		private class StateSnapshot
		{
			// Token: 0x0600199D RID: 6557 RVA: 0x00081330 File Offset: 0x0007F530
			public StateSnapshot(TdsParserStateObject state)
			{
				this._snapshotInBuffs = new List<TdsParserStateObject.PacketData>();
				this._stateObj = state;
			}

			// Token: 0x0600199E RID: 6558 RVA: 0x0008134A File Offset: 0x0007F54A
			internal void CloneNullBitmapInfo()
			{
				if (this._stateObj._nullBitmapInfo.ReferenceEquals(this._snapshotNullBitmapInfo))
				{
					this._stateObj._nullBitmapInfo = this._stateObj._nullBitmapInfo.Clone();
				}
			}

			// Token: 0x0600199F RID: 6559 RVA: 0x00081380 File Offset: 0x0007F580
			internal void CloneCleanupAltMetaDataSetArray()
			{
				if (this._stateObj._cleanupAltMetaDataSetArray != null && this._snapshotCleanupAltMetaDataSetArray == this._stateObj._cleanupAltMetaDataSetArray)
				{
					this._stateObj._cleanupAltMetaDataSetArray = (_SqlMetaDataSetCollection)this._stateObj._cleanupAltMetaDataSetArray.Clone();
				}
			}

			// Token: 0x060019A0 RID: 6560 RVA: 0x000813D0 File Offset: 0x0007F5D0
			internal void PushBuffer(byte[] buffer, int read)
			{
				TdsParserStateObject.PacketData packetData = new TdsParserStateObject.PacketData();
				packetData.Buffer = buffer;
				packetData.Read = read;
				this._snapshotInBuffs.Add(packetData);
			}

			// Token: 0x060019A1 RID: 6561 RVA: 0x00081400 File Offset: 0x0007F600
			internal bool Replay()
			{
				if (this._snapshotInBuffCurrent < this._snapshotInBuffs.Count)
				{
					TdsParserStateObject.PacketData packetData = this._snapshotInBuffs[this._snapshotInBuffCurrent];
					this._stateObj._inBuff = packetData.Buffer;
					this._stateObj._inBytesUsed = 0;
					this._stateObj._inBytesRead = packetData.Read;
					this._snapshotInBuffCurrent++;
					return true;
				}
				return false;
			}

			// Token: 0x060019A2 RID: 6562 RVA: 0x00081474 File Offset: 0x0007F674
			internal void Snap()
			{
				this._snapshotInBuffs.Clear();
				this._snapshotInBuffCurrent = 0;
				this._snapshotInBytesUsed = this._stateObj._inBytesUsed;
				this._snapshotInBytesPacket = this._stateObj._inBytesPacket;
				this._snapshotPendingData = this._stateObj._pendingData;
				this._snapshotErrorTokenReceived = this._stateObj._errorTokenReceived;
				this._snapshotMessageStatus = this._stateObj._messageStatus;
				this._snapshotNullBitmapInfo = this._stateObj._nullBitmapInfo;
				this._snapshotLongLen = this._stateObj._longlen;
				this._snapshotLongLenLeft = this._stateObj._longlenleft;
				this._snapshotCleanupMetaData = this._stateObj._cleanupMetaData;
				this._snapshotCleanupAltMetaDataSetArray = this._stateObj._cleanupAltMetaDataSetArray;
				this._snapshotHasOpenResult = this._stateObj._hasOpenResult;
				this._snapshotReceivedColumnMetadata = this._stateObj._receivedColMetaData;
				this._snapshotAttentionReceived = this._stateObj._attentionReceived;
				this.PushBuffer(this._stateObj._inBuff, this._stateObj._inBytesRead);
			}

			// Token: 0x060019A3 RID: 6563 RVA: 0x0008158C File Offset: 0x0007F78C
			internal void ResetSnapshotState()
			{
				this._snapshotInBuffCurrent = 0;
				this.Replay();
				this._stateObj._inBytesUsed = this._snapshotInBytesUsed;
				this._stateObj._inBytesPacket = this._snapshotInBytesPacket;
				this._stateObj._pendingData = this._snapshotPendingData;
				this._stateObj._errorTokenReceived = this._snapshotErrorTokenReceived;
				this._stateObj._messageStatus = this._snapshotMessageStatus;
				this._stateObj._nullBitmapInfo = this._snapshotNullBitmapInfo;
				this._stateObj._cleanupMetaData = this._snapshotCleanupMetaData;
				this._stateObj._cleanupAltMetaDataSetArray = this._snapshotCleanupAltMetaDataSetArray;
				this._stateObj._hasOpenResult = this._snapshotHasOpenResult;
				this._stateObj._receivedColMetaData = this._snapshotReceivedColumnMetadata;
				this._stateObj._attentionReceived = this._snapshotAttentionReceived;
				this._stateObj._bTmpRead = 0;
				this._stateObj._partialHeaderBytesRead = 0;
				this._stateObj._longlen = this._snapshotLongLen;
				this._stateObj._longlenleft = this._snapshotLongLenLeft;
				this._stateObj._snapshotReplay = true;
			}

			// Token: 0x060019A4 RID: 6564 RVA: 0x000816A8 File Offset: 0x0007F8A8
			internal void PrepareReplay()
			{
				this.ResetSnapshotState();
			}

			// Token: 0x0400126B RID: 4715
			private List<TdsParserStateObject.PacketData> _snapshotInBuffs;

			// Token: 0x0400126C RID: 4716
			private int _snapshotInBuffCurrent;

			// Token: 0x0400126D RID: 4717
			private int _snapshotInBytesUsed;

			// Token: 0x0400126E RID: 4718
			private int _snapshotInBytesPacket;

			// Token: 0x0400126F RID: 4719
			private bool _snapshotPendingData;

			// Token: 0x04001270 RID: 4720
			private bool _snapshotErrorTokenReceived;

			// Token: 0x04001271 RID: 4721
			private bool _snapshotHasOpenResult;

			// Token: 0x04001272 RID: 4722
			private bool _snapshotReceivedColumnMetadata;

			// Token: 0x04001273 RID: 4723
			private bool _snapshotAttentionReceived;

			// Token: 0x04001274 RID: 4724
			private byte _snapshotMessageStatus;

			// Token: 0x04001275 RID: 4725
			private TdsParserStateObject.NullBitmap _snapshotNullBitmapInfo;

			// Token: 0x04001276 RID: 4726
			private ulong _snapshotLongLen;

			// Token: 0x04001277 RID: 4727
			private ulong _snapshotLongLenLeft;

			// Token: 0x04001278 RID: 4728
			private _SqlMetaDataSet _snapshotCleanupMetaData;

			// Token: 0x04001279 RID: 4729
			private _SqlMetaDataSetCollection _snapshotCleanupAltMetaDataSetArray;

			// Token: 0x0400127A RID: 4730
			private readonly TdsParserStateObject _stateObj;
		}
	}
}
