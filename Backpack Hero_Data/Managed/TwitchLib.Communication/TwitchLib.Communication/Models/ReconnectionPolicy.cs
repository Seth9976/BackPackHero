using System;

namespace TwitchLib.Communication.Models
{
	// Token: 0x02000004 RID: 4
	public class ReconnectionPolicy
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002418 File Offset: 0x00000618
		public ReconnectionPolicy()
		{
			this._reconnectStepInterval = 3000;
			this._minReconnectInterval = 3000;
			this._maxReconnectInterval = 30000;
			this._maxAttempts = default(int?);
			this._initMaxAttempts = default(int?);
			this._attemptsMade = 0;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000246D File Offset: 0x0000066D
		public void SetMaxAttempts(int attempts)
		{
			this._maxAttempts = new int?(attempts);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000247C File Offset: 0x0000067C
		public void Reset()
		{
			this._attemptsMade = 0;
			this._minReconnectInterval = this._reconnectStepInterval;
			this._maxAttempts = this._initMaxAttempts;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000249E File Offset: 0x0000069E
		public void SetAttemptsMade(int count)
		{
			this._attemptsMade = count;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000024A7 File Offset: 0x000006A7
		public ReconnectionPolicy(int minReconnectInterval, int maxReconnectInterval, int? maxAttempts)
		{
			this._reconnectStepInterval = minReconnectInterval;
			this._minReconnectInterval = ((minReconnectInterval > maxReconnectInterval) ? maxReconnectInterval : minReconnectInterval);
			this._maxReconnectInterval = maxReconnectInterval;
			this._maxAttempts = maxAttempts;
			this._initMaxAttempts = maxAttempts;
			this._attemptsMade = 0;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000024E4 File Offset: 0x000006E4
		public ReconnectionPolicy(int minReconnectInterval, int maxReconnectInterval)
		{
			this._reconnectStepInterval = minReconnectInterval;
			this._minReconnectInterval = ((minReconnectInterval > maxReconnectInterval) ? maxReconnectInterval : minReconnectInterval);
			this._maxReconnectInterval = maxReconnectInterval;
			this._maxAttempts = default(int?);
			this._initMaxAttempts = default(int?);
			this._attemptsMade = 0;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002534 File Offset: 0x00000734
		public ReconnectionPolicy(int reconnectInterval)
		{
			this._reconnectStepInterval = reconnectInterval;
			this._minReconnectInterval = reconnectInterval;
			this._maxReconnectInterval = reconnectInterval;
			this._maxAttempts = default(int?);
			this._initMaxAttempts = default(int?);
			this._attemptsMade = 0;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002572 File Offset: 0x00000772
		public ReconnectionPolicy(int reconnectInterval, int? maxAttempts)
		{
			this._reconnectStepInterval = reconnectInterval;
			this._minReconnectInterval = reconnectInterval;
			this._maxReconnectInterval = reconnectInterval;
			this._maxAttempts = maxAttempts;
			this._initMaxAttempts = maxAttempts;
			this._attemptsMade = 0;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000025A8 File Offset: 0x000007A8
		internal void ProcessValues()
		{
			this._attemptsMade++;
			bool flag = this._minReconnectInterval < this._maxReconnectInterval;
			if (flag)
			{
				this._minReconnectInterval += this._reconnectStepInterval;
			}
			bool flag2 = this._minReconnectInterval > this._maxReconnectInterval;
			if (flag2)
			{
				this._minReconnectInterval = this._maxReconnectInterval;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002607 File Offset: 0x00000807
		public int GetReconnectInterval()
		{
			return this._minReconnectInterval;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002610 File Offset: 0x00000810
		public bool AreAttemptsComplete()
		{
			int attemptsMade = this._attemptsMade;
			int? maxAttempts = this._maxAttempts;
			return (attemptsMade == maxAttempts.GetValueOrDefault()) & (maxAttempts != null);
		}

		// Token: 0x0400001B RID: 27
		private readonly int _reconnectStepInterval;

		// Token: 0x0400001C RID: 28
		private readonly int? _initMaxAttempts;

		// Token: 0x0400001D RID: 29
		private int _minReconnectInterval;

		// Token: 0x0400001E RID: 30
		private readonly int _maxReconnectInterval;

		// Token: 0x0400001F RID: 31
		private int? _maxAttempts;

		// Token: 0x04000020 RID: 32
		private int _attemptsMade;
	}
}
