using System;
using System.Xml.Schema;

namespace System.Data
{
	// Token: 0x020000EC RID: 236
	internal sealed class ConstraintTable
	{
		// Token: 0x06000CFC RID: 3324 RVA: 0x0003C458 File Offset: 0x0003A658
		public ConstraintTable(DataTable t, XmlSchemaIdentityConstraint c)
		{
			this.table = t;
			this.constraint = c;
		}

		// Token: 0x04000850 RID: 2128
		public DataTable table;

		// Token: 0x04000851 RID: 2129
		public XmlSchemaIdentityConstraint constraint;
	}
}
