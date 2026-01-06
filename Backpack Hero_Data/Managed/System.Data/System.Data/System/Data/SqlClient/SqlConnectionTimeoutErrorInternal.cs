using System;
using System.Text;

namespace System.Data.SqlClient
{
	// Token: 0x02000192 RID: 402
	internal class SqlConnectionTimeoutErrorInternal
	{
		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001381 RID: 4993 RVA: 0x0005E643 File Offset: 0x0005C843
		internal SqlConnectionTimeoutErrorPhase CurrentPhase
		{
			get
			{
				return this._currentPhase;
			}
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x0005E64C File Offset: 0x0005C84C
		public SqlConnectionTimeoutErrorInternal()
		{
			this._phaseDurations = new SqlConnectionTimeoutPhaseDuration[9];
			for (int i = 0; i < this._phaseDurations.Length; i++)
			{
				this._phaseDurations[i] = null;
			}
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x0005E688 File Offset: 0x0005C888
		public void SetFailoverScenario(bool useFailoverServer)
		{
			this._isFailoverScenario = useFailoverServer;
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x0005E691 File Offset: 0x0005C891
		public void SetInternalSourceType(SqlConnectionInternalSourceType sourceType)
		{
			this._currentSourceType = sourceType;
			if (this._currentSourceType == SqlConnectionInternalSourceType.RoutingDestination)
			{
				this._originalPhaseDurations = this._phaseDurations;
				this._phaseDurations = new SqlConnectionTimeoutPhaseDuration[9];
				this.SetAndBeginPhase(SqlConnectionTimeoutErrorPhase.PreLoginBegin);
			}
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x0005E6C4 File Offset: 0x0005C8C4
		internal void ResetAndRestartPhase()
		{
			this._currentPhase = SqlConnectionTimeoutErrorPhase.PreLoginBegin;
			for (int i = 0; i < this._phaseDurations.Length; i++)
			{
				this._phaseDurations[i] = null;
			}
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x0005E6F4 File Offset: 0x0005C8F4
		internal void SetAndBeginPhase(SqlConnectionTimeoutErrorPhase timeoutErrorPhase)
		{
			this._currentPhase = timeoutErrorPhase;
			if (this._phaseDurations[(int)timeoutErrorPhase] == null)
			{
				this._phaseDurations[(int)timeoutErrorPhase] = new SqlConnectionTimeoutPhaseDuration();
			}
			this._phaseDurations[(int)timeoutErrorPhase].StartCapture();
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x0005E721 File Offset: 0x0005C921
		internal void EndPhase(SqlConnectionTimeoutErrorPhase timeoutErrorPhase)
		{
			this._phaseDurations[(int)timeoutErrorPhase].StopCapture();
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x0005E730 File Offset: 0x0005C930
		internal void SetAllCompleteMarker()
		{
			this._currentPhase = SqlConnectionTimeoutErrorPhase.Complete;
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0005E73C File Offset: 0x0005C93C
		internal string GetErrorMessage()
		{
			StringBuilder stringBuilder;
			string text;
			switch (this._currentPhase)
			{
			case SqlConnectionTimeoutErrorPhase.PreLoginBegin:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_PreLogin_Begin());
				text = SQLMessage.Duration_PreLogin_Begin(this._phaseDurations[1].GetMilliSecondDuration());
				break;
			case SqlConnectionTimeoutErrorPhase.InitializeConnection:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_PreLogin_InitializeConnection());
				text = SQLMessage.Duration_PreLogin_Begin(this._phaseDurations[1].GetMilliSecondDuration() + this._phaseDurations[2].GetMilliSecondDuration());
				break;
			case SqlConnectionTimeoutErrorPhase.SendPreLoginHandshake:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_PreLogin_SendHandshake());
				text = SQLMessage.Duration_PreLoginHandshake(this._phaseDurations[1].GetMilliSecondDuration() + this._phaseDurations[2].GetMilliSecondDuration(), this._phaseDurations[3].GetMilliSecondDuration());
				break;
			case SqlConnectionTimeoutErrorPhase.ConsumePreLoginHandshake:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_PreLogin_ConsumeHandshake());
				text = SQLMessage.Duration_PreLoginHandshake(this._phaseDurations[1].GetMilliSecondDuration() + this._phaseDurations[2].GetMilliSecondDuration(), this._phaseDurations[3].GetMilliSecondDuration() + this._phaseDurations[4].GetMilliSecondDuration());
				break;
			case SqlConnectionTimeoutErrorPhase.LoginBegin:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_Login_Begin());
				text = SQLMessage.Duration_Login_Begin(this._phaseDurations[1].GetMilliSecondDuration() + this._phaseDurations[2].GetMilliSecondDuration(), this._phaseDurations[3].GetMilliSecondDuration() + this._phaseDurations[4].GetMilliSecondDuration(), this._phaseDurations[5].GetMilliSecondDuration());
				break;
			case SqlConnectionTimeoutErrorPhase.ProcessConnectionAuth:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_Login_ProcessConnectionAuth());
				text = SQLMessage.Duration_Login_ProcessConnectionAuth(this._phaseDurations[1].GetMilliSecondDuration() + this._phaseDurations[2].GetMilliSecondDuration(), this._phaseDurations[3].GetMilliSecondDuration() + this._phaseDurations[4].GetMilliSecondDuration(), this._phaseDurations[5].GetMilliSecondDuration(), this._phaseDurations[6].GetMilliSecondDuration());
				break;
			case SqlConnectionTimeoutErrorPhase.PostLogin:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_PostLogin());
				text = SQLMessage.Duration_PostLogin(this._phaseDurations[1].GetMilliSecondDuration() + this._phaseDurations[2].GetMilliSecondDuration(), this._phaseDurations[3].GetMilliSecondDuration() + this._phaseDurations[4].GetMilliSecondDuration(), this._phaseDurations[5].GetMilliSecondDuration(), this._phaseDurations[6].GetMilliSecondDuration(), this._phaseDurations[7].GetMilliSecondDuration());
				break;
			default:
				stringBuilder = new StringBuilder(SQLMessage.Timeout());
				text = null;
				break;
			}
			if (this._currentPhase != SqlConnectionTimeoutErrorPhase.Undefined && this._currentPhase != SqlConnectionTimeoutErrorPhase.Complete)
			{
				if (this._isFailoverScenario)
				{
					stringBuilder.Append("  ");
					stringBuilder.AppendFormat(null, SQLMessage.Timeout_FailoverInfo(), this._currentSourceType);
				}
				else if (this._currentSourceType == SqlConnectionInternalSourceType.RoutingDestination)
				{
					stringBuilder.Append("  ");
					stringBuilder.AppendFormat(null, SQLMessage.Timeout_RoutingDestination(), new object[]
					{
						this._originalPhaseDurations[1].GetMilliSecondDuration() + this._originalPhaseDurations[2].GetMilliSecondDuration(),
						this._originalPhaseDurations[3].GetMilliSecondDuration() + this._originalPhaseDurations[4].GetMilliSecondDuration(),
						this._originalPhaseDurations[5].GetMilliSecondDuration(),
						this._originalPhaseDurations[6].GetMilliSecondDuration(),
						this._originalPhaseDurations[7].GetMilliSecondDuration()
					});
				}
			}
			if (text != null)
			{
				stringBuilder.Append("  ");
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000D16 RID: 3350
		private SqlConnectionTimeoutPhaseDuration[] _phaseDurations;

		// Token: 0x04000D17 RID: 3351
		private SqlConnectionTimeoutPhaseDuration[] _originalPhaseDurations;

		// Token: 0x04000D18 RID: 3352
		private SqlConnectionTimeoutErrorPhase _currentPhase;

		// Token: 0x04000D19 RID: 3353
		private SqlConnectionInternalSourceType _currentSourceType;

		// Token: 0x04000D1A RID: 3354
		private bool _isFailoverScenario;
	}
}
