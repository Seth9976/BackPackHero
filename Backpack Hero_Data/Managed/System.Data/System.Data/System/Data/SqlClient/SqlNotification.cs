using System;

namespace System.Data.SqlClient
{
	// Token: 0x020001AB RID: 427
	internal class SqlNotification : MarshalByRefObject
	{
		// Token: 0x060014B3 RID: 5299 RVA: 0x0006676F File Offset: 0x0006496F
		internal SqlNotification(SqlNotificationInfo info, SqlNotificationSource source, SqlNotificationType type, string key)
		{
			this._info = info;
			this._source = source;
			this._type = type;
			this._key = key;
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x060014B4 RID: 5300 RVA: 0x00066794 File Offset: 0x00064994
		internal SqlNotificationInfo Info
		{
			get
			{
				return this._info;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x060014B5 RID: 5301 RVA: 0x0006679C File Offset: 0x0006499C
		internal string Key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x060014B6 RID: 5302 RVA: 0x000667A4 File Offset: 0x000649A4
		internal SqlNotificationSource Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x000667AC File Offset: 0x000649AC
		internal SqlNotificationType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x04000DB8 RID: 3512
		private readonly SqlNotificationInfo _info;

		// Token: 0x04000DB9 RID: 3513
		private readonly SqlNotificationSource _source;

		// Token: 0x04000DBA RID: 3514
		private readonly SqlNotificationType _type;

		// Token: 0x04000DBB RID: 3515
		private readonly string _key;
	}
}
