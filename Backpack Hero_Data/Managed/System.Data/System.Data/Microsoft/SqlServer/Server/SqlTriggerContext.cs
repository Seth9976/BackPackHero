using System;
using System.Data.Common;
using System.Data.SqlTypes;
using Unity;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Provides contextual information about the trigger that was fired. </summary>
	// Token: 0x020003BF RID: 959
	public sealed class SqlTriggerContext
	{
		// Token: 0x06002EE4 RID: 12004 RVA: 0x000CAE35 File Offset: 0x000C9035
		internal SqlTriggerContext(TriggerAction triggerAction, bool[] columnsUpdated, SqlXml eventInstanceData)
		{
			this._triggerAction = triggerAction;
			this._columnsUpdated = columnsUpdated;
			this._eventInstanceData = eventInstanceData;
		}

		/// <summary>Gets the number of columns contained by the data table bound to the trigger. This property is read-only.</summary>
		/// <returns>The number of columns contained by the data table bound to the trigger, as an integer. </returns>
		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06002EE5 RID: 12005 RVA: 0x000CAE54 File Offset: 0x000C9054
		public int ColumnCount
		{
			get
			{
				int num = 0;
				if (this._columnsUpdated != null)
				{
					num = this._columnsUpdated.Length;
				}
				return num;
			}
		}

		/// <summary>Gets the event data specific to the action that fired the trigger.</summary>
		/// <returns>The event data specific to the action that fired the trigger as a <see cref="T:System.Data.SqlTypes.SqlXml" /> if more information is available; null otherwise.</returns>
		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06002EE6 RID: 12006 RVA: 0x000CAE75 File Offset: 0x000C9075
		public SqlXml EventData
		{
			get
			{
				return this._eventInstanceData;
			}
		}

		/// <summary>Indicates what action fired the trigger.</summary>
		/// <returns>The action that fired the trigger as a <see cref="T:Microsoft.SqlServer.Server.TriggerAction" />.</returns>
		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06002EE7 RID: 12007 RVA: 0x000CAE7D File Offset: 0x000C907D
		public TriggerAction TriggerAction
		{
			get
			{
				return this._triggerAction;
			}
		}

		/// <summary>Returns true if a column was affected by an INSERT or UPDATE statement.</summary>
		/// <returns>true if the column was affected by an INSERT or UPDATE operation.</returns>
		/// <param name="columnOrdinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.InvalidOperationException">Called in the context of a trigger where the value of the <see cref="P:Microsoft.SqlServer.Server.SqlTriggerContext.TriggerAction" /> property is not Insert or Update.</exception>
		// Token: 0x06002EE8 RID: 12008 RVA: 0x000CAE85 File Offset: 0x000C9085
		public bool IsUpdatedColumn(int columnOrdinal)
		{
			if (this._columnsUpdated != null)
			{
				return this._columnsUpdated[columnOrdinal];
			}
			throw ADP.IndexOutOfRange(columnOrdinal);
		}

		// Token: 0x06002EE9 RID: 12009 RVA: 0x0000E24C File Offset: 0x0000C44C
		internal SqlTriggerContext()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001BB2 RID: 7090
		private TriggerAction _triggerAction;

		// Token: 0x04001BB3 RID: 7091
		private bool[] _columnsUpdated;

		// Token: 0x04001BB4 RID: 7092
		private SqlXml _eventInstanceData;
	}
}
