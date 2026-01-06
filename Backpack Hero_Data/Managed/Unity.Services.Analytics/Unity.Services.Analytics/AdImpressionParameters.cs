using System;

namespace Unity.Services.Analytics
{
	// Token: 0x0200000A RID: 10
	public struct AdImpressionParameters
	{
		// Token: 0x04000026 RID: 38
		public AdCompletionStatus AdCompletionStatus;

		// Token: 0x04000027 RID: 39
		public AdProvider AdProvider;

		// Token: 0x04000028 RID: 40
		public string PlacementID;

		// Token: 0x04000029 RID: 41
		public string PlacementName;

		// Token: 0x0400002A RID: 42
		public AdPlacementType? PlacementType;

		// Token: 0x0400002B RID: 43
		public double? AdEcpmUsd;

		// Token: 0x0400002C RID: 44
		public string SdkVersion;

		// Token: 0x0400002D RID: 45
		public string AdImpressionID;

		// Token: 0x0400002E RID: 46
		public string AdStoreDstID;

		// Token: 0x0400002F RID: 47
		public string AdMediaType;

		// Token: 0x04000030 RID: 48
		public long? AdTimeWatchedMs;

		// Token: 0x04000031 RID: 49
		public long? AdTimeCloseButtonShownMs;

		// Token: 0x04000032 RID: 50
		public long? AdLengthMs;

		// Token: 0x04000033 RID: 51
		public bool? AdHasClicked;

		// Token: 0x04000034 RID: 52
		public string AdSource;

		// Token: 0x04000035 RID: 53
		public string AdStatusCallback;
	}
}
