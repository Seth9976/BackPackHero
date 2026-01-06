using System;

namespace Unity.Services.Analytics
{
	// Token: 0x02000011 RID: 17
	public struct TransactionParameters
	{
		// Token: 0x04000055 RID: 85
		public string PaymentCountry;

		// Token: 0x04000056 RID: 86
		public string ProductID;

		// Token: 0x04000057 RID: 87
		public long? RevenueValidated;

		// Token: 0x04000058 RID: 88
		public string TransactionID;

		// Token: 0x04000059 RID: 89
		public string TransactionReceipt;

		// Token: 0x0400005A RID: 90
		public string TransactionReceiptSignature;

		// Token: 0x0400005B RID: 91
		public TransactionServer? TransactionServer;

		// Token: 0x0400005C RID: 92
		public string TransactorID;

		// Token: 0x0400005D RID: 93
		public string StoreItemSkuID;

		// Token: 0x0400005E RID: 94
		public string StoreItemID;

		// Token: 0x0400005F RID: 95
		public string StoreID;

		// Token: 0x04000060 RID: 96
		public string StoreSourceID;

		// Token: 0x04000061 RID: 97
		public string TransactionName;

		// Token: 0x04000062 RID: 98
		public TransactionType TransactionType;

		// Token: 0x04000063 RID: 99
		public Product ProductsReceived;

		// Token: 0x04000064 RID: 100
		public Product ProductsSpent;
	}
}
