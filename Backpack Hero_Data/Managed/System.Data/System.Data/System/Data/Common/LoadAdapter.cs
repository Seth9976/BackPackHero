using System;

namespace System.Data.Common
{
	// Token: 0x02000329 RID: 809
	internal sealed class LoadAdapter : DataAdapter
	{
		// Token: 0x0600260C RID: 9740 RVA: 0x000AC012 File Offset: 0x000AA212
		internal LoadAdapter()
		{
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x000AC01A File Offset: 0x000AA21A
		internal int FillFromReader(DataTable[] dataTables, IDataReader dataReader, int startRecord, int maxRecords)
		{
			return this.Fill(dataTables, dataReader, startRecord, maxRecords);
		}
	}
}
