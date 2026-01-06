using System;
using System.Collections.ObjectModel;
using System.Globalization;

namespace System.Data.ProviderBase
{
	// Token: 0x020002F3 RID: 755
	internal sealed class FieldNameLookup : BasicFieldNameLookup
	{
		// Token: 0x06002254 RID: 8788 RVA: 0x0009EB55 File Offset: 0x0009CD55
		public FieldNameLookup(string[] fieldNames, int defaultLocaleID)
			: base(fieldNames)
		{
			this._defaultLocaleID = defaultLocaleID;
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x0009EB65 File Offset: 0x0009CD65
		public FieldNameLookup(ReadOnlyCollection<string> columnNames, int defaultLocaleID)
			: base(columnNames)
		{
			this._defaultLocaleID = defaultLocaleID;
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x0009EB75 File Offset: 0x0009CD75
		public FieldNameLookup(IDataReader reader, int defaultLocaleID)
			: base(reader)
		{
			this._defaultLocaleID = defaultLocaleID;
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x0009EB88 File Offset: 0x0009CD88
		protected override CompareInfo GetCompareInfo()
		{
			CompareInfo compareInfo = null;
			if (-1 != this._defaultLocaleID)
			{
				compareInfo = CompareInfo.GetCompareInfo(this._defaultLocaleID);
			}
			if (compareInfo == null)
			{
				compareInfo = base.GetCompareInfo();
			}
			return compareInfo;
		}

		// Token: 0x0400171B RID: 5915
		private readonly int _defaultLocaleID;
	}
}
