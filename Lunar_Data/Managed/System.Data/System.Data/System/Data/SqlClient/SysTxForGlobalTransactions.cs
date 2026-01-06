using System;
using System.Reflection;
using System.Transactions;

namespace System.Data.SqlClient
{
	// Token: 0x020001EA RID: 490
	internal static class SysTxForGlobalTransactions
	{
		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x00071F2A File Offset: 0x0007012A
		public static MethodInfo EnlistPromotableSinglePhase
		{
			get
			{
				return SysTxForGlobalTransactions._enlistPromotableSinglePhase.Value;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x060017CB RID: 6091 RVA: 0x00071F36 File Offset: 0x00070136
		public static MethodInfo SetDistributedTransactionIdentifier
		{
			get
			{
				return SysTxForGlobalTransactions._setDistributedTransactionIdentifier.Value;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x060017CC RID: 6092 RVA: 0x00071F42 File Offset: 0x00070142
		public static MethodInfo GetPromotedToken
		{
			get
			{
				return SysTxForGlobalTransactions._getPromotedToken.Value;
			}
		}

		// Token: 0x04000F4E RID: 3918
		private static readonly Lazy<MethodInfo> _enlistPromotableSinglePhase = new Lazy<MethodInfo>(() => typeof(Transaction).GetMethod("EnlistPromotableSinglePhase", new Type[]
		{
			typeof(IPromotableSinglePhaseNotification),
			typeof(Guid)
		}));

		// Token: 0x04000F4F RID: 3919
		private static readonly Lazy<MethodInfo> _setDistributedTransactionIdentifier = new Lazy<MethodInfo>(() => typeof(Transaction).GetMethod("SetDistributedTransactionIdentifier", new Type[]
		{
			typeof(IPromotableSinglePhaseNotification),
			typeof(Guid)
		}));

		// Token: 0x04000F50 RID: 3920
		private static readonly Lazy<MethodInfo> _getPromotedToken = new Lazy<MethodInfo>(() => typeof(Transaction).GetMethod("GetPromotedToken"));
	}
}
