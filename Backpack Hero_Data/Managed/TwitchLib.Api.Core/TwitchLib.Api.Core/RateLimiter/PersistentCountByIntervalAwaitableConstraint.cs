using System;
using System.Collections.Generic;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api.Core.RateLimiter
{
	// Token: 0x0200000A RID: 10
	public class PersistentCountByIntervalAwaitableConstraint : CountByIntervalAwaitableConstraint
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00002EBC File Offset: 0x000010BC
		public PersistentCountByIntervalAwaitableConstraint(int count, TimeSpan timeSpan, Action<DateTime> saveStateAction, IEnumerable<DateTime> initialTimeStamps, ITime time = null)
			: base(count, timeSpan, time)
		{
			this._saveStateAction = saveStateAction;
			if (initialTimeStamps == null)
			{
				return;
			}
			foreach (DateTime dateTime in initialTimeStamps)
			{
				base._timeStamps.Push(dateTime);
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002F20 File Offset: 0x00001120
		protected override void OnEnded(DateTime now)
		{
			this._saveStateAction.Invoke(now);
		}

		// Token: 0x0400001B RID: 27
		private readonly Action<DateTime> _saveStateAction;
	}
}
