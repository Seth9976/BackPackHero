using System;
using System.Data.Common;

namespace System.Data.ProviderBase
{
	// Token: 0x02000303 RID: 771
	internal class TimeoutTimer
	{
		// Token: 0x06002301 RID: 8961 RVA: 0x000A0F9A File Offset: 0x0009F19A
		internal static TimeoutTimer StartSecondsTimeout(int seconds)
		{
			TimeoutTimer timeoutTimer = new TimeoutTimer();
			timeoutTimer.SetTimeoutSeconds(seconds);
			return timeoutTimer;
		}

		// Token: 0x06002302 RID: 8962 RVA: 0x000A0FA8 File Offset: 0x0009F1A8
		internal static TimeoutTimer StartMillisecondsTimeout(long milliseconds)
		{
			return new TimeoutTimer
			{
				_timerExpire = checked(ADP.TimerCurrent() + milliseconds * 10000L),
				_isInfiniteTimeout = false
			};
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x000A0FCA File Offset: 0x0009F1CA
		internal void SetTimeoutSeconds(int seconds)
		{
			if (TimeoutTimer.InfiniteTimeout == (long)seconds)
			{
				this._isInfiniteTimeout = true;
				return;
			}
			this._timerExpire = checked(ADP.TimerCurrent() + ADP.TimerFromSeconds(seconds));
			this._isInfiniteTimeout = false;
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06002304 RID: 8964 RVA: 0x000A0FF6 File Offset: 0x0009F1F6
		internal bool IsExpired
		{
			get
			{
				return !this.IsInfinite && ADP.TimerHasExpired(this._timerExpire);
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06002305 RID: 8965 RVA: 0x000A100D File Offset: 0x0009F20D
		internal bool IsInfinite
		{
			get
			{
				return this._isInfiniteTimeout;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06002306 RID: 8966 RVA: 0x000A1015 File Offset: 0x0009F215
		internal long LegacyTimerExpire
		{
			get
			{
				if (!this._isInfiniteTimeout)
				{
					return this._timerExpire;
				}
				return long.MaxValue;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06002307 RID: 8967 RVA: 0x000A1030 File Offset: 0x0009F230
		internal long MillisecondsRemaining
		{
			get
			{
				long num;
				if (this._isInfiniteTimeout)
				{
					num = long.MaxValue;
				}
				else
				{
					num = ADP.TimerRemainingMilliseconds(this._timerExpire);
					if (0L > num)
					{
						num = 0L;
					}
				}
				return num;
			}
		}

		// Token: 0x04001767 RID: 5991
		private long _timerExpire;

		// Token: 0x04001768 RID: 5992
		private bool _isInfiniteTimeout;

		// Token: 0x04001769 RID: 5993
		internal static readonly long InfiniteTimeout;
	}
}
