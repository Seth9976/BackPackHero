using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200039B RID: 923
	internal class SmiMetaDataPropertyCollection
	{
		// Token: 0x06002C5E RID: 11358 RVA: 0x000C23DC File Offset: 0x000C05DC
		private static SmiMetaDataPropertyCollection CreateEmptyInstance()
		{
			SmiMetaDataPropertyCollection smiMetaDataPropertyCollection = new SmiMetaDataPropertyCollection();
			smiMetaDataPropertyCollection.SetReadOnly();
			return smiMetaDataPropertyCollection;
		}

		// Token: 0x06002C5F RID: 11359 RVA: 0x000C23EC File Offset: 0x000C05EC
		internal SmiMetaDataPropertyCollection()
		{
			this._properties = new SmiMetaDataProperty[3];
			this._isReadOnly = false;
			this._properties[0] = SmiMetaDataPropertyCollection.s_emptyDefaultFields;
			this._properties[1] = SmiMetaDataPropertyCollection.s_emptySortOrder;
			this._properties[2] = SmiMetaDataPropertyCollection.s_emptyUniqueKey;
		}

		// Token: 0x17000762 RID: 1890
		internal SmiMetaDataProperty this[SmiPropertySelector key]
		{
			get
			{
				return this._properties[(int)key];
			}
			set
			{
				if (value == null)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
				}
				this.EnsureWritable();
				this._properties[(int)key] = value;
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06002C62 RID: 11362 RVA: 0x000C245F File Offset: 0x000C065F
		internal bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
		}

		// Token: 0x06002C63 RID: 11363 RVA: 0x000C2467 File Offset: 0x000C0667
		internal void SetReadOnly()
		{
			this._isReadOnly = true;
		}

		// Token: 0x06002C64 RID: 11364 RVA: 0x000C2470 File Offset: 0x000C0670
		private void EnsureWritable()
		{
			if (this.IsReadOnly)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
		}

		// Token: 0x04001B20 RID: 6944
		private const int SelectorCount = 3;

		// Token: 0x04001B21 RID: 6945
		private SmiMetaDataProperty[] _properties;

		// Token: 0x04001B22 RID: 6946
		private bool _isReadOnly;

		// Token: 0x04001B23 RID: 6947
		private static readonly SmiDefaultFieldsProperty s_emptyDefaultFields = new SmiDefaultFieldsProperty(new List<bool>());

		// Token: 0x04001B24 RID: 6948
		private static readonly SmiOrderProperty s_emptySortOrder = new SmiOrderProperty(new List<SmiOrderProperty.SmiColumnOrder>());

		// Token: 0x04001B25 RID: 6949
		private static readonly SmiUniqueKeyProperty s_emptyUniqueKey = new SmiUniqueKeyProperty(new List<bool>());

		// Token: 0x04001B26 RID: 6950
		internal static readonly SmiMetaDataPropertyCollection EmptyInstance = SmiMetaDataPropertyCollection.CreateEmptyInstance();
	}
}
