using System;
using System.Data.ProviderBase;

namespace System.Data.Odbc
{
	// Token: 0x020002A4 RID: 676
	internal sealed class OdbcReferenceCollection : DbReferenceCollection
	{
		// Token: 0x06001DCE RID: 7630 RVA: 0x0006E66A File Offset: 0x0006C86A
		public override void Add(object value, int tag)
		{
			base.AddItem(value, tag);
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x000920BA File Offset: 0x000902BA
		protected override void NotifyItem(int message, int tag, object value)
		{
			if (message != 0)
			{
				if (message == 1 && 1 == tag)
				{
					((OdbcCommand)value).RecoverFromConnection();
					return;
				}
			}
			else if (1 == tag)
			{
				((OdbcCommand)value).CloseFromConnection();
			}
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x0006E752 File Offset: 0x0006C952
		public override void Remove(object value)
		{
			base.RemoveItem(value);
		}

		// Token: 0x040015EB RID: 5611
		internal const int Closing = 0;

		// Token: 0x040015EC RID: 5612
		internal const int Recover = 1;

		// Token: 0x040015ED RID: 5613
		internal const int CommandTag = 1;
	}
}
