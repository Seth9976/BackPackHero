using System;
using System.Diagnostics.Tracing;

namespace System.Collections.Concurrent
{
	// Token: 0x020007B1 RID: 1969
	[EventSource(Name = "System.Collections.Concurrent.ConcurrentCollectionsEventSource", Guid = "35167F8E-49B2-4b96-AB86-435B59336B5E")]
	internal sealed class CDSCollectionETWBCLProvider : EventSource
	{
		// Token: 0x06003E5A RID: 15962 RVA: 0x0006ABDA File Offset: 0x00068DDA
		private CDSCollectionETWBCLProvider()
		{
		}

		// Token: 0x06003E5B RID: 15963 RVA: 0x000DB4C9 File Offset: 0x000D96C9
		[Event(1, Level = EventLevel.Warning)]
		public void ConcurrentStack_FastPushFailed(int spinCount)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(1, spinCount);
			}
		}

		// Token: 0x06003E5C RID: 15964 RVA: 0x000DB4DE File Offset: 0x000D96DE
		[Event(2, Level = EventLevel.Warning)]
		public void ConcurrentStack_FastPopFailed(int spinCount)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(2, spinCount);
			}
		}

		// Token: 0x06003E5D RID: 15965 RVA: 0x000DB4F3 File Offset: 0x000D96F3
		[Event(3, Level = EventLevel.Warning)]
		public void ConcurrentDictionary_AcquiringAllLocks(int numOfBuckets)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(3, numOfBuckets);
			}
		}

		// Token: 0x06003E5E RID: 15966 RVA: 0x000DB508 File Offset: 0x000D9708
		[Event(4, Level = EventLevel.Verbose)]
		public void ConcurrentBag_TryTakeSteals()
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				base.WriteEvent(4);
			}
		}

		// Token: 0x06003E5F RID: 15967 RVA: 0x000DB51C File Offset: 0x000D971C
		[Event(5, Level = EventLevel.Verbose)]
		public void ConcurrentBag_TryPeekSteals()
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				base.WriteEvent(5);
			}
		}

		// Token: 0x0400262F RID: 9775
		public static CDSCollectionETWBCLProvider Log = new CDSCollectionETWBCLProvider();

		// Token: 0x04002630 RID: 9776
		private const EventKeywords ALL_KEYWORDS = EventKeywords.All;

		// Token: 0x04002631 RID: 9777
		private const int CONCURRENTSTACK_FASTPUSHFAILED_ID = 1;

		// Token: 0x04002632 RID: 9778
		private const int CONCURRENTSTACK_FASTPOPFAILED_ID = 2;

		// Token: 0x04002633 RID: 9779
		private const int CONCURRENTDICTIONARY_ACQUIRINGALLLOCKS_ID = 3;

		// Token: 0x04002634 RID: 9780
		private const int CONCURRENTBAG_TRYTAKESTEALS_ID = 4;

		// Token: 0x04002635 RID: 9781
		private const int CONCURRENTBAG_TRYPEEKSTEALS_ID = 5;
	}
}
