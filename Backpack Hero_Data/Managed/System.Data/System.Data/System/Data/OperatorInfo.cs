using System;

namespace System.Data
{
	// Token: 0x0200009C RID: 156
	internal sealed class OperatorInfo
	{
		// Token: 0x06000A48 RID: 2632 RVA: 0x0002FCD2 File Offset: 0x0002DED2
		internal OperatorInfo(Nodes type, int op, int pri)
		{
			this._type = type;
			this._op = op;
			this._priority = pri;
		}

		// Token: 0x040006C5 RID: 1733
		internal Nodes _type;

		// Token: 0x040006C6 RID: 1734
		internal int _op;

		// Token: 0x040006C7 RID: 1735
		internal int _priority;
	}
}
