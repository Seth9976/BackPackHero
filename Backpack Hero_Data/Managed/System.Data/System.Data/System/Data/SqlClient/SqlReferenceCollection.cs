using System;
using System.Data.ProviderBase;

namespace System.Data.SqlClient
{
	// Token: 0x020001CC RID: 460
	internal sealed class SqlReferenceCollection : DbReferenceCollection
	{
		// Token: 0x0600166C RID: 5740 RVA: 0x0006E66A File Offset: 0x0006C86A
		public override void Add(object value, int tag)
		{
			base.AddItem(value, tag);
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x0006E674 File Offset: 0x0006C874
		internal void Deactivate()
		{
			base.Notify(0);
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x0006E680 File Offset: 0x0006C880
		internal SqlDataReader FindLiveReader(SqlCommand command)
		{
			if (command == null)
			{
				return base.FindItem<SqlDataReader>(1, (SqlDataReader dataReader) => !dataReader.IsClosed);
			}
			return base.FindItem<SqlDataReader>(1, (SqlDataReader dataReader) => !dataReader.IsClosed && command == dataReader.Command);
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x0006E6DC File Offset: 0x0006C8DC
		internal SqlCommand FindLiveCommand(TdsParserStateObject stateObj)
		{
			return base.FindItem<SqlCommand>(2, (SqlCommand command) => command.StateObject == stateObj);
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x0006E70C File Offset: 0x0006C90C
		protected override void NotifyItem(int message, int tag, object value)
		{
			if (tag == 1)
			{
				SqlDataReader sqlDataReader = (SqlDataReader)value;
				if (!sqlDataReader.IsClosed)
				{
					sqlDataReader.CloseReaderFromConnection();
					return;
				}
			}
			else
			{
				if (tag == 2)
				{
					((SqlCommand)value).OnConnectionClosed();
					return;
				}
				if (tag == 3)
				{
					((SqlBulkCopy)value).OnConnectionClosed();
				}
			}
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x0006E752 File Offset: 0x0006C952
		public override void Remove(object value)
		{
			base.RemoveItem(value);
		}

		// Token: 0x04000EE1 RID: 3809
		internal const int DataReaderTag = 1;

		// Token: 0x04000EE2 RID: 3810
		internal const int CommandTag = 2;

		// Token: 0x04000EE3 RID: 3811
		internal const int BulkCopyTag = 3;
	}
}
