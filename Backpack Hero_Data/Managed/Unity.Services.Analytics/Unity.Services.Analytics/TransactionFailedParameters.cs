using System;

namespace Unity.Services.Analytics
{
	// Token: 0x02000016 RID: 22
	public struct TransactionFailedParameters
	{
		// Token: 0x04000075 RID: 117
		public string PaymentCountry;

		// Token: 0x04000076 RID: 118
		public long? EngagementID;

		// Token: 0x04000077 RID: 119
		public bool? IsInitiator;

		// Token: 0x04000078 RID: 120
		public string StoreID;

		// Token: 0x04000079 RID: 121
		public string StoreSourceID;

		// Token: 0x0400007A RID: 122
		public string TransactionID;

		// Token: 0x0400007B RID: 123
		public string StoreItemID;

		// Token: 0x0400007C RID: 124
		public string AmazonUserID;

		// Token: 0x0400007D RID: 125
		public string StoreItemSkuID;

		// Token: 0x0400007E RID: 126
		public string ProductID;

		// Token: 0x0400007F RID: 127
		public string GameStoreID;

		// Token: 0x04000080 RID: 128
		public TransactionServer? TransactionServer;

		// Token: 0x04000081 RID: 129
		public long? RevenueValidated;

		// Token: 0x04000082 RID: 130
		public string TransactionName;

		// Token: 0x04000083 RID: 131
		public TransactionType TransactionType;

		// Token: 0x04000084 RID: 132
		public Product ProductsReceived;

		// Token: 0x04000085 RID: 133
		public Product ProductsSpent;

		// Token: 0x04000086 RID: 134
		public string FailureReason;
	}
}
