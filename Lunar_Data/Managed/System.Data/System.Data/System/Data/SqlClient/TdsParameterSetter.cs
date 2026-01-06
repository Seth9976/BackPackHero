using System;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x020001F5 RID: 501
	internal class TdsParameterSetter : SmiTypedGetterSetter
	{
		// Token: 0x060017D5 RID: 6101 RVA: 0x000721A0 File Offset: 0x000703A0
		internal TdsParameterSetter(TdsParserStateObject stateObj, SmiMetaData md)
		{
			this._target = new TdsRecordBufferSetter(stateObj, md);
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x00005AE9 File Offset: 0x00003CE9
		internal override bool CanGet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x060017D7 RID: 6103 RVA: 0x0000CD07 File Offset: 0x0000AF07
		internal override bool CanSet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x000721B5 File Offset: 0x000703B5
		internal override SmiTypedGetterSetter GetTypedGetterSetter(SmiEventSink sink, int ordinal)
		{
			return this._target;
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x000721BD File Offset: 0x000703BD
		public override void SetDBNull(SmiEventSink sink, int ordinal)
		{
			this._target.EndElements(sink);
		}

		// Token: 0x04001101 RID: 4353
		private TdsRecordBufferSetter _target;
	}
}
