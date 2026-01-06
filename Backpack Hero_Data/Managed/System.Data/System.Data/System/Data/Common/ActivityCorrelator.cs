using System;
using System.Globalization;

namespace System.Data.Common
{
	// Token: 0x02000378 RID: 888
	internal static class ActivityCorrelator
	{
		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06002B07 RID: 11015 RVA: 0x000BE1A3 File Offset: 0x000BC3A3
		internal static ActivityCorrelator.ActivityId Current
		{
			get
			{
				if (ActivityCorrelator.t_tlsActivity == null)
				{
					ActivityCorrelator.t_tlsActivity = new ActivityCorrelator.ActivityId();
				}
				return new ActivityCorrelator.ActivityId(ActivityCorrelator.t_tlsActivity);
			}
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x000BE1C0 File Offset: 0x000BC3C0
		internal static ActivityCorrelator.ActivityId Next()
		{
			if (ActivityCorrelator.t_tlsActivity == null)
			{
				ActivityCorrelator.t_tlsActivity = new ActivityCorrelator.ActivityId();
			}
			ActivityCorrelator.t_tlsActivity.Increment();
			return new ActivityCorrelator.ActivityId(ActivityCorrelator.t_tlsActivity);
		}

		// Token: 0x040019F2 RID: 6642
		[ThreadStatic]
		private static ActivityCorrelator.ActivityId t_tlsActivity;

		// Token: 0x02000379 RID: 889
		internal class ActivityId
		{
			// Token: 0x17000729 RID: 1833
			// (get) Token: 0x06002B09 RID: 11017 RVA: 0x000BE1E7 File Offset: 0x000BC3E7
			// (set) Token: 0x06002B0A RID: 11018 RVA: 0x000BE1EF File Offset: 0x000BC3EF
			internal Guid Id { get; private set; }

			// Token: 0x1700072A RID: 1834
			// (get) Token: 0x06002B0B RID: 11019 RVA: 0x000BE1F8 File Offset: 0x000BC3F8
			// (set) Token: 0x06002B0C RID: 11020 RVA: 0x000BE200 File Offset: 0x000BC400
			internal uint Sequence { get; private set; }

			// Token: 0x06002B0D RID: 11021 RVA: 0x000BE209 File Offset: 0x000BC409
			internal ActivityId()
			{
				this.Id = Guid.NewGuid();
				this.Sequence = 0U;
			}

			// Token: 0x06002B0E RID: 11022 RVA: 0x000BE223 File Offset: 0x000BC423
			internal ActivityId(ActivityCorrelator.ActivityId activity)
			{
				this.Id = activity.Id;
				this.Sequence = activity.Sequence;
			}

			// Token: 0x06002B0F RID: 11023 RVA: 0x000BE244 File Offset: 0x000BC444
			internal void Increment()
			{
				uint num = this.Sequence + 1U;
				this.Sequence = num;
			}

			// Token: 0x06002B10 RID: 11024 RVA: 0x000BE261 File Offset: 0x000BC461
			public override string ToString()
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", this.Id, this.Sequence);
			}
		}
	}
}
